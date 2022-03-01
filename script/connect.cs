using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class connect : MonoBehaviour
{
    string url = "http://cdn-www.dailypuppy.com/media/dogs/anonymous/coffee_poodle01.jpg_w450.jpg";

    void Start()
    {
//        StartCoroutine(Connect());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Connect());
    }
    private IEnumerator Connect()
    {
        var www = new UnityWebRequest(url);
        www = null;
        //        yeild return www;
//        Debug.Log(www);

        yield return new WaitUntil(() => www != null);

        Debug.Log(www);
//        GetComponent<SourceImage>() = "arrow";
    }
}
