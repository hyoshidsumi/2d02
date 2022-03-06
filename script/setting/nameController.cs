using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nameController : MonoBehaviour
{
    public GameObject canvasConfirm;
    void Start()
    {
        Debug.Log("c");
        string name=PlayerPrefs.GetString("name");
        if(name=="") {
            Debug.Log("a");
        }else{
            Debug.Log("b");
        }
        PlayerPrefs.SetString("b","b");
        
        canvasConfirm.SetActive(false);
    }

    void Update()
    {
        
    }


}
