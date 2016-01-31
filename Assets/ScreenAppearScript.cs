using UnityEngine;
using System.Collections;

public class ScreenAppearScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MakeVisible () {
		foreach (Transform t in transform) {
			t.gameObject.SetActive (true);
		}
	}

	public void MakeInvisible () {
		foreach (Transform t in transform) {
			t.gameObject.SetActive (false);
		}
	}
}
