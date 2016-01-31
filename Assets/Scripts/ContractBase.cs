using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ContractBase {
	public float perfTimeMult = 1F;
	public float finalPerf;//how well the contract was completed in the end
	public float diff;
	public string contractName;//IMPORTANT: MUST  BE ASSIGNED TO POST POINT DISTRIBUTION

	//stats
	//0 - 4 corresponds to strength, cleverness, seduction, deception, occult
	public List<float> statReqList;
    public bool accepted;
    public bool repeatable;

	//sentence is verb + noun + adverb
	//verb is from top stat, adverb is from 2nd top stat
	public List<string[]> verbList = new List<string[]>(5);//no need for spaces at the beginning or ends
	public string[] nounList = {"president", "pizza guy", "tax collector", "pope", "mayor", "internet shitposter", "neighbor lady", "math teacher", "principal", "boss", "ceo", "in-laws"};
	public List<string[]> adverbList = new List<string[]>(5);

    public ContractBase()
    {
        this.accepted = false;
    }

	public ContractBase(float difficulty) {
		diff = difficulty;
		statReqList = (new float[] {0F, 0F, 0F, 0F, 0F}).ToList();
		distributeStats (difficulty); 
		initWords ();
		contractName = writeContractTitle ();
        this.accepted = false;
        this.repeatable = false;

	}

	public ContractBase (int[] statReqs, float[] statDifficulties) {//to initialize a contract with a specific set of stat indices with specific difficulties.  the two arrays match up 1 to 1
		diff = statDifficulties.Sum();
		for(int i = 0; i < statReqs.Length; i++) {//for each index i of stat requirements
			statReqList [statReqs[i]] = statDifficulties [i];//set the stat requirement of the statReq index 
		}
		initWords ();
		contractName = writeContractTitle ();
	}

	//initializes the arrays of verbs and adverbs
	private void initWords () {
		verbList.Add (new string[] {"kill", "hurt", "maim", "crush", "rough up"});//power
		verbList.Add (new string[] {"fool", "dupe", "trick", "stupify", "mislead"});//cleverness
		verbList.Add (new string[] {"attract", "allure", "tempt", "maneuver", "lure"});//seduction
		verbList.Add (new string[] {"lie to", "swindle", "cheat", "dupe", "delude"});//deception
		verbList.Add (new string[] {"electrify", "singe", "flood", "reduce", "disintegrate"});//occult

		adverbList.Add (new string[] {"brutally", "painfully", "maliciously", "cruelly", "inhumanely"});
		adverbList.Add (new string[] {"sneakily", "intelligently", "silently", "dexterously", "adpetly"});
		adverbList.Add (new string[] {"charmingly", "elegantly", "swankily", "deftly", "nimbly"});
		adverbList.Add (new string[] {"amorally", "unscrupulously", "unprinciply", "unethically", "slyly"});
		adverbList.Add (new string[] {"archaically", "arcanely", "magically", "supernaturally", "mystically"});
	}

	//distributes the difficulty randomly throughout the different stat requirements
	private void distributeStats (float difficulty) {
		int statCount = 0;
		for (int i = 0; i < statReqList.Count; i++) {
			statReqList [i] = Random.value;
			if(statReqList[i] > 0.5F) {
				statCount++;
			}
		}
		float diffBase = difficulty / statCount;//the mid value of stat requirements
		for (int i = 0; i < statReqList.Count; i++) {
			if (statReqList [i] > 0.5F) {
				statReqList [i] = Mathf.Abs(Random.Range (diffBase - (diffBase / 2), diffBase + (diffBase / 2)));
			} else {
				statReqList [i] = 0F;
			}
		}
	}

	//returns a number between 1 and 0 that represents how well a contract went
	public virtual float getContractPerformance (DemonBase demon) {
		float output = 1F;
		for(int i = 0; i < statReqList.Count; i++) {
			float f = statReqList [i];
			if (f > 0.01F) {//avoid div/0 errors
				output *= demon.statList [i] / statReqList [i];
			}
		}
		finalPerf = output;
		return output;
	}

	//returns the time it takes to complete a contract
	public virtual float getContractCompletionTime (DemonBase demon) {
		float perf = getContractPerformance (demon);
		float compTime = perfTimeMult / perf;
		return compTime;
	}

	//builds an appropriate title for the contract
	public virtual string writeContractTitle () {

		List<float> orderedStats;
		float[] tempArr = new float[5];
		statReqList.CopyTo (tempArr);
		orderedStats = tempArr.ToList ();
		orderedStats.Sort ();
		float maxStat = orderedStats[orderedStats.Count - 1];//first and second most important stats
		int maxLoc = statReqList.IndexOf(maxStat);
		float secondMaxStat = orderedStats[orderedStats.Count - 2];
		int secondMaxLoc = statReqList.IndexOf (secondMaxStat);

		string noun;

		string verb = verbList [maxLoc] [(int)(Random.value * verbList [maxLoc].Length)];
		string adverb;
		noun = nounList [(int)(Random.value * nounList.Length)];
		adverb = adverbList [secondMaxLoc] [(int)(Random.value * adverbList [secondMaxLoc].Length)];

		return (this.diff + "\t" + verb + " the " + noun + " " + adverb);
	}

	// Returns how well you did on the contract the last time you ran it
	public virtual float getCachedPerformance() {
		return 6.6F / (5F * Mathf.Pow ((diff * Mathf.Log (finalPerf)), (2F / 5)));
	}
}