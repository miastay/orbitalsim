using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yAxis : MonoBehaviour
{
    public GameObject MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0f, MainCamera.transform.position.y, 299f);
    }
}
