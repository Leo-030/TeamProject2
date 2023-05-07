using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    class Character
    {
        public int id;
        public string image;
        public string name;
        public TypeData type;
        public int hp;
        public int maxHp;
        public int str;
        public int def;
        public List<Buf> bufList;

        public Character(CharacterData c)
        {
            this.id = c.id;
            this.image = new string(c.image);
            this.name = new string(c.name);
            for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
            {
                if (c.type == DataManager.instance.database.typeDataList[i].id)
                {
                    this.type = new TypeData(DataManager.instance.database.typeDataList[i]);
                    break;
                }
            }
            int level = DataManager.instance.database.playerData.level;
            this.hp = c.hp + c.hpInc * level;
            this.maxHp = c.hp + c.hpInc * level;
            this.str = c.str + c.strInc * level;
            this.def = c.def + c.defInc * level;
            this.bufList = new List<Buf>();
        }

        public void Reset()
        {
            for (int i = 0; i < bufList.Count; i++)
            {
                str -= bufList[i].str;
                def -= bufList[i].def;
            }
            bufList.Clear();
            hp = 0;
        }
    }

    class Card
    {
        public int id;
        public int characterId;
        public string image;
        public string name;
        public string description;
        public int cost;

        public Card(CharacterData c, int index)
        {
            this.characterId = c.id;
            this.image = new string(c.image);
            for (int i = 0; i < DataManager.instance.database.skillDataList.Count; i++)
            {
                if (c.skillList[index] == DataManager.instance.database.skillDataList[i].id)
                {
                    this.id = DataManager.instance.database.skillDataList[i].id;
                    this.name = new string(DataManager.instance.database.skillDataList[i].name);
                    this.description = new string(DataManager.instance.database.skillDataList[i].description);
                    this.cost = DataManager.instance.database.skillDataList[i].cost;
                    break;
                }
            }
        }
    }

    class Monster
    {
        public string image;
        public string name;
        public TypeData type;
        public int hp;
        public int maxHp;
        public int str;
        public int def;
        public int cycle;
        public List<Pattern> patternList;
        public List<Buf> bufList;

        public Monster(MonsterData m, int level)
        {
            this.image = new string(m.image);
            this.name = new string(m.name);
            for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
            {
                if (m.type == DataManager.instance.database.typeDataList[i].id)
                {
                    this.type = new TypeData(DataManager.instance.database.typeDataList[i]);
                    break;
                }
            }
            this.hp = m.hp + m.hpInc * level;
            this.maxHp = m.hp + m.hpInc * level;
            this.str = m.str + m.strInc * level;
            this.def = m.def + m.defInc * level;
            this.patternList = new List<Pattern>();
            for (int i = 0; i < m.patternList.Count; i++)
            {
                this.patternList.Add(new Pattern(m.patternList[i]));
            }
            this.bufList = new List<Buf>();
        }

        public Monster(BossData b, int level)
        {
            this.image = new string(b.image);
            this.name = new string(b.name);
            for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
            {
                if (b.type == DataManager.instance.database.typeDataList[i].id)
                {
                    this.type = new TypeData(DataManager.instance.database.typeDataList[i]);
                    break;
                }
            }
            this.hp = b.hp + b.hpInc * level;
            this.maxHp = b.hp + b.hpInc * level;
            this.str = b.str + b.strInc * level;
            this.def = b.def + b.defInc * level;
            this.patternList = new List<Pattern>();
            for (int i = 0; i < b.patternList.Count; i++)
            {
                this.patternList.Add(new Pattern(b.patternList[i]));
            }
            this.bufList = new List<Buf>();
        }

        public void Reset()
        {
            for (int i = 0; i < bufList.Count; i++)
            {
                str -= bufList[i].str;
                def -= bufList[i].def;
            }
            bufList.Clear();
            hp = 0;
        }
    }

    class Pattern
    {
        public int id;
        public string name;
        public string description;
        public int order;

        public Pattern(PatternOrder p)
        {
            this.id = p.id;
            for (int i = 0; i < DataManager.instance.database.patternDataList.Count; i++)
            {
                if (p.id == DataManager.instance.database.patternDataList[i].id)
                {
                    this.name = new string(DataManager.instance.database.patternDataList[i].name);
                    this.description = new string(DataManager.instance.database.patternDataList[i].description);
                    break;
                }
            }
            this.order = p.order;
        }
    }

    class Buf
    {
        public int turn;
        public bool isLive;
        public bool isTarget;
        public int damageType;
        public int damage;
        public int damageDeadHpPercent;
        public int str;
        public int def;

        public Buf(int turn, bool isLive, bool isTarget, int damageType, int damage, int damageDeadHpPercent, int str, int def)
        {
            this.turn = turn;
            this.isLive = isLive;
            this.isTarget = isTarget;
            this.damageType = damageType;
            this.damage = damage;
            this.damageDeadHpPercent = damageDeadHpPercent;
            this.str = str;
            this.def = def;       
        }
    }


    public List<GameObject> bgimage;
    public GameObject selectCharacterPrefab;
    public GameObject cancleCharacterPrefab;
    public GameObject wrapper;
    public GameObject characterHpPrefab;
    public GameObject turnPrefab;
    public GameObject cardPrefab;
    public GameObject wantPrefab;
    public GameObject selectWantPrefab;
    public GameObject endGame;

    public int cost = 0;
    public int nextAttack = 100;
    public int nextAttackType = -1;
    public int[] field;

    private int stage;
    private int sub;
    private int level;
    private GameObject canvas;
    private GameObject selectUI;
    private GameObject fightUI;
    private List<int> idList = new List<int>();
    private List<GameObject> idGameObjectList = new List<GameObject>();
    private List<Character> liveCharacterList = new List<Character>();
    private List<GameObject> liveCharacterGameObjectList = new List<GameObject>();
    private List<Character> deadCharacterList = new List<Character>();
    private List<Monster> liveMonsterList = new List<Monster>();
    private List<GameObject> liveMonsterGameObjectList = new List<GameObject>();
    private List<Monster> deadMonsterList = new List<Monster>();
    private List<Card> liveCardList = new List<Card>();
    private List<Card> deadCardList = new List<Card>();
    private List<Card> currentCardList = new List<Card>();
    private List<GameObject> currentCardGameObjectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        stage = StageManager.instance.stage;
        sub = StageManager.instance.sub;
        for (int i = 0; i < bgimage.Count; i++)
        {
            if (i == stage - 1)
            {
                bgimage[i].SetActive(true);
            }
        }
        canvas = GameObject.Find("Canvas");
        selectUI = canvas.transform.GetChild(0).gameObject;
        fightUI = canvas.transform.GetChild(1).gameObject;
        
        // selectUI 설정
        GameObject selectScroll = selectUI.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        int col = 4;
        int width = 315;
        for (int i = 0; i < DataManager.instance.database.playerData.characterList.Count; i++)
        {
            if (i % col == 0)
            {
                GameObject wrapperTmp = Instantiate(wrapper);
                wrapperTmp.transform.SetParent(selectScroll.transform);
                wrapperTmp.transform.localScale = new Vector3(1, 1, 1);
            }

            GameObject tmp = Instantiate(selectCharacterPrefab);
            TextMeshProUGUI t = tmp.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            tmp.transform.SetParent(selectScroll.transform.GetChild(i / col));
            tmp.transform.localScale = new Vector3(1, 1, 1);
            tmp.transform.localPosition = new Vector3(-475 + width * (i % col), 0, 0);
            tmp.GetComponent<SelectCharacterPrefab>().id = DataManager.instance.database.playerData.characterList[i];
            foreach (CharacterData c in DataManager.instance.database.characterDataList)
            {
                if (tmp.GetComponent<SelectCharacterPrefab>().id == c.id)
                {
                    t.text = c.name;
                    switch (c.type)
                    {
                        case 0:
                            t.color = new Color32(255, 255, 255, 255);
                            break;
                        case 1:
                            t.color = new Color32(255, 0, 0, 255);
                            break;
                        case 2:
                            t.color = new Color32(0, 78, 255, 255);
                            break;
                        case 3:
                            t.color = new Color32(0, 255, 33, 255);
                            break;
                        case 4:
                            t.color = new Color32(4, 255, 253, 255);
                            break;
                        case 5:
                            t.color = new Color32(255, 241, 4, 255);
                            break;
                        case 6:
                            t.color = new Color32(158, 4, 255, 255);
                            break;
                    }
                    tmp.GetComponent<Image>().sprite = LoadImage("./Data/" + c.image + "/SelectCharacter.png");
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
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

    public void Next()
    {
        if (idList.Count == 0)
        {
            return;
        }
        selectUI.SetActive(false);
        fightUI.SetActive(true);
        StartFight();
    }

    public void SelectParty(int id)
    {
        if (idList.Count >= 3)
        {
            return;
        }
        for (int i = 0; i < idList.Count; i++)
        {
            if (id == idList[i])
            {
                return;
            }
        }
        idList.Add(id);
        GameObject tmp = Instantiate(cancleCharacterPrefab);
        tmp.GetComponent<CancleCharacterPrefab>().id = id;
        tmp.transform.SetParent(selectUI.transform);
        tmp.transform.localScale = new Vector3(1, 1, 1);
        tmp.transform.localPosition = new Vector3(-500 + idGameObjectList.Count * 500, 200, 0);
        TextMeshProUGUI t = tmp.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        foreach (CharacterData c in DataManager.instance.database.characterDataList)
        {
            if (id == c.id)
            {
                t.text = c.name;
                switch (c.type)
                {
                    case 0:
                        t.color = new Color32(255, 255, 255, 255);
                        break;
                    case 1:
                        t.color = new Color32(255, 0, 0, 255);
                        break;
                    case 2:
                        t.color = new Color32(0, 78, 255, 255);
                        break;
                    case 3:
                        t.color = new Color32(0, 255, 33, 255);
                        break;
                    case 4:
                        t.color = new Color32(4, 255, 253, 255);
                        break;
                    case 5:
                        t.color = new Color32(255, 241, 4, 255);
                        break;
                    case 6:
                        t.color = new Color32(158, 4, 255, 255);
                        break;
                }
                tmp.GetComponent<Image>().sprite = LoadImage("./Data/" + c.image + "/Character.png");
                break;
            }
        }
        idGameObjectList.Add(tmp);
    }

    public void CancleParty(int id)
    {
        if (idList.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < idList.Count; i++)
        {
            if (id == idList[i])
            {
                idList.RemoveAt(i);
                Destroy(idGameObjectList[i]);
                idGameObjectList.RemoveAt(i);
                break;
            }
        }
        for (int i = 0; i < idGameObjectList.Count; i++)
        {
            idGameObjectList[i].transform.localPosition = new Vector3(-500 + i * 500, 200, 0);
        }
    }

    private void StartFight()
    {
        // fightUI 설정
        for (int i = 0; i < idList.Count; i++)
        {
            for (int j = 0; j < DataManager.instance.database.characterDataList.Count; j++)
            {
                if (idList[i] == DataManager.instance.database.characterDataList[j].id)
                {
                    liveCharacterList.Add(new Character(DataManager.instance.database.characterDataList[j]));
                    for (int k = 0; k < DataManager.instance.database.characterDataList[j].skillList.Count; k++)
                    {
                        deadCardList.Add(new Card(DataManager.instance.database.characterDataList[j], k));
                    }
                    break;
                }                
            }
        }
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            GameObject tmp = Instantiate(characterHpPrefab);
            tmp.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = LoadImage("./Data/" + liveCharacterList[i].image + "/Character.png");
            tmp.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = liveCharacterList[i].hp +"/" + liveCharacterList[i].maxHp;
            tmp.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Slider>().value = 1;
            tmp.transform.SetParent(fightUI.transform);
            tmp.transform.localScale = new Vector3(1, 1, 1);
            tmp.transform.localPosition = new Vector3(-500 + 200 * i, 100, 0);
            liveCharacterGameObjectList.Add(tmp);
        }

        for (int i = 0; i < DataManager.instance.database.stageDataList.Count; i++)
        {
            if (stage == DataManager.instance.database.stageDataList[i].stage && sub == DataManager.instance.database.stageDataList[i].sub)
            {
                level = DataManager.instance.database.stageDataList[i].level;
                for (int j = 0; j < DataManager.instance.database.stageDataList[i].monsterList.Count; j++)
                {
                    for (int k = 0; k < DataManager.instance.database.monsterDataList.Count; k++)
                    {
                        if (DataManager.instance.database.stageDataList[i].monsterList[j] == DataManager.instance.database.monsterDataList[k].id)
                        {
                            liveMonsterList.Add(new Monster(DataManager.instance.database.monsterDataList[k], DataManager.instance.database.stageDataList[i].level));
                            break;
                        }
                    }
                }
                for (int j = 0; j < DataManager.instance.database.stageDataList[i].bossList.Count; j++)
                {
                    for (int k = 0; k < DataManager.instance.database.bossDataList.Count; k++)
                    {
                        if (DataManager.instance.database.stageDataList[i].bossList[j] == DataManager.instance.database.bossDataList[k].id)
                        {
                            liveMonsterList.Add(new Monster(DataManager.instance.database.bossDataList[k], DataManager.instance.database.stageDataList[i].level));
                            break;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            GameObject tmp = Instantiate(characterHpPrefab);
            tmp.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = LoadImage("./Data/" + liveMonsterList[i].image + "/Character.png");
            tmp.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = liveMonsterList[i].hp + "/" + liveMonsterList[i].maxHp;
            tmp.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Slider>().value = 1;
            tmp.transform.SetParent(fightUI.transform);
            tmp.transform.localScale = new Vector3(1, 1, 1);
            tmp.transform.localPosition = new Vector3(500 - 200 * i, 200, 0);
            liveMonsterGameObjectList.Add(tmp);
        }

        field = new int[DataManager.instance.database.typeDataList.Count];

        for (int i = 0; i < field.Length; i++)
        {
            field[i] = 100;
        }

        StartPlayer();
    }

    private void StartPlayer()
    {
        
        GameObject turnTmp = Instantiate(turnPrefab);
        turnTmp.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Player 턴";
        turnTmp.transform.SetParent(canvas.transform);
        turnTmp.transform.localScale = new Vector3(1, 1, 1);
        turnTmp.transform.localPosition = new Vector3(0, 0, 0);
        Destroy(turnTmp, 2);

        cost = 5;

        if (liveCardList.Count < 5)
        {
            for (int i = 0; i < deadCardList.Count; i++)
            {
                liveCardList.Add(deadCardList[i]);
            }
            deadCardList.Clear();
            for (int i = 0; i < liveCardList.Count; i++)
            {
                int random1 = Random.Range(0, liveCardList.Count);
                int random2 = Random.Range(0, liveCardList.Count);
                Card tmp = liveCardList[random1];
                liveCardList[random1] = liveCardList[random2];
                liveCardList[random2] = tmp;
            }
        }
        
        for (int i = 0; i < 5; i++)
        {
            currentCardList.Add(liveCardList[0]);
            liveCardList.RemoveAt(0);
        }
        for (int i = 0; i < currentCardList.Count; i++)
        {
            GameObject tmp = Instantiate(cardPrefab);
            tmp.GetComponent<CardPrefab>().id = currentCardList[i].id;
            tmp.GetComponent<CardPrefab>().characterId = currentCardList[i].characterId;
            tmp.GetComponent<Image>().sprite = LoadImage("./Data/" + currentCardList[i].image + "/Character.png");
            tmp.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "코스트: " + currentCardList[i].cost.ToString();
            tmp.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text += "\n" + currentCardList[i].description;
            tmp.transform.SetParent(fightUI.transform);
            tmp.transform.localScale = new Vector3(1, 1, 1);
            tmp.transform.localPosition = new Vector3(-500+ i * 180, -250, 0);
            currentCardGameObjectList.Add(tmp);
        }
        endGame.SetActive(true);
        endGame.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "덱: " + liveCardList.Count + "\n묘지: " + deadCardList.Count + "\n코스트: " + cost;
    }

    public void ChooseWantMonster(int characterId, int id)
    {
        endGame.SetActive(false);
        for (int i = 0; i < currentCardGameObjectList.Count; i++)
        {
            currentCardGameObjectList[i].SetActive(false);
        }

        GameObject tmp = Instantiate(wantPrefab);
        tmp.transform.SetParent(fightUI.transform);
        tmp.transform.localScale = new Vector3(1, 1, 1);
        tmp.transform.localPosition = new Vector3(0, -200, 0);
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            GameObject tmp1 = Instantiate(selectWantPrefab);
            tmp1.GetComponent<SelectWantPrefab>().id = id;
            tmp1.GetComponent<SelectWantPrefab>().choose = i;
            tmp1.GetComponent<SelectWantPrefab>().characterId = characterId;
            tmp1.GetComponent<Image>().sprite = LoadImage("./Data/" + liveMonsterList[i].image + "/Character.png");
            tmp1.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            tmp1.transform.SetParent(tmp.transform);
            tmp1.transform.localScale = new Vector3(1, 1, 1);
            tmp1.transform.localPosition = new Vector3(-200 + i * 200, 0, 0);
        }
    }

    public void PrintCurrentCard()
    {
        for (int i = 0; i < currentCardGameObjectList.Count; i++)
        {
            currentCardGameObjectList[i].SetActive(true);
        }
        endGame.SetActive(true);
        endGame.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "덱: " + liveCardList.Count + "\n묘지: " + deadCardList.Count + "\n코스트: " + cost;
    }

    public void DestroyCard(int id)
    {
        for (int i = 0; i < currentCardGameObjectList.Count; i++)
        {
            Destroy(currentCardGameObjectList[i]);
        }
        currentCardGameObjectList.Clear();

        for (int i = 0; i < currentCardList.Count; i++)
        {
            if (id == currentCardList[i].id)
            {
                deadCardList.Add(currentCardList[i]);
                currentCardList.RemoveAt(i);
                break;
            }
        }
        
        for (int i = 0; i < currentCardList.Count; i++)
        {
            GameObject tmp = Instantiate(cardPrefab);
            tmp.GetComponent<CardPrefab>().id = currentCardList[i].id;
            tmp.GetComponent<CardPrefab>().characterId = currentCardList[i].characterId;
            tmp.GetComponent<Image>().sprite = LoadImage("./Data/" + currentCardList[i].image + "/Character.png");
            tmp.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "코스트: " + currentCardList[i].cost.ToString();
            tmp.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text += "\n" + currentCardList[i].description;
            tmp.transform.SetParent(fightUI.transform);
            tmp.transform.localScale = new Vector3(1, 1, 1);
            tmp.transform.localPosition = new Vector3(-500 + i * 180, -250, 0);
            currentCardGameObjectList.Add(tmp);
        }
        endGame.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "덱: " + liveCardList.Count + "\n묘지: " + deadCardList.Count + "\n코스트: " + cost;
    }

    public bool CheckCost(int cost)
    {
        if (this.cost - cost < 0)
        {
            return false;
        }
        this.cost -= cost;
        return true;
    }

    private void DetectDeadMonster()
    {
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            if (liveMonsterList[i].hp <= 0)
            {
                liveMonsterList[i].Reset();
                deadMonsterList.Add(liveMonsterList[i]);
            }
        }
        for (int i = 0; i < deadMonsterList.Count; i++)
        {
            liveMonsterList.Remove(deadMonsterList[i]);
        }
    }

    private void DetectDeadCharacter()
    {
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            bool isLive = false;
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isLive)
                {
                    isLive = true;
                    break;
                }
            }
            if (liveCharacterList[i].hp <= 0)
            {
                if (isLive)
                {
                    liveCharacterList[i].hp = 1;
                }
                else
                {
                    liveCharacterList[i].Reset();
                    deadCharacterList.Add(liveCharacterList[i]);
                }
            }
        }
        for (int i = 0; i < deadCharacterList.Count; i++)
        {
            liveCharacterList.Remove(deadCharacterList[i]);
        }
        List<Card> delete = new List<Card>();
        for (int i = 0; i < liveCardList.Count; i++)
        {
            for (int j = 0; j < deadCharacterList.Count; j++)
            {
                if (liveCardList[i].characterId == deadCharacterList[j].id)
                {
                    delete.Add(liveCardList[i]);
                }
            }
        }
        for (int i = 0; i < delete.Count; i++)
        {
            liveCardList.Remove(delete[i]);
        }
    }

    private void DetectOverHp()
    {
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (liveCharacterList[i].hp > liveCharacterList[i].maxHp)
            {
                liveCharacterList[i].hp = liveCharacterList[i].maxHp;
            }
        }
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            if (liveMonsterList[i].hp > liveMonsterList[i].maxHp)
            {
                liveMonsterList[i].hp = liveMonsterList[i].maxHp;
            }
        }
    }

    private void PrintCM()
    {
        for (int i = 0; i < liveCharacterGameObjectList.Count; i++)
        {
            Destroy(liveCharacterGameObjectList[i]);
        }
        liveCharacterGameObjectList.Clear();
        for (int i = 0; i < liveMonsterGameObjectList.Count; i++)
        {
            Destroy(liveMonsterGameObjectList[i]);
        }
        liveMonsterGameObjectList.Clear();

        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            GameObject tmp = Instantiate(characterHpPrefab);
            tmp.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = LoadImage("./Data/" + liveCharacterList[i].image + "/Character.png");
            tmp.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = liveCharacterList[i].hp + "/" + liveCharacterList[i].maxHp;
            tmp.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Slider>().value = (float) liveCharacterList[i].hp / liveCharacterList[i].maxHp;
            tmp.transform.SetParent(fightUI.transform);
            tmp.transform.localScale = new Vector3(1, 1, 1);
            tmp.transform.localPosition = new Vector3(-500 + 200 * i, 100, 0);
            liveCharacterGameObjectList.Add(tmp);
        }

        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            GameObject tmp = Instantiate(characterHpPrefab);
            tmp.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = LoadImage("./Data/" + liveMonsterList[i].image + "/Character.png");
            tmp.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = liveMonsterList[i].hp + "/" + liveMonsterList[i].maxHp;
            tmp.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Slider>().value = (float)liveMonsterList[i].hp / liveMonsterList[i].maxHp;
            tmp.transform.SetParent(fightUI.transform);
            tmp.transform.localScale = new Vector3(1, 1, 1);
            tmp.transform.localPosition = new Vector3(500 - 200 * i, 200, 0);
            liveMonsterGameObjectList.Add(tmp);
        }
        EndGame();
    }

    public void EndPlayerTurn()
    {
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            List<Buf> delete = new List<Buf>();
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                int attackType = liveCharacterList[i].bufList[j].damageType;
                if (attackType  != -1)
                {
                    int typeStr = 100;
                    for (int k = 0; k < DataManager.instance.database.typeDataList.Count; k++)
                    {
                        if (attackType == DataManager.instance.database.typeDataList[k].id)
                        {
                            if (liveCharacterList[k].type.id == DataManager.instance.database.typeDataList[i].strongType)
                            {
                                typeStr = DataManager.instance.database.typeDataList[k].strongInc;
                                break;
                            }
                        }
                    }
                    liveCharacterList[i].hp -= liveCharacterList[i].bufList[j].damage * typeStr / 100;
                    liveCharacterList[i].hp -= (liveCharacterList[i].maxHp - liveCharacterList[i].hp) * liveCharacterList[i].bufList[j].damageDeadHpPercent / 100 * typeStr / 100;
                }
                liveCharacterList[i].bufList[j].turn--;
                if (liveCharacterList[i].bufList[j].turn == 0)
                {
                    liveCharacterList[i].str -= liveCharacterList[i].bufList[j].str;
                    liveCharacterList[i].def -= liveCharacterList[i].bufList[j].def;
                    delete.Add(liveCharacterList[i].bufList[j]);
                }
            }
            for (int j = 0; j < delete.Count; j++)
            {
                liveCharacterList[i].bufList.Remove(delete[j]);
            }
        }
        DetectDeadCharacter();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            List<Buf> delete = new List<Buf>();
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                int attackType = liveMonsterList[i].bufList[j].damageType;
                if (attackType != -1)
                {
                    int typeStr = 100;
                    for (int k = 0; k < DataManager.instance.database.typeDataList.Count; k++)
                    {
                        if (attackType == DataManager.instance.database.typeDataList[k].id)
                        {
                            if (liveMonsterList[k].type.id == DataManager.instance.database.typeDataList[k].strongType)
                            {
                                typeStr = DataManager.instance.database.typeDataList[k].strongInc;
                                break;
                            }
                        }
                    }
                    liveMonsterList[i].hp -= liveMonsterList[i].bufList[j].damage * typeStr / 100;
                    liveMonsterList[i].hp -= (liveMonsterList[i].maxHp - liveMonsterList[i].hp) * liveMonsterList[i].bufList[j].damageDeadHpPercent / 100 * typeStr / 100;
                }
                liveMonsterList[i].bufList[j].turn--;
                if (liveMonsterList[i].bufList[j].turn == 0)
                {
                    liveMonsterList[i].str -= liveMonsterList[i].bufList[j].str;
                    liveMonsterList[i].def -= liveMonsterList[i].bufList[j].def;
                    delete.Add(liveMonsterList[i].bufList[j]);
                }
            }
            for (int j = 0; j < delete.Count; j++)
            {
                liveMonsterList[i].bufList.Remove(delete[j]);
            }
        }
        DetectDeadMonster();
        nextAttack = 100;
        nextAttackType = -1;
        PrintCM();
        for (int i = 0; i < currentCardList.Count; i++)
        {
            deadCardList.Add(currentCardList[i]);
        }
        currentCardList.Clear();
        for (int i = 0; i < currentCardGameObjectList.Count; i++)
        {
            Destroy(currentCardGameObjectList[i]);
        }
        currentCardGameObjectList.Clear();
        endGame.SetActive(false);
        StartMonster();
    }

    private void StartMonster()
    {
        GameObject turnTmp = Instantiate(turnPrefab);
        turnTmp.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Monster 턴";
        turnTmp.transform.SetParent(canvas.transform);
        turnTmp.transform.localScale = new Vector3(1, 1, 1);
        turnTmp.transform.localPosition = new Vector3(0, 0, 0);
        Destroy(turnTmp, 1);
        // 몬스터 행동
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            List<Pattern> patternList = new List<Pattern>();
            for (int j = 0; j < liveMonsterList[i].patternList.Count; j++)
            {
                if (liveMonsterList[i].patternList[j].order == liveMonsterList[i].cycle)
                {
                    patternList.Add(liveMonsterList[i].patternList[j]);
                }
            }
            int random = Random.Range(0, patternList.Count);
            int exe = patternList[random].id;
            switch (exe)
            {
                case 0:
                    Pattern0(liveMonsterList[i]);
                    break;
                case 1:
                    Pattern1(liveMonsterList[i]);
                    break;
                case 2:
                    Pattern2(liveMonsterList[i]);
                    break;
                case 3:
                    Pattern3(liveMonsterList[i]);
                    break;
                case 4:
                    Pattern4(liveMonsterList[i]);
                    break;
                case 5:
                    Pattern5(liveMonsterList[i]);
                    break;
                case 6:
                    Pattern6(liveMonsterList[i]);
                    break;
                case 7:
                    Pattern7(liveMonsterList[i]);
                    break;
                case 8:
                    Pattern8(liveMonsterList[i]);
                    break;
                case 9:
                    Pattern9(liveMonsterList[i]);
                    break;
                case 10:
                    Pattern10(liveMonsterList[i]);
                    break;
                case 11:
                    Pattern11(liveMonsterList[i]);
                    break;
                case 12:
                    Pattern12(liveMonsterList[i]);
                    break;
                case 13:
                    Pattern13(liveMonsterList[i]);
                    break;
                case 14:
                    Pattern14(liveMonsterList[i]);
                    break;
                case 15:
                    Pattern15(liveMonsterList[i]);
                    break;
                case 16:
                    Pattern16(liveMonsterList[i]);
                    break;
                case 17:
                    Pattern17(liveMonsterList[i]);
                    break;
                case 18:
                    Pattern18(liveMonsterList[i]);
                    break;
                case 19:
                    Pattern19(liveMonsterList[i]);
                    break;
                case 20:
                    Pattern20(liveMonsterList[i]);
                    break;
                case 21:
                    Pattern21(liveMonsterList[i]);
                    break;
                case 22:
                    Pattern22(liveMonsterList[i]);
                    break;
                case 23:
                    Pattern23(liveMonsterList[i]);
                    break;
                case 24:
                    Pattern24(liveMonsterList[i]);
                    break;
            }
            int maxOrder = liveMonsterList[i].patternList[0].order;
            for (int j = 0; j < liveMonsterList[i].patternList.Count; j++)
            {
                if (liveMonsterList[i].patternList[j].order > maxOrder)
                {
                    maxOrder = liveMonsterList[i].patternList[j].order;
                }
            }
            liveMonsterList[i].cycle = (liveMonsterList[i].cycle + 1) % (maxOrder + 1);
        }
        Invoke("EndMonsterTurn", 1f);
    }

    private void EndMonsterTurn()
    {
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            List<Buf> delete = new List<Buf>();
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                int attackType = liveCharacterList[i].bufList[j].damageType;
                if (attackType != -1)
                {
                    int typeStr = 100;
                    for (int k = 0; k < DataManager.instance.database.typeDataList.Count; k++)
                    {
                        if (attackType == DataManager.instance.database.typeDataList[k].id)
                        {
                            if (liveCharacterList[k].type.id == DataManager.instance.database.typeDataList[i].strongType)
                            {
                                typeStr = DataManager.instance.database.typeDataList[k].strongInc;
                                break;
                            }
                        }
                    }
                    liveCharacterList[i].hp -= liveCharacterList[i].bufList[j].damage * typeStr / 100;
                    liveCharacterList[i].hp -= (liveCharacterList[i].maxHp - liveCharacterList[i].hp) * liveCharacterList[i].bufList[j].damageDeadHpPercent / 100 * typeStr / 100;
                }
                liveCharacterList[i].bufList[j].turn--;
                if (liveCharacterList[i].bufList[j].turn == 0)
                {
                    liveCharacterList[i].str -= liveCharacterList[i].bufList[j].str;
                    liveCharacterList[i].def -= liveCharacterList[i].bufList[j].def;
                    delete.Add(liveCharacterList[i].bufList[j]);
                }
            }
            for (int j = 0; j < delete.Count; j++)
            {
                liveCharacterList[i].bufList.Remove(delete[j]);
            }
        }
        DetectDeadCharacter();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            List<Buf> delete = new List<Buf>();
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                int attackType = liveMonsterList[i].bufList[j].damageType;
                if (attackType != -1)
                {
                    int typeStr = 100;
                    for (int k = 0; k < DataManager.instance.database.typeDataList.Count; k++)
                    {
                        if (attackType == DataManager.instance.database.typeDataList[k].id)
                        {
                            if (liveMonsterList[k].type.id == DataManager.instance.database.typeDataList[k].strongType)
                            {
                                typeStr = DataManager.instance.database.typeDataList[k].strongInc;
                                break;
                            }
                        }
                    }
                    liveMonsterList[i].hp -= liveMonsterList[i].bufList[j].damage * typeStr / 100;
                    liveMonsterList[i].hp -= (liveMonsterList[i].maxHp - liveMonsterList[i].hp) * liveMonsterList[i].bufList[j].damageDeadHpPercent / 100 * typeStr / 100;
                }   
                liveMonsterList[i].bufList[j].turn--;
                if (liveMonsterList[i].bufList[j].turn == 0)
                {
                    liveMonsterList[i].str -= liveMonsterList[i].bufList[j].str;
                    liveMonsterList[i].def -= liveMonsterList[i].bufList[j].def;
                    delete.Add(liveMonsterList[i].bufList[j]);
                }
            }
            for (int j = 0; j < delete.Count; j++)
            {
                liveMonsterList[i].bufList.Remove(delete[j]);
            }
        }
        DetectDeadMonster();
        nextAttack = 100;
        nextAttackType = -1;
        PrintCM();
        StartPlayer();
    }

    private void EndGame()
    {
        if (liveMonsterList.Count == 0)
        {
            DataManager.instance.database.playerData.exp += deadMonsterList.Count * 1000 + 100 * level;
            while (DataManager.instance.database.playerData.exp >= 1000 + 100 * DataManager.instance.database.playerData.level)
            {
                DataManager.instance.database.playerData.exp -= 1000 + 100 * DataManager.instance.database.playerData.level;
                DataManager.instance.database.playerData.level += 1;
            }
            DataManager.instance.JsonSave();
            StageManager.instance.sub++;
            StageManager.instance.isChange = true;
            SceneManager.LoadScene("Stage " + stage);
        }
        if (liveCharacterList.Count == 0)
        {
            DataManager.instance.database.playerData.exp += deadMonsterList.Count * 1000 + 50 * level;
            while (DataManager.instance.database.playerData.exp >= 1000 + 100 * DataManager.instance.database.playerData.level)
            {
                DataManager.instance.database.playerData.exp -= 1000 + 100 * DataManager.instance.database.playerData.level;
                DataManager.instance.database.playerData.level += 1;
            }
            DataManager.instance.JsonSave();
            StageManager.instance.isChange = true;
            SceneManager.LoadScene("Stage " + stage);
        }
    }

    // 카드 효과
    public void Skill0(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 0;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] /100 - liveMonsterList[choose].def / 2;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill1()
    {
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            liveCharacterList[i].bufList.Add(new Buf(2, true, false, -1, 0, 0, 0, 0));
        }
    }

    public void Skill2(int characterId)
    {
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                liveCharacterList[i].bufList.Add(new Buf(2, false, true, -1, 0, 0, 0, 0));
                break;
            }
        }
    }

    public void Skill3()
    {
        nextAttack = 150;
        nextAttackType = 0;
    }

    public void Skill4(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 0;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill5(int characterId)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 1;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }
        for (int choose = 0; choose < liveMonsterList.Count; choose++)
        {
            int typeStr = 100;
            for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
            {
                if (attackType == DataManager.instance.database.typeDataList[i].id)
                {
                    if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                    {
                        typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                        break;
                    }
                }
            }

            int str = liveCharacterList[index].str;
            int attack = str * 60 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
            if (attack > 0)
            {
                liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
            }
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill6(int characterId)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 1;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }
        for (int choose = 0; choose < liveMonsterList.Count; choose++)
        {
            int typeStr = 100;
            for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
            {
                if (attackType == DataManager.instance.database.typeDataList[i].id)
                {
                    if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                    {
                        typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                        break;
                    }
                }
            }
            int attack = liveMonsterList[choose].hp * 30 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
            if (attack > 0)
            {
                liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
            }    
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill7(int choose)
    {
        liveMonsterList[choose].bufList.Add(new Buf(-1, false, false, 1, 10, 0, 0, 0));
    }

    public void Skill8()
    {
        for (int i = 0; i < field.Length; i++)
        {
            field[i] = 100;
        }
        field[1] = 125;
        field[2] = 75;
    }

    public void Skill9(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 1;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }
    public void Skill10()
    {
        int minIndex = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (liveCharacterList[minIndex].hp > liveCharacterList[i].hp)
            {
                minIndex = i;
            }
        }
        liveCharacterList[minIndex].hp = liveCharacterList[minIndex].hp + liveCharacterList[minIndex].hp * 30 / 100;
        DetectOverHp();
        PrintCM();
    }

    public void Skill11()
    {
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            int str = liveCharacterList[i].str * 50 / 100;
            liveCharacterList[i].bufList.Add(new Buf(1, false, false, -1, 0, 0, str, 0));
            liveCharacterList[i].str += str;
        }
    }

    public void Skill12()
    {
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            liveCharacterList[i].hp = liveCharacterList[i].hp + (liveCharacterList[i].maxHp - liveCharacterList[i].hp) * 10 / 100;
        }
        DetectOverHp();
        PrintCM();
    }

    public void Skill13()
    {
        for (int i = 0; i < field.Length; i++)
        {
            field[i] = 100;
        }
        field[2] = 125;
        field[3] = 75;
    }

    public void Skill14(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 2;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill15(int characterId)
    {
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                int def = liveCharacterList[i].def * 50 / 100;
                liveCharacterList[i].bufList.Add(new Buf(4, false, true, -1, 0, 0, 0, def));
                liveCharacterList[i].def += def;
                break;
            }
        }
    }

    public void Skill16(int characterId)
    {
        int minIndex = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (liveCharacterList[minIndex].hp > liveCharacterList[i].hp)
            {
                minIndex = i;
            }
        }
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                int def = liveCharacterList[i].def * 10 / 100;
                liveCharacterList[minIndex].bufList.Add(new Buf(4, false, false, -1, 0, 0, 0, def));
                liveCharacterList[minIndex].def += def;
                liveCharacterList[minIndex].hp += (liveCharacterList[minIndex].maxHp - liveCharacterList[minIndex].hp) * 10 / 100;
                break;
            }
        }
        DetectOverHp();
        PrintCM();
    }

    public void Skill17()
    {
        int minIndex = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (liveCharacterList[minIndex].hp > liveCharacterList[i].hp)
            {
                minIndex = i;
            }
        }
        int str = liveCharacterList[minIndex].str * 50 / 100 * (-1);
        int def = liveCharacterList[minIndex].def * 100 / 100;
        liveCharacterList[minIndex].bufList.Add(new Buf(2, false, false, -1, 0, 0, str, def));
        liveCharacterList[minIndex].str += str;
        liveCharacterList[minIndex].def += def;
    }

    public void Skill18(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 3;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
            liveCharacterList[index].hp += attack * 50 / 100;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectOverHp();
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill19(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 3;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }
    public void Skill20(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 4;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill21(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 4;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 300 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill22(int characterId)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 4;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        int minIndex = 0;
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            if (liveMonsterList[minIndex].hp > liveMonsterList[i].hp)
            {
                minIndex = i;
            }
        }
        int choose = minIndex;
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 200 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if(attack > 0 )
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill23()
    {
        nextAttack = 150;
    }

    public void Skill24(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 4;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill25(int choose)
    {
        liveMonsterList[choose].bufList.Add(new Buf(-1, false, false, 5, 0, 10, 0, 0));
    }

    public void Skill26(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 5;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 200 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill27()
    {
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            liveCharacterList[i].hp = liveCharacterList[i].maxHp;
        }

        DetectOverHp();
        PrintCM();
    }

    public void Skill28(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 5;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str * (currentCardList.Count + 1);
        for (int i = 0; i < currentCardGameObjectList.Count; i++)
        {
            Destroy(currentCardGameObjectList[i]);
        }
        currentCardGameObjectList.Clear();
        currentCardList.Clear();
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill29(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 5;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }
    public void Skill30(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 6;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 200 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill31(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 0;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int str = liveMonsterList[index].str * deadCardList.Count;
        int attack = str * 50 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    public void Skill32()
    {
        for (int i = 0; i < currentCardList.Count; i++)
        {
            deadCardList.Add(currentCardList[i]);
        }
        currentCardList.Clear();
        for (int i = 0; i < currentCardGameObjectList.Count; i++)
        {
            Destroy(currentCardGameObjectList[i]);
        }
        currentCardGameObjectList.Clear();
        if (liveCardList.Count < 5)
        {
            for (int i = 0; i < deadCardList.Count; i++)
            {
                liveCardList.Add(deadCardList[i]);
            }
            deadCardList.Clear();
            for (int i = 0; i < liveCardList.Count; i++)
            {
                int random1 = Random.Range(0, liveCardList.Count);
                int random2 = Random.Range(0, liveCardList.Count);
                Card tmp = liveCardList[random1];
                liveCardList[random1] = liveCardList[random2];
                liveCardList[random2] = tmp;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            currentCardList.Add(liveCardList[0]);
            liveCardList.RemoveAt(0);
        }
        for (int i = 0; i < currentCardList.Count; i++)
        {
            GameObject tmp = Instantiate(cardPrefab);
            tmp.GetComponent<CardPrefab>().id = currentCardList[i].id;
            tmp.GetComponent<CardPrefab>().characterId = currentCardList[i].characterId;
            tmp.GetComponent<Image>().sprite = LoadImage("./Data/" + currentCardList[i].image + "/Character.png");
            tmp.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "코스트: " + currentCardList[i].cost.ToString();
            tmp.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text += "\n" + currentCardList[i].description;
            tmp.transform.SetParent(fightUI.transform);
            tmp.transform.localScale = new Vector3(1, 1, 1);
            tmp.transform.localPosition = new Vector3(-500 + i * 180, -250, 0);
            currentCardGameObjectList.Add(tmp);
        }
    }

    public void Skill33(int choose)
    {
        int minIndex = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (liveCharacterList[minIndex].hp > liveCharacterList[i].hp)
            {
                minIndex = i;
            }
        }

        int tmp = liveMonsterList[choose].hp;
        liveMonsterList[choose].hp = liveCharacterList[minIndex].hp;
        liveMonsterList[choose].hp = tmp;
        DetectOverHp();
        PrintCM();
    }

    public void Skill34(int characterId, int choose)
    {
        int index = 0;
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            if (characterId == liveCharacterList[i].id)
            {
                index = i;
                break;
            }
        }

        int attackType = 6;
        if (nextAttackType != -1)
        {
            attackType = nextAttackType;
        }

        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveMonsterList.Count; i++)
        {
            for (int j = 0; j < liveMonsterList[i].bufList.Count; j++)
            {
                if (liveMonsterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveMonsterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = liveCharacterList[index].str;
        int attack = str * 100 / 100 * typeStr / 100 * nextAttack / 100 * field[attackType] / 100 - liveMonsterList[choose].def;
        if (attack > 0)
        {
            liveMonsterList[choose].hp = liveMonsterList[choose].hp - attack;
        }
        nextAttackType = -1;
        nextAttack = 100;
        DetectDeadMonster();
        PrintCM();
    }

    // 패턴 효과
    private void Pattern0(Monster m)
    {
        int attackType = 1;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 100 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern1(Monster m)
    {
        m.bufList.Add(new Buf(2, false, true, -1, 0, 0, 0, 0));
    }

    private void Pattern2(Monster m)
    {
        int attackType = 1;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 150 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern3(Monster m)
    {
        int attackType = 2;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 100 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern4(Monster m)
    {
        int attackType = 2;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 150 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern5(Monster m)
    {
        int attackType = 3;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 100 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern6(Monster m)
    {
        int attackType = 3;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 150 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern7(Monster m)
    {
        int attackType = 4;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 100 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern8(Monster m)
    {
        int attackType = 4;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 150 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern9(Monster m)
    {
        int attackType = 5;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 100 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern10(Monster m)
    {
        int attackType = 5;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 150 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern11(Monster m)
    {
        int attackType = 6;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 100 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern12(Monster m)
    {
        int attackType = 6;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 150 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern13(Monster m)
    {
        int attackType = 7;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 100 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern14(Monster m)
    {
        int attackType = 7;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 150 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern15(Monster m)
    {
        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        liveCharacterList[choose].bufList.Add(new Buf(2, false, false, 1, 10, 0, 0, 0));
    }

    private void Pattern16(Monster m)
    {
        m.hp += (m.maxHp - m.hp) * 10 / 100;
        DetectOverHp();
    }

    private void Pattern17(Monster m)
    {
        int def = m.def * 50 / 100;
        m.bufList.Add(new Buf(2, false, false, -1, 0, 0, 0, def));
        m.def += def;
    }

    private void Pattern18(Monster m)
    {
        int attackType = 3;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 150 / 100 * typeStr / 100 * field[attackType] / 100 - liveCharacterList[choose].def;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
            m.hp += attack * 50 / 100;
        }
        DetectDeadCharacter();
        DetectOverHp();
        PrintCM();
    }

    private void Pattern19(Monster m)
    {
        int attackType = 4;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 150 / 100 * typeStr / 100 * field[attackType] / 100;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern20(Monster m)
    {
        int str = m.str * 50 / 100;
        m.bufList.Add(new Buf(3, false, false, -1, 0, 0, str, 0));
        m.str += str;
    }

    private void Pattern21(Monster m)
    {
        int attackType = 5;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str;
        int attack = str * 200 / 100 * typeStr / 100 * field[attackType] / 100;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern22(Monster m)
    {
        int attackType = 6;

        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        int typeStr = 100;
        for (int i = 0; i < DataManager.instance.database.typeDataList.Count; i++)
        {
            if (attackType == DataManager.instance.database.typeDataList[i].id)
            {
                if (liveCharacterList[choose].type.id == DataManager.instance.database.typeDataList[i].strongType)
                {
                    typeStr = DataManager.instance.database.typeDataList[i].strongInc;
                    break;
                }
            }
        }

        int str = m.str * liveCardList.Count;
        int attack = str * 30 / 100 * typeStr / 100 * field[attackType] / 100;
        if (attack > 0)
        {
            liveCharacterList[choose].hp = liveCharacterList[choose].hp - attack;
        }
        DetectDeadCharacter();
        PrintCM();
    }

    private void Pattern23(Monster m)
    {
        int choose = Random.Range(0, liveCharacterList.Count);
        List<int> targetIndex = new List<int>();
        for (int i = 0; i < liveCharacterList.Count; i++)
        {
            for (int j = 0; j < liveCharacterList[i].bufList.Count; j++)
            {
                if (liveCharacterList[i].bufList[j].isTarget == true)
                {
                    targetIndex.Add(i);
                }
            }
        }
        if (targetIndex.Count > 0)
        {
            choose = Random.Range(0, targetIndex.Count);
        }

        liveCharacterList[choose].bufList.Add(new Buf(2, false, false, 7, 10, 0, 0, 0));
    }

    private void Pattern24(Monster m)
    {
        int def = m.def * 100 / 100;
        m.bufList.Add(new Buf(2, false, false, -1, 0, 0, 0, def));
        m.def += def;
    }
}
