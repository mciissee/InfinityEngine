using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to draw a button in the inspector
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class InspectorButtonAttribute : PropertyAttribute
    {
        /// <summary>
        /// Gets the name of the method
        /// </summary>
        public string MethodName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the text of the button (the name of the method by default)
        /// </summary>
        public string Label
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the button is centered
        /// </summary>
        public bool Center
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the width of the button
        /// </summary>
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the height of the button
        /// </summary>
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the location of the button
        /// </summary>
        public InspectorButtonLocations Location
        {
            get;
            private set;
        }

        public InspectorButtonAttribute(string method, InspectorButtonLocations location = InspectorButtonLocations.Bottom)
        {
            MethodName = method;
            Location = location;
            Label = method;
        }

        public InspectorButtonAttribute(string method, string label, InspectorButtonLocations location = InspectorButtonLocations.Bottom)
        {
            MethodName = method;
            Label = label;
            Location = location;
        }

        public InspectorButtonAttribute(string method, int width, int height, bool center = false, InspectorButtonLocations location = InspectorButtonLocations.Bottom)
        {
            MethodName = method;
            Label = method;
            Location = location;
            Width = width;
            Height = height;
            Center = center;
        }

        public InspectorButtonAttribute(string method, string label, int width, int height, bool center = false, InspectorButtonLocations location = InspectorButtonLocations.Bottom)
        {
            MethodName = method;
            Label = label;
            Location = location;
            Width = width;
            Height = height;
            Center = center;
        }
    }
}
