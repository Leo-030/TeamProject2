using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage7EndStoryManager : MonoBehaviour
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
                Print("악마 진 벨리나", "패배하다니... 이제 나의 꿈은 끝인가.. 모두가 웃을 수는 없는 것인가1!!");
                break;
            case 0:
                Print("데미우르스(나)", "넌 정말 모두가 웃을 수 있는 세상을 만들고 싶었구나... 하지만 방법이 잘못됐어.");
                break;
            case 1:
                Print("데미우르스(나)", "힘은 모든 것을 해결하지 못해. 힘으로 해결해도 다른 문제가 생기지.");
                break;
            case 2:
                Print("악마 진 벨리나", "그런가... 그렇구나.. 내가 잘못된거였어....");
                break;
            case 3:
                Print("데미우르스(나)", "아니야 꼭 그렇지만은 않아. 힘을 쓰는 것도 어느정도는 필요해. 하지만 넌 정도를 몰랐을 뿐이지.");
                break;
            case 4:
                Print("데미우르스(나)", "이제부터라도 그걸 깨닫고 다 같이 행복한 세상을 만들지 않을래?");
                break;
            case 5:
                Print("악마 진 벨리나", "그렇군... 그런거였어... 알겠어. 같이 좋은 세상을 만들자.");
                break;
            case 6:
                Print("", "다 같이 행복한 세상을 위해. 모두가 좋은 세상을 위해.");
                break;
            case 7:
                Print("", "The End");
                break;
            default:
                SceneManager.LoadScene("Lobby");
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
