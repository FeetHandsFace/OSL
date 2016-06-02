using System;
using System.Collections.Generic;

public class Merchant {

	public bool stolenFrom;
	public int shortChanged;
	public Inventory inven;
	public List<Secret> randomSecrets;//replace with an array of secrets

	public Merchant(Inventory invntry, Secret rando) {
		randomSecrets = new List<Secret>();
 		inven = invntry;
		stolenFrom = false;
		shortChanged = 0;
	}

	public Merchant(Inventory invntry, List<Secret> randos, bool stolen, int shortted) {
		randomSecrets = randos;
		inven = invntry;
		stolenFrom = stolen;
		shortChanged = shortted;
	}

	public void changeOutSecrets() {
		if (stolenFrom) {
			//search for a secret about the player in the persistant.playerSelectionList
		}else if(shortChanged > 0) {
			if (shortChanged == 2) {
				stolenFrom = true;
				//search for a secret about the player in the persistant.playerSelectionList

			} else { //this happens if shortChanged is 1
				//get only one old secret
			}
		} else {
			//get random secrets until you have at least one that is at most 2 days old
		}
	}

    public void beginTrade() {
		inven.initiateTrade(randomSecrets, this);
	}
}
