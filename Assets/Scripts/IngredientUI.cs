using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class IngredientUI : MonoBehaviour {

    public GameObject buttonTextPrefab;

    List<Transform> buttonLocations;
    List<List<GameObject>> buttons;

    // Use this for initialization
    void Start()
    {
        buttonLocations = new List<Transform>();
        foreach (Transform t in transform)
        {
            buttonLocations.Add(t);
        }
        buttons = new List<List<GameObject>>();
        for (int j = 0; j < Recipes.instance.tier_GmO.Count; j++)
        {
            GameObject[] g = Recipes.instance.tier_GmO[j].ToArray();
            buttons.Add(new List<GameObject>());
            for (int i = 0; i < buttonLocations.Count; i++)
            {
                //place button
                GameObject newButton = Instantiate(g[i]);
                newButton.name = g[i].name;
                newButton.transform.SetParent(buttonLocations[i]);
                newButton.transform.localPosition = Vector3.zero;
                newButton.transform.localScale = Vector3.one;
                //add text thing to the button
                GameObject textThing = Instantiate(buttonTextPrefab);
                textThing.transform.SetParent(newButton.transform);
                textThing.transform.localPosition = Vector3.zero;
                textThing.transform.localScale = Vector3.one;
                RectTransform rect = textThing.GetComponent<RectTransform>();
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.one;

                buttons[j].Add(newButton);
            }
        }
        OpenTab(0);
    }

    public void UpdateNumbers()
    {
        foreach(List<GameObject> l in buttons)
        {
            foreach(GameObject g in l)
            {
                int num = GameManager.instance.HeldIngredients[g.name];
                foreach(Text t in g.GetComponentsInChildren<Text>())
                {
                    t.text = num.ToString();
                }
                if(num == 0)
                {
                    g.GetComponent<Image>().enabled = true;
                } else
                {
                    g.GetComponent<Image>().enabled = false;
                }
            }
        }
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
