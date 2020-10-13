using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using Proyecto26;
using System.Net;
using System;
using SimpleJSON;


public class DatabaseManager
{
    private const string projectID = "orbitalsim-9caa2";
    private static readonly string databaseURL = $"https://{projectID}.firebaseio.com/";

    public delegate void PostPresetCallback();

    public static void PostPreset(PresetObject preset, string presetID, PostPresetCallback callback)
    {
        RestClient.Put<PresetObject>($"{databaseURL}presets/{presetID}.json", preset).Then(response => {
            callback();
        });
    }

    public delegate void GetPresetCallback(PresetObject preset);
    public static void GetPreset(string presetID, GetPresetCallback callback)
    {
        RestClient.Get<PresetObject>($"{databaseURL}presets/{presetID}.json").Then(preset =>
        {
            callback(preset);
        });
    }

    private static fsSerializer serializer = new fsSerializer();
    public delegate void GetPresetsCallback(Dictionary<string, PresetObject> presets);
    public static void GetPresets(GetPresetsCallback callback)
    {
        RestClient.Get($"{databaseURL}/presets/.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, PresetObject>), ref deserialized);

            var presets = deserialized as Dictionary<string, PresetObject>;
            callback(presets);
        });
    }




    private static T _download_serialized_json_data<T>(string url, string id) where T : new()
    {

        using (var w = new WebClient())
        {

            var json_data = string.Empty;

            // attempt to download JSON data as a string

            try
            {

                json_data = w.DownloadString($"{databaseURL}presets/{id}.json");

            }

            catch (Exception e) { }

            // if string with JSON data is not empty, deserialize it to class and return its instance

            return !string.IsNullOrEmpty(json_data) ? JsonUtility.FromJson<T>(json_data) : new T();

        }

    }
    private static List<PresetMenuItem> _download_all_serialized_json_data()
    {
        List<PresetMenuItem> downloadedPresetMenuItems = new List<PresetMenuItem>();
        using (var w = new WebClient())
        {

            var json_data = string.Empty;

            // fix this to iterate over all IDS

            try
            {

                json_data = w.DownloadString($"{databaseURL}presets/.json");

            }

            catch (Exception e) { }

            // if string with JSON data is not empty, deserialize it to class and return its instance

            JSONNode data = JSON.Parse(json_data);
            JSONArray dataArr = data.AsArray;
            for(int i = 1; i < dataArr.Count; i++)
            {
                PresetObject coPreset = _download_serialized_json_data<PresetObject>($"{databaseURL}presets/.json", (i+""));
                PresetMenuItem newItem = GameObject.Find("PresetPanel").GetComponent<PresetsManager>().instantiateNewPresetMenuItem((i+""), coPreset);
                downloadedPresetMenuItems.Add(newItem);
            }

            return downloadedPresetMenuItems;

        }

    }
    public static PresetObject RetrieveStoredData(string id)
    {
        PresetObject orbitalDataSettings = _download_serialized_json_data<PresetObject>($"{databaseURL}presets.json", id);
        return orbitalDataSettings;
        //GameObject.Find("Main Camera").GetComponent<PlanetsController>().unpackPresetObject(orbitalDataSettings);
    }

    public async static void RequestDelete(string id)
    {
    }

    public static void RequestPush(string id)
    {

    }

    public static List<PresetMenuItem> RefreshList()
    {
        return _download_all_serialized_json_data();
    }

}
