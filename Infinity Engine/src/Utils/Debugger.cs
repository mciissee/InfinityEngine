/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using UnityEngine;

//// <summary>
//// This namespace Provides access to useful classes.
//// </summary>
namespace InfinityEngine.Utils
{
    /// <summary>  
    /// Values that represent the log type of the class <see cref="Utils.Debugger"/>. 
    /// </summary>
    public enum LogType
    {
        /// <summary>Errors and warnings</summary>
        Default,
        /// <summary>Only errors</summary>
        ErrorsOnly,
        /// <summary>Errors, warnings, and additional informations</summary>
        Verbose,
    }

    ///<summary>
    /// Log informations in editor console only if the current running platform is Unity Editor
    /// </summary>
    public static class Debugger
    {

        ///<summary>
        /// Log the given message if the current platform is Unity Editor and <see cref="Infinity.EnableLog"/> 
        /// is set to <c>true</c> and <see cref="Infinity.LoggingType"/> != <see cref="LogType.Verbose"/>
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Log(object message)
        {
            if (Infinity.LoggingType != LogType.Verbose && Infinity.EnableLog)
                Debug.Log(message);
        }

        /// <summary>
        /// Log the given message if the current platform is Unity Editor and <see cref="Infinity.EnableLog"/> 
        /// is set to <c>true</c> and <see cref="Infinity.LoggingType"/> != <see cref="LogType.Verbose"/>
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="context">Target Object if the log message is clicked on Unity Editor, the context Object will be focused</param>
        public static void Log(object message, Object context)
        {
            if (Infinity.LoggingType != LogType.Verbose && Infinity.EnableLog)
                Debug.Log(message, context);
        }

        /// <summary>
        /// Log the given message in warning format if the current platform is Unity Editor and <see cref="Infinity.EnableLog"/> 
        /// is set to <c>true</c> and <see cref="Infinity.LoggingType"/> != <see cref="LogType.ErrorsOnly"/>
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void LogWarning(object message)
        {
            if (Infinity.LoggingType != LogType.ErrorsOnly && Infinity.EnableLog)
                Debug.LogWarning(message);
        }

        /// <summary>
        /// Log the given message in warning format if the current platform is Unity Editor and <see cref="Infinity.EnableLog"/> 
        /// is set to <c>true</c> and <see cref="Infinity.LoggingType"/> != <see cref="LogType.ErrorsOnly"/>
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="context">Target Object if the log message is clicked on Unity Editor, the context Object will be focused</param>
        public static void LogWarning(object message, Object context)
        {
            if (Infinity.LoggingType != LogType.ErrorsOnly && Infinity.EnableLog)
                Debug.LogWarning(message, context);
        }

        /// <summary>
        /// Log the given message in error format if the current platform is Unity Editor and <see cref="Infinity.EnableLog"/> 
        /// is set to <c>true</c>
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void LogError(object message)
        {
            if (Infinity.EnableLog)
                Debug.LogError(message);
        }

        /// <summary>
        /// Log the given message in error format if the current platform is Unity Editor and <see cref="Infinity.EnableLog"/> 
        /// is set to <c>true</c>
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="context">Target Object if the log message is clicked on Unity Editor, the context Object will be focused</param>
        public static void LogError(object message, Object context)
        {
            if (Infinity.EnableLog)
                Debug.LogError(message, context);
        }

    }

}