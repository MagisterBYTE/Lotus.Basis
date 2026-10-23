#if UNITY_2017_1_OR_NEWER
using System;

using Lotus.Core.Inspector;
using Lotus.Core.Serialization;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityTween
    *@{*/
    /// <summary>
    /// Компонент для хранения и управления анимацией значением двухмерного вектора
    /// </summary>
    /// <remarks>
    /// Реализация компонента для хранения (используется хранилища анимационных кривых) и управления анимацией
    /// двухмерного вектора
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Tween/Animation Vector2D")]
    public class LotusTweenVector2D : MonoBehaviour
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        [LotusDisplayName(nameof(Tween))]
        [LotusSerializeMember(nameof(Tween))]
        internal CTweenVector2D _tweenVector2D;

        [SerializeField]
        [LotusDisplayName(nameof(UseTransform))]
        [LotusSerializeMember(nameof(UseTransform))]
        internal bool _useTransform;

        [SerializeField]
        [LotusDisplayName(nameof(Parameter))]
        [LotusSerializeMember(nameof(Parameter))]
        internal TTweenTransformParameterType _parameter;

        [SerializeField]
        [LotusDisplayName(nameof(PlaneProjection))]
        [LotusSerializeMember(nameof(PlaneProjection))]
        internal TDimensionPlane _planeProjection;

        [NonSerialized]
        internal Transform _thisTransform;
        #endregion

        #region Properties
        /// <summary>
        /// Аниматор значения 2D вектора
        /// </summary>
        public CTweenVector2D Tween
        {
            get { return _tweenVector2D; }
        }

        /// <summary>
        /// Текущее значение переменной
        /// </summary>
        public Vector2 Value
        {
            get { return _tweenVector2D.Value; }
        }

        /// <summary>
        /// Начальное значение переменной
        /// </summary>
        public Vector2 StartValue
        {
            get { return _tweenVector2D.StartValue; }
            set { _tweenVector2D.StartValue = value; }
        }

        /// <summary>
        /// Целевое значение переменной
        /// </summary>
        public Vector2 TargetValue
        {
            get { return _tweenVector2D.TargetValue; }
            set { _tweenVector2D.TargetValue = value; }
        }

        /// <summary>
        /// Режим проигрывания анимации
        /// </summary>
        public TTweenWrapMode WrapMode
        {
            get { return _tweenVector2D.WrapMode; }
            set { _tweenVector2D.WrapMode = value; }
        }

        /// <summary>
        /// Время в течение которого должно измениться значение переменной от начального к конечной
        /// </summary>
        public float Duration
        {
            get { return _tweenVector2D.Duration; }
            set { _tweenVector2D.Duration = value; }
        }

        /// <summary>
        /// Время в течение которого должно измениться значение переменной от начального к конечной
        /// </summary>
        [Obsolete("Ранее ошибочно связывалось с CorrectTime. Используйте Duration")]
        public float TimeAnimation
        {
            get { return Duration; }
            set { Duration = value; }
        }

        /// <summary>
        /// Нормализованное время прохождение анимации в пределах от 0 до 1
        /// </summary>
        public float NormalizeTime
        {
            get { return _tweenVector2D.NormalizeTime; }
        }

        /// <summary>
        /// Статус игнорирования масштабирования времени
        /// </summary>
        public bool IgnoreTimeScale
        {
            get { return _tweenVector2D.IgnoreTimeScale; }
            set { _tweenVector2D.IgnoreTimeScale = value; }
        }

        /// <summary>
        /// Количество циклов проигрывания циклических анимаций. 0 - бесконечно
        /// </summary>
        public int CountLoop
        {
            get { return _tweenVector2D.CountLoop; }
            set { _tweenVector2D.CountLoop = value; }
        }

        /// <summary>
        /// Текущие количество проигрывания циклических анимаций
        /// </summary>
        public int CurrentCountLoop
        {
            get { return _tweenVector2D.CurrentCountLoop; }
            set { _tweenVector2D.CurrentCountLoop = value; }
        }

        /// <summary>
        /// Статус применения изменяемого значения к текущему объекту трансформации
        /// </summary>
        public bool UseTransform
        {
            get { return _useTransform; }
            set { _useTransform = value; }
        }

        /// <summary>
        /// Параметр трансформации к которому применяется изменяемое значение
        /// </summary>
        public TTweenTransformParameterType Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }

        /// <summary>
        /// Плоскость проекции – конкретизация параметра трансформации
        /// </summary>
        public TDimensionPlane PlaneProjection
        {
            get { return _planeProjection; }
            set { _planeProjection = value; }
        }

        /// <summary>
        /// Статус анимации
        /// </summary>
        public bool IsPlay
        {
            get { return _tweenVector2D.IsPlay; }
        }

        /// <summary>
        /// Установка/снятие паузы анимации
        /// </summary>
        public bool IsPause
        {
            get { return _tweenVector2D.IsPause; }
            set { _tweenVector2D.IsPause = value; }
        }

        /// <summary>
        /// Статус проигрывания анимации вперед
        /// </summary>
        public bool IsForward
        {
            get { return _tweenVector2D.IsForward; }
        }

        /// <summary>
        /// Событие для нотификации о начале анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationStart
        {
            get { return _tweenVector2D.OnAnimationStart; }
            set { _tweenVector2D.OnAnimationStart = value; }
        }

        /// <summary>
        /// Событие для нотификации об окончании анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationCompleted
        {
            get { return _tweenVector2D.OnAnimationCompleted; }
            set { _tweenVector2D.OnAnimationCompleted = value; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Инициализация скрипта при присоединении его к объекту(в режиме редактора)
        /// </summary>
        public void Reset()
        {
            Init();
        }

        /// <summary>
        /// Псевдоконструктор скрипта
        /// </summary>
        public void Awake()
        {
            Init();
        }

        /// <summary>
        /// Включение скрипта
        /// </summary>
        public void OnEnable()
        {
            Init();
        }

        /// <summary>
        /// Старт скрипта
        /// </summary>
        public void Start()
        {
            _thisTransform = transform;
        }

        /// <summary>
        /// Обновление скрипта каждый кадр
        /// </summary>
        public void Update()
        {
            if (_tweenVector2D.IsPlay)
            {
                _tweenVector2D.UpdateAnimation();
            }

            if (_useTransform)
            {
                UpdateTransform();
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Первичная инициализация данных и параметров компонента
        /// </summary>
        public void Init()
        {
            if (_tweenVector2D == null)
            {
                _tweenVector2D = new CTweenVector2D();
            }
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            _tweenVector2D.StartAnimation();
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        /// <param name="targetPosition">Целевая позиция</param>
        public void StartAnimation(Vector2 targetPosition)
        {
            _tweenVector2D.StartValue = new Vector2(_thisTransform.position.x, _thisTransform.position.y);
            _tweenVector2D.TargetValue = targetPosition;
            _tweenVector2D.StartAnimation();
        }

        /// <summary>
        /// Остановка анимации
        /// </summary>
        public void StopAnimation()
        {
            _tweenVector2D.StopAnimation();
        }

        /// <summary>
        /// Обновление трансформации
        /// </summary>
        public void UpdateTransform()
        {
            switch (_parameter)
            {
                case TTweenTransformParameterType.Position:
                    {
                        switch (_planeProjection)
                        {
                            case TDimensionPlane.XY:
                                {
                                    _thisTransform.position = new Vector3(_tweenVector2D.Value.x, _tweenVector2D.Value.y, _thisTransform.position.z);
                                }
                                break;
                            case TDimensionPlane.XZ:
                                {
                                    _thisTransform.position = new Vector3(_tweenVector2D.Value.x, _thisTransform.position.y, _tweenVector2D.Value.y);
                                }
                                break;
                            case TDimensionPlane.ZY:
                                {
                                    _thisTransform.position = new Vector3(_thisTransform.position.x, _tweenVector2D.Value.x, _tweenVector2D.Value.y);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTweenTransformParameterType.LocalPosition:
                    {
                        switch (_planeProjection)
                        {
                            case TDimensionPlane.XY:
                                {
                                    _thisTransform.localPosition = new Vector3(_tweenVector2D.Value.x, _tweenVector2D.Value.y, _thisTransform.localPosition.z);
                                }
                                break;
                            case TDimensionPlane.XZ:
                                {
                                    _thisTransform.localPosition = new Vector3(_tweenVector2D.Value.x, _thisTransform.localPosition.y, _tweenVector2D.Value.y);
                                }
                                break;
                            case TDimensionPlane.ZY:
                                {
                                    _thisTransform.localPosition = new Vector3(_thisTransform.localPosition.x, _tweenVector2D.Value.x, _tweenVector2D.Value.y);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTweenTransformParameterType.Rotation:
                    {
                        switch (_planeProjection)
                        {
                            case TDimensionPlane.XY:
                                {
                                    _thisTransform.eulerAngles = new Vector3(_tweenVector2D.Value.x, _tweenVector2D.Value.y,
                                        _thisTransform.eulerAngles.z);
                                }
                                break;
                            case TDimensionPlane.XZ:
                                {
                                    _thisTransform.eulerAngles = new Vector3(_tweenVector2D.Value.x, _thisTransform.eulerAngles.y,
                                        _tweenVector2D.Value.y);
                                }
                                break;
                            case TDimensionPlane.ZY:
                                {
                                    _thisTransform.eulerAngles = new Vector3(_thisTransform.eulerAngles.x, _tweenVector2D.Value.x,
                                        _tweenVector2D.Value.y);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTweenTransformParameterType.LocalRotation:
                    {
                        switch (_planeProjection)
                        {
                            case TDimensionPlane.XY:
                                {
                                    _thisTransform.localEulerAngles = new Vector3(_tweenVector2D.Value.x, _tweenVector2D.Value.y,
                                        _thisTransform.localEulerAngles.z);
                                }
                                break;
                            case TDimensionPlane.XZ:
                                {
                                    _thisTransform.localEulerAngles = new Vector3(_tweenVector2D.Value.x, _thisTransform.localEulerAngles.y,
                                        _tweenVector2D.Value.y);
                                }
                                break;
                            case TDimensionPlane.ZY:
                                {
                                    _thisTransform.localEulerAngles = new Vector3(_thisTransform.localEulerAngles.x, _tweenVector2D.Value.x,
                                        _tweenVector2D.Value.y);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTweenTransformParameterType.Scale:
                    {
                        switch (_planeProjection)
                        {
                            case TDimensionPlane.XY:
                                {
                                    _thisTransform.localScale = new Vector3(_tweenVector2D.Value.x, _tweenVector2D.Value.y, _thisTransform.localScale.z);
                                }
                                break;
                            case TDimensionPlane.XZ:
                                {
                                    _thisTransform.localScale = new Vector3(_tweenVector2D.Value.x, _thisTransform.localScale.y, _tweenVector2D.Value.y);
                                }
                                break;
                            case TDimensionPlane.ZY:
                                {
                                    _thisTransform.localScale = new Vector3(_thisTransform.localScale.x, _tweenVector2D.Value.x, _tweenVector2D.Value.y);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Запуск анимации
        /// </summary>
        [ContextMenu("Play")]
        [LotusMethodCall("Play", 0, TMethodCallMode.OnlyPlay)]
        public void PlayInEditor()
        {
            StartAnimation();
        }
#endif
        #endregion
    }
    /**@}*/
}
#endif
