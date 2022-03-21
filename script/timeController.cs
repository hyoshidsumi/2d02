using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeController : MonoBehaviour
{
    public bool isCount = true;
    public int itime=0;
    public bool isRegen = false;
    public GameObject timer;
    bool isTimeRegen = true;
    float time = 0;

    void Start()
    {
        timer = this.transform.Find("timer").Find("timerText").gameObject;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(isCount) { 
            time += Time.deltaTime;
            if((int)time%4 == 0) {
                if(isTimeRegen == true) {
                    isTimeRegen = false;
                    isRegen = true;
                } else {
                    isRegen = false;
                }
            } else {
                isTimeRegen = true;
            }
            itime = (int)time;
            timer.GetComponent<Text>().text = itime.ToString();            
        } else {
            PlayerPrefs.SetInt("time",itime);
        }

    }

    void UpdateScore() {
        Debug.Log(itime);
    }
}
