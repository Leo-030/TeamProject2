using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;
    public int currentStage = 0;
    public GameObject node;
    public GameObject currentPartyState;
    public const int maxStage = 10;
    const int nodeKinds = 2;
    private List<GameObject> nodeList = new List<GameObject>();
    private GameObject playUI;
    private TextMeshProUGUI stage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            instance.Start();
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playUI = GameObject.Find("Play UI");
        stage = playUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        currentStage++;
        stage.text = "Stage " + instance.currentStage;
        for (int i = 0; i < PartyManager.instance.currentParty.Count; i++)
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
            tmp.transform.SetParent(GameObject.Find("Play UI").transform);
            tmp.transform.localPosition = new Vector3(-500, 125 + -185 * i, 0);
        }
        Destroy(GameObject.Find("CurrentPartyState"));
        if (currentStage == maxStage)
        {
            Boss();
            return;
        }
        StartPlay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Boss()
    {
        CreateBossNode();
    }

    void CreateBossNode()
    {
        GameObject tmp = Instantiate(node);
        tmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "보스 전투";
        tmp.transform.GetChild(1).GetComponent<SelectButton>().onClickEvent += () => Select("Boss");
        tmp.transform.SetParent(playUI.transform);
        tmp.transform.localPosition = new Vector3(100, 3, 0);
        nodeList.Add(tmp);
    }

    void StartPlay()
    {
        nodeList.Clear();
        CreateNode();
        CreateNode();
        CreateNode();
        for (int i = 0; i < nodeList.Count; i++)
        {
            nodeList[i].transform.localPosition = new Vector3(-270 + 370 * i, 3, 0);
        }
    }

    private Sprite LoadImage(string path)
    {
        byte[] byteTexture = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(0, 0);
        texture.LoadImage(byteTexture);

        Rect rect = new Rect(0, 0, texture.width, texture.height);

        return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }

    void CreateNode()
    {
        int index = Random.Range(0, nodeKinds);
        switch (index)
        {
            case 0:
                CreateFightNode();
                break;
            case 1:
                CreateHealNode();
                break;
        }
    }

    void Select(string name)
    {
        SceneManager.LoadScene(name);
    }

    void CreateFightNode()
    {
        GameObject tmp = Instantiate(node);
        tmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "전투";
        tmp.transform.GetChild(1).GetComponent<SelectButton>().onClickEvent += () => Select("Fight");
        tmp.transform.SetParent(playUI.transform);
        nodeList.Add(tmp);
    }

    void CreateHealNode()
    {
        GameObject tmp = Instantiate(node);
        tmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "힐";
        tmp.transform.GetChild(1).GetComponent<SelectButton>().onClickEvent += () => Select("Heal");
        tmp.transform.SetParent(playUI.transform);
        nodeList.Add(tmp);
    }
}
