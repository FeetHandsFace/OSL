using UnityEngine;
using System.Collections;
using System;

public class Calender : MonoBehaviour {

	public static int getDay {get {return today;}}
	static int today;

	void OnLevelWasLoaded(int level) {
		//if the level is a trading space call today.day() on the level manager
	}

	void Start() {
		today = 0;
	}

	public void changeDay() {
		today++;
	}


	// Update is called once per frame
	void Update () {
	
	}
}
