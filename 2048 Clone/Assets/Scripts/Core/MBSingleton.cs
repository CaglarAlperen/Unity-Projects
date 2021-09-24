using System;
using UnityEngine;

public abstract class MBSingleton<T> : MonoBehaviour where T : MBSingleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}
