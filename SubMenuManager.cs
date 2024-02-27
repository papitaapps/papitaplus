using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UPersian.Components;

public class SubMenuManager : MonoBehaviour
{
    public static SubMenuManager instance;
    public Animator menuUI;
    public RtlText menuName;
    public List<GameObject> prouduct;
    public List<RtlText> ProuductName;
    public Color loadToColor;
    int witchPackage;
    public GameObject nullGame,boxUpdate,LockBox, boxNetIcon,boxNeedsUpdate,introPage;
    public GameObject[] dogsIntro;
    public RtlText nullText, PlayerPosition, PlayerCoins;
    [HideInInspector]
    public int DownloadFinished;
    public AudioClip m_audio;
    public AudioSource audioSource;
    public Color[] backgrounds;
    public Sprite[] background_image,headersImage, uiSprite;
    public Image m_back,header,ui_buttons_home,ui_buttons_buy;
    [HideInInspector] public bool lastItemCreated;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        /*
        if (Screen.width > 2300)
        {
            menuUI.Play("submenu menu 2400");
        }
        else
        {
            menuUI.Play("submenu menu 1920");
        }
        */
    }
    void Start()
    {
       StartCoroutine(introToPage());
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = m_audio;

        
        int withAd = PlayerPrefs.GetInt("with_Ad");
      
        if (withAd == 0)
            PlayerPosition.text = "0";
        else
            PlayerPosition.text = PlayerPrefs.GetString("player_Position");

        PlayerCoins.text = UnityEngine.PlayerPrefs.GetInt("coins").ToString();

        if (PlayerPrefs.GetInt("comeForBuy") == 1) // az setting omade bekhare
        {
            PlayerPrefs.SetInt("comeForBuy", 0);
            LockBox.SetActive(true);
            audioSource.Play();
        }

        if (AudioController.instance.canPlay & !AudioController.instance.audiosource[0].isPlaying)
        {
            AudioController.instance.playbackground();
        }

        float currentVersion = float.Parse(Application.version);
        float minorVersion = PlayerPrefs.GetFloat("minorVersion");

        // اپدیت جدید منتشر شده اما فورس نیست
        if (currentVersion < minorVersion)
        {
            boxNeedsUpdate.SetActive(true);
            setBtn();
            boxNeedsUpdate.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(body));
        }

        StartCoroutine(GetAccPay(PlayerPrefs.GetString("base_url") + "/account/tariffs"));

        makeBackground();

        loadToColor = backgrounds[PlayerPrefs.GetInt("witchCat")];

        if (withAd == 1 & AdiveryConfig.instance.BannerIsShowing)
            AdiveryConfig.instance.OnBannerAdDestroy();
    }
    IEnumerator introToPage()
    {
        int witchCat = PlayerPrefs.GetInt("witchCat");
        introPage.GetComponent<Image>().color = backgrounds[witchCat];
        introPage.SetActive(true);

        if (witchCat == 5 || witchCat == 25)
        {
            for (int i = 0; i < dogsIntro.Length; i++)
            {
                dogsIntro[i].SetActive(false);
            }
            dogsIntro[2].SetActive(true);
           
        }
        else
        {
            int w = Random.Range(0, dogsIntro.Length);
            for (int i = 0; i < dogsIntro.Length; i++)
            {
                dogsIntro[i].SetActive(i == w);
            }
           
        }
        yield return new WaitUntil(() => lastItemCreated); // az scrollvirew item mikhone
        introPage.SetActive(false);
    }
    void makeBackground()
    {
        int witch = PlayerPrefs.GetInt("witchCat");

        m_back.sprite = background_image[witch];
        header.sprite = headersImage[witch];
        ui_buttons_home.sprite = uiSprite[witch];
        ui_buttons_buy.sprite = uiSprite[witch];
    }
    public void getProducts()
    {
        int witchCat = PlayerPrefs.GetInt("witchCat");
        int categoriesCount = PlayerPrefs.GetInt("categoriesCount");
        int productCount = PlayerPrefs.GetInt("productCount");

        for (int s = 0; s < categoriesCount; s++)
        {
            if (witchCat == s)
            {
                menuName.text = PlayerPrefs.GetString("categories" + s);
            }
        }

        for (int i = 0; i < prouduct.Count; i++)
        {
            prouduct[i].SetActive(i < productCount);
        }

        for (int i = 0; i < ProuductName.Count; i++)
        {
            if (i < productCount)
            {
                ProuductName[i].text = PlayerPrefs.GetString("categories" + i + "product" + i);
                //Debug.Log(ProuductName[i].text + "    " + ProuductName[i].text.Length);

                if (ProuductName[i].text.Length > 13)
                {
                    ProuductName[i].fontSize = 30;
                }
            }
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

                Initiate.Fade("MainmenuGame", loadToColor, 3f);
            }
        }
    }
    int CountMovie = 0;
    int CountGame = 0;

    IEnumerator GetAccPay(string urlCheck)
    {
        Debug.Log("token : " + UnityEngine.PlayerPrefs.GetString("user_token"));
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlCheck))
        {

            webRequest.SetRequestHeader("Authorization", "Bearer " + UnityEngine.PlayerPrefs.GetString("user_token"));
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.chunkedTransfer = false;
            webRequest.certificateHandler = new BypassCertificate();
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = urlCheck.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                
            }
            else
            {


                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text.Contains("Unauthenticated"))
                {
                    Debug.Log("user paride !!!");
#if UNITY_ANDROID && !UNITY_EDITOR
   introManager.instance._ShowAndroidToastMessage("اطلاعات کاربر پیدا نشد لطفا دوباره وارد شوید");
#endif
                }
                else
                {
                    if (string.IsNullOrEmpty(webRequest.downloadHandler.text))
                    {
                        // اتمام اشتراک
                        PlayerPrefs.SetInt("MyDays", 0);
                        PlayerPrefs.SetInt("with_Ad", 0);

                    }
                    else
                    {
                        CheckPayData(webRequest.downloadHandler.text);


                    }
                }


            }
        }

    }
    int howmoutchScrollview, countercheckScrollviewCreated;
    public void howMoutchScrollView(int a)
    {
        howmoutchScrollview = a;
        Debug.Log("howmoutchScrollview : " + howmoutchScrollview);
    }
    public void checkScrollviewCreated()
    {
        
        if(countercheckScrollviewCreated< howmoutchScrollview-1)
        {
            countercheckScrollviewCreated++;
        }
        else
        {
            lastItemCreated = true;
        }
        Debug.Log(countercheckScrollviewCreated);
    }
    public void CheckPayData(string _url)
    {

        // برای خواندن در اندروید قایل لینک در روت و اضافه کردن دات نت جیسون الزامی است
        var user = JsonConvert.DeserializeObject<RootCheckPayment>(_url);


            if (user.countday == null)
            {
                
                // BuyButton[0].gameObject.SetActive(false);
                PlayerPrefs.SetInt("MyDays", 0);
                PlayerPrefs.SetInt("with_Ad", 0);
            }
            else
            {
                PlayerPrefs.SetInt("MyDays", int.Parse(user.countday));
               // Debug.LogError("day of user : " + PlayerPrefs.GetInt("MyDays"));
               // Debug.Log("start time : " + user.start_day);
              //  Debug.Log("start time : " + user.end_day);
              //  PlayerPrefs.SetString("start_Day", user.start_day);
              //  PlayerPrefs.SetString("end_Day", user.end_day);
                if (PlayerPrefs.GetInt("MyDays") > 0)
                {


                    PlayerPrefs.SetInt("loginSucces", 2);
                    PlayerPrefs.SetInt("with_Ad", 1);
                }
                else
                {
                  
                    PlayerPrefs.SetInt("loginSucces", 1);
                    PlayerPrefs.SetInt("with_Ad", 0);
                }
            }
        






    }
    string body;
    void setBtn()
    {
        int store = UnityEngine.PlayerPrefs.GetInt("store");
        string storeUrl;

        switch (store)
        {
            case 1:
                storeUrl = "https://play.google.com/store/apps/details?id=" + Application.identifier;
                break;
            case 2:
                storeUrl = "https://cafebazaar.ir/app/" + Application.identifier;
                break;
            case 3:
                storeUrl = "https://myket.ir/app/" + Application.identifier;
                break;
            case 5:
                storeUrl = "https://toopmarket.com/app/" + Application.identifier;
                break;
            default:
                storeUrl = "https://play.google.com/store/apps/details?id=" + Application.identifier;
                break;
        }

        body = storeUrl;
    }

    public void changeScene(string Scene)
    {
        Time.timeScale = 0f; // توقف اجرای کدها
        Resources.UnloadUnusedAssets(); // خالی کردن حافظه رم

        if (GameObject.Find("Audio Controller")) 
            AudioController.instance.playSound(1);

        Time.timeScale = 1f;
        // Initiate.Fade(Scene, loadToColor, 3f);
        GetComponent<LevelLoader>().LoadLevel(Scene);
    }
}
[System.Serializable]


public class RootGetLinks
{
    public int id { get; set; }
    public string file_link { get; set; }
    public string file_name { get; set; }
    public string screenshot_link { get; set; }
    public string screenshot_name { get; set; }
    public string package_id { get; set; }
    public object created_at { get; set; }
    public object updated_at { get; set; }
}
