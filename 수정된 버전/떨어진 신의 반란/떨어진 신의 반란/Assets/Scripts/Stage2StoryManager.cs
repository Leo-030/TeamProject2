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
                Print("���� �� ����", "�ȳ� ���̿츣��! ���� ���̾�?");
                break;
            case 0:
                Print("���̿츣��(��)", "���� ���̾�? �װ� ���� �� �Ҹ���!");
                break;
            case 1:
                Print("���� �� ����", "�־�~~");
                break;
            case 2:
                Print("���̿츣��(��)", "��.. ȸ�ǰ� �־��ݾ�... ������ �ش޶�� ���ݾ�...");
                break;
            case 3:
                Print("���� �� ����", "��ġ��... �ű�� ���� ���� ��...");
                break;
            case 4:
                Print("���̿츣��(��)", "�׷� ���� ������ �� �����ϰž�? �ƿ� ������ ���� �׷�?");
                break;
            case 5:
                Print("���� �� ����", "���� �װ� ���� �����̴�! �������� ������ �ܴ�! �׷� �ູ�ϰ���? ����...");
                break;
            case 6:
                Print("���̿츣��(��)", "����.... ���� ������ �ڰ�? ���̶� ������ ��������... �� �׷��� �ʾ�?");
                break;
            case 7:
                Print("���� �� ����", "�׷��� ���� ���� ��~ ���� ���̵�� ����!");
                break;
            case 8:
                Print("���̿츣��(��)", "����� ���� ȸ�Ǵ� ��������... ����..");
                break;
            case 9:
                Print("���� �� ����", "Ǫ��. �˰ھ� �׷��� �ֿ��ϴµ� �������ٰ�!");
                break;
            case 10:
                Print("", "�׷��� ���� ȸ�ǿ��� ����� ������ �ʾҴ�...");
                break;
            case 11:
                Print("���̿츣��(��)", "����..");
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
