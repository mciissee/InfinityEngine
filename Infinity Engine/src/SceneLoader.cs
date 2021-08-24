using System;
using UnityEngine;

namespace InfinityEngine
{

    /// <summary>
    ///   Simple scene loader script
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        /// <summary>
        /// The scene to load
        /// </summary>
        public Scene scene;

        /// <summary>
        /// Scene loading delay
        /// </summary>
        public float delay;

        /// <summary>
        /// Load the scene on start
        /// </summary>
        public bool loadOnStart;

        /// <summary>
        /// Load the scene
        /// </summary>
        public void Load()
        {
            Infinity.LoadLevelAfterDelay(scene.SceneName, delay);
        }

        private void OnValidate()
        {
            delay = Mathf.Clamp(delay, 0f, delay);
        }

        private void Start()
        {
            if (loadOnStart)
            {
                Load();
            }
        }
    }
}
