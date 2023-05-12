using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage2StoryManager : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI text;
    public List<GameObject> images;
    public List<GameObject> audios;
    private int next = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (next)
        {
            case -1:
                Print("물의 신 스쿠", "안녕 데미우르스! 무슨 일이야?");
                break;
            case 0:
                Print("데미우르스(나)", "무슨 일이야? 그게 지금 할 소리야!");
                break;
            case 1:
                Print("물의 신 스쿠", "왜애~~");
                break;
            case 2:
                Print("데미우르스(나)", "하.. 회의가 있었잖아... 참여는 해달라고 했잖아...");
                break;
            case 3:
                Print("물의 신 스쿠", "그치만... 거기는 물이 없는 걸...");
                break;
            case 4:
                Print("데미우르스(나)", "그럼 물이 없으면 안 움직일거야? 아에 물에서 자지 그래?");
                break;
            case 5:
                Print("물의 신 스쿠", "헤헷 그거 좋은 생각이다! 이제부터 물에서 잔다! 그럼 행복하겠지? 헤헤...");
                break;
            case 6:
                Print("데미우르스(나)", "에휴.... 정말 물에서 자게? 신이라 문제는 없겠지만... 좀 그렇지 않아?");
                break;
            case 7:
                Print("물의 신 스쿠", "그래도 물이 좋은 걸~ 좋은 아이디어 고마워!");
                break;
            case 8:
                Print("데미우르스(나)", "고마우면 다음 회의는 참석해줘... 에휴..");
                break;
            case 9:
                Print("물의 신 스쿠", "푸훗. 알겠어 그렇게 애원하는데 참여해줄게!");
                break;
            case 10:
                Print("", "그렇게 다음 회의에도 스쿠는 나오지 않았다...");
                break;
            case 11:
                Print("데미우르스(나)", "에휴..");
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
