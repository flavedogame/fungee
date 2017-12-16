﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {

	public PlayerControl playerControl;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("left")) {
			playerControl.MoveLeft ();
		}else if(Input.GetKeyDown ("right")) {
			playerControl.MoveRight ();
		}else if(Input.GetKeyDown ("up")) {
			playerControl.MoveUp ();
		}else if(Input.GetKeyDown ("down")) {
			playerControl.MoveDown ();
		}
	}
}