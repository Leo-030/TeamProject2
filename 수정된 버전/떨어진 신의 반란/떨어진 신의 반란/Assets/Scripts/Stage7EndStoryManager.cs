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
                Print("�Ǹ� �� ������", "�й��ϴٴ�... ���� ���� ���� ���ΰ�.. ��ΰ� ���� ���� ���� ���ΰ�1!!");
                break;
            case 0:
                Print("���̿츣��(��)", "�� ���� ��ΰ� ���� �� �ִ� ������ ����� �;�����... ������ ����� �߸��ƾ�.");
                break;
            case 1:
                Print("���̿츣��(��)", "���� ��� ���� �ذ����� ����. ������ �ذ��ص� �ٸ� ������ ������.");
                break;
            case 2:
                Print("�Ǹ� �� ������", "�׷���... �׷�����.. ���� �߸��Ȱſ���....");
                break;
            case 3:
                Print("���̿츣��(��)", "�ƴϾ� �� �׷������� �ʾ�. ���� ���� �͵� ��������� �ʿ���. ������ �� ������ ������ ������.");
                break;
            case 4:
                Print("���̿츣��(��)", "�������Ͷ� �װ� ���ݰ� �� ���� �ູ�� ������ ������ ������?");
                break;
            case 5:
                Print("�Ǹ� �� ������", "�׷���... �׷��ſ���... �˰ھ�. ���� ���� ������ ������.");
                break;
            case 6:
                Print("", "�� ���� �ູ�� ������ ����. ��ΰ� ���� ������ ����.");
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
