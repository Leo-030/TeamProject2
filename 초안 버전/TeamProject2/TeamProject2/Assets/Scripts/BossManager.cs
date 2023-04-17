using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public GameObject currentPartyState;
    public GameObject openTurnUI;
    public GameObject playerUI;
    private const int actionBoss = 2;
    private List<GameObject> bossObjectList = new List<GameObject>();
    private List<CharacterData> bossList = new List<CharacterData>();
    private List<CharacterData> currentBossList = new List<CharacterData>();
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
        CreateBoss();
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

    void CreateBoss()
    {
        int index = 0;
        bossList.Add(DataManager.instance.dataBase.bossDataList[index]);
        CharacterData boss = new CharacterData();
        boss.id = DataManager.instance.dataBase.bossDataList[index].id;
        boss.name = DataManager.instance.dataBase.bossDataList[index].name;
        boss.image = DataManager.instance.dataBase.bossDataList[index].image;
        boss.hp = DataManager.instance.dataBase.bossDataList[index].hp;
        boss.str = DataManager.instance.dataBase.bossDataList[index].str;
        currentBossList.Add(boss);
    }
    
    void Print()
    {
        if (currentBossList.Count == 0)
        {
            SceneManager.LoadScene("Finish");
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
            string text = "Boss Turn";
            GameObject turn = Instantiate(openTurnUI);
            turn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
            turn.transform.SetParent(GameObject.Find("Canvas").transform);
            turn.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(turn, 1);
        }
        
        
        for (int i = 0; i < bossObjectList.Count; i++)
        {
            Destroy(bossObjectList[i]);
        }
        bossObjectList.Clear();
        for (int i = 0; i < partyObjectList.Count; i++)
        {
            Destroy(partyObjectList[i]);
        }
        partyObjectList.Clear();

        int width = 250;
        int height = -400;
        for (int i = 0; i < currentBossList.Count; i++)
        {
            string imgPath = Path.Combine("./Data/", currentBossList[i].image);
            string name = currentBossList[i].name;
            int currentHp = currentBossList[i].hp;
            int hp = bossList[i].hp;
            int str = currentBossList[i].str;
            GameObject tmp = Instantiate(currentPartyState);
            tmp.transform.GetChild(0).GetComponent<Image>().sprite = LoadImage(imgPath);
            tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
            tmp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Hp : " + currentHp + " / " +hp;
            tmp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Str : " + str;
            tmp.transform.SetParent(GameObject.Find("Boss UI").transform);
            tmp.transform.localPosition = new Vector3(300 - width * (i % 4), 200 + height * (i / 4), 0);
            bossObjectList.Add(tmp);
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
            tmp.transform.SetParent(GameObject.Find("Boss UI").transform);
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
            for (int i = 0; i < currentBossList.Count; i++)
            {
                int index = Random.Range(0, actionBoss);
                switch (index)
                {
                    case 0:
                        BossAttack(i);
                        break;
                    case 1:
                        BossPass(i);
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
        currentBossList[index].hp -= PartyManager.instance.currentParty[selected].str;
        if (currentBossList[index].hp <= 0)
        {
            currentBossList.RemoveAt(index);
            bossList.RemoveAt(index);
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

    void BossAttack(int monsterIndex)
    {
        int index = Random.Range(0, PartyManager.instance.currentParty.Count);
        PartyManager.instance.currentParty[index].hp -= currentBossList[monsterIndex].str;
        if (PartyManager.instance.currentParty[index].hp <= 0)
        {
            PartyManager.instance.currentParty.RemoveAt(index);
            PartyManager.instance.party.RemoveAt(index);
        }
    }

    void BossPass(int monsterIndex)
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
