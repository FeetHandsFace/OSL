using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Boss : MonoBehaviour {

	bool blackMailed, fired, talking;
	Persistant bossAndPlayerInfo;
	Inventory inven;
	TVBroadcast broadCast;
	string bossName;
	Queue<string> dialogueBlock;


	public string[] unPublishableSecrets, sexistUnPublishableSecrets, cisSexistUnPublishableSecrets;
	public Text dialogueBox;
	
	// Use this for initialization
	void Start () {
		talking = false;
		//bossName =  need to choose the boss' name
		bossAndPlayerInfo = Persistant.persist;
		inven = GetComponent<Inventory>();
		broadCast = GetComponent<TVBroadcast>();
		blackMailed = bossAndPlayerInfo.bossIsBlackMailed;
		fired = bossAndPlayerInfo.bossWasFired;
		dialogueBlock = new Queue<string>();
	}

	public void introduction(){

	}

	public void beginConversation() {
		talking = true;
		dialogueBox.text = "You have something for me?";
		inven.initiatePitch(this);
	}

	//The only secrets you are allowed to normally pitch are ones that support the system and status quo
	public void pitchSecret(Secret pitch){
		if (blackMailed) {
			//any and all secrets will be immediatly accepted except secrets about your boss
			//print accpetance and add to the queue
			if (pitch.groupName != bossName) {
				foreach (string s in pitch.blackmailAcceptance.Split("."[0])) { dialogueBlock.Enqueue(s); }
				dialogueBox.text = dialogueBlock.Dequeue() + ".";
				if (pitch.delayedBroadCastDialogue != "") broadCast.addStory(pitch.delayedBroadCastDialogue);
				broadCast.addStory(pitch.broadCastDialogue);
			} else {
				dialogueBox.text = "I won't; whatever else you want, but not this.";
			}
		} else if (pitch.groupName == bossName) {
			//blackmail the boss
			blackMailed = true;
			dialogueBox.text = "Okay. Whatever you want; just please don't spread this around";
		} else if (pitch.pitchCount > 0) {
			//print generic dialogue for second refusal
			dialogueBox.text = "I saw this already and said no; stop wasting my time.";
		} else if (pitch.pitchCount > 1) {
			//print generic dialogue for third refusal
			dialogueBox.text = "You are wasting my time; get the hell out of my office.";
		} else if (Array.Exists (unPublishableSecrets, delegate (string a) {return a == pitch.groupName;})) {
			//print rejection (applies to everyone)
			foreach (string s in pitch.pullRejection(bossAndPlayerInfo.mainCharacter).Split("."[0])) { dialogueBlock.Enqueue(s); }
			dialogueBox.text = dialogueBlock.Dequeue() + ".";
		} else if (bossAndPlayerInfo.isFemale && Array.Exists (sexistUnPublishableSecrets, delegate (string a) {return a == pitch.groupName;})) {
			//print rejection (applies to mami, aisha, and luca only)
			foreach (string s in pitch.pullRejection(bossAndPlayerInfo.mainCharacter).Split("."[0])) { dialogueBlock.Enqueue(s); }
			dialogueBox.text = dialogueBlock.Dequeue() + ".";
		} else if (bossAndPlayerInfo.playerOuted && Array.Exists (cisSexistUnPublishableSecrets, delegate (string a) {return a == pitch.groupName;})) {
			//print rejection (can apply to mami and alex)
			foreach (string s in pitch.pullRejection(bossAndPlayerInfo.mainCharacter).Split("."[0])) { dialogueBlock.Enqueue(s); }
			dialogueBox.text = dialogueBlock.Dequeue() + ".";
		} else {
			//print accept and add to the queue
			foreach (string s in pitch.acceptance.Split("."[0])) { dialogueBlock.Enqueue(s); }
			dialogueBox.text = dialogueBlock.Dequeue() + ".";
			if (pitch.delayedBroadCastDialogue != "") broadCast.addStory(pitch.delayedBroadCastDialogue);
			broadCast.addStory(pitch.broadCastDialogue);
		}
		pitch.pitchCount++;
		inven.endPitch();
	}

	// Update is called once per frame
	void Update () {
		if (talking && (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))) {
			if (dialogueBlock.Count != 0) {
				dialogueBox.text = dialogueBlock.Dequeue() + ".";
			} else {
				dialogueBox.text = "";
			}
		}
	}
}
