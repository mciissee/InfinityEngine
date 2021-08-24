using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to draw a progress bar (the decorated field must be a float type)
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ProgressBarAttribute : PropertyAttribute
    {
        public ProgressBarAttribute()
        {
        }
    }
}
