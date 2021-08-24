/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using UnityEngine;
using InfinityEngine;
using InfinityEngine.Interpolations;

namespace InfinityEngine.Extensions
{
    /// <summary>
    ///  Extension methods for <a href="https://docs.unity3d.com/ScriptReference/RectTransform.html"> RectTransform </a> class
    /// </summary>
    public static class RectTransformExtensions
    {

        #region Methods
        /// <summary>
        /// Translates smoothly the anchoredPosition3D of this RectTransform
        /// </summary>
        /// <param name="rectTransform">This RectTransform</param>
        /// <param name="from">starts position</param>
        /// <param name="to">finals position</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMoveAnchor3D(this RectTransform rectTransform, Vector3 from, Vector3 to, float duration)
        {
            return Infinity.To(from, newPos => rectTransform.anchoredPosition3D = newPos, to, duration).SetGameObject(rectTransform.gameObject);
        }
   
        /// <summary>
        /// Translates smoothly the anchoredPosition3D of this RectTransformfrom 
        /// </summary>
        /// <param name="rectTransform">This RectTransform</param>
        /// <param name="to">finals position</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMoveAnchor3D(this RectTransform rectTransform, Vector3 to, float duration)
        {
            return rectTransform.DOMoveAnchor3D(rectTransform.anchoredPosition3D, to, duration);
        }
 
        /// <summary>
        /// Changes smoothly the x component of the anchroredPosition3D of this RectTransformon
        /// </summary>
        /// <param name="rectTransform">This RectTransform</param>
        /// <param name="from">starts position</param>
        /// <param name="to">finals position</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMoveAnchor2D(this RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            return Infinity.To(from, newPos => rectTransform.anchoredPosition = newPos, to, duration).SetGameObject(rectTransform.gameObject);
        }
  
        /// <summary>
        /// Translates smoothly the anchoredPosition2D of this RectTransform
        /// </summary>
        /// <param name="rectTransform">This RectTransform</param>
        /// <param name="to">finals position</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOMoveAnchor2D(this RectTransform rectTransform, Vector2 to, float duration)
        {
            return rectTransform.DOMoveAnchor2D(rectTransform.anchoredPosition, to, duration);
        }

        /// <summary>
        /// Changes smoothly the size of this RectTransformon
        /// </summary>
        /// <param name="rectTrasform">This RectTransform</param>
        /// <param name="from">starts size</param>
        /// <param name="to">finals size</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation"/></returns>
        public static Interpolable DOSizeDelta(this RectTransform rectTrasform, Vector2 from, Vector2 to, float duration)
        {
            return Infinity.To(from, value => rectTrasform.sizeDelta = value, to, duration).SetGameObject(rectTrasform.gameObject);
        }
        #endregion Methods
    }
}