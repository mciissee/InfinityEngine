/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

//// <summary>
//// This namespace provides access to extension methods for unity engines components and c# language.
//// Some of these extension methods are usable without coding in unity inspector thanks to the reflection.
//// </summary>
namespace InfinityEngine.Extensions
{
    /// <summary>
    /// Extension methods for array object
    /// </summary>
    public static class ArrayExtensions
    {
        #region Array

        /// <summary>
        /// Adds the given element in this array.
        /// </summary>
        /// <example>
        ///     Example :
        ///     <code>
        ///     var array = new int[]{1, 2, 3};
        ///     array.Add(4);
        ///     array.Log();
        ///     </code>
        ///     This code produces the following result :
        ///     1 2 3 4
        /// </example>
        /// <typeparam name="TSource">Type of the elements of this array</typeparam>
        /// <param name="array">This array</param>
        /// <param name="elem">Element to add</param>
        /// <returns>This array</returns>
        public static TSource[] Add<TSource>(this TSource[] array, TSource elem)
        {
            var count = array.Length;
            Array.Resize(ref array, array.Length + 1);
            array[count] = elem;
            return array;
        }

        /// <summary>
        /// Adds the given element into this array if it is not in the array.
        /// </summary>
        /// <example>
        ///     Example :
        ///     <code>
        ///         var array = new int[]{1,2,3,4,5};
        ///         array.AddIFNot(5);
        ///         array.AddIFNot(1);
        ///         array.AddIFNot(6);
        ///         array.Log();
        ///     </code>
        ///     This code produces the following result : 
        ///   
        ///     1
        ///     
        ///     2
        ///     
        ///     3
        ///     
        ///     4
        ///     
        ///     5
        ///     
        ///     6
        /// </example>
        /// <typeparam name="TSource">Type of the elements of this array</typeparam>
        /// <param name="array">This array</param>
        /// <param name="element">Element to add</param>
        /// <returns>this array</returns>
        public static TSource[] AddIFNot<TSource>(this TSource[] array, TSource element)
        {
            if (!array.Contains(element))
                array.Add(element);

            return array;
        }


        /// <summary>
        /// Apply the same action for each element of this array.
        /// </summary>
        /// <example>
        ///     Exemple :  
        ///     <code>
        ///     var array = new int[]{1,2,3,4,5};
        ///     array.ForEach(
        ///         action:(element) => {
        ///             Debug.Log(element * 2);
        ///         },
        ///         callback:()=>{
        ///             Debug.Log("Finish");
        ///         }
        ///     </code>
        ///     This code produces the following result : 
        ///     
        ///     2
        ///     
        ///     4
        ///     
        ///     6
        ///     
        ///     8
        ///     
        ///     10 
        ///     
        ///     Finish
        /// </example>
        /// <typeparam name="TSource">Type of the elements of this array</typeparam>
        /// <param name="array">This array</param>
        /// <param name="action">Action to do with each elements</param>
        /// <param name="callback">Optional callback action at the end of the processus</param>
        public static void ForEach<TSource>(this TSource[] array, Action<TSource> action, Action callback = null)
        {
            Array.ForEach(array, action);
            if (callback != null)
                callback.Invoke();
        }

        /// <summary>
        /// Apply the same action for each element of this array.
        /// </summary>
        /// <example>
        ///     Exemple :  
        ///     <code>
        ///     var array = new int[]{1,2,3,4,5};
        ///     array.ForEach((elem, index) => array[index] *= 2);
        ///     array.Log();
        ///     </code>
        ///     This code produces the following result : 
        ///     
        ///     2
        ///     
        ///     4
        ///     
        ///     6
        ///     
        ///     8
        ///     
        ///     10 
        /// </example>
        /// <typeparam name="TSource">Type of the elements of this array</typeparam>
        /// <param name="array">This array</param>
        /// <param name="action">Action to do with each elements</param>
        /// <param name="callback">Optional callback action at the end of the processus</param>
        public static void ForEach<TSource>(this TSource[] array, Action<TSource, int> action, Action callback = null)
        {
            var enumerator = array.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                action((TSource)enumerator.Current, index);
                index++;
            }
            if (callback != null)
                callback.Invoke();
        }

        /// <summary>
        /// Find the index of an element in a array
        /// </summary>
        /// <example>
        ///     Example :
        ///     <code>
        ///     var array = new array[]{5, 1, 64, 7, 8, -1};
        ///     Debug.Log(array.IndexOf(-1));
        ///     </code>
        ///     This code produces the following result : 5
        /// </example>
        /// <remarks>
        /// This function return the index of the <c>first</c> element. So in the case {1,2,2,3} this function return 1
        /// </remarks>
        /// <typeparam name="TSource">Type of the elements in the array</typeparam>
        /// <param name="array">This array reference</param>
        /// <param name="elem">Element to find</param>
        /// <returns>Index of the element in the array</returns>
        public static int IndexOf<TSource>(this TSource[] array, TSource elem)
        {
            return Array.IndexOf(array, elem);
        }

        /// <summary>
        /// Find the last element of this array with the given predicate
        /// </summary>
        /// <example>
        ///     Exemple :    
        ///     <code>
        ///     var array = new int[]{1,2,4,2,5,2,8};
        ///     Debug.Log(array.Last(elem => elem &lt; 2));
        ///     </code>
        ///     This code produces the following result : 1
        ///     
        /// </example>
        /// <typeparam name="TSource">Type of the elements in the array</typeparam>
        /// <param name="array">This array reference</param>
        /// <param name="predicate">The given predicate</param>
        /// <returns>The last element with the given predicate</returns>
        public static TSource Last<TSource>(this TSource[] array, Func<TSource, bool> predicate)
        {
            return Array.FindLast(array, elem => predicate(elem));
        }

