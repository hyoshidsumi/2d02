using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class pController : Photon.Pun.MonoBehaviourPun
{
    public float speed;
    public string ma,ms,md,mw;
    public string mhold, mdhold, mwhold, mahold;
    public string mq, me, mr;
    public string dfire, wfire, sfire;
    public float phi=180;
    
    Rigidbody2D rbody;
    public float ix, iy, ix0, iy0;
    bool iq, ie, ir;    
	Animator motion;
    string motionName = "";
    string lastMotion = "";
    bool isFire = false;
    bool isLeft;
    int health;
    bool isDamage = false;
    public int health_max;
    GameObject gc;
    Vector2 tpos = new Vector2(0,0), tpos0 = new Vector2(0,0);

    public SpriteRenderer sp;
    float sumTime = 0f;

    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        motion = this.GetComponent<Animator>();
        motionName = mhold;
        lastMotion = mhold;
        health = health_max;
        gc = GameObject.FindGameObjectWithTag("GameController");
        sp = GetComponent<SpriteRenderer>();
        sp.color = new Color(1f, 1f, 1f, 0.3f);
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width * 3.0f / 5.0f)
            {
                speed = 11.0f;

                if (touch.phase == TouchPhase.Stationary)
                {
                    ix = ix0;
                    iy = iy0;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    ix = touch.deltaPosition.x;
                    iy = touch.deltaPosition.y;
                    if (Mathf.Abs(ix) < 4.0f)
                    {
                        ix = 0.0f;
                    }
                    else
                    {
                        ix = ix / Mathf.Abs(ix);
                    }
                    if (Mathf.Abs(iy) < 4.0f)
                    {
                        iy = 0.0f;
                    }
                    else
                    {
                        iy = iy / Mathf.Abs(iy);
                    }
                    if (Mathf.Abs(ix) > 0 || Mathf.Abs(iy) > 0)
                    {
                        ix0 = ix;
                        iy0 = iy;
                    }
                }
            }
        }
        else
        {
            ix = 0.0f;
            iy = 0.0f;
            ix = Input.GetAxisRaw("Horizontal");
            iy = Input.GetAxisRaw("Vertical");
        }

        if (iy > 0.0f)
        {
            motionName = mw;
            if (ix > iy)
            {
                phi = 45;
            }
            else if (-ix > iy)
            {
                phi = -45;
            }
            else
            {
                phi = 90;
            }
        }
        else if (iy < 0.0f)
        {
            motionName = ms;
            if (ix > -iy)
            {
                phi = 135;
            }
            else if (ix < iy)
            {
                phi = -135;
            }
            else
            {
                phi = 180;
            }
        }
        else
        {
            if (ix > 0.0f)
            {
                motionName = md;
                transform.localScale = new Vector3(11, 11, 1);
                phi = 0;
                isLeft = false;
            }
            else if (ix < 0.0f)
            {
                motionName = md;
                transform.localScale = new Vector3(-11, 11, 1);
                phi = -90;
                isLeft = true;
            }
            else
            {
                if ((lastMotion == "md") || (lastMotion == "mdhold"))
                {
                    motionName = mdhold;
                    if (isLeft == false)
                    {
                        phi = 90;
                    }
                    else
                    {
                        transform.localScale = new Vector3(-11, 11, 1);
                        phi = -90;
                    }
                }
                else if ((lastMotion == "mw") || (lastMotion == "mwhold"))
                {
                    motionName = mwhold;
                    phi = 0;
                }
                else
                {
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

        if (motionName != lastMotion)
        {
            motion.Play(motionName);
            lastMotion = motionName;
        }

        if (isDamage)
        {
            sumTime += Time.deltaTime;
            if (sumTime < 0.1f) {
                sp.color = new Color(1f, 1f, 1f, 0.2f);
            } else if (sumTime < 0.2f) {
                sp.color = new Color(1f, 1f, 1f, 1f);
            } else {
                sumTime = 0f;
            }
        } else {
            sp.color = new Color(1f, 1f, 1f, 1f);
        }

    }

    public void stopFire() {
        isFire = false;
    }

    private void FixedUpdate()
    {
        if(!photonView.IsMine) return;
        rbody.velocity = new Vector2(ix, iy) * speed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(!photonView.IsMine) return;
        gameManager gm = gc.GetComponent<gameManager>();

        if(isDamage) return;
        isDamage = true;

        health -= 10;
        if(health < 0) {
            health = 0;
            gm.fail();
        }
        
        Slider hSlider = gc.transform.Find("pStatus").Find("hSlider").GetComponent<Slider>();
        hSlider.value = (float)health/(float)health_max;
        gm.hText.GetComponent<Text>().text = health.ToString();

        StartCoroutine(onDamage());

    }

    public IEnumerator onDamage() {
        yield return new WaitForSeconds(1.5f);
        isDamage = false;
    }
}
