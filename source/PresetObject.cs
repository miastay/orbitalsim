using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PresetObject
{
    public string name;
    public StringDictDictionary bodies;

    public PresetObject(string name, StringDictDictionary bodies)
    {
        this.name = name;
        this.bodies = bodies;
    }
    public PresetObject() { }
}
