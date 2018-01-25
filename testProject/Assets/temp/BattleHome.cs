using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHome : BattleHPObject {

	public Text tempGameoverText;
	
	// Update is called once per frame
	void Update () {
	}

	override protected void Die(){
		if (isDead) {
			return;
		}
		Destroy (this.gameObject, 3);
		isDead = true;
		GetComponent<BoxCollider2D> ().enabled = false;
		Debug.Log ("die");
		anim.SetBool ("die",true);
		Gameover ();
	}

	void Gameover ()	{
		tempGameoverText.enabled = true;
	}
}
