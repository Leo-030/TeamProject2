using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public GameObject openUI;

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
        if (PartyManager.instance.party.Count == PartyManager.numMembers)
        {
            for (int i = 0; i < PartyManager.instance.party.Count; i++)
            {
                CharacterData c = new CharacterData();
                c.id = PartyManager.instance.party[i].id;
                c.name = PartyManager.instance.party[i].name;
                c.image = PartyManager.instance.party[i].image;
                c.hp = PartyManager.instance.party[i].hp;
                c.str = PartyManager.instance.party[i].str;
                PartyManager.instance.currentParty.Add(c);
            }
            SceneManager.LoadScene("Play");
        }
        else
        {
            GameObject tmp = Instantiate(openUI);
            tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + PartyManager.numMembers + "명을 선택해주세요!";
            tmp.transform.SetParent(GameObject.Find("Party UI").transform);
            tmp.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
