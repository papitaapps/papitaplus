using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UPersian.Components;
using Newtonsoft.Json;

public class settingController : MonoBehaviour
{
    
    public Transform menu2400, menu1920;
    public GameObject backPos;
    public string[] numbersAlphabet;
    public int[] numbers;
    string[] myalphabetnumbers = {"a","b","c" };
    int[] random = { 1, 2, 3 };
    private Text question;
    private Text Answers;
     public Text userCode;
    public GameObject questionBox1, questionBox2;
    string textToCopy;
    [SerializeField] InputField inputField,inputPhone;
    int counteranswer = 0;
    private type typing;

    public GameObject[] ages,gender;
   
    public RtlText oldParentsName, OldChildName, message,childrenName,parentsName,
        Days,baste,startDay,endDay,inviteCode, inviteCode_pepale,howManyDays;
  
    //public Text childrenName;
    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (Screen.height > 2300)
        {
            Debug.Log("screen : 2400");
            backPos.transform.position = menu2400.position;
            questionBox1.SetActive(true);
            typing = GameObject.Find("question box 2400").GetComponent<type>();
        }
        else
        {
            Debug.Log("screen : 1920");
            backPos.transform.position = menu1920.position;
            questionBox2.SetActive(true);
            typing = GameObject.Find("question box 1920").GetComponent<type>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        question = GameObject.Find("question").GetComponent<Text>();
        Answers = GameObject.Find("answer").GetComponent<Text>();
        random[0] = Random.Range(0, 9);
        random[1] = Random.Range(0, 8);
        random[2] = Random.Range(0, 8);
        myalphabetnumbers[0] = numbersAlphabet[random[2]];
        myalphabetnumbers[1] = numbersAlphabet[random[1]];
        myalphabetnumbers[2] = numbersAlphabet[random[0]];

        Debug.Log(random[0] + " "+random[1] +" "+ random[2]);
        question.text =  myalphabetnumbers[2] + "  -  " + myalphabetnumbers[1] + "  -  " + myalphabetnumbers[0];

        NewGenderChild = PlayerPrefs.GetInt("gender");
        inviteCode.text = PlayerPrefs.GetString("user_InviteCode");
        setPersonalInfo();

        if (PlayerPrefs.GetInt("MyDays") > 0)
        {
            howManyDays.text = PlayerPrefs.GetInt("MyDays").ToString() + " روز باقی مانده " ;
        }

        textToCopy = UnityEngine.PlayerPrefs.GetString("m_sarial");
    }
    public void setPayAccountInfo()
    {
        Days.text = PlayerPrefs.GetInt("MyDays").ToString();
        inviteCode_pepale.text = PlayerPrefs.GetString("user_details_InviteCode");
        //baste.text = 
    }
    public void setPersonalInfo()
    {
        //set age and name and gender
       
        OldChildName.text =  PlayerPrefs.GetString("ChildrenName") + " از " + PlayerPrefs.GetString("city");
       

        if (CheckForNotLatin(OldChildName.text))
        {
            OldChildName.alignment = TextAnchor.MiddleLeft;

        }
        if (PlayerPrefs.HasKey("ParentsName"))
        {
            oldParentsName.text = "نام شما : " + PlayerPrefs.GetString("ParentsName");

            if (CheckForNotLatin(oldParentsName.text))
            {
                oldParentsName.alignment = TextAnchor.MiddleLeft;
            }
        }
        else
        {
            oldParentsName.text = "نام خود را وارد نکرده اید";
        }
       

        int myAge = PlayerPrefs.GetInt("age");

        for (int i = 0; i < ages.Length; i++)
        {

            if (i == myAge - 1)
            {

                ages[i].GetComponent<Outline>().enabled = true;
                Debug.Log("hisAge " + PlayerPrefs.GetInt("age"));

            }
            else
            {
                ages[i].GetComponent<Outline>().enabled = false;
            }
        }

        if (PlayerPrefs.GetInt("gender") == 1)
        {
            Debug.Log("gender " + "male");
            gender[0].SetActive(false);
            gender[1].SetActive(true);
            gender[2].SetActive(true);
            gender[3].SetActive(false);
        }
        else
        {
            Debug.Log("gender " + "female");
            gender[0].SetActive(true);
            gender[1].SetActive(false);
            gender[2].SetActive(false);
            gender[3].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure user is on Android platform
            if (Application.platform == RuntimePlatform.Android)
            {

                // Check if Back was pressed this frame
                if (Input.GetKeyDown(KeyCode.Escape))
                {

                    Initiate.Fade("MainmenuGame", Color.white, 3f);
                }
            }
        
        if (counteranswer == 3 )
        {
            counteranswer = 0;
            Invoke("deleteAnswer", 0.75f);
         
        }

        if (message.text.Length > 0)
        {
            int code = (int)message.text[0];
            if ((code > 96 && code < 123) || (code > 64 && code < 91))
            {
                message.alignment = TextAnchor.UpperLeft;
            }
            else
            {
                message.alignment = TextAnchor.UpperRight;
            }
        }

        if (childrenName.text.Length > 0)
        {
            int code1 = (int)childrenName.text[0];
            if ((code1 > 96 && code1 < 123) || (code1 > 64 && code1 < 91))
            {
                childrenName.alignment = TextAnchor.MiddleLeft;
            }
            else
            {
                childrenName.alignment = TextAnchor.MiddleRight;
            }
        }
        if (parentsName.text.Length > 0)
        {
            int code2 = (int)parentsName.text[0];
            if ((code2 > 96 && code2 < 123) || (code2 > 64 && code2 < 91))
            {
                parentsName.alignment = TextAnchor.MiddleLeft;
            }
            else
            {
                parentsName.alignment = TextAnchor.MiddleRight;
            }
        }

       
        

    }
    public void countBtnAnswer()
    {
        counteranswer = counteranswer + 1;
    }
    void deleteAnswer()
    {
        typing.del();
        typing.del();
        typing.del();
    }
    public void checkanswers()
    {
        string userAns = Answers.text;
        string myAns = (random[2] + 1).ToString() + (random[1]+1).ToString() + (random[0] + 1).ToString();
        Debug.Log(userAns+"="+myAns);
        if (myAns == userAns)
        {
            questionBox1.SetActive(false);
            questionBox2.SetActive(false);

            
            StartCoroutine(GetRequestAtFirst(PlayerPrefs.GetString("base_url") +"/account/user"));
            StartCoroutine(GetRequestAtFirst2(PlayerPrefs.GetString("base_url") + "/user/settings"));
            StartCoroutine(GetRequestAtFirst3(PlayerPrefs.GetString("base_url") + "/settings"));
            inviteCode.text = PlayerPrefs.GetString("user_InviteCode");
        }
        else
        {
          
        }
    }
    public void getAgain()
    {
        StartCoroutine(GetRequestAtFirst(PlayerPrefs.GetString("base_url") +"/account/user"));
        StartCoroutine(GetRequestAtFirst2(PlayerPrefs.GetString("base_url") + "/user/settings"));
        StartCoroutine(GetRequestAtFirst3(PlayerPrefs.GetString("base_url") + "/settings"));
    }
    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    string text;
    public void checkAlphabetName()
    {
        text = inputField.text;
        inputField.text = Regex.Replace(text, @"[^a-zA-Z ]", "");
        // inputField.text = Regex.Replace(text, @"[^a-zA-Z0-9 *#@]", "");
    }
    int wordIndex = -1;
    public void checkphoneNumber()
    {
        text = inputPhone.text;
        inputPhone.text = Regex.Replace(text, @"[^0-9 ]", "");
        if (wordIndex >= 9)
        {
           if(GameObject.Find("Button Send Cod")) GameObject.Find("Button Send Cod").GetComponent<Button>().interactable = true;
        }
        else
        {
            wordIndex++;
        }
        // inputField.text = Regex.Replace(text, @"[^a-zA-Z0-9 *#@]", "");
    }
   
    public void SavePersonalInformation()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("در حال ارسال اطلاعات");
#endif
        sendParent();

        saveChildereninfo = true;
    }
    int parents;
    public void MomOrDad(int a)
    {
        parents = a;
        if (a == 1)
        {
            //mom
            GameObject.Find("mom").GetComponent<Image>().color = Color.white;
            GameObject.Find("dad").GetComponent<Image>().color = Color.gray;
      


        }
        else
        {
            //dad
            GameObject.Find("dad").GetComponent<Image>().color = Color.white;
            GameObject.Find("mom").GetComponent<Image>().color = Color.gray;
       
        }

    }
    public void openLogOutBox()
    {
        AudioController.instance.playSound(2);
    }
    public void ShowPersonalInformation(GameObject myobject)
    {
        myobject.SetActive(true);
        myobject.GetComponent<Animator>().Play("move in");
        GameObject.Find("mom").GetComponent<Image>().color = Color.white;
        GameObject.Find("dad").GetComponent<Image>().color = Color.white;

       
        if (PlayerPrefs.HasKey("parent"))
        {
            if (PlayerPrefs.GetInt("parent") == 1)
            {
                //mom
                GameObject.Find("mom").GetComponent<Image>().color = Color.white;
                GameObject.Find("dad").GetComponent<Image>().color = Color.gray;
            }
            else
            {
                //dad
                GameObject.Find("dad").GetComponent<Image>().color = Color.white;
                GameObject.Find("mom").GetComponent<Image>().color = Color.gray;
            }
           
        }
    
        GameObject.Find("InputField phone").GetComponent<InputField>().text = PlayerPrefs.GetString("phoneNumber");
        if(AudioController.instance !=null) AudioController.instance.playSound(1);
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    int NewGenderChild,newAgeChild;
    bool saveChildereninfo=false;
    public void selectAge(int a)
    {
        for (int i = 0; i < ages.Length; i++)
        {
            if (a == 8)
            {
                PlayerPrefs.SetInt("age", 100);
                ages[i].GetComponent<Outline>().enabled = true;
                Debug.Log("hisAge " + PlayerPrefs.GetInt("age"));
            }
           else if (i == a & a!=8)
            {
                PlayerPrefs.SetInt("age", a + 1);
                ages[i].GetComponent<Outline>().enabled = true;
                Debug.Log("hisAge " + PlayerPrefs.GetInt("age"));

            }
            else
            {
                ages[i].GetComponent<Outline>().enabled = false;
            }
        }

        newAgeChild = a + 1;
    }
   
    public void SaveChildInfo()
    {
        StartCoroutine(rgisterUserData(false));
    }
    void savingChild(bool comeFromParentSection)
    {
        if (NewGenderChild == 0)
        {
            NewGenderChild = PlayerPrefs.GetInt("gender");
        }
        if (newAgeChild == 0)
        {
            newAgeChild = PlayerPrefs.GetInt("age");
        }
        PlayerPrefs.SetInt("loginSucces", 2);
        if (!comeFromParentSection)
        {
            SaveChildrenInfo(NewGenderChild, newAgeChild);
            saveChildereninfo = true;
        }
      
    }
    void SaveChildrenInfo(int a,int b)
    {
        if (!string.IsNullOrEmpty(GameObject.Find("InputFieldname").GetComponent<InputField>().text))
        {
            PlayerPrefs.SetString("ChildrenName", GameObject.Find("InputFieldname").GetComponent<InputField>().text);
        }
       
        if (!string.IsNullOrEmpty(GameObject.Find("InputFieldcity").GetComponent<InputField>().text))
        {
            PlayerPrefs.SetString("city", GameObject.Find("InputFieldcity").GetComponent<InputField>().text);
        }
           
        Debug.Log(PlayerPrefs.GetString("ChildrenName"));
        PlayerPrefs.SetInt("age", b);
        PlayerPrefs.SetInt("gender", a);

       
    }
    private string name;


    IEnumerator rgisterUserData(bool comeFromParentsSection)
    {
        //  Level 2


        string UserMobile = UnityEngine.PlayerPrefs.GetString("phoneNumber");




        string clientID = UnityEngine.PlayerPrefs.GetString("m_sarial");

        if (comeFromParentsSection)
        {
             name = PlayerPrefs.GetString("ChildrenName");
        }
        else
        {
            if (GameObject.Find("InputFieldname").GetComponent<InputField>().text.Length >= 3)
            {
                 name = GameObject.Find("InputFieldname").GetComponent<InputField>().text;
                 PlayerPrefs.SetString("ChildrenName", name);
            }
            else
            {
                name = PlayerPrefs.GetString("ChildrenName");
            }
           
        }
        


        string newpassword = UnityEngine.PlayerPrefs.GetString("password");

        string age = PlayerPrefs.GetInt("age").ToString();

        string favourite = "[" + PlayerPrefs.GetString("favourite") + "100]";

        WWWForm form = new WWWForm();

        form.AddField("name", name);
        form.AddField("mobile", UnityEngine.PlayerPrefs.GetString("phoneNumber"));
        form.AddField("client_id", UnityEngine.PlayerPrefs.GetString("m_sarial"));
        //form.AddField("questions", qusetion);
        form.AddField("age", age);
        form.AddField("favorites", favourite);
        if (PlayerPrefs.GetInt("gender") == 1)
        {
            
            form.AddField("gender", "male");
        }
        else
        {
            form.AddField("gender", "female");
            
        }
        
        form.AddField("password", newpassword);

       
        using (var www = UnityWebRequest.Post(PlayerPrefs.GetString("base_url") + "/account/register", form))
        {
            www.SetRequestHeader("Accept", "application/json");
            www.chunkedTransfer = false;
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();



            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            if ((www.downloadHandler.text).Contains("Error"))
            {
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("اینترنت متصل نیست");
#endif
            }
            else if (System.String.IsNullOrEmpty(www.downloadHandler.text))
            {
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("عدم ارتباط با سرور");
#endif
            }
            else
            {
               if(www.downloadHandler.text.Contains("user exists"))
                {
                    Debug.Log("tekrari");
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("این شماره تماس تکراری است");
#endif
                }
                else if (www.downloadHandler.text.Contains("user not found"))
                {
                    Debug.Log("cant change");
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("امکان پذیز نیست با پشتیبانی برای تغییر شماره تماس بگیرید");
#endif
                }
                else
                {
                    savingChild(comeFromParentsSection);
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("اطلاعات با موفقیت ذخیره شد");
#endif
                }




            }
        }





    }

    public void backBtnChildernInfo()
    {
        if (!saveChildereninfo)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("ذخیره نشد !");
#endif
            saveChildereninfo = false;
        }

    }
    public void selectGender(int a)
    {
        NewGenderChild = a;
        if (a == 1)
        {
            gender[0].SetActive(false);
            gender[1].SetActive(true);
            gender[2].SetActive(true);
            gender[3].SetActive(false);
        }
        else
        {
            gender[0].SetActive(true);
            gender[1].SetActive(false);
            gender[2].SetActive(false);
            gender[3].SetActive(true);
        }
    }



