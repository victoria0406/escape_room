using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Buttonevent : MonoBehaviour
{
    // Start is called before the first frame update

    public Button b_start;
    public GameObject start_script;
    public GameObject game_panel;

    public Button bag_button;
    public GameObject bag_panel;
    private bool bag_active;

    public Button take_glass;
    bool use_glass;
    public Button[] use_item_b;
    public TextMeshProUGUI script;
    public GameObject blur;



    public Button ending;

    private GameManager gameManager;
    public GameObject canvas;
    
    void Start()
    {
        b_start.onClick.AddListener(start_game);
        bag_button.onClick.AddListener(Bag_click);
        bag_active = false;
        use_glass = false;
        take_glass.onClick.AddListener(glass_on);
        ending.onClick.AddListener(see_end);
        //GameObject.Find("GameManager").SendMessage("find_kyu_book");
        for (int i = 0; i < use_item_b.Length; i++)
        {
            int k = i;
            use_item_b[i].onClick.AddListener(delegate {
                Debug.Log("click" + k);
                GameObject.Find("GameManager").SendMessage("Seeitem", k );
            });
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void start_game()
    {
        start_script.SetActive(true);
        game_panel.SetActive(false);
    }

    void Bag_click()
    {
        Debug.Log(bag_active);
        if (bag_active)
        {
            bag_panel.SetActive(false);
            bag_active = false;
        }
        else
        {
            bag_panel.SetActive(true);
            bag_active = true;
        }
        

    }

    void use_item()
    {
        for (int i= 0; i < use_item_b.Length; i++){
            use_item_b[i].onClick.AddListener(delegate {
                GameObject.Find("GameManager").SendMessage("Seeitem",i+1);
                });
        }
    }

    void glass_on()
    {
        if (!use_glass)
        {
            using_glasses();
            script.gameObject.SetActive(true);
            script.text = "안경을 사용합니다";
            StartCoroutine(delete_ment());
        }
        else
        {
            take_off_glasses();
            script.gameObject.SetActive(true);
            script.text = "안경을 벗었습니다";
            StartCoroutine(delete_ment());
        }
        use_glass = !use_glass;
    }
    IEnumerator delete_ment()
    {
        yield return new WaitForSeconds(1.0f);
        script.gameObject.SetActive(false);

    }


    //안경 탈착
    void using_glasses()
    {
        //glasses_panel.SetActive(true);
        //glass_ob.SetActive(true);
        blur.SetActive(false);

    }
    void take_off_glasses()
    {
        //glasses_panel.SetActive(false);
        //glass_ob.SetActive(false);
        blur.SetActive(true);
    }

    void see_end()
    {
        string scr = "탈출에 성공했다/왜 병규가 나를 가둔걸까?/.../(다음날 오후)/";
        GameObject.Find("GameManager").SendMessage("read_script", scr);
    }


}
