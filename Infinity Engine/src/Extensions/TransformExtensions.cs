/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using InfinityEngine.Interpolations;

namespace InfinityEngine.Extensions
{    

	 /// <summary>
     ///  Extension methods for <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform</a>  class
     /// </summary>
    public static class TransformExtensions
    {
        #region Methods

        #region Translation

        /// <summary>
        /// Translates smoothly this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  in world space. 
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="from">from position</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMove(this Transform transform, Vector3 from, Vector3 to, float duration)
        {
            return Infinity.To(from, newPos => transform.position = newPos, to, duration).SetGameObject(transform.gameObject);
        }
        /// <summary>
        /// Translates smoothly this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  in world space.
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMove(this Transform transform, Vector3 to, float duration)
        {
            return transform.DOMove(transform.position, to, duration);
        }
        /// <summary>
        /// Translates smoothly this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   in local space.
        /// </summary>
        /// <param name="transform">this transform</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMoveLocal(this Transform transform, Vector3 from, Vector3 to, float duration)
        {
            return Infinity.To(from, newPos => transform.localPosition = newPos, to, duration).SetGameObject(transform.gameObject);
        }
        /// <summary>
        /// Translates smoothly this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   in local space.
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMoveLocal(this Transform transform, Vector3 to, float duration)
        {
            return transform.DOMoveLocal(transform.localPosition, to, duration);
        }
        /// <summary>
        /// Shakes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   position
        /// </summary>
        /// <param name="transform">this Transform</param>
        /// <param name="strength">Shake strength</param>
        /// <param name="amount">Shake amount</param>
        /// <param name="duration">Shake duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOShakePosition(this Transform transform, Vector3 strength, float amount, float duration)
        {
            var from = transform.localPosition;

            var positionAmount = UnityEngine.Random.insideUnitSphere * amount;
            positionAmount.x *= strength.x;
            positionAmount.y *= strength.y;
            positionAmount.z *= strength.z;

            return Infinity.To(transform.localPosition, value => transform.localPosition = value, from, duration)
                .OnUpdate((arg) =>
                {
                    positionAmount = UnityEngine.Random.insideUnitSphere * amount;
                    positionAmount.x *= strength.x;
                    positionAmount.y *= strength.y;
                    positionAmount.z *= strength.z;

                    transform.localPosition += positionAmount;
                })
                .OnComplete((arg) => transform.localPosition = from);

        }

        #endregion Translation

        #region Rotation

        /// <summary>
        /// Rotates smoothly this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  in world space.
        /// </summary>
        /// <param name="transform">The transform</param>
        /// <param name="destination">finals rotation</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORotate(this Transform transform, Quaternion destination, float duration)
        {
            return Infinity.To(transform.rotation, newPos => transform.rotation = newPos, destination, duration).SetGameObject(transform.gameObject);
        }
        /// <summary>
        /// Rotates smoothly this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   in world space.
        /// </summary>
        /// <param name="transform">The transform</param>
        /// <param name="starts">from rotation</param>
        /// <param name="destination">finals rotation</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORotate(this Transform transform, Quaternion starts, Quaternion destination, float duration)
        {
            return Infinity.To(starts, newRotation => transform.rotation = newRotation, destination, duration).SetGameObject(transform.gameObject);
        }
        /// <summary>
        /// Rotates smoothly this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   in local space.
        /// </summary>
        /// <param name="transform">The transform</param>
        /// <param name="destination">finals rotation</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORotateLocal(this Transform transform, Quaternion destination, float duration)
        {
            return Infinity.To(transform.localRotation, newRotation => transform.localRotation = newRotation, destination, duration).SetGameObject(transform.gameObject);
        }
        /// <summary>
        /// Rotates smoothly this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   in local space.
        /// </summary>
        /// <param name="transform">The transform</param>
        /// <param name="starts">from rotation</param>
        /// <param name="destination">finals rotation</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORotateLocal(this Transform transform, Quaternion starts, Quaternion destination, float duration)
        {
            return Infinity.To(starts, newRotation => transform.localRotation = newRotation, destination, duration).SetGameObject(transform.gameObject);
        }
        /// <summary>
        /// Shakes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  rotation
        /// </summary>
        /// <param name="transform">this Transform</param>
        /// <param name="strength">Shake strength</param>
        /// <param name="amount">Shake amount</param>
        /// <param name="duration">Shake duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOShakeRotation(this Transform transform, Vector3 strength, float amount, float duration)
        {
            var from = transform.localRotation;

            var rotationAmount = UnityEngine.Random.insideUnitSphere * amount;
            rotationAmount.x *= strength.x;
            rotationAmount.y *= strength.y;
            rotationAmount.z *= strength.z;

            return Infinity.To(transform.localRotation, value => transform.localRotation = value, from, duration)
                .OnUpdate((arg) =>
                {
                    rotationAmount = UnityEngine.Random.insideUnitSphere * amount;
                    rotationAmount.x *= strength.x;
                    rotationAmount.y *= strength.y;
                    rotationAmount.z *= strength.z;

                    transform.localRotation = Quaternion.Euler(rotationAmount);
                })
                .OnComplete((arg) => transform.localRotation = from);

        }

