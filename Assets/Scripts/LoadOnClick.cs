using UnityEngine;
using System.Collections;


public class LoadOnClick : MonoBehaviour {

	public string destination;
	public string home;
	
	public void load(){
		if (/*check for save game*/false) {
			Application.LoadLevel(home);
		} else {
			Application.LoadLevel(destination);
		}
	}
}