    ///////////////////////////////////////////////// log out ////////////////////////////////////////////////////////////////


    public void LogOut()
    {

        StartCoroutine(logoutUser());
    }

    IEnumerator logoutUser()
    {
        WWWForm form = new WWWForm();
        using (UnityWebRequest webRequest = UnityWebRequest.Post(PlayerPrefs.GetString("base_url") +"/account/logout",form))
        {
            // Request and wait for the desired page.
            webRequest.SetRequestHeader("Authorization", "Bearer " + UnityEngine.PlayerPrefs.GetString("user_token"));
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.chunkedTransfer = false;
            webRequest.certificateHandler = new BypassCertificate();
            yield return webRequest.SendWebRequest();

     

            if (webRequest.isNetworkError)
            {
                InternetAvailabilityTest.instance.CheckConnection();
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                CheckForLogOut(webRequest.downloadHandler.text);
            }
        }
        

    }

    public void CheckForLogOut(string _url)
    {
      

        var user = JsonConvert.DeserializeObject<RootLogout>(_url);

        if (user.status == 200 | user.message.Contains("Unauthenticated") )
        {
            Debug.Log("Loged Out");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("logouted", 1);
#if UNITY_ANDROID && !UNITY_EDITOR
   introManager.instance._ShowAndroidToastMessage("با موفقیت خارج شدید");
#endif
            Initiate.Fade("intro", Color.grey, 3f);
        }
        else
        {
            Debug.Log("dont log out");
#if UNITY_ANDROID && !UNITY_EDITOR
   introManager.instance._ShowAndroidToastMessage("خروج ناموفق! اینترنت خود را چک کنید");
#endif
        }
    

    }
    public void goToIntro()
    {
        
        PlayerPrefs.SetInt("comeForBuy", 1);
        SceneManager.LoadScene("submenu");
    }
   
