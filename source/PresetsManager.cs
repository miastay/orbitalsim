using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

public class PresetsManager : MonoBehaviour
{
    public GameObject PresetItemPrefab;
    public static List<PresetMenuItem> presetMenuItemList = new List<PresetMenuItem>();
    // Start is called before the first frame update
    void Start()
    {
        RefreshListOfPresets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveCurrent()
    {
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(PlanetsController.getAllBodyFields());
        string encodedText = Convert.ToBase64String(bytesToEncode);
        Debug.Log(encodedText);
    }

    public void importNew()
    {

    }
    public void decode(string key)
    {

    }


    public PresetMenuItem instantiateNewPresetMenuItem(string id, PresetObject ob)
    {
        GameObject item = Instantiate(PresetItemPrefab) as GameObject;
        PresetMenuItem menuItem = item.GetComponent<PresetMenuItem>();
        menuItem.create(id, ob);
        presetMenuItemList.Add(menuItem);
        return menuItem;
    }

    public void RetrieveAllStoredPresets(string id)
    {
        /*
        DatabaseManager.GetPresets(presets =>
        {
            foreach (var preset in presets)
            {
                Debug.Log($"{preset.Value.name}");
            }
        });
        */
        DatabaseManager.RetrieveStoredData(id);
    }
    public void RefreshListOfPresets()
    {
        foreach(PresetMenuItem i in presetMenuItemList)
        {
            Destroy(i.gameObject);
        }
        presetMenuItemList = DatabaseManager.RefreshList();
    }
}
