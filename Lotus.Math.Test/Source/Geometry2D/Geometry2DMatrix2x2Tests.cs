using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DMatrix2x2Tests
    {
        #region Matrix2Dx2 Tests
        [Test]
        public void Matrix2Dx2_Determinat_WithIdentityMatrix_ReturnsOne()
        {
            // Arrange
            var a1 = 1.0;
            var a2 = 0.0;
            var b1 = 0.0;
            var b2 = 1.0;

            // Act
            var result = Matrix2Dx2.Determinat(a1, a2, b1, b2);

            // Assert
            ClassicAssert.AreEqual(1.0, result);
        }

        [Test]
        public void Matrix2Dx2_Determinat_WithSimpleMatrix_ReturnsCorrectDeterminant()
        {
            // Arrange
            var a1 = 2.0;
            var a2 = 3.0;
            var b1 = 4.0;
            var b2 = 5.0;

            // Act
            var result = Matrix2Dx2.Determinat(a1, a2, b1, b2);

            // Assert
            ClassicAssert.AreEqual(-2.0, result);
        }

        [Test]
        public void Matrix2Dx2_Determinat_WithZeroDeterminant_ReturnsZero()
        {
            // Arrange
            var a1 = 1.0;
            var a2 = 2.0;
            var b1 = 2.0;
            var b2 = 4.0;

            // Act
            var result = Matrix2Dx2.Determinat(a1, a2, b1, b2);

            // Assert
            ClassicAssert.AreEqual(0.0, result);
        }
        #endregion

        #region Matrix2Dx2f Tests
        [Test]
        public void Matrix2Dx2f_Determinat_WithIdentityMatrix_ReturnsOne()
        {
            // Arrange
            var a1 = 1.0f;
            var a2 = 0.0f;
            var b1 = 0.0f;
            var b2 = 1.0f;

            // Act
            var result = Matrix2Dx2f.Determinat(a1, a2, b1, b2);

            // Assert
            ClassicAssert.AreEqual(1.0f, result);
        }

        [Test]
        public void Matrix2Dx2f_Determinat_WithSimpleMatrix_ReturnsCorrectDeterminant()
        {
            // Arrange
            var a1 = 2.0f;
            var a2 = 3.0f;
            var b1 = 4.0f;
            var b2 = 5.0f;

            // Act
            var result = Matrix2Dx2f.Determinat(a1, a2, b1, b2);

            // Assert
            ClassicAssert.AreEqual(-2.0f, result);
        }

        [Test]
        public void Matrix2Dx2f_Determinat_WithZeroDeterminant_ReturnsZero()
        {
            // Arrange
            var a1 = 1.0f;
            var a2 = 2.0f;
            var b1 = 2.0f;
            var b2 = 4.0f;

            // Act
            var result = Matrix2Dx2f.Determinat(a1, a2, b1, b2);

            // Assert
            ClassicAssert.AreEqual(0.0f, result);
        }
        #endregion
    }
}
