using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage6StoryManager : MonoBehaviour
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
                Print("", "어둠의 신 벨라는 수많은 신들이 있는 신계에서도 유독 눈에 띄는 신이었다. 그리고 그 이유는 그다지 좋은 이유는 아니었다.");
                break;
            case 0:
                Print("", "화사하고 따뜻한 분위기의 신계에서 어둠을 관장하는 신은 그다지 좋은 시선을 받지 못했다.");
                break;
            case 1:
                Print("", "그리고 빛을 상징하는 신계와 대적하는 악마가 어둠을 뜻하니 좋을리가 없었다.");
                break;
            case 2:
                Print("", "그럼에도 불구하고 벨라 본인 자체는 꽤 낙관적인 성격의 소유자였다.");
                break;
            case 3:
                Print("", "다른 신들의 태도가 그리 달갑지 않음에도 본인은 항상 주변에게 친절 했으며 누구에게나 도움의 손길을 건네주었다.");
                break;
            case 4:
                Print("", "나는 그런 벨라의 모습을 보면서 다시 한번 생각했던것 같다");
                break;
            case 5:
                Print("", "빛이 존재하려면 어둠 또한 존재해야함을");
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