        #endregion Rotation

        #region Scale

        /// <summary>
        /// Changes smoothly the scales of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  
        /// </summary>
        /// <param name="transform">The transform </param>
        /// <param name="starts">from scale</param>
        /// <param name="destination">finals scale</param>
        /// <param name="duration">Transition duration</param>
        /// <returns>Object of type Transition</returns>
        public static Interpolable DOScale(this Transform transform, Vector3 starts, Vector3 destination, float duration)
        {
            return Infinity.To(starts, value => transform.localScale = value, destination, duration)
                .SetGameObject(transform.gameObject);
        }
        /// <summary>
        /// Changes smoothly the scales of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>
        /// </summary>
        /// <param name="transform">This Transform </param>
        /// <param name="destination">finals scale</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOScale(this Transform transform, Vector3 destination, float duration)
        {
            return transform.DOScale(transform.localScale, destination, duration).SetGameObject(transform.gameObject);
        }
        /// <summary>
        /// Shakes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   scale
        /// </summary>
        /// <param name="transform">this Transform</param>
        /// <param name="strength">Shake strength</param>
        /// <param name="amount">Shake amount</param>
        /// <param name="duration">Shake duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOShakeScale(this Transform transform, Vector3 strength, float amount, float duration)
        {
            var from = transform.localScale;

            var scaleAmount = UnityEngine.Random.insideUnitSphere * amount;
            scaleAmount.x *= strength.x;
            scaleAmount.y *= strength.y;
            scaleAmount.z *= strength.z;

            return Infinity.To(transform.localScale, value => transform.localScale = value, from, duration)
                .OnUpdate((arg) =>
                {
                    scaleAmount = UnityEngine.Random.insideUnitSphere * amount;
                    scaleAmount.x *= strength.x;
                    scaleAmount.y *= strength.y;
                    scaleAmount.z *= strength.z;

                    transform.localScale = scaleAmount;
                })
                .OnComplete((arg) => transform.localScale = from);

        }

        #endregion Scale

        #region Others


        /// <summary>
        /// Changes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  x position value
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="x">new x</param>
        /// <param name="space">Space to use (Space.World by default)</param>
        public static void SetX(this Transform transform, float x, Space space = Space.World)
        {
            if (space == Space.World)
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            else
                transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }

        /// <summary>
        /// Changes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   y position value
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="y">new y</param>
        /// <param name="space">Space to use (Space.World by default)</param>
        public static void SetY(this Transform transform, float y, Space space = Space.World)
        {
            if (space == Space.World)
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            else
                transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }

        /// <summary>
        /// Changes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  z position value
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="z">new z</param>
        /// <param name="space">Space to use (Space.World by default) </param>
        public static void SetZ(this Transform transform, float z, Space space = Space.World)
        {
            if (space == Space.World)
                transform.position = new Vector3(transform.position.x, transform.position.y, z);
            else
                transform.localPosition = new Vector3(transform.position.x, transform.position.y, z);
        }

        /// <summary>
        /// Changes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   x rotation value
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="x">new x</param>
        /// <param name="space">Space to use (Space.World by default)</param>
        public static void SetRotationX(this Transform transform, float x, Space space = Space.World)
        {
            if (space == Space.World)
            {
                var v = transform.rotation.eulerAngles;
                v.x = x;
                transform.rotation = Quaternion.Euler(v);
            }
            else
            {
                var v = transform.rotation.eulerAngles;
                v.x = x;
                transform.localRotation = Quaternion.Euler(v);
            }
        }

        /// <summary>
        /// Changes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   y rotation value
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="y">new y</param>
        /// <param name="space">Space to use (Space.World by default)</param>
        public static void SetRotationY(this Transform transform, float y, Space space = Space.World)
        {
            if (space == Space.World)
            {
                var v = transform.rotation.eulerAngles;
                v.y = y;
                transform.rotation = Quaternion.Euler(v);
            }
            else
            {
                var v = transform.rotation.eulerAngles;
                v.y = y;
                transform.localRotation = Quaternion.Euler(v);
            }
        }

