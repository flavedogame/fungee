using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer {
	void OnNotify ();
}

public class StepManager : MonoBehaviour {
	List<Observer> observers = new List<Observer>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddObserver(Observer observer){
		observers.Add (observer);
	}

	public void RemoveObserver(Observer observer){
		observers.Remove (observer);
	}

	public void Notify(){
		for (int i = 0; i < observers.Count; i++) {
			observers [i].OnNotify ();
		}
	}
}
