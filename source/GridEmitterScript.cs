using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEmitterScript : MonoBehaviour
{
    private int width = 5000;
    private Vector3[] positions;
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[width * width];
        lr = gameObject.GetComponent<LineRenderer>();
        MakeLines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeLines()
    {
        /*
        lr.positionCount = width * width;
        int i = 0;
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < width; y++)
            {
                positions[i] = new Vector3(x*5, y*5, 0f);
                i++;
            }
        }
        lr.SetPositions(positions);
        UnityEngine.Debug.Log("drew");
        */
        for(int i = 1; i < width*2; i++)
        {
            Debug.DrawRay(new Vector3(-width, i, 5f), new Vector3(width*2, 0f, 0f), Color.red, Mathf.Infinity, true);
            Debug.DrawRay(new Vector3(-width, -i, 5f), new Vector3(width*2, 0f, 0f), Color.red, Mathf.Infinity, true);
            Debug.DrawRay(new Vector3(i, -width, 5f), new Vector3(0f, width * 2, 0f), Color.red, Mathf.Infinity, true);
            Debug.DrawRay(new Vector3(-i, -width, 5f), new Vector3(0f, width * 2, 0f), Color.red, Mathf.Infinity, true);
        }
        Debug.DrawRay(new Vector3(-width, 0f, 5f), new Vector3(width*2, 0f, 0f), Color.green, Mathf.Infinity, true);
        Debug.DrawRay(new Vector3(0f, -width, 5f), new Vector3(0f, width*2, 0f), Color.green, Mathf.Infinity, true);
    }
}
