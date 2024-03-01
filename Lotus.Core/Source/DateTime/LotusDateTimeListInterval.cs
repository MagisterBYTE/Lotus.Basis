using System;

namespace Lotus.Core
{
    /** \addtogroup CoreDateTime
    *@{*/
    /// <summary>
    /// Коллекция содержащая объекты реализующие интерфейс <see cref="ILotusDateTimeable"/> с определённым временным интервалом.
    /// </summary>
    /// <remarks>
    /// Сама коллекция образует определённый временной период, элементы которой расположены  в соответствии с 
    /// временным интервалом.
    /// </remarks>
    /// <typeparam name="TItemTimeable">Тип элемента списка.</typeparam>
    public class ListTimeInterval<TItemTimeable> : ListArray<TItemTimeable> where TItemTimeable : ILotusDateTimeable
    {
        #region Fields
        // Основные параметры
        protected internal TTimeInterval _timeInterval;

        // Служебные данные
        protected internal TItemTimeable _dummyItem = Activator.CreateInstance<TItemTimeable>();
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Временной интервал.
        /// </summary>
        /// <remarks>
        /// Временной интервал обозначает что свойство <see cref="ILotusDateTimeable.Date"/> элементов списка 
        /// отличаются ни кратную величин интервала
        /// </remarks>
        public TTimeInterval TimeInterval
        {
            get { return _timeInterval; }
            set { _timeInterval = value; }
        }

        /// <summary>
        /// Количество минут временного периода.
        /// </summary>
        public int CountMinutes
        {
            get { return (DateLast - DateFirst).HasValue ? (DateLast - DateFirst)!.Value.Minutes : 0; }
        }

        /// <summary>
        /// Количество часов временного периода.
        /// </summary>
        public int CountHours
        {
            get { return (DateLast - DateFirst).HasValue ? (DateLast - DateFirst)!.Value.Hours : 0; }
        }

        /// <summary>
        /// Количество дней временного периода.
        /// </summary>
        public int CountDay
        {
            get { return (DateLast - DateFirst).HasValue ? (DateLast - DateFirst)!.Value.Days : 0; }
        }

        /// <summary>
        /// Количество недель временного периода.
        /// </summary>
        public int CountWeek
        {
            get
            {
                double count_days = (DateLast - DateFirst).HasValue ? (DateLast - DateFirst)!.Value.Days : 0;
                return (int)Math.Ceiling(count_days / 7.0);
            }
        }

        //
        // ПРОИЗВОДНЫЕ ДАННЫЕ
        //
        /// <summary>
        /// Первый объект даты-времени.
        /// </summary>
        public DateTime? DateFirst
        {
            get { return ItemFirst?.Date; }
        }

        /// <summary>
        /// Предпоследний объект даты-времени.
        /// </summary>
        public DateTime? DatePenultimate
        {
            get { return ItemPenultimate?.Date; }
        }

