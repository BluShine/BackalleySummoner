using UnityEngine;
using System.Collections;

public class EndStates : MonoBehaviour {

	// Use this for initialization
	public static void Win(int p)
	{
		GameObject.Find ("Win").SetActive (true);
	}

	public static void Lose(int p)
	{
		GameObject.Find ("Lose").SetActive (true);
	}

	public static void Neutral(int p)
	{
		GameObject.Find ("Neutral").SetActive (true);
	}
}
