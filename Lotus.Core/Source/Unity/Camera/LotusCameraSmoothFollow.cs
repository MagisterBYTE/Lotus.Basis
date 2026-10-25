#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityCamera
    *@{*/
    /// <summary>
    /// Камера с плавным слежением за объектом
    /// </summary>
    /// <remarks>
    /// Реализация компонента камеры с плавным слежением за объектом с возможностью изменять расстояние до объекта и
    /// режимом орбиты.
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Camera/SmoothFollow-Camera")]
    public class LotusCameraSmoothFollow : MonoBehaviour
    {
        #region Fields
        [Header("Main setting")]
        [SerializeField]
        [LotusDisplayName(nameof(Target))]
        internal Transform _target;
        [SerializeField]
        [LotusDisplayName(nameof(IsZoom))]
        internal bool _isZoom = true;
        [SerializeField]
        [LotusDisplayName(nameof(Distance))]
        internal float _distance = 4;
        [SerializeField]
        [LotusDisplayName(nameof(Height))]
        internal float _height = 2;
        [SerializeField]
        [LotusDisplayName(nameof(RotateDamping))]
        internal float _rotateDamping = 5;
        [SerializeField]
        [LotusDisplayName(nameof(HeightDamping))]
        internal float _heightDamping = 2;

        // Вращение вокруг объекта
        [Header("Setting orbit")]
        [SerializeField]
        [LotusDisplayName(nameof(IsSupportOrbit))]
        internal bool _isSupportOrbit = true;
        [SerializeField]
        [LotusDisplayName(nameof(SpeedXOrbit))]
        internal float _speedXOrbit = 50;
        [SerializeField]
        [LotusDisplayName(nameof(SpeedYOrbit))]
        internal float _speedYOrbit = 50;
        [NonSerialized]
        internal bool _isOrbit = false;
        [NonSerialized]
        internal float _currentX;
        [NonSerialized]
        internal float _currentY;
        [NonSerialized]
        internal float _orbitDistance;
        [NonSerialized]
        internal bool _isControl = true;

        // Дополнительное управление
        [Header("Manager height")]
        [SerializeField]
        [LotusDisplayName(nameof(KeyHeightUp))]
        internal KeyCode _keyHeightUp = KeyCode.Keypad8;
        [SerializeField]
        [LotusDisplayName(nameof(KeyHeightDown))]
        internal KeyCode _keyHeightDown = KeyCode.Keypad2;

        // Кэшированные данные
        [NonSerialized]
        internal Transform _thisTransform;
        #endregion

        #region Properties
        /// <summary>
        /// Целевой объект
        /// </summary>
        public Transform Target
        {
            get { return _target; }
            set { _target = value; }
        }

        /// <summary>
        /// Возможность изменять дистанцию(наезд) до целевого объекта
        /// </summary>
        public bool IsZoom
        {
            get { return _isZoom; }
            set { _isZoom = value; }
        }

        /// <summary>
        /// Расстояние до целевого объекта в плоскости XZ
        /// </summary>
        public float Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        /// <summary>
        /// Расстояние до целевого объекта в плоскости Y
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// Скорость вращения камеры
        /// </summary>
        public float RotateDamping
        {
            get { return _rotateDamping; }
            set { _rotateDamping = value; }
        }

        /// <summary>
        /// Скорость снижения камеры
        /// </summary>
        public float HeightDamping
        {
            get { return _heightDamping; }
            set { _heightDamping = value; }
        }

        /// <summary>
        /// Возможность вращение камеры вокруг целевого объекта
        /// </summary>
        public bool IsSupportOrbit
        {
            get { return _isSupportOrbit; }
            set { _isSupportOrbit = value; }
        }

        /// <summary>
        /// Скорость вращения в плоскости X
        /// </summary>
        public float SpeedXOrbit
        {
            get { return _speedXOrbit; }
            set { _speedXOrbit = value; }
        }

        /// <summary>
        /// Скорость вращения в плоскости Y
        /// </summary>
        public float SpeedYOrbit
        {
            get { return _speedYOrbit; }
            set { _speedYOrbit = value; }
        }

        /// <summary>
        /// Клавиша для увеличения высоты
        /// </summary>
        public KeyCode KeyHeightUp
        {
            get { return _keyHeightUp; }
            set { _keyHeightUp = value; }
        }

        /// <summary>
        /// Клавиша для уменьшения высоты
        /// </summary>
        public KeyCode KeyHeightDown
        {
            get { return _keyHeightDown; }
            set { _keyHeightDown = value; }
        }

        /// <summary>
        /// Статус управления камерой
        /// </summary>
        public bool IsControl
        {
            get { return _isControl; }
            set { _isControl = value; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Инициализация скрипта при присоединении его к объекту(в режиме редактора)
        /// </summary>
        public void Reset()
        {
            _distance = 4;
            _height = 2;
            _rotateDamping = 5;
            _heightDamping = 2;

            _speedXOrbit = 50;
            _speedYOrbit = 50;
        }


        /// <summary>
        /// Псевдоконструктор скрипта
        /// </summary>
        public void Awake()
        {
            _thisTransform = this.transform;
        }


        /// <summary>
        /// Обновление скрипта каждый кадр
        /// </summary>
        public void Update()
        {
            if (_isControl)
            {
                if (_isSupportOrbit)
                {
                    if (XInputDispatcher.GetMouseButtonDown(1))
                    {
                        _isOrbit = true;
                    }
                    if (XInputDispatcher.GetMouseButtonUp(1))
                    {
                        _isOrbit = false;
                    }
                }
            }
        }


        /// <summary>
        /// Обновление скрипта каждый кадр (после Update)
        /// </summary>
        public void LateUpdate()
        {
            if (_isControl)
            {
                ManagerToDesktop();
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Управление камерой для стандартного ввода
        /// </summary>
        public void ManagerToDesktop()
        {
            // Если камера находится в режиме орбиты
            if (_isOrbit)
            {
                // Получаем текущие положение камеры
                _currentX = _thisTransform.eulerAngles.y;
                _currentY = _thisTransform.eulerAngles.x;

                _currentX += XInputDispatcher.GetAxis(XCameraInput.AxisHorizontalName) * _speedXOrbit * Time.deltaTime;
                _currentY -= XInputDispatcher.GetAxis(XCameraInput.AxisVerticalName) * _speedYOrbit * Time.deltaTime;

                var rotation = Quaternion.Euler(_currentY, _currentX, 0);
                Vector3 position = (rotation * new Vector3(0.0f, 0.0f, -_orbitDistance)) + _target.position;

                _thisTransform.rotation = rotation;
                _thisTransform.position = position;

                // Приближение камеры
                if (_isZoom)
                {
                    _orbitDistance += XInputDispatcher.GetAxis(XCameraInput.AxisOrbitName);
                }

                ComputeDistance();
            }
            else
            {
                this.SmoothFollowLook();

                // Приближение камеры
                if (_isZoom)
                {
                    Vector3 dir = _target.position - _thisTransform.position;
                    _orbitDistance = dir.magnitude;
                    _thisTransform.position += dir * XInputDispatcher.GetAxis(XCameraInput.AxisOrbitName);
                }

                ComputeDistance();
            }

            // Смещения высоты камеры
            if (XInputDispatcher.GetKey(_keyHeightUp))
            {
                _height += 0.05f;
            }

            if (XInputDispatcher.GetKey(_keyHeightDown))
            {
                _height -= 0.05f;
            }
        }


        /// <summary>
        /// Управление камерой для мобильного ввода
        /// </summary>
        public void ManagerToMobile()
        {
        }


        /// <summary>
        /// Вычисление дистанции
        /// </summary>
        public void ComputeDistance()
        {
            // Вычисление расстояния
            _distance = Vector2.Distance(new Vector2(_thisTransform.position.x, _thisTransform.position.z),
                new Vector2(_target.position.x, _target.position.z));

            // Вычисление высоты
            _height = _thisTransform.position.y - _target.position.y;
        }


        /// <summary>
        /// Плавный возврат камеры в направление объекта
        /// </summary>
        public void SmoothFollowLook()
        {
            // Calculate the current rotation angles
            var wanted_rotation_angle = Target.eulerAngles.y;
            var wanted_height = Target.position.y + _height;

            var current_rotation_angle = transform.eulerAngles.y;
            var current_height = transform.position.y;

            // Damp the rotation around the y-axis
            current_rotation_angle = Mathf.LerpAngle(current_rotation_angle, wanted_rotation_angle, _rotateDamping * Time.deltaTime);

            // Damp the height
            current_height = Mathf.Lerp(current_height, wanted_height, _heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var current_rotation = Quaternion.Euler(0, current_rotation_angle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = _target.position;
            transform.position -= current_rotation * Vector3.forward * _distance;

            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, current_height, transform.position.z);

            //Always look at the target
            transform.LookAt(Target);
        }


        /// <summary>
        /// Перемещение камеры из текущей позиции в вычисленную
        /// </summary>
        public void MoveToComputePosition()
        {
            _isControl = false;

            // Calculate the current rotation angles
            var wanted_rotation_angle = Target.eulerAngles.y;
            var wanted_height = Target.position.y + _height;

            var current_rotation_angle = transform.eulerAngles.y;
            var current_height = transform.position.y;

            // Damp the rotation around the y-axis
            current_rotation_angle = Mathf.LerpAngle(current_rotation_angle, wanted_rotation_angle, _rotateDamping * Time.deltaTime);

            // Damp the height
            current_height = Mathf.Lerp(current_height, wanted_height, _heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var current_rotation = Quaternion.Euler(0, current_rotation_angle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            Vector3 position = _target.position;
            position -= current_rotation * Vector3.forward * _distance;

            // Set the height of the camera
            var target_position = new Vector3(position.x, current_height, position.z);

            var look_target = Quaternion.LookRotation((_target.position - target_position).normalized);

            this.MoveTo(0.5f, target_position, TEasingType.CubeInOut, () =>
            {
                _isControl = true;
            });

            this.RotationTo(0.5f, look_target, TEasingType.SineInOut);
        }
        #endregion
    }
    /**@}*/
}
#endif
