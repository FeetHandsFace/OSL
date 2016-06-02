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
	

	System.Random randomGenerator;

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
		randomGenerator = new System.Random();

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
			List<Secret> randomList;
			for (int i = 0; i < merchants.Length; i++) {
				randomList = MasterList.masterDictionary[randomGenerator.Next(1, MasterList.masterDictionary.Count)];
                merchants[i] = new Merchant(playerInventory, randomList[randomGenerator.Next(0, randomList.Count)]);
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
				playerInventory.secretsInventory[i].valueUpdate();
			}
		}
		masterList.newDay();
		//reload each merchant with new random secrets at the day change
		//When a merchant gets a secret all their daysAccquired should be set to 1 and the value updated
		//If player didn't watch news then pop the news stories of the stack for the day
	}

	// Update is called once per frame
	void Update () {
	
	}
}
