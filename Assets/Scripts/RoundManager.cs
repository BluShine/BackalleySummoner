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
            Text t = contractDisplays[i];
            ContractBase c = gameRunner.open_contracts[i];
            t.text = c.contractName;
            t.transform.Find("Difficulty").GetComponent<Text>().text = c.diff;
            t.transform.Find("Money").GetComponent<Text>().text = "$" + c.rewardMoney;
            t.transform.Find("Reputation").GetComponent<Text>().text = "~" + c.diff;
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
		float tempRep = gameRunner.reputation;
		float tempMon = gameRunner.hellBucks;
		foreach (ContractBase CB in gameRunner.open_contracts.Where (elem => elem.accepted).ToArray()) {
			gameRunner.cashContract (CB);
		}
		over.SetActive (true);

		Text missionText = over.transform.FindChild ("Missions").GetComponent<Text> ();
		//open contracts in game manager
		string newText = "";
		foreach (ContractBase contract in gameRunner.open_contracts) {
			newText += contract.contractName.TrimStart(' ', '1', '2' ,'3', '4', '5', '\t') + ": " + ((contract.finalPerf > 1) ? "Success" : "Failure") + "\n";
		}
		missionText.text = newText;

		Text repText = over.transform.FindChild ("Rep").GetComponent<Text> ();
		repText.text = (gameRunner.reputation - tempRep).ToString();

		Text moneyText = over.transform.FindChild ("Money").GetComponent<Text> ();
		moneyText.text = (gameRunner.hellBucks - tempMon).ToString();
	}

   public void RefreshContractUI()
   {
      for(int i = 0; i < contractDisplays.Length; i++)
      {
         ContractBase contract = gameRunner.open_contracts[i];
         contractDisplays[i].gameObject.SetActive(!contract.accepted);
      }
   }
}