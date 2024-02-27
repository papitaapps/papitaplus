using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

using UnityEngine.SceneManagement;

public class mainData : MonoBehaviour
{
    public static mainData instance;
    [Header("Main Buttons")]
    public Button[] buttons;
    public string[] urls;
    public string[] Legend_urls;
   
    [Space(20)]
    [Header("Main objects")]
    public GameObject loading;
    public GameObject serverBox, handHelper;
    public firebaseEvents[] firebaseE;
    [Space(20)]
    [Header("position buttons")]

 
    public Scrollbar scroll;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
   void Start()
    {
        //  Debug.Log("user Token : " + UnityEngine.PlayerPrefs.GetString("user_token"));


        scroll.value = PlayerPrefs.GetFloat("scrollValueMain");

        
        

        for (int i = 0; i < urls.Length; i++)
        {
           
            urls[i] = PlayerPrefs.GetString("base_url") + Legend_urls[i];

            PlayerPrefs.SetString("my_Cat_Url_" + i.ToString(), urls[i]);
        }
       
        StartCoroutine(GetRequestAtFirst(PlayerPrefs.GetString("base_url") +"/categories"));
        #region makeButtons
         // En

            buttons[0].onClick.AddListener(() => openLevels(urls[0], "submenu", 0,0));
            buttons[1].onClick.AddListener(() => openLevels(urls[1], "submenu", 1,1));
            buttons[2].onClick.AddListener(() => openLevels(urls[2], "submenu", 2,2));
            buttons[3].onClick.AddListener(() => openLevels(urls[3], "submenu", 3,3));
            buttons[4].onClick.AddListener(() => openLevels(urls[4], "submenu", 4,4));
            buttons[5].onClick.AddListener(() => openLevels(urls[5], "submenu", 5,5));
            buttons[6].onClick.AddListener(() => openLevels(urls[6], "submenu", 6,6));
            buttons[7].onClick.AddListener(() => openLevels(urls[7], "submenu", 7,7));
            buttons[8].onClick.AddListener(() => openLevels(urls[8], "submenu", 8,8));
            buttons[9].onClick.AddListener(() => openLevels(urls[9], "submenu", 9,9));
            buttons[10].onClick.AddListener(() => openLevels(urls[10], "submenu", 10,10));
            buttons[11].onClick.AddListener(() => openLevels(urls[11], "submenu", 11,11));
            buttons[12].onClick.AddListener(() => openLevels(urls[12], "submenu", 12,12));
             buttons[13].onClick.AddListener(() => openLevels(urls[13], "submenu", 13,13));
            //  buttons[14].onClick.AddListener(() => openLevels(urls[14], "submenu", 14,14));
            //  buttons[15].onClick.AddListener(() => openLevels(urls[15], "submenu", 15,15));
            // buttons[16].onClick.AddListener(() => openLevels(urls[16], "submenu", 16,16));
            //  buttons[17].onClick.AddListener(() => openLevels(urls[17], "submenu", 17,17));
            // buttons[18].onClick.AddListener(() => openLevels(urls[16], "submenu", 18,18));
            // buttons[19].onClick.AddListener(() => openLevels(urls[16], "submenu", 19,19));
            

       
            // Fa

            buttons[20].onClick.AddListener(() => openLevels(urls[20], "submenu", 20,20));
            buttons[21].onClick.AddListener(() => openLevels(urls[21], "submenu", 21,21));
            buttons[22].onClick.AddListener(() => openLevels(urls[22], "submenu", 22,22));
            buttons[23].onClick.AddListener(() => openLevels(urls[23], "submenu", 23,23));
            buttons[24].onClick.AddListener(() => openLevels(urls[24], "submenu", 24,24));
            buttons[25].onClick.AddListener(() => openLevels(urls[25], "submenu", 25,25));
            buttons[26].onClick.AddListener(() => openLevels(urls[26], "submenu", 26,26));
            buttons[27].onClick.AddListener(() => openLevels(urls[27], "submenu", 27,27));
            buttons[28].onClick.AddListener(() => openLevels(urls[28], "submenu", 28,28));
            buttons[29].onClick.AddListener(() => openLevels(urls[29], "submenu", 29,29));
            buttons[30].onClick.AddListener(() => openLevels(urls[30], "submenu", 30,30));
            buttons[31].onClick.AddListener(() => openLevels(urls[31], "submenu", 31,31));
        //  buttons[12].onClick.AddListener(() => openLevels(urls[32], "submenu", 32,32));
        //  buttons[13].onClick.AddListener(() => openLevels(urls[33], "submenu", 33,33));
        //  buttons[14].onClick.AddListener(() => openLevels(urls[34], "submenu", 34,34));

        // buttons[15].gameObject.SetActive(false);
        // buttons[16].gameObject.SetActive(false);







        #endregion


        // make random animation for btns
        if (PlayerPrefs.HasKey("witchLang"))
        {
            int[] numbers = new int[4];
            GenerateRandomNumbers(numbers);
            Debug.Log(numbers[0] + ", " + numbers[1]);
            StartCoroutine(AnimateButton(0, numbers));
        }

    }
    IEnumerator AnimateButton(int index, int[] numbers)
    {
        buttons[numbers[index]].gameObject.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        if (index < numbers.Length - 1)
        {
            StartCoroutine(AnimateButton(index + 1, numbers));
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            buttons[numbers[2]].gameObject.GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(0.5f);
            buttons[numbers[3]].gameObject.GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(3.5f);
            if (SceneManager.GetActiveScene().name == "MainmenuGame" && PlayerPrefs.GetFloat("scrollValueMain") < 0.2f)
            {
                handHelper.SetActive(true);
            }
        }
    }
    private void GenerateRandomNumbers(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            int randomNumber;
            do
            {
                if(PlayerPrefs.GetInt("witchLang") == 1)
                    randomNumber = Random.Range(20, 31);
                else
                    randomNumber = Random.Range(0, 12);
            } while (ArrayContains(numbers, randomNumber));
            numbers[i] = randomNumber;
        }
    }

    private bool ArrayContains(int[] array, int number)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == number)
            {
                return true;
            }
        }
        return false;
    }


    string changeScene;
    int witchCat;
    public void openLevels(string url , string Scene,int witch,int witchButton)
    {
        InternetAvailabilityTest.instance.CheckConnection();
        //loading.transform.position = buttons[witch].transform.position;
        loading.SetActive(true);
        changeScene = Scene;
        PlayerPrefs.SetFloat("scrollValueMain", scroll.value);
        witchCat = witch; // کدام کتگوری را میخواهد ببیند
        Debug.Log("going to categori : " + witchCat);
        AudioController.instance.playSoundGameCategory(witch);
        StartCoroutine(GetRequest(url));
        for(int i = 0; i < buttons.Length; i++)
        {
            if (i == witchButton)
            {
                buttons[i].gameObject.GetComponent<Button>().enabled = false;
                buttons[i].gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.3f,0.3f,0);
                Color m = buttons[i].gameObject.GetComponent<Image>().color;
                m.a = 1;
                buttons[i].gameObject.GetComponent<Image>().color = m;
            }
            else
            {
                buttons[i].interactable = false;
             
                Color m = buttons[i].gameObject.GetComponent<Image>().color;
                m.a = 0.5f;
                buttons[i].gameObject.GetComponent<Image>().color = m;
            }
        }

        Firebase.Analytics.FirebaseAnalytics.LogEvent(firebaseE[witch].id, firebaseE[witch].name, 1);


    }

    IEnumerator GetRequest(string uri)
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
                InternetAvailabilityTest.instance.CheckConnection();
                loading.SetActive(false);
                for (int i = 0; i < buttons.Length; i++)
                {
                   
                        buttons[i].gameObject.GetComponent<Button>().enabled = true;
                        buttons[i].gameObject.GetComponent<Button>().interactable = true;
                        buttons[i].gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.2813326f, 0.2813326f, 0);
                        Color m = buttons[i].gameObject.GetComponent<Image>().color;
                        m.a = 1;
                        buttons[i].gameObject.GetComponent<Image>().color = m;
                   
                   
                }
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text.Contains("Unauthenticated"))
                {
                    PlayerPrefs.SetInt("with_Ad", 0);
                    PlayerPrefs.SetInt("MyDays", 0);
                    Debug.Log("user paride !!!!");

                    PlayerPrefs.DeleteAll();

                    SceneManager.LoadScene("intro");
#if UNITY_ANDROID && !UNITY_EDITOR
   introManager.instance._ShowAndroidToastMessage("اطلاعات شما موجود نیست ویا با دستگاه دیگری وارد شده اید");
#endif
                }
                else
                {
                    CheckData(webRequest.downloadHandler.text);
                }
                
            }
        }
    }

    public void CheckData(string _url)
    {

        // برای خواندن در اندروید قایل لینک در روت و اضافه کردن دات نت جیسون الزامی است
        var user = JsonConvert.DeserializeObject<List<RootPackages>>(_url);
   

        //ذخیره عنوان های داخل مانند فیلم و بازی
        for(int i = 0; i < user.Count; i++)
        {
            string nb = "categories" +i + "product" + i;
            PlayerPrefs.SetString(nb, user[i].name);
        }
       
        PlayerPrefs.SetInt("witchCat", witchCat);
        PlayerPrefs.SetInt("productCount", user.Count);// کتگوری انتخابی چند پروداکت دارد
        if(user.Count == 0)
        {
            // نره به سین بعدی چون توش خالیه
            for (int i = 0; i < buttons.Length; i++)
            {

                buttons[i].gameObject.GetComponent<Button>().enabled = true;
                buttons[i].gameObject.GetComponent<Button>().interactable = true;
                buttons[i].gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.2813326f, 0.2813326f, 0);
                Color m = buttons[i].gameObject.GetComponent<Image>().color;
                m.a = 1;
                buttons[i].gameObject.GetComponent<Image>().color = m;


            }
            loading.SetActive(false);
            PlayerPrefs.SetInt("has_bouth" + witchCat, 0);
        }

        else if (user.Count == 1)
        {
            PlayerPrefs.SetInt("has_bouth" + witchCat, 0);
            if (user[0].type == "movie")
            //if (user[0].name == "فیلم")
            {
                Debug.Log("has just movie ");
                PlayerPrefs.SetInt("productCountGame", 0);
                PlayerPrefs.SetInt("has_Game" + witchCat, 0);

                if (user[0].products.Count > 0)
                {
                    PlayerPrefs.SetInt("productCountMovie", user[0].products.Count); // پروداکت فیلم چند لینک دارد
                    
                    PlayerPrefs.SetInt("has_Movie" + witchCat, 1);
                    

                    for (int n = 0; n < user[0].products.Count; n++)
                    {
                        PlayerPrefs.SetInt("prouductIDMovie" + witchCat + n, user[0].products[n].id);
                        PlayerPrefs.SetString("MoveLink" + witchCat + n,  user[0].products[n].full_link_file);
                        PlayerPrefs.SetString("MoveLinkName" + witchCat + n, user[0].products[n].file_name);
                        PlayerPrefs.SetString("ScreenShotMovie" + witchCat + n, user[0].products[n].full_link_screenshot);
                        PlayerPrefs.SetString("ScreenShotMovieName" + witchCat + n, user[0].products[n].screenshot_name);
                        PlayerPrefs.SetString("MovieName" + witchCat + n, user[0].products[n].name);


                    }
                }
                else
                {
                    Debug.Log("no product ");
                    PlayerPrefs.SetInt("productCountMovie", 0);
                  
                    PlayerPrefs.SetInt("has_Movie" + witchCat, 0);
                    
                }

            }

            //if (user[s].type == "game") // ذخیره لینک های بازی
            else
            {
                Debug.Log("has just game ");
                PlayerPrefs.SetInt("has_Movie" + witchCat, 0);
                PlayerPrefs.SetInt("productCountMovie", 0);
                if (user[0].products.Count > 0)
                {

                    PlayerPrefs.SetInt("productCountGame", user[0].products.Count); // پروداکت بازی چند محصول دارد
                    PlayerPrefs.SetInt("has_Game" + witchCat, 1);
                  

                    for (int n = 0; n < user[0].products.Count; n++)
                    {
                        PlayerPrefs.SetInt("prouductID" + witchCat + n, user[0].products[n].id);
                        PlayerPrefs.SetString("GameLink" + witchCat + n,  user[0].products[n].full_link_file);
                        PlayerPrefs.SetString("GameLinkName" + witchCat + n, user[0].products[n].file_name);
                        PlayerPrefs.SetString("ScreenShotGame" + witchCat + n,  user[0].products[n].full_link_screenshot);
                        PlayerPrefs.SetString("ScreenShotGameName" + witchCat + n, user[0].products[n].screenshot_name);
                        PlayerPrefs.SetString("Game_Scene_Name" + witchCat + n, user[0].products[n].scene_name);
                        PlayerPrefs.SetString("Game_Save_Addres" + witchCat + n, user[0].products[n].save_address);
                        PlayerPrefs.SetString("Game_Del_Addres" + witchCat + n, user[0].products[n].del_address);
                        PlayerPrefs.SetString("Game_Version" + witchCat + n, user[0].products[n].version);
                        PlayerPrefs.SetString("GameName" + witchCat + n, user[0].products[n].name);


                    }

                }
                else
                {
                    PlayerPrefs.SetInt("productCountGame", 0);
                 
                    PlayerPrefs.SetInt("has_Game" + witchCat, 0);
                   
                }

            }

            MainMenuManager.instance.changeScene(changeScene);
        }
        else   // بیشتر از یک موضوع داریم
        {
            if (witchCat == 5 | witchCat == 25) // کتگوری 5 فقط فیلم داره
            {
                Debug.Log("cat 5 & 25 just has movies");

                for (int s = 0; s < user.Count; s++)
                {
                    if (user[s].products.Count > 0)
                    {
                        //////// تعداد اسکرول ویو های فیلم رو ست میکنیم برای بخش دانلودها
                        int howmoutchMovie = PlayerPrefs.GetInt("howmoutchMovie");
                        howmoutchMovie++;
                        PlayerPrefs.SetInt("howmoutchMovie", howmoutchMovie);
                        ////////

                        PlayerPrefs.SetInt("productCountMovie"+s, user[s].products.Count); // پروداکت فیلم چند لینک دارد
                        PlayerPrefs.SetInt("has_Movie" + witchCat+s, 1);
                        for (int n = 0; n < user[s].products.Count; n++)
                        {
                            PlayerPrefs.SetInt("prouductIDMovie" + witchCat +s+ n, user[s].products[n].id);
                            PlayerPrefs.SetString("MoveLink" + witchCat + s + n, user[s].products[n].full_link_file);
                            PlayerPrefs.SetString("MoveLinkName" + witchCat + s + n, user[s].products[n].file_name);
                            PlayerPrefs.SetString("ScreenShotMovie" + witchCat + s + n, user[s].products[n].full_link_screenshot);
                            PlayerPrefs.SetString("ScreenShotMovieName" + witchCat + s + n, user[s].products[n].screenshot_name);
                            PlayerPrefs.SetString("MovieName" + witchCat + s + n, user[s].products[n].name);


                        }
                    }
                    else
                    {
                        Debug.Log("no product ");
                        PlayerPrefs.SetInt("productCountMovie"+s, 0); // پروداکت فیلم چند لینک دارد
                        PlayerPrefs.SetInt("has_Movie" + witchCat+s, 0);
                    }
                }

                MainMenuManager.instance.changeScene(changeScene);
            }
            else
            {
                Debug.Log("has movie and game");
                PlayerPrefs.SetInt("has_bouth" + witchCat, 1);
                // ذخیره لینک ها
                for (int s = 0; s < user.Count; s++)
                {
                    Debug.Log("teeedad scrool view ha" + user.Count);
                    if (user[s].type == "movie") // ذخیره لینک های فیلم
                                                 // if (user[s].name == "فیلم")
                    {
                        Debug.Log("saving movie links ");


                        if (user[s].products.Count > 0)
                        {
                            PlayerPrefs.SetInt("productCountMovie", user[s].products.Count); // پروداکت فیلم چند لینک دارد
                            PlayerPrefs.SetInt("has_Movie" + witchCat, 1);
                            for (int n = 0; n < user[s].products.Count; n++)
                            {
                                PlayerPrefs.SetInt("prouductIDMovie" + witchCat + n, user[s].products[n].id);
                                PlayerPrefs.SetString("MoveLink" + witchCat + n, user[s].products[n].full_link_file);
                                PlayerPrefs.SetString("MoveLinkName" + witchCat + n, user[s].products[n].file_name);
                                PlayerPrefs.SetString("ScreenShotMovie" + witchCat + n, user[s].products[n].full_link_screenshot);
                                PlayerPrefs.SetString("ScreenShotMovieName" + witchCat + n, user[s].products[n].screenshot_name);
                                PlayerPrefs.SetString("MovieName" + witchCat + n, user[s].products[n].name);


                            }
                        }
                        else
                        {
                            Debug.Log("no product ");
                            PlayerPrefs.SetInt("productCountMovie", 0); // پروداکت فیلم چند لینک دارد
                            PlayerPrefs.SetInt("has_Movie" + witchCat, 0);
                        }

                    }

                    if (user[s].type == "game") // ذخیره لینک های بازی
                                                //  if (user[s].name == "بازی")
                    {

                        if (user[s].products.Count > 0)
                        {

                            PlayerPrefs.SetInt("productCountGame", user[s].products.Count); // پروداکت بازی چند محصول دارد
                            PlayerPrefs.SetInt("has_Game" + witchCat, 1);
                            for (int n = 0; n < user[s].products.Count; n++)
                            {
                                PlayerPrefs.SetInt("prouductID" + witchCat + n, user[s].products[n].id);
                                PlayerPrefs.SetString("GameLink" + witchCat + n, user[s].products[n].full_link_file);
                                PlayerPrefs.SetString("GameLinkName" + witchCat + n, user[s].products[n].file_name);
                                PlayerPrefs.SetString("ScreenShotGame" + witchCat + n, user[s].products[n].full_link_screenshot);
                                PlayerPrefs.SetString("ScreenShotGameName" + witchCat + n, user[s].products[n].screenshot_name);
                                PlayerPrefs.SetString("Game_Scene_Name" + witchCat + n, user[s].products[n].scene_name);
                                PlayerPrefs.SetString("Game_Save_Addres" + witchCat + n, user[s].products[n].save_address);
                                PlayerPrefs.SetString("Game_Del_Addres" + witchCat + n, user[s].products[n].del_address);
                                PlayerPrefs.SetString("Game_Version" + witchCat + n, user[s].products[n].version);
                                PlayerPrefs.SetString("GameName" + witchCat + n, user[s].products[n].name);


                            }

                        }
                        else
                        {
                            PlayerPrefs.SetInt("productCountGame", 0);
                            PlayerPrefs.SetInt("has_Game" + witchCat, 0);
                        }

                    }

                }

                MainMenuManager.instance.changeScene(changeScene);
            }
          
        }
     
       

    }


    ////////////////////////////////////////////////////// just first time
    
    IEnumerator GetRequestAtFirst(string uri)
    {
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
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
                if (webRequest.downloadHandler.text.Contains("Unauthenticated"))
                {
                    PlayerPrefs.SetInt("with_Ad", 0);
                    PlayerPrefs.SetInt("MyDays", 0);
                    Debug.Log("user paride !!!!");
#if UNITY_ANDROID && !UNITY_EDITOR
   introManager.instance._ShowAndroidToastMessage("اطلاعات شما موجود نیست ویا با دستگاه دیگری وارد شده اید");
#endif
                    PlayerPrefs.DeleteAll();
                  
                    SceneManager.LoadScene("intro");
                }
                else if ((webRequest.downloadHandler.text).Contains("Error"))
                {
                    serverBox.SetActive(true);
                }
                else
                {
                    CheckDataAtFirst(webRequest.downloadHandler.text);
                }
                    
            }
        }
       
    }
    public void CheckDataAtFirst(string _url)
    {

        // برای خواندن در اندروید قایل لینک در روت و اضافه کردن دات نت جیسون الزامی است
        var user = JsonConvert.DeserializeObject<List<RootCategoris>>(_url);

        PlayerPrefs.SetInt("categoriesCount", user.Count);
        Debug.Log(" we have " + user.Count + " categories");
        for (int i = 0; i < user.Count; i++)
        {
            string n = "categories" + i;
            PlayerPrefs.SetString(n, user[i].name);
        }

        

    }

   

  
}
[System.Serializable]
public class RootPackages
{
    public int id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string category_id { get; set; }
    public object created_at { get; set; }
    public object updated_at { get; set; }
    public List<ProductData> products { get; set; }
}
[System.Serializable]
public class RootCategoris
{
    public int id { get; set; }
    public string name { get; set; }
   
}
[System.Serializable]
public class firebaseEvents
{
    public string id;
    public string name;

}
[System.Serializable]
public class ProductData
{
    public int id { get; set; }
    public string file_link { get; set; }
    public string file_name { get; set; }
    public string name { get; set; }
    public string screenshot_link { get; set; }
    public string screenshot_name { get; set; }
    public string version { get; set; }
    public string package_id { get; set; }
    public object created_at { get; set; }
    public object updated_at { get; set; }
    public string scene_name { get; set; }
    public string save_address { get; set; }
    public string del_address { get; set; }
    public string full_link_file { get; set; }
    public string full_link_screenshot { get; set; }
}
