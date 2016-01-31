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
        this.finalPerf = -1f * demon.GetTier();
        return -1f*demon.GetTier();
    }
    public override string writeContractTitle()
    {
        return "Gather Ingredients";
    }
}
