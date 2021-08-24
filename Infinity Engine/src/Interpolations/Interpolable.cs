using System;
using UnityEngine;

namespace InfinityEngine.Interpolations
{
    /// <summary>
    ///  Base interface of <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /> class.<br />
    ///  As the class <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /> is generic and unusable with sequence objects,<br />
    ///  This interface works as an marker an provides access to the members of <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /> class.
    /// </summary>
    public interface Interpolable
    {
        /// <summary>
        /// The type of the data to interpolate.
        /// </summary>
        Type Type
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating the current completed percent of the interpolation.
        /// </summary>
        float CompletedPercent
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the interpolation is terminated.
        /// </summary>
        /// <remarks>
        /// The value is <c>true</c> only if the interpolation has already been launched and it is currently complete.
        /// </remarks>
        bool IsTerminated
        {
            get;
        }

        /// <summary>   
        /// Gets a value indicating whether the interpolation is started (<c>true</c> only after a call of the function <see cref="M:InfinityEngine.Interpolations.Interpolable.Start" />)
        /// </summary>
        bool IsStarted
        {
            get;
        }

        /// <summary>   
        /// Gets a value indicating whether the interpolation is playing. (<c>true</c> only if the interpolation is playing -&gt; <c>false</c> on pause state)
        /// </summary>
        bool IsPlaying
        {
            get;
        }

        /// <summary>   
        /// Gets a value indicating whether the interpolation is paused.     
        /// </summary>
        bool IsPaused
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating the duration of the interpolation
        /// </summary>
        float Duration
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating the number of times the interpolation will be repeated. (-1 indicates an infinite repetition)
        /// </summary>
        int RepeatCount
        {
            get;
        }

        /// <summary>
        /// Gets a value 
        /// </summary>
        float StartDelay
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating the pause time between each repetition of the interpolation
        /// </summary>
        float RepeatInterval
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the interpolation is linked to a GameObject
        /// </summary>
        bool HasGameObject
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the interpolable object is cached to be reused. (<c>false</c> by default)
        /// </summary>
        bool IsCached
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating the type of repetition.
        /// </summary>
        LoopTypes LoopType
        {
            get;
        }

        /// <summary>
        /// Changes the start value
        /// </summary>
        /// <exception cref="T:System.ArgumentException">
        /// Throwed when the type of the given <c>value</c> 
        /// is not the same as the type of the generic parameter <c>T</c> of interpolable.
        /// </exception>
        /// <param name="value">new value</param>
        /// <returns>this</returns>
        Interpolable SetNewEnd(object value);

        /// <summary>
        /// Changes the end value
        /// </summary>
        /// <exception cref="T:System.ArgumentException">
        /// Throwed when the type of the given <c>value</c> 
        /// is not the same as the type of the generic parameter <c>T</c> of interpolable.
        /// </exception>
        /// <param name="value">new value</param>
        /// <returns>this</returns>
        Interpolable SetNewStart(object value);

        /// <summary>
        /// Waits a while before starts the interpolable
        /// </summary>
        /// <param name="delay">delay in seconds</param>
        /// <returns>this</returns>
        Interpolable SetStartDelay(float delay);

        /// <summary>
        /// Repeat the interpolation (-1 = infinite loop)
        /// </summary>
        /// <param name="repeat">number of loop</param>
        /// <param name="loopType">Loop type</param>
        /// <returns>this</returns>
        Interpolable SetRepeat(int repeat, LoopTypes loopType = LoopTypes.Restart);

        /// <summary>
        ///  Waits a while after completes the interpolation before restarting or reversing it in repeat case.
        /// </summary>
        /// <param name="interval">Repeat interval in seconds</param>
        /// <returns>this</returns>
        Interpolable SetRepeatInterval(float interval);

        /// <summary>
        /// Ease function to use.
        /// </summary>
        /// <param name="easeType">Ease function</param>
        /// <returns>this</returns>
        Interpolable SetEase(EaseTypes easeType);

        /// <summary>
        /// Use Custom <a href="https://docs.unity3d.com/ScriptReference/AnimationCurve.html"> AnimationCurve </a>  as ease function.
        /// </summary>
        /// <param name="motion">the animation curve to use</param>
        /// <returns>this</returns>
        Interpolable SetEase(AnimationCurve motion);

