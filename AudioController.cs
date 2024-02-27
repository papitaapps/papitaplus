using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioSource[] audiosource;
    public bool playing,canPlay;
    public AudioClip click, warning,CommingSoon;
    public AudioClip[] categoryName;
    public AudioClip[] chooseLang;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioController");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }

     
        
       
         
    }
    void Start()
    {
        canPlay = true;
        playbackground();
    }
    public void playbackground()
    {
        if (canPlay)
        {
            playing = true;
            audiosource[0].Play();
        }

       
    }
    public void Stopbackground()
    {
        playing = false;
        audiosource[0].Stop();
       
    }
    public void playSound(int a)
    {
        if (a == 1)
        {
            audiosource[1].PlayOneShot(click, 1);
        }
        else if (a == 2)
        {
            audiosource[1].PlayOneShot(warning, 1);
        }
        else if (a == 3)
        {
            audiosource[1].PlayOneShot(CommingSoon, 1);
        }
    }
    public void playSoundGameCategory(int a)
    {
      
            audiosource[1].PlayOneShot(categoryName[a], 1);
       
           
       
    }
    public void playChooseLang(int a)
    {
        audiosource[1].clip = chooseLang[a];
        audiosource[1].Play();



    }
}
