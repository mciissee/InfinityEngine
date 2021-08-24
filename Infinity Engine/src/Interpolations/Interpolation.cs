#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
#pragma warning disable RECS0108 // Warns about static fields in generic types

using InfinityEngine.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using InfinityEngine.Utils;

//// <summary>
//// This namespace allow to creates smooth animations thanks to interpolation formulas.
//// </summary>
namespace InfinityEngine.Interpolations
{
    /// <summary>
    ///     Interpolates the value of a data between 2 values in a given time.
    /// </summary>
    /// <remarks>
    ///          In the current version, this class can interpolate the data of type : <br />
    ///
    ///          - <see cref="T:System.Int32" /><br /> - <see cref="T:System.Single" /><br /> - <see cref="T:UnityEngine.Vector2" /><br /> - <see cref="T:UnityEngine.Vector3" /><br /> 
    ///          - <see cref="T:UnityEngine.Vector4" /><br /> - <see cref="T:UnityEngine.Quaternion" /><br />  - <see cref="T:UnityEngine.Color" /><br /> - <see cref="T:UnityEngine.Rect" /><br /> 
    ///          - <see cref="T:UnityEngine.RectOffset" /><br /> 
    /// </remarks>
    /// <typeparam name="T">The type of the data to interpolate</typeparam>
    [Serializable]
    public class Interpolation<T> : Interpolable
    {
        /// <summary> 
        /// List of types supported as generic parameter of this class. 
        /// </summary>
        public static readonly HashSet<Type> SupportedTypes = new HashSet<Type>
        {
            typeof(float),
            typeof(int),
            typeof(Vector2),
            typeof(Vector3),
            typeof(Vector4),
            typeof(Quaternion),
            typeof(Color),
            typeof(Rect),
            typeof(RectOffset)
        };

        private GameObject mGameObject;

        private T mStartValue;

        private T mEndValue;

        private Type mType;

        private bool mIsTerminated;

        private bool mIsPlaying;

        private bool mIspaused;

        private bool mHasGameObject;

        private bool mIsStarted;

        private bool mIsCached;

        private Action<T> mSetter;

        private int mRepeatCount;

        private float mCurrentTime;

        private float mDuration;

        private float mStartDelay;

        private float mRepeatInterval;

        private float mLastTimeScale = 1f;

        private EaseTypes mEaseType;

        private LoopTypes mLoopType;

        private RotationModes mRotationMode;

        private bool mDisableOnPause;

        private bool mDisableOnHide;

        /// <summary>
        /// Custom ease function
        /// </summary>
        private AnimationCurve mCustomEase;

        /// <summary>
        /// The GameObject linked to the interpolation if it exists
        /// </summary>
        public GameObject GameObject => mGameObject;

        /// <summary>
        /// Gets a value indicating the current completed percent of the interpolation.
        /// </summary>
        public float CompletedPercent
        {
            get
            {
                if (mDuration <= 0f)
                {
                    return 0f;
                }
                return mCurrentTime / mDuration;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the interpolable object is cached to be reused. (<c>false</c> by default)
        /// </summary>
        public bool IsCached => mIsCached;

        /// <summary>   
        /// Gets a value indicating whether the interpolation is started (<c>true</c> only after a call of the function <see cref="M:InfinityEngine.Interpolations.Interpolation`1.Start" />)
        /// </summary>
        public bool IsStarted => mIsStarted;

        /// <summary>   
        /// Gets a value indicating whether the interpolation is playing. (<c>true</c> only if the interpolation is playing -&gt; <c>false</c> on pause state)
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                return mIsPlaying;
            }
            internal set
            {
                mIsPlaying = value;
            }
        }

        /// <summary>   
        /// Gets a value indicating whether the interpolation is paused.     
        /// </summary>
        public bool IsPaused => mIspaused;

        /// <summary>
        /// Gets a value indicating whether the interpolation is terminated.
        /// </summary>
        /// <remarks>
        /// The value is <c>true</c> only if the interpolation has already been launched and it is currently complete.
        /// </remarks>
        public bool IsTerminated => mIsTerminated;

        /// <summary>
        /// Gets the time scale of the interpolation.
        /// The duration of the interpolation by this value on each update.
        /// </summary>
        public float TimeScale
        {
            get;
            set;
        }

        /// <summary>
        /// The type of the data which is interpolated by the Interpolable.
        /// </summary>
        public Type Type => mType;

