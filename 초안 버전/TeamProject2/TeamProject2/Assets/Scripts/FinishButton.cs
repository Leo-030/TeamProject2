using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishButton : MonoBehaviour
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
        DataManager.instance.playerData.gold += 100;
        DataManager.instance.JsonSave();
        GameObject tmp = Instantiate(openUI);
        tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "100G¸¦ È¹µæÇß½À´Ï´Ù. ´øÀü¿¡¼­ ³ª°©´Ï´Ù.";
        tmp.transform.SetParent(GameObject.Find("Finish UI").transform);
        tmp.transform.localPosition = new Vector3(0, 0, 0);
    }
}
