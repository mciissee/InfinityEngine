/************************************************************************************************************************************
* Developed by Mamadou Cisse                                                                                                        *
* Mail => mciissee@gmail.com                                                                                                        *
* Twitter => http://www.twitter.com/IncMce                                                                                          *
* Unity Asset Store catalog: http://u3d.as/riS                                                                              *
*************************************************************************************************************************************/

using InfinityEngine.Interpolations;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityEngine.Extensions
{

    /// <summary>
    ///  Extension methods for <a href="https://docs.unity3d.com/ScriptReference/Camera.html"> Camera </a> class
    /// </summary>
    public static class CameraExtensions
    {
        #region Methods

        /// <summary>
        /// Fades this Camera alpha
        /// </summary>
        /// <param name="camera">This Camera</param>
        /// <param name="from">Starts Color</param>
        /// <param name="to">Finals Color</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this Camera camera, float from, float to, float duration)
        {
            var tmp = camera.backgroundColor;
            return Infinity.To(from, (newValue) =>
            {
                tmp.a = newValue;
                camera.backgroundColor = tmp;
            }, to, duration);
        }

        /// <summary>
        /// Fade this Camera alpha
        /// </summary>
        /// <param name="camera">this Camera</param>
        /// <param name="to">final Color</param>
        /// <param name="duration">interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOFade(this Camera camera, float to, float duration)
        {
            return camera.DOFade(camera.backgroundColor.a, to, duration);
        }

        /// <summary>
        /// Fades this camera Color
        /// </summary>
        /// <param name="camera">This camera</param>
        /// <param name="from">Starts value</param>
        /// <param name="to">Finals value</param>
        /// <param name="duration">Interpolation duration</param>
        public static Interpolable DOColor(this Camera camera, Color from, Color to, float duration)
        {
            return Infinity.To(from, color => camera.backgroundColor = color, to, duration);
        }

        /// <summary>
        /// Fades this camera Color
        /// </summary>
        /// <param name="camera">This camera</param>
        /// <param name="to">Finals value</param>
        /// <param name="duration">Interpolation duration</param>
        public static Interpolable DOColor(this Camera camera, Color to, float duration)
        {
            return camera.DOColor(camera.backgroundColor, to, duration);
        }

        /// <summary>
        /// Picks a color in the given list and changes the color of this camera each '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="camera">this Camera</param>
        /// <param name="colors">Color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this Camera camera, List<Color> colors, float interval, float duration)
        {
            Interpolable interpolation = null;
            interpolation = camera.DOColor(colors.Random(), duration).OnComplete((arg) =>
            {
                colors.Shuffle();
                interpolation.SetNewStart(colors.Random());
                interpolation.Reverse();
            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(camera.gameObject);

            return interpolation;
        }

        /// <summary>
        /// Picks a color in the given list and changes the color of this camera each '<paramref name="interval"/>' seconds.
        /// </summary>
        /// <param name="camera">this Camera</param>
        /// <param name="colors">Color list</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOPickColor(this Camera camera, Color[] colors, float interval, float duration)
        {
            Interpolable interpolation = null;
            interpolation = camera.DOColor(colors.Random(), duration).OnComplete((arg) =>
            {
                colors.Shuffle();
                interpolation.SetNewStart(colors.Random());
                interpolation.Reverse();
            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(camera.gameObject);
            return interpolation;
        }

        /// <summary>
        /// Changes the color of the camera each time <paramref name="interval"/> seconds.
        /// </summary>
        /// <param name="camera">this Camera</param>
        /// <param name="interval">The interval</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DORandomColor(this Camera camera, float interval, float duration)
        {
            Interpolable interpolation = null;
            interpolation = camera.DOColor(Infinity.RandomColor(), duration).OnComplete((arg) =>
            {
                interpolation.SetNewStart(Infinity.RandomColor());
                interpolation.Reverse();
            }).SetRepeatInterval(interval).SetRepeat(-1).SetGameObject(camera.gameObject);
            return interpolation;
        }

        /// <summary>
        /// Changes smoothly the far value of this camera.
        /// </summary>
        /// <param name="camera">This camera</param>
        /// <param name="from">Starts value</param>
        /// <param name="to">Finals value</param>
        /// <param name="duration">Interpolation duration</param>
        public static Interpolable DOFar(this Camera camera, float from, float to, float duration)
        {
            return Infinity.To(from, value => camera.farClipPlane = value, to, duration);
        }

        /// <summary>
        /// Changes smoothly the far value of this camera.
        /// </summary>
        /// <param name="camera">This camera</param>
        /// <param name="to">Finals value</param>
        /// <param name="duration">Interpolation duration</param>
        public static Interpolable DOFar(this Camera camera, float to, float duration)
        {
            return camera.DOFar(camera.farClipPlane, to, duration);
        }

        /// <summary>
        /// Changes smoothly the near value of this camera.
        /// </summary>
        /// <param name="camera">This camera</param>
        /// <param name="from">Starts value</param>
        /// <param name="to">Finals value</param>
        /// <param name="duration">Interpolation duration</param>
        public static Interpolable DONear(this Camera camera, float from, float to, float duration)
        {
            return Infinity.To(from, value => camera.nearClipPlane = value, to, duration);
        }

        /// <summary>
        /// Changes smoothly the near value of this camera.
        /// </summary>
        /// <param name="camera">This camera</param>>
        /// <param name="to">Finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DONear(this Camera camera, float to, float duration)
        {
            return camera.DOFar(camera.nearClipPlane, to, duration);
        }

        /// <summary>
        /// Changes smoothly the orthographic size of this camera.
        /// </summary>
        /// <param name="camera">This camera</param>
        /// <param name="from">Starts value</param>
        /// <param name="to">Finals value</param>
        /// <param name="duration">Interpolation duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOOrtho(this Camera camera, float from, float to, float duration)
        {
            return Infinity.To(from, value => camera.orthographicSize = value, to, duration);
        }

        /// <summary>
        /// Changes smoothly the orthographic size of this camera.
        /// </summary>
        /// <param name="camera">This camera</param>
        /// <param name="to">Finals value</param>
        /// <param name="duration"></param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOOrtho(this Camera camera, float to, float duration)
        {
            return camera.DOFar(camera.orthographicSize, to, duration);
        }

        /// <summary>
        /// Changes smoothly the field of view of this camera.
        /// </summary>
        /// <param name="camera">This camera</param>
        /// <param name="from">Starts value</param>
        /// <param name="to">Finals value</param>
        /// <param name="duration">Interpolation duration</param>
        public static Interpolable DOFieldOfView(this Camera camera, float from, float to, float duration)
        {
            return Infinity.To(from, value => camera.fieldOfView = value, to, duration);
        }

        /// <summary>
        /// Changes smoothly the field of view of this camera.
        /// </summary>
        /// <param name="camera">This camera</param>
        /// <param name="to">Finals value</param>
        /// <param name="duration">Interpolation duration</param>
        public static Interpolable DOFieldOfViewTo(this Camera camera, float to, float duration)
        {
            return camera.DOFar(camera.fieldOfView, to, duration);
        }

        /// <summary>
        /// Shakes this camera rotation
        /// </summary>
        /// <param name="camera">this camera</param>
        /// <param name="strength">Shake strength</param>
        /// <param name="amount">Shake amount</param>
        /// <param name="duration">Shake duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOShakeRotation(this Camera camera, Vector3 strength, float amount, float duration)
        {
            return camera.transform.DOShakeRotation(strength, amount, duration);

        }

        /// <summary>
        /// Shakes this camera position
        /// </summary>
        /// <param name="camera">this camera</param>
        /// <param name="strength">Shake strength</param>
        /// <param name="amount">Shake amount</param>
        /// <param name="duration">Shake duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOShakePosition(this Camera camera, Vector3 strength, float amount, float duration)
        {
            return camera.transform.DOShakePosition(strength, amount, duration);

        }

        /// <summary>
        /// Shakes this camera scale
        /// </summary>
        /// <param name="camera">this camera</param>
        /// <param name="strength">Shake strength</param>
        /// <param name="amount">Shake amount</param>
        /// <param name="duration">Shake duration</param>
        /// <returns>Object of type Interpolation</returns>
        public static Interpolable DOShakeScale(this Camera camera, Vector3 strength, float amount, float duration)
        {
            return camera.transform.DOShakeScale(strength, amount, duration);

        }

        #endregion Methods
    }
}
