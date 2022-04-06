using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellController : MonoBehaviour
{
    int r;
    public float dt_metastasis;
    public GameObject cellPrefab, extinctPrefab, coinPrefab, portionPrefab;
    public float dpos;
    public int hp;
    public int tree_max;    
    public int nDestroy;
    bool isMultiple = false;
    GameObject gc;
    gameManager gm;
     void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
        gm = gc.GetComponent<gameManager>();

    }
    void Update()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        if(trees.Length < tree_max) {
            if(isMultiple == false) {
                isMultiple = true;
                Invoke("metastasis",dt_metastasis);
            }
        }
    }

    void metastasis(){
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

    void OnCollisionEnter2D(Collision2D col) {
//        if(col.gameObject.tag == "tile") {
//            Destroy(gameObject);  
//        }
        hp--;
        if(hp < 0) {
            GameObject effect_extinct = Instantiate(extinctPrefab,transform.position,Quaternion.identity);
            r = UnityEngine.Random.Range(0, 3);//0,1,2,3
            Debug.Log(r);
            if(r == 2) {
                GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            } else if (r == 1) {
                GameObject portion = Instantiate(portionPrefab, transform.position, Quaternion.identity);
            }
            gm.addDestroy();
            Destroy(gameObject);
        }
    }
}
