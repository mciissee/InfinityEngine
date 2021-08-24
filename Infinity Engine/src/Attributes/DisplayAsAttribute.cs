using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to override the name of the field in the inspector
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayAsAttribute : PropertyAttribute
    {
        /// <summary>
        /// Gets the display name of the field
        /// </summary>
        public string DisplayName
        {
            get;
            private set;
        }

        /// <summary>
        /// Displays the decorated field with the given name
        /// </summary>
        /// <param name="name">The name to display in the inspector</param>
        public DisplayAsAttribute(string name)
        {
            DisplayName = name;
        }
    }
}
