using System.Collections.Generic;

public class Coworker {

	int indexOfSecretColomn, indexOfSecretRow;
	public bool alreadySearching; 
	DialogueSystem dialogueSystem;
	Inventory inven;

	public Coworker(Inventory invntry, DialogueSystem dSystem) {
		inven = invntry;
		dialogueSystem = dSystem;
		indexOfSecretColomn = -1;  indexOfSecretRow = -1;
		alreadySearching = false;
	}

	public Coworker(int firstIndex, int secondIndex, Inventory invntry, DialogueSystem dSystem, bool searching) {
		inven = invntry;
		dialogueSystem = dSystem;
		indexOfSecretColomn = firstIndex; indexOfSecretRow = secondIndex;
		alreadySearching = searching;
	}

	public void beginConversation() {
		if(alreadySearching) {
			dialogueSystem.loadDialogueBlock("You have to give me some time to reasearch.");
		} else { 
			inven.giveToCoworker(this);
			switch(indexOfSecretColomn) {
				case -2://couldn't find another secret in the family
				dialogueSystem.loadDialogueBlock("Hey I wasn't able to find anything with what you gave me.");
				indexOfSecretColomn = -1;
				indexOfSecretRow = -1;
				break;
				case -1://waiting to get a secret from you
				dialogueSystem.loadDialogueBlock("What's up?");
				//After they are given a secret say "I'll see what I can do, or something non commital
				//If the coworker was just given a secret then if the player tries to talk to them throw up generic dismissal dialogue (make a box so it varies between coworkers,
				//or make it a random array of generic dismissals 
				break;
				default://coworker has a secret for the player
				dialogueSystem.loadDialogueBlock("I have something for you");
				Secret s = MasterList.masterDictionary[indexOfSecretColomn][indexOfSecretRow];
				s.dayAccquired = Calender.getDay;
				s.valueUpdate();
				inven.acquireHelper(s);
				indexOfSecretRow = -1;
				indexOfSecretColomn = -1;
				break;
			}
		} 
	}

	public void giveSecret(Secret toResearch) {
		dialogueSystem.loadDialogueBlock("Thanks, I'll see what I can do");
		alreadySearching = true;
		//there should probably be a probablilty that any secret given in this way can be leaked to the public
		List<Secret> familyList = MasterList.masterDictionary[toResearch.groupNumber - 1];
		int i = familyList.Count;
		while(i >= 0 || indexOfSecretRow < 0) {
			if(!familyList[i].wasGivenByCoworker && familyList[i] != toResearch) {
				indexOfSecretRow = i;
			}
			i--;
		}
		if(indexOfSecretRow < 0) {
			indexOfSecretRow = -2;
			indexOfSecretColomn = -2;
		}
	}
}
