using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonBase : MonoBehaviour {

	//demon stats
	//0 - 4 corresponds to strength, cleverness, seduction, deception, occult
	public List<float> statList = new List<float>();

   // demon tier
   public int tier;

   // particles for spawn animation
   public GameObject ParticleInstance;

	//all items held by the demon
	public List<ItemBase> itemInventory;

   // Various Demon Parts
   private string[] limbNames = new string[] { "LeftArm", "RightArm" , "LeftLeg", "RightLeg" };
   GameObject[] limbObjects;
   //SpriteRenderer[] limbSprites;
   float[] startRotations;


   public void setStats(float[] stats, int tier)
   {
      statList = new List<float>(stats);
      this.tier = tier;
   }

	// Use this for initialization
	void Start () {
      int l = limbNames.Length;
      limbObjects = new GameObject[l];
      //limbSprites = new SpriteRenderer[l];
      startRotations = new float[l];
	   for(int i = 0; i < l; i++)
      {
         limbObjects[i] = transform.FindChild(limbNames[i]).gameObject;
         //limbSprites[i] = limbObjects[i].GetComponent<SpriteRenderer>();
         startRotations[i] = limbObjects[i].transform.eulerAngles.z;
      }
      StartCoroutine(DemonDisplay());
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void applyItemBoosts () {
        var values = System.Enum.GetValues(typeof(Ingredients.stats));
        foreach (ItemBase i in itemInventory) {
			foreach(Ingredients.stats value in values) {
				statList [(int)value] += i.statList [(int)value];
			}
		}
	}
    public int GetTier()
    {
        return tier;
    }

    IEnumerator DemonDisplay()
    {
       if(ParticleInstance)
       {
          ParticleInstance.transform.position = this.transform.position;
       }
       transform.localScale = Vector3.zero;
       for(float i = 0; i < 2; i += Time.deltaTime)
       {
          float ratio = i / 2;
          transform.localScale = new Vector3(ratio, ratio, ratio);
          yield return null;
       }
       float timePassed = 0;
       while(!Input.GetKeyDown(KeyCode.Space))
       {
          timePassed += 2 * Time.deltaTime;
          timePassed %= 4 * Mathf.PI;
          //demon.transform.eulerAngles = new Vector3(0, 0, 20 * Mathf.Sin(timePassed));
          int factor = 15;
          for(int i = 0; i < limbObjects.Length; i++)
          {
             limbObjects[i].transform.eulerAngles = new Vector3(0, 0, startRotations[i] + factor * Mathf.Sin(timePassed * ((i < 2) ? 1 : 0.5f)));
             factor *= -1;
          }
          yield return null;
       }
    }
}
