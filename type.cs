using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class type : MonoBehaviour {

    public bool phonenumber,kodtaeed;
    string word = null;
    public int wordIndex = -1;
    string alpha=null,alpha2=null;
    public Text myName = null;
    char[] nameChar = new char[30];
    public Text index=null;
    public GameObject mybtn;
   
  
    int show;
    bool mustcheck = true;
   

    // Use this for initialization
    void Start()
    {
      
    }
  
    void Update()
    {

        if (mustcheck)
        {
            SubmitPasswordButton();
        }



    }

    public void alphabetFunction(string alphabet)
    {
        if (phonenumber)
        {
            if (wordIndex <= 9)
            {
                wordIndex++;
                char[] keepchar = alphabet.ToCharArray();
                nameChar[wordIndex] = keepchar[0];
                alpha = nameChar[wordIndex].ToString();
                word = word + alpha;
                myName.text = word;
                index.text = wordIndex.ToString();
            }
        }
        else if(kodtaeed)
        {
            if (wordIndex <= 5)
            {
                wordIndex++;
                char[] keepchar = alphabet.ToCharArray();
                nameChar[wordIndex] = keepchar[0];
                alpha = nameChar[wordIndex].ToString();
                word = word + alpha;
                myName.text = word;
                index.text = wordIndex.ToString();
            }
        }
        else
        {
            if (wordIndex <= 3)
            {
                wordIndex++;
                char[] keepchar = alphabet.ToCharArray();
                nameChar[wordIndex] = keepchar[0];
                alpha = nameChar[wordIndex].ToString();
                word = word + alpha;
                myName.text = word;
                index.text = wordIndex.ToString();
            }
        }
     

  
   
       


    }
    public void del()
    {

        if (wordIndex >= 0)
        {
            


          //  mybtn.SetActive(false);
            wordIndex--;
            alpha2 = null;
            for(int i = 0; i < wordIndex + 1; i++)
            {
                alpha2 = alpha2 + nameChar[i].ToString();
            }

            word = alpha2;
            myName.text = word;
            index.text = wordIndex.ToString();
            mustcheck = true;

            if(phonenumber)
                mybtn.GetComponent<Button>().interactable = false;
        }


    }

    bool checkNetworkAvailability()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SubmitPasswordButton()
    {
        //Check if length of the string is less than 8
     
        //Check if the length of the string is more than or equal to 8
        if (System.Text.RegularExpressions.Regex.IsMatch(myName.text, @"^(\+98|0)?9\d{9}$"))
        {
            // Debug.Log("Password Accepted!");

           

            try
            {
                if (checkNetworkAvailability())
                {
                    if (phonenumber)
                    {
                        mybtn.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        Invoke("change", 1f);
                    }
                   
                  
                }
                else
                {

                    mybtn.GetComponent<Button>().interactable = false;
                }

            }
            catch (System.Exception)
            {
            }

        }


 

    }
    GameObject btnNum;
    void change()
    {
        btnNum = GameObject.Find("back num + btn");
       // btnNum.GetComponent<Button>().interactable = true;
        mybtn.SetActive(true);
       

        mustcheck = false;
      

    }

    void OnMouseDown()
    {
        Debug.Log("sss");
    }

    public void sabtshomare()
    {
      
        Invoke("fadeout", 2f);
    }

    void fadeout()
    {
     //   internumber.SetActive(false);
    }
}
