using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowScript : MonoBehaviour
{
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GameObject.Find("FollowButtonText").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlanetsController.follow == PlanetsController.selectedBody)
        {
            gameObject.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
            txt.text = "Following";
        } else
        {
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            txt.text = "Follow";
        }
    }
}
