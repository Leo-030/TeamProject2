using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterPrefab : MonoBehaviour, IPointerClickHandler
{
    public GameObject currentUI;
    public GameObject nextUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        currentUI.SetActive(false);
        nextUI.SetActive(true);
    }
}
