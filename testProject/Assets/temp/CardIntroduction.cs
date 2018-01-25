using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardIntroduction : MonoBehaviour {
	private static CardIntroduction _instance;
	public static CardIntroduction Instance{get{return _instance;}}
	void Awake(){
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
		} else {
			_instance = this;
		}
	}
	public GameObject sceneLoc;
	public Text info;
	public Text attack;
	public Text hp;
	// Use this for initialization
	void Start () {
		
	}

	public void selectMonsterType(GameManager.MonsterType type){
		if (sceneLoc.transform.childCount > 0) {
			DestroyImmediate (sceneLoc.transform.GetChild (0).gameObject);
		}
		GameObject monster = Instantiate (GameManager.Instance.monsterMapDictionary [type].spawnObject) as GameObject;
		monster.transform.parent = sceneLoc.transform;
		monster.transform.localPosition = new Vector3 (0, 0, 10);
		Hamster monsterComp = monster.GetComponentInChildren<Hamster> ();
		info.text = monsterComp.info;
		attack.text = "Attack: "+monsterComp.attack.ToString();
		hp.text ="HP: "+ monsterComp.maxHealth.ToString();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
