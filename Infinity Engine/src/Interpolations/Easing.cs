/************************************************************************************************************************************
* Developed by Mamadou Cisse     (Easing equations by Robber Penner http://robertpenner.com/easing/)                                                                                            *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/
using System;
using UnityEngine;

namespace InfinityEngine.Interpolations
{
    /// <summary>
    ///   Easing class allow you to apply custom mathematical formulas to an animation. 
    ///
    ///   This produces a realistics animations effect like bounce effect..
    /// </summary>
    public static class Easing
    {
        /// <summary>
        /// Interpolate the value of the given object of type <c>T</c> between <paramref name="from" /> and <paramref name="to" />
        /// in <paramref name="time" /> seconds
        /// </summary>
        /// <typeparam name="T">Type to Ease</typeparam>
        /// <param name="type">Ease Type (Exemple : slow at start and fast at the end)</param>
        /// <param name="from">initial value</param>
        /// <param name="to">final value</param>
        /// <param name="time">duration</param>
        /// <param name="motion">Custom motion curve <default>null</default></param>
        /// <param name="rotationMode">Rotation mode in the case of Quaternion interpolation</param>
        /// <returns></returns>
        public static T Ease<T>(EaseTypes type, T from, T to, float time, AnimationCurve motion = null, RotationModes rotationMode = RotationModes.Fast)
        {
            if ((object)typeof(T) == typeof(float))
            {
                return (T)(object)Ease(type, (float)(object)from, (float)(object)to, time, motion);
            }
            if ((object)typeof(T) == typeof(int))
            {
                return (T)(object)Ease(type, (int)(object)from, (int)(object)to, time, motion);
            }
            if ((object)typeof(T) == typeof(Vector2))
            {
                return (T)(object)Easing.Ease(type, (Vector2)(object)from, (Vector2)(object)to, time, motion);
            }
            if ((object)typeof(T) == typeof(Vector3))
            {
                return (T)(object)Easing.Ease(type, (Vector3)(object)from, (Vector3)(object)to, time, motion);
            }
            if ((object)typeof(T) == typeof(Vector4))
            {
                return (T)(object)Easing.Ease(type, (Vector4)(object)from, (Vector4)(object)to, time, motion);
            }
            if ((object)typeof(T) == typeof(Quaternion))
            {
                return (T)(object)Ease(type, (Quaternion)(object)from, (Quaternion)(object)to, time, motion, rotationMode);
            }
            if ((object)typeof(T) == typeof(Color))
            {
                return (T)(object)Easing.Ease(type, (Color)(object)from, (Color)(object)to, time, motion);
            }
            if ((object)typeof(T) == typeof(Rect))
            {
                return (T)(object)Easing.Ease(type, (Rect)(object)from, (Rect)(object)to, time, motion);
            }
            if ((object)typeof(T) == typeof(DoubleColor))
            {
                DoubleColor doubleColor = (DoubleColor)(object)from;
                DoubleColor doubleColor2 = (DoubleColor)(object)to;
                doubleColor.First = Easing.Ease(type, (Color)(object)doubleColor[0], (Color)(object)doubleColor2[0], time, motion);
                doubleColor.Second = Easing.Ease(type, (Color)(object)doubleColor[1], (Color)(object)doubleColor2[1], time, motion);
                return (T)(object)doubleColor;
            }
            return default(T);
        }

        private static float Ease(EaseTypes type, float from, float to, float time, AnimationCurve motion = null)
        {
            if (type == EaseTypes.Custom)
            {
                if (motion != null)
                {
                    return Mathf.Lerp(from, to, motion.Evaluate(time));
                }
                return to;
            }
            return Get(type, from, to, time);
        }

        private static int Ease(EaseTypes type, int from, int to, float time, AnimationCurve motion = null)
        {
            return (int)Ease(type, (float)from, (float)to, time, motion);
        }

        private static Vector2 Ease(EaseTypes type, Vector2 from, Vector2 to, float time, AnimationCurve motion = null)
        {
            if (type == EaseTypes.Custom)
            {
                if (motion != null)
                {
                    return Vector2.Lerp(from, to, motion.Evaluate(time));
                }
                return Get(EaseTypes.Linear, from, to, time);
            }
            return Get(type, from, to, time);
        }

        private static Vector3 Ease(EaseTypes type, Vector3 from, Vector3 to, float time, AnimationCurve motion = null)
        {
            if (type == EaseTypes.Custom)
            {
                if (motion != null)
                {
                    return Vector3.Lerp(from, to, motion.Evaluate(time));
                }
                return Get(EaseTypes.Linear, from, to, time);
            }
            return Get(type, from, to, time);
        }

        private static Vector4 Ease(EaseTypes type, Vector4 from, Vector4 to, float time, AnimationCurve motion = null)
        {
            if (type == EaseTypes.Custom)
            {
                if (motion != null)
                {
                    return Vector4.Lerp(from, to, motion.Evaluate(time));
                }
                return Get(EaseTypes.Linear, from, to, time);
            }
            return Get(type, from, to, time);
        }

        private static Quaternion Ease(EaseTypes type, Quaternion from, Quaternion to, float time, AnimationCurve motion = null, RotationModes mode = RotationModes.Fast)
        {
            if (type == EaseTypes.Custom)
            {
                if (motion != null)
                {
                    return Quaternion.Lerp(from, to, motion.Evaluate(time));
                }
                return Get(EaseTypes.Linear, from, to, time, mode);
            }
            return Get(type, from, to, time, mode);
        }

        private static Color Ease(EaseTypes type, Color from, Color to, float time, AnimationCurve motion = null)
        {
            if (type == EaseTypes.Custom)
            {
                if (motion != null)
                {
                    return Color.Lerp(from, to, motion.Evaluate(time));
                }
                return Get(EaseTypes.Linear, from, to, time);
            }
            return Get(type, from, to, time);
        }

        private static Rect Ease(EaseTypes type, Rect from, Rect to, float time, AnimationCurve motion = null)
        {
            if (type == EaseTypes.Custom)
            {
                if (motion != null)
                {
                    Vector2.Lerp(from.size, to.size, motion.Evaluate(time));
                    Vector2.Lerp(from.position, to.position, motion.Evaluate(time));
                    return from;
                }
                return Get(EaseTypes.Linear, from, to, time);
            }
            return Get(type, from, to, time);
        }

        private static RectOffset Ease(EaseTypes type, RectOffset from, RectOffset to, float time, AnimationCurve motion = null)
        {
            if (type == EaseTypes.Custom)
            {
                if (motion != null)
                {
                    from.left = (int)Mathf.Lerp((float)from.left, (float)to.left, motion.Evaluate(time));
                    from.right = (int)Mathf.Lerp((float)from.right, (float)to.right, motion.Evaluate(time));
                    from.top = (int)Mathf.Lerp((float)from.top, (float)to.top, motion.Evaluate(time));
                    from.bottom = (int)Mathf.Lerp((float)from.bottom, (float)to.bottom, motion.Evaluate(time));
                    return from;
                }
                return Get(EaseTypes.Linear, from, to, time);
            }
            return Get(type, from, to, time);
        }

        private static float Get(EaseTypes type, float from, float to, float t)
        {
            return Equation(type, from, to, t);
        }

        private static Vector2 Get(EaseTypes type, Vector2 from, Vector2 to, float t)
        {
            from.x = Get(type, from.x, to.x, t);
            from.y = Get(type, from.y, to.y, t);
            return from;
        }

        private static Vector3 Get(EaseTypes type, Vector3 from, Vector3 to, float t)
        {
            from.x = Get(type, from.x, to.x, t);
            from.y = Get(type, from.y, to.y, t);
            from.z = Get(type, from.z, to.z, t);
            return from;
        }

        private static Vector4 Get(EaseTypes type, Vector4 from, Vector4 to, float t)
        {
            from.x = Get(type, from.x, to.x, t);
            from.y = Get(type, from.y, to.y, t);
            from.z = Get(type, from.z, to.z, t);
            from.w = Get(type, from.w, to.w, t);
            return from;
        }

        private static Quaternion Get(EaseTypes type, Quaternion from, Quaternion to, float t)
        {
            from.x = Get(type, from.x, to.x, t);
            from.y = Get(type, from.y, to.y, t);
            from.z = Get(type, from.z, to.z, t);
            from.w = Get(type, from.w, to.w, t);
            return from;
        }

        private static Quaternion Get(EaseTypes type, Quaternion from, Quaternion to, float t, RotationModes mode)
        {
            if (mode == RotationModes.Fast)
            {
                from.x = Get(type, from.x, to.x, t);
                from.y = Get(type, from.y, to.y, t);
                from.z = Get(type, from.z, to.z, t);
                from.w = Get(type, from.w, to.w, t);
                return from;
            }
            return Quaternion.Euler(Get(type, from.eulerAngles, to.eulerAngles, t));
        }

        private static Color Get(EaseTypes type, Color from, Color to, float t)
        {
            from.r = Get(type, from.r, to.r, t);
            from.g = Get(type, from.g, to.g, t);
            from.b = Get(type, from.b, to.b, t);
            from.a = Get(type, from.a, to.a, t);
            return from;
        }

        private static Rect Get(EaseTypes type, Rect from, Rect to, float t)
        {
            from.Set(
                Get(type, from.x, to.x, t),
                Get(type, from.y, to.y, t),
                Get(type, from.width, to.width, t),
                Get(type, from.height, to.height, t)
            );
            return from;
        }

        private static RectOffset Get(EaseTypes type, RectOffset from, RectOffset to, float t)
        {
            from.left = ((int)Get(type, (float)from.left, (float)to.left, t));
            from.right = ((int)Get(type, (float)from.right, (float)to.right, t));
            from.top = ((int)Get(type, (float)from.top, (float)to.top, t));
            from.bottom = ((int)Get(type, (float)from.bottom, (float)to.bottom, t));
            return from;
        }

        private static float In(Func<float, float, float> function, float time, float begin, float change, float duration = 1f)
        {
            if (time >= duration)
            {
                return begin + change;
            }
            if (time <= 0f)
            {
                return begin;
            }
            return change * function(time, duration) + begin;
        }

        private static float Out(Func<float, float, float> function, float time, float begin, float change, float duration = 1f)
        {
            if (time >= duration)
            {
                return begin + change;
            }
            if (time <= 0f)
            {
                return begin;
            }
            return begin + change - change * function(duration - time, duration);
        }

        private static float InOut(Func<float, float, float> function, float time, float begin, float change, float duration = 1f)
        {
            if (time >= duration)
            {
                return begin + change;
            }
            if (time <= 0f)
            {
                return begin;
            }
            if (time < duration / 2f)
            {
                return In(function, time * 2f, begin, change / 2f, duration);
            }
            return Out(function, time * 2f - duration, begin + change / 2f, change / 2f, duration);
        }

        private static float OutIn(Func<float, float, float> function, float time, float begin, float change, float duration = 1f)
        {
            if (time >= duration)
            {
                return begin + change;
            }
            if (time <= 0f)
            {
                return begin;
            }
            if (time < duration / 2f)
            {
                return Out(function, time * 2f, begin, change / 2f, duration);
            }
            return In(function, time * 2f - duration, begin + change / 2f, change / 2f, duration);
        }

        private static float Linear(float time, float duration = 1f)
        {
            return time / duration;
        }

        private static float Quad(float time, float duration = 1f)
        {
            return (time /= duration) * time;
        }

        private static float Cubic(float time, float duration = 1f)
        {
            return (time /= duration) * time * time;
        }

        private static float Quart(float time, float duration = 1f)
        {
            return (time /= duration) * time * time * time;
        }

        private static float Quint(float time, float duration = 1f)
        {
            return (time /= duration) * time * time * time * time;
        }

        private static float Sine(float time, float duration = 1f)
        {
            return 1f - Mathf.Cos(time / duration * 1.57079637f);
        }

        private static float Expo(float time, float duration = 1f)
        {
            return Mathf.Pow(2f, 10f * (time / duration - 1f));
        }

        private static float Circ(float time, float duration = 1f)
        {
            return 0f - (Mathf.Sqrt(1f - (time /= duration) * time) - 1f);
        }

        private static float Elastic(float time, float duration = 1f)
        {
            time /= duration;
            float num = duration * 0.3f;
            float num2 = num / 4f;
            return 0f - Mathf.Pow(2f, 10f * (time -= 1f)) * Mathf.Sin((time * duration - num2) * 6.28318548f / num);
        }

        private static float Back(float time, float duration = 1f)
        {
            return (time /= duration) * time * (2.70158f * time - 1.70158f);
        }

        private static float Bounce(float time, float duration = 1f)
        {
            time = duration - time;
            if ((time /= duration) < 0.363636374f)
            {
                return 1f - 7.5625f * time * time;
            }
            if (time < 0.727272749f)
            {
                return 1f - (7.5625f * (time -= 0.545454562f) * time + 0.75f);
            }
            if (time < 0.909090936f)
            {
                return 1f - (7.5625f * (time -= 0.8181818f) * time + 0.9375f);
            }
            return 1f - (7.5625f * (time -= 0.954545438f) * time + 0.984375f);
        }

        private static float Equation(EaseTypes type, float from, float to, float time)
        {
            switch (type)
            {
                case EaseTypes.Linear:
                    return In(Linear, time, from, to - from);
                case EaseTypes.QuadIn:
                    return In(Quad, time, from, to - from);
                case EaseTypes.QuadOut:
                    return Out(Quad, time, from, to - from);
                case EaseTypes.QuadInOut:
                    return InOut(Quad, time, from, to - from);
                case EaseTypes.QuadOutIn:
                    return OutIn(Quad, time, from, to - from);
                case EaseTypes.CubicIn:
                    return In(Cubic, time, from, to - from);
                case EaseTypes.CubicOut:
                    return Out(Cubic, time, from, to - from);
                case EaseTypes.CubicInOut:
                    return InOut(Cubic, time, from, to - from);
                case EaseTypes.CubicOutIn:
                    return OutIn(Cubic, time, from, to - from);
                case EaseTypes.QuartIn:
                    return In(Quart, time, from, to - from);
                case EaseTypes.QuartOut:
                    return Out(Quart, time, from, to - from);
                case EaseTypes.QuartInOut:
                    return InOut(Quart, time, from, to - from);
                case EaseTypes.QuartOutIn:
                    return OutIn(Quart, time, from, to - from);
                case EaseTypes.QuintIn:
                    return In(Quint, time, from, to - from);
                case EaseTypes.QuintOut:
                    return Out(Quint, time, from, to - from);
                case EaseTypes.QuintInOut:
                    return InOut(Quint, time, from, to - from);
                case EaseTypes.QuintOutIn:
                    return OutIn(Quint, time, from, to - from);
                case EaseTypes.SineIn:
                    return In(Sine, time, from, to - from);
                case EaseTypes.SineOut:
                    return Out(Sine, time, from, to - from);
                case EaseTypes.SineInOut:
                    return InOut(Sine, time, from, to - from);
                case EaseTypes.SineOutIn:
                    return OutIn(Sine, time, from, to - from);
                case EaseTypes.ExpoIn:
                    return In(Expo, time, from, to - from);
                case EaseTypes.ExpoOut:
                    return Out(Expo, time, from, to - from);
                case EaseTypes.ExpoInOut:
                    return InOut(Expo, time, from, to - from);
                case EaseTypes.ExpoOutIn:
                    return OutIn(Expo, time, from, to - from);
                case EaseTypes.CircIn:
                    return In(Circ, time, from, to - from);
                case EaseTypes.CircOut:
                    return Out(Circ, time, from, to - from);
                case EaseTypes.CircInOut:
                    return InOut(Circ, time, from, to - from);
                case EaseTypes.CircOutIn:
                    return OutIn(Circ, time, from, to - from);
                case EaseTypes.ElasticIn:
                    return In(Elastic, time, from, to - from);
                case EaseTypes.ElasticOut:
                    return Out(Elastic, time, from, to - from);
                case EaseTypes.ElasticInOut:
                    return InOut(Elastic, time, from, to - from);
                case EaseTypes.ElasticOutIn:
                    return OutIn(Elastic, time, from, to - from);
                case EaseTypes.BackIn:
                    return In(Back, time, from, to - from);
                case EaseTypes.BackOut:
                    return Out(Back, time, from, to - from);
                case EaseTypes.BackInOut:
                    return InOut(Back, time, from, to - from);
                case EaseTypes.BackOutIn:
                    return OutIn(Back, time, from, to - from);
                case EaseTypes.BounceIn:
                    return In(Bounce, time, from, to - from);
                case EaseTypes.BounceOut:
                    return Out(Bounce, time, from, to - from);
                case EaseTypes.BounceInOut:
                    return InOut(Bounce, time, from, to - from);
                case EaseTypes.BounceOutIn:
                    return OutIn(Bounce, time, from, to - from);
                default:
                    return In(Linear, time, from, to - from);
            }
        }
    }
}
