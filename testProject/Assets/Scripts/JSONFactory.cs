﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using LitJson;

//list of JSON file with path extensions
//take in scene number, output Narrative Event
//only NarrativeManager should be able to use this script

namespace JSONFactory
{
	class JSONAssembly
	{
		/*private static Dictionary<string,  string> _resourceList = new Dictionary<int,string> {
			{"tv1","/Resources/TV1.json"}
		};*/

		/*private static string PathForScene(string key){
			string resourcePathResult;
			if (_resourceList.TryGetValue (key, out resourcePathResult)) {
				return _resourceList [key];
			} else {
				throw new Exception ("the scene number you provided is not inthe resource list");
			}
		}*/
		private static bool IsValidJSON (string path)
		{
			return (Path.GetExtension (path) == ".json") ? true : false;
		}
/*
		public static NarrativeEvent RunJSONFactoryForScene(int sceneNumber) {
			string resourcePath = PathForScene (sceneNumber);
			if (IsValidJSON (resourcePath)) {
				string jsonString = File.ReadAllText (Application.dataPath + resourcePath);
				NarrativeEvent narrativeEvent = JsonMapper.ToObject<NarrativeEvent> (jsonString);
				return narrativeEvent;
			} else {
				throw new Exception ("the json is not valid");
			}
		}*/

		public static NarrativeEvent RunJSONFactoryForDialog (TextAsset text)
		{
			string jsonString = text.text;
			NarrativeEvent narrativeEvent = JsonMapper.ToObject<NarrativeEvent> (jsonString);
			return narrativeEvent;
		}

	public static Dictionary<string,Achievement> RunJSONFactoryForAchievement (TextAsset text)
		{
			string jsonString = text.text;
		Dictionary<string,Achievement> achievementDictionary = JsonMapper.ToObject<Dictionary<string,Achievement>> (jsonString);
		return achievementDictionary;
		}

	public static void SaveAchievementToJson (Dictionary<string,Achievement> achievements)
	{
		JsonData json = JsonMapper.ToJson (achievements);
		Debug.Log ("json" + json.ToString());
			Debug.Log ("file path " + Application.dataPath);
		File.WriteAllText(Application.dataPath+"/Resources/achievement.json",json.ToString());
		//	string jsonString = text.text;
		//	Dictionary<string,Achievement> achievementDictionary = JsonMapper.ToObject<Dictionary<string,Achievement>> (jsonString);
		//	return achievementDictionary;
	}
	}


}