    public void CopyToClipboard()
    {



        userCode.text = "user Code :" + textToCopy;
        // دسترسی به کلاس ClipboardManager در سیستم عامل اندروید
        AndroidJavaObject unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject clipboardManager = unityActivity.Call<AndroidJavaObject>("getSystemService", "clipboard");

        // ایجاد یک شیء ClipData جدید با متن مورد نظر
        AndroidJavaClass clipDataClass = new AndroidJavaClass("android.content.ClipData");
        AndroidJavaObject clipData = clipDataClass.CallStatic<AndroidJavaObject>("newPlainText", "text", textToCopy);

        // کپی کردن متن به کلیپ بورد
        clipboardManager.Call("setPrimaryClip", clipData);

        Debug.Log("Text copied to clipboard: " + textToCopy);
#if UNITY_IOS
        userCode.text = "user Code :" + textToCopy;
       TouchScreenKeyboard keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
            keyboard.text = textToCopy;
            keyboard.active = true;
            keyboard.text = textToCopy;
            keyboard.active = false;
#endif

        Debug.Log("Clipboard copy is not supported on this platform.");




    }
    public class userLogOut
    {
        public string token;


    }

    public void Quit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


    public void moveOut(GameObject obj){


        StartCoroutine(changes(obj));
    }

