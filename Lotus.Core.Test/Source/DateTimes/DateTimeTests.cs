using System;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.DateTimes
{
    /// <summary>
    /// Статический класс для тестирования подсистемы работы с датой и временем модуля базового ядра.
    /// </summary>
    public static class DateTimeTests
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public class TestTimePeriod : ILotusDateTimeable, IComparable<TestTimePeriod>
        {
            public DateTime Date { get; set; }

            public TestTimePeriod()
            {

            }

            public TestTimePeriod(DateTime dateTime)
            {
                Date = dateTime;
            }

            public int CompareTo(TestTimePeriod other)
            {
                return Date.CompareTo(other.Date);
            }

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }

        /// <summary>
        /// Тестирование методов <see cref="ListTimePeriod{TTimePeriod}"/>.
        /// </summary>
        [Test]
        public static void TestDateTime()
        {
            var from = new System.DateTime(2014, 1, 2, 12, 17, 0, DateTimeKind.Utc);

            // Проверка минут
            {
                var count_minut = 35;
                var to_minut = new DateTime(2014, 1, 2, 12, count_minut, 0, DateTimeKind.Utc);
                var list_minutes = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Minutely, 30);
                list_minutes.AssingTimePeriod(from, to_minut);

                ClassicAssert.AreEqual(list_minutes.Count, count_minut + 1 - 17);
                ClassicAssert.AreEqual(list_minutes.CountMinutes, count_minut - 17);
            }

            // Проверка часов
            {
                var count_hour = 22;
                var to_hour = new DateTime(2014, 1, 2, count_hour, 22, 0, DateTimeKind.Utc);
                var list_houres = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Hourly, 30);
                list_houres.AssingTimePeriod(from, to_hour);

                ClassicAssert.AreEqual(list_houres.Count, count_hour + 1 - 12);
                ClassicAssert.AreEqual(list_houres.CountHours, count_hour - 12);
            }

            // Проверка дней
            {
                var count_day = 17;
                var to_day = new DateTime(2014, 1, count_day, 12, 17, 0, DateTimeKind.Utc);
                var list_days = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);
                list_days.AssingTimePeriod(from, to_day);

                ClassicAssert.AreEqual(list_days.Count, count_day + 1 - 2);
                ClassicAssert.AreEqual(list_days.CountDay, count_day - 2);

                // Проверка недель
                var to_week = new DateTime(2016, 1, 6, 12, 17, 0, DateTimeKind.Utc);
                var list_week = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Weekly, 30);
                list_week.AssingTimePeriod(from, to_week);

                ClassicAssert.AreEqual(list_week.Count, list_week.CountWeek + 1);
            }

            //
            // Поиск индекса по дате
            //
            {
                var from_index = new DateTime(2012, 1, 6, 12, 17, 0, DateTimeKind.Utc);
                var to_index = new DateTime(2019, 1, 6, 12, 17, 0, DateTimeKind.Utc);
                var list_index = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);
                list_index.AssingTimePeriod(from_index, to_index);

                var weekends = list_index.CountWeekends();
                ClassicAssert.AreEqual(weekends, list_index.RemoveWeekends());

                var index = list_index.GetIndexFromDate(new DateTime(2015, 8, 2, 0, 0, 0, DateTimeKind.Utc));
                var find_date = list_index[index].Date;
                ClassicAssert.AreEqual(find_date.Year, 2015);
                ClassicAssert.AreEqual(find_date.Month, 7);
                ClassicAssert.AreEqual(find_date.Day, 31);

                ClassicAssert.AreEqual(list_index.GetIndexFromDate(2011, 2, 12), 0);
                ClassicAssert.AreEqual(list_index.GetIndexFromDate(DateTime.Now), list_index.LastIndex);
            }

            //
            // Получение интерполированной даты
            //
            {
                var from_index = new DateTime(2012, 1, 6, 0, 0, 0, DateTimeKind.Utc);
                var to_index = new DateTime(2012, 1, 26, 0, 0, 0, DateTimeKind.Utc);

                ClassicAssert.AreEqual(from_index.GetInterpolatedDate(to_index, 0).Day, 6);
                ClassicAssert.AreEqual(from_index.GetInterpolatedDate(to_index, 0.1f).Day, 8);
                ClassicAssert.AreEqual(from_index.GetInterpolatedDate(to_index, 0.5f).Day, 16);
                ClassicAssert.AreEqual(from_index.GetInterpolatedDate(to_index, 1).Day, 26);

                var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);

                list.Add(new TestTimePeriod(from_index));
                list.Add(new TestTimePeriod(to_index));

                ClassicAssert.AreEqual(list.GetInterpolatedDate(-2).Value.Day, 6);
                ClassicAssert.AreEqual(list.GetInterpolatedDate(0).Value.Day, 6);
                ClassicAssert.AreEqual(list.GetInterpolatedDate(0.1f).Value.Day, 8);
                ClassicAssert.AreEqual(list.GetInterpolatedDate(0.5f).Value.Day, 16);
                ClassicAssert.AreEqual(list.GetInterpolatedDate(1).Value.Day, 26);
            }

            //
            // Обрезать список сначала
            //
            {
                var start_date = new DateTime(2019, 1, 6, 0, 0, 0, DateTimeKind.Utc);
                var end_date = new DateTime(2019, 6, 18, 0, 0, 0, DateTimeKind.Utc);
                var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, start_date, end_date, 30);

                // Удаляем до конечной даты
                var count = list.Count;
                ClassicAssert.AreEqual(list.TrimStart(end_date, true), count);
                ClassicAssert.AreEqual(list.Count, 0);

                // Удаляем до конечной даты (но ее не удаляем)
                list.AssingTimePeriod(start_date, end_date);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimStart(end_date, false), count - 1);
                ClassicAssert.AreEqual(list.DateFirst.Value.Day, 18);

                // Удаляем до 15 числа
                list.AssingTimePeriod(start_date, end_date);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimStart(new DateTime(2019, 1, 15, 0, 0, 0, DateTimeKind.Utc), true), 10);
                ClassicAssert.AreEqual(list.DateFirst.Value.Day, 16);

                // Удаляем до 15 числа (но его не удаляем)
                list.AssingTimePeriod(start_date, end_date);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimStart(new DateTime(2019, 1, 15, 0, 0, 0, DateTimeKind.Utc), false), 9);
                ClassicAssert.AreEqual(list.DateFirst.Value.Day, 15);

                // Удаляем выходные
                start_date = new DateTime(2019, 6, 1, 0, 0, 0, DateTimeKind.Utc);
                end_date = new DateTime(2019, 7, 18, 0, 0, 0, DateTimeKind.Utc);
                list.AssingTimePeriod(start_date, end_date);
                ClassicAssert.AreEqual(list[0].Date, new DateTime(2019, 6, 1, 0, 0, 0, DateTimeKind.Utc)); // Суббота
                ClassicAssert.AreEqual(list[1].Date, new DateTime(2019, 6, 2, 0, 0, 0, DateTimeKind.Utc)); // Воскресенье
                ClassicAssert.AreEqual(list[2].Date, new DateTime(2019, 6, 3, 0, 0, 0, DateTimeKind.Utc)); // Понедельник
                ClassicAssert.AreEqual(list[3].Date, new DateTime(2019, 6, 4, 0, 0, 0, DateTimeKind.Utc)); // Вторник
                ClassicAssert.AreEqual(list[4].Date, new DateTime(2019, 6, 5, 0, 0, 0, DateTimeKind.Utc)); // Среда
                ClassicAssert.AreEqual(list[5].Date, new DateTime(2019, 6, 6, 0, 0, 0, DateTimeKind.Utc)); // Четверг
                ClassicAssert.AreEqual(list[6].Date, new DateTime(2019, 6, 7, 0, 0, 0, DateTimeKind.Utc)); // Пятница
                ClassicAssert.AreEqual(list[7].Date, new DateTime(2019, 6, 8, 0, 0, 0, DateTimeKind.Utc)); // Суббота
                ClassicAssert.AreEqual(list[8].Date, new DateTime(2019, 6, 9, 0, 0, 0, DateTimeKind.Utc)); // Воскресенье
                ClassicAssert.AreEqual(list[9].Date, new DateTime(2019, 6, 10, 0, 0, 0, DateTimeKind.Utc)); // Понедельник
                ClassicAssert.AreEqual(list[10].Date, new DateTime(2019, 6, 11, 0, 0, 0, DateTimeKind.Utc)); // Вторник

                // Удаляем выходные
                count = list.CountWeekends();
                ClassicAssert.AreEqual(list.RemoveWeekends(), count);
                ClassicAssert.AreEqual(list[0].Date, new DateTime(2019, 6, 3, 0, 0, 0, DateTimeKind.Utc)); // Понедельник
                ClassicAssert.AreEqual(list[1].Date, new DateTime(2019, 6, 4, 0, 0, 0, DateTimeKind.Utc)); // Вторник
                ClassicAssert.AreEqual(list[2].Date, new DateTime(2019, 6, 5, 0, 0, 0, DateTimeKind.Utc)); // Среда
                ClassicAssert.AreEqual(list[3].Date, new DateTime(2019, 6, 6, 0, 0, 0, DateTimeKind.Utc)); // Четверг
                ClassicAssert.AreEqual(list[4].Date, new DateTime(2019, 6, 7, 0, 0, 0, DateTimeKind.Utc)); // Пятница
                ClassicAssert.AreEqual(list[5].Date, new DateTime(2019, 6, 10, 0, 0, 0, DateTimeKind.Utc)); // Понедельник
                ClassicAssert.AreEqual(list[6].Date, new DateTime(2019, 6, 11, 0, 0, 0, DateTimeKind.Utc)); // Вторник

                // Удаляем до 9 числа
                list.TrimStart(new DateTime(2019, 6, 9, 0, 0, 0, DateTimeKind.Utc));
                ClassicAssert.AreEqual(list[0].Date.Month, 6);
                ClassicAssert.AreEqual(list[0].Date.Day, 10);

                // Удаляем до 10 числа (удалиться и оно)
                list.TrimStart(new DateTime(2019, 7, 10, 0, 0, 0, DateTimeKind.Utc));
                ClassicAssert.AreEqual(list[0].Date.Month, 7);
                ClassicAssert.AreEqual(list[0].Date.Day, 11);
            }

            //
            // Обрезать список с конца
            //
            {
                var start_date = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var end_date = new DateTime(2020, 2, 29, 0, 0, 0, DateTimeKind.Utc);
                var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, start_date, end_date, 30);

                // Удаляем все
                var count = list.Count;
                ClassicAssert.AreEqual(list.TrimEnd(start_date, true), count);

                // Удаляем все за исключением первого элемента
                list.AssingTimePeriod(start_date, end_date);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimEnd(start_date, false), count - 1);

                list.AssingTimePeriod(start_date, end_date);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimEnd(new DateTime(2020, 2, 25, 0, 0, 0, DateTimeKind.Utc)), 5);
                ClassicAssert.AreEqual(list.DateLast.Value.Day, 24);

                list.AssingTimePeriod(start_date, end_date);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimEnd(new DateTime(2020, 2, 20, 0, 0, 0, DateTimeKind.Utc), false), 9);
                ClassicAssert.AreEqual(list.DateLast.Value.Day, 20);
            }
        }
    }
}