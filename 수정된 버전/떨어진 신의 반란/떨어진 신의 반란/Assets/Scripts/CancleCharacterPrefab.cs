using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CancleCharacterPrefab : MonoBehaviour, IPointerClickHandler
{
    public int id;
    private FightManager fightManager;

    // Start is called before the first frame update
    void Start()
    {
        fightManager = GameObject.Find("FightManager").GetComponent<FightManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        fightManager.CancleParty(id);
    }
}
