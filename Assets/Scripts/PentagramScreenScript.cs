using UnityEngine;
using System.Collections;

public class PentagramScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveIntoMain () {
		foreach (Transform t in transform) {
			t.gameObject.SetActive (true);
		}
	}
}
