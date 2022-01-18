using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassWordManager : MonoBehaviour
{
    public TextMeshProUGUI[] btn_pw;
    public Button btn_submit;
    public TMP_InputField txt_pw;
    string pre_password = "";
    string password;
    private int textLimit = 4;
    public Button btn_out;

    private GameManager gameManager;
    public GameObject canvas;
    public TMP_InputField pw_input;
    public TextMeshProUGUI[] pw;

    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;



    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        txt_pw.characterLimit = textLimit;
        txt_pw.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        btn_submit.onClick.AddListener(delegate { CheckPassword(); });
        btn_out.onClick.AddListener(OutPW);
    }

    void Update()
    {

    }

    public void ValueChangeCheck()
    {

        password = txt_pw.text;

        if (pre_password.Length < password.Length)
        {
            for (int i = 0; i < textLimit; ++i)
            {
                if (i == password.Length - 1)
                {
                    btn_pw[i].text = password[i].ToString();
                    StartCoroutine(Fade(0, 1, btn_pw[i]));
                }
            }
        }
        else
        {
            for (int i = 0; i < textLimit; ++i)
            {
                if (i == password.Length)
                {
                    btn_pw[i].text = "";
                }
            }
        }



        pre_password = password;
    }

    public void OutPW()
    {
        canvas.transform.Find("password").gameObject.SetActive(false);
    }

    public void CheckPassword()
    {
        if (password == "1201")
        {
            gameManager.game_over = true;
            OutPW();
        }
        else
        {
            Debug.Log("오답입니다");
            pw_input.text = "";
            for(int i = 0; i < 4; i++)
            {
                pw[i].text = "";
            }
        }
    }

    public IEnumerator Fade(float start, float end, TextMeshProUGUI txt)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {

            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = txt.color;
            color.a = Mathf.Lerp(start, end, percent);
            txt.color = color;
            yield return null;
        }
    }

}
