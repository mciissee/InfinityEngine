#pragma warning disable RECS0082 // Parameter has the same name as a member and hides it

using InfinityEngine.DesignPatterns;
using System;
using UnityEngine;

namespace InfinityEngine
{
    /// <summary>
    /// Displays message on screen.
    /// </summary>
    public class Toast : Singleton<Toast>
    {
        /// <summary>
        /// Screen positions.
        /// </summary>
        public enum Position
        {
            TopLeft,
            TopRight,
            TopCenter,
            BottomLeft,
            BottomRight,
            BottomCenter,
            Center,
            CenterLeft,
            CenterRight
        }

        /// <summary>
        /// short duration 
        /// </summary>
        public const float LENGHT_SHORT = 3f;

        /// <summary>
        /// long duration
        /// </summary>
        public const float LENGHT_LONG = 5f;

        private Position _Position;

        private Rect position;

        private string text;

        private bool show;

        private GUIStyle style;

        /// <summary>
        /// Gets a value indicating if the toast is visible on screen.
        /// </summary>
        public static bool IsVisible => Instance.show;

        /// <summary>
        /// Toast style
        /// </summary>
        public static GUIStyle Style
        {
            get
            {
                return Instance.style;
            }
            set
            {
                Instance.style = value;
            }
        }

        private void Awake()
        {
            show = false;
            text = "";
            Infinity.onSceneChanged = (Action)Delegate.Combine(Infinity.onSceneChanged, new Action(Dispose));
        }

        private void OnGUI()
        {
            if (show)
            {
                if (style == null)
                {
                    style = GUI.skin.box;
                    style.fontStyle = 0;
                    style.fontSize = 18;
                    style.alignment = TextAnchor.MiddleCenter;
                    style.border = new RectOffset(6, 6, 6, 6);
                    style.normal.textColor = Color.white;
                }
                position = GUILayoutUtility.GetRect(new GUIContent(text), GUI.skin.box);
                ref Rect reference = ref position;
                reference.y = (reference.y - 5f);
                ref Rect reference2 = ref position;
                reference2.height = (reference2.height + 5f);
                FindScreenPosition();
                GUI.Box(position, text, style);
            }
        }

        private void FindScreenPosition()
        {
            switch (_Position)
            {
                case Position.TopLeft:
                    position.x = (0f);
                    position.y = (0f);
                    break;
                case Position.TopCenter:
                    position.x = Screen.width / 2 - position.width / 2f;
                    position.y = (0f);
                    break;
                case Position.TopRight:
                    position.x = Screen.width - position.width;
                    position.y = (0f);
                    break;
                case Position.BottomLeft:
                    position.x = (0f);
                    position.y = Screen.height - position.height;
                    break;
                case Position.BottomCenter:
                    position.x = Screen.width / 2 - position.width / 2f;
                    position.y = Screen.height - position.height;
                    break;
                case Position.BottomRight:
                    position.x = Screen.width - position.width;
                    position.y = Screen.height - position.height;
                    break;
                case Position.CenterLeft:
                    position.x = (0f);
                    position.y = Screen.height / 2 - position.height / 2f;
                    break;
                case Position.Center:
                    position.x = Screen.width / 2 - position.width / 2f;
                    position.y = Screen.height / 2 - position.height / 2f;
                    break;
                case Position.CenterRight:
                    position.x = Screen.width - position.width;
                    position.y = Screen.height / 2 - position.height / 2f;
                    break;
            }
        }

        /// <summary>
        /// Display a message in screen at Bottom-center during 3 secs
        /// </summary>
        /// <param name="text"></param>
        public static void MakeText(string text)
        {
            MakeText(text, 3f, Position.BottomCenter);
        }

        /// <summary>
        /// Display a message in screen
        /// </summary>
        /// <param name="text">the message</param>
        /// <param name="duration">the message duration</param>
        public static void MakeText(string text, float duration)
        {
            MakeText(text, duration, Position.BottomCenter);
        }

        /// <summary>
        /// Display a message in screen
        /// </summary>
        /// <param name="text">the message</param>
        /// <param name="position">message position on screen</param>
        public static void MakeText(string text, Position position)
        {
            MakeText(text, 3f, position);
        }

        /// <summary>
        /// Display a message in screen
        /// </summary>
        /// <param name="text">the message</param>
        /// <param name="duration">the duration</param>
        /// <param name="position">message position on screen</param>
        public static void MakeText(string text, float duration, Position position)
        {
            Instance.text = text;
            Instance.show = true;
            Instance._Position = position;
            Infinity.After(duration, delegate
            {
                Instance.text = string.Empty;
                Instance.show = false;
            });
        }

        /// <summary>
        /// Hide the current text
        /// </summary>
        public static void Dispose()
        {
            Instance.show = false;
        }
    }
}
