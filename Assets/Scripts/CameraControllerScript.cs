using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour {

	public Transform character;

	void Start(){
		//remember to standardized the camera starting position and rotation
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = (new Vector3(character.position.x, transform.position.y, character.position.z - 15f));
	}
}
