using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum se {
    Arrow, ArrowBomb, Bomb, Bang, Clear,
}
public class soundManager : MonoBehaviour
{
    public static soundManager sm;
    public AudioClip bgm;
    public AudioClip seArrow, seArrowBomb, seBomb, seBang;
    public AudioClip meClear;

    void Start()
    {
        if(sm == null) {
            sm = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        
    }
    public void playBGM() {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = bgm;
            audio.Play();
    }
    public void playSE(se type){
        AudioSource audio = GetComponent<AudioSource>();
        if(type == se.Arrow) {
            audio.PlayOneShot(seArrow);
        } else if(type == se.ArrowBomb) {
            audio.PlayOneShot(seArrowBomb);
        } else if(type == se.Bomb) {
            audio.PlayOneShot(seBomb);
        } else if(type == se.Bang) {
            audio.PlayOneShot(seBang);
        } else if(type == se.Clear) {
            audio.PlayOneShot(meClear);            
        }
    }
}
