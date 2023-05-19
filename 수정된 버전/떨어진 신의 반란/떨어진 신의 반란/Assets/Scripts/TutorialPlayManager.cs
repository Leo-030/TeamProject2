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
                Print("���� �� ����", "���� ���̿츣�� �ȳ�!\n��� �� �Ⱥ��̴��� ���ϰ� ����..����!");
                break;
            case 0:
                Print("???", "������ ���� ���ݿ� �̷��� ���� ���ϴٴ� ���� �ŵ��� ���͵� �ƴϱ���\n��? �� ������ ������ �ֱ���! �߰���!");
                break;
            case 1:
                Print("���� �� ����", "���ƾƾ�!");
                break;
            case 2:
                Print("���̿츣��(��)", "��... ��...! �ʰ� �� ��� �����ž�!");
                break;
            case 3:
                Print("���� �� ����", "�... �... ������... ����");
                break;
            case 4:
                Print("���̿츣��(��)", "��� �� �ΰ� ��! �ʵ� ���� ������ �� �־�!");
                break;
            case 5:
                Print("���� �� ����", "�ʵ� ���ݾ�.. �̹� ���� ����... �ʶ� � ������");
                break;
            case 6:
                Print("???", "���� ��󸶸� ���±���! �� ���� ������ ����ָ�!");
                break;
            case 7:
                Print("���� �� ����", "� ������!");
                break;
            case 8:
                ChangeObject(fisrtImage, secondImage);
                Print("", "�׷��� ���� �� ������ ������� ����ĥ �� �־�����...\n" +
                    "������� �Ű迡 ���� �� ���� �ΰ���� ������ �� �ۿ� ������.");
                break;
            case 9:
                ChangeObject(secondImage, thirdImage);
                ChangeObject(firstAudio, secondAudio);
                Print("???", "���� ã�Ҵ�.");
                break;
            case 10:
                Print("���̿츣��(��)", "����??");
                break;
            case 11:
                Print("???", "���� ��. ������ �� ����");
                break;
            case 12:
                Print("���̿츣��(��)", "�� ��� ��Ƴ�����?\n" +
                    "�� �׳� ���п�...");
                break;
            case 13:
                Print("������ �� ����", "��� �� ������ �ñ��ϱ⵵ �ϰ� ���� ��� ��Ƴ��Ҵ��� �ñ��ϱ⵵ �ϰ���. �������ٰ�! �������� �� �̾߱Ⱑ �ɰž�.");
                break;
            case 14:
                Print("������ �� ����", "� �Ǹ��� ������ �̲��� �츮 �Ű迡 ó���Ծ�. �� �Ǹ��� ���� �����Ͽ� �����߷Ȱ� �� ���� �����ϱ� ��������. �Ƹ� ���� ������ �� ����.");
                break;
            case 15:
                Print("������ �� ����", "�Ǹ��� �� ���� ������ ������ ���� �ŵ��� �����س��ư���. �׸��� �Ű踦 �����ĳ��� �츮 �ܿ��� �Ƹ� ���...");
                break;
            case 16:
                Print("������ �� ����", "���� �� �����Ӱ� �̸� �������Ծ�! �̷��ĵ� ������ ���̶�! ����!");
                break;
            case 17:
                Print("���̿츣��(��)", "�׷� ���� ���� ���ΰ�...");
                break;
            case 18:
                Print("������ �� ����", "�ƴ��� �ƴ���. �ʰ� ������ ���� ���п� ����� �ִٱ�!");
                break;
            case 19:
                Print("���̿츣��(��)", "���...?");
                break;
            case 20:
                Print("������ �� ����", "�� ������ �����ݾ�! ���� ������ ������ �츮�� �������� �� �����ž�!");
                break;
            case 21:
                Print("������ �� ����", "���� ����... �ƴ��� �� �ڼ��� �� ���߿� �������ٰ�! ������ ���� ã�ƿ� �Ǹ��� �ϼ��ε�� �ѹ� �ο�����! �׷� ���ݰ� �ɰž�!");
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
