using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
class DemonAppendagePositioning : MonoBehaviour
{
   private string[] attributes = new string[] { "Power", "Cleverness", "Seduction", "Deception", "Occult" };
   private string[] partNames = new string[] { "LeftArm", "RightArm", "LeftLeg", "RightLeg", "LeftHorn", "RightHorn", "MiddleHorn" };
   public int tierNum = 1;
   GameObject[] partObjects;
   SpriteRenderer[] partSprites;

   SpriteRenderer spriteRenderer;

   public bool done = false;
   public bool saveMap;

   public int attIndex = 0;
   public int appIndex = 0;

   public DemonMapper bigassprefab;

   void Start()
   {
      partObjects = new GameObject[partNames.Length];
      partSprites = new SpriteRenderer[partNames.Length];
      for(int i = 0; i < partNames.Length; i++)
      {
         partObjects[i] = transform.FindChild(partNames[i]).gameObject;
         partSprites[i] = partObjects[i].GetComponent<SpriteRenderer>();
      }
      spriteRenderer = GetComponent<SpriteRenderer>();
      //StartCoroutine(partPositioning());
   }

   void Update()
   {
      if(Input.GetKeyDown(KeyCode.Space))
      {
         done = true;
      }
      if(done)
      {
         if(saveMap)
         {
            GameObject nakedDemon = GameObject.Instantiate(gameObject);
            nakedDemon.name = name;
            DestroyImmediate(nakedDemon.GetComponent<DemonAppendagePositioning>());
#if UNITY_EDITOR
                bigassprefab.SavePrefab(appIndex, attIndex, nakedDemon);
#endif
            }
         

         done = false;
         appIndex++;
         if(appIndex >= attributes.Length)
         {
            appIndex = 0;
            attIndex++;
            if(attIndex >= attributes.Length)
               attIndex = 0;
         }
         loadParts();
      }
   }

   void Reset()
   {
      appIndex = 0;
      attIndex = 0;
      //bigassprefab = GameObject.Find("bigprefab").GetComponent<DemonMapper>();
      loadParts();
   }

   [ContextMenu("Load Sprites")]
   void loadParts()
   {
      string bodyAttr = attributes[attIndex];
      List<Sprite> bodySprites = new List<Sprite>(Resources.LoadAll<Sprite>("Parts/" + bodyAttr + "_" + tierNum));
      spriteRenderer.sprite = bodySprites.Find(x => x.name == bodyAttr + "_Body_" + tierNum);
      string appendAttr = attributes[appIndex];
      name = bodyAttr + "_" + appendAttr;
      if(loadFromPrefab()) return;
      for(int i = 0; i < partNames.Length; i++)
      {
         List<Sprite> appendSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Parts/" + appendAttr + "_" + tierNum));
         partSprites[i].sprite = appendSprites.Find(x => x.name == appendAttr + "_" + partNames[i] + "_" + tierNum);
      }
   }

   [ContextMenu("Load From Prefab")]
   bool loadFromPrefab()
   {
      if(!bigassprefab) return false;
      Transform pre = bigassprefab.transform.FindChild(attributes[attIndex] + "_" + attributes[appIndex]);
      if(!pre) return false;
      string appendAttr = attributes[appIndex];
      for(int i = 0; i < partNames.Length; i++)
      {
         DestroyImmediate(partObjects[i]);
         partObjects[i] = Instantiate(pre.FindChild(partNames[i]).gameObject);
         partObjects[i].transform.SetParent(transform, false);
         partObjects[i].name = partNames[i];
         partSprites[i] = partObjects[i].GetComponent<SpriteRenderer>();
         //List<Sprite> appendSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Parts/" + appendAttr + "_" + tierNum));
         //partSprites[i].sprite = appendSprites.Find(x => x.name == appendAttr + "_" + partNames[i] + "_" + tierNum);
      }
      return true;
   }

   [ContextMenu("Reload Body Sprite")]
   void reloadBody()
   {
      string bodyAttr = attributes[attIndex];
      List<Sprite> bodySprites = new List<Sprite>(Resources.LoadAll<Sprite>("Parts/" + bodyAttr + "_" + tierNum));
      spriteRenderer.sprite = bodySprites.Find(x => x.name == bodyAttr + "_Body_" + tierNum);
   }
}
