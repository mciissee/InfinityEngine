using InfinityEngine.Utils;
using System;
using System.Reflection;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to display a message box.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class MessageAttribute : PropertyAttribute
    {
        internal string message;

        /// <summary>
        /// The message to display
        /// </summary>
        public string Message
        {
            get;
            protected set;
        }

        /// <summary>
        /// The type of the message
        /// </summary>
        public MessageTypes MessageType
        {
            get;
            protected set;
        }

        /// <summary>
        /// The name of a function that returns a boolean indicating whether the message should be displayed
        /// </summary>
        public string MemberName
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the type of the reflected member
        /// </summary>
        public MemberTypes MemberType
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a value indicating whether the message is linked to a condition
        /// </summary>
        public bool HasCondition
        {
            get;
            protected set;
        }

        /// <summary>
        /// Creates new instance of <c>MessageAttribute</c>
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="messageType">The type of the member</param>
        public MessageAttribute(string message, MessageTypes messageType)
        {
            Message = message;
            this.message = message;
            MessageType = messageType;
        }

        /// <summary>
        /// Creates new instance of <c>MessageAttribute</c>
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="messageType">The type of the message</param>
        /// <param name="memberName"> The name of a function that returns a boolean indicating whether the message should be displayed</param>
        /// <param name="memberType">The type of the member</param>
        public MessageAttribute(string message, MessageTypes messageType, string memberName, MemberTypes memberType)
        {
            Message = message;
            this.message = message;
            MessageType = messageType;
            MemberName = memberName;
            MemberType = memberType;
            HasCondition = true;
        }

        internal bool HasProblem(object target, FieldInfo field, Func<MemberInfo, bool> condition)
        {
            if (!HasCondition)
            {
                return false;
            }
            Type type = target.GetType();
            if (!string.IsNullOrEmpty(MemberName))
            {
                switch (MemberType)
                {
                    case MemberTypes.Field:
                        {
                            FieldInfo cachedField = ReflectionUtils.GetCachedField(type, MemberName);
                            if ((object)field == null)
                            {
                                field = ReflectionUtils.GetCachedField(type.BaseType, MemberName);
                                if ((object)field == null)
                                {
                                    Message = $"The attribute {base.GetType().Name} of the field {cachedField.Name} refers to a missing field -> {MemberName} ";
                                    return true;
                                }
                            }
                            Message = message;
                            return condition(cachedField);
                        }
                    case MemberTypes.Property:
                        {
                            PropertyInfo cachedProperty = ReflectionUtils.GetCachedProperty(type, MemberName);
                            if ((object)cachedProperty == null)
                            {
                                cachedProperty = ReflectionUtils.GetCachedProperty(type.BaseType, MemberName);
                                if ((object)cachedProperty == null)
                                {
                                    Message = $"The attribute {base.GetType().Name} of the field {cachedProperty.Name} refers to a missing property -> {MemberName} ";
                                    return true;
                                }
                            }
                            Message = message;
                            return condition(cachedProperty);
                        }
                    case MemberTypes.Method:
                        {
                            MethodInfo cachedMethod = ReflectionUtils.GetCachedMethod(type, MemberName);
                            if ((object)cachedMethod == null)
                            {
                                cachedMethod = ReflectionUtils.GetCachedMethod(type.BaseType, MemberName);
                                if ((object)cachedMethod == null)
                                {
                                    Message = $"The attribute {base.GetType().Name} of the field {cachedMethod.Name} refers to a missing Method -> {MemberName} ";
                                    return true;
                                }
                            }
                            Message = message;
                            return condition(cachedMethod);
                        }
                }
            }
            return false;
        }

        internal virtual bool HasProblem(object target, FieldInfo field)
        {
            if (HasCondition)
            {
                switch (MemberType)
                {
                    case MemberTypes.Field:
                        return HasProblem(target, field, (MemberInfo info) => (bool)(info as FieldInfo).GetValue(target));
                    case MemberTypes.Property:
                        return HasProblem(target, field, (MemberInfo info) => (bool)(info as PropertyInfo).GetValue(target, null));
                    case MemberTypes.Method:
                        return HasProblem(target, field, (MemberInfo info) => (bool)(info as MethodInfo).Invoke(target, null));
                    default:
                        return false;
                }
            }
            return false;
        }

        internal static bool TryFindInvalidAttribute(object target, FieldInfo field, out MessageAttribute invalidAttribute)
        {
            Type type = target.GetType();
            MessageAttribute[] attributes = ReflectionUtils.GetAttributes<MessageAttribute>(field, inherit: true);
            invalidAttribute = null;
            MessageAttribute[] array = attributes;
            foreach (MessageAttribute messageAttribute in array)
            {
                if (messageAttribute.HasProblem(target, field))
                {
                    invalidAttribute = messageAttribute;
                    return true;
                }
            }
            return false;
        }
    }
}
