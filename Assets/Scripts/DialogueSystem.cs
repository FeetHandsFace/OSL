using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

	public Text converstationBox;
	Queue<string> dialogueBlock;

	// Use this for initialization
	void Start () {
		dialogueBlock = new Queue<string>();
	}

	void OnLevelWasLoaded(int level) {
		cutOffConversation();
	}

	public void loadDialogueBlock(string block) {
		foreach (string s in block.Split("."[0])) { dialogueBlock.Enqueue(s); }
		converstationBox.text = dialogueBlock.Dequeue() + ".";
	}

	public bool stillTalking() {
		return dialogueBlock.Count == 0;
	}

	public void cutOffConversation() {
		dialogueBlock.Clear();
		converstationBox.text = "";
		//cut off conversation when leaving a scene or when leaving the boss collider sphere
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			if (dialogueBlock.Count != 0) {
				converstationBox.text = dialogueBlock.Dequeue() + ".";
			} else {
				converstationBox.text = "";
			}
		}
	}
}
