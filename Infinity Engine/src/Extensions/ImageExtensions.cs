/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using InfinityEngine.Interpolations;


namespace InfinityEngine.Extensions
{

    /// <summary>
    /// Extension methods for <a href="https://docs.unity3d.com/ScriptReference/UI.Image.html"> UI.Image </a> class
    /// </summary>
    public static class ImageExtensions
    {
        #region Methods

        /// <summary>
        /// Changes this Image alpha
        /// </summary>
        /// <param name="image">this Image</param>
        /// <param name="alpha">New alpha</param>
        public static void SetAlpha(this Image image, float alpha)
        {
            var tmp = image.color;
            tmp.a = alpha;
            image.color = tmp;
        }

        /// <summary>
        /// Fades this Image alpha
        /// </summary>
        /// <param name="image">this Image</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this Image image, float from, float to, float duration)
        {
            return Infinity.To(from, (newValue) => image.SetAlpha(newValue), to, duration).SetGameObject(image.gameObject);
        }
        /// <summary>
        /// Fades this Image alpha
        /// </summary>
        /// <param name="image">this Image</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this Image image, float to, float duration)
        {
            return image.DOFade(image.color.a, to, duration).SetGameObject(image.gameObject);
        }

        /// <summary>
        /// Lerps this Image color
        /// </summary>
        /// <param name="image">this Image</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this Image image, Color from, Color to, float duration)
        {
            return Infinity.To(from, color => image.color = color, to, duration).SetGameObject(image.gameObject);
        }
        /// <summary>
        /// Lerps this Image color
        /// </summary>
        /// <param name="image">this Image</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this Image image, Color to, float duration)
        {
            return image.DOColor(image.color, to, duration).SetGameObject(image.gameObject);
        }


        /// <summary>
        /// Changes the color of this image each time '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="image">this Image</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORandomColor(this Image image, float interval, float duration)
        {
           return image.DOColor(Infinity.RandomColor(), duration).OnComplete((arg) =>
            {
                arg.SetNewStart(Infinity.RandomColor());
                arg.Reverse();

            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(image.gameObject);

        }

        /// <summary>
        /// Picks a color in the given list and changes the color of this image each '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="image">this Image</param>
        /// <param name="colors">The color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this Image image, List<Color> colors, float interval, float duration)
        {
            return image.DOColor(colors.Random(), duration).OnComplete((arg) =>
            {
                colors.Shuffle();
                arg.SetNewStart(colors.Random());
                arg.Reverse();

            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(image.gameObject);
        }
        /// <summary>
        /// Picks a color in the given list and changes the color of this image each '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="image">this Image</param>
        /// <param name="colors">The color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this Image image, Color[] colors, float interval, float duration)
        {
            Interpolable interpolation = null;
            interpolation = image.DOColor(colors.Random(), duration).OnComplete((arg) =>
            {
                colors.Shuffle();
                interpolation.SetNewStart(colors.Random());
                interpolation.Reverse();

            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(image.gameObject);

            return interpolation;
        }

        /// <summary>
        /// Changes smoothly the fill amount of this Image.
        /// </summary>
        /// <param name="image">This Image</param>
        /// <param name="from">starts value</param>
        /// <param name="to">final value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFillAmount(this Image image, float from, float to, float duration)
        {
            if (from < 0)
                from = 0;
            if (to > 1)
                to = 1;

            image.fillAmount = from;
            return Infinity.To(from, value => image.fillAmount = value, to, duration).SetGameObject(image.gameObject);
        }
        /// <summary>
        /// Changes smoothly the fill amount of this Image from 0 to 1.
        /// </summary>
        /// <param name="image">This Image</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFillAmountIn(this Image image, float duration)
        {
            return image.DOFillAmount(0.0f, 1.0f, duration).SetGameObject(image.gameObject);
        }
        /// <summary>
        /// Changes smoothly the fill amount of this Image from 1 to 0.
        /// </summary>
        /// <param name="image">This Image</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFillAmountOut(this Image image, float duration)
        {
            return image.DOFillAmount(1.0f, 0.0f, duration).SetGameObject(image.gameObject);
        }

        #endregion Methods
    }
}
