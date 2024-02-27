using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UPersian.Components;
using UnityEngine.Networking;
using System;
using System.IO;
using UnityEngine.Video;
using Newtonsoft.Json;

public class leaderBoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static leaderBoardManager instance;
    public InputField cityName;
    public GameObject  thisUser, littleBox,getCity,boxLess10;
    public GameObject[] userLevels;
    public Sprite[] userColors;
    public RtlText[] users;
    public RtlText scoreUI;
    bool showThisUser = true, showBox = false, showimageBox = false;

    string mySelfLevel,video_link;
    public VideoPlayer video;
    public Button next;
    int videoVersion;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        // PlayerPrefs.SetInt("coins", 150);
       // PlayerPrefs.DeleteKey("video_version");
       
        next.gameObject.SetActive(false);
        SetData();
      
        userLevels[0].GetComponent<Image>().sprite = userColors[0];
        userLevels[1].GetComponent<Image>().sprite = userColors[0];
        userLevels[2].GetComponent<Image>().sprite = userColors[0];

        userLevels[3].GetComponent<Image>().sprite = userColors[1];
        CounterCoins();
      
    }

   
    public void show()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("city")))
        {
            MainMenuManager.instance.leader[0].SetActive(false);
            //get city first
            getCity.SetActive(true);

        }
        else
        {
            MainMenuManager.instance.leader[0].SetActive(false);
            MainMenuManager.instance.leader[1].SetActive(true);
            getCity.SetActive(false);
            StartCoroutine(menuManager());
        }
       
    }

    public void SetData()
    {
       
        StartCoroutine(InitUserData());
    }
    IEnumerator menuManager()
    {

        yield return new WaitForSeconds(0.2f);
        // if need
        if (showThisUser)
        {
            thisUser.SetActive(true);

        }

            if (users[7].text.Length < 16)
            {
                users[7].fontSize = 50;
            }
            else if (users[7].text.Length < 30)
            {
                users[7].fontSize = 45;
            }
            else if (users[7].text.Length < 45)
            {
                users[7].fontSize = 40;
            }
            else if (users[7].text.Length > 45)
            {
                users[7].fontSize = 35;
            }

      
        yield return new WaitForSeconds(1.5f);
        // if need
        if (showThisUser)
        {
           
            userLevels[3].GetComponent<Image>().sprite = userColors[1];
        }
        if (mySelfLevel == "1")
        {
            userLevels[0].GetComponent<Image>().sprite = userColors[1];
          
        }
        if (mySelfLevel == "2")
        {
            userLevels[1].GetComponent<Image>().sprite = userColors[1];
        }
        if (mySelfLevel == "3")
        {
            userLevels[2].GetComponent<Image>().sprite = userColors[1];
        }
        if (showBox)
            littleBox.SetActive(true);

        if (showimageBox)
            giftImage.gameObject.SetActive(true);
    }
    string name;
    string age ;
    string city ;
    string scores;
    IEnumerator InitUserData()
    {

        if(PlayerPrefs.HasKey("ChildrenName"))
        {

            name = UnityEngine.PlayerPrefs.GetString("ChildrenName");
            age = PlayerPrefs.GetInt("age").ToString();
            city = UnityEngine.PlayerPrefs.GetString("city");
            scores = UnityEngine.PlayerPrefs.GetInt("coins").ToString();
        }
        else
        {
            name = "کاربر بی نام";
            age = "5";
            city = "ایران";
            scores = UnityEngine.PlayerPrefs.GetInt("coins").ToString();

        }

        string parameters = "?city=" + city + "&name=" + name + "&age=" + age + "&score=" + scores;

        Debug.Log(PlayerPrefs.GetString("base_url") + "/user/scores" + parameters);
        using (UnityWebRequest www = UnityWebRequest.Get(PlayerPrefs.GetString("base_url") + "/user/scores" + parameters))
        {
            www.SetRequestHeader("Authorization", "Bearer " + UnityEngine.PlayerPrefs.GetString("user_token"));
            www.SetRequestHeader("Accept", "application/json");
            www.chunkedTransfer = false;
            www.certificateHandler = new BypassCertificate();
      
            yield return www.SendWebRequest();


            Debug.Log(www.downloadHandler.text);

            processJsonData(www.downloadHandler.text);
         
        }

    }
    string mounth, year,mounthVideo;
    void processJsonData(string _url)
    {
        LeaderDataClass jsnData = JsonUtility.FromJson<LeaderDataClass>(_url);


        foreach (LeaderList m in jsnData.interData)
        {

            if (m.status == "200")
            {

                #region set data 1
                users[6].text ="رتبه " + m.mySelf;
                PlayerPrefs.SetString("player_Position", m.mySelf);

                if (m.mySelf == "1" || m.mySelf == "2" || m.mySelf == "3")
                {
                    showThisUser = false;
                    mySelfLevel = m.mySelf;
                    Debug.Log("my self : " + mySelfLevel);
                }

                if (int.Parse(m.mySelf) < 11 )
                {
                    if (PlayerPrefs.GetInt("loginSucces") != 2 & PlayerPrefs.GetInt("coins") >1000)
                    {
                        boxLess10.SetActive(true);
                    }
                    Debug.Log("my self : " + int.Parse(m.mySelf));
                }
                // "date":"1401-08"
                year = m.date.Substring(0, 4);
                Debug.Log("date : " + m.date);
                #region mounths
                if (m.date.Contains("-01"))
                {
                    mounth = "FARVARDIN";
                    mounthVideo = "farvardin";
                }
                else if (m.date.Contains("-02"))
                {
                    mounth = "اردیبهشت";
                    mounthVideo = "ORDIBEHESHT";
                }
                else if (m.date.Contains("-03"))
                {
                    mounth = "خرداد";
                    mounthVideo = "KHORDAD";
                }
                else if (m.date.Contains("-04"))
                {
                    mounth = "تیر";
                    mounthVideo = "TIR";
                }
                else if (m.date.Contains("-05"))
                {
                    mounth = "مرداد";
                    mounthVideo = "MORDAD";
                }
                else if (m.date.Contains("-06"))
                {
                    mounth = "شهریور";
                    mounthVideo = "SHAHRIVAR";
                }
                else if (m.date.Contains("-07"))
                {
                    mounth = "مهر";
                    mounthVideo = "MEHR";
                }
                else if (m.date.Contains("-08"))
                {
                    mounth = "آبان";
                    mounthVideo = "ABAN";
                }
                else if (m.date.Contains("-09"))
                {
                    mounth = "آذر";
                    mounthVideo = "AZAR";
                }
                else if (m.date.Contains("-10"))
                {
                    mounth = "دی";
                    mounthVideo = "DEY";
                }
                else if (m.date.Contains("-11"))
                {
                    mounth = "بهمن";
                    mounthVideo = "BAHMAN";
                }
                else if (m.date.Contains("-12"))
                {
                    mounth = "اسفند";
                    mounthVideo = "ESFAND";
                } 
                #endregion

                users[9].text = mounth + " " + year;

                // gift boxes
                users[10].text = m.message;
                users[10].fontSize = m.messageFont;
                showBox = m.showBox;
                showimageBox = m.showImageBox;
                videoVersion = m.video_version;
               // Debug.Log("video :  "+m.video_link);
                video_link = m.video_link;
                if (string.IsNullOrEmpty(video_link) | videoVersion==0)
                {
                    Debug.Log("dont have video To Show");
                }
                else
                {
                    if (PlayerPrefs.GetInt("video_version") < m.video_version & PlayerPrefs.GetString("phoneNumber") !="09125208862" & PlayerPrefs.GetInt("ToutShown")==1)
                    {
                        Debug.Log("showing video");
                        video.url = video_link;
                        video.Play();
                        setting_Ad_video();
                    }
                   
                }
                
                if (showimageBox)
                {
                    imageURL = m.ImageUrl;
                    OnLoadImage();
                }
                #endregion



            }


        }


        MyUserLeaderBoardDataClass jsnDatas = JsonUtility.FromJson<MyUserLeaderBoardDataClass>(_url);


        foreach (MyUserLeaderBoardList s in jsnDatas.user)
        {

            Debug.Log(s.score);

            users[7].text = s.name + " " + (s.age).ToString() + " ساله از " + s.city;
            users[8].text = s.score;
            int sc = int.Parse(s.score);

          

                UnityEngine.PlayerPrefs.SetInt("coins", sc);

            
          
          

        }

        LeaderBoardDataClass jsnDataOfapp = JsonUtility.FromJson<LeaderBoardDataClass>(_url);
        foreach (LeaderBoardList x in jsnDataOfapp.data)
        {
            //ctrl + k + s
            #region setting nafarat
            // نفر اول
            if (x.num == 1)
            {
                userLevels[0].SetActive(true);
                users[1].text = x.score;
                users[0].text = x.name + " " + (x.age).ToString() + " ساله از " + x.city;
                if (users[0].text.Length < 16)
                {
                    users[0].fontSize = 50;
                }
                else if (users[0].text.Length < 30)
                {
                    users[0].fontSize = 45;
                }
                else if (users[0].text.Length < 45)
                {
                    users[0].fontSize = 40;
                }
                else if (users[0].text.Length > 45)
                {
                    users[0].fontSize = 35;
                }
            }


            // نفر دوم
            if (x.num == 2)
            {
                userLevels[1].SetActive(true);
                users[3].text = x.score;
                users[2].text = x.name + " " + (x.age).ToString() + " ساله از " + x.city;
                if (users[2].text.Length < 16)
                {
                    users[2].fontSize = 50;
                }
                else if (users[2].text.Length < 30)
                {
                    users[2].fontSize = 45;
                }
                else if (users[2].text.Length < 45)
                {
                    users[2].fontSize = 40;
                }
                else if (users[2].text.Length > 45)
                {
                    users[2].fontSize = 35;
                }
            }

            // نفر سوم
            if (x.num == 3)
            {
                userLevels[2].SetActive(true);
                users[5].text = x.score;
                users[4].text = x.name + " " + (x.age).ToString() + " ساله از " + x.city;
                if (users[4].text.Length < 16)
                {
                    users[4].fontSize = 50;
                }
                else if (users[4].text.Length < 30)
                {
                    users[4].fontSize = 45;
                }
                else if (users[4].text.Length < 45)
                {
                    users[4].fontSize = 40;
                }
                else if (users[4].text.Length > 45)
                {
                    users[4].fontSize = 35;
                }
            }

            if (!showThisUser)
            {
                thisUser.SetActive(false);
            }


            if (showThisUser)
            {

                userLevels[3].GetComponent<Image>().sprite = userColors[1];
            }
            if (mySelfLevel == "1")
            {
                userLevels[0].GetComponent<Image>().sprite = userColors[1];

            }
            if (mySelfLevel == "2")
            {
                userLevels[1].GetComponent<Image>().sprite = userColors[1];
            }
            if (mySelfLevel == "3")
            {
                userLevels[2].GetComponent<Image>().sprite = userColors[1];
            }
            if (showBox)
                littleBox.SetActive(true);

            if (showimageBox)
                giftImage.gameObject.SetActive(true);
        } 
        #endregion


    }
    bool hasInternet = false;


    IEnumerator checkInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }
    public GameObject videoFrame;
    public Text videoTime, videoMounth;
    void setting_Ad_video()
    {
        if(AudioController.instance != null)
        {
            AudioController.instance.playing = false;
            AudioController.instance.Stopbackground();
        }
           

        videoFrame.SetActive(true);
        videoMounth.text = mounthVideo + " " + year;
        int sysHour = DateTime.Now.Hour;
        int sysMin = DateTime.Now.Minute;
        videoTime.text = sysHour.ToString() + ":" + sysMin.ToString();

             video.gameObject.GetComponent<TimerController>().BeginTimer();
        next.onClick.AddListener(() => OnMovieFinished(video));
        
        video.loopPointReached += OnMovieFinished;


    }
    void OnMovieFinished(VideoPlayer player)
    {
        Debug.Log("Event for movie end called");
        player.Stop();
      
        if ( AudioController.instance.canPlay)
        {
            
            AudioController.instance.playbackground();
        }
           
        StartCoroutine(GetRequestAd_video(PlayerPrefs.GetString("base_url") + "/user/video_seen"));
        videoFrame.SetActive(false);
    }
    IEnumerator GetRequestAd_video(string uri)
    {
        WWWForm form = new WWWForm();

        form.AddField("video_version", videoVersion);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri,form))
        {

            webRequest.SetRequestHeader("Authorization", "Bearer " + UnityEngine.PlayerPrefs.GetString("user_token"));
            webRequest.SetRequestHeader("Accept", "application/json");
            //point:
            // BypassCertificate class in the main app controller script
            webRequest.chunkedTransfer = false;
            webRequest.certificateHandler = new BypassCertificate();
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
                InternetAvailabilityTest.instance.CheckConnection();

            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                if ((webRequest.downloadHandler.text).Contains("Error"))
                {
                    // serverBox.SetActive(true);
                }
                else
                {
                    PlayerPrefs.SetInt("video_version", videoVersion);
                }

            }
        }

    }

    public void toast()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _ShowAndroidToastMessage("برای دیدن رتبه خود در نفرات برتر اینترنت خود را روشن کنید");
