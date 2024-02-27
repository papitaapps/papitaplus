using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public static Timer instanseTimer;
    public float timeRemaining = 120;
    public bool timerIsRunning = false;
    public Text timeText;
    void Awake()
    {
        if (instanseTimer == null) instanseTimer = this;
      

    }
    private void Start()
    {
        // Starts the timer automatically
       // timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                if (PlayerPrefs.GetInt("loginSucces") == 0)
                {
                    Debug.Log("Time has run out!");
                
                    if (MainAppController.instance.waitForSmsRegister)
                    {
                        MainAppController.instance.waitForSmsRegister = false;
                        GameObject.Find("Text status kod").GetComponent<Text>().text = "ﺖﻓﺎﯾ ﻥﺎﯾﺎﭘ ﺎﻤﺷ ﻥﺎﻣﺯ" + "\n"
        + "ﺪﯿﻨﮐ ﮏﭼ ﺍﺭ ﺩﻮﺧ ﺱﺎﻤﺗ ﻩﺭﺎﻤﺷ ﺎﻔﻄﻟ" + "\n" + "ﺪﯿﻧﺰﺑ ﺍﺭ ﻞﺒﻗ ﻮﻨﻣ ﻝﺎﺳﺭﺍ ﻪﻤﮐﺩ ﺍﺩﺪﺠﻣ ﻭ";
#if UNITY_ANDROID && !UNITY_EDITOR
  
                introManager.instance._ShowAndroidToastMessage("زمان شما پایان یافت");
#endif
                        introManager.instance.buttons[41].gameObject.SetActive(true);
                    }
                    // introManager.instance.buttons[41].gameObject.SetActive(true);
                    // introManager.instance.buttons[49].gameObject.SetActive(true);
                }

            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}