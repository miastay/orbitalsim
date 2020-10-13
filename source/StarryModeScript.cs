using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarryModeScript : MonoBehaviour
{
    public GameObject starImage;
    // Start is called before the first frame update
    void Start()
    {
        starImage = GameObject.Find("StarImage");
        starImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlanetsController.currentMode != PlanetsController.Mode.STARRY)
        {
            starImage.SetActive(false);
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
            starImage.SetActive(true);
            starImage.transform.localScale = Vector3.one * 2;
        }
    }
}
