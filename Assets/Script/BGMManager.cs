using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BGMManager : MonoBehaviour
{
    [System.Serializable]
    public struct BgmType
    {
        public string name;
        public AudioClip audio;
    }

    // get other public variables which control bgm
    GameManager gameManager;
    get_item getItem;

    public BgmType[] BGMList;
    private AudioSource BGM;
    private string NowBGMname = "";
    private bool is_change_to_second_bgm = false;

    void Start()
    {
        getItem = GameObject.Find("AR Session").GetComponent<get_item>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        BGM = gameObject.AddComponent<AudioSource>();
        BGM.loop = true;
        if (BGMList.Length > 0) PlayBGM(BGMList[0].name);
        // need to add valueChangeevent

    }


    void Update()
    {
        if(getItem.is_find_smartphone && getItem.is_find_hair && getItem.is_find_books && !is_change_to_second_bgm)
        {
            is_change_to_second_bgm = true;
            PlayBGM("middle");
        }

        if (gameManager.game_over)
        {
            PlayBGM("ending");
        }
    }

    public void PlayBGM(string name)
    {
        Debug.Log(name);
        if (NowBGMname.Equals(name)) return;

        for (int i = 0; i < BGMList.Length; ++i)
        {
            if (BGMList[i].name.Equals(name))
            {
                BGM.clip = BGMList[i].audio;
                BGM.Play();
                NowBGMname = name;
            }
        }
    }

}
