using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SphereCollider))]
public class GoToBed : MonoBehaviour {

	bool isHighlighted = false;
	SphereCollider sphereCollider;
	

	// Use this for initialization
	void Start () {
		sphereCollider = GetComponent<SphereCollider>();
	}

	void OnTriggerEnter(Collider other) {
		isHighlighted = true;
	}

	void OnTriggerExit(Collider other) {
		isHighlighted = false;
	}

	// Update is called once per frame
	void Update () {
		if (isHighlighted && Input.GetMouseButtonDown(0)) {
			isHighlighted = false;
			sphereCollider.enabled = false;
			Persistant.persist.sleepCycle();
			//have "their eyes close" 
		}
	}
}
