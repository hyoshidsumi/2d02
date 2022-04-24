using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class fire2Controller : Photon.Pun.MonoBehaviourPun
{
    public float speed = 11.0f;
    public GameObject arrowPrefab, arrow2Prefab, bombPrefab;
    Slider slider;
    public bool isGetButtonDown;
    public int power_max = 100;
    public int power_fire1 = 8;
    public int power_fire2 = 40;
    public bool isPower = true;
    bool isFire = false, isFire3 = false, isFire3bomb;
    public Animator motion;
    float radius,theta,larrow=0.3f;
    float ix, iy;
    int power;
    public AudioSource seShoot;
    GameObject gc;
    timeController tc;
    pController p;
    
    void Start() {
        Vector3 pos = transform.position;
        power = power_max;
        p = GetComponent<pController>();
        gc = GameObject.FindGameObjectWithTag("GameController");
        slider = gc.transform.Find("pStatus").Find("pSlider").GetComponent<Slider>();
    }

    void Update()  {
        tc = gc.GetComponent<timeController>();
        tc.isCount = true;

        if(tc.isRegen) {
            power += 40;
            if(power > power_max) {
                power = power_max;
            }
            slider.value = (float)power/(float)power_max;
        }

        if(Input.GetButton("Fire1")) {
            fire1();
        }

        if(Input.GetButtonDown("Fire2")) {
            fire2();
        }

        if(Input.GetButtonDown("Fire3")) {
            fire3();
        }

        gc.transform.Find("pStatus").Find("power").GetComponent<Text>().text = power.ToString();
    }
    public void fire1(){
        if(!isFire) {
            isFire = true;
            motion.SetBool("isFire", true);
            soundManager.sm.playSE(se.Arrow);
            Invoke("fire1main",0.2f);
            Invoke("stopFire",0.2f);
        } else
        {
            motion.SetBool("isFire", false);
        }
    }
    public void fire2() {
        if(!isFire) {
            if(power-power_fire2 >= 0) {
                power -= power_fire2;
                isFire = true;
                motion.SetBool("isFire",true);
                soundManager.sm.playSE(se.Arrow);
                Invoke("fire2main",0.2f);
                Invoke("stopFire",0.2f);
                slider.value = (float)power/(float)power_max;
            } else {
                motion.SetBool("isFire",false);
            }
        }
    }    
    public void fire3(){
            if(isFire3bomb) {
            isFire3 = false;
            isFire3bomb = false;
            soundManager.sm.playSE(se.Bomb);
            bomb3main();
        } else {
            if(!isFire3) {
                isFire3bomb = true;
                isFire3 = true;
                soundManager.sm.playSE(se.ArrowBomb);
                Invoke("fire3main",0.2f);
            }
        }
    }
    public void fire1main(){
        if(!photonView.IsMine) return;

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        pController p = go.GetComponent<pController>();
        theta = (-p.phi+90)*Mathf.PI/180;
        ix = Mathf.Cos(theta);
        iy = Mathf.Sin(theta);

        GameObject arrow = Instantiate(arrowPrefab, go.transform.position, Quaternion.Euler(0,0,-p.phi));
        Rigidbody2D b = arrow.GetComponent<Rigidbody2D>();

        Vector3 v = new Vector3(Mathf.Cos(theta),Mathf.Sin(theta))*speed;
        b.AddForce(v, ForceMode2D.Impulse);        
            
    }
    void fire2main(){
        if(!photonView.IsMine) return;

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        pController p = go.GetComponent<pController>();
        theta = (-p.phi+90)*Mathf.PI/180;
        ix = Mathf.Cos(theta);
        iy = Mathf.Sin(theta);
        Vector3 pos = go.transform.position;
        pos.x += larrow * ix;
        pos.y += larrow * iy;
            
        for(int i=0;i<5;i++) {
            radius = -p.phi + (i-2)*120/5;
            GameObject arrow = Instantiate(arrowPrefab, pos, Quaternion.Euler(0,0,radius));
            Rigidbody2D b = arrow.GetComponent<Rigidbody2D>();
            theta = (-p.phi+90 + (i-2)*120/5) * Mathf.PI/180;
            ix = Mathf.Cos(theta);
            iy = Mathf.Sin(theta);                
            Vector3 v = new Vector3(ix,iy)*speed;
            b.AddForce(v, ForceMode2D.Impulse);
        }
    }

    public void fire3main(){
        GameObject go = GameObject.FindGameObjectWithTag("Player");        
        pController p = go.GetComponent<pController>();
        theta = (-p.phi+90)*Mathf.PI/180;
        ix = Mathf.Cos(theta);
        iy = Mathf.Sin(theta);
        GameObject arrow = Instantiate(arrow2Prefab, go.transform.position, Quaternion.Euler(0,0,-p.phi));
        Rigidbody2D b = arrow.GetComponent<Rigidbody2D>();
        Vector3 v = new Vector3(Mathf.Cos(theta),Mathf.Sin(theta))*speed;
        b.AddForce(v, ForceMode2D.Impulse);
    }
    void bomb3main(){
        GameObject[] arrows = GameObject.FindGameObjectsWithTag("bomb");
        foreach(GameObject arrow in arrows) {
            GameObject bomb = Instantiate(bombPrefab, arrow.transform.position, Quaternion.identity);
            Destroy(arrow);
        }
    }


    public void stopFire() {
        isFire = false;
    }

    public void addPower(int n) {
        power += n;
    }

}
