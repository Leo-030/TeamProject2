using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterPrefab : MonoBehaviour, IPointerClickHandler
{
    public GameObject currentUI;
    public GameObject nextUI;

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
        currentUI.SetActive(false);
        nextUI.SetActive(true);
    }
}
