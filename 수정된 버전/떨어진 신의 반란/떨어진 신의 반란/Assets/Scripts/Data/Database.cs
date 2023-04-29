using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Database
{
    public PlayerData playerData;
    public List<TypeData> typeDataList;
    public List<SkillData> skillDataList;
    public List<CharacterData> characterDataList;
    public List<PatternData> patternDataList; 
    public List<MonsterData> monsterDataList;
    public List<BossData> bossDataList;
    public List<StageData> stageDataList;
}
