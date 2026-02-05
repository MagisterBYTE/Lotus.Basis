using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Converters
{
    /// <summary>
    /// Тесты для <see cref="XBooleanConverter"/>.
    /// </summary>
    [TestFixture]
    public class BooleanConverterTests
    {
        /// <summary>
        /// Тест метода ToBoolean - с int значением.
        /// </summary>
        [Test]
        public void ToBoolean_WithIntValue_ReturnsCorrectValue()
        {
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean(1));
            ClassicAssert.IsFalse(XBooleanConverter.ToBoolean(0));
        }

        /// <summary>
        /// Тест метода ToBoolean - с long значением.
        /// </summary>
        [Test]
        public void ToBoolean_WithLongValue_ReturnsCorrectValue()
        {
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean(1L));
            ClassicAssert.IsFalse(XBooleanConverter.ToBoolean(0L));
        }

        /// <summary>
        /// Тест метода ToBoolean - с float значением.
        /// </summary>
        [Test]
        public void ToBoolean_WithFloatValue_ReturnsCorrectValue()
        {
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean(1.0f));
            ClassicAssert.IsFalse(XBooleanConverter.ToBoolean(0.0f));
        }

        /// <summary>
        /// Тест метода ToBoolean - с double значением.
        /// </summary>
        [Test]
        public void ToBoolean_WithDoubleValue_ReturnsCorrectValue()
        {
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean(1.0));
            ClassicAssert.IsFalse(XBooleanConverter.ToBoolean(0.0));
        }

        /// <summary>
        /// Тест метода ToBoolean - с string значением "True".
        /// </summary>
        [Test]
        public void ToBoolean_WithStringTrue_ReturnsTrue()
        {
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean("True"));
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean("true"));
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean("1"));
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean("on"));
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean("On"));
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean("истина"));
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean("Истина"));
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean("да"));
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean("Да"));
        }

        /// <summary>
        /// Тест метода ToBoolean - с string значением "False".
        /// </summary>
        [Test]
        public void ToBoolean_WithStringFalse_ReturnsFalse()
        {
            ClassicAssert.IsFalse(XBooleanConverter.ToBoolean("False"));
            ClassicAssert.IsFalse(XBooleanConverter.ToBoolean("false"));
            ClassicAssert.IsFalse(XBooleanConverter.ToBoolean("0"));
            ClassicAssert.IsFalse(XBooleanConverter.ToBoolean("off"));
        }

        /// <summary>
        /// Тест метода ToBoolean - с null значением возвращает defaultValue.
        /// </summary>
        [Test]
        public void ToBoolean_WithNull_ReturnsDefaultValue()
        {
            ClassicAssert.IsFalse(XBooleanConverter.ToBoolean(null!));
            ClassicAssert.IsTrue(XBooleanConverter.ToBoolean(null!, true));
        }

        /// <summary>
        /// Тест метода Parse - с различными true значениями.
        /// </summary>
        [Test]
        public void Parse_WithTrueValues_ReturnsTrue()
        {
            ClassicAssert.IsTrue(XBooleanConverter.Parse("True"));
            ClassicAssert.IsTrue(XBooleanConverter.Parse("true"));
            ClassicAssert.IsTrue(XBooleanConverter.Parse("1"));
            ClassicAssert.IsTrue(XBooleanConverter.Parse("on"));
            ClassicAssert.IsTrue(XBooleanConverter.Parse("истина"));
            ClassicAssert.IsTrue(XBooleanConverter.Parse("да"));
        }

        /// <summary>
        /// Тест метода Parse - с false значениями.
        /// </summary>
        [Test]
        public void Parse_WithFalseValues_ReturnsFalse()
        {
            ClassicAssert.IsFalse(XBooleanConverter.Parse("False"));
            ClassicAssert.IsFalse(XBooleanConverter.Parse("false"));
            ClassicAssert.IsFalse(XBooleanConverter.Parse("0"));
            ClassicAssert.IsFalse(XBooleanConverter.Parse("off"));
        }
    }
}
