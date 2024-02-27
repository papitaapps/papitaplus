using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
   
{

    public static LevelManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
               
                }
    }
    void Start()
    {
        
    }
    public async void LoadScene(string SceneName)
    {
        var scene = SceneManager.LoadSceneAsync(SceneName);
        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);

        do
        {
            //await Task.Delay(100);
            _progressBar.fillAmount = scene.progress;
        } while (scene.progress<0.95f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
