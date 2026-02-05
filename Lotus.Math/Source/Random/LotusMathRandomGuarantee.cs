using System;
using System.Collections.Generic;
using System.Linq;

namespace Lotus.Maths
{
    /** \addtogroup MathRandom
	*@{*/
    /// <summary>
    /// Генератор получения гарантированных вероятностных значений в указанном интервале.
    /// </summary>
    /// <remarks>
    /// Реализация генератора который обеспечивает точное в процентной отношении получения гарантированных
    /// вероятностных значений в указанном интервале.
    /// <para>
    /// Разберем более подробно использование объектов данного типа.
    /// </para>
    /// <para>
    /// Например, нам нужно получить объект с вероятностью 25% на 100 вызовов. В этом случае надо использовать
    /// AddProbability(1, 25). Теперь если вызвать NextProbability() - 100 раз, гарантировано 
    /// будут возвращено 25 раз значение 1, т.е. значение индекса
    /// </para>
    /// <para>
    /// Например, нам нужно получить 1-ю вещь с вероятностью 25%, 2-ю вещь с вероятностью 50% и 3-ю вещь с вероятностью 25%
    /// В этом случае надо использовать AddProbabilityList(25, 50, 25), здесь индексы присваиваются автоматически начиная с нулю
    /// Обратите внимания в сумме проценты дают 100% - это значит каждый раз будет выпадать какая-либо вещь, 
    /// т.е. будет возвращаться индекс 0 (что соответствует 1 вещи) или 1 или 2
    /// </para>
    /// </remarks>
    public class RandomGuarantee
    {
        #region Fields
        internal int _capacity;
        internal int[] _data;
        // Используем List только для хранения "правил", а не всех элементов
        internal List<(int Index, int Percent)> _rules = new();
        internal int _currentIndex;
        private readonly ILotusRandom _random; // Используем ваш быстрый генератор
        #endregion

        #region Properties
        /// <summary>
        /// Емкость генератора.
        /// </summary>
        public int Capacity
        {
            get { return _capacity; }
        }

        /// <summary>
        /// Список заданных условий.
        /// </summary>
        public List<(int Index, int Percent)> Rules
        {
            get
            {
                return _rules;
            }
        }

        /// <summary>
        /// Список заданных вероятностей в процентах (только значения шансов).
        /// </summary>
        public List<int> Probability
        {
            get
            {
                // Выбираем только процент из наших правил (Value, Percent)
                return [.. _rules.Select(r => r.Percent)];
            }
        }

