
#pragma warning disable XS0001 // Find APIs marked as TODO in Mono
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UnityEngine;
using Component = UnityEngine.Component;

namespace InfinityEngine.Extensions
{
    /// <summary>
    ///  Extension methods
    /// </summary>
    public static class Extensions
    {
        private static HashSet<char> Alphabet = new HashSet<char>
        {
            'a',
            'b',
            'c',
            'd',
            'e',
            'f',
            'g',
            'h',
            'i',
            'j',
            'k',
            'l',
            'm',
            'n',
            'o',
            'p',
            'q',
            'r',
            's',
            't',
            'u',
            'v',
            'w',
            'x',
            'y',
            'z'
        };

        /// <summary>
        /// Changes the value of the boolean to an integer
        /// ( '0 or 1' 1 if the value of the boolean is <c>true</c> 0 otherwise)
        /// </summary>
        /// <param name="b">The boolean</param>
        /// <returns>1 if b != <c>false</c> 0 otherwise</returns>
        public static int ToInt(this bool b)
        {
            if (!b)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// Changes the value of the integer to a boolean. (<c>true</c> if the integer is different from 0 <c>false</c> otherwise)
        /// </summary>
        /// <param name="n">The integer</param>
        /// <returns><c>true</c> if n != 0</returns>
        public static bool ToBool(this int n)
        {
            return n != 0;
        }

        /// <summary>
        /// Return the logical negation of the given predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Func<T, bool> Not<T>(this Func<T, bool> predicate)
        {
            return (T x) => !predicate(x);
        }

        /// <summary>
        /// Return the logical negation of the given expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);
        }

