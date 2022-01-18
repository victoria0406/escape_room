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

    string script1 = "어? 여긴 어디지?/익숙한 장소인데?/아? 우리 반이구나… 근데 왜 나혼자 있지?/기억이 하나도 안나/내 안경은 어디있어?/안경을 찾아보자/";

    string script2 = "기억이... 돌아왔다.../맞아, 어제 우리반은 술자리를 했었지.../그때 문을 잠근 사람이... 떠오르는거 같은데.../다른 단서가 필요하겠어/";



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
