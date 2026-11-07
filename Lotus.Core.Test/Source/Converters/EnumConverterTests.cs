using System;

namespace Lotus.Core.Converters
{
    /// <summary>
    /// Тесты для <see cref="XEnumConverter"/>.
    /// </summary>
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
        [Fact]
        public void ToEnum_WithStringValue_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnum<TestEnum>("Value1");
            Assert.Equal(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода ToEnum - со string значением (case insensitive).
        /// </summary>
        [Fact]
        public void ToEnum_WithStringValueCaseInsensitive_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnum<TestEnum>("value1");
            Assert.Equal(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода ToEnum - с невалидной строкой возвращает defaultValue.
        /// </summary>
        [Fact]
        public void ToEnum_WithInvalidString_ReturnsDefaultValue()
        {
            var defaultValue = TestEnum.Value2;
            var result = XEnumConverter.ToEnum<TestEnum>("InvalidValue", defaultValue);
            Assert.Equal(defaultValue, result);
        }

        /// <summary>
        /// Тест метода ToEnum - с int значением.
        /// </summary>
        [Fact]
        public void ToEnum_WithIntValue_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnum<TestEnum>(0);
            Assert.Equal(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода ToEnum - с object значением (string).
        /// </summary>
        [Fact]
        public void ToEnum_WithObjectString_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnum<TestEnum>((object)"Value2", TestEnum.Value1);
            Assert.Equal(TestEnum.Value2, result);
        }

        /// <summary>
        /// Тест метода ToEnum - с object значением (int).
        /// </summary>
        [Fact]
        public void ToEnum_WithObjectInt_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnum<TestEnum>((object)1, TestEnum.Value1);
            Assert.Equal(TestEnum.Value2, result);
        }

        /// <summary>
        /// Тест метода ToEnumOfType - с string значением.
        /// </summary>
        [Fact]
        public void ToEnumOfType_WithStringValue_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnumOfType(typeof(TestEnum), "Value1");
            Assert.NotNull(result);
            Assert.Equal(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода ToEnumOfType - с int значением.
        /// </summary>
        [Fact]
        public void ToEnumOfType_WithIntValue_ReturnsCorrectEnum()
        {
            var result = XEnumConverter.ToEnumOfType(typeof(TestEnum), 0);
            Assert.NotNull(result);
            Assert.Equal(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода ToEnumOfType - с null значением возвращает значение по умолчанию.
        /// </summary>
        [Fact]
        public void ToEnumOfType_WithNull_ReturnsDefaultValue()
        {
            var result = XEnumConverter.ToEnumOfType(typeof(TestEnum), null);
            Assert.NotNull(result);
            Assert.Equal(TestEnum.Value1, result); // 0 = Value1
        }

        /// <summary>
        /// Тест метода TryToEnum - с валидной строкой возвращает true.
        /// </summary>
        [Fact]
        public void TryToEnum_WithValidString_ReturnsTrue()
        {
            var success = XEnumConverter.TryToEnum<TestEnum>("Value1", out var result);
            Assert.True(success);
            Assert.Equal(TestEnum.Value1, result);
        }

        /// <summary>
        /// Тест метода TryToEnum - с невалидной строкой возвращает false.
        /// </summary>
        [Fact]
        public void TryToEnum_WithInvalidString_ReturnsFalse()
        {
            var success = XEnumConverter.TryToEnum<TestEnum>("InvalidValue", out var result);
            Assert.False(success);
        }

        /// <summary>
        /// Тест метода TryToEnum - с null значением возвращает true и значение по умолчанию.
        /// </summary>
        [Fact]
        public void TryToEnum_WithNull_ReturnsTrueWithDefaultValue()
        {
            var success = XEnumConverter.TryToEnum<TestEnum>(null, out var result);
            Assert.True(success);
            Assert.Equal(TestEnum.Value1, result); // 0 = Value1
        }

        /// <summary>
        /// Тест метода ToSplitEnums - с валидной строкой.
        /// </summary>
        [Fact]
        public void ToSplitEnums_WithValidString_ReturnsArray()
        {
            var result = XEnumConverter.ToSplitEnums<TestEnum>("Value1,Value2,Value3");
            Assert.Equal(3, result.Length);
            Assert.Equal(TestEnum.Value1, result[0]);
            Assert.Equal(TestEnum.Value2, result[1]);
            Assert.Equal(TestEnum.Value3, result[2]);
        }

        /// <summary>
        /// Тест метода ToSplitEnums - с пустой строкой возвращает пустой массив.
        /// </summary>
        [Fact]
        public void ToSplitEnums_WithEmptyString_ReturnsEmptyArray()
        {
            var result = XEnumConverter.ToSplitEnums<TestEnum>("");
            Assert.Equal(0, result.Length);
        }

        /// <summary>
        /// Тест метода ToSplitEnums - с кастомным сепаратором.
        /// </summary>
        [Fact]
        public void ToSplitEnums_WithCustomSeparator_ReturnsArray()
        {
            var result = XEnumConverter.ToSplitEnums<TestEnum>("Value1;Value2", ";");
            Assert.Equal(2, result.Length);
            Assert.Equal(TestEnum.Value1, result[0]);
            Assert.Equal(TestEnum.Value2, result[1]);
        }
    }
}
