using UnityEngine;
using System.Collections;

public class TwitchReader : MonoBehaviour {

    public TwitchInterface tInterface;

	// Use this for initialization
	void Start () {
        //build list of items
        foreach
        //add listener
        tInterface.messageReciever += readMessage;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void readMessage(string message)
    {

    }
}
