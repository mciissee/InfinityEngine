namespace InfinityEngine.Interpolations
{
    /// <summary>
    /// Interpolation loop type.
    /// </summary>
    public enum LoopTypes
    {
        /// <summary>
        /// Restarts the interpolable from the beginning (A =&gt; B  , A =&gt; B)
        /// </summary>
        Restart,
        /// <summary>
        /// Reverse the starts value and the ends value of the interpolable at each loop.(A =&gt; B then B =&gt; A)
        /// </summary>
        Reverse
    }
}
