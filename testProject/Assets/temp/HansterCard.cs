using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HansterCard : Card {
	public GameObject instancedMonster;
	public string monsterName;
	public GameManager.MonsterType monsterType;

	// Use this for initialization
	override protected void Start () {
		base.Start ();
		GetComponentInChildren<Text> ().text = monsterName + "\n" + cost;

	}
	
	// Update is called once per frame




	override protected void UseCard(){
		//Debug.Log ("create hamster");
		Instantiate (instancedMonster, PlayerManager.Instance.spawnPosition.position, Quaternion.identity);
	}
}
