using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONFactory;
using AssemblyCSharp;

public class DialogBubbleManager : MonoBehaviour, IManager {

	public GameObject vCurrentBubble = null;
	public List<PixelBubble> bubbles = new List<PixelBubble>();
	public List<DialogueBubble> characters;
	private DialogueBubble currentSpeaker;
	public TextAsset text;

	public bool loop;


	private NarrativeEvent currentEvent;

	private int stepIndex = 0;

	private float stepTime = 0f;

	public ManagerState currentState{ get; private set; }
	public void BootSequence() {
		Debug.Log (string.Format ("{0} is booting up", GetType ().Name));
		currentEvent = JSONFactory.JSONAssembly.RunJSONFactoryForText (text);
		currentState = ManagerState.Completed;
		UpdateDialogue ();
		Debug.Log (string.Format ("{0} status = {1}", GetType ().Name, currentState));
	}

	float DurationOfDialogue() {
		return currentEvent.dialogues [stepIndex].dialogueText.Length * 0.05f + 2f;
	}

	void Update(){
		if (stepIndex < currentEvent.dialogues.Count) {
			stepTime += Time.deltaTime;
			//currentEvent.dialogues [stepIndex].duration
			if (stepTime >= DurationOfDialogue()) {
				stepTime = 0;
				updateBubbleState ();
			}
		} else {
			currentSpeaker.HideBubble ();
		}
	}

	private void UpdateDialogue() {
		Dialogue currentDialog = currentEvent.dialogues [stepIndex];
		currentSpeaker = characters [(int)currentDialog.characterType];
		currentSpeaker.ShowBubble (currentDialog.dialogueText, currentDialog.bubbleType, DurationOfDialogue());
	}

	void updateBubbleState() {
		currentSpeaker.HideBubble ();
		if(stepIndex < currentEvent.dialogues.Count) {
			stepIndex++;
			if (stepIndex == currentEvent.dialogues.Count) {
				if (loop) {
					stepIndex = 0;
				} else {
					ConversationEnd ();
				}
			}
			UpdateDialogue ();
		} else{
		}
	}

	void ConversationEnd(){}

}
