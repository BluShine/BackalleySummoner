using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Recipes : MonoBehaviour {
    public Dictionary<string, Ingredients> name_ingredient = new Dictionary<string, Ingredients>();
    public Dictionary<Ingredients.bodyParts, HashSet<Ingredients>> part_ingredient;
    public Dictionary<Ingredients.stats, HashSet<Ingredients>> stat_ingredient;
    public List<HashSet<GameObject>> tier_GmO;
    public List<HashSet<Ingredients>> tier_ingredient;
    // Use this for initialization
    public static Recipes instance;
    public GameObject base_ingredients;
	public void Awake () {
		Debug.Log ("recipe start");
        tier_ingredient = new List<HashSet<Ingredients>>();
        System.Random rnd = new System.Random((int)System.DateTime.Now.Ticks);
        tier_GmO = new List<HashSet<GameObject>>(new HashSet<GameObject>[] {});
        foreach (Transform tiers in base_ingredients.transform)
        {
            HashSet<GameObject> h = new HashSet<GameObject>();
            tier_GmO.Add(h);
            foreach (Transform ingredient in tiers)
            {
                h.Add(ingredient.gameObject);
            }
        }
        stat_ingredient = new Dictionary<Ingredients.stats, HashSet<Ingredients>>();
        part_ingredient = new Dictionary<Ingredients.bodyParts, HashSet<Ingredients>>();
        var values = System.Enum.GetValues(typeof(Ingredients.bodyParts));
        foreach(Ingredients.bodyParts value in values)
        {
            part_ingredient.Add(value, new HashSet<Ingredients>());
        }
        values = System.Enum.GetValues(typeof(Ingredients.stats));
        foreach (Ingredients.stats value in values)
        {
            stat_ingredient.Add(value, new HashSet<Ingredients>());
        }
        for (int x = 0; x < tier_GmO.Count; x++)
        {
            List<GameObject> ingredient_l = GetIngredientsTier(x);
            GameObject[] stats = new GameObject[ingredient_l.Count];
            int[] parts_n = new int[5];
            int[] parts_v = { 2, 3, 5 };
            for (int y = 0; y < 5; y++)
            {
                parts_n[y] = 1;
                for (int z = 0; z < 2; z++)
                {
                    int chk = rnd.Next(0, ingredient_l.Count);
                    stats[y*2+z] = ingredient_l[chk];
                    ingredient_l.RemoveAt(chk);
                }
                
            }
            for (int y = 0; y < 3; y++)
            {
                for (int a = 0; a < 3 || (y >= 2 && a < 4); a++)
                {
                    int min = parts_n.Min();
                    int count = 0;
                    if (min > 1)
                    {
                        count = parts_n.Where(elem => elem < 6 && elem % parts_v[y] > 0).Count();
                        min = 5;
                    }
                    else
                    {
                        count = parts_n.Where(elem => elem < 2).Count();
                        min = 1;
                    }
                    int chk = rnd.Next(0, count);
                    int ct = 0;
                    for (int z = 0; z < parts_n.Count(); z++)
                    {
                        if (ct == chk && parts_n[z] <= min && parts_n[z] % parts_v[y] > 0)
                        {
                            parts_n[z] *= parts_v[y];
                            break;
                        }
                        if (parts_n[z] <= min && parts_n[z] % parts_v[y] > 0)
                        {
                            ct++;
                        }
                    }
                }
            }
            HashSet<Ingredients> tier_set = new HashSet<Ingredients>();
            for (int y = 0; y < stats.Count(); y++)
            {
                foreach(int prime in parts_v)
                {
                    if(parts_n[y/2] % prime == 0)
                    {
                        Ingredients temp = new Ingredients(stats[y].name, y / 2, prime, x, stats[y]);
                        tier_set.Add(temp);
                        name_ingredient.Add(stats[y].name, temp);
                        stat_ingredient[((Ingredients.stats)(y / 2))].Add(temp);
                        part_ingredient[((Ingredients.bodyParts)prime)].Add(temp);
                        parts_n[y / 2] /= prime;
                        break;
                    }
                }
            }
            tier_ingredient.Add(tier_set);
        }
        instance = this;
        Debug.Log(name_ingredient.Count);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // Asssign recipes at start of game
    void Assign () {

    }
    List<GameObject> GetIngredientsTier(int x)
    {
        return this.tier_GmO[x].ToList();
    }
}
