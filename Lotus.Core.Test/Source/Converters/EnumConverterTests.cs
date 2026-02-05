using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Converters
{
    /// <summary>
    /// Тесты для <see cref="XEnumConverter"/>.
    /// </summary>
    [TestFixture]
    public class EnumConverterTests
    {
        private enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }

        /// <summary>
        /// Тест метода ToEnum - со string значением.
        /// </summary>
        [Test]
        public void ToEnum_WithStringValue_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnum<TestEnum>("Value1");
            ClassicAssert.AreEqual(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода ToEnum - со string значением (case insensitive).
        /// </summary>
        [Test]
        public void ToEnum_WithStringValueCaseInsensitive_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnum<TestEnum>("value1");
            ClassicAssert.AreEqual(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода ToEnum - с невалидной строкой возвращает defaultValue.
        /// </summary>
        [Test]
        public void ToEnum_WithInvalidString_ReturnsDefaultValue()
        {
            var defaultValue = TestEnum.Value2;
            var result = XEnumConverter.ToEnum<TestEnum>("InvalidValue", defaultValue);
            ClassicAssert.AreEqual(defaultValue, result);
        }

        /// <summary>
        /// Тест метода ToEnum - с int значением.
        /// </summary>
        [Test]
        public void ToEnum_WithIntValue_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnum<TestEnum>(0);
            ClassicAssert.AreEqual(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода ToEnum - с object значением (string).
        /// </summary>
        [Test]
        public void ToEnum_WithObjectString_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnum<TestEnum>((object)"Value2", TestEnum.Value1);
            ClassicAssert.AreEqual(TestEnum.Value2, result);
        }

        /// <summary>
        /// Тест метода ToEnum - с object значением (int).
        /// </summary>
        [Test]
        public void ToEnum_WithObjectInt_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnum<TestEnum>((object)1, TestEnum.Value1);
            ClassicAssert.AreEqual(TestEnum.Value2, result);
        }

        /// <summary>
        /// Тест метода ToEnumOfType - с string значением.
        /// </summary>
        [Test]
        public void ToEnumOfType_WithStringValue_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnumOfType(typeof(TestEnum), "Value1");
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода ToEnumOfType - с int значением.
        /// </summary>
        [Test]
        public void ToEnumOfType_WithIntValue_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnumOfType(typeof(TestEnum), 0);
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода ToEnumOfType - с null значением возвращает значение по умолчанию.
        /// </summary>
        [Test]
        public void ToEnumOfType_WithNull_ReturnsDefaultValue()
        {
            var result = XEnumConverter.ToEnumOfType(typeof(TestEnum), null);
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(TestEnum.Value1, result); // 0 = Value1
        }

        /// <summary>
        /// Тест метода TryToEnum - с валидной строкой возвращает true.
        /// </summary>
        [Test]
        public void TryToEnum_WithValidString_ReturnsTrue()
        {
            var success = XEnumConverter.TryToEnum<TestEnum>("Value1", out var result);
            ClassicAssert.IsTrue(success);
            ClassicAssert.AreEqual(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода TryToEnum - с невалидной строкой возвращает false.
        /// </summary>
        [Test]
        public void TryToEnum_WithInvalidString_ReturnsFalse()
        {
            var success = XEnumConverter.TryToEnum<TestEnum>("InvalidValue", out var result);
            ClassicAssert.IsFalse(success);
        }

        /// <summary>
        /// Тест метода TryToEnum - с null значением возвращает true и значение по умолчанию.
        /// </summary>
        [Test]
        public void TryToEnum_WithNull_ReturnsTrueWithDefaultValue()
        {
            var success = XEnumConverter.TryToEnum<TestEnum>(null, out var result);
            ClassicAssert.IsTrue(success);
            ClassicAssert.AreEqual(TestEnum.Value1, result); // 0 = Value1
        }

        /// <summary>
        /// Тест метода ToSplitEnums - с валидной строкой.
        /// </summary>
        [Test]
        public void ToSplitEnums_WithValidString_ReturnsArray()
        {
            var result = XEnumConverter.ToSplitEnums<TestEnum>("Value1,Value2,Value3");
            ClassicAssert.AreEqual(3, result.Length);
            ClassicAssert.AreEqual(TestEnum.Value1, result[0]);
            ClassicAssert.AreEqual(TestEnum.Value2, result[1]);
            ClassicAssert.AreEqual(TestEnum.Value3, result[2]);
        }

        /// <summary>
        /// Тест метода ToSplitEnums - с пустой строкой возвращает пустой массив.
        /// </summary>
        [Test]
        public void ToSplitEnums_WithEmptyString_ReturnsEmptyArray()
        {
            var result = XEnumConverter.ToSplitEnums<TestEnum>("");
            ClassicAssert.AreEqual(0, result.Length);
        }

        /// <summary>
        /// Тест метода ToSplitEnums - с кастомным сепаратором.
        /// </summary>
        [Test]
        public void ToSplitEnums_WithCustomSeparator_ReturnsArray()
        {
            var result = XEnumConverter.ToSplitEnums<TestEnum>("Value1;Value2", ";");
            ClassicAssert.AreEqual(2, result.Length);
            ClassicAssert.AreEqual(TestEnum.Value1, result[0]);
            ClassicAssert.AreEqual(TestEnum.Value2, result[1]);
        }
    }
}
