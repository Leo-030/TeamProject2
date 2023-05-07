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
            DataManager.instance.database.playerData.stageClear = stageManager.GetComponent<StageManager>().stage;
            DataManager.instance.JsonSave();
        }
        Destroy(stageManager);
        SceneManager.LoadScene("Lobby");
    }
}
