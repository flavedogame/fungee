using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeEvent{
	public List<Dialogue> dialogues;

}

public enum DialogueBubbleType{
	Talk, Think
}

public enum CharacterType{
	Hero,Ally, Mentor
}

public struct Dialogue{
	public CharacterType characterType;
	public string name;
	public string atlasImageName;
	public string dialogueText;
	public float duration;
	public DialogueBubbleType bubbleType;
}
