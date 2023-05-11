using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage6StoryManager : MonoBehaviour
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
                Print("", "����� �� ����� ������ �ŵ��� �ִ� �Ű迡���� ���� ���� ��� ���̾���. �׸��� �� ������ �״��� ���� ������ �ƴϾ���.");
                break;
            case 0:
                Print("", "ȭ���ϰ� ������ �������� �Ű迡�� ����� �����ϴ� ���� �״��� ���� �ü��� ���� ���ߴ�.");
                break;
            case 1:
                Print("", "�׸��� ���� ��¡�ϴ� �Ű�� �����ϴ� �Ǹ��� ����� ���ϴ� �������� ������.");
                break;
            case 2:
                Print("", "�׷����� �ұ��ϰ� ���� ���� ��ü�� �� �������� ������ �����ڿ���.");
                break;
            case 3:
                Print("", "�ٸ� �ŵ��� �µ��� �׸� �ް��� �������� ������ �׻� �ֺ����� ģ�� ������ �������Գ� ������ �ձ��� �ǳ��־���.");
                break;
            case 4:
                Print("", "���� �׷� ������ ����� ���鼭 �ٽ� �ѹ� �����ߴ��� ����");
                break;
            case 5:
                Print("", "���� �����Ϸ��� ��� ���� �����ؾ�����");
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
