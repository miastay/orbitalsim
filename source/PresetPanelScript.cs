using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetPanelScript : MonoBehaviour
{
    public static bool isPresetsVisible;
    public static GameObject OpenPresetsPanel;
    // Start is called before the first frame update
    void Start()
    {
        OpenPresetsPanel = GameObject.Find("OpenPresetsPanel");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPresetsVisible)
        {
            OpenPresetsPanel.gameObject.SetActive(true);
        }
        else
        {
            OpenPresetsPanel.gameObject.SetActive(false);
        }
    }
    public void switchVisibility()
    {
        isPresetsVisible = !isPresetsVisible;
    }
    public void hide()
    {
        isPresetsVisible = false;
    }
}
