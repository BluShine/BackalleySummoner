using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

public class DemonMapper : MonoBehaviour {
#if UNITY_EDITOR
    public GameObject[,] demonMap = new GameObject[5,5];

   public void SavePrefab(int x, int y, GameObject demon)
   {
      Transform t = transform.FindChild(demon.name);
      if(t)
      {
         DestroyImmediate(t.gameObject);
      }
      demon.transform.SetParent(transform, false);
      demonMap[x, y] = demon;

      PrefabUtility.ReplacePrefab(gameObject, PrefabUtility.GetPrefabParent(gameObject), ReplacePrefabOptions.ConnectToPrefab);
   }
#endif
}
