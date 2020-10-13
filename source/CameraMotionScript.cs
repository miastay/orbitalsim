using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(PlanetsController.followMassCenter)
        {
            gameObject.transform.position = PlanetsController.getMassCenter();
        } else if(PlanetsController.follow != null && PlanetsController.running) {
            gameObject.transform.position = new Vector3(PlanetsController.follow.gameObject.transform.position.x, PlanetsController.follow.gameObject.transform.position.y, -10f);
        } else
        {
            //gameObject.transform.position = new Vector3(0f, 0f, -10f);
            
            }
        if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f)
        {
            PlanetsController.follow = null;
            PlanetsController.MassCenterToggle.isOn = false;
            PlanetsController.followMassCenter = false;
        }
        gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * PlanetsController.camDist, Input.GetAxis("Vertical") * Time.deltaTime * PlanetsController.camDist, 0f);


        if (Input.GetKey(KeyCode.Minus) && PlanetsController.camDist < 15f && PlanetsController.canZoom)
        {
            PlanetsController.camDist += 0.05f;
        }
        if (Input.GetKey(KeyCode.Equals) && PlanetsController.camDist > 2f && PlanetsController.canZoom)
        {
            PlanetsController.camDist -= 0.05f;
        }
        GetComponent<Camera>().orthographicSize = PlanetsController.camDist;
    }
    public static void fCam()
    {
        GameObject.Find("Main Camera").gameObject.transform.position = new Vector3(PlanetsController.follow.gameObject.transform.position.x, PlanetsController.follow.gameObject.transform.position.y, -10f);
    }
    public void goToMassCenter()
    {
        gameObject.transform.position = PlanetsController.getMassCenter();
    }
}
