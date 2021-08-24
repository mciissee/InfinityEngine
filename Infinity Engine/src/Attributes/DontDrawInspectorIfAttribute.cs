using InfinityEngine.Utils;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to not draw the inspector of a monobehaviour if a given condion is true
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DontDrawInspectorIfAttribute : PropertyAttribute
    {
        /// <summary>
        /// Gets the name of the method to which returns a value indicating whether the inspector can be drawed
        /// </summary>
        public string MethodName
        {
            get;
            private set;
        }

        /// <summary>
        /// The message to show when the method return <c>false</c>
        /// </summary>
        public string Message
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the method with the name <see cref="P:InfinityEngine.Attributes.DontDrawInspectorIfAttribute.MethodName" /> is missing
        /// </summary>
        internal bool IsMissingFunction
        {
            get;
            private set;
        }

        /// <summary>
        /// Draws the inspector of the monobehaviour  only if the method with the name <paramref name="methodName" /> returns <c>false</c>
        /// </summary>
        /// <param name="methodName">The name of the method to which returns a value indicating whether the inspector can be drawed</param>
        /// <param name="message">The message to show when the method return <c>false</c></param>
        public DontDrawInspectorIfAttribute(string methodName, string message)
        {
            MethodName = methodName;
            Message = message;
        }

        internal static bool TryFindInvalidAttribute(object targetClass, out DontDrawInspectorIfAttribute invalidAttribute)
        {
            Type type = targetClass.GetType();
            DontDrawInspectorIfAttribute[] attributes = ReflectionUtils.GetAttributes<DontDrawInspectorIfAttribute>(type);
            invalidAttribute = null;
            DontDrawInspectorIfAttribute[] array = attributes;
            foreach (DontDrawInspectorIfAttribute dontDrawInspectorIfAttribute in array)
            {
                MethodInfo cachedMethod = ReflectionUtils.GetCachedMethod(type, dontDrawInspectorIfAttribute.MethodName);
                if ((object)cachedMethod == null)
                {
                    invalidAttribute = dontDrawInspectorIfAttribute;
                    invalidAttribute.IsMissingFunction = true;
                    return true;
                }
                if ((bool)cachedMethod.Invoke(targetClass, null))
                {
                    invalidAttribute = dontDrawInspectorIfAttribute;
                    return true;
                }
            }
            return false;
        }
    }
}
