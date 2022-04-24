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
    public GameObject residue;
    changeScene cs;
    int nDestroy;
    public int nCoin,nPortion;
    public Animator aportion;
    bool isGetPortion=false;
    GameObject player, grid;

    void Start()
    {
        clearUI.SetActive(false);
        failUI.SetActive(false);
        tc = GetComponent<timeController>();
//        text.GetComponent<Text>().text = PlayerPrefs.GetString("b");
//        residue = GameObject.FindGameObjectWithTag("residue");
        nDestroy = 0;

        soundManager.sm.playBGM();

        player = GameObject.FindGameObjectWithTag("Player");
        grid = GameObject.FindGameObjectWithTag("grid");

        //aportion = transform.Find("inputUI").Find("bPortion").gameObject.GetComponent<Animator>();

        //debug—p
        //        PlayerPrefs.SetInt("bestTime",100);
        //        PlayerPrefs.SetInt("bestScore",0);        

        loadSavedata();
    }

    void loadSavedata()
    {
        transform.Find("myName").gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("name");
        transform.Find("pStatus").Find("lName").GetComponent<Text>().text = PlayerPrefs.GetString("name");
        nPortion = PlayerPrefs.GetInt("nPortion");
        nCoin = PlayerPrefs.GetInt("nCoin");
    }

    void savedata()
    {
        PlayerPrefs.SetInt("nPortion",nPortion);
        PlayerPrefs.SetInt("nCoin",nCoin);
    }

    void Update()
    {
        Debug.Log(grid.transform.Find("Tilemap") + " " + grid.transform.Find("Tilemap").GetComponent<Renderer>().sortingOrder);
//        Debug.Log("sortingLayer:" + GetComponent<SortingLayer>());
        Debug.Log(" sortingOrder" + player.GetComponent<Renderer>().sortingOrder);
        Debug.Log(" Renderer:" + player.GetComponent<Renderer>() + " SRender:" + player.GetComponent<SpriteRenderer>());
        if(Time.time > 4.0f)
        {
            grid.transform.Find("Tilemap2").GetComponent<Renderer>().sortingOrder = 2;
        }
        aportion.SetBool("isGetPortion", true);
        //Debug.Log(transform.Find("inputUI").Find("bPortion"));



        Animator a = transform.Find("inputUI").Find("bCoin").GetComponent<Animator>();
        a.SetBool("isGetCoin", true);


        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        if(trees.Length == 0) {
//        if(trees.Length < 5) { //for Debug (clear stage)

            if(clearUI.activeSelf == false) {
                clear();
            }
        }
//debug        residue.GetComponent<Text>().text = trees.Length.ToString();
        residue.GetComponent<Text>().text = (nCoin*100).ToString();

        if (Input.GetButtonDown("Cancel")){
            menu();
        }
    }
    public void addDestroy() {
        nDestroy++;
    }
    public void addCoin() {
        nCoin++;
        transform.Find("inputUI").Find("bCoin").Find("Text").GetComponent<Text>().text = nCoin.ToString();

        Debug.Log(transform.Find("inputUI").Find("bCoin").GetComponent<Animator>());
    }
    public void addPortion()
    {
        nPortion++;
        transform.Find("inputUI").Find("bPortion").Find("Text").GetComponent<Text>().text = nPortion.ToString();

    }
    public void clear() {
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

        savedata();
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
    public void portion()
    {
        Slider pSlider = transform.Find("pStatus").Find("pSlider").GetComponent<Slider>();
        nPortion -= 1;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        pController pc = p.GetComponent<pController>();
        pc.usePortion(10);

    }

}
