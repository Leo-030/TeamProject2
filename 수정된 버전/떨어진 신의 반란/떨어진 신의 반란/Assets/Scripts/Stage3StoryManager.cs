using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage3StoryManager : MonoBehaviour
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
                Print("�ڿ��� �� ����", "�ȳ� ���̿츣��!");
                break;
            case 0:
                Print("���̿츣��(��)", "�ȳ�. ���� ���̾�? �׻� �� �༮ �ܿ��� �Ű浵 �Ⱦ�����.");
                break;
            case 1:
                Print("�ڿ��� �� ����", "������... �װ�.. Ȥ�� ���� ����� ���ϰ� �־�?");
                break;
            case 2:
                Print("���̿츣��(��)", "������ ���� �� �༮�� ���� ���̿�����");
                break;
            case 3:
                Print("�ڿ��� �� ����", "��ġ��...");
                break;
            case 4:
                Print("���̿츣��(��)", "�˰ھ� �����ٰ�. �� �ڿ� �ִ� ������ �ٷ��� ����?");
                break;
            case 5:
                Print("�ڿ��� �� ����", "... �׷��� ��!");
                break;
            case 6:
                Print("���̿츣��(��)", "������... ����� ���� ��~�� �������� å �а� �־�. �� ����");
                break;
            case 7:
                Print("�ڿ��� �� ����", "����... ����...");
                break;
            case 8:
                Print("���̿츣��(��)", "������ �ߵǸ� ���� ����?");
                break;
            case 9:
                Print("�ڿ��� �� ����", "�˰ھ�... ��¶�� ����!");
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
