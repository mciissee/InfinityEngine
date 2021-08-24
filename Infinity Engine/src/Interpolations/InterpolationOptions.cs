using System;
using UnityEngine;

namespace InfinityEngine.Interpolations
{
    /// <summary>
    /// Options for an <see cref="T:InfinityEngine.Interpolations.Interpolable" /> object.
    ///
    /// This class is serialzable and visible in unity inspector, you can use it to configurates an Interpolable object.
    /// </summary>
    [Serializable]
    public class InterpolationOptions
    {
        [SerializeField]
        private float delay;

        [SerializeField]
        private int repeat;

        [SerializeField]
        private float repeatInterval;

        [SerializeField]
        private LoopTypes repeatType;

        [SerializeField]
        private EaseTypes ease;

        [SerializeField]
        private AnimationCurve curve;

        /// <summary>
        /// The start delay of the interpolation.
        /// </summary>
        public float Delay
        {
            get
            {
                return delay;
            }
            set
            {
                delay = value;
            }
        }

        /// <summary>
        /// The number of repeatition of the interpolation.
        /// </summary>
        public int Repeat
        {
            get
            {
                return repeat;
            }
            set
            {
                repeat = value;
            }
        }

        /// <summary>
        /// The repeatition interval of the interpolation.
        /// </summary>
        public float RepeatInterval
        {
            get
            {
                return repeatInterval;
            }
            set
            {
                repeatInterval = value;
            }
        }

        /// <summary>
        /// The repeatition type.
        /// </summary>
        public LoopTypes RepeatType
        {
            get
            {
                return repeatType;
            }
            set
            {
                repeatType = value;
            }
        }

        /// <summary>
        /// The ease type
        /// </summary>
        public EaseTypes Ease
        {
            get
            {
                return ease;
            }
            set
            {
                ease = value;
            }
        }

        /// <summary>
        /// The AnimationCurve to use as ease.
        /// </summary>
        public AnimationCurve Curve
        {
            get
            {
                return curve;
            }
            set
            {
                curve = value;
            }
        }
    }
}
