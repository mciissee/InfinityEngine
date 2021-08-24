#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using UnityEngine;
using System;

namespace InfinityEngine.Interpolations
{

    /// <summary>
    ///  Represents 2 colors in a single struct
    /// </summary>
    [Serializable]
    public struct DoubleColor
    {
        /// <summary>
        /// The first Color
        /// </summary>
        [SerializeField] public Color first;
        /// <summary>
        /// The second Color
        /// </summary>
        [SerializeField] public Color second;

        /// <summary>
        /// The first Color
        /// </summary>
        public Color First { get { return first; } set { first = value; } }
        
        /// <summary>
        /// The second Color
        /// </summary>
        public Color Second { get { return second; } set { second = value; } }

        /// <summary>
        /// Indexer to get or set colors within this DoubleColor using array index syntax.
        /// </summary>
        ///
        /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
        ///                                                 the required range. </exception>
        ///
        /// <param name="index">    Zero-based index of the entry to access. </param>
        ///
        /// <returns>   The indexed item. </returns>
        public Color this[int index]
        {
            get {
                    if (index < 0 || index > 1)
                    {
                        throw new ArgumentOutOfRangeException();                 
                    }
                    else
                    {
                        if (index == 0)
                            return First;
                        else
                            return Second;
                    }
                    
                }
            set
            {
                if (index < 0 || index > 1)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    if (index == 0)
                        First = value;
                    else
                        Second = value;
                }
            }
        }
       
        /// <summary>   Store two white colors </summary>
        public static DoubleColor white
        {
            get { return new DoubleColor(Color.white, Color.white); }
        }
        /// <summary>   Store two red colors </summary>
        public static DoubleColor red
        {
            get { return new DoubleColor(Color.red, Color.red); }
        }
        /// <summary>   Store two blue colors </summary>
        public static DoubleColor blue
        {
            get { return new DoubleColor(Color.blue, Color.blue); }
        }
        /// <summary>   Store two green colors </summary>
        public static DoubleColor green
        {
            get { return new DoubleColor(Color.green, Color.green); }
        }
        /// <summary>   Store two black colors </summary>
        public static DoubleColor black
        {
            get { return new DoubleColor(Color.black, Color.black); }
        }

        /// <summary>
        /// Creates new struct that contains 2 colors
        /// </summary>
        /// <param name="first">the first color</param>
        /// <param name="second">the second color</param>
        public DoubleColor(Color first, Color second)
        {
            this.first = first;
            this.second = second;
        }

        /// <summary>   Addition operation between 2 DoubleColor objects. </summary>
        ///
        /// <param name="first">    The first object. </param>
        /// <param name="second">   The second object. </param>
        ///
        /// <returns>   The result of the operation. </returns>
        public static DoubleColor operator +(DoubleColor first, DoubleColor second)
        {
            return new DoubleColor(first.First + second.First, first.Second + second.Second);
        }

        /// <summary>   Subtraction operation between 2 DoubleColor objects.</summary>
        ///
        /// <param name="fist">     The first object. </param>
        /// <param name="second">   The second object. </param>
        ///
        /// <returns>   The result of the operation. </returns>
        public static DoubleColor operator -(DoubleColor fist, DoubleColor second)
        {
            return new DoubleColor(fist.First - second.First, fist.Second - second.Second);
        }

        /// <summary>   Multiplication operation between a DoubleColor object and a float. </summary>
        ///
        /// <param name="first">    The DoubleColor object. </param>
        /// <param name="value">    The value of the float. </param>
        ///
        /// <returns>   The result of the operation. </returns>
        public static DoubleColor operator *(DoubleColor first, float value)
        {
            return new DoubleColor(first.First * value, first.Second * value);
        }


        /// <summary>   Division operation between a DoubleColor object and a float. </summary>
        ///
        /// <param name="first">    The DoubleColor object. </param>
        /// <param name="value">    The value of the float. </param>
        ///
        /// <returns>   The result of the operation. </returns>
        public static DoubleColor operator /(DoubleColor first, float value)
        {
            return new DoubleColor(first.First / value, first.Second / value);
        }
    }
}