    private IEnumerator changes(GameObject myObj)
    {
        myObj.GetComponent<Animator>().Play("move out");
            yield return new WaitForSeconds(0.85f);
        myObj.SetActive(false);
    }

    bool CheckForNotLatin(string stringToCheck)
    {
        bool boolToReturn = false;
        foreach (char c in stringToCheck)
        {
            int code = (int)c;
            // for lower and upper cases respectively
            if ((code > 96 && code < 123) || (code > 64 && code < 91))
                boolToReturn = true;
            // visit http://www.dotnetperls.com/ascii-table for more codes
        }
        return boolToReturn;
    }

    ///////////////////////////////////////////////// get data /////////////////////////////////////////////////////////////
   


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
                startDay.text = "عدم ارتباط با سرور";
                endDay.text = "باپشتیبانی تماس بگیرید";
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
                    if (PlayerPrefs.GetInt("MyDays") > 0)
                    {
                        startDay.text = "شروع : " + PlayerPrefs.GetString("start_Day");
                        endDay.text = "پایان : " + PlayerPrefs.GetString("end_Day");
                        CheckDataAtFirst(webRequest.downloadHandler.text);
                    }
                    else
                    {
                        startDay.text = PlayerPrefs.GetString("ChildrenName") + " عزیزم ";
                        endDay.text = "حساب شما تمام شده";
                    }
                    
                }

            }
        }

    }
    IEnumerator GetRequestAtFirst2(string uri)
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
                if ((webRequest.downloadHandler.text).Contains("Error"))
                {
                    // serverBox.SetActive(true);
                }
                else
                {
                    CheckDataAtFirst2(webRequest.downloadHandler.text);
                }

            }
        }

    }
    IEnumerator GetRequestAtFirst3(string uri)
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
                if ((webRequest.downloadHandler.text).Contains("Error"))
                {
                    // serverBox.SetActive(true);
                }
                else
                {
                    CheckDataAtFirst3(webRequest.downloadHandler.text);
                }

            }
        }

    }
    public void CheckDataAtFirst(string _url)
    {
       // PlayerPrefs.SetString("ParentsName", "aa");
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("ParentsName")))
        {
            // برای خواندن در اندروید قایل لینک در روت و اضافه کردن دات نت جیسون الزامی است
            var user = JsonConvert.DeserializeObject<RootSettingBeforeParentsInsert>(_url);


           

            Debug.Log(user.user.age);
            PlayerPrefs.SetInt("age", int.Parse(user.user.age));
            PlayerPrefs.SetString("ChildrenName", user.user.name);
            if (user.user.gender == "male")
            {
                PlayerPrefs.SetInt("gender", 1);
            }
            else
            {
                PlayerPrefs.SetInt("gender", 2);
            }

       
        }
        else
        {
            // برای خواندن در اندروید قایل لینک در روت و اضافه کردن دات نت جیسون الزامی است
            var user = JsonConvert.DeserializeObject<RootSettingAfterParentsInsert>(_url);

            Debug.Log(user.user.invite_code);
            PlayerPrefs.SetString("user_InviteCode", user.user.invite_code);
            PlayerPrefs.SetString("user_details_InviteCode", user.user.number_of_invitations);
            inviteCode.text = PlayerPrefs.GetString("user_InviteCode");

            Debug.Log(user.user.age);
            PlayerPrefs.SetInt("age", int.Parse(user.user.age));
            PlayerPrefs.SetString("ChildrenName", user.user.name);
            if (user.user.gender == "male")
            {
                PlayerPrefs.SetInt("gender", 1);
            }
            else
            {
                PlayerPrefs.SetInt("gender", 2);
            }

           
                PlayerPrefs.SetString("ParentsName", user.user.settings[0].value);
            
            Debug.Log(user.user.settings[0].value);
            if (user.user.settings[1].value == "mother")
            {
                PlayerPrefs.SetInt("parent", 1);
            }
            else if (user.user.settings[1].value == "father")
            {
                PlayerPrefs.SetInt("parent", 2);
            }

        }

    }
    public void CheckDataAtFirst2(string _url)
    {

        // برای خواندن در اندروید قایل لینک در روت و اضافه کردن دات نت جیسون الزامی است
        var user = JsonConvert.DeserializeObject<RootUserDetails>(_url);


        Debug.Log("link user : " + user.user_details);
        Debug.Log("invite code : " + user.user.invite_code);
        
    }
    public void CheckDataAtFirst3(string _url)
    {

        // برای خواندن در اندروید قایل لینک در روت و اضافه کردن دات نت جیسون الزامی است
        var user = JsonConvert.DeserializeObject<RootSettings>(_url);


       

        PlayerPrefs.SetString("link_whatsApp", user.whatsapp);
        PlayerPrefs.SetString("link_telegram", user.telegram);
        PlayerPrefs.SetString("link_Insta", user.instagram);
    }
    void sendParent()
    {
        StartCoroutine(SendParentsInformation());
    }

    public void openSocial(int a)
    {
        if (a == 1)
        {
            Debug.Log(PlayerPrefs.GetString("link_Insta"));
            Application.OpenURL(PlayerPrefs.GetString("link_Insta"));
          
        }
        else if (a == 2) {
            Application.OpenURL(PlayerPrefs.GetString("link_whatsApp"));
          
        }
        else if (a == 3)
        {
           
            Application.OpenURL(PlayerPrefs.GetString("link_telegram"));
          
        }

    }

    IEnumerator SendParentsInformation()
    {


        WWWForm form = new WWWForm();

        if (string.IsNullOrEmpty(GameObject.Find("InputFieldname").GetComponent<InputField>().text))
        {
            form.AddField("name","not set");
        }
        else
        {
            form.AddField("name", GameObject.Find("InputFieldname").GetComponent<InputField>().text);
        }
       
        if (parents == 2)
        {
            form.AddField("parent", "father");
        }
        else if(parents == 1)
        {
            form.AddField("parent", "mother");
        }
        else
        {
            form.AddField("parent", "not set");
        }
        if(GameObject.Find("InputField phone").GetComponent<InputField>().text.Length >= 11)
        {
            form.AddField("mobile", GameObject.Find("InputField phone").GetComponent<InputField>().text);
            if (GameObject.Find("InputField password").GetComponent<InputField>().text.Length >= 4)
            {
                PlayerPrefs.SetString("password", GameObject.Find("InputField password").GetComponent<InputField>().text);
            }
            else
            {
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("رمز حداقل چهاررقمی تعیین کنید");
#endif
                Debug.Log("return null -- dont have password");
                yield break;
            }
        }
        else
        {
            Debug.Log("return null -- dont have phone number");
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("شماره تماس را به درستی وارد نمایید");
#endif
            yield break;
        }
        using (UnityWebRequest webReq = UnityWebRequest.Post(PlayerPrefs.GetString("base_url") +"/user/settings", form))
        {

            webReq.SetRequestHeader("Authorization", "Bearer " + UnityEngine.PlayerPrefs.GetString("user_token"));
            webReq.SetRequestHeader("Accept", "application/json");
            webReq.chunkedTransfer = false;
            webReq.certificateHandler = new BypassCertificate();
            // Request and wait for the desired page.
            yield return webReq.SendWebRequest();

            // string[] pages = uri.Split('/');
            //  int page = pages.Length - 1;

            if (webReq.isNetworkError)
            {
                
                    InternetAvailabilityTest.instance.CheckConnection();
               
            }
            else
            {
                PlayerPrefs.SetString("ParentsName", GameObject.Find("InputFieldname").GetComponent<InputField>().text);
                PlayerPrefs.SetInt("parent", parents);
                if (GameObject.Find("InputField phone").GetComponent<InputField>().text.Length >= 11)
                {
                   
                    PlayerPrefs.SetString("phoneNumber", GameObject.Find("InputField phone").GetComponent<InputField>().text);
                }

                StartCoroutine(rgisterUserData(true));


                Debug.Log(webReq.downloadHandler.text);
              //  processData(webReq.downloadHandler.text);
            }
        }


    }

    public void showToast()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("برای ذخیره روی دکمه ذخیره در بالای صفحه بزنید");
