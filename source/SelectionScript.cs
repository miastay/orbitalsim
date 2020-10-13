using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlanetsController.selectedBody != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.transform.position = PlanetsController.selectedBody.gameObject.transform.position;
            gameObject.transform.position -= new Vector3(0f, 0f, -1f);
            rigidbody = PlanetsController.selectedBody.gameObject.GetComponent<Rigidbody>();
            gameObject.transform.localScale = new Vector3(((float)rigidbody.mass / 20f) + 0.3f, ((float)rigidbody.mass / 20f) + 0.3f, 0f);
        } else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
