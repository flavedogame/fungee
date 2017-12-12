using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCards : MonoBehaviour {
	public bool isAttack;
	// Use this for initialization
	void Start () {
		List<GameManager.MonsterType> list;
		if (isAttack) {
			list = PlayerPrefManager.Instance.attackMonsters;
		} else {
			list = PlayerPrefManager.Instance.ownedMonsters;
		}
		foreach (GameManager.MonsterType type in list) {
			Debug.Log (type);
			GameObject card =Instantiate( GameManager.Instance.monsterMapDictionary [type].cardObject) as GameObject;
			card.transform.parent = transform;
			card.transform.localScale = new Vector3 (1, 1, 1);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
