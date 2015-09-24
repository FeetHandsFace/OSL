using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//Persistant scrpit, Loads and saves eveything, as well as keeping track of the master list of secrets and player list of secrets.
public class Persistant : MonoBehaviour {

	public static Persistant persist; 
	public List<Secret> masterList, playerSelectionList;
	public TextAsset masterFile, playerSelectionFile; 
	Inventory playerInventory;
	System.Random rand = new System.Random();
	public bool bossIsBlackMailed, bossWasFired, playerOuted, isFemale;
	public string mainCharacter;

	void Awake () {
		if (persist == null) {
			DontDestroyOnLoad (gameObject);
			persist = this;
		} else if (persist != this) {
			Destroy(gameObject);
		}
		masterList = new List<Secret> ();
		playerInventory = GetComponent<Inventory> ();
		string[] fileParser = masterFile.text.Split("\n"[0]);
		for(int i = 0; i < fileParser.Length; i += 16){
			masterList.Add(new Secret(rand.Next(1, 5), Convert.ToInt32(fileParser[i]), Convert.ToInt32(fileParser[i+1]), fileParser[i+2], fileParser[i+3], fileParser[i+4], 
			                          fileParser[i+5], fileParser[i+6], fileParser[i+7], fileParser[i+8], fileParser[i+9], fileParser[i+10], fileParser[i+11], fileParser[i+12], 
			                          fileParser[i+13], fileParser[i+14], fileParser[i+15]));
		}
		playerSelectionList = new List<Secret> ();
		fileParser = playerSelectionFile.text.Split ("\n" [0]);
		for(int i = 0; i < fileParser.Length; i += 16){
			playerSelectionList.Add(new Secret(1, Convert.ToInt32(fileParser[i]), Convert.ToInt32(fileParser[i+1]), fileParser[i+2], fileParser[i+3], fileParser[i+4], 
			                                   fileParser[i+5], fileParser[i+6], fileParser[i+7], fileParser[i+8], fileParser[i+9], fileParser[i+10], fileParser[i+11], 
			                                   fileParser[i+12], fileParser[i+13], fileParser[i+14], fileParser[i+15]));
		}
		bossWasFired = false;
		bossIsBlackMailed = false;
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
