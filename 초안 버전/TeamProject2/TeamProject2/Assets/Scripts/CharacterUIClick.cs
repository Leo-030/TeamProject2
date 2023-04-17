using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterUIClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject openUI;
    public CharacterData data;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(GameObject.Find("State(Clone)"));

        string imgPath = Path.Combine("./Data/", data.image);
        string name = data.name;
        int hp = data.hp;
        int str = data.str;
        GameObject tmp = Instantiate(openUI);
        tmp.transform.GetChild(0).GetComponent<Image>().sprite = LoadImage(imgPath);
        tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
        tmp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Hp : " + hp;
        tmp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Str : " + str;
        tmp.transform.GetChild(4).GetComponent<EquipButton>().data = data;
        tmp.transform.SetParent(GameObject.Find("StateWrapper").transform);
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
