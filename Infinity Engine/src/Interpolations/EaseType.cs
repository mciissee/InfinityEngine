namespace InfinityEngine.Interpolations
{
    /// <summary>
    /// EaseType allow you to apply custom mathematical formulas to an animation.
    /// </summary>
    /// <example>
    ///   <br />
    ///   Preview of ease functions (images from MSDN documentation).<br />
    ///  <ul>
    ///     <li>Ease back : <br /> <img src="images/ISI Interpolation/ease_back.png" /></li>
    ///     <li>Ease Bounce : <br /> <img src="images/ISI Interpolation/ease_bounce.png" /></li>
    ///     <li>Ease Circle : <br /> <img src="images/ISI Interpolation/ease_circle.png" /></li>
    ///     <li>Ease Cubic : <br /> <img src="images/ISI Interpolation/ease_cubic.png" /></li>
    ///     <li>Ease Elastic : <br /> <img src="images/ISI Interpolation/ease_elastic.png" /></li>
    ///     <li>Ease Exponential : <br /> <img src="images/ISI Interpolation/ease_exponential.png" /></li>
    ///     <li>Ease Quadratic : <br /> <img src="images/ISI Interpolation/ease_quadratic.png" /></li>
    ///     <li>Ease Quartic : <br /> <img src="images/ISI Interpolation/ease_quartic.png" /></li>
    ///     <li>Ease Quintic : <br /> <img src="images/ISI Interpolation/ease_quintic.png" /></li>
    ///     <li>Ease Sine : <br /> <img src="images/ISI Interpolation/ease_sine.png" /></li>
    ///  </ul>
    /// </example>
    public enum EaseTypes
    {
        /// <summary>  linear interpolation </summary>
        Linear,
        /// <summary>  Creates a custom ease function defined by an <a href="https://docs.unity3d.com/ScriptReference/AnimationCurve.html">AnimationCurve</a></summary>
        Custom,
        /// <summary> 
        /// Creates an animation that accelerates using the formula f( t) = t^2.
        ///             </summary>
        QuadIn,
        /// <summary> Creates an animation that decelerates using the formula f( t) = t^2. </summary>
        QuadOut,
        /// <summary> Creates an animation that accelerates and decelerates using the formula f( t) = t^2. </summary>
        QuadInOut,
        /// <summary> 
        ///             Creates an animation that decelerates and accelerates using the formula f( t) = t^2. 
        ///             </summary>
        QuadOutIn,
        /// <summary> 
        /// Creates an animation that accelerates using the formula f( t) = t^3. </summary>
        CubicIn,
        /// <summary> Creates an animation that decelerates using the formula f( t) = t^3. </summary>
        CubicOut,
        /// <summary> Creates an animation that accelerates and decelerates using the formula f( t) = t^3. </summary>
        CubicInOut,
        /// <summary> Creates an animation that decelerates and accelerates using the formula f( t) = t^3. </summary>
        CubicOutIn,
        /// <summary> 
        /// Creates an animation that accelerates using the formula f( t) = t^4.</summary>
        QuartIn,
        /// <summary> Creates an animation that decelerates using the formula f( t) = t^4. </summary>
        QuartOut,
        /// <summary> Creates an animation that accelerates and decelerates using the formula f( t) = t^4. </summary>
        QuartInOut,
        /// <summary> Creates an animation that decelerates and accelerates using the formula f( t) = t^4. </summary>
        QuartOutIn,
        /// <summary> Creates an animation that accelerates using the formula f( t) = t^5. </summary> 
        QuintIn,
        /// <summary> Creates an animation that decelerates using the formula f( t) = t^5. </summary>
        QuintOut,
        /// <summary> Creates an animation that accelerates and decelerates using the formula f( t) = t^5. </summary>
        QuintInOut,
        /// <summary> Creates an animation that decelerates and accelerates using the formula f( t) = t^5. </summary>
        QuintOutIn,
        /// <summary> Creates an animation that accelerates using a sine formula. </summary>
        SineIn,
        /// <summary> Creates an animation that decelerates using a sine formula. </summary>
        SineOut,
        /// <summary> Creates an animation that accelerates and decelerates using a sine formula. </summary>
        SineInOut,
        /// <summary> Creates an animation that decelerates and accelerates using a sine formula. </summary>
        SineOutIn,
        /// <summary> Creates an animation that accelerates using an exponential formula. </summary>
        ExpoIn,
        /// <summary> Creates an animation that decelerates using a exponential formula. </summary>
        ExpoOut,
        /// <summary> Creates an animation that accelerates and decelerates using a exponential formula. </summary>
        ExpoInOut,
        /// <summary> Creates an animation that decelerates and accelerates using a exponential formula. </summary>
        ExpoOutIn,
        /// <summary> Creates an animation that accelerates using a circular formula. </summary>
        CircIn,
        /// <summary> Creates an animation that decelerates using a circular formula. </summary>
        CircOut,
        /// <summary> Creates an animation that accelerates and decelerates using a circular formula. </summary>
        CircInOut,
        /// <summary> Creates an animation that decelerates and accelerates using a circular formula. </summary>
        CircOutIn,
        /// <summary> Creates an animation that resembles a spring oscillating back and forth.</summary>
        ElasticIn,
        /// <summary> Creates an animation that resembles a spring oscillating forth and back at the end.</summary>
        ElasticOut,
        /// <summary> Creates an animation that resembles a spring oscillating back and forth at the beginning and forth and back the end.</summary>
        ElasticInOut,
        /// <summary> Creates an animation that resembles a spring oscillating forth and back at the beginning and back and forth the end.</summary>
        ElasticOutIn,
        /// <summary> Retracts the motion of an animation slightly before it begins.</summary>
        BackIn,
        /// <summary> Retracts the motion of an animation slightly before it ends.</summary>
        BackOut,
        /// <summary> Retracts the motion of an animation slightly before it begins and before it ends.</summary>
        BackInOut,
        /// <summary> Retracts the motion of an animation slightly after it begins and after it ends.</summary>
        BackOutIn,
        /// <summary> Creates a bouncing effect at the beginning</summary>
        BounceIn,
        /// <summary> Creates a bouncing effect at the ends</summary>
        BounceOut,
        /// <summary> Creates a bouncing effect at the beginning and the ends</summary>
        BounceInOut,
        /// <summary> Creates a bouncing effect at the beginning and the ends</summary>
        BounceOutIn
    }
}