        /// <summary>
        /// Gets the ends value of the interpolation.
        /// </summary>
        public T StartValue => mStartValue;

        /// <summary>
        /// Gets the starts value of the interpolation.
        /// </summary>
        public T EndValue => mEndValue;

        /// <summary>
        /// Gets a value indicating whether the interpolation should be disabled when gameobject linked to it is inactive.
        /// </summary>
        public bool IsDisableOnHide => mDisableOnHide;

        /// <summary>
        /// Gets a value indicating whether the interpolation should be disabled when the application is on pause.
        /// </summary>
        /// <value>the value is <c>true</c> by default</value>
        public bool IsDisableOnPause => mDisableOnPause;

        /// <summary>
        /// Gets the type of ease used for the interpolation
        /// </summary>
        public EaseTypes EaseType => mEaseType;

        /// <summary>
        /// Gets a value indicating the duration of the interpolation
        /// </summary>
        public float Duration => mDuration;

        /// <summary>
        /// Gets a value indicating the number of times the interpolation will be repeated. (-1 indicates an infinite repetition)
        /// </summary>
        public int RepeatCount => mRepeatCount;

        /// <summary>
        /// Gets a value 
        /// </summary>
        public float StartDelay => mStartDelay;

        /// <summary>
        /// Gets a value indicating the pause time between each repetition of the interpolation
        /// </summary>
        public float RepeatInterval => mRepeatInterval;

        /// <summary>
        /// Gets a value indicating the type of repetition.
        /// </summary>
        public LoopTypes LoopType => mLoopType;

        /// <summary>
        /// Gets a value indicating whether the interpolation is linked to a GameObject
        /// </summary>
        public bool HasGameObject => mHasGameObject;

        /// <summary>
        /// Action performed at the begening
        /// </summary>
        public Action<Interpolable> StartCallback
        {
            get;
            set;
        }

        /// <summary>
        /// Action performed during the interpolation
        /// </summary>
        public Action<Interpolable> UpdateCallback
        {
            get;
            set;
        }

        /// <summary>
        /// Action performed when the interpolation is paused
        /// </summary>
        public Action<Interpolable> PauseCallback
        {
            get;
            set;
        }

        /// <summary>
        /// Action performed after the interpolation
        /// </summary>
        public Action<Interpolable> CompleteCallback
        {
            get;
            set;
        }

        /// <summary>
        /// Action performed when the interpolation is finished and stopped
        /// </summary>
        public Action<Interpolable> TerminateCallback
        {
            get;
            set;
        }

        /// <summary>
        /// Creates new Interpolation.
        /// </summary>
        /// <param name="from">Starts value</param>
        /// <param name="to">Ends value</param>
        /// <param name="setter">The setter function</param>
        /// <param name="duration">The duration</param>
        internal Interpolation(T from, T to, float duration, Action<T> setter)
        {
            Reset(from, to, duration, setter);
        }

        internal Interpolation<T> Reset(T getter, T to, float duration, Action<T> setter)
        {
            if (mType == null)
            {
                mType = typeof(T);
            }
            mGameObject = null;
            mStartValue = getter;
            mSetter = setter;
            mEndValue = to;
            mDuration = duration;
            mRepeatCount = 0;
            mRepeatInterval = 0f;
            mEaseType = EaseTypes.Linear;
            mLoopType = LoopTypes.Restart;
            mRotationMode = RotationModes.Around360;
            mStartDelay = 0f;
            TimeScale = 1f;
            mLastTimeScale = 1f;
            mDisableOnPause = true;
            mDisableOnHide = true;
            mIsTerminated = false;
            mIsPlaying = false;
            mIsStarted = false;
            mCurrentTime = 0f;
            mCustomEase = null;
            mHasGameObject = false;
            StartCallback = null;
            UpdateCallback = null;
            PauseCallback = null;
            CompleteCallback = null;
            TerminateCallback = null;
            return this;
        }

        /// <summary>
        /// Adds a callback action to do at the begining of the Interpolable.
        /// </summary>
        /// <example>
        ///   The following code is an example of use of this method :
        ///     <code>
        ///         var a = 0;
        ///         va interpolation = Infinity.To(a, newValue=&gt; a = newValue, 10, 5)
        ///                                     .OnStart(arg =&gt; 
        ///                                     {
        ///                                         arg.SetNewEnd(50);
        ///                                     });
        ///
        ///         interpolation.Start();
        ///
        ///     </code>
        ///     This code interpolates "a" value from 0 to 10 in 5 seconds and modify the end value of the interpolation when it starts.
        /// </example>
        /// <param name="action">Callback Action</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable OnStart(Action<Interpolable> action)
        {
            StartCallback += action;
            return this;
        }

