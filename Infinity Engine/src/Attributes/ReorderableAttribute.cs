using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to draw display an array or list thanks to unity reorderable list
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ReorderableAttribute : PropertyAttribute
    {
        public ReorderableAttribute()
        {
        }
    }
}
