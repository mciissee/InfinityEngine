/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using System;
using UnityEngine;

namespace InfinityEngine
{
    /// <summary>  
    /// Base interface of all KeyValue classes.
    /// </summary>
    public interface IKeyValue
    {
        /// <summary>
        /// The key of the encapsulated object.
        /// </summary>
        string Key { get; set; }

        /// <summary>  
        ///  The encapsulated object . (You have to cast it)
        ///</summary>
        object Obj { get; }

        /// <summary>
        /// Checks if the encapsulated object is missing
        /// </summary>
        bool IsMissing { get; }

    }

    /// <summary>
    /// Encapsulates a pair of key-value where the type of key is <c>string</c> and the type of value <c>T</c>
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    [Serializable]
    public class KeyValue<T> : IKeyValue
    {

        [SerializeField] private string key;
        [SerializeField] private T value;

        /// <summary>
        /// The key
        /// </summary>
        public string Key
        {
            get => key;
            set => key = value;
        }

        /// <summary>  
        ///  The encapsulated object. (You must cast it)
        ///</summary>
        public object Obj => value;

        /// <summary>
        /// Checks if the object linked to this is missing
        /// </summary>
        public bool IsMissing => Equals(value, null);

        /// <summary>
        ///The encapsulated object
        /// </summary>
        public T Value
        {
            get => value;
            set => this.value = value;
        }

        /// <summary>
        /// Creates new instance of <c>KeyPair</c>
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        public KeyValue(string key, T value)
        {
            this.key = key;
            this.value = value;
        }

        /// <summary>
        /// Cast implicitly this object as an object of type T
        /// </summary>
        /// <param name="arg">this</param>
        public static implicit operator T(KeyValue<T> arg) => arg.value;
    }

    /// <summary>
    ///  Encapsulates a pair of key-value where the type of key is <c>string</c> and the type of value <c>int</c>
    /// </summary>
    [Serializable]
    public class IntKV : KeyValue<int>
    {
        /// <summary>
        /// Creates new instance of <c>IntKeyPair</c>
        /// </summary>
        /// <param name="key">this key</param>
        /// <param name="value">this value</param>
        public IntKV(string key, int value) : base(key, value)
        {

        }

        /// <summary>
        /// Allows to use this object like an integer object (only for get purpose)
        /// </summary>
        /// <param name="arg">this</param>
        public static implicit operator int(IntKV arg) => arg.Value;
    }

    /// <summary>
    /// Encapsulates a pair of key-value where the type of key is <c>string</c> and the type of value <c>float</c>
    /// </summary>
    [Serializable]
    public class FloatKV : KeyValue<float>
    {

        /// <summary>
        /// Creates new instance of <c>FloatKeyPair</c>
        /// </summary>
        /// <param name="key">this key</param>
        /// <param name="value">this value</param>
        public FloatKV(string key, float value) : base(key, value)
        {

        }
    }

    /// <summary>
    ///  Encapsulates a pair of key-value where the type of key is <c>string</c> and the type of value <c>long</c>
    /// </summary>
    [Serializable]
    public class LongKV : KeyValue<long>
    {
        /// <summary>
        /// Creates new instance of <c>LongKeyPair</c>
        /// </summary>
        /// <param name="key">this key</param>
        /// <param name="value">this value</param>
        public LongKV(string key, long value) : base(key, value)
        {

        }

    }

    /// <summary>
    /// Encapsulates a pair of key-value where the type of key is <c>string</c> and the type of value <c>bool</c>
    /// </summary>
    [Serializable]
    public class BoolKV : KeyValue<bool>
    {
        /// <summary>
        /// Creates new instance of <c>BoolKeyPair</c>
        /// </summary>
        /// <param name="key">this key</param>
        /// <param name="value">this value</param>
        public BoolKV(string key, bool value) : base(key, value)
        {

        }

        /// <summary>
        /// Allows to use this object like an bool object (only for get purpose)
        /// </summary>
        /// <param name="arg">this</param>
        public static implicit operator bool(BoolKV arg) => arg.Value;
    }

    /// <summary>
    ///   Encapsulates a pair of key-value where the type of key is <c>string</c> and the type of value <c>string</c>
    /// </summary>
    [Serializable]
    public class StringKV : KeyValue<string>
    {
        /// <summary>
        /// Creates new instance of <c>StringKeyPair</c>
        /// </summary>
        /// <param name="key">this key</param>
        /// <param name="value">this value</param>
        public StringKV(string key, string value) : base(key, value)
        {

        }

    }

    /// <summary>
    ///  Encapsulates a pair of key-value where the type of key is <c>string</c> and the type of value <c>Vector2</c>
    /// </summary>
    [Serializable]
    public class Vector2KV : KeyValue<Vector2>
    {
        /// <summary>
        /// Creates new instance of <c>Vector2KeyPair</c>
        /// </summary>
        /// <param name="key">this key</param>
        /// <param name="value">this value</param>
        public Vector2KV(string key, Vector2 value) : base(key, value)
        {

        }

    }

