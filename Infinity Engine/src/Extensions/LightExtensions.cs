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
    ///  Extension methods for <a href="https://docs.unity3d.com/ScriptReference/Light.html"> Light</a>  class
    /// </summary>
    public static class LightExtensions
    {
        #region Methods

        /// <summary>
        /// Fades this Light alpha
        /// </summary>
        /// <param name="light">this Light</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this Light light, float from, float to, float duration)
        {
            Color tmp = light.color;
            return Infinity.To(from, (newValue) => {
                tmp.a = newValue;
                light.color = tmp;
            }, to, duration);
        }
        /// <summary>
        /// Fades this Light alpha
        /// </summary>
        /// <param name="light">this Light</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this Light light, float to, float duration)
        {
            return light.DOFade(light.color.a, to, duration);
        }
   
        /// <summary>
        /// Fades this Light color
        /// </summary>
        /// <param name="light">this Light</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this Light light, Color from, Color to, float duration)
        {
            return Infinity.To(from, color => light.color = color, to, duration);
        }
        /// <summary>
        /// Fades this Light color
        /// </summary>
        /// <param name="light">this Light</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOColor(this Light light, Color to, float duration)
        {
            return light.DOColor(light.color, to, duration);
        }

        /// <summary>
        /// Fades this Light intensity
        /// </summary>
        /// <param name="light">this Light</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOIntensity(this Light light, float from, float to, float duration)
        {
            return Infinity.To(from, value => light.intensity = value, to, duration);
        }

        /// <summary>
        /// Fades this Light intensity
        /// </summary>
        /// <param name="light">this Light</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOIntensity(this Light light, float to, float duration)
        {

            return light.DOIntensity(light.intensity, to, duration);

        }
    
        /// <summary>
        /// Changes smoothly the strength of the shadow of this light
        /// </summary>
        /// <param name="light">this Light</param>
        /// <param name="from">starts value</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOShadowStrength(this Light light, float from, float to, float duration)
        {
            return Infinity.To(from, value => light.shadowStrength = value, to, duration);
        }

        /// <summary>
        /// Changes smoothly the strength of the shadow of this light
        /// </summary>
        /// <param name="light">this Light</param>
        /// <param name="to">finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOShadowStrengthTo(this Light light, float to, float duration)
        {

            return light.DOShadowStrength(light.shadowStrength, to, duration);

        }

        #endregion Methods
    }
}
