using UnityEngine;
using System.Collections.Generic;

public class Tab1Script : MonoBehaviour {

	public GameObject tab;

	static List<Tab1Script> alltabs;

	// Use this for initialization
	void Start () {
		if (alltabs == null) {
			alltabs = new List<Tab1Script> ();
			foreach (Tab1Script t in GameObject.FindObjectsOfType<Tab1Script>()) {
				alltabs.Add (t);
			}
			Debug.Log (GameObject.FindObjectsOfType<Tab1Script> ().Length);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickTab () {
		Debug.Log ("click"); 
		Debug.Log (GameObject.FindObjectsOfType<Tab1Script> ().Length);
		foreach (Tab1Script t in GameObject.FindObjectsOfType<Tab1Script> ()) {
			t.tab.SetActive (false);
		}
		tab.SetActive (true);
	}
}
