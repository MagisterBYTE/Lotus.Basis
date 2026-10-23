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
    /// Компонент для хранения и управления анимацией цветового значения
    /// </summary>
    /// <remarks>
    /// Реализация компонента для хранения (используется хранилища анимационных кривых) и управления анимацией
    /// цветового значения
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Tween/Animation Color")]
    public class LotusTweenColor : MonoBehaviour
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        [LotusDisplayName(nameof(Tween))]
        [LotusSerializeMember(nameof(Tween))]
        internal CTweenColor _tweenColor;
        [SerializeField]
        [LotusDisplayName(nameof(UseMaterial))]
        [LotusSerializeMember(nameof(UseMaterial))]
        internal bool _useMaterial;
        [NonSerialized]
        internal Material _targetMaterial;
        #endregion

        #region Properties
        /// <summary>
        /// Аниматор цветового значения
        /// </summary>
        public CTweenColor Tween
        {
            get { return _tweenColor; }
        }

        /// <summary>
        /// Текущее значение переменной
        /// </summary>
        public Color Value
        {
            get { return _tweenColor.Value; }
        }

        /// <summary>
        /// Начальное значение переменной
        /// </summary>
        public Color StartValue
        {
            get { return _tweenColor.StartValue; }
            set { _tweenColor.StartValue = value; }
        }

        /// <summary>
        /// Целевое значение переменной
        /// </summary>
        public Color TargetValue
        {
            get { return _tweenColor.TargetValue; }
            set { _tweenColor.TargetValue = value; }
        }

        /// <summary>
        /// Режим проигрывания анимации
        /// </summary>
        public TTweenWrapMode WrapMode
        {
            get { return _tweenColor.WrapMode; }
            set { _tweenColor.WrapMode = value; }
        }

        /// <summary>
        /// Время в течение которого должно измениться значение переменной от начального к конечной
        /// </summary>
        public float Duration
        {
            get { return _tweenColor.Duration; }
            set { _tweenColor.Duration = value; }
        }

        /// <summary>
        /// Время в течение которого должно измениться значение переменной от начального к конечной
        /// </summary>
        [Obsolete("Используйте Duration")]
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
            get { return _tweenColor.NormalizeTime; }
        }

        /// <summary>
        /// Статус игнорирования масштабирования времени
        /// </summary>
        public bool IgnoreTimeScale
        {
            get { return _tweenColor.IgnoreTimeScale; }
            set { _tweenColor.IgnoreTimeScale = value; }
        }

        /// <summary>
        /// Количество циклов проигрывания циклических анимаций. 0 - бесконечно
        /// </summary>
        public int CountLoop
        {
            get { return _tweenColor.CountLoop; }
            set { _tweenColor.CountLoop = value; }
        }

        /// <summary>
        /// Текущие количество проигрывания циклических анимаций
        /// </summary>
        public int CurrentCountLoop
        {
            get { return _tweenColor.CurrentCountLoop; }
            set { _tweenColor.CurrentCountLoop = value; }
        }

        /// <summary>
        /// Статус применения изменяемого значения к материалу
        /// </summary>
        public bool UseMaterial
        {
            get { return _useMaterial; }
            set { _useMaterial = value; }
        }

        /// <summary>
        /// Статус анимации
        /// </summary>
        public bool IsPlay
        {
            get { return _tweenColor.IsPlay; }
        }

        /// <summary>
        /// Установка/снятие паузы анимации
        /// </summary>
        public bool IsPause
        {
            get { return _tweenColor.IsPause; }
            set { _tweenColor.IsPause = value; }
        }

        /// <summary>
        /// Статус проигрывания анимации вперед
        /// </summary>
        public bool IsForward
        {
            get { return _tweenColor.IsForward; }
        }

        /// <summary>
        /// Событие для нотификации о начале анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationStart
        {
            get { return _tweenColor.OnAnimationStart; }
            set { _tweenColor.OnAnimationStart = value; }
        }

        /// <summary>
        /// Событие для нотификации об окончании анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationCompleted
        {
            get { return _tweenColor.OnAnimationCompleted; }
            set { _tweenColor.OnAnimationCompleted = value; }
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
            var renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                _targetMaterial = renderer.sharedMaterial;
            }
        }

        /// <summary>
        /// Обновление скрипта каждый кадр
        /// </summary>
        public void Update()
        {
            if (_tweenColor.IsPlay)
            {
                _tweenColor.UpdateAnimation();
            }

            if (_useMaterial)
            {
                UpdateMaterial();
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Первичная инициализация данных и параметров компонента
        /// </summary>
        public void Init()
        {
            if (_tweenColor == null)
            {
                _tweenColor = new CTweenColor();
            }
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            _tweenColor.StartAnimation();
        }

        /// <summary>
        /// Остановка анимации
        /// </summary>
        public void StopAnimation()
        {
            _tweenColor.StopAnimation();
        }

        /// <summary>
        /// Обновление материала
        /// </summary>
        public void UpdateMaterial()
        {
            if (_targetMaterial != null)
            {
                _targetMaterial.color = _tweenColor.Value;
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
