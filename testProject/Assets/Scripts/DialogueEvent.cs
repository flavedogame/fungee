﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour {
	FlowController flowController;
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
		Animator animator = go.GetComponent<Animator> ();
		animator.SetTrigger ("zoomout");
		//go.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}