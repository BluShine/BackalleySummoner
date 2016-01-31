using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PentaItemPlacer : MonoBehaviour {

    public IngredientUI ui;
    List<Transform> places;

    public Ingredients[] currentIngs;

	// Use this for initialization
	void Start () {
        currentIngs = new Ingredients[3];
	    foreach(List<GameObject> lg in ui.buttons)
        {
            foreach(GameObject g in lg)
            {
                g.GetComponent<Button>().onClick.AddListener(() => PlaceItem(g)); 
            }
        }

        foreach(Transform t in transform)
        {
            places.Add(t);
        }
	}
	
	public void PlaceItem(GameObject item)
    {
        Ingredients ingrererererererererrentttt = Recipes.instance.name_ingredient[item.name];
        switch (ingrererererererererrentttt.GetPart())
        {
            case Ingredients.bodyParts.Horns:
                currentIngs[0] = ingrererererererererrentttt;
                foreach(Transform t in places[0])
                {
                    Destroy(t);
                }
                GameObject newIngrentetetet = Instantiate(ingrererererererererrentttt.GetGmO());
                newIngrentetetet.transform.parent = places[0];
                newIngrentetetet.transform.localPosition = Vector3.zero;
                newIngrentetetet.transform.localScale = Vector3.one;
                break;
            case Ingredients.bodyParts.Body:
                currentIngs[1] = ingrererererererererrentttt;
                foreach (Transform t in places[1])
                {
                    Destroy(t);
                }
                GameObject newIngrerentetetet2 = Instantiate(ingrererererererererrentttt.GetGmO());
                newIngrerentetetet2.transform.parent = places[1];
                newIngrerentetetet2.transform.localPosition = Vector3.zero;
                newIngrerentetetet2.transform.localScale = Vector3.one;
                break;
            case Ingredients.bodyParts.Limbs:
                currentIngs[2] = ingrererererererererrentttt;
                foreach (Transform t in places[2])
                {
                    Destroy(t);
                }
                GameObject newIngrenenenetetetet3 = Instantiate(ingrererererererererrentttt.GetGmO());
                newIngrenenenetetetet3.transform.parent = places[2];
                newIngrenenenetetetet3.transform.localPosition = Vector3.zero;
                newIngrenenenetetetet3.transform.localScale = Vector3.one;
                break;
        }
    }

    public void SubmitDemon()
    {
        foreach (Ingredients ingred in currentIngs)
        {
            if (ingred == null)
                return;
        }

        DemonFactory.Instance.makeDemon(currentIngs[1], currentIngs[2], currentIngs[0]);
    }
}
