using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class PlanetsController : MonoBehaviour
{
    public static List<GameObject> planets = new List<GameObject>();
    public static List<GameObject> resetPlanets = new List<GameObject>();
    public GameObject p1, p2;
    public static bool running;
    public GameObject editPanel;
    public GameObject BodyPrefab;
    public static float extentX = 8, extentY = 4;
    System.Random rand = new System.Random();
    public static GameObject selectedBody;
    public static GameObject follow;
    public static InputField xField, yField, massField, xVelField, yVelField;
    public static Button DeleteButton, ResetButton;
    public static Button DarkModeButton, LightModeButton, StarryModeButton;
    public static Image ColorButton;
    public static GameObject xF, yF, mF, xvF, yvF;
    public static GameObject selectedPanel;
    public static bool panelHover;
    public static float gravityConstant = 10f;
    public static float trailLife = 1f;
    public static Slider GravitySlider;
    public static Text GravitySliderValueText;
    public static Slider TrailSlider;
    public static Text TrailSliderValueText;
    public static bool wasReset;
    public static bool followMassCenter, isGridOn;
    public static Toggle MassCenterToggle, GridToggle;
    public static float camDist = 5f;
    public static Mode currentMode = Mode.DARK;
    public static bool canZoom = true;
    public static bool showColors;
    public GameObject colorCanvas;
    public InputField HexInput;
    public Text versionText;
    public const string version = "Build 0.9b2";
    public FlexibleColorPicker ColorPicker;
    public static bool isAuth;
    public bool showNaming;
    public GameObject PresetNameCanvas;
    public InputField NameInput;
    public GameObject grid;

    public enum Mode
    {
        DARK,
        LIGHT,
        STARRY
    }
    // Start is called before the first frame update
    void Start()
    {
        editPanel = GameObject.Find("PlayPanel");
        xField = GameObject.Find("x-input").GetComponent<InputField>();
        yField = GameObject.Find("y-input").GetComponent<InputField>();
        massField = GameObject.Find("mass-input").GetComponent<InputField>();
        xF = GameObject.Find("x-inputText");
        yF = GameObject.Find("y-inputText");
        mF = GameObject.Find("mass-inputText");
        xVelField = GameObject.Find("xvel-input").GetComponent<InputField>();
        yVelField = GameObject.Find("yvel-input").GetComponent<InputField>();
        xvF = GameObject.Find("xvel-inputText");
        yvF = GameObject.Find("yvel-inputText");
        selectedPanel = GameObject.Find("SelectedPanel");
        DeleteButton = GameObject.Find("DeleteBodyButton").GetComponent<Button>();
        GravitySlider = GameObject.Find("GravitySlider").GetComponent<Slider>();
        GravitySliderValueText = GameObject.Find("GravitySliderValueText").GetComponent<Text>();
        TrailSlider = GameObject.Find("TrailSlider").GetComponent<Slider>();
        TrailSliderValueText = GameObject.Find("TrailSliderValueText").GetComponent<Text>();
        ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        MassCenterToggle = GameObject.Find("MassCenterToggle").GetComponent<Toggle>();
        GridToggle = GameObject.Find("GridToggle").GetComponent<Toggle>();
        versionText = GameObject.Find("VersionText").GetComponent<Text>();
        colorCanvas = GameObject.Find("ColorPickerCanvas");
        ColorButton = GameObject.Find("ColorButton").GetComponent<Image>();
        DarkModeButton = GameObject.Find("DarkModeButton").GetComponent<Button>();
        LightModeButton = GameObject.Find("LightModeButton").GetComponent<Button>();
        StarryModeButton = GameObject.Find("StarryModeButton").GetComponent<Button>();
        HexInput = GameObject.Find("HexInput").GetComponent<InputField>();
        ColorPicker = GameObject.Find("FlexibleColorPicker").GetComponent<FlexibleColorPicker>();
        PresetNameCanvas = GameObject.Find("PresetNameCanvas");
        NameInput = GameObject.Find("NameInputField").GetComponent<InputField>();
        grid = GameObject.Find("GridObject");

        NewBody();
        NewBody();
        xField.onEndEdit.AddListener(OnXInput);
        yField.onEndEdit.AddListener(OnYInput);
        massField.onEndEdit.AddListener(OnMassInput);
        xVelField.onEndEdit.AddListener(OnXVelInput);
        yVelField.onEndEdit.AddListener(OnYVelInput);
        DeleteButton.onClick.AddListener(OnDelete);
        GridToggle.isOn = true;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("EditCanvas").GetComponent<Canvas>().enabled = (!PlanetsController.running);
        panelHover = editPanel.gameObject.GetComponent<PanelHoverScript>().mouseOver;
        if (selectedBody == null)
        {
            selectedPanel.gameObject.SetActive(false);
        } else
        {
            selectedPanel.gameObject.SetActive(true);
            ColorButton.color = selectedBody.GetComponent<BodyControllerScript>().col;
            if (running) {
               updateFields();
            }
        }
        setGravityConstant((float)GravitySlider.value);
        GravitySliderValueText.text = gravityConstant + "";
        if(TrailSlider.value == 5)
        {
            trailLife = Mathf.Infinity;
            TrailSliderValueText.text = "Inf";
        } else
        {
            trailLife = (float)(TrailSlider.value * TrailSlider.value);
            TrailSliderValueText.text = Mathf.Round(trailLife) + "s";
        }
        
        ResetButton.interactable = (resetPlanets.Count != 0);
        if(planets.Count <= 1)
        {
            MassCenterToggle.isOn = false;
            MassCenterToggle.interactable = false;
        } else
        {
            MassCenterToggle.interactable = true;
        }
        
        if (showColors)
        {
            colorCanvas.gameObject.SetActive(true);
        }
        else
        {
            colorCanvas.gameObject.SetActive(false);
        }

        if (showNaming)
        {
            PresetNameCanvas.gameObject.SetActive(true);
        }
        else
        {
            PresetNameCanvas.gameObject.SetActive(false);
        }
        followMassCenter = MassCenterToggle.isOn;
        isGridOn = GridToggle.isOn;
        grid.SetActive(isGridOn);

    }

    public void StopApp()
    {
        Debug.Log("Stopping");
        Application.Quit();
    }

    public static Vector3 getMassCenter()
    {
        float xbar = 0, ybar = 0;
        float i = 0f;
        foreach(GameObject o in planets)
        {
            float m = o.GetComponent<Rigidbody>().mass;
            i += m;
            xbar += (o.transform.position.x * m); ybar += (o.transform.position.y * m);
        }
        return new Vector3((xbar / i), (ybar / i), -10f);

    }
    public static void updateFields() {
        if(selectedBody != null) {
            xField.text = selectedBody.gameObject.transform.position.x + ""; 
            yField.text = selectedBody.gameObject.transform.position.y + "";
            massField.text = selectedBody.GetComponent<Rigidbody>().mass + "";
            xVelField.text = selectedBody.gameObject.GetComponent<BodyControllerScript>().storedForces.x + "";
            yVelField.text = selectedBody.gameObject.GetComponent<BodyControllerScript>().storedForces.y + "";
        }
        WillZoom();
    }
    public void toggleRunning()
    {
        if(!running)
        {
            for (int i = 0; i < planets.Count; i++) {
                planets[i].gameObject.GetComponent<BodyControllerScript>().restoreForces();
            }
            running = true;
        } else
        {
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].gameObject.GetComponent<BodyControllerScript>().storeForces();
                planets[i].gameObject.GetComponent<BodyControllerScript>().SetMaxTime();
            }
            running = false;
        }
    }
    public void saveCurrentAsPreset(string name)
    {
        string idl = PresetsManager.presetMenuItemList.Count + 1 + "";
        PresetObject preset1 = new PresetObject(name, GetAllBodyData());
        PresetMenuItem mItem = GameObject.Find("PresetPanel").GetComponent<PresetsManager>().instantiateNewPresetMenuItem(idl, preset1);
       // mItem.transform.parent = GameObject.Find("Content").transform;

        //mItem.transform.localScale = new Vector3(1f, 1f, 1f);
        DatabaseManager.PostPreset(preset1, idl, () =>
        {
            Debug.Log("Preset Added");
        });
    }


    public void NewBody()
    {
        GameObject newB = Instantiate(BodyPrefab, new Vector3((float)(rand.NextDouble()*extentX) - extentX/2f, (float)(rand.NextDouble()*extentY) - extentY/2f, 0f), Quaternion.identity) as GameObject;
        newB.gameObject.GetComponent<BodyControllerScript>().col = new Color(1f, 0f, 0f);
        newB.gameObject.GetComponent<Rigidbody>().mass = rand.Next()%15;
        resetPlanets.Add(newB);
        planets.Add(newB);
    }
    public void PutBody(float x, float y, float xv, float yv, float mass, Color c)
    {
        GameObject newB = Instantiate(BodyPrefab, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        newB.gameObject.GetComponent<BodyControllerScript>().col = c;
        newB.gameObject.GetComponent<Rigidbody>().mass = mass;
        newB.gameObject.transform.position = new Vector3(x, y, 0f);
        newB.gameObject.GetComponent<BodyControllerScript>().initialForces = new Vector3(xv, yv, 0f);
        newB.gameObject.GetComponent<BodyControllerScript>().storedForces = new Vector3(xv, yv, 0f);
        newB.gameObject.GetComponent<BodyControllerScript>().reassignInitials();
        resetPlanets.Add(newB);
        planets.Add(newB);
    }
    public void Reset()
    {
        if (!resetEqualsCurrent())
        {
            planets = resetPlanets.AsReadOnly().ToList<GameObject>();
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].gameObject.GetComponent<BodyControllerScript>().trail.time = 0f;
                planets[i].gameObject.GetComponent<BodyControllerScript>().trail.enabled = false;
                planets[i].gameObject.SetActive(true);
                planets[i].gameObject.GetComponent<BodyControllerScript>().resetForces();
            }
            GameObject.Find("Main Camera").GetComponent<CameraMotionScript>().goToMassCenter();
        } else
        {
            for (int i = 0; i < planets.Count; i++)
            {
                Destroy(planets[i]);
            }
            planets.Clear();
            resetPlanets.Clear();
        }
    }
    public void ReassignDefault()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            planets[i].GetComponent<BodyControllerScript>().reassignInitials();
        }
    }
    public static bool resetEqualsCurrent() 
    {
        for(int i = 0; i < planets.Count; i++)
        {
            if(planets[i].gameObject.transform.position != planets[i].gameObject.GetComponent<BodyControllerScript>().initialPosition || planets[i].gameObject.GetComponent<BodyControllerScript>().initialForces != planets[i].gameObject.GetComponent<BodyControllerScript>().storedForces) {
                return false;
            }
        }
        return true;
    }
    public void OnXInput(string input)
    {
        try
        {
            selectedBody.gameObject.GetComponent<BodyControllerScript>().trail.time = 0;
            selectedBody.gameObject.transform.position += new Vector3((-1 * selectedBody.gameObject.transform.position.x) + float.Parse(input), 0f, 0f);
        } catch (FormatException e)
        {
            xField.text = selectedBody.gameObject.transform.position.x + "";
        }
        WillZoom();
        
    }
    public void OnYInput(string input)
    {
        try
        {
            selectedBody.gameObject.transform.position += new Vector3(0f, (-1 * selectedBody.gameObject.transform.position.y) + float.Parse(input), 0f);
        } catch (FormatException e)
        {
            yField.text = selectedBody.gameObject.transform.position.y + "";
        }
        WillZoom();
    }
    public void OnXVelInput(string input)
    {
        try
        {
            selectedBody.gameObject.GetComponent<BodyControllerScript>().storedForces += new Vector3((-1 * selectedBody.gameObject.GetComponent<BodyControllerScript>().storedForces.x) + float.Parse(input), 0f, 0f);
        } catch (FormatException e)
        {
            xVelField.text = selectedBody.gameObject.GetComponent<BodyControllerScript>().storedForces.x + "";
        }
        WillZoom();
    }
    public void OnYVelInput(string input)
    {
        try
        {
            selectedBody.gameObject.GetComponent<BodyControllerScript>().storedForces += new Vector3(0f, (-1 * selectedBody.gameObject.GetComponent<BodyControllerScript>().storedForces.y) + float.Parse(input), 0f);
        } catch (FormatException e)
        {
            yVelField.text = selectedBody.gameObject.GetComponent<BodyControllerScript>().storedForces.y + "";
        }
        WillZoom();
    }
    public void OnMassInput(string input)
    {
        try
        {
            selectedBody.gameObject.GetComponent<Rigidbody>().mass = float.Parse(input);
            selectedBody.gameObject.GetComponent<BodyControllerScript>().updateMass();
        } catch (FormatException e)
        {
            massField.text = selectedBody.GetComponent<Rigidbody>().mass + "";
        }
        WillZoom();
    }
    public void OnDelete()
    {
        selectedBody = null;
        resetPlanets.Remove(selectedBody);
        planets.Remove(selectedBody);
        Destroy(selectedBody);
    }
    public void setGravityConstant(float g)
    {
        gravityConstant = g;
    }

    public void nextBody()
    {
        selectedBody = planets[(planets.IndexOf(selectedBody) + 1)%planets.Count];
        updateFields();
    }
    public void lastBody()
    {
        if(planets.IndexOf(selectedBody) == 0)
        {
            selectedBody = planets[planets.Count - 1];
        } else
        {
            selectedBody = planets[(planets.IndexOf(selectedBody) - 1)];
        }
        updateFields();
    }

    public void toggleFollow()
    {
        if(selectedBody != null && follow != selectedBody)
        {
            MassCenterToggle.isOn = false;
            followMassCenter = false;
            follow = selectedBody;
            CameraMotionScript.fCam();
        } else
        {
            follow = null;
        }
    }
    public static string getAllBodyFields()
    {
        string ret = "";
        foreach (GameObject o in planets) {
            ret += Math.Round(o.transform.position.x, 2);
            ret += ",";
            ret += Math.Round(o.transform.position.y, 2);
            ret += ",";
            ret += Math.Round(o.GetComponent<Rigidbody>().velocity.x, 2);
            ret += ",";
            ret += Math.Round(o.GetComponent<Rigidbody>().velocity.y, 2);
        }
        ret += ".";
            return ret;
    }

    public static StringDictDictionary GetAllBodyData()
    {
        StringDictDictionary allBodies = StringDictDictionary.New<StringDictDictionary>();
        foreach(GameObject o in planets)
        {
            allBodies.dictionary.Add(planets.IndexOf(o) + "", o.GetComponent<BodyControllerScript>().getFields());
        }
        return allBodies;
    }

    public void unpackPresetObject(PresetObject o)
    {
        Reset(); Reset();
        resetPlanets.Clear();
        planets.Clear();
        
        foreach (KeyValuePair<string, StringStringDictionary> entry in o.bodies.dictionary)
        {
            int k = -1; string[] values = new string[6];
            foreach(KeyValuePair<string, string> secondary in entry.Value.dictionary)
            {
                k++;
                values[k] = secondary.Value;
                if((k+1) % 6 == 0)
                {
                    Color newColor;
                    bool w = (ColorUtility.TryParseHtmlString(values[5], out newColor));
                    PutBody(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]), float.Parse(values[4]), newColor);
                    k = 0;
                }
          
            }
        }
        ReassignDefault();
    }

    public void DarkMode()
    {
        currentMode = Mode.DARK;
        GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
    }

    public void LightMode()
    {
        currentMode = Mode.LIGHT;
        GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(1f, 1f, 1f);
    }

    public void StarryMode()
    {
        currentMode = Mode.STARRY;
        GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(0.05f, 0f, 0f);
    }

    public void CantZoom()
    {
        canZoom = false;
    }

    public static void WillZoom()
    {
        canZoom = true;
    }

    public void updateVersion()
    {
        versionText.text = version + " on Unity 2019.3.7f1";
    }

    public void colorsOff()
    {
        showColors = false;
        WillZoom();
    }
    public void colorsOn()
    {
        ColorPicker.color = selectedBody.GetComponent<BodyControllerScript>().col;
        HexInput.text = "#" + ColorUtility.ToHtmlStringRGB(ColorPicker.color);
        showColors = true;
    }

    public void setColor()
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(HexInput.text, out newColor))
        {
            selectedBody.GetComponent<BodyControllerScript>().col = newColor;
            selectedBody.GetComponent<BodyControllerScript>().updateTrail();
        }
        colorsOff();
    }

    public void nameOff()
    {
        showNaming = false;
        WillZoom();
    }

    public void nameOn()
    {
        showNaming = true;
    }

    public void setName()
    {
        saveCurrentAsPreset(NameInput.text);
        nameOff();
    }

}
