using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {

	public PlayerControl playerControl;
	public int step = 0;
	TVController tvController;
	// Use this for initialization
	void Start () {
		tvController = GameObject.Find ("tv camera test").GetComponent<TVController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerControl.CanTakeInput ()) {
			return;
		}
		if (Input.touchCount >= 1) {
			Vector2 vTouchPos = Input.GetTouch (0).position;
			Ray ray = Camera.main.ScreenPointToRay (vTouchPos);
			RaycastHit vHit;
			if (Physics.Raycast (ray.origin, ray.direction, out vHit)) {
				Debug.Log ("touch on " + vHit);
				if (vHit.transform.tag == "tv") {
					Debug.Log ("open tv");
					tvController.Zoomin ();
				}
			}
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
