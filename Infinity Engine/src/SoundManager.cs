/************************************************************************************************************************************													   *
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS	                                                                            *
*************************************************************************************************************************************/


using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InfinityEngine.Extensions;
using InfinityEngine.DesignPatterns;
using System.Collections.Generic;

namespace InfinityEngine
{

    /// <summary>
    ///  Manages application sound.<br/>
    ///  This is a singleton class and all members of the class are static.
    /// </summary>
    [AddComponentMenu("InfinityEngine/Sound Manager")]
    public class SoundManager : Singleton<SoundManager>
    {

        #region Fields

        private const string MusicOnPref = "___MUSIC_ON___";
        private const string MusicVolumePref = "___MUSIC_VOLUME___";
        private const string EffectOnPref = "___EFFECT_ON___";
        private const string EffectVolumePref = "___EFFECT_VOLUME___";
        private const string CurrentMusicPref = "___CURRENT_MUSIC___";

        private static int CurrentMusicIndex = 0;
        private AudioSource mSonoreEffectSource;
        private AudioSource mMusicSource;

        [SerializeField] private List<AudioClip> musics;
        [SerializeField] private bool isPlayingOnAwake = false;
        [SerializeField] private bool isLoop = true;

        #region Properties

        /// <summary>
        /// The volume of the musics <c>AudioSource</c>
        /// </summary>
        public static float MusicVolume
        {
            get { return PlayerPrefs.GetFloat(MusicVolumePref, 0.5f); }

            set
            {
                PlayerPrefs.SetFloat(MusicVolumePref, value);
                if (Instance.mMusicSource != null)
                    Instance.mMusicSource.volume = value;
            }
        }

        /// <summary>
        /// The volume of the sonore effects <c>AudioSource</c>
        /// </summary>
        public static float EffectVolume
        {
            get { return PlayerPrefs.GetFloat(EffectVolumePref, 0.5f); }

            set
            {
                PlayerPrefs.SetFloat(EffectVolumePref, value);
                if (Instance.mSonoreEffectSource != null)
                    Instance.mSonoreEffectSource.volume = value;
            }
        }

        private bool HasMusic
        {
            get
            {
                if (Instance.musics == null || Instance.musics.Count == 0)
                    return false;

                return PlayerPrefs.GetInt(MusicOnPref, 1).ToBool();
            }
            set
            {
                if (value)
                    DOOnMusic();
                else
                    DOOffMusic();

            }
        }

        private bool HasEffect
        {
            get { return PlayerPrefs.GetInt(EffectOnPref, 1).ToBool(); }
            set
            {
                if (value)
                    DOOnEffects();
                else
                    DOOffEffects();
            }
        }

        /// <summary>
        /// The current playing status of the musics <c>AudioSource</c>.
        /// </summary>
        /// <value><c>true</c> if there is a music that is playing <c>false</c> otherwise.</value>
        public static bool IsPlayingMusic
        {
            get
            {
                if (Instance.mMusicSource == null)
                    return false;

                return Instance.mMusicSource.isPlaying;
            }
        }

        /// <summary>
        /// The current playing status of the effect <c>AudioSource</c>.
        /// </summary>
        /// <value><c>true</c> if there is a effect sonore that is playing <c>false</c> otherwise.</value>
        public static bool IsPlayingEffect
        {
            get
            {
                if (Instance.mSonoreEffectSource == null)
                    return false;

                return Instance.mSonoreEffectSource.isPlaying;
            }
        }

        /// <summary>
        /// The number of musics available.
        /// </summary>
        public static int MusicCount
        {
            get
            {
                if (Instance.musics == null)
                    return 0;
                return Instance.musics.Count;
            }
        }

        /// <summary>
        /// Music that is playing currently.
        /// </summary>
        /// <value>The current <c>AudioClip</c> if there is a music that is playing <c>null</c> otherwise. </value>
        public static AudioClip CurrentMusic
        {
            get
            {
                if (!IsPlayingMusic)
                    return null;
                return Instance.mMusicSource.clip;
            }
        }

        /// <summary>
        /// Is the musics are played in loop?
        /// </summary>
        public static bool Loop { get { return Instance.isLoop; } set { Instance.isLoop = value; } }

        #endregion Properties

        #endregion Fields

        #region Methods

        #region Unity

        private void Awake()
        {

            AddMusicSource();
            AddEffectSource();

            if (isPlayingOnAwake)
                PlayMusic();
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt(MusicOnPref, 1);
            PlayerPrefs.SetInt(EffectOnPref, 1);
            PlayerPrefs.SetFloat(MusicVolumePref, .5f);
            PlayerPrefs.SetFloat(EffectVolumePref, .5f);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                PauseMusic();
            else
                ResumeMusic();
        }

        #endregion Unity

        #region Other

        /// <summary>
        /// Add Listeners to the given buttons
        /// </summary>
        /// <param name="musicToggle">Toggle to control music enable state</param>
        /// <param name="musicSlider">Slider to control music volume</param>
        /// <param name="effectToggle">Toggle to control effects enable state</param>
        /// <param name="effectSlider">Slider to control effects volume</param>
        public static void AddListeners(Toggle musicToggle, Slider musicSlider, Toggle effectToggle, Slider effectSlider)
        {
            musicToggle.isOn = Instance.HasMusic;
            musicToggle.onValueChanged.AddListener((bool value) =>
            {
                Instance.HasMusic = value;
            });

            effectToggle.isOn = Instance.HasEffect;
            effectToggle.onValueChanged.AddListener((bool value) =>
            {
                Instance.HasEffect = value;
            });

            musicSlider.value = MusicVolume;
            musicSlider.onValueChanged.AddListener((float value) =>
            {
                MusicVolume = value;
            });

            effectSlider.value = EffectVolume;
            effectSlider.onValueChanged.AddListener((float value) =>
            {
                EffectVolume = value;
            });

        }

