#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;

using UnityEngine;

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

        #region Dispatcher methods
        
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

            Vector2 mousePoint = Input.mousePosition;
            mousePoint.y = Screen.height - mousePoint.y;
            DeltaPointer = mousePoint - PositionPointer;
            PositionPointer = mousePoint;
#else
#endif
        }
        #endregion

        #region Main methods
        
        /// <summary>
        /// Обновление данных стандартного ввода
        /// </summary>
        
        public static void UpdateDesktopInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePoint = Input.mousePosition;
                mousePoint.y = Screen.height - mousePoint.y;
                PositionPointerLeftDown = mousePoint;

                for (var i = 0; i < PointersDown.Count; i++)
                {
                    PointersDown[i].PointerDown(0);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Vector2 mousePoint = Input.mousePosition;
                mousePoint.y = Screen.height - mousePoint.y;
                PositionPointerRightDown = mousePoint;

                for (var i = 0; i < PointersDown.Count; i++)
                {
                    PointersDown[i].PointerDown(1);
                }
            }

            if (Input.GetMouseButtonDown(2))
            {
                for (var i = 0; i < PointersDown.Count; i++)
                {
                    PointersDown[i].PointerDown(2);
                }
            }

            if (Input.GetMouseButton(0))
            {
                for (var i = 0; i < PointersPress.Count; i++)
                {
                    PointersPress[i].PointerPress(0);
                }
            }

            if (Input.GetMouseButton(1))
            {
                for (var i = 0; i < PointersPress.Count; i++)
                {
                    PointersPress[i].PointerPress(1);
                }
            }

            if (Input.GetMouseButton(2))
            {
                for (var i = 0; i < PointersPress.Count; i++)
                {
                    PointersPress[i].PointerPress(2);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePoint = Input.mousePosition;
                mousePoint.y = Screen.height - mousePoint.y;
                PositionPointerLeftUp = mousePoint;

                for (var i = 0; i < PointersUp.Count; i++)
                {
                    PointersUp[i].PointerUp(0);
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                Vector2 mousePoint = Input.mousePosition;
                mousePoint.y = Screen.height - mousePoint.y;
                PositionPointerRightUp = mousePoint;

                for (var i = 0; i < PointersUp.Count; i++)
                {
                    PointersUp[i].PointerUp(1);
                }
            }

            if (Input.GetMouseButtonUp(2))
            {
                for (var i = 0; i < PointersUp.Count; i++)
                {
                    PointersUp[i].PointerUp(2);
                }
            }

            if (DeltaPointer.sqrMagnitude > 0.01f)
            {
                var pointer = -1;
                if (Input.GetMouseButton(0))
                {
                    pointer = 0;
                }
                if (Input.GetMouseButton(1))
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
            if (Input.touchCount > 0)
            {
                Touch[] touches = Input.touches;
                for (var it = 0; it < touches.Length; it++)
                {
                    if (it == 0)
                    {
                        Vector2 mousePoint = touches[it].position;
                        mousePoint.y = Screen.height - mousePoint.y;
                        PositionPointer = mousePoint;
                        DeltaPointer = touches[it].deltaPosition;
                    }
                    if (it == 1)
                    {
                        Vector2 mousePoint = touches[it].position;
                        mousePoint.y = Screen.height - mousePoint.y;
                        PositionPointer2 = mousePoint;
                        DeltaPointer2 = touches[it].deltaPosition;
                    }

                    if (touches[it].phase == TouchPhase.Began)
                    {
                        if (it == 0)
                        {
                            Vector2 mousePoint = touches[it].position;
                            mousePoint.y = Screen.height - mousePoint.y;
                            PositionPointerLeftDown = mousePoint;
                        }

                        for (var i = 0; i < PointersDown.Count; i++)
                        {
                            PointersDown[i].PointerDown(it);
                        }
                    }

                    if (touches[it].phase == TouchPhase.Stationary || touches[it].phase == TouchPhase.Moved)
                    {
                        for (var i = 0; i < PointersPress.Count; i++)
                        {
                            PointersPress[i].PointerPress(it);
                        }
                    }

                    if (touches[it].phase == TouchPhase.Moved)
                    {
                        for (var i = 0; i < PointersMove.Count; i++)
                        {
                            PointersMove[i].PointerMove(it);
                        }
                    }

                    if (touches[it].phase == TouchPhase.Ended || touches[it].phase == TouchPhase.Canceled)
                    {
                        if (it == 0)
                        {
                            Vector2 mousePoint = touches[it].position;
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
        }

        
        /// <summary>
        /// Статус нажатия указанной кнопки мыши
        /// </summary>
        /// <param name="mouseButton">Кнопка мыши</param>
        /// <returns>Статус нажатия</returns>
        
        public static bool IsMouseButtonDown(TMouseButtonInput mouseButton)
        {
            if (mouseButton == TMouseButtonInput.Left && Input.GetMouseButtonDown(0))
            {
                return (true);
            }
            else
            {
                if (mouseButton == TMouseButtonInput.Right && Input.GetMouseButtonDown(1))
                {
                    return (true);
                }
                else
                {
                    if (mouseButton == TMouseButtonInput.Middle && Input.GetMouseButtonDown(2))
                    {
                        return (true);
                    }
                }
            }

            return (false);
        }

        
        /// <summary>
        /// Статус удержания указанной кнопки мыши
        /// </summary>
        /// <param name="mouseButton">Кнопка мыши</param>
        /// <returns>Статус удержания</returns>
        
        public static bool IsMouseButton(TMouseButtonInput mouseButton)
        {
            if (mouseButton == TMouseButtonInput.Left && Input.GetMouseButton(0))
            {
                return (true);
            }
            else
            {
                if (mouseButton == TMouseButtonInput.Right && Input.GetMouseButton(1))
                {
                    return (true);
                }
                else
                {
                    if (mouseButton == TMouseButtonInput.Middle && Input.GetMouseButton(2))
                    {
                        return (true);
                    }
                }
            }

            return (false);
        }

        
        /// <summary>
        /// Статус отпускания указанной кнопки мыши
        /// </summary>
        /// <param name="mouseButton">Кнопка мыши</param>
        /// <returns>Статус отпускания</returns>
        
        public static bool IsMouseButtonUp(TMouseButtonInput mouseButton)
        {
            if (mouseButton == TMouseButtonInput.Left && Input.GetMouseButtonUp(0))
            {
                return (true);
            }
            else
            {
                if (mouseButton == TMouseButtonInput.Right && Input.GetMouseButtonUp(1))
                {
                    return (true);
                }
                else
                {
                    if (mouseButton == TMouseButtonInput.Middle && Input.GetMouseButtonUp(2))
                    {
                        return (true);
                    }
                }
            }

            return (false);
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
        #endregion

        #region Notification methods
        
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
        #endregion

        #region Virtual input methods
        
        /// <summary>
        /// Получение статуса удержания нажатой кнопки
        /// </summary>
        /// <param name="name">Имя кнопки</param>
        /// <returns>Статус удержания нажатой кнопки</returns>
        
        public static bool GetButton(string name)
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            // Ищем сначала среди зарегистрированных кнопок
            ILotusVirtualButton button = null;
            if (Buttons.TryGetValue(name, out button))
            {
                return button.IsButtonPressed;
            }
            else
            {
                return Input.GetButton(name);
            }
#else
            // Ищем только среди зарегистрированных кнопок
            ILotusVirtualButton button = null;
            if (Buttons.TryGetValue(name, out button))
            {
                return (button.IsButtonPressed);
            }
            else
            {
                return (false);
            }
#endif
        }

        
        /// <summary>
        /// Получение статуса нажатия кнопки
        /// </summary>
        /// <param name="name">Имя кнопки</param>
        /// <returns>Статус нажатия кнопки</returns>
        
        public static bool GetButtonDown(string name)
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            // Ищем сначала среди зарегистрированных кнопок
            ILotusVirtualButton button = null;
            if (Buttons.TryGetValue(name, out button))
            {
                return button.IsButtonDown;
            }
            else
            {
                return Input.GetButtonDown(name);
            }

#else
            // Ищем только среди зарегистрированных кнопок
            ILotusVirtualButton button = null;
            if (Buttons.TryGetValue(name, out button))
            {
                return (button.IsButtonDown);
            }
            else
            {
                return (false);
            }
#endif
        }

        
        /// <summary>
        /// Получение статуса отпускания кнопки
        /// </summary>
        /// <param name="name">Имя кнопки</param>
        /// <returns>Статус отпускания кнопки</returns>
        
        public static bool GetButtonUp(string name)
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            // Ищем сначала среди зарегистрированных кнопок
            ILotusVirtualButton button = null;
            if (Buttons.TryGetValue(name, out button))
            {
                return button.IsButtonUp;
            }
            else
            {
                return Input.GetButtonUp(name);
            }
#else
            // Ищем только среди зарегистрированных кнопок
            ILotusVirtualButton button = null;
            if (Buttons.TryGetValue(name, out button))
            {
                return (button.IsButtonUp);
            }
            else
            {
                return (false);
            }
#endif
        }

        
        /// <summary>
        /// Получение смещения/перемещения по одномерной оси
        /// </summary>
        /// <param name="name">Имя оси</param>
        /// <returns>Смещения/перемещения по одномерной оси в относительных единицах</returns>
        
        public static float GetAxis(string name)
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            return Input.GetAxis(name);

#else
            // Ищем только среди зарегистрированных осей
            ILotusVirtualAxis axis = null;
            if (Axes.TryGetValue(name, out axis))
            {
                return (axis.GetAxis());
            }
            else
            {
                return (0.0f);
            }
#endif
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

            return new Vector2(Input.GetAxis(AxisFirstMainHorizontal), Input.GetAxis(AxisFirstMainVertical));
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

            return Input.GetAxis(AxisFirstMainHorizontal);

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

            return Input.GetAxis(AxisFirstMainVertical);

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

            return new Vector2(Input.GetAxis(AxisSecondMainHorizontal), Input.GetAxis(AxisSecondMainVertical));
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

            return Input.GetAxis(AxisSecondMainHorizontal);

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

            return Input.GetAxis(AxisSecondMainVertical);
#else
            return (AxisSecondMainJoystick.GetAxisVertical());
#endif
        }
        #endregion
    }
    /**@}*/
}
#endif
