using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonBase : MonoBehaviour {
	public Sprite demonGraphic;

	//demon stats
	//0 - 4 corresponds to strength, cleverness, seduction, deception, occult
	public List<float> statList = new List<float>();

	//all items held by the demon
	public List<ItemBase> itemInventory;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void applyItemBoosts () {
		foreach (ItemBase i in itemInventory) {
			for (int j = 0; j < 5; j++) {
				statList [j] += i.statList [j];
			}
		}
	}
}
