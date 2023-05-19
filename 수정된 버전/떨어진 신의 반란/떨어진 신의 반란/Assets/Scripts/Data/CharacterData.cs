using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public int id;
    public string image;
    public string name;
    public string description;
    public int type;
    public int hp;
    public int hpInc;
    public int str;
    public int strInc;
    public int def;
    public int defInc;
    public List<int> skillList;
}
