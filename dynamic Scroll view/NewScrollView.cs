using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScrollView : MonoBehaviour
{
    public static NewScrollView instance;
    public Transform scrollViewContent;
    public GameObject prefab;
    GameObject newSpaceShip;
    public List<Sprite> spaceShips;
    public List<GameObject> mybtns;
    public List<GameObject> SpaceShipObj;
    public List<Transform> mybtnsPos;
    public bool isMovie, ended;
    public int witchScroolView;
    [HideInInspector] public ScrollRectEx ssss;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


       // ssss = this.gameObject.transform.parent.transform.parent.gameObject.GetComponent<ScrollRectEx>();
    }
    void Start()
    {

        ended = false;

        if (!isMovie)
            witchScroolView = 100;

        string savedFloat = PlayerPrefs.GetInt("prouductIDMovie" + PlayerPrefs.GetInt("witchCat")).ToString() + witchScroolView.ToString();

        if (!isMovie)
        {
            int howMoutchProduct = PlayerPrefs.GetInt("howMoutchProduct" + PlayerPrefs.GetInt("witchCat").ToString());

            if (PlayerPrefs.GetInt("productCountGame") > howMoutchProduct && howMoutchProduct > 3)
            {
                PlayerPrefs.SetInt("howMoutchProduct" + PlayerPrefs.GetInt("witchCat").ToString(), PlayerPrefs.GetInt("productCountGame"));
                StartCoroutine(SmoothScroll(0.95f, true));
            }
            else
            {
                Debug.Log(PlayerPrefs.GetFloat(savedFloat));
                float targetValue = PlayerPrefs.GetFloat(savedFloat);
              //  StartCoroutine(SmoothScroll(targetValue, false));
            }
        }
        else
        {
            Debug.Log(PlayerPrefs.GetFloat(savedFloat));
            float targetValue = PlayerPrefs.GetFloat(savedFloat);
           // StartCoroutine(SmoothScroll(targetValue, false));
        }
        setProducts();
    }

    IEnumerator SmoothScroll(float targetValue, bool showmessage)
    {
        yield return new WaitForSeconds(1.5f);
        float duration = 0.5f;
        float currentVelocity = 0f;
        float startValue = ssss.horizontalScrollbar.value;

        while (Mathf.Abs(ssss.horizontalScrollbar.value - targetValue) > 0.01f)
        {
            ssss.horizontalScrollbar.value = Mathf.SmoothDamp(ssss.horizontalScrollbar.value, targetValue, ref currentVelocity, duration);
            yield return null;
        }

        ssss.horizontalScrollbar.value = targetValue;
        if (showmessage) GameObject.Find("box message").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void setProducts()
    {
        // gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(gameObject.transform.GetChild(0).gameObject);
        int witchCat = PlayerPrefs.GetInt("witchCat");
        int productCountMovie = PlayerPrefs.GetInt("productCountMovie" + witchScroolView);
        int productCountGame = PlayerPrefs.GetInt("productCountGame");

        if (witchCat == 5 || witchCat == 25)
        {
            if (productCountMovie > 0)
            {
                AddItemsToList(productCountMovie);
                loadDataOnBtn();
            }
        }
        else
        {
            if (productCountMovie > 0 && productCountGame > 0)
            {
                int maxCount = Mathf.Max(productCountMovie, productCountGame);
                AddItemsToList(maxCount);
                Debug.Log("Movie and game have products");
                loadDataOnBtn();
            }
            else if (productCountMovie > 0)
            {
                AddItemsToList(productCountMovie);
                Debug.Log("Only movie has products");
                loadDataOnBtn();
            }
            else if (productCountGame > 0)
            {
                AddItemsToList(productCountGame);
                Debug.Log("Only game has products");
                loadDataOnBtn();
            }
        }
    }

    private void AddItemsToList(int count)
    {
        for (int i = 0; i < count; i++)
        {
            mybtns.Add(prefab);
            mybtnsPos.Add(scrollViewContent);
            SpaceShipObj.Add(newSpaceShip);
        }
    }
    int counterM, counterG;
    public void loadDataOnBtn()
    {

        if (isMovie)
        {
            if (PlayerPrefs.GetInt("witchCat") == 5 | PlayerPrefs.GetInt("witchCat") == 25)
            {
                int productCount = PlayerPrefs.GetInt("productCountMovie" + witchScroolView);
                if (productCount > 0)
                {
                    StartCoroutine(CreateSpaceShips(productCount, true));
                }
            }
            else
            {
                int productCount = PlayerPrefs.GetInt("productCountMovie");
                if (productCount > 0)
                {
                    StartCoroutine(CreateSpaceShips(productCount, true));
                }
            }




        }
        else
        {

            int productCount = PlayerPrefs.GetInt("productCountGame");
            if (productCount > 0)
            {
                StartCoroutine(CreateSpaceShips(productCount, false));
            }

        }

    }

    private IEnumerator CreateSpaceShips(int productCount, bool isMovie)
    {
        const int batchSize = 3;
        List<Sprite> sprites = new List<Sprite>();

        for (int i = 0; i < productCount; i++)
        {
            sprites.Add(SaveLoadImage.instance.giftImage.sprite);

            if (i % batchSize == 0)
            {
                yield return new WaitForEndOfFrame(); // انتظار بین دسته‌ها
            }
        }

        AddItemsToList(productCount);

        for (int s = 0; s < sprites.Count; s++)
        {
            GameObject newSpaceShip = Instantiate(mybtns[s], mybtnsPos[s]);
            newSpaceShip.GetComponent<NewItem>().flag = s;
            newSpaceShip.GetComponent<NewItem>().witchScroolView = witchScroolView;
            newSpaceShip.GetComponent<NewItem>().isMovie = isMovie;
            newSpaceShip.GetComponent<NewItem>().downImage(s);
            if (s == sprites.Count - 1) newSpaceShip.GetComponent<NewItem>().lastItem = true;
            SpaceShipObj.Add(newSpaceShip);

            yield return new WaitForEndOfFrame(); // انتظار بین ایجاد spaceShip ها در هر دسته
        }


        ended = true;
        Debug.Log(ended);
    }

    void OpenLink(string link)
    {
        Debug.Log(link);
    }

}
