using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage4StoryManager : MonoBehaviour
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
                Print("", "하늘의 신 하피는 전지전능한 신들 중에서도 몇 안되게 자유롭게 날 수 있는 신이었다. ");
                break;
            case 0:
                Print("", "그래서 그만큼 눈에 띄기도 했으며 부러움을 받기도 했지만 일부 신들의 질투를 받기도 했다. ");
                break;
            case 1:
                Print("", "때로 몇몇 신들의 질투심은 심해서 하피에게 피해를 주는 경우도 있었다. 내가 하피와 알게된건 그쯤의 일이다.");
                break;
            case 2:
                Print("하늘의 신 하피", "왜... 왜... 난 날 수 있는 거야? 왜...");
                break;
            case 3:
                Print("하늘의 신 하피", "왜 나한테만 그래. 왜 다들 나한테만 그러냐고!!!");
                break;
            case 4:
                Print("데미우르스(나)", "...");
                break;
            case 5:
                Print("", "그때의 나는 바닥에 떨어져 있던 깃털을 가지고 조그만 새 모양을 만들어서 하피의 눈 앞으로 날려주었다.");
                break;
            case 6:
                Print("하늘의 신 하피", "???");
                break;
            case 7:
                Print("하늘의 신 하피", "... 풋");
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
