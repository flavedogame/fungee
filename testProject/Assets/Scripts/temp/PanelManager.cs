using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONFactory;

public class PanelManager : MonoBehaviour, IManager {

	private PanelConfig rightPanel;
	private PanelConfig leftPanel;

	private NarrativeEvent currentEvent;

	private bool leftCharacterActive = true;
	private int stepIndex = 0;

	public ManagerState currentState{ get; private set; }
	public void BootSequence() {
		Debug.Log (string.Format ("{0} is booting up", GetType ().Name));
		leftPanel = GameObject.Find ("LeftCharacterPanel").GetComponent<PanelConfig> ();

		rightPanel = GameObject.Find ("RightCharacterPanel").GetComponent<PanelConfig> ();
		//currentEvent = /JSONFactory.JSONAssembly.RunJSONFactoryForScene (1);
		currentState = ManagerState.Completed;
		InitializePanels ();
		Debug.Log (string.Format ("{0} status = {1}", GetType ().Name, currentState));
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			updatePanelState ();
		}
	}

	private void InitializePanels() {
		leftPanel.characterIsTalking = true;
		rightPanel.characterIsTalking = false;
		leftCharacterActive = !leftCharacterActive;
		leftPanel.Configure (currentEvent.dialogues [stepIndex]);
		rightPanel.Configure (currentEvent.dialogues [stepIndex + 1]);
		StartCoroutine( MasterManager1.animationManager.IntroAnimation ());
		stepIndex++;
	}

	private void ConfigurePanels() {
		leftPanel.characterIsTalking = leftCharacterActive;
		rightPanel.characterIsTalking = !leftCharacterActive;
		if (leftCharacterActive) {
			leftPanel.Configure (currentEvent.dialogues [stepIndex]);
			rightPanel.ToggleCharacterMask ();
		} else {
			rightPanel.Configure (currentEvent.dialogues [stepIndex]);
			leftPanel.ToggleCharacterMask ();
		}
	}

	void updatePanelState() {
		if(stepIndex < currentEvent.dialogues.Count) {
			ConfigurePanels ();
			leftCharacterActive = !leftCharacterActive;
			stepIndex++;
		} else{
			StartCoroutine (MasterManager1.animationManager.ExitAnimation ());
		}
	}


}
