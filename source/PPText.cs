using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PPText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlanetsController.running)
        {
            gameObject.GetComponent<Text>().text = "Pause";
        } else
        {
            gameObject.GetComponent<Text>().text = "Play";
        }
    }
}
