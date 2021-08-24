#pragma warning disable XS0001 // Find APIs marked as TODO in Mono

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace InfinityEngine.Utils
{
    /// <summary>
    /// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
    /// Provides a method for performing a deep copy of an object.
    /// Binary Serialization is used to perform the copy.
    /// </summary>
    public static class ObjectCopier
    {
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(source));
            }
            if (Equals(source, null))
            {
                return default(T);
            }
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0L, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
