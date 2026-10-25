#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityInput
    *@{*/
    /// <summary>
    /// Центральный диспетчер пользовательского ввода
    /// </summary>
    /// <remarks>
    /// <para>
    /// Реализация центрального диспетчера пользовательского ввода который обобщает и абстрагирует ввод от источника данных,
    /// устройств и различных платформ и дополнительно представляет его в виде конкретных событий.
    /// </para>
    /// <para>
    /// Управляется центральным диспетчером <see cref="LotusSystemDispatcher"/>.
    /// Если центральный диспетчер не используется, то методы нужно вызывать вручную в соответствующих местах
    /// </para>
    /// </remarks>
    public static class XInputDispatcher
    {
        #region Fields
        /// <summary>
        /// Положение указателя в экранных координатах
        /// </summary>
        /// <remarks>
        /// Начало координат верхний левый угол экрана
        /// </remarks>
        public static Vector2 PositionPointer;

        /// <summary>
        /// Положение указателя (второго) в экранных координатах (для мобильных платформ)
        /// </summary>
        /// <remarks>
        /// Начало координат верхний левый угол экрана
        /// </remarks>
        public static Vector2 PositionPointer2;

        /// <summary>
        /// Положение указателя в экранных координатах когда была нажата левая кнопка мыши
        /// </summary>
        public static Vector2 PositionPointerLeftDown;

        /// <summary>
        /// Положение указателя в экранных координатах когда была отпущена левая кнопка мыши
        /// </summary>
        public static Vector2 PositionPointerLeftUp;

        /// <summary>
        /// Положение указателя в экранных координатах когда была нажата правая кнопка мыши
        /// </summary>
        public static Vector2 PositionPointerRightDown;

        /// <summary>
        /// Положение указателя в экранных координатах когда была отпущена правая кнопка мыши
        /// </summary>
        public static Vector2 PositionPointerRightUp;

        /// <summary>
        /// Смещение указателя
        /// </summary>
        public static Vector2 DeltaPointer;

        /// <summary>
        /// Смещение указателя (второго) (для мобильных платформ)
        /// </summary>
        public static Vector2 DeltaPointer2;

        // Обработчики событий
        private static List<ILotusPointerDown> _pointersDown;
        private static List<ILotusPointerPress> _pointersPress;
        private static List<ILotusPointerMove> _pointersMove;
        private static List<ILotusPointerUp> _pointersUp;
        private static Dictionary<string, ILotusVirtualButton> _buttons;
        private static Dictionary<string, ILotusVirtualAxis> _axes;
        private static Dictionary<string, ILotusVirtualJoystick> _joysticks;

        // Основная и дополнительная оси управления
#if UNITY_EDITOR || UNITY_STANDALONE
        private static readonly string AxisFirstMainHorizontal = XAxis.HORIZONTAL;
        private static readonly string AxisFirstMainVertical = XAxis.VERTICAL;
        private static readonly string AxisSecondMainHorizontal = XAxis.MOUSE_X;
        private static readonly string AxisSecondMainVertical = XAxis.MOUSE_Y;
#else
        public static ILotusVirtualJoystick AxisFirstMainJoystick = null;
        public static ILotusVirtualJoystick AxisSecondMainJoystick = null;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Статус смещение указателя
        /// </summary>
        public static bool IsDeltaMove
        {
            get { return DeltaPointer.sqrMagnitude > 0.1f; }
        }

        /// <summary>
        /// Смещение указателя по X
        /// </summary>
        public static float DeltaPointerX
        {
            get { return DeltaPointer.x; }
        }

        /// <summary>
        /// Смещение указателя по Y
        /// </summary>
        public static float DeltaPointerY
        {
            get { return DeltaPointer.y; }
        }

        /// <summary>
        /// Список обработчиков событий на событие <see cref="ILotusPointerDown.PointerDown(int)"/> 
        /// </summary>
        public static List<ILotusPointerDown> PointersDown
        {
            get
            {
                if (_pointersDown == null)
                {
                    _pointersDown = new List<ILotusPointerDown>();
                }
                return (_pointersDown);
            }
        }

        /// <summary>
        /// Список обработчиков событий на событие <see cref="ILotusPointerPress.PointerPress(int)"/> 
        /// </summary>
        public static List<ILotusPointerPress> PointersPress
        {
            get
            {
                if (_pointersPress == null)
                {
                    _pointersPress = new List<ILotusPointerPress>();
                }
                return (_pointersPress);
            }
        }

        /// <summary>
        /// Список обработчиков событий на событие <see cref="ILotusPointerMove.PointerMove(int)"/>
        /// </summary>
        public static List<ILotusPointerMove> PointersMove
        {
            get
            {
                if (_pointersMove == null)
                {
                    _pointersMove = new List<ILotusPointerMove>();
                }
                return (_pointersMove);
            }
        }

        /// <summary>
        /// Список обработчиков событий на событие <see cref="ILotusPointerUp.PointerUp(int)"/>
        /// </summary>
        public static List<ILotusPointerUp> PointersUp
        {
            get
            {
                if (_pointersUp == null)
                {
                    _pointersUp = new List<ILotusPointerUp>();
                }
                return (_pointersUp);
            }
        }

        /// <summary>
        /// Список виртуальных кнопок
        /// </summary>
        public static Dictionary<string, ILotusVirtualButton> Buttons
        {
            get
            {
                if (_buttons == null)
                {
                    _buttons = new Dictionary<string, ILotusVirtualButton>();
                }
                return (_buttons);
            }
        }

        /// <summary>
        /// Список виртуальных осей
        /// </summary>
        public static Dictionary<string, ILotusVirtualAxis> Axes
        {
            get
            {
                if (_axes == null)
                {
                    _axes = new Dictionary<string, ILotusVirtualAxis>();
                }
                return _axes;
            }
        }

        /// <summary>
        /// Список виртуальных осей
        /// </summary>
        [Obsolete("Опечатка в имени. Используйте Axes")]
        public static Dictionary<string, ILotusVirtualAxis> Axises
        {
            get { return Axes; }
        }

        /// <summary>
        /// Список виртуальных джойстиков
        /// </summary>
        public static Dictionary<string, ILotusVirtualJoystick> Joysticks
        {
            get
            {
                if (_joysticks == null)
                {
                    _joysticks = new Dictionary<string, ILotusVirtualJoystick>();
                }
                return (_joysticks);
            }
        }
        #endregion

        #region Cursor
        /// <summary>
        /// Масштаб дельты мыши, близкий к default sensitivity оси Mouse X/Y.
        /// </summary>
        public const float MouseAxisScale = 0.1f;

        /// <summary>
        /// Положение курсора в экранных координатах (начало — левый нижний угол).
        /// </summary>
        public static Vector3 MousePosition
        {
            get
            {
                var mouse = Mouse.current;
                if (mouse != null)
                {
                    var pos = mouse.position.ReadValue();
                    return new Vector3(pos.x, pos.y, 0f);
                }

                var touch = Touchscreen.current;
                if (touch != null && touch.primaryTouch.press.isPressed)
                {
                    var pos = touch.primaryTouch.position.ReadValue();
                    return new Vector3(pos.x, pos.y, 0f);
                }

                return Vector3.zero;
            }
        }

        public static bool GetMouseButton(int button)
        {
            var control = GetMouseButtonControl(button);
            return control != null && control.isPressed;
        }

        public static bool GetMouseButtonDown(int button)
        {
            var control = GetMouseButtonControl(button);
            return control != null && control.wasPressedThisFrame;
        }

        public static bool GetMouseButtonUp(int button)
        {
            var control = GetMouseButtonControl(button);
            return control != null && control.wasReleasedThisFrame;
        }

        public static bool IsMouseButton(TMouseButtonInput mouseButton)
        {
            return mouseButton != TMouseButtonInput.None && GetMouseButton((int)mouseButton);
        }

        public static bool IsMouseButtonDown(TMouseButtonInput mouseButton)
        {
            return mouseButton != TMouseButtonInput.None && GetMouseButtonDown((int)mouseButton);
        }

        public static bool IsMouseButtonUp(TMouseButtonInput mouseButton)
        {
            return mouseButton != TMouseButtonInput.None && GetMouseButtonUp((int)mouseButton);
        }

        private static Vector2 ReadMouseDelta()
        {
            var mouse = Mouse.current;
            return mouse != null ? mouse.delta.ReadValue() : Vector2.zero;
        }

        private static float ReadMouseScroll()
        {
            var mouse = Mouse.current;
            if (mouse == null)
            {
                return 0f;
            }

            return mouse.scroll.ReadValue().y / 120f;
        }

        private static ButtonControl GetMouseButtonControl(int button)
        {
            var mouse = Mouse.current;
            if (mouse == null)
            {
                return null;
            }

            switch (button)
            {
                case 0: return mouse.leftButton;
                case 1: return mouse.rightButton;
                case 2: return mouse.middleButton;
                case 3: return mouse.forwardButton;
                case 4: return mouse.backButton;
                default: return null;
            }
        }

        private static bool TryGetMouseButtonControl(KeyCode keyCode, out ButtonControl button)
        {
            button = null;
            switch (keyCode)
            {
                case KeyCode.Mouse0: button = GetMouseButtonControl(0); break;
                case KeyCode.Mouse1: button = GetMouseButtonControl(1); break;
                case KeyCode.Mouse2: button = GetMouseButtonControl(2); break;
                case KeyCode.Mouse3: button = GetMouseButtonControl(3); break;
                case KeyCode.Mouse4: button = GetMouseButtonControl(4); break;
            }

            return button != null;
        }
        #endregion

        #region Touch
        public static int TouchCount
        {
            get
            {
                var screen = Touchscreen.current;
                if (screen == null)
                {
                    return 0;
                }

                var count = 0;
                foreach (var t in screen.touches)
                {
                    if (IsActiveTouch(t))
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        public static bool TryGetTouch(int index, out Vector2 position, out Vector2 deltaPosition, out UnityEngine.InputSystem.TouchPhase phase)
        {
            position = Vector2.zero;
            deltaPosition = Vector2.zero;
            phase = UnityEngine.InputSystem.TouchPhase.Canceled;

            var screen = Touchscreen.current;
            if (screen == null || index < 0)
            {
                return false;
            }

            var current = 0;
            foreach (var t in screen.touches)
            {
                if (!IsActiveTouch(t))
                {
                    continue;
                }

                if (current == index)
                {
                    position = t.position.ReadValue();
                    deltaPosition = t.delta.ReadValue();
                    phase = ToTouchPhase(t);
                    return true;
                }

                current++;
            }

            return false;
        }

        private static bool IsActiveTouch(TouchControl touch)
        {
            return touch.press.isPressed || touch.press.wasPressedThisFrame || touch.press.wasReleasedThisFrame;
        }

        private static UnityEngine.InputSystem.TouchPhase ToTouchPhase(TouchControl touch)
        {
            if (touch.press.wasPressedThisFrame)
            {
                return UnityEngine.InputSystem.TouchPhase.Began;
            }

            if (touch.press.wasReleasedThisFrame)
            {
                return UnityEngine.InputSystem.TouchPhase.Ended;
            }

            if (touch.press.isPressed)
            {
                return touch.delta.ReadValue().sqrMagnitude > 0.01f ? UnityEngine.InputSystem.TouchPhase.Moved : UnityEngine.InputSystem.TouchPhase.Stationary;
            }

            return UnityEngine.InputSystem.TouchPhase.Canceled;
        }
        #endregion

        #region Keyboard
        public static bool GetKey(KeyCode keyCode)
        {
            if (TryGetMouseButtonControl(keyCode, out var button))
            {
                return button.isPressed;
            }

            var keyboard = Keyboard.current;
            if (keyboard == null || !TryMapKey(keyCode, out var key))
            {
                return false;
            }

            return keyboard[key].isPressed;
        }

        public static bool GetKeyDown(KeyCode keyCode)
        {
            if (TryGetMouseButtonControl(keyCode, out var button))
            {
                return button.wasPressedThisFrame;
            }

            var keyboard = Keyboard.current;
            if (keyboard == null || !TryMapKey(keyCode, out var key))
            {
                return false;
            }

            return keyboard[key].wasPressedThisFrame;
        }

        public static bool GetKeyUp(KeyCode keyCode)
        {
            if (TryGetMouseButtonControl(keyCode, out var button))
            {
                return button.wasReleasedThisFrame;
            }

            var keyboard = Keyboard.current;
            if (keyboard == null || !TryMapKey(keyCode, out var key))
            {
                return false;
            }

            return keyboard[key].wasReleasedThisFrame;
        }

        private static bool TryMapKey(KeyCode keyCode, out Key key)
        {
            key = Key.None;

            if (keyCode >= KeyCode.A && keyCode <= KeyCode.Z)
            {
                key = (Key)((int)Key.A + (keyCode - KeyCode.A));
                return true;
            }

            if (keyCode >= KeyCode.Alpha1 && keyCode <= KeyCode.Alpha9)
            {
                key = (Key)((int)Key.Digit1 + (keyCode - KeyCode.Alpha1));
                return true;
            }

            if (keyCode >= KeyCode.Keypad0 && keyCode <= KeyCode.Keypad9)
            {
                key = (Key)((int)Key.Numpad0 + (keyCode - KeyCode.Keypad0));
                return true;
            }

            if (keyCode >= KeyCode.F1 && keyCode <= KeyCode.F12)
            {
                key = (Key)((int)Key.F1 + (keyCode - KeyCode.F1));
                return true;
            }

            switch (keyCode)
            {
                case KeyCode.Alpha0: key = Key.Digit0; return true;
                case KeyCode.Space: key = Key.Space; return true;
                case KeyCode.Return: key = Key.Enter; return true;
                case KeyCode.KeypadEnter: key = Key.NumpadEnter; return true;
                case KeyCode.Escape: key = Key.Escape; return true;
                case KeyCode.Tab: key = Key.Tab; return true;
                case KeyCode.Backspace: key = Key.Backspace; return true;
                case KeyCode.Delete: key = Key.Delete; return true;
                case KeyCode.Insert: key = Key.Insert; return true;
                case KeyCode.Home: key = Key.Home; return true;
                case KeyCode.End: key = Key.End; return true;
                case KeyCode.PageUp: key = Key.PageUp; return true;
                case KeyCode.PageDown: key = Key.PageDown; return true;
                case KeyCode.LeftArrow: key = Key.LeftArrow; return true;
                case KeyCode.RightArrow: key = Key.RightArrow; return true;
                case KeyCode.UpArrow: key = Key.UpArrow; return true;
                case KeyCode.DownArrow: key = Key.DownArrow; return true;
                case KeyCode.LeftShift: key = Key.LeftShift; return true;
                case KeyCode.RightShift: key = Key.RightShift; return true;
                case KeyCode.LeftControl: key = Key.LeftCtrl; return true;
                case KeyCode.RightControl: key = Key.RightCtrl; return true;
                case KeyCode.LeftAlt: key = Key.LeftAlt; return true;
                case KeyCode.RightAlt: key = Key.RightAlt; return true;
                case KeyCode.LeftCommand: key = Key.LeftCommand; return true;
                case KeyCode.RightCommand: key = Key.RightCommand; return true;
                case KeyCode.LeftWindows: key = Key.LeftWindows; return true;
                case KeyCode.RightWindows: key = Key.RightWindows; return true;
                case KeyCode.CapsLock: key = Key.CapsLock; return true;
                case KeyCode.Numlock: key = Key.NumLock; return true;
                case KeyCode.KeypadPeriod: key = Key.NumpadPeriod; return true;
                case KeyCode.KeypadDivide: key = Key.NumpadDivide; return true;
                case KeyCode.KeypadMultiply: key = Key.NumpadMultiply; return true;
                case KeyCode.KeypadMinus: key = Key.NumpadMinus; return true;
                case KeyCode.KeypadPlus: key = Key.NumpadPlus; return true;
                case KeyCode.Minus: key = Key.Minus; return true;
                case KeyCode.Equals: key = Key.Equals; return true;
                case KeyCode.LeftBracket: key = Key.LeftBracket; return true;
                case KeyCode.RightBracket: key = Key.RightBracket; return true;
                case KeyCode.Semicolon: key = Key.Semicolon; return true;
                case KeyCode.Quote: key = Key.Quote; return true;
                case KeyCode.BackQuote: key = Key.Backquote; return true;
                case KeyCode.Comma: key = Key.Comma; return true;
                case KeyCode.Period: key = Key.Period; return true;
                case KeyCode.Slash: key = Key.Slash; return true;
                case KeyCode.Backslash: key = Key.Backslash; return true;
                default:
                    return false;
            }
        }
        #endregion

        #region Common

        /// <summary>
        /// Перезапуск данных центрального диспетчера в режиме редактора
        /// </summary>

        public static void OnResetEditor()
        {
        }


        /// <summary>
        /// Первичная инициализация данных центрального диспетчера пользовательского ввода
        /// </summary>

        public static void OnInit()
        {
        }


        /// <summary>
        /// Запуск центрального диспетчера пользовательского ввода
        /// </summary>

        public static void OnStart()
        {
        }


        /// <summary>
        /// Обновление центрального диспетчера каждый кадр
        /// </summary>

        public static void OnUpdate()
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            UpdateDesktopInput();

#else
            UpdateMobileInput();
#endif
        }


        /// <summary>
        /// Обновление центрального диспетчера каждый кадр после (после Update)
        /// </summary>

        public static void OnLateUpdate()
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            Vector2 mousePoint = MousePosition;
            mousePoint.y = Screen.height - mousePoint.y;
            DeltaPointer = mousePoint - PositionPointer;
            PositionPointer = mousePoint;
#else
#endif
        }

        /// <summary>
        /// Обновление данных стандартного ввода
        /// </summary>

        public static void UpdateDesktopInput()
        {
            if (GetMouseButtonDown(0))
            {
                Vector2 mousePoint = MousePosition;
                mousePoint.y = Screen.height - mousePoint.y;
                PositionPointerLeftDown = mousePoint;

                for (var i = 0; i < PointersDown.Count; i++)
                {
                    PointersDown[i].PointerDown(0);
                }
            }

            if (GetMouseButtonDown(1))
            {
                Vector2 mousePoint = MousePosition;
                mousePoint.y = Screen.height - mousePoint.y;
                PositionPointerRightDown = mousePoint;

                for (var i = 0; i < PointersDown.Count; i++)
                {
                    PointersDown[i].PointerDown(1);
                }
            }

            if (GetMouseButtonDown(2))
            {
                for (var i = 0; i < PointersDown.Count; i++)
                {
                    PointersDown[i].PointerDown(2);
                }
            }

            if (GetMouseButton(0))
            {
                for (var i = 0; i < PointersPress.Count; i++)
                {
                    PointersPress[i].PointerPress(0);
                }
            }

            if (GetMouseButton(1))
            {
                for (var i = 0; i < PointersPress.Count; i++)
                {
                    PointersPress[i].PointerPress(1);
                }
            }

            if (GetMouseButton(2))
            {
                for (var i = 0; i < PointersPress.Count; i++)
                {
                    PointersPress[i].PointerPress(2);
                }
            }

            if (GetMouseButtonUp(0))
            {
                Vector2 mousePoint = MousePosition;
                mousePoint.y = Screen.height - mousePoint.y;
                PositionPointerLeftUp = mousePoint;

                for (var i = 0; i < PointersUp.Count; i++)
                {
                    PointersUp[i].PointerUp(0);
                }
            }

            if (GetMouseButtonUp(1))
            {
                Vector2 mousePoint = MousePosition;
                mousePoint.y = Screen.height - mousePoint.y;
                PositionPointerRightUp = mousePoint;

                for (var i = 0; i < PointersUp.Count; i++)
                {
                    PointersUp[i].PointerUp(1);
                }
            }

            if (GetMouseButtonUp(2))
            {
                for (var i = 0; i < PointersUp.Count; i++)
                {
                    PointersUp[i].PointerUp(2);
                }
            }

            if (DeltaPointer.sqrMagnitude > 0.01f)
            {
                var pointer = -1;
                if (GetMouseButton(0))
                {
                    pointer = 0;
                }
                if (GetMouseButton(1))
                {
                    pointer = 1;
                }
                for (var i = 0; i < PointersMove.Count; i++)
                {
                    PointersMove[i].PointerMove(pointer);
                }
            }
        }


        /// <summary>
        /// Обновление данных мобильного ввода
        /// </summary>

        public static void UpdateMobileInput()
        {
            var touchCount = TouchCount;
            for (var it = 0; it < touchCount; it++)
            {
                if (!TryGetTouch(it, out var position, out var deltaPosition, out var phase))
                {
                    continue;
                }

                if (it == 0)
                {
                    var mousePoint = position;
                    mousePoint.y = Screen.height - mousePoint.y;
                    PositionPointer = mousePoint;
                    DeltaPointer = deltaPosition;
                }
                if (it == 1)
                {
                    var mousePoint = position;
                    mousePoint.y = Screen.height - mousePoint.y;
                    PositionPointer2 = mousePoint;
                    DeltaPointer2 = deltaPosition;
                }

                if (phase == UnityEngine.InputSystem.TouchPhase.Began)
                {
                    if (it == 0)
                    {
                        var mousePoint = position;
                        mousePoint.y = Screen.height - mousePoint.y;
                        PositionPointerLeftDown = mousePoint;
                    }

                    for (var i = 0; i < PointersDown.Count; i++)
                    {
                        PointersDown[i].PointerDown(it);
                    }
                }

                if (phase == UnityEngine.InputSystem.TouchPhase.Stationary || phase == UnityEngine.InputSystem.TouchPhase.Moved)
                {
                    for (var i = 0; i < PointersPress.Count; i++)
                    {
                        PointersPress[i].PointerPress(it);
                    }
                }

                if (phase == UnityEngine.InputSystem.TouchPhase.Moved)
                {
                    for (var i = 0; i < PointersMove.Count; i++)
                    {
                        PointersMove[i].PointerMove(it);
                    }
                }

                if (phase == UnityEngine.InputSystem.TouchPhase.Ended || phase == UnityEngine.InputSystem.TouchPhase.Canceled)
                {
                    if (it == 0)
                    {
                        var mousePoint = position;
                        mousePoint.y = Screen.height - mousePoint.y;
                        PositionPointerLeftUp = mousePoint;
                    }

                    for (var i = 0; i < PointersUp.Count; i++)
                    {
                        PointersUp[i].PointerUp(it);
                    }
                }
            }
        }

        /// <summary>
        /// Получение направление перемещения указателя посредством <see cref="UnityEngine.EventSystems.EventSystem"/>
        /// </summary>
        /// <param name="dx">Дельта смещения по оси X</param>
        /// <param name="dy">Дельта смещения по оси Y</param>
        /// <returns>Направление перемещения указателя</returns>

        public static TDirection2D GetDirectionFromInputEvent(float dx, float dy)
        {
            if (Mathf.Abs(dx) > Mathf.Abs(dy))
            {
                if (dx > 0)
                {
                    return (TDirection2D.Right);
                }
                else
                {
                    return (TDirection2D.Left);
                }
            }
            else
            {
                if (dy > 0)
                {
                    return (TDirection2D.Up);
                }
                else
                {
                    return (TDirection2D.Down);
                }
            }
        }


        /// <summary>
        /// Получение направление перемещения указателя посредством <see cref="UnityEngine.Event"/>
        /// </summary>
        /// <param name="dx">Дельта смещения по оси X</param>
        /// <param name="dy">Дельта смещения по оси Y</param>
        /// <returns>Направление перемещения указателя</returns>

        public static TDirection2D GetDirectionFromInputClassic(float dx, float dy)
        {
            if (Mathf.Abs(dx) > Mathf.Abs(dy))
            {
                if (dx > 0)
                {
                    return (TDirection2D.Right);
                }
                else
                {
                    return (TDirection2D.Left);
                }
            }
            else
            {
                if (dy > 0)
                {
                    return (TDirection2D.Down);
                }
                else
                {
                    return (TDirection2D.Up);
                }
            }
        }

        /// <summary>
        /// Регистрация подписки на обработку события нажатия
        /// </summary>
        /// <param name="pointerDown">Интерфейс нажатия</param>

        public static void RegisterPointerDown(ILotusPointerDown pointerDown)
        {
            PointersDown.Add(pointerDown);
        }


        /// <summary>
        /// Регистрация подписки на обработку события удерживания
        /// </summary>
        /// <param name="pointerPress">Интерфейс удерживания</param>

        public static void RegisterPointerPress(ILotusPointerPress pointerPress)
        {
            PointersPress.Add(pointerPress);
        }


        /// <summary>
        /// Регистрация подписки на обработку события перемещения
        /// </summary>
        /// <param name="pointerMove">Интерфейс перемещения</param>

        public static void RegisterPointerMove(ILotusPointerMove pointerMove)
        {
            PointersMove.Add(pointerMove);
        }


        /// <summary>
        /// Регистрация подписки на обработку события отпускания
        /// </summary>
        /// <param name="pointerUp">Интерфейс отпускания</param>

        public static void RegisterPointerUp(ILotusPointerUp pointerUp)
        {
            PointersUp.Add(pointerUp);
        }


        /// <summary>
        /// Отмена регистрации подписки на обработку события нажатия
        /// </summary>
        /// <param name="pointerDown">Интерфейс нажатия</param>
        public static void UnregisterPointerDown(ILotusPointerDown pointerDown)
        {
            PointersDown.Remove(pointerDown);
        }

        /// <summary>
        /// Отмена регистрации подписки на обработку события нажатия
        /// </summary>
        /// <param name="pointerDown">Интерфейс нажатия</param>
        [Obsolete("Опечатка в имени. Используйте UnregisterPointerDown")]
        public static void UnRegisterPointerDown(ILotusPointerDown pointerDown)
        {
            UnregisterPointerDown(pointerDown);
        }

        /// <summary>
        /// Отмена регистрации подписки на обработку события удерживания
        /// </summary>
        /// <param name="pointerPress">Интерфейс удерживания</param>
        public static void UnregisterPointerPress(ILotusPointerPress pointerPress)
        {
            PointersPress.Remove(pointerPress);
        }

        /// <summary>
        /// Отмена регистрации подписки на обработку события удерживания
        /// </summary>
        /// <param name="pointerPress">Интерфейс удерживания</param>
        [Obsolete("Опечатка в имени. Используйте UnregisterPointerPress")]
        public static void UnRegisterPointerPress(ILotusPointerPress pointerPress)
        {
            UnregisterPointerPress(pointerPress);
        }

        /// <summary>
        /// Отмена регистрации подписки на обработку события перемещения
        /// </summary>
        /// <param name="pointerMove">Интерфейс перемещения</param>
        public static void UnregisterPointerMove(ILotusPointerMove pointerMove)
        {
            PointersMove.Remove(pointerMove);
        }

        /// <summary>
        /// Отмена регистрации подписки на обработку события перемещения
        /// </summary>
        /// <param name="pointerMove">Интерфейс перемещения</param>
        [Obsolete("Опечатка в имени. Используйте UnregisterPointerMove")]
        public static void UnRegisterPointerMove(ILotusPointerMove pointerMove)
        {
            UnregisterPointerMove(pointerMove);
        }

        /// <summary>
        /// Отмена регистрации на обработку события отпускания
        /// </summary>
        /// <param name="pointerUp">Интерфейс отпускания</param>
        public static void UnregisterPointerUp(ILotusPointerUp pointerUp)
        {
            PointersUp.Remove(pointerUp);
        }

        /// <summary>
        /// Отмена регистрации на обработку события отпускания
        /// </summary>
        /// <param name="pointerUp">Интерфейс отпускания</param>
        [Obsolete("Опечатка в имени. Используйте UnregisterPointerUp")]
        public static void UnRegisterPointerUp(ILotusPointerUp pointerUp)
        {
            UnregisterPointerUp(pointerUp);
        }

        /// <summary>
        /// Получение статуса удержания нажатой кнопки
        /// </summary>
        /// <param name="name">Имя кнопки</param>
        /// <returns>Статус удержания нажатой кнопки</returns>

        public static bool GetButton(string name)
        {
            if (Buttons.TryGetValue(name, out var button))
            {
                return button.IsButtonPressed;
            }

            return ReadHardwareButton(name);
        }


        /// <summary>
        /// Получение статуса нажатия кнопки
        /// </summary>
        /// <param name="name">Имя кнопки</param>
        /// <returns>Статус нажатия кнопки</returns>

        public static bool GetButtonDown(string name)
        {
            if (Buttons.TryGetValue(name, out var button))
            {
                return button.IsButtonDown;
            }

            return ReadHardwareButtonDown(name);
        }


        /// <summary>
        /// Получение статуса отпускания кнопки
        /// </summary>
        /// <param name="name">Имя кнопки</param>
        /// <returns>Статус отпускания кнопки</returns>

        public static bool GetButtonUp(string name)
        {
            if (Buttons.TryGetValue(name, out var button))
            {
                return button.IsButtonUp;
            }

            return ReadHardwareButtonUp(name);
        }


        /// <summary>
        /// Получение смещения/перемещения по одномерной оси
        /// </summary>
        /// <param name="name">Имя оси</param>
        /// <returns>Смещения/перемещения по одномерной оси в относительных единицах</returns>

        public static float GetAxis(string name)
        {
            if (Axes.TryGetValue(name, out var axis))
            {
                return axis.GetAxis();
            }

            return ReadHardwareAxis(name);
        }


        /// <summary>
        /// Получение смещения/перемещения по двумерной оси
        /// </summary>
        /// <param name="name">Имя оси</param>
        /// <returns>Смещения/перемещения по одномерной оси в относительных единицах</returns>

        public static Vector2 GetJoystick(string name)
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            return Vector2.zero;
#else
            // Ищем только среди зарегистрированных осей
            ILotusVirtualJoystick joystick = null;
            if (Joysticks.TryGetValue(name, out joystick))
            {
                return (joystick.GetAxis());
            }
            else
            {
                return (Vector2.zero);
            }
#endif
        }


        /// <summary>
        /// Получение смещения/перемещения по первой основной оси
        /// </summary>
        /// <returns>Смещение/перемещение по первой основной оси</returns>

        public static Vector2 GetAxisFirstMain()
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            return new Vector2(GetAxis(AxisFirstMainHorizontal), GetAxis(AxisFirstMainVertical));
#else
            return (AxisFirstMainJoystick.GetAxis());
#endif
        }


        /// <summary>
        /// Получение смещения/перемещения по горизонтали первой основной оси
        /// </summary>
        /// <returns>Смещение/перемещение по горизонтали</returns>

        public static float GetAxisFirstMainHorizontal()
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            return GetAxis(AxisFirstMainHorizontal);

#else
            return (AxisFirstMainJoystick.GetAxisHorizontal());
#endif
        }


        /// <summary>
        /// Получение смещения/перемещения по вертикали первой основной оси
        /// </summary>
        /// <returns>Смещение/перемещение по вертикали первой основной оси</returns>

        public static float GetAxisFirstMainVertical()
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            return GetAxis(AxisFirstMainVertical);

