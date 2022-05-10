using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bFireController : MonoBehaviour
{
    public GameObject arrowPrefab, arrow2Prefab, bombPrefab;
    public Slider slider;
    public bool isGetButtonDown;
    public int power_max = 100;
    public int power_fire1 = 8;
    public int power_fire2 = 40;
    public bool isPower = true;
    timeController tc;
    bool isFire = false, isFire3 = false;
    public Animator motion;
    float radius, theta, larrow = 0.3f;
    float ix, iy;
    int power;
    pController p;
    float speed = 11.0f;

    void Start()
    {
        GameObject pobj = GameObject.FindGameObjectWithTag("Player");
        p = pobj.GetComponent<pController>();
        power = power_max;
    }
    public void bfire1()
    {
        if (!isFire)
        {
            isFire = true;
            Invoke("fire1", 0.2f);
            Invoke("stopFire", 0.2f);
        }
    }

    void fire1()
    {
        theta = (-p.phi + 90) * Mathf.PI / 180;
        ix = Mathf.Cos(theta);
        iy = Mathf.Sin(theta);

        GameObject arrow = Instantiate(arrowPrefab, p.transform.position, Quaternion.Euler(0, 0, -p.phi));
        Rigidbody2D b = arrow.GetComponent<Rigidbody2D>();

        Vector3 v = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) * speed;
        b.AddForce(v, ForceMode2D.Impulse);

    }
    public void fire2()
    {
        pController p = GetComponent<pController>();
        theta = (-p.phi + 90) * Mathf.PI / 180;
        ix = Mathf.Cos(theta);
        iy = Mathf.Sin(theta);
        Vector3 pos = transform.position;
        pos.x += larrow * ix;
        pos.y += larrow * iy;

        for (int i = 0; i < 5; i++)
        {
            radius = -p.phi + (i - 2) * 120 / 5;
            GameObject arrow = Instantiate(arrowPrefab, pos, Quaternion.Euler(0, 0, radius));
            Rigidbody2D b = arrow.GetComponent<Rigidbody2D>();
            theta = (-p.phi + 90 + (i - 2) * 120 / 5) * Mathf.PI / 180;
            ix = Mathf.Cos(theta);
            iy = Mathf.Sin(theta);
            Vector3 v = new Vector3(ix, iy) * speed;
            b.AddForce(v, ForceMode2D.Impulse);
        }
    }

    public void fire3()
    {
        pController p = GetComponent<pController>();
        theta = (-p.phi + 90) * Mathf.PI / 180;
        ix = Mathf.Cos(theta);
        iy = Mathf.Sin(theta);
        GameObject arrow = Instantiate(arrow2Prefab, transform.position, Quaternion.Euler(0, 0, -p.phi));
        Rigidbody2D b = arrow.GetComponent<Rigidbody2D>();
        Vector3 v = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) * speed;
        b.AddForce(v, ForceMode2D.Impulse);
    }
    public void bomb3()
    {
        GameObject arrow = GameObject.FindGameObjectWithTag("bomb");
        GameObject bomb = Instantiate(bombPrefab, arrow.transform.position, Quaternion.identity);
        Destroy(arrow);
    }


    public void stopFire()
    {
        isFire = false;
    }
    void Update()
    {
        /*
        if (Input.GetButton("Fire1"))
        {
            if (!isFire)
            {
                isFire = true;
                Invoke("fire1", 0.2f);
                Invoke("stopFire", 0.2f);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (!isFire)
            {
                if (power - power_fire2 >= 0)
                {
                    power -= power_fire2;
                    isFire = true;
                    motion.SetBool("isFire", true);
                    Invoke("fire2", 0.2f);
                    Invoke("stopFire", 0.2f);
                    slider.value = (float)power / (float)power_max;
                }
                else
                {
                    motion.SetBool("isFire", false);
                }
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {

            if (isFire3)
            {
                isFire = false;
                isFire3 = false;
                bomb3();
            }
            else
            {
                if (!isFire)
                {
                    isFire3 = true;
                    isFire = true;
                    Invoke("fire3", 0.2f);
                }
            }
        }
        */
    }
}
