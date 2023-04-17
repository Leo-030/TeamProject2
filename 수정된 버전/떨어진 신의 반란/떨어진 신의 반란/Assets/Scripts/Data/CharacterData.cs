using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public int id;
    public string name;
    public string description;
    public TypeData type;
    public int hp;
    public int mp;
    public int healMp;
    public int str;
    public int def;
    public List<SkillData> hasSkillList;

    public CharacterData(int id, string name, string description, TypeData type, int hp, int mp, int healMp, int str, int def, List<SkillData> hasSkillList)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.type = type;
        this.hp = hp;
        this.mp = mp;
        this.healMp = healMp;
        this.str = str;
        this.def = def;
        this.hasSkillList = hasSkillList;
    }

    public CharacterData(CharacterData c)
    {
        this.id = c.id;
        this.name = c.name;
        this.description = c.description;
        this.type = c.type;
        this.hp = c.hp;
        this.mp = c.mp;
        this.healMp = c.healMp;
        this.str = c.str;
        this.def = c.def;
        this.hasSkillList = c.hasSkillList;
    }
}
