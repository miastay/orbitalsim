using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityArrowScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlanetsController.selectedBody != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = (true);
            gameObject.transform.position = PlanetsController.selectedBody.gameObject.transform.position;
            Vector3 rotation = new Vector3(0f, 0f, 0f);
            BodyControllerScript sc = PlanetsController.selectedBody.gameObject.GetComponent<BodyControllerScript>();

            if (!PlanetsController.running)
            {
                rotation.z = (float)((Mathf.Atan2(sc.storedForces.y, sc.storedForces.x) * 180) / Mathf.PI) + 180;
                gameObject.transform.eulerAngles = rotation;
                gameObject.transform.localScale = new Vector3(Mathf.Sqrt((sc.storedForces.x * sc.storedForces.x) + (sc.storedForces.y * sc.storedForces.y)) / 10f, 0.19f, 1f);
            } else
            {
                Rigidbody rb = sc.gameObject.GetComponent<Rigidbody>();
                rotation.z = (float)((Mathf.Atan2(rb.velocity.y, rb.velocity.x) * 180) / Mathf.PI) + 180;
                gameObject.transform.eulerAngles = rotation;
                gameObject.transform.localScale = new Vector3(Mathf.Sqrt((rb.velocity.x * rb.velocity.x) + (rb.velocity.y * rb.velocity.y)) / 10f, 0.19f, 1f);
            }
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = (false);
        }
    }
}
