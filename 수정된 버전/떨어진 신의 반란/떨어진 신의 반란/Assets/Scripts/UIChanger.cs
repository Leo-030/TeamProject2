using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChanger : MonoBehaviour
{
    public GameObject currentUI;
    public GameObject nextUI;
    
    public void ChangeUI()
    {
        currentUI.SetActive(false);
        nextUI.SetActive(true);
    }
}