#endif
        Debug.Log("please click on save button");
    }
    public void sendMessage()
    {
        if (!string.IsNullOrEmpty(GameObject.Find("InputField message").GetComponent<InputField>().text))
        {
            StartCoroutine(SendTicket());
        }
        else
        {
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("لطفا پیامی بنویسید");
#endif
        }
    }

    IEnumerator SendTicket()
    {


        WWWForm form = new WWWForm();

      
            form.AddField("message", GameObject.Find("InputField message").GetComponent<InputField>().text);
    
        using (UnityWebRequest webReq = UnityWebRequest.Post(PlayerPrefs.GetString("base_url") +"/user/contact-us", form))
        {

            webReq.SetRequestHeader("Authorization", "Bearer " + UnityEngine.PlayerPrefs.GetString("user_token"));
            webReq.SetRequestHeader("Accept", "application/json");
            webReq.chunkedTransfer = false;
            webReq.certificateHandler = new BypassCertificate();
            // Request and wait for the desired page.
            yield return webReq.SendWebRequest();

            // string[] pages = uri.Split('/');
            //  int page = pages.Length - 1;

            if (webReq.isNetworkError)
            {

                InternetAvailabilityTest.instance.CheckConnection();

            }
            else
            {
#if UNITY_ANDROID && !UNITY_EDITOR
    _ShowAndroidToastMessage("ارسال شد منتظر تماس ما باشید");
#endif

                Debug.Log(webReq.downloadHandler.text);
                //  processData(webReq.downloadHandler.text);
            }
        }


    }
    ////////////////////////////////////// show toast message/////////////////////////////////////////////////////////////////
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


}

