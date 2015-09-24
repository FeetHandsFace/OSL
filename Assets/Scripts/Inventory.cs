using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour {

	List<Secret> tradeAndDevalue;
	List<Secret> secretsInventory;
	Secret upForTrade;

	//Generic secret icon for the inventory
	public Button generic;
	RectTransform secretButtonSize;

	public Button valueSortButton, groupSortButton, contents, playerOffering, tradeButton;
	public Image cullingMask;
	public Text descrptionBox;

	public Boss daBoss;
	public State getState {get {return state;}}
	State state;

	// Use this for initialization
	void Start() {
		daBoss = GetComponent<Boss>();
		secretsInventory = new List<Secret>();
		tradeAndDevalue = new List<Secret> ();
		state = State.IDLE;
		descrptionBox.text = "";
		secretButtonSize = generic.GetComponent<RectTransform>();
		
		for(int i = 0; i < secretsInventory.Count; i++) {
			secretButtonBuilder(secretsInventory[i]);
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

	public void initiateTrade(Secret upForTrade){
		state = State.TRADING;
		this.upForTrade = upForTrade;
		displayTradeWindow ();
		contents.GetComponentInChildren<Text> ().text = upForTrade.groupNumber.ToString ();
	}


	public void initiatePitch(Boss boss) {
		state = State.PITCHING;
	}

	public void endPitch() {
		state = State.IDLE;
		hideInventory();
	}

	//Adds secrets to the inventory after a trade.
	public void acquire (){
		if (!secretsInventory.Contains (upForTrade)) {
			acquireHelper(upForTrade);
		}
	}

	public void acquireHelper(Secret newlyAcquired){
		secretsInventory.Add (newlyAcquired);
		secretButtonBuilder (newlyAcquired);
	}

	void secretButtonBuilder(Secret toBeBuilt){
		Button temp = Instantiate (generic, new Vector3 ((secretButtonSize.sizeDelta.x * 1.2f * (secretsInventory.Count % 4)),- (secretButtonSize.sizeDelta.y * 1.2f * ((secretsInventory.Count / 4)+1))), Quaternion.identity) as Button;
		temp.transform.SetParent (cullingMask.transform, false);
		temp.GetComponent<DisplaySecret>().thisSecret = toBeBuilt;
		toBeBuilt.secretObject = temp;
	}
	
	//Can't lose secrets, they simlpy lose value.
	public void devalue(){
		//Devaluation will be represented by the number of days since being traded, so the 
		//secrets value will double every time they are traded 
		//offer.value *= 2; 
	}

	//Displays the inventory 
	public void goFromTradeToInventory(){
		hideTradeWindow ();
		displayInventory ();
	}

	//when a secret is choosen during a trade it is added to the pool and to the list of secrets to be traded and devalued
	//the secrets that are added are also represented in the trade pool
	public void goBackToTrade (Secret bid){
		//if trade and devalue does not contain secret then add it and display it otherwise remove it and stop displaying it
		if (tradeAndDevalue.Contains (bid)) {
			tradeAndDevalue.Remove (bid);
		} else {
			tradeAndDevalue.Add(bid);
		}
		displayTradeWindow ();
		hideInventory ();
		string temp = "";
		for(int i = 0; i < tradeAndDevalue.Count; i++) {
			temp += "\r\n" + tradeAndDevalue[i].groupName;
		}
		playerOffering.GetComponentInChildren<Text> ().text = temp;
	}

	void hideTradeWindow(){
		contents.gameObject.SetActive (false);
		playerOffering.gameObject.SetActive (false);
		tradeButton.gameObject.SetActive (false);
	}

	void displayTradeWindow(){
		contents.gameObject.SetActive (true);
		playerOffering.gameObject.SetActive (true);
		tradeButton.gameObject.SetActive (true);
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
		descrptionBox.gameObject.SetActive(false);
		cullingMask.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevel != 0) {
			if (Input.GetKeyUp ("i")) {
				switch(state){
				case State.TRADING:
					hideTradeWindow();
					displayInventory ();
					break;
				case State.IDLE:
					displayInventory ();
					break;
				case State.PITCHING:
					displayInventory();
					break;
				default:
					break;
				}
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
				default:
					break;
				}
			}
		}
	}
}

public enum State {TRADING, PITCHING, IDLE};

