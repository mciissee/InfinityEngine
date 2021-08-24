using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InfinityEngine
{
    /// <summary>
    /// <see cref="T:InfinityEngine.SoundManager" /> user interface.
    /// </summary>
    public class SoundManagerUI : MonoBehaviour
    {
        /// <summary>
        /// Music volume slider 
        /// </summary>
        public Slider musicSlider;

        /// <summary>
        /// Sonore effects volume slider
        /// </summary>
        public Slider effectSlider;

        private void Start()
        {
            musicSlider.value = SoundManager.MusicVolume;
            effectSlider.value = SoundManager.EffectVolume;
            musicSlider.onValueChanged.AddListener(value => SoundManager.MusicVolume = value);
            effectSlider.onValueChanged.AddListener(value => SoundManager.EffectVolume = value);
        }
    }
}