[System.Serializable]
public class RootLogout
{
    public string message { get; set; }
    public int status { get; set; }
}


[System.Serializable]
public class RootSettingAfterParentsInsert
{
    public string message { get; set; }
    public UserSetting user { get; set; }
    public Setting setting { get; set; }
}
[System.Serializable]
public class RootSettingBeforeParentsInsert
{
    public string message { get; set; }
    public User user { get; set; }
    public List<object> setting { get; set; }
}
[System.Serializable]
public class Setting
{
    public int id { get; set; }
    public string user_id { get; set; }
    public string key { get; set; }
    public string value { get; set; }

}

[System.Serializable]
public class UserSetting
{
    public int id { get; set; }
    public string name { get; set; }
    public string invite_code { get; set; }
    public string number_of_invitations { get; set; }
    public string client_id { get; set; }
    public string device { get; set; }
    public string currect_app_version { get; set; }
    public string age { get; set; }
    public string favorites { get; set; }
    public string gender { get; set; }
    public string mobile { get; set; }
    public string account_verified { get; set; }
    public object email { get; set; }
    public string ip { get; set; }
    public object api_token { get; set; }
    public object email_verified_at { get; set; }
    public string market_id { get; set; }
    public string status_id { get; set; }
 
    public List<Setting> settings { get; set; }
}


