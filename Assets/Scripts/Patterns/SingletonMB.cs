using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ywr.Tools
{
    public class SingletonMB<T> : MonoBehaviour where T : MonoBehaviour
    {
        //singleton generic instance
        private static T instance;

        //multi thread lock
        private static readonly object locker = new object();

        [Tooltip("Is this singleton destroyed when the scene changes")] [SerializeField]
        private bool isDontDestroyOnLoad;

        [Tooltip("It raises an exception when finds another singleton in the scene")] [SerializeField]
        private bool isSilent;

        public static T Instance
        {
            get { return instance; }
        }

        protected virtual void Awake()
        {
            lock (locker)
            {
                if (instance == null)
                {
                    instance = this as T;
                    if (isDontDestroyOnLoad)
                        DontDestroyOnLoad(gameObject);
                }
                else if ((instance as SingletonMB<T>) != this)
                {
                    var singletons = FindObjectsOfType(typeof(T));
                    if (singletons.Length > 1)
                    {
                        if (isSilent)
                        {
                            foreach (var duplicated in singletons)
                            {
                                //if not this instance destroy it silently
                                if (duplicated != instance)
                                    Destroy(duplicated);
                            }
                        }
                        else
                        {
                            var singletonsNames = string.Empty;
                            foreach (var duplicated in singletons)
                                singletonsNames += duplicated.name + ", ";

                            //log error for all objects that have this monobehavior
                            Debug.LogError("[" + GetType() + "] Something went really wrong, " +
                                           "there is more than one Singleton: \"" + typeof(T) +
                                           "\". GameObject names: " +
                                           singletonsNames);
                        }
                    }

                    return;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if ((instance as SingletonMB<T>) == this)
            {
                instance = null;
            }
        }
    }
}