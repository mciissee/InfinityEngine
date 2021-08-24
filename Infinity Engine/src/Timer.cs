using InfinityEngine.Attributes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace InfinityEngine
{
    /// <summary>
    /// Timer type
    /// </summary>
    public enum TimerType
    {
        /// <summary>
        /// Timer
        /// </summary>
        Timer,
        /// <summary>
        /// ProgressBar 
        /// </summary>
        ProgressBar
    }

    /// <summary>
    ///    Timer component
    /// </summary>
    [OverrideInspector]
    public class Timer : MonoBehaviour
    {
        [InfinityHeader]
        [SerializeField]
        private bool __help__;

        [Accordion("Parameters")]
        [Message("Timer -> The label will display the remaining time in timer format [15, 14, 13 ..., 0]\nProgressBar -> The label will display the remaining time in percentage format[0%, 5% ..., 100%]", MessageTypes.Info)]
        [SerializeField]
        private TimerType type;

        [Accordion("Parameters")]
        [Message("The image to use as progress bar (must have the field ImageType sets to ImageType.Filled)", MessageTypes.Info)]
        [SerializeField]
        private Image progressBar;

        [Accordion("Parameters")]
        [Message("The label which displays the remaining time", MessageTypes.Info)]
        [SerializeField]
        private Text label;

        [Accordion("Parameters")]
        [Message("The duration of the timer", MessageTypes.Info)]
        [SerializeField]
        private float maxValue;

        [Space(5f)]
        [Accordion("Options")]
        [Message("from duration to 0 instead of to duration ", MessageTypes.Info)]
        [SerializeField]
        private bool reverse;

        [Accordion("Options")]
        [Message("Starts the timer when the script is initialized", MessageTypes.Info)]
        [SerializeField]
        private bool startOnAwake;

        [Accordion("Options")]
        [Message("Auto restarts the timer at the end", MessageTypes.Info)]
        [SerializeField]
        private bool autoRestart;

        [Accordion("Options")]
        [Message("Changes the color of the timer at 75%", MessageTypes.Info)]
        [SerializeField]
        private bool changeColor = true;

        [Accordion("Options")]
        [Message("The starts color of the timer", MessageTypes.Info)]
        [VisibleIf("changeColor", MemberTypes.Field, false)]
        [SerializeField]
        private Color startsColor;

        [Accordion("Options")]
        [Message("The color of the timer at 75%", MessageTypes.Info)]
        [VisibleIf("changeColor", MemberTypes.Field, false)]
        [SerializeField]
        private Color endsColor;

        private float remainingTime;

        private float rate;

        /// <summary>
        /// Action to do when the timer finish
        /// </summary>
        public Action onEnd;

        /// <summary>
        /// The remaning time 
        /// </summary>
        public float RemainingTime => remainingTime;

        /// <summary>
        /// The type of the timer
        /// </summary>
        public TimerType Type
        {
            get => type;
            set => type = value;
        }

        private void Awake()
        {
            if (label != null)
            {
                label.text = (reverse ? maxValue.ToString() : "0");
            }
            progressBar.type = Image.Type.Filled;
            progressBar.fillAmount = reverse ? 1 : 0;
            if (startOnAwake)
            {
                if (autoRestart)
                {
                    onEnd = (Action)Delegate.Combine(onEnd, new Action(RestartTimer));
                }
                StartTimer();
            }
        }

        /// <summary>
        /// Creates new timer
        /// </summary>
        /// <param name="progress">Progress bar image (Must be filled type)</param>
        /// <param name="maxValue">Timer max value</param>
        /// <param name="reverse">if set to <c>true</c> timer start value will be the max value</param>
        /// <returns>new Timer</returns>
        public static Timer NewTimer(Image progress, float maxValue, bool reverse = false)
        {
            var val = new GameObject("Timer", new Type[]
            {
                typeof(Timer)
            });
            var component = val.GetComponent<Timer>();
            component.progressBar = progress;
            component.maxValue = maxValue;
            component.label = null;
            component.onEnd = null;
            component.reverse = reverse;
            return component;
        }

        /// <summary>
        /// Creates new timer
        /// </summary>
        /// <param name="progress">Progress bar image (Must be filled type)</param>
        /// <param name="maxValue">Timer max value</param>
        /// <param name="label">Label that display timer value</param>
        /// <param name="reverse">if set to <c>true</c> timer start value will be the max value</param>
        /// <returns>new Timer</returns>
        public static Timer NewTimer(Image progress, float maxValue, Text label, bool reverse = false)
        {
            var timer = NewTimer(progress, maxValue, reverse);
            timer.label = label;
            return timer;
        }

        /// <summary>
        /// Creates new timer
        /// </summary>
        /// <param name="progress">Progress bar image (Must be filled type)</param>
        /// <param name="maxValue">Timer max value</param>
        /// <param name="label">Label that display timer value</param>
        /// <param name="callback">En callback action</param>
        /// <param name="reverse">if set to <c>true</c> timer start value will be the max value</param>
        /// <returns>new Timer</returns>
        public static Timer NewTimer(Image progress, float maxValue, Text label, Action callback, bool reverse = false)
        {
            var timer = NewTimer(progress, maxValue, label, reverse);
            timer.onEnd = callback;
            return timer;
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StartTimer()
        {
            this.StartCoroutine(DoTickTimer());
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void StopTimer()
        {
            this.StopAllCoroutines();
        }

        /// <summary>
        /// Restarts the timer
        /// </summary>
        public void RestartTimer()
        {
            StopTimer();
            ResetTimer();
            StartTimer();
        }

        /// <summary>
        /// Pauses the timer
        /// </summary>
        public void PauseTimer()
        {
            rate = 0f;
        }

        /// <summary>
        /// Resumes the timer
        /// </summary>
        public void ResumeTimer()
        {
            rate = 1f / maxValue;
        }

        /// <summary>
        /// Resets the timer
        /// </summary>
        public void ResetTimer()
        {
            var text = string.Empty;
            if (reverse)
            {
                progressBar.fillAmount = 1f;
                remainingTime = maxValue;
                text = maxValue.ToString();
            }
            else
            {
                progressBar.fillAmount = 0f;
                remainingTime = 0f;
                text = "0";
            }
            if (label != null)
            {
                label.text = text;
            }
        }

        /// <summary>
        /// Resets the timer
        /// </summary>
        /// <param name="value">new max value</param>
        public void ResetTimer(float value)
        {
            maxValue = value;
            ResetTimer();
        }

        private IEnumerator DoTickTimer()
        {
            if (changeColor)
            {
                progressBar.color = startsColor;
            }
            var min = 0f;
            var max = 1f;
            if (reverse)
            {
                max = 0f;
                min = 1f;
            }
            var timer = 0f;
            rate = 1f / maxValue;
            while (timer < 1f)
            {
                timer += rate * Time.deltaTime;
                var num = Mathf.Lerp(min, max, timer);
                progressBar.fillAmount = num;
                remainingTime = (float)(int)(num * maxValue);
                if (timer >= 0.75f && changeColor)
                {
                    progressBar.color = endsColor;
                }
                if (label != null)
                {
                    switch (type)
                    {
                        case TimerType.Timer:
                            label.text = remainingTime.ToString();
                            break;
                        case TimerType.ProgressBar:
                            label.text = (int)(timer * 100f) + "%";
                            break;
                    }
                }
                yield return null;
            }
            onEnd?.Invoke();
        }

    }
}
