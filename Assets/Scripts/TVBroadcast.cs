using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TVBroadcast : MonoBehaviour {

    public TextAsset topStories;
    Stack<string> stories;
	public Text broadCastWindow;
	Queue<string> currentlyBroadcasting;

    void Start () {
		currentlyBroadcasting = new Queue<string>();
		stories = new Stack<string>(topStories.text.Split("\n"[0]));
	}

    public void beginBroadCast() {
		foreach (string s in stories.Pop().Split("."[0])) { currentlyBroadcasting.Enqueue(s); }
		broadCastWindow.text = currentlyBroadcasting.Dequeue() + ".";
    }

	public void addStory(string s) {
		stories.Push(s);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) {
			if (currentlyBroadcasting.Count != 0) {
				broadCastWindow.text = currentlyBroadcasting.Dequeue() + ".";
			} else {
				broadCastWindow.text = "";
			}
		}
	}
}
