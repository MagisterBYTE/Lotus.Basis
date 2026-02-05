using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Converters
{
    /// <summary>
    /// Тесты для <see cref="XPrimitiveConverter"/>.
    /// </summary>
    [TestFixture]
    public class PrimitiveConverterTests
    {
        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Boolean.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithBoolean_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<bool>(TypeCode.Boolean, "True");
            ClassicAssert.IsTrue(result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Int32.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithInt32_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<int>(TypeCode.Int32, "123");
            ClassicAssert.AreEqual(123, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Int64.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithInt64_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<long>(TypeCode.Int64, "123456789012345");
            ClassicAssert.AreEqual(123456789012345L, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Single.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithSingle_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<float>(TypeCode.Single, "123.45");
            ClassicAssert.AreEqual(123.45f, result, 0.01f);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Double.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithDouble_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<double>(TypeCode.Double, "123.456789");
            ClassicAssert.AreEqual(123.456789, result, 0.000001);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Decimal.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithDecimal_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<decimal>(TypeCode.Decimal, "123.456789");
            ClassicAssert.AreEqual(123.456789m, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.String.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithString_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<string>(TypeCode.String, "Test");
            ClassicAssert.AreEqual("Test", result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.DateTime.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithDateTime_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<DateTime>(TypeCode.DateTime, "2021-01-01");
            ClassicAssert.AreEqual(2021, result.Year);
            ClassicAssert.AreEqual(1, result.Month);
            ClassicAssert.AreEqual(1, result.Day);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Char.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithChar_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<char>(TypeCode.Char, "A");
            ClassicAssert.AreEqual('A', result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Byte.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithByte_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<byte>(TypeCode.Byte, "255");
            ClassicAssert.AreEqual(255, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.SByte.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithSByte_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<sbyte>(TypeCode.SByte, "127");
            ClassicAssert.AreEqual(127, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Int16.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithInt16_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<short>(TypeCode.Int16, "12345");
            ClassicAssert.AreEqual(12345, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.UInt16.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithUInt16_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<ushort>(TypeCode.UInt16, "65535");
            ClassicAssert.AreEqual(65535, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.UInt32.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithUInt32_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<uint>(TypeCode.UInt32, "4294967295");
            ClassicAssert.AreEqual(4294967295u, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.UInt64.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithUInt64_ReturnsCorrectValue()
        {
            var result = XPrimitiveConverter.ParsePrimitive<ulong>(TypeCode.UInt64, "18446744073709551615");
            ClassicAssert.AreEqual(18446744073709551615UL, result);
        }

        /// <summary>
        /// Тест метода ParsePrimitive - с TypeCode.Empty возвращает default.
        /// </summary>
        [Test]
        public void ParsePrimitive_WithEmpty_ReturnsDefault()
        {
            var result = XPrimitiveConverter.ParsePrimitive<int>(TypeCode.Empty, "123");
            ClassicAssert.AreEqual(default(int), result);
        }

        /// <summary>
        /// Тест метода ToPrimitive - возвращает default (не реализован).
        /// </summary>
        [Test]
        public void ToPrimitive_ReturnsDefault()
        {
            var result = XPrimitiveConverter.ToPrimitive<int>(TypeCode.Int32, 123);
            ClassicAssert.AreEqual(default(int), result);
        }
    }
}
