using InfinityEngine.ResourceManagement;
using System;
using UnityEngine;

namespace InfinityEngine.Extensions
{
    /// <summary>   
    /// Extension methods for PoolManager class 
    /// </summary>
    public static class PoolManagerExtension
    {
        /// <summary>
        /// Disables this gameObject and push it to the PoolManager.
        /// </summary>
        /// <param name="go">The GameObject</param>
        public static void ToPool(this GameObject go)
        {
            if (go == null)
            {
                throw new NullReferenceException("null GameObject cannot be pushed to the pool manager");
            }
            PoolManager.Push(go);
        }

        /// <summary>
        /// Disables the gameObject of this Transform and push it to the PoolManager.
        /// </summary>
        /// <param name="transform">The GameObject</param>
        public static void ToPool(this Transform transform)
        {
            if (transform == null)
            {
                throw new NullReferenceException("null Transform cannot be pushed to the pool manager");
            }
            transform.gameObject.ToPool();
        }

        /// <summary>
        /// Disables the gameObject of this MonoBehaviour and push it to the PoolManager.
        /// </summary>
        /// <param name="mono">The GameObject</param>
        public static void ToPool(this MonoBehaviour mono)
        {
            if (mono == null)
            {
                throw new NullReferenceException("null Component cannot be pushed to the pool manager");
            }
            mono.gameObject.ToPool();
        }

        /// <summary>
        ///  Instantiates a GameObject from the pool which has the given GameObject name as tag.
        /// </summary>
        /// <param name="prefab">The GameObject to Pop (the name of the gameObject is used as tag)</param>
        /// <param name="onInit">Action do to when this GameObject is instantiated</param>
        /// <returns>A GameObject from the PoolManager</returns>
        public static GameObject FromPool(this GameObject prefab, Action<GameObject> onInit)
        {
            return PoolManager.Pop(prefab, onInit);
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
        public static GameObject FromPool(this GameObject prefab, Vector3 position, Quaternion rotation, Space space = 0)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0002: Unknown result type (might be due to invalid IL or missing references)
            //IL_0003: Unknown result type (might be due to invalid IL or missing references)
            return PoolManager.Pop(prefab, position, rotation, space);
        }

        /// <summary>
        ///  Instantiates a GameObject from the pool which has the given GameObject name as tag 
        ///  at Vector3.zero and reset it rotation to Quaternion.identity
        /// </summary>
        /// <param name="prefab">The GameObject to Pop (the name of the gameObject is used as tag)</param>
        /// <returns>A GameObject from the PoolManager</returns>&gt;
        public static GameObject FromPool(this GameObject prefab)
        {
            return PoolManager.Pop(prefab);
        }

        /// <summary>
        ///  Instantiates a GameObject from the pool which has the given GameObject name as tag 
        ///  at Vector3.zero and reset it rotation to Quaternion.identity and returns the component of type 'T' from the GameObject
        /// </summary>
        /// <typeparam name="T">The type of the component to find</typeparam>
        /// <param name="prefab">The GameObject to Pop (the name of the gameObject is used as tag)</param>
        /// <returns>The component of type T of prefab</returns>&gt;
        public static T FromPool<T>(this GameObject prefab) where T : Component
        {
            return PoolManager.Pop(prefab).GetComponent<T>();
        }
    }
}
