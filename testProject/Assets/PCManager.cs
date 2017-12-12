using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCManager : MonoBehaviour {
	[System.Serializable]
	public class EnemySpawn{
		public float timeBefore;
		public int spawnRepeatTime;
		public GameManager.MonsterType monsterType;
	}
	public List<EnemySpawn> enemyList;
	public Transform spawnPosition;

	int index;
	int repeatIndex;
	float time;
	// Use this for initialization
	void Start () {
		index = repeatIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= enemyList [index].timeBefore) {
			time = 0;
			GameObject go = Instantiate (GameManager.Instance.monsterMapDictionary [enemyList [index].monsterType].spawnObject, spawnPosition.position, Quaternion.identity) as GameObject;
			go.GetComponentInChildren<Hamster> ().isAlly = false;
			go.GetComponentInChildren<Hamster> ().setupBattleObject ();
			repeatIndex++;
			if (repeatIndex >= enemyList [index].spawnRepeatTime) {
				index++;
				repeatIndex = 0;
			}
			if (index >= enemyList.Count) {
				index = 0;
			}
		}
	}
}
