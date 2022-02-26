using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectController : MonoBehaviour
{
    void Start()
    {
        Invoke("destroy",2.0f);
    }

    void destroy() {
        Destroy(gameObject);
    }
    void Update()
    {
        
    }
}