#endif
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
#endif





    ////////////////////////////////////////////////////   load & save image ////////////////////////////////////////////////////////////


    [SerializeField]
    Image giftImage,firstPersonImage;
    string imageURL;
    string fileName;


    public void OnLoadImage()
    {
        Debug.Log("image url : " + imageURL);
        StartCoroutine(LoadTextureFromWeb());
    }

    IEnumerator LoadTextureFromWeb()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
            giftImage.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
            firstPersonImage.sprite= Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
            giftImage.SetNativeSize();
            firstPersonImage.SetNativeSize();
        }
    }

    public void OnLoadImageFromDiskButtonClick()
    {
        if (!File.Exists(Application.persistentDataPath + fileName))
        {
            Debug.LogError("File Not Exist!");
            return;
        }

        LoadImageFromDisk();
    }
    // load texture bytes from disk and compose sprite from bytes
    private void LoadImageFromDisk()
    {
        byte[] textureBytes = File.ReadAllBytes(Application.persistentDataPath + fileName);
        Texture2D loadedTexture = new Texture2D(0, 0);
        loadedTexture.LoadImage(textureBytes);
        giftImage.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
        giftImage.SetNativeSize();

    }

    public void OnSaveImageButtonClick()
    {
        if (giftImage.sprite == null)
        {
            Debug.LogError("No Image to Save!");
            return;
        }

        WriteImageOnDisk();
    }

    private void WriteImageOnDisk()
    {
        byte[] textureBytes = giftImage.sprite.texture.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + fileName, textureBytes);
        Debug.Log("File Written On Disk!");
    }

    /// <summary>
    /// click event - make image blank 
    /// </summary>
    public void OnBlankImageButtonClick()
    {
        giftImage.sprite = null;
    }

    /// <summary>
    /// //////////////////////////////////////////////////////// score showing //////////////////////////////////////////////////////
    /// </summary>
    int score, displayScore;
    float scoreTimer=0.02f;
    void CounterCoins()
    {
       
        score = UnityEngine.PlayerPrefs.GetInt("coins");

        if (score < 100)
        {
            scoreTimer = 0.002f;
        }
        else if (score < 500)
        {
            scoreTimer = 0.0001f;
        }
        else
        {
            scoreTimer = 0.00005f;
        }
        displayScore = 0;
        scoreUI.text = displayScore.ToString();

        if (score > 0)
        {
            StartCoroutine(ScoreUpdater());
        }
   
       
    }

    private IEnumerator ScoreUpdater()
    {
        while (true)
        {
            if (displayScore < score)
            {
                displayScore++; //Increment the display score by 1
                scoreUI.text =  displayScore.ToString(); //Write it to the UI
            }
            else
            {
              
            }
            yield return new WaitForSeconds(scoreTimer); // I used .2 secs but you can update it as fast as you want
        }
    }


    public void getCityName()
    {
        if (!string.IsNullOrEmpty(cityName.text))
        {
            PlayerPrefs.SetString("city", cityName.text);
            SetData();
            //
            //show();
            getCity.SetActive(false);

            if (PlayerPrefs.GetInt("ToutShown") == 0)
            {
                ToutorialManager.instance.StartShowing();
            }
        }
        
    }
}

[System.Serializable]
public class LeaderDataClass
{
    public string status;
    public List<LeaderList> interData;

}

[System.Serializable]
public class LeaderList
{

    public string status;
    public string mySelf;
    public bool showBox;
    public string message;
    public string date;
    public int messageFont;
    public bool showImageBox;
    public string ImageUrl;
    public int video_version;
    public string video_link;
}
[System.Serializable]
public class LeaderBoardDataClass
{
    public string id;
    public List<LeaderBoardList> data;

}

[System.Serializable]
public class LeaderBoardList
{

    public string id;
    public int num;
    public string name;
    public string age;
    public string city;
    public string score;

}
[System.Serializable]
public class MyUserLeaderBoardDataClass
{
    public string name;
    public List<MyUserLeaderBoardList> user;

}

[System.Serializable]
public class MyUserLeaderBoardList
{


    public string name;
    public string age;
    public string city;
    public string score;

}
