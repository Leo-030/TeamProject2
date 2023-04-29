using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

[Serializable]
public class StageData
{
    public int stage;
    public int sub;
    public int level;
    public List<int> monsterList;
    public List<int> bossList;

    //public StageData(StageData s)
    //{
    //    this.stage = s.stage;
    //    this.sub = s.sub;
    //    this.level = s.level;
    //    this.monsterList = s.monsterList.ToList();
    //    this.bossList = s.bossList.ToList();
    //}
}
