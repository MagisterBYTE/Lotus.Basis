#if UNITY_2017_1_OR_NEWER
using System;
using System.ComponentModel;

using Lotus.Core.Serialization;

using UnityEngine;

namespace Lotus.Core
{
    /**
     * \defgroup CoreUnityTween Подсистема анимации Unity
     * \ingroup Core
     * \brief Подсистема предназначена для анимации различных параметров посредством анимационных кривых или
        функций плавности. Данная подсистема специально предназначена для типовой анимации различных параметров
        (трансформации, цвета, спрайта) с поддержкой различных режимов посредством анимационных кривых.
     * \details
        //## Возможности/особенности
        1. Полноценное управления различными режимами воспроизведения анимации
        2. Информирование путем событий о начале и окончании анимации
        3. Возможность использования как готовых компонент, так и независимых классов в пользовательских скриптах

        //## Описание
        Подсистема анимации обеспечивает плавное изменение величины строго определенного типа по определенному правилу.
        В большинстве случаев в качестве правила (источника) выступает анимационная кривая.
        Использование в качестве правила (источника) функции запланировано на будущие выпуски.

        //## Использование хранилища кривых
        Для централизованного доступа, хранения и представления анимационных кривых используется
        системный ресурс \ref Lotus.Core.LotusTweenCurveStorage.
        Данный ресурс представляет методы добавления, удаления анимационных кривых, доступ к кривой по индексу,
        вычисление значения кривой по ключу времени, а также методы по сохранению данных всех анимационных кривых
        в файл и последующей загрузки.

        //## Использование хранилища анимации
        Для централизованного доступа, хранения и представления анимации спрайтов используется
        системный ресурс \ref Lotus.Core.LotusTweenSpriteStorage.
        Данный ресурс представляет методы добавления, удаления, получения анимационных спрайтов(последовательности
        спрайтов одной анимационной цепочки), а также заполнение анимационных цепочек на основе текстуры спрайта.

        //## Использование
        1. Путем создания переменных класса. В качестве типа переменных могут выступать типы: \ref Lotus.Core.CTweenInteger, \ref Lotus.Core.CTweenSingle и другие.
        2. В методе Update или LateUpdate должен выполнятся метод UpdateAnimation.
        3. В качестве отдельных компонент \ref Lotus.Core.LotusTweenSingle, \ref Lotus.Core.LotusTweenColor.
        4. Для работы подсистемы анимации на сцене должен присутствовать центральный диспетчер анимации \ref Lotus.Core.LotusTweenDispatcher.
     * @{
     */
    /// <summary>
    /// Источник данных по проигрыванию анимации
    /// </summary>
    /// <remarks>
    /// Воспроизведение всегда идёт по анимационной кривой. Значение FunctionEasing не реализовано
    /// </remarks>
    public enum TTweenAnimationSource
    {
        /// <summary>
        /// Функция плавности
        /// </summary>
        [Obsolete("Функции плавности не реализованы. Используйте AnimationCurve")]
        FunctionEasing,

        /// <summary>
        /// Анимационная кривая
        /// </summary>
        AnimationCurve
    }

    /// <summary>
    /// Режим проигрывания анимации
    /// </summary>
    public enum TTweenWrapMode
    {
        /// <summary>
        /// Однократное проигрывание анимации
        /// </summary>
        Once,

        /// <summary>
        /// Однократное проигрывание анимации (назад)
        /// </summary>
        OnceBackward,

        /// <summary>
        /// Циклическое проигрывание анимации
        /// </summary>
        Loop,

        /// <summary>
        /// Однократное проигрывание анимации вперед и назад
        /// </summary>
        PingPong,

        /// <summary>
        /// Циклическое проигрывание анимации вперед и назад
        /// </summary>
        LoopPingPong
    }

    /// <summary>
    /// Параметр трансформации (для связывания параметров)
    /// </summary>
    public enum TTweenTransformParameterType
    {
        /// <summary>
        /// Позиция
        /// </summary>
        Position,

        /// <summary>
        /// Локальная позиция
        /// </summary>
        LocalPosition,

        /// <summary>
        /// Вращение
        /// </summary>
        Rotation,

        /// <summary>
        /// Локальное вращение
        /// </summary>
        LocalRotation,

        /// <summary>
        /// Масштабирование
        /// </summary>
        Scale
    }

    /// <summary>
    /// Обобщенный интерфейс для анимации значения
    /// </summary>
    public interface ILotusTweenValue
    {
        #region Properties
        /// <summary>
        /// Игнорирование масштабирование времени
        /// </summary>
        bool IgnoreTimeScale { get; }

        /// <summary>
        /// Продолжительность анимации
        /// </summary>
        float Duration { get; }
        #endregion

        #region Main methods
        /// <summary>
        /// Применение текущего значения к анимации
        /// </summary>
        /// <param name="percentage">Процент в частях</param>
        void TweenValue(float percentage);
        #endregion
    }

