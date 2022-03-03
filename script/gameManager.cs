using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    public GameObject clearUI;
    public GameObject sliderUI;
    public GameObject text;
    public GameObject hText;
    public GameObject pText;
    public GameObject inputUI;
    timeController tc;
    GameObject residue;
    changeScene cs;

    void Start()
    {
        clearUI.SetActive(false);
        sliderUI.SetActive(true);
        tc = GetComponent<timeController>();
        text.GetComponent<Text>().text = PlayerPrefs.GetString("b");
        residue = GameObject.FindGameObjectWithTag("residue");
    }

    void Update()
    {

        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        if(trees.Length == 0) {
            clear();
        }
        residue.GetComponent<Text>().text = trees.Length.ToString();
        
        if(Input.GetButtonDown("Cancel")){
            menu();
        }
    }
    public void clear() {
        clearUI.SetActive(true);
        sliderUI.SetActive(false);
        tc.isCount = false;
        PlayerPrefs.SetString("b","b");
    }

    void menu() {
        cs = GetComponent<changeScene>();
        Debug.Log(cs);
        cs.Load2("selectMap");
    }

    public void fire1(){
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        fire2Controller f2c = p.GetComponent<fire2Controller>();
        f2c.fire1();
    }
    public void fire2(){
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        fire2Controller f2c = p.GetComponent<fire2Controller>();
        f2c.fire2();
    }
    public void fire3(){
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        fire2Controller f2c = p.GetComponent<fire2Controller>();
        f2c.fire3();
    }        
}
