using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public bool tutorial;
    public string name;
    public int level;
    public int stageClear;
    public List<CharacterData> hasCharacterList;
}