        /// <summary>
        /// Find the last element of this array
        /// </summary>
        /// <example>
        ///     <code>
        ///     var array = new array[]{1,2,3,4,5};
        ///     Debug.Log(array.Last());
        ///     </code>
        ///     This code produces the following result : 5
        /// </example>
        /// <typeparam name="TSource">Type of the elements in the array</typeparam>
        /// <param name="array">This array reference</param>
        /// <returns>The last element of this array</returns>
        public static TSource Last<TSource>(this TSource[] array)
        {
            if (array.Length == 0)
                return default(TSource);

            return array[array.Length - 1];
        }

        /// <summary>
        ///  Find the last index of the given element in this array
        /// </summary>
        /// <example>
        ///     Example :
        ///     <code>
        ///     var array = new int[]{1, 2, 1, 3, 4, 2, 5, 2, 8};
        ///     Debug.Log(array.LastIndexOf(2));
        ///     </code>
        ///     This code produces the following result : 7
        /// </example>
        /// <typeparam name="TSource">Type of the elements in this array</typeparam>
        /// <param name="array">This array</param>
        /// <param name="elem">The element to find</param>
        /// <returns>Index of the element in the array</returns>
        public static int LastIndexOf<TSource>(this TSource[] array, TSource elem)
        {
            return Array.LastIndexOf(array, elem);
        }

        /// <summary>
        /// Show all elements of this array on the console
        /// </summary>
        /// <example>
        ///     <code>
        ///     var array = new int[]{1,2,3};
        ///     array.Log();
        ///     </code>
        ///     This code produces the following result :
        ///     
        ///     1
        ///     
        ///     2
        ///     
        ///     3
        /// </example>
        /// <typeparam name="TSource">Type of the elements in this array</typeparam>
        /// <param name="array">this array</param>
        public static void Log<TSource>(this TSource[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Debug.Log(array[i]);
            }
        }

        /// <summary>
        /// Return a random element of this array
        /// </summary>
        /// <typeparam name="TSource">Type of the elements in the array</typeparam>
        /// <param name="array">This array reference</param>
        /// <returns>random element of the array</returns>
        public static TSource Random<TSource>(this TSource[] array)
        {
            var rand = new System.Random();
            var index = rand.Next(0, array.Length);
            return array[index];
        }

        /// <summary>
        /// Remove the element at index in this array
        /// </summary>
        /// <example>
        ///     <code>
        ///     var array = new int[]{1,2,3};
        ///     array.RemoveAt(1);
        ///     array.Log();
        ///     </code>
        ///     This code produces the following result :
        ///     
        ///     1
        ///     
        ///     3
        /// </example>
        /// <typeparam name="TSource">Type of the elements of this array</typeparam>
        /// <param name="array">This Array</param>
        /// <param name="index">Index to remove</param>
        /// <returns>This Array</returns>
        public static TSource[] RemoveAt<TSource>(this TSource[] array, int index)
        {
            for (var i = index; i < array.Length - 1; i++)
            {
                // moving elements downwards, to fill the gap at [index]
                array[i] = array[i + 1];
            }
            // finally, let's decrement Array's size by one
            Array.Resize(ref array, array.Length - 1);
            return array;
        }

        /// <summary>
        /// Remove the given element from this array
        /// </summary>
        /// <example>
        ///     <code>
        ///     var array = new int[]{1,2,3};
        ///     array.Remove(2);
        ///     array.Log();
        ///     </code>
        ///     This code produces the following result :
        ///     
        ///     1
        ///     
        ///     3
        /// </example>
        /// <typeparam name="TSource">Type of the elements in this array</typeparam>
        /// <param name="array">This array</param>
        /// <param name="elem">The element to remove</param>
        /// <returns>This Array</returns>
        public static TSource[] Remove<TSource>(this TSource[] array, TSource elem)
        {
            if (array.Contains(elem))
                array.RemoveAt(array.IndexOf(elem));

            return array;
        }

        /// <summary>
        /// Shuffle this array
        /// </summary>
        /// <typeparam name="TSource">Type of the elements in the array</typeparam>
        /// <param name="array">This array reference</param>
        /// <returns>this array</returns>
        public static TSource[] Shuffle<TSource>(this TSource[] array)
        {
            var j = 0;
            var count = array.Length;
            for (var i = 0; i < count; i++)
            {
                j = UnityEngine.Random.Range(i, count);
                TSource tmp = array[i];
                array[i] = array[j];
                array[j] = tmp;
            }
            return array;
        }

        /// <summary> 
        /// Shift the array.
        /// </summary>
        /// <example>
        /// Exemple :
        ///     <code>
        ///         var array = new int[]{1, 2, 3, 4}
        ///         array.Shift();
        ///         array.Log();
        ///     </code>
        ///     This code produces the following result :
        ///     2
        ///     
        ///     3
        ///     
        ///     4
        ///     
        ///     1
        /// </example>
        /// <typeparam name="TSource">Type of the elements in the array</typeparam>
        /// <param name="array">This array reference</param>
        /// <returns>this array</returns>
        public static TSource[] Shift<TSource>(this TSource[] array)
        {
            TSource tmp = array[0];
            var count = array.Length;
            for (int i = 1; i < count; i++)
            {
                array[i - 1] = array[i];
            }
            array[count - 1] = tmp;
            return array;
        }

        public static HashSet<TSource> ToHashSet<TSource>(this TSource[] array)
        {
            var hash = new HashSet<TSource>();
            foreach (var it in array)
                hash.Add(it);

            return hash;
        }
        #endregion Array

    }
}