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
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickTab () {
		foreach (Tab1Script t in GameObject.FindObjectsOfType<Tab1Script> ()) {
			t.tab.SetActive (false);
		}
		tab.SetActive (true);
	}
}
