public class Coworker {

	public bool avoidingProtagonist, introduced;
	public int ownSecretsRecieved;
	int indexOfSecretColomn, indexOfSecretRow;
	DialogueSystem dialogueSystem;
	Inventory inven;

	public Coworker(Inventory invntry, DialogueSystem dSystem) {
		inven = invntry;
		dialogueSystem = dSystem;
		avoidingProtagonist = false;
		introduced = false;
		ownSecretsRecieved = 0;
		indexOfSecretColomn = -1;  indexOfSecretRow = -1;
	}

	public Coworker(int firstIndex, int secondIndex, Inventory invntry, DialogueSystem dSystem, bool avoiding, bool wereIntroduced, int ownSecrets) {
		inven = invntry;
		dialogueSystem = dSystem;
		introduced = wereIntroduced;
		avoidingProtagonist = avoiding;
		ownSecretsRecieved = ownSecrets;
		indexOfSecretColomn = firstIndex; indexOfSecretRow = secondIndex;
	}

	public void beginConversation() {
		inven.giveToCoworker(this);
		switch(indexOfSecretColomn) {
			case -2://couldn't find another secret in the family
				dialogueSystem.loadDialogueBlock("Hey I wasn't able to find anything with what you gave me.");
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
				s.dayAccquired = Calender.getDay - 2;
				s.valueUpdate();
				inven.acquireHelper(s);
				indexOfSecretRow = -1;
				indexOfSecretColomn = -1;
				break;
		}
	}

	public void giveSecret(int toResearch) {
		dialogueSystem.loadDialogueBlock("Thanks, I'll see what I can do");
		//there should probably be a probablilty that any secret given in this way can be leaked to the public
		//if the given index of the secret has already been given to the coworker they should let the player know, keep this as a boolean in the secret script
	}
}
