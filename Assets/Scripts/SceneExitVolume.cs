using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(SphereCollider))]
public class SceneExitVolume : MonoBehaviour {

	public Canvas exitButton;
	SphereCollider sphereCollider;

	void Start() {
		sphereCollider = GetComponent<SphereCollider>();
		sphereCollider.isTrigger = true;
	}

	void OnTriggerEnter(Collider other) {
		exitButton.gameObject.SetActive(true);
	}

	void OnTriggerExit(Collider other) {
		exitButton.gameObject.SetActive(false);
	}
}
