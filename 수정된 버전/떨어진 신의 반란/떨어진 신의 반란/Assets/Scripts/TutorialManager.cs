using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    void Awake()
    {
        if (DataManager.instance.database.playerData.tutorial)
        {

        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
