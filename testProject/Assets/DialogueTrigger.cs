using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	public DialogBubbleManager dialogManager;
	public List<DialogueBubble> characters;
	public TextAsset text;
	public FlowController flowController;
	public bool canTriggerRepeatedly;

//	public enum DialogTriggerType{
//		hit, achievement
//	}

	public int hitMask;
	public string achievement;
	public bool isTriggeredByInteract;

	bool isTriggered;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isTriggered && !flowController.finishFirstTV) {
			Debug.Log ("trigger");
			isTriggered = true;
			flowController.finishFirstTV = true;
			dialogManager.characters = new List<DialogueBubble> (characters);

			dialogManager.setDialog (text);
		}
	}
}
