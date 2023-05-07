using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public bool tutorial;
    public int level;
    public int exp;
    public int stageClear;
    public List<int> characterList;
    public AudioData audioData;
}
