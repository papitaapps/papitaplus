using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using System.IO;



using System.Text.RegularExpressions;

using System.Runtime.InteropServices;

using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class AddContact : MonoBehaviour

{

#if UNITY_IPHONE

[DllImport("__Internal")]

private static extern bool saveToGallery(string path);

[DllImport("__Internal")]

private static extern void iOSTesseract();

#endif



    public string nameField;

    public string phoneField;

    public string EmailField;

    public string AddressField;




   public  void openUrl()
    {
        //  Application.OpenURL("https://grnt.link/grnt1020#");
        Application.OpenURL("https://islandofkids.com/app/assetBundle/lamsa/vcard.vcf");
        //StartCoroutine(DownloadFile());
    }




    public void OnbtnContactField()

    {

        //checkPage = 0;

        String displayName = nameField;

        String phoneNumber = phoneField;

        String phoneTypeStr = "mobile"; //home, mobile, work


        //contactLib.CallStatic("addContact", activityContext, displayName,
        //phoneNumber, phoneTypeStr);

        String emailaddress = EmailField;

        String address = AddressField;

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject jcurrentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");

        jcurrentActivity.CallStatic("addContact", jcurrentActivity,
        displayName, phoneNumber, phoneTypeStr, emailaddress, address);

       // OnbtnBack();



    }
    IEnumerator DownloadFile()
    {
      
        var uwr = new UnityWebRequest("https://islandofkids.com/app/assetBundle/lamsa/grnt-grnt1020.vcf", UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.persistentDataPath, "grnt-grnt1020.vcf");
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();
       
            Debug.Log("File successfully downloaded and saved to " + path);
        Application.OpenURL((Application.persistentDataPath) + "grnt-grnt1020.vcf");
    }

}
