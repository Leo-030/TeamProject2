using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPrefab : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
    public int characterId;
    private GameObject fightUI;
    private GameObject tmp;
    private FightManager fightManager;

    // Start is called before the first frame update
    void Start()
    {
        fightUI = GameObject.Find("Fight UI");
        fightManager = GameObject.Find("FightManager").GetComponent<FightManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        bool use = fightManager.CheckCost(DataManager.instance.database.skillDataList[id].cost);
        if (use == false)
        {
            return;
        }
        Destroy(tmp);
        fightManager.DestroyCard(id);
        switch (id)
        {
            case 0:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 1:
                fightManager.Skill1();
                break;
            case 2:
                fightManager.Skill2(characterId);
                break;
            case 3:
                fightManager.Skill3();
                break;
            case 4:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 5:
                fightManager.Skill5(characterId);
                break;
            case 6:
                fightManager.Skill6(characterId);
                break;
            case 7:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 8:
                fightManager.Skill8();
                break;
            case 9:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 10:
                fightManager.Skill10();
                break;
            case 11:
                fightManager.Skill11();
                break;
            case 12:
                fightManager.Skill12();
                break;
            case 13:
                fightManager.Skill13();
                break;
            case 14:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 15:
                fightManager.Skill15(characterId);
                break;
            case 16:
                fightManager.Skill16(characterId);
                break;
            case 17:
                fightManager.Skill17();
                break;
            case 18:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 19:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 20:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 21:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 22:
                fightManager.Skill22(characterId);
                break;
            case 23:
                fightManager.Skill23();
                break;
            case 24:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 25:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 26:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 27:
                fightManager.Skill27();
                break;
            case 28:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 29:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 30:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 31:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 32:
                fightManager.Skill32();
                break;
            case 33:
                fightManager.ChooseWantMonster(characterId, id);
                break;
            case 34:
                fightManager.ChooseWantMonster(characterId, id);
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tmp = Instantiate(this.gameObject);
        tmp.transform.SetParent(fightUI.transform);
        tmp.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        tmp.transform.localPosition = new Vector3(0, 50, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(tmp);
    }
}
