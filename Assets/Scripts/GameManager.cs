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
	void Start() {
		Debug.Log ("Game Manager Start");
		/*Recipes.instance = new Recipes ();
		Recipes.instance.RecipeStart ();*/
        reputation = 0;
        open_contracts = new ContractBase[6];
		for(int i = 0; i < open_contracts.Length; i++) {
			open_contracts[i] = new ContractBase (1F);
		}
		HeldIngredients = new Dictionary<string, int>();
        //open_contracts[5] = new Gather_Contract();
        instance = this;
		GenerateContracts ();
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
		List<Ingredients> il = new List<Ingredients> ();

		int batches = num / 3;
		var values = System.Enum.GetValues(typeof(Ingredients.bodyParts));
		System.Random rnd = new System.Random((int)System.DateTime.Now.Ticks);
        if(!this.HeldIngredients.Any())
        {
            foreach (KeyValuePair<string, Ingredients> pair in Recipes.instance.name_ingredient)
            {
                HeldIngredients.Add(pair.Key, 0);
            }
        }
		for(int x = 0; x < batches; x++)
		{
			foreach(Ingredients.bodyParts part in values)
			{
				HashSet<Ingredients> curr = new HashSet<Ingredients>(Recipes.instance.part_ingredient[part].Where(
                    elem => Recipes.instance.tier_ingredient[tier].
                    Contains(elem))
                    .ToList());
				int chk = rnd.Next(0, curr.Count);
				Debug.Log (chk);
				Ingredients j = curr.ToArray()[chk];
				this.HeldIngredients[j.GetName()]++;
				il.Add (j);
			}
		}
		num -= (batches * 3);
		for (int x = 0; x < num; x++)
		{
			HashSet<GameObject> curr = Recipes.instance.tier_GmO[tier];
			int chk = rnd.Next(0, curr.Count);
			string j = curr.ToArray()[chk].name;
			this.HeldIngredients[j]++;
			il.Add (Recipes.instance.name_ingredient[j]);
		}

		ItemPopup.Popup (il);
	}
}