using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pController : MonoBehaviour
{
    public float speed;
    public string ma,ms,md,mw;
    public string mhold, mdhold, mwhold, mahold;
    public string mq, me, mr;
    public string dfire, wfire, sfire;
    public float phi=180;
    
    Rigidbody2D rbody;
    public float ix, iy;
    bool iq, ie, ir;    
	Animator motion;
    string motionName = "";
    string lastMotion = "";
//    string fireName = "";
    bool isFire = false;
    bool isLeft;
    int health;
    public int health_max;
    public Slider hSlider;
    gameManager gm;
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        motion = this.GetComponent<Animator>();
        motionName = mhold;
        lastMotion = mhold;
        health = health_max;
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        gm = gc.GetComponent<gameManager>();
    }

    void Update()
    {
        
            ix = Input.GetAxisRaw("Horizontal");
            iy = Input.GetAxisRaw("Vertical");
            switch(iy) {
                case 1: 
                    motionName = mw;
                    switch(ix) {
                        case 1: phi = 45; break;
                        case -1: phi = -45; break;
                        default: phi = 90; break;
                    }
                    break;
                case -1:
                    motionName = ms;
                    switch(ix) {
                        case 1: phi = 135; break;
                        case -1: phi = -135; break;
                        default: phi = 180; break;
                    }
                    break;
                default:
                switch(ix) {
                    case 1: 
                        motionName = md;
                        transform.localScale = new Vector3(11,11,1);       
                        phi = 0;
                        isLeft = false;
                        break;
                    case -1:
                        motionName = md;
                        transform.localScale = new Vector3(-11,11,1);
                        phi = -90;
                        isLeft = true;
                        break;
                    default:
                        if((lastMotion=="md")||(lastMotion=="mdhold")) {
                            motionName = mdhold;
                            if(isLeft == false) {
                                phi = 90;
                            } else {
                                transform.localScale = new Vector3(-11,11,1);
                                phi = -90;
                            }
                        } else if((lastMotion=="mw")||(lastMotion=="mwhold")) {
                            motionName = mwhold;
                            phi = 0;
                        } else {
                            motionName = mhold;
                            phi = 180;
                        }
                        break;
                }
                break;
            }
            
            ir = Input.GetButtonDown("Fire3");
            if(ir == true) {
                if(isFire == false) {
                    isFire = true;
                    switch(phi) {
                        case 0:
                        GetComponent<Animator>().Play("wfire"); break;
                        case 45:
                        GetComponent<Animator>().Play("wfire"); break;
                        case -45:
                        GetComponent<Animator>().Play("wfire"); break;
                        case 90:
                        GetComponent<Animator>().Play("dfire"); break;
                        case -90:
                        GetComponent<Animator>().Play("dfire"); break;
                        default :
                        GetComponent<Animator>().Play("sfire"); break;
                    }
                    Invoke("stopFire",0.0f);
                }
            }

            if(motionName != lastMotion) {
                motion.Play(motionName);
                lastMotion = motionName;
            }

    }

    public void stopFire() {
        isFire = false;
    }

    private void FixedUpdate()
    {

        rbody.velocity = new Vector2(ix*speed, iy*speed);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        health -= 10;
        if(health < 0) {
            health = 0;
            Debug.Log("dead");
        }
        hSlider.value = (float)health/(float)health_max;
        gm.hText.GetComponent<Text>().text = health.ToString();
    }
}
