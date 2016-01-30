using UnityEngine;
using System.Collections;

public class Ingredients {
    public enum bodyParts { Horns = 2, Limbs = 3, Body = 5 };
    public enum stats { Power, Cleverness, Seduction, Deception, Occult };
    stats stat;
    string i_name;
    bodyParts part;
    int tier;
    public stats GetStat()
    {
        return this.stat;
    }
    public bodyParts GetPart()
    {
        return this.part;
    }
    public string GetName()
    {
        return this.i_name;
    }
    public Ingredients(string name, int stat, int part, int tier)
    {
        this.i_name = name;
        this.stat = (stats)stat;
        this.part = (bodyParts)part;
        this.tier = tier;
    }
    public string ToString()
    { return this.i_name + " " + this.stat + " " + this.part;
    }
}
