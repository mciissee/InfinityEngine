/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/


using UnityEngine;
using InfinityEngine.Interpolations;

namespace InfinityEngine.Extensions
{
    /// <summary>
    ///  Extension methods for <a href="https://docs.unity3d.com/ScriptReference/Rigidbody.html"> RigidBody </a> 
    ///  and <a href="https://docs.unity3d.com/ScriptReference/Rigidbody2D.html"> RigidBody2D </a> class
    /// </summary>
    public static class RigidBodyExtensions
    {
        #region Methods

        /// <summary>
        /// Moves smoothly this RigidBody
        /// </summary>
        /// <param name="body">this RigidBody</param>
        /// <param name="start">starts value</param>
        /// <param name="destination">finals value</param>
        /// <param name="duration">Interpolation</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMove(this Rigidbody body, Vector3 start, Vector3 destination, float duration)
        {
            return Infinity.To(start, value => body.MovePosition(value), destination, duration).SetGameObject(body.gameObject);
        }

        /// <summary>
        /// Moves smoothly this RigidBody
        /// </summary>
        /// <param name="body">this RigidBody</param>
        /// <param name="destination">finals value</param>
        /// <param name="duration">Interpolation</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMove(this Rigidbody body, Vector3 destination, float duration)
        {
            return body.DOMove(body.position, destination, duration).SetGameObject(body.gameObject);
        }

        /// <summary>
        /// Rotates smoothly this RigidBody
        /// </summary>
        /// <param name="body">this RigidBody</param>
        /// <param name="start">starts value</param>
        /// <param name="destination">finals value</param>
        /// <param name="duration">Interpolation</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORotate(this Rigidbody body, Quaternion start, Quaternion destination, float duration)
        {
            return Infinity.To(start, value => body.MoveRotation(value), destination, duration)
                          .SetGameObject(body.gameObject);
        }

        /// <summary>
        /// Rotates smoothly this RigidBody
        /// </summary>
        /// <param name="body">this RigidBody</param>
        /// <param name="destination">finals value</param>
        /// <param name="duration">Interpolation</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORotate(this Rigidbody body, Quaternion destination, float duration)
        {
            return body.DORotate(body.rotation, destination, duration).SetGameObject(body.gameObject);
        }

        /// <summary>
        /// Moves smoothly this RigidBody2D
        /// </summary>
        /// <param name="body">this RigidBody2D</param>
        /// <param name="start">starts value</param>
        /// <param name="destination">finals value</param>
        /// <param name="duration">Interpolation</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMove2D(this Rigidbody2D body, Vector2 start, Vector2 destination, float duration)
        {
            return Infinity.To(start, value => body.MovePosition(value), destination, duration).SetGameObject(body.gameObject);
        }

        /// <summary>
        /// Moves smoothly this RigidBody2D
        /// </summary>
        /// <param name="body">this RigidBody2D</param>
        /// <param name="destination">finals value</param>
        /// <param name="duration">Interpolation</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMove2D(this Rigidbody2D body, Vector2 destination, float duration)
        {
            return body.DOMove2D(body.position, destination, duration).SetGameObject(body.gameObject);
        }
  
        /// <summary>
        /// Rotates smoothly this RigidBody2D
        /// </summary>
        /// <param name="body">this RigidBody2D</param>
        /// <param name="start">starts value</param>
        /// <param name="destination">finals value</param>
        /// <param name="duration">Interpolation</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORotate2D(this Rigidbody2D body, float start, float destination, float duration)
        {
            return Infinity.To(start, value => body.MoveRotation(value), destination, duration) .SetGameObject(body.gameObject);
        }

        /// <summary>
        /// Rotates smoothly this RigidBody2D
        /// </summary>
        /// <param name="body">this RigidBody2D</param>
        /// <param name="destination">finals value</param>
        /// <param name="duration">Interpolation</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORotate2D(this Rigidbody2D body, float destination, float duration)
        {
            return body.DORotate2D(body.rotation, destination, duration).SetGameObject(body.gameObject);
        }

        #endregion Methods
    }
}