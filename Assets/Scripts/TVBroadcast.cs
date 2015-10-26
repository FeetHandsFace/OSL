using System.Collections.Generic;

public class TVBroadcast  {

    public Stack<string> stories;
	DialogueSystem dialogueSystem;

	public TVBroadcast(DialogueSystem dSystem, string allStories) {
		dialogueSystem =dSystem;
		stories = new Stack<string>(allStories.Split("\n"[0]));
	}

    public void beginBroadCast() {
		dialogueSystem.loadDialogueBlock(stories.Pop());
    }

	public void addStory(string s) {
		stories.Push(s);
	}
}
