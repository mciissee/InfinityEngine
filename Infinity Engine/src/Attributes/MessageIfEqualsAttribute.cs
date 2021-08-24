using System;
using System.Reflection;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to display a message when the decorated field is equals to a given value
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class MessageIfEqualsAttribute : MessageAttribute
    {
        private object expectedValue;

        /// <summary>
        /// Displays a message when the value of decorated field is equals to <paramref name="expectedValue" />
        /// </summary>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="message">The message to display</param>
        /// <param name="messageType">The type of the message</param>
        public MessageIfEqualsAttribute(object expectedValue, string message, MessageTypes messageType)
            : base(message, messageType)
        {
            this.expectedValue = expectedValue;
        }

        internal override bool HasProblem(object target, FieldInfo field)
        {
            return field.GetValue(target).Equals(expectedValue);
        }
    }
}
