using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start_script : MonoBehaviour
{
    public Text script;
    public Button next;

    int turn = 0;
    
    // Start is called before the first frame update

    string script1 = "��? ���� �����?/�ͼ��� ����ε�?/��? �츮 ���̱����� �ٵ� �� ��ȥ�� ����?/����� �ϳ��� �ȳ�/�� �Ȱ��� ����־�?/�Ȱ��� ã�ƺ���/";

    string script2 = "�����... ���ƿԴ�.../�¾�, ���� �츮���� ���ڸ��� �߾���.../�׶� ���� ��� �����... �������°� ������.../�ٸ� �ܼ��� �ʿ��ϰھ�/";



    string[] script_list;
    void Start()
    {
        next.onClick.AddListener(next_script);
        script_list = script1.Split('/');
        script.text = script_list[0];
        turn++;
    }
    // Update is called once per frame

    void next_script()
    {
        script.text = script_list[turn];
        turn++;
        if (turn == script_list.Length)
        {
            final_script();
        }
    }

    void final_script()
    {
        this.gameObject.SetActive(false);
    }

    public void Start_memory()
    {
        turn = 0;
        next.onClick.AddListener(next_script);
        script_list = script2.Split('/');
        script.text = script_list[0];
        turn++;
    }
}
