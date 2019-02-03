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
            //multi thread lock
            lock (locker)
            {
                // if null we set the instance to be this and mark the
                // gameobject whether or not is destroyed on load
                if (instance == null)
                {
                    instance = this as T;
                    if (isDontDestroyOnLoad)
                        DontDestroyOnLoad(gameObject);
                }
                else if ((instance as SingletonMB<T>) != this)
                {
                    //if not null we grab all possible objects of this type
                    var singletons = FindObjectsOfType(typeof(T));

                    if (singletons.Length > 1)
                    {
                        if (isSilent)
                        {
                            foreach (var duplicated in singletons)
                            {
                                //if the singleton is silent, just destroy the sparing objects
                                if (duplicated != instance)
                                    Destroy(duplicated);
                            }
                        }
                        else
                        {
                            //if not silent, we raise an error with the names of the sparing objects
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