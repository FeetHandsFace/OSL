﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class MasterList : MonoBehaviour {

	public static Dictionary<int, List<Secret>> masterDictionary;
	public DayArray[] allDays;
	public TextAsset protagonist; 
	public static System.Random randomGenerator;

	public void startNew() {
		randomGenerator = new System.Random();
		masterDictionary = new Dictionary<int, List<Secret>>();
		string[] fileParser = protagonist.text.Split("\n"[0]);
		Secret secret;
		Inventory inven = GetComponent<Inventory>();
		for(int j = 0; j < fileParser.Length; j += 10) {
			secret = new Secret(0, Convert.ToInt32(fileParser[j]), Convert.ToInt32(fileParser[j + 1]), fileParser[j + 2], fileParser[j + 3], fileParser[j + 4],
										fileParser[j + 5], fileParser[j + 6], fileParser[j + 7], fileParser[j + 8], fileParser[j + 9], false);
			inven.acquireHelper(secret);
		}
		parseTextAssetArray(0);
		//add the list of secrets in dictionary slot zero (protagonist secrets) to the player inventory
	}

	public void newDay() {
		parseTextAssetArray(Calender.getDay);
	}

	void parseTextAssetArray(int day) {
		string[] fileParser;
		Secret secret;
		for(int i = 0; i < allDays[day].textArray.Length; i++) {
			fileParser = allDays[day].textArray[i].text.Split("\n"[0]);
			if(!masterDictionary.ContainsKey(i)) {
				masterDictionary.Add(i, new List<Secret>());
			}
			for(int j = 0; j < fileParser.Length; j += 10) {
				secret = new Secret(randomGenerator.Next(1, 5), Convert.ToInt32(fileParser[j]), Convert.ToInt32(fileParser[j + 1]), fileParser[j + 2], fileParser[j + 3], fileParser[j + 4],
										  fileParser[j + 5], fileParser[j + 6], fileParser[j + 7], fileParser[j + 8], fileParser[j + 9], false);
				masterDictionary[i].Add(secret);
			}
		}
	}

	void Update() {

	}

}

[System.Serializable]
public class DayArray {
	public TextAsset[] textArray;
}
