using System;
using UnityEngine;
//// <summary>  
//// This namespace provides access to custom attributes.
//// </summary>
namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Attribute used to overrides the way unity draw a class or a struct inside the inspector window.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class CustomDrawerAttribute : PropertyAttribute
    {
    }
}
