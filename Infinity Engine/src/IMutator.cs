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
    /// This is a marker interface for all Mutator classes.
    /// 
    /// You typically use this interface with a list or dictionary to make references to the methods encapsulated by Mutator classes.
    /// </summary>
    public interface IMutator { }

    /// <summary>
    ///  Encapsulates a method with takes 0 parameter and which returns no value.
    /// </summary>
    /// <remarks>
    /// There is 4 variations of this class :<br/>
    ///     - <see cref="Mutator{T}"/><br/>
    ///     - <see cref="Mutator{T1, T2}"/><br/>
    ///     - <see cref="Mutator{T1, T2, T3}"/><br/>
    ///     - <see cref="Mutator{T1, T2, T3, T4}"/><br/>
    /// 
    /// All theses classes allow to encapasulate  functions with a more parameters
    /// </remarks>
    public class Mutator : IMutator
    {
        private Action action;

        /// <summary>
        /// Creates new instance of this class 
        /// </summary>
        /// <param name="action">The method to encapsulate</param>
        public Mutator(Action action)
        {
            this.action = action;
        }

        /// <summary>
        /// Invokes the encapsulated function.
        /// </summary>
        public void Invoke()
        {
            action.Invoke();
        }

    }

    /// <summary>
    ///  Encapsulates a method with takes 1 parameter and which returns no value.
    /// </summary>
    /// <typeparam name="T">Type of the parameter of the encapsulated function</typeparam>
    public class Mutator<T> : IMutator
    {
        private Action<T> action;

        /// <summary>
        /// Create new instance of this class 
        /// </summary>
        /// <param name="action">The method to encapsulate</param>
        public Mutator(Action<T> action)
        {
            this.action = action;
        }

        /// <summary>
        /// Invokes the encapsulated function.
        /// </summary>
        /// <param name="arg">the parameter of the encapsulated function</param>
        public void Invoke(T arg)
        {
            action.Invoke(arg);
        }

    }

    /// <summary>
    /// Encapsulates a method with takes 2 parameters and which returns no value.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter of the encapsulated function</typeparam>
    /// <typeparam name="T2">Type of the second parameter of the encapsulated function</typeparam>
    public class Mutator<T1, T2> : IMutator
    {
        private Action<T1, T2> action;

        /// <summary>
        /// Creates new instance of this class 
        /// </summary>
        /// <param name="action">The method to encapsulate</param>
        public Mutator(Action<T1, T2> action)
        {
            this.action = action;
        }

        /// <summary>
        /// Invokes the encapsulated function.
        /// </summary>
        /// <param name="arg1">the fist parameter of the encapsulated function</param>
        /// <param name="arg2">the second parameter of the encapsulated function</param>
        public void Invoke(T1 arg1, T2 arg2)
        {
            action.Invoke(arg1, arg2);
        }

    }

    /// <summary>
    /// Encapsulates a method with takes 3 parameters and which returns no value.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter of the encapsulated function</typeparam>
    /// <typeparam name="T2">Type of the second parameter of the encapsulated function</typeparam>
    /// <typeparam name="T3">Type of the third parameter of the encapsulated function</typeparam>
    public class Mutator<T1, T2, T3> : IMutator
    {
        private Action<T1, T2, T3> action;

        /// <summary>
        /// Creates new instance of this class 
        /// </summary>
        /// <param name="action">The method to encapsulate</param>
        public Mutator(Action<T1, T2, T3> action)
        {
            this.action = action;
        }

        /// <summary>
        /// Invokes the encapsulated function.
        /// </summary>
        /// <param name="arg1">the fist parameter of the encapsulated function</param>
        /// <param name="arg2">the second parameter of the encapsulated function</param>
        /// <param name="arg3">the third parameter of the encapsulated function</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            action.Invoke(arg1, arg2, arg3);
        }

    }

    /// <summary>
    /// Encapsulates a method with takes 4 parameters and which returns no value.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter of the encapsulated function</typeparam>
    /// <typeparam name="T2">Type of the second parameter of the encapsulated function</typeparam>
    /// <typeparam name="T3">Type of the third parameter of the encapsulated function</typeparam>
    /// <typeparam name="T4">Type of the fourth parameter of the encapsulated function</typeparam>
    public class Mutator<T1, T2, T3, T4> : IMutator
    {
        private Action<T1, T2, T3, T4> action;

        /// <summary>
        /// Creates new instance of this class 
        /// </summary>
        /// <param name="action">The method to encapsulate</param>
        public Mutator(Action<T1, T2, T3, T4> action)
        {
            this.action = action;
        }

        /// <summary>
        /// Invoke the encapsulated function.
        /// </summary>
        /// <param name="arg1">the fist parameter of the encapsulated function</param>
        /// <param name="arg2">the second parameter of the encapsulated function</param>
        /// <param name="arg3">the third parameter of the encapsulated function</param>
        /// <param name="arg4">the fouth parameter of the encapsulated function</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            action.Invoke(arg1, arg2, arg3, arg4);
        }

    }

}