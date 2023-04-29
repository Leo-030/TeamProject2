using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public GameObject wrapper;
    public GameObject characterPrefab;
    public GameObject characterDataPrefab;
    public GameObject stagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Charcter UI 안의 character 채워 넣기
        GameObject canvas = GameObject.Find("Canvas");
        GameObject characterUI = canvas.transform.GetChild(3).gameObject;
        GameObject characterScroll = characterUI.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        int col = 4;
        int width = 315;
        for (int i = 0; i < DataManager.instance.database.playerData.characterList.Count; i++)
        {
            if (i % col == 0)
            {
                GameObject wrapperTmp = Instantiate(wrapper);
                wrapperTmp.transform.SetParent(characterScroll.transform);
                wrapperTmp.transform.localScale = new Vector3(1, 1, 1);
            }

            GameObject characterTmp = Instantiate(characterPrefab);
            characterTmp.GetComponent<CharcterPrefab>().currentUI = characterUI;
            TextMeshProUGUI t = characterTmp.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            characterTmp.transform.SetParent(characterScroll.transform.GetChild(i / col));
            characterTmp.transform.localScale = new Vector3(1, 1, 1);
            characterTmp.transform.localPosition = new Vector3(-475 + width * (i % col) , 0, 0);

            GameObject uiTmp = Instantiate(characterDataPrefab);
            uiTmp.SetActive(false);
            uiTmp.transform.SetParent(canvas.transform);
            uiTmp.transform.localScale = new Vector3(1, 1, 1);
            uiTmp.transform.localPosition = new Vector3(0, 0, 0);
            uiTmp.transform.GetChild(0).GetChild(4).gameObject.GetComponent<UIChanger>().currentUI = uiTmp;
            uiTmp.transform.GetChild(0).GetChild(4).gameObject.GetComponent<UIChanger>().nextUI = characterUI;
            characterTmp.GetComponent<CharcterPrefab>().nextUI = uiTmp;

            int characterId = DataManager.instance.database.playerData.characterList[i];
            int typeId = -1;
            List<int> skillList = new List<int>();
            foreach (CharacterData c in DataManager.instance.database.characterDataList)
            {
                if (characterId == c.id)
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
                    characterTmp.GetComponent<Image>().sprite = LoadImage("./Data/" + c.image + "/Character.png");
                    uiTmp.GetComponent<Image>().sprite = LoadImage("./Data/" + c.image + "/BackGround.png");
                    uiTmp.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = c.name;
                    typeId = c.type;
                    uiTmp.transform.GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = c.description;
                    skillList = c.skillList;
                    break;
                }
            }

            foreach (TypeData type in DataManager.instance.database.typeDataList)
            {
                if(typeId == type.id)
                {
                    uiTmp.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = type.name;
                    break;
                }
            }

            foreach (int skillId in skillList)
            {
                foreach (SkillData skill in DataManager.instance.database.skillDataList)
                {
                    if (skillId == skill.id)
                    {
                        uiTmp.transform.GetChild(0).GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text += "\n코스트: " + skill.cost + " - " + skill.description;
                        break;
                    }
                }
            }

        }

        // Stage UI 안의 stage 활성화
        GameObject stageUI = canvas.transform.GetChild(5).gameObject;
        GameObject stageScroll = stageUI.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        for (int i = 0; i < DataManager.instance.database.playerData.stageClear + 1; i++)
        {
            if (i >= 7)
            {
                break;
            }
            stageScroll.transform.GetChild(i).gameObject.SetActive(true);
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
}
