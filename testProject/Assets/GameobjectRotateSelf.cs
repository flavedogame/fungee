using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectRotateSelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * Time.deltaTime*15);
		float scale = Mathf.Sin(Time.time)  / 30.0f;
		transform.localScale = new Vector3 (1, 1, 1) + new Vector3 (scale,scale, scale);
	}
}
