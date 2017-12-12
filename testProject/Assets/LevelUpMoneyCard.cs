using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMoneyCard : Card {

	override protected void Start () {
		base.Start ();
		cost = PlayerManager.Instance.costToLevelUpMoney;
		GetComponentInChildren<Text> ().text = "Level up Money" + "\n"+cost;

	}


	override protected void UseCard(){
		Debug.Log ("level up money");
		if (PlayerManager.Instance.moneyIncreasePeriod < PlayerManager.Instance.moneyHighestLimit) {
			PlayerManager.Instance.moneyIncreasePeriod -= 0.05f;
			PlayerManager.Instance.moneyLimit += 50;
			PlayerManager.Instance.costToLevelUpMoney += 40;
			cost = PlayerManager.Instance.costToLevelUpMoney;
		}
	}
}
