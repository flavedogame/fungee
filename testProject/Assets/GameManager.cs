using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	private static GameManager _instance;
	public static GameManager Instance{get{return _instance;}}
	void Awake(){
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
		} else {
			_instance = this;
		}
	}

	public bool isInBattle;
	public enum MonsterType {hamster,giraffe};
	[System.Serializable]
	public class MonsterMap{
		public MonsterType monsterType;
		public GameObject spawnObject;
		public GameObject cardObject;

	}
	public List<MonsterMap> monsterMapList;
	public Dictionary<MonsterType,MonsterMap> monsterMapDictionary;
	// Use this for initialization
	void Start () {
		monsterMapDictionary = new Dictionary<MonsterType,MonsterMap> ();
		foreach (MonsterMap mm in monsterMapList) {
			monsterMapDictionary [mm.monsterType] = mm;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
