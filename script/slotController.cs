using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotController : MonoBehaviour
{
    bool isStop = false;
    float h;
    float dy_sum;
    Vector2 spos;
    GameObject number;

    private void Awake()
    {
//        number = transform.Find("number").gameObject;
//Awake‚Å‚·‚é‚ÆƒGƒ‰[o‚é‚ªÀÛ‚Í“®‚­
    }
    void Start()
    {
        number = transform.Find("number").gameObject;
        h = number.GetComponent<SpriteRenderer>().bounds.size.y;
        spos = number.transform.position;

    }
    void Update()
    {
        if (!isStop)
        {
            number.transform.Translate(0,-0.02f,0);
            if (number.transform.position.y <= spos.y - h/2.0f)
            {
                number.transform.position = spos;
            }
        }
    }
//    IEnumerator start(int imax)
    void start(int imax)
    {


            transform.position = spos;


    }

}
