using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public TextMesh timeCounter;
    public TextMesh fullTime;
    public Text Timer;
    private TimeSpan timePlaying;
    public bool timerGoing, AD_Video , firstVideo;

    private float elapsedTime;

    private void Awake()
    {
        instance = this;
       
    }

    private void Start()
    {
        if (!AD_Video)
        {
            timeCounter.text = " 00:00";
            timerGoing = false;
        }
       
      
    }
    private void OnEnable()
    {
        if (firstVideo)
        {
           
            BeginTimer();
        }
    }
    float length ;
    public float NearToEnd;
    public void BeginTimer()
    {
        if (!AD_Video)
        {
            timerGoing = true;
            elapsedTime = 0f;
            length = (float)GetComponent<VideoPlayer>().length / 60;
            fullTime.text = " / " + length.ToString("00.00");
            NearToEnd = (float)((GetComponent<VideoPlayer>().length / 60) - (0.1f));
            StartCoroutine(UpdateTimer());
            if (PlayerPrefs.GetInt("witchCat") == 5)
            {
                PlayerPrefs.SetString("movieLenght" + PlayerPrefs.GetInt("witchCat") + PlayerPrefs.GetInt("witchScroolView") + PlayerPrefs.GetInt("VideoFlag"), length.ToString("00.00"));

            }
            else
            {
                PlayerPrefs.SetString("movieLenght" + PlayerPrefs.GetInt("witchCat") + PlayerPrefs.GetInt("VideoFlag"), length.ToString("00.00"));

            }
        }
        else
        {
            timerGoing = true;
            elapsedTime = 0f;
            StartCoroutine(UpdateTimer());
        }
    
        //  Debug.Log(PlayerPrefs.GetString("movieLenght" + PlayerPrefs.GetInt("witchCat") + PlayerPrefs.GetInt("VideoFlag")));
    }

    public void EndTimer()
    {
        timerGoing = false;
    }
  
    private IEnumerator UpdateTimer()
    {
        if (!AD_Video)
        {
            while (timerGoing)
            {
                elapsedTime = (float)GetComponent<VideoPlayer>().time;
                //  OnPlayerTime = (float)GetComponent<VideoPlayer>().time;
                timePlaying = TimeSpan.FromSeconds(elapsedTime);
                string timePlayingStr = timePlaying.ToString("mm':'ss");

                timeCounter.text = timePlayingStr;

                yield return null;
            }
        }
        else
        {
            while (timerGoing)
            {
                elapsedTime = (float)GetComponent<VideoPlayer>().time;
                //  OnPlayerTime = (float)GetComponent<VideoPlayer>().time;
                timePlaying = TimeSpan.FromSeconds(elapsedTime);
                if (elapsedTime > 10)
                {
                    leaderBoardManager.instance.next.gameObject.SetActive(true);
                }
                string timePlayingStr = timePlaying.ToString("mm':'ss");

                Timer.text = timePlayingStr;

                yield return null;
            }
        }
     
    }
    bool videoFinish=true;
    void Update()
    {
        if (elapsedTime/60 > NearToEnd & videoFinish & !AD_Video)
        {
            Debug.Log("near to Finish Video");
            videoFinish = false;
            MyVideoPlayer.instance.menubar.SetActive(true);
            MyVideoPlayer.instance.animmenu.Play("video menu 2");
        }
      
    }
}
