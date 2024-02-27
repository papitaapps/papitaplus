using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickkeyboard : MonoBehaviour {

    // Use this for initialization

    public type mytype;
    public string alpha;
    public GameObject intNumBox;
    public Font m_Font;
    public Color m_color;
    public bool testBaazar;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
       // intNumBox.SetActive(true);
        mytype.alphabetFunction(alpha);
        if (!testBaazar)
        {
            GameObject.FindWithTag("phone").GetComponent<Text>().fontSize = 60;
            GameObject.FindWithTag("phone").GetComponent<Text>().font = m_Font;
            GameObject.FindWithTag("phone").GetComponent<Text>().color = m_color;
        }
     
        GetComponent<AudioSource>().Play();
        StartCoroutine(coroutineA());
    }

  
    Image image;
    
    IEnumerator coroutineA()
    {

        image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0.5f;
        image.color = tempColor;
        yield return new WaitForSeconds(0.2f);

         image = GetComponent<Image>();
        var tempColor1 = image.color;
        tempColor.a = 1f;
        image.color = tempColor;

    }

}
