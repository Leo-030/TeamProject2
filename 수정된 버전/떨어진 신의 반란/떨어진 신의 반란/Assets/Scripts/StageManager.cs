using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageManager : MonoBehaviour
{
    public int stage;
    public int sub = 1;
    public bool isChange = false;
    public static StageManager instance;
    public GameObject wrapper;
    public GameObject subStagePrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject stagerUI = canvas.transform.GetChild(0).gameObject;
        GameObject stageScroll = stagerUI.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        int col = 5;
        int width = 250;
        for (int i = 0; i < sub; i++)
        {
            if(i >= 10)
            {
                break;
            }
            if (i % col == 0)
            {
                GameObject wrapperTmp = Instantiate(wrapper);
                wrapperTmp.transform.SetParent(stageScroll.transform);
                wrapperTmp.transform.localScale = new Vector3(1, 1, 1);
            }

            GameObject subStageTmp = Instantiate(subStagePrefab);
            subStageTmp.GetComponent<SubStagePrefab>().stage = stage;
            subStageTmp.GetComponent<SubStagePrefab>().sub = i + 1;
            TextMeshProUGUI t = subStageTmp.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            subStageTmp.transform.SetParent(stageScroll.transform.GetChild(i / col));
            subStageTmp.transform.localScale = new Vector3(1, 1, 1);
            subStageTmp.transform.localPosition = new Vector3(-500 + width * (i % col), 0, 0);
            if (i == sub - 1)
            {
                t.text = (i + 1).ToString();
            }
            else
            {
                subStageTmp.GetComponent<Button>().interactable = false;
                t.text = "Clear";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange && SceneManager.GetActiveScene().name.Equals("Stage " + stage))
        {
            Start();
            isChange = false;
        }
    }
}
