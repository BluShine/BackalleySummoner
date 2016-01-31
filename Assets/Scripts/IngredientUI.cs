using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IngredientUI : MonoBehaviour {

    List<Transform> buttonLocations;
    List<List<GameObject>> buttons;

	// Use this for initialization
	void Start () {
        buttonLocations = new List<Transform>();
        foreach(Transform t in transform)
        {
            buttonLocations.Add(t);
        }
        buttons = new List<List<GameObject>>();
        for (int j = 0; j < Recipes.instance.tier_GmO.Count; j++)
        {
            GameObject[] g = Recipes.instance.tier_GmO[j].ToArray();
            buttons.Add(new List<GameObject>());
            for(int i = 0; i < buttonLocations.Count; i++)
            {
                GameObject newButton = Instantiate(g[i]);
                newButton.transform.SetParent(buttonLocations[i]);
                newButton.transform.localPosition = Vector3.zero;
                newButton.transform.localScale = Vector3.one;
                buttons[j].Add(newButton);
            }
        }
        OpenTab(0);
	}
	
	public void OpenTab(int tab)
    {
        foreach(List<GameObject> l in buttons)
        {
            foreach(GameObject g in l)
            {
                g.SetActive(false);
            }
        }
        foreach(GameObject b in buttons[tab])
        {
            b.SetActive(true);
        }
    }
}
