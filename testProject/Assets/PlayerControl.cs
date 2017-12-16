using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public bool isDefaultFacingRight;
	private Puppet2D_GlobalControl globalControl;

	Vector3 targetDirection;
	Vector3 startPosition;
	Vector3 finalPosition;
	bool isMoving;
	float tileWidth = 1.0f;
	float time = 0f;
	// Use this for initialization
	void Start () {
		globalControl = GetComponent<Puppet2D_GlobalControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving) {
			time += Time.deltaTime;
			if(time >= 1){
				isMoving = false;
				time = 0;
				transform.position = finalPosition;
			} else {
				transform.Translate (Time.deltaTime * targetDirection);
			}
			Debug.Log (transform.position+" targetDirection "+targetDirection+" finalPosition "+startPosition+targetDirection);
		}
	}

	public void Shoot(){
		
	}

	public void MoveUp(){
		if (!isMoving) {
			isMoving = true;
			FaceLeft ();
			startPosition = transform.position;
			targetDirection = new Vector3 (0f, tileWidth, 0f);
			finalPosition = startPosition + targetDirection;
		}
	}

	public void MoveDown(){
		if (!isMoving) {
			isMoving = true;
			FaceLeft ();
			startPosition = transform.position;
			targetDirection = new Vector3 (0f, -tileWidth, 0f);
			finalPosition = startPosition + targetDirection;
		}
	}

	public void MoveLeft(){
		if (!isMoving) {
			isMoving = true;
			FaceLeft ();
			startPosition = transform.position;
			targetDirection = new Vector3 (-tileWidth, 0f, 0f);
			finalPosition = startPosition + targetDirection;
		}
	}

	public void MoveRight(){
		if (!isMoving) {
			isMoving = true;
			FaceRight ();
			startPosition = transform.position;
			targetDirection = new Vector3 (-tileWidth, 0f, 0f);
			finalPosition = startPosition - targetDirection;
		}
	}

	void Face(bool isRight) {
		globalControl.flip = isDefaultFacingRight^isRight;
	}
			
	void FaceRight(){
		Face (true);
	}
	void FaceLeft(){
		Face (false);
	}
}
