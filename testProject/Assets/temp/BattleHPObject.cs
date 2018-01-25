using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHPObject : BattleObject {
	public int maxHealth = 100;
	int health;
	int damageTaken;

	public bool isDead;


	protected Animator anim;

	// Use this for initialization
	virtual protected void Start () {
		health = maxHealth;

		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetDamage(int damage){
		health -= damage;
		damageTaken += damage;
		if (health <= 0) {
			Die ();
		}
	}

	virtual protected void Die(){
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
}