        /// <summary>
        /// Список вероятностей, где каждое значение повторяется согласно его проценту.
        /// </summary>
        public List<int> ProbabilityDetail
        {
            get
            {
                var result = new List<int>();
                foreach (var rule in _rules)
                {
                    // Рассчитываем количество вхождений как при заполнении Data
                    var count = (int)Math.Round((double)rule.Percent * _capacity / 100.0);
                    for (var i = 0; i < count; i++)
                    {
                        result.Add(rule.Index);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Текущий индекс данных.
        /// </summary>
        public int CurrentIndex
        {
            get { return _currentIndex; }
        }

        /// <summary>
        /// Текущее значение данных.
        /// </summary>
        public int CurrentValue
        {
            get
            {
                if (_currentIndex < 0 || _currentIndex >= _capacity) return -1;
                return _data[_currentIndex];
            }
        }

        /// <summary>
        /// Данные.
        /// </summary>
        public int[] Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Текущая суммарная вероятность в процентах.
        /// </summary>
        public int TotalProbabilitySetted => _rules.Sum(r => r.Percent);
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует базовую емкость генератора.
        /// </summary>
        /// <param name="random">Генератор.</param>
        /// <param name="capacity">Емкость генератора.</param>
        public RandomGuarantee(ILotusRandom random, int capacity = 100)
        {
            _random = random;
            _capacity = capacity;
            _data = new int[_capacity];
            _currentIndex = -1;

            // Инициализируем массив пустотой сразу
            Array.Fill(_data, -1);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Пересобирает массив данных на основе установленных вероятностей и перемешивает его.
        /// </summary>
        public void Reset()
        {
            var writePos = 0;

            // 1. Заполняем массив согласно правилам
            foreach (var rule in _rules)
            {
                // Используем double для более точного распределения при разной емкости
                var count = (int)Math.Round((double)rule.Percent * _capacity / 100.0);

                for (var i = 0; i < count && writePos < _capacity; i++)
                {
                    _data[writePos++] = rule.Index;
                }
            }

            // 2. Заполняем оставшееся место "пустотой" (-1)
            while (writePos < _capacity)
            {
                _data[writePos++] = -1;
            }

            // 3. Перемешивание (Fisher-Yates) с использованием вашего ILotusRandom
            for (var i = _capacity - 1; i > 0; i--)
            {
                var j = (int)(_random.NextInteger((uint)i + 1));
                var temp = _data[i];
                _data[i] = _data[j];
                _data[j] = temp;
            }

            _currentIndex = -1;
        }

        /// <summary>
        /// Переопределение емкости генератора.
        /// </summary>
        /// <remarks>
        /// Автоматически очищается список вероятностей, его надо создавать по-новому.
        /// </remarks>
        /// <param name="capacity">Емкость генератора.</param>
        public void ResetCapacity(int capacity)
        {
            _capacity = capacity;
            _data = new int[_capacity];
            _rules = [];
            _currentIndex = -1;
        }

        /// <summary>
        /// Добавление вероятности значения.
        /// </summary>
        /// <remarks>
        /// Индекс значения и есть статус выпадения этого значения. Должен быть больше нуля.
        /// </remarks>
        /// <param name="index">Индекс вероятности.</param>
        /// <param name="probability">Вероятность значения в процентах.</param>
        public void AddProbability(int index, int probability)
        {
            _rules.Add((index, probability));
            // Reset() лучше вызывать вручную после добавления всех вероятностей, 
            // либо оставить здесь, если добавления редкие.
            Reset();
        }

        /// <summary>
        /// Добавление вероятности значения списком.
        /// </summary>
        /// <remarks>
        /// Здесь индексы присваиваются автоматически начиная с нулю.
        /// </remarks>
        /// <param name="probabilities">Вероятность значения в процентах.</param>
        public void AddProbabilityList(params int[] probabilities)
        {
            for (var i = 0; i < probabilities.Length; i++)
            {
                _rules.Add((i, probabilities[i]));
            }
            Reset();
        }

        /// <summary>
        /// Очистка списка вероятностей.
        /// </summary>
        public void ClearProbability()
        {
            _rules.Clear();
            _currentIndex = -1;
            Array.Fill(_data, -1);
        }

        /// <summary>
        /// Получение следующий вероятности.
        /// </summary>
        /// <returns>Следующая вероятность.</returns>
        public int NextProbability()
        {
            // Безопасный переход по кольцу
            _currentIndex = (_currentIndex + 1) % _capacity;
            return _data[_currentIndex];
        }

        /// <summary>
        /// Получение следующий вероятности в перезапуск по новому в конце цикла.
        /// </summary>
        /// <returns>Следующая вероятность.</returns>
        public int NextProbabilityAndReset()
        {
            if (_currentIndex >= _capacity - 1)
            {
                Reset(); // Перемешиваем заново перед новым циклом
            }
            return NextProbability();
        }

        /// <summary>
        /// Проверяет, совпадает ли СЛЕДУЮЩЕЕ значение с заданным индексом, 
        /// и ТОЛЬКО ТОГДА подтверждает сдвиг указателя.
        /// </summary>
        /// <param name="index">Индекс для проверки (например, ID предмета).</param>
        /// <returns>True, если выпало искомое значение.</returns>
        public bool CheckProbability(int index)
        {
            // Вычисляем, какой индекс будет следующим, не меняя _currentIndex
            var nextIdx = (_currentIndex + 1) % _capacity;

            if (_data[nextIdx] == index)
            {
                _currentIndex = nextIdx; // Подтверждаем переход
                return true;
            }

            return false;
        }

        /// <summary>
        /// Просто заглядывает в следующее значение без перемещения указателя.
        /// </summary>
        public int PeekNext()
        {
            var nextIdx = (_currentIndex + 1) % _capacity;
            return _data[nextIdx];
        }

        /// <summary>
        /// Получение данных вероятностей в виде списка строк.
        /// </summary>
        /// <returns>Список строк.</returns>
        public IList<string> GetDataStrings()
        {
            var lines = new string[_data.Length];

            for (var i = 0; i < _data.Length; i++)
            {
                lines[i] = "i = " + i.ToString() + ", value = " + Data[i].ToString();
            }

            return lines;
        }
        #endregion
    }
    /**@}*/
}