    /// <summary>
    ///   Encapsulates a pair of key-value where the type of key is <c>string</c> and the type of value <c>Vector3</c>
    /// </summary>
    [Serializable]
    public class Vector3KV : KeyValue<Vector3>
    {
        /// <summary>
        /// Creates new instance of <c>Vector3KeyPair</c>
        /// </summary>
        /// <param name="key">this key</param>
        /// <param name="value">this value</param>
        public Vector3KV(string key, Vector3 value) : base(key, value)
        {

        }

    }

    /// <summary>
    ///  Encapsulates a pair of key-value where the type of key is <c>string</c> and the type of value <c>Vector4</c>
    /// </summary>
    [Serializable]
    public class Vector4KV : KeyValue<Vector4>
    {
        /// <summary>
        /// Creates new instance of <c>Vector4KeyPair</c>
        /// </summary>
        /// <param name="key">this key</param>
        /// <param name="value">this value</param>
        public Vector4KV(string key, Vector4 value) : base(key, value)
        {

        }

    }

    /// <summary>
    ///  Encapsulates a pair of key-value where the type of key is <c>string</c> and the type of value <c>Quaternion</c>
    /// </summary>
    [Serializable]
    public class QuaternionKV : KeyValue<Quaternion>
    {
        /// <summary>
        /// Creates new instance of <c>QuaternionKeyPair</c>
        /// </summary>
        /// <param name="key">this key</param>
        /// <param name="value">this value</param>
        public QuaternionKV(string key, Quaternion value) : base(key, value)
        {

        }
    }

    /// <summary>
    /// Encapsulates a pair of key-value where the type of key is <c>string</c>  and the type of value <c>Color</c>
    /// </summary>
    [Serializable]
    public class ColorKV : KeyValue<Color>
    {
        /// <summary>
        /// Creates new instance of <c>ColorKeyPair</c>
        /// </summary>
        /// <param name="key">this key</param>
        /// <param name="value">this value</param>
        public ColorKV(string key, Color value) : base(key, value)
        {

        }
    }


    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/AnimationClip.html"> UnityEngine.AnimationClip </a>  identified by a string key.
    /// </summary>
    [Serializable]
    public class AnimationClipKV : KeyValue<AnimationClip>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        ///</summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public AnimationClipKV(string name, AnimationClip value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/AudioClip.html"> UnityEngine.AudioClip </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class AudioClipKV : KeyValue<AudioClip>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        ///</summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public AudioClipKV(string name, AudioClip value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/Font.html"> UnityEngine.Font </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class FontKV : KeyValue<Font>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        ///</summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public FontKV(string name, Font value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/GameObject.html"> UnityEngine.GameObject </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class GameObjectKV : KeyValue<GameObject>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        ///</summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public GameObjectKV(string name, GameObject value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/GUISkin.html"> UnityEngine.GUISkin </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class GUISkinKV : KeyValue<GUISkin>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        ///</summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public GUISkinKV(string name, GUISkin value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/Material.html"> UnityEngine.Material </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class MaterialKV : KeyValue<Material>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        /// </summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public MaterialKV(string name, Material value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/Mesh.html"> UnityEngine.Mesh </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class MeshKV : KeyValue<Mesh>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        /// </summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public MeshKV(string name, Mesh value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/PhysicMaterial.html"> UnityEngine.PhysicMaterial </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class PhysicMaterialKV : KeyValue<PhysicMaterial>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        /// </summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public PhysicMaterialKV(string name, PhysicMaterial value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/PhysicsMaterial2D.html"> UnityEngine.PhysicsMaterial2D </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class PhysicsMaterial2DKV : KeyValue<PhysicsMaterial2D>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        /// </summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public PhysicsMaterial2DKV(string name, PhysicsMaterial2D value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/Shader.html"> UnityEngine.Shader </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class ShaderKV : KeyValue<Shader>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        /// </summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public ShaderKV(string name, Shader value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/Sprite.html"> UnityEngine.Sprite </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class SpriteKV : KeyValue<Sprite>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        /// </summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public SpriteKV(string name, Sprite value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/TextAsset.html"> UnityEngine.TextAsset </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class TextAssetKV : KeyValue<TextAsset>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        /// </summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public TextAssetKV(string name, TextAsset value) : base(name, value) { }
    }

    /// <summary>
    /// Encapsulates an Object of the type <a href="https://docs.unity3d.com/ScriptReference/Texture2D.html"> UnityEngine.Texture2D </a> identified by a string key.
    /// </summary>
    [Serializable]
    public class Texture2DKV : KeyValue<Texture2D>
    {
        /// <summary>
        ///   Creates new instance of this class. 
        ///</summary>
        /// <param name="name"> The name of the resource </param>
        /// <param name="value"> The object linked to this resource.</param>
        public Texture2DKV(string name, Texture2D value) : base(name, value) { }
    }
}