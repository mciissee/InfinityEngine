using UnityEngine;

namespace InfinityEngine
{
    /// <summary>
    ///    Script that contains useful functions usable with the buttons in unity inspector.
    /// </summary>
    public class Helper : MonoBehaviour
    {
        /// <summary>
        /// Quit application
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Open a app link in parameter
        /// </summary>
        /// <param name="url">link</param>
        public void OpenUrl(string url)
        {
            Application.OpenURL(url);
        }

        /// <summary>
        /// Open the page of the android application identified by the given package name in android market
        /// </summary>
        /// <param name="packageName">The name of package (com.cpmpany.product..)</param>
        public void OpenAndroidMarketUrl(string packageName)
        {
            Application.OpenURL($"market://details?id={packageName}");
        }

        /// <summary>
        /// Plays the given audio clip
        /// </summary>
        /// <param name="clip">The audio to play</param>
        public void PlaySound(AudioClip clip)
        {
            SoundManager.PlayEffect(clip);
        }
    }
}
