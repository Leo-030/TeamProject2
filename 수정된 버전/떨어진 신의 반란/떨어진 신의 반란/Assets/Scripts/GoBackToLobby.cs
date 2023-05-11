using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToLobby : MonoBehaviour
{
    private GameObject stageManager;

    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.Find("StageManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBack()
    {
        if (stageManager.GetComponent<StageManager>().stage > DataManager.instance.database.playerData.stageClear && stageManager.GetComponent<StageManager>().sub > 10)
        {
            switch (stageManager.GetComponent<StageManager>().stage)
            {
                case 1:
                    DataManager.instance.database.playerData.characterList.Add(1);
                    break;
                case 2:
                    DataManager.instance.database.playerData.characterList.Add(2);
                    break;
                case 3:
                    DataManager.instance.database.playerData.characterList.Add(3);
                    break;
                case 4:
                    DataManager.instance.database.playerData.characterList.Add(4);
                    break;
                case 5:
                    DataManager.instance.database.playerData.characterList.Add(5);
                    break;
                case 6:
                    DataManager.instance.database.playerData.characterList.Add(6);
                    break;
            }
            DataManager.instance.database.playerData.stageClear = stageManager.GetComponent<StageManager>().stage;
            DataManager.instance.JsonSave();
            if (stageManager.GetComponent<StageManager>().stage == 7)
            {
                Destroy(stageManager);
                SceneManager.LoadScene("Stage 7 End Story");
                return;
            }
        }
        Destroy(stageManager);
        SceneManager.LoadScene("Lobby");
    }
}
