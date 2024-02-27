using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
	
	
	public GameObject loadingScreen;
	//public Slider slider;
    public Image slider;
    void Start()
    {
        loadingScreen.SetActive(false);
    }
    public void LoadLevelbuy(string sceneIndex)

    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        UnityEngine.PlayerPrefs.SetInt("he is in game", 1);
    }


    public void LoadLevel (string sceneIndex)
	 
	 {
		 StartCoroutine(LoadAsynchronously(sceneIndex));
	 }
	 
	 IEnumerator LoadAsynchronously (string sceneIndex)
	 {
		 AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);
		 
		 loadingScreen.SetActive(true);
		 
		 while (!operation.isDone)
		 {
			 float progress = Mathf.Clamp01(operation.progress / 0.9f);
			 
			 slider.fillAmount = progress;
			
			 
			 yield return null;
		 }
		 
	 }	 
}
	
