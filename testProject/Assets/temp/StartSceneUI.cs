using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour {

	public void ClickBattle(){
		SceneManager.LoadScene ("battle");
		GameManager.Instance.isInBattle = true;
	}

	public void ClickCards(){
		SceneManager.LoadScene ("cards");
		GameManager.Instance.isInBattle = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
