using InfinityEngine.Utils;
using System;
using System.Reflection;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to display a message when the decorated field is equals to a given value
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class MessageIfEmptyAttribute : MessageAttribute
    {
        /// <summary>
        /// Displays a message when the value of decorated array of list field is empty.
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="messageType">The type of the message</param>
        public MessageIfEmptyAttribute(string message, MessageTypes messageType)
            : base(message, messageType)
        {
        }

        internal override bool HasProblem(object target, FieldInfo field)
        {
            object value = field.GetValue(target);
            if (value == null)
            {
                return false;
            }
            var type = value.GetType();
            if (ReflectionUtils.IsList(value))
            {
                base.Message = message;
                return ReflectionUtils.GetCachedProperty(type, "Count").GetValue(value, null).Equals(0);
            }
            if (type.IsArray)
            {
                base.Message = message;
                return ReflectionUtils.GetCachedProperty(type, "Length").GetValue(value, null).Equals(0);
            }
            base.Message = "The attribute MessageIfEmpty is valid only for List or Array field";
            return true;
        }
    }
}
