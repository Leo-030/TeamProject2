using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFightManager : MonoBehaviour
{
    public GameObject monster;
    public GameObject character;
    public List<GameObject> tutorialUI;
    public GameObject fightUI;
    public GameObject menuUI;
    private CharacterData monsterData;
    private CharacterData characterData;
    private bool isChange = false;
    private bool myTurn = true;
    private int tutorialScene = 0;

    // Start is called before the first frame update
    void Start()
    {
        monsterData = new CharacterData(0, "���� �ϼ���", "Ʃ�丮�� ���� �ϼ����̴�.",TypeData.fire ,100, 0, 0, 10, 10, new List<SkillData>());
        string description = "������ �� ����� �翬�� �����ο� �������� �� ������ ������ �����ִ� ���� �年�ϴ�." +
            "���� �Ű谡 �������� ���� �ʾҴٸ� ��� �츮�� �� �����־��� ���� �ֽ��ϴ�.";
        List<SkillData> skillData = new List<SkillData>();
        skillData.Add(new SkillData(0, "������ Ÿ��", "������ ������ ����� ������ �ľ��Ͽ� �����ϴ�.", 50, 0, 0, 0, 0, 0, 0, 250));
        skillData.Add(new SkillData(1, "�����ο�", "������ ����մϴ�. ���� ������ ������ ���� �ֽ��ϴ�.", 50, 0, 0, 0, 200, 120, 2, 0));
        characterData = new CharacterData(0, "������ �� ����", description, TypeData.normal, 100, 100, 10, 10, 10, skillData);
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

    void ChangeObject(GameObject first, GameObject second)
    {
        first.SetActive(false);
        second.SetActive(true);
    }

    
}
