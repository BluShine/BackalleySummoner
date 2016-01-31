using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {
	public GameManager gameRunner;
	public List<ContractBase> completedContracts;
	public Text[] contractDisplays = new Text[6];
	void Start () {
		//gameRunner.GenerateContracts ();
		//StartRound ();
	}

	//assigns 5 contracts, cashes intermediate contracts, gives ingredients
	public void StartRound () {
		foreach (ContractBase CB in gameRunner.open_contracts.Where (elem => elem.accepted).ToArray()) {
			gameRunner.cashContract (CB);
		}
		gameRunner.GenerateContracts();
		gameRunner.GiveIngredients(1, 8);
		for(int i = 0; i < contractDisplays.Length; i++) {
			contractDisplays [i].text = gameRunner.open_contracts [i].writeContractTitle();
		}
		gameRunner.updateUI ();
	}
}