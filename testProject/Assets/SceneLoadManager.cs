using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SceneManager.LoadScene (0, LoadSceneMode.Additive);


	}

	public void GameOver(){
		Debug.Log ("game over");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
