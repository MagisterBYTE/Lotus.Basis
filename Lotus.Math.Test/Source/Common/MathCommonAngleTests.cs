using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathCommonAngleTests
    {
        #region NormalizationFull Tests
        [Test]
        public void NormalizationFull_Double_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var angle = 20.0;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(20.0, result);
        }

        [Test]
        public void NormalizationFull_Double_WithValueAtBoundary_ReturnsZero()
        {
            // Arrange
            var angle = 360.0;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(0.0, result);
        }

        [Test]
        public void NormalizationFull_Double_WithValueOver360_ReturnsNormalized()
        {
            // Arrange
            var angle = 361.0;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(1.0, result);
        }

        [Test]
        public void NormalizationFull_Double_WithNegativeValue_ReturnsNormalized()
        {
            // Arrange
            var angle = -2.0;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(358.0, result);
        }

        [Test]
        public void NormalizationFull_Double_WithNegative180_Returns180()
        {
            // Arrange
            var angle = -180.0;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(180.0, result);
        }

        [Test]
        public void NormalizationFull_Double_WithLargeValue_ReturnsNormalized()
        {
            // Arrange
            var angle = 720.0;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(0.0, result);
        }

        [Test]
        public void NormalizationFull_Float_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var angle = 20.0f;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(20.0f, result);
        }

        [Test]
        public void NormalizationFull_Float_WithValueAtBoundary_ReturnsZero()
        {
            // Arrange
            var angle = 360.0f;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }

        [Test]
        public void NormalizationFull_Float_WithValueOver360_ReturnsNormalized()
        {
            // Arrange
            var angle = 361.0f;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(1.0f, result);
        }

        [Test]
        public void NormalizationFull_Float_WithNegativeValue_ReturnsNormalized()
        {
            // Arrange
            var angle = -2.0f;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(358.0f, result);
        }

        [Test]
        public void NormalizationFull_Float_WithNegative180_Returns180()
        {
            // Arrange
            var angle = -180.0f;

            // Act
            var result = XMathAngle.NormalizationFull(angle);

            // Assert
            ClassicAssert.AreEqual(180.0f, result);
        }
        #endregion

        #region NormalizationHalf Tests
        [Test]
        public void NormalizationHalf_Double_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var angle = 20.0;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(20.0, result);
        }

        [Test]
        public void NormalizationHalf_Double_WithValueAt360_ReturnsZero()
        {
            // Arrange
            var angle = 360.0;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(0.0, result);
        }

        [Test]
        public void NormalizationHalf_Double_WithValueOver360_ReturnsNormalized()
        {
            // Arrange
            var angle = 361.0;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(1.0, result);
        }

        [Test]
        public void NormalizationHalf_Double_WithNegativeValue_ReturnsNegative()
        {
            // Arrange
            var angle = -2.0;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(-2.0, result);
        }

        [Test]
        public void NormalizationHalf_Double_WithNegative180_Returns180()
        {
            // Arrange
            var angle = -180.0;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(180.0, result);
        }

        [Test]
        public void NormalizationHalf_Double_With270_ReturnsNegative90()
        {
            // Arrange
            var angle = 270.0;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(-90.0, result);
        }

        [Test]
        public void NormalizationHalf_Float_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var angle = 20.0f;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(20.0f, result);
        }

        [Test]
        public void NormalizationHalf_Float_WithValueAt360_ReturnsZero()
        {
            // Arrange
            var angle = 360.0f;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }

        [Test]
        public void NormalizationHalf_Float_WithValueOver360_ReturnsNormalized()
        {
            // Arrange
            var angle = 361.0f;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(1.0f, result);
        }

        [Test]
        public void NormalizationHalf_Float_WithNegativeValue_ReturnsNegative()
        {
            // Arrange
            var angle = -2.0f;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(-2.0f, result);
        }

        [Test]
        public void NormalizationHalf_Float_WithNegative180_Returns180()
        {
            // Arrange
            var angle = -180.0f;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(180.0f, result);
        }

        [Test]
        public void NormalizationHalf_Float_With270_ReturnsNegative90()
        {
            // Arrange
            var angle = 270.0f;

            // Act
            var result = XMathAngle.NormalizationHalf(angle);

            // Assert
            ClassicAssert.AreEqual(-90.0f, result);
        }
        #endregion

        #region Clamp Tests
        [Test]
        public void Clamp_Double_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var angle = 45.0;
            var min = 0.0;
            var max = 90.0;

            // Act
            var result = XMathAngle.Clamp(angle, min, max);

            // Assert
            ClassicAssert.AreEqual(45.0, result);
        }

        [Test]
        public void Clamp_Double_WithValueAboveMax_ReturnsMax()
        {
            // Arrange
            var angle = 100.0;
            var min = 0.0;
            var max = 90.0;

            // Act
            var result = XMathAngle.Clamp(angle, min, max);

            // Assert
            ClassicAssert.AreEqual(90.0, result);
        }

        [Test]
        public void Clamp_Double_WithValueBelowMin_ReturnsMin()
        {
            // Arrange
            var angle = -10.0;
            var min = 0.0;
            var max = 90.0;

            // Act
            var result = XMathAngle.Clamp(angle, min, max);

            // Assert
            ClassicAssert.AreEqual(0.0, result);
        }

        [Test]
        public void Clamp_Double_WithLargeAngle_ReturnsClamped()
        {
            // Arrange
            var angle = 450.0;
            var min = 0.0;
            var max = 90.0;

            // Act
            var result = XMathAngle.Clamp(angle, min, max);

            // Assert
            ClassicAssert.AreEqual(90.0, result);
        }

        [Test]
        public void Clamp_Float_WithValueInRange_ReturnsSameValue()
        {
            // Arrange
            var angle = 45.0f;
            var min = 0.0f;
            var max = 90.0f;

            // Act
            var result = XMathAngle.Clamp(angle, min, max);

            // Assert
            ClassicAssert.AreEqual(45.0f, result);
        }

        [Test]
        public void Clamp_Float_WithValueAboveMax_ReturnsMax()
        {
            // Arrange
            var angle = 100.0f;
            var min = 0.0f;
            var max = 90.0f;

            // Act
            var result = XMathAngle.Clamp(angle, min, max);

            // Assert
            ClassicAssert.AreEqual(90.0f, result);
        }
        #endregion

        #region RevolutionsToDegrees Tests
        [Test]
        public void RevolutionsToDegrees_Double_WithOneRevolution_Returns360()
        {
            // Arrange
            var revolution = 1.0;

            // Act
            var result = XMathAngle.RevolutionsToDegrees(revolution);

            // Assert
            ClassicAssert.AreEqual(360.0, result);
        }

        [Test]
        public void RevolutionsToDegrees_Double_WithHalfRevolution_Returns180()
        {
            // Arrange
            var revolution = 0.5;

            // Act
            var result = XMathAngle.RevolutionsToDegrees(revolution);

            // Assert
            ClassicAssert.AreEqual(180.0, result);
        }

        [Test]
        public void RevolutionsToDegrees_Float_WithOneRevolution_Returns360()
        {
            // Arrange
            var revolution = 1.0f;

            // Act
            var result = XMathAngle.RevolutionsToDegrees(revolution);

            // Assert
            ClassicAssert.AreEqual(360.0f, result);
        }
        #endregion

        #region RevolutionsToRadians Tests
        [Test]
        public void RevolutionsToRadians_Double_WithOneRevolution_Returns2Pi()
        {
            // Arrange
            var revolution = 1.0;

            // Act
            var result = XMathAngle.RevolutionsToRadians(revolution);

            // Assert
            ClassicAssert.AreEqual(XMath.PI2_D, result, 0.001);
        }

        [Test]
        public void RevolutionsToRadians_Float_WithOneRevolution_Returns2Pi()
        {
            // Arrange
            var revolution = 1.0f;

            // Act
            var result = XMathAngle.RevolutionsToRadians(revolution);

            // Assert
            ClassicAssert.AreEqual(XMath.PI2_F, result, 0.001f);
        }
        #endregion

        #region RevolutionsToGradians Tests
        [Test]
        public void RevolutionsToGradians_Double_WithOneRevolution_Returns400()
        {
            // Arrange
            var revolution = 1.0;

            // Act
            var result = XMathAngle.RevolutionsToGradians(revolution);

            // Assert
            ClassicAssert.AreEqual(400.0, result);
        }

        [Test]
        public void RevolutionsToGradians_Float_WithOneRevolution_Returns400()
        {
            // Arrange
            var revolution = 1.0f;

            // Act
            var result = XMathAngle.RevolutionsToGradians(revolution);

            // Assert
            ClassicAssert.AreEqual(400.0f, result);
        }
        #endregion
    }
}
