using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TypeData
{
    public int id;
    public string name;
    public static TypeData normal = new TypeData(0, "公加己");
    public static TypeData fire = new TypeData(1, "阂加己");
    public static TypeData water = new TypeData(2, "拱加己");
    public static TypeData nature = new TypeData(3, "磊楷加己");
    public static TypeData ground = new TypeData(4, "措瘤加己");
    public static TypeData light = new TypeData(5, "蝴加己");
    public static TypeData dark = new TypeData(6, "绢狄加己");

    private TypeData(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}
