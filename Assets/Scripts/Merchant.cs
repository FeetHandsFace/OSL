using System;
using System.Collections.Generic;

public class Merchant {

	public bool stolenFrom, hasTradedToday;
	public int shortChanged;
	public Inventory inven;
	public List<Secret> randomSecrets;//replace with an array of secrets

	public Merchant(Inventory invntry) {
		randomSecrets = new List<Secret>();
 		inven = invntry;
		stolenFrom = false;
		hasTradedToday = false;
		shortChanged = 0;
		//---IMPORTANT-----------------Replace this with specific start of game secrets for each merchant
		List<Secret> randomList;
		for(int i = 0; i < 3; i++) {
			randomList = MasterList.masterDictionary[MasterList.randomGenerator.Next(1, MasterList.masterDictionary.Count)];
			randomSecrets.Add(randomList[MasterList.randomGenerator.Next(0, randomList.Count)]);
		}
		//----------------------------------------------------------------------------------------------
	}

	public Merchant(Inventory invntry, List<Secret> randos, bool stolen, bool hasTraded, int shortted) {
		randomSecrets = randos;
		inven = invntry;
		stolenFrom = stolen;
		hasTradedToday = hasTraded;
		shortChanged = shortted;
	}

	public void changeOutSecrets() {
		randomSecrets.Clear();
		List<Secret> randomList;
		if(stolenFrom) {
			//search for a secret about the player in the persistant.playerSelectionList
		}else if(shortChanged > 0) {
			if (shortChanged == 2) {
				stolenFrom = true;
				//search for a secret about the player in the persistant.playerSelectionList

			} else { //this happens if shortChanged is 1
				//get only one old secret
			}
		} else {
			//get random secrets until you have three secrets
			for(int i = 0; i < 3; i++) {
				randomList = MasterList.masterDictionary[MasterList.randomGenerator.Next(1, MasterList.masterDictionary.Count)];
				randomSecrets.Add(randomList[MasterList.randomGenerator.Next(0, randomList.Count)]);
			}
		}
		hasTradedToday = true;
	}

    public void beginTrade() {
		inven.initiateTrade(randomSecrets, this);
	}
}
