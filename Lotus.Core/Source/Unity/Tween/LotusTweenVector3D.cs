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
    /// Компонент для хранения и управления анимацией значением трехмерного вектора
    /// </summary>
    /// <remarks>
    /// Реализация компонента для хранения (используется хранилища анимационных кривых) и управления анимацией
    /// трехмерного вектора
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Tween/Animation Vector3D")]
    public class LotusTweenVector3D : MonoBehaviour
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        [LotusDisplayName(nameof(Tween))]
        [LotusSerializeMember(nameof(Tween))]
        internal CTweenVector3D _tweenVector3D;

        [SerializeField]
        [LotusDisplayName(nameof(UseTransform))]
        [LotusSerializeMember(nameof(UseTransform))]
        internal bool _useTransform;

        [SerializeField]
        [LotusDisplayName(nameof(Parameter))]
        [LotusSerializeMember(nameof(Parameter))]
        internal TTweenTransformParameterType _parameter;

        [NonSerialized]
        internal Transform _thisTransform;
        #endregion

        #region Properties
        /// <summary>
        /// Аниматор значения 3D вектора
        /// </summary>
        public CTweenVector3D Tween
        {
            get { return _tweenVector3D; }
        }

        /// <summary>
        /// Текущее значение переменной
        /// </summary>
        public Vector3 Value
        {
            get { return _tweenVector3D.Value; }
        }

        /// <summary>
        /// Начальное значение переменной
        /// </summary>
        public Vector3 StartValue
        {
            get { return _tweenVector3D.StartValue; }
            set { _tweenVector3D.StartValue = value; }
        }

        /// <summary>
        /// Целевое значение переменной
        /// </summary>
        public Vector3 TargetValue
        {
            get { return _tweenVector3D.TargetValue; }
            set { _tweenVector3D.TargetValue = value; }
        }

        /// <summary>
        /// Режим проигрывания анимации
        /// </summary>
        public TTweenWrapMode WrapMode
        {
            get { return _tweenVector3D.WrapMode; }
            set { _tweenVector3D.WrapMode = value; }
        }

        /// <summary>
        /// Время в течение которого должно измениться значение переменной от начального к конечной
        /// </summary>
        public float Duration
        {
            get { return _tweenVector3D.Duration; }
            set { _tweenVector3D.Duration = value; }
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
            get { return _tweenVector3D.NormalizeTime; }
        }

        /// <summary>
        /// Статус игнорирования масштабирования времени
        /// </summary>
        public bool IgnoreTimeScale
        {
            get { return _tweenVector3D.IgnoreTimeScale; }
            set { _tweenVector3D.IgnoreTimeScale = value; }
        }

        /// <summary>
        /// Количество циклов проигрывания циклических анимаций. 0 - бесконечно
        /// </summary>
        public int CountLoop
        {
            get { return _tweenVector3D.CountLoop; }
            set { _tweenVector3D.CountLoop = value; }
        }

        /// <summary>
        /// Текущие количество проигрывания циклических анимаций
        /// </summary>
        public int CurrentCountLoop
        {
            get { return _tweenVector3D.CurrentCountLoop; }
            set { _tweenVector3D.CurrentCountLoop = value; }
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
        /// Статус анимации
        /// </summary>
        public bool IsPlay
        {
            get { return _tweenVector3D.IsPlay; }
        }

        /// <summary>
        /// Установка/снятие паузы анимации
        /// </summary>
        public bool IsPause
        {
            get { return _tweenVector3D.IsPause; }
            set { _tweenVector3D.IsPause = value; }
        }

        /// <summary>
        /// Статус проигрывания анимации вперед
        /// </summary>
        public bool IsForward
        {
            get { return _tweenVector3D.IsForward; }
        }

        /// <summary>
        /// Событие для нотификации о начале анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationStart
        {
            get { return _tweenVector3D.OnAnimationStart; }
            set { _tweenVector3D.OnAnimationStart = value; }
        }

        /// <summary>
        /// Событие для нотификации об окончании анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationCompleted
        {
            get { return _tweenVector3D.OnAnimationCompleted; }
            set { _tweenVector3D.OnAnimationCompleted = value; }
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
            if (_tweenVector3D.IsPlay)
            {
                _tweenVector3D.UpdateAnimation();
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
            if (_tweenVector3D == null)
            {
                _tweenVector3D = new CTweenVector3D();
            }
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            _tweenVector3D.StartAnimation();
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        /// <param name="targetPosition">Целевая позиция</param>
        public void StartAnimation(Vector3 targetPosition)
        {
            _tweenVector3D.StartValue = _thisTransform.position;
            _tweenVector3D.TargetValue = targetPosition;
            _tweenVector3D.StartAnimation();
        }

        /// <summary>
        /// Остановка анимации
        /// </summary>
        public void StopAnimation()
        {
            _tweenVector3D.StopAnimation();
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
                        _thisTransform.position = _tweenVector3D.Value;
                    }
                    break;
                case TTweenTransformParameterType.LocalPosition:
                    {
                        _thisTransform.localPosition = _tweenVector3D.Value;
                    }
                    break;
                case TTweenTransformParameterType.Rotation:
                    {
                        _thisTransform.eulerAngles = _tweenVector3D.Value;
                    }
                    break;
                case TTweenTransformParameterType.LocalRotation:
                    {
                        _thisTransform.localEulerAngles = _tweenVector3D.Value;
                    }
                    break;
                case TTweenTransformParameterType.Scale:
                    {
                        _thisTransform.localScale = _tweenVector3D.Value;
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
