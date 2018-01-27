using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : MonoBehaviour {

	bool isZoomout;
	bool isZoomin;
	Rect zoomStart;
	Rect zoomTarget;
	float startZoomTime;
	Camera thisCamera;
	float zoomDuration  = 0.3f;
	public bool isZoomedIn = true;
	// Use this for initialization
	void Start () {
		thisCamera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isZoomout||isZoomin) {
			if (Time.time >= startZoomTime + zoomDuration) {
				if (isZoomout) {
					FinishZoomoutTV ();
				}
				isZoomin = isZoomout = false;
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
		isZoomin = false;
		isZoomedIn = false;
		startZoomTime = Time.time;
		zoomStart = thisCamera.rect;
		Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
		//Debug.Log ("screen pos is " + screenPos);
		//Debug.Log ("screen pos after is " + screenPos.x/Screen.width);
		zoomTarget = new Rect (screenPos.x/Screen.width - 0.22f, 0.36f, 0.39f, 0.435f);
		//thisCamera.rect = new Rect (0.1f, zoomTarget.y, zoomTarget.width, zoomTarget.height);
		//thisCamera.pixelRect = new Rect (0.1f, zoomTarget.y, zoomTarget.width, zoomTarget.height);
	}

	public void Zoomin(){
		//Debug.Log ("is zoomed in" + isZoomedIn);
		isZoomin = true;
		isZoomout = false;
		isZoomedIn = true;
		thisCamera.enabled = true;
		startZoomTime = Time.time;
		zoomStart = thisCamera.rect;
		Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
		//Debug.Log ("screen pos is " + screenPos);
		zoomTarget = new Rect (0f,0f,1.3f,1f);
	}

	public void FinishZoomoutTV(){

		isZoomedIn = false;
		thisCamera.enabled = false;

	}
	public void FinishZoominTV(){

	}
}
