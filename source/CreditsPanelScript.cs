using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanelScript : MonoBehaviour
{
    public GameObject CreditsCanvas;
    public static bool isAboutVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        CreditsCanvas = GameObject.Find("CreditsCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (isAboutVisible)
        {
            CreditsCanvas.gameObject.SetActive(true);
        }
        else
        {
            CreditsCanvas.gameObject.SetActive(false);
        }
    }
    public void switchVisibility()
    {
        isAboutVisible = !isAboutVisible;
    }
    public void hide()
    {
        isAboutVisible = false;
    }
    public void show()
    {
        isAboutVisible = true;
    }
}
