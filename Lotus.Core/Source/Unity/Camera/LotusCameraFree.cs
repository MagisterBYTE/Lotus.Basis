#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityCamera
    *@{*/
    /// <summary>
    /// Камера со свободным перемещением
    /// </summary>
    /// <remarks>
    /// Реализация компонента камеры с абсолютно свободным перемещением в пространстве
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Camera/Free-Camera")]
    public class LotusCameraFree : MonoBehaviour
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
        [LotusDisplayName(nameof(KeyMoveUp))]
        internal KeyCode _keyMoveUp = KeyCode.Keypad8;
        [SerializeField]
        [LotusDisplayName(nameof(KeyMoveDown))]
        internal KeyCode _keyMoveDown = KeyCode.Keypad2;
        [SerializeField]
        [LotusDisplayName(nameof(Speed))]
        internal float _speed = 50;

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
        /// Клавиша для смещения вверх
        /// </summary>
        public KeyCode KeyMoveUp
        {
            get { return _keyMoveUp; }
            set { _keyMoveUp = value; }
        }

        /// <summary>
        /// Клавиша для смещения вниз
        /// </summary>
        public KeyCode KeyMoveDown
        {
            get { return _keyMoveDown; }
            set { _keyMoveDown = value; }
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
            if (XInputDispatcher.GetKey(_keyMoveUp))
            {
                _thisTransform.position += _thisTransform.up * Time.deltaTime * _speed;
            }
            if (XInputDispatcher.GetKey(_keyMoveDown))
            {
                _thisTransform.position += -_thisTransform.up * Time.deltaTime * _speed;
            }

            if (XInputDispatcher.GetMouseButton(0))
            {
                _thisTransform.Rotate(Vector3.up, XInputDispatcher.GetAxis(XCameraInput.AxisHorizontalName));
            }
        }


        /// <summary>
        /// Управление камерой для мобильного ввода
        /// </summary>
        public void ManagerToMobile()
        {
        }
        #endregion
    }
    /**@}*/
}
#endif
