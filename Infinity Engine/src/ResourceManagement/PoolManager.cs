using InfinityEngine.DesignPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using InfinityEngine.Utils;

namespace InfinityEngine.ResourceManagement
{
    /// <summary>
    ///     GameObject Pool Manager.
    ///
    ///     Insteads of destroys, instantiates always the GameObjects of the scene, this class deactivate them when they are unused 
    ///     and reactivates them when you want to instantiates new GameObject.
    /// </summary>
    [AddComponentMenu("InfinityEngine/Pool Manager")]
    public class PoolManager : Singleton<PoolManager>
    {
        [SerializeField]
        private Dictionary<string, ObjectPooler> poolers;

        private ObjectPooler currentPool;

        private void Awake()
        {
            Infinity.onSceneChanged = (Action)Delegate.Remove(Infinity.onSceneChanged, new Action(ClearAllPools));
            Infinity.onSceneChanged = (Action)Delegate.Combine(Infinity.onSceneChanged, new Action(ClearAllPools));
            if (poolers == null)
            {
                poolers = new Dictionary<string, ObjectPooler>();
            }
        }

        private void CreatePool(string poolName, ObjectPooler objectPool)
        {
            if (poolers.TryGetValue(poolName, out currentPool))
            {
                currentPool.Clear();
            }
            else
            {
                poolers.Add(poolName, objectPool);
            }
        }

        private ObjectPooler GetPool(string poolName)
        {
            try
            {
                return poolers[poolName];
            }
            catch
            {
                Debugger.LogError($"There is no pool for the GameObject {poolName} ", this.gameObject);
            }
            return null;
        }

        /// <summary>
        ///  Instantiates a GameObject from the pool which has the given GameObject name as tag.
        /// </summary>
        /// <param name="prefab">The GameObject to Pop (the name of the gameObject is used as tag)</param>
        /// <param name="onInit">Action do to when this GameObject is instantiated</param>
        /// <returns>A GameObject from the PoolManager</returns>
        public static GameObject Pop(GameObject prefab, Action<GameObject> onInit)
        {
            string name = prefab.name;
            if (Instance.poolers.TryGetValue(name, out Instance.currentPool))
            {
                return Instance.currentPool.Pop(onInit);
            }
            ObjectPooler objectPooler = new ObjectPooler(name, prefab, 10);
            Instance.CreatePool(name, objectPooler);
            return objectPooler.Pop(onInit);
        }

        /// <summary>
        ///  Instantiates a GameObject from the pool which has the given GameObject name as tag.
        /// </summary>
        /// <param name="prefab">The GameObject to Pop (the name of the gameObject is used as tag)</param>
        /// <param name="position">Instantiates position</param>
        /// <param name="rotation">Instantiates rotation</param>
        /// <param name="space">
        /// if set to <c>Space.World</c>, the GameObject will be instantiated in world space.
        /// Otherwise it will be instantiated in local space.
        /// </param>
        /// <returns>A GameObject from the PoolManager</returns>
        public static GameObject Pop(GameObject prefab, Vector3 position, Quaternion rotation, Space space = 0)
        {
            return Pop(prefab, delegate (GameObject go)
            {
                if (go != null)
                {
                    if ((int)space == 0)
                    {
                        go.transform.position = position;
                        go.transform.rotation = rotation;
                    }
                    else
                    {
                        go.transform.localPosition = position;
                        go.transform.localRotation = rotation;
                    }
                }
            });
        }

        /// <summary>
        ///  Instantiates a GameObject from the pool which has the given GameObject name as tag 
        ///  at Vector3.zero and reset it rotation to Quaternion.identity
        /// </summary>
        /// <param name="prefab">The GameObject to Pop (the name of the gameObject is used as tag)</param>
        /// <returns>A GameObject from the PoolManager</returns>&gt;
        public static GameObject Pop(GameObject prefab)
        {
            return Pop(prefab, Vector3.zero, Quaternion.identity);
        }

        private static void Push(string tag, GameObject target)
        {
            if (!(target == null))
            {
                if (Instance.poolers.Any() && Instance.poolers.ContainsKey(tag))
                {
                    Instance.GetPool(tag).Push(target);
                }
                else
                {
                    Instance.CreatePool(tag, new ObjectPooler(tag, target, 10));
                    Push(tag, target);
                }
            }
        }

