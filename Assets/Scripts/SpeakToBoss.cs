using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class SpeakToBoss : MonoBehaviour {

	Boss boss;
	DialogueSystem dialogueSystem;
	public SphereCollider sphereCollider;
	bool isHighlighted = false;

	// Use this for initialization
	void Start() {
		boss = Persistant.persist.boss;
		dialogueSystem = Persistant.persist.GetComponent<DialogueSystem>();
		sphereCollider = GetComponent<SphereCollider>();
		sphereCollider.isTrigger = true;
	}

	void OnTriggerEnter(Collider other) {
		isHighlighted = true;
	}

	void OnTriggerExit(Collider other) {
		isHighlighted = false;
		if (dialogueSystem.stillTalking()) dialogueSystem.cutOffConversation();
		sphereCollider.enabled = false;
		//boss.angry = true;
	}
	
	// When you start speaking to the boss you cannot speak to the boss again 
	void Update () {
		if (isHighlighted && Input.GetMouseButtonDown(0)) {
			boss.beginConversation(this);
			isHighlighted = false;
		}
	}
}
