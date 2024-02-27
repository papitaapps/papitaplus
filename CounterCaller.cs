using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class CounterCaller : MonoBehaviour
{
    private string PHP_URL = "https://islandofkids.com/app/counterVideo/counter.php?value=";
   
    public Text views, likeConter;
    public GameObject showCounterBox,videoBox;
    public VideoPlayer videoPlayer;
    bool checking;
    private void OnEnable()
    {
        showViews();
      //  string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "firstvideo.mp4");
       // StartCoroutine(LoadVideo(videoPath));
    }
    IEnumerator LoadVideo(string path)
    {
        var videoUrl = System.IO.Path.Combine("file://", path);
        videoPlayer.url = videoUrl;
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
    }
    async void showViews()
    {
        Counter(2);
        await System.Threading.Tasks.Task.Delay(2);
        CounterLike(4);
    }
    public void Counter(int a)
    {

        if (a == 2)
        {
            
            StartCoroutine(CallCounter(a));
        }
        else
        {

            if (PlayerPrefs.GetInt("countView") == 0)
            {

                StartCoroutine(CallCounter(a));
            }
        }


    }
    public async void VideoShowing(bool show)
    {
        if (show)
        {
            AudioController.instance.Stopbackground();
            videoPlayer.Prepare();
            videoPlayer.Play();
            await System.Threading.Tasks.Task.Delay(50);
            videoPlayer.Stop();
            await System.Threading.Tasks.Task.Delay(50);
            videoPlayer.Play();
            checking = true;
        }
        else
        {
            if ( AudioController.instance.canPlay)
                AudioController.instance.playbackground();
            videoPlayer.Stop();
            checking = false;
        }
    }
    public void CounterLike(int a)
    {
       
          
        if (a == 4)
        {
           
            StartCoroutine(CallCounter(a));
        }
        else
        {
          
            if ( PlayerPrefs.GetInt("countLike") == 0)
            {
              
               
                StartCoroutine(CallCounter(a));
            }
        }
            
              
        

    }

    private void Update()
    {
        if (checking)
        {
            if (!videoPlayer.isPlaying)
            {
              
                videoPlayer.Stop();
                checking = false;
                videoBox.SetActive(false);
            }

        }
    }

    private IEnumerator CallCounter(int a)
    {
        Debug.Log(PHP_URL);
        UnityWebRequest www = UnityWebRequest.Get(PHP_URL + a.ToString());
        yield return www.SendWebRequest();

        if (!www.isNetworkError && !www.isHttpError)
        {
            // int counter = int.Parse(www.downloadHandler.text);
            if (a == 1 | a == 2)
                views.text = www.downloadHandler.text;
            else if (a == 3 | a == 4)
                likeConter.text = www.downloadHandler.text;

            if(a==3)
                PlayerPrefs.SetInt("countLike", 1);

            if(a==1)
                PlayerPrefs.SetInt("countView", 1);
             Debug.Log("Counter: " + www.downloadHandler.text);
        }
        else
        {
            //  Debug.LogError("Failed to call counter PHP script: " + www.error);
            showCounterBox.SetActive(false);
        }
    }


}