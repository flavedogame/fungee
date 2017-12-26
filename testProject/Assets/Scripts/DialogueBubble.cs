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
		Debug.Log("layer"+LayerMask.NameToLayer("character"));
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
		vBubbleObject.transform.localScale = bubbleLocation.localScale;
		//Debug.Log ("layer " + layer);
		SetLayerRecursively (vBubbleObject, layer);
		if (isOnLeft) {
			Transform bubble = vBubbleObject.transform.Find ("bubble");
			bubble.localScale = new Vector3 (-1f*bubble.localScale.x, bubble.localScale.y, 1f);
		}

		if (dialogue.bubbleType == DialogueBubbleType.Think) {
			vBubbleObject.GetComponent<Bubble> ().UseThinkBubble ();
		}
			

		if (dialogue.anim!=null && dialogue.anim.Length>0) {
			string[] anims = dialogue.anim.Split ('|');
			foreach (string anim in anims) {
				string[] animPair = anim.Split (',');
				if (animPair.Length>1) {
					animator.SetBool (animPair [0], false);
				} else {
					animator.SetBool (animPair [0], true);
				}
				lastAnim = animPair [0];
			}
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
