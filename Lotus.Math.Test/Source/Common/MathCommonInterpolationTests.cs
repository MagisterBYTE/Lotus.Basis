using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathCommonInterpolationTests
    {
        #region Lerp Tests
        [Test]
        public void Lerp_Double_WithTimeZero_ReturnsStart()
        {
            // Arrange
            var start = 0.0;
            var end = 100.0;
            var time = 0.0;

            // Act
            var result = XMathInterpolation.Lerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(0.0, result);
        }

        [Test]
        public void Lerp_Double_WithTimeOne_ReturnsEnd()
        {
            // Arrange
            var start = 0.0;
            var end = 100.0;
            var time = 1.0;

            // Act
            var result = XMathInterpolation.Lerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(100.0, result);
        }

        [Test]
        public void Lerp_Double_WithTimeHalf_ReturnsMiddle()
        {
            // Arrange
            var start = 0.0;
            var end = 100.0;
            var time = 0.5;

            // Act
            var result = XMathInterpolation.Lerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(50.0, result);
        }

        [Test]
        public void Lerp_Float_WithTimeZero_ReturnsStart()
        {
            // Arrange
            var start = 0.0f;
            var end = 100.0f;
            var time = 0.0f;

            // Act
            var result = XMathInterpolation.Lerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }

        [Test]
        public void Lerp_Float_WithTimeOne_ReturnsEnd()
        {
            // Arrange
            var start = 0.0f;
            var end = 100.0f;
            var time = 1.0f;

            // Act
            var result = XMathInterpolation.Lerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(100.0f, result);
        }

        [Test]
        public void Lerp_Float_WithTimeHalf_ReturnsMiddle()
        {
            // Arrange
            var start = 0.0f;
            var end = 100.0f;
            var time = 0.5f;

            // Act
            var result = XMathInterpolation.Lerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(50.0f, result);
        }

        [Test]
        public void Lerp_Byte_WithTimeZero_ReturnsStart()
        {
            // Arrange
            byte start = 0;
            byte end = 100;
            var time = 0.0f;

            // Act
            var result = XMathInterpolation.Lerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(0, result);
        }

        [Test]
        public void Lerp_Byte_WithTimeOne_ReturnsEnd()
        {
            // Arrange
            byte start = 0;
            byte end = 100;
            var time = 1.0f;

            // Act
            var result = XMathInterpolation.Lerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(100, result);
        }
        #endregion

        #region Hermite Tests
        [Test]
        public void Hermite_Double_WithTimeZero_ReturnsStart()
        {
            // Arrange
            var start = 0.0;
            var end = 100.0;
            var time = 0.0;

            // Act
            var result = XMathInterpolation.Hermite(start, end, time);

            // Assert
            ClassicAssert.AreEqual(0.0, result);
        }

        [Test]
        public void Hermite_Double_WithTimeOne_ReturnsStart()
        {
            // Arrange
            var start = 0.0;
            var end = 100.0;
            var time = 0.0;

            // Act
            var result = XMathInterpolation.Hermite(start, end, time);

            // Assert
            ClassicAssert.AreEqual(0.0, result);
        }

        [Test]
        public void Hermite_Float_WithTimeZero_ReturnsStart()
        {
            // Arrange
            var start = 0.0f;
            var end = 100.0f;
            var time = 0.0f;

            // Act
            var result = XMathInterpolation.Hermite(start, end, time);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }
        #endregion

        #region Sinerp Tests
        [Test]
        public void Sinerp_Double_WithTimeZero_ReturnsStart()
        {
            // Arrange
            var start = 0.0;
            var end = 100.0;
            var time = 0.0;

            // Act
            var result = XMathInterpolation.Sinerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(0.0, result, 0.001);
        }

        [Test]
        public void Sinerp_Double_WithTimeOne_ReturnsEnd()
        {
            // Arrange
            var start = 0.0;
            var end = 100.0;
            var time = 1.0;

            // Act
            var result = XMathInterpolation.Sinerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(100.0, result, 0.001);
        }

        [Test]
        public void Sinerp_Float_WithTimeZero_ReturnsStart()
        {
            // Arrange
            var start = 0.0f;
            var end = 100.0f;
            var time = 0.0f;

            // Act
            var result = XMathInterpolation.Sinerp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.001f);
        }
        #endregion

        #region Coserp Tests
        [Test]
        public void Coserp_Double_WithTimeZero_ReturnsStart()
        {
            // Arrange
            var start = 0.0;
            var end = 100.0;
            var time = 0.0;

            // Act
            var result = XMathInterpolation.Coserp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(0.0, result, 0.001);
        }

        [Test]
        public void Coserp_Double_WithTimeOne_ReturnsEnd()
        {
            // Arrange
            var start = 0.0;
            var end = 100.0;
            var time = 1.0;

            // Act
            var result = XMathInterpolation.Coserp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(100.0, result, 0.001);
        }

        [Test]
        public void Coserp_Float_WithTimeZero_ReturnsStart()
        {
            // Arrange
            var start = 0.0f;
            var end = 100.0f;
            var time = 0.0f;

            // Act
            var result = XMathInterpolation.Coserp(start, end, time);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.001f);
        }
        #endregion

        #region SmoothStep Tests
        [Test]
        public void SmoothStep_Float_WithZero_ReturnsZero()
        {
            // Arrange
            var amount = 0.0f;

            // Act
            var result = XMathInterpolation.SmoothStep(amount);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }

        [Test]
        public void SmoothStep_Float_WithOne_ReturnsOne()
        {
            // Arrange
            var amount = 1.0f;

            // Act
            var result = XMathInterpolation.SmoothStep(amount);

            // Assert
            ClassicAssert.AreEqual(1.0f, result);
        }

        [Test]
        public void SmoothStep_Float_WithHalf_ReturnsSmoothValue()
        {
            // Arrange
            var amount = 0.5f;

            // Act
            var result = XMathInterpolation.SmoothStep(amount);

            // Assert
            ClassicAssert.Greater(result, 0.0f);
            ClassicAssert.Less(result, 1.0f);
        }

        [Test]
        public void SmoothStep_Double_WithValueInRange_ReturnsSmoothValue()
        {
            // Arrange
            var start = 0.0;
            var end = 100.0;
            var value = 50.0;

            // Act
            var result = XMathInterpolation.SmoothStep(start, end, value);

            // Assert
            ClassicAssert.Greater(result, 0.0);
            ClassicAssert.Less(result, 1.0);
        }

        [Test]
        public void SmoothStep_Float_WithValueInRange_ReturnsSmoothValue()
        {
            // Arrange
            var start = 0.0f;
            var end = 100.0f;
            var value = 50.0f;

            // Act
            var result = XMathInterpolation.SmoothStep(start, end, value);

            // Assert
            ClassicAssert.Greater(result, 0.0f);
            ClassicAssert.Less(result, 1.0f);
        }
        #endregion

        #region SmootherStep Tests
        [Test]
        public void SmootherStep_WithZero_ReturnsZero()
        {
            // Arrange
            var amount = 0.0f;

            // Act
            var result = XMathInterpolation.SmootherStep(amount);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }

        [Test]
        public void SmootherStep_WithOne_ReturnsOne()
        {
            // Arrange
            var amount = 1.0f;

            // Act
            var result = XMathInterpolation.SmootherStep(amount);

            // Assert
            ClassicAssert.AreEqual(1.0f, result);
        }

        [Test]
        public void SmootherStep_WithHalf_ReturnsSmoothValue()
        {
            // Arrange
            var amount = 0.5f;

            // Act
            var result = XMathInterpolation.SmootherStep(amount);

            // Assert
            ClassicAssert.Greater(result, 0.0f);
            ClassicAssert.Less(result, 1.0f);
        }
        #endregion

        #region Gauss Tests
        [Test]
        public void Gauss_Double_WithCenterPoint_ReturnsAmplitude()
        {
            // Arrange
            var amplitude = 1.0;
            var x = 0.0;
            var y = 0.0;
            var centerX = 0.0;
            var centerY = 0.0;
            var sigmaX = 1.0;
            var sigmaY = 1.0;

            // Act
            var result = XMathInterpolation.Gauss(amplitude, x, y, centerX, centerY, sigmaX, sigmaY);

            // Assert
            ClassicAssert.AreEqual(amplitude, result, 0.001);
        }

        [Test]
        public void Gauss_Double_WithOffsetPoint_ReturnsSmallerValue()
        {
            // Arrange
            var amplitude = 1.0;
            var x = 2.0;
            var y = 2.0;
            var centerX = 0.0;
            var centerY = 0.0;
            var sigmaX = 1.0;
            var sigmaY = 1.0;

            // Act
            var result = XMathInterpolation.Gauss(amplitude, x, y, centerX, centerY, sigmaX, sigmaY);

            // Assert
            ClassicAssert.Less(result, amplitude);
            ClassicAssert.Greater(result, 0.0);
        }

        [Test]
        public void Gauss_Float_WithCenterPoint_ReturnsAmplitude()
        {
            // Arrange
            var amplitude = 1.0f;
            var x = 0.0f;
            var y = 0.0f;
            var centerX = 0.0f;
            var centerY = 0.0f;
            var sigmaX = 1.0f;
            var sigmaY = 1.0f;

            // Act
            var result = XMathInterpolation.Gauss(amplitude, x, y, centerX, centerY, sigmaX, sigmaY);

            // Assert
            ClassicAssert.AreEqual(amplitude, result, 0.001f);
        }

        [Test]
        public void Gauss_Float_WithOffsetPoint_ReturnsSmallerValue()
        {
            // Arrange
            var amplitude = 1.0f;
            var x = 2.0f;
            var y = 2.0f;
            var centerX = 0.0f;
            var centerY = 0.0f;
            var sigmaX = 1.0f;
            var sigmaY = 1.0f;

            // Act
            var result = XMathInterpolation.Gauss(amplitude, x, y, centerX, centerY, sigmaX, sigmaY);

            // Assert
            ClassicAssert.Less(result, amplitude);
            ClassicAssert.Greater(result, 0.0f);
        }
        #endregion
    }
}
