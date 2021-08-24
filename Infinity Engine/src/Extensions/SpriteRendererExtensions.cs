/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using InfinityEngine.Interpolations;

namespace InfinityEngine.Extensions
{
    /// <summary>
    ///  Extension methods for <a href="https://docs.unity3d.com/ScriptReference/SpriteRenderer.html"> SpriteRenderer </a> class
    /// </summary>
    public static class SpriteRendererExtensions
    {

        #region Methods
        /// <summary>
        /// Changes this spriteRenderer alpha
        /// </summary>
        /// <param name="spriteRenderer">this SpriteRenderer</param>
        /// <param name="alpha">New alpha</param>
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
        {
            Color tmp = spriteRenderer.color;
            tmp.a = alpha;
            spriteRenderer.color = tmp;
        }

        /// <summary>
        ///  Fades the alpha of this SpriteRenderer
        /// </summary>
        /// <param name="spriteRenderer">this SpriteRenderer</param>
        ///<param name="from">initial alpha</param>
        ///<param name="to">final alpha</param>
        /// <param name="duration">change interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this SpriteRenderer spriteRenderer, float from, float to, float duration)
        {
            return Infinity.To(from, (newValue) => spriteRenderer.SetAlpha(newValue), to, duration).SetGameObject(spriteRenderer.gameObject);
        }

        /// <summary>
        /// Fades the alpha of this SpriteRenderer
        /// </summary>
        /// <param name="spriteRenderer">this SpriteRenderer</param>
        ///<param name="to">final alpha</param>
        /// <param name="duration">change interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this SpriteRenderer spriteRenderer, float to, float duration)
        {
            return spriteRenderer.DOFade(spriteRenderer.color.a, to, duration);
        }

        ///<summary>
        /// Lerp the color of this SpriteRenderer
        /// </summary>
        /// <param name="spriteRenderer">this SpriteRenderer</param>
        ///<param name="from">initial alpha</param>
        ///<param name="to">final alpha</param>
        /// <param name="duration">change interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this SpriteRenderer spriteRenderer, Color from, Color to, float duration)
        {
            return Infinity.To(from, color => spriteRenderer.color = color, to, duration)
                .SetGameObject(spriteRenderer.gameObject);
        }
        ///<summary>
        /// Lerp the color of this SpriteRenderer
        /// </summary>
        /// <param name="spriteRenderer">this SpriteRenderer</param>
        ///<param name="to">final alpha</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this SpriteRenderer spriteRenderer, Color to, float duration)
        {
            return spriteRenderer.DOColor(spriteRenderer.color, to, duration);
        }

        /// <summary>
        /// Changes the color of this image each  '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="spriteRenderer">this SpriteRenderer</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORandomColor(this SpriteRenderer spriteRenderer, float interval, float duration)
        {
            Interpolable interpolation = null;
            interpolation = spriteRenderer.DOColor(Infinity.RandomColor(), duration).OnComplete((arg) =>
            {
                interpolation.SetNewStart(Infinity.RandomColor());
                interpolation.Reverse();
            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(spriteRenderer.gameObject);

            return interpolation;
        }

        /// <summary>
        /// Picks a color in the given list and changes the color of this spriteRenderer each '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="spriteRenderer">this SpriteRenderer</param>
        /// <param name="colors">Color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this SpriteRenderer spriteRenderer, List<Color> colors, float interval, float duration)
        {
            Interpolable interpolation = null;
            interpolation = spriteRenderer.DOColor(colors.Random(), duration).OnComplete((arg) =>
            {
                colors.Shuffle();
                interpolation.SetNewStart(colors.Random());
                interpolation.Reverse();
            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(spriteRenderer.gameObject);

            return interpolation;
        }

        /// <summary>
        /// Picks a color in the given list and changes the color of this spriteRenderer each '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="spriteRenderer">this SpriteRenderer</param>
        /// <param name="colors">Color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this SpriteRenderer spriteRenderer, Color[] colors, float interval, float duration)
        {
            Interpolable interpolation = null;
            interpolation = spriteRenderer.DOColor(colors.Random(), duration).OnComplete((arg) =>
            {
                colors.Shuffle();
                interpolation.SetNewStart(colors.Random());
                interpolation.Reverse();
            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(spriteRenderer.gameObject);

            return interpolation;
        }

        #endregion Methods
    }
}
