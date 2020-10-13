using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkModeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlanetsController.currentMode != PlanetsController.Mode.DARK)
        {
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        } else
        {
            gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
        }
    }

}
