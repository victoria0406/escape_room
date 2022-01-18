using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spining : MonoBehaviour
{
    public GameObject[] spinning_ob;
    float rotSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       foreach (GameObject ob in spinning_ob)
        {
            ob.transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
        }
        //spinning_ob.transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
}
