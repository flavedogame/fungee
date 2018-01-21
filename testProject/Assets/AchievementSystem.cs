using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour {

	public TextAsset text;

	Dictionary<string,Achievement> achievements;

	// Use this for initialization
	void Start () {
		achievements = JSONFactory.JSONAssembly.RunJSONFactoryForAchievement (text);
		Debug.Log ("achievement " + achievements ["finishFirstTV"].finishValue);
		//AddAchievement ("finishFirstTV", 1);
		ResetAchievement();
		SaveAchievement ();
		//put achievement name into a list
		//read achievement from json
	}

	void ResetAchievement(){
		List<string> names = new List<string> (achievements.Keys);
		foreach (string name in names) {
			ModifyAchievement (name, 0);
			//set to default instead of 0?
		}
	}

	void AddAchievement(string name, int addValue){
		ModifyAchievement(name, achievements[name].currentValue+addValue);
	}

	void ModifyAchievement(string name, int value) {
		Achievement achievement = achievements [name];
		achievement.currentValue = value;
		achievements [name] = achievement;
	}

	void SaveAchievement(){
		JSONFactory.JSONAssembly.SaveAchievementToJson (achievements);
	}



	//when quit game, save achievement

	//get achievement info, how much have we finished
	
	// Update is called once per frame
	void Update () {
		
	}
}
