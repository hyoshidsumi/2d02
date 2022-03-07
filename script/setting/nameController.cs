using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nameController : MonoBehaviour
{
    public GameObject canvasConfirm;
    void Start()
    {
        string name=PlayerPrefs.GetString("name");
        if(name=="") {
        }else{
        }
        canvasConfirm.SetActive(false);
    }

    void Update()
    {

        Debug.Log(transform.Find("Panel").gameObject);

    }


}
