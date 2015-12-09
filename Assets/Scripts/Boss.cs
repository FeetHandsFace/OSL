using System;

public enum BossState {CALM, ANGRY, CALM1, ANGRY1, CALM2, PERMANGRY}

public class Boss {

	public bool blackMailed, fired;
	public BossState bossState;
	Persistant bossAndPlayerInfo;
	Inventory inven;
	TVBroadcast broadCast;
	DialogueSystem dialogueSystem;
	string bossName;

	SpeakToBoss speakToBoss;

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
		bossState = BossState.CALM;
	}

	public Boss(Inventory invntry, DialogueSystem dSystem, TVBroadcast tvBroadcast, string[] uPS, string[] sUPS, string[] cUPS, bool wasBlackMailed, bool wasFired, BossState bState) {
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
		bossState = bState;
	}

	public void introduction(){

	}

	public void beginConversation(SpeakToBoss stb) {
		speakToBoss = stb;
		if (blackMailed) {
			dialogueSystem.loadDialogueBlock("You have something for me?");
			inven.initiatePitch();
		} else { 
			switch (bossState) {
			case BossState.CALM://Not angry
				dialogueSystem.loadDialogueBlock("You have something for me?");
				inven.initiatePitch();
				break;
			case BossState.ANGRY://Got angry once and has not expressed it 

				break;
			case BossState.CALM1://Got angry once and has already expressed it 
				break;
			case BossState.ANGRY1://Got angry twice and has not expressed it 
				//angry++;
				break;
			case BossState.CALM2://Got angry twice and has already expressed it
				break;
			case BossState.PERMANGRY://Got angry third time and has not expressed it 
				//angry++;
				break;
			default:
				break;
			}
		}
	}

	//The only secrets you are allowed to normally pitch are ones that support the system and status quo
	public void pitchSecret(Secret pitch){
		inven.endPitch();
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
			//tank the value of the given secret and mark as offered to the boss
			pitch.wasBroadcast = true;
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
			//tank the value of the given secret and mark as broadcast for the player
			pitch.wasBroadcast = true;
		}
		pitch.pitchCount++;
		speakToBoss.sphereCollider.enabled = false;
	}
}
