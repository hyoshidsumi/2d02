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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void confirmName(){
        Debug.Log("aae");
        canvas.SetActive(true);
    }

    public void cancel(){
        gameObject.SetActive(false);
    }
}
