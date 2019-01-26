using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMB<T> : MonoBehaviour where T : MonoBehaviour
{
    //singleton generic instance
    private static T MyInstance;

    //multi thread lock
    private static readonly object MyLock = new object();
    public bool IsDontDestroyOnLoad;


    public static T Instance
    {
        get { return MyInstance; }
    }

    protected virtual void Awake()
    {
        lock (MyLock)
        {
            if (MyInstance == null)
            {

                MyInstance = this as T;
                if (IsDontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
            else if ((MyInstance as SingletonMB<T>) != this)
            {
                var singletons = FindObjectsOfType(typeof(T));
                if (singletons.Length > 1)
                {
                    var singletonsNames = string.Empty;
                    foreach (var singleton in singletons)
                        singletonsNames += singleton.name + ", ";

                    Debug.LogError("[" + GetType() + "] Something went really wrong, " +
                        "there is more than one Singleton: \"" + typeof(T) + "\". GameObject names: " + singletonsNames);
                }
                return;
            }
        }
    }


    protected virtual void OnDestroy()
    {
        if ((MyInstance as SingletonMB<T>) == this)
        {
            MyInstance = null;
        }
    }
}
