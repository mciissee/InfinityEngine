using InfinityEngine.Extensions;
using System;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace InfinityEngine.Utils
{
    /// <summary>
    ///  Static references to System.Type objects 
    /// </summary>
    public static class TypeOF
    {
        /// <summary>
        /// typeof(<see cref="UnityEngine.Object"/>)
        /// </summary>
        public static Type Object = typeof(UnityEngine.Object);
        /// <summary>
        /// typeof(<see cref="bool"/>)
        /// </summary>
        public static Type Boolean = typeof(bool);

        /// <summary>
        /// typeof(<see cref="int"/>)
        /// </summary>
        public static Type Int32 = typeof(int);

        /// <summary>
        /// typeof(<see cref="long"/>)
        /// </summary>
        public static Type Long = typeof(long);

        /// <summary>
        /// typeof(<see cref="double"/>)
        /// </summary>
        public static Type Double = typeof(double);

        /// <summary>
        /// typeof(<see cref="float"/>)
        /// </summary>
        public static Type Float = typeof(float);

        /// <summary>
        /// typeof(<see cref="string"/>)
        /// </summary>
        public static Type String = typeof(string);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Vector2"/>)
        /// </summary>
        public static Type Vector2 = typeof(Vector2);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Vector3"/>)
        /// </summary>
        public static Type Vector3 = typeof(Vector3);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Vector4"/>)
        /// </summary>
        public static Type Vector4 = typeof(Vector4);
        /// <summary>
        /// typeof(<see cref="UnityEngine.Quaternion"/>)
        /// </summary>
        public static Type Quaternion = typeof(Quaternion);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Color"/>)
        /// </summary>
        public static Type Color = typeof(Color);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Rect"/>)
        /// </summary>
        public static Type Rect = typeof(Rect);

        /// <summary>
        /// typeof(<see cref="UnityEngine.RectOffset"/>)
        /// </summary>
        public static Type RectOffset = typeof(RectOffset);
        /// <summary>
        /// typeof(<see cref="UnityEngine.GameObject"/>)
        /// </summary>
        public static Type GameObject = typeof(GameObject);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Transform"/>)
        /// </summary>
        public static Type Transform = typeof(Transform);

        /// <summary>
        /// typeof(<see cref="UnityEngine.RectTransform"/>)
        /// </summary>
        public static Type RectTransform = typeof(RectTransform);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Rigidbody"/>)
        /// </summary>
        public static Type Rigidbody = typeof(Rigidbody);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Rigidbody2D"/>)
        /// </summary>
        public static Type Rigidbody2D = typeof(Rigidbody2D);

        /// <summary>
        /// typeof(<see cref="UnityEngine.UI.Text"/>)
        /// </summary>
        public static Type Text = typeof(Text);

        /// <summary>
        /// typeof(<see cref="UnityEngine.CanvasGroup"/>)
        /// </summary>
        public static Type CanvasGroup = typeof(CanvasGroup);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Canvas"/>)
        /// </summary>
        public static Type Canvas = typeof(Canvas);

        /// <summary>
        /// typeof(<see cref="UnityEngine.SpriteRenderer"/>)
        /// </summary>
        public static Type SpriteRenderer = typeof(SpriteRenderer);

        /// <summary>
        /// typeof(<see cref="UnityEngine.MeshRenderer"/>)
        /// </summary>
        public static Type MeshRenderer = typeof(MeshRenderer);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Mesh"/>)
        /// </summary>
        public static Type Mesh = typeof(Mesh);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Light"/>)
        /// </summary>
        public static Type Light = typeof(Light);
        /// <summary>
        /// typeof(<see cref="UnityEngine.LineRenderer"/>)
        /// </summary>
        public static Type LineRenderer = typeof(LineRenderer);

        /// <summary>
        /// typeof(<see cref="UnityEngine.TrailRenderer"/>)
        /// </summary>
        public static Type TrailRenderer = typeof(TrailRenderer);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Camera"/>)
        /// </summary>
        public static Type Camera = typeof(Camera);

        /// <summary>
        /// typeof(<see cref="UnityEngine.UI.Image"/>)
        /// </summary>
        public static Type Image = typeof(Image);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Texture2D"/>)
        /// </summary>
        public static Type Texture2D = typeof(Texture2D);

        /// <summary>
        /// typeof(<see cref="UnityEngine.GUISkin"/>)
        /// </summary>
        public static Type GUISkin = typeof(GUISkin);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Font"/>)
        /// </summary>
        public static Type Font = typeof(Font);

        /// <summary>
        /// typeof(<see cref="UnityEngine.AudioClip"/>)
        /// </summary>
        public static Type AudioClip = typeof(AudioClip);

        /// <summary>
        /// typeof(<see cref="UnityEngine.TextAsset"/>)
        /// </summary>
        public static Type TextAsset = typeof(TextAsset);
        /// <summary>
        /// typeof(<see cref="UnityEngine.Material"/>)
        /// </summary>
        public static Type Material = typeof(Material);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Sprite"/>)
        /// </summary>
        public static Type Sprite = typeof(Sprite);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Shader"/>)
        /// </summary>
        public static Type Shader = typeof(Shader);
        /// <summary>
        /// typeof(<see cref="UnityEngine.Animation"/>)
        /// </summary>
        public static Type Animation = typeof(Animation);

        /// <summary>
        /// typeof(<see cref="UnityEngine.AnimationClip"/>)
        /// </summary>
        public static Type AnimationClip = typeof(AnimationClip);

        /// <summary>
        /// typeof(<see cref="UnityEngine.Animator"/>)
        /// </summary>
        public static Type Animator = typeof(Animator);

        /// <summary>
        /// typeof(<see cref="UnityEngine.PhysicMaterial"/>)
        /// </summary>
        public static Type PhysicMaterial = typeof(PhysicMaterial);

        /// <summary>
        /// typeof(<see cref="UnityEngine.PhysicsMaterial2D"/>)
        /// </summary>
        public static Type PhysicsMaterial2D = typeof(PhysicsMaterial2D);

        /// <summary>
        /// typeof(<see cref="System.Xml.XmlDocument"/>)
        /// </summary>
        public static Type XmlDocument = typeof(XmlDocument);

        /// <summary>
        /// typeof(<see cref="System.FlagsAttribute"/>)
        /// </summary>
        public static Type FlagsAttribute = typeof(FlagsAttribute);

        /// <summary>   Searches for the first field of this class which match for the given name. </summary>
        ///
        /// <param name="name"> The name. </param>
        /// <returns>   A Type. </returns>
        public static Type Find(string name)
        {
            var type = typeof(TypeOF);
            var found = type.GetFields().FirstOrDefault(field => field.Name == name);
            return found == null ? null : (Type)found.GetValue(null);
        }
    }
}
