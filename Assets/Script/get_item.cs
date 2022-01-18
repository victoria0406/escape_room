using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class get_item : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public GameObject canvas;

    public GameObject content;
    public Text dist_text;

    public GameObject bag;

    private RaycastHit hit;
    private GameObject panel;

    public GameObject evid_list;
    public GameObject detect_list;

    float rotSpeed = 10f;

    string main_ment="";
    string sub_ment="";

    // 각 증거 찾았는지 확인하는 변수
    public bool is_find_smartphone = false;
    public bool is_find_books = false;
    public bool is_find_hair = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("hihi");
        // bag에 대한 것
        if (bag != null)
        {
            float dist_bag = Vector3.Distance(bag.transform.position, cam.transform.position);
            dist_text.text = dist_bag.ToString();
            if (dist_bag<0.7)
            {
                shake_bag();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {

            Debug.Log("check1");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("check1");
                GameObject obj = hit.collider.gameObject;
                string objectName = obj.name;
                   
                    
                float dist = Vector3.Distance(obj.transform.position, cam.transform.position);

                Debug.Log(objectName);
                //hit.collider.gameObject.SetActive(false);
                Debug.Log("check2");

                //각각 아이템별 멘트
                if (objectName == "Glasses_wrap")
                {
                    main_ment = "안경을 획득했습니다";
                    sub_ment = "드디어 안경을 찾았다. 안경을 쓰면 조금 더 잘 보이겠다.";
                    content.transform.Find("glass_panel").gameObject.SetActive(true);
                    evid_list.transform.Find("Glasses_wrap").gameObject.SetActive(false);
                }
                else
                {
                    if (EventSystem.current.IsPointerOverGameObject()) //이거 카메라가 달라서 안먹으니까 고쳐
                    {
                        Debug.Log("block");
                        return;
                    }
                    if (objectName == "jang's_book")
                    {
                        main_ment = "장병규님의 저서를 발견했다.";
                        sub_ment = "여기 뭐가 묻은거 같은데? 자세히 보자";
                        content.transform.Find("jang's_book_panel").gameObject.SetActive(true);
                        evid_list.transform.Find("jang's_book").gameObject.SetActive(false);
                        is_find_hair = true;
                    }
                    else if (objectName == "node")
                    {
                        main_ment = "3반의 생일 메모를 발견했다.";
                        sub_ment = "3반의 생일에 대한 메모네? 왜 존재하는거지?";
                        content.transform.Find("birthsheet_panel").gameObject.SetActive(true);
                        detect_list.transform.Find("node").gameObject.SetActive(false);
                    }
                    else if (objectName == "lockh")
                    {
                        // 비밀번호 띄우기
                        canvas.transform.Find("password").gameObject.SetActive(true);
                        detect_list.transform.Find("lockh").gameObject.SetActive(false);
                    }
                    else if (objectName == "kaist")
                    {
                        // 패널 띄우기
                        // canvas.transform.Find("find_diff").gameObject.SetActive(true);
                        GameObject.Find("GameManager").SendMessage("find_kyu_book");
                        detect_list.transform.Find("kaist").gameObject.SetActive(false);
                    }
                }

                canvas.transform.Find("main_ment").gameObject.SetActive(true);
                canvas.transform.Find("Script").gameObject.SetActive(true);
                canvas.transform.Find("main_ment").GetComponent<TextMeshProUGUI>().text = main_ment;
                canvas.transform.Find("Script").GetComponent<TextMeshProUGUI>().text = sub_ment;
                StartCoroutine(delete_ment());
                if (objectName != "backpack" && objectName != "lockh")
                {
                    Destroy(obj);
                }

            }
            
        }
        
        

        
        
        
    }

    IEnumerator delete_ment()
    {
        yield return new WaitForSeconds(2.0f);
        canvas.transform.Find("main_ment").GetComponent<TextMeshProUGUI>().text = "";
        canvas.transform.Find("Script").GetComponent<TextMeshProUGUI>().text = "";
        main_ment = "";
        sub_ment = "";
        canvas.transform.Find("main_ment").gameObject.SetActive(false);
        canvas.transform.Find("Script").gameObject.SetActive(false);
        

    }

    public void shake_bag()
    {
        
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.y = -Input.acceleration.z;
        dir.z = Input.acceleration.x;
        main_ment = "가방을 발견했습니다";
        sub_ment = "가방을 흔들어보자";

        canvas.transform.Find("main_ment").gameObject.SetActive(true);
        canvas.transform.Find("Script").gameObject.SetActive(true);
        canvas.transform.Find("main_ment").GetComponent<TextMeshProUGUI>().text = main_ment;
        canvas.transform.Find("Script").GetComponent<TextMeshProUGUI>().text = sub_ment;
        StartCoroutine(delete_ment());
        if (dir.sqrMagnitude > 10)
        {
            
            //흔드는 이멘트 추가

            content.transform.Find("phone_panel").gameObject.SetActive(true); //아직 가져오면 안되지만 테스트용
            Destroy(bag);
            main_ment = "핸드폰이 떨어졌다.";
            sub_ment = "누구 폰이지? 한번 확인해보자";

            is_find_smartphone = true;
            evid_list.transform.Find("backpack").gameObject.SetActive(false);

            canvas.transform.Find("main_ment").gameObject.SetActive(true);
            canvas.transform.Find("Script").gameObject.SetActive(true);
            canvas.transform.Find("main_ment").GetComponent<TextMeshProUGUI>().text = main_ment;
            canvas.transform.Find("Script").GetComponent<TextMeshProUGUI>().text = sub_ment;
            StartCoroutine(delete_ment());

        }
        
    }
}
