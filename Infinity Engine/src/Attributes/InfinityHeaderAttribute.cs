using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    ///  Attribute used to display our logo and a help button (the decorated field must be a bool type with the name '__help__').
    /// </summary>
    /// <remarks>
    /// If you to draw the header for inherited components, the field must be public or protected
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class InfinityHeaderAttribute : PropertyAttribute
    {
        /// <summary>
        /// Gets the url of the online doc
        /// </summary>
        public string OnlineDocUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Display the default header.
        /// </summary>
        public InfinityHeaderAttribute()
        {
        }

        /// <summary>
        /// Display a header with a button allowing to open an online doc
        /// </summary>
        /// <param name="onlineDocUrl">The url of the online doc</param>
        public InfinityHeaderAttribute(string onlineDocUrl)
        {
            OnlineDocUrl = onlineDocUrl;
        }
    }
}
