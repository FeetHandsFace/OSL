//using UnityEngine;
using System;
//using System.Collections;
using UnityEngine.UI;
//Need to add an int for the severity of the secret and a color code to corrispond to the secret
public class Secret : IComparable {

	public int daysSinceAccquired, trueDSA, severity, value, groupNumber, pitchCount;
	public string groupName, description, crimeTag, aishaRejection, lucaRejection, willRejection, alexRejection, rakeshRejection, mamiRejection, cissexistRejection, 
					acceptance, blackmailAcceptance, broadCastDialogue, delayedBroadCastDialogue;
	public bool wasTraded, wasBroadcast;

	public Button secretObject;
	public Inventory inven;

	//Constructs a secret
	/*secret format in masterfile
	 Secret severity (integer) hidden
	 Secret groupNumber (integer) hidden
	 Secret crime Tag (string) player facing
	 Secret description (string) player facing
	 Secret groupName (string) player facing
	 Aisha rejection of secret (string)
	 Luca rejection of secret (string)
	 Will rejection of secret (string)
	 Alex rejection of secret (string)
	 Rakesh rejection of secret (string)
	 mamai rejection of secret (string)
	 cissexist rejection of secret (string)
	 Acceptance of secret (string)
	 BlackMail acceptance of secret (string)
	 Secret Broadcast dialogue (string)
	 delayedBroadCastDialogue rejection of secret (string)*/
	public Secret(int trueDaysSinceAccquired, int severity, int groupNumber, string crimeTag, string description, string groupName, string aishaRejection, string lucaRejection, 
	              string willRejection, string alexRejection, string rakeshRejection, string mamiRejection, string cissexistRejection, string acceptance, string blackmailAcceptance, 
	              string broadCastDialogue, string delayedBroadCastDialogue) {
		daysSinceAccquired = 0;	//the player is not given access to the true number of days since the secret was first discovered
		trueDSA = trueDaysSinceAccquired;
		this.severity = severity;
		this.groupNumber = groupNumber;
		this.crimeTag = crimeTag;
		this.description = description;
		this.groupName = groupName;
		this.aishaRejection = aishaRejection;
		this.lucaRejection = lucaRejection;
		this.willRejection = willRejection;
		this.alexRejection = alexRejection;
		this.rakeshRejection = rakeshRejection;
		this.mamiRejection = mamiRejection;
		this.cissexistRejection = cissexistRejection;
		this.acceptance = acceptance;
		this.blackmailAcceptance = blackmailAcceptance;
		this.broadCastDialogue = broadCastDialogue;
		this.delayedBroadCastDialogue = delayedBroadCastDialogue;
		wasTraded = false; //I don't think i need this
		wasBroadcast = false; //maybe don't need this either, I will simply tank the value of the secret
		inven = Persistant.persist.GetComponent<Inventory> ();
		valueUpdate ();
	}

	public string pullRejection(string s) {
		switch (s) {
		case "Aisha Al-Suwaidi":
			return aishaRejection;
		case "Rakesh Jagirani":
			return rakeshRejection;
		case "Luca Mirzoyan":
			return lucaRejection;
		case "Will Prescott":
			return willRejection;
		case "Mami Watabe":
			return mamiRejection;
		case "Alex Wells":
			return alexRejection;
		}
		return "";
	}

	public void valueUpdate(){
		value = (severity * groupNumber) / trueDSA;
	}

	public void displayText(){
		inven.descrptionBox.text = groupName + "\r\n" + description + "\r\n" + "Days Since Accquired: " + daysSinceAccquired + "\r\n" + crimeTag;
	}

	public void hideText(){
		inven.descrptionBox.text = "";
	}

	//Compares two secrets based on their market value.
	public int CompareTo (object other){
		Secret temp = other as Secret;
		return temp.value - this.value; 
	}
}
