/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InfinityEngine.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="List{T}"/>
    /// </summary>
    public static class ListExtensions
    {

        /// <summary>
        /// Adds the given element into this list if it is not in the list.
        /// </summary>
        /// <example>
        ///     Example :
        ///     <code>
        ///         var list = new List&lt;int&gt;{1,2,3,4,5};
        ///         list.AddIFNot(5);
        ///         list.AddIFNot(1);
        ///         list.AddIFNot(6);
        ///         list.Log();
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
        /// <typeparam name="TSource">Type of the elements of this list</typeparam>
        /// <param name="list">This list</param>
        /// <param name="element">Element to add</param>
        /// <returns>this list</returns>
        public static List<TSource> AddIFNot<TSource>(this List<TSource> list, TSource element)
        {
            if (!list.Contains(element))
                list.Add(element);
            return list;
        }


        /// <summary>
        /// Finds the first element of this list that meets the given predicate
        /// </summary>
        /// <example>
        ///     Example :
        ///     <code>
        ///     var list = new List&lt;int&gt;{1,2,3,-1,2,5,10};
        ///     Debug.Log(list.First(element => element &lt; 0));
        ///     </code>
        ///     This code produces the following result : -1
        /// </example>
        /// <typeparam name="TSource">Type of the elements of the list </typeparam>
        /// <param name="list">this list reference</param>
        /// <param name="predicate">the given predicate</param>
        /// <returns>The first element that meets the predicate</returns>
        public static TSource First<TSource>(this List<TSource> list, Func<TSource, bool> predicate)
        {
            return list.Find(elem => predicate(elem));
        }

        /// <summary>
        /// Produces new list with the intersection of two sequences by using the default equality comparer.
        /// </summary>
        /// <example>
        ///     Example :
        ///     <code>
        ///         var firstArray = new List&lt;int&gt;{1,2,3,4,5};
        ///         var secondArray = new List&lt;int&gt;{8,7,3,4,5};
        ///         firstArray.Union(secondArray).Log();
        ///     </code>
        ///     
        ///     This code produces the following result :
        ///     3
        ///     
        ///     4
        ///     
        ///     5
        /// </example>
        /// <typeparam name="TSource">Type of the elements of this list</typeparam>
        /// <param name="list">This list</param>
        /// <param name="list2">The second list</param>
        /// <returns>The intersection of the two arrays</returns>
        public static List<TSource> Intersect<TSource>(this List<TSource> list, TSource[] list2)
        {
            return list.Cast<TSource>().Intersect(list2).ToList();
        }

        /// <summary>
        /// Find the last element of this list with the given predicate
        /// </summary>
        /// <example>
        ///     Example :    
        ///     <code>
        ///     var list = new List&lt;int&gt;{1,2,4,2,5,2,8};
        ///     Debug.Log(list.Last(elem => elem &lt; 2));
        ///     </code>
        ///     This code produces the following result : 1
        ///     
        /// </example>
        /// <typeparam name="TSource">Type of the elements in the list</typeparam>
        /// <param name="list">This list reference</param>
        /// <param name="predicate">The given predicate</param>
        /// <returns>The last element with the given predicate</returns>
        public static TSource Last<TSource>(this List<TSource> list, Func<TSource, bool> predicate)
        {
            return list.FindLast(elem => predicate(elem));
        }

        /// <summary>
        /// Find the last element of this list
        /// </summary>
        /// <example>
        ///     <code>
        ///     var list = new List&lt;int&gt;{1,2,3,4,5};
        ///     Debug.Log(list.Last());
        ///     </code>
        ///     This code produces the following result : 5
        /// </example>
        /// <typeparam name="TSource">Type of the elements in the list</typeparam>
        /// <param name="list">This list reference</param>
        /// <returns>The last element of this list</returns>
        public static TSource Last<TSource>(this List<TSource> list)
        {
            if (list.Count == 0)
                return default(TSource);
            return list[list.Count - 1];
        }

        /// <summary>
        /// Show all elements of this list on the console
        /// </summary>
        /// <example>
        ///     <code>
        ///     var list = new List&lt;int&gt;{1,2,3};
        ///     list.Log();
        ///     </code>
        ///     This code produces the following result :
        ///     
        ///     1
        ///     
        ///     2
        ///     
        ///     3
        /// </example>
        /// <typeparam name="TSource">Type of the elements in this list</typeparam>
        /// <param name="list">this list</param>
        public static void Log<TSource>(this List<TSource> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Debug.Log(list[i]);
            }
        }

        /// <summary>
        /// return random element of this list
        /// </summary>
        /// <typeparam name="TSource">list type</typeparam>
        /// <param name="list">the list</param>
        /// <returns>random element of the list</returns>
        public static TSource Random<TSource>(this List<TSource> list)
        {
            System.Random rand = new System.Random();
            int index = rand.Next(0, list.Count);
            return list[index];
        }

        /// <summary>
        /// shuffle this list
        /// </summary>
        /// <typeparam name="TSource">list type</typeparam>
        /// <param name="list">the list</param>
        /// <returns>this list</returns>
        public static List<TSource> Shuffle<TSource>(this List<TSource> list)
        {
            int j = 0;
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                j = UnityEngine.Random.Range(i, count);
                TSource tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }

            return list;
        }

        /// <summary> 
        /// Shift the list.
        /// </summary>
        /// <example>
        /// Example :
        ///     <code>
        ///         var list = new List&lt;int&gt;{1, 2, 3, 4}
        ///         list.Shift();
        ///         list.Log();
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
        /// <typeparam name="TSource">Type of the elements in the list</typeparam>
        /// <param name="list">This list reference</param>
        /// <returns>this list</returns>
        public static List<TSource> Shift<TSource>(this List<TSource> list)
        {
            TSource tmp = list[0];
            int count = list.Count;
            for (int i = 1; i < count; i++)
            {
                list[i - 1] = list[i];
            }
            list[count - 1] = tmp;

            return list;
        }
    }
}