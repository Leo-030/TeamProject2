using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageData
{
    public int stage;
    public int sub;
    public int level;
    public List<int> monsterList;
    public List<int> bossList;
}
