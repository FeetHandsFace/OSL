using UnityEngine;
using System.Collections;

public class Trade : ProximityTrigger {

	Secret randomSecret;
	Inventory inven;

	// Use this for initialization
	void Start () {
		randomSecret = Persistant.persist.masterList [Random.Range (0, 1)];
		inven = Persistant.persist.GetComponent<Inventory> ();
	}

	void Update(){
		if(Input.GetMouseButtonDown(0) && isHighlighted){
			inven.initiateTrade (randomSecret);
		}
	}
}
