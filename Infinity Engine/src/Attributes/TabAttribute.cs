using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to draw a member inside a tab
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TabAttribute : PropertyAttribute
    {
        /// <summary>
        /// The title of the tab
        /// </summary>
        public string Title
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the draw order of the tab
        /// </summary>
        public int DrawOrder
        {
            get;
            private set;
        }

        /// <summary>
        /// Draws the decorated field inside of an accordion named with the given title
        /// </summary>
        /// <param name="title">The title of the accordion</param>
        public TabAttribute(string title)
        {
            Title = title;
        }

        /// <summary>
        /// Draws the decorated field inside of a tab named with the given title and specify
        /// the draw order of the tab (use this constructor for one of the fields decorated with the attribute 'Tab')
        /// </summary>
        /// <param name="title">The title of the accordion</param>
        /// <param name="drawOrder">
        /// Used to specify the draw order of the accordion
        /// (use this parameter only for one of the fields decorated with the attribute 'Tab')
        /// </param>
        public TabAttribute(string title, int drawOrder)
        {
            Title = title;
            DrawOrder = drawOrder;
        }
    }
}