        #region Music

        private void AddMusicSource()
        {
            if (Instance.mMusicSource == null)
            {
                var audioSource = new GameObject("Music Audio Source");
                mMusicSource = audioSource.AddComponent<AudioSource>();
                mMusicSource.playOnAwake = false;
                mMusicSource.loop = isLoop;
                mMusicSource.volume = MusicVolume;

                Infinity.onSceneChanged += () => Destroy(audioSource.gameObject);
            }
        }

        /// <summary>
        /// Begins Play music if there is music.
        /// </summary>
        public static void PlayMusic()
        {
            if (!Instance.HasMusic)
                return;

            Instance.AddMusicSource();

            if (Instance.isLoop)
            {
                CurrentMusicIndex = PlayerPrefs.GetInt(CurrentMusicPref);
                if (CurrentMusicIndex >= Instance.musics.Count)
                    CurrentMusicIndex = 0;

                Instance.mMusicSource.clip = Instance.musics[CurrentMusicIndex];
                Instance.mMusicSource.Play();
            }
            else
            {
                Instance.StartCoroutine(Instance.AutoPlay());
            }
        }

        /// <summary>
        /// Add new music
        /// </summary>
        /// <param name="clip">The music</param>
        public static void AddMusic(AudioClip clip)
        {
            if (Instance.musics == null)
                Instance.musics = new List<AudioClip>();

            Instance.musics.Add(clip);
        }

        /// <summary>
        /// Play the music at the given index if it exists.
        /// </summary>
        /// <param name="index">index of the music</param>
        public static void PlayMusic(int index)
        {
            CurrentMusicIndex = index;
            PlayerPrefs.SetInt(CurrentMusicPref, CurrentMusicIndex);
            PlayMusic();
        }

        /// <summary>
        /// Stop playing music
        /// </summary>
        public static void StopMusic()
        {
            if (!Instance.HasMusic)
                return;

            if (Instance.mMusicSource != null)
                Instance.mMusicSource.Stop();
        }

        /// <summary>
        /// Pause current music if the there a music that is playing.
        /// </summary>
        public static void PauseMusic()
        {
            if (!Instance.HasMusic)
                return;

            if (Instance.mMusicSource != null)
                Instance.mMusicSource.Pause();
        }

        /// <summary>
        /// Disable pause and resume the current music if there is a music.
        /// </summary>
        public static void ResumeMusic()
        {
            if (!Instance.HasMusic)
                return;
            if (Instance.mMusicSource != null)
                Instance.mMusicSource.UnPause();
        }

        /// <summary>
        /// Shuffle music if there is a musics.
        /// </summary>
        public static void ShuffleMusics()
        {
            if (!Instance.HasMusic)
                return;
            Instance.AddMusicSource();
            Instance.musics.Shuffle();
        }

        private IEnumerator AutoPlay()
        {
            while (HasMusic)
            {
                if (CurrentMusicIndex >= Instance.musics.Count)
                    CurrentMusicIndex = 0;

                mMusicSource.clip = Instance.musics[CurrentMusicIndex];
                mMusicSource.Play();

                yield return new WaitForSeconds(mMusicSource.clip.length);
                CurrentMusicIndex++;
            }

            PlayerPrefs.SetInt(CurrentMusicPref, CurrentMusicIndex);

        }

        private void DOOnMusic()
        {
            PlayerPrefs.SetInt(MusicOnPref, 1);
            PlayMusic();
        }

        private void DOOffMusic()
        {
            PlayerPrefs.SetInt(MusicOnPref, 0);
            if (mMusicSource != null)
                mMusicSource.Stop();

        }

        #endregion Music

        #region Effect

        private void AddEffectSource()
        {
            if (Instance.mSonoreEffectSource == null)
            {
                var audioSource = new GameObject("Effect Audio Source");
                mSonoreEffectSource = audioSource.AddComponent<AudioSource>();
                mSonoreEffectSource.volume = EffectVolume;
                Infinity.onSceneChanged += () => Destroy(audioSource.gameObject);
            }
        }

        /// <summary>
        /// Play the given <paramref name="clip"/> AudioClip
        /// </summary>
        /// <param name="clip">AudioClip to play</param>
        public static void PlayEffect(AudioClip clip)
        {
            if (!Instance.HasEffect)
                return;

            Instance.AddEffectSource();
            Instance.mSonoreEffectSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Play the given <paramref name="clip"/> AudioClip in 3D space.
        /// </summary>
        /// <param name="clip">AudioClip to play</param>
        /// <param name="position">Position</param>
        public static void PlayEffect(AudioClip clip, Vector3 position)
        {
            if (!Instance.HasEffect)
                return;

            Instance.AddEffectSource();
            AudioSource.PlayClipAtPoint(clip, position);
        }

        /// <summary>
        /// On sonore effect audio source
        /// </summary>
        private void DOOnEffects()
        {
            PlayerPrefs.SetInt(EffectOnPref, 1);
        }

        /// <summary>
        /// Off sonore effect audio source
        /// </summary>
        private void DOOffEffects()
        {
            PlayerPrefs.SetInt(EffectOnPref, 0);

        }

        #endregion Effect

        #endregion Other

        #endregion Methods

    }
}