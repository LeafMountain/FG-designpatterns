using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class Singleton<T> where T : Singleton<T>, new()
{
    static T instance = new T();

    public Singleton()
    {
        // Delete instance if one already exists
        if (instance != null)
            Debug.Log("Only one of [" + typeof(T).ToString() + "] instance allowed");
    }

    public static T GetInstance()
    {
        if (instance == null)
        {
            T instance = new T();
        }

        return instance;
    }
}
