using System;
 
using UnityEngine;
 
// ---------------
//  String => Int
// ---------------
[Serializable]
public class StringIntDictionary : SerializableDictionary<string, int> {}
 
// ---------------
//  GameObject => Float
// ---------------
[Serializable]
public class GameObjectFloatDictionary : SerializableDictionary<GameObject, float> {}

[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> {}

[Serializable]
public class StringDictDictionary : SerializableDictionary<string, StringStringDictionary> { }
