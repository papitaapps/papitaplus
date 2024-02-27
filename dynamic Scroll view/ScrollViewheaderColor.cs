using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;

public class ScrollViewheaderColor : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
       
    }

    private void Start()
    {
        this.gameObject.GetComponent<RtlText>().color = SubMenuManager.instance.loadToColor;
    }
}
