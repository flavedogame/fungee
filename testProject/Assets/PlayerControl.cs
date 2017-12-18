using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class PlayerControl : MonoBehaviour {
	public bool isDefaultFacingRight;
	public float speed = 1.0f;
	private StepManager stepManager;
	public Transform gunTop;
	private Puppet2D_GlobalControl globalControl;
	private Animator animator;

	private bool isFacingRight = true;

	Vector3 targetDirection;
	Vector3 startPosition;
	Vector3 finalPosition;
	bool isMoving;
	bool isShooting;
	bool isClimbing;
	bool isFalling;
	bool isClimbingFinish;
	float tileWidth = 1.0f;
	float time = 0f;
	// Use this for initialization
	void Start () {
		globalControl = GetComponent<Puppet2D_GlobalControl> ();
		animator = GetComponent<Animator> ();
		//gunTop = transform.Find ("gunTop");
		stepManager = GameObject.FindObjectOfType<StepManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isClimbingFinish) {
			transform.position += new Vector3 (0f, 1f, 0f);
			isClimbingFinish = false;
		}
		gameObject.SetActive (true);
		if (isMoving) {
			time += Time.deltaTime;
			if (time >= 1.0f/speed) {
				isMoving = false;
				AnimatorRunner.Run (animator, Constants.AnimationTuples.stopMoveAnimation);
				AnimatorRunner.Run (animator, Constants.AnimationTuples.stopFallAnimation);
				time = 0;
				transform.position = finalPosition;
			} else {
				transform.Translate (Time.deltaTime * speed * targetDirection );
			}
			Debug.Log (transform.position + " targetDirection " + targetDirection + " finalPosition " + startPosition + targetDirection);
		} else {
			if (!IsOnGround()) {
				Fall ();
			}
		}
	}

	public void Jump(){
		isClimbing = true;
		AnimatorRunner.Run (animator, Constants.AnimationTuples.climbAnimation);
		stepManager.Notify ();
	}

	public void FinishJump(){
		isClimbing = false;
		isClimbingFinish = true;
		AnimatorRunner.Run (animator, Constants.AnimationTuples.stopClimbAnimation);
	}

	public bool IsOnGround(){
		return true;
		LayerMask mask1 = LayerMask.GetMask ("Default");
		LayerMask mask2 = LayerMask.GetMask ("weapon");
		LayerMask mask = mask1|mask2;
		return Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (0, -1), 1f, mask1) ||
			Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (0, -1), 1f, mask2);
	}

	public bool CanMoveLeft(){
		LayerMask mask = LayerMask.GetMask ("Default");
		return !Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (-1, 0), 1f, mask);
	}

	public bool CanMoveRight(){
		LayerMask mask = LayerMask.GetMask ("Default");
		return !Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (1, 0), 1f, mask);
	}

	public bool CanMoveUp(){
		return Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (0, 1), 1f, 9);
	}

	public bool CanTakeInput(){
		//Debug.Log (isMoving + " " + isShooting + " " + isClimbing);
		return !isMoving && !isShooting && !isClimbing && !isFalling;
	}

	public void Fall(){
		if (!isMoving) {
			isMoving = true;
			AnimatorRunner.Run (animator, Constants.AnimationTuples.fallAnimation);
			startPosition = transform.position;
			targetDirection = new Vector3 (0f, -tileWidth, 0f);
			finalPosition = startPosition + targetDirection;
			stepManager.Notify ();
		}
	}

	public void Shoot(){
		isShooting = true;
		AnimatorRunner.Run (animator, Constants.AnimationTuples.shootAnimation);

	}

	public void ShootArrow(){
		GameObject arrow = Instantiate(Resources.Load<GameObject> ("arrow"));
		ArrowScript arrowScript = arrow.GetComponent<ArrowScript> ();
		arrow.transform.position = gunTop.position;
		arrowScript.SetDirection (isFacingRight);
		stepManager.AddObserver (arrowScript);
		stepManager.Notify ();
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
			if (CanMoveLeft ()) {
				targetDirection = new Vector3 (-tileWidth, 0f, 0f);
			} else {
				targetDirection = Vector3.zero;
			}
			finalPosition = startPosition + targetDirection;
			stepManager.Notify ();
		}
	}

	public void MoveRight(){
		if (!isMoving) {
			isMoving = true;
						AnimatorRunner.Run (animator, Constants.AnimationTuples.moveAnimation);
			FaceRight ();
			startPosition = transform.position;
			if (CanMoveRight ()) {
				targetDirection = new Vector3 (-tileWidth, 0f, 0f);
			} else {
				targetDirection = Vector3.zero;
			}
			finalPosition = startPosition - targetDirection;
			stepManager.Notify ();
		}
	}

	void Face(bool isRight) {
		globalControl.flip = isDefaultFacingRight^isRight;
		isFacingRight = isRight;
	}
			
	void FaceRight(){
		Face (true);
	}
	void FaceLeft(){
		Face (false);
	}
}
