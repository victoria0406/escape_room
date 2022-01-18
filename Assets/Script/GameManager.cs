using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject start; //��¥ ���� ȭ��
    public Button game_start_B;
    public bool game_over = false;
    public bool time_out = false;
    public GameObject start_script; //���۰� ������ ���� ȭ�� ��� ġ�� �뵵
    public Button next; //���� ��� �ѱ�°�
    string[] script_list;
    int turn = 0;

    private get_item getItem;
    
    public GameObject game_room;


    public GameObject item_detail_panel; //���� ������ �ڼ��� ���� �뵵
    public Button item_close_b;

    public GameObject find_kyu_book_panel; //���� å Ʋ�� �׸� ã��
    public Button kyu_book_button;
    public Button not_kyu_book_button;
    public GameObject kyu_evid_in_list;

    public GameObject canvas;

    public GameObject password; //��й�ȣ ġ�� �뵵


    // type effect �ֱ� ���� ����
    private float delay = 0.05f;
    private string full_text;
    private string current_text = "";
    //public GameObject ending;//Game over, Clear �����ϴ� �뵵

    public GameObject kaist;
    public GameObject lockh;
    public Camera cam;
    private RaycastHit hit;

    void Start()
    {
        getItem = GameObject.Find("AR Session").GetComponent<get_item>();

        start.SetActive(true);
        game_start_B.onClick.AddListener(game_start);
        next.onClick.AddListener(next_script);

    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                Destroy(obj);
                string objectName = obj.name;

                if(objectName == "lockh")
                {
                    // ��й�ȣ ����
                    canvas.transform.Find("password").gameObject.SetActive(true);
                }
                else if(objectName == "kaist")
                {
                    // �г� ����
                    // canvas.transform.Find("find_diff").gameObject.SetActive(true);
                    find_kyu_book();
                }
            }

            
        }*/
        if (time_out)
        {
            canvas.transform.Find("Time_out").gameObject.SetActive(true);
        }
        else if (game_over)
        {
            canvas.transform.Find("Good_end").gameObject.SetActive(true);
        }

    }
    void game_start()
    {
        string scr = "��? ���� �����?/�ͼ��� ����ε�?/�ơ� �츮 �й��̱���, �ٵ� �� �� ȥ�� ����?/����� �ϳ��� �ȳ�/�� �Ȱ��� �������?/�Ȱ���� ã�ƺ���/";
        read_script(scr);
    }

    //��ũ��Ʈ �κ�
    //��ũ��Ʈ�� ���� ���� �Ѱܼ� �о�� 
    public void read_script(string script)
    {
        turn = 0;
        script_list = script.Split('/');
        Debug.Log(script_list.Length);
        start_script.SetActive(true);
        
        full_text = script_list[turn];
        StartCoroutine(show_text());
        turn++;
    }
    void next_script()
    {
        full_text = script_list[turn];
        StartCoroutine(show_text());
        Debug.Log("turn"+turn);
        turn++;
        if (turn == script_list.Length)
        {
            final_script();
        }
    }

    void final_script()
    {
        start_script.SetActive(false);
        game_room.SetActive(true);
    }
    //��ũ��Ʈ �κ�

    //������ ���� �뵵 (�����ۿ� ���� ������ ������ ���� �����ۿ� �̸� �����صΰ�, �ش� ��ư �����ɷ� 

    public void Seeitem(int num)
    {
        Debug.Log("seeitem"+num);
        item_detail_panel.SetActive(true);
        item_close_b.onClick.AddListener(delegate { close_item_panel(num); });
        item_detail_panel.transform.GetChild(num).gameObject.SetActive(true);
        //if(num == 0)
        //{
        //    is_find_smartphone = true;
        //}
        //else if(num == 1)
        //{
        //    is_find_birthday = true;
        //}
    }
    void close_item_panel(int num)
    {
        item_detail_panel.transform.GetChild(num).gameObject.SetActive(false);
        item_detail_panel.SetActive(false);
    }

    //���� å ã�� �̺�Ʈ
    public void find_kyu_book()
    {
        find_kyu_book_panel.SetActive(true);
        kyu_book_button.onClick.AddListener(find_book);
        not_kyu_book_button.onClick.AddListener(not_find_book);
        getItem.is_find_books = true;
        // �гο� �ܼ��� ���� ���� �߰��ؾ���
    }

    IEnumerator found_kyu_book()
    {
        yield return new WaitForSeconds(2.0f);
        find_kyu_book_panel.SetActive(false);
    }
    void find_book()
    {
        kyu_book_button.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        string main_ment = "�ܼ��� ã�ҽ��ϴ�: ������ å�� �����";
        string sub_ment = "�������� �ִ� å�� �������? ���� �������";
        canvas.transform.Find("main_ment").gameObject.SetActive(true);
        canvas.transform.Find("Script").gameObject.SetActive(true);
        canvas.transform.Find("main_ment").GetComponent<TextMeshProUGUI>().text = main_ment;
        canvas.transform.Find("Script").GetComponent<TextMeshProUGUI>().text = sub_ment;
        kyu_evid_in_list.SetActive(true);
        StartCoroutine(delete_ment());
        StartCoroutine(found_kyu_book());

    }

    void not_find_book()
    {
        string sub_ment = "���Ⱑ �ƴѰ���";
        canvas.transform.Find("Script").gameObject.SetActive(true);
        canvas.transform.Find("Script").GetComponent<TextMeshProUGUI>().text = sub_ment;
        StartCoroutine(delete_ment());
    }

    IEnumerator delete_ment()
    {
        yield return new WaitForSeconds(1.0f);
        canvas.transform.Find("main_ment").gameObject.SetActive(false);
        canvas.transform.Find("Script").gameObject.SetActive(false);

    }

    IEnumerator show_text()
    {
        for(int i = 0; i <= full_text.Length; ++i)
        {
            current_text = full_text.Substring(0, i);
            start_script.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = current_text;
            yield return new WaitForSeconds(delay);
        }
    }
}
