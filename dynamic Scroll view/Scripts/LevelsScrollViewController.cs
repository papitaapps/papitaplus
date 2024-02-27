using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// LevelsScrollViewController - generate scrollview items
/// handle all things those required for scrollview controller
/// </summary>
public class LevelsScrollViewController : MonoBehaviour
{

    [SerializeField] Text levelNumberText;
    [SerializeField] int countertarget;
    [SerializeField] int startFrom;
    [SerializeField] GameObject[] levelBtnPref;
    [SerializeField] Transform levelBtnParent;
    [SerializeField] playGameScript loadScene;

    private void Start()
    {
       // numberOfLevels = PlayerPrefs.GetInt("countOfarray");
        LoadLevelButtons();
      
 
    }

    // load level buttons on game start
    private void LoadLevelButtons()
    {
        for (int i = startFrom; i <= countertarget; i++)
        {
            if (PlayerPrefs.GetInt("asset_" + PlayerPrefs.GetInt("witchCat") + i.ToString()) == 1)
            {
                GameObject levelBtnObj = Instantiate(levelBtnPref[i], levelBtnParent) as GameObject;
                levelBtnObj.GetComponent<LevelButtonItem>().levelIndex = i;
                levelBtnObj.GetComponent<LevelButtonItem>().levelsScrollViewController = this;
                levelBtnObj.GetComponent<LevelButtonItem>().toggle = false;
                levelBtnObj.GetComponent<LevelButtonItem>().loadasset = loadScene;
                Debug.Log("your downloaded asset is : " + i.ToString());
            }
            else
            {
               
            }
          
        }
    }

    // user defined public method to handle something when user press any level button
    // at present we are just changing level number, in future you can do anything that is required at here
    public void OnLevelButtonClick(int levelIndex)
    {
       // levelNumberText.text = "Level " + (levelIndex + 1);
    }

}
