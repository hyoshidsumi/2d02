using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{

    public string sceneName;
    public string test="aaa";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Load2(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void Close() {
        Debug.Log(transform.parent.parent);
        transform.parent.gameObject.SetActive(false);
    }
}
