using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollDownlaoded : MonoBehaviour
{
    // Start is called before the first frame update
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;
    void Start()
    {
        for(int a = 0; a <= 20; a++)
        {
            generateItem(a);
        }
        //  scrollView.verticalNormalizedPosition = 1;
        scrollView.horizontalNormalizedPosition = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateItem(int itemNumber)
    {
        GameObject scrollItemObj = Instantiate(scrollItemPrefab);
        scrollItemObj.transform.SetParent(scrollContent.transform,false);
       // scrollItemObj.transform.Find("num").gameObject.GetComponent<Text>().text = itemNumber.ToString();
    }
    public void changeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
