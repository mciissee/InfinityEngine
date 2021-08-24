using System;
using UnityEngine;

namespace InfinityEngine.Utils
{
    /// <summary>
    /// Class used to apply options to a font awesome style in editor mode
    /// </summary>
    public class FAOption
    {
        private readonly Action<GUIStyle> action;

        private FAOption(Action<GUIStyle> action)
        {
            this.action = action;
        }

        public static FAOption FontSize(int size)
        {
            return new FAOption(delegate (GUIStyle style)
            {
                style.fontSize = size;
            });
        }

        public static FAOption TextColor(Color color)
        {
            return new FAOption(delegate (GUIStyle style)
            {
                style.normal.textColor = color;
            });
        }

        public static FAOption TextAnchor(TextAnchor anchor)
        {
            return new FAOption(delegate (GUIStyle style)
            {
                style.alignment = anchor;
            });
        }

        public static FAOption Padding(RectOffset padding)
        {
            return new FAOption(delegate (GUIStyle style)
            {
                style.padding = padding;
            });
        }

        internal void ApplyTo(GUIStyle style)
        {
            action(style);
        }
    }
}
