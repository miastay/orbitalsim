using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PresetPanelScript.isPresetsVisible)
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector2(-16f, -79f);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 25f);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector2(-100f, -79f);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
        }
    }
}
