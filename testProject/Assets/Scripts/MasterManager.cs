using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogBubbleManager))]
public class MasterManager : MonoBehaviour {
	private List<IManager> _managerList = new List<IManager> ();
	public static DialogBubbleManager dialogBubbleManager { get; private set; }
	void Awake(){
		dialogBubbleManager = GetComponent<DialogBubbleManager> ();
		_managerList.Add (dialogBubbleManager);
		StartCoroutine (BootAllManagers ());
	}

	private IEnumerator BootAllManagers(){
		foreach (IManager manager in _managerList) {
			manager.BootSequence ();
		}
		yield return null;
	}

}
