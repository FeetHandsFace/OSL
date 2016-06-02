using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public enum State { TRADING, PITCHING, PITCHING_COWORKER, IDLE };

public class Inventory : MonoBehaviour {

	public float valueOfTakenSecrets;

	public List<Secret> secretsInventory;

	//Generic secret icon for the inventory
	public Button generic;
	RectTransform secretButtonSize;

	public Button valueSortButton, groupSortButton;
	public Image tradeContents, playerOffering, cullingMask;
	public Text descrptionBox, scrollOverInfo;

	public Boss daBoss;
	public Coworker coWorker;
	public Merchant merchant;
	public static State getState {get {return state;}}
	static State state;

	//If Player just went to world map check with the merchant they dealt with. If the merchant is a silent merchant then evealute the secrets the player traded against the ones they took
	//mark the merchant as stolen from or short changed accordingly
	void OnLevelWasLoaded(int level) {
		state = State.IDLE;
		if (level == 3 && merchant != null) {
			float paymentValue = 0;
			playerOffering.gameObject.SetActive(true);
			BurnerSecret[] playerPayment = playerOffering.GetComponentsInChildren<BurnerSecret>();
			playerOffering.gameObject.SetActive(false);
			for(int i = 0; i < playerPayment.Length; i++) {
				if (merchant.randomSecrets.Contains(playerPayment[i].secretData)) {
					if(playerPayment.Length == 1)merchant.stolenFrom = true;
				} else {
					if(!playerPayment[i].secretData.wasBroadcast) paymentValue += playerPayment[i].secretData.value;
				}
				if (playerPayment[i].secretData.dayAccquired == 0) playerPayment[i].secretData.dayAccquired = Calender.getDay;
			}
			if(paymentValue == 0) {
				merchant.stolenFrom = true;
			}else if(paymentValue < valueOfTakenSecrets) {
				merchant.shortChanged++;
				valueOfTakenSecrets = 0f;
			}

			//make the merchant behave accordingly for the next session i.e. get new random secrets , or get only one secret if shortchanged, or get one of the players secrets if stolen from
			merchant.changeOutSecrets();
			merchant = null;
		}
	}

	public void startNew(Boss boss) {
		daBoss = boss;
		state = State.IDLE;
		secretButtonSize = generic.GetComponent<RectTransform>();

		secretsInventory = new List<Secret>();
	}

	public void loadSave(Boss boss, List<Secret> secrets) {
		daBoss = boss;
		state = State.IDLE;
		secretButtonSize = generic.GetComponent<RectTransform>();

		secretsInventory = secrets;
		for (int i = 0; i < secretsInventory.Count; i++) {
			acquireHelper(secretsInventory[i]);
		}
	}

	//Sorts the secretsInventory based on the market value of a secert.
	public void valueSort(){
		secretsInventory.Sort ();		
		for(int i = 0; i < secretsInventory.Count; i++) {
			secretsInventory[i].secretObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((secretButtonSize.sizeDelta.x * 1.2f *((i%4)+1)),- (secretButtonSize.sizeDelta.y * 1.2f * ((i/4)+1)));
		}
	}
	
	//Sorts the secretsInventory based on story grouping. 
	public void groupSort(){
		secretsInventory.Sort(delegate (Secret a, Secret b){
			return a.groupNumber - b.groupNumber;
		});
		for(int i = 0; i < secretsInventory.Count; i++) {
			secretsInventory[i].secretObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((secretButtonSize.sizeDelta.x * 1.2f *((i%4)+1)), - (secretButtonSize.sizeDelta.y * 1.2f * ((i/4)+1)));
		}
	}

	//anytime a player clicks on a trade object this method runs
	public void initiateTrade(List<Secret> upForTrade, Merchant mrchnt){
		//If the player inventory is up then don't display the trade window
		if(!cullingMask.gameObject.activeSelf)displayTradeWindow();
		state = State.TRADING;
		if (merchant != mrchnt) {
			merchant = mrchnt;
			RectTransform rectT;
			for(int i = 0; i < upForTrade.Count; i++) {
				GameObject obj = ObjectPooler.current.getPooledObject();
				obj.transform.SetParent(tradeContents.transform, false);
				BurnerSecret burner = obj.GetComponent<BurnerSecret>();
				burner.secretData = upForTrade[i];
				burner.mouseInfoText = scrollOverInfo;
				obj.SetActive(true);
				rectT = obj.GetComponent<RectTransform>();
				rectT.anchoredPosition = new Vector2(i * 50 - 100, rectT.anchoredPosition.y);
			}
		}
	}
	//Used when moving out of the collider of a trade object
	public void endTrade() {
		state = State.IDLE;
		hideTradeWindow();
	}
	//Used when player starts talking to boss
	public void initiatePitch() {
		state = State.PITCHING;
	}
	//Used when player gives something to the boss
	public void endPitch() {
		state = State.IDLE;
		hideInventory();
	}
	//Used when player starts talking to a coworker
	public void giveToCoworker(Coworker cw) {
		coWorker = cw;
		state = State.PITCHING_COWORKER;
	}

