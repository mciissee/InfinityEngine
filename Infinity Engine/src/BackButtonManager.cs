#pragma warning disable RECS0649 // Comparison of floating point numbers with equality operator

using InfinityEngine.DesignPatterns;
using System;
using UnityEngine;

namespace InfinityEngine
{
    /// <summary>
    ///    Manages back button pressed event (escape on window , back arrow on mobile)
    /// </summary>
    public class BackButtonManager : Singleton<BackButtonManager>
    {
        private int backClickCount;

        private float resetInterval = 5f;

        private float time;

        private Action action;

        /// <summary>
        /// Action to do when the button is pressed
        /// </summary>
        public Action Action
        {
            get => action;
            set => action = value;
        }

        private void Awake()
        {
            time = Time.time + resetInterval;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                backClickCount++;
                Action?.Invoke();
            }
            if (Time.time > time)
            {
                backClickCount = 0;
                time = Time.time + resetInterval;
            }
        }

        /// <summary>
        /// The number of times that the button is pressed during the last 5 seconds
        /// </summary>
        /// <returns></returns>
        public static int GetClickCount()
        {
            return Instance.backClickCount;
        }

        /// <summary>
        /// Create you custom action
        /// </summary>
        /// <param name="action">the action to do when back key is pressed</param>
        public static void SetAction(Action action)
        {
            Instance.Action = action;
        }

        /// <summary>
        /// Quit Application if user click two times the back button
        /// in 5 secs else show the message in screen
        /// </summary>
        ///  <param name="message">the message to display if is the first click</param>
        public static void SetActionExit(string message)
        {
            Instance._ActionExit(message);
        }

        /// <summary>
        /// Load the level in parameter if user click two times the back button
        /// in 5 secs else show the message in screen
        /// </summary>
        /// <param name="level">the level to load</param>
        /// <param name="message">the message to display if is the first click</param>
        public static void SetActionLoadLevel(string level, string message)
        {
            Instance._ActionLoadLevel(level, message);
        }

        private void _ActionExit(string message)
        {
            Action = () =>
            {
                if (GetClickCount() >= 2)
                {
                    Application.Quit();
                }
                else
                {
                    Toast.MakeText("Press again to Quit !");
                }
            };
            Instance.time = Time.time + Instance.resetInterval;
        }

        private void _ActionLoadLevel(string level, string message)
        {
            Action = () =>
            {
                if (GetClickCount() >= 2)
                {
                    backClickCount = 0;
                    time = Time.time + resetInterval;
                    Toast.Dispose();
                    Infinity.LoadLevelAfterDelay(level, 0f);
                }
                else
                {
                    Toast.MakeText(message);
                }
            };
        }
    }
}
