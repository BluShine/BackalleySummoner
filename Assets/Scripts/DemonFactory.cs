using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonFactory : MonoBehaviour
{
	public static DemonFactory Instance;
	public GameObject[] DemonTierPrefabs;
	public GameObject ParticlePrefab;


	private string[] limbNames = new string[] { "LeftArm", "RightArm", "LeftLeg", "RightLeg" };
	private string[] hornNames = new string[] { "LeftHorn", "RightHorn", "MiddleHorn" };

	void Awake ()
	{
		Instance = this;
		DontDestroyOnLoad (this);
	}

	public int[] vals = new int[4];
	public bool next, prev;
	DemonBase last;

	void Update ()
	{
		//if(Input.GetKeyDown(KeyCode.Space)) next = true;
		//if(Input.GetKeyDown(KeyCode.Backspace)) prev = true;
		if (next) {
			if (!Input.GetKey ("x"))
				next = false;
			if (last)
				DestroyImmediate (last.gameObject);
			do {
				vals [0]++;
				for (int i = 0; i < 4; i++) {
					if (vals [i] >= 5) {
						vals [i] = 0;
						if (i < 3)
							vals [i + 1]++;
					}
				}
			} while((vals [2] != 4) == (vals [0] != 4));
			last = makeDemon (vals [2], ((Ingredients.stats)vals [3]).ToString (), vals [0], ((Ingredients.stats)vals [1]).ToString (), vals [0], ((Ingredients.stats)vals [1]).ToString ());
		} else if (prev) {
			prev = false;
			if (last)
				DestroyImmediate (last.gameObject);
			vals [0]--;
			for (int i = 0; i < 4; i++) {
				if (vals [i] < 0) {
					vals [i] = 4;
					if (i < 3)
						vals [i + 1]--;
				}
			}
			last = makeDemon (vals [2], ((Ingredients.stats)vals [3]).ToString (), vals [0], ((Ingredients.stats)vals [1]).ToString (), vals [0], ((Ingredients.stats)vals [1]).ToString ());
		}
	}

	public DemonBase makeDemon (Ingredients body, Ingredients limbs, Ingredients horns)
	{
		DemonBase newDemon = makeDemon (body.GetTier (), body.GetStat ().ToString (), limbs.GetTier (), limbs.GetStat ().ToString (), horns.GetTier (), horns.GetStat ().ToString ());
		float baseStat = (body.GetTier () + limbs.GetTier () + horns.GetTier () + 3) / 6f;
		float[] newStats = new float[5] { baseStat, baseStat, baseStat, baseStat, baseStat };
		newStats [(int)body.GetStat ()] += body.GetTier () + 1;
		newStats [(int)limbs.GetStat ()] += limbs.GetTier () + 1;
		newStats [(int)horns.GetStat ()] += horns.GetTier () + 1;
		newDemon.setStats (newStats, Mathf.FloorToInt ((body.GetTier () + limbs.GetTier () + horns.GetTier () + 3) / 3f));
		if (ParticlePrefab)
		newDemon.ParticleInstance = Instantiate (ParticlePrefab);
		switch((int)body.GetStat()) {
		case(0):
			SoundManager.instance.playSound ("power");
			break;
		case(1):
			SoundManager.instance.playSound ("clever");
			break;
		case(2):
			SoundManager.instance.playSound ("deception");
			break;
		case(3):
			SoundManager.instance.playSound ("seduction");
			break;
		case(4):
			SoundManager.instance.playSound ("occult");
			break;
		}
		return newDemon;
	}

	public DemonBase makeDemon (int bodyTier, string bodyStat, int limbTier, string limbStat, int hornTier, string hornStat)
	{
		GameObject newDemon = new GameObject ();
		newDemon.name = bodyStat + "_" + limbStat + "_" + hornStat;
		newDemon.transform.position = Vector3.zero;
		SpriteRenderer bodySprite = newDemon.AddComponent<SpriteRenderer> ();
		bodySprite.sortingLayerName = "Demons";
		bodySprite.sortingOrder = 2;
		List<Sprite> attrSprites = new List<Sprite> (Resources.LoadAll<Sprite> ("Parts/" + bodyStat + "_" + (bodyTier + 1)));
		bodySprite.sprite = attrSprites.Find (x => x.name == bodyStat + "_Body_" + (bodyTier + 1));

		// Grab the Arms/Legs
		Transform demonLimbs = DemonTierPrefabs [bodyTier].transform.FindChild (bodyStat + "_" + limbStat);
		GameObject newAppendage;
		for (int i = 0; i < limbNames.Length; i++) {
			newAppendage = Instantiate (demonLimbs.FindChild (limbNames [i]).gameObject);
			newAppendage.transform.SetParent (newDemon.transform, false);
			newAppendage.name = limbNames [i];
			List<Sprite> appendSprites = new List<Sprite> (Resources.LoadAll<Sprite> ("Parts/" + limbStat + "_" + (limbTier + 1)));
			newAppendage.GetComponent<SpriteRenderer> ().sprite = appendSprites.Find (x => x.name == limbStat + "_" + limbNames [i] + "_" + (limbTier + 1));
			newAppendage.GetComponent<SpriteRenderer> ().sortingLayerName = "Demons";
		}

		// Grab the Horns
		Transform demonHorns = DemonTierPrefabs [bodyTier].transform.FindChild (bodyStat + "_" + hornStat);
		for (int i = 0; i < hornNames.Length; i++) {
			newAppendage = Instantiate (demonHorns.FindChild (hornNames [i]).gameObject);
			newAppendage.transform.SetParent (newDemon.transform, false);
			newAppendage.name = hornNames [i];
			List<Sprite> appendSprites = new List<Sprite> (Resources.LoadAll<Sprite> ("Parts/" + hornStat + "_" + (hornTier + 1)));
			newAppendage.GetComponent<SpriteRenderer> ().sprite = appendSprites.Find (x => x.name == hornStat + "_" + hornNames [i] + "_" + (hornTier + 1));
			newAppendage.GetComponent<SpriteRenderer> ().sortingLayerName = "Demons";
		}

		return newDemon.AddComponent<DemonBase> ();
	}
}
