/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using InfinityEngine;
using System.Collections.Generic;
using UnityEngine;
using InfinityEngine.Interpolations;
using InfinityEngine.Utils;

namespace InfinityEngine.Extensions
{
    /// <summary>
    /// Extension methods for <a href="https://docs.unity3d.com/ScriptReference/Material.html"> Material</a>  class
    /// </summary>
    public static class MaterialExtensions
    {
        #region Methods 

        /// <summary>
        /// Fades the color of this material
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this Material material, float from, float to, float duration)
        {
            if (material.HasProperty("_Color"))
            {
                Color tmp = material.color;
                return Infinity.To(from, (newValue) =>
                {
                    tmp.a = newValue;
                    material.color = tmp;
                }, to, duration);
            }
            else
            {
                Debugger.LogError("This material has no _Color property");
                return null;
            }

        }

        /// <summary>
        /// Fades the color of this material
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this Material material, float to, float duration)
        {
            return material.DOFade(material.color.a, to, duration);
        }

        /// <summary>
        /// Changes smoothly the color of the property of this Material with the given name.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this Material material, string propertyName, Color from, Color to, float duration)
        {
            if (material.HasProperty(propertyName))
                return Infinity.To(from, color => material.SetColor(propertyName, color), to, duration);
            else
                Debugger.LogError(string.Format("This material has no  {0} property", propertyName));

            return null;
        }


        /// <summary>
        /// Changes smoothly the color of the property of this Material with the given name.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this Material material, string propertyName, Color to, float duration)
        {
            if (material.HasProperty(propertyName))
                return Infinity.To(material.GetColor(propertyName), color => material.SetColor(propertyName, color), to, duration);
            else
                Debugger.LogError(string.Format("This material has no  {0} property", propertyName));
            return null;
        }


        /// <summary>
        /// Changes smoothly the color of this Material.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this Material material, Color from, Color to, float duration)
        {
            return material.DOColor("_Color", from, to, duration);
        }
        /// <summary>
        /// Changes smoothly the color of this Material.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this Material material, Color to, float duration)
        {
            return material.DOColor(material.color, to, duration);
        }

        /// <summary>
        /// Changes the color of the property of this Material with the given name each time '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORandomColor(this Material material, string propertyName, float interval, float duration)
        {
            if (material.HasProperty(propertyName))
            {
                Interpolable interpolation = null;
                interpolation = material.DOColor(propertyName, Infinity.RandomColor(), duration).OnComplete((arg) =>
                {
                    interpolation.SetNewStart(Infinity.RandomColor());
                    interpolation.Reverse();

                }).SetRepeatInterval(interval).SetRepeat(-1);

                return interpolation;
            }
            else
            {
                Debugger.LogError(string.Format("This material has no  {0} property", propertyName));
                return null;
            }
        }

        /// <summary>
        /// Changes the color of this Material each time '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORandomColor(this Material material, float interval, float duration)
        {
            return material.DORandomColor("_Color", interval, duration);

        }

        /// <summary>
        /// Picks a color in the given list and changes the color the property of this Material with the given name each time '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="propertyName">name of the property</param>
        /// <param name="colors">The color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this Material material, string propertyName, List<Color> colors, float interval, float duration)
        {
            if (material.HasProperty(propertyName))
            {
                Interpolable interpolation = null;
                interpolation = material.DOColor(propertyName, colors.Random(), duration).OnComplete((arg) =>
                {
                    colors.Shuffle();
                    interpolation.SetNewStart(colors.Random());
                    interpolation.Reverse();
                }).SetRepeatInterval(interval).SetRepeat(-1);

                return interpolation;
            }
            else
            {
                Debugger.LogError(string.Format("This material has no  {0} property", propertyName));
                return null;
            }
        }
        /// <summary>
        /// Picks a color in the given list and changes the color the property of this Material with the given name each time '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="propertyName">name of the property</param>
        /// <param name="colors">The color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this Material material, string propertyName, Color[] colors, float interval, float duration)
        {
            if (material.HasProperty(propertyName))
            {
                Interpolable interpolation = null;
                interpolation = material.DOColor(propertyName, colors.Random(), duration).OnComplete((arg) =>
                {
                    colors.Shuffle();
                    interpolation.SetNewStart(colors.Random());
                    interpolation.Reverse();
                }).SetRepeatInterval(interval).SetRepeat(-1);

                return interpolation;
            }
            else
            {
                Debugger.LogError(string.Format("This material has no  {0} property", propertyName));
                return null;
            }
        }

        /// <summary>
        /// Picks a color in the given list and changes the color of this Material each time '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="colors">The color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this Material material, List<Color> colors, float interval, float duration)
        {
            return material.DOPickColor("_Color", colors, interval, duration);
        }

        /// <summary>
        /// Picks a color in the given list and changes the color of this Material each time '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="colors">The color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this Material material, Color[] colors, float interval, float duration)
        {
            return material.DOPickColor("_Color", colors, interval, duration);
        }

        /// <summary>
        /// Changes this Material mainTextureOffset. Use this methods to do scrolling effect
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="to">Offset</param>
        /// <param name="duration">Duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOOffset(this Material material, Vector2 to, float duration)
        {
            return Infinity.To(material.mainTextureOffset, value => material.mainTextureOffset = value, to, duration);
        }
        /// <summary>
        /// Changes the texture offset of the property of this Material with the given name.
        /// </summary>
        /// <param name="material">this Material</param>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="to">Offset</param>
        /// <param name="duration">Duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOOffset(this Material material, string propertyName, Vector2 to, float duration)
        {
            return Infinity.To(material.GetTextureOffset(propertyName), value => material.SetTextureOffset(propertyName, value), to, duration);
        }

        #endregion Methods
    }
}
