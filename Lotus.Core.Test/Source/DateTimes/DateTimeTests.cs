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
                var countMinut = 35;
                var toMinut = new DateTime(2014, 1, 2, 12, countMinut, 0, DateTimeKind.Utc);
                var listMinutes = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Minutely, 30);
                listMinutes.AssignTimePeriod(from, toMinut);

                ClassicAssert.AreEqual(listMinutes.Count, countMinut + 1 - 17);
                ClassicAssert.AreEqual(listMinutes.CountMinutes, countMinut - 17);
            }

            // Проверка часов
            {
                var countHour = 22;
                var toHour = new DateTime(2014, 1, 2, countHour, 22, 0, DateTimeKind.Utc);
                var listHoures = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Hourly, 30);
                listHoures.AssignTimePeriod(from, toHour);

                ClassicAssert.AreEqual(listHoures.Count, countHour + 1 - 12);
                ClassicAssert.AreEqual(listHoures.CountHours, countHour - 12);
            }

            // Проверка дней
            {
                var countDay = 17;
                var toDay = new DateTime(2014, 1, countDay, 12, 17, 0, DateTimeKind.Utc);
                var listDays = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);
                listDays.AssignTimePeriod(from, toDay);

                ClassicAssert.AreEqual(listDays.Count, countDay + 1 - 2);
                ClassicAssert.AreEqual(listDays.CountDay, countDay - 2);

                // Проверка недель
                var toWeek = new DateTime(2016, 1, 6, 12, 17, 0, DateTimeKind.Utc);
                var listWeek = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Weekly, 30);
                listWeek.AssignTimePeriod(from, toWeek);

                ClassicAssert.AreEqual(listWeek.Count, listWeek.CountWeek + 1);
            }

            //
            // Поиск индекса по дате
            //
            {
                var fromIndex = new DateTime(2012, 1, 6, 12, 17, 0, DateTimeKind.Utc);
                var toIndex = new DateTime(2019, 1, 6, 12, 17, 0, DateTimeKind.Utc);
                var listIndex = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);
                listIndex.AssignTimePeriod(fromIndex, toIndex);

                var weekends = listIndex.CountWeekends();
                ClassicAssert.AreEqual(weekends, listIndex.RemoveWeekends());

                var index = listIndex.GetIndexFromDate(new DateTime(2015, 8, 2, 0, 0, 0, DateTimeKind.Utc));
                var findDate = listIndex[index].Date;
                ClassicAssert.AreEqual(findDate.Year, 2015);
                ClassicAssert.AreEqual(findDate.Month, 7);
                ClassicAssert.AreEqual(findDate.Day, 31);

                ClassicAssert.AreEqual(listIndex.GetIndexFromDate(2011, 2, 12), 0);
                ClassicAssert.AreEqual(listIndex.GetIndexFromDate(DateTime.Now), listIndex.LastIndex);
            }

            //
            // Получение интерполированной даты
            //
            {
                var fromIndex = new DateTime(2012, 1, 6, 0, 0, 0, DateTimeKind.Utc);
                var toIndex = new DateTime(2012, 1, 26, 0, 0, 0, DateTimeKind.Utc);

                ClassicAssert.AreEqual(fromIndex.GetInterpolatedDate(toIndex, 0).Day, 6);
                ClassicAssert.AreEqual(fromIndex.GetInterpolatedDate(toIndex, 0.1f).Day, 8);
                ClassicAssert.AreEqual(fromIndex.GetInterpolatedDate(toIndex, 0.5f).Day, 16);
                ClassicAssert.AreEqual(fromIndex.GetInterpolatedDate(toIndex, 1).Day, 26);

                var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);

                list.Add(new TestTimePeriod(fromIndex));
                list.Add(new TestTimePeriod(toIndex));

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
                var startDate = new DateTime(2019, 1, 6, 0, 0, 0, DateTimeKind.Utc);
                var endDate = new DateTime(2019, 6, 18, 0, 0, 0, DateTimeKind.Utc);
                var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, startDate, endDate, 30);

                // Удаляем до конечной даты
                var count = list.Count;
                ClassicAssert.AreEqual(list.TrimStart(endDate, true), count);
                ClassicAssert.AreEqual(list.Count, 0);

                // Удаляем до конечной даты (но ее не удаляем)
                list.AssignTimePeriod(startDate, endDate);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimStart(endDate, false), count - 1);
                ClassicAssert.AreEqual(list.DateFirst.Value.Day, 18);

                // Удаляем до 15 числа
                list.AssignTimePeriod(startDate, endDate);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimStart(new DateTime(2019, 1, 15, 0, 0, 0, DateTimeKind.Utc), true), 10);
                ClassicAssert.AreEqual(list.DateFirst.Value.Day, 16);

                // Удаляем до 15 числа (но его не удаляем)
                list.AssignTimePeriod(startDate, endDate);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimStart(new DateTime(2019, 1, 15, 0, 0, 0, DateTimeKind.Utc), false), 9);
                ClassicAssert.AreEqual(list.DateFirst.Value.Day, 15);

                // Удаляем выходные
                startDate = new DateTime(2019, 6, 1, 0, 0, 0, DateTimeKind.Utc);
                endDate = new DateTime(2019, 7, 18, 0, 0, 0, DateTimeKind.Utc);
                list.AssignTimePeriod(startDate, endDate);
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
                var startDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var endDate = new DateTime(2020, 2, 29, 0, 0, 0, DateTimeKind.Utc);
                var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, startDate, endDate, 30);

                // Удаляем все
                var count = list.Count;
                ClassicAssert.AreEqual(list.TrimEnd(startDate, true), count);

                // Удаляем все за исключением первого элемента
                list.AssignTimePeriod(startDate, endDate);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimEnd(startDate, false), count - 1);

                list.AssignTimePeriod(startDate, endDate);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimEnd(new DateTime(2020, 2, 25, 0, 0, 0, DateTimeKind.Utc)), 5);
                ClassicAssert.AreEqual(list.DateLast.Value.Day, 24);

                list.AssignTimePeriod(startDate, endDate);
                count = list.Count;
                ClassicAssert.AreEqual(list.TrimEnd(new DateTime(2020, 2, 20, 0, 0, 0, DateTimeKind.Utc), false), 9);
                ClassicAssert.AreEqual(list.DateLast.Value.Day, 20);
            }
        }
    }
}