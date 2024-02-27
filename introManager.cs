using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UPersian.Components;
using System;

public class introManager : MonoBehaviour
{
    public static introManager instance;
    public GameObject[] levels;
    
    public Button[] buttons;
    public GameObject[] ages;
    public GameObject[] favourite;
    public Transform target;
    public Transform startPos;
    public InputField[] inputField;
    public RtlText childnames;
   
    [Space(30)]
    public GameObject Dog,boxMessage;
    public Image loadingSlider;

    private AsyncOperation asyncOperation;
    private const string LastLoginKey = "LastLogin";
    private bool hasReward;
    // Start is called before the first frame update
    int selected,selectedChild;
    void Awake()
    {
        if (instance == null) instance = this;

        Screen.orientation = ScreenOrientation.Portrait;
    }
    void Start()
    {
        //play backGround
        Dog.SetActive(false);
        hasReward = false;
    
        if (PlayerPrefs.GetInt("comeForAddData") == 1)
        {
            PlayerPrefs.SetInt("comeForAddData", 0);
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
            }
            levels[2].gameObject.SetActive(true);
        }
       

        else 
        {
           
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
            }

            levels[0].gameObject.SetActive(true);
        }
        //ترتیب دکمه ها مهم است
        //وقتی با استپ 100 میفرستیم یعنی بدون تاخیر برو بعدی

        buttons[0].onClick.AddListener(() => changeScene());
        buttons[1].onClick.AddListener(() => buttonCallBack(1, 2,100,0));
        //login
        buttons[2].onClick.AddListener(() => buttonCallBack(0, 20,100,0));
        buttons[3].onClick.AddListener(() => buttonCallBack(0, 2,100,0));
        //questions boy-girl - bou=1 / girl =2
        buttons[4].onClick.AddListener(() => buttonCallBack(2, 3,1,2));
        buttons[5].onClick.AddListener(() => buttonCallBack(2, 3,1,1));
        //back btn question boy-girl
        buttons[6].onClick.AddListener(() => buttonCallBack(2, 0,100,0));
        //questions age // اینجا قبلا میرفت به4 ولی مراحل جواب دادن به سوالات حذف شذ و مستقیم بره به شماره وارد کردن
        buttons[7].onClick.AddListener(() => buttonCallBack(3, 15, 0, 0));
      
        //back btn question age
        buttons[8].onClick.AddListener(() => buttonCallBack(3, 2, 100, 0));

        //questions mozoat
        buttons[9].onClick.AddListener(() => buttonCallBack(4, 5, 2, 0));

        //back btn question mozoat
        buttons[10].onClick.AddListener(() => buttonCallBack(4, 3, 100, 0));

        //questions soalat(1) yes =1 / no =2 / notsure=3
        buttons[11].onClick.AddListener(() => buttonCallBack(5, 6, 3, 1));
        buttons[12].onClick.AddListener(() => buttonCallBack(5, 6, 3, 2));
        buttons[13].onClick.AddListener(() => buttonCallBack(5, 6, 3, 3));

        //questions soalat(2) yes =1 / no =2 / notsure=3
        buttons[14].onClick.AddListener(() => buttonCallBack(6, 7, 4, 1));
        buttons[15].onClick.AddListener(() => buttonCallBack(6, 7, 4, 2));
        buttons[16].onClick.AddListener(() => buttonCallBack(6, 7, 4, 3));

        //questions soalat(3) yes =1 / no =2 / notsure=3
        buttons[17].onClick.AddListener(() => buttonCallBack(7, 8, 5, 1));
        buttons[18].onClick.AddListener(() => buttonCallBack(7, 8, 5, 2));
        buttons[19].onClick.AddListener(() => buttonCallBack(7, 8, 5, 3));

        //questions soalat(4) yes =1 / no =2 / notsure=3
        buttons[20].onClick.AddListener(() => buttonCallBack(8, 9, 6, 1));
        buttons[21].onClick.AddListener(() => buttonCallBack(8, 9, 6, 2));
        buttons[22].onClick.AddListener(() => buttonCallBack(8, 9, 6, 3));

        //questions soalat(5) yes =1 / no =2 / notsure=3
        buttons[23].onClick.AddListener(() => buttonCallBack(9, 10, 7, 1));
        buttons[24].onClick.AddListener(() => buttonCallBack(9, 10, 7, 2));
        buttons[25].onClick.AddListener(() => buttonCallBack(9, 10, 7, 3));

        //questions soalat(6) yes =1 / no =2 / notsure=3
        buttons[26].onClick.AddListener(() => buttonCallBack(10, 11, 8, 1));
        buttons[27].onClick.AddListener(() => buttonCallBack(10, 11, 8, 2));
        buttons[28].onClick.AddListener(() => buttonCallBack(10, 11, 8, 3));

        //questions soalat(7) yes =1 / no =2 / notsure=3
        buttons[29].onClick.AddListener(() => buttonCallBack(11, 12, 9, 1));
        buttons[30].onClick.AddListener(() => buttonCallBack(11, 12, 9, 2));
        buttons[31].onClick.AddListener(() => buttonCallBack(11, 12, 9, 3));

        //questions soalat(8) yes =1 / no =2 / notsure=3
        buttons[32].onClick.AddListener(() => buttonCallBack(12, 13, 10, 1));
        buttons[33].onClick.AddListener(() => buttonCallBack(12, 13, 10, 2));
        buttons[34].onClick.AddListener(() => buttonCallBack(12, 13, 10, 3));

        //after questions
        buttons[35].onClick.AddListener(() => buttonCallBack(13, 15, 0, 0));

        //back btn login 0
        buttons[36].onClick.AddListener(() => buttonCallBack(14, 0, 100, 0));

        //choose sms or gmail ---- 37 just for test and defult is 0
        buttons[37].onClick.AddListener(() => buttonCallBack(14, 20, 0, 0));
        buttons[38].onClick.AddListener(() => buttonCallBack(14, 17, 0, 0));

        //back btn login sms
        buttons[39].onClick.AddListener(() => buttonCallBack(15, 14, 100, 0));
        //back btn login gmail
        buttons[40].onClick.AddListener(() => buttonCallBack(17, 14, 100, 0));
        //back btn login sms - 2
        buttons[41].onClick.AddListener(() => buttonCallBack(16, 15, 100, 0));
        //back btn login gmail -2
        buttons[42].onClick.AddListener(() => buttonCallBack(18, 17, 100, 0));
        //login with pass word
        buttons[43].onClick.AddListener(() => buttonCallBack(20, 0, 98, 0));
        // btn forget pass
        buttons[44].onClick.AddListener(() => buttonCallBack(20, 21, 94, 0));
        //btn sing up
        buttons[45].onClick.AddListener(() => buttonCallBack(20, 2, 99, 0));
        //btn send code for forgetetd pass
        buttons[46].onClick.AddListener(() => buttonCallBack(21, 22, 97, 0));
        //btn change pass
        buttons[47].onClick.AddListener(() => buttonCallBack(22, 0, 96, 0));
        //btn after sms kod sent
        buttons[48].onClick.AddListener(() => buttonCallBack(22, 0, 95, 0));
        // btn back from change pass
        buttons[49].onClick.AddListener(() => buttonCallBack(22, 21, 100, 0));

        buttons[50].onClick.AddListener(() => buttonCallBack(21, 20, 100, 0));


        buttons[51].onClick.AddListener(() => Application.OpenURL("https://islandofkids.com/Privacy-Policy-papita-plus.html"));

        inputField[0].onValueChanged.AddListener((str) =>
        {
            if (inputField[0].text.Length > 3)
            {
                buttons[48].interactable = true;
            }
        });
        inputField[1].onValueChanged.AddListener((str) =>
        {
            if (inputField[1].text.Length > 3)
            {
                buttons[47].interactable = true;
            }
        });
      
    }

    public  void CheckTime()
    {
        // بررسی زمان ورود قبلی
        DateTime lastLoginTime = GetLastLoginTime();
        DateTime currentTime = DateTime.Today;

        TimeSpan elapsedTime = currentTime - lastLoginTime;

        // بررسی اگر یک روز گذشته باشد
        if (elapsedTime.Days >= 1)
        {
            // اعطای جایزه
            GiveReward();

            // ذخیره زمان ورود فعلی
            SaveCurrentLoginTime(currentTime);
        }
        else
        {
            Debug.Log("dont have reward");
        }
    }
    private DateTime GetLastLoginTime()
    {
        long lastLoginBinary = Convert.ToInt64(PlayerPrefs.GetString(LastLoginKey, "0"));
        return DateTime.FromBinary(lastLoginBinary);
    }

    private void SaveCurrentLoginTime(DateTime currentTime)
    {
        string currentTimeBinary = currentTime.ToBinary().ToString();
        PlayerPrefs.SetString(LastLoginKey, currentTimeBinary);
        PlayerPrefs.Save();
    }

    private void GiveReward()
    {
        Debug.Log("has reward");
        hasReward = true;
        boxMessage.SetActive(true);

        int score = UnityEngine.PlayerPrefs.GetInt("coins");
        int newcoin = score + 5;
        UnityEngine.PlayerPrefs.SetInt("coins", newcoin);
        
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    private void buttonCallBack(int back, int next,int step,int answer)
    {
        Debug.Log("Button Clicked. Received next: " +next + " with back: " + back + " with step: " + step + " with answer: " + answer);
        selected = next;
        AudioController.instance.playSound(1);
     
        // استپ ها برای سوالات است
        switch (step)
        {
            case 1:
                PlayerPrefs.SetInt("gender", answer);
                PlayerPrefs.SetString("ChildrenName", inputField[2].text);
                PlayerPrefs.SetString("city", inputField[3].text);
               
               // Debug.Log("child name" + PlayerPrefs.GetString("ChildrenName"));
                
                break;
            case 2:
                PlayerPrefs.DeleteKey("favourite");
                for(int i=0 ; i < favourite.Length; i++)
                {
                    if (favourite[i].GetComponent<Outline>().enabled)
                    {
                        string mozo = PlayerPrefs.GetString("favourite");
                        mozo = mozo + i.ToString()+",";
                        PlayerPrefs.SetString("favourite",mozo);
                        Debug.Log(PlayerPrefs.GetString("favourite"));
                    }
                }
                break;
            case 3:
                PlayerPrefs.SetInt("question1", answer);
                if (answer == 1)
                {
                    buttons[11].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 2)
                {
                    buttons[12].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 3)
                {
                    buttons[13].gameObject.GetComponent<Outline>().enabled = true;
                }
                break;
            case 4:
                PlayerPrefs.SetInt("question2", answer);
                if (answer == 1)
                {
                    buttons[14].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 2)
                {
                    buttons[15].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 3)
                {
                    buttons[16].gameObject.GetComponent<Outline>().enabled = true;
                }
                break;
            case 5:
                PlayerPrefs.SetInt("question3", answer);
                if (answer == 1)
                {
                    buttons[17].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 2)
                {
                    buttons[18].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 3)
                {
                    buttons[19].gameObject.GetComponent<Outline>().enabled = true;
                }

                
                break;
            case 6:
                PlayerPrefs.SetInt("question4", answer);
                if (answer == 1)
                {
                    buttons[20].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 2)
                {
                    buttons[21].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 3)
                {
                    buttons[22].gameObject.GetComponent<Outline>().enabled = true;
                }
                break;
            case 7:
                PlayerPrefs.SetInt("question5", answer);
                if (answer == 1)
                {
                    buttons[23].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 2)
                {
                    buttons[24].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 3)
                {
                    buttons[25].gameObject.GetComponent<Outline>().enabled = true;
                }
                Firebase.Analytics.FirebaseAnalytics.LogEvent("NimeSoalat", "NimeSoalat_app", 1);
                break;
            case 8:
                PlayerPrefs.SetInt("question6", answer);
                if (answer == 1)
                {
                    buttons[26].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 2)
                {
                    buttons[27].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 3)
                {
                    buttons[28].gameObject.GetComponent<Outline>().enabled = true;
                }
                break;
            case 9:
                PlayerPrefs.SetInt("question7", answer);
                if (answer == 1)
                {
                    buttons[29].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 2)
                {
                    buttons[30].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 3)
                {
                    buttons[31].gameObject.GetComponent<Outline>().enabled = true;
                }
                break;
            case 10:
                PlayerPrefs.SetInt("question8", answer);
                if (answer == 1)
                {
                    buttons[32].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 2)
                {
                    buttons[33].gameObject.GetComponent<Outline>().enabled = true;
                }
                else if (answer == 3)
                {
                    buttons[34].gameObject.GetComponent<Outline>().enabled = true;
                }
                PlayerPrefs.SetInt("QuestionsAnswered" ,1);
                Firebase.Analytics.FirebaseAnalytics.LogEvent("payane_Solat", "payane_Solat_app", 1);
                break;
            default:
              
                break;
        }


        StartCoroutine(changes(back, next,step));
    }

    public void selectAge(int a)
    {
        AudioController.instance.playSound(1);
        for (int i = 0; i < ages.Length; i++)
        {
            if (a == 8)
            {
                PlayerPrefs.SetInt("age", 100);
                ages[i].GetComponent<Outline>().enabled = true;
                Debug.Log("hisAge " + PlayerPrefs.GetInt("age"));
            }
            else if (i == a & a != 8)
            {
                PlayerPrefs.SetInt("age", a+1);
                ages[i].GetComponent<Outline>().enabled = true;
                Debug.Log("hisAge " + PlayerPrefs.GetInt("age"));

            }
            else
            {
                ages[i].GetComponent<Outline>().enabled = false;
            }
        }
    }
    public void SelectFromList(int b)
    {
        if (favourite[b].GetComponent<Outline>().enabled)
        {
            favourite[b].GetComponent<Outline>().enabled = false;
            
        }
        else
        {
            favourite[b].GetComponent<Outline>().enabled = true;
        }
    }

    public void SendCode(int a)
    {
        AudioController.instance.playSound(1);
        if (a == 1)
        {
            //sms
            PlayerPrefs.SetString("phoneNumber", GameObject.Find("userMobileNumber").GetComponent<Text>().text);
            StartCoroutine(changes(15, 16, 100));
            MainAppController.instance.register();
      
            
        }
        else
        {
            //gmail
           // GameObject.Find("userGmail").GetComponent<Text>().text
            StartCoroutine(changes(17, 18, 100));
        }
    }

    public void goForGetPassword()
    {
        AudioController.instance.playSound(1);
      
            //sms
            PlayerPrefs.SetString("phoneNumber", GameObject.Find("userMobileNumber").GetComponent<Text>().text);
            StartCoroutine(changes(15, 16, 100));
          //  MainAppController.instance.register();

    }

    // Update is called once per frame
    void Update()
    {
        if (childnames.text.Length>0)
        {
            int code = (int)childnames.text[0];
            if ((code > 96 && code < 123) || (code > 64 && code < 91))
            {
                childnames.alignment = TextAnchor.MiddleLeft;
            }
            else
            {
                childnames.alignment = TextAnchor.MiddleRight;
            }
        }
     
    }
 
   public void DoChanges(int a, int b, int st)
    {
        StartCoroutine(changes(a, b, st));
    }
    private IEnumerator changes(int a,int b ,int st)
    {
        InternetAvailabilityTest.instance.CheckConnection();

        if (st == 99)
        {
            //روی دکمه ثبت نام زده
            yield return new WaitForSeconds(0);
            if (PlayerPrefs.HasKey("ChildrenName"))
            {
                //اینجا اطلاعاتش رو قبلا وارد کرده مانند نام و سن و روی دکمه ثبت نام زده

                b = 15;
            }

            levels[a].SetActive(false);
            levels[b].transform.position = startPos.position;
            levels[b].SetActive(true);
            StartCoroutine(MoveOverSeconds(levels[b], target.position, 0.4f));
            Firebase.Analytics.FirebaseAnalytics.LogEvent("sabtNam_btn", "sabtNam_btn_app", 1);

        }
        else if (st == 98)
        {
            UnityEngine.PlayerPrefs.SetString("phoneNumber", levels[20].transform.GetChild(3).gameObject.GetComponent<InputField>().text);
            UnityEngine.PlayerPrefs.SetString("password", levels[20].transform.GetChild(4).gameObject.GetComponent<InputField>().text);
            MainAppController.instance.Login();
            buttons[43].gameObject.GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(3);
            buttons[43].gameObject.GetComponent<Animator>().enabled = false;
            if (PlayerPrefs.GetInt("loginSucces") == 2)
            {
                MainAppController.instance.checkFirstButton();
                CheckTime();
                buttons[2].gameObject.SetActive(false);
                levels[a].SetActive(false);
                levels[b].transform.position = startPos.position;
                levels[b].SetActive(true);
                StartCoroutine(MoveOverSeconds(levels[b], target.position, 0.4f));
               
                Firebase.Analytics.FirebaseAnalytics.LogEvent("Login_btn_success", "Login_btn_success_app", 1);
                
            }
            else
            {
                AudioController.instance.playSound(2);
               

                // levels[20].transform.GetChild(3).gameObject.GetComponent<InputField>().text = null;
                // levels[20].transform.GetChild(4).gameObject.GetComponent<InputField>().text = null;
            }
            //اینجا موبایلو پسورد رو بررسی کرده و نتیجه رو برمیگردونه

        }
        else if (st == 97)
        {
            //دکمه فراموشی رمز
            PlayerPrefs.SetString("phoneNumber", GameObject.Find("userMobileNumber").GetComponent<Text>().text);
          
            levels[a].SetActive(false);
            levels[b].transform.position = startPos.position;
            levels[b].SetActive(true);
            StartCoroutine(MoveOverSeconds(levels[b], target.position, 0.4f));
            levels[b].transform.GetChild(3).gameObject.GetComponent<Text>().color = Color.black;
            levels[b].transform.GetChild(3).gameObject.GetComponent<Text>().text = "ﻩﺭﺎﻤﺷ ﻪﺑ ﻩﺪﺷ ﮏﻣﺎﯿﭘ ﺪﮐ" + "\n" + "\n" +
                "ﯽﻤﻗﺭ 4 ﻞﻗﺍﺪﺣ ﺪﯾﺪﺟ ﺰﻣﺭ ﺎﺑ ﻩﺍﺮﻤﻫ " + "\n" + "ﺪﯿﯾﺎﻤﻧ ﺩﺭﺍﻭ ﺍﺭ ";
            levels[b].transform.GetChild(5).gameObject.GetComponent<InputField>().text = null;
            levels[b].transform.GetChild(6).gameObject.GetComponent<InputField>().text = null;
            MainAppController.instance.SendResetPassword();

            yield return new WaitForSeconds(2);

          

        }
        else if (st == 96)
        {
            //دکمه تغیر رمز
            PlayerPrefs.SetString("KodTaeed", GameObject.Find("codeTaeedResetPass").GetComponent<Text>().text);
            PlayerPrefs.SetString("password", GameObject.Find("newPassword").GetComponent<Text>().text);
            buttons[47].gameObject.GetComponent<Animator>().enabled = true;
            MainAppController.instance.ResetPassword();
           
            yield return new WaitForSeconds(3);
            buttons[47].gameObject.GetComponent<Animator>().enabled = false;
            if (PlayerPrefs.GetInt("passwordChanged") == 1)
            {
                levels[a].SetActive(false);
                levels[b].transform.position = startPos.position;
                levels[b].SetActive(true);
                StartCoroutine(MoveOverSeconds(levels[b], target.position, 0.4f));
            }
           

        }
        else if (st == 95)
        {
            //دکمه نهایی کردن ثبت نام
            PlayerPrefs.SetString("KodTaeed", GameObject.Find("userSmsCode").GetComponent<Text>().text);
            PlayerPrefs.SetString("password", GameObject.Find("myPassword").GetComponent<Text>().text);
            buttons[48].gameObject.GetComponent<Animator>().enabled = true;
            //MainAppController.instance.verifyUserFirstTime();
            MainAppController.instance.register();

            yield return new WaitForSeconds(3);
            buttons[48].gameObject.GetComponent<Animator>().enabled = false;
            if (PlayerPrefs.GetInt("loginSucces") == 2)
            {
                MainAppController.instance.checkFirstButton();
                CheckTime();
                buttons[2].gameObject.SetActive(false);
                levels[16].SetActive(false);
                levels[a].SetActive(false);
                levels[b].transform.position = startPos.position;
                levels[b].SetActive(true);
                StartCoroutine(MoveOverSeconds(levels[b], target.position, 0.4f));
                MainAppController.instance.AfterSuccesLogin();
               
            }
            else
            {
                AudioController.instance.playSound(2);
            }


        }
        else if (st == 94)
        {

           
           
            yield return new WaitForSeconds(0.1f);
            buttons[48].gameObject.GetComponent<Animator>().enabled = false;
          
                levels[a].SetActive(false);
                levels[b].transform.position = startPos.position;
                levels[b].SetActive(true);
            for (int i = 0; i < 11; i++)
            {
                levels[b].transform.GetChild(2).gameObject.GetComponent<type>().del();
            }
            StartCoroutine(MoveOverSeconds(levels[b], target.position, 0.4f));


            Firebase.Analytics.FirebaseAnalytics.LogEvent("forgetPass", "forgetPass_app", 1);
        }
        else if (st == 100 )
        {
            yield return new WaitForSeconds(0);
          

            levels[a].SetActive(false);
            levels[b].transform.position = startPos.position;
            levels[b].SetActive(true);
            StartCoroutine(MoveOverSeconds(levels[b], target.position, 0.4f));

            if (b == 1)
            {
                Firebase.Analytics.FirebaseAnalytics.LogEvent("bezanBerim", "bezanBerim_app", 1);
            }
            else if (b == 20 & a==0)
            {
               Firebase.Analytics.FirebaseAnalytics.LogEvent("btn_have_account", "have_account_name", 1);
            }
        }
        else if (st == 37) // for test
        {
            yield return new WaitForSeconds(0);

            Initiate.Fade("MainmenuGame", Color.black, 3f);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);

            levels[a].SetActive(false);
            levels[b].transform.position = startPos.position;
            levels[b].SetActive(true);
            StartCoroutine(MoveOverSeconds(levels[b], target.position, 0.4f));

            if(a==3 & b == 4)
            {
                Firebase.Analytics.FirebaseAnalytics.LogEvent("getAge", "getAge_app", 1);
            }

            if (a == 2 & b == 3)
            {
                Firebase.Analytics.FirebaseAnalytics.LogEvent("getGender", "getGender_app", 1);
            }
        }
      


    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }
    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    public void SelectBtn(int b)
    {
        if (b == 0 | b == 1)
        {
            PlayerPrefs.SetInt("sex", b);
          
        }
        else if (b == 2 | b == 3 | b == 4)
        {
            PlayerPrefs.SetInt("age", b);
        }
        else if (b == 5 | b == 6 | b == 7)
        {
            PlayerPrefs.SetInt("qeustion1", b);
    
        }
        else if (b == 8 | b == 9 | b == 10)
        {
            PlayerPrefs.SetInt("qeustion2", b);
          
        }
        else if (b == 11 | b == 12 | b == 13)
        {
            PlayerPrefs.SetInt("qeustion3", b);
         
        }
        else if (b == 14 | b == 15 | b == 16)
        {
            PlayerPrefs.SetInt("qeustion4", b);
          
        }
        else if (b == 17 | b == 18 )
        {
            PlayerPrefs.SetInt("lang", b);
            PlayerPrefs.SetInt("setQuestions", 1);
     
        }
    }

    async void changeScene()
    {
        //SceneManager.LoadScene("MainmenuGame");
        //LevelManager.Instance.LoadScene("MainmenuGame");
        
        Resources.UnloadUnusedAssets(); // خالی کردن حافظه رم

        int index = UnityEngine.Random.Range(0, 2);
        /*
        if (hasReward & PlayerPrefs.GetInt("MyDays") <= 0)
        {
            Dog.SetActive(true);
           
            await System.Threading.Tasks.Task.Delay(300);
            Dog.transform.GetChild(1).gameObject.SetActive(true);
            if (index == 0) StartCoroutine(LoadSceneAsync("game37_sample"));
            else StartCoroutine(LoadSceneAsync("game43_sample"));
        }
        else
        {
            if (PlayerPrefs.GetInt("loginSucces") == 2)
            {
                Dog.SetActive(true);
               
                await System.Threading.Tasks.Task.Delay(300);
                Dog.transform.GetChild(1).gameObject.SetActive(true);
                StartCoroutine(LoadSceneAsync("MainmenuGame"));
            }
               
            else
                buttonCallBack(0, 2, 100, 0);
        }
        */

        Debug.Log("login succes : " + PlayerPrefs.GetInt("loginSucces"));

        if (PlayerPrefs.GetInt("loginSucces") == 2)
        {

            if (hasReward & PlayerPrefs.GetInt("MyDays") <= 0)
            {
                Dog.SetActive(true);

                await System.Threading.Tasks.Task.Delay(300);
                Dog.transform.GetChild(1).gameObject.SetActive(true);
                if (index == 0) StartCoroutine(LoadSceneAsync("game37_sample"));
                else StartCoroutine(LoadSceneAsync("game43_sample"));
            }
            else
            {
                if (PlayerPrefs.GetInt("loginSucces") == 2)
                {
                    Dog.SetActive(true);

                    await System.Threading.Tasks.Task.Delay(300);
                    Dog.transform.GetChild(1).gameObject.SetActive(true);
                    StartCoroutine(LoadSceneAsync("MainmenuGame"));
                }

                else
                    buttonCallBack(0, 2, 100, 0);
            }
        }

        else
            buttonCallBack(0, 2, 100, 0);


        // Initiate.Fade("MainmenuGame", Color.yellow, 3f);
        //SceneManager.LoadScene("MainmenuGame");
        Firebase.Analytics.FirebaseAnalytics.LogEvent("btn_vorod", "btn_vorod_name", 1);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
          
            loadingSlider.fillAmount = progress;
            
            if (progress >= 1f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
#if UNITY_ANDROID && !UNITY_EDITOR
    ////////////////////////////////////// show toast message/////////////////////////////////////////////////////////////////

    public void _ShowAndroidToastMessage(string message)
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
