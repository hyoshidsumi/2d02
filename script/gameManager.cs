using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    public GameObject clearUI;
    public GameObject tText;
    tController t;

    void Start()
    {
        clearUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        if(trees.Length == 0) {
            clear();
            t.isCount = false;
        } else
        {
            int it = (int)t.display;
            Debug.Log(it);
            t.isCount = false;
            tText.GetComponent<Text>().text = it.ToString();
        }
        
    }
    void clear() {
        clearUI.SetActive(true);
    }
}
