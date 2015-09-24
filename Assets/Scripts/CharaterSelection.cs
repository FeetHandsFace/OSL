using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharaterSelection : MonoBehaviour {

	public string characterName;
	public Text displayForCharacterName;
	Persistant persistantSecretLists;

	// Use this for initialization
	void Start () {
		persistantSecretLists = Persistant.persist;
		displayForCharacterName.text = characterName;
	}

	public void thisCharacterIsSelected(){
		persistantSecretLists.moveCharacterSecrets (characterName);
		Application.LoadLevel (2);
	}
}
