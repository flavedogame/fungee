using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	public DialogBubbleManager dialogManager;
	public List<DialogueBubble> characters;
	public TextAsset text;
	public FlowController flowController;
	public bool canTriggerRepeatedly;

//	public enum DialogTriggerType{
//		hit, achievement
//	}

	public int hitMask;
	public string achievements;

	//todo: better way to list all tags
	public string collisionTag;
	public bool isTriggeredByTouch;

	bool hasTriggered;
	// Use this for initialization
	void Start () {
		
	}

	bool DoesConformAchievement() {
		if (achievements.Length == 0)
			return true;
		string[] achievementList = achievements.Split ('|');
		foreach (string achievement in achievementList) {
			Debug.Log ("achievement " + achievement.Length);
			string[] achievementMightWithNot = achievement.Split ('!');
			if (achievementMightWithNot.Length > 1) {
				if (AchievementSystem.Instance.HasAchievementFinished (achievementMightWithNot [1])) {
					return false;
				}
			} else {
				if (!AchievementSystem.Instance.HasAchievementFinished (achievementMightWithNot [0])) {
					return false;
				}
			}

		}
		return true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isTriggeredByTouch && collisionTag.Length == 0 && DoesConformAchievement()) {
			//achievement trigger
			setDialog ();
		}

		if (isTriggeredByTouch) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
				
				Vector2 vTouchPos = Input.GetTouch (0).position;
				Debug.Log ("touch!"+vTouchPos);
				Ray ray = Camera.main.ScreenPointToRay (vTouchPos);
				RaycastHit2D hit = Physics2D.GetRayIntersection (ray);
				Debug.Log ("hit!" + hit.collider.gameObject+ " " + gameObject);
				if (hit.collider.gameObject == gameObject) {
					Debug.Log ("set dialog");
					setDialog ();
				}
			}
		}

//		if (!isTriggered && !flowController.finishFirstTV) {
//			Debug.Log ("trigger");
//			isTriggered = true;
//			flowController.finishFirstTV = true;
//			setDialog ();
//		}
	}

	void setDialog(){
		//check if dialog happening
		dialogManager.characters = new List<DialogueBubble> (characters);

		dialogManager.setDialog (text);
		if (!canTriggerRepeatedly) {
			Destroy (this);
			return;
		}
		hasTriggered = true;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (collisionTag.Length > 0 && col.gameObject.tag == collisionTag) {
			//todo: check achievement
			setDialog();
		}
	}
}
