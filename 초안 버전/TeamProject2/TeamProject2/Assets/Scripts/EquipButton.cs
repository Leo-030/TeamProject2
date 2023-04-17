using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
    public CharacterData data;

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
        if (PartyManager.instance.party.Count >= PartyManager.numMembers)
        {
            return;
        }
        bool isParty = false;
        for (int i = 0; i < PartyManager.instance.party.Count; i++)
        {
            if (data.id == PartyManager.instance.party[i].id)
            {
                isParty = true;
                break;
            }
        }
        if (!isParty)
        {
            PartyManager.instance.party.Add(data);
            PartyManager.instance.isChange = true;
        }
    }
}
