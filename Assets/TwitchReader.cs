using UnityEngine;
using System.Collections.Generic;

public class TwitchReader : MonoBehaviour {

    public TwitchInterface tInterface;

    List<string> spawnableIngredients;

	// Use this for initialization
	void Start () {
        //build list of items
        spawnableIngredients = new List<string>();
        foreach (string s in Recipes.instance.name_ingredient.Keys)
        {
            spawnableIngredients.Add(s);
        }
        //add listener
        tInterface.messageReciever += readMessage;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void readMessage(string message)
    {
        foreach(string s in spawnableIngredients)
        {
            if(message.Contains(s))
            {
                if(message.Contains("-"))
                    GameManager.instance.HeldIngredients[s]--;
                else
                    GameManager.instance.HeldIngredients[s]++;
                return;
            }
        }
    }
}
