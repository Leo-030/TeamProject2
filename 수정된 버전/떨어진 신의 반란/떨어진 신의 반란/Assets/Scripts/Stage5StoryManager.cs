using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage5StoryManager : MonoBehaviour
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
                Print("", "세이야는 화려한 빛으로 감싸져 있는 빛의 신이다.");
                break;
            case 0:
                Print("", "정세가 상당히 안정되어 있던 당시의 신계에서는 싸움 혹은 전투라고 할 만한 사건들을 거의 찾아볼 수 없었다.");
                break;
            case 1:
                Print("", "나는 그날따라 유독 바깥을 돌아다니고 싶어서 신계의 경계 외각까지도 돌아다녔다.");
                break;
            case 2:
                Print("???", "크르르.. 맛있는 신의 냄새군... 크크크");
                break;
            case 3:
                Print("데미우르스(나)", "왜 악마가 여기에! 어서... 도망가야... 으윽");
                break;
            case 4:
                Print("악마", "도망갈 수 있을 줄 알았느냐! 잘 가라!");
                break;
            case 5:
                Print("데미우르스(나)", "...");
                break;
            case 6:
                Print("악마", "으악!");
                break;
            case 7:
                ChangeObject(images[0], images[1]);
                Print("빛의 신 세이야", "악마가 왜 이런 곳에... 넌.. 괜.. 괜찮아?");
                break;
            case 8:
                Print("데미우르스(나)", "... 고마워... 덕분에 살았어.. 정말 고마워");
                break;
            case 9:
                Print("빛의 신 세이야", "뭘... 그.. 다친 곳은 꼭 치료 받고... 그럼..");
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
