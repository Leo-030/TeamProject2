using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage1StoryManager : MonoBehaviour
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
                Print("불의 신 니켈", "안녕 데미우르스...");
                break;
            case 0:
                Print("데미우르스(나)", "무슨 일이야. 왜 이렇게 기운이 없어?");
                break;
            case 1:
                Print("불의 신 니켈", "나는 불의 신에 자격이 없는 걸까?");
                break;
            case 2:
                Print("데미우르스(나)", "왜 그래.. 불의 신하면 너 말고는 떠오르지 않는걸?");
                break;
            case 3:
                Print("불의 신 니켈", "정말? 그치만... 난 이렇게 소심한데... 다들 불의 신이라면 조금 더 활발해야 한다고...");
                break;
            case 4:
                Print("데미우르스(나)", "그렇지 만은 않은 걸? 불도 고요한걸. 불멍이라고 들어봤어?");
                break;
            case 5:
                Print("불의 신 니켈", "불멍?");
                break;
            case 6:
                Print("데미우르스(나)", "응. 불을 보면서 멍떄리는 것을 불멍이라고 해. 불이 시끄러웠으면 불멍은 불가능한 걸?");
                break;
            case 7:
                Print("불의 신 니켈", "그... 그런가?");
                break;
            case 8:
                Print("데미우르스(나)", "응. 기운 내! 너는 누가 뭐라해도 불의 신이야!");
                break;
            case 9:
                Print("불의 신 니켈", "푸훗. 알겠어. 기운낼게!");
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
