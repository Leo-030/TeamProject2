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
                Print("���� �� ����", "�ȳ� ���̿츣��...");
                break;
            case 0:
                Print("���̿츣��(��)", "���� ���̾�. �� �̷��� ����� ����?");
                break;
            case 1:
                Print("���� �� ����", "���� ���� �ſ� �ڰ��� ���� �ɱ�?");
                break;
            case 2:
                Print("���̿츣��(��)", "�� �׷�.. ���� ���ϸ� �� ����� �������� �ʴ°�?");
                break;
            case 3:
                Print("���� �� ����", "����? ��ġ��... �� �̷��� �ҽ��ѵ�... �ٵ� ���� ���̶�� ���� �� Ȱ���ؾ� �Ѵٰ�...");
                break;
            case 4:
                Print("���̿츣��(��)", "�׷��� ���� ���� ��? �ҵ� ����Ѱ�. �Ҹ��̶�� ���þ�?");
                break;
            case 5:
                Print("���� �� ����", "�Ҹ�?");
                break;
            case 6:
                Print("���̿츣��(��)", "��. ���� ���鼭 �ۋ����� ���� �Ҹ��̶�� ��. ���� �ò��������� �Ҹ��� �Ұ����� ��?");
                break;
            case 7:
                Print("���� �� ����", "��... �׷���?");
                break;
            case 8:
                Print("���̿츣��(��)", "��. ��� ��! �ʴ� ���� �����ص� ���� ���̾�!");
                break;
            case 9:
                Print("���� �� ����", "Ǫ��. �˰ھ�. ����!");
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
