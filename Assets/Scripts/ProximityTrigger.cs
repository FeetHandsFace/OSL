using UnityEngine;
using System.Collections;

public class ProximityTrigger : MonoBehaviour {

	public Material normal, highlighted;
	protected bool isHighlighted = false;
	
	void OnTriggerEnter(Collider other){
		if(GetComponent<Renderer>() != null) {
			GetComponent<Renderer>().material = highlighted;
		} else {
			foreach(Renderer r in GetComponentsInChildren<Renderer>()) r.material = highlighted;
		}
		isHighlighted = true;
	}
	
	void OnTriggerExit(Collider other) {
		if(GetComponent<Renderer>() != null) {
			GetComponent<Renderer>().material = normal;
		} else {
			foreach(Renderer r in GetComponentsInChildren<Renderer>()) r.material = normal;
		}
		isHighlighted = false;
	}

	protected void deActiveProximityTrigger() {
		OnTriggerExit(null);
		GetComponent<SphereCollider>().enabled = false;
	}
}
