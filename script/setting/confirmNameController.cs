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
        string name = transform.parent.Find("tbName").Find("Text").gameObject.GetComponent<Text>().text;
        if (name != "") {
            canvas.SetActive(true);
        }
        canvas.transform.Find("bg").Find("lName").gameObject.GetComponent<Text>().text = name;
    }

    public void cancel(){
        transform.parent.parent.gameObject.SetActive(false);
    }

    public void registerName()
    {
        PlayerPrefs.SetString("name", transform.parent.Find("lName").gameObject.GetComponent<Text>().text);
        GetComponent<changeScene>().Load2("pancreas");
    }
}
