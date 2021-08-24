using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to draw a member inside an accordion
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class AccordionAttribute : PropertyAttribute
    {
        /// <summary>
        /// The title of the accordion
        /// </summary>
        public string Title
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the draw order of the accordion
        /// </summary>
        public int DrawOrder
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether only one accordion can be expanded at the same time.
        /// </summary>
        public bool IsSingleMode
        {
            get;
            private set;
        }

        /// <summary>
        /// Draws the decorated field inside of an accordion named with the given title
        /// </summary>
        /// <param name="title">The title of the accordion</param>
        public AccordionAttribute(string title)
        {
            Title = title;
        }

        /// <summary>
        /// Draws the decorated field inside of an accordion named with the given title and specify
        /// the draw order of the accordion (use this constructor for one of the fields inside of the same accordion)
        /// </summary>
        /// <param name="title">The title of the accordion</param>
        /// <param name="drawOrder">
        /// Used to specify the draw order of the accordion
        /// (use this parameter only for one of the fields with the accordion)
        /// </param>
        public AccordionAttribute(string title, int drawOrder)
        {
            Title = title;
            DrawOrder = drawOrder;
        }

        /// <summary>
        /// Draws the decorated field inside of an accordion named with the given title and specify
        /// the draw order of the accordion (use this constructor for one of the fields inside of the same accordion)
        /// </summary>
        /// <param name="title">The title of the accordion</param>
        /// <param name="singleMode">A value indicating whether only one accordion can be expanded at the same time</param>
        /// <param name="drawOrder">
        /// Used to specify the draw order of the accordion
        /// (use this parameter only for one of the fields with the accordion)
        /// </param>
        public AccordionAttribute(string title, bool singleMode, int drawOrder)
        {
            Title = title;
            IsSingleMode = singleMode;
            DrawOrder = drawOrder;
        }
    }
}
