using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
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

    //public MonsterData(int id, string image, string name, int type, int hp, int hpInc, int str, int strInc, int def, int defInc, List<SkillData> hasSkillList)
    //{
    //    this.id = id;
    //    this.image = new string(image);
    //    this.name = new string(name);
    //    this.type = type;
    //    this.hpInc = hpInc;
    //    this.str = str;
    //    this.strInc = strInc;
    //    this.def = def;
    //    this.defInc = defInc;
    //    this.hasPatternList = hasPatternList.ToList();
    //}

    //public MonsterData(MonsterData c)
    //{
    //    this.id = c.id;
    //    this.image = new string(c.image);
    //    this.name = new string(c.name);
    //    this.type = c.type;
    //    this.hp = c.hp;
    //    this.hpInc = c.hpInc;
    //    this.str = c.str;
    //    this.strInc = c.strInc;
    //    this.def = c.def;
    //    this.defInc = defInc;
    //    this.hasPatternList = c.hasPatternList.ToList();
    //}
}
