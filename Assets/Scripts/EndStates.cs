using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndStates : MonoBehaviour {

    public GameObject win;
    public GameObject lose;
    public GameObject neutral;

    static EndStates instance;

    void Start()
    {
        instance = this;
    }

    // Use this for initialization
    public static void Win(float p)
	{
		instance.win.GetComponentInChildren<Text>().text = "Win!\nYou got " + p +" Hellbucks";
        instance.win.SetActive (true);
	}

	public static void Lose(float p)
	{
        instance.lose.GetComponentInChildren<Text>().text = "Lose!\nYou got " + p +" Hellbucks";
		instance.lose.SetActive (true);
	}

	public static void Neutral(float p)
	{
        Debug.Log(p);
        instance.neutral.GetComponentInChildren<Text>().text = "Game Over!\nYou got " + p +" Hellbucks";
        instance.neutral.SetActive (true);
	}
}