        /// <summary>
        /// Cast the given IEnumerator to a  IEnumerable
        /// </summary>
        /// <param name="enumerator">this IEnumerator</param>
        /// <returns>this  IEnumerator casted to an IEnumerable</returns>
        public static IEnumerable ToEnumerable(this IEnumerator enumerator)
        {
            enumerator.MoveNext();
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Cast the given generic IEnumerator to a generic IEnumerable
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="enumerator">this IEnumerator</param>
        /// <returns>this generic IEnumerator casted to a generic IEnumerable</returns>
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            enumerator.MoveNext();
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Do an action for each key value pair of this dictionary
        /// </summary>
        /// <typeparam name="TKey">Key Type</typeparam>
        /// <typeparam name="TValue">Value Type</typeparam>
        /// <param name="dictionary">this dictionary</param>
        /// <param name="action">action to do</param>
        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            foreach (KeyValuePair<TKey, TValue> item in dictionary)
            {
                action(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Replaces the first ocurence of the given string in this string.
        /// </summary>
        /// <param name="Source">this string</param>
        /// <param name="Find">the string to replace</param>
        /// <param name="Replace">new value</param>
        /// <returns>this string</returns>
        public static string ReplaceFirst(this string Source, string Find, string Replace)
        {
            int startIndex = Source.IndexOf(Find, StringComparison.Ordinal);
            return Source.Remove(startIndex, Find.Length).Insert(startIndex, Replace);
        }

        /// <summary>
        /// Replaces the last ocurence of the given string in this string.
        /// </summary>
        /// <param name="Source">this string</param>
        /// <param name="Find">the string to replace</param>
        /// <param name="Replace">new value</param>
        /// <returns>this string</returns>
        public static string ReplaceLast(this string Source, string Find, string Replace)
        {
            int startIndex = Source.LastIndexOf(Find, StringComparison.Ordinal);
            return Source.Remove(startIndex, Find.Length).Insert(startIndex, Replace);
        }

        /// <summary>
        /// Checks if the char if in alphabet letters
        /// </summary>
        /// <param name="c">the char to check</param>
        /// <returns><c>true</c> if the char is in alphabet <c>false</c> otherwise</returns>
        public static bool IsInAlphabet(this char c)
        {
            return Alphabet.Contains(c.ToLower());
        }


        /// <summary>
        /// Returns all distinct elements of the given source, where "distinctness"
        /// is determined via a projection and the specified comparer for the projected type.
        /// </summary>
        /// <remarks>
        /// This operator uses deferred execution and streams the results, although
        /// a set of already-seen keys is retained. If a key is seen multiple times,
        /// only the first element with that key is returned.
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="keySelector">Projection for determining "distinctness"</param>
        /// <returns>A sequence consisting of distinct elements from the source sequence,
        /// comparing them by the specified key projection.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource item in source)
            {
                if (seenKeys.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Try to parse the given to an Enum of type 'T'
        /// </summary>
        /// <typeparam name="T">The type of the enum</typeparam>
        /// <param name="theEnum">The enum</param>
        /// <param name="returnValue">The result value</param>
        /// <returns><c>true</c> if the string is parsed <c>false</c> otherwise</returns>
        public static bool TryParse<T>(this Enum theEnum, out T returnValue)
        {
            returnValue = default(T);
            if (Enum.IsDefined(typeof(T), theEnum.ToString()))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                returnValue = (T)converter.ConvertFromString(theEnum.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a string by repeating <paramref name="n" /> times the string 
        /// </summary>
        /// <example>
        /// <code>
        /// Debug.Log("MyString", 3);
        /// </code>
        /// The example produces the following result:<br />
        /// MyStringMyStringMyString
        /// </example>
        /// <param name="s">The string</param>
        /// <param name="n">The number of repetition</param>
        /// <returns>New string</returns>
        public static string Repeat(this string s, int n)
        {
            return new StringBuilder().Insert(0, s, n).ToString();
        }

        /// <summary>
        /// Creates a string by repeating <paramref name="n" /> times the char 
        /// </summary>
        /// <example>
        /// <code>
        /// Debug.Log("a", 3);
        /// </code>
        /// The example produces the following result:<br />
        /// aaa
        /// </example>
        /// <param name="c">The char</param>
        /// <param name="n">The number of repetition</param>
        /// <returns>New string</returns>
        public static string Repeat(this char c, int n)
        {
            return new string(c, n);
        }

        /// <summary>
        /// Returns the upper version of the char
        /// </summary>
        /// <param name="c">The char</param>
        /// <returns>The upper version of the char</returns>
        public static char ToUpper(this char c)
        {
            return c.ToString().ToUpper()[0];
        }

        /// <summary>
        /// Returns the lower version of the char
        /// </summary>
        /// <param name="c">The char</param>
        /// <returns>The lower version of the char</returns>
        public static char ToLower(this char c)
        {
            return c.ToString().ToLower()[0];
        }

        /// <summary>
        /// Try to get the Component of the given type or add the component if it not exist
        /// </summary>
        /// <typeparam name="T">Component Type</typeparam>
        /// <param name="child">this Component</param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this Component child) where T : Component
        {
            T val = child.GetComponent<T>();
            if ((object)val == null)
            {
                val = child.gameObject.AddComponent<T>();
            }
            return val;
        }

        /// <summary>
        /// Try to get the Component of the given type or add the component if it not exist
        /// </summary>
        /// <typeparam name="T">Component Type</typeparam>
        /// <param name="gameObject">this GameObject</param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T val = gameObject.GetComponent<T>();
            if ((object)val == null)
            {
                val = gameObject.gameObject.AddComponent<T>();
            }
            return val;
        }

        /// <summary>
        /// Returns all GameObject childs of this GameObject
        /// </summary>
        /// <param name="gameObject">this GameObject</param>
        /// <returns>all child of this GameObject</returns>
        public static List<GameObject> GetChilds(this GameObject gameObject)
        {
            return (from Transform elem in gameObject.transform.GetEnumerator().ToEnumerable()
                    select elem.gameObject).ToList();
        }

        /// <summary>
        /// Returns all components of this GameObject
        /// </summary>
        /// <param name="gameObject">this GameObject</param>
        /// <returns>all Components of this GameObject</returns>
        public static Component[] GetAllComponents(this GameObject gameObject)
        {
            return gameObject.GetComponents<Component>();
        }

        /// <summary>
        /// Returns the Quaternion representation of this Vector4
        /// </summary>
        /// <param name="vect"></param>
        /// <returns>A Quaternion</returns>
        public static Quaternion ToQuaternion(this Vector4 vect)
        {
            return new Quaternion(vect.x, vect.y, vect.z, vect.w);
        }

        /// <summary>
        /// Returns the Vector4 representation of this Quaternion
        /// </summary>
        /// <param name="quaternion">this Quaternion</param>
        /// <returns>A Vector4</returns>
        public static Vector4 ToVector4(this Quaternion quaternion)
        {
            return new Vector4(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }
    }
}
