using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fire2Controller : MonoBehaviour
{
    public float speed = 11.0f;
    public GameObject arrowPrefab, arrow2Prefab, bombPrefab;
    public Slider slider;
    public bool isGetButtonDown;
    public int power_max = 100;
    public int power_fire1 = 8;
    public int power_fire2 = 40;
    public bool isPower = true;
    timeController tc;
    pController p;
    bool isFire = false, isFire3 = false;
    public Animator motion;
    float radius,theta,larrow=0.3f;
    float ix, iy;
    int power;
    
    void Start() {
        Vector3 pos = transform.position;
        power = power_max;
    }

    void Update()  {
        pController p = GetComponent<pController>();
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
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
    }
    public void fire1(){
                if(!isFire) {
                isFire = true;
                Invoke("fire1main",0.2f);
                Invoke("stopFire",0.2f);
            }
    }
    public void fire2() {
            if(!isFire) {
                if(power-power_fire2 >= 0) {
                    power -= power_fire2;
                    isFire = true;
                    motion.SetBool("isFire",true);
                    Invoke("fire2main",0.2f);
                    Invoke("stopFire",0.2f);
                    slider.value = (float)power/(float)power_max;
                } else {
                    motion.SetBool("isFire",false);
                }
            }
    }    
    public void fire3(){
            if(isFire3) {
            isFire = false;
            isFire3 = false;
            bomb3main();
        } else {
            if(!isFire) {
                isFire3 = true;
                isFire = true;
                Invoke("fire3main",0.2f);
            }
        }
    }
    public void fire1main(){
        pController p = GetComponent<pController>();
        Debug.Log(p);
        theta = (-p.phi+90)*Mathf.PI/180;
        ix = Mathf.Cos(theta);
        iy = Mathf.Sin(theta);

        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0,0,-p.phi));
        Rigidbody2D b = arrow.GetComponent<Rigidbody2D>();

        Vector3 v = new Vector3(Mathf.Cos(theta),Mathf.Sin(theta))*speed;
        b.AddForce(v, ForceMode2D.Impulse);        
            
    }
    void fire2main(){
        pController p = GetComponent<pController>();
        theta = (-p.phi+90)*Mathf.PI/180;
        ix = Mathf.Cos(theta);
        iy = Mathf.Sin(theta);
        Vector3 pos = transform.position;
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
        pController p = GetComponent<pController>();
        theta = (-p.phi+90)*Mathf.PI/180;
        ix = Mathf.Cos(theta);
        iy = Mathf.Sin(theta);
        GameObject arrow = Instantiate(arrow2Prefab, transform.position, Quaternion.Euler(0,0,-p.phi));
        Rigidbody2D b = arrow.GetComponent<Rigidbody2D>();
        Vector3 v = new Vector3(Mathf.Cos(theta),Mathf.Sin(theta))*speed;
        b.AddForce(v, ForceMode2D.Impulse);             
    }
    void bomb3main(){
        GameObject arrow = GameObject.FindGameObjectWithTag("bomb");
        GameObject bomb = Instantiate(bombPrefab, arrow.transform.position, Quaternion.identity);
        Destroy(arrow);
    }


    public void stopFire() {
        isFire = false;
    }

}
