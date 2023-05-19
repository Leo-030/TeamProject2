using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage3StoryManager : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI text;
    public List<GameObject> images;
    public List<GameObject> audios;
    private int next = -1;

    // Update is called once per frame
    void Update()
    {
        switch (next)
        {
            case -1:
                Print("자연의 신 레미", "안녕 데미우르스!");
                break;
            case 0:
                Print("데미우르스(나)", "안녕. 무슨 일이야? 항상 그 녀석 외에는 신경도 안쓰더니.");
                break;
            case 1:
                Print("자연의 신 레미", "아하하... 그게.. 혹시 지금 지루는 뭐하고 있어?");
                break;
            case 2:
                Print("데미우르스(나)", "하하하 역시 그 녀석에 관한 일이였구나");
                break;
            case 3:
                Print("자연의 신 레미", "그치만...");
                break;
            case 4:
                Print("데미우르스(나)", "알겠어 도와줄게. 그 뒤에 있는 선물을 줄려는 거지?");
                break;
            case 5:
                Print("자연의 신 레미", "... 그래서 뭐!");
                break;
            case 6:
                Print("데미우르스(나)", "하하하... 지루는 지금 저~기 나무에서 책 읽고 있어. 얼른 가봐");
                break;
            case 7:
                Print("자연의 신 레미", "헤헤... 고마워...");
                break;
            case 8:
                Print("데미우르스(나)", "하하하 잘되면 한턱 쏘기다?");
                break;
            case 9:
                Print("자연의 신 레미", "알겠어... 어쨋든 고마워!");
                break;
            default:
                SceneManager.LoadScene("Fight");
                break;
        }
    }

    public void OnClick()
    {
        next++;
    }

    private void Print(string name, string text)
    {
        this.name.text = name;
        this.text.text = text;
    }

    private void ChangeObject(GameObject first, GameObject second)
    {
        first.SetActive(false);
        second.SetActive(true);
    }
}
