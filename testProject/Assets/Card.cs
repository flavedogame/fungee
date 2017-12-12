using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
	public int cost = 30;

	Button button;
	// Use this for initialization
	virtual protected void Start () {
		button = GetComponent<Button> ();
	}
	
	// Update is called once per frame
	protected void Update () {
		if (CanUseCard ()) {
			button.interactable = true;
		} else {
			button.interactable = false;
		}
	}

	public void ClickCard(){
		//cost money
		if (CanUseCard()) {
			//Debug.Log ("buy");
			if (GameManager.Instance.isInBattle) {
				PlayerManager.Instance.money -= cost;
				UseCard ();
			} else {
				CardIntroduction.Instance.selectMonsterType (((HansterCard)this).monsterType);
			}
		} else {
			//Debug.Log ("cant buy");
		}
		//instantiate
	}

	protected bool CanUseCard(){
		if (GameManager.Instance.isInBattle) {
			if (PlayerManager.Instance.money > cost) {
				return true;
			} else {
				return false;
			}
		}
		return true;
	}

	virtual protected void UseCard(){
		Debug.LogError ("should not use general card");
	}
}
