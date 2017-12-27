using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : MonoBehaviour {

	FlowController flowController;

	// Use this for initialization
	void Start () {
		flowController = GameObject.FindObjectOfType<FlowController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FinishZoomoutTV(){
		gameObject.SetActive (false);
		flowController.finishFirstTV = true;

	}
}