        /// <summary>
        /// Disables the given gameObject
        /// </summary>
        /// <param name="target">The GameObject</param>
        public static void Push(GameObject target)
        {
            if (!(target == null))
            {
                string name = target.name;
                if (name.EndsWith("]", StringComparison.Ordinal))
                {
                    name = name.Substring(0, name.LastIndexOf('_'));
                    Push(name, target);
                }
                else
                {
                    Push(target.name, target);
                }
            }
        }

        /// <summary>
        /// Resets (deactivates all gameobjects of the pool)  the pool with the given tag
        /// </summary>
        /// <param name="tag"></param>
        public static void ResetPoolWithTag(string tag)
        {
            Instance.GetPool(tag)?.Reset();
        }

        /// <summary>
        /// Resets (deactivates all gameobjects) of the PoolManager.
        /// </summary>
        public static void ResetAllPools()
        {
            foreach (KeyValuePair<string, ObjectPooler> pooler in Instance.poolers)
            {
                pooler.Value.Reset();
            }
        }

        /// <summary>
        /// Clears (destroy all gameobjects) of the PoolManager.
        /// </summary>
        public static void ClearAllPools()
        {
            foreach (KeyValuePair<string, ObjectPooler> pooler in Instance.poolers)
            {
                pooler.Value.Clear();
            }
        }
    }

    /// <summary>
    /// Pool of reusable GameObjects
    /// </summary>
    [Serializable]
    public class ObjectPooler
    {
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private Transform parent;

        private Stack<GameObject> availableGameObjects;

        private List<GameObject> allGameObjects;

        /// <summary>
        /// Creates new GameObject pool with the given <paramref name="name" />  and <paramref name="prefab" />.
        /// </summary>
        /// <param name="name">The name of the pool</param>
        /// <param name="prefab">The prefab of the pool</param>
        /// <param name="initialCapacity">Number of GameObject to instantiates</param>
        public ObjectPooler(string name, GameObject prefab, int initialCapacity)
        {
            //IL_002e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0034: Expected O, but got Unknown
            this.prefab = prefab;
            availableGameObjects = new Stack<GameObject>();
            allGameObjects = new List<GameObject>();
            GameObject val = new GameObject(name + " Pool");
            parent = val.transform;
            for (int i = 0; i < initialCapacity; i++)
            {
                GameObject item = CreateGameObject();
                allGameObjects.Add(item);
                availableGameObjects.Push(item);
            }
        }

        private GameObject CreateGameObject()
        {
            GameObject val = UnityEngine.Object.Instantiate<GameObject>(prefab);
            val.name = ($"{prefab.name}_[{allGameObjects.Count + 1}]");
            val.transform.SetParent(parent);
            val.SetActive(false);
            return val;
        }

        /// <summary>
        /// Returns a GameObject from the pool
        /// </summary>
        /// <param name="InitAction">Optional initialization action</param>
        /// <returns>A GameObject from the pool</returns>
        public GameObject Pop(Action<GameObject> InitAction = null)
        {
            GameObject val = null;
            if (!availableGameObjects.Any())
            {
                val = CreateGameObject();
                allGameObjects.Add(val);
                availableGameObjects.Push(val);
            }
            val = availableGameObjects.Pop();
            InitAction?.Invoke(val);
            SetActive(val, value: true);
            return val;
        }

        /// <summary>
        /// Deactivates the given GameObject and adds it in the pool
        /// </summary>
        /// <param name="target">GameObject to deactivate</param>
        public void Push(GameObject target)
        {
            availableGameObjects.Push(target);
            SetActive(target, value: false);
        }

        /// <summary>  Destroy all GameObjects of the pool </summary>
        public void Clear()
        {
            foreach (GameObject availableGameObject in availableGameObjects)
            {
                Object.Destroy(availableGameObject);
            }
            foreach (GameObject allGameObject in allGameObjects)
            {
                Object.Destroy(allGameObject);
            }
            availableGameObjects.Clear();
            allGameObjects.Clear();
        }

        /// <summary>   Deactivates all GameObject  of the pool </summary>
        public void Reset()
        {
            availableGameObjects.Clear();
            foreach (GameObject allGameObject in allGameObjects)
            {
                allGameObject.SetActive(false);
                availableGameObjects.Push(allGameObject);
            }
        }

        protected void SetActive(GameObject target, bool value)
        {
            if (target != null)
            {
                target.SetActive(value);
            }
        }
    }
}
