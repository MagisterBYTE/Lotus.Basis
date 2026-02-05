using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DSizeTests
    {
        #region Size2D Tests
        [Test]
        public void Size2D_Constructor_WithWidthAndHeight_InitializesCorrectly()
        {
            // Arrange & Act
            var size = new Size2D(10.0, 20.0);

            // Assert
            ClassicAssert.AreEqual(10.0, size.Width);
            ClassicAssert.AreEqual(20.0, size.Height);
        }

        [Test]
        public void Size2D_IsEmpty_WithZeroSize_ReturnsTrue()
        {
            // Arrange
            var size = new Size2D(0.0, 0.0);

            // Act
            var result = size.IsEmpty;

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Size2D_IsEmpty_WithNonZeroSize_ReturnsFalse()
        {
            // Arrange
            var size = new Size2D(10.0, 20.0);

            // Act
            var result = size.IsEmpty;

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Size2D_Area_ReturnsCorrectArea()
        {
            // Arrange
            var size = new Size2D(10.0, 20.0);

            // Act
            var result = size.Area;

            // Assert
            ClassicAssert.AreEqual(200.0, result);
        }

        [Test]
        public void Size2D_Diagonal_ReturnsCorrectDiagonal()
        {
            // Arrange
            var size = new Size2D(3.0, 4.0);

            // Act
            var result = size.Diagonal;

            // Assert
            ClassicAssert.AreEqual(5.0, result, 0.001);
        }

        [Test]
        public void Size2D_AddOperator_ReturnsSum()
        {
            // Arrange
            var size1 = new Size2D(10.0, 20.0);
            var size2 = new Size2D(5.0, 15.0);

            // Act
            var result = size1 + size2;

            // Assert
            ClassicAssert.AreEqual(15.0, result.Width);
            ClassicAssert.AreEqual(35.0, result.Height);
        }

        [Test]
        public void Size2D_SubtractOperator_ReturnsDifference()
        {
            // Arrange
            var size1 = new Size2D(10.0, 20.0);
            var size2 = new Size2D(5.0, 15.0);

            // Act
            var result = size1 - size2;

            // Assert
            ClassicAssert.AreEqual(5.0, result.Width);
            ClassicAssert.AreEqual(5.0, result.Height);
        }

        [Test]
        public void Size2D_MultiplyOperator_ReturnsScaledSize()
        {
            // Arrange
            var size = new Size2D(10.0, 20.0);
            var scalar = 2.0;

            // Act
            var result = size * scalar;

            // Assert
            ClassicAssert.AreEqual(20.0, result.Width);
            ClassicAssert.AreEqual(40.0, result.Height);
        }

        [Test]
        public void Size2D_DivideOperator_ReturnsScaledSize()
        {
            // Arrange
            var size = new Size2D(10.0, 20.0);
            var scalar = 2.0;

            // Act
            var result = size / scalar;

            // Assert
            ClassicAssert.AreEqual(5.0, result.Width);
            ClassicAssert.AreEqual(10.0, result.Height);
        }

        [Test]
        public void Size2D_EqualityOperator_WithEqualSizes_ReturnsTrue()
        {
            // Arrange
            var size1 = new Size2D(10.0, 20.0);
            var size2 = new Size2D(10.0, 20.0);

            // Act
            var result = size1 == size2;

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Size2D_EqualityOperator_WithDifferentSizes_ReturnsFalse()
        {
            // Arrange
            var size1 = new Size2D(10.0, 20.0);
            var size2 = new Size2D(5.0, 15.0);

            // Act
            var result = size1 == size2;

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Size2D_StaticLerp_ReturnsInterpolatedSize()
        {
            // Arrange
            var from = new Size2D(10.0, 20.0);
            var to = new Size2D(30.0, 40.0);
            var time = 0.5;

            // Act
            var result = Size2D.Lerp(in from, in to, time);

            // Assert
            ClassicAssert.AreEqual(20.0, result.Width);
            ClassicAssert.AreEqual(30.0, result.Height);
        }

        [Test]
        public void Size2D_DeserializeFromString_ReturnsCorrectSize()
        {
            // Arrange
            var data = "10.5; 20.7";

            // Act
            var result = Size2D.DeserializeFromString(data);

            // Assert
            ClassicAssert.AreEqual(10.5, result.Width, 0.001);
            ClassicAssert.AreEqual(20.7, result.Height, 0.001);
        }

        [Test]
        public void Size2D_SerializeToString_ReturnsCorrectString()
        {
            // Arrange
            var size = new Size2D(10.5, 20.7);

            // Act
            var result = size.SerializeToString();

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.IsTrue(result.Contains("10.5") || result.Contains("10,5"));
            ClassicAssert.IsTrue(result.Contains("20.7") || result.Contains("20,7"));
        }

        [Test]
        public void Size2D_SetMaximize_SetsMaxComponents()
        {
            // Arrange
            var size = new Size2D(10.0, 20.0);
            var size1 = new Size2D(15.0, 15.0);
            var size2 = new Size2D(5.0, 25.0);

            // Act
            size.SetMaximize(in size1, in size2);

            // Assert
            ClassicAssert.AreEqual(15.0, size.Width);
            ClassicAssert.AreEqual(25.0, size.Height);
        }

        [Test]
        public void Size2D_SetMinimize_SetsMinComponents()
        {
            // Arrange
            var size = new Size2D(10.0, 20.0);
            var size1 = new Size2D(15.0, 15.0);
            var size2 = new Size2D(5.0, 25.0);

            // Act
            size.SetMinimize(in size1, in size2);

            // Assert
            ClassicAssert.AreEqual(5.0, size.Width);
            ClassicAssert.AreEqual(15.0, size.Height);
        }
        #endregion

        #region Size2Df Tests
        [Test]
        public void Size2Df_Constructor_WithWidthAndHeight_InitializesCorrectly()
        {
            // Arrange & Act
            var size = new Size2Df(10.0f, 20.0f);

            // Assert
            ClassicAssert.AreEqual(10.0f, size.Width);
            ClassicAssert.AreEqual(20.0f, size.Height);
        }

        [Test]
        public void Size2Df_IsEmpty_WithZeroSize_ReturnsTrue()
        {
            // Arrange
            var size = new Size2Df(0.0f, 0.0f);

            // Act
            var result = size.IsEmpty;

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Size2Df_Area_ReturnsCorrectArea()
        {
            // Arrange
            var size = new Size2Df(10.0f, 20.0f);

            // Act
            var result = size.Area;

            // Assert
            ClassicAssert.AreEqual(200.0f, result);
        }

        [Test]
        public void Size2Df_Diagonal_ReturnsCorrectDiagonal()
        {
            // Arrange
            var size = new Size2Df(3.0f, 4.0f);

            // Act
            var result = size.Diagonal;

            // Assert
            ClassicAssert.AreEqual(5.0f, result, 0.001f);
        }

        [Test]
        public void Size2Df_AddOperator_ReturnsSum()
        {
            // Arrange
            var size1 = new Size2Df(10.0f, 20.0f);
            var size2 = new Size2Df(5.0f, 15.0f);

            // Act
            var result = size1 + size2;

            // Assert
            ClassicAssert.AreEqual(15.0f, result.Width);
            ClassicAssert.AreEqual(35.0f, result.Height);
        }

        [Test]
        public void Size2Df_StaticLerp_ReturnsInterpolatedSize()
        {
            // Arrange
            var from = new Size2Df(10.0f, 20.0f);
            var to = new Size2Df(30.0f, 40.0f);
            var time = 0.5f;

            // Act
            var result = Size2Df.Lerp(in from, in to, time);

            // Assert
            ClassicAssert.AreEqual(20.0f, result.Width);
            ClassicAssert.AreEqual(30.0f, result.Height);
        }

        [Test]
        public void Size2Df_DeserializeFromString_ReturnsCorrectSize()
        {
            // Arrange
            var data = "10.5; 20.7";

            // Act
            var result = Size2Df.DeserializeFromString(data);

            // Assert
            ClassicAssert.AreEqual(10.5f, result.Width, 0.001f);
            ClassicAssert.AreEqual(20.7f, result.Height, 0.001f);
        }
        #endregion
    }
}
