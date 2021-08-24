using InfinityEngine.DesignPatterns;
using InfinityEngine.Interpolations;
using InfinityEngine.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using LogType = InfinityEngine.Utils.LogType;

//// <summary>  
//// The main namespace of the API.
//// </summary>
namespace InfinityEngine
{
    /// <summary>  
    ///     The core of <c>InfinityEngine API</c>.
    ///     This class is an singleton, you should not instantiate it or place it manually on Unity inspector inside the editor.<para> </para>
    ///     It uses the principe of facade design pattern to provides a static access to many features and plugins of the framework.
    /// </summary>
    public class Infinity : Singleton<Infinity>
    {
        /// <summary>
        /// The version of the api
        /// </summary>
        public const string Version = "1.2.0";

        private static PropertyInfo editorTimeSinceStartup;

        /// <summary>  
        /// Type of the logging. 
        /// </summary>
        public static LogType LoggingType;

        /// <summary>
        /// Callback invoked <b>ONLY</b> when you change the application scene thanks to <see cref="M:InfinityEngine.Infinity.LoadLevelAfterDelay(System.String,System.Single)" />.
        /// </summary>
        public static Action onSceneChanged;

        /// <summary>
        /// Delegate for EditorApplication.update
        /// </summary>
        public static Action editorUpdate;

        [SerializeField]
        private InterpolationManager mInterpolationManager;

        private bool isStarted;

        private bool isPaused;

        /// <summary>
        /// The log functions of the class <see cref="T:InfinityEngine.Utils.Debugger" /> works only if this value is set to <c>true</c>.<para> </para> 
        /// This value is always set to <c>false</c> when the application is not running on Unity Editor.
        /// and <c>true</c> by default when the application is running on Unity Editor.
        /// </summary> 
        public static bool EnableLog
        {
            get
            {
                if (PlayerPrefs.GetInt("ENABLE_LOG", 1) == 1)
                {
                    return Application.isEditor;
                }
                return false;
            }
            set => PlayerPrefs.SetInt("ENABLE_LOG", value ? 1 : 0);
        }

        /// <summary>
        /// Gets a value indicating wheter the application is paused (<c>Time</c>.time scale sets to).
        /// </summary>
        public static bool IsPaused => Instance.isPaused;

        /// <summary>
        ///  Gets the time the editor took in seconds to update.
        /// </summary>
        public static float EditorDeltaTime
        {
            get
            {
                var cachedProperty = ReflectionUtils.GetCachedProperty(ReflectionUtils.FindType("InfinityEditor.InfinityEditor"), "deltaTime");
                return (float)cachedProperty.GetValue(null, null);
            }
        }

        /// <summary>
        /// Gets the time since the editor was started (EditorApplication.timeSinceStartup)
        /// </summary>
        public static float EditorTimeSinceStartup
        {
            get
            {
                if (editorTimeSinceStartup == null)
                {
                    editorTimeSinceStartup = ReflectionUtils.GetCachedProperty(ReflectionUtils.FindType("UnityEditor.EditorApplication"), "timeSinceStartup");
                }
                return (float)(double)editorTimeSinceStartup.GetValue(null, null);
            }
        }

        private void Awake()
        {
            StartEngine();
        }

        /// <summary>
        /// Stops all coroutines and call the garbage collector.
        /// </summary>
        /// <remarks>
        /// When you call this function, all asynchronous methods (like <see cref="M:InfinityEngine.Infinity.For(System.Single,System.Action,System.Action)" />) that are currently invoked will be stopped.         
        /// </remarks>
        public static void Clear()
        {
            Instance.StopAllCoroutines();
            Instance.mInterpolationManager.Clear();
            GC.Collect();
        }

        internal static void StartEngine()
        {
            if (!Instance.isStarted)
            {
                onSceneChanged -= Clear;
                onSceneChanged += Clear;
                Instance.mInterpolationManager = new InterpolationManager();
                Clear();
                Instance.gameObject.name = $"<---[Infinity Engine] version {Version}--->";
                Instance.isPaused = false;
                Instance.isStarted = true;
            }
        }

