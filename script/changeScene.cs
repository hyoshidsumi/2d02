using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{

    public string sceneName;
    public string test="aaa";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
}
