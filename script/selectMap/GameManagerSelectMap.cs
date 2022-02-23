using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSelectMap : MonoBehaviour
{
    changeScene cs;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("Cancel")) {
            cs = GetComponent<changeScene>();
            cs.Load2("title");
        }
    }
}
