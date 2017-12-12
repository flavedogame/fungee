using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderMove : MonoBehaviour {
	public float scrollSpeed = 0.1f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.deltaTime * scrollSpeed;
		transform.position = transform.position+ new Vector3 (offset,0,0);
	}
}
