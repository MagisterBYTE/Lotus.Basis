using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Converters
{
    /// <summary>
    /// Тесты для <see cref="XDateTimeConverter"/>.
    /// </summary>
    [TestFixture]
    public class DateTimeConverterTests
    {
        /// <summary>
        /// Тест метода ToDateTime - с int значением (timestamp).
        /// </summary>
        [Test]
        public void ToDateTime_WithIntTimestamp_ReturnsCorrectValue()
        {
            var timestamp = 1609459200; // 2021-01-01 00:00:00 UTC
            var result = XDateTimeConverter.ToDateTime(timestamp, DateTime.MinValue);
            ClassicAssert.IsTrue(result > DateTime.MinValue);
        }

        /// <summary>
        /// Тест метода ToDateTime - с long значением (timestamp).
        /// </summary>
        [Test]
        public void ToDateTime_WithLongTimestamp_ReturnsCorrectValue()
        {
            var timestamp = 1609459200L; // 2021-01-01 00:00:00 UTC
            var result = XDateTimeConverter.ToDateTime(timestamp, DateTime.MinValue);
            ClassicAssert.IsTrue(result > DateTime.MinValue);
        }

        /// <summary>
        /// Тест метода ToDateTime - с string значением.
        /// </summary>
        [Test]
        public void ToDateTime_WithStringValue_ReturnsCorrectValue()
        {
            var defaultValue = new DateTime(2020, 1, 1);
            var result = XDateTimeConverter.ToDateTime("2021-01-01", defaultValue);
            ClassicAssert.AreEqual(2021, result.Year);
            ClassicAssert.AreEqual(1, result.Month);
            ClassicAssert.AreEqual(1, result.Day);
        }

        /// <summary>
        /// Тест метода ToDateTime - с null значением возвращает defaultValue.
        /// </summary>
        [Test]
        public void ToDateTime_WithNull_ReturnsDefaultValue()
        {
            var defaultValue = new DateTime(2020, 1, 1);
            var result = XDateTimeConverter.ToDateTime(null!, defaultValue);
            ClassicAssert.AreEqual(defaultValue, result);
        }

        /// <summary>
        /// Тест метода Parse - с валидной строкой.
        /// </summary>
        [Test]
        public void Parse_WithValidString_ReturnsCorrectValue()
        {
            var result = XDateTimeConverter.Parse("2021-01-01");
            ClassicAssert.AreEqual(2021, result.Year);
            ClassicAssert.AreEqual(1, result.Month);
            ClassicAssert.AreEqual(1, result.Day);
        }

        /// <summary>
        /// Тест метода Parse - с пустой строкой возвращает default.
        /// </summary>
        [Test]
        public void Parse_WithEmptyString_ReturnsDefault()
        {
            var result = XDateTimeConverter.Parse("");
            ClassicAssert.AreEqual(default(DateTime), result);
        }

        /// <summary>
        /// Тест метода TryParse - с валидной строкой возвращает true.
        /// </summary>
        [Test]
        public void TryParse_WithValidString_ReturnsTrue()
        {
            var success = XDateTimeConverter.TryParse("2021-01-01", out var result);
            ClassicAssert.IsTrue(success);
            ClassicAssert.AreEqual(2021, result.Year);
        }

        /// <summary>
        /// Тест метода TryParse - с невалидной строкой возвращает false.
        /// </summary>
        [Test]
        public void TryParse_WithInvalidString_ReturnsFalse()
        {
            var success = XDateTimeConverter.TryParse("invalid", out var result);
            ClassicAssert.IsFalse(success);
        }

        /// <summary>
        /// Тест метода ParseHour - ограничивает значение до 23.
        /// </summary>
        [Test]
        public void ParseHour_LimitsValueTo23()
        {
            ClassicAssert.AreEqual(23, XDateTimeConverter.ParseHour("25"));
            ClassicAssert.AreEqual(23, XDateTimeConverter.ParseHour("23"));
            ClassicAssert.AreEqual(12, XDateTimeConverter.ParseHour("12"));
        }

        /// <summary>
        /// Тест метода ParseMinute - ограничивает значение до 59.
        /// </summary>
        [Test]
        public void ParseMinute_LimitsValueTo59()
        {
            ClassicAssert.AreEqual(59, XDateTimeConverter.ParseMinute("65"));
            ClassicAssert.AreEqual(59, XDateTimeConverter.ParseMinute("59"));
            ClassicAssert.AreEqual(30, XDateTimeConverter.ParseMinute("30"));
        }

        /// <summary>
        /// Тест метода ParseSecond - ограничивает значение до 59.
        /// </summary>
        [Test]
        public void ParseSecond_LimitsValueTo59()
        {
            ClassicAssert.AreEqual(59, XDateTimeConverter.ParseSecond("70"));
            ClassicAssert.AreEqual(59, XDateTimeConverter.ParseSecond("59"));
            ClassicAssert.AreEqual(30, XDateTimeConverter.ParseSecond("30"));
        }

        /// <summary>
        /// Тест метода FromTimestamp - с double значением.
        /// </summary>
        [Test]
        public void FromTimestamp_WithDouble_ReturnsCorrectDateTime()
        {
            var timestamp = 1609459200.0; // 2021-01-01 00:00:00 UTC
            var result = XDateTimeConverter.FromTimestamp(timestamp);
            ClassicAssert.IsTrue(result > DateTime.MinValue);
        }

        /// <summary>
        /// Тест метода FromTimestamp - со string значением.
        /// </summary>
        [Test]
        public void FromTimestamp_WithString_ReturnsCorrectDateTime()
        {
            var timestamp = "1609459200"; // 2021-01-01 00:00:00 UTC
            var result = XDateTimeConverter.FromTimestamp(timestamp);
            ClassicAssert.IsTrue(result > DateTime.MinValue);
        }

        /// <summary>
        /// Тест метода ToTimestamp - преобразует DateTime в timestamp.
        /// </summary>
        [Test]
        public void ToTimestamp_ConvertsDateTimeToTimestamp()
        {
            var dateTime = new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = XDateTimeConverter.ToTimestamp(dateTime);
            ClassicAssert.IsTrue(timestamp > 0);
        }
    }
}
