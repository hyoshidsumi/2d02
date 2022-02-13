using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire2Controller : MonoBehaviour
{
    public float speed = 11.0f;
    public GameObject arrowPrefab;
    bool isFire = false;
    string motionName;
    Animator motion;
    float radius,theta,larrow=0.3f;
    float ix, iy;
    
    void Start() {
        Vector3 pos = transform.position;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire2")) {
            if(!isFire) {
                isFire = true;
                Invoke("fire",0.2f);
                Invoke("stopFire",0.2f);
            }
        }
    }

    public void fire(){

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

    public void stopFire() {
        isFire = false;
    }

}
