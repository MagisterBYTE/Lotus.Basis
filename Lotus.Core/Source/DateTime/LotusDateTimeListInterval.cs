//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема работы с датой и временем
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDateTimeListInterval.cs
*		Определение коллекции которая содержит объекты поддерживающие концепцию временного интервала.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreDateTime
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Коллекция содержащая объекты реализующие интерфейс <see cref="ILotusDateTimeable"/> с определённым временным интервалом
		/// </summary>
		/// <remarks>
		/// Сама коллекция образует определённый временной период, элементы которой расположены  в соответствии с 
		/// временным интервалом
		/// </remarks>
		/// <typeparam name="TItemTimeable">Тип элемента списка</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class ListTimeInterval<TItemTimeable> : ListArray<TItemTimeable> where TItemTimeable : ILotusDateTimeable
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal TTimeInterval mTimeInterval;

			// Служебные данные
			protected internal TItemTimeable mDummyItem = Activator.CreateInstance<TItemTimeable>();
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Временной интервал
			/// </summary>
			/// <remarks>
			/// Временной интервал обозначает что свойство <see cref="ILotusDateTimeable.Date"/> элементов списка 
			/// отличаются ни кратную величин интервала
			/// </remarks>
			public TTimeInterval TimeInterval
			{
				get { return (mTimeInterval); }
				set { mTimeInterval = value; }
			}

			/// <summary>
			/// Количество минут временного периода
			/// </summary>
			public Int32 CountMinutes
			{
				get { return ((DateLast - DateFirst).Minutes); }
			}

			/// <summary>
			/// Количество часов временного периода
			/// </summary>
			public Int32 CountHours
			{
				get { return ((DateLast - DateFirst).Hours); }
			}

			/// <summary>
			/// Количество дней временного периода
			/// </summary>
			public Int32 CountDay
			{
				get { return ((DateLast - DateFirst).Days); }
			}

			/// <summary>
			/// Количество недель временного периода
			/// </summary>
			public Int32 CountWeek
			{
				get
				{
					Double count_days = (DateLast - DateFirst).Days;
					return ((Int32)Math.Ceiling(count_days / 7.0));
				}
			}

			//
			// ПРОИЗВОДНЫЕ ДАННЫЕ
			//
			/// <summary>
			/// Первый объект даты-времени
			/// </summary>
			public DateTime DateFirst
			{
				get { return (ItemFirst.Date); }
			}

			/// <summary>
			/// Предпоследний объект даты-времени
			/// </summary>
			public DateTime DatePenultimate
			{
				get { return (ItemPenultimate.Date); }
			}

			/// <summary>
			/// Последний объект даты-времени
			/// </summary>
			public DateTime DateLast
			{
				get { return (ItemLast.Date); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public ListTimeInterval()
				: base(120)
			{
				mTimeInterval = TTimeInterval.Daily;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="capacity">Начальная максимальная емкость списка</param>
			//---------------------------------------------------------------------------------------------------------
			public ListTimeInterval(Int32 capacity) 
				: base(capacity)
			{
				mTimeInterval = TTimeInterval.Daily;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="time_interval">Временной интервал</param>
			/// <param name="capacity">Начальная максимальная емкость списка</param>
			//---------------------------------------------------------------------------------------------------------
			public ListTimeInterval(TTimeInterval time_interval, Int32 capacity = 120)
				: base(capacity)
			{
				mTimeInterval = time_interval;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="time_interval">Временной интервал</param>
			/// <param name="start_data">Дата начала периода</param>
			/// <param name="end_data">Дата окончания периода</param>
			/// <param name="capacity">Начальная максимальная емкость списка</param>
			//---------------------------------------------------------------------------------------------------------
			public ListTimeInterval(TTimeInterval time_interval, DateTime start_data, DateTime end_data, Int32 capacity = 120)
				: base(capacity)
			{
				mTimeInterval = time_interval;
				AssingTimePeriod(start_data, end_data);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Присвоение указанного временного периода
			/// </summary>
			/// <param name="start_data">Дата начала периода</param>
			/// <param name="end_data">Дата окончания периода</param>
			//---------------------------------------------------------------------------------------------------------
			public void AssingTimePeriod(DateTime start_data, DateTime end_data)
			{
				Clear();

				DateTime current = start_data;
				TItemTimeable first_item = Activator.CreateInstance<TItemTimeable>();
				first_item.Date = start_data;
				Add(first_item);
				switch (mTimeInterval)
				{
					case TTimeInterval.Minutely:
						{
							while (current < end_data)
							{
								current += TimeSpan.FromMinutes(1);

								if (current <= end_data)
								{
									TItemTimeable current_item = Activator.CreateInstance<TItemTimeable>();
									current_item.Date = current;
									Add(current_item);
								}
							}
						}
						break;
					case TTimeInterval.Hourly:
						{
							while (current < end_data)
							{
								current += TimeSpan.FromHours(1);

								if (current <= end_data)
								{
									TItemTimeable current_item = Activator.CreateInstance<TItemTimeable>();
									current_item.Date = current;
									Add(current_item);
								}
							}
						}
						break;
					case TTimeInterval.Daily:
						{
							while (current < end_data)
							{
								current += TimeSpan.FromDays(1);

								if (current <= end_data)
								{
									TItemTimeable current_item = Activator.CreateInstance<TItemTimeable>();
									current_item.Date = current;
									Add(current_item);
								}
							}
						}
						break;
					case TTimeInterval.Weekly:
						{
							while (current < end_data)
							{
								current += TimeSpan.FromDays(1);

								if (current <= end_data && current.DayOfWeek == DayOfWeek.Monday)
								{
									TItemTimeable current_item = Activator.CreateInstance<TItemTimeable>();
									current_item.Date = current;
									Add(current_item);
								}
							}
						}
						break;
					case TTimeInterval.Monthly:
						{
							while (current < end_data)
							{
								current += TimeSpan.FromDays(1);

								if (current <= end_data && current.Day == 1)
								{
									TItemTimeable current_item = Activator.CreateInstance<TItemTimeable>();
									current_item.Date = current;
									Add(current_item);
								}
							}
						}
						break;
					case TTimeInterval.Yearly:
						{
							while (current < end_data)
							{
								current += TimeSpan.FromDays(1);

								if (current <= end_data && current.DayOfYear == 1)
								{
									TItemTimeable current_item = Activator.CreateInstance<TItemTimeable>();
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение индекса элемента по указанным параметрам даты
			/// </summary>
			/// <param name="year">Год</param>
			/// <param name="month">Месяц</param>
			/// <param name="day">День</param>
			/// <returns>Индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetIndexFromDate(Int32 year, Int32 month, Int32 day)
			{
				return (GetIndexFromDate(new DateTime(year, month, day)));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение индекса элемента по дате
			/// </summary>
			/// <param name="date">Дата</param>
			/// <returns>Индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetIndexFromDate(DateTime date)
			{
				if (date <= DateFirst)
				{
					return (0);
				}
				if (date >= DateLast)
				{
					return (LastIndex);
				}

				for (var i = 1; i < mCount; i++)
				{
					if (mArrayOfItems[i].Date == date)
					{
						return (i);
					}
					else
					{
						if (mArrayOfItems[i].Date > date)
						{
							return (i - 1);
						}
					}
				}

				return (LastIndex);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение интерполированного значение даты указанному вещественному индексу
			/// </summary>
			/// <param name="index">Индекс</param>
			/// <returns>Дата</returns>
			//---------------------------------------------------------------------------------------------------------
			public DateTime GetInterpolatedDate(Single index)
			{
				if (index > 0)
				{
					var start_index = (Int32)Math.Floor(index);
					var end_index = (Int32)Math.Ceiling(index);
					var delta = index - start_index;

					return (mArrayOfItems[start_index].Date.GetInterpolatedDate(mArrayOfItems[end_index].Date, delta));
				}
				else
				{
					return (DateFirst);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подсчет количество выходных дней
			/// </summary>
			/// <returns>Количество выходных дней</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CountWeekends()
			{
				var count = 0;

				for (var i = 0; i < mCount; i++)
				{
					TItemTimeable item = mArrayOfItems[i];
					if (item.Date.DayOfWeek == DayOfWeek.Sunday || item.Date.DayOfWeek == DayOfWeek.Saturday)
					{
						count++;
					}
				}

				return (count);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление выходных дней
			/// </summary>
			/// <returns>Количество удаленных дней</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 RemoveWeekends()
			{
				var count = RemoveAll((TItemTimeable item) =>
				{
					if (item.Date.DayOfWeek == DayOfWeek.Sunday || item.Date.DayOfWeek == DayOfWeek.Saturday)
					{
						return (true);
					}
					else
					{
						return (false);
					}
				});

				return (count);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список сначала до указанной даты
			/// </summary>
			/// <param name="date">Дата</param>
			/// <param name="included">Включать указанную дату в удаление</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 TrimStart(DateTime date, Boolean included = true)
			{
				mDummyItem.Date = date;
				return (TrimClosestStart(mDummyItem, included));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список с конца до указанной даты
			/// </summary>
			/// <param name="date">Дата</param>
			/// <param name="included">Включать указанную дату в удаление</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 TrimEnd(DateTime date, Boolean included = true)
			{
				mDummyItem.Date = date;
				return (TrimClosestEnd(mDummyItem, included));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка с указанным периодом
			/// </summary>
			/// <typeparam name="TResult">Тип списка</typeparam>
			/// <param name="start_index">Начальный индекс периода</param>
			/// <param name="end_index">Конечный индекс периода</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TResult GetListPeriod<TResult>(Int32 start_index, Int32 end_index)
				where TResult : ListTimeInterval<TItemTimeable>, new()
			{
				var list = new TResult();
				list.TimeInterval = mTimeInterval;

				// Это не количество, а индекс поэтому и равно
				for (var i = start_index; i <= end_index; i++)
				{
					list.Add(mArrayOfItems[i]);
				}

				list.SetIndexElement();
				return (list);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка с указанным периодом
			/// </summary>
			/// <typeparam name="TResult">Тип списка</typeparam>
			/// <param name="start_date">Дата начала периода</param>
			/// <param name="end_data">Дата окончания периода</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TResult GetListPeriod<TResult>(DateTime start_date, DateTime end_data)
				where TResult : ListTimeInterval<TItemTimeable>, new()
			{
				var start_index = GetIndexFromDate(start_date);
				var end_index = GetIndexFromDate(end_data);

				return (GetListPeriod<TResult>(start_index, end_index));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Дублирование списка с указанным периодом
			/// </summary>
			/// <typeparam name="TResult">Тип списка</typeparam>
			/// <param name="start_index">Начальный индекс периода</param>
			/// <param name="end_index">Конечный индекс периода</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TResult DublicateListPeriod<TResult>(Int32 start_index, Int32 end_index)
				where TResult : ListTimeInterval<TItemTimeable>, new()
			{
				var list = new TResult();
				list.TimeInterval = mTimeInterval;

				//Это не количество, а индекс поэтому и равно
				for (var i = start_index; i <= end_index; i++)
				{
					list.Add((TItemTimeable)mArrayOfItems[i].Clone());
				}

				list.SetIndexElement();
				return (list);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Дублирование списка с указанным периодом
			/// </summary>
			/// <typeparam name="TResult">Тип списка</typeparam>
			/// <param name="start_date">Дата начала периода</param>
			/// <param name="end_data">Дата окончания периода</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TResult DublicateListPeriod<TResult>(DateTime start_date, DateTime end_data)
				where TResult : ListTimeInterval<TItemTimeable>, new()
			{
				var start_index = GetIndexFromDate(start_date);
				var end_index = GetIndexFromDate(end_data);

				return (DublicateListPeriod<TResult>(start_index, end_index));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка с указанным временным интервалом
			/// </summary>
			/// <typeparam name="TResult">Тип списка</typeparam>
			/// <param name="time_interval">Временной интервал</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TResult GetConvertedListInterval<TResult>(TTimeInterval time_interval)
				where TResult : ListTimeInterval<TItemTimeable>, new()
			{
				return (GetConvertedListPeriodAndInterval<TResult>(time_interval, 0, LastIndex));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка с указанным временным интервалом и указанным временным периодом
			/// </summary>
			/// <typeparam name="TResult">Тип списка</typeparam>
			/// <param name="time_interval">Временной интервал</param>
			/// <param name="start_index">Начальный индекс периода</param>
			/// <param name="end_index">Конечный индекс периода</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TResult GetConvertedListPeriodAndInterval<TResult>(TTimeInterval time_interval, Int32 start_index, Int32 end_index)
				where TResult : ListTimeInterval<TItemTimeable>, new()
			{
				TResult list = null;
				switch (mTimeInterval)
				{
					case TTimeInterval.Minutely:
						break;
					case TTimeInterval.Hourly:
						{
							switch (time_interval)
							{
								case TTimeInterval.Minutely:
									break;
								case TTimeInterval.Hourly:
									{
										list = GetListPeriod<TResult>(start_index, end_index);
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
							switch (time_interval)
							{
								case TTimeInterval.Minutely:
									break;
								case TTimeInterval.Hourly:
									break;
								case TTimeInterval.Daily:
									{
										list = GetListPeriod<TResult>(start_index, end_index);
									}
									break;
								case TTimeInterval.Weekly:
									{
										list = new TResult();
										list.TimeInterval = TTimeInterval.Weekly;

										for (var i = 0; i < mCount; i++)
										{
											if (mArrayOfItems[i].Date.DayOfWeek == DayOfWeek.Monday)
											{
												list.Add(mArrayOfItems[i]);
											}
										}
									}
									break;
								case TTimeInterval.Monthly:
									{
										list = new TResult();
										list.TimeInterval = TTimeInterval.Monthly;

										for (var i = 0; i < mCount; i++)
										{
											if (mArrayOfItems[i].Date.Day == 1)
											{
												list.Add(mArrayOfItems[i]);
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
							switch (time_interval)
							{
								case TTimeInterval.Minutely:
									break;
								case TTimeInterval.Hourly:
									break;
								case TTimeInterval.Daily:
									break;
								case TTimeInterval.Weekly:
									{
										list = GetListPeriod<TResult>(start_index, end_index);
									}
									break;
								case TTimeInterval.Monthly:
									{
										list = new TResult();
										list.TimeInterval = TTimeInterval.Monthly;

										for (var i = 0; i < mCount; i++)
										{
											if (mArrayOfItems[i].Date.Day == 1)
											{
												list.Add(mArrayOfItems[i]);
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

				list.SetIndexElement();
				return (list);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка с указанным временным интервалом и указанным временным периодом
			/// </summary>
			/// <typeparam name="TResult">Тип списка</typeparam>
			/// <param name="time_interval">Временной интервал</param>
			/// <param name="start_date">Дата начала периода</param>
			/// <param name="end_data">Дата окончания периода</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TResult GetConvertedListPeriodAndInterval<TResult>(TTimeInterval time_interval, DateTime start_date, DateTime end_data)
				where TResult : ListTimeInterval<TItemTimeable>, new()
			{
				var index_from = GetIndexFromDate(start_date);
				var index_to = GetIndexFromDate(end_data);

				return (GetConvertedListPeriodAndInterval<TResult>(time_interval, index_from, index_to));
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================