    /// <summary>
    /// Класс для хранения и управления анимации обобщенным типом
    /// </summary>
    /// <typeparam name="TAnimatableType">Тип анимационного значения</typeparam>
    [Serializable]
    public class TweenGenericObject<TAnimatableType> : ILotusTweenValue
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        [LotusSerializeMember(nameof(Name))]
        internal string _name;
        [SerializeField]
        [LotusSerializeMember(nameof(WrapMode))]
        internal TTweenWrapMode _wrapMode;
        [NonSerialized]
        internal TAnimatableType _value;
        [SerializeField]
        [LotusSerializeMember(nameof(StartValue))]
        internal TAnimatableType _startValue;
        [SerializeField]
        [LotusSerializeMember(nameof(TargetValue))]
        internal TAnimatableType _targetValue;
        [SerializeField]
        [LotusSerializeMember(nameof(IgnoreTimeScale))]
        internal bool _ignoreTimeScale;
        [NonSerialized]
        internal TAnimatableType _distance;
        [SerializeField]
        [LotusSerializeMember(nameof(Duration))]
        internal float _duration = 0.3f;
        [NonSerialized]
        internal float _correctTime;
        [NonSerialized]
        internal float _currentTime;
        [NonSerialized]
        internal float _deltaTime;
        [NonSerialized]
        internal float _normalizeTime;
        [NonSerialized]
        internal bool _preEnd;
        [NonSerialized]
        internal bool _start;
        [NonSerialized]
        internal int _currentCountLoop;
        [SerializeField]
        [LotusSerializeMember(nameof(CountLoop))]
        internal int _countLoop;
        [NonSerialized]
        internal bool _isPause;
        [NonSerialized]
        internal bool _isForward;
        [SerializeField]
        [LotusSerializeMember(nameof(CurveIndexForward))]
        internal int _curveIndexForward;
        [SerializeField]
        [LotusSerializeMember(nameof(CurveIndexBackward))]
        internal int _curveIndexBackward;
        [NonSerialized]
        internal int _currentIndexForward = -1;

        // События
        [NonSerialized]
        internal Action<string> _onAnimationStart;
        [NonSerialized]
        internal Action<string> _onAnimationCompleted;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Название анимации
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Текущее значение переменной
        /// </summary>
        public TAnimatableType Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Начальное значение переменной
        /// </summary>
        public TAnimatableType StartValue
        {
            get { return _startValue; }
            set { _startValue = value; }
        }

        /// <summary>
        /// Целевое значение переменной
        /// </summary>
        public TAnimatableType TargetValue
        {
            get { return _targetValue; }
            set { _targetValue = value; }
        }

        /// <summary>
        /// Режим проигрывания анимации
        /// </summary>
        public TTweenWrapMode WrapMode
        {
            get { return _wrapMode; }
            set { _wrapMode = value; }
        }

        /// <summary>
        /// Время(по умолчанию) в течение которого должно измениться значение переменной от начального к конечной
        /// </summary>
        public float Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        /// <summary>
        /// Нормализованное время прохождение анимации в пределах от 0 до 1
        /// </summary>
        [Description("Нормализованное время прохождение анимации в пределах от 0 до 1")]
        public float NormalizeTime
        {
            get { return _normalizeTime; }
        }

        /// <summary>
        /// Статус игнорирования масштабирования времени
        /// </summary>
        public bool IgnoreTimeScale
        {
            get { return _ignoreTimeScale; }
            set { _ignoreTimeScale = value; }
        }

        /// <summary>
        /// Статус активной анимации
        /// </summary>
        /// <remarks>
        /// Означает что нужно вызвать каждый кадр метод UpdateAnimation в производных классах
        /// </remarks>
        public bool IsPlay
        {
            get { return _start; }
        }

        /// <summary>
        /// Установка/снятие паузы анимации
        /// </summary>
        public bool IsPause
        {
            get { return _isPause; }
            set { _isPause = value; }
        }

        /// <summary>
        /// Статус проигрывания анимации вперед
        /// </summary>
        public bool IsForward
        {
            get { return _isForward; }
        }

        /// <summary>
        /// Имя анимационной кривой
        /// </summary>
        public string CurveName
        {
            get
            {
                return LotusTweenDispatcher.CurveStorage.Curves[_curveIndexForward].Name;
            }
        }

        /// <summary>
        /// Количество циклов проигрывания циклических анимаций. 0 - бесконечно
        /// </summary>
        [Description("Количество циклов проигрывания циклических анимаций. 0 - бесконечно")]
        public int CountLoop
        {
            get { return _countLoop; }
            set { _countLoop = value; }
        }

        /// <summary>
        /// Текущие количество проигрывания циклических анимаций
        /// </summary>
        public int CurrentCountLoop
        {
            get { return _currentCountLoop; }
            set { _currentCountLoop = value; }
        }

        /// <summary>
        /// Индекс кривой для анимации вперед
        /// </summary>
        public int CurveIndexForward
        {
            get { return _curveIndexForward; }
        }

        /// <summary>
        /// Индекс кривой для анимации назад
        /// </summary>
        public int CurveIndexBackward
        {
            get { return _curveIndexBackward; }
        }

        /// <summary>
        /// Текущий индекс кривой для анимации вперед
        /// </summary>
        /// <remarks>
        /// По умолчанию используется индекс кривой <see cref="CurveIndexForward"/>, однако
        /// если используется только однократное проигрывание и программное управление,
        /// то можно использовать в качестве индекса кривой индекс кривой <see cref="CurveIndexBackward"/>
        /// </remarks>
        public int CurrentIndexForward
        {
            get { return _currentIndexForward; }
            set { _currentIndexForward = value; }
        }

        //
        // СОБЫТИЯ
        //
        /// <summary>
        /// Событие для нотификации о начале анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationStart
        {
            get { return _onAnimationStart; }
            set { _onAnimationStart = value; }
        }

        /// <summary>
        /// Событие для нотификации об окончании анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationCompleted
        {
            get { return _onAnimationCompleted; }
            set { _onAnimationCompleted = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public TweenGenericObject()
        {
            _name = string.Empty;
            _correctTime = 1f;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Сброс анимации
        /// </summary>
        protected void ResetAnimation()
        {
            if (_currentIndexForward == -1)
            {
                _currentIndexForward = _curveIndexForward;
            }

            _correctTime = _duration / LotusTweenDispatcher.CurveStorage.GetTimeCurve(_currentIndexForward);
            _currentTime = 0;
            _value = _startValue;
            _isForward = true;
            _currentCountLoop = 0;
            _preEnd = false;
            _deltaTime = 0;

            if (_wrapMode == TTweenWrapMode.OnceBackward)
            {
                _isForward = false;
                _value = _targetValue;
                _correctTime = _duration / LotusTweenDispatcher.CurveStorage.GetTimeCurve(_curveIndexBackward);
            }
        }

        /// <summary>
        /// Обновление времени анимации и определение статуса продолжения
        /// </summary>
        /// <returns>Статус продолжения анимации</returns>
        protected bool UpdateTimeAnimation()
        {
            // Выход из последнего прохода
            if (_preEnd)
            {
                _start = false;
                _preEnd = false;

                if (_onAnimationCompleted != null)
                {
                    _onAnimationCompleted(_name);
                }

                return false;
            }

            if (_start && _isPause == false)
            {
                // Прошла часть времени - прибавляем
                _currentTime += _ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;

                if (_isForward)
                {
                    // Получаем значение на кривой по соответствующему нормализованному времени
                    _normalizeTime = _currentTime / _correctTime;
                    _deltaTime = LotusTweenDispatcher.CurveStorage.Evaluate(_currentIndexForward, _normalizeTime);
                }
                else
                {
                    // Получаем значение на кривой по соответствующему нормализованному времени
                    var backTime = LotusTweenDispatcher.CurveStorage.GetTimeCurve(_curveIndexBackward);
                    _normalizeTime = backTime - (_currentTime / _correctTime);
                    _deltaTime = LotusTweenDispatcher.CurveStorage.Evaluate(_curveIndexBackward, _normalizeTime);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка статуса анимации и вызов событий
        /// </summary>
        protected void CheckAnimationStatus()
        {
            // Время закончилось
            if (_currentTime >= _duration)
            {
                switch (_wrapMode)
                {
                    case TTweenWrapMode.Once:
                        {
                            _preEnd = true;
                            _value = _targetValue;
                        }
                        break;
                    case TTweenWrapMode.OnceBackward:
                        {
                            _preEnd = true;
                            _value = _startValue;
                        }
                        break;
                    case TTweenWrapMode.Loop:
                        {
                            _currentTime = 0;
                            _value = _startValue;

                            if (_countLoop != 0)
                            {
                                if (_currentCountLoop == _countLoop)
                                {
                                    _preEnd = true;
                                }
                            }

                            _currentCountLoop++;
                        }
                        break;
                    case TTweenWrapMode.PingPong:
                        {
                            if (_isForward)
                            {
                                _isForward = false;
                                _value = _targetValue;
                                _currentTime = 0;
                                _correctTime = _duration / LotusTweenDispatcher.CurveStorage.GetTimeCurve(_curveIndexForward);
                            }
                            else
                            {
                                _preEnd = true;
                                _value = _startValue;
                                _correctTime = _duration / LotusTweenDispatcher.CurveStorage.GetTimeCurve(_curveIndexBackward);
                            }
                        }
                        break;
                    case TTweenWrapMode.LoopPingPong:
                        {
                            if (_isForward)
                            {
                                _isForward = false;
                                _value = _targetValue;
                                _currentTime = 0;
                                _correctTime = _duration / LotusTweenDispatcher.CurveStorage.GetTimeCurve(_curveIndexForward);
                            }
                            else
                            {
                                _isForward = true;
                                _value = _startValue;
                                _currentTime = 0;

                                if (_countLoop != 0)
                                {
                                    if (_currentCountLoop == _countLoop)
                                    {
                                        _preEnd = true;
                                    }
                                }

                                _currentCountLoop++;

                                _correctTime = _duration / LotusTweenDispatcher.CurveStorage.GetTimeCurve(_curveIndexBackward);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Остановка анимации
        /// </summary>
        public void StopAnimation()
        {
            ResetAnimation();
            _start = false;
        }

        /// <summary>
        /// Установка индекса кривой анимации вперед по имени анимационной кривой
        /// </summary>
        /// <param name="curveName">Имя анимационной кривой</param>
        public void SetCurveForwardIndex(string curveName)
        {
            _curveIndexForward = LotusTweenDispatcher.CurveStorage.GetIndexCurve(curveName);
        }

        /// <summary>
        /// Установка индекса кривой анимации назад по имени анимационной кривой
        /// </summary>
        /// <param name="curveName">Имя анимационной кривой</param>
        public void SetCurveBackwardIndex(string curveName)
        {
            _curveIndexBackward = LotusTweenDispatcher.CurveStorage.GetIndexCurve(curveName);
        }

        /// <summary>
        /// Применение текущего значения к анимации
        /// </summary>
        /// <param name="percentage">Процент в частях</param>
        public virtual void TweenValue(float percentage)
        {
        }
        #endregion
    }

    /// <summary>
    /// Класс для хранения и управления анимации целым значением
    /// </summary>
    [Serializable]
    public class CTweenInteger : TweenGenericObject<int>, ILotusTaskInfo
    {
        #region ILotusTaskInfo properties
        /// <summary>
        /// Статус завершение задачи
        /// </summary>
        public bool IsTaskCompleted
        {
            get { return !_start; }
        }

        /// <summary>
        /// Процент выполнения задачи
        /// </summary>
        public float TaskPercentCompletion
        {
            get { return _normalizeTime; }
        }
        #endregion

        #region ILotusTaskInfo methods
        /// <summary>
        /// Запуск выполнение задачи
        /// </summary>
        /// <remarks>
        /// Здесь должна выполняться первоначальная работа по подготовки к выполнению задачи
        /// </remarks>
        public void RunTask()
        {
            StartAnimation();
        }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        /// <remarks>
        /// Непосредственное выполнение задачи. Метод будет вызываться каждый раз в зависимости от способа
        /// и режима выполнения задачи
        /// </remarks>
        public void ExecuteTask()
        {
            if (UpdateTimeAnimation())
            {
                // Изменяем значение переменной
                _value = (int)(_startValue + (_deltaTime * _distance));

                CheckAnimationStatus();
            }
        }

        /// <summary>
        /// Принудительная остановка выполнения задачи
        /// </summary>
        /// <remarks>
        /// Если задачи будет принудительно остановлена то здесь можно/нужно реализовать необходимые действия
        /// </remarks>
        public void StopTask()
        {
            StopAnimation();
        }

        /// <summary>
        /// Переустановка данных задачи
        /// </summary>
        /// <remarks>
        /// Здесь может быть выполняться работа по подготовки к выполнению задачи, но без запуска на выполнение
        /// </remarks>
        public void ResetTask()
        {
            StopAnimation();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            ResetAnimation();

            _distance = _targetValue - _startValue;

            if (_onAnimationStart != null)
            {
                _onAnimationStart(_name);
            }

            _start = true;
        }

        /// <summary>
        /// Обновление анимации
        /// </summary>
        public void UpdateAnimation()
        {
            if (UpdateTimeAnimation())
            {
                // Изменяем значение переменной
                _value = (int)(_startValue + (_deltaTime * _distance));

                CheckAnimationStatus();
            }
        }
        #endregion
    }

    /// <summary>
    /// Класс для хранения и управления анимации вещественным значением
    /// </summary>
    [Serializable]
    public class CTweenSingle : TweenGenericObject<float>, ILotusTaskInfo
    {
        #region ILotusTaskInfo properties
        /// <summary>
        /// Статус завершение задачи
        /// </summary>
        public bool IsTaskCompleted
        {
            get { return !_start; }
        }

        /// <summary>
        /// Процент выполнения задачи
        /// </summary>
        public float TaskPercentCompletion
        {
            get { return _normalizeTime; }
        }
        #endregion

        #region ILotusTaskInfo methods
        /// <summary>
        /// Запуск выполнение задачи
        /// </summary>
        /// <remarks>
        /// Здесь должна выполняться первоначальная работа по подготовки к выполнению задачи
        /// </remarks>
        public void RunTask()
        {
            StartAnimation();
        }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        /// <remarks>
        /// Непосредственное выполнение задачи. Метод будет вызываться каждый раз в зависимости от способа
        /// и режима выполнения задачи
        /// </remarks>
        public void ExecuteTask()
        {
            if (UpdateTimeAnimation())
            {
                // Изменяем значение переменной
                _value = _startValue + (_deltaTime * _distance);

                CheckAnimationStatus();
            }
        }

        /// <summary>
        /// Принудительная остановка выполнения задачи
        /// </summary>
        /// <remarks>
        /// Если задачи будет принудительно остановлена то здесь можно/нужно реализовать необходимые действия
        /// </remarks>
        public void StopTask()
        {
            StopAnimation();
        }

        /// <summary>
        /// Переустановка данных задачи
        /// </summary>
        /// <remarks>
        /// Здесь может быть выполняться работа по подготовки к выполнению задачи, но без запуска на выполнение
        /// </remarks>
        public void ResetTask()
        {
            StopAnimation();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            ResetAnimation();

            _distance = _targetValue - _startValue;

            if (_onAnimationStart != null)
            {
                _onAnimationStart(_name);
            }

            _start = true;
        }

        /// <summary>
        /// Обновление анимации
        /// </summary>
        public void UpdateAnimation()
        {
            if (UpdateTimeAnimation())
            {
                // Изменяем значение переменной
                _value = _startValue + (_deltaTime * _distance);

                CheckAnimationStatus();
            }
        }
        #endregion
    }

    /// <summary>
    /// Класс для хранения и управления анимации цветовым значением
    /// </summary>
    [Serializable]
    public class CTweenColor : TweenGenericObject<Color>, ILotusTaskInfo
    {
        #region ILotusTaskInfo properties
        /// <summary>
        /// Статус завершение задачи
        /// </summary>
        public bool IsTaskCompleted
        {
            get { return !_start; }
        }

        /// <summary>
        /// Процент выполнения задачи
        /// </summary>
        public float TaskPercentCompletion
        {
            get { return _normalizeTime; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public CTweenColor()
        {
            _value = Color.white;
            _targetValue = Color.red;
        }
        #endregion

        #region ILotusTaskInfo methods
        /// <summary>
        /// Запуск выполнение задачи
        /// </summary>
        /// <remarks>
        /// Здесь должна выполняться первоначальная работа по подготовки к выполнению задачи
        /// </remarks>
        public void RunTask()
        {
            StartAnimation();
        }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        /// <remarks>
        /// Непосредственное выполнение задачи. Метод будет вызываться каждый раз в зависимости от способа
        /// и режима выполнения задачи
        /// </remarks>
        public void ExecuteTask()
        {
            if (UpdateTimeAnimation())
            {
                // Изменяем значение переменной
                _value = _startValue + (_deltaTime * _distance);

                CheckAnimationStatus();
            }
        }

        /// <summary>
        /// Принудительная остановка выполнения задачи
        /// </summary>
        /// <remarks>
        /// Если задачи будет принудительно остановлена то здесь можно/нужно реализовать необходимые действия
        /// </remarks>
        public void StopTask()
        {
            StopAnimation();
        }

        /// <summary>
        /// Переустановка данных задачи
        /// </summary>
        /// <remarks>
        /// Здесь может быть выполняться работа по подготовки к выполнению задачи, но без запуска на выполнение
        /// </remarks>
        public void ResetTask()
        {
            StopAnimation();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            ResetAnimation();

            _distance = _targetValue - _startValue;

            if (_onAnimationStart != null)
            {
                _onAnimationStart(_name);
            }

            _start = true;
        }

        /// <summary>
        /// Обновление анимации
        /// </summary>
        public void UpdateAnimation()
        {
            if (UpdateTimeAnimation())
            {
                // Изменяем значение переменной
                _value = _startValue + (_deltaTime * _distance);

                CheckAnimationStatus();
            }
        }
        #endregion
    }

    /// <summary>
    /// Класс для хранения и управления анимации 2D вектором
    /// </summary>
    [Serializable]
    public class CTweenVector2D : TweenGenericObject<Vector2>, ILotusTaskInfo
    {
        #region ILotusTaskInfo properties
        /// <summary>
        /// Статус завершение задачи
        /// </summary>
        public bool IsTaskCompleted
        {
            get { return !_start; }
        }

        /// <summary>
        /// Процент выполнения задачи
        /// </summary>
        public float TaskPercentCompletion
        {
            get { return _normalizeTime; }
        }
        #endregion

        #region ILotusTaskInfo methods
        /// <summary>
        /// Запуск выполнение задачи
        /// </summary>
        /// <remarks>
        /// Здесь должна выполняться первоначальная работа по подготовки к выполнению задачи
        /// </remarks>
        public void RunTask()
        {
            StartAnimation();
        }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        /// <remarks>
        /// Непосредственное выполнение задачи. Метод будет вызываться каждый раз в зависимости от способа
        /// и режима выполнения задачи
        /// </remarks>
        public void ExecuteTask()
        {
            if (UpdateTimeAnimation())
            {
                // Изменяем значение переменной
                _value = _startValue + (_deltaTime * _distance);

                CheckAnimationStatus();
            }
        }

        /// <summary>
        /// Принудительная остановка выполнения задачи
        /// </summary>
        /// <remarks>
        /// Если задачи будет принудительно остановлена то здесь можно/нужно реализовать необходимые действия
        /// </remarks>
        public void StopTask()
        {
            StopAnimation();
        }

        /// <summary>
        /// Переустановка данных задачи
        /// </summary>
        /// <remarks>
        /// Здесь может быть выполняться работа по подготовки к выполнению задачи, но без запуска на выполнение
        /// </remarks>
        public void ResetTask()
        {
            StopAnimation();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            ResetAnimation();

            _distance = _targetValue - _startValue;

            if (_onAnimationStart != null)
            {
                _onAnimationStart(_name);
            }

            _start = true;
        }

        /// <summary>
        /// Обновление анимации
        /// </summary>
        public void UpdateAnimation()
        {
            if (UpdateTimeAnimation())
            {
                // Изменяем значение переменной
                _value = _startValue + (_deltaTime * _distance);

                CheckAnimationStatus();
            }
        }
        #endregion
    }

    /// <summary>
    /// Класс для хранения и управления анимации 3D вектором
    /// </summary>
    [Serializable]
    public class CTweenVector3D : TweenGenericObject<Vector3>, ILotusTaskInfo
    {
        #region ILotusTaskInfo properties
        /// <summary>
        /// Статус завершение задачи
        /// </summary>
        public bool IsTaskCompleted
        {
            get { return !_start; }
        }

        /// <summary>
        /// Процент выполнения задачи
        /// </summary>
        public float TaskPercentCompletion
        {
            get { return _normalizeTime; }
        }
        #endregion

        #region ILotusTaskInfo methods
        /// <summary>
        /// Запуск выполнение задачи
        /// </summary>
        /// <remarks>
        /// Здесь должна выполняться первоначальная работа по подготовки к выполнению задачи
        /// </remarks>
        public void RunTask()
        {
            StartAnimation();
        }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        /// <remarks>
        /// Непосредственное выполнение задачи. Метод будет вызываться каждый раз в зависимости от способа
        /// и режима выполнения задачи
        /// </remarks>
        public void ExecuteTask()
        {
            if (UpdateTimeAnimation())
            {
                // Изменяем значение переменной
                _value = _startValue + (_deltaTime * _distance);

                CheckAnimationStatus();
            }
        }

        /// <summary>
        /// Принудительная остановка выполнения задачи
        /// </summary>
        /// <remarks>
        /// Если задачи будет принудительно остановлена то здесь можно/нужно реализовать необходимые действия
        /// </remarks>
        public void StopTask()
        {
            StopAnimation();
        }

        /// <summary>
        /// Переустановка данных задачи
        /// </summary>
        /// <remarks>
        /// Здесь может быть выполняться работа по подготовки к выполнению задачи, но без запуска на выполнение
        /// </remarks>
        public void ResetTask()
        {
            StopAnimation();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            ResetAnimation();

            _distance = _targetValue - _startValue;

            if (_onAnimationStart != null)
            {
                _onAnimationStart(_name);
            }

            _start = true;
        }

        /// <summary>
        /// Обновление анимации
        /// </summary>
        public void UpdateAnimation()
        {
            if (UpdateTimeAnimation())
            {
                // Изменяем значение переменной
                _value = _startValue + (_deltaTime * _distance);

                CheckAnimationStatus();
            }
        }
        #endregion
    }

    /// <summary>
    /// Специальный класс для хранения и управления анимации спрайтами
    /// </summary>
    [Serializable]
    public class CTweenSprite : ILotusTaskInfo
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        internal string _name;
        [SerializeField]
        internal int _groupIndex;
        [SerializeField]
        internal int _groupSpriteIndex;
        [SerializeField]
        internal int _storageSpriteIndex;
        [SerializeField]
        internal TTweenWrapMode _wrapMode;
        [SerializeField]
        internal int _startFrame;
        [SerializeField]
        internal int _targetFrame;
        [SerializeField]
        internal float _duration = 0.3f;
        [NonSerialized]
        internal float _normalizeTime;
        [NonSerialized]
        internal float _currentTime;
        [NonSerialized]
        internal int _frameIndex;
        [NonSerialized]
        internal int _frameCount;
        [NonSerialized]
        internal bool _isPause;
        [NonSerialized]
        internal bool _start;
        [NonSerialized]
        internal bool _isForward;

        // События
        [NonSerialized]
        internal Action<string> _onAnimationStart;
        [NonSerialized]
        internal Action<string> _onAnimationCompleted;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Название анимации
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Индекс группы анимационных цепочек
        /// </summary>
        public int GroupIndex
        {
            get { return _groupIndex; }
            set
            {
                _groupIndex = value;
                _storageSpriteIndex = LotusTweenDispatcher.SpriteStorage.GetStorageSpriteIndex(_groupIndex, _groupSpriteIndex);
            }
        }

        /// <summary>
        /// Имя группы анимационных цепочек
        /// </summary>
        public string GroupName
        {
            get
            {
                if (_groupIndex == -1)
                {
                    return string.Empty;
                }
                else
                {
                    return LotusTweenDispatcher.SpriteStorage.GroupSprites[_groupIndex].Name;
                }
            }
        }

        /// <summary>
        /// Индекс анимационной цепочки в группе
        /// </summary>
        public int GroupSpriteIndex
        {
            get { return _groupSpriteIndex; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                if (value > LotusTweenDispatcher.SpriteStorage.GroupSprites[_groupIndex].Count - 1)
                {
                    value = LotusTweenDispatcher.SpriteStorage.GroupSprites[_groupIndex].Count - 1;
                }
                _groupSpriteIndex = value;
                _storageSpriteIndex = LotusTweenDispatcher.SpriteStorage.GetStorageSpriteIndex(_groupIndex, _groupSpriteIndex);
            }
        }

        /// <summary>
        /// Имя анимационной цепочки из группы
        /// </summary>
        public string GroupSpriteName
        {
            get
            {
                if (_groupSpriteIndex == -1)
                {
                    return string.Empty;
                }
                else
                {
                    return LotusTweenDispatcher.SpriteStorage.GroupSprites[_groupIndex][_groupSpriteIndex].Name;
                }
            }
        }

        /// <summary>
        /// Индекс анимационной цепочки из хранилища анимаций спрайтов
        /// </summary>
        public int StorageSpriteIndex
        {
            get { return _storageSpriteIndex; }
            set
            {
                _storageSpriteIndex = value;
                LotusTweenDispatcher.SpriteStorage.GetGroupIndexAndSpriteIndex(_storageSpriteIndex, ref _groupIndex, ref _groupSpriteIndex);
            }
        }

        /// <summary>
        /// Режим проигрывания анимации
        /// </summary>
        public TTweenWrapMode WrapMode
        {
            get { return _wrapMode; }
            set { _wrapMode = value; }
        }

        /// <summary>
        /// Начальный кадр
        /// </summary>
        public int StartFrame
        {
            get { return _startFrame; }
            set { _startFrame = value; }
        }

        /// <summary>
        /// Целевой кадр
        /// </summary>
        public int TargetFrame
        {
            get { return _targetFrame; }
            set { _targetFrame = value; }
        }

        /// <summary>
        /// Время в течение которого должно измениться значение переменной от начального к конечной
        /// </summary>
        public float Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        /// <summary>
        /// Нормализованное время прохождение анимации в пределах от 0 до 1
        /// </summary>
        public float NormalizeTime
        {
            get { return _normalizeTime; }
        }

        /// <summary>
        /// Номер текущего кадра
        /// </summary>
        public int FrameIndex
        {
            get { return _frameIndex; }
        }

        /// <summary>
        /// Текущий проигрываемый спрайт
        /// </summary>
        public Sprite FrameSprite
        {
            get
            {
                if (_storageSpriteIndex == -1)
                {
                    return null;
                }
                else
                {
                    return LotusTweenDispatcher.SpriteStorage.GetFrameSprite(_groupIndex, _groupSpriteIndex, _frameIndex);
                }
            }
        }

        /// <summary>
        /// Статус проигрывания анимации
        /// </summary>
        /// <remarks>
        /// Означает что нужно вызвать каждый кадр метод UpdateAnimation в производных классах
        /// </remarks>
        public bool IsPlay
        {
            get { return _start; }
        }

        /// <summary>
        /// Установка/снятие паузы анимации
        /// </summary>
        public bool IsPause
        {
            get { return _isPause; }
            set { _isPause = value; }
        }

        /// <summary>
        /// Статус проигрывания анимации вперед
        /// </summary>
        public bool IsForward
        {
            get { return _isForward; }
        }

        //
        // СОБЫТИЯ
        //
        /// <summary>
        /// Событие для нотификации о начале анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationStart
        {
            get { return _onAnimationStart; }
            set { _onAnimationStart = value; }
        }

        /// <summary>
        /// Событие для нотификации об окончании анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationCompleted
        {
            get { return _onAnimationCompleted; }
            set { _onAnimationCompleted = value; }
        }
        #endregion

        #region ILotusTaskInfo properties
        /// <summary>
        /// Статус завершение задачи
        /// </summary>
        public bool IsTaskCompleted
        {
            get
            {
                if (_wrapMode != TTweenWrapMode.Loop && _wrapMode != TTweenWrapMode.LoopPingPong)
                {
                    return !_start;
                }
                else
                {
#if UNITY_EDITOR
                    Debug.LogWarningFormat("Task sprite <{0}> WrapMode = {1}", _name, _wrapMode);
#endif
                    return true;
                }
            }
        }

        /// <summary>
        /// Процент выполнения задачи
        /// </summary>
        public float TaskPercentCompletion
        {
            get { return _normalizeTime; }
        }
        #endregion

        #region ILotusTaskInfo methods
        /// <summary>
        /// Запуск выполнение задачи
        /// </summary>
        /// <remarks>
        /// Здесь должна выполняться первоначальная работа по подготовки к выполнению задачи
        /// </remarks>
        public void RunTask()
        {
            StartAnimation();
        }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        /// <remarks>
        /// Непосредственное выполнение задачи. Метод будет вызываться каждый раз в зависимости от способа
        /// и режима выполнения задачи
        /// </remarks>
        public void ExecuteTask()
        {
            UpdateAnimation();
        }

        /// <summary>
        /// Принудительная остановка выполнения задачи
        /// </summary>
        /// <remarks>
        /// Если задачи будет принудительно остановлена то здесь можно/нужно реализовать необходимые действия
        /// </remarks>
        public void StopTask()
        {
            StopAnimation();
        }

        /// <summary>
        /// Переустановка данных задачи
        /// </summary>
        /// <remarks>
        /// Здесь может быть выполняться работа по подготовки к выполнению задачи, но без запуска на выполнение
        /// </remarks>
        public void ResetTask()
        {
            StopAnimation();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Старт анимации
        /// </summary>
        /// <param name="animationName">Имя проигрываемой анимации</param>
        public void StartAnimation(string animationName)
        {
            // Только если анимация не запущена
            if (_start == false)
            {
                for (var groupIndex = 0; groupIndex < LotusTweenDispatcher.SpriteStorage.GroupCount; groupIndex++)
                {
                    var spriteGroup = LotusTweenDispatcher.SpriteStorage.GroupSprites[groupIndex];
                    if (spriteGroup != null)
                    {
                        for (var i = 0; i < spriteGroup.Count; i++)
                        {
                            var spriteData = spriteGroup[i];
                            if (spriteData != null && spriteData.Name == animationName)
                            {
                                _groupIndex = groupIndex;
                                _groupSpriteIndex = i;
                                _storageSpriteIndex = LotusTweenDispatcher.SpriteStorage.GetStorageSpriteIndex(groupIndex, i);
                                _currentTime = 0;
                                _frameCount = spriteData.Count;
                                _start = true;
                                _isForward = true;

                                if (_onAnimationStart != null)
                                {
                                    _onAnimationStart(animationName);
                                }

                                if (_wrapMode == TTweenWrapMode.OnceBackward)
                                {
                                    _isForward = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        /// <param name="index">Индекс проигрываемой анимации</param>
        public void StartAnimation(int index)
        {
            // Только если анимация не запущена
            if (_start == false)
            {
                _groupSpriteIndex = index;
                _storageSpriteIndex = LotusTweenDispatcher.SpriteStorage.GetStorageSpriteIndex(_groupIndex, index);
                _start = true;
                _isForward = true;
                _currentTime = 0;
                _frameCount = LotusTweenDispatcher.SpriteStorage.GroupSprites[GroupIndex][index].Count;

                if (_onAnimationStart != null)
                {
                    _onAnimationStart(GroupSpriteName);
                }

                if (_wrapMode == TTweenWrapMode.OnceBackward)
                {
                    _isForward = false;
                }
            }
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            // Только если анимация не запущена
            if (_start == false)
            {
                _start = true;
                _isForward = true;
                _currentTime = 0;
                LotusTweenDispatcher.SpriteStorage.GetGroupIndexAndSpriteIndex(_storageSpriteIndex, ref _groupIndex, ref _groupSpriteIndex);
                _frameCount = LotusTweenDispatcher.SpriteStorage.GroupSprites[GroupIndex][_groupSpriteIndex].Count;

                if (_onAnimationStart != null)
                {
                    _onAnimationStart(GroupSpriteName);
                }

                if (_wrapMode == TTweenWrapMode.OnceBackward)
                {
                    _isForward = false;
                }
            }
        }

        /// <summary>
        /// Обновление анимации
        /// </summary>
        public void UpdateAnimation()
        {
            if (_start && _isPause == false)
            {
                // Прошла часть времени - прибавляем
                _currentTime += Time.deltaTime;

                if (_isForward)
                {
                    // Получаем значение на кривой по соответствующему нормализованному времени
                    _normalizeTime = _currentTime / _duration;
                }
                else
                {
                    // Получаем значение на кривой по соответствующему нормализованному времени
                    _normalizeTime = 1.0f - (_currentTime / _duration);
                }

                _frameIndex = _startFrame + (int)((_normalizeTime * (_targetFrame - _startFrame)) + 0.5f);
                _frameIndex = Mathf.Clamp(_frameIndex, 0, _frameCount - 1);

                if (_currentTime >= _duration)
                {
                    switch (_wrapMode)
                    {
                        case TTweenWrapMode.Once:
                            {
                                _start = false;
                                _frameIndex = _targetFrame;

                                if (_onAnimationCompleted != null)
                                {
                                    _onAnimationCompleted(GroupSpriteName);
                                }
                            }
                            break;
                        case TTweenWrapMode.OnceBackward:
                            {
                                _start = false;
                                _frameIndex = _startFrame;

                                if (_onAnimationCompleted != null)
                                {
                                    _onAnimationCompleted(GroupSpriteName);
                                }
                            }
                            break;
                        case TTweenWrapMode.Loop:
                            {
                                _currentTime = 0;
                                _frameIndex = _startFrame;
                            }
                            break;
                        case TTweenWrapMode.PingPong:
                            {
                                if (_isForward)
                                {
                                    _isForward = false;
                                    _frameIndex = _targetFrame;
                                    _currentTime = 0;
                                }
                                else
                                {
                                    _start = false;
                                    _frameIndex = _startFrame;

                                    if (_onAnimationCompleted != null)
                                    {
                                        _onAnimationCompleted(GroupSpriteName);
                                    }
                                }
                            }
                            break;
                        case TTweenWrapMode.LoopPingPong:
                            {
                                if (_isForward)
                                {
                                    _isForward = false;
                                    _frameIndex = _targetFrame;
                                    _currentTime = 0;
                                }
                                else
                                {
                                    _isForward = true;
                                    _frameIndex = _startFrame;
                                    _currentTime = 0;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Остановка анимации
        /// </summary>
        public void StopAnimation()
        {
            _currentTime = 0;
            _frameIndex = _startFrame;
            _start = false;
            _isForward = true;
        }
        #endregion
    }

    /**@}*/
}
#endif
