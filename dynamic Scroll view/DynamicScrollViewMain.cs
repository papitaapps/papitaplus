using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;

public class DynamicScrollViewMain : MonoBehaviour
{
    public static DynamicScrollViewMain instance;
    public Transform scrollViewContent;
    public GameObject prefab;
    GameObject newSpaceShip;
    public List<Sprite> spaceShips;
    public List<GameObject> mybtns;
    public List<GameObject> SpaceShipObj;
    public List<Transform> mybtnsPos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        int productCount = PlayerPrefs.GetInt("productCount");

        for (int i = 0; i < productCount; i++)
        {
            mybtns.Insert(i, prefab);
            mybtnsPos.Insert(i, scrollViewContent);
            SpaceShipObj.Insert(i, newSpaceShip);

            SubMenuManager.instance.prouduct.Insert(i, SubMenuManager.instance.nullGame);
            SubMenuManager.instance.ProuductName.Insert(i, SubMenuManager.instance.nullText);
        }
        loadDataOnBtn();

    }
    int counterM, counterG;
    int counterscrollviews;
    public void loadDataOnBtn()
    {

        int witchCat = PlayerPrefs.GetInt("witchCat");
        int productCountMovie = PlayerPrefs.GetInt("productCountMovie");
        int productCountGame = PlayerPrefs.GetInt("productCountGame");
        int hasBought = PlayerPrefs.GetInt("has_bouth" + witchCat);
        int hasMovie = PlayerPrefs.GetInt("has_Movie" + witchCat);
     
     

        if (witchCat == 5 || witchCat == 25)
        {
            for (int s = 0; s < mybtns.Count; s++)
            {
                int currentValue = PlayerPrefs.GetInt("productCountMovie" + s);

              
               

                if (currentValue > 0)
                {
                    counterscrollviews++;
                    SpaceShipObj[s] = Instantiate(mybtns[s], mybtnsPos[s]);
                    SubMenuManager.instance.prouduct[s] = SpaceShipObj[s];
                    SubMenuManager.instance.ProuductName[s] = SpaceShipObj[s].transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.gameObject.GetComponent<RtlText>();
                    SpaceShipObj[s].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().isMovie = true;
                    SpaceShipObj[s].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().witchScroolView = s;

                    SpaceShipObj[s].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().setProducts();
                }

               

            }

           
        }
        else
        {
            if (productCountMovie > 0 & productCountGame > 0)
            {
                for (int s = 0; s < mybtns.Count; s++)
                {
                    counterscrollviews++;
                    SpaceShipObj[s] = Instantiate(mybtns[s], mybtnsPos[s]);
                    SubMenuManager.instance.prouduct[s] = SpaceShipObj[s];
                    SubMenuManager.instance.ProuductName[s] = SpaceShipObj[s].transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.gameObject.GetComponent<RtlText>();
                    if (hasBought == 1)
                    {
                        if (s == 1)
                        {
                            SpaceShipObj[s].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().isMovie = true;
                            SpaceShipObj[s].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().setProducts();
                        }
                        else
                        {
                            SpaceShipObj[s].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().setProducts();

                        }
                    }
                    else if (hasMovie == 1)
                    {
                        SpaceShipObj[s].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().isMovie = true;
                        SpaceShipObj[s].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().setProducts();
                    }
                }


            }
            else if (productCountGame > 0)
            {
                counterscrollviews++;
                SpaceShipObj[0] = Instantiate(mybtns[0], mybtnsPos[0]);
                SubMenuManager.instance.prouduct[0] = SpaceShipObj[0];
                SubMenuManager.instance.ProuductName[0] = SpaceShipObj[0].transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.gameObject.GetComponent<RtlText>();
                SpaceShipObj[0].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().setProducts();

            }
            else if (productCountMovie > 0)
            {
                counterscrollviews++;
                SpaceShipObj[0] = Instantiate(mybtns[0], mybtnsPos[0]);
                SubMenuManager.instance.prouduct[0] = SpaceShipObj[0];
                SubMenuManager.instance.ProuductName[0] = SpaceShipObj[0].transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.gameObject.GetComponent<RtlText>();
                SpaceShipObj[0].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().isMovie = true;
                SpaceShipObj[0].transform.GetChild(0).transform.GetChild(2).transform.gameObject.GetComponent<DynamicScrollView>().setProducts();

            }
        }

   

        SubMenuManager.instance.getProducts();

        SubMenuManager.instance.howMoutchScrollView(counterscrollviews);
    }

    void OpenLink(string link)
    {
        Debug.Log(link);
    }
    /*
     List<T> someList = new List();
    someList.Add(x)        // Adds x to the end of the list
 someList.Insert(0, x)  // Adds x at the given index
 someList.Remove(x)     // Removes the first x observed
 someList.RemoveAt(0)   // Removes the item at the given index
 someList.Count()       // Always good to know how many elements you have!
 */
}
