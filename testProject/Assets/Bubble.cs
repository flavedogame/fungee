using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {
	public GameObject thinkBubble;
	public GameObject talkBubble;
	public Vector3 parentOriginPosition;
	public Transform parentTransform;
	// Use this for initialization
	void Start () {
		
	}

	public static GameObject CreateBubble(DialogueBubbleType dialogBubbleType, Transform parent) {
		GameObject bubble = Instantiate (Resources.Load ("bubbleDialog", typeof(GameObject))) as GameObject;
		if(dialogBubbleType == DialogueBubbleType.Think){
			bubble.GetComponent<Bubble>().UseThinkBubble();
		}
		bubble.transform.position = parent.position; 
		bubble.transform.localScale = parent.localScale;
		bubble.GetComponent<Bubble>().parentTransform = parent;
		bubble.GetComponent<Bubble>(). parentOriginPosition = parent.position;
		return bubble;
	}

	public void UseThinkBubble(){
		thinkBubble.SetActive (true);
		talkBubble.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (parentTransform.position + " life " + parentOriginPosition + " is " + transform.position);
		Vector3 offset = parentTransform.position - parentOriginPosition;
		parentOriginPosition = parentTransform.position;
		transform.position += offset;
	}
}
