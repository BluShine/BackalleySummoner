using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour {
	//method in game manager called GiveIngredients
	//that method calls a method in here that is going to pop up a ui element
	//that ui element is then going to show the ingredients that it got in a list
	//theres going to be a button that you press that dismisses the list

	// Use this for initialization



	public static void Popup (List<Ingredients> il) {
		int x = 0;
		GameObject[] ing = new GameObject[8];
		for(x = 0; x < il.Count; x++)
		{
			ing [x] = il [x].GetGmO ();
		}
		for (x = x; x < 8; x++) {
			ing [x] = null;
		}
        GameObject iGet = GameObject.Find("item-get");
        iGet.SetActive(true);
        int i = 0;
        foreach(Transform t in iGet.transform)
        {
            if(i < il.Count)
            {
                GameObject obj = Instantiate(il[i].GetGmO());
                obj.transform.parent = t;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
            }
        }
    }
	
	// Update is called once per frame
	public static void Dismiss () {
        GameObject iGet = GameObject.Find("item-get");
        foreach(Image img in iGet.GetComponentsInChildren<Image>())
        {
            Destroy(img.gameObject);
        }
        iGet.SetActive(false);
    }
}
