using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    public GameObject clearUI;
    public GameObject failUI;
    public GameObject text;
    public GameObject hText;
    public GameObject pText;
    public GameObject inputUI;
    timeController tc;
    GameObject residue;
    changeScene cs;
    int nDestroy;

    void Start()
    {
        clearUI.SetActive(false);
        failUI.SetActive(false);
        tc = GetComponent<timeController>();
        text.GetComponent<Text>().text = PlayerPrefs.GetString("b");
        residue = GameObject.FindGameObjectWithTag("residue");
        nDestroy = 0;

        soundManager.sm.playBGM();

//        PlayerPrefs.SetInt("bestTime",100);
//        PlayerPrefs.SetInt("bestScore",0);        
    }

    void Update()
    {
        transform.Find("pStatus").Find("lName").GetComponent<Text>().text=PlayerPrefs.GetString("name");

        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
//        if(trees.Length == 0) {
        if(trees.Length < 5) {

            if(clearUI.activeSelf == false) {
                clear();
            }
        }
        residue.GetComponent<Text>().text = trees.Length.ToString();
        
        if(Input.GetButtonDown("Cancel")){
            menu();
        }
    }
    public async void addDestroy() {
        nDestroy++;
        Debug.Log(nDestroy);
    }
    public async void clear() {
        clearUI.SetActive(true);
        tc.isCount = false;
        soundManager.sm.playSE(se.Clear);

        int time = int.Parse(transform.Find("timer").Find("timerText").GetComponent<Text>().text);     
        int score = nDestroy * 10 + (100 -time) * 10;
        string date = DateTime.Now.ToString("MM/dd HH:mm");
        clearUI.transform.Find("Image").Find("name").gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("name");
        clearUI.transform.Find("Image").Find("date").gameObject.GetComponent<Text>().text = date;
        clearUI.transform.Find("Image").Find("time").gameObject.GetComponent<Text>().text = time.ToString(); 
        clearUI.transform.Find("Image").Find("score").gameObject.GetComponent<Text>().text = score.ToString(); 
        int bestTime = PlayerPrefs.GetInt("bestTime");
        int bestScore = PlayerPrefs.GetInt("bestScore");
        string bestTimeDate = PlayerPrefs.GetString("bestTimeDate");
        string bestScoreDate = PlayerPrefs.GetString("bestScoreDate");
        if(time < bestTime) {
            bestTime = time;
            bestTimeDate = date;
            PlayerPrefs.SetInt("bestTime",bestTime);
            PlayerPrefs.SetString("bestTimeDate",date);            
        }
        if(score > bestScore) {
            bestScore = score;
            bestScoreDate = date;
            PlayerPrefs.SetInt("bestScore",bestScore);
            PlayerPrefs.SetString("bestScoreDate",date);            
        }
        clearUI.transform.Find("Image").Find("bestTime").gameObject.GetComponent<Text>().text = bestTime.ToString(); 
        clearUI.transform.Find("Image").Find("bestScore").gameObject.GetComponent<Text>().text = bestScore.ToString(); 
        clearUI.transform.Find("Image").Find("bestTimeDate").gameObject.GetComponent<Text>().text = bestTimeDate.ToString(); 
        clearUI.transform.Find("Image").Find("bestScoreDate").gameObject.GetComponent<Text>().text = bestScoreDate.ToString(); 
    }

    public void fail() {
        failUI.SetActive(true);
    }

    void menu() {
        cs = GetComponent<changeScene>();
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
