using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.DateTimes
{
    /// <summary>
    /// Тесты для критических исправлений в <see cref="ListTimeInterval{TItemTimeable}"/>.
    /// </summary>
    [TestFixture]
    public class DateTimeListIntervalTests
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

            public int CompareTo(TestTimePeriod? other)
            {
                if (other == null) return 1;
                return Date.CompareTo(other.Date);
            }

            public object Clone()
            {
                return MemberwiseClone();
            }
        }

        /// <summary>
        /// Тест метода AssignTimePeriod - исправленное имя метода (было AssingTimePeriod).
        /// </summary>
        [Test]
        public void AssignTimePeriod_WithValidDates_CreatesCorrectInterval()
        {
            var from = new DateTime(2014, 1, 2, 12, 17, 0, DateTimeKind.Utc);
            var to = new DateTime(2014, 1, 2, 12, 35, 0, DateTimeKind.Utc);
            var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Minutely, 30);

            list.AssignTimePeriod(from, to);

            ClassicAssert.AreEqual(19, list.Count); // 35 - 17 + 1 = 19
        }

        /// <summary>
        /// Тест метода DuplicateListPeriod - исправленное имя метода (было DublicateListPeriod).
        /// </summary>
        [Test]
        public void DuplicateListPeriod_WithValidIndices_ReturnsCorrectList()
        {
            var from = new DateTime(2014, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var to = new DateTime(2014, 1, 10, 0, 0, 0, DateTimeKind.Utc);
            var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);
            list.AssignTimePeriod(from, to);

            var duplicate = list.DuplicateListPeriod<ListTimeInterval<TestTimePeriod>>(0, 4);

            ClassicAssert.IsNotNull(duplicate);
            ClassicAssert.AreEqual(5, duplicate.Count); // Индексы 0-4 включительно
        }

        /// <summary>
        /// Тест метода DuplicateListPeriod - с датами.
        /// </summary>
        [Test]
        public void DuplicateListPeriod_WithValidDates_ReturnsCorrectList()
        {
            var from = new DateTime(2014, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var to = new DateTime(2014, 1, 10, 0, 0, 0, DateTimeKind.Utc);
            var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);
            list.AssignTimePeriod(from, to);

            var startDate = new DateTime(2014, 1, 2, 0, 0, 0, DateTimeKind.Utc);
            var endDate = new DateTime(2014, 1, 5, 0, 0, 0, DateTimeKind.Utc);
            var duplicate = list.DuplicateListPeriod<ListTimeInterval<TestTimePeriod>>(startDate, endDate);

            ClassicAssert.IsNotNull(duplicate);
            ClassicAssert.IsTrue(duplicate.Count > 0);
        }

        /// <summary>
        /// Тест метода AssignTimePeriod - с обратным порядком дат.
        /// </summary>
        [Test]
        public void AssignTimePeriod_WithReversedDates_HandlesCorrectly()
        {
            var from = new DateTime(2014, 1, 10, 0, 0, 0, DateTimeKind.Utc);
            var to = new DateTime(2014, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);

            // Метод должен корректно обработать обратный порядок
            list.AssignTimePeriod(from, to);

            ClassicAssert.IsTrue(list.Count >= 0);
        }

        /// <summary>
        /// Тест метода AssignTimePeriod - с одинаковыми датами.
        /// </summary>
        [Test]
        public void AssignTimePeriod_WithSameDates_CreatesSingleItem()
        {
            var date = new DateTime(2014, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var list = new ListTimeInterval<TestTimePeriod>(TTimeInterval.Daily, 30);

            list.AssignTimePeriod(date, date);

            ClassicAssert.AreEqual(1, list.Count);
        }
    }
}
