/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

#pragma warning disable RECS0108 // Warns about static fields in generic types

using UnityEngine;
using InfinityEngine.Utils;


//// <summary>
//// This namespace provides access to the implementation of some design patterns.
//// </summary>
namespace InfinityEngine.DesignPatterns
{
    /// <summary>
    ///   Singleton Manager
    /// </summary>
    /// <typeparam name="T">Singleton type(must inherit from MonoBehaviour)</typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static object _lock = new object();

        /// <summary>
        /// Unity callback invoked when this gameObject is enable
        /// </summary>
        protected virtual void OnEnable()
        {
            _instance = Instance;
            _instance.gameObject.name += " (Singleton)";
        }

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    var type = typeof(T);
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType(type) as T;

                        if (FindObjectsOfType(type).Length > 1)
                        {
                            Debugger.LogError("[Singleton]- There should never be more than 1 singleton! ");
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            var singleton = new GameObject(type.ToString());
                            _instance = singleton.AddComponent<T>();
                            if (Application.isPlaying)
                                DontDestroyOnLoad(_instance.transform.root.gameObject);
                            Debugger.Log(string.Format("[Singleton] An instance of {0} was created !", typeof(T)));
                        }
                        else
                        {
                            Debugger.Log(string.Format("[Singleton] Using instance already created: {0}", _instance.GetType()));
                            if (Application.isPlaying)
                                DontDestroyOnLoad(_instance.transform.root.gameObject);
                        }
                    }
                    else
                    {
                        var array = FindObjectsOfType(type) as T[];
                        if (array.Length > 1)
                        {
                            Destroy(array[1].gameObject);
                            return _instance;
                        }
                    }

                    return _instance;
                }
            }
        }

        /// <summary>
        /// Checks if there is an instance of this component in the scene
        /// </summary>
        public static bool IsMissingInScene => FindObjectOfType<T>() == null;

        /// <summary>
        /// Destroy this singleton instance.
        /// </summary>
        public static void DestroySingleton() => Destroy(_instance.gameObject);

    }
}