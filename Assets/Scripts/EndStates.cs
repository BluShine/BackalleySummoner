using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndStates : MonoBehaviour {
	// Use this for initialization
	public static void Win(float p)
	{
		decimal value = (decimal)p;
		GameObject.Find("Win").GetComponent<Text>().text = "You got " + value.ToString("C") +" Hellbucks";
		GameObject.Find ("Win").SetActive (true);
	}

	public static void Lose(float p)
	{
		decimal value = (decimal)p;
		GameObject.Find("Lose").GetComponent<Text>().text = "You got " + value.ToString("C") +" Hellbucks";
		GameObject.Find ("Lose").SetActive (true);
	}

	public static void Neutral(float p)
	{
		decimal value = (decimal)p;
		GameObject.Find("Neutral").GetComponent<Text>().text = "You got " + value.ToString("C") +" Hellbucks";
		GameObject.Find ("Neutral").SetActive (true);
	}
}