        /// <summary>
        /// Adds a callback action to do when this Interpolable is paused.
        /// </summary>
        /// <example>
        ///   The following code is an example of use of this method :
        ///     <code>
        ///         var a = 0;
        ///         va interpolation = Infinity.To(a, newValue=&gt; a = newValue, 10, 5)
        ///                                     .OnStart(arg =&gt; {Debug.Log("On Pause"); });
        ///
        ///         interpolation.Start();
        ///
        ///     </code>
        ///     This code interpolates "a" value from 0 to 10 in 5 seconds and prints "On Pause" when 'interpolation' is paused.
        /// </example>
        /// <param name="action">Callback Action</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable OnPause(Action<Interpolable> action)
        {
            PauseCallback += action;
            return this;
        }

        /// <summary>
        /// Adds a callback action to do when this Interpolable is updating.
        /// </summary>
        /// <example>
        ///   The following code is an example of use of this method :
        ///     <code>
        ///         var a = 0;
        ///         var interpolation = Infinity.To(a, newValue=&gt; a = newValue, 10, 5)
        ///                                     .OnUpdate((arg) =&gt; {
        ///                                         if(arg.CompletedPercent &gt;= .5f)
        ///                                             arg.Terminate();
        ///
        ///                                     });
        ///
        ///         interpolation.Start();
        ///     </code>
        ///     This code interpolates "a" value from 0 to 10 in 5 seconds and stops the interpolation when it is completed at 50%.
        /// </example>
        /// <param name="action">Callback Action</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable OnUpdate(Action<Interpolable> action)
        {
            UpdateCallback += action;
            return this;
        }

        /// <summary>
        /// Adds a callback action to do when this Interpolable reached  the end value.
        /// </summary>
        /// <example>
        ///   The following code is an example of use of this method :
        ///     <code>
        ///
        ///        var interpolation = Infinity.To(0, newValue=&gt; a = newValue, 10, 5)
        ///                 .SetRepeat(2)
        ///                 .OnComplete(arg =&gt; arg.Reverse() );
        ///         interpolation.Start();
        ///      </code>
        ///     This code interpolates "a" value from 0 to 10 in 5 seconds, then from 10 to 0 in 5 seconds.
        /// </example>
        /// <param name="action">Callback action to do</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable OnComplete(Action<Interpolable> action)
        {
            CompleteCallback += action;
            return this;
        }

        /// <summary>
        /// Adds a callback action to do when the Interpolable is stopped.
        /// </summary>
        /// <example>
        ///   The following code is an example of use of this method :
        ///     <code>
        ///         var a = 0;
        ///         va interpolation = Infinity.To(a, newValue=&gt; a = newValue, 10, 5)
        ///                                     .OnTerminate(arg =&gt; {Debug.Log("On Terminate"); });
        ///
        ///         interpolation.Start();
        ///
        ///     </code>
        ///     This code interpolates "a" value from 0 to 10 in 5 seconds and prints "On Terminate" when 'interpolation' is finished.
        /// </example>
        /// <param name="action">Callback Action</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable OnTerminate(Action<Interpolable> action)
        {
            TerminateCallback += action;
            return this;
        }

        /// <summary>
        /// Waits a while before starts this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" />
        /// </summary>
        /// <param name="delay">delay in seconds</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetStartDelay(float delay)
        {
            mStartDelay = delay;
            mStartDelay = Mathf.Clamp(mStartDelay, 0f, mStartDelay);
            return this;
        }

        /// <summary>
        /// Repeats the <see cref="T:InfinityEngine.Interpolations.Interpolable" /> (-1 = infinite loop)
        /// </summary>
        /// <param name="count">number of loop</param>
        /// <param name="loopType">Loop type</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetRepeat(int count, LoopTypes loopType = LoopTypes.Restart)
        {
            mRepeatCount = count;
            mRepeatCount = Mathf.Clamp(mRepeatCount, -1, mRepeatCount);
            mLoopType = loopType;
            return this;
        }

        /// <summary>
        ///  Waits a while after completes this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /> before restarting or reversing this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /> in repeat case
        /// </summary>
        /// <param name="interval">Repeat interval in seconds</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetRepeatInterval(float interval)
        {
            mRepeatInterval = interval;
            mRepeatInterval = Mathf.Clamp(mRepeatInterval, 0f, mRepeatInterval);
            return this;
        }

