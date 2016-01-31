using UnityEngine;
using System.Collections;

public class EndStates : MonoBehaviour {

	// Use this for initialization
	public static void Win(float p)
	{
		GameObject.Find ("Win").SetActive (true);
	}

	public static void Lose(float p)
	{
		GameObject.Find ("Lose").SetActive (true);
	}

	public static void Neutral(float p)
	{
		GameObject.Find ("Neutral").SetActive (true);
	}
}
