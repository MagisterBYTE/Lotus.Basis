using System;
using System.Collections.Generic;

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
        internal List<int> _probability;
        internal int _currentIndex;
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
        /// Список вероятностей в процентах.
        /// </summary>
        public List<int> Probability
        {
            get { return _probability; }
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
                if (_currentIndex == -1)
                {
                    return _currentIndex;
                }
                else
                {
                    return _data[_currentIndex];
                }
            }
        }

        /// <summary>
        /// Данные.
        /// </summary>
        public int[] Data
        {
            get { return _data; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует базовую емкость генератора.
        /// </summary>
        /// <param name="capacity">Емкость генератора.</param>
        public RandomGuarantee(int capacity = 100)
        {
            _capacity = capacity;
            _data = new int[_capacity];
            _probability = new List<int>();
            _currentIndex = -1;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Перезапуск данных.
        /// </summary>
        public void Reset()
        {
            for (var i = 0; i < _probability.Count; i++)
            {
                _data[i] = _probability[i];
            }

            for (var ir = _probability.Count; ir < _capacity; ir++)
            {
                _data[ir] = -1;
            }

            var rnd = new Random();
            var n = _data.Length;
            while (n > 1)
            {
                n--;
                var k = rnd.Next(n + 1);

                var old = _data[n];
                _data[n] = _data[k];
                _data[k] = old;
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
            _probability.Clear();
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
            var count = probability * _capacity / 100;
            for (var i = 0; i < count; i++)
            {
                _probability.Add(index);
            }

            Reset();
        }

        /// <summary>
        /// Добавление вероятности значения списком.
        /// </summary>
        /// <remarks>
        /// Здесь индексы присваиваются автоматически начиная с нулю.
        /// </remarks>
        /// <param name="probability">Вероятность значения в процентах.</param>
        public void AddProbabilityList(params int[] probability)
        {
            for (var index = 0; index < probability.Length; index++)
            {
                var count = probability[index] * _capacity / 100;
                for (var i = 0; i < count; i++)
                {
                    _probability.Add(index);
                }
            }

            Reset();
        }

        /// <summary>
        /// Очистка списка вероятностей.
        /// </summary>
        public void ClearProbability()
        {
            _probability.Clear();
        }

        /// <summary>
        /// Получение следующий вероятности.
        /// </summary>
        /// <returns>Следующая вероятность.</returns>
        public int NextProbability()
        {
            if (_currentIndex == _capacity - 1)
            {
                _currentIndex = 0;
            }
            else
            {
                _currentIndex++;
            }

            return _data[_currentIndex];
        }

        /// <summary>
        /// Получение следующий вероятности в перезапуск по новому в конце цикла.
        /// </summary>
        /// <returns>Следующая вероятность.</returns>
        public int NextProbabilityAndReset()
        {
            if (_currentIndex == _capacity - 1)
            {
                Reset();
                _currentIndex = 0;
            }
            else
            {
                _currentIndex++;
            }

            return _data[_currentIndex];
        }

        /// <summary>
        /// Проверка на совпадение данной вероятности.
        /// </summary>
        /// <param name="index">Индекс вероятности.</param>
        /// <returns>Статус вероятность.</returns>
        public bool CheckProbability(int index)
        {
            if (_currentIndex == _capacity - 1)
            {
                _currentIndex = 0;
            }
            else
            {
                _currentIndex++;
            }

            return _data[_currentIndex] == index;
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