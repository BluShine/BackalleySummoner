using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {
	public float hellBucks;//cash dolla
	public float reputation;//how reputable you are, 0 to 100
	public static GameManager instance;
   public RoundManager roundManager;

	//TODO implement after initial demo
	public List<DemonBase> availableDemons;
	//items inactive in inventory
	public List<ItemBase> storedItems;
	//items providing passive upgrades
	public List<ItemBase> passiveItems;

	public Dictionary<string, int> HeldIngredients;


	public float cashInflowMultiplier = 1F;

	public ContractBase[] open_contracts;
	public List<ContractBase> cachedContracts = new List<ContractBase>(0);
	//UI elements that correspond to visible stats
	public Text cashBox;//Hell Bucks
	public Text repBox;//Reputation


	//methods
	void Start() {
		/*Recipes.instance = new Recipes ();
		Recipes.instance.RecipeStart ();*/
        reputation = 0;
        open_contracts = new ContractBase[6];
		for(int i = 0; i < open_contracts.Length; i++) {
			open_contracts[i] = new ContractBase (1F);
		}
		HeldIngredients = new Dictionary<string, int>();
        foreach (KeyValuePair<string, Ingredients> pair in Recipes.instance.name_ingredient)
        {
            HeldIngredients.Add(pair.Key, 0);
        }
        open_contracts[5] = new GatherQuest();
        GenerateContracts();
        instance = this;
        roundManager = GetComponent<RoundManager>();
	}

	//updates the UI elements to the proper value
	public void updateUI () {
		cashBox.text = ((int)hellBucks).ToString () + " HELLBUCKS";
		repBox.text = ((int)reputation).ToString () + " REPUTATION";
	}

	//adds cash from contract to your cash reserve
	public void cashContract (ContractBase contract) {
		if(!contract.repeatable) contract.accepted = true;
		float cashInflux = contract.finalPerf * contract.diff;
		cashInflux *= cashInflowMultiplier;
		hellBucks += cashInflux;
		reputation += contract.getCachedPerformance();
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
		for(int x = 0; x < batches; x++)
		{
			foreach(Ingredients.bodyParts part in values)
			{
				HashSet<Ingredients> curr = new HashSet<Ingredients>(Recipes.instance.part_ingredient[part].Where(
                    elem => Recipes.instance.tier_ingredient[tier].
                    Contains(elem))
                    .ToList());
				int chk = rnd.Next(0, curr.Count);
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

		//ItemPopup.Popup (il);
		IngredientUI.UpdateAllNumbers ();
	}



	public GameObject demonScreen;
	public GameObject requestScreen;
	ContractBase currentUnassignedContract;
	public Text topRightField;
	//click quest, set it to active if not repeating, go to demon screen
	public void goToDemonAssignment (int contractIndex) {//call when you click a contract
		demonScreen.SetActive (true);
		requestScreen.SetActive (false);
		currentUnassignedContract = open_contracts[contractIndex];
		topRightField.text = open_contracts [contractIndex].contractName;
		Debug.Log (open_contracts[contractIndex].contractName);
	}

	public void assignDemon (DemonBase demon) {//call when submitting a demon for a contract
		currentUnassignedContract.getContractPerformance (demon);
		demonScreen.SetActive (false);
		if (currentUnassignedContract.repeatable) {
			cashContract (currentUnassignedContract);
		}
		else cacheContract (currentUnassignedContract);
      roundManager.RefreshContractUI();
      // Now that we've checked the demon, we can get rid of it
      Destroy(demon.gameObject);
	}

	private void cacheContract (ContractBase contract) {//called after assignDemon
      if(!contract.repeatable) contract.accepted = true;
		cachedContracts.Add (contract);
	}

	public void cashAllContracts () {//call when round starts
		foreach (ContractBase CB in cachedContracts) {
			cashContract (CB);
		}
		cachedContracts.Clear ();
	}
}