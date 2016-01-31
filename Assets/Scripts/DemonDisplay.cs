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

   void Update()
   {
      if(Input.GetKeyDown(KeyCode.C))
      {
         DemonBase d = DemonFactory.Instance.makeDemon(Random.Range(0, 4), attributes[Random.Range(0, 5)], Random.Range(0, 4), attributes[Random.Range(0, 5)], Random.Range(0, 4), attributes[Random.Range(0, 5)]);
         d.transform.position = new Vector3(Random.Range(-6.8f, 6.8f), Random.Range(-5f, 5f), 0);
         //StartCoroutine(DemonDisplay(d));
      }
   }

   public void DisplayDemon(DemonBase demon)
   {

   }


   IEnumerator Display(DemonBase demon)
   {
      demon.transform.localScale = Vector3.zero;
      for(float i = 0; i < growTime; i += Time.deltaTime)
      {
         float ratio = i / growTime;
         demon.transform.localScale = new Vector3(ratio, ratio, ratio);
         yield return null;
      }
      float timePassed = 0;
      while(!Input.GetKeyDown(KeyCode.Space))
      {
         timePassed += 0.5f * Time.deltaTime;
         timePassed %= 2 * Mathf.PI;
         //demon.transform.eulerAngles = new Vector3(0, 0, 20 * Mathf.Sin(timePassed));
         yield return null;
      }
   }
}
