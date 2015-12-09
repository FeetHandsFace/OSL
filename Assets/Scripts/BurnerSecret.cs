using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent (typeof(EventTrigger))]
public class BurnerSecret : MonoBehaviour {

	public Secret secretData;
	public Text mouseInfoText;

	// Use this for initialization
	void Start () {
	
	}

	public void showTagAndName() {
		mouseInfoText.text = secretData.groupName + " " + secretData.crimeTag;
	}
	
	public void hideInfo() {
		mouseInfoText.text = "";
	}

	public void take() {
		mouseInfoText.text = "";
		if(secretData.secretObject != null) {   //you are taking back a secret that you alread own
			gameObject.SetActive(false);
		} else {                            //you are taking a secret from a merchant
			secretData.inven.valueOfTakenSecrets += secretData.value;
			secretData.inven.acquireHelper(secretData);
			gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
