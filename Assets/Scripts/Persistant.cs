using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//Persistant scrpit, Loads and saves eveything, as well as keeping track of the master list of secrets and player list of secrets.
public class Persistant : MonoBehaviour {

	public static Persistant persist;

	bool newGame;
	
	//------------------------------------Information to be passed to other scripts
	public List<Secret> masterList, playerSelectionList;
	public TextAsset masterFile, playerSelectionFile, topNewsStories;
	public string[] unPublishableSecrets, sexistUnPublishableSecrets, cisSexistUnPublishableSecrets;
	

	System.Random rand = new System.Random();

	//---------------------------------------------Player Info
	[HideInInspector]
	public bool playerOuted, isFemale;
	[HideInInspector]
	public string mainCharacter;

	//----------------------------------------------Things to Save
	//Components
	Inventory playerInventory;
	DialogueSystem dialogueSystem;
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
		masterList = new List<Secret>();
		//Component scripts initilizded 
		dialogueSystem = GetComponent<DialogueSystem>();
		playerInventory = GetComponent<Inventory>();
		//Non-monobehavior scripts initilized 
		tvBroadcast = new TVBroadcast(dialogueSystem, topNewsStories.text);
		boss = new Boss(playerInventory, dialogueSystem, tvBroadcast, unPublishableSecrets, sexistUnPublishableSecrets, cisSexistUnPublishableSecrets);
		coWorkers = new Coworker[3] { new Coworker(playerInventory, dialogueSystem), new Coworker(playerInventory, dialogueSystem), new Coworker(playerInventory, dialogueSystem) }; // Start with 3 coworkers
		

		newGame = true; //check if save exists newGame = true/false

		if (newGame) {

			playerInventory.startNew(boss);

			string[] fileParser = masterFile.text.Split("\n"[0]);
			for (int i = 0; i < fileParser.Length; i += 16) {
				masterList.Add(new Secret(rand.Next(1, 5), Convert.ToInt32(fileParser[i]), Convert.ToInt32(fileParser[i + 1]), fileParser[i + 2], fileParser[i + 3], fileParser[i + 4],
										  fileParser[i + 5], fileParser[i + 6], fileParser[i + 7], fileParser[i + 8], fileParser[i + 9], fileParser[i + 10], fileParser[i + 11], fileParser[i + 12],
										  fileParser[i + 13], fileParser[i + 14], fileParser[i + 15]));
			}
			playerSelectionList = new List<Secret>();
			fileParser = playerSelectionFile.text.Split("\n"[0]);
			for (int i = 0; i < fileParser.Length; i += 16) {
				playerSelectionList.Add(new Secret(1, Convert.ToInt32(fileParser[i]), Convert.ToInt32(fileParser[i + 1]), fileParser[i + 2], fileParser[i + 3], fileParser[i + 4],
												   fileParser[i + 5], fileParser[i + 6], fileParser[i + 7], fileParser[i + 8], fileParser[i + 9], fileParser[i + 10], fileParser[i + 11],
												   fileParser[i + 12], fileParser[i + 13], fileParser[i + 14], fileParser[i + 15]));
			}

			merchants = new Merchant[2];
			for (int i = 0; i < merchants.Length; i++) {
				merchants[i] = new Merchant(playerInventory, masterList[rand.Next(0, 1)]);
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

	public void moveCharacterSecrets(string characterName){
		mainCharacter = characterName;
		Debug.Log ("start move");
		for (int i = 0; i < playerSelectionList.Count; i++) {
			if(playerSelectionList[i].groupName == characterName){
				playerInventory.acquireHelper(playerSelectionList[i]);
			}
		}
		Debug.Log ("finish move");

	}

	public void refreshWhileAsleep() {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
