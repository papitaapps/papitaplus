using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SaveLoadImage : MonoBehaviour
{
    public static SaveLoadImage instance;
    [SerializeField]
   public Image giftImage;
   public string imageURL;
   public string fileName;

    [Header("movie")]
    public GameObject boxDownlaod;
    public Slider slider;
    public Text percent;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        canDowbloadNext = true;
    }
    public void OnLoadImage(string url , string Fname)
    {
        do
        {
            imageURL = url;

            fileName = Fname;
            Debug.Log("my url Down:" + imageURL + "\n" + "png name : " + fileName);
            StartCoroutine(LoadTextureFromWeb());
            canDowbloadNext = false;
        }
        while (canDowbloadNext);
       
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
            giftImage.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
            giftImage.SetNativeSize();
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
        giftImage.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
        giftImage.SetNativeSize();
        canDowbloadNext = true;
    }

    public void OnSaveImageButtonClick()
    {
        if (giftImage.sprite == null)
        {
            Debug.LogError("No Image to Save!");
            return;
        }

        WriteImageOnDisk();
    }

    private void WriteImageOnDisk()
    {
        byte[] textureBytes = giftImage.sprite.texture.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + fileName, textureBytes);
        Debug.Log("File Written On Disk!");
        LoadImageFromDisk(fileName);
      
        // SubMenuManager.instance.DownloadFinished++;
        // SubMenuManager.instance.getProducts();
    }
    bool canDowbloadNext;
    /// <summary>
    /// click event - make image blank 
    /// </summary>
    public void OnBlankImageButtonClick()
    {
        giftImage.sprite = null;
    }

    ////////////////////////////////////////////////////////////////////




    ////////////////////////////////////////////download video ...



    ///////////////////////////////////////////////////////////////////
    string VideoUrl;
    string VideoName;
    int flag;
   public void OnDownloadVideo(string url , string NameV,int f)
    {
        string _pathToFile = Path.Combine(Application.persistentDataPath, NameV);
        if (File.Exists(_pathToFile))
        {
            /*
            Debug.Log("file exist on : " + _pathToFile);
            PlayerPrefs.SetInt("Video_" + PlayerPrefs.GetInt("witchCat") + f.ToString(), 1);
            PlayerPrefs.SetString("VideoStreamFromFile", "file:///" + _pathToFile);
            PlayerPrefs.SetInt("VideoFlag", f);
            PlayerPrefs.SetString("VideoAddresDel" + PlayerPrefs.GetInt("witchCat")+f, _pathToFile);
         //   Debug.LogError("addres : " + PlayerPrefs.GetString("VideoAddresDel" + PlayerPrefs.GetInt("witchCat") + f));
            SubMenuManager.instance.changeScene("video_1");
            */
        }
        else
        {
            VideoUrl = url;
            VideoName = NameV;
            flag = f;
            StartCoroutine(DownloadVideo());
            boxDownlaod.SetActive(true);
            loadingStart = true;
        }
  
    }
    
    IEnumerator DownloadVideo()
    {
      //  UnityWebRequest www = UnityWebRequest.Get("http://dl.papitafood.ir/product/4/VISOYft2bsyTTnNMb23bvZKbq5FNHmRwSxffAXa71671272155.mp4");
        req = new WWW(VideoUrl);
        yield return req;
       // StartCoroutine(ShowDownloadProgress(req));
        if (!string.IsNullOrEmpty(req.error))
        {
            Debug.Log(req.error);
        }
        else
        {
            string _pathToFile = Path.Combine(Application.persistentDataPath, VideoName);
            loadingStart = true;
           
           

            Debug.Log("video : " + _pathToFile);
           // Directory.CreateDirectory(Application.persistentDataPath + "/" + SaveAddress[i]);
    
          //  File.WriteAllBytes(Application.persistentDataPath + "/" + SaveAddress[i] + "/" + sceneNames[i], www.bytes);
            File.WriteAllBytes(_pathToFile, req.bytes);
              Debug.Log("Done");
            boxDownlaod.SetActive(false);
            if (PlayerPrefs.GetInt("witchCat") == 5)
            {
                PlayerPrefs.SetInt("Video_" + PlayerPrefs.GetInt("witchCat") + PlayerPrefs.GetInt("witchScroolView") + flag.ToString(), 1);
                PlayerPrefs.SetString("VideoStreamFromFile" + PlayerPrefs.GetInt("witchCat") + PlayerPrefs.GetInt("witchScroolView") + flag.ToString(), "file:///" + _pathToFile);
                PlayerPrefs.SetInt("VideoFlag", flag);
                PlayerPrefs.SetString("VideoAddresDel" + PlayerPrefs.GetInt("witchCat") + PlayerPrefs.GetInt("witchScroolView") + flag, _pathToFile);
                PlayerPrefs.SetInt("toutDownloadVideo", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Video_" + PlayerPrefs.GetInt("witchCat") + flag.ToString(), 1);
                PlayerPrefs.SetString("VideoStreamFromFile" + PlayerPrefs.GetInt("witchCat") + flag.ToString(), "file:///" + _pathToFile);
                PlayerPrefs.SetInt("VideoFlag", flag);
                PlayerPrefs.SetString("VideoAddresDel" + PlayerPrefs.GetInt("witchCat") + flag, _pathToFile);
                PlayerPrefs.SetInt("toutDownloadVideo", 1);
            }
            
            slider.gameObject.SetActive(false);
            
           // SubMenuManager.instance.changeScene("video_1");

            while (req.isDone)
            {
               loadingStart = false;
                yield return new WaitForSeconds(.1f);
            }
       
        }
    }
    public bool loadingStart;
    WWW req;
    private void Update()
    {
        if (loadingStart)
        {
            double v = req.progress;

            slider.value = (float)v;


            v = System.Math.Round(v, 2);

            v *= 100;
            percent.text = "" + v + "%";//portrait
                                        // percent2.text = "" + v + "%";//landscape
        }

    }
}
