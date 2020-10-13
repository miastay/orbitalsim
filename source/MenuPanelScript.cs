using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelScript : MonoBehaviour
{
    public static bool isMenuVisible;
    public static GameObject OpenMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        OpenMenuPanel = GameObject.Find("OpenMenuPanel");
    }

    // Update is called once per frame
    void Update()
    {
        if(isMenuVisible)
        {
            OpenMenuPanel.gameObject.SetActive(true);
        } else
        {
            OpenMenuPanel.gameObject.SetActive(false);
        }
    }
    public void switchVisibility()
    {
        isMenuVisible = !isMenuVisible;
    }
    public void hide()
    {
        isMenuVisible = false;
    }
}
