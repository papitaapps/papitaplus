using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// LevelButtonItem - attached to level button
/// handle specific button related actions
/// </summary>
public class LevelButtonItem : MonoBehaviour
{

    [HideInInspector] public int levelIndex;
    [HideInInspector] public bool toggle=true;
    [HideInInspector] public LevelsScrollViewController levelsScrollViewController;
    [HideInInspector] public playGameScript loadasset;
    //
    [SerializeField] Text levelButtonText;


    private void Start()
    {
        levelButtonText.text =  (levelIndex + 1).ToString();
        if (toggle == false)
        {
            this.gameObject.transform.GetChild(1).GetComponent<Toggle>().isOn = false;
        }
    }

    // click event of level button
    public void OnLevelButtonClick()
    {
        // levelsScrollViewController.OnLevelButtonClick(levelIndex);
        loadasset.playGamePressed(levelIndex,0,false);
    }

    void Update()
    {
       
    }
    public void manageDel()
    {
        if (this.gameObject.transform.GetChild(1).GetComponent<Toggle>().isOn)
        {
            loadasset.witchDel(levelIndex);
          //  Debug.Log("you want to delete asset : " + levelIndex.ToString());
        }
        else
        {
            loadasset.witchNotDel(levelIndex);
          //  Debug.Log("you cancelling delete asset : " + levelIndex.ToString());
        }
    }


}
