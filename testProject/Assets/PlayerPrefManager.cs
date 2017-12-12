using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefManager : MonoBehaviour {

	public Text rubyText;
	public Text levelText;

	private static PlayerPrefManager _instance;
	public static PlayerPrefManager Instance{get{return _instance;}}
	void Awake(){
		DontDestroyOnLoad (gameObject);
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
		} else {
			_instance = this;
		}
	}

	public int ruby;
	public int level;
	public List<GameManager.MonsterType> ownedMonsters;
	public List<GameManager.MonsterType> attackMonsters;
	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("firstTimeOpen")) {
			PlayerPrefs.SetInt ("firstTimeOpen", 1);
			PlayerPrefs.SetInt ("ruby", 20);
			PlayerPrefs.SetInt ("level", 1);
			PlayerPrefs.SetInt ("hamster_owned", 1);
			PlayerPrefs.SetInt ("hamster_attack", 1);
			PlayerPrefs.SetInt ("giraffe_owned", 1);
		}

			ruby = PlayerPrefs.GetInt ("ruby");

			level = PlayerPrefs.GetInt ("level");
		
		rubyText.text = "Ruby: " + ruby;
		levelText.text = "Level: " + level;

		string[] monsterTypes = System.Enum.GetNames (typeof(GameManager.MonsterType));
		foreach (string type in monsterTypes) {
			Debug.Log (type);
			GameManager.MonsterType typeEnum = (GameManager.MonsterType )System.Enum.Parse(typeof(GameManager.MonsterType),type);

			if (PlayerPrefs.HasKey (type + "_attack") && PlayerPrefs.GetInt (type + "_attack") == 1) {
				attackMonsters.Add (typeEnum);
			}else if (PlayerPrefs.HasKey (type + "_owned") && PlayerPrefs.GetInt (type + "_owned") == 1) {
				ownedMonsters.Add (typeEnum);
			}
		}
		foreach (GameManager.MonsterType t in attackMonsters) {
			Debug.Log (t);
		}
	}

	public void addRuby(int value){
		ruby += value;
		PlayerPrefs.SetInt ("ruby", ruby);
		rubyText.text = "Ruby: " + ruby;
	}

	public void addLevel(int value){
		level += value;
		PlayerPrefs.SetInt ("level", level);
		levelText.text = "Level: " + level;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