        /// <summary>
        /// Changes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  z rotation value
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="z">new z</param>
        /// <param name="space">Space to use (Space.World by default)</param>
        public static void SetRotationZ(this Transform transform, float z, Space space = Space.World)
        {
            if (space == Space.World)
            {
                Vector3 v = transform.rotation.eulerAngles;
                v.z = z;
                transform.rotation = Quaternion.Euler(v);
            }
            else
            {
                Vector3 v = transform.rotation.eulerAngles;
                v.z = z;
                transform.localRotation = Quaternion.Euler(v);
            }
        }

        /// <summary>
        /// <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  x scale value
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="x">new x</param>
        public static void SetScaleX(this Transform transform, float x)
        {
            Vector3 v = transform.localScale;
            v.x = x;
            transform.localScale = v;

        }

        /// <summary>
        /// Changes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   y scale value
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="y">new y</param>
        /// <param name="space">Space to use (Space.World by default)</param>
        public static void SetScaleY(this Transform transform, float y, Space space = Space.World)
        {
            Vector3 v = transform.localScale;
            v.y = y;
            transform.localScale = v;
        }

        /// <summary>
        /// Changes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   z scale value
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <param name="z">new z</param>
        /// <param name="space">Space to use (Space.World by default)</param>
        public static void SetScaleZ(this Transform transform, float z, Space space = Space.World)
        {
            Vector3 v = transform.localScale;
            v.z = z;
            transform.localScale = v;
        }


        /// <summary>
        /// return the last child of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  
        /// </summary>
        /// <param name="transform">This Transform</param>
        /// <returns>last child of this transform</returns>
        public static Transform LastChild(this Transform transform)
        {
            int count = transform.childCount;
            if (count > 0)
                return transform.GetChild(count - 1);
            else
                return null;
        }

        /// <summary>
        /// Return an integer that represents the number of childs of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   that meets the given predicate
        /// </summary>
        /// <param name="transform">this Transform</param>
        /// <param name="predicate">the predicate</param>
        /// <returns></returns>
        public static int CountChild(this Transform transform, Func<Transform, bool> predicate)
        {
            return transform.GetEnumerator().ToEnumerable().Cast<Transform>().Count(predicate);
        }

        /// <summary>
        /// Return an list of all parents Transform of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static List<Transform> GetParents(this Transform transform)
        {
            if (transform.parent == null)
                return null;

            var result = new List<Transform>();
            var current = transform.parent;
            while(current != null)
            {
                result.Add(current);
                current = current.parent;
            }
            return result;
        }

        /// <summary>
        /// Apply the same <paramref name="action"/> for each child elements in this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>
        /// </summary>
        /// <example>
        /// The following code is an example of use of this method :
        /// <code>
        ///     transform.ForChild(child => child.gameObject.SetActive(false));
        /// </code>
        /// This code deactivates all child of this transform.
        /// </example>
        /// <remarks>
        /// This function has an optional callback action parameter, you can use it like in the following example :
        /// <example>
        /// <code>
        ///     transform.ForChild(child => child.gameObject.SetActive(false), () => { Debug.Log("This is an example with the callback parameter"); });
        /// </code>
        /// </example>
        /// </remarks>
        /// <param name="transform">This Transform</param>
        /// <param name="action">action to apply</param>
        /// <param name="callback">callback action at the end</param>
        public static void ForChild(this Transform transform, Action<Transform> action, Action callback = null)
        {
            foreach (Transform child in transform)
                action(child);

            if (callback != null)
                callback.Invoke();
        }

        /// <summary>
        /// Check if the given predicate if <c>true</c> for all parents of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>
        /// </summary>
        /// <example>
        /// The following code is an example of use of this method :
        /// <code>
        ///     Debug.Log(transform.TrueForAllParent(parent => parent.gameObject.activeSelf));
        /// </code>
        /// This code log <c>true</c> on Unity console if all parent of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   are active.
        /// </example>
        /// <remarks>
        /// This function return <c>true</c> if this <paramref name="transform"/> has no parent.
        /// </remarks>
        /// <param name="transform">the Transform</param>
        /// <param name="predicate">the predicate</param>
        /// <returns> <c>true</c> if this <paramref name="transform"/> has no parent or if the predicate is <c>true</c> for all parent of this transform, <c>false</c> otherwise. </returns>
        public static bool TrueForAllParent(this Transform transform, Func<Transform, bool> predicate)
        {
            return !TrueForAnyParent(transform, predicate.Not());
        }

