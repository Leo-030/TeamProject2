using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Stage7StoryManager : MonoBehaviour
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
                Print("�Ǹ� �� ������", "���� ��� �׷��� ���� ����� �Ǹ��� �ƴϿ���. �׻� �̹ڹ޴� ��ġ�� �Ǹ�����.");
                break;
            case 0:
                Print("�Ǹ� �� ������", "�׷��� ����ߴ�. �Ű�� ����ġ�ڰ�.");
                break;
            case 1:
                Print("�Ǹ� �� ������", "������!!!! �Ű赵 ó���� ���̿���! �ھַο� �ŵ��� ���� �Ŷ�� ������� �޸� �����ڵ� õ������");
                break;
            case 2:
                Print("�Ǹ� �� ������", "�׷��� ����߾�. ������ �Ƹ���� �����ڰ�. ���Բ� ����ڰ�.");
                break;
            case 3:
                Print("�Ǹ� �� ������", "�׷��� ���� �����. �׸��� �Ǹ����� ��������. ���� ���� ���� �Ű���̴�.");
                break;
            case 4:
                Print("�Ǹ� �� ������", "���� ��ΰ� ���� ������ ���� ���̴�!!!");
                break;
            case 5:
                Print("���̿츣��(��)", "... ���� ������ �˰ھ�.. ������... ������ �ٸ� ����� �����ž�..");
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
