using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotController : MonoBehaviour
{
    float h;
    float dy_sum;
    Vector2 spos;

    void Start()
    {
        h = GetComponent<SpriteRenderer>().bounds.size.y;

    }
    void Update()
    {
        StartCoroutine(start(20));

    }
    IEnumerator start(int imax)
    {
        spos = transform.position;

        float x = spos.x;
        float y = spos.y;
        float y_ini = y;

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.1f);
            y -= 0.5f;
            dy_sum += 0.5f;
            Debug.Log(dy_sum + " h:" + h + " a:" + a);
            
            spos = new Vector2(x, y);
            transform.position = spos;
        }

        if (dy_sum > h)
        {
            y = y_ini;
        }

    }

}