        /// <summary>
        /// Interpolates smoothly the value of the variable of type <typeparamref name="T" /> between <paramref name="startValue" /> and 
        /// <paramref name="endValue" /> in <paramref name="duration" /> seconds thanks to <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /> class.
        /// </summary>
        ///  <example>    
        ///    The following code is an example of use of this method :    
        ///      <code>
        ///         using UnityEngine;
        ///         using UnityEngine.UI;
        ///         using InfinityEngine;
        ///
        ///         public class ExampleClass : MonoBehaviour {        
        ///
        ///             // reference to a Text object (must be specified in the inspector panel of the editor)
        ///             public Text label;
        ///             // reference to a Image object (must be specified in the inspector panel of the editor)
        ///             public Image progressBar;
        ///
        ///             void Start()
        ///             {
        ///                 Infinity.To(0.0f, 1.0f, 2, value =&gt; {
        ///                     // Change the text of the label during the interpolation 
        ///                     label.text = (int)(value * 100) + "%";
        ///                     // Change the fill amount of the progressBar image
        ///                     progressBar.fillAmount = value;
        ///
        ///                     }).Start(); // Starts the interpolation
        ///             }
        ///         }   
        ///     </code>
        ///      This code produces the following result : <br />
        ///      <img src="images/Infinity/Infinity.To_example.gif" />
        /// </example> 
        /// <remarks>
        ///  - You must call the method <see cref="M:InfinityEngine.Interpolations.Interpolable.Start" />  to launch the interpolation.<br />
        ///  - It is possible to add options to the interpolations thanks to the chained methods of <see cref="T:InfinityEngine.Interpolations.Interpolable" /> interface.<br />
        ///  - You cannot use any data type with this function, see <see cref="F:InfinityEngine.Interpolations.Interpolation`1.SupportedTypes" /> to know what data type interpolate thanks this function.<br />
        ///  - A lot of pre-maded interpolations are created for unity components and you can use them thanks to extension methods of <see cref="N:InfinityEngine.Extensions" /> namespace 
        ///   or thanks to the plugin <a href="http://u3d.as/GRf"> ISI Interpolation </a><br />. 
        /// </remarks>
        /// <exception cref="T:System.ArgumentException">
        ///     Throwed when the type of the generic argument <typeparamref name="T" /> is not in <see cref="F:InfinityEngine.Interpolations.Interpolation`1.SupportedTypes" />. 
        /// </exception>
        /// <typeparam name="T">The type of the data to interpolate.</typeparam>
        /// <param name="startValue">The starts value of the data.</param>
        /// <param name="endValue">The ends value of the data.</param>
        /// <param name="duration">The duration of the interpolation.</param>
        /// <param name="setter">The action to do with current value during the interpolation.</param>
        /// <returns>An object of type <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /></returns>
        public static Interpolation<T> To<T>(T startValue, T endValue, float duration, Action<T> setter)
        {
            if (Interpolation<T>.SupportedTypes.Contains(typeof(T)))
            {
                if (!Application.isPlaying)
                {
                    return new Interpolation<T>(startValue, endValue, duration, setter);
                }
                return Instance.mInterpolationManager.Pop(startValue, endValue, duration, setter);
            }
            throw new ArgumentException($"{typeof(T)} type cannot be interpolated by InfinityEngine !");
        }

        /// <summary>
        /// Interpolates smoothly the value of the variable of type <typeparamref name="T" /> between <paramref name="startValue" /> and 
        /// <paramref name="endValue" /> in <paramref name="duration" /> seconds thanks to <see cref="T:InfinityEngine.Interpolations.Interpolation`1" /> class.
        /// </summary>
        /// <remarks>
        ///  This code is the same as <see cref="M:InfinityEngine.Infinity.To``1(``0,``0,System.Single,System.Action{``0})" /> excepts that the parameters are not in the same order.
        /// </remarks>
        /// <exception cref="T:System.ArgumentException">
        ///     Throwed when the type of the generic argument <typeparamref name="T" /> is not in <see cref="F:InfinityEngine.Interpolations.Interpolation`1.SupportedTypes" />. 
        /// </exception>
        /// <typeparam name="T">The type of the data to interpolate.</typeparam>
        /// <param name="startValue">The starts value of the data.</param>
        /// <param name="setter">The action to do with current value during the interpolation.</param>
        /// <param name="endValue">The ends value of the data.</param>
        /// <param name="duration">The duration of the interpolation.</param>
        /// <returns>  An object of type <see cref="T:InfinityEngine.Interpolations.Interpolation`1" />.</returns>
        public static Interpolation<T> To<T>(T startValue, Action<T> setter, T endValue, float duration)
        {
            if (Interpolation<T>.SupportedTypes.Contains(typeof(T)))
            {
                if (!Application.isPlaying)
                {
                    return new Interpolation<T>(startValue, endValue, duration, setter);
                }
                return Instance.mInterpolationManager.Pop(startValue, endValue, duration, setter);
            }
            throw new ArgumentException($"{typeof(T)} type cannot be interpolated by InfinityEngine !");
        }

