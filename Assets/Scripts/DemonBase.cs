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
        var values = System.Enum.GetValues(typeof(Ingredients.stats));
        foreach (ItemBase i in itemInventory) {
			foreach(Ingredients.stats value in values) {
				statList [(int)value] += i.statList [(int)value];
			}
		}
	}
    public int GetTier()
    {
        return 1;
    }
}
