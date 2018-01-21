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
		//put achievement name into a list
		//read achievement from json
	}



	//when quit game, save achievement

	//get achievement info, how much have we finished
	
	// Update is called once per frame
	void Update () {
		
	}
}
