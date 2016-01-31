﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Recipes : MonoBehaviour {
    public Dictionary<string, Ingredients> ingredient_stat = new Dictionary<string, Ingredients>();
    public Dictionary<Ingredients.bodyParts, HashSet<Ingredients>> part_ingredient;
    public Dictionary<Ingredients.stats, HashSet<Ingredients>> stat_ingredient;
    public List<HashSet<string>> tier_ingredient;
    // Use this for initialization
    public static Recipes instance;
	void Start () {
        System.Random rnd = new System.Random((int)System.DateTime.Now.Ticks);
        tier_ingredient = new List<HashSet<string>>(5);
        stat_ingredient = new Dictionary<Ingredients.stats, HashSet<Ingredients>>();
        part_ingredient = new Dictionary<Ingredients.bodyParts, HashSet<Ingredients>>();
        for (int x = 0; x < tier_ingredient.Count; x++)
        {
			List<string> ingredient_l = null;
            //List<string> ingredient_l = GetIngredientsTier(x);
            string[] stats = new string[ingredient_l.Count];
            int[] parts_n = new int[5];
            int[] parts_v = { 2, 3, 5 };
            for (int y = 0; y < 5; y++)
            {
                parts_n[y] = 1;
                stat_ingredient.Add((Ingredients.stats)y, new HashSet<Ingredients>());
                for (int z = 0; z < 2; z++)
                {
                    int chk = rnd.Next(0, ingredient_l.Count);
                    stats[y*2+z] = ingredient_l[chk];
                    ingredient_l.RemoveAt(chk);
                }
                
            }
            for (int y = 0; y < 3; y++)
            {
                part_ingredient.Add((Ingredients.bodyParts)parts_v[y],new HashSet<Ingredients>());
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
            for (int y = 0; y < stats.Count(); y++)
            {
                foreach(int prime in parts_v)
                {
                    if(parts_n[y/2] % prime == 0)
                    {
                        Ingredients temp = new Ingredients(stats[y], y / 2, prime, x);
                        ingredient_stat.Add(stats[y], temp);
                        stat_ingredient[((Ingredients.stats)(y / 2))].Add(temp);
                        part_ingredient[((Ingredients.bodyParts)prime)].Add(temp);
                        parts_n[y / 2] /= prime;
                        break;
                    }
                }
            }
        }
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // Asssign recipes at start of game
    void Assign () {

    }
    List<string> GetIngredientsTier(int x)
    {
        if(!tier_ingredient[x].Any())
        {
        }
        return this.tier_ingredient[x].ToList();
    }
}
