﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class SpeakToBoss : MonoBehaviour {

	Boss boss;
	DialogueSystem dialogueSystem;
	SphereCollider sphereCollider;
	bool isHighlighted = false;

	// Use this for initialization
	void Start() {
		boss = Persistant.persist.boss;
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
	
	// When you start speaking to the boss you cannot speak to the boss again 
	void Update () {
		if (isHighlighted && Input.GetMouseButtonDown(0)) {
			boss.beginConversation();
			isHighlighted = false;
			sphereCollider.enabled = false;
		}
	}
}
