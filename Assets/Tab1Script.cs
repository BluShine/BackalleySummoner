using UnityEngine;
using System.Collections;

public class Tab1Script : MonoBehaviour {

	public GameObject tab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickTab () {
		Debug.Log ("click");
		tab.SetActive (!tab.activeSelf);
	}
}
