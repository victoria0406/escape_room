using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Swipe_ : MonoBehaviour
{
    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange = 50f;
    public float tapRange = 10f;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        List<int> active_thing = new List<int>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf == true)
            {
                active_thing.Add(i);
                count++;
            }
        }

        pos = new float[count];
        float distanace = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distanace * i;
        }
        if (Input.touchCount > 0)
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distanace / 2) && scroll_pos > pos[i] - (distanace / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        //int k = 0;

        for (int i = 0; i < pos.Length; ++i)
        {
            if (scroll_pos < pos[i] + (distanace / 2) && scroll_pos > pos[i] - (distanace / 2))
            {
                int k = active_thing[i];
                
                transform.GetChild(k).localScale = Vector3.Lerp(transform.GetChild(k).localScale, new Vector3(1f, 1f), 0.1f);
                for (int j = 0; j < transform.childCount; ++j)
                {
                    if (j != k)
                    {
                            transform.GetChild(j).localScale = Vector3.Lerp(transform.GetChild(j).localScale, new Vector3(0.8f, 0.8f),0.1f);
                    }
                
                }
                
            }
        }

    }


    void SwipeMenu()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if (!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    stopTouch = true;
                }
            }

        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange)
            {
                Debug.Log("tap");
            }
        }


    }
}