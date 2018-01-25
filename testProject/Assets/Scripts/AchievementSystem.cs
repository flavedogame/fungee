using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : Singleton<AchievementSystem> {

	protected AchievementSystem(){}

	public TextAsset text;

	Dictionary<string,Achievement> achievements;

	// Use this for initialization
	void Start () {
		achievements = JSONFactory.JSONAssembly.RunJSONFactoryForAchievement (text);
		//Debug.Log ("achievement " + achievements ["finishFirstTV"].finishValue);
		ResetAchievement();
		//AddAchievement ("finishFirstTV", 1);
		//SaveAchievement ();
		//put achievement name into a list
		//read achievement from json
	}

	void ResetAchievement(){
		List<string> names = new List<string> (achievements.Keys);
		foreach (string name in names) {
			Debug.Log ("reset achievement " + name);
			ModifyAchievement (name, 0,false);
			//set to default instead of 0?
		}
		SaveAchievement ();
	}

	public Achievement GetAchievement(string name) {
		if (!achievements.ContainsKey(name)) {
			Debug.LogError ("achievement not exist " + name);
		}
		return achievements [name];
	}

	public bool HasAchievementFinished(string name){
		Achievement a = GetAchievement (name);
		return (a.currentValue >= a.finishValue);
	}

	public void AddAchievement(string name, int addValue){
		Debug.Log ("add achievement " + name +" "+addValue);
		ModifyAchievement(name, achievements[name].currentValue+addValue);
	}

	void ModifyAchievement(string name, int value, bool shouldSave = true) {
		//check if name exist
		if (!achievements.ContainsKey(name)) {
			Debug.LogError ("achievement not exist " + name);
		}
		Achievement achievement = achievements [name];
		achievement.currentValue = value;
		achievements [name] = achievement;
		if (shouldSave) {
			SaveAchievement ();
		}
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
