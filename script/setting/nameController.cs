using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nameController : MonoBehaviour
{
    public GameObject canvasConfirm;
    void Start()
    {
        string name = PlayerPrefs.GetString("name");
        if(name=="") {
        }else{
            transform.Find("Panel").Find("bg").Find("tbName").Find("Text").gameObject.GetComponent<Text>().text = name;
        }
        transform.Find("Panel").Find("bg").Find("bName").gameObject.GetComponent<Button>().interactable = false;

        canvasConfirm.SetActive(false);


    }

    void Update()
    {
        if (transform.Find("Panel").Find("bg").Find("tbName").Find("Text").gameObject.GetComponent<Text>().text != "")
        {
            transform.Find("Panel").Find("bg").Find("bName").gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            transform.Find("Panel").Find("bg").Find("bName").gameObject.GetComponent<Button>().interactable = false;
        }
    }


}
