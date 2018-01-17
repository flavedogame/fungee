using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : MonoBehaviour {

	FlowController flowController;
	bool isZoomout;
	bool isZoomin;
	Rect zoomStart;
	Rect zoomTarget;
	float startZoomTime;
	Camera thisCamera;
	float zoomDuration  = 0.3f;
	bool isZoomedIn = true;
	// Use this for initialization
	void Start () {
		flowController = GameObject.FindObjectOfType<FlowController> ();
		thisCamera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isZoomout||isZoomin) {
			if (Time.time >= startZoomTime + zoomDuration) {

				thisCamera.enabled = false;
				if (isZoomout) {
					FinishZoomoutTV ();
				}

				isZoomout = false;
			}
			float t = (Time.time - startZoomTime)/zoomDuration;
			thisCamera.rect = new Rect (Mathf.SmoothStep (zoomStart.x, zoomTarget.x, t),
				Mathf.SmoothStep (zoomStart.y, zoomTarget.y, t),
				Mathf.SmoothStep (zoomStart.width, zoomTarget.width, t),
				Mathf.SmoothStep (zoomStart.height, zoomTarget.height, t));
			
		}
	}

	public void Zoomout(){
		isZoomout = true;
		startZoomTime = Time.time;
		zoomStart = thisCamera.rect;

		Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
		Debug.Log ("screen pos is " + screenPos);
		Debug.Log ("screen pos after is " + screenPos.x/Screen.width);
		zoomTarget = new Rect (screenPos.x/Screen.width - 0.22f, 0.36f, 0.39f, 0.435f);
		//thisCamera.rect = new Rect (0.1f, zoomTarget.y, zoomTarget.width, zoomTarget.height);
		//thisCamera.pixelRect = new Rect (0.1f, zoomTarget.y, zoomTarget.width, zoomTarget.height);
	}

	public void Zoomin(){
		Debug.Log ("is zoomed in" + isZoomedIn);
		if (isZoomedIn) {
			return;
		}
		isZoomedIn = true;
		isZoomin = true;
		thisCamera.enabled = true;
		startZoomTime = Time.time;
		zoomStart = thisCamera.rect;
		Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
		Debug.Log ("screen pos is " + screenPos);
		zoomTarget = new Rect (0f,0f,1.3f,1f);
	}

	public void FinishZoomoutTV(){

		isZoomedIn = false;
		thisCamera.enabled = false;
		flowController.finishFirstTV = true;

	}
	public void FinishZoominTV(){

	}
}
