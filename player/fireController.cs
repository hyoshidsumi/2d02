using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireController : MonoBehaviour
{
    public float speed = 11.0f;
    public GameObject arrowPrefab;
    bool isFire = false;
    string motionName;
    Animator motion;
    float theta;

    float ix=0, iy=0;
    
    void Start() {
        Vector3 pos = transform.position;
    }

    void Update()
    {
        if(Input.GetButton("Fire1")) {
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

            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0,0,-p.phi));
            Rigidbody2D b = arrow.GetComponent<Rigidbody2D>();

            Vector3 v = new Vector3(Mathf.Cos(theta),Mathf.Sin(theta))*speed;
            b.AddForce(v, ForceMode2D.Impulse);        
            
    }

    public void stopFire() {
        isFire = false;
    }

}
