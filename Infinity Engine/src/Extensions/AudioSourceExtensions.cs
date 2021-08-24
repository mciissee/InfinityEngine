/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using InfinityEngine.Interpolations;
using UnityEngine;

namespace InfinityEngine.Extensions
{
    /// <summary>
    ///  Extension methods for <a href="https://docs.unity3d.com/ScriptReference/AudioSource.html"> AudioSource </a> class
    /// </summary>
    public static class AudioSourceExtensions
    {
        #region Methods

        /// <summary>
        /// Fades this audio source volume
        /// </summary>
        /// <param name="audioSource">this AudioSource</param>
        /// <param name="from">starts volume</param>
        /// <param name="to">finals volume</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this AudioSource audioSource, float from, float to, float duration)
        {
            return Infinity.To(from, value => audioSource.volume = value, to, duration);
        }

        /// <summary>
        /// Fades this audio source volume
        /// </summary>
        /// <param name="audioSource">this AudioSource</param>
        /// <param name="to">finals volume</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this AudioSource audioSource, float to, float duration)
        {
            if (to < 0f)
            {
                to = 0f;
            }
            else if (to > 1f)
            {
                to = 1f;
            }
            return audioSource.DOFade(audioSource.volume, to, duration);
        }

        /// <summary>
        /// Fades this audio source pitch
        /// </summary>
        /// <param name="audio">this AudioSource</param>
        /// <param name="from">starts volume</param>
        /// <param name="to">finals volume</param>
        /// <param name="duration">interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPitch(this AudioSource audio, float from, float to, float duration)
        {
            return Infinity.To(from, value => audio.pitch = value, to, duration);
        }

        /// <summary>
        /// Fades this audio source pitch
        /// </summary>
        /// <param name="audio">this AudioSource</param>
        /// <param name="to">finals volume</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPitch(this AudioSource audio, float to, float duration)
        {
            return audio.DOPitch(audio.pitch, to, duration);
        }

        #endregion Methods
    }
}
