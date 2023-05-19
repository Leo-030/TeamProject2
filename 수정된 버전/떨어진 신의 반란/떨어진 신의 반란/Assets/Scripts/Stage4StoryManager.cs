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
                Print("", "�ϴ��� �� ���Ǵ� ���������� �ŵ� �߿����� �� �ȵǰ� �����Ӱ� �� �� �ִ� ���̾���. ");
                break;
            case 0:
                Print("", "�׷��� �׸�ŭ ���� ��⵵ ������ �η����� �ޱ⵵ ������ �Ϻ� �ŵ��� ������ �ޱ⵵ �ߴ�. ");
                break;
            case 1:
                Print("", "���� ��� �ŵ��� �������� ���ؼ� ���ǿ��� ���ظ� �ִ� ��쵵 �־���. ���� ���ǿ� �˰ԵȰ� ������ ���̴�.");
                break;
            case 2:
                Print("�ϴ��� �� ����", "��... ��... �� �� �� �ִ� �ž�? ��...");
                break;
            case 3:
                Print("�ϴ��� �� ����", "�� �����׸� �׷�. �� �ٵ� �����׸� �׷��İ�!!!");
                break;
            case 4:
                Print("���̿츣��(��)", "...");
                break;
            case 5:
                Print("", "�׶��� ���� �ٴڿ� ������ �ִ� ������ ������ ���׸� �� ����� ���� ������ �� ������ �����־���.");
                break;
            case 6:
                Print("�ϴ��� �� ����", "???");
                break;
            case 7:
                Print("�ϴ��� �� ����", "... ǲ");
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
