using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class PresetMenuItem : MonoBehaviour
{
    public string id;
    public PresetObject storedPreset;
    public GameObject presetName;
    public GameObject pullButton;
    public GameObject deleteButton;
    private bool editable = true;
    public int y;
    // Start is called before the first frame update
    void Start()
    {

        transform.SetParent(GameObject.Find("Content").GetComponent<RectTransform>(), true);
        //this.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        presetName.GetComponent<Text>().text = storedPreset.name;
        //connectPreset();
    }

    // Update is called once per frame
    void Update()
    {
        //y = (PresetsManager.presetMenuItemList.IndexOf(this)*-22)-13;
        this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        
        if (!editable && !PlanetsController.isAuth)
        {
            deleteButton.GetComponent<Button>().interactable = (false);
        } else
        {
            deleteButton.GetComponent<Button>().interactable = (true);
        }

    }

    public void create(string id, PresetObject obj)
    {
        this.id = id;
        this.storedPreset = obj;
    }

    public void connectPreset()
    {
        storedPreset = DatabaseManager.RetrieveStoredData(id);
        presetName.GetComponent<Text>().text = storedPreset.name;
    }

    public void putPresetInSpace()
    {
        GameObject.Find("Main Camera").GetComponent<PlanetsController>().unpackPresetObject(storedPreset);
    }

    public void DeletePreset()
    {
        Destroy(this.gameObject);
        DatabaseManager.RequestDelete(id);
    }


}
