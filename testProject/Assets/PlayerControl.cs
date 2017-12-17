using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class PlayerControl : MonoBehaviour {
	public bool isDefaultFacingRight;
	private Puppet2D_GlobalControl globalControl;
	private Animator animator;

	Vector3 targetDirection;
	Vector3 startPosition;
	Vector3 finalPosition;
	bool isMoving;
	bool isShooting;
	bool isClimbing;
	bool isFalling;
	float tileWidth = 1.0f;
	float time = 0f;
	// Use this for initialization
	void Start () {
		globalControl = GetComponent<Puppet2D_GlobalControl> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving) {
			time += Time.deltaTime;
			if (time >= 1) {
				isMoving = false;
				AnimatorRunner.Run (animator, Constants.AnimationTuples.stopMoveAnimation);
				AnimatorRunner.Run (animator, Constants.AnimationTuples.stopFallAnimation);
				time = 0;
				transform.position = finalPosition;
			} else {
				transform.Translate (Time.deltaTime * targetDirection);
			}
			Debug.Log (transform.position + " targetDirection " + targetDirection + " finalPosition " + startPosition + targetDirection);
		} else {
			if (!IsOnGround()) {
				Fall ();
			}
		}
	}

	public bool IsOnGround(){
		return Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (0, -1), 1f, 9);
	}

	public bool CanMoveLeft(){
		return !Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (1, 0), 1f, 9);
	}

	public bool CanMoveRight(){
		return !Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (-1, 0), 1f, 9);
	}

	public bool CanMoveUp(){
		return Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (0, 1), 1f, 9);
	}

	public bool CanTakeInput(){
		//Debug.Log (isMoving + " " + isShooting + " " + isClimbing);
		return !isMoving && !isShooting && !isClimbing && !isFalling;
	}

	public void Fall(){
		isMoving = true;
		AnimatorRunner.Run (animator, Constants.AnimationTuples.fallAnimation);
		startPosition = transform.position;
		targetDirection = new Vector3 (0f, -tileWidth, 0f);
		finalPosition = startPosition + targetDirection;
	}

	public void Shoot(){
		isShooting = true;
		AnimatorRunner.Run (animator, Constants.AnimationTuples.shootAnimation);
	}

	public void FinishShooting(){
		isShooting = false;
		AnimatorRunner.Run (animator, Constants.AnimationTuples.stopShootAnimation);
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
			AnimatorRunner.Run (animator, Constants.AnimationTuples.moveAnimation);
			FaceLeft ();
			startPosition = transform.position;
			targetDirection = new Vector3 (-tileWidth, 0f, 0f);
			finalPosition = startPosition + targetDirection;
		}
	}

	public void MoveRight(){
		if (!isMoving) {
			isMoving = true;
						AnimatorRunner.Run (animator, Constants.AnimationTuples.moveAnimation);
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
