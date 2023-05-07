using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectCharacterPrefab : MonoBehaviour, IPointerClickHandler
{
    public int id;
    private FightManager fightManager;

    // Start is called before the first frame update
    void Start()
    {
        fightManager = GameObject.Find("FightManager").GetComponent<FightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        fightManager.SelectParty(id);
    }
}