        internal static void StartInterpolation<T>(Interpolation<T> interpolation)
        {
            if (Interpolation<T>.SupportedTypes.Contains(typeof(T)))
            {
                if (Application.isPlaying)
                {
                    Instance.StartCoroutine(_StartInterpolation(interpolation));
                }
                else
                {
                    DOEditorCoroutine(_EditorStartInterpolation(interpolation));
                }
                return;
            }
            throw new ArgumentException($"{typeof(T)} type cannot be interpolated by InfinityEngine !");
        }

        internal static void RecycleInterpolation<T>(Interpolation<T> interpolation)
        {
            if (!interpolation.IsCached)
            {
                Instance.mInterpolationManager.Push(interpolation);
            }
        }

        private static IEnumerator _StartInterpolation<T>(Interpolation<T> interpolation)
        {
            var loopCount = 0;
            yield return new WaitForSecondsRealtime(interpolation.StartDelay);
            interpolation.StartCallback?.Invoke(interpolation);
            while (interpolation.RepeatCount == -1 || loopCount <= interpolation.RepeatCount)
            {
                var timer = 0f;
                while (timer < interpolation.Duration)
                {
                    if (interpolation.HasGameObject && interpolation.GameObject == null)
                    {
                        interpolation.Terminate();
                        break;
                    }
                    if (interpolation.IsTerminated)
                    {
                        break;
                    }
                    if (interpolation.CanBeInterpolated())
                    {
                        timer += ((!interpolation.IsDisableOnPause && Math.Abs(Time.deltaTime) < double.Epsilon)
                                  ? (Time.unscaledDeltaTime * interpolation.TimeScale)
                                  : (Time.deltaTime * interpolation.TimeScale));
                        interpolation.Interpolate(timer / interpolation.Duration);
                        interpolation.UpdateCallback?.Invoke(interpolation);
                    }
                    yield return null;
                }
                if (interpolation.IsTerminated)
                {
                    break;
                }
                if (interpolation.RepeatCount != -1)
                {
                    loopCount++;
                }
                if (interpolation.LoopType == LoopTypes.Reverse)
                {
                    interpolation.Reverse();
                }
                interpolation.CompleteCallback?.Invoke(interpolation);
                yield return new WaitForSecondsRealtime(interpolation.RepeatInterval);
            }
            interpolation.Terminate();
        }

        private static IEnumerator _EditorStartInterpolation<T>(Interpolation<T> interpolation)
        {
            interpolation.SetCached();
            var loopCount = 0;
            yield return new EditorWaitForSeconds(interpolation.StartDelay);
            interpolation.StartCallback?.Invoke(interpolation);
            while (interpolation.RepeatCount == -1 || loopCount <= interpolation.RepeatCount)
            {
                var timer = 0f;
                while (timer < interpolation.Duration)
                {
                    if (interpolation.HasGameObject && interpolation.GameObject == null)
                    {
                        interpolation.Terminate();
                        break;
                    }
                    if (interpolation.IsTerminated)
                    {
                        break;
                    }
                    if (interpolation.CanBeInterpolated())
                    {
                        timer += EditorDeltaTime * interpolation.TimeScale;
                        interpolation.Interpolate(timer / interpolation.Duration);
                        interpolation.UpdateCallback?.Invoke(interpolation);
                    }
                    yield return null;
                }
                if (interpolation.IsTerminated)
                {
                    break;
                }
                if (interpolation.RepeatCount != -1)
                {
                    loopCount++;
                }
                if (interpolation.LoopType == LoopTypes.Reverse)
                {
                    interpolation.Reverse();
                }
                interpolation.CompleteCallback?.Invoke(interpolation);
                yield return new EditorWaitForSeconds(interpolation.RepeatInterval);
            }
            interpolation?.Terminate();
        }

        /// <summary>
        /// Do asynchronously the given <paramref name="action" /> during '<paramref name="duration" />' seconds
        ///  using scaled time (the function does not run if the game is paused 'Time.deltaTime sets to 0'). 
        /// Unlike a conventional for loop, this program runs asynchronously and does not block the program.
        /// </summary>
        /// <example> 
        ///                 Example 1. <br />
        ///
        /// The following code is an example of use of this method without callback action : <br />
        /// <code>
        /// Infinity.For(5, ()=&gt;{
        ///     if(!Input.GetKey(KeyCode.Space))
        ///         Debug.Log("Please keep your finger in space key");
        /// });
        /// </code>
        ///
        /// This code checks during 5 seconds if user press on space key and print a message if the user don't press space key.<br />
        ///
        /// Example 2. <br />
        ///
        /// The following code is an example of use of this method with callback action : <br />
        ///             <code>
        /// Infinity.For(
        ///         duration:5,
        ///         action:()=&gt;{
        ///             if(!Input.GetKey(KeyCode.Space))
        ///                 Debug.Log("Please keep your finger in space key");
        ///         },
        ///         callback:()=&gt;{
        ///             Debug.Log("This is a callback message showed after 5 seconds");
        ///         }
        ///     );
        ///             </code>
        ///   This code do the same think that the first except that it show a callback message after 5 seconds.
        /// </example>
        /// <remarks>
        ///  The action is not executed only one time.
        /// </remarks>
        /// <param name="duration">The duration of the action</param>
        /// <param name="action">The action</param>
        /// <param name="callback">Optional callback action at the end</param>
        public static void For(float duration, Action action, Action callback = null)
        {
            Instance.StartCoroutine(Instance._DoFor(duration, action, callback));
        }

        /// <summary>
        /// Do asynchronously the given <paramref name="action" /> during '<paramref name="duration" />' seconds using unscaled time
        /// (the function run even if the game is paused 'Time.deltaTime sets to 0'). 
        /// Unlike a conventional for loop, this program runs asynchronously and does not block the program.
        /// </summary>
        /// <remarks>
        ///  The action is not executed only one time.
        /// </remarks>
        /// <param name="duration">The duration of the action</param>
        /// <param name="action">The action</param>
        /// <param name="callback">Optional callback action at the end</param>
        public static void ForRealTime(float duration, Action action, Action callback = null)
        {
            Instance.StartCoroutine(Instance._DoForRealTime(duration, action, callback));
        }

        private IEnumerator _DoFor(float time, Action action, Action callback)
        {
            var startTime = Time.time;
            while (Time.time < startTime + time)
            {
                action?.Invoke();
                yield return null;
            }
            callback?.Invoke();
        }

        private IEnumerator _DoForRealTime(float time, Action action, Action callback)
        {
            float startTime = Time.unscaledTime;
            while (Time.unscaledTime < startTime + time)
            {
                action?.Invoke();
                yield return null;
            }
            callback?.Invoke();
        }

        /// <summary>
        /// Do the given <paramref name="action" /> after '<paramref name="delay" />' seconds
        /// using scaled time (the function does not run if the game is paused 'Time.deltaTime sets to 0'). 
        /// </summary>
        ///             <example>  
        ///             The following code is an example of use of this method : 
        ///             <code>
        ///    Infinity.After(5, ()=&gt;{
        ///        Debug.Log("This is a message showed after 5 seconds");
        ///    });
        ///             </code>
        ///             This code shows the message 'This is a message showed after 5 seconds' after 5 seconds.
        ///             </example>
        /// <remarks>
        /// You can use this method in a single line like : 
        /// <code> 
        ///     Infinity.After(5 ,() =&gt; Debug.Log("This is a message showed after 5 seconds") ); 
        /// </code>
        /// </remarks>
        /// <param name="delay">Action delay</param>
        /// <param name="action">Action to do after the delay</param>
        public static void After(float delay, Action action)
        {
            Instance.StartCoroutine(Instance._DoAfter(delay, action));
        }

        /// <summary>
        /// Do the given <paramref name="action" /> after '<paramref name="delay" />' seconds 
        /// using unscaled time (the function run even if the game is paused 'Time.deltaTime sets to 0'). 
        /// </summary>
        /// <param name="delay">Action delay</param>
        /// <param name="action">Action to do after the delay</param>
        public static void AfterRealTime(float delay, Action action)
        {
            Instance.StartCoroutine(Instance._DoAfterRealTime(delay, action));
        }

        private IEnumerator _DoAfter(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

        private IEnumerator _DoAfterRealTime(float delay, Action action)
        {
            yield return new WaitForSecondsRealtime(delay);
            action();
        }

        /// <summary>
        /// Do asynchronously the given <paramref name="action" /> while the given <paramref name="predicate" /> is <c>true</c>.
        /// Unlike a conventional while loop, this program runs asynchronously and does not block the program.
        /// </summary>
        /// <example>
        ///     The following code is an example of use of this method : 
        ///     <code>    
        ///         Infinity.While(
        ///             predicate:()=&gt;{ return !Input.GetKey(KeyCode.Space); }, 
        ///             action:()=&gt;{
        ///                 Debug.Log("Please press on space key to exit");
        ///             }, 
        ///             callback:()=&gt;{
        ///                  Application.Quit();
        ///             });
        ///     </code>
        /// This code displays "Please press on space key to exit" while the space key is not pressed and quit the application when the key is pressed.
        /// </example>
        /// <remarks>
        ///  You can use this method in a single line like : 
        /// <code> 
        ///     Infinity.While(() =&gt; !Input.GetKey(KeyCode.Space) ,() =&gt; Debug.Log("Please press on space key to exit"), Application.Quit ); 
        /// </code>
        /// Like <see cref="M:InfinityEngine.Infinity.For(System.Single,System.Action,System.Action)" /> method, you can use a <paramref name="callback" /> action after the end of this method.
        /// </remarks>
        /// <param name="predicate">The given predicate</param>
        /// <param name="action">Action to do</param>
        /// <param name="callback">Optional callback action</param>
        public static void While(Func<bool> predicate, Action action, Action callback = null)
        {
            Instance.StartCoroutine(Instance._DoWhile(predicate, action, callback));
        }

        private IEnumerator _DoWhile(Func<bool> predicate, Action action, Action callback)
        {
            while (predicate())
            {
                action?.Invoke();
                yield return new WaitForFixedUpdate();
            }
            callback?.Invoke();
        }

        /// <summary>
        /// Checks asynchronously when the given <paramref name="predicate" /> is <c>true</c>. an do the given <paramref name="action" /> when the predicate is <c>true</c>.
        /// </summary>
        /// <example>
        ///     The following code is an example of use of this method : 
        ///     <code>    
        ///         Infinity.When(
        ///             predicate:()=&gt;{ return Input.GetKey(KeyCode.Space); }, 
        ///             action:()=&gt;{
        ///                 Debug.Log("The game is started");
        ///             });
        ///     </code>
        /// This code displays "The game is started" when the space key is pressed.
        /// <remarks>
        /// You can use this method in a single line like : 
        /// <code> 
        ///     Infinity.When(() =&gt; Input.GetKey(KeyCode.Space) ,() =&gt; Debug.Log("The game is started") ); 
        /// </code>
        /// </remarks>
        ///             </example>
        /// <param name="predicate">The given predicate</param>
        /// <param name="action">Action to do</param>
        public static void When(Func<bool> predicate, Action action)
        {
            Instance.StartCoroutine(Instance._DoWhen(predicate, action));
        }

        private IEnumerator _DoWhen(Func<bool> predicate, Action action)
        {
            yield return new WaitUntil(predicate);
            action?.Invoke();
        }

        /// <summary>
        /// Makes a slow motion effect by changing the time scale of the application.
        /// </summary>
        /// <param name="duration">Slow motion duration</param>
        /// <param name="scaleFactor">Time scale will be divided by this value (2 by default)</param>
        /// <param name="callback">Optional callback action</param>
        public static void SlowMotion(float duration, int scaleFactor = 2, Action callback = null)
        {
            if (scaleFactor > 0)
            {
                For(duration / scaleFactor, () =>
                {
                    Time.timeScale = (1f / scaleFactor);
                    Time.fixedDeltaTime = (0.02f * Time.timeScale);
                }, () =>
                {
                    Time.timeScale = (1f);
                    Time.fixedDeltaTime = (0.02f);
                    callback?.Invoke();
                });
            }
        }

        /// <summary>
        /// Returns a random color
        /// </summary>
        /// <returns>Random color</returns>
        public static Color RandomColor()
        {
            return new Color(Random.value,
                             Random.value,
                             Random.value,
                             1f);
        }

        /// <summary>
        ///  Returns a random color
        /// </summary>
        /// <param name="maxR">Max random value for red in the range [0, 1] </param>
        /// <param name="maxG">Max random value for green in the range [0, 1]</param>
        /// <param name="maxB">Max random value for blue in the range [0, 1]</param>
        /// <param name="minA">Max random value for alpha in the range [0, 1]</param>
        /// <returns>Random Color</returns>
        public static Color RandomColor(float maxR, float maxG, float maxB, float minA = 1f)
        {
            return new Color(Random.Range(0f, maxR), Random.Range(0f, maxG), Random.Range(0f, maxB), Random.Range(minA, 1f));
        }

        /// <summary>
        /// Generates a random number in the range [min, max] different from the given <paramref name="prohibedValue" />.
        /// </summary>
        /// <example>
        ///     The following code is an example of use of this method : 
        ///     <code>
        ///         var random Infinity.RandomNumberLessValue(0, 10, 4);
        ///         Debug.Log(random);
        ///     </code>
        ///     This code print a random number in the range [0, 10] different from 4
        /// </example>
        /// <remarks>
        /// This code return 0 if <paramref name="min" /> == <paramref name="max" /> == <paramref name="prohibedValue" />
        /// </remarks>
        /// <param name="min">min value (inclusive)</param>
        /// <param name="max">max value (inclusive)</param>
        /// <param name="prohibedValue">prohihed value to generate</param>
        /// <returns>random  number different from the given prohibed value. </returns>
        public static int RandomNumberLessValue(int min, int max, int prohibedValue)
        {
            if (min == max && min == prohibedValue)
            {
                return 0;
            }
            int num;
            for (num = Random.Range(min, max + 1); num == prohibedValue; num = Random.Range(min, max + 1))
            {
            }
            return num;
        }

        /// <summary>
        /// Generates a random number in the range [min, max] different from the values that are in the given <paramref name="prohibedValues" /> list.
        /// </summary>
        /// <example>
        ///     The following code is an example of use of this method : 
        ///     <code>
        ///         var random = Infinity.RandomNumberLessValues(0, 10, new List&lt;int&gt;(){1,3,5,7,9});
        ///         Debug.Log(random);
        ///     </code>
        ///     This code print a random number that is even between [0, 10]
        /// </example>
        /// <remarks>
        /// This code return 0 if <paramref name="min" /> == <paramref name="max" /> or if all number in the range [min, max] are in <paramref name="prohibedValues" /> list.
        /// </remarks>
        /// <param name="min">min value (inclusive)</param>
        /// <param name="max">max value (inclusive)</param>
        /// <param name="prohibedValues">list of prohibed values</param>
        /// <returns>random number different from the given prohibed values. </returns>
        public static int RandomNumberLessValues(int min, int max, List<int> prohibedValues)
        {
            if (min == max || min > max)
            {
                return 0;
            }
            var flag = false;
            for (int i = min; i <= max; i++)
            {
                if (!prohibedValues.Contains(i))
                {
                    flag = true;
                    break;
                }
            }
            var num = 0;
            if (flag)
            {
                num = Random.Range(min, max + 1);
                while (prohibedValues.Contains(num))
                {
                    num = Random.Range(min, max + 1);
                }
            }
            else
            {
                Utils.Debugger.LogError("The prohibed values list contains all number in the given range.");
            }
            return num;
        }

        /// <summary>
        /// Starts the given coroutine
        /// </summary>
        /// <param name="arg">Coroutine to start</param>
        public static void DOCoroutine(IEnumerator arg)
        {
            Instance.StartCoroutine(arg);
        }

        /// <summary>
        /// Starts the given coroutine in editor mode
        /// </summary>
        /// <param name="arg">Coroutine to start</param>
        public static EditorCoroutine DOEditorCoroutine(IEnumerator arg)
        {
            return EditorCoroutine.Start(arg);
        }

        /// <summary>
        /// Creates new Color from the given hexadecimal code
        /// </summary>
        /// <param name="hex">The hexadecimal code (FFFFFF or #FFFFFF)</param>
        /// <returns>New <c>Color</c> object from the given hexadecimal code if the hexadecimal code is valid <c>Color</c>.black otherwise.</returns>
        public static Color HexToColor(string hex)
        {
            hex = hex.Replace("#", "");
            try
            {
                var b = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                var b2 = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                var b3 = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                return new Color32(b, b2, b3, byte.MaxValue);
            }
            catch (Exception ex)
            {
                Utils.Debugger.LogError($"{hex} is not a hexadecimal color {ex.StackTrace}", Instance.gameObject);
                return Color.black;
            }
        }

        /// <summary>
        /// Checks if the application is on pause.
        /// </summary>
        /// <remarks>
        /// This method checks if the value of <c>Time.timeScale</c>  is set to 0:
        /// </remarks>
        /// <returns><c>true</c> if <c>Time.timeScale</c> is set to 0 <c>false</c> otherwise.</returns>
        public static bool IsOnPause()
        {
            if (!Instance.isPaused)
            {
                return Math.Abs(Time.timeScale) < double.Epsilon;
            }
            return true;
        }

        /// <summary>
        /// Loads the level in parameter after <paramref name="delay" /> seconds
        /// </summary>
        /// <param name="level">the name of the level to load</param>
        /// <param name="delay">loading delay</param>
        public static void LoadLevelAfterDelay(string level, float delay)
        {
            Time.timeScale = 1f;
            Instance.StartCoroutine(_LoadLevelAfterDelays(level, delay));
        }

        private static IEnumerator _LoadLevelAfterDelays(string level, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            Clear();
            SceneManager.LoadScene(level);
            onSceneChanged?.Invoke();
        }

        /// <summary>
        ///  Adds an event listener to <paramref name="pauseBtn" />.<para> </para>
        ///  When the button is clicked, the function invokes the delegate <paramref name="onPauseIn" /> if <see cref="P:InfinityEngine.Infinity.IsPaused" /> return <c>true</c><para> </para>
        ///  otherwise it invokes the delegate <paramref name="onPauseOut" />.
        /// </summary>
        /// <param name="pauseBtn">The button which will pause the game</param>
        /// <param name="onPauseIn">Action to do when the application is on pause</param>
        /// <param name="onPauseOut">Action to do when the pause finish</param>
        public static void AddPauseListener(Button pauseBtn, Action onPauseIn, Action onPauseOut)
        {
            pauseBtn.onClick.AddListener(() =>
            {
                if (IsPaused)
                    onPauseIn?.Invoke();
                else
                    onPauseOut?.Invoke();
            });
        }

        /// <summary>
        ///  Adds an event listener to <paramref name="pauseBtn" /> and <paramref name="resumeBtn" />.<para> </para>
        ///  When the <paramref name="pauseBtn" /> is clicked, the function invokes the delegate <paramref name="onPauseIn" />.<para> </para>
        ///  When the <paramref name="resumeBtn" /> is clicked,it invokes the delegate <paramref name="onPauseOut" />.
        /// </summary>
        /// <param name="pauseBtn">Pause button</param>
        /// <param name="resumeBtn">Resume button</param>
        /// <param name="onPauseIn">Action to do when the application is on pause</param>
        /// <param name="onPauseOut">Action to do when the pause finish</param>
        public static void AddPauseListener(Button pauseBtn, Button resumeBtn, Action onPauseIn, Action onPauseOut)
        {
            pauseBtn.onClick.AddListener(() => onPauseIn?.Invoke());
            resumeBtn.onClick.AddListener(() => onPauseOut?.Invoke());
        }

        /// <summary>
        /// Do the given <paramref name="task" /> <paramref name="iteration" /> times and return the time taken by the tasks.
        /// </summary>
        /// <param name="iteration">The number of time to do the task </param>
        /// <param name="task">The task</param>
        /// <returns>The time taken by the task</returns>
        public static float TestPerformance(int iteration, Action task)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < iteration; i++)
            {
                task();
            }
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
