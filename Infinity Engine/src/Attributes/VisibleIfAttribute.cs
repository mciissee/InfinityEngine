using InfinityEngine.Utils;
using System;
using System.Reflection;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to indicates that a field should be drawed only if a given condition is <c>true</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class VisibleIfAttribute : PropertyAttribute
    {
        /// <summary>
        /// Gets the name of the member which indicates whether the field decorated by the attribute should be visible.
        /// </summary>
        public string MemberName
        {
            get;
            private set;
        }

        /// <summary>
        ///  Gets the type of the member which indicates whether the field decorated by the attribute should be visible.
        /// </summary>
        public MemberTypes MemberType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the field should be visible only when the negation of the condition is true.
        /// </summary>
        public bool Negate
        {
            get;
            private set;
        }

        /// <summary>
        /// Display the field in the inspector only if the value member with the given name is <c>true</c>
        /// </summary>
        /// <param name="memberName">The name of the method</param>
        /// <param name="memberType">
        /// Gets the type of the member which indicates whether the field decorated by the attribute should be visible.
        /// </param>
        /// <param name="negate">A value indicating whether the field should be visible only when the negation of the condition is true.</param>
        public VisibleIfAttribute(string memberName, MemberTypes memberType, bool negate = false)
        {
            MemberName = memberName;
            MemberType = memberType;
            Negate = negate;
        }

        internal bool TryGetReferencedMember(object target, out MemberInfo member)
        {
            member = null;
            switch (MemberType)
            {
                case MemberTypes.Field:
                    member = ReflectionUtils.GetCachedField(target.GetType(), MemberName);
                    break;
                case MemberTypes.Method:
                    member = ReflectionUtils.GetCachedMethod(target.GetType(), MemberName);
                    break;
                case MemberTypes.Property:
                    member = ReflectionUtils.GetCachedProperty(target.GetType(), MemberName);
                    break;
            }
            return (object)member != null;
        }

        internal static bool TryFindInvalidAttribute(object target, FieldInfo field, out VisibleIfAttribute invalidAttribute)
        {
            Type type = target.GetType();
            VisibleIfAttribute[] attributes = ReflectionUtils.GetAttributes<VisibleIfAttribute>(field, inherit: true);
            invalidAttribute = null;
            VisibleIfAttribute[] array = attributes;
            foreach (VisibleIfAttribute visibleIfAttribute in array)
            {
                if (!visibleIfAttribute.TryGetReferencedMember(target, out MemberInfo _))
                {
                    invalidAttribute = visibleIfAttribute;
                    return true;
                }
            }
            return false;
        }
    }
}
