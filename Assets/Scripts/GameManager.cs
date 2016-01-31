using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {
	public float hellBucks;//cash dolla
	public float reputation;//how reputable you are, 0 to 100
	public static GameManager instance;

	//TODO implement after initial demo
	public List<DemonBase> availableDemons;
	//items inactive in inventory
	public List<ItemBase> storedItems;
	//items providing passive upgrades
	public List<ItemBase> passiveItems;

	public Dictionary<string, int> HeldIngredients;


	public float cashInflowMultiplier = 1F;

	public ContractBase[] open_contracts;
	//UI elements that correspond to visible stats
	public Text cashBox;//Hell Bucks
	public Text repBox;//Reputation




	//methods
	void Start()
	{
		open_contracts = new ContractBase[6];
		for(int i = 0; i < open_contracts.Length; i++) {
			open_contracts[i] = new ContractBase (1F);
		}
		HeldIngredients = new Dictionary<string, int>();
		//open_contracts[5] = new Gather_Contract();
		instance = this;
	}

	//updates the UI elements to the proper value
	public void updateUI () {
		cashBox.text = ((int)hellBucks).ToString () + " HELLBUCKS";
		repBox.text = ((int)reputation).ToString () + " REPUTATION";
	}

	//adds cash from contract to your cash reserve
	public void cashContract (ContractBase contract) {
		float cashInflux = contract.finalPerf * contract.diff;
		cashInflux *= cashInflowMultiplier;
		hellBucks += cashInflux;
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
	public void GenerateContracts()
	{
		System.Random rnd = new System.Random((int)System.DateTime.Now.Ticks);
		for(int x = 0; x < 5; x++)
		{
			int difficulty = (int)Mathf.Min(5,Mathf.Max(reputation / 20 + rnd.Next(-1, 1), 1));
			open_contracts[x] = new ContractBase(difficulty);
		}
	}
	public void GiveIngredients(int tier, int num)
	{
		int batches = num / 3;
		var values = System.Enum.GetValues(typeof(Ingredients.bodyParts));
		System.Random rnd = new System.Random((int)System.DateTime.Now.Ticks);
		for(int x = 0; x < batches; x++)
		{
			foreach(Ingredients.bodyParts part in values)
			{
				HashSet<Ingredients> curr = Recipes.instance.part_ingredient[part];
				int chk = rnd.Next(0, curr.Count);
				this.HeldIngredients[curr.ToArray()[chk].GetName()]++;
			}
		}
		num -= (batches * 3);
		for (int x = 0; x < num; x++)
		{
			HashSet<GameObject> curr = Recipes.instance.tier_ingredient[tier];
			int chk = rnd.Next(0, curr.Count);
			this.HeldIngredients[curr.ToArray()[chk].name]++;
		}
	}
}