        /// <summary>
        /// Apply the given options to interpolable.
        /// </summary>
        /// <param name="options">The options</param>
        /// <returns>this</returns>
        Interpolable SetOptions(InterpolationOptions options);

        /// <summary>
        /// Disables interpolable when Unity <a href="https://docs.unity3d.com/ScriptReference/Time-timeScale.html"> Time.timeScale </a> is set to <c>0</c>.
        /// </summary>
        /// <param name="flag">if set to <c>true</c> interpolable will be paused when the application is on pause</param>
        /// <returns>this</returns>
        Interpolable SetDisableOnPause(bool flag);

        /// <summary>
        /// Changes the duration
        /// </summary>
        /// <param name="value">new duration</param>
        /// <returns>this</returns>
        Interpolable SetNewDuration(float value);

        /// <summary>
        /// Disables the interpolable when the GameObject which is linked to it is inactive.
        /// </summary>
        /// <param name="flag">if set to <c>true</c> interpolable will be paused when the GameObject which is linked to it is inactive.</param>
        /// <returns>this</returns>
        Interpolable SetDisableOnHide(bool flag);

        /// <summary>
        /// Sets the rotation mode of interpolable in the case of Quaternion interpolation
        /// </summary>
        /// <param name="mode">The rotation mode</param>
        /// <returns></returns>
        Interpolable SetRotationMode(RotationModes mode);

        /// <summary>
        /// Attach a GameObject to interpolable.
        /// </summary>
        /// <param name="go">GameObject to attach</param>
        /// <returns>this</returns>
        Interpolable SetGameObject(GameObject go);

        /// <summary>
        /// Reverse the interpolable
        /// </summary>
        Interpolable Reverse();

        /// <summary>
        /// Starts the interpolation and return it
        /// </summary>
        /// <returns>The interpolable</returns>
        Interpolable Start();

        /// <summary>
        /// Terminates the interpolation
        /// </summary>
        Interpolable Terminate();

        /// <summary>
        /// Pauses the interpolation for the given seconds
        /// </summary>
        /// <param name="time">Pause time</param>
        Interpolable PauseFor(float time);

        /// <summary>
        /// Toggle the pause state.
        /// </summary>
        Interpolable TogglePause();

        /// <summary>
        /// Allow you to manually control the lifecycle of the interpolable (required when you assign the interpolable to an variable that you reuse)
        /// </summary>
        Interpolable SetCached();

        /// <summary>
        /// Add a callback action to do at the begining of the interpolation.
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
        ///     This code interpolate "a" value from 0 to 10 in 5 seconds and modify the end value of the interpolation when it starts.
        /// </example>
        /// <param name="action">Callback Action</param>
        /// <returns>this</returns>
        Interpolable OnStart(Action<Interpolable> action);

        /// <summary>
        /// Add a callback action to do when the interpolation is paused.
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
        ///     This code interpolate "a" value from 0 to 10 in 5 seconds and print "On Pause" when 'interpolation' is paused.
        /// </example>
        /// <param name="action">Callback Action</param>
        /// <returns>this</returns>
        Interpolable OnPause(Action<Interpolable> action);

        /// <summary>
        /// Add a callback action to do when the interpolation is updating.
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
        ///     This code interpolate "a" value from 0 to 10 in 5 seconds and stop the interpolation when it is completed at 50%.
        /// </example>
        /// <param name="action">Callback Action</param>
        /// <returns>this</returns>
        Interpolable OnUpdate(Action<Interpolable> action);

        /// <summary>
        /// Add a callback action to do when the interpolation reached  the end value.
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
        ///     This code interpolate "a" value from 0 to 10 in 5 seconds, then from 10 to 0 in 5 seconds.
        /// </example>
        /// <param name="action">Callback action to do</param>
        /// <returns>this</returns>
        Interpolable OnComplete(Action<Interpolable> action);

        /// <summary>
        /// Action to do when the interpolation is stopped.
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
        ///     This code interpolate "a" value from 0 to 10 in 5 seconds and print "On Terminate" when 'interpolation' is finished.
        /// </example>
        /// <param name="action">Callback Action</param>
        /// <returns>this</returns>
        Interpolable OnTerminate(Action<Interpolable> action);
    }
}
