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
        monsterData = new CharacterData(0, "불의 하수인", "튜토리얼 불의 하수인이다.",TypeData.fire ,100, 0, 0, 10, 10, new List<SkillData>());
        string description = "지혜의 신 지루는 당연히 지혜로운 신이지만 그 지혜를 남에게 빌려주는 일은 드뭅니다." +
            "만약 신계가 이지경이 되지 않았다면 평생 우리를 안 도와주었을 수도 있습니다.";
        List<SkillData> skillData = new List<SkillData>();
        skillData.Add(new SkillData(0, "지혜의 타격", "지혜를 가지고 상대의 약점을 파악하여 때립니다.", 50, 0, 0, 0, 0, 0, 0, 250));
        skillData.Add(new SkillData(1, "지혜로움", "지혜를 사용합니다. 물론 남에게 빌려줄 수도 있습니다.", 50, 0, 0, 0, 200, 120, 2, 0));
        characterData = new CharacterData(0, "지혜의 신 지루", description, TypeData.normal, 100, 100, 10, 10, 10, skillData);
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
