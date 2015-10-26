using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SphereCollider))]
public class IdleMovement : MonoBehaviour {

	public float minStationaryTime, maxStationaryTime, speed;
	float currentStationaryTime, timePeriod;

	bool nearPlayer, closeToDestination;

	public Transform[] possibleDestinations;
	public Transform destination;

	Transform myTransform;

	System.Random rand;

	// Use this for initialization
	void Start () {
		rand = new System.Random();
		myTransform = transform;
		nearPlayer = false;
		closeToDestination = false;
	}

	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<CharacterController>() != null) {
			nearPlayer = true;
		} else {
			closeToDestination = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.GetComponent<CharacterController>() != null) {
			nearPlayer = false;
		}
		closeToDestination = false;
	}

	// Update is called once per frame
	void FixedUpdate() {
		if (!nearPlayer) { 
			if (timePeriod < currentStationaryTime) {
				timePeriod += Time.deltaTime;
				destination = possibleDestinations[rand.Next(0, possibleDestinations.Length)];
			} else {

			}
		}
	}
}
