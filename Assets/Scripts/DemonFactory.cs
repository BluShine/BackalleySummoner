using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonFactory : MonoBehaviour
{
   public static DemonFactory Instance;
   public GameObject[] DemonTierPrefabs;


   private string[] limbNames = new string[] { "LeftArm", "RightArm", "LeftLeg", "RightLeg" };
   private string[] hornNames = new string[] { "LeftHorn", "RightHorn", "MiddleHorn" };

   void Awake()
   {
      Instance = this;
      DontDestroyOnLoad(this);
   }

   public int[] vals = new int[4];
   public bool next, prev;
   DemonBase last;
   void Update()
   {
      if(Input.GetKeyDown(KeyCode.Space)) next = true;
      if(Input.GetKeyDown(KeyCode.Backspace)) prev = true;
      if(next)
      {
         if(!Input.GetKey("x")) next = false;
         if(last)
            DestroyImmediate(last.gameObject);
         do
         {
            vals[0]++;
            for(int i = 0; i < 4; i++)
            {
               if(vals[i] >= ((i % 2 == 0) ? 4 : 5))
               {
                  vals[i] = 0;
                  if(i < 3)
                     vals[i + 1]++;
               }
            }
         } while((vals[2] != 3) == (vals[0] != 3));
         last = makeDemon(vals[2], ((Ingredients.stats)vals[3]).ToString(), vals[0], ((Ingredients.stats)vals[1]).ToString(), vals[0], ((Ingredients.stats)vals[1]).ToString());
      }
      else if(prev)
      {
         prev = false;
         if(last)
            DestroyImmediate(last.gameObject);
         vals[0]--;
         for(int i = 0; i < 4; i++)
         {
            if(vals[i] < 0)
            {
               vals[i] = ((i % 2 == 0) ? 3 : 4);
               if(i < 3)
                  vals[i + 1]--;
            }
         }
         last = makeDemon(vals[2], ((Ingredients.stats)vals[3]).ToString(), vals[0], ((Ingredients.stats)vals[1]).ToString(), vals[0], ((Ingredients.stats)vals[1]).ToString());
      }
   }

   public DemonBase makeDemon(Ingredients body, Ingredients limbs, Ingredients horns)
   {
      DemonBase newDemon = makeDemon(body.GetTier(), body.GetStat().ToString(), limbs.GetTier(), limbs.GetStat().ToString(), horns.GetTier(), horns.GetStat().ToString());
      // Give the demon all the ingredients?  or stats?? or something???
      return newDemon;
   }

   public DemonBase makeDemon(int bodyTier, string bodyStat, int limbTier, string limbStat, int hornTier, string hornStat)
   {
      GameObject newDemon = new GameObject();
      newDemon.transform.position = Vector3.zero;
      SpriteRenderer bodySprite = newDemon.AddComponent<SpriteRenderer>();
      List<Sprite> attrSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Parts/" + bodyStat + "_" + (bodyTier + 1)));
      bodySprite.sprite = attrSprites.Find(x => x.name == bodyStat + "_Body_" + (bodyTier + 1));

      // Grab the Arms/Legs
      Transform demonLimbs = DemonTierPrefabs[bodyTier].transform.FindChild(bodyStat + "_" + limbStat);
      GameObject newAppendage;
      for(int i = 0; i < limbNames.Length; i++)
      {
         newAppendage = Instantiate(demonLimbs.FindChild(limbNames[i]).gameObject);
         newAppendage.transform.SetParent(newDemon.transform, false);
         newAppendage.name = limbNames[i];
         List<Sprite> appendSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Parts/" + limbStat + "_" + (limbTier + 1)));
         newAppendage.GetComponent<SpriteRenderer>().sprite = appendSprites.Find(x => x.name == limbStat + "_" + limbNames[i] + "_" + (limbTier + 1));
      }

      // Grab the Horns
      Transform demonHorns = DemonTierPrefabs[bodyTier].transform.FindChild(bodyStat + "_" + hornStat);
      for(int i = 0; i < hornNames.Length; i++)
      {
         newAppendage = Instantiate(demonHorns.FindChild(hornNames[i]).gameObject);
         newAppendage.transform.SetParent(newDemon.transform, false);
         newAppendage.name = hornNames[i];
         List<Sprite> appendSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Parts/" + hornStat + "_" + (hornTier + 1)));
         newAppendage.GetComponent<SpriteRenderer>().sprite = appendSprites.Find(x => x.name == hornStat + "_" + hornNames[i] + "_" + (hornTier + 1));
      }

      return newDemon.AddComponent<DemonBase>();
   }
}
