#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityCamera
    *@{*/
    /// <summary>
    /// Камера с фиксированной позиций для слежения за объектом
    /// </summary>
    /// <remarks>
    /// Реализация компонента камеры с фиксированной позиций для слежения за объектом с возможностью изменять
    /// расстояние до объекта и режимом орбиты.
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Camera/FixedFollow-Camera")]
    public class LotusCameraFixedFollow : MonoBehaviour
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        [LotusDisplayName(nameof(Target))]
        internal Transform _target;
        [SerializeField]
        [LotusDisplayName(nameof(IsZoom))]
        internal bool _isZoom = true;
        [SerializeField]
        [LotusDisplayName(nameof(Distance))]
        internal Vector3 _distance;
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

        // Кэшированные данные
        [HideInInspector]
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
        public Vector3 Distance
        {
            get { return _distance; }
            set { _distance = value; }
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
            _speedXOrbit = 50;
            _speedYOrbit = 50;
        }


        /// <summary>
        /// Псевдоконструктор скрипта
        /// </summary>
        public void Awake()
        {
            _thisTransform = this.transform;
            _distance = _target.position - _thisTransform.position;
            _orbitDistance = _distance.magnitude;
        }


        /// <summary>
        /// Обновление скрипта каждый кадр
        /// </summary>
        public void Update()
        {
            if (_isControl)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _isOrbit = true;
                    _orbitDistance = _distance.magnitude;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    _isOrbit = false;
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
                // Приближение камеры
                var delta_zoom = Input.GetAxis(XCameraInput.AxisOrbitName);
                if (Mathf.Approximately(delta_zoom, 0) == false && _isZoom)
                {
                    _orbitDistance += delta_zoom;
                }

                // Получаем текущие положение камеры
                _currentX = _thisTransform.eulerAngles.y;
                _currentY = _thisTransform.eulerAngles.x;

                float dx = 0;
                float dy = 0;
                dx = Input.GetAxis(XCameraInput.AxisHorizontalName);
                dy = Input.GetAxis(XCameraInput.AxisVerticalName);

                _currentX += dx * _speedXOrbit * Time.deltaTime;
                _currentY -= dy * _speedYOrbit * Time.deltaTime;

                var rotation = Quaternion.Euler(_currentY, _currentX, 0);
                Vector3 position = (rotation * new Vector3(0.0f, 0.0f, -_orbitDistance)) + _target.position;

                _thisTransform.rotation = rotation;
                _thisTransform.position = position;

                _distance = _target.position - _thisTransform.position;
            }
            else
            {
                // Приближение камеры
                var delta_zoom = Input.GetAxis(XCameraInput.AxisOrbitName);
                if (Mathf.Approximately(delta_zoom, 0) == false && _isZoom)
                {
                    Vector3 dir = _target.position - _thisTransform.position;
                    _orbitDistance = dir.magnitude;
                    _thisTransform.position += dir * delta_zoom;
                    _distance = _target.position - _thisTransform.position;
                }

                _thisTransform.position = _target.position - _distance;
                _thisTransform.LookAt(_target);
            }
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
            _isControl = false;

            // Получаем текущие положение камеры
            _currentX = _thisTransform.eulerAngles.y;
            _currentY = _thisTransform.eulerAngles.x;

            var rotation = Quaternion.Euler(_currentY, _currentX, 0);
            Vector3 position = (rotation * new Vector3(0.0f, 0.0f, -_orbitDistance)) + _target.position;

            this.MoveTo(0.5f, position, TEasingType.CubeInOut, () =>
            {
                _distance = _target.position - _thisTransform.position;
                _isControl = true;
            });

            this.RotationTo(0.5f, rotation, TEasingType.CubeInOut);
        }
        #endregion
    }
    /**@}*/
}
#endif
