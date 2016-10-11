using System;
using UnityEngine.UI;
//When these first secrets are built the reference to secretObject is null
public class Secret : IComparable {

	public int dayAccquired, severity, groupNumber, pitchCount;
	public float value;
    public string groupName, description, crimeTag,  rejection, acceptance, blackmailAcceptance, broadCastDialogue, delayedBroadCastDialogue;
	public bool wasBroadcast, wasGivenByCoworker;

	public Button secretObject;
	public Inventory inven;

	//Constructs a secret
	/*secret format in masterfile
	 Secret severity (integer) hidden
	 Secret groupNumber (integer) hidden
	 Secret crime Tag (string) player facing
	 Secret description (string) player facing
	 Secret groupName (string) player facing
	 rejection of secret (string)
	 Acceptance of secret (string)
	 BlackMail acceptance of secret (string)
	 Secret Broadcast dialogue (string)
	 delayedBroadCastDialogue rejection of secret (string)*/
	public Secret(int dayAccquired, int severity, int groupNumber, string crimeTag, string description, string groupName, string rejection, string acceptance, string blackmailAcceptance, 
	              string broadCastDialogue, string delayedBroadCastDialogue, bool wasGivenByCoworker) {
		this.dayAccquired = dayAccquired;
		this.severity = severity;
		this.groupNumber = groupNumber;
		this.crimeTag = crimeTag;
		this.description = description;
		this.groupName = groupName;
		this.rejection = rejection;
		this.acceptance = acceptance;
		this.blackmailAcceptance = blackmailAcceptance;
		this.broadCastDialogue = broadCastDialogue;
		this.delayedBroadCastDialogue = delayedBroadCastDialogue;
		this.wasGivenByCoworker = wasGivenByCoworker; //I don't think i need this
		wasBroadcast = false; //maybe don't need this either, I will simply tank the value of the secret
		inven = Persistant.persist.GetComponent<Inventory> ();
		valueUpdate ();
	}

	public void valueUpdate(){
		if (dayAccquired == 0) {
			value = Single.PositiveInfinity;
		} else {
			value = (severity * groupNumber) / (Calender.getDay - dayAccquired);
		}
	}

	public void displayText(){
		inven.descrptionBox.text = groupName + "\r\n" + description + "\r\n" + "Days Since Accquired: " + dayAccquired + "\r\n" + crimeTag;
	}

	public void hideText(){
		inven.descrptionBox.text = "";
	}

	//Compares two secrets based on their market value.
	public int CompareTo (object other){
		Secret temp = other as Secret;
		return (int) (temp.value - this.value); 
	}
}
