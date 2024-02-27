using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicScroolViewDownloaded : MonoBehaviour
{
    public static DynamicScroolViewDownloaded instance;
    public int WitchCat;
    public Transform scrollViewContent;
    public GameObject prefab;
    public Sprite sp;
    GameObject newSpaceShip;
    public List<Sprite> spaceShips;
    public List<GameObject> mybtns;
    public List<GameObject> SpaceShipObj;
    public List<Transform> mybtnsPos;

   [HideInInspector] public int counterMenu;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
       


    }
    public void preLoad()
    {
     
        


        for (int i = 0; i < counterMenu; i++)
        {
            mybtns.Insert(i, prefab);
            mybtnsPos.Insert(i, scrollViewContent);
            SpaceShipObj.Insert(i, newSpaceShip);
          
        }

     

    }

    public void loadDataOnBtn()
    {
      this.gameObject.transform.GetChild(0).gameObject.SetActive(false);


        spaceShips.Clear();

       
        for (int i = 0; i < counterMenu; i++)
            {
                spaceShips.Insert(i, sp);
            }
        Debug.Log("spaceships : " + spaceShips.Count);
            for (int s = 0; s < spaceShips.Count; s++)
            {
          
            if (PlayerPrefs.GetInt("asset_" + WitchCat + s) == 1)
            {

               
                SpaceShipObj[s] = Instantiate(mybtns[s], mybtnsPos[s]);
                SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().flag = s;
                SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().isMovie = false;
                SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().downImage(s);
            }
          

            }

        for (int s = 0; s < spaceShips.Count; s++)
        {
            if (WitchCat == 5)
            {
                for (int i = 0; s < PlayerPrefs.GetInt("howmoutchMovie"); i++)
                {
                    if (PlayerPrefs.GetInt("Video_" + WitchCat +i+ s) == 1)
                    {


                        SpaceShipObj[s] = Instantiate(mybtns[s], mybtnsPos[s]);
                        SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().flag = s;
                        SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().witchScroolView = i;//for cat 5
                        SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().witchCat = WitchCat;//for cat 5
                        SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().isMovie = true;
                        SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().downImage(s);
                    }
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("Video_" + WitchCat + s) == 1)
                {


                    SpaceShipObj[s] = Instantiate(mybtns[s], mybtnsPos[s]);
                    SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().flag = s;
                    SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().isMovie = true;
                    SpaceShipObj[s].GetComponent<ScrollViewItemDownload>().downImage(s);
                }

            }



        }

    }

    void OpenLink(string link)
    {
        Debug.Log(link);
    }

}
