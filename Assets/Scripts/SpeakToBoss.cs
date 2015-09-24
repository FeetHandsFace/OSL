using UnityEngine;
using System.Collections;

public class SpeakToBoss : ProximityTrigger {

	Boss boss;

	// Use this for initialization
	void Start () {
		boss = Persistant.persist.GetComponent<Boss>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isHighlighted && Input.GetMouseButtonDown(0)) {
			boss.beginConversation();
			deActiveProximityTrigger();
		}
	}
}
