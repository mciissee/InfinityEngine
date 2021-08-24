using System;
using UnityEngine;

namespace InfinityEngine.Attributes
{
    /// <summary>
    ///  Attribute used to display a pop-up window that will specify the value of a string variable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class PopupAttribute : PropertyAttribute
    {
        public readonly char separator = ',';

        public readonly string values;

        public readonly bool enableSearch;

        public readonly PopupValueTypes valueType;

        public int selectedIndex;

        /// <summary>
        ///  Display a pop-up window that will specify the value of the string variable.
        /// </summary>
        /// <param name="values">Must be the name of a field, a simple const string or the name of a player pref</param>
        /// <param name="valueType">The way in which the 'values' parameter of the function is processed depends on this value.</param>
        /// <param name="enableSearch">If sets to <c>true</c>, a search option will be enable</param>
        public PopupAttribute(string values, PopupValueTypes valueType, bool enableSearch = false)
        {
            this.values = values;
            this.valueType = valueType;
            this.enableSearch = enableSearch;
        }

        /// <summary>
        ///  Display a pop-up window that will specify the value of the string variable.
        /// </summary>
        /// <param name="values">Must be the name of a field, a simple const string or the name of a player pref</param>
        /// <param name="separator">The separator char which separates the values</param>
        /// <param name="valueType">The way in which the 'values' parameter of the function is processed depends on this value.</param>
        /// <param name="enableSearch">If sets to <c>true</c>, a search option will be enable</param>
        public PopupAttribute(string values, char separator, PopupValueTypes valueType, bool enableSearch = false)
        {
            this.values = values;
            this.separator = separator;
            this.valueType = valueType;
            this.enableSearch = enableSearch;
        }
    }
}
