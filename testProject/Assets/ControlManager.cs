using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {

	public PlayerControl playerControl;
	public int step = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerControl.CanTakeInput ()) {
			return;
		}
		if (Input.GetKeyDown ("left")) {
			playerControl.MoveLeft ();
		} else if (Input.GetKeyDown ("right")) {
			playerControl.MoveRight ();
		} else if (Input.GetKeyDown ("up")) {
			playerControl.Jump ();
		} else if (Input.GetKeyDown ("down")) {
			playerControl.Fall ();
		} else if (Input.GetMouseButtonDown (0)) {
			playerControl.Shoot ();
		} else if (Input.GetKeyDown ("space")) {
			playerControl.Jump ();
		} else {
			return;
		}

	}
}
