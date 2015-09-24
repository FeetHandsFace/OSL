using UnityEngine;
using System.Collections;

public class LoadOnClick : ProximityTrigger {

	public string destination; 
	
	public void load(){
		Application.LoadLevel (destination);
	}
	
	void Update(){
		if (Input.GetMouseButtonDown (0) && isHighlighted)load ();
	}
}