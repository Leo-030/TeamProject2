using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;
    public GameObject currentPartyState;
    public GameObject openTurnUI;
    public GameObject playerUI;
    public const int maxMonster = 3;
    private const int actionMonster = 2;
    private List<GameObject> monsterObjectList = new List<GameObject>();
    private List<CharacterData> monsterList = new List<CharacterData>();
    private List<CharacterData> currentMonsterList = new List<CharacterData>();
    private List<GameObject> partyObjectList = new List<GameObject>();
    private bool isChange = false;
    private bool myTurn = true;
    private int selected = 0;
    private GameObject playerUITmp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateMonsters();
        Print();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange)
        {
            Print();
            isChange = false;
        }
    }

    void CreateMonsters()
    {
        int numMonster = Random.Range(0, maxMonster) + 1;
        for (int i = 0; i < numMonster; i++)
        {
            int index = Random.Range(0, DataManager.instance.dataBase.monsterDataList.Count);
            monsterList.Add(DataManager.instance.dataBase.monsterDataList[index]);
            CharacterData monster = new CharacterData();
            monster.id = DataManager.instance.dataBase.monsterDataList[index].id;
            monster.name = DataManager.instance.dataBase.monsterDataList[index].name;
            monster.image = DataManager.instance.dataBase.monsterDataList[index].image;
            monster.hp = DataManager.instance.dataBase.monsterDataList[index].hp;
            monster.str = DataManager.instance.dataBase.monsterDataList[index].str;
            currentMonsterList.Add(monster);
        }
    }
    
    void Print()
    {
        if (currentMonsterList.Count == 0)
        {
            SceneManager.LoadScene("Play");
            return;
        }
        if (PartyManager.instance.currentParty.Count == 0)
        {
            SceneManager.LoadScene("Lobby");
            return;
        }
        if (myTurn && selected == 0)
        {
            string text = "Player Turn";
            GameObject turn = Instantiate(openTurnUI);
            turn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
            turn.transform.SetParent(GameObject.Find("Canvas").transform);
            turn.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(turn, 1);
        }
        else if (!myTurn)
        {
            string text = "Monster Turn";
            GameObject turn = Instantiate(openTurnUI);
            turn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
            turn.transform.SetParent(GameObject.Find("Canvas").transform);
            turn.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(turn, 1);
        }
        
        
        for (int i = 0; i < monsterObjectList.Count; i++)
        {
            Destroy(monsterObjectList[i]);
        }
        monsterObjectList.Clear();
        for (int i = 0; i < partyObjectList.Count; i++)
        {
            Destroy(partyObjectList[i]);
        }
        partyObjectList.Clear();

        int width = 250;
        int height = -400;
        for (int i = 0; i < currentMonsterList.Count; i++)
        {
            string imgPath = Path.Combine("./Data/", currentMonsterList[i].image);
            string name = currentMonsterList[i].name;
            int currentHp = currentMonsterList[i].hp;
            int hp = monsterList[i].hp;
            int str = currentMonsterList[i].str;
            GameObject tmp = Instantiate(currentPartyState);
            tmp.transform.GetChild(0).GetComponent<Image>().sprite = LoadImage(imgPath);
            tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
            tmp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Hp : " + currentHp + " / " +hp;
            tmp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Str : " + str;
            tmp.transform.SetParent(GameObject.Find("Fight UI").transform);
            tmp.transform.localPosition = new Vector3(300 - width * (i % 4), 200 + height * (i / 4), 0);
            monsterObjectList.Add(tmp);
        }

        for (int i = 0; i < PartyManager.instance.currentParty.Count;  i++)
        {
            string imgPath = Path.Combine("./Data/", PartyManager.instance.currentParty[i].image);
            string name = PartyManager.instance.currentParty[i].name;
            int currentHp = PartyManager.instance.currentParty[i].hp;
            int hp = PartyManager.instance.party[i].hp;
            int str = PartyManager.instance.currentParty[i].str;
            GameObject tmp = Instantiate(currentPartyState);
            tmp.transform.GetChild(0).GetComponent<Image>().sprite = LoadImage(imgPath);
            tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
            tmp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Hp : " + currentHp + " / " + hp;
            tmp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Str : " + str;
            tmp.transform.SetParent(GameObject.Find("Fight UI").transform);
            tmp.transform.localPosition = new Vector3(-500 + width * (i % 4), -200 + height * (i / 4), 0);
            partyObjectList.Add(tmp);
        }

        if (myTurn)
        {
            if (selected >= PartyManager.instance.currentParty.Count)
            {
                selected = 0;
                myTurn = false;
                Print();
                return;
            }
            PrintPlayerUI(selected);
        }
        else
        {
            for (int i = 0; i < currentMonsterList.Count; i++)
            {
                int index = Random.Range(0, actionMonster);
                switch (index)
                {
                    case 0:
                        MonsterAttack(i);
                        break;
                    case 1:
                        MonsterPass(i);
                        break;
                }
            }
            myTurn = true;
            Invoke("Print", 2);
        }
    }

    void PrintPlayerUI(int index)
    {
        partyObjectList[index].GetComponent<Image>().color = Color.yellow;
        playerUITmp = Instantiate(playerUI);
        playerUITmp.transform.SetParent(GameObject.Find("PlayerUIWrapper").transform);
        playerUITmp.transform.localPosition = new Vector3(0, 0, 0);
    }

    void EndPlayerUI()
    {
        Destroy(playerUITmp);
    }

    public void Attack(int index)
    {
        currentMonsterList[index].hp -= PartyManager.instance.currentParty[selected].str;
        if (currentMonsterList[index].hp <= 0)
        {
            currentMonsterList.RemoveAt(index);
            monsterList.RemoveAt(index);
        }
        selected++;
        isChange = true;
        EndPlayerUI();
    }

    public void Pass()
    {
        selected++;
        isChange = true;
        EndPlayerUI();
    }

    void MonsterAttack(int monsterIndex)
    {
        int index = Random.Range(0, PartyManager.instance.currentParty.Count);
        PartyManager.instance.currentParty[index].hp -= currentMonsterList[monsterIndex].str;
        if (PartyManager.instance.currentParty[index].hp <= 0)
        {
            PartyManager.instance.currentParty.RemoveAt(index);
            PartyManager.instance.party.RemoveAt(index);
        }
    }

    void MonsterPass(int monsterIndex)
    {

    }

    private Sprite LoadImage(string path)
    {
        byte[] byteTexture = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(0, 0);
        texture.LoadImage(byteTexture);

        Rect rect = new Rect(0, 0, texture.width, texture.height);

        return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }
}
