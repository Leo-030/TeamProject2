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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
