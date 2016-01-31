using UnityEngine;
using System.Collections;

public class InvenScreenScript : MonoBehaviour {

	public GameObject initialScreen;


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
