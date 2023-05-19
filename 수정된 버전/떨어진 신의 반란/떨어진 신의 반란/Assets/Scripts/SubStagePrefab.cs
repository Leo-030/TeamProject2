using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubStagePrefab : MonoBehaviour
{
    public int stage;
    public int sub;
    private bool isStory = false;

    // Start is called before the first frame update
    void Start()
    {
        if (sub == 10 && stage > DataManager.instance.database.playerData.stageClear)
        {
            isStory = true;
        }
    }

    public void OnClick()
    {
        if (isStory)
        {
            SceneManager.LoadScene("Stage " + stage +" Story");
            return;
        }
        SceneManager.LoadScene("Fight");
    }
}
