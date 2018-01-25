using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamster : BattleMonster {
	public int attack = 20;
	public float attackPeriod = 1.5f;
	float timePassed;
	public int damageToHitBack = 50;
	public float attackRange = 50;
	public BattleHPObject target;
	Hamster allyBefore;
	public float speed = 10;
	Rigidbody2D rb;
	public string info;


	int currentOffset;
	static int zOffset;

	// Use this for initialization
	override protected void Start () {
		base.Start ();
		foreach (Anima2D.SpriteMeshInstance sr in GetComponentsInChildren<Anima2D.SpriteMeshInstance>()) {
			sr.sortingOrder += zOffset;
		}
		zOffset += 10;
		currentOffset = zOffset;
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (GameManager.Instance && !GameManager.Instance.isInBattle) {
			return;
		}
		if (isDead) {
			return;
		}
		if (target && target.isDead) {
			Debug.Log ("target is dead " + target);
			target = null;
		}
		if (allyBefore && allyBefore.isDead) {
			allyBefore = null;
		}
		if (!target) {
				Move ();
			if (allyBefore && allyBefore.target && !allyBefore.target.isDead) {
				Debug.Log ("allybefore is " + allyBefore);
				target = allyBefore.target;
			}
		} else {
			Debug.Log ("target is " + target);
			timePassed += Time.deltaTime;
			if (timePassed >= attackPeriod) {
				timePassed = 0;
				Attack (target);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("collide with " + col);
		if (col.gameObject.GetComponent<BattleHPObject> ()&&
			col.gameObject.GetComponent<BattleHPObject> ().isAlly ^ isAlly) {
			//Debug.Log ("target " + col.gameObject);
			target = col.gameObject.GetComponent<BattleHPObject>();
		} else if (col.gameObject.GetComponent<Hamster>()&&
			col.GetType () == typeof(CircleCollider2D)&&
			(col.gameObject.transform.position.x-transform.position.x)*(isAlly?-1:1)>0) {
			allyBefore = col.gameObject.GetComponent<Hamster>();
		}
	}



	override protected void Die(){
		if (isDead) {
			return;
		}
		Destroy (this.gameObject, 3);
		isDead = true;
		GetComponent<CircleCollider2D> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
		Debug.Log ("die");
		anim.SetBool ("die",true);
	}


	void Attack (BattleHPObject targetHam){
		Debug.Log ("attack");
		anim.SetTrigger ("attack");
		targetHam.GetDamage (attack);
	}

	void Move(){
		anim.SetBool ("move",true);
		rb.MovePosition (rb.position + new Vector2( speed * Time.deltaTime,0)*(isAlly?-1:1));
		//Debug.Log ("move");
	}
}