        /// <summary>
        /// Ease type to use.
        /// </summary>
        /// <param name="easeType">Ease type</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetEase(EaseTypes easeType)
        {
            mEaseType = easeType;
            return this;
        }

        /// <summary>
        /// Use Custom <a href="https://docs.unity3d.com/ScriptReference/AnimationCurve.html"> AnimationCurve </a>  like ease.
        /// </summary>
        /// <param name="motion">the animation curve to use</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetEase(AnimationCurve motion)
        {
            mEaseType = EaseTypes.Custom;
            if (motion == null)
            {
                motion = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            }
            mCustomEase = motion;
            return this;
        }

        /// <summary>
        /// Apply the given options to this.
        /// </summary>
        /// <param name="options">The options</param>
        /// <returns>this</returns>
        public Interpolable SetOptions(InterpolationOptions options)
        {
            mStartDelay = options.Delay;
            mRepeatCount = options.Repeat;
            mRepeatInterval = options.RepeatInterval;
            mLoopType = options.RepeatType;
            mEaseType = options.Ease;
            mCustomEase = options.Curve;
            return this;
        }

        /// <summary>
        /// Disables this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /> when Unity <a href="https://docs.unity3d.com/ScriptReference/Time-timeScale.html"> Time.timeScale </a> is set to <c>0</c>.
        /// </summary>
        /// <param name="flag">if set to <c>true</c> this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /> will be paused when the application is on pause</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetDisableOnPause(bool flag)
        {
            mDisableOnPause = flag;
            return this;
        }

        /// <summary>
        /// Changes the duration
        /// </summary>
        /// <exception cref="T:System.ArgumentException">
        /// Throwed when the type of the given <c>value</c> 
        /// is not the same as the type of the generic parameter <c>T</c> of this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" />.
        /// </exception>
        /// <param name="value">new duration</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetNewDuration(float value)
        {
            mDuration = value;
            mDuration = Mathf.Clamp(mDuration, 0f, mDuration);
            return this;
        }

        /// <summary>
        /// Changes the starts value
        /// </summary>
        /// <exception cref="T:System.ArgumentException">
        /// Throwed when the type of the given <c>value</c> 
        /// is not the same as the type of the generic parameter <c>T</c> of this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" />.
        /// </exception>
        /// <param name="value">new value</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetNewEnd(object value)
        {
            if (value.GetType() != Type)
            {
                throw new ArgumentException();
            }
            mEndValue = (T)value;
            return this;
        }

        /// <summary>
        /// Change the ends value
        /// </summary>
        /// <param name="value">new value</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetNewStart(object value)
        {
            if (value.GetType() != Type)
            {
                throw new ArgumentException();
            }
            mStartValue = (T)value;
            return this;
        }

        /// <summary>
        /// Changes the starts value
        /// </summary>
        /// <param name="value">new value</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetNewEnd(T value)
        {
            mEndValue = value;
            return this;
        }

        /// <summary>
        /// Changes the ends value
        /// </summary>
        /// <param name="value">new value</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetNewStart(T value)
        {
            mStartValue = value;
            return this;
        }

        /// <summary>
        /// Disables the interpolable when the GameObject which is linked to it is inactive.
        /// </summary>
        /// <param name="flag">if set to <c>true</c> this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /> will be paused when the <see cref="P:InfinityEngine.Interpolations.Interpolation`1.GameObject" /> which is linked to this is inactive.</param>
        /// <returns>this <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public Interpolable SetDisableOnHide(bool flag)
        {
            mDisableOnHide = flag;
            return this;
        }

        /// <summary>
        /// Attach a GameObject to this Interpolation
        /// </summary>
        /// <param name="go">GameObject to attach</param>
        /// <returns></returns>
        public Interpolable SetGameObject(GameObject go)
        {
            mGameObject = go;
            mHasGameObject = true;
            return this;
        }

        /// <summary>
        /// Sets the rotation mode of the interpolation in the case of Quaternion interpolation
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Interpolable SetRotationMode(RotationModes mode)
        {
            mRotationMode = mode;
            return this;
        }

