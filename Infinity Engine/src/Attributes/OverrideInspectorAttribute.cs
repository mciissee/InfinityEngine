using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to override the replaces the way Unity draw the inspector of a MonoBehaviour component
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class OverrideInspectorAttribute : PropertyAttribute
    {
        public OverrideInspectorAttribute()
        {
        }
    }
}
