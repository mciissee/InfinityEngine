using UnityEngine;

namespace InfinityEngine
{
    /// <summary>    
    ///  Scene object usable in unity inspector.
    /// </summary>
    [System.Serializable]
    public class Scene
    {
        [SerializeField]
        private Object sceneAsset;

        /// <summary>
        /// The name of the scene
        /// </summary>
        public string SceneName => sceneAsset?.name;

        /// <summary>
        /// Convert automatically this <see cref="T:InfinityEngine.Scene" /> object to string object 
        /// (allows to use this object with Unity <see cref="T:UnityEngine.SceneManagement.SceneManager" /> methods.
        /// </summary>
        /// <param name="scene"></param>
        public static implicit operator string(Scene scene)
        {
            return scene.SceneName;
        }
    }
}
