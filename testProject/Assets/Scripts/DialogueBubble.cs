using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueBubble : MonoBehaviour {

	public int layer;
	public bool isOnLeft;
	public Transform bubbleLocation;

	Animator animator;

	private Text dialogText;
	private string lastAnim;

	void Start(){
		animator = GetComponent<Animator> ();
	}

	private void SetLayerRecursively(GameObject obj, int newLayer){
		obj.layer = newLayer;
		foreach (Transform child in obj.transform) {
			SetLayerRecursively (child.gameObject, newLayer);
		}
	}
	public void ShowBubble(Dialogue dialogue, float duration){
		if (animator == null) {
			animator = GetComponent<Animator> ();
		}
		//Debug.Log ("show bubble " + text + " with type " + type);
		GameObject vBubbleObject = null;

		vBubbleObject = Instantiate (Resources.Load ("bubbleDialog", typeof(GameObject))) as GameObject;
		vBubbleObject.transform.position = bubbleLocation.position; 
		//Debug.Log ("layer " + layer);
		SetLayerRecursively (vBubbleObject, layer);
		if (isOnLeft) {
			Transform bubble = vBubbleObject.transform.Find ("bubble");
			bubble.localScale = new Vector3 (-1f, 1f, 1f);
		}

		if (dialogue.bubbleType == DialogueBubbleType.Think) {
			vBubbleObject.GetComponent<Bubble> ().UseThinkBubble ();
		}
			

		if (dialogue.anim.Length>0) {
			animator.SetBool (dialogue.anim, true);
			lastAnim = dialogue.anim;
		} else {
		}
		dialogText = vBubbleObject.transform.GetComponentInChildren<Text> ();
		//dialogText.text = text;
		StartCoroutine (AnimateText(dialogue.dialogueText));
		Destroy (vBubbleObject, duration);
	}

	IEnumerator AnimateText(string dialogueText){
		dialogText.text = "";
		foreach (char letter in dialogueText) {
			dialogText.text += letter;
			yield return new WaitForSeconds (0.05f);
		}
	}

	public void HideBubble(){

		if (lastAnim!=null&&lastAnim.Length>0) {
			animator.SetBool (lastAnim, false);
		}
		//Debug.Log ("hide bubble ");
	}


}
