using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class TurnOnTV : MonoBehaviour {

	TVBroadcast broadCast;
	DialogueSystem dialogueSystem;
	SphereCollider sphereCollider;
	bool isHighlighted = false;

	// Use this for initialization
	void Start () {
		broadCast = Persistant.persist.tvBroadcast;
		dialogueSystem = Persistant.persist.GetComponent<DialogueSystem>();
		sphereCollider = GetComponent<SphereCollider>();
		sphereCollider.isTrigger = true;
	}

	void OnTriggerEnter(Collider other) {
		isHighlighted = true;
	}

	void OnTriggerExit(Collider other) {
		isHighlighted = false;
	}

	// Update is called once per frame
	void Update () {
		if(isHighlighted && Input.GetMouseButtonDown(0)) {
			broadCast.beginBroadCast();
			isHighlighted = false;
			sphereCollider.enabled = false;
		}
	}
}
