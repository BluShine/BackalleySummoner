using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContractBase {
	public float perfTimeMult = 1F;
	public float finalPerf;//how well the contract was completed in the end

	//stats
	//0 - 4 corresponds to strength, cleverness, seduction, deception, occult
	public List<float> statReqList = new List<float>();

	/*
	 * public float strengthReq;
	 * public float clevernessReq;
	 * public float seductionReq;
	 * public float deceptionReq;
	 * public float occultReq;
	*/

	public ContractBase(float difficulty) {
		int statCount = 0;
		for (int i = 0; i < 5; i++) {
			statReqList [i] = Random.value;
			if(statReqList[i] > 0.5F) {
				statCount++;
			}
		}
		float diffBase = difficulty / statCount;//the mid value of stat requirements
		for (int i = 0; i < 5; i++) {
			if (statReqList [i] > 0.5F) {
				statReqList [i] = Random.Range (diffBase - (diffBase / 2), diffBase + (diffBase / 2));
			} else {
				statReqList [i] = 0F;
			}
		}

	}


	//returns a number between 1 and 0 that represents how well a contract went
	public float getContractPerformance (DemonBase demon) {
        return 0;
	}
	public float getContractCompletionTime (DemonBase demon) {
		float perf = getContractPerformance (demon);
		float compTime = perfTimeMult / perf;
        return 0;
	}
}
