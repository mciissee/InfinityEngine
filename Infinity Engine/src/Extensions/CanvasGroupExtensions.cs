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
    /// Extension methods for <a href="https://docs.unity3d.com/ScriptReference/CanvasGroup.html"> CanvasGroup </a> class
    /// </summary>
    public static class CanvasGroupExtensions
    {
        #region Methods

        /// <summary>
        /// Fades this CanvasGroup alpha
        /// </summary>
        /// <param name="canvas">This CanvasGroup</param>
        /// <param name="from">starts alpha</param>
        /// <param name="to">finals alpha</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this CanvasGroup canvas, float from, float to, float duration)
        {
            canvas.alpha = from;
            return Infinity.To(from, newValue => canvas.alpha = newValue, to, duration).SetGameObject(canvas.gameObject);
        }
        /// <summary>
        /// Fades this CanvasGroup alpha from 0 to 1
        /// </summary>
        /// <param name="canvas">This CanvasGroup</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFadeIn(this CanvasGroup canvas, float duration = 1.0f)
        {
            canvas.gameObject.SetActive(true);
            return canvas.DOFade(0.0f, 1.0f, duration);
        }

        /// <summary>
        /// Fades this CanvasGroup alpha from 1 to 0
        /// </summary>
        /// <param name="canvas">This CanvasGroup</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFadeOut(this CanvasGroup canvas, float duration = 1.0f)
        {
            return canvas.DOFade(1.0f, 0.0f, duration).OnComplete((arg) =>
            {
                canvas.gameObject.SetActive(false);
            });
        }

        #endregion Methods
    }
}
