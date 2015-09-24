using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	Vector3 movingTo;

	public float walkSpeed;

	// Use this for initialization
	void Start () {
		movingTo = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hitInfo;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo, 30f)) {
					movingTo = new Vector3 (hitInfo.point.x, transform.position.y, hitInfo.point.z);
				}
			}
			if (movingTo != transform.position)transform.position = Vector3.Lerp (transform.position, movingTo, walkSpeed * Time.deltaTime);
		}
	}
}
