using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	private static PlayerManager _instance;

	public int money;
	public int moneyLimit = 100;
	public float moneyIncreasePeriod=1f;
	public int moneyHighestLimit = 500;
	public int costToLevelUpMoney = 40;
	float moneyIncreaseTime;
	public float time;
	public List<GameObject> monsterList;
	public Transform spawnPosition;

	public static PlayerManager Instance {get{return _instance;}}


	void Awake(){
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
		} else {
			_instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (money <= moneyLimit) {
			moneyIncreaseTime += Time.deltaTime;
			if (moneyIncreaseTime >= moneyIncreasePeriod) {
				money += 1;
				moneyIncreaseTime -= moneyIncreasePeriod;
			}
		}
	}
}
