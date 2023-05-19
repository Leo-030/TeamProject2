using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TutorialPlayManager : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI text;
    public GameObject fisrtImage;
    public GameObject secondImage;
    public GameObject thirdImage;
    public GameObject firstAudio;
    public GameObject secondAudio;
    private int next = -1;

    // Update is called once per frame
    void Update()
    {
        switch (next)
        {
            case -1:
                Print("불의 신 니켈", "어이 데미우르스 안녕!\n요새 통 안보이더니 뭐하고 지내..으악!");
                break;
            case 0:
                Print("???", "하하하 나의 공격에 이렇게 쉽게 당하다니 역시 신들은 별것도 아니구만\n어? 그 옆에도 벌레가 있구나! 잘가라!");
                break;
            case 1:
                Print("불의 신 니켈", "으아아악!");
                break;
            case 2:
                Print("데미우르스(나)", "왜... 왜...! 너가 나 대신 맞은거야!");
                break;
            case 3:
                Print("불의 신 니켈", "어서... 어서... 도망가... 으윽");
                break;
            case 4:
                Print("데미우르스(나)", "어떻게 널 두고 가! 너도 같이 도망갈 수 있어!");
                break;
            case 5:
                Print("불의 신 니켈", "너도 알잖아.. 이미 끝난 것을... 너라도 어서 도망가");
                break;
            case 6:
                Print("???", "아주 드라마를 쓰는구나! 그 옆의 벌레도 잡아주마!");
                break;
            case 7:
                Print("불의 신 니켈", "어서 도망가!");
                break;
            case 8:
                ChangeObject(fisrtImage, secondImage);
                Print("", "그렇게 불의 신 니켈의 희생으로 도망칠 수 있었지만...\n" +
                    "지배당한 신계에 있을 수 없어 인간계로 떨어질 수 밖에 없었다.");
                break;
            case 9:
                ChangeObject(secondImage, thirdImage);
                ChangeObject(firstAudio, secondAudio);
                Print("???", "드디어 찾았다.");
                break;
            case 10:
                Print("데미우르스(나)", "누구??");
                break;
            case 11:
                Print("???", "나야 나. 지혜의 신 지루");
                break;
            case 12:
                Print("데미우르스(나)", "넌 어떻게 살아남았지?\n" +
                    "난 그녀 덕분에...");
                break;
            case 13:
                Print("지혜의 신 지루", "어떻게 된 일인지 궁금하기도 하고 내가 어떻게 살아남았는지 궁금하기도 하겠지. 설명해줄게! 이제부터 긴 이야기가 될거야.");
                break;
            case 14:
                Print("지혜의 신 지루", "어떤 악마가 무리를 이끌고 우리 신계에 처들어왔어. 그 악마는 신을 공격하여 쓰려뜨렸고 그 신을 조종하기 시작했지. 아마 정신 지배인 것 같아.");
                break;
            case 15:
                Print("지혜의 신 지루", "악마는 그 정신 지배의 힘으로 점차 신들을 지배해나아갔어. 그리고 신계를 도망쳐나온 우리 외에는 아마 모두...");
                break;
            case 16:
                Print("지혜의 신 지루", "물론 난 지혜롭게 미리 빠져나왔어! 이래봬도 지혜의 신이라구! 에헴!");
                break;
            case 17:
                Print("데미우르스(나)", "그럼 이제 끝난 것인가...");
                break;
            case 18:
                Print("지혜의 신 지루", "아니지 아니지. 너가 도망쳐 나온 덕분에 방법이 있다구!");
                break;
            case 19:
                Print("데미우르스(나)", "어떻게...?");
                break;
            case 20:
                Print("지혜의 신 지루", "넌 전략의 신이잖아! 너의 전략의 힘으로 우리를 구원해줄 수 있을거야!");
                break;
            case 21:
                Print("지혜의 신 지루", "너의 힘은... 아니지 더 자세한 건 나중에 설명해줄게! 지금은 저기 찾아온 악마의 하수인들과 한번 싸워보자! 그럼 깨닫게 될거야!");
                break;
            default:
                SceneManager.LoadScene("TutorialFight");
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
