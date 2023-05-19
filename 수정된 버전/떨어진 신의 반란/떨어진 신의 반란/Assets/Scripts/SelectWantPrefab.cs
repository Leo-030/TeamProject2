using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectWantPrefab : MonoBehaviour, IPointerClickHandler
{
    public int id;
    public int choose;
    public int characterId;
    private FightManager fightManager;

    // Start is called before the first frame update
    void Start()
    {
        fightManager = GameObject.Find("FightManager").GetComponent<FightManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (id)
        {
            case 0:
                fightManager.Skill0(characterId, choose);
                break;
            case 4:
                fightManager.Skill4(characterId, choose);
                break;
            case 7:
                fightManager.Skill7(choose);
                break;
            case 9:
                fightManager.Skill9(characterId, choose);
                break;
            case 14:
                fightManager.Skill14(characterId, choose);
                break;
            case 18:
                fightManager.Skill18(characterId, choose);
                break;
            case 19:
                fightManager.Skill19(characterId, choose);
                break;
            case 20:
                fightManager.Skill20(characterId, choose);
                break;
            case 21:
                fightManager.Skill21(characterId, choose);
                break;
            case 24:
                fightManager.Skill24(characterId, choose);
                break;
            case 25:
                fightManager.Skill25(choose);
                break;
            case 26:
                fightManager.Skill26(characterId, choose);
                break;
            case 28:
                fightManager.Skill28(characterId, choose);
                break;
            case 29:
                fightManager.Skill29(characterId, choose);
                break;
            case 30:
                fightManager.Skill30(characterId, choose);
                break;
            case 31:
                fightManager.Skill31(characterId, choose);
                break;
            case 33:
                fightManager.Skill33(choose);
                break;
            case 34:
                fightManager.Skill34(characterId, choose);
                break;
        }
        Destroy(this.transform.parent.gameObject);
        fightManager.PrintCurrentCard();
    }
}
