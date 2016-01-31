using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class PentaItemPlacer : MonoBehaviour {


    public IngredientUI ingredUI;
    List<Transform> places;

    public Ingredients[] currentIngs;
    public Button backButton, submitButton, dismissButton;

    DemonBase summonedDemon;

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
        int index = partToIndex(ingrererererererererrentttt.GetPart());
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
           if(ingred == null)
              return;
           else
           {
              Destroy(places[partToIndex(ingred.GetPart())].GetChild(0).gameObject);
              GameManager.instance.HeldIngredients[ingred.GetName()]--;
           }
        }
        IngredientUI.UpdateAllNumbers();

        summonedDemon = DemonFactory.Instance.makeDemon(currentIngs[1], currentIngs[2], currentIngs[0]);
        summonedDemon.transform.position = new Vector3(-2.5f, 1);
        //DemonFactory.Instance.makeDemon(currentIngs[1], currentIngs[2], currentIngs[0]);
        backButton.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        dismissButton.gameObject.SetActive(true);

    }

   public void DismissDemon()
    {
       dismissButton.interactable = false;
       StartCoroutine(demonFliesAway());
    }

   IEnumerator demonFliesAway()
   {
      Vector3 flightPath = new Vector3(1, Random.Range(-1f, 1f)).normalized;
      if(Random.value > 0.5) flightPath *= -1;
      Vector3 perpPath = (flightPath.x > 0) ? new Vector3(-1 * flightPath.y, flightPath.x) : new Vector3(flightPath.y, -1 * flightPath.x);
      float speed = 5;
      Vector3 flightPos = summonedDemon.transform.position;
      float dist = Vector2.Distance(summonedDemon.transform.position, new Vector2(-2.5f, 1));
      while(dist  < 15)
      {
         flightPos += flightPath * Time.deltaTime * speed;
         summonedDemon.transform.position = flightPos + perpPath * Mathf.Abs(Mathf.Sin(dist));
         yield return null;
         dist = Vector2.Distance(summonedDemon.transform.position, new Vector2(-2.5f, 1));
      }
      backButton.gameObject.SetActive(true);
      submitButton.gameObject.SetActive(true);
      dismissButton.interactable = true;
      dismissButton.gameObject.SetActive(false);
      GameManager.instance.assignDemon(summonedDemon);
   }

   int partToIndex(Ingredients.bodyParts bp)
    {
       switch(bp)
       {
          case Ingredients.bodyParts.Horns:
             return 0;
          case Ingredients.bodyParts.Body:
             return 1;
          case Ingredients.bodyParts.Limbs:
             return 2;
       }
       return -1;
    }
}
