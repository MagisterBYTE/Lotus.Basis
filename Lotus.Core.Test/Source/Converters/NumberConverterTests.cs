using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Converters
{
    [TestFixture]
    public class NumberConverterTests
    {
        [Test]
        public void ParsableTextInteger()
        {
            var result = XNumberConverter.ParsableTextInteger("0122.33", 19);
            ClassicAssert.AreEqual("0122", result);

            result = XNumberConverter.ParsableTextInteger("0122,33", 19);
            ClassicAssert.AreEqual("0122", result);

            result = XNumberConverter.ParsableTextInteger("0122,33,222", 19);
            ClassicAssert.AreEqual("012233222", result);

            result = XNumberConverter.ParsableTextInteger("0122,33,222.222", 19);
            ClassicAssert.AreEqual("012233222", result);
        }

        [Test]
        public void ToInt_WithValidInteger_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToInt(123);
            ClassicAssert.AreEqual(123, result);
        }

        [Test]
        public void ToInt_WithValidString_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToInt("123");
            ClassicAssert.AreEqual(123, result);
        }

        [Test]
        public void ToInt_WithInvalidString_ReturnsDefaultValue()
        {
            var result = XNumberConverter.ToInt("abc", 999);
            ClassicAssert.AreEqual(999, result);
        }

        [Test]
        public void ParseInt_WithValidString_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ParseInt("123");
            ClassicAssert.AreEqual(123, result);
        }

        [Test]
        public void ParseInt_WithInvalidString_ReturnsDefaultValue()
        {
            var result = XNumberConverter.ParseInt("abc", 999);
            ClassicAssert.AreEqual(999, result);
        }

        [Test]
        public void TryParseInt_WithValidString_ReturnsTrueAndCorrectValue()
        {
            var success = XNumberConverter.TryParseInt("123", out var result);
            ClassicAssert.IsTrue(success);
            ClassicAssert.AreEqual(123, result);
        }

        [Test]
        public void TryParseInt_WithInvalidString_ReturnsFalse()
        {
            var success = XNumberConverter.TryParseInt("abc", out var result);
            ClassicAssert.IsFalse(success);
        }

        [Test]
        public void ToLong_WithValidLong_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToLong(1234567890123456789L);
            ClassicAssert.AreEqual(1234567890123456789L, result);
        }

        [Test]
        public void ToLong_WithValidString_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToLong("1234567890123456789");
            ClassicAssert.AreEqual(1234567890123456789L, result);
        }

        [Test]
        public void ParseLong_WithValidString_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ParseLong("1234567890123456789");
            ClassicAssert.AreEqual(1234567890123456789L, result);
        }

        [Test]
        public void TryParseLong_WithValidString_ReturnsTrueAndCorrectValue()
        {
            var success = XNumberConverter.TryParseLong("1234567890123456789", out var result);
            ClassicAssert.IsTrue(success);
            ClassicAssert.AreEqual(1234567890123456789L, result);
        }

        [Test]
        public void ToSingle_WithValidFloat_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToSingle(123.45f);
            ClassicAssert.AreEqual(123.45f, result);
        }

        [Test]
        public void ToSingle_WithValidString_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToSingle("123.45");
            ClassicAssert.AreEqual(123.45f, result);
        }

        [Test]
        public void ParseSingle_WithValidString_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ParseSingle("123.45");
            ClassicAssert.AreEqual(123.45f, result);
        }

        [Test]
        public void TryParseSingle_WithValidString_ReturnsTrueAndCorrectValue()
        {
            var success = XNumberConverter.TryParseSingle("123.45", out var result);
            ClassicAssert.IsTrue(success);
            ClassicAssert.AreEqual(123.45f, result);
        }

        [Test]
        public void ToDouble_WithValidDouble_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToDouble(123.456789);
            ClassicAssert.AreEqual(123.456789, result);
        }

        [Test]
        public void ToDouble_WithValidString_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToDouble("123.456789");
            ClassicAssert.AreEqual(123.456789, result);
        }

        [Test]
        public void ParseDouble_WithValidString_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ParseDouble("1,23.456789");
            ClassicAssert.AreEqual(123.456789, result);
        }

        [Test]
        public void TryParseDouble_WithValidString_ReturnsTrueAndCorrectValue()
        {
            var success = XNumberConverter.TryParseDouble("123,456789", out var result);
            ClassicAssert.IsTrue(success);
            ClassicAssert.AreEqual(123.456789, result);
        }

        [Test]
        public void ToDecimal_WithValidDecimal_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToDecimal(123.456789m);
            ClassicAssert.AreEqual(123.456789m, result);
        }

        [Test]
        public void ToDecimal_WithValidString_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToDecimal("123,456789");
            ClassicAssert.AreEqual(123.456789m, result);
        }

        [Test]
        public void ParseDecimal_WithValidString_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ParseDecimal("123.456789");
            ClassicAssert.AreEqual(123.456789m, result);
        }


        [Test]
        public void ToNumber_WithDoubleAndInt32TargetType_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToNumber(typeof(int), 123.456);
            ClassicAssert.AreEqual(123, result);
        }

        [Test]
        public void ToNumber_WithDecimalAndDoubleTargetType_ReturnsCorrectValue()
        {
            var result = XNumberConverter.ToNumber(typeof(double), 123.456789m);
            ClassicAssert.AreEqual(123.456789, result);
        }
    }
}
