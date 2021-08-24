using UnityEngine;

namespace InfinityEngine
{
    /// <summary>
    /// Class used to suspends the execution of a coroutine inside the editor.
    /// </summary>
    public class EditorWaitForSeconds : CustomYieldInstruction
    {
        private readonly float waitTime;

        /// <summary>
        /// Gets a value indicating whether the coroutine should be suspended
        /// </summary>
        public override bool keepWaiting => Infinity.EditorTimeSinceStartup < waitTime;

        /// <summary>
        /// Suspends the execution of a coroutine for the given time in seconds
        /// </summary>
        /// <param name="time">The time to suspends the coroutine</param>
        public EditorWaitForSeconds(float time)
        {
            waitTime = Infinity.EditorTimeSinceStartup + time;
        }
    }
}
