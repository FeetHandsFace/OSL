using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(IdleMovement))]
public class SpeakToCoworker : MonoBehaviour {

	public int identityNumber;
	bool isHighlighted = false;
	Coworker coworker;
	DialogueSystem dialogueSystem;
	SphereCollider sphereCollider;

	void Start() {
		coworker = Persistant.persist.coWorkers[identityNumber];
		dialogueSystem = Persistant.persist.GetComponent<DialogueSystem>();
		sphereCollider = GetComponent<SphereCollider>();
	}

	void OnTriggerEnter(Collider other) {
		isHighlighted = true;
	}

	void OnTriggerExit(Collider other) {
		isHighlighted = false;
		if (dialogueSystem.stillTalking()) dialogueSystem.cutOffConversation();
	}

	//When you start speaking to this coworker you cannot speak to them again afterwards
	void Update() {
		if (isHighlighted && Input.GetMouseButtonDown(0)) {
			coworker.beginConversation();
			isHighlighted = false;
			sphereCollider.enabled = false;
		}
	}
}
