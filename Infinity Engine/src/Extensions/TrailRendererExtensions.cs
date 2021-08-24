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
    /// Extension methods for  <a href="https://docs.unity3d.com/ScriptReference/TrailRenderer.html"> TrailRenderer</a>  class
    /// </summary>
    public static class TrailRendererExtensions
    {
        #region Methods

        /// <summary>
        /// Resizes this TrailRenderer starts and ends witdh
        /// </summary>
        /// <param name="trail">The TrailRenderer</param>
        /// <param name="newStartWidth">new start width</param>
        /// <param name="newEndWidth">new end width</param>
        /// <param name="duration">interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOSize(this TrailRenderer trail, float newStartWidth, float newEndWidth, float duration)
        {
            return Infinity.To(new Vector2(trail.startWidth, trail.endWidth), (Vector2 value) =>
            {
                trail.startWidth = value.x;
                trail.endWidth = value.y;
            }, new Vector2(newStartWidth, newEndWidth), duration);
        }

        /// <summary>
        /// Changes the end width of this TrailRenderer.
        /// </summary>
        /// <param name="trail">The TrailRenderer</param>
        /// <param name="to">new end width</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOEndWidth(this TrailRenderer trail, float to, float duration)
        {
            return Infinity.To(trail.endWidth, value => trail.endWidth = value, to, duration);
        }

        /// <summary>
        /// Changes the start width of this TrailRenderer.
        /// </summary>
        /// <param name="trail">The TrailRenderer</param>
        /// <param name="from">new start width</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOStartWidth(this TrailRenderer trail, float from, float duration)
        {
            return Infinity.To(trail.startWidth, value => trail.startWidth = value, from, duration);
        }

        /// <summary>
        /// Changes the time of this TrailRenderer.
        /// </summary>
        /// <param name="trail">The TrailRenderer</param>
        /// <param name="to">new time</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOTime(this TrailRenderer trail, float to, float duration)
        {
            return Infinity.To(trail.time, value => trail.time = value, to, duration);
        }

        #endregion Methods
    }
}