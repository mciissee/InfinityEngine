/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using InfinityEngine.Interpolations;
using System.Collections.Generic;

namespace InfinityEngine.Extensions
{
    /// <summary>
    ///  Extension methods for <a href="https://docs.unity3d.com/ScriptReference/UI.Text.html"> UI.Text </a>  class
    /// </summary>
    public static class TextExtensions
    {
        #region Methods

        /// <summary>
        /// Changes this Image alpha
        /// </summary>
        /// <param name="text">this Text</param>
        /// <param name="alpha">New alpha</param>
        public static void SetAlpha(this Text text, float alpha)
        {
            var tmp = text.color;
            tmp.a = alpha;
            text.color = tmp;
        }

        /// <summary>
        /// Fades this Text alpha
        /// </summary>
        /// <param name="text">this Text</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this Text text, float from, float to, float duration)
        {
            return Infinity.To(from, (newValue) => text.SetAlpha(newValue), to, duration).SetGameObject(text.gameObject);
        }
        /// <summary>
        /// Fades this Text alpha
        /// </summary>
        /// <param name="text">this Text</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this Text text, float to, float duration)
        {
            return text.DOFade(text.color.a, to, duration);
        }

        /// <summary>
        /// Lerps this Text Color
        /// </summary>
        /// <param name="text">this Text</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this Text text, Color from, Color to, float duration)
        {
            return Infinity.To(from, color => text.color = color, to, duration).SetGameObject(text.gameObject);
        }

        /// <summary>
        /// Lerps this Text Color
        /// </summary>
        /// <param name="text">this Text</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this Text text, Color to, float duration)
        {
            return text.DOColor(text.color, to, duration);
        }

        /// <summary>
        /// Picks a color in the given list and changes the color of this text each '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="text">this Text</param>
        /// <param name="colors">Color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this Text text, List<Color> colors, float interval, float duration)
        {
            Interpolable interpolation = null;
            interpolation = text.DOColor(colors.Random(), duration).OnComplete((arg) =>
            {
                colors.Shuffle();
                interpolation.SetNewStart(colors.Random());
                interpolation.Reverse();
            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(text.gameObject);
            return interpolation;
        }

        /// <summary>
        /// Picks a color in the given list and changes the color of this text each '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="text">this Text</param>
        /// <param name="colors">Color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this Text text, Color[] colors, float interval, float duration)
        {
            Interpolable interpolation = null;
            interpolation = text.DOColor(colors.Random(), duration).OnComplete((arg) =>
            {
                colors.Shuffle();
                interpolation.SetNewStart(colors.Random());
                interpolation.Reverse();
            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(text.gameObject);
            return interpolation;
        }


        /// <summary>
        /// Changes the color of the camera each time <paramref name="interval"/> seconds.
        /// </summary>
        /// <param name="text">this Text</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORandomColor(this Text text, float interval, float duration)
        {
            Interpolable interpolation = null;
            interpolation = text.DOColor(Infinity.RandomColor(), duration).OnComplete((arg) =>
            {
                interpolation.SetNewStart(Infinity.RandomColor());
                interpolation.Reverse();
            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(text.gameObject);
            return interpolation;
        }
        /// <summary>
        /// Do score effect
        /// </summary>
        /// <param name="text">the Text</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">interpolation duration</param>
        /// <example>
        ///     The code :
        ///     <code>DoScore(0, 1000, 1);</code>
        ///     Display on screen 0 to 1000 in 1 seconds
        /// </example>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOScore(this Text text, int from, int to, float duration)
        {
            return Infinity.To(from, value => text.text = value.ToString(), to, duration).SetGameObject(text.gameObject);
        }

        /// <summary>
        /// Changes smoothly the scale of  this Text fromm Vector3.zero to the given value
        /// </summary>
        /// <param name="text">this Text</param>
        /// <param name="to">finals value</param>
        /// <param name="message">Text message</param>
        /// <param name="color">Text color</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOZoomOut(this Text text, Vector3 to, string message, Color color, float duration)
        {
            text.text = message;
            text.color = color;
            text.gameObject.SetActive(true);
            return Infinity.To(Vector3.zero, value => text.transform.localScale = value, to, duration).SetGameObject(text.gameObject)
                .OnComplete((arg) => { text.gameObject.SetActive(false); });
        }

        #endregion Methods
    }
}
