using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Recipes : MonoBehaviour {
    Random math = new Random();
    Dictionary<string, Ingredients> ingredients = new Dictionary<string, Ingredients>(); 
	// Use this for initialization
	void Start () {
        System.Random rnd = new System.Random((int)System.DateTime.Now.Ticks);
        for (int x = 0; x < 5; x++)
        {
            List<string> ingredient_l = GetIngredientsTier(x);
            string[] stats = new string[ingredient_l.Count];
            int[] parts_n = new int[5];
            int[] parts_v = { 2, 3, 5 };
            for (int y = 0; y < 10; y++)
            {
                parts_n[y/2] = 1;
                int chk = rnd.Next(0, ingredient_l.Count);
                stats[y] = ingredient_l[chk];
                ingredient_l.RemoveAt(chk);
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
                        string pr = "";
                        foreach (int elem in parts_n)
                        {
                            pr += elem + ", ";
                        }
                    }
                }
            }
            for (int y = 0; y < stats.Count(); y++)
            {
                foreach(int prime in parts_v)
                {
                    if(parts_n[y/2] % prime == 0)
                    {
                        ingredients.Add(stats[y], new Ingredients(stats[y], y / 2, prime));
                        parts_n[y / 2] /= prime;
                        break;
                    }
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // Asssign recipes at start of game
    void Assign () {

    }
}
