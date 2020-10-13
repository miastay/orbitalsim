using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonModifyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MenuPanelScript.isMenuVisible)
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector2(-16f, 80f);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 25f);
        } else
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector2(-100f, 80f);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
        }
    }
}
