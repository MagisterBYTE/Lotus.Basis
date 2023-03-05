//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема работы с датой и временем
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCoreDateTimeTesting.cs
*		Тестирование подсистемы работы с датой и временем модуля базового ядра.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для тестирования подсистемы работы с датой и временем модуля базового ядра
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XCoreDateTimeTesting
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Служебный класс для тестирования
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public class TestTimePeriod : ILotusDateTimeable, IComparable<TestTimePeriod>
			{
				public DateTime Date { get; set; }

				public TestTimePeriod()
				{

				}

				public TestTimePeriod(DateTime date_time)
				{
					Date = date_time;
				}

				public int CompareTo(TestTimePeriod other)
				{
					return (Date.CompareTo(other.Date));
				}

				public object Clone()
				{
					return (this.MemberwiseClone());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="ListTimePeriod{TTimePeriod}"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestDateTime()
			{
				DateTime from = new DateTime(2014, 1, 2, 12, 17, 0);

				// Проверка минут
				{
					Int32 count_minut = 35;
					DateTime to_minut = new DateTime(2014, 1, 2, 12, count_minut, 0);
					ListTimeInterval<TestTimePeriod> list_minutes = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Minutely, 30);
					list_minutes.AssingTimePeriod(from, to_minut);

					Assert.AreEqual(list_minutes.Count, count_minut + 1 - 17);
					Assert.AreEqual(list_minutes.CountMinutes, count_minut - 17);
				}

				// Проверка часов
				{
					Int32 count_hour = 22;
					DateTime to_hour = new DateTime(2014, 1, 2, count_hour, 22, 0);
					ListTimeInterval<TestTimePeriod> list_houres = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Hourly, 30);
					list_houres.AssingTimePeriod(from, to_hour);

					Assert.AreEqual(list_houres.Count, count_hour + 1 - 12);
					Assert.AreEqual(list_houres.CountHours, count_hour - 12);
				}

				// Проверка дней
				{
					Int32 count_day = 17;
					DateTime to_day = new DateTime(2014, 1, count_day, 12, 17, 0);
					ListTimeInterval<TestTimePeriod> list_days = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);
					list_days.AssingTimePeriod(from, to_day);

					Assert.AreEqual(list_days.Count, count_day + 1 - 2);
					Assert.AreEqual(list_days.CountDay, count_day - 2);

					// Проверка недель
					DateTime to_week = new DateTime(2016, 1, 6, 12, 17, 0);
					ListTimeInterval<TestTimePeriod> list_week = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Weekly, 30);
					list_week.AssingTimePeriod(from, to_week);

					Assert.AreEqual(list_week.Count, list_week.CountWeek + 1);
				}

				//
				// Поиск индекса по дате
				//
				{
					DateTime from_index = new DateTime(2012, 1, 6, 12, 17, 0);
					DateTime to_index = new DateTime(2019, 1, 6, 12, 17, 0);
					ListTimeInterval<TestTimePeriod> list_index = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);
					list_index.AssingTimePeriod(from_index, to_index);

					Int32 weekends = list_index.CountWeekends();
					Assert.AreEqual(weekends, list_index.RemoveWeekends());

					Int32 index = list_index.GetIndexFromDate(new DateTime(2015, 8, 2));
					DateTime find_date = list_index[index].Date;
					Assert.AreEqual(find_date.Year, 2015);
					Assert.AreEqual(find_date.Month, 7);
					Assert.AreEqual(find_date.Day, 31);

					Assert.AreEqual(list_index.GetIndexFromDate(2011, 2, 12), 0);
					Assert.AreEqual(list_index.GetIndexFromDate(DateTime.Now), list_index.LastIndex);
				}

				//
				// Получение интерполированной даты
				//
				{
					DateTime from_index = new DateTime(2012, 1, 6);
					DateTime to_index = new DateTime(2012, 1, 26);

					Assert.AreEqual(from_index.GetInterpolatedDate(to_index, 0).Day, 6);
					Assert.AreEqual(from_index.GetInterpolatedDate(to_index, 0.1f).Day, 8);
					Assert.AreEqual(from_index.GetInterpolatedDate(to_index, 0.5f).Day, 16);
					Assert.AreEqual(from_index.GetInterpolatedDate(to_index, 1).Day, 26);

					ListTimeInterval<TestTimePeriod> list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);

					list.Add(new TestTimePeriod(from_index));
					list.Add(new TestTimePeriod(to_index));

					Assert.AreEqual(list.GetInterpolatedDate(-2).Day, 6);
					Assert.AreEqual(list.GetInterpolatedDate(0).Day, 6);
					Assert.AreEqual(list.GetInterpolatedDate(0.1f).Day, 8);
					Assert.AreEqual(list.GetInterpolatedDate(0.5f).Day, 16);
					Assert.AreEqual(list.GetInterpolatedDate(1).Day, 26);
				}

				//
				// Обрезать список сначала
				//
				{
					DateTime start_date = new DateTime(2019, 1, 6);
					DateTime end_date = new DateTime(2019, 6, 18);
					ListTimeInterval<TestTimePeriod> list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, start_date, end_date, 30);

					// Удаляем до конечной даты
					Int32 count = list.Count;
					Assert.AreEqual(list.TrimStart(end_date, true), count);
					Assert.AreEqual(list.Count, 0);

					// Удаляем до конечной даты (но ее не удаляем)
					list.AssingTimePeriod(start_date, end_date);
					count = list.Count;
					Assert.AreEqual(list.TrimStart(end_date, false), count - 1);
					Assert.AreEqual(list.DateFirst.Day, 18);

					// Удаляем до 15 числа
					list.AssingTimePeriod(start_date, end_date);
					count = list.Count;
					Assert.AreEqual(list.TrimStart(new DateTime(2019, 1, 15), true), 10);
					Assert.AreEqual(list.DateFirst.Day, 16);

					// Удаляем до 15 числа (но его не удаляем)
					list.AssingTimePeriod(start_date, end_date);
					count = list.Count;
					Assert.AreEqual(list.TrimStart(new DateTime(2019, 1, 15), false), 9);
					Assert.AreEqual(list.DateFirst.Day, 15);

					// Удаляем выходные
					start_date = new DateTime(2019, 6, 1);
					end_date = new DateTime(2019, 7, 18);
					list.AssingTimePeriod(start_date, end_date);
					Assert.AreEqual(list[0].Date, new DateTime(2019, 6, 1)); // Суббота
					Assert.AreEqual(list[1].Date, new DateTime(2019, 6, 2)); // Воскресенье
					Assert.AreEqual(list[2].Date, new DateTime(2019, 6, 3)); // Понедельник
					Assert.AreEqual(list[3].Date, new DateTime(2019, 6, 4)); // Вторник
					Assert.AreEqual(list[4].Date, new DateTime(2019, 6, 5)); // Среда
					Assert.AreEqual(list[5].Date, new DateTime(2019, 6, 6)); // Четверг
					Assert.AreEqual(list[6].Date, new DateTime(2019, 6, 7)); // Пятница
					Assert.AreEqual(list[7].Date, new DateTime(2019, 6, 8)); // Суббота
					Assert.AreEqual(list[8].Date, new DateTime(2019, 6, 9)); // Воскресенье
					Assert.AreEqual(list[9].Date, new DateTime(2019, 6, 10)); // Понедельник
					Assert.AreEqual(list[10].Date, new DateTime(2019, 6, 11)); // Вторник

					// Удаляем выходные
					count = list.CountWeekends();
					Assert.AreEqual(list.RemoveWeekends(), count);
					Assert.AreEqual(list[0].Date, new DateTime(2019, 6, 3)); // Понедельник
					Assert.AreEqual(list[1].Date, new DateTime(2019, 6, 4)); // Вторник
					Assert.AreEqual(list[2].Date, new DateTime(2019, 6, 5)); // Среда
					Assert.AreEqual(list[3].Date, new DateTime(2019, 6, 6)); // Четверг
					Assert.AreEqual(list[4].Date, new DateTime(2019, 6, 7)); // Пятница
					Assert.AreEqual(list[5].Date, new DateTime(2019, 6, 10)); // Понедельник
					Assert.AreEqual(list[6].Date, new DateTime(2019, 6, 11)); // Вторник

					// Удаляем до 9 числа
					list.TrimStart(new DateTime(2019, 6, 9));
					Assert.AreEqual(list[0].Date.Month, 6);
					Assert.AreEqual(list[0].Date.Day, 10);

					// Удаляем до 10 числа (удалиться и оно)
					list.TrimStart(new DateTime(2019, 7, 10));
					Assert.AreEqual(list[0].Date.Month, 7);
					Assert.AreEqual(list[0].Date.Day, 11);
				}

				//
				// Обрезать список с конца
				//
				{
					DateTime start_date = new DateTime(2020, 1, 1);
					DateTime end_date = new DateTime(2020, 2, 29);
					ListTimeInterval<TestTimePeriod> list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, start_date, end_date, 30);

					// Удаляем все
					Int32 count = list.Count;
					Assert.AreEqual(list.TrimEnd(start_date, true), count);

					// Удаляем все за исключением первого элемента
					list.AssingTimePeriod(start_date, end_date);
					count = list.Count;
					Assert.AreEqual(list.TrimEnd(start_date, false), count - 1);
					
					list.AssingTimePeriod(start_date, end_date);
					count = list.Count;
					Assert.AreEqual(list.TrimEnd(new DateTime(2020, 2, 25)), 5);
					Assert.AreEqual(list.DateLast.Day, 24);

					list.AssingTimePeriod(start_date, end_date);
					count = list.Count;
					Assert.AreEqual(list.TrimEnd(new DateTime(2020, 2, 20), false), 9);
					Assert.AreEqual(list.DateLast.Day, 20);
				}
			}
		}
	}
}
//=====================================================================================================================