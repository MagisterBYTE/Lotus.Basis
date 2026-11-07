using System;

namespace Lotus.Core.Converters
{
    /// <summary>
    /// Тесты для <see cref="XBooleanConverter"/>.
    /// </summary>
    public class BooleanConverterTests
    {
        /// <summary>
        /// Тест метода ToBoolean - с int значением.
        /// </summary>
        [Fact]
        public void ToBoolean_WithIntValue_ReturnsCorrectValue()
        {
            Assert.True(XBooleanConverter.ToBoolean(1));
            Assert.False(XBooleanConverter.ToBoolean(0));
        }

        /// <summary>
        /// Тест метода ToBoolean - с long значением.
        /// </summary>
        [Fact]
        public void ToBoolean_WithLongValue_ReturnsCorrectValue()
        {
            Assert.True(XBooleanConverter.ToBoolean(1L));
            Assert.False(XBooleanConverter.ToBoolean(0L));
        }

        /// <summary>
        /// Тест метода ToBoolean - с float значением.
        /// </summary>
        [Fact]
        public void ToBoolean_WithFloatValue_ReturnsCorrectValue()
        {
            Assert.True(XBooleanConverter.ToBoolean(1.0f));
            Assert.False(XBooleanConverter.ToBoolean(0.0f));
        }

        /// <summary>
        /// Тест метода ToBoolean - с double значением.
        /// </summary>
        [Fact]
        public void ToBoolean_WithDoubleValue_ReturnsCorrectValue()
        {
            Assert.True(XBooleanConverter.ToBoolean(1.0));
            Assert.False(XBooleanConverter.ToBoolean(0.0));
        }

        /// <summary>
        /// Тест метода ToBoolean - с string значением "True".
        /// </summary>
        [Fact]
        public void ToBoolean_WithStringTrue_ReturnsTrue()
        {
            Assert.True(XBooleanConverter.ToBoolean("True"));
            Assert.True(XBooleanConverter.ToBoolean("true"));
            Assert.True(XBooleanConverter.ToBoolean("1"));
            Assert.True(XBooleanConverter.ToBoolean("on"));
            Assert.True(XBooleanConverter.ToBoolean("On"));
            Assert.True(XBooleanConverter.ToBoolean("истина"));
            Assert.True(XBooleanConverter.ToBoolean("Истина"));
            Assert.True(XBooleanConverter.ToBoolean("да"));
            Assert.True(XBooleanConverter.ToBoolean("Да"));
        }

        /// <summary>
        /// Тест метода ToBoolean - с string значением "False".
        /// </summary>
        [Fact]
        public void ToBoolean_WithStringFalse_ReturnsFalse()
        {
            Assert.False(XBooleanConverter.ToBoolean("False"));
            Assert.False(XBooleanConverter.ToBoolean("false"));
            Assert.False(XBooleanConverter.ToBoolean("0"));
            Assert.False(XBooleanConverter.ToBoolean("off"));
        }

        /// <summary>
        /// Тест метода ToBoolean - с null значением возвращает defaultValue.
        /// </summary>
        [Fact]
        public void ToBoolean_WithNull_ReturnsDefaultValue()
        {
            Assert.False(XBooleanConverter.ToBoolean(null!));
            Assert.True(XBooleanConverter.ToBoolean(null!, true));
        }

        /// <summary>
        /// Тест метода Parse - с различными true значениями.
        /// </summary>
        [Fact]
        public void Parse_WithTrueValues_ReturnsTrue()
        {
            Assert.True(XBooleanConverter.Parse("True"));
            Assert.True(XBooleanConverter.Parse("true"));
            Assert.True(XBooleanConverter.Parse("1"));
            Assert.True(XBooleanConverter.Parse("on"));
            Assert.True(XBooleanConverter.Parse("истина"));
            Assert.True(XBooleanConverter.Parse("да"));
        }

        /// <summary>
        /// Тест метода Parse - с false значениями.
        /// </summary>
        [Fact]
        public void Parse_WithFalseValues_ReturnsFalse()
        {
            Assert.False(XBooleanConverter.Parse("False"));
            Assert.False(XBooleanConverter.Parse("false"));
            Assert.False(XBooleanConverter.Parse("0"));
            Assert.False(XBooleanConverter.Parse("off"));
        }
    }
}
