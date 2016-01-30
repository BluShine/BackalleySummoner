using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBase {

	//item stats
	//0 - 4 corresponds to strength, cleverness, seduction, deception, occult
	public List<float> statList = new List<float>();

	//added and multiplied amounts to add to the cash flow multiplier
	public float cashInAddend = 0F;
	public float cashInMultiplicand = 1F;

	public itemType type;

	public enum itemType {
		passive,
		oneTime
	}
}
