using InfinityEngine.DesignPatterns;
using InfinityEngine.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityEngine
{
    /// <summary>
    ///   Class in charge to handle input in the game
    /// </summary>
    public class InputManager : Singleton<InputManager>
    {
        public delegate void TouchLeft();

        public delegate void TouchRight();

        public delegate void TouchUp();

        public delegate void TouchDown();

        public delegate void TouchScreen();

        public enum MobileInput
        {
            SWIP,
            TAP
        }

        public bool BLOCK_INPUT;

        private MobileInput mobileInput = MobileInput.TAP;

        private Vector3 firstTouch;

        private Vector3 lastTouch;

        private float dragDistance;

        private List<Vector3> touchPositions = new List<Vector3>();

        /// <summary>
        /// Event trigger when touching the screen on the left part
        /// </summary>
        public static event TouchLeft OnTouchLeft;

        /// <summary>
        /// Event trigger when touching the screen on the right part
        /// </summary>
        public static event TouchRight OnTouchRight;

        /// <summary>
        /// Event trigger when touching the screen on the top part (not used here but you can use it )
        /// </summary>
        public static event TouchUp OnTouchUp;

        /// <summary>
        /// Event trigger when touching the screen on the top part (not used here but you can use it )
        /// </summary>
        public static event TouchDown OnTouchDown;

        /// <summary>
        /// Event trigger when touching the screen
        /// </summary>
        public static event TouchScreen OnTouchScreen;

        private void Awake()
        {
            dragDistance = (float)(Screen.height * 20 / 100);
        }

        private void Update()
        {
            if (!BLOCK_INPUT)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _OnTouchScreen();
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    _OnTouchLeft();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _OnTouchRight();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _OnTouchUp();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _OnTouchDown();
                }
            }
        }

        private void SwipDetection()
        {
            var touches = Input.touches;
            var num = 0;
            while (true)
            {
                if (num >= touches.Length)
                {
                    return;
                }
                var val = touches[num];
                if (val.phase == TouchPhase.Began)
                {
                    firstTouch = val.position;
                    lastTouch = val.position;
                }
                if (val.phase == TouchPhase.Moved)
                {
                    touchPositions.Add(val.position);
                }
                if (val.phase == TouchPhase.Ended)
                {
                    lastTouch = val.position;
                    if (Mathf.Abs(lastTouch.x - firstTouch.x) > dragDistance || Mathf.Abs(lastTouch.y - firstTouch.y) > dragDistance)
                    {
                        break;
                    }
                }
                num++;
            }
            if (Mathf.Abs(lastTouch.x - firstTouch.x) > Mathf.Abs(lastTouch.y - firstTouch.y))
            {
                if (lastTouch.x > firstTouch.x)
                {
                    Debugger.Log("Right Swipe");
                    _OnTouchRight();
                }
                else
                {
                    Debugger.Log("Left Swipe");
                    _OnTouchLeft();
                }
            }
            else if (lastTouch.y > firstTouch.y)
            {
                _OnTouchUp();
            }
            else
            {
                Debugger.Log("Down Swipe");
                _OnTouchDown();
            }
        }

        private void TapDetection()
        {
            var touchCount = Input.touchCount;
            if (touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var phase = touch.phase;
                if (phase == TouchPhase.Began)
                {
                    if (touch.position.x < Screen.width / 2f)
                    {
                        if (Screen.orientation == ScreenOrientation.Landscape)
                        {
                            _OnTouchLeft();
                        }
                        else
                        {
                            _OnTouchDown();
                        }
                    }
                    else if (touch.position.x >= Screen.width / 2f)
                    {
                        if (Screen.orientation == ScreenOrientation.Landscape)
                        {
                            _OnTouchRight();
                        }
                        else
                        {
                            _OnTouchUp();
                        }
                    }
                }
            }
        }

        private void _OnTouchScreen()
        {
            OnTouchScreen?.Invoke();
        }

        private void _OnTouchLeft()
        {
            _OnTouchScreen();
            if (!BLOCK_INPUT)
            {
                OnTouchLeft();
            }
        }

        private void _OnTouchRight()
        {
            _OnTouchScreen();
            if (!BLOCK_INPUT)
            {
                OnTouchRight?.Invoke();
            }
        }

        private void _OnTouchUp()
        {
            _OnTouchScreen();
            if (!BLOCK_INPUT)
            {
                OnTouchUp?.Invoke();
            }
        }

        private void _OnTouchDown()
        {
            _OnTouchScreen();
            if (!BLOCK_INPUT)
            {
                OnTouchDown?.Invoke();
            }
        }

        public static void SetMobileInputType(MobileInput type)
        {
            Instance.mobileInput = type;
        }

        private void OnGameStarted()
        {
            BLOCK_INPUT = false;
        }

        private void OnGameEnded()
        {
            BLOCK_INPUT = true;
        }
    }
}
