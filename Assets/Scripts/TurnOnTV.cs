using UnityEngine;
using System.Collections;

public class TurnOnTV : ProximityTrigger {

	TVBroadcast broadCast;

	// Use this for initialization
	void Start () {
		broadCast = Persistant.persist.GetComponent<TVBroadcast>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isHighlighted && Input.GetMouseButtonDown(0)) {
			broadCast.beginBroadCast();
			deActiveProximityTrigger();
		}
	}
}
