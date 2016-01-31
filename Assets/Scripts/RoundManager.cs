using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {
	public GameManager gameRunner;
	public List<ContractBase> completedContracts;
	public Text[] contractDisplays = new Text[6];
	public int roundCount = 0;
	public GameObject[] disableAfterStart;
	public void disableInvScreens () {foreach (GameObject g in disableAfterStart) g.SetActive (false);}
	public GameObject over;

	//assigns 5 contracts, cashes intermediate contracts, gives ingredients
	public void StartRound () {
		gameRunner.GenerateContracts();
		gameRunner.GiveIngredients(0, 8);
		for(int i = 0; i < contractDisplays.Length; i++) {
			contractDisplays [i].text = gameRunner.open_contracts [i].contractName;
		}
		gameRunner.updateUI ();
		IngredientUI.UpdateAllNumbers ();
		roundCount++;
		over.SetActive (false);
	}

	public void EndRound()
	{
		if (roundCount != 0 && gameRunner.cachedContracts.Count > 0) {
			gameRunner.cashAllContracts ();
			foreach (Text t in contractDisplays)
				t.gameObject.SetActive (true);
		}
		if (GameManager.instance.reputation < 0) {
			EndStates.Lose (GameManager.instance.hellBucks);
			return;
		} else if(this.roundCount > 10 && GameManager.instance.hellBucks > 200)
		{
			EndStates.Win (GameManager.instance.hellBucks);
			return;
		} else if(this.roundCount > 10)
		{
			EndStates.Neutral (GameManager.instance.hellBucks);
			return;
		}
		foreach (ContractBase CB in gameRunner.open_contracts.Where (elem => elem.accepted).ToArray()) {
			gameRunner.cashContract (CB);
		}
		over.SetActive (true);
	}
}