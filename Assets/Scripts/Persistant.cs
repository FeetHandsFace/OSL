using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Persistant scrpit, Loads and saves eveything, as well as keeping track of the master list of secrets and player list of secrets.
public class Persistant : MonoBehaviour {

	public static Persistant persist;

	bool newGame;
	
	//------------------------------------Information to be passed to other scripts
	public TextAsset topNewsStories;
	public string[] unPublishableSecrets;
	


	//----------------------------------------------Things to Save
	//Components
	Inventory playerInventory;
	DialogueSystem dialogueSystem;
	MasterList masterList;
	Calender calender;
	//Non-monobehavior scripts
	public Boss boss;
	public Coworker[] coWorkers;
	public Merchant[] merchants;
	public TVBroadcast tvBroadcast;
	//----------------------------------------------------------

	void Awake () {
		if (persist == null) {
			DontDestroyOnLoad (gameObject);
			persist = this;
		} else if (persist != this) {
			Destroy(gameObject);
		}
	}

	void Start() {

		//Component scripts initilizded 
		masterList = GetComponent<MasterList>();
		dialogueSystem = GetComponent<DialogueSystem>();
		playerInventory = GetComponent<Inventory>();
		calender = GetComponent<Calender>();
		//Non-monobehavior scripts initilized 
		tvBroadcast = new TVBroadcast(dialogueSystem, topNewsStories.text);
		boss = new Boss(playerInventory, dialogueSystem, tvBroadcast, unPublishableSecrets);
		coWorkers = new Coworker[3] { new Coworker(playerInventory, dialogueSystem), new Coworker(playerInventory, dialogueSystem), new Coworker(playerInventory, dialogueSystem) }; // Start with 3 coworkers
		

		newGame = true; //check if save exists newGame = true/false

		if (newGame) {

			playerInventory.startNew(boss);
			masterList.startNew();
			//Initilize the merchants and give them a reference to the player's inventory and a random secret
			merchants = new Merchant[2];
			for (int i = 0; i < merchants.Length; i++) {
                merchants[i] = new Merchant(playerInventory);
			}
		} else {
			Load();
		}

	}

	public void Load() {
		//playerInventory.loadSave(boss);
		//boss.loadSave();
		//for (int i = 0; i < coWorkers.Length; i++) { coWorkers[i].loadSave(); }
		//for(int i = 0; i < merchants.Lenght; i++} { merchants[i].loadSave();}
		//tvBroadcast.loadSave();
	}

	public void Save() {

	}

	Secret[] textAssetParser(TextAsset rawFile) {
		string[] rawStrings = rawFile.text.Split("\n"[0]);
		Secret[] builtSecrets = new Secret[(rawStrings.Length / 16)];

		return builtSecrets;
	}

	public void sleepCycle() {
		calender.changeDay();
		for(int i = 0; i < playerInventory.secretsInventory.Count; i++) {
			if (playerInventory.secretsInventory[i].dayAccquired != 0) {
				Debug.Log(playerInventory.secretsInventory[i].dayAccquired + " day accquired");
				Debug.Log(Calender.getDay);
				playerInventory.secretsInventory[i].valueUpdate();
			}
		}
		masterList.newDay();
		for(int i = 0; i < merchants.Length; i++) {
			merchants[i].hasTradedToday = false;
		}
		for(int i = 0; i < coWorkers.Length; i++) {
			coWorkers[i].alreadySearching = false;
		}
		//Should a merchant change out their secrets every day?
		//When a merchant gets a secret all their daysAccquired should be set to 1 and the value updated
		//If player didn't watch news then pop the news stories of the stack for the day
	}

	// Update is called once per frame
	void Update () {
	
	}
}
