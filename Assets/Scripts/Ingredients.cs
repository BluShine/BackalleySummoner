using UnityEngine;
using System.Collections;

public class Ingredients {
    public enum bodyParts { Head = 2, Limbs = 3, Torso = 5 };
    public enum stats { Power, Cleverness, Seduction, Deception, Occult };
    stats stat;
    string i_name;
    bodyParts part;
    public stats GetStat()
    {
        return this.stat;
    }
    public bodyParts getPart()
    {
        return this.part;
    }
    public Ingredients(string name, int stat, int part)
    {
        this.i_name = name;
        this.stat = (stats)stat;
        this.part = (bodyParts)part;
    }
    public string ToString()
    { return this.i_name + " " + this.stat + " " + this.part;
    }
}
