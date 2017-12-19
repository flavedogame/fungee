using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, Observer {

	public Transform patrolLeftPoint;
	public Transform patrolRightPoint;

	PlayerControl enemyController;

	// Use this for initialization
	void Start () {
		StepManager stepManager = GameObject.FindObjectOfType<StepManager> ();
		stepManager.AddObserver (this);
		enemyController = GetComponent<PlayerControl> ();
	}

	public void OnNotify(){
		if (IsAttacked ()) {
			GetAttacked ();
		} else {
			Patrol ();
		}
	}

	bool IsAttacked(){
		return false;
	}

	void GetAttacked(){
		
	}
		

	void Patrol(){
		if (UtilityCS.AlmostEqual (transform.position, patrolLeftPoint.position)) {
			enemyController.MoveRight ();
		} else if (UtilityCS.AlmostEqual (transform.position, patrolRightPoint.position)) {
			enemyController.MoveLeft ();
		} else if (enemyController.isFacingRight) {
			enemyController.MoveRight ();
		} else {
			enemyController.MoveLeft ();
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
