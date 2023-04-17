using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public Database database;
    private string databasePath;

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

    void Start()
    {
        databasePath = Path.Combine("./Data/", "database.json");
        JsonLoad();
    }

    public void JsonLoad()
    {
        database = new Database();

        if (!File.Exists(databasePath))
        {
            Application.Quit();
        }
        else
        {
            string loadJson = File.ReadAllText(databasePath);
            database = JsonUtility.FromJson<Database>(loadJson);
        }
    }

    public void JsonSave()
    {
        string json = JsonUtility.ToJson(database, true);
        File.WriteAllText(databasePath, json);
    }
}
