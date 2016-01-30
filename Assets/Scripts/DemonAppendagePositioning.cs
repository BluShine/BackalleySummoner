using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class DemonAppendagePositioning : MonoBehaviour
{
   private string[] attributes = new string[] { "Power", "Cleverness", "Seduction", "Deception", "Occult" };
   private string[] partNames = new string[] { "LeftArm", "RightArm", "LeftLeg", "RightLeg", "LeftHorn", "RightHorn", "MiddleHorn" };
   private const int tierNum = 1;
   GameObject[] partObjects;
   SpriteRenderer[] partSprites;

   SpriteRenderer spriteRenderer;

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
      StartCoroutine(partPositioning());
   }

   void Update()
   {
      
   }

   IEnumerator partPositioning()
   {
      foreach(string bodyAttr in attributes)
      {
         // Load Body
         List<Sprite> bodySprites = new List<Sprite>(Resources.LoadAll<Sprite>("Parts/" + bodyAttr + "_" + tierNum));
         spriteRenderer.sprite = bodySprites.Find( x => x.name == bodyAttr + "_Body_" + tierNum);
         Debug.Log(bodyAttr + "_Body_");
         gameObject.name = bodyAttr;
         foreach(string appendAttr in attributes)
         {
            Debug.Log(appendAttr + "_Appendages_");
            for(int i = 0; i < partNames.Length; i++)
            {
               List<Sprite> appendSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Parts/" + appendAttr + "_" + tierNum));
               partSprites[i].sprite = appendSprites.Find(x => x.name == appendAttr + "_" + partNames[i] + "_" + tierNum);
               Debug.Log(appendAttr + "_" + partNames[i] + "_" + tierNum);
            }
            while(!Input.GetKeyDown(KeyCode.Space))
               yield return null;
            // Save the positions of stuff
            yield return null;
         }
      }
   }
}
