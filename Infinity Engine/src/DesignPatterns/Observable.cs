/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

namespace InfinityEngine.DesignPatterns
{

    /// <summary>
    ///  Model component of MVC Design pattern
    /// </summary>
    public class Observable : MonoBehaviour, IObservable
    {
        private readonly List<Observer> observers = new List<Observer>();

        /// <summary>
        /// add the given observer to observers list
        /// </summary>
        /// <param name="obs">the observer to add</param>
        public void AddObserver(Observer obs)
        {
            observers.Add(obs);
        }

        /// <summary>
        /// Notify all obsevers of the model (the method (OnChange()' of all observers will be invoked) 
        /// </summary>
        public void NotifyObservers()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].OnChanged(this);
            }
        }

        /// <summary>
        /// Removes all observers
        /// </summary>
        public void RemoveObservers()
        {
            observers.Clear();
        }

        /// <summary>
        /// remove the given observer
        /// </summary>
        /// <param name="obs">the observer to remove</param>
        public void RemoveObserver(Observer obs)
        {
            observers.Remove(obs);
        }

        /// <summary>
        /// Checks if the given <see cref="Observer"/> <paramref name="obs"/> is observing this <see cref="IObservable"/>
        /// </summary>
        /// <param name="obs"></param>
        /// <returns><c>true</c> if the given <see cref="Observer"/> is observing this.</returns>
        public bool HasObserver(Observer obs)
        {
            return observers.Contains(obs);
        }

    }

}