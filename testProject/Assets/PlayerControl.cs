using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class PlayerControl : MonoBehaviour {
	public bool isDefaultFacingRight;
	public bool isPlayer;
	public float speed = 1.0f;
	private StepManager stepManager;
	public Transform gunTop;
	public Transform shootPointLeft;
	public Transform shootPointRight;
	private Puppet2D_GlobalControl globalControl;
	private Animator animator;
	public bool isDead;
	public Transform spineNode;

	private Vector3 originPosition;//3.348,0.35

	public bool isFacingRight = true;

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
		originPosition = transform.position;
		Debug.Log ("originPosition" + originPosition);
		Debug.Log ("originPosition.local" + transform.localPosition);
	}

	Vector3 CorrectedPosition(Vector3 rawPosition){
		float removeOffsetX = Mathf.Round (rawPosition.x - originPosition.x);
		removeOffsetX += originPosition.x;

		Debug.Log ("originPosition.y"+originPosition.y);
		Debug.Log ("rawPosition.y"+rawPosition.y);
		float removeOffsetY = Mathf.Round (rawPosition.y - originPosition.y);
		removeOffsetY += originPosition.y;
		Debug.Log ("removeOffsetY"+removeOffsetY);

		return new Vector3 (removeOffsetX, removeOffsetY, originPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			return;
		}
		if (WillDead ()) {
			Dead ();
		}
		if (isClimbingFinish) {
			transform.position += targetDirection;
			transform.position = CorrectedPosition (transform.position);
			isClimbingFinish = false;
		}
		gameObject.SetActive (true);
		if (isMoving) {
			time += Time.deltaTime;
			if (time >= 1.0f/speed) {
				StartCoroutine (delaySetMoving());
				AnimatorRunner.Run (animator, Constants.AnimationTuples.stopMoveAnimation);
				AnimatorRunner.Run (animator, Constants.AnimationTuples.stopFallAnimation);
				time = 0;
				transform.position = CorrectedPosition( finalPosition);
			} else {
				transform.Translate (Time.deltaTime * speed * targetDirection );
			}
			//Debug.Log (transform.position + " targetDirection " + targetDirection + " finalPosition " + startPosition + targetDirection);
		} else {
			if (!IsOnGround()) {
				Fall ();
			}
		}
	}

	IEnumerator delaySetMoving(){
		yield return new WaitForSeconds (0.01f);
		isMoving = false;
	}

	public void ThrowOver(){
		isClimbing = true;
		AnimatorRunner.Run (animator, Constants.AnimationTuples.throwOverAnimation);
		stepManager.Notify ();
	}

	public void FinishThrowOver(){
		isClimbing = false;
		isClimbingFinish = true;
		AnimatorRunner.Run (animator, Constants.AnimationTuples.stopThrowOverAnimation);
	}

	public bool WillDead(){
		if (isPlayer) {
			
			LayerMask mask = LayerMask.GetMask ("enemy");
			return Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (isFacingRight ? 1 : -1, 0), 1f, mask);
		} else {
			LayerMask mask = LayerMask.GetMask ("weapon");
			return Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (isFacingRight ? 1 : -1, 0), 0.1f, mask);
		}
	}

	public void Dead(){
		isDead = true;
		if (isPlayer) {
			LayerMask mask = LayerMask.GetMask ("enemy");
			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (isFacingRight ? 1 : -1, 0), 1f, mask);
			AnimatorRunner.Run (animator, Constants.AnimationTuples.deadAnimation);
		} else {
			
			LayerMask mask = LayerMask.GetMask ("weapon");
			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (isFacingRight ? 1 : -1,0), 0.1f, mask);

			GameObject arrow = hit.collider.gameObject.transform.root.gameObject;
			Debug.Log ("arrow"+arrow);
			arrow.transform.parent = this.spineNode;
			ArrowScript arrowScript = arrow.GetComponent<ArrowScript> ();
			arrowScript.StopMoving ();
			AnimatorRunner.Run (animator, Constants.AnimationTuples.deadAnimation);
		}
	}

	public void FinishDead(){
		GameObject.FindObjectOfType<SceneLoadManager> ().GameOver ();
	}

	public void Jump(){
		isClimbing = true;
		targetDirection = new Vector3 (0, 1, 0);
		AnimatorRunner.Run (animator, Constants.AnimationTuples.climbAnimation);
		stepManager.Notify ();
	}

	public void FinishJump(){
		isClimbing = false;
		isClimbingFinish = true;
		AnimatorRunner.Run (animator, Constants.AnimationTuples.stopClimbAnimation);
	}

	public bool IsOnGround(){
		LayerMask mask1 = LayerMask.GetMask ("Default");
		LayerMask mask2 = LayerMask.GetMask ("weapon");
		LayerMask mask = mask1|mask2;
		return Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (0, -1), 1f, mask1) ||
			Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (0, -1), 1f, mask2);
	}

	public bool CanMoveLeft(){
		if (!isPlayer)
			return true;
		LayerMask mask = LayerMask.GetMask ("Default");
		LayerMask mask2 = LayerMask.GetMask ("weapon");
		return !Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (-1, 0), 1f, mask) &&
			!Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (-1, 0), 1f, mask2);
	}

	public bool CanThrowOverLeft(){
		LayerMask mask = LayerMask.GetMask ("Default");
		return !Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y) + new Vector2(0,1), new Vector2 (-1, 0), 1f, mask);
	}

	public bool CanMoveRight(){
		if (!isPlayer)
			return true;
		LayerMask mask = LayerMask.GetMask ("Default");
		LayerMask mask2 = LayerMask.GetMask ("weapon");
		return !Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (1, 0), 1f, mask)&&
			!Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (1, 0), 1f, mask2);
	}

	public bool CanThrowOverRight(){
		LayerMask mask = LayerMask.GetMask ("Default");
		return !Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y) + new Vector2(0,1), new Vector2 (1, 0), 1f, mask);
	}

	public bool CanTakeInput(){
		//Debug.Log (isMoving + " " + isShooting + " " + isClimbing);
		return !isMoving && !isShooting && !isClimbing && !isFalling &&!isClimbingFinish && !isDead;
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
		if (isFacingRight) {
			
			arrow.transform.position = shootPointRight.position;
		} else {
			arrow.transform.position = shootPointLeft.position;
		}
		Debug.Log ("shoot arrow face right " + isFacingRight);
		arrowScript.SetDirection (isFacingRight);
		stepManager.AddObserver (arrowScript);
		stepManager.Notify ();
	}


	public void FinishShooting(){
		isShooting = false;
		AnimatorRunner.Run (animator, Constants.AnimationTuples.stopShootAnimation);
	}

	public void MoveLeft(){
		if (!isMoving) {
			FaceLeft ();
			if (!CanMoveLeft ()&&CanThrowOverLeft()) {
				ThrowOver ();
				targetDirection = new Vector3 (-tileWidth, 1, 0);
			} else {
				isMoving = true;
				AnimatorRunner.Run (animator, Constants.AnimationTuples.moveAnimation);
				startPosition = transform.position;
				if (CanMoveLeft ()) {
					targetDirection = new Vector3 (-tileWidth, 0f, 0f);
				} else {
					targetDirection = Vector3.zero;
				}
				finalPosition = startPosition + targetDirection;
			}
			stepManager.Notify ();
		}
	}

	public void MoveRight(){
		if (!isMoving) {
			FaceRight ();
			if (!CanMoveRight ()&&CanThrowOverRight()) {
				ThrowOver ();
				targetDirection = new Vector3 (tileWidth, 1, 0);
			} else {
				isMoving = true;
				AnimatorRunner.Run (animator, Constants.AnimationTuples.moveAnimation);
				startPosition = transform.position;
				if (CanMoveRight ()) {
					targetDirection = new Vector3 (-tileWidth, 0f, 0f);
				} else {
					targetDirection = Vector3.zero;
				}
				finalPosition = startPosition - targetDirection;
			}
			stepManager.Notify ();
		}
	}

	void Face(bool isRight) {
		globalControl.flip = isDefaultFacingRight^isRight;
		isFacingRight = isRight;
		Debug.Log ("face to right " + isRight);
	}
			
	void FaceRight(){
		Face (true);
	}
	void FaceLeft(){
		Face (false);
	}
}
