using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
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

    //public CharacterData(int id, string image, string name, string description, int type, int hp, int hpInc, int str, int strInc, int def, int defInc, List<SkillData> hasSkillList)
    //{
    //    this.id = id;
    //    this.image = new string(image);
    //    this.name = new string(name);
    //    this.description = new string(description);
    //    this.type = type;
    //    this.hp = hp;
    //    this.hpInc = hpInc;
    //    this.str = str;
    //    this.strInc = strInc;
    //    this.def = def;
    //    this.defInc = defInc;
    //    this.hasSkillList = hasSkillList.ToList();
    //}

    //public CharacterData(CharacterData c)
    //{
    //    this.id = c.id;
    //    this.image = new string(c.image);
    //    this.name = new string(c.name);
    //    this.description = new string(c.description);
    //    this.type = c.type;
    //    this.hp = c.hp;
    //    this.hpInc = c.hpInc;
    //    this.str = c.str;
    //    this.strInc = c.strInc;
    //    this.def = c.def;
    //    this.defInc = defInc;
    //    this.hasSkillList = c.hasSkillList.ToList();
    //}
}
