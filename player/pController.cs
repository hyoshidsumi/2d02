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
    changeScene cs;

    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        motion = this.GetComponent<Animator>();
        motionName = mhold;
        lastMotion = mhold;
        health = health_max;
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        gm = gc.GetComponent<gameManager>();
        cs = gc.GetComponent<changeScene>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(0,0);

        if(Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            ix = touch.deltaPosition.x;
            iy = touch.deltaPosition.y;
            speed = 2.0f;
            if(Mathf.Abs(ix) < 4.0f) {
                ix = 0.0f;
            }
            if(Mathf.Abs(iy) < 4.0f) {
                iy = 0.0f;
            }
/*            
            if(ix>0){
                ix = 1;
            } else if(ix<0) {
                ix = -1;
            } 
            if(iy>0){
                iy = 1;
            } else if(iy<0) {
                iy = -1;
            }
*/            
        } else {
            ix = Input.GetAxisRaw("Horizontal");
            iy = Input.GetAxisRaw("Vertical");
        }

        if(iy > 0.0f) {
            motionName = mw;
            if(ix > iy) {
                phi = 45;
            } else if(-ix > iy) {
                phi = -45;
            } else {
                phi = 90;
            }
        } else if(iy < 0.0f) {
            motionName = ms;
            if(ix > -iy) {
                phi = 135;
            } else if(ix < iy) {
                phi = -135;
            } else {
                phi = 180;
            }
        } else {
            if(ix > 0.0f) {
                motionName = md;
                transform.localScale = new Vector3(11,11,1);
                phi = 0;
                isLeft = false;
            } else if(ix < 0.0f) {
                motionName = md;
                transform.localScale = new Vector3(-11,11,1);
                phi = -90;
                isLeft = true;                
            } else {
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
            }
        }

/*        
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
*/

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
//        if(ix!=0.0f || iy!=0.0f){
//        }
        rbody.velocity = new Vector2(ix, iy) * speed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        health -= 10;
        if(health < 0) {
            health = 0;
            gm.clear();
//            cs.Load2("pancreas");
        }
        hSlider.value = (float)health/(float)health_max;
        gm.hText.GetComponent<Text>().text = health.ToString();
    }
}
