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
    ///  Extension methods for <a href="https://docs.unity3d.com/ScriptReference/LineRenderer.html"> LineRenderer</a>  class
    /// </summary>
    public static class LineRendererExtensions
    {
        #region Methods
        /// <summary>
        /// Changes smoothly the color of this LineRenderer.
        /// </summary>
        /// <param name="lineRenderer">this LineRenderer</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this LineRenderer lineRenderer, DoubleColor from, DoubleColor to, float duration)
        {
            return Infinity.To(from, value =>
            {
                lineRenderer.startColor = value[0];
                lineRenderer.endColor = value[1];
            }, to, duration);
        }
        /// <summary>
        /// Changes smoothly the size of this LineRenderer.
        /// </summary>
        /// <param name="lineRenderer">this LineRenderer</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOSize(this LineRenderer lineRenderer, Vector2 from, Vector2 to, float duration)
        {
            return Infinity.To(from, value =>
            {
                lineRenderer.startWidth = value.x;
                lineRenderer.endWidth = value.y;
            }, to, duration);
        }

        /// <summary>
        /// Changes smoothly the position of this LineRenderer.
        /// </summary>
        /// <param name="lineRenderer">this LineRenderer</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPosition(this LineRenderer lineRenderer, Vector3 from, Vector3 to, float duration)
        {
            lineRenderer.SetPosition(0, from);
            return Infinity.To(from, value => lineRenderer.SetPosition(1, value), to, duration);
        }

        #endregion Methods
    }
}
