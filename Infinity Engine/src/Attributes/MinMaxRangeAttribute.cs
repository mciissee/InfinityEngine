using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to make a the min and max value of a <see cref="T:InfinityEngine.MinMax" /> object be rectricted in a range
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MinMaxRangeAttribute : PropertyAttribute
    {
        /// <summary>
        /// The limit of the min value
        /// </summary>
        public float minLimit;

        /// <summary>
        /// The limit of the max value
        /// </summary>
        public float maxLimit;

        /// <summary>
        /// The name of the getter function which changes dynamically the value of <see cref="F:InfinityEngine.Attributes.MinMaxRangeAttribute.minLimit" />
        /// </summary>
        public string minLimitGetterFunction;

        /// <summary>
        /// The name of the getter function which changes dynamically the value of <see cref="F:InfinityEngine.Attributes.MinMaxRangeAttribute.maxLimit" />
        /// </summary>
        public string maxLimitGetterFunction;

        /// <summary>
        /// Gets a value indicating whether the min and max value of the decorated <see cref="T:InfinityEngine.MinMax" /> object must be casted to an integer
        /// </summary>
        public bool isIntegerRange;

        /// <summary>
        /// Gets a value indicating whether the limit can be changed dynamically
        /// </summary>
        public bool isDynamic;

        /// <summary>
        /// Restricts the min and max value of the <see cref="T:InfinityEngine.MinMax" /> object between two float values
        /// </summary>
        /// <param name="minLimit">The minimum value allowed for the min value of the <see cref="T:InfinityEngine.MinMax" /> object</param>
        /// <param name="maxLimit">The maximum value allowed for the min value of the <see cref="T:InfinityEngine.MinMax" /> object</param>
        public MinMaxRangeAttribute(float minLimit, float maxLimit)
        {
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
        }

        /// <summary>
        /// Restricts the min and max value of the <see cref="T:InfinityEngine.MinMax" /> object between two integers values 
        /// </summary>
        /// <param name="minLimit">The minimum value allowed for the min value of the <see cref="T:InfinityEngine.MinMax" /> object</param>
        /// <param name="maxLimit">The maximum value allowed for the min value of the <see cref="T:InfinityEngine.MinMax" /> object</param>
        public MinMaxRangeAttribute(int minLimit, int maxLimit)
        {
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
            isIntegerRange = true;
        }

        /// <summary>
        /// Restricts the min and max value of the <see cref="T:InfinityEngine.MinMax" /> object between two values provided dynamically by 2 getter functions
        /// </summary>
        /// <example>
        /// using UnityEngine;
        /// using InfinityEngine;
        /// using InfinityEngine.Attributes;
        ///
        /// public class TestClass : MonoBehaviour{
        ///
        ///     [MinMaxRange("GetMin", "GetMax")]
        ///     public MinMax minMax;
        ///
        ///     public CustomClass customClass;
        ///
        ///     public int GetMin(){ return 0; }
        ///
        ///     public int GetMax(){ return 10; }
        ///
        /// }
        ///
        /// public class CustomClass{
        ///
        ///     // As you can see  the functions are defined in the TestClass not in this class
        ///     [MinMaxRange("GetMin", "GetMax")]
        ///     public MinMax customClassMinMax;
        /// }
        /// </example>
        /// <param name="minLimitGetterFunction">The name of the getter function which returns the minimum value allowed for the min value of the <see cref="T:InfinityEngine.MinMax" /> object</param>
        /// <param name="maxLimitGetterFunction">The name of the getter function which returns the maximum value allowed for the min value of the <see cref="T:InfinityEngine.MinMax" /> object</param>
        public MinMaxRangeAttribute(string minLimitGetterFunction, string maxLimitGetterFunction)
        {
            this.minLimitGetterFunction = minLimitGetterFunction;
            this.maxLimitGetterFunction = maxLimitGetterFunction;
            isDynamic = true;
        }

        public MinMaxRangeAttribute(float minLimit, string maxLimitGetterFunction)
        {
            this.minLimit = minLimit;
            this.maxLimitGetterFunction = maxLimitGetterFunction;
            isDynamic = true;
        }

        public MinMaxRangeAttribute(string minLimitGetterFunction, float maxLimit)
        {
            this.minLimitGetterFunction = minLimitGetterFunction;
            this.maxLimit = maxLimit;
            isDynamic = true;
        }
    }
}
