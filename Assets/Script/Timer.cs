using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float time = 600.0f;
    private int minutes;
    private int seconds;
    private GameManager gameManager;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        minutes = (int)time / 60;
        seconds = (int)(time - minutes * 60);
        time -= Time.deltaTime;
        if (time >=0)
        {
            timeText.text = string.Format("{0} : {1:D2}", minutes, seconds);
        }


        else
        {
            gameManager.time_out = true;
        }
    }
}