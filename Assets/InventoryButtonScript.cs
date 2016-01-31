﻿using UnityEngine;
using System.Collections;

public class InventoryButtonScript : MonoBehaviour {

	public GameObject initialScreen;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DoStuff() {
		Debug.Log ("inventory pressed");
	}

	public void MakeVisible () {
		foreach (Transform t in transform) {
			initialScreen.SetActive (true);
			t.gameObject.SetActive (true);
		}
	}

	public void MakeInvisible () {
		foreach (Transform t in transform) {
			t.gameObject.SetActive (false);
		}
	}
		
}
