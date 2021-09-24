using System;
using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    private static readonly Lazy<T> sInstance = 
        new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);

    public static T Instance => sInstance.Value;
}
