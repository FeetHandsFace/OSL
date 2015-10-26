using System;

public class Boss {

	public bool blackMailed, fired, angry;
	Persistant bossAndPlayerInfo;
	Inventory inven;
	TVBroadcast broadCast;
	DialogueSystem dialogueSystem;
	string bossName;


	string[] unPublishableSecrets, sexistUnPublishableSecrets, cisSexistUnPublishableSecrets;

	public Boss(Inventory invntry, DialogueSystem dSystem, TVBroadcast tvBroadcast, string[] uPS, string[] sUPS, string[] cUPS) {
		//bossName =  need to choose the boss' name
		bossAndPlayerInfo = Persistant.persist;
		inven = invntry;
		dialogueSystem = dSystem;
		broadCast = tvBroadcast;
		unPublishableSecrets = uPS;
		sexistUnPublishableSecrets = sUPS;
		cisSexistUnPublishableSecrets = cUPS;
		blackMailed = false;
		fired = false;
		angry = false;
	}

	public Boss(Inventory invntry, DialogueSystem dSystem, TVBroadcast tvBroadcast, string[] uPS, string[] sUPS, string[] cUPS, bool wasBlackMailed, bool wasFired, bool isAngry) {
		//bossName =  need to choose the boss' name
		bossAndPlayerInfo = Persistant.persist;
		inven = invntry;
		dialogueSystem = dSystem;
		broadCast = tvBroadcast;
		unPublishableSecrets = uPS;
		sexistUnPublishableSecrets = sUPS;
		cisSexistUnPublishableSecrets = cUPS;
		blackMailed = wasBlackMailed;
		fired = wasFired;
		angry = isAngry;
	}

	public void introduction(){

	}

	public void beginConversation() {
		dialogueSystem.loadDialogueBlock("You have something for me?");
		inven.initiatePitch();
	}

	//The only secrets you are allowed to normally pitch are ones that support the system and status quo
	public void pitchSecret(Secret pitch){
		if (blackMailed) {
			//any and all secrets will be immediatly accepted except secrets about your boss
			//print accpetance and add to the queue
			if (pitch.groupName != bossName) {
				dialogueSystem.loadDialogueBlock(pitch.blackmailAcceptance);
				if (pitch.delayedBroadCastDialogue != "") broadCast.addStory(pitch.delayedBroadCastDialogue);
				broadCast.addStory(pitch.broadCastDialogue);
			} else {
				dialogueSystem.loadDialogueBlock("I won't; whatever else you want, but not this.");
			}
		} else if (pitch.groupName == bossName) {
			//blackmail the boss
			blackMailed = true;
			dialogueSystem.loadDialogueBlock("Okay. Whatever you want; just please don't spread this around");
		}else if(pitch.groupName == bossAndPlayerInfo.mainCharacter) {
			dialogueSystem.loadDialogueBlock("You are not news.");
		} else if (pitch.pitchCount > 0) {
			//print generic dialogue for second refusal
			dialogueSystem.loadDialogueBlock("I saw this already and said no; stop wasting my time.");
		} else if (pitch.pitchCount > 1) {
			//print generic dialogue for third refusal
			dialogueSystem.loadDialogueBlock("You are wasting my time; get the hell out of my office.");
		} else if (Array.Exists (unPublishableSecrets, delegate (string a) {return a == pitch.groupName;})) {
			//print rejection (applies to everyone)
			dialogueSystem.loadDialogueBlock(pitch.pullRejection(bossAndPlayerInfo.mainCharacter));
		} else if (bossAndPlayerInfo.isFemale && Array.Exists (sexistUnPublishableSecrets, delegate (string a) {return a == pitch.groupName;})) {
			//print rejection (applies to mami, aisha, and luca only)
			dialogueSystem.loadDialogueBlock(pitch.pullRejection(bossAndPlayerInfo.mainCharacter));
		} else if (bossAndPlayerInfo.playerOuted && Array.Exists (cisSexistUnPublishableSecrets, delegate (string a) {return a == pitch.groupName;})) {
			//print rejection (can apply to mami and alex)
			dialogueSystem.loadDialogueBlock(pitch.pullRejection(bossAndPlayerInfo.mainCharacter));

		} else {
			//print accept and add to the queue
			dialogueSystem.loadDialogueBlock(pitch.acceptance);
			if (pitch.delayedBroadCastDialogue != "") broadCast.addStory(pitch.delayedBroadCastDialogue);
			broadCast.addStory(pitch.broadCastDialogue);
		}
		pitch.pitchCount++;
		inven.endPitch();
	}
}