	public void acquireHelper(Secret newlyAcquired){
		secretsInventory.Add (newlyAcquired);
		secretButtonBuilder (newlyAcquired);
	}

	void secretButtonBuilder(Secret toBeBuilt){
		Button temp = Instantiate (generic, new Vector3 ((secretButtonSize.sizeDelta.x * 1.2f * (secretsInventory.Count % 5)),- (secretButtonSize.sizeDelta.y * 1.2f * ((secretsInventory.Count / 5)+1))), Quaternion.identity) as Button;
		temp.transform.SetParent (cullingMask.transform, false);
		temp.GetComponent<DisplaySecret>().thisSecret = toBeBuilt;
		toBeBuilt.secretObject = temp;
	}
	
	//Can't lose secrets, they simlpy lose value.
	public void devalue(){
		//Devaluation will be represented by the number of days since being traded, so the 
		//secrets value will double every day you have them
		//offer.value *= 2; 
	}

	//Displays the inventory 
	public void goFromTradeToInventory(){
		hideTradeWindow ();
		displayInventory ();
	}

	//Loads chosen secret from inventory onto burner drive then puts it in trading window
	public void goBackToTrade (Secret bid){
		GameObject obj = ObjectPooler.current.getPooledObject();
		obj.transform.SetParent(playerOffering.transform, false);
		//randomly position burner in trade window
		obj.transform.localPosition = new Vector2(Random.Range(-playerOffering.rectTransform.sizeDelta.x/2, playerOffering.rectTransform.sizeDelta.x / 2), Random.Range(-playerOffering.rectTransform.sizeDelta.y / 2, playerOffering.rectTransform.sizeDelta.y / 2));
		BurnerSecret burner = obj.GetComponent<BurnerSecret>();
		burner.secretData = bid;
		burner.mouseInfoText = scrollOverInfo;
		obj.SetActive(true);
		displayTradeWindow();
		hideInventory();
	}

	void hideTradeWindow(){
		tradeContents.gameObject.SetActive (false);
		playerOffering.gameObject.SetActive (false);
		scrollOverInfo.gameObject.SetActive(false);
	}

	void displayTradeWindow(){
		tradeContents.gameObject.SetActive (true);
		playerOffering.gameObject.SetActive (true);
		scrollOverInfo.gameObject.SetActive(true);
	}

	void displayInventory(){
		valueSortButton.gameObject.SetActive(true);
		groupSortButton.gameObject.SetActive(true);
		descrptionBox.gameObject.SetActive(true);
		cullingMask.gameObject.SetActive (true);
	}

	void hideInventory(){
		valueSortButton.gameObject.SetActive(false);
		groupSortButton.gameObject.SetActive(false);
		descrptionBox.text = "";
		descrptionBox.gameObject.SetActive(false);
		cullingMask.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//Need to stop people from opening inventory in first and second scenes-------------------------------IMPORTANT!!!!!!!!!-------------------------------------------
		if (Input.GetKeyUp ("i")) {
			Debug.Log(state);
			switch (state) {
			case State.TRADING:
				if (cullingMask.gameObject.activeSelf) { //If your inventory is up while trading and you press i then bring trade back up
					hideInventory();
					displayTradeWindow();
				} else {//If the trade table is up while trading and you press bring up inventory
					goFromTradeToInventory();
				}
				break;
			case State.IDLE:
				displayInventory ();
				break;
			case State.PITCHING:
				displayInventory();
				break;
			case State.PITCHING_COWORKER:
				displayInventory();
				break;
			default:
				break;
			}
			Debug.Log(state);
		}
		if (Input.GetKeyUp ("space")){
			switch(state){
			case State.TRADING:
				hideTradeWindow();
				hideInventory (); 
				state = State.IDLE;
				break;
			case State.IDLE:
				hideInventory ();
				break;
			case State.PITCHING:
				hideInventory();
				break;
			case State.PITCHING_COWORKER:
				hideInventory();
				break;
			default:
				break;
			}
		}
	}
}


