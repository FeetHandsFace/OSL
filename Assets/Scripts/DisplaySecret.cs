using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplaySecret : MonoBehaviour {

	public Secret thisSecret;
	public Image img;

	void Start() {
		img = GetComponent<Image>();
	}

	public void mouseEnter(){
		thisSecret.displayText ();
	}

	public void mouseExit(){
		thisSecret.hideText ();
	}

	public void bidSecret(){
		Debug.Log("display state " + Inventory.getState);
		switch (Inventory.getState) {
		case State.IDLE:
			break;
		case State.PITCHING:
			thisSecret.inven.daBoss.pitchSecret(thisSecret);
			break;
		case State.PITCHING_COWORKER:
			//thisSecret.inven.coWorker.giveSecret(thisSecret);
			break;
		case State.TRADING:
			thisSecret.inven.goBackToTrade (thisSecret);
			break;
		default:
			break;
		}
	}
}

