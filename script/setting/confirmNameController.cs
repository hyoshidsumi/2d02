using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class confirmNameController : MonoBehaviour
{
    public GameObject canvas;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void confirmName(){
        canvas.SetActive(true);
    }

    public void cancel(){
        transform.parent.parent.gameObject.SetActive(false);
        
    }
}
