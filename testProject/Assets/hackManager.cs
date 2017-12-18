using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hackManager : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.active) {
			player.SetActive (true);
		}
	}
}
