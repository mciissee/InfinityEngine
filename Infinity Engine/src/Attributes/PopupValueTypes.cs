namespace InfinityEngine.Attributes
{
    /// <summary>
    /// The type of way popup values are stored
    /// </summary>
    public enum PopupValueTypes
    {
        /// <summary>
        /// Used to specify that the parameter 'values' of the contructor of <see cref="T:InfinityEngine.Attributes.PopupAttribute" /> is a constant string with the values separated by a given separator char.<para> </para>
        /// Example : [PopupAttribute("AA,BB,CC", PopupValueTypes.String)] or [PopupAttribute(MyClass.MyVariable, PopupValueTypes.String)]
        /// </summary>
        String,
        /// <summary>
        /// Used to specify that the parameter 'values' of the contructor of <see cref="T:InfinityEngine.Attributes.PopupAttribute" /> is the key of a string playerpref.
        /// </summary>
        PlayerPref,
        /// <summary>
        /// Used to specify that the parameter 'values' of the contructor of <see cref="T:InfinityEngine.Attributes.PopupAttribute" /> is the name of a function which returns a string or a string array
        /// </summary>
        Method
    }
}
