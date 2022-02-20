using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    public GameObject clearUI;
    public GameObject sliderUI;
    public Slider slider;
    public int power_max = 100;
    timeController tc;
    fire2Controller fc2;
    int power;
    int power_fire2 = 20;

    void Start()
    {
        clearUI.SetActive(false);
        sliderUI.SetActive(true);
        tc = GetComponent<timeController>();

        GameObject pa = GameObject.FindGameObjectWithTag("Player");
        fc2 = pa.GetComponent<fire2Controller>();

        power = power_max;
        tc.isCount = true;
    }

    void Update()
    {
        if(tc.isRegen) {
            power += 40;
            if(power > power_max) {
                power = power_max;
            }
            slider.value = (float)power/(float)power_max;
        }

        if(fc2.isGetButtonDown) {
            
            if(power-power_fire2 >= 0) {
                power -= power_fire2;
                fc2.isPower = true;
            } else {
                fc2.isPower = false;
            }
            slider.value = (float)power/(float)power_max;
        }

        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        if(trees.Length == 0) {
            clear();
        }
        
    }
    void clear() {
        clearUI.SetActive(true);
        sliderUI.SetActive(false);
        tc.isCount = false;
    }
}
