using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tController : MonoBehaviour
{
    public bool isCount = true;
    public float max_time = 180;
    public float display;
    float time = 0;
    bool isTimeover = false;

    void Start()
    {
        display = max_time;
    }

    void Update()
    {
        if (isCount)
        {
            if (isTimeover == false)
            {
                time += Time.deltaTime;
                display = max_time - time;
                if (display <= 0)
                {
                    display = 0;
                    isTimeover = true;
                }
            }
//            Debug.Log(time + " " + display);
        }
    }
}
