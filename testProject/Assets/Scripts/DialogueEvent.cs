using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour {
	FlowController flowController;
	public AchievementSystem achievementSystem;

	// Use this for initialization
	void Start () {
		
	}

	public void TriggerEvent (string eventName){
		Debug.Log ("triggerEvent");
		Invoke (eventName, 0.1f);
	}

	public void ZoomOutTV(){
		Debug.Log ("zoom out tv");
		GameObject go= GameObject.Find ("tv camera test");
		if(go!=null && go.active){
			TVController tvController = go.GetComponent<TVController> ();
			tvController.Zoomout ();

		//Animator animator = go.GetComponent<Animator> ();
		//animator.SetTrigger ("zoomout");
		}
	}
		

	public void AddAchievement(string[] p) {
		//check p[1] is a number
		if (p.Length != 2) {
			Debug.LogError ("format not correct");
			return;
		}
		Debug.Log ("dialog event add achievement " + p[0]+" "+p[1]);
		AchievementSystem.Instance.AddAchievement (p [0], int.Parse (p [1]));
	}

	// Update is called once per frame
	void Update () {
		
	}
}
