using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public int id;
    public string name;
    public string image;
    public int hp;
    public int str;
}

public class DataBase
{
    public List<CharacterData> characterDataList = new List<CharacterData>();
    public List<CharacterData> monsterDataList = new List<CharacterData>();
    public List<CharacterData> bossDataList = new List<CharacterData>();
}

public class PlayerData
{
    public int gold;
    public List<int> hasCharacterId = new List<int>();
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    string dataBasePath;
    string playerDataPath;
    public DataBase dataBase;
    public PlayerData playerData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        dataBasePath = Path.Combine("./Data/", "dataBase.json");
        playerDataPath = Path.Combine("./Data/", "playerData.json");
        JsonLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JsonLoad()
    {
        dataBase = new DataBase();
        playerData = new PlayerData();

        if (!File.Exists(dataBasePath))
        {
            Application.Quit();
        }
        else
        {
            string loadJson = File.ReadAllText(dataBasePath);
            dataBase = JsonUtility.FromJson<DataBase>(loadJson);
        }

        if (!File.Exists(playerDataPath))
        {
            playerData.gold = 300;
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(playerDataPath);
            playerData = JsonUtility.FromJson<PlayerData>(loadJson);
        }
    }

    public void JsonSave()
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(playerDataPath, json);
        json = JsonUtility.ToJson(dataBase, true);
        File.WriteAllText(dataBasePath, json);
    }
}
