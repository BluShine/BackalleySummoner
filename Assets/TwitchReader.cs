using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwitchReader : MonoBehaviour {

    public TwitchInterface tInterface;

    List<string> spawnableIngredients;

    Dictionary<string, Texture2D> emotes;
    public bool useEmotes = false;
    bool emotesLoaded = false;

    static string emoteURL = "https://api.twitch.tv/kraken/chat/*/emoticons";

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

        if(useEmotes)
        {
            StartCoroutine(LoadEmotes());
        }
    }

    IEnumerator LoadEmotes()
    {
        emotes = new Dictionary<string, Texture2D>();
        //TODO: Do this stuff in a coroutine
        WWW emoteData = new WWW(emoteURL.Replace("*", tInterface.username));

        yield return emoteData.isDone;


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
                    GameManager.instance.HeldIngredients[s] = Mathf.Min(0, GameManager.instance.HeldIngredients[s] - 1);
                else
                    GameManager.instance.HeldIngredients[s]++;
                IngredientUI.UpdateAllNumbers();
                return;
            }
        }
    }
}
