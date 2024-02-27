using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayPauseVideo : MonoBehaviour
{
    private VideoPlayer video;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        double v = video.length;
        Debug.Log(v);
    }
    public void PlayVideo()
    {
        video.Play();
    }
    public void PauseVideo()
    {
        video.Pause();
       
    }
}