        /// <summary>
        /// Check if the given predicate if <c>true</c> for all child of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>  .
        /// </summary>
        /// <example>
        /// The following code is an example of use of this method :
        /// <code>
        ///     Debug.Log(transform.TrueForAllChild(child => child.gameObject.activeSelf, true));
        /// </code>
        /// This code log <c>true</c> on Unity console if all child of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   are active.
        /// </example>
        /// <remarks>
        /// This function return <c>true</c> if this <paramref name="transform"/> has no child.
        /// </remarks>
        /// <param name="transform">the Transform</param>
        /// <param name="predicate">the predicate</param>
        /// <param name="includeSubChild">Check the predicate for all sub child</param>
        /// <returns> <c>true</c> if this <paramref name="transform"/> has no child or if the predicate is <c>true</c> for all child of this transform, <c>false</c> otherwise. </returns>
        public static bool TrueForAllChild(this Transform transform, Func<Transform, bool> predicate, bool includeSubChild = false)
        {
                return !TrueForAnyChild(transform, predicate.Not(), includeSubChild);
        }

        /// <summary>
        /// Check if the given predicate if <c>true</c> for any parent of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>
        /// </summary>
        /// <example>
        /// The following code is an example of use of this method :
        /// <code>
        ///     Debug.Log(transform.TrueForAnyParent(parent => parent.gameObject.activeSelf));
        /// </code>
        /// This code log <c>true</c> on Unity console if any parent of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   is active.
        /// </example>
        /// <remarks>
        /// This function return <c>false</c> if this <paramref name="transform"/> has no parent.
        /// </remarks>
        /// <param name="transform">the Transform</param>
        /// <param name="predicate">the predicate</param>
        /// <returns><c>true</c> only (it return false if this Transform has no parent) if the predicate is <c>true</c> for any parent of this Transform <c>false</c> otherwise. </returns>
        public static bool TrueForAnyParent(this Transform transform, Func<Transform, bool> predicate)
        {
            var parent = transform.parent;
            while (parent != null)
            {
                if (predicate(parent))
                    return true;

                parent = parent.parent;
            }
            return false;
        }

        /// <summary>
        /// Check if the given predicate if <c>true</c> for any child of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a> .
        /// </summary>
        /// <example>
        /// The following code is an example of use of this method :
        /// <code>
        ///     Debug.Log(transform.TrueForAnyChild(parent => parent.gameObject.activeSelf));
        /// </code>
        /// This code log <c>true</c> on Unity console if any child of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a>   is active.
        /// </example>
        /// <remarks>
        /// This function return <c>false</c> if this <paramref name="transform"/> has no child.
        /// </remarks>
        /// <param name="transform">the Transform</param>
        /// <param name="predicate">the predicate</param>
        /// <param name="includeSubChild">Check the predicate for all sub child</param>
        /// <returns><c>true</c> only (it return false if this Transform has no child) if the predicate is <c>true</c> for any child of this Transform <c>false</c> otherwise. </returns>
        public static bool TrueForAnyChild(this Transform transform, Func<Transform, bool> predicate, bool includeSubChild = false)
        {    
            for (int i = 0; i < transform.childCount; i++)
            {
                if (predicate(transform.GetChild(i)))
                    return true;

                if (includeSubChild)
                {
                    bool isRecursive = TrueForAnyChild(transform.GetChild(i), predicate, true);
                    if (isRecursive)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a> 
        /// <a href="https://docs.unity3d.com/ScriptReference/GameObject.html"> GameObject</a> active state
        /// </summary>
        /// <param name="transform">this Transform</param>
        /// <returns>this transform GameObject active state</returns>
        public static bool GameObjectIsActive(this Transform transform)
        {
            if (transform == null)
                return false;

            return transform.gameObject.activeSelf;
        }

        /// <summary>
        /// Changes this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a> 
        /// <a href="https://docs.unity3d.com/ScriptReference/GameObject.html"> GameObject</a> active state
        /// </summary>
        /// <param name="transform">this Transform</param>
        /// <param name="active">new active state</param>
        public static void SetGameObjectActive(this Transform transform, bool active)
        {
            if(transform != null)
                transform.gameObject.SetActive(active);
        }

        /// <summary>
        /// Return all Transform childs of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a> 
        /// </summary>
        /// <param name="transform">this Transform</param>
        /// <returns>all childs transform of this Transform</returns>
        public static List<Transform> GetChilds(this Transform transform)
        {
            return transform.GetEnumerator().ToEnumerable().Cast<Transform>().ToList();
        }

        /// <summary>
        /// Return all of this <a href="https://docs.unity3d.com/ScriptReference/Component.html"> Components</a>
        /// of this <a href="https://docs.unity3d.com/ScriptReference/Transform.html"> Transform </a> 
        /// </summary>
        /// <param name="transform">this Transform</param>
        /// <returns>all components of this Transform</returns>
        public static Component[] GetAllComponents(this Transform transform)
        {
            return transform.GetComponents<Component>();
        }

        #endregion Others

        #endregion Methods
    }
}