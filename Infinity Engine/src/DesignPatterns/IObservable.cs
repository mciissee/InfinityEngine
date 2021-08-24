/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

namespace InfinityEngine.DesignPatterns
{

    /// <summary>  
    ///    interface implemented by the class <see cref="Observable"/>
    /// </summary>
    public interface IObservable
    {

        /// <summary>
        /// Add new observer to this.
        /// </summary>
        /// <param name="obs">The Observer</param>
        void AddObserver(Observer obs);

        /// <summary>
        /// Remove All Observers
        /// </summary>
        void RemoveObservers();

        /// <summary>
        /// Remove the given observer
        /// </summary>
        /// <param name="obs"></param>
        void RemoveObserver(Observer obs);

        /// <summary>
        /// Notify all observers that this object has changed
        /// </summary>
        void NotifyObservers();

        /// <summary>
        /// Check if the given <see cref="Observer"/> <paramref name="obs"/> is observing this <see cref="IObservable"/>
        /// </summary>
        /// <param name="obs"></param>
        /// <returns><c>true</c> if the given <see cref="Observer"/> is observing this.</returns>
        bool HasObserver(Observer obs);
    }
}