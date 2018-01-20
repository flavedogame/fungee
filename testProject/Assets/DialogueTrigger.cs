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

	//todo: better way to list all tags
	public string collisionTag;
	public bool isTriggeredByInteract;

	bool isTriggered;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		if (!isTriggered && !flowController.finishFirstTV) {
//			Debug.Log ("trigger");
//			isTriggered = true;
//			flowController.finishFirstTV = true;
//			setDialog ();
//		}
	}

	void setDialog(){
		dialogManager.characters = new List<DialogueBubble> (characters);

		dialogManager.setDialog (text);
		if (!canTriggerRepeatedly) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("trigger" + collisionTag + " " + col.gameObject.tag);
		if (collisionTag.Length > 0 && col.gameObject.tag == collisionTag) {
			//todo: check achievement
			setDialog();
		}
	}
}
