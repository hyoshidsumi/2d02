using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class treantController : MonoBehaviour
{
    public int health_max;
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
    bool isFire = false ,isMove=true;
    bool isLeft;
    int health;
    int r;
    float time;
    pController p;
    public GameObject arrowPrefab;
    public Slider hSlider;

    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        motion = this.GetComponent<Animator>();
        motionName = mhold;
        lastMotion = mhold;
        health = health_max;
    }

    void Update()
    {

        GameObject po = GameObject.FindGameObjectWithTag("Player");
        p = po.GetComponent<pController>();
        Vector3 ppos = p.transform.position;
        float dist = Vector2.Distance(transform.position, ppos);
        if(dist < 8 && isFire == false)
        {
            float dx = ppos.x - transform.position.x;
            float dy = ppos.y - transform.position.y;
            float rad = Mathf.Atan2(dy, dx);
            float angle = rad * Mathf.Rad2Deg;
            Quaternion r = Quaternion.Euler(0, 0, angle-90);
            float bx = Mathf.Cos(rad);
            float by = Mathf.Sin(rad);
            Vector3 b = new Vector3(bx, by) * 14.0f;

            isFire = true;
            GameObject arrow = Instantiate(arrowPrefab, transform.position, r);
            Rigidbody2D barrow = arrow.GetComponent<Rigidbody2D>();
            barrow.AddForce(b, ForceMode2D.Impulse);        
            Invoke("stopFire",0.4f);

            isMove = false;
        }

        //move
        if (isMove)
        {

            time += Time.deltaTime;
            if (time > 2.0f)
            {
                time = 0;
                r = UnityEngine.Random.Range(0, 4);//0,1,2,3  
                switch (r)
                {
                    case 0: ix = 1; iy = 0; break;
                    case 1: ix = -1; iy = 0; break;
                    case 2: ix = 0; iy = 1; break;
                    case 3: ix = 0; iy = -1; break;
                    default: break;
                }

                switch (iy)
                {
                    case 1:
                        motionName = mw;
                        switch (ix)
                        {
                            case 1: phi = 45; break;
                            case -1: phi = -45; break;
                            default: phi = 90; break;
                        }
                        break;
                    case -1:
                        motionName = ms;
                        switch (ix)
                        {
                            case 1: phi = 135; break;
                            case -1: phi = -135; break;
                            default: phi = 180; break;
                        }
                        break;
                    default:
                        switch (ix)
                        {
                            case 1:
                                motionName = md;
                                transform.localScale = new Vector3(11, 11, 1);
                                phi = 0;
                                isLeft = false;
                                break;
                            case -1:
                                motionName = md;
                                transform.localScale = new Vector3(-11, 11, 1);
                                phi = -90;
                                isLeft = true;
                                break;
                            default:
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
                                break;
                        }
                        break;
                }

                if (motionName != lastMotion)
                {
                    motion.Play(motionName);
                    lastMotion = motionName;
                }
            }
        } else
        {

        }
    }

    public void stopFire() {
        isFire = false;
        isMove = true;
    }

    private void FixedUpdate()
    {
        rbody.velocity = new Vector2(ix*speed, iy*speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "arrow1")
        {
            health = health - 4 ;
        }

        hSlider.value = (float)health / (float)health_max;

        if (health < 1)
        {
            Destroy(gameObject);
        }
    }

}
