#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityCamera
    *@{*/
    /// <summary>
    /// Камера с возможностью вращения вокруг объекта по эллипсу
    /// </summary>
    /// <remarks>
    /// <para>
    /// Реализация компонента камеры с возможностью вращения вокруг объекта по эллипсу, перемещением, скроллинга
    /// и плавной остановки вращения
    /// </para>
    /// <para>
    /// http://unity3d.ru/distribution/viewtopic.php?f=12&t=7816
    /// </para>
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Camera/MouseEllipseOrbit")]
    public class LotusCameraMouseEllipseOrbit : MonoBehaviour
    {
        #region Fields
        // Основные параметры
        [Header("Main setting")]
        [SerializeField]
        [LotusDisplayName(nameof(Target))]
        internal Transform _target;
        [SerializeField]
        [LotusDisplayName(nameof(ButtonOrbit))]
        internal TMouseButtonInput _buttonOrbit = TMouseButtonInput.Right;

        // Вращение
        [Header("Rotation")]
        [SerializeField]
        [LotusDisplayName(nameof(RadiusEllipseA))]
        internal float _radiusEllipseA = 10;
        [SerializeField]
        [LotusDisplayName(nameof(RadiusEllipseB))]
        internal float _radiusEllipseB = 10;
        [SerializeField]
        [LotusDisplayName(nameof(RotationSpeedX))]
        internal float _rotationSpeedX = 250;
        [SerializeField]
        [LotusDisplayName(nameof(RotationSpeedY))]
        internal float _rotationSpeedY = 120;
        [SerializeField]
        [LotusDisplayName(nameof(MinLimitY))]
        internal float _minLimitY = -20;
        [SerializeField]
        [LotusDisplayName(nameof(MaxLimitY))]
        internal float _maxLimitY = 80;

        // Паннинг
        [Header("Panning")]
        [SerializeField]
        [LotusDisplayName(nameof(IsPanning))]
        internal bool _isPanning;
        [SerializeField]
        [LotusDisplayName(nameof(ButtonPan))]
        internal TMouseButtonInput _buttonPan = TMouseButtonInput.Middle;
        [SerializeField]
        [LotusDisplayName(nameof(PanningSpeed))]
        internal float _panningSpeed = 5;
        [SerializeField]
        [LotusDisplayName(nameof(PanningLayer))]
        internal LayerMask _panningLayer;

        // Скроллинг
        [Header("Scrolling")]
        [SerializeField]
        [LotusDisplayName(nameof(IsScrolling))]
        internal bool _isScrolling = true;
        [SerializeField]
        [LotusDisplayName(nameof(ScrollSpeed))]
        internal float _scrollSpeed = 1;
        [SerializeField]
        [LotusDisplayName(nameof(ScrollMinDistance))]
        internal float _scrollMinDistance = 1;
        [SerializeField]
        [LotusDisplayName(nameof(ScrollMaxDistance))]
        internal float _scrollMaxDistance = 10;

        // Кэшированные данные
        [HideInInspector]
        internal Transform _thisTransform;
        [HideInInspector]
        internal Camera _currentCamera;

        // Служебные данные
        private float _angleX = 0;
        private float _angleY = 0;
        private float _distance = 10;
        private Vector3 _newTargetPosition;
        private Vector3 _angles;
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
        /// Кнопка мыши для активации вращения
        /// </summary>
        public TMouseButtonInput ButtonOrbit
        {
            get { return _buttonOrbit; }
            set { _buttonOrbit = value; }
        }

        //
        // ВРАЩЕНИЕ
        //
        /// <summary>
        /// Размер полуоси эллипса
        /// </summary>
        public float RadiusEllipseA
        {
            get { return _radiusEllipseA; }
            set { _radiusEllipseA = value; }
        }

        /// <summary>
        /// Размер полуоси эллипса
        /// </summary>
        public float RadiusEllipseB
        {
            get { return _radiusEllipseB; }
            set { _radiusEllipseB = value; }
        }

        /// <summary>
        /// Скорость вращения по X
        /// </summary>
        public float RotationSpeedX
        {
            get { return _rotationSpeedX; }
            set { _rotationSpeedX = value; }
        }

        /// <summary>
        /// скорость вращения по Y
        /// </summary>
        public float RotationSpeedY
        {
            get { return _rotationSpeedY; }
            set { _rotationSpeedY = value; }
        }

        /// <summary>
        /// Минимальный предел просмотра по оси Y
        /// </summary>
        public float MinLimitY
        {
            get { return _minLimitY; }
            set { _minLimitY = value; }
        }

        /// <summary>
        /// Максимальный предел просмотра по оси Y
        /// </summary>
        public float MaxLimitY
        {
            get { return _maxLimitY; }
            set { _maxLimitY = value; }
        }

        //
        // ПЕРЕМЕЩЕНИЕ
        //
        /// <summary>
        /// Статус перемещения
        /// </summary>
        public bool IsPanning
        {
            get { return _isPanning; }
            set { _isPanning = value; }
        }

        /// <summary>
        /// Кнопка мыши для активации перемещения
        /// </summary>
        public TMouseButtonInput ButtonPan
        {
            get { return _buttonPan; }
            set { _buttonPan = value; }
        }

        /// <summary>
        /// Скорость перемещения
        /// </summary>
        public float PanningSpeed
        {
            get { return _panningSpeed; }
            set { _panningSpeed = value; }
        }

        /// <summary>
        /// Слой, в котором находится плоскость перемещения
        /// </summary>
        public int PanningLayer
        {
            get { return _panningLayer; }
            set { _panningLayer = value; }
        }

        //
        // СКРОЛЛИНГ
        //
        /// <summary>
        /// Скроллинг
        /// </summary>
        public bool IsScrolling
        {
            get { return _isScrolling; }
            set { _isScrolling = value; }
        }

        /// <summary>
        /// Скорость скроллинга
        /// </summary>
        public float ScrollSpeed
        {
            get { return _scrollSpeed; }
            set { _scrollSpeed = value; }
        }

        /// <summary>
        /// Минимальная дистанция скроллинга
        /// </summary>
        public float ScrollMinDistance
        {
            get { return _scrollMinDistance; }
            set { _scrollMinDistance = value; }
        }

        /// <summary>
        /// Максимальная дистанция скроллинга
        /// </summary>
        public float ScrollMaxDistance
        {
            get { return _scrollMaxDistance; }
            set { _scrollMaxDistance = value; }
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
            _currentCamera = this.GetComponent<Camera>();
            _angles = _thisTransform.eulerAngles;
        }


        /// <summary>
        /// Старт скрипта
        /// </summary>
        public void Start()
        {
            _newTargetPosition = _target.position;

            _angleX = _angles.y;
            _angleY = _angles.x;

            // Make the rigid body not change rotation
            Orbit();
            Scroll();
        }


        /// <summary>
        /// Обновление скрипта каждый кадр
        /// </summary>
        public void Update()
        {

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
        /// Ограничение угла
        /// </summary>
        /// <param name="angle">Угол, задается в градусах</param>
        /// <param name="min">Минимальный угол</param>
        /// <param name="max">Максимальный угол</param>
        /// <returns>Нормализованный угол</returns>
        public static float Clamp(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;

            if (angle > max)
            {
                return max;
            }
            if (angle < min)
            {
                return min;
            }

            return angle;
        }


        /// <summary>
        /// Управление камерой для стандартного ввода
        /// </summary>
        public void ManagerToDesktop()
        {
            // вращение
            if (XInputDispatcher.IsMouseButton(_buttonOrbit))
            {
                Orbit();
            }

            if (_isPanning)
            {
                Panning();
            }
            if (_isScrolling)
            {
                Scroll();
            }

            transform.localPosition = (transform.localRotation) * new Vector3(0, 0, -_distance);
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

        }


        /// <summary>
        /// Вращения камеры
        /// </summary>
        private void Orbit()
        {
            if (_target)
            {
                _angleX += XInputDispatcher.GetAxis(XCameraInput.AxisHorizontalName) * _rotationSpeedX * 0.02f;
                _angleY -= XInputDispatcher.GetAxis(XCameraInput.AxisVerticalName) * _rotationSpeedY * 0.02f;

                var y = Clamp(_angleY, _minLimitY, _maxLimitY);
                var rotation = Quaternion.Euler(y, _angleX, 0);
                transform.rotation = rotation;

                Vector3 target_dir = transform.position - _target.position;
                var angle = Vector3.Angle(target_dir, _target.forward);
                var numerator = Mathf.Pow(_radiusEllipseA, 2) * Mathf.Pow(_radiusEllipseB, 2);

                var denominator = (Mathf.Pow(_radiusEllipseA, 2) * Mathf.Pow(Mathf.Sin(angle * Mathf.Deg2Rad), 2)) +
                (Mathf.Pow(_radiusEllipseB, 2) * Mathf.Pow(Mathf.Cos(angle * Mathf.Deg2Rad), 2));

                _distance = Mathf.Sqrt(numerator / denominator);
            }
        }


        /// <summary>
        /// Перемещение камеры
        /// </summary>
        private void Panning()
        {
            // передвижение мыши
            if (XInputDispatcher.IsMouseButton(_buttonPan))
            {
                Snap();
            }

            _target.position = Vector3.Lerp(_target.position, _newTargetPosition, Time.deltaTime * _panningSpeed);
        }


        /// <summary>
        /// Привязка камеры к новой позиции
        /// </summary>
        private void Snap()
        {
            Ray ray = _currentCamera.ScreenPointToRay(XInputDispatcher.MousePosition);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 4000, ~_panningLayer))
            {
                _newTargetPosition = hit.point;
            }
        }


        /// <summary>
        /// Скроллинг
        /// </summary>
        private void Scroll()
        {
            // приблизить
            if ((XInputDispatcher.GetAxis(XCameraInput.AxisOrbitName)) > 0)
            {
                if (_distance >= _scrollMinDistance)
                {
                    var dist1 = _distance;
                    _distance += -(XInputDispatcher.GetAxis(XCameraInput.AxisOrbitName)) * _scrollSpeed;
                    var k = dist1 / _distance;
                    _radiusEllipseA = _radiusEllipseA / k;
                    _radiusEllipseB = _radiusEllipseB / k;
                }
            }
            //отдалить
            if ((XInputDispatcher.GetAxis(XCameraInput.AxisOrbitName)) < 0)
            {
                if (_distance <= _scrollMaxDistance)
                {
                    var dist1 = _distance;
                    _distance += -(XInputDispatcher.GetAxis(XCameraInput.AxisOrbitName)) * _scrollSpeed;
                    var k = dist1 / _distance;
                    _radiusEllipseA = _radiusEllipseA / k;
                    _radiusEllipseB = _radiusEllipseB / k;
                }
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