#else
            return (AxisFirstMainJoystick.GetAxisVertical());
#endif
        }


        /// <summary>
        /// Получение смещения/перемещения по второй основной оси
        /// </summary>
        /// <returns>Смещение/перемещение по второй основной оси</returns>

        public static Vector2 GetAxisSecondMain()
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            return new Vector2(GetAxis(AxisSecondMainHorizontal), GetAxis(AxisSecondMainVertical));
#else
            return (AxisSecondMainJoystick.GetAxis());
#endif
        }


        /// <summary>
        /// Получение смещения/перемещения по горизонтали второй основной оси
        /// </summary>
        /// <returns>Смещение/перемещение по горизонтали второй основной оси</returns>

        public static float GetAxisSecondMainHorizontal()
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            return GetAxis(AxisSecondMainHorizontal);

#else
            return (AxisSecondMainJoystick.GetAxisHorizontal());
#endif
        }


        /// <summary>
        /// Получение смещения/перемещения по вертикали второй основной оси
        /// </summary>
        /// <returns>Смещение/перемещение по вертикали второй основной оси</returns>

        public static float GetAxisSecondMainVertical()
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            return GetAxis(AxisSecondMainVertical);
#else
            return (AxisSecondMainJoystick.GetAxisVertical());
#endif
        }

        private static bool ReadHardwareButton(string name)
        {
            switch (name)
            {
                case XAxis.FIRE1:
                    return GetMouseButton(0) || GetKey(KeyCode.LeftControl);
                case XAxis.FIRE2:
                    return GetMouseButton(1) || GetKey(KeyCode.LeftAlt);
                case XAxis.FIRE3:
                    return GetMouseButton(2) || GetKey(KeyCode.LeftCommand);
                case XAxis.JUMP:
                    return GetKey(KeyCode.Space);
                case XAxis.SUBMIT:
                    return GetKey(KeyCode.Return) || GetKey(KeyCode.KeypadEnter);
                case XAxis.CANCEL:
                    return GetKey(KeyCode.Escape);
                default:
                    return false;
            }
        }

        private static bool ReadHardwareButtonDown(string name)
        {
            switch (name)
            {
                case XAxis.FIRE1:
                    return GetMouseButtonDown(0) || GetKeyDown(KeyCode.LeftControl);
                case XAxis.FIRE2:
                    return GetMouseButtonDown(1) || GetKeyDown(KeyCode.LeftAlt);
                case XAxis.FIRE3:
                    return GetMouseButtonDown(2) || GetKeyDown(KeyCode.LeftCommand);
                case XAxis.JUMP:
                    return GetKeyDown(KeyCode.Space);
                case XAxis.SUBMIT:
                    return GetKeyDown(KeyCode.Return) || GetKeyDown(KeyCode.KeypadEnter);
                case XAxis.CANCEL:
                    return GetKeyDown(KeyCode.Escape);
                default:
                    return false;
            }
        }

        private static bool ReadHardwareButtonUp(string name)
        {
            switch (name)
            {
                case XAxis.FIRE1:
                    return GetMouseButtonUp(0) || GetKeyUp(KeyCode.LeftControl);
                case XAxis.FIRE2:
                    return GetMouseButtonUp(1) || GetKeyUp(KeyCode.LeftAlt);
                case XAxis.FIRE3:
                    return GetMouseButtonUp(2) || GetKeyUp(KeyCode.LeftCommand);
                case XAxis.JUMP:
                    return GetKeyUp(KeyCode.Space);
                case XAxis.SUBMIT:
                    return GetKeyUp(KeyCode.Return) || GetKeyUp(KeyCode.KeypadEnter);
                case XAxis.CANCEL:
                    return GetKeyUp(KeyCode.Escape);
                default:
                    return false;
            }
        }

        private static float ReadHardwareAxis(string name)
        {
            switch (name)
            {
                case XAxis.HORIZONTAL:
                    return ReadComposite(
                        GetKey(KeyCode.A) || GetKey(KeyCode.LeftArrow),
                        GetKey(KeyCode.D) || GetKey(KeyCode.RightArrow),
                        GamepadAxisX());
                case XAxis.VERTICAL:
                    return ReadComposite(
                        GetKey(KeyCode.S) || GetKey(KeyCode.DownArrow),
                        GetKey(KeyCode.W) || GetKey(KeyCode.UpArrow),
                        GamepadAxisY());
                case XAxis.MOUSE_X:
                    return ReadMouseDelta().x * MouseAxisScale;
                case XAxis.MOUSE_Y:
                    return ReadMouseDelta().y * MouseAxisScale;
                case XAxis.MOUSE_SCROLL_WHEEL:
                    return ReadMouseScroll();
                case XAxis.FIRE1:
                case XAxis.FIRE2:
                case XAxis.FIRE3:
                case XAxis.JUMP:
                    return ReadHardwareButton(name) ? 1f : 0f;
                default:
                    return 0f;
            }
        }

        private static float ReadComposite(bool negative, bool positive, float analog)
        {
            var digital = 0f;
            if (positive)
            {
                digital += 1f;
            }

            if (negative)
            {
                digital -= 1f;
            }

            return Mathf.Abs(analog) > Mathf.Abs(digital) ? analog : digital;
        }

        private static float GamepadAxisX()
        {
            var pad = Gamepad.current;
            return pad != null ? pad.leftStick.x.ReadValue() : 0f;
        }

        private static float GamepadAxisY()
        {
            var pad = Gamepad.current;
            return pad != null ? pad.leftStick.y.ReadValue() : 0f;
        }
        #endregion
    }
    /**@}*/
}
#endif