        /// <summary>
        /// Последний объект даты-времени.
        /// </summary>
        public DateTime? DateLast
        {
            get { return ItemLast?.Date; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public ListTimeInterval()
            : base(120)
        {
            _timeInterval = TTimeInterval.Daily;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        public ListTimeInterval(int capacity)
            : base(capacity)
        {
            _timeInterval = TTimeInterval.Daily;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="timeInterval">Временной интервал.</param>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        public ListTimeInterval(TTimeInterval timeInterval, int capacity = 120)
            : base(capacity)
        {
            _timeInterval = timeInterval;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="timeInterval">Временной интервал.</param>
        /// <param name="startData">Дата начала периода.</param>
        /// <param name="endData">Дата окончания периода.</param>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        public ListTimeInterval(TTimeInterval timeInterval, DateTime startData, DateTime endData, int capacity = 120)
            : base(capacity)
        {
            _timeInterval = timeInterval;
            AssingTimePeriod(startData, endData);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Присвоение указанного временного периода.
        /// </summary>
        /// <param name="startData">Дата начала периода.</param>
        /// <param name="endData">Дата окончания периода.</param>
        public void AssingTimePeriod(DateTime startData, DateTime endData)
        {
            Clear();

            var current = startData;
            var first_item = Activator.CreateInstance<TItemTimeable>();
            first_item.Date = startData;
            Add(first_item);
            switch (_timeInterval)
            {
                case TTimeInterval.Minutely:
                    {
                        while (current < endData)
                        {
                            current += TimeSpan.FromMinutes(1);

                            if (current <= endData)
                            {
                                var current_item = Activator.CreateInstance<TItemTimeable>();
                                current_item.Date = current;
                                Add(current_item);
                            }
                        }
                    }
                    break;
                case TTimeInterval.Hourly:
                    {
                        while (current < endData)
                        {
                            current += TimeSpan.FromHours(1);

                            if (current <= endData)
                            {
                                var current_item = Activator.CreateInstance<TItemTimeable>();
                                current_item.Date = current;
                                Add(current_item);
                            }
                        }
                    }
                    break;
                case TTimeInterval.Daily:
                    {
                        while (current < endData)
                        {
                            current += TimeSpan.FromDays(1);

                            if (current <= endData)
                            {
                                var current_item = Activator.CreateInstance<TItemTimeable>();
                                current_item.Date = current;
                                Add(current_item);
                            }
                        }
                    }
                    break;
                case TTimeInterval.Weekly:
                    {
                        while (current < endData)
                        {
                            current += TimeSpan.FromDays(1);

                            if (current <= endData && current.DayOfWeek == DayOfWeek.Monday)
                            {
                                var current_item = Activator.CreateInstance<TItemTimeable>();
                                current_item.Date = current;
                                Add(current_item);
                            }
                        }
                    }
                    break;
                case TTimeInterval.Monthly:
                    {
                        while (current < endData)
                        {
                            current += TimeSpan.FromDays(1);

                            if (current <= endData && current.Day == 1)
                            {
                                var current_item = Activator.CreateInstance<TItemTimeable>();
                                current_item.Date = current;
                                Add(current_item);
                            }
                        }
                    }
                    break;
                case TTimeInterval.Yearly:
                    {
                        while (current < endData)
                        {
                            current += TimeSpan.FromDays(1);

                            if (current <= endData && current.DayOfYear == 1)
                            {
                                var current_item = Activator.CreateInstance<TItemTimeable>();
                                current_item.Date = current;
                                Add(current_item);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Получение индекса элемента по указанным параметрам даты.
        /// </summary>
        /// <param name="year">Год.</param>
        /// <param name="month">Месяц.</param>
        /// <param name="day">День.</param>
        /// <returns>Индекс элемента.</returns>
        public int GetIndexFromDate(int year, int month, int day)
        {
            return GetIndexFromDate(new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <summary>
        /// Получение индекса элемента по дате.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <returns>Индекс элемента.</returns>
        public int GetIndexFromDate(DateTime date)
        {
            if (date <= DateFirst)
            {
                return 0;
            }
            if (date >= DateLast)
            {
                return LastIndex;
            }

            for (var i = 1; i < _count; i++)
            {
                if (_arrayOfItems[i]!.Date == date)
                {
                    return i;
                }
                else
                {
                    if (_arrayOfItems[i]!.Date > date)
                    {
                        return i - 1;
                    }
                }
            }

            return LastIndex;
        }

        /// <summary>
        /// Получение интерполированного значение даты указанному вещественному индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Дата.</returns>
        public DateTime? GetInterpolatedDate(float index)
        {
            if (index > 0)
            {
                var start_index = (int)Math.Floor(index);
                var end_index = (int)Math.Ceiling(index);
                var delta = index - start_index;

                return _arrayOfItems[start_index]!.Date.GetInterpolatedDate(_arrayOfItems[end_index]!.Date, delta);
            }
            else
            {
                return DateFirst;
            }
        }

        /// <summary>
        /// Подсчет количество выходных дней.
        /// </summary>
        /// <returns>Количество выходных дней.</returns>
        public int CountWeekends()
        {
            var count = 0;

            for (var i = 0; i < _count; i++)
            {
                var item = _arrayOfItems[i]!;
                if (item.Date.DayOfWeek == DayOfWeek.Sunday || item.Date.DayOfWeek == DayOfWeek.Saturday)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Удаление выходных дней.
        /// </summary>
        /// <returns>Количество удаленных дней.</returns>
        public int RemoveWeekends()
        {
            var count = RemoveAll((TItemTimeable? item) =>
            {
                if (item!.Date.DayOfWeek == DayOfWeek.Sunday || item.Date.DayOfWeek == DayOfWeek.Saturday)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

            return count;
        }

        /// <summary>
        /// Обрезать список сначала до указанной даты.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="included">Включать указанную дату в удаление.</param>
        /// <returns>Количество удаленных элементов.</returns>
        public int TrimStart(DateTime date, bool included = true)
        {
            _dummyItem.Date = date;
            return TrimClosestStart(_dummyItem, included);
        }

        /// <summary>
        /// Обрезать список с конца до указанной даты.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="included">Включать указанную дату в удаление.</param>
        /// <returns>Количество удаленных элементов.</returns>
        public int TrimEnd(DateTime date, bool included = true)
        {
            _dummyItem.Date = date;
            return TrimClosestEnd(_dummyItem, included);
        }

        /// <summary>
        /// Получение списка с указанным периодом.
        /// </summary>
        /// <typeparam name="TResult">Тип списка.</typeparam>
        /// <param name="startIndex">Начальный индекс периода.</param>
        /// <param name="endIndex">Конечный индекс периода.</param>
        /// <returns>Список.</returns>
        public virtual TResult GetListPeriod<TResult>(int startIndex, int endIndex)
            where TResult : ListTimeInterval<TItemTimeable>, new()
        {
            var list = new TResult();
            list.TimeInterval = _timeInterval;

            // Это не количество, а индекс поэтому и равно
            for (var i = startIndex; i <= endIndex; i++)
            {
                list.Add(_arrayOfItems[i]);
            }

            list.SetIndexElement();
            return list;
        }

        /// <summary>
        /// Получение списка с указанным периодом.
        /// </summary>
        /// <typeparam name="TResult">Тип списка.</typeparam>
        /// <param name="startDate">Дата начала периода.</param>
        /// <param name="endData">Дата окончания периода.</param>
        /// <returns>Список.</returns>
        public virtual TResult GetListPeriod<TResult>(DateTime startDate, DateTime endData)
            where TResult : ListTimeInterval<TItemTimeable>, new()
        {
            var start_index = GetIndexFromDate(startDate);
            var end_index = GetIndexFromDate(endData);

            return GetListPeriod<TResult>(start_index, end_index);
        }

        /// <summary>
        /// Дублирование списка с указанным периодом.
        /// </summary>
        /// <typeparam name="TResult">Тип списка.</typeparam>
        /// <param name="startIndex">Начальный индекс периода.</param>
        /// <param name="endIndex">Конечный индекс периода.</param>
        /// <returns>Список.</returns>
        public virtual TResult DublicateListPeriod<TResult>(int startIndex, int endIndex)
            where TResult : ListTimeInterval<TItemTimeable>, new()
        {
            var list = new TResult();
            list.TimeInterval = _timeInterval;

            //Это не количество, а индекс поэтому и равно
            for (var i = startIndex; i <= endIndex; i++)
            {
                list.Add((TItemTimeable)_arrayOfItems[i]!.Clone());
            }

            list.SetIndexElement();
            return list;
        }

        /// <summary>
        /// Дублирование списка с указанным периодом.
        /// </summary>
        /// <typeparam name="TResult">Тип списка.</typeparam>
        /// <param name="startDate">Дата начала периода.</param>
        /// <param name="endData">Дата окончания периода.</param>
        /// <returns>Список.</returns>
        public virtual TResult? DublicateListPeriod<TResult>(DateTime startDate, DateTime endData)
            where TResult : ListTimeInterval<TItemTimeable>, new()
        {
            var start_index = GetIndexFromDate(startDate);
            var end_index = GetIndexFromDate(endData);

            return DublicateListPeriod<TResult>(start_index, end_index);
        }

        /// <summary>
        /// Получение списка с указанным временным интервалом.
        /// </summary>
        /// <typeparam name="TResult">Тип списка.</typeparam>
        /// <param name="timeInterval">Временной интервал.</param>
        /// <returns>Список.</returns>
        public virtual TResult? GetConvertedListInterval<TResult>(TTimeInterval timeInterval)
            where TResult : ListTimeInterval<TItemTimeable>, new()
        {
            return GetConvertedListPeriodAndInterval<TResult>(timeInterval, 0, LastIndex);
        }

        /// <summary>
        /// Получение списка с указанным временным интервалом и указанным временным периодом.
        /// </summary>
        /// <typeparam name="TResult">Тип списка.</typeparam>
        /// <param name="timeInterval">Временной интервал.</param>
        /// <param name="startIndex">Начальный индекс периода.</param>
        /// <param name="endIndex">Конечный индекс периода.</param>
        /// <returns>Список.</returns>
        public virtual TResult? GetConvertedListPeriodAndInterval<TResult>(TTimeInterval timeInterval, int startIndex, int endIndex)
            where TResult : ListTimeInterval<TItemTimeable>, new()
        {
            TResult? list = null;
            switch (_timeInterval)
            {
                case TTimeInterval.Minutely:
                    break;
                case TTimeInterval.Hourly:
                    {
                        switch (timeInterval)
                        {
                            case TTimeInterval.Minutely:
                                break;
                            case TTimeInterval.Hourly:
                                {
                                    list = GetListPeriod<TResult>(startIndex, endIndex);
                                }
                                break;
                            case TTimeInterval.Daily:
                                {

                                }
                                break;
                            case TTimeInterval.Weekly:
                                break;
                            case TTimeInterval.Monthly:
                                break;
                            case TTimeInterval.Yearly:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTimeInterval.Daily:
                    {
                        switch (timeInterval)
                        {
                            case TTimeInterval.Minutely:
                                break;
                            case TTimeInterval.Hourly:
                                break;
                            case TTimeInterval.Daily:
                                {
                                    list = GetListPeriod<TResult>(startIndex, endIndex);
                                }
                                break;
                            case TTimeInterval.Weekly:
                                {
                                    list = new TResult();
                                    list.TimeInterval = TTimeInterval.Weekly;

                                    for (var i = 0; i < _count; i++)
                                    {
                                        if (_arrayOfItems[i]!.Date.DayOfWeek == DayOfWeek.Monday)
                                        {
                                            list.Add(_arrayOfItems[i]);
                                        }
                                    }
                                }
                                break;
                            case TTimeInterval.Monthly:
                                {
                                    list = new TResult();
                                    list.TimeInterval = TTimeInterval.Monthly;

                                    for (var i = 0; i < _count; i++)
                                    {
                                        if (_arrayOfItems[i]!.Date.Day == 1)
                                        {
                                            list.Add(_arrayOfItems[i]);
                                        }
                                    }
                                }
                                break;
                            case TTimeInterval.Yearly:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTimeInterval.Weekly:
                    {
                        switch (timeInterval)
                        {
                            case TTimeInterval.Minutely:
                                break;
                            case TTimeInterval.Hourly:
                                break;
                            case TTimeInterval.Daily:
                                break;
                            case TTimeInterval.Weekly:
                                {
                                    list = GetListPeriod<TResult>(startIndex, endIndex);
                                }
                                break;
                            case TTimeInterval.Monthly:
                                {
                                    list = new TResult();
                                    list.TimeInterval = TTimeInterval.Monthly;

                                    for (var i = 0; i < _count; i++)
                                    {
                                        if (_arrayOfItems[i]!.Date.Day == 1)
                                        {
                                            list.Add(_arrayOfItems[i]);
                                        }
                                    }
                                }
                                break;
                            case TTimeInterval.Yearly:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TTimeInterval.Monthly:
                    break;
                case TTimeInterval.Yearly:
                    break;
                default:
                    break;
            }

            if (list != null)
            {
                list.SetIndexElement();
            }
            return list;
        }

        /// <summary>
        /// Получение списка с указанным временным интервалом и указанным временным периодом.
        /// </summary>
        /// <typeparam name="TResult">Тип списка.</typeparam>
        /// <param name="timeInterval">Временной интервал.</param>
        /// <param name="startDate">Дата начала периода.</param>
        /// <param name="endData">Дата окончания периода.</param>
        /// <returns>Список.</returns>
        public virtual TResult? GetConvertedListPeriodAndInterval<TResult>(TTimeInterval timeInterval, DateTime startDate, DateTime endData)
            where TResult : ListTimeInterval<TItemTimeable>, new()
        {
            var index_from = GetIndexFromDate(startDate);
            var index_to = GetIndexFromDate(endData);

            return GetConvertedListPeriodAndInterval<TResult>(timeInterval, index_from, index_to);
        }
        #endregion
    }
    /**@}*/
}