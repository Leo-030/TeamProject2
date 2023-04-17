using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealButton : MonoBehaviour
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
        for (int i = 0; i < PartyManager.instance.currentParty.Count; i++)
        {
            PartyManager.instance.currentParty[i].hp += 2;
            if (PartyManager.instance.currentParty[i].hp > PartyManager.instance.party[i].hp)
            {
                PartyManager.instance.currentParty[i].hp = PartyManager.instance.party[i].hp;
            }
        }

        GameObject tmp = Instantiate(openUI);
        tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "모두 2씩 회복했습니다.";
        tmp.transform.SetParent(GameObject.Find("Heal UI").transform);
        tmp.transform.localPosition = new Vector3(0, 0, 0);
    }
}
