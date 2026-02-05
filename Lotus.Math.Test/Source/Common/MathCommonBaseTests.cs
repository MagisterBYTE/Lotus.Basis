using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathCommonBaseTests
    {
        #region XMath - IsZero Tests
        [Test]
        public void IsZero_Double_WithZeroValue_ReturnsTrue()
        {
            // Arrange & Act
            var result = XMath.IsZero(0.0);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void IsZero_Double_WithSmallValue_ReturnsTrue()
        {
            // Arrange & Act
            var result = XMath.IsZero(0.00000000002);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void IsZero_Double_WithNormalValue_ReturnsFalse()
        {
            // Arrange & Act
            var result = XMath.IsZero(0.1);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void IsZero_Float_WithZeroValue_ReturnsTrue()
        {
            // Arrange & Act
            var result = XMath.IsZero(0.0f);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void IsZero_Float_WithSmallValue_ReturnsTrue()
        {
            // Arrange & Act
            var result = XMath.IsZero(0.000000002f);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void IsZero_Float_WithNormalValue_ReturnsFalse()
        {
            // Arrange & Act
            var result = XMath.IsZero(0.1f);

            // Assert
            ClassicAssert.IsFalse(result);
        }
        #endregion

        #region XMath - IsOne Tests
        [Test]
        public void IsOne_Double_WithOneValue_ReturnsTrue()
        {
            // Arrange & Act
            var result = XMath.IsOne(1.0);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void IsOne_Double_WithAlmostOneValue_ReturnsTrue()
        {
            // Arrange & Act
            var result = XMath.IsOne(1.00000000002);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void IsOne_Double_WithNormalValue_ReturnsFalse()
        {
            // Arrange & Act
            var result = XMath.IsOne(2.0);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void IsOne_Float_WithOneValue_ReturnsTrue()
        {
            // Arrange & Act
            var result = XMath.IsOne(1.0f);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void IsOne_Float_WithAlmostOneValue_ReturnsTrue()
        {
            // Arrange & Act
            var result = XMath.IsOne(1.000000002f);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void IsOne_Float_WithNormalValue_ReturnsFalse()
        {
            // Arrange & Act
            var result = XMath.IsOne(2.0f);

            // Assert
            ClassicAssert.IsFalse(result);
        }
        #endregion

        #region XMath - Clamp01 Tests
        [Test]
        public void Clamp01_Double_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var value = 0.5;

            // Act
            var result = XMath.Clamp01(value);

            // Assert
            ClassicAssert.AreEqual(0.5, result);
        }

        [Test]
        public void Clamp01_Double_WithNegativeValue_ReturnsZero()
        {
            // Arrange
            var value = -0.5;

            // Act
            var result = XMath.Clamp01(value);

            // Assert
            ClassicAssert.AreEqual(0.0, result);
        }

        [Test]
        public void Clamp01_Double_WithValueGreaterThanOne_ReturnsOne()
        {
            // Arrange
            var value = 2.0;

            // Act
            var result = XMath.Clamp01(value);

            // Assert
            ClassicAssert.AreEqual(1.0, result);
        }

        [Test]
        public void Clamp01_Float_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var value = 0.5f;

            // Act
            var result = XMath.Clamp01(value);

            // Assert
            ClassicAssert.AreEqual(0.5f, result);
        }

        [Test]
        public void Clamp01_Float_WithNegativeValue_ReturnsZero()
        {
            // Arrange
            var value = -0.5f;

            // Act
            var result = XMath.Clamp01(value);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }

        [Test]
        public void Clamp01_Float_WithValueGreaterThanOne_ReturnsOne()
        {
            // Arrange
            var value = 2.0f;

            // Act
            var result = XMath.Clamp01(value);

            // Assert
            ClassicAssert.AreEqual(1.0f, result);
        }
        #endregion

        #region XMath - Clamp Tests
        [Test]
        public void Clamp_Double_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var value = 50.0;
            var min = 20.0;
            var max = 100.0;

            // Act
            var result = XMath.Clamp(value, min, max);

            // Assert
            ClassicAssert.AreEqual(50.0, result);
        }

        [Test]
        public void Clamp_Double_WithValueBelowMin_ReturnsMin()
        {
            // Arrange
            var value = 10.0;
            var min = 20.0;
            var max = 100.0;

            // Act
            var result = XMath.Clamp(value, min, max);

            // Assert
            ClassicAssert.AreEqual(20.0, result);
        }

        [Test]
        public void Clamp_Double_WithValueAboveMax_ReturnsMax()
        {
            // Arrange
            var value = 150.0;
            var min = 20.0;
            var max = 100.0;

            // Act
            var result = XMath.Clamp(value, min, max);

            // Assert
            ClassicAssert.AreEqual(100.0, result);
        }

        [Test]
        public void Clamp_Float_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var value = 50.0f;
            var min = 20.0f;
            var max = 100.0f;

            // Act
            var result = XMath.Clamp(value, min, max);

            // Assert
            ClassicAssert.AreEqual(50.0f, result);
        }

        [Test]
        public void Clamp_Float_WithValueBelowMin_ReturnsMin()
        {
            // Arrange
            var value = 10.0f;
            var min = 20.0f;
            var max = 100.0f;

            // Act
            var result = XMath.Clamp(value, min, max);

            // Assert
            ClassicAssert.AreEqual(20.0f, result);
        }

        [Test]
        public void Clamp_Float_WithValueAboveMax_ReturnsMax()
        {
            // Arrange
            var value = 150.0f;
            var min = 20.0f;
            var max = 100.0f;

            // Act
            var result = XMath.Clamp(value, min, max);

            // Assert
            ClassicAssert.AreEqual(100.0f, result);
        }

        [Test]
        public void Clamp_Int_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var value = 50;
            var min = 20;
            var max = 100;

            // Act
            var result = XMath.Clamp(value, min, max);

            // Assert
            ClassicAssert.AreEqual(50, result);
        }

        [Test]
        public void Clamp_Int_WithValueBelowMin_ReturnsMin()
        {
            // Arrange
            var value = 10;
            var min = 20;
            var max = 100;

            // Act
            var result = XMath.Clamp(value, min, max);

            // Assert
            ClassicAssert.AreEqual(20, result);
        }

        [Test]
        public void Clamp_Int_WithValueAboveMax_ReturnsMax()
        {
            // Arrange
            var value = 150;
            var min = 20;
            var max = 100;

            // Act
            var result = XMath.Clamp(value, min, max);

            // Assert
            ClassicAssert.AreEqual(100, result);
        }
        #endregion

        #region XMath - Approximately Tests
        [Test]
        public void Approximately_Double_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            var a = 200.003;
            var b = 200.0033;

            // Act
            var result = XMath.Approximately(a, b);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Approximately_Double_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            var a = 200.0;
            var b = 300.0;

            // Act
            var result = XMath.Approximately(a, b);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Approximately_Double_WithCustomEpsilon_ReturnsTrue()
        {
            // Arrange
            var a = 200.0;
            var b = 200.05;
            var epsilon = 0.1;

            // Act
            var result = XMath.Approximately(a, b, epsilon);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Approximately_Float_WithEqualValues_ReturnsTrue()
        {
            // Arrange
            var a = 200.003f;
            var b = 200.0033f;

            // Act
            var result = XMath.Approximately(a, b);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Approximately_Float_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            var a = 200.0f;
            var b = 300.0f;

            // Act
            var result = XMath.Approximately(a, b);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Approximately_Float_WithCustomEpsilon_ReturnsTrue()
        {
            // Arrange
            var a = 200.0f;
            var b = 200.05f;
            var epsilon = 0.1f;

            // Act
            var result = XMath.Approximately(a, b, epsilon);

            // Assert
            ClassicAssert.IsTrue(result);
        }
        #endregion

        #region XMath - ConvertInterval Tests
        [Test]
        public void ConvertInterval_Double_WithValidInputs_ReturnsCorrectValue()
        {
            // Arrange
            var destStart = 2.0;
            var destEnd = 5.0;
            var sourceStart = 0.0;
            var sourceEnd = 10.0;
            var value = 4.0;

            // Act
            var result = XMath.ConvertInterval(destStart, destEnd, sourceStart, sourceEnd, value);

            // Assert
            ClassicAssert.AreEqual(6.6666, result, 0.001);
        }

        [Test]
        public void ConvertInterval_Double_WithSimpleMapping_ReturnsCorrectValue()
        {
            // Arrange
            var destStart = 0.0;
            var destEnd = 6.0;
            var sourceStart = 2.0;
            var sourceEnd = 5.0;
            var value = 4.0;

            // Act
            var result = XMath.ConvertInterval(destStart, destEnd, sourceStart, sourceEnd, value);

            // Assert
            ClassicAssert.AreEqual(4.0, result, 0.001);
        }

        [Test]
        public void ConvertInterval_Float_WithValidInputs_ReturnsCorrectValue()
        {
            // Arrange
            var destStart = 2.0f;
            var destEnd = 5.0f;
            var sourceStart = 0.0f;
            var sourceEnd = 10.0f;
            var value = 4.0f;

            // Act
            var result = XMath.ConvertInterval(destStart, destEnd, sourceStart, sourceEnd, value);

            // Assert
            ClassicAssert.AreEqual(6.6666f, result, 0.001f);
        }
        #endregion

        #region XMath - RoundToNearest Tests
        [Test]
        public void RoundToNearest_Double_WithPositiveValue_ReturnsRoundedValue()
        {
            // Arrange
            var value = 2335.0233;
            var round = 1;

            // Act
            var result = XMath.RoundToNearest(value, round);

            // Assert
            ClassicAssert.AreEqual(2335.0, result);
        }

        [Test]
        public void RoundToNearest_Double_WithRoundTwo_ReturnsRoundedValue()
        {
            // Arrange
            var value = 2335.0233;
            var round = 2;

            // Act
            var result = XMath.RoundToNearest(value, round);

            // Assert
            ClassicAssert.AreEqual(2336.0, result);
        }

        [Test]
        public void RoundToNearest_Double_WithRoundTen_ReturnsRoundedValue()
        {
            // Arrange
            var value = 2335.0233;
            var round = 10;

            // Act
            var result = XMath.RoundToNearest(value, round);

            // Assert
            ClassicAssert.AreEqual(2340.0, result);
        }

        [Test]
        public void RoundToNearest_Double_WithRoundHundred_ReturnsRoundedValue()
        {
            // Arrange
            var value = 2330.0233;
            var round = 100;

            // Act
            var result = XMath.RoundToNearest(value, round);

            // Assert
            ClassicAssert.AreEqual(2300.0, result);
        }

        [Test]
        public void RoundToNearest_Double_WithNegativeValue_ReturnsRoundedValue()
        {
            // Arrange
            var value = -2335.0233;
            var round = 10;

            // Act
            var result = XMath.RoundToNearest(value, round);

            // Assert
            ClassicAssert.AreEqual(-2340.0, result);
        }

        [Test]
        public void RoundToNearest_Float_WithPositiveValue_ReturnsRoundedValue()
        {
            // Arrange
            var value = 2335.0233f;
            var round = 1;

            // Act
            var result = XMath.RoundToNearest(value, round);

            // Assert
            ClassicAssert.AreEqual(2335.0f, result);
        }
        #endregion

        #region XMath - RoundToSingle Tests
        [Test]
        public void RoundToSingle_WithPositiveValue_ReturnsRoundedValue()
        {
            // Arrange
            var value = 1782.56f;
            var round = 0.2f;

            // Act
            var result = XMath.RoundToSingle(value, round);

            // Assert
            ClassicAssert.AreEqual(1782.6f, result, 0.01f);
        }

        [Test]
        public void RoundToSingle_WithValueAtBoundary_ReturnsRoundedValue()
        {
            // Arrange
            var value = 1782.5f;
            var round = 0.2f;

            // Act
            var result = XMath.RoundToSingle(value, round);

            // Assert
            ClassicAssert.AreEqual(1782.6f, result, 0.01f);
        }

        [Test]
        public void RoundToSingle_WithValueBelowBoundary_ReturnsRoundedValue()
        {
            // Arrange
            var value = 1782.41f;
            var round = 0.2f;

            // Act
            var result = XMath.RoundToSingle(value, round);

            // Assert
            ClassicAssert.AreEqual(1782.4f, result, 0.01f);
        }

        [Test]
        public void RoundToSingle_WithNegativeValue_ReturnsRoundedValue()
        {
            // Arrange
            var value = -1782.56f;
            var round = 0.2f;

            // Act
            var result = XMath.RoundToSingle(value, round);

            // Assert
            ClassicAssert.AreEqual(-1782.6f, result, 0.01f);
        }
        #endregion

        #region XMath - Swap Tests
        [Test]
        public void Swap_WithIntValues_SwapsValues()
        {
            // Arrange
            var left = 10;
            var right = 20;

            // Act
            XMath.Swap(ref left, ref right);

            // Assert
            ClassicAssert.AreEqual(20, left);
            ClassicAssert.AreEqual(10, right);
        }

        [Test]
        public void Swap_WithDoubleValues_SwapsValues()
        {
            // Arrange
            var left = 10.5;
            var right = 20.7;

            // Act
            XMath.Swap(ref left, ref right);

            // Assert
            ClassicAssert.AreEqual(20.7, left);
            ClassicAssert.AreEqual(10.5, right);
        }

        [Test]
        public void Swap_WithStringValues_SwapsValues()
        {
            // Arrange
            var left = "left";
            var right = "right";

            // Act
            XMath.Swap(ref left, ref right);

            // Assert
            ClassicAssert.AreEqual("right", left);
            ClassicAssert.AreEqual("left", right);
        }
        #endregion

        #region XMath - Sqrt Tests
        [Test]
        public void Sqrt_WithValidValue_ReturnsSquareRoot()
        {
            // Arrange
            var value = 16.0f;

            // Act
            var result = XMath.Sqrt(value);

            // Assert
            ClassicAssert.AreEqual(4.0f, result);
        }

        [Test]
        public void Sqrt_WithZero_ReturnsZero()
        {
            // Arrange
            var value = 0.0f;

            // Act
            var result = XMath.Sqrt(value);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }
        #endregion

        #region XMath - InvSqrt Tests
        [Test]
        public void InvSqrt_Double_WithValidValue_ReturnsInverseSquareRoot()
        {
            // Arrange
            var value = 16.0;

            // Act
            var result = XMath.InvSqrt(value);

            // Assert
            ClassicAssert.AreEqual(0.25, result, 0.001);
        }

        [Test]
        public void InvSqrt_Double_WithZero_ReturnsOne()
        {
            // Arrange
            var value = 0.0;

            // Act
            var result = XMath.InvSqrt(value);

            // Assert
            ClassicAssert.AreEqual(1.0, result);
        }

        [Test]
        public void InvSqrt_Float_WithValidValue_ReturnsInverseSquareRoot()
        {
            // Arrange
            var value = 16.0f;

            // Act
            var result = XMath.InvSqrt(value);

            // Assert
            ClassicAssert.AreEqual(0.25f, result, 0.001f);
        }

        [Test]
        public void InvSqrt_Float_WithZero_ReturnsOne()
        {
            // Arrange
            var value = 0.0f;

            // Act
            var result = XMath.InvSqrt(value);

            // Assert
            ClassicAssert.AreEqual(1.0f, result);
        }
        #endregion

        #region XMath - Sin/Cos Tests
        [Test]
        public void Sin_WithZero_ReturnsZero()
        {
            // Arrange
            var radians = 0.0f;

            // Act
            var result = XMath.Sin(radians);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.001f);
        }

        [Test]
        public void Sin_WithPiOverTwo_ReturnsOne()
        {
            // Arrange
            var radians = XMath.PI_2_F;

            // Act
            var result = XMath.Sin(radians);

            // Assert
            ClassicAssert.AreEqual(1.0f, result, 0.001f);
        }

        [Test]
        public void Cos_WithZero_ReturnsOne()
        {
            // Arrange
            var radians = 0.0f;

            // Act
            var result = XMath.Cos(radians);

            // Assert
            ClassicAssert.AreEqual(1.0f, result, 0.001f);
        }

        [Test]
        public void Cos_WithPiOverTwo_ReturnsZero()
        {
            // Arrange
            var radians = XMath.PI_2_F;

            // Act
            var result = XMath.Cos(radians);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.001f);
        }
        #endregion

        #region XMath - ToPartFromPercent Tests
        [Test]
        public void ToPartFromPercent_WithValidPercent_ReturnsCorrectPart()
        {
            // Arrange
            var percent = 50;

            // Act
            var result = XMath.ToPartFromPercent(percent);

            // Assert
            ClassicAssert.AreEqual(0.5f, result);
        }

        [Test]
        public void ToPartFromPercent_WithZero_ReturnsZero()
        {
            // Arrange
            var percent = 0;

            // Act
            var result = XMath.ToPartFromPercent(percent);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }

        [Test]
        public void ToPartFromPercent_WithHundred_ReturnsOne()
        {
            // Arrange
            var percent = 100;

            // Act
            var result = XMath.ToPartFromPercent(percent);

            // Assert
            ClassicAssert.AreEqual(1.0f, result);
        }

        [Test]
        public void ToPartFromPercent_WithNegative_ReturnsZero()
        {
            // Arrange
            var percent = -10;

            // Act
            var result = XMath.ToPartFromPercent(percent);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }

        [Test]
        public void ToPartFromPercent_WithOverHundred_ReturnsOne()
        {
            // Arrange
            var percent = 150;

            // Act
            var result = XMath.ToPartFromPercent(percent);

            // Assert
            ClassicAssert.AreEqual(1.0f, result);
        }
        #endregion
    }
}
