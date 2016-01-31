using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class DemonDisplay : MonoBehaviour
{
   private string[] attributes = new string[] { "Power", "Cleverness", "Seduction", "Deception", "Occult" };

   public float growTime = 2f;

   void Start()
   {

   }

   int fib = 0;
   int lastfib = 1;
   void Update()
   {
      if(Input.GetKeyDown(KeyCode.C))
      {
         int temp = fib;
         fib = lastfib + fib;
         lastfib = temp;
         for(int i = 0; i < fib; i++)
         {
            DemonBase d = DemonFactory.Instance.makeDemon(Random.Range(0, 4), attributes[Random.Range(0, 5)], Random.Range(0, 4), attributes[Random.Range(0, 5)], Random.Range(0, 4), attributes[Random.Range(0, 5)]);
            d.transform.position = new Vector3(Random.Range(-6.8f, 6.8f), Random.Range(-5f, 5f), 0);
         }
         //StartCoroutine(DemonDisplay(d));
      }
   }
}
