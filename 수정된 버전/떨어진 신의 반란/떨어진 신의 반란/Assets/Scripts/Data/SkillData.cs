using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData
{
    public int id;
    public string name;
    public string description;
    public int useMp;
    public int hp;
    public int mp;
    public int healMp;
    public int str;
    public int def;
    public int turn;
    public int skillStr;

    public SkillData(int id, string name, string description, int useMp, int hp, int mp, int healMp, int str, int def, int turn, int skillStr)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.useMp = useMp;
        this.hp = hp;
        this.mp = mp;
        this.healMp = healMp;
        this.str = str;
        this.def = def;
        this.turn = turn;
        this.skillStr = skillStr;
    }
}
