using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData
{
    public int id;
    public string image;
    public string name;
    public int type;
    public int hp;
    public int hpInc;
    public int str;
    public int strInc;
    public int def;
    public int defInc;
    public List<PatternOrder> patternList;
}
