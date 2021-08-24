/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/


using System;

namespace InfinityEngine
{

	/// <summary>
    ///  Marker interface for all Accessor classes. 
    /// 
    ///     You typically use this interface with a list or dictionary to make references to the methods encapsulated by Accessor classes.
    /// </summary>
    public interface IAccessor { }

    /// <summary>
    ///  Encapsulates a method with takes 0 parameter and which returns a value.
    /// </summary>
    /// <remarks>
    /// There is 4 variations of this class : <br/>
    ///     - <see cref="Accessor{T, TResult}"/> <br/>
    ///     - <see cref="Accessor{T1, T2, TResult}"/> <br/>
    ///     - <see cref="Accessor{T1, T2, T3, TResult}"/> <br/>
    ///     - <see cref="Accessor{T1, T2, T3, T4, TResult}"/> <br/>
    ///    
    /// All theses classes allow to encapasulate functions with a more parameters
    /// </remarks>
    /// <typeparam name="TResult">Type of the value returned by the encapsulated function</typeparam>
    public class Accessor<TResult> : IAccessor
    {
        private Func<TResult> func;

        /// <summary>
        /// Create new instance of this class 
        /// </summary>
        /// <param name="func">The method to encapsulate</param>
        public Accessor(Func<TResult> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Invokes the encapsulated function.
        /// </summary>
        /// <returns>An object of type <c>TResult</c></returns>
        public TResult Invoke()
        {
            return func.Invoke();
        }

    }

    /// <summary>
    ///  Encapsulates a method with takes 1 parameter and which returns a value.
    /// </summary>
    /// <typeparam name="T">Type of the parameter of the encapsulated function</typeparam>
    /// <typeparam name="TResult">Type of the value returned by the encapsulated function</typeparam>
    public class Accessor<T, TResult> : IAccessor
    {
        private Func<T, TResult> func;

        /// <summary>
        /// Creates new instance of this class 
        /// </summary>
        /// <param name="func">The method to encapsulate</param>
        public Accessor(Func<T, TResult> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Invokes the encapsulated function.
        /// </summary>
        /// <param name="arg">the parameter of the encapsulated function</param>
        /// <returns>An object of type <c>TResult</c></returns>
        public TResult Invoke(T arg)
        {
            return func.Invoke(arg);
        }

    }

    /// <summary>
    ///  Encapsulates a method with takes 2 parameter and which returns a value.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter of the encapsulated function</typeparam>
    /// <typeparam name="T2">Type of the second parameter of the encapsulated function</typeparam> 
    /// <typeparam name="TResult">Type of the value returned by the encapsulated function</typeparam>
    public class Accessor<T1, T2, TResult> : IAccessor
    {
        private Func<T1, T2, TResult> func;

        /// <summary>
        /// Creates new instance of this class 
        /// </summary>
        /// <param name="func">The method to encapsulate</param>
        public Accessor(Func<T1, T2, TResult> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Invokes the encapsulated function.
        /// </summary>
        /// <param name="arg1">the first parameter of the encapsulated function</param>
        /// <param name="arg2">the second parameter of the encapsulated function</param>
        /// <returns>An object of type <c>TResult</c></returns>
        public TResult Invoke(T1 arg1, T2 arg2)
        {
            return func.Invoke(arg1, arg2);
        }

    }

    /// <summary>
    /// Encapsulates a method with takes 2 parameter and which returns a value.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter of the encapsulated function</typeparam>
    /// <typeparam name="T2">Type of the second parameter of the encapsulated function</typeparam> 
    /// <typeparam name="T3">Type of the third parameter of the encapsulated function</typeparam> 
    /// <typeparam name="TResult">Type of the value returned by the encapsulated function</typeparam>
    public class Accessor<T1, T2, T3, TResult> : IAccessor
    {
        private Func<T1, T2, T3, TResult> func;

        /// <summary>
        /// Creates new instance of this class 
        /// </summary>
        /// <param name="func">The method to encapsulate</param>
        public Accessor(Func<T1, T2, T3, TResult> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Invokes the encapsulated function.
        /// </summary>
        /// <param name="arg1">the first parameter of the encapsulated function</param>
        /// <param name="arg2">the second parameter of the encapsulated function</param>
        /// <param name="arg3">the third parameter of the encapsulated function</param>
        /// <returns>An object of type <c>TResult</c></returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            return func.Invoke(arg1, arg2, arg3);
        }

    }

    /// <summary>
    /// Encapsulates a method with takes 2 parameter and which returns a value.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter of the encapsulated function</typeparam>
    /// <typeparam name="T2">Type of the second parameter of the encapsulated function</typeparam> 
    /// <typeparam name="T3">Type of the third parameter of the encapsulated function</typeparam> 
    /// <typeparam name="T4">Type of the fourth parameter of the encapsulated function</typeparam> 
    /// <typeparam name="TResult">Type of the value returned by the encapsulated function</typeparam>
    public class Accessor<T1, T2, T3, T4, TResult> : IAccessor
    {
        private Func<T1, T2, T3, T4, TResult> func;

        /// <summary>
        /// Creates new instance of this class 
        /// </summary>
        /// <param name="func">The method to encapsulate</param>
        public Accessor(Func<T1, T2, T3, T4, TResult> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Invokes the encapsulated function.
        /// </summary>
        /// <param name="arg1">the first parameter of the encapsulated function</param>
        /// <param name="arg2">the second parameter of the encapsulated function</param>
        /// <param name="arg3">the third parameter of the encapsulated function</param>
        /// <param name="arg4">the fourth parameter of the encapsulated function</param>
        /// <returns>An object of type <c>TResult</c></returns>
        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return func.Invoke(arg1, arg2, arg3, arg4);
        }

    }
}