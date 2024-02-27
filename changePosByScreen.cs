using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePosByScreen : MonoBehaviour
{

    public Transform pos2400,pos1920;
    
    void Start()
    {
      
            if (Screen.height > 2300 | Screen.width > 2300)
            {

               // this.gameObject.transform.position = pos2400.position;
               
               

            }
            else
            {
               // this.gameObject.transform.position = pos1920.position;
            }
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
