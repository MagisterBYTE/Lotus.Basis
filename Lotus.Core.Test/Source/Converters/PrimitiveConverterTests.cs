using System;

namespace Lotus.Core.Converters
{
    /// <summary>
    /// Тесты для <see cref="XPrimitiveConverter"/>.
    /// </summary>
    public class PrimitiveConverterTests
    {
        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Boolean.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithBoolean_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<bool>(TypeCode.Boolean, "True");
            Assert.True(result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Int32.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithInt32_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<int>(TypeCode.Int32, "123");
            Assert.Equal(123, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Int64.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithInt64_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<long>(TypeCode.Int64, "123456789012345");
            Assert.Equal(123456789012345L, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Single.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithSingle_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<float>(TypeCode.Single, "123.45");
            Assert.Equal(123.45f, result, 0.01f);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Double.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithDouble_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<double>(TypeCode.Double, "123.456789");
            Assert.Equal(123.456789, result, 0.000001);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Decimal.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithDecimal_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<decimal>(TypeCode.Decimal, "123.456789");
            Assert.Equal(123.456789m, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.String.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithString_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<string>(TypeCode.String, "Test");
            Assert.Equal("Test", result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.DateTime.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithDateTime_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<DateTime>(TypeCode.DateTime, "2021-01-01");
            Assert.Equal(2021, result.Year);
            Assert.Equal(1, result.Month);
            Assert.Equal(1, result.Day);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Char.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithChar_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<char>(TypeCode.Char, "A");
            Assert.Equal('A', result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Byte.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithByte_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<byte>(TypeCode.Byte, "255");
            Assert.Equal(255, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.SByte.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithSByte_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<sbyte>(TypeCode.SByte, "127");
            Assert.Equal(127, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Int16.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithInt16_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<short>(TypeCode.Int16, "12345");
            Assert.Equal(12345, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.UInt16.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithUInt16_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<ushort>(TypeCode.UInt16, "65535");
            Assert.Equal(65535, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.UInt32.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithUInt32_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<uint>(TypeCode.UInt32, "4294967295");
            Assert.Equal(4294967295u, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.UInt64.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithUInt64_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<ulong>(TypeCode.UInt64, "18446744073709551615");
            Assert.Equal(18446744073709551615UL, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Empty возвращает default.
        /// </summary>
        [Fact]
        public void ParsePrimitive_WithEmpty_ReturnsDefault()
        {
            var result = XPrimitiveConverter.ParsePrimitive<int>(TypeCode.Empty, "123");
            Assert.Equal(default(int), result);
        }

        /// <summary>
        /// Тест метода ToPrimitive - возвращает default (не реализован).
        /// </summary>
        [Fact]
        public void ToPrimitive_ReturnsDefault()
        {
            var result = XPrimitiveConverter.ToPrimitive<int>(TypeCode.Int32, 123);
            Assert.Equal(default(int), result);
        }
    }
}
