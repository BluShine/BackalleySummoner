using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public float hellBucks;//cash dolla
	public float reputation;//how reputable you are, 0 to 100

	//TODO implement after initial demo
	public List<DemonBase> availableDemons;
	//items inactive in inventory
	public List<ItemBase> storedItems;
	//items providing passive upgrades
	public List<ItemBase> passiveItems;


	public float cashInflowMultiplier = 1F;


	//UI elements that correspond to visible stats
	public Text cashBox;//Hell Bucks
	public Text repBox;//Reputation




	//methods

	//updates the UI elements to the proper value
	public void updateUI () {
		cashBox.text = ((int)hellBucks).ToString ();
		repBox.text = ((int)reputation).ToString ();
	}

	//adds cash from contract to your cash reserve
	public void cashContract (float unscaledCashInflux) {
		unscaledCashInflux *= cashInflowMultiplier;
		hellBucks += unscaledCashInflux;
	}

	//applies a passive item and removes it from your inventory
	public void applyItem (int itemIndex) {
		ItemBase item = storedItems [itemIndex];
		if(item.type == ItemBase.itemType.passive) {
			cashInflowMultiplier *= item.cashInMultiplicand;
			cashInflowMultiplier += item.cashInAddend;

			storedItems.RemoveAt (itemIndex);
		}
	}

	//applies a one time use item to a specific demon and removes it from your inventory
	public void giveDemonItem (int itemIndex, DemonBase demon) {
		if(storedItems[itemIndex].type == ItemBase.itemType.oneTime) {
			demon.itemInventory.Add (storedItems[itemIndex]);
			demon.applyItemBoosts ();
			storedItems.RemoveAt (itemIndex);
		}
	}
}