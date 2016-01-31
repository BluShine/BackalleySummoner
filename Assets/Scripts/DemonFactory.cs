using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonFactory : MonoBehaviour
{
   public static DemonFactory Instance;
   public GameObject[] DemonTierPrefabs;

   void Awake()
   {
      Instance = this;
      DontDestroyOnLoad(this);
   }

   public DemonBase makeDemon(Ingredients body, Ingredients limbs, Ingredients horns)
   {
      return null;
   }
}
