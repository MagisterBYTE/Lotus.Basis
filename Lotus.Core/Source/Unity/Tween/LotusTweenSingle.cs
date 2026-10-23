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
    /// Компонент для хранения и управления анимацией значением вещественного типа
    /// </summary>
    /// <remarks>
    /// Реализация компонента для хранения (используется хранилища анимационных кривых) и управления анимацией
    /// значением вещественного типа
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Tween/Animation Single")]
    public class LotusTweenSingle : MonoBehaviour
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        [LotusDisplayName(nameof(TweenSingle))]
        [LotusSerializeMember(nameof(TweenSingle))]
        internal CTweenSingle _tweenSingle;
        [SerializeField]
        [LotusDisplayName(nameof(UseTransform))]
        [LotusSerializeMember(nameof(UseTransform))]
        internal bool _useTransform;
        [SerializeField]
        [LotusDisplayName(nameof(Parameter))]
        [LotusSerializeMember(nameof(Parameter))]
        internal TTweenTransformParameterType _parameter;
        [SerializeField]
        [LotusDisplayName(nameof(ParameterComponent))]
        [LotusSerializeMember(nameof(ParameterComponent))]
        internal TDimensionComponent _parameterComponent;
        [NonSerialized]
        internal Transform _thisTransform;
        #endregion

        #region Properties
        /// <summary>
        /// Аниматор значения вещественного типа
        /// </summary>
        public CTweenSingle TweenSingle
        {
            get { return _tweenSingle; }
        }

        /// <summary>
        /// Текущее значение переменной
        /// </summary>
        public float Value
        {
            get { return _tweenSingle.Value; }
        }

        /// <summary>
        /// Начальное значение переменной
        /// </summary>
        public float StartValue
        {
            get { return _tweenSingle.StartValue; }
            set { _tweenSingle.StartValue = value; }
        }

        /// <summary>
        /// Целевое значение переменной
        /// </summary>
        public float TargetValue
        {
            get { return _tweenSingle.TargetValue; }
            set { _tweenSingle.TargetValue = value; }
        }

        /// <summary>
        /// Режим проигрывания анимации
        /// </summary>
        public TTweenWrapMode WrapMode
        {
            get { return _tweenSingle.WrapMode; }
            set { _tweenSingle.WrapMode = value; }
        }

        /// <summary>
        /// Время в течение которого должно измениться значение переменной от начального к конечной
        /// </summary>
        public float Duration
        {
            get { return _tweenSingle.Duration; }
            set { _tweenSingle.Duration = value; }
        }

        /// <summary>
        /// Нормализованное время прохождение анимации в пределах от 0 до 1
        /// </summary>
        public float NormalizeTime
        {
            get { return _tweenSingle.NormalizeTime; }
        }

        /// <summary>
        /// Статус игнорирования масштабирования времени
        /// </summary>
        public bool IgnoreTimeScale
        {
            get { return _tweenSingle.IgnoreTimeScale; }
            set { _tweenSingle.IgnoreTimeScale = value; }
        }

        /// <summary>
        /// Количество циклов проигрывания циклических анимаций. 0 - бесконечно
        /// </summary>
        public int CountLoop
        {
            get { return _tweenSingle.CountLoop; }
            set { _tweenSingle.CountLoop = value; }
        }

        /// <summary>
        /// Текущие количество проигрывания циклических анимаций
        /// </summary>
        public int CurrentCountLoop
        {
            get { return _tweenSingle.CurrentCountLoop; }
            set { _tweenSingle.CurrentCountLoop = value; }
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
        /// Компонент трансформации – конкретизация параметра трансформации
        /// </summary>
        public TDimensionComponent ParameterComponent
        {
            get { return _parameterComponent; }
            set { _parameterComponent = value; }
        }

        /// <summary>
        /// Статус анимации
        /// </summary>
        public bool IsPlay
        {
            get { return _tweenSingle.IsPlay; }
        }

        /// <summary>
        /// Установка/снятие паузы анимации
        /// </summary>
        public bool IsPause
        {
            get { return _tweenSingle.IsPause; }
            set { _tweenSingle.IsPause = value; }
        }

        /// <summary>
        /// Статус проигрывания анимации вперед
        /// </summary>
        public bool IsForward
        {
            get { return _tweenSingle.IsForward; }
        }

        /// <summary>
        /// Событие для нотификации о начале анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationStart
        {
            get { return _tweenSingle.OnAnimationStart; }
            set { _tweenSingle.OnAnimationStart = value; }
        }

        /// <summary>
        /// Событие для нотификации об окончании анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationCompleted
        {
            get { return _tweenSingle.OnAnimationCompleted; }
            set { _tweenSingle.OnAnimationCompleted = value; }
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
            if (_tweenSingle.IsPlay)
            {
                _tweenSingle.UpdateAnimation();
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
            if (_tweenSingle == null)
            {
                _tweenSingle = new CTweenSingle();
            }
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            _tweenSingle.StartAnimation();
        }

        /// <summary>
        /// Остановка анимации
        /// </summary>
        public void StopAnimation()
        {
            _tweenSingle.StopAnimation();
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
                        switch (_parameterComponent)
                        {
                            case TDimensionComponent.X:
                                {
                                    _thisTransform.position = new Vector3(_tweenSingle.Value, _thisTransform.position.y, _thisTransform.position.z);
                                }
                                break;
                            case TDimensionComponent.Y:
                                {
                                    _thisTransform.position = new Vector3(_thisTransform.position.x, _tweenSingle.Value, _thisTransform.position.z);
                                }
                                break;
                            case TDimensionComponent.Z:
                                {
                                    _thisTransform.position = new Vector3(_thisTransform.position.x, _thisTransform.position.y, _tweenSingle.Value);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTweenTransformParameterType.LocalPosition:
                    {
                        switch (_parameterComponent)
                        {
                            case TDimensionComponent.X:
                                {
                                    _thisTransform.localPosition = new Vector3(_tweenSingle.Value, _thisTransform.localPosition.y, _thisTransform.localPosition.z);
                                }
                                break;
                            case TDimensionComponent.Y:
                                {
                                    _thisTransform.localPosition = new Vector3(_thisTransform.localPosition.x, _tweenSingle.Value, _thisTransform.localPosition.z);
                                }
                                break;
                            case TDimensionComponent.Z:
                                {
                                    _thisTransform.localPosition = new Vector3(_thisTransform.localPosition.x, _thisTransform.localPosition.y, _tweenSingle.Value);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTweenTransformParameterType.Rotation:
                    {
                        switch (_parameterComponent)
                        {
                            case TDimensionComponent.X:
                                {
                                    _thisTransform.eulerAngles = new Vector3(_tweenSingle.Value, _thisTransform.eulerAngles.y, _thisTransform.eulerAngles.z);
                                }
                                break;
                            case TDimensionComponent.Y:
                                {
                                    _thisTransform.eulerAngles = new Vector3(_thisTransform.eulerAngles.x, _tweenSingle.Value, _thisTransform.eulerAngles.z);
                                }
                                break;
                            case TDimensionComponent.Z:
                                {
                                    _thisTransform.eulerAngles = new Vector3(_thisTransform.eulerAngles.x, _thisTransform.eulerAngles.y, _tweenSingle.Value);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTweenTransformParameterType.LocalRotation:
                    {
                        switch (_parameterComponent)
                        {
                            case TDimensionComponent.X:
                                {
                                    _thisTransform.localEulerAngles = new Vector3(_tweenSingle.Value, _thisTransform.localEulerAngles.y, _thisTransform.localEulerAngles.z);
                                }
                                break;
                            case TDimensionComponent.Y:
                                {
                                    _thisTransform.localEulerAngles = new Vector3(_thisTransform.localEulerAngles.x, _tweenSingle.Value, _thisTransform.localEulerAngles.z);
                                }
                                break;
                            case TDimensionComponent.Z:
                                {
                                    _thisTransform.localEulerAngles = new Vector3(_thisTransform.localEulerAngles.x, _thisTransform.localEulerAngles.y, _tweenSingle.Value);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTweenTransformParameterType.Scale:
                    {
                        switch (_parameterComponent)
                        {
                            case TDimensionComponent.X:
                                {
                                    _thisTransform.localScale = new Vector3(_tweenSingle.Value, _thisTransform.localScale.y, _thisTransform.localScale.z);
                                }
                                break;
                            case TDimensionComponent.Y:
                                {
                                    _thisTransform.localScale = new Vector3(_thisTransform.localScale.x, _tweenSingle.Value, _thisTransform.localScale.z);
                                }
                                break;
                            case TDimensionComponent.Z:
                                {
                                    _thisTransform.localScale = new Vector3(_thisTransform.localScale.x, _thisTransform.localScale.y, _tweenSingle.Value);
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
