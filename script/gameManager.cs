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

        soundManager.sm.playBGM();
    }

    void Update()
    {
        transform.Find("pStatus").Find("lName").GetComponent<Text>().text=PlayerPrefs.GetString("name");

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
        soundManager.sm.playSE(se.Clear);
        PlayerPrefs.SetString("b","b");
    }

    void menu() {
        cs = GetComponent<changeScene>();
        cs.Load2("selectMap");
    }

}
