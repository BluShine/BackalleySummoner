using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GGJSplashScript : MonoBehaviour {
	public Image matRend;
	int life = 3;

	// Use this for initialization
	void Start () {
		//Destroy (gameObject, life);
		StartCoroutine(fadeAway());
	}
	
	IEnumerator fadeAway () {
		WaitForSeconds WS = new WaitForSeconds (1F);
		yield return WS;
		WS = new WaitForSeconds (0.07f);
		for(float a = 1F; a > 0F; a -= 0.1F) {
			Color oldC = matRend.color;
			matRend.color = new Color (oldC.r, oldC.g, oldC.b, a);
			yield return WS;
		}
		Destroy (gameObject);
	}
}
