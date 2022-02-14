using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    public GameObject clearUI;

    void Start()
    {
        clearUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        Debug.Log(trees.Length);
        if(trees.Length == 0) {
            clear();

        }
        
    }
    void clear() {
        clearUI.SetActive(true);
    }
}