[System.Serializable]

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class RootUserDetails
{
    public UserDet user { get; set; }
    public string name { get; set; }
    public string parent { get; set; }
    public string mobile { get; set; }
    public string user_details { get; set; }
}
[System.Serializable]
public class UserDet
{
    public int id { get; set; }
    public string name { get; set; }
    public string invite_code { get; set; }
    public string client_id { get; set; }
    public string device { get; set; }
    public string currect_app_version { get; set; }
    public string age { get; set; }
    public string favorites { get; set; }
    public string gender { get; set; }
    public string mobile { get; set; }
    public string account_verified { get; set; }
    public object email { get; set; }
    public string ip { get; set; }
    public object api_token { get; set; }
    public object email_verified_at { get; set; }
    public string market_id { get; set; }
    public string status_id { get; set; }

}

[System.Serializable]

public class RootSettings
{
    public int id { get; set; }
    public string version { get; set; }
    public string status { get; set; }
    public string message { get; set; }
    public string telegram { get; set; }
    public string instagram { get; set; }
    public string whatsapp { get; set; }
    public string hasdico { get; set; }
    public string dicotext { get; set; }
    public string firsttimefree { get; set; }
    public int newsversion { get; set; }
    public string newsmessage { get; set; }
    public string newsimage { get; set; }
    public object created_at { get; set; }
    public object updated_at { get; set; }
}







