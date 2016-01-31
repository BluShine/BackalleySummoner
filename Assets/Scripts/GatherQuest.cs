using UnityEngine;
using System.Collections;

public class GatherQuest : ContractBase {
    public GatherQuest()
    {
        this.repeatable = true;
        this.contractName = writeContractTitle();

    }
    public override float getContractPerformance(DemonBase demon)
    {
        GameManager.instance.GiveIngredients(Mathf.Max(demon.GetTier(), 4), 4);
		this.finalPerf = (0.25F * (-1f*demon.GetTier() + 5));
		return 0.25F * (-1f*demon.GetTier() + 5);
    }
    public override string writeContractTitle()
    {
        return "Gather Ingredients";
    }

	public override float getCachedPerformance() {
		return -10;
	}
}
