using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour, Observer {
	public bool isStatic;
	private bool isMoving;
	private float time;
	float tileWidth = 1.0f;
	public float speed = 1.0f;
	Vector3 targetDirection;
	Vector3 startPosition;
	Vector3 finalPosition;
	private bool isFacingRight;
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
		
	public void SetDirection(bool faceRight){
		isFacingRight = faceRight;
		transform.localScale = new Vector3 (transform.localScale.x * (faceRight?1f:-1f), transform.localScale.y, transform.localScale.z);
	}
	// Update is called once per frame
	void Update () {
		if (isMoving) {
			time += Time.deltaTime;
			if (time >= 1.0f/speed) {
				isMoving = false;
				time = 0;
				transform.position = finalPosition;
				/*
				if(isDuplicateArrow()){
					Destroy(this.gameObject,2f);
					return;
				}*/
				if (isWallForward ()) {
					isStatic = true;
					animator.SetTrigger ("quiver");
				}

			} else {
				transform.Translate (Time.deltaTime * targetDirection* speed);
			}
			//Debug.Log (transform.position + " targetDirection " + targetDirection + " finalPosition " + startPosition + targetDirection);
		}
	}

	public void StopMoving(){
		isMoving = false;
	}

	public void OnNotify(){
		//Debug.Log ("get notify");
		if (!isWallForward ()) {
			isMoving = true;
			startPosition = transform.position;
			targetDirection = new Vector3 (tileWidth * (isFacingRight ? 1 : -1), 0f, 0f);
			finalPosition = startPosition + targetDirection;
		} else {

			isStatic = true;
		}

	}

	public bool isWallForward(){
		LayerMask mask = LayerMask.GetMask ("Default");
		return Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), new Vector2 ((isFacingRight?1f:-1f), 0), 0.5f, mask);
	}
	/*
	public bool isDuplicateArrow(){
		LayerMask mask = LayerMask.GetMask ("weapon");
		Debug.Log ("mask" + mask.value);
		Debug.Log ("duplicate" + !!Physics2D.Raycast (new Vector2 (transform.position.x - 1.5f, transform.position.y), new Vector2 (1f, 0), 1f, mask));
		return Physics2D.Raycast (new Vector2 (transform.position.x-1.5f, transform.position.y), new Vector2 (1f, 0), 1f, mask);
	}*/
}
