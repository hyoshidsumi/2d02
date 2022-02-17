using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellController : MonoBehaviour
{
    int r;
    public float dt_metastasis;
    public GameObject cellPrefab;
    public float dpos;
    bool isMultiple = false;
    public int maxTree;
     void Start()
    {
    }
    void Update()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        if (trees.Length < maxTree) 
            {
                if (isMultiple == false)
                {
                    isMultiple = true;
                    Invoke("metastasis", dt_metastasis);
                }
            }
    }

    void metastasis()
    {
        Vector3 pos = transform.position;
        r=UnityEngine.Random.Range(0,4);//0,1,2,3  
        switch(r) {
            case 0: pos.x += dpos; break;
            case 1: pos.x -= dpos; break;
            case 2: pos.y += dpos; break;
            case 3: pos.y -= dpos; break;
            default: break;
        }
        GameObject cell_new = Instantiate(cellPrefab,pos,Quaternion.identity);
        isMultiple = false;
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        Destroy(gameObject);
    }
}