        /// <summary>
        /// Creates new Interpolable with this configuration.
        /// The parameters which will be copied are : <para> </para> 
        /// repeatition count, repeatition interval, repeatition type, start delay, disable on pause state, disable on hide state, ease type, rotation mode
        /// </summary>
        /// <param name="other">Other to copie</param>
        /// <returns>The copied interpolation</returns>
        public Interpolation<TOther> CopyTo<TOther>(Interpolation<TOther> other)
        {
            other.mRepeatCount = mRepeatCount;
            other.mLoopType = mLoopType;
            other.mDisableOnPause = mDisableOnPause;
            other.mDisableOnHide = mDisableOnHide;
            other.mStartDelay = mStartDelay;
            other.mRepeatInterval = mRepeatInterval;
            other.mEaseType = mEaseType;
            other.mCustomEase = mCustomEase;
            other.mRotationMode = mRotationMode;
            return other;
        }

        /// <summary>
        /// Starts the interpolation and return it.
        /// </summary>
        /// <returns>the interpolation</returns>
        public Interpolable Start()
        {
            if (EaseType == EaseTypes.Custom && mCustomEase == null)
            {
                Debugger.LogError("Animation Curve cannot be null for EaseType.Custom.");
                return this;
            }
            mIsStarted = true;
            mIsPlaying = false;
            mIsTerminated = false;
            mIspaused = false;
            Infinity.StartInterpolation(this);
            return this;
        }

        /// <summary>
        /// Inverses starts and ends value
        /// </summary>
        public Interpolable Reverse()
        {
            T val = mStartValue;
            mStartValue = mEndValue;
            mEndValue = val;
            return this;
        }

        /// <summary>
        /// Stops this Interpolable
        /// </summary>
        public Interpolable Terminate()
        {
            if (mIsStarted && !mIsTerminated)
            {
                mIsTerminated = true;
                mIsPlaying = false;
                mIspaused = false;
                mIsStarted = false;
                TerminateCallback?.Invoke(this);
                if (!mIsCached)
                {
                    Infinity.RecycleInterpolation(this);
                }
            }
            return this;
        }

        /// <summary>
        /// Pauses the interpolation for <paramref name="time" /> seconds.
        /// </summary>
        /// <param name="time">pause time in seconds</param>
        public Interpolable PauseFor(float time)
        {
            time = Mathf.Clamp(time, 0f, time);
            mLastTimeScale = TimeScale;
            TimeScale = 0f;
            mIspaused = true;
            mIsPlaying = false;
            PauseCallback?.Invoke(this);
            Infinity.After(time, delegate
            {
                TimeScale = mLastTimeScale;
                mIspaused = false;
            });
            return this;
        }

        /// <summary>
        /// Switchs this pause state
        /// </summary>
        public Interpolable TogglePause()
        {
            TimeScale = ((TimeScale > 0f) ? 0f : mLastTimeScale);
            mIspaused = (TimeScale == 0f);
            if (mIspaused)
            {
                PauseCallback?.Invoke(this);
                mIsPlaying = false;
            }
            return this;
        }

        /// <summary>
        /// Allow you to manually control the lifecycle of the interpolable (required when you assign the interpolable to an variable that you reuse)
        /// </summary>
        public Interpolable SetCached()
        {
            mIsCached = true;
            return this;
        }

        /// <summary>
        /// Updates the interpolation. (Don't call this method manually).
        /// </summary>
        /// <param name="time">The current time of the interpolation</param>
        internal void Interpolate(float time)
        {
            if (IsPlaying)
            {
                mCurrentTime = time * mDuration;
                if (EaseType == EaseTypes.Custom && mCustomEase != null)
                {
                    mSetter(Easing.Ease(EaseType, StartValue, EndValue, time, mCustomEase, mRotationMode));
                }
                else
                {
                    mSetter(Easing.Ease(EaseType, StartValue, EndValue, time, null, mRotationMode));
                }
            }
        }

        /// <summary>
        /// Checks if the interpolation can be updated.
        /// </summary>
        /// <returns> (not <see cref="P:InfinityEngine.Interpolations.Interpolation`1.IsDisableOnHide" /> sets to <c>true</c> and <see cref="P:InfinityEngine.Interpolations.Interpolation`1.GameObject" /> and all it parents are active if the it is not null)</returns>
        internal bool CanBeInterpolated()
        {
            if (IsDisableOnHide && GameObject != null)
            {
                return mIsPlaying = (GameObject.activeSelf && GameObject.transform.TrueForAllParent((Transform parent) => parent.gameObject.activeSelf));
            }
            mIsPlaying = true;
            return true;
        }
    }
}
