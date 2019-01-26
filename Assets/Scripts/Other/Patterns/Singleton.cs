using UnityEngine;

namespace ywr.Tools
{
    public class Singleton<T> where T : class, new()
    {
        private static T MyInstance;
        private static readonly object MyLock = new object();

        protected Singleton()
        {

        }

        public static T Instance
        {
            get
            {
                lock (MyLock)
                {
                    if (MyInstance == null)
                        MyInstance = new T();
                    //Debug.LogError("[Singleton] Something went really wrong there is more than one Singleton: " + typeof(T));
                }

                return MyInstance;
            }
        }
    }
}