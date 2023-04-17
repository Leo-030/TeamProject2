using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TypeData
{
    public int id;
    public string name;
    public static TypeData normal = new TypeData(0, "���Ӽ�");
    public static TypeData fire = new TypeData(1, "�ҼӼ�");
    public static TypeData water = new TypeData(2, "���Ӽ�");
    public static TypeData nature = new TypeData(3, "�ڿ��Ӽ�");
    public static TypeData ground = new TypeData(4, "�����Ӽ�");
    public static TypeData light = new TypeData(5, "���Ӽ�");
    public static TypeData dark = new TypeData(6, "��ҼӼ�");

    private TypeData(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}
