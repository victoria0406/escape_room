using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using TMPro;

public class PointManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ARcam;
    public GameObject[] Objs;

    public GameObject evid_list;
    internal float dist;//AR카메라랑 물체 사이 거리
    public float Range = 2;

    public Text dist_text;
    private bool now_blink = false;

    private float time = 0.0f;
    private void Awake()
    {
        foreach(GameObject obj in Objs)
        {
            obj.SetActive(false);
        }
        
        if (ARcam == null)
        {
            ARcam = GameObject.Find("AR Camera");
        }
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        Debug.Log(time);
        
        if (ARcam != null)
        {
            float min_dist = 100;
            int min_thing = 0;
            float fov = ARcam.GetComponent<Camera>().fieldOfView;
            //Debug.Log(fov);
            //dist_text.text = fov.ToString();
            foreach (GameObject Obj in Objs)
            {
                if (Obj == null)
                {
                    
                }
                else
                {
                    dist = Vector3.Distance(Obj.transform.position, ARcam.transform.position);
                    
                    if (dist < Range)
                    {
                        Obj.SetActive(true);
                        if (time > 1)
                        {
                            StartCoroutine(BlinkText(Obj.name));
                            time = 0;
                        }
                        
                        //evid_list.transform.Find(Obj.name).GetComponent<TextMeshProUGUI>().color = Color.green;
                        //print(gameObject.name +dist + "has been reached!");
                    }
                    else if (dist > Range)
                    {
                        Obj.SetActive(false);
                        evid_list.transform.Find(Obj.name).GetComponent<TextMeshProUGUI>().color = Color.white;
                    }
                    if (min_dist > dist)
                    {
                        min_dist = dist;
                    }
                }

            }
            //dist_text.text = min_dist.ToString();


        }
    }
    IEnumerator BlinkText(string name)
    {
        evid_list.transform.Find(name).GetComponent<TextMeshProUGUI>().color = Color.gray;
        yield return new WaitForSeconds(0.5f);
        evid_list.transform.Find(name).GetComponent<TextMeshProUGUI>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        now_blink = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, Range);
    }
    #endif

}