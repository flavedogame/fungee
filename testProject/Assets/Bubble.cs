using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {
	public GameObject thinkBubble;
	public GameObject talkBubble;
	// Use this for initialization
	void Start () {
		
	}

	public void UseThinkBubble(){
		thinkBubble.SetActive (true);
		talkBubble.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
