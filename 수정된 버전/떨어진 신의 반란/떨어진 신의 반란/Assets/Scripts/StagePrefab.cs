using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StagePrefab : MonoBehaviour, IPointerClickHandler
{
    public int index;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Stage " + index);
    }
}
