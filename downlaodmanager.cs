using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UPersian.Components;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.Android;

public class downlaodmanager : MonoBehaviour
{
    public static downlaodmanager instance;
    public GameObject[] menu;
    public GameObject[] buttons;
   
    public Button[] buttonsUI;
    public Animator menuUI;
    public Color loadToColor;
    public GameObject loading, netBox;
    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        if (Screen.width > 2300)
        {
            menuUI.Play("download menu 2400");
        }
        else
        {
            menuUI.Play("download menu 1920");
        }
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
    
        //back btn
        buttonsUI[0].onClick.AddListener(() => buttonCallBack("MainmenuGame", 1));
        changeMenu(0);

      
        if (Application.platform == RuntimePlatform.Android)
        {
            // Request the RECORD_AUDIO permission
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
            }

            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }

        }

      

    }

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
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
    private void buttonCallBack(string name, int doing)
    {
        AudioController.instance.playSound(1);
        if (doing == 1)
        {
           // changeScene(name);
            Initiate.Fade(name, loadToColor, 3f);
        }
        else if (doing == 2)
        {
            
        }
        else if (doing == 3)
        {
         
        }

    }
  
    public void changeScene(string scene)
    {
        AudioController.instance.playSound(1);
        SceneManager.LoadScene(scene);
       
    }
    public int witchMenuSelected;
    public void changeMenu(int a)
    {
        AudioController.instance.playSound(1);
        loading.SetActive(true);
        witchMenuSelected = a;
        StartCoroutine(GetRequestProducts(PlayerPrefs.GetString("my_Cat_Url_" + a)));
      
    }

    
    //////////////////////////////////   check products ////////////////////////////////////////////


    IEnumerator GetRequestProducts(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            webRequest.SetRequestHeader("Authorization", "Bearer " + UnityEngine.PlayerPrefs.GetString("user_token"));
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.chunkedTransfer = false;
            webRequest.certificateHandler = new BypassCertificate();
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
                loading.SetActive(false);
                netBox.SetActive(true);
            }
            else
            {
                // Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                CheckDataProducts(webRequest.downloadHandler.text);
            }
        }
    }
   
    public void openDeleteBox()
    {
        AudioController.instance.playSound(2);
    }
    public void CheckDataProducts(string _url)
    {

        // برای خواندن در اندروید قایل لینک در روت و اضافه کردن دات نت جیسون الزامی است
        var user = JsonConvert.DeserializeObject<List<RootPackages>>(_url);


        // ذخیره لینک ها
        for (int s = 0; s < user.Count; s++)
        {

            //if (user[s].type == "game") // ذخیره لینک های بازی
            if (user.Count == 0)
            {
               // menu[witchMenuSelected].gameObject.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<DynamicScroolViewDownloaded>().counterMenu = 0;

            }
            else if (user.Count == 1)
            {
                if (user[s].type == "game")
                {

                  //  menu[witchMenuSelected].gameObject.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<DynamicScroolViewDownloaded>().counterMenu = user[s].products.Count;

                }
                else
                {
                   // menu[witchMenuSelected].gameObject.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<DynamicScroolViewDownloaded>().counterMenu = user[s].products.Count;

                }
            }
            else
            {
                int pluses =0;
                for(int i=0; i< user.Count; i++)
                {
                    pluses = pluses + user[s].products.Count;
                }
               // menu[witchMenuSelected].gameObject.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<DynamicScroolViewDownloaded>().counterMenu = pluses;

            }


        }
        // پیش فرض در نظر گرفتیم که 50 بازی بیشتر نداریم
         menu[witchMenuSelected].gameObject.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<DynamicScroolViewDownloaded>().counterMenu = 50;


        GameObject[] killOldButton;
        killOldButton = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < killOldButton.Length; i++)
        {
            Destroy(killOldButton[i].gameObject);
        }
        PlayerPrefs.SetInt("witchCatDownlaodMenu", witchMenuSelected);
        for (int i = 0; i < menu.Length; i++)
        {
            if (witchMenuSelected == i)
            {
                menu[i].SetActive(true);
                Image b = buttons[i].GetComponent<Image>();
                Color cb = b.color;
                cb.a = 1;
                b.color = cb;
                buttons[i].transform.localScale = new Vector3(1.2f, 1.2f, 1);
                playGameScript.pgs.changeLinks(i );
                menu[i].transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<RtlText>().text = PlayerPrefs.GetString("categories" + i);
                menu[i].transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<RtlText>().fontSize = 40;
            }
            else
            {
                menu[i].SetActive(false);
                Image b = buttons[i].GetComponent<Image>();
                Color cb = b.color;
                cb.a = 0.6f;
                b.color = cb;
                buttons[i].transform.localScale = new Vector3(1f, 1f, 1);
            }

        }
        menu[witchMenuSelected].gameObject.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<DynamicScroolViewDownloaded>().WitchCat = witchMenuSelected;

        menu[witchMenuSelected].gameObject.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<DynamicScroolViewDownloaded>().preLoad();
        menu[witchMenuSelected].gameObject.transform.GetChild(0).GetChild(2).transform.gameObject.GetComponent<DynamicScroolViewDownloaded>().loadDataOnBtn();

        loading.SetActive(false);

    }
}
