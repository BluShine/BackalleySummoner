using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PentaItemPlacer : MonoBehaviour {


    public IngredientUI ingredUI;
    List<Transform> places;

    public Ingredients[] currentIngs;

	// Use this for initialization
	void Start () {
        currentIngs = new Ingredients[3];
	    foreach(List<GameObject> lg in ingredUI.buttons)
        {
            foreach(GameObject g in lg)
            {
                GameObject p = g;
                g.GetComponent<Button>().onClick.AddListener(() => PlaceItem(p)); 
            }
        }
        places = new List<Transform>();
        foreach(Transform t in transform)
        {
            places.Add(t);
        }
	}
	
	public void PlaceItem(GameObject item)
    {
        if (GameManager.instance.HeldIngredients[item.name] < 1)
            return;
        Ingredients ingrererererererererrentttt = Recipes.instance.name_ingredient[item.name];
        int index = 0;
        switch (ingrererererererererrentttt.GetPart())
        {
            case Ingredients.bodyParts.Horns:
                index = 0;
                break;
            case Ingredients.bodyParts.Body:
                index = 1;
                break;
            case Ingredients.bodyParts.Limbs:
                index = 2;
                break;
        }
        currentIngs[index] = ingrererererererererrentttt;
        foreach (Transform t in places[index])
        {
            Destroy(t.gameObject);
        }
        GameObject newIngrentetetet = Instantiate(ingrererererererererrentttt.GetGmO());
        newIngrentetetet.transform.parent = places[index];
        newIngrentetetet.transform.localPosition = Vector3.zero;
        newIngrentetetet.transform.localScale = Vector3.one;
    }

    public void SubmitDemon()
    {
        foreach (Ingredients ingred in currentIngs)
        {
            if (ingred == null)
                return;
            else
                GameManager.instance.HeldIngredients[ingred.GetName()]--;
        }
        IngredientUI.UpdateAllNumbers();

		GameManager.instance.assignDemon(DemonFactory.Instance.makeDemon(currentIngs[1], currentIngs[2], currentIngs[0]));
        //DemonFactory.Instance.makeDemon(currentIngs[1], currentIngs[2], currentIngs[0]);
    }
}
