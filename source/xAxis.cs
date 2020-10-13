using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xAxis : MonoBehaviour
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
        transform.position = new Vector3(MainCamera.transform.position.x, 0f, 299f);
    }
}
