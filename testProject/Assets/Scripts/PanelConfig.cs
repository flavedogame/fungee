using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelConfig : MonoBehaviour {
	public bool characterIsTalking;
	public Image avatarImage;
	public Image textBG;
	public Text characterName;
	public Text dialogue;

	private Color maskActiveColor = new Color (101.0f / 225f, 101.0f / 225f, 101.0f / 255f);

	public void ToggleCharacterMask(){
		if (characterIsTalking) {
			avatarImage.color = textBG.color = Color.white;
		} else {
			avatarImage.color = textBG.color = maskActiveColor;
		}
	}

	IEnumerator AnimateText(string dialogueText){
		dialogue.text = "";
		foreach (char letter in dialogueText) {
			dialogue.text += letter;
			yield return new WaitForSeconds (0.05f);
		}
	}

	public void Configure(Dialogue currentDialogue) {
		ToggleCharacterMask ();
		avatarImage.sprite = MasterManager.atlasManager.loadSprite (currentDialogue.atlasImageName);
		characterName.text = currentDialogue.name;
		if (characterIsTalking) {
			StartCoroutine (AnimateText(currentDialogue.dialogueText));
		} else {
			dialogue.text = "";
		}
	}
}
