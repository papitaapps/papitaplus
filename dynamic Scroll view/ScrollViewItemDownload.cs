using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.Networking;

public class ScrollViewItemDownload : MonoBehaviour
{
    // Start is called before the first frame update

    public Image childImage;
    public int flag;
    public bool isMovie;
    public int witchScroolView;//for cat 5
    public int witchCat;//for cat 5
    void Start()
    {
        this.gameObject.transform.GetChild(1).GetComponent<Toggle>().isOn = false;
        Invoke("setButton", 0.2f);
    }
    void setButton()
    {
        if (isMovie)
        {
            if (witchCat == 5)
            {
                GetComponent<Button>().onClick.AddListener(() => OpenLink(flag, PlayerPrefs.GetInt("prouductIDMovie" + PlayerPrefs.GetInt("witchCat") + witchScroolView + flag)));
                if (PlayerPrefs.HasKey("movieLenght" + PlayerPrefs.GetInt("witchCat") + witchScroolView + flag))
                {
                    this.gameObject.transform.GetChild(1).gameObject.SetActive(true); //tag video
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text =
                         PlayerPrefs.GetString("timerCounter" + PlayerPrefs.GetInt("witchCat") + witchScroolView + flag) + " / " +
                        PlayerPrefs.GetString("movieLenght" + PlayerPrefs.GetInt("witchCat") + witchScroolView + flag);
                }
                else
                {
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                }
            }
            else
            {
                GetComponent<Button>().onClick.AddListener(() => SaveLoadImage.instance.OnDownloadVideo(PlayerPrefs.GetString("MoveLink" + PlayerPrefs.GetInt("witchCat") + flag), PlayerPrefs.GetString("MoveLinkName" + PlayerPrefs.GetInt("witchCat") + flag), flag));
                if (PlayerPrefs.HasKey("movieLenght" + PlayerPrefs.GetInt("witchCat") + flag))
                {
                    this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    this.gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Text>().text =
                         PlayerPrefs.GetString("timerCounter" + PlayerPrefs.GetInt("witchCat") + flag) + " / " +
                        PlayerPrefs.GetString("movieLenght" + PlayerPrefs.GetInt("witchCat") + flag);
                }
                else
                {
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                }
            }
         
        }
        else
        {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            if (float.Parse(PlayerPrefs.GetString("Game_Version" + PlayerPrefs.GetInt("witchCat") + flag)) <= float.Parse(Application.version))
            {
                GetComponent<Button>().onClick.AddListener(() => playGameScript.pgs.playGamePressed(flag, PlayerPrefs.GetInt("prouductID" + PlayerPrefs.GetInt("witchCat") + flag), true));
            }
            else
            {
               // GetComponent<Button>().onClick.AddListener(() => OpenBox());
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OpenLink(int f,int id)
    {
        StartCoroutine(SendProudutIdToServer(PlayerPrefs.GetString("base_url") +"/categories/product-user", id));
        if (witchCat == 5)
        {
            AudioController.instance.playSound(1);
            
            AudioController.instance.Stopbackground();
            PlayerPrefs.SetInt("VideoFlag", f);
            PlayerPrefs.SetInt("witchScroolView", witchScroolView);
            SubMenuManager.instance.changeScene("video_1");
        }
        else
        {
            AudioController.instance.playSound(1);
           
            AudioController.instance.Stopbackground();

            PlayerPrefs.SetInt("VideoFlag", f);
            SubMenuManager.instance.changeScene("video_1");
        }
      
    }

    public void downImage(int a)
    {
        if (isMovie)
        {

            if (witchCat == 5)
            {
                if (PlayerPrefs.HasKey("movieLenght" + PlayerPrefs.GetInt("witchCat") + witchScroolView + flag) & File.Exists(Application.persistentDataPath + "VideoScreenShot" + PlayerPrefs.GetInt("witchCat") + witchScroolView + flag + ".png"))
                {
                    LoadImageFromDisk("VideoScreenShot" + PlayerPrefs.GetInt("witchCat") + witchScroolView + flag + ".png");
                    Debug.Log("screen shot taken by user");
                }
                else
                {
                    if (!File.Exists(Application.persistentDataPath + PlayerPrefs.GetString("ScreenShotMovieName" + PlayerPrefs.GetInt("witchCat") + witchScroolView + a)))
                    {
                        Debug.LogError("screen shot Movie Not Exist!");
                        OnLoadImage(PlayerPrefs.GetString("ScreenShotMovie" + PlayerPrefs.GetInt("witchCat") + witchScroolView + a), PlayerPrefs.GetString("ScreenShotMovieName" + PlayerPrefs.GetInt("witchCat") + witchScroolView + a));
                    }
                    else
                    {
                        LoadImageFromDisk(PlayerPrefs.GetString("ScreenShotMovieName" + PlayerPrefs.GetInt("witchCat") + witchScroolView + a));
                    }
                }
            }
            else
            {
                // witchCatDownlaodMenu => az download manager set mishe
                if (PlayerPrefs.HasKey("movieLenght" + PlayerPrefs.GetInt("witchCat") + flag) & File.Exists(Application.persistentDataPath + "VideoScreenShot" + PlayerPrefs.GetInt("witchCat") + flag + ".png"))
                {
                    LoadImageFromDisk("VideoScreenShot" + PlayerPrefs.GetInt("witchCat") + flag + ".png");
                    Debug.Log("screen shot taken by user");
                }
                else
                {
                    if (!File.Exists(Application.persistentDataPath + PlayerPrefs.GetString("ScreenShotMovieName" + PlayerPrefs.GetInt("witchCatDownlaodMenu") + a)))
                    {
                        Debug.LogError("screen shot Movie Not Exist!");
                        OnLoadImage(PlayerPrefs.GetString("ScreenShotMovie" + PlayerPrefs.GetInt("witchCatDownlaodMenu") + a), PlayerPrefs.GetString("ScreenShotMovieName" + PlayerPrefs.GetInt("witchCatDownlaodMenu") + a));
                    }
                    else
                    {
                        LoadImageFromDisk(PlayerPrefs.GetString("ScreenShotMovieName" + PlayerPrefs.GetInt("witchCatDownlaodMenu") + a));
                    }
                }
            }

         
         

        }
        else
        {
            if (!File.Exists(Application.persistentDataPath + PlayerPrefs.GetString("ScreenShotGameName" + PlayerPrefs.GetInt("witchCatDownlaodMenu") + a)))
            {
                Debug.LogError("screen shot Game Not Exist!");
                OnLoadImage(PlayerPrefs.GetString("ScreenShotGame" + PlayerPrefs.GetInt("witchCatDownlaodMenu") + a), PlayerPrefs.GetString("ScreenShotGameName" + PlayerPrefs.GetInt("witchCatDownlaodMenu") + a));

            }
            else
            {
                LoadImageFromDisk(PlayerPrefs.GetString("ScreenShotGameName" + PlayerPrefs.GetInt("witchCatDownlaodMenu") + a));

            }

        }
    }


    public void manageDel()
    {
        if (this.gameObject.transform.GetChild(1).GetComponent<Toggle>().isOn)
        {
            if (this.gameObject.GetComponent<ScrollViewItemDownload>().isMovie)
            {
                playGameScript.pgs.witchDelVideo(flag);
            }
            else
            {
                playGameScript.pgs.witchDel(flag);
            }
           
            //  Debug.Log("you want to delete asset : " + levelIndex.ToString());
        }
        else
        {
            if (this.gameObject.GetComponent<ScrollViewItemDownload>().isMovie)
            {
                playGameScript.pgs.witchNotDelVideo(flag);
            }
            else
            {
                playGameScript.pgs.witchNotDel(flag);
            }
          
            //  Debug.Log("you cancelling delete asset : " + levelIndex.ToString());
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////
    ////////////////////////////////////////



    [SerializeField]
    public string imageURL;
    public string fileName;

    public void OnLoadImage(string url, string Fname)
    {
        imageURL = url;

        fileName = Fname;
        Debug.Log("my url Down:" + imageURL + "\n" + "png name : " + fileName);
        StartCoroutine(LoadTextureFromWeb());

    }

    IEnumerator LoadTextureFromWeb()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
            childImage.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
            childImage.SetNativeSize();
            OnSaveImageButtonClick();


        }
    }

    public void OnLoadImageFromDiskButtonClick()
    {
        if (!File.Exists(Application.persistentDataPath + fileName))
        {
            Debug.LogError("File Not Exist!");
            return;
        }

        // LoadImageFromDisk();
    }
    // load texture bytes from disk and compose sprite from bytes
    public void LoadImageFromDisk(string Fnames)
    {
        fileName = Fnames;
        byte[] textureBytes = File.ReadAllBytes(Application.persistentDataPath + fileName);
        Debug.Log("load image from this Addres : " + Application.persistentDataPath + fileName);
        Texture2D loadedTexture = new Texture2D(0, 0);
        loadedTexture.LoadImage(textureBytes);
        childImage.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
        childImage.SetNativeSize();

    }

    public void OnSaveImageButtonClick()
    {
        if (childImage.sprite == null)
        {
            Debug.LogError("No Image to Save!");
            return;
        }

        WriteImageOnDisk();
    }

    private void WriteImageOnDisk()
    {
        byte[] textureBytes = childImage.sprite.texture.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + fileName, textureBytes);
        Debug.Log("File Written On Disk!");

    }

    /// <summary>
    /// click event - make image blank 
    /// </summary>
    public void OnBlankImageButtonClick()
    {
        childImage.sprite = null;
    }

    IEnumerator SendProudutIdToServer(string uri, int pID)
    {
       
        WWWForm form = new WWWForm();

        form.AddField("products_id", pID);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {

            webRequest.SetRequestHeader("Authorization", "Bearer " + UnityEngine.PlayerPrefs.GetString("user_token"));
            webRequest.SetRequestHeader("Accept", "application/json");
            //point:
            // BypassCertificate class in the main app controller script
            webRequest.chunkedTransfer = false;
            webRequest.certificateHandler = new BypassCertificate();
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            Debug.Log(webRequest.downloadHandler.text);
            if ((webRequest.downloadHandler.text).Contains("Error"))
            {

            }
            else if (System.String.IsNullOrEmpty(webRequest.downloadHandler.text))
            {


            }
            else
            {
                Debug.Log("prouduct id : " + pID + " sent To Server");
            }
        }

    }
}
