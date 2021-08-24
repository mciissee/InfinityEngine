/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

namespace InfinityEngine.DesignPatterns
{
    /// <summary>
    /// View component of MVC design pattern
    /// </summary>
    public interface Observer
    {

        /// <summary>
        /// Callback method called when the observed model has changed
        /// </summary>
        /// <param name="obj">The observed Model</param>
        void OnChanged(object obj);
    }

}