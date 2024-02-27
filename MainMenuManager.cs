using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UPersian.Components;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MainMenuManager instance;
    public Button[] buttons;
    public Sprite[] useravatar;
    private Animator setting;
    public GameObject BoxAlarmLeader,boxAlarmDownloaded,boxWitchLang,btnsEn,btnsFa,boxNews,boxAlarmInpuNumber;
    public GameObject[] leader;
    public GameObject[] SoundControl;
    private Text childname, age,coins;
    public Color loadToColor;
    public AudioSource audioSource;
    public Sprite[] ChooseLang;
    public Image ScreenShotSample;
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
            instance = this;
    }
    void Start()
    {
        // witchLang az script main app controller pak mishavad harbar
      
        btnsEn.SetActive(false);
        btnsFa.SetActive(false);
        ScreenShotSample.gameObject.SetActive(false);


        //sound btn
        if (GameObject.Find("Audio Controller")) CheckAudio();
          
        if (PlayerPrefs.GetInt("with_Ad") == 1 & AdiveryConfig.instance.BannerIsShowing)
            AdiveryConfig.instance.OnBannerAdDestroy();


        StartCoroutine(makingMainMenu());


    }

    IEnumerator makingMainMenu()
    {
        if (PlayerPrefs.GetInt("comeFromSample") == 0)
        {
            if (PlayerPrefs.GetInt("ToutShown") == 0) ToutorialManager.instance.StartShowing();

            DoLangStuff();


            ShowNews(0);

            setting = GameObject.Find("sub menu").GetComponent<Animator>();
        }
        else   // اینجا از بازی سمپل اومده
        {
            ComeFromSample();

        }

        yield return new WaitForSeconds(0.2f);


        childname = GameObject.Find("childname").GetComponent<RtlText>();
        coins = GameObject.Find("coins").GetComponent<RtlText>();
        age = GameObject.Find("age").GetComponent<RtlText>();

        makeAvatar();
        yield return new WaitForSeconds(0.2f);
        childnameSection();

        coins.text = PlayerPrefs.GetInt("coins").ToString();
        // Debug.Log("child name : " + PlayerPrefs.GetString("ChildrenName"));
        makeButtons();

        leader[0].SetActive(true);
        leader[1].SetActive(false);

        yield return new WaitForSeconds(2.5f);
       // checkAlarmBoxNumber();
    }

    void CheckAudio()
    {
        if (!AudioController.instance.audiosource[0].isPlaying)
        {
            SoundControl[1].SetActive(true);
            SoundControl[0].SetActive(false);
            SoundControl[2].GetComponent<Button>().onClick.AddListener(() => changeSoundPos(true));
        }
        else
        {
            SoundControl[2].GetComponent<Button>().onClick.AddListener(() => changeSoundPos(false));
            SoundControl[0].SetActive(true);
            SoundControl[1].SetActive(false);

        }
    }
    void ShowNews(int a)
    {
        boxNews.transform.GetChild(4).gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("news_image") )
        {
            Image image = boxNews.transform.GetChild(4).gameObject.GetComponent<Image>();
            string url = PlayerPrefs.GetString("news_image");
            StartCoroutine(LoadTextureFromWeb(url, image));
        }
       
        // نشان دادن اخبار جدید
        if (a == 0)
        {
            if (PlayerPrefs.GetInt("ToutShown") == 1 & PlayerPrefs.HasKey("witchLang") &
          PlayerPrefs.GetInt("user_news_version") < PlayerPrefs.GetInt("news_version"))
            {
                boxNews.SetActive(true);
                boxNews.GetComponent<Animator>().Play("box news");
                boxNews.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<RtlText>().text = PlayerPrefs.GetString("news_Message");
                buttons[5].onClick.RemoveAllListeners();
                buttons[5].onClick.AddListener(() => seenNews());
            }
            else
            {
                boxNews.SetActive(false);
            }
        }
        else
        {
            boxNews.SetActive(true);
            boxNews.GetComponent<Animator>().Play("box news");
            boxNews.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<RtlText>().text = PlayerPrefs.GetString("news_Message");
            buttons[5].onClick.AddListener(() => seenNews());

        }
      
    }
    IEnumerator LoadTextureFromWeb(string imageURL , Image childImage)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
           
        }
        else
        {
            boxNews.transform.GetChild(4).gameObject.SetActive(true);
            Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
            childImage.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
            childImage.SetNativeSize();
           
        }
    }

    void DoLangStuff()
    {
        if (!PlayerPrefs.HasKey("witchLang") & PlayerPrefs.GetInt("ToutShown") == 1)
        {
            showBoxSelectLang();
        }
        else
        {
            if (PlayerPrefs.HasKey("witchLang"))
                buttons[2].gameObject.GetComponent<Image>().sprite = ChooseLang[PlayerPrefs.GetInt("witchLang") - 1];

            AdiveryConfig.instance.loadBanner();

            boxWitchLang.SetActive(false);
            if (PlayerPrefs.GetInt("witchLang") == 1)
                btnsFa.SetActive(true);
            else
                btnsEn.SetActive(true);


        }
    }
    void ComeFromSample()
    {
        PlayerPrefs.SetInt("comeFromSample", 0);

        LoadImageFromDisk(PlayerPrefs.GetString("SamplegameScreenShot"));
        ScreenShotSample.gameObject.SetActive(true);
        btnsFa.SetActive(true);
        AdiveryConfig.instance.loadBanner();
        PlayerPrefs.SetInt("witchLang", 1);
        buttons[2].gameObject.GetComponent<Image>().sprite = ChooseLang[PlayerPrefs.GetInt("witchLang") - 1];
        setting = GameObject.Find("sub menu").GetComponent<Animator>();
        AudioController.instance.playbackground();
    }
    void childnameSection()
    {
        if (PlayerPrefs.HasKey("ChildrenName"))
        {
            if (PlayerPrefs.GetInt("age") > 9)
            {
                age.text = "بیشتر از 8 سال";
            }
            else
            {
                age.text = PlayerPrefs.GetInt("age").ToString() + " ساله ";
            }
            string m_name = PlayerPrefs.GetString("ChildrenName");
            childname.text = m_name;
            childname.gameObject.GetComponent<Button>().enabled = false;
            GameObject.Find("avatar").GetComponent<Button>().onClick.AddListener(() => openLeaderBoard());
        }
        else
        {
            childname.text = "کاربر بی نام";
            childname.gameObject.GetComponent<Button>().onClick.AddListener(() => GoForLogin());
            GameObject.Find("avatar").GetComponent<Button>().onClick.AddListener(() => GoForLogin());

        }
    }
    void makeAvatar()
    {
        if (PlayerPrefs.GetInt("gender") == 1)
        {
            GameObject.Find("avatar").GetComponent<SpriteRenderer>().sprite = useravatar[0];

        }
        else if (PlayerPrefs.GetInt("gender") == 2)
        {
            GameObject.Find("avatar").GetComponent<SpriteRenderer>().sprite = useravatar[1];
        }
        else
        {
            GameObject.Find("avatar").GetComponent<SpriteRenderer>().sprite = useravatar[0];
        }
    }
    void makeButtons()
    {
        //btn open setting
        buttons[0].onClick.AddListener(() => buttonCallBack("menu seting animation - 1", 1));
        //btn close setting
        buttons[1].onClick.AddListener(() => buttonCallBack("menu seting animation - 2", 2));

        //downlaod btn
        //buttons[2].onClick.AddListener(() => buttonCallBack("downloaded", 3));
        buttons[2].onClick.AddListener(() => showBoxSelectLang());
        //setting button
        buttons[3].onClick.AddListener(() => buttonCallBack("setting", 3));
        buttons[4].onClick.AddListener(() => openLeaderBoard());
        //show news
        buttons[6].onClick.AddListener(() => ShowNews(1));
    }

    void checkAlarmBoxNumber()
    {
        if (PlayerPrefs.GetInt("loginSucces") != 2 & PlayerPrefs.GetInt("ToutShown") == 1
          & PlayerPrefs.GetInt("user_news_version") == PlayerPrefs.GetInt("news_version"))
        {
            PlayerPrefs.SetInt("comeForAddData", 1);
            boxAlarmInpuNumber.SetActive(true);
        }
    }
    public void LoadImageFromDisk(string Fnames)
    {
        
        byte[] textureBytes = File.ReadAllBytes(Application.persistentDataPath + Fnames);
        // Debug.Log("load image from this Addres : " + Application.persistentDataPath + fileName);
        Texture2D loadedTexture = new Texture2D(0, 0);
        loadedTexture.LoadImage(textureBytes);
    
        ScreenShotSample.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
        ScreenShotSample.SetNativeSize();

     

    }

    bool news = false;
    async void seenNews()
    {
        PlayerPrefs.SetInt("user_news_version", PlayerPrefs.GetInt("news_version"));
        boxNews.GetComponent<Animator>().Play("box news go");
        await System.Threading.Tasks.Task.Delay(1000);
        boxNews.SetActive(false);
        if (PlayerPrefs.GetInt("freeFirstTime") == 1 &!news)
        {
            Debug.Log("show box again");
            boxNews.SetActive(true);
            boxNews.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<RtlText>().text =
                "میتونید تمام بازی ها رو یکبار رایگان ببینید. یادتون باشه حتما بعد از خرید برای پشتیبانی بهتر شماره تماس خودتون رو ثبت کنید";
        }
        news = true;
    }
    void showBoxSelectLang()
    {
        boxWitchLang.SetActive(true);
        AudioController.instance.playChooseLang(0);
       // AdiveryConfig.instance.OnBannerAdDestroy();

    }
    public void GoForLogin()
    {
        PlayerPrefs.SetInt("comeForAddData", 1);
        SceneManager.LoadScene("intro");
    }

    public void SelectLang(int L)
    {
        PlayerPrefs.DeleteKey("scrollValueMain");
        PlayerPrefs.SetInt("witchLang", L);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        AudioController.instance.playChooseLang(L);

        
        if (L==1)
            Firebase.Analytics.FirebaseAnalytics.LogEvent("btn_Select_Farsi", "btn_Select_Farsi_name", 1);
        else if(L==2)
            Firebase.Analytics.FirebaseAnalytics.LogEvent("btn_Select_En", "btn_Select_En_name", 1);

    }
    private void buttonCallBack(string name, int doing)
    {
        if (name == "downloaded")
        {
            if (PlayerPrefs.GetInt("with_Ad") == 0)
            {
                boxAlarmDownloaded.SetActive(true);
             

            }
            else
            {
                if (doing == 1)
                {
                    setting.Play(name);
                }
                else if (doing == 2)
                {
                    setting.Play(name);
                }
                else if (doing == 3)
                {
                    //changeScene(name);
                    Initiate.Fade(name, loadToColor, 3f);

                    if (name == "downloaded")
                    {
                        Firebase.Analytics.FirebaseAnalytics.LogEvent("btn_downloaded", "btn_downloaded_name", 1);
                    }
                    else
                    {
                        Firebase.Analytics.FirebaseAnalytics.LogEvent("btn_leader_board", "btn_leader_board_name", 1);
                    }
                }
            }
        }
        else
        {
            if (doing == 1)
            {
                setting.Play(name);
            }
            else if (doing == 2)
            {
                setting.Play(name);
            }
            else if (doing == 3)
            {
                //changeScene(name);
                Initiate.Fade(name, loadToColor, 3f);

                if (name == "downloaded")
                {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("btn_downloaded", "btn_downloaded_name", 1);
                }
                else
                {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("btn_leader_board", "btn_leader_board_name", 1);
                }
            }
        }
    

    }

    public void changeScene(string Scene)
    {
        // SceneManager.LoadScene(Scene);
        //  Initiate.Fade(Scene, loadToColor, 3f);
        StartCoroutine(LoadAsynchronously(Scene));
    }
    IEnumerator LoadAsynchronously(string sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // loadingScreen.SetActive(true);
        Text text = mainData.instance.loading.transform.GetChild(0).GetComponent<Text>();
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            int progressPercentage = Mathf.RoundToInt(progress * 100); // تبدیل به درصد

            // slider.fillAmount = progress;
            text.text = progressPercentage.ToString() + " % "; // نمایش درصد

            yield return null;
        }

    }
    void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {

            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                Initiate.Fade("intro", loadToColor, 3f);
            }
        }
    }

    ///////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////
    /// LEADER Board
    //////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////
    

    void openLeaderBoard()
    {
        if(PlayerPrefs.GetInt("with_Ad") == 0)
        {

            BoxAlarmLeader.SetActive(true);
            BoxAlarmLeader.transform.GetChild(1).gameObject.SetActive(false);
            BoxAlarmLeader.transform.GetChild(0).gameObject.GetComponent<RtlText>().text = "برای ورود به جدول نفرات برتر و دریافت جایزه ماهانه نیاز به خرید اشتراک دارید";
        }
        else
        {
            if (!PlayerPrefs.HasKey("phoneNumber"))
            {
                BoxAlarmLeader.transform.GetChild(4).gameObject.SetActive(false);
                BoxAlarmLeader.SetActive(true);
                BoxAlarmLeader.transform.GetChild(1).gameObject.SetActive(true);
                PlayerPrefs.SetInt("comeForAddData", 1);
                BoxAlarmLeader.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("intro"));
                BoxAlarmLeader.transform.GetChild(0).gameObject.GetComponent<RtlText>().text = "برای ورود به جدول نفرات برتر باید اطلاعات کاربری فرزندتان را تکمیل نمایید";

            }
            else
            {
                audioSource.Play();
                buttons[4].onClick.AddListener(() => closeLeaderBoard());
                Color a = buttons[4].GetComponent<Image>().color;
                a.a = 0.5f;
                buttons[4].GetComponent<Image>().color = a;

                leaderBoardManager.instance.show();
            }
         
        }
      
    }
    public void closeLeaderBoard()
    {
        leader[1].SetActive(false);
        leader[0].SetActive(true);
        buttons[4].onClick.AddListener(() => openLeaderBoard());
        Color a = buttons[4].GetComponent<Image>().color;
        a.a = 1f;
        buttons[4].GetComponent<Image>().color = a;
        leaderBoardManager.instance.getCity.SetActive(false);
    }
    public void changeSoundPos(bool status)
    {
        if (status)
        {
            SoundControl[0].SetActive(true);
            SoundControl[1].SetActive(false);
            SoundControl[2].GetComponent<Button>().onClick.AddListener(() => changeSoundPos(false));
            AudioController.instance.canPlay = true;
            AudioController.instance.playbackground();
            
        }
        else
        {
            SoundControl[1].SetActive(true);
            SoundControl[0].SetActive(false);
            SoundControl[2].GetComponent<Button>().onClick.AddListener(() => changeSoundPos(true));
            AudioController.instance.canPlay = false;
            AudioController.instance.Stopbackground();
            
        }

    }

    public void Exitapp()
    {
        Application.Quit();
    }
}
