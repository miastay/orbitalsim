using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class BodyControllerScript : MonoBehaviour
{
    public Rigidbody rigidbody;
    public TrailRenderer trail;
    public Vector3 storedForces;
    public Vector3 initialForces;
    public Vector3 initialPosition;
    public Color col = new Color(1f, 0.5f, 0f);
    public Vector3 offset;
    public bool drag;
    public string name;
    public Material mat;
    System.Random rand = new System.Random();
    private int matID;
    
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        trail = gameObject.GetComponent<TrailRenderer>();
        initialPosition = rigidbody.position;
        mat = gameObject.GetComponent<Renderer>().material;
        matID = Shader.PropertyToID("Tint");
       // col = new Color(1f, 0f, 0f);
        //initialForces = rigidbody.velocity;
        updateMass();
        trail.enabled = true;
    }
    void Update()
    {
        //trail.time = (5 / rigidbody.velocity.sqrMagnitude);
        mat.SetColor("_Color", col);
    }
    void FixedUpdate()
    {
        float g = PlanetsController.gravityConstant;
        if(PlanetsController.running)
        {
            trail.emitting = true; trail.enabled = true;
            trail.time = PlanetsController.trailLife;
            Vector3 resForce = Vector3.zero;
            for (int i = 0; i < PlanetsController.planets.Count; i++)
            {
                if (PlanetsController.planets[i].transform != transform)
                {
                    Vector3 dir = PlanetsController.planets[i].gameObject.transform.position - transform.position;
                    float dist2 = dir.sqrMagnitude;
                    float gForce = g * rigidbody.mass * PlanetsController.planets[i].gameObject.GetComponent<BodyControllerScript>().rigidbody.mass / dist2;
                    resForce += (dir.normalized * gForce);
                }
            }
            rigidbody.AddForce(resForce);
        } else
        {
            /*
            if (Input.GetMouseButton(0) && PlanetsController.selectedBody != null
                && PlanetsController.selectedBody.GetComponent<BodyControllerScript>().drag == false
                && !PlanetsController.panelHover)
            {
                PlanetsController.selectedBody = null;
            }
            */
            
            trail.emitting = false;
        }
    }
    public void SetMaxTime()
    {
        trail.time = Mathf.Infinity;
    }
    void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        PlanetsController.selectedBody = this.gameObject;
        PlanetsController.updateFields();
        drag = true;
    }
    void OnMouseUp()
    {
        if(!Input.GetMouseButton(0))
            drag = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<BodyControllerScript>().rigidbody.mass >= rigidbody.mass && PlanetsController.running)
        {
            PlanetsController.planets.Remove(this.gameObject);
            trail.time = 0;
            PlanetsController.selectedBody = null;
            gameObject.SetActive(false);
        }
    }
    void OnMouseDrag()
    {
        if (!PlanetsController.running && PlanetsController.selectedBody == this.gameObject)
        {
            
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            PlanetsController.updateFields();
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            curPosition.z = 0f;
            rigidbody.position = curPosition; gameObject.transform.position = curPosition;
        }
    }
    public void storeForces()
    {
        storedForces = rigidbody.velocity;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
    }
    public void restoreForces()
    {
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.velocity = storedForces;
    }
    public void resetForces()
    {
        rigidbody.position = initialPosition;
        gameObject.transform.position = initialPosition;
        storedForces = initialForces;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
    }
    public void giveRandomForces()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3((float)rand.NextDouble(), (float)rand.NextDouble(), 0f);
    }
    public void reassignInitials()
    {
        initialForces = storedForces;
        initialPosition = gameObject.transform.position;
    }
    public void updateMass() {
        gameObject.transform.localScale = new Vector3(((float)rigidbody.mass/20) + 0.1f, ((float)rigidbody.mass / 20) + 0.1f, 0f);
        updateTrail();
    }

    public void updateTrail()
    {
        trail.startWidth = rigidbody.mass / 20f;
        trail.startColor = col;
        trail.endColor = col / 1.5f;
    }

    public StringStringDictionary getFields()
    {
        StringStringDictionary fields = StringStringDictionary.New<StringStringDictionary>();
        fields.dictionary.Add("x", gameObject.transform.position.x +"");
        fields.dictionary.Add("y", gameObject.transform.position.y + "");
        fields.dictionary.Add("xv", initialForces.x + "");
        fields.dictionary.Add("yv", initialForces.y + "");
        fields.dictionary.Add("mass", rigidbody.mass + "");
        fields.dictionary.Add("col", "#" + ColorUtility.ToHtmlStringRGB(col));
        return fields;
    }

}
