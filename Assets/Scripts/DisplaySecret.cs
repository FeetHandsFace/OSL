using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplaySecret : MonoBehaviour {

	public Secret thisSecret;

	public void mouseEnter(){
		thisSecret.displayText ();
	}

	public void mouseExit(){
		thisSecret.hideText ();
	}

	public void bidSecret(){
		switch (thisSecret.inven.getState) {
		case State.IDLE:
			break;
		case State.PITCHING:
			thisSecret.inven.daBoss.pitchSecret(thisSecret);
			break;
		case State.PITCHING_COWORKER:
			thisSecret.inven.coWorker.giveSecret(thisSecret);
			break;
		case State.TRADING:
			thisSecret.inven.goBackToTrade (thisSecret);
			if(GetComponent<Image>().color == Color.gray){
				GetComponent<Image>().color = Color.white;
			}else{
				GetComponent<Image>().color = Color.gray;
			}
			break;
		default:
			break;
		}
	}
}

