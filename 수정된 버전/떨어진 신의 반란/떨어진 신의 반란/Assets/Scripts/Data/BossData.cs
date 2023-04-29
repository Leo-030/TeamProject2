using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

[Serializable]
public class BossData
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

    //public BossData(int id, string image, string name, int type, int hp, int str, int def, List<SkillData> hasSkillList)
    //{
    //    this.id = id;
    //    this.image = new string(image);
    //    this.name = new string(name);
    //    this.type = type;
    //    this.str = str;
    //    this.def = def;
    //    this.hasPatternList = hasPatternList.ToList();
    //}

    //public BossData(BossData c)
    //{
    //    this.id = c.id;
    //    this.image = new string(c.image);
    //    this.name = new string(c.name);
    //    this.type = c.type;
    //    this.hp = c.hp;
    //    this.str = c.str;
    //    this.def = c.def;
    //    this.hasPatternList = c.hasPatternList.ToList();
    //}
}
