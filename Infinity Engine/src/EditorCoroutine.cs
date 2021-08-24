using System;
using System.Collections;
using UnityEngine;

namespace InfinityEngine
{
    /// <summary>
    /// Provides a way to use coroutines in editor mode.
    /// </summary>
    public class EditorCoroutine
    {
        private readonly IEnumerator routine;

        private object current;

        /// <summary>
        /// Gets a value indicating whether the coroutine is running
        /// </summary>
        public bool IsRunning
        {
            get;
            private set;
        }

        private EditorCoroutine(IEnumerator routine)
        {
            this.routine = routine;
        }

        /// <summary>
        /// Starts the coroutine
        /// </summary>
        /// <param name="routine">The coroutine</param>
        /// <returns>An object of type EditorCoroutine</returns>
        public static EditorCoroutine Start(IEnumerator routine)
        {
            var editorCoroutine = new EditorCoroutine(routine);
            editorCoroutine.Start();
            return editorCoroutine;
        }

        /// <summary>
        /// Stops the coroutine
        /// </summary>
        public void Stop()
        {
            IsRunning = false;
            Infinity.editorUpdate -= Update;
        }

        private void Start()
        {
            Infinity.editorUpdate += Update;
            IsRunning = true;
        }

        private void Update()
        {
            current = routine.Current;
            if (current is IEnumerator enumerator)
            {
                if (!enumerator.MoveNext())
                {
                    Next();
                }
            }
            else if (current is WWW www)
            {
                if (www.isDone)
                {
                    Next();
                }
            }
            else
            {
                Next();
            }
        }

        private void Next()
        {
            if (!routine.MoveNext())
            {
                Stop();
            }
        }
    }
}
