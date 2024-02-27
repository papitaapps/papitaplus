using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ToutorialManager : MonoBehaviour
{
    
    public static ToutorialManager instance;
    public Button m_button;
    public RtlText message;
    [TextArea(1, 10)]
    public string[] m_message;
    public GameObject[] Sections;
    public Transform[] Text_positions;
    public AudioSource sound;
    public AudioClip[] sounds;
    int counter = 0;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
      // StartShowing();
    }
    public async void StartShowing()
    {
        // StartCoroutine(wirteMessage(m_message[0]));
        m_button.gameObject.SetActive(true);
        for (int i = 0; i < Sections.Length; i++)
        {
            Sections[i].SetActive(false);
        }
        await System.Threading.Tasks.Task.Delay(1000);
        Sections[0].SetActive(true);
        sound.clip = sounds[counter];
        sound.Play();
        message.text = m_message[0];
        message.alignment = TextAnchor.MiddleCenter;
        message.gameObject.transform.position = Text_positions[0].transform.position;
        message.fontSize = 45;
        m_button.onClick.AddListener(() => changeSections());
      
    }

    IEnumerator wirteMessage(string text)
    {
        message.text = string.Empty;
        for (int i = 0; i < text.Length; i++)
        {
            message.text += text[i];
            //Wait a certain amount of time, then continue with the for loop
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;

    }
    void changeSections()
    {
        counter++;
        Debug.Log(counter);
       
        switch (counter)
        {
            case 1:
                message.text = m_message[counter];
                message.alignment = TextAnchor.MiddleCenter;
                message.fontSize = 35;
                sound.clip = sounds[counter];
                sound.Play();
                break;
                ///////////////////////////////////////////////////////////////////////
            case 2:
                Sections[0].SetActive(false);
                Sections[1].SetActive(true);
                message.text = m_message[counter];
                message.alignment = TextAnchor.UpperRight;
                message.gameObject.transform.position = Text_positions[1].transform.position;
                message.fontSize = 30;
                sound.clip = sounds[counter];
                sound.Play();
                break;
            case 3:
                Sections[0].SetActive(false);
                Sections[1].SetActive(true);
                message.text = m_message[counter];
                message.alignment = TextAnchor.UpperRight;
                message.gameObject.transform.position = Text_positions[1].transform.position;
                message.fontSize = 30;
                sound.clip = sounds[counter];
                sound.Play();
                break;
            case 4:
                Sections[0].SetActive(false);
                Sections[1].SetActive(true);
                message.text = m_message[counter];
                message.alignment = TextAnchor.UpperRight;
                message.gameObject.transform.position = Text_positions[1].transform.position;
                message.fontSize = 30;
                sound.clip = sounds[counter];
                sound.Play();
                break;
            case 5:
                Sections[0].SetActive(false);
                Sections[1].SetActive(true);
                message.text = m_message[counter];
                message.alignment = TextAnchor.UpperRight;
                message.gameObject.transform.position = Text_positions[1].transform.position;
                message.fontSize = 30;
                sound.clip = sounds[counter];
                sound.Play();
                break;
                //////////////////////////////////////////////////////////////////////////
            case 6:
                Sections[1].SetActive(false);
                Sections[2].SetActive(true);
                message.text = m_message[counter];
                message.alignment = TextAnchor.UpperRight;
                message.gameObject.transform.position = Text_positions[2].transform.position;
                sound.clip = sounds[counter];
                sound.Play();
                break;
                //////////////////////////////////////////////////////////////////////////////////
            case 7:
                Sections[2].SetActive(false);
                Sections[3].SetActive(true);
                message.text = m_message[counter];
                message.alignment = TextAnchor.UpperRight;
                message.gameObject.transform.position = Text_positions[3].transform.position;
                sound.clip = sounds[counter];
                sound.Play();
                break;
            case 8:
                Sections[2].SetActive(false);
                Sections[3].SetActive(true);
                message.text = m_message[counter];
                message.alignment = TextAnchor.UpperRight;
                message.gameObject.transform.position = Text_positions[3].transform.position;
                sound.clip = sounds[counter];
                sound.Play();
                break;
            case 9:
                Sections[2].SetActive(false);
                Sections[3].SetActive(true);
                message.text = m_message[counter];
                message.alignment = TextAnchor.UpperRight;
                message.gameObject.transform.position = Text_positions[3].transform.position;
                sound.clip = sounds[counter];
                sound.Play();
                break;
                ///////////////////////////////////////////////////////////////////////////
            case 10:
                Sections[3].SetActive(false);
                Sections[4].SetActive(true);
                message.text = m_message[counter];
                message.alignment = TextAnchor.MiddleCenter;
                message.gameObject.transform.position = Text_positions[4].transform.position;
                sound.clip = sounds[counter];
                sound.Play();
                break;
            /////////////////////////////////////////////////////////////////////////////
            case 11:

                m_button.gameObject.SetActive(false);
                PlayerPrefs.SetInt("ToutShown", 1);

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
               // mainData.instance.openLevels(mainData.instance.urls[0], "submenu", 0);
                break;
            default:
                
                break;
        }

        
     
    }
    public void turnOffTut()
    {
        m_button.gameObject.SetActive(false);
        PlayerPrefs.SetInt("ToutShown", 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }


}
