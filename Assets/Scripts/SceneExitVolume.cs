using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(SphereCollider))]
public class SceneExitVolume : MonoBehaviour {

	public Canvas exitButton;

	void OnTriggerEnter(Collider other) {
		exitButton.gameObject.SetActive(true);
	}

	void OnTriggerExit(Collider other) {
		exitButton.gameObject.SetActive(false);
	}
}
