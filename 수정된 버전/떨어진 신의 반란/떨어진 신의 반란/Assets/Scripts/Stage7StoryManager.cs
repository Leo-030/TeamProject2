using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage7StoryManager : MonoBehaviour
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
                Print("악마 진 벨리나", "나는 사실 그렇게 높은 등급의 악마는 아니였지. 항상 핍박받는 위치의 악마였지.");
                break;
            case 0:
                Print("악마 진 벨리나", "그래서 결심했다. 신계로 도망치자고.");
                break;
            case 1:
                Print("악마 진 벨리나", "하지만!!!! 신계도 처참한 곳이였어! 자애로운 신들이 있을 거라는 예상과는 달리 위선자들 천지였지");
                break;
            case 2:
                Print("악마 진 벨리나", "그래서 결심했어. 세상을 아름답게 만들자고. 다함께 웃어보자고.");
                break;
            case 3:
                Print("악마 진 벨리나", "그래서 힘을 모았지. 그리고 악마들을 지배했지. 이제 남은 곳은 신계뿐이다.");
                break;
            case 4:
                Print("악마 진 벨리나", "이제 모두가 웃을 세상이 오는 것이다!!!");
                break;
            case 5:
                Print("데미우르스(나)", "... 너의 생각은 알겠어.. 하지만... 힘말고 다른 방법이 있을거야..");
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
