using System;
using System.Runtime.InteropServices;

namespace InfinityEngine.Attributes
{
    /// <summary>
    /// Simple struct used just for decorate the inspector of an class
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct DecoratorField
    {
    }
}
