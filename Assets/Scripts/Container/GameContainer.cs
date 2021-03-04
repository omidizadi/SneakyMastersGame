using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContainer : MonoBehaviour
{
    
    private static Dictionary<Type, MonoBehaviour> storage = new Dictionary<Type, MonoBehaviour>();
    
    public static T ResolveSingleton<T>() where T : MonoBehaviour
    {
        if (storage.ContainsKey(typeof(T)))
            return (T) storage[typeof(T)];
        var instance = FindObjectOfType<T>();
        storage.Add(typeof(T), instance);
        return instance;
    }

    public static void FlushContainer()
    {
        storage.Clear();
    }

}
