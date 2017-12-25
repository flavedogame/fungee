using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueBubble : MonoBehaviour {

	public int layer;
	public bool isOnLeft;
	public Transform bubbleLocation;

	private Text dialogText;

	private void SetLayerRecursively(GameObject obj, int newLayer){
		obj.layer = newLayer;
		foreach (Transform child in obj.transform) {
			SetLayerRecursively (child.gameObject, newLayer);
		}
	}
	public void ShowBubble(string text, DialogueBubbleType type, float duration){
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
		dialogText = vBubbleObject.transform.GetComponentInChildren<Text> ();
		//dialogText.text = text;
		StartCoroutine (AnimateText(text));
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
		//Debug.Log ("hide bubble ");
	}


}
