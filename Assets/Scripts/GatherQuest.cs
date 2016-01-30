using UnityEngine;
using System.Collections;

public class GatherQuest : ContractBase {
    public GatherQuest()
    {
        this.repeatable = true;
        this.contractName = writeContractTitle();

    }
    public override float getContractCompletionTime(DemonBase demon)
    {
        return .1f;
    }
    public override float getContractPerformance(DemonBase demon)
    {
        GameManager.instance.GiveIngredients(demon.GetTier(), 4);
        return 1f;
    }
    public override string writeContractTitle()
    {
        return "Gather Quest";
    }
}
