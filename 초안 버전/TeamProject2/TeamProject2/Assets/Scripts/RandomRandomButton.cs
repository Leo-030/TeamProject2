using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomRandomButton : MonoBehaviour
{
    public GameObject pickUpUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        if (DataManager.instance.playerData.gold < 100)
        {
            return;
        }
        DataManager.instance.playerData.gold -= 100;
        int pickUpIndex = Random.Range(0, DataManager.instance.dataBase.characterDataList.Count);
        CharacterData pickUpCharacter = DataManager.instance.dataBase.characterDataList[pickUpIndex];
        bool has = false;
        for (int i = 0; i < DataManager.instance.playerData.hasCharacterId.Count; i++)
        {
            if (pickUpCharacter.id == DataManager.instance.playerData.hasCharacterId[i])
            {
                has = true;
                break;
            }
        }
        if (!has)
        {
            DataManager.instance.playerData.hasCharacterId.Add(pickUpCharacter.id);
            DataManager.instance.playerData.hasCharacterId.Sort();
        }
        DataManager.instance.JsonSave();
        string imgPath = Path.Combine("./Data/", "Image/0.png");
        string text = "²Î";
        if (!has)
        {
            imgPath = Path.Combine("./Data/", pickUpCharacter.image);
            text = pickUpCharacter.name;
        }        
        GameObject tmp = Instantiate(pickUpUI);
        tmp.transform.GetChild(1).GetComponent<Image>().sprite = LoadImage(imgPath);
        tmp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = text;
        tmp.transform.SetParent(this.transform.parent.transform.parent);
        tmp.transform.localPosition = new Vector3(0, 0, 0);
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
