using System;

namespace Lotus.Core.Converters
{
    /// <summary>
    /// Тесты для <see cref="XDateTimeConverter"/>.
    /// </summary>
    public class DateTimeConverterTests
    {
        /// <summary>
        /// Тест метода ToDateTime - с int значением (timestamp).
        /// </summary>
        [Fact]
        public void ToDateTime_WithIntTimestamp_ReturnsCorrectValue()
        {
            var timestamp = 1609459200; // 2021-01-01 00:00:00 UTC
            var result = XDateTimeConverter.ToDateTime(timestamp, DateTime.MinValue);
            Assert.True(result > DateTime.MinValue);
        }

        /// <summary>
        /// Тест метода ToDateTime - с long значением (timestamp).
        /// </summary>
        [Fact]
        public void ToDateTime_WithLongTimestamp_ReturnsCorrectValue()
        {
            var timestamp = 1609459200L; // 2021-01-01 00:00:00 UTC
            var result = XDateTimeConverter.ToDateTime(timestamp, DateTime.MinValue);
            Assert.True(result > DateTime.MinValue);
        }

        /// <summary>
        /// Тест метода ToDateTime - с string значением.
        /// </summary>
        [Fact]
        public void ToDateTime_WithStringValue_ReturnsCorrectValue()
        {
            var defaultValue = new DateTime(2020, 1, 1);
            var result = XDateTimeConverter.ToDateTime("2021-01-01", defaultValue);
            Assert.Equal(2021, result.Year);
            Assert.Equal(1, result.Month);
            Assert.Equal(1, result.Day);
        }

        /// <summary>
        /// Тест метода ToDateTime - с null значением возвращает defaultValue.
        /// </summary>
        [Fact]
        public void ToDateTime_WithNull_ReturnsDefaultValue()
        {
            var defaultValue = new DateTime(2020, 1, 1);
            var result = XDateTimeConverter.ToDateTime(null!, defaultValue);
            Assert.Equal(defaultValue, result);
        }

        /// <summary>
        /// Тест метода Parse - с валидной строкой.
        /// </summary>
        [Fact]
        public void Parse_WithValidString_ReturnsCorrectValue()
        {
            var result = XDateTimeConverter.Parse("2021-01-01");
            Assert.Equal(2021, result.Year);
            Assert.Equal(1, result.Month);
            Assert.Equal(1, result.Day);
        }

        /// <summary>
        /// Тест метода Parse - с пустой строкой возвращает default.
        /// </summary>
        [Fact]
        public void Parse_WithEmptyString_ReturnsDefault()
        {
            var result = XDateTimeConverter.Parse("");
            Assert.Equal(default(DateTime), result);
        }

        /// <summary>
        /// Тест метода TryParse - с валидной строкой возвращает true.
        /// </summary>
        [Fact]
        public void TryParse_WithValidString_ReturnsTrue()
        {
            var success = XDateTimeConverter.TryParse("2021-01-01", out var result);
            Assert.True(success);
            Assert.Equal(2021, result.Year);
        }

        /// <summary>
        /// Тест метода TryParse - с невалидной строкой возвращает false.
        /// </summary>
        [Fact]
        public void TryParse_WithInvalidString_ReturnsFalse()
        {
            var success = XDateTimeConverter.TryParse("invalid", out var result);
            Assert.False(success);
        }

        /// <summary>
        /// Тест метода ParseHour - ограничивает значение до 23.
        /// </summary>
        [Fact]
        public void ParseHour_LimitsValueTo23()
        {
            Assert.Equal(23, XDateTimeConverter.ParseHour("25"));
            Assert.Equal(23, XDateTimeConverter.ParseHour("23"));
            Assert.Equal(12, XDateTimeConverter.ParseHour("12"));
        }

        /// <summary>
        /// Тест метода ParseMinute - ограничивает значение до 59.
        /// </summary>
        [Fact]
        public void ParseMinute_LimitsValueTo59()
        {
            Assert.Equal(59, XDateTimeConverter.ParseMinute("65"));
            Assert.Equal(59, XDateTimeConverter.ParseMinute("59"));
            Assert.Equal(30, XDateTimeConverter.ParseMinute("30"));
        }

        /// <summary>
        /// Тест метода ParseSecond - ограничивает значение до 59.
        /// </summary>
        [Fact]
        public void ParseSecond_LimitsValueTo59()
        {
            Assert.Equal(59, XDateTimeConverter.ParseSecond("70"));
            Assert.Equal(59, XDateTimeConverter.ParseSecond("59"));
            Assert.Equal(30, XDateTimeConverter.ParseSecond("30"));
        }

        /// <summary>
        /// Тест метода FromTimestamp - с double значением.
        /// </summary>
        [Fact]
        public void FromTimestamp_WithDouble_ReturnsCorrectDateTime()
        {
            var timestamp = 1609459200.0; // 2021-01-01 00:00:00 UTC
            var result = XDateTimeConverter.FromTimestamp(timestamp);
            Assert.True(result > DateTime.MinValue);
        }

        /// <summary>
        /// Тест метода FromTimestamp - со string значением.
        /// </summary>
        [Fact]
        public void FromTimestamp_WithString_ReturnsCorrectDateTime()
        {
            var timestamp = "1609459200"; // 2021-01-01 00:00:00 UTC
            var result = XDateTimeConverter.FromTimestamp(timestamp);
            Assert.True(result > DateTime.MinValue);
        }

        /// <summary>
        /// Тест метода ToTimestamp - преобразует DateTime в timestamp.
        /// </summary>
        [Fact]
        public void ToTimestamp_ConvertsDateTimeToTimestamp()
        {
            var dateTime = new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = XDateTimeConverter.ToTimestamp(dateTime);
            Assert.True(timestamp > 0);
        }
    }
}
