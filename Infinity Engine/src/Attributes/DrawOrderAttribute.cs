using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to specify a custom draw order for a field
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DrawOrderAttribute : PropertyAttribute
    {
        /// <summary>
        /// Gets the draw order of the decorated field
        /// </summary>
        public int Order
        {
            get;
            private set;
        }

        /// <summary>
        /// Specify a custom draw order for the decorated field
        /// </summary>
        /// <param name="drawOrder">The draw order of the field</param>
        public DrawOrderAttribute(int drawOrder)
        {
            Order = drawOrder;
        }
    }
}
