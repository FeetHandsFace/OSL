public class Coworker {

	public bool avoidingProtagonist, introduced;
	public int ownSecretsRecieved;
	DialogueSystem dialogueSystem;
	Inventory inven;

	public Coworker(Inventory invntry, DialogueSystem dSystem) {
		inven = invntry;
		dialogueSystem = dSystem;
		avoidingProtagonist = false;
		introduced = false;
		ownSecretsRecieved = 0;
	}

	public Coworker(Inventory invntry, DialogueSystem dSystem, bool avoiding, bool wereIntroduced, int ownSecrets) {
		inven = invntry;
		dialogueSystem = dSystem;
		introduced = wereIntroduced;
		avoidingProtagonist = avoiding;
		ownSecretsRecieved = ownSecrets;
	}

	public void beginConversation() {
		dialogueSystem.loadDialogueBlock("What's up?");
		inven.giveToCoworker(this);
	}

	public void giveSecret(Secret toDevalue) {
		dialogueSystem.loadDialogueBlock("Thanks, I'll see what I can do");
		//devalue the traded secret
	}
}
