using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public GameObject MainCamera;
    public int gridSize;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        gridSize = 30 + (int)(PlanetsController.camDist*2);
       // transform.localScale = new Vector3((int)PlanetsController.camDist*8, (int)PlanetsController.camDist*8, 1f);
        transform.position = new Vector3((int)MainCamera.transform.position.x, (int)MainCamera.transform.position.y, 300f);
    }
}
