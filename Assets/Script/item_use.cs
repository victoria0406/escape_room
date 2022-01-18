using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class item_use : MonoBehaviour
{
    public Button[] item_button;
    private Button use_item_button;
    public TextMeshProUGUI script;
    public GameObject item_panel;

    //�������� ���� ������ bool�� ǥ���� ��
    bool glass_on;
    bool mag_on;

    //public GameObject glasses_panel;
    public GameObject glass_ob;

    public GameObject blur;

    public GameObject script_panel;

    public Camera scenecam;



    // Start is called before the first frame update
    void Start()
    {
        foreach (Button b_item in item_button){
            b_item.onClick.AddListener(delegate { Use_item(b_item); });
        }
        glass_on = false;
        mag_on = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Use_item(Button butt)
    {
        //Debug.Log("item:"+item_what_i_use+item_on);
        string temp_item = butt.transform.GetChild(1).name;
        Debug.Log(temp_item);
        // �Ȱ濡 ���ؼ�
        if (temp_item == "item_glasses")
        {
            if (!glass_on)
            {
                using_glasses();
                script.gameObject.SetActive(true);
                script.text = "�Ȱ��� ����մϴ�";
                StartCoroutine(delete_ment());
            }
            else
            {
                take_off_glasses();
                script.gameObject.SetActive(true);
                script.text = "�Ȱ��� �������ϴ�";
                StartCoroutine(delete_ment());
            }
            glass_on = !glass_on;
            
        }

                    //item_panel.transform.Find("glasses").gameObject.SetActive(false);
        

        //Debug.Log(use_item_button.transform.GetChild(0).name+" �� ����մϴ�."); //�� setActive(false)�Ǿ� �־ getchild �� ����� �������� �� �� �ִ�.
    }
    
    IEnumerator delete_ment()
    {
        yield return new WaitForSeconds(1.0f);
        script.gameObject.SetActive(false);
        script.text = "";

    }


    //�Ȱ� Ż��
    void using_glasses()
    {
        //glasses_panel.SetActive(true);
        glass_ob.SetActive(true);
        blur.SetActive(false);

    }
    void take_off_glasses()
    {
        //glasses_panel.SetActive(false);
        glass_ob.SetActive(false);
        blur.SetActive(true);
    }

}
