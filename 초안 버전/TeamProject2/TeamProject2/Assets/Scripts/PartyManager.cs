using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    public static PartyManager instance;
    public const int numMembers = 3;
    public List<CharacterData> party = new List<CharacterData>();
    public List<CharacterData> currentParty = new List<CharacterData>();
    public GameObject wrapper;
    public GameObject characterUI;
    public GameObject partyState;
    public bool isChange = false;
    private List<GameObject> partyObject = new List<GameObject>();

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
        PrintHasCharacterList();
        PrintParty();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange)
        {
            PrintParty();
            isChange = false;
        }
    }

    void PrintHasCharacterList()
    {
        int width = 150;
        int height = 200;
        int cols = 4;
        for (int i = 0; i < DataManager.instance.playerData.hasCharacterId.Count; i++)
        {
            if (i % cols == 0)
            {
                GameObject wrapperTmp = Instantiate(wrapper);
                wrapperTmp.transform.SetParent(GameObject.Find("Content").transform);
            }
            for (int j = 0; j < DataManager.instance.dataBase.characterDataList.Count; j++)
            {
                if (DataManager.instance.playerData.hasCharacterId[i] == DataManager.instance.dataBase.characterDataList[j].id)
                {
                    string imgPath = Path.Combine("./Data/", DataManager.instance.dataBase.characterDataList[j].image);
                    string text = DataManager.instance.dataBase.characterDataList[j].name;
                    GameObject tmp = Instantiate(characterUI);
                    tmp.GetComponent<CharacterUIClick>().data = DataManager.instance.dataBase.characterDataList[j];
                    tmp.transform.GetChild(0).GetComponent<Image>().sprite = LoadImage(imgPath);
                    tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
                    tmp.transform.SetParent(GameObject.Find("Content").transform.GetChild(i / 4));
                    tmp.transform.localPosition = new Vector3(-225 + width * (i % cols), 0, 0);
                    
                    break;
                }
            }
        }
    }

    void PrintParty()
    {
        for (int i = 0; i < partyObject.Count; i++)
        {
            Destroy(partyObject[i]);
        }
        partyObject.Clear();
        int width = 250;
        int height = -400;
        for (int i = 0; i < party.Count; i++)
        {
            string imgPath = Path.Combine("./Data/", party[i].image);
            string name = party[i].name;
            int hp = party[i].hp;
            int str = party[i].str;
            GameObject tmp = Instantiate(partyState);
            tmp.transform.GetChild(0).GetComponent<Image>().sprite = LoadImage(imgPath);
            tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
            tmp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Hp : " + hp;
            tmp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Str : " + str;
            tmp.transform.GetChild(4).GetComponent<ClearButton>().id = i;
            tmp.transform.SetParent(GameObject.Find("Party UI").transform);
            tmp.transform.localPosition = new Vector3(-300 + width * (i % 4), 200 + height * (i / 4), 0);
            partyObject.Add(tmp);
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
}
