using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFightManager : MonoBehaviour
{
    public List<GameObject> tutorialUI;
    public GameObject fightUI;
    public GameObject menuUI;
    private bool isChange = false;
    private int tutorialScene = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange)
        {
            Print();
        }
    }

    void Print()
    {
        if (tutorialUI.Count > tutorialScene)
        {
            ChangeObject(tutorialUI[tutorialScene - 1], tutorialUI[tutorialScene]);
            isChange = false;
            return;
        }
        if (tutorialUI.Count == tutorialScene)
        {
            ChangeObject(tutorialUI[tutorialScene - 1], fightUI);
            isChange = false;
            return;
        }
        
        isChange = false;
    }

    public void NextTutorialScene()
    {
        if (tutorialUI.Count > tutorialScene)
        {
            tutorialScene++;
        }
        isChange = true;
    }

    public void GoBack()
    {
        menuUI.SetActive(false);
        if (tutorialUI.Count > tutorialScene)
        {
            tutorialUI[tutorialScene].SetActive(true);
        }
        else
        {
            fightUI.SetActive(true);
        }
    }

    public void Finish()
    {
        if (DataManager.instance.database.playerData.tutorial == false)
        {
            DataManager.instance.database.playerData.tutorial = true;
            DataManager.instance.database.playerData.characterList.Add(0);
            DataManager.instance.JsonSave();
        }

        SceneManager.LoadScene("Lobby");
    }

    void ChangeObject(GameObject first, GameObject second)
    {
        first.SetActive(false);
        second.SetActive(true);
    }     
}
