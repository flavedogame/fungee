using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using LitJson;

//list of JSON file with path extensions
//take in scene number, output Narrative Event
//only NarrativeManager should be able to use this script

namespace JSONFactory{
	class JSONAssembly{
		private static Dictionary<int,  string> _resourceList = new Dictionary<int,string> {
			{1,"/Resources/Event1.json"}
		};

		private static string PathForScene(int sceneNumber){
			string resourcePathResult;
			if (_resourceList.TryGetValue (sceneNumber, out resourcePathResult)) {
				return _resourceList [sceneNumber];
			} else {
				throw new Exception ("the scene number you provided is not inthe resource list");
			}
		}
		private static bool IsValidJSON(string path) {
			return (Path.GetExtension (path) == ".json") ? true : false;
		}
		public static NarrativeEvent RunJSONFactoryForScene(int sceneNumber) {
			string resourcePath = PathForScene (sceneNumber);
			if (IsValidJSON (resourcePath)) {
				string jsonString = File.ReadAllText (Application.dataPath + resourcePath);
				NarrativeEvent narrativeEvent = JsonMapper.ToObject<NarrativeEvent> (jsonString);
				return narrativeEvent;
			} else {
				throw new Exception ("the json is not valid");
			}
		}
	}
}
