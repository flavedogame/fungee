using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleObject : MonoBehaviour {

	public bool isAlly;

	// Use this for initialization
	void Start () {
	}

	public void setupBattleObject(){
		if (!isAlly) {
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
