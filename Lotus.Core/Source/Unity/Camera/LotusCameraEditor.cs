#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityCamera
    *@{*/
    /// <summary>
    /// Камера аналогичная камеры в редакторе Unity
    /// </summary>
    /// <remarks>
    /// Реализация компонента камеры по управлению аналогично камеры в редакторе Unity
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Camera/Editor-Camera")]
    public class LotusCameraEditor : MonoBehaviour
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        [LotusDisplayName(nameof(KeyMoveForward))]
        internal KeyCode _keyMoveForward = KeyCode.W;
        [SerializeField]
        [LotusDisplayName(nameof(KeyMoveBackward))]
        internal KeyCode _keyMoveBackward = KeyCode.S;
        [SerializeField]
        [LotusDisplayName(nameof(KeyMoveLeft))]
        internal KeyCode _keyMoveLeft = KeyCode.A;
        [SerializeField]
        [LotusDisplayName(nameof(KeyMoveRight))]
        internal KeyCode _keyMoveRight = KeyCode.D;
        [SerializeField]
        [LotusDisplayName(nameof(Speed))]
        internal float _speed = 50;
        [NonSerialized]
        internal Vector3 _rotation;

        // Кэшированные данные
        [HideInInspector]
        internal Transform _thisTransform;
        #endregion

        #region Properties
        /// <summary>
        /// Клавиша для перемещения вперед по направлению взгляда
        /// </summary>
        public KeyCode KeyMoveForward
        {
            get { return _keyMoveForward; }
            set { _keyMoveForward = value; }
        }

        /// <summary>
        /// Клавиша для перемещения назад по направлению взгляда
        /// </summary>
        public KeyCode KeyMoveBackward
        {
            get { return _keyMoveBackward; }
            set { _keyMoveBackward = value; }
        }

        /// <summary>
        /// Клавиша для смещения влево
        /// </summary>
        public KeyCode KeyMoveLeft
        {
            get { return _keyMoveLeft; }
            set { _keyMoveLeft = value; }
        }

        /// <summary>
        /// Клавиша для смещения вправо
        /// </summary>
        public KeyCode KeyMoveRight
        {
            get { return _keyMoveRight; }
            set { _keyMoveRight = value; }
        }

        /// <summary>
        /// Относительная скорость перемещения
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Инициализация скрипта при присоединении его к объекту(в режиме редактора)
        /// </summary>
        public void Reset()
        {

        }


        /// <summary>
        /// Псевдоконструктор скрипта
        /// </summary>
        public void Awake()
        {
            _thisTransform = this.transform;
        }


        /// <summary>
        /// Обновление скрипта каждый кадр (после Update)
        /// </summary>

        public void LateUpdate()
        {
            ManagerToDesktop();
        }
        #endregion

        #region Main methods

        /// <summary>
        /// Управление камерой для стандартного ввода
        /// </summary>
        public void ManagerToDesktop()
        {
            // Нажата средняя кнопка мыши
            if (XInputDispatcher.GetMouseButton(2))
            {
                _thisTransform.position += _thisTransform.right * _speed * XInputDispatcher.GetAxis(XCameraInput.AxisHorizontalName) * Time.deltaTime;
                _thisTransform.position += Vector3.up * _speed * XInputDispatcher.GetAxis(XCameraInput.AxisVerticalName) * Time.deltaTime;
            }
            else
            {
                if (XInputDispatcher.GetMouseButton(1))
                {
                    _rotation.y += XInputDispatcher.GetAxis(XCameraInput.AxisHorizontalName);
                    _rotation.x += XInputDispatcher.GetAxis(XCameraInput.AxisVerticalName);
                    _thisTransform.eulerAngles = _rotation;

                    if (XInputDispatcher.GetKey(_keyMoveForward))
                    {
                        _thisTransform.position += _thisTransform.forward * Time.deltaTime * _speed;
                    }
                    if (XInputDispatcher.GetKey(_keyMoveBackward))
                    {
                        _thisTransform.position += -_thisTransform.forward * Time.deltaTime * _speed;
                    }
                    if (XInputDispatcher.GetKey(_keyMoveLeft))
                    {
                        _thisTransform.position += -_thisTransform.right * Time.deltaTime * _speed;
                    }
                    if (XInputDispatcher.GetKey(_keyMoveRight))
                    {
                        _thisTransform.position += _thisTransform.right * Time.deltaTime * _speed;
                    }
                }
            }

            _thisTransform.position += _thisTransform.forward * XInputDispatcher.GetAxis(XCameraInput.AxisOrbitName);
        }


        /// <summary>
        /// Управление камерой для мобильного ввода
        /// </summary>

        public void ManagerToMobile()
        {
        }


        /// <summary>
        /// Перемещение камеры из текущей позиции в вычисленную
        /// </summary>

        public void MoveToComputePosition()
        {
            // Получаем текущие положение камеры
            _rotation.y = _thisTransform.eulerAngles.y;
            _rotation.x = _thisTransform.eulerAngles.x;
        }
        #endregion
    }
    /**@}*/
}
#endif