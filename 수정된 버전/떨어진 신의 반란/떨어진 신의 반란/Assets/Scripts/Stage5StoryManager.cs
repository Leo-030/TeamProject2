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
                Print("", "���̾ߴ� ȭ���� ������ ������ �ִ� ���� ���̴�.");
                break;
            case 0:
                Print("", "������ ����� �����Ǿ� �ִ� ����� �Ű迡���� �ο� Ȥ�� ������� �� ���� ��ǵ��� ���� ã�ƺ� �� ������.");
                break;
            case 1:
                Print("", "���� �׳����� ���� �ٱ��� ���ƴٴϰ� �; �Ű��� ��� �ܰ������� ���ƴٳ��.");
                break;
            case 2:
                Print("???", "ũ����.. ���ִ� ���� ������... ũũũ");
                break;
            case 3:
                Print("���̿츣��(��)", "�� �Ǹ��� ���⿡! �... ��������... ����");
                break;
            case 4:
                Print("�Ǹ�", "������ �� ���� �� �˾Ҵ���! �� ����!");
                break;
            case 5:
                Print("���̿츣��(��)", "...");
                break;
            case 6:
                Print("�Ǹ�", "����!");
                break;
            case 7:
                ChangeObject(images[0], images[1]);
                Print("���� �� ���̾�", "�Ǹ��� �� �̷� ����... ��.. ��.. ������?");
                break;
            case 8:
                Print("���̿츣��(��)", "... ����... ���п� ��Ҿ�.. ���� ����");
                break;
            case 9:
                Print("���� �� ���̾�", "��... ��.. ��ģ ���� �� ġ�� �ް�... �׷�..");
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
