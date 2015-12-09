using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(SphereCollider))]
public class GoToBed : MonoBehaviour {

	bool isHighlighted = false;
	SphereCollider sphereCollider;

	public Canvas eyeLids;

	// Use this for initialization
	void Start () {
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
		if (Input.GetMouseButtonDown(0)) {
			if (isHighlighted) {
				isHighlighted = false;
				sphereCollider.enabled = false;
				Persistant.persist.sleepCycle();
				eyeLids.gameObject.SetActive(true);
			} else if(eyeLids.gameObject.activeSelf){
				//change clock and other time references
				//maybe have float(time) in persistant that all things reference and just change that
				eyeLids.gameObject.SetActive(false);
			}
		}
	}
}
