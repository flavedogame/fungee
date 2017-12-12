using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class AnimationManger : MonoBehaviour, IManager {

	Animator panelAnimator;

	public ManagerState currentState{ get; private set; }
	public void BootSequence() {
		Debug.Log (string.Format ("{0} is booting up", GetType ().Name));
		panelAnimator = GameObject.Find ("Canvas").GetComponent<Animator> ();
		currentState = ManagerState.Completed;
		Debug.Log (string.Format ("{0} status = {1}", GetType ().Name, currentState));
	}

	public IEnumerator IntroAnimation() {
		yield return AnimateWithTuple (Constants.AnimationTuples.introAnimation);
	}

	public IEnumerator ExitAnimation(){
		yield return AnimateWithTuple (Constants.AnimationTuples.exitAnimation);
	}

	public IEnumerator AnimateWithTuple(Constants.AnimationTuple animationTuple){
		panelAnimator.SetBool (animationTuple.parameter, animationTuple.value);
		yield return new WaitForSeconds (1);
	}
}
