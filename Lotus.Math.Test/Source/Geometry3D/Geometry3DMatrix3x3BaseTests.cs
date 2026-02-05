#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using System.Drawing;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DMatrix3x3BaseTests
    {
        [Test]
        public void Add_AddsMatricesCorrectly()
        {
            // Arrange
            var matrix1 = new Matrix3Dx2f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f);
            var matrix2 = new Matrix3Dx2f(2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f);

            // Act
            var result = Matrix3Dx2f.Add(matrix1, matrix2);

            // ClassicAssert
            ClassicAssert.AreEqual(new Matrix3Dx2f(3.0f, 5.0f, 7.0f, 9.0f, 11.0f, 13.0f), result);
        }

        [Test]
        public void Subtract_SubtractsMatricesCorrectly()
        {
            // Arrange
            var matrix1 = new Matrix3Dx2f(5.0f, 6.0f, 7.0f, 8.0f, 9.0f, 10.0f);
            var matrix2 = new Matrix3Dx2f(2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f);

            // Act
            var result = Matrix3Dx2f.Subtract(matrix1, matrix2);

            // ClassicAssert
            ClassicAssert.AreEqual(new Matrix3Dx2f(3.0f, 3.0f, 3.0f, 3.0f, 3.0f, 3.0f), result);
        }

        [Test]
        public void Multiply_MultipliesMatricesCorrectly()
        {
            // Arrange
            var matrix1 = new Matrix3Dx2f(2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f);
            var matrix2 = new Matrix3Dx2f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f);

            // Act
            var result = Matrix3Dx2f.Multiply(matrix1, matrix2);

            // ClassicAssert
            ClassicAssert.AreEqual(new Matrix3Dx2f(11.0f, 16.0f, 19.0f, 28.0f, 32.0f, 46.0f), result);
        }

        [Test]
        public void Invert_ReturnsInverseMatrix()
        {
            // Arrange
            var matrix = new Matrix3Dx2f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f);

            // Act
            var invertedMatrix = Matrix3Dx2f.Invert(matrix);

            // ClassicAssert
            var identityMatrix = Matrix3Dx2f.Multiply(matrix, invertedMatrix);
            ClassicAssert.That(identityMatrix.IsIdentity);
        }

        [Test]
        public void Constructor_CreatesMatrixWithGivenValues()
        {
            var matrix = new Matrix3Dx2f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f);

            ClassicAssert.AreEqual(1.0f, matrix.M11);
            ClassicAssert.AreEqual(2.0f, matrix.M12);
            ClassicAssert.AreEqual(3.0f, matrix.M21);
            ClassicAssert.AreEqual(4.0f, matrix.M22);
            ClassicAssert.AreEqual(5.0f, matrix.M31);
            ClassicAssert.AreEqual(6.0f, matrix.M32);
        }

        [Test]
        public void Identity_ReturnsIdentityMatrix()
        {
            var identityMatrix = Matrix3Dx2f.Identity;

            ClassicAssert.IsTrue(identityMatrix.IsIdentity);
            ClassicAssert.AreEqual(1.0f, identityMatrix.M11);
            ClassicAssert.AreEqual(0.0f, identityMatrix.M12);
            ClassicAssert.AreEqual(0.0f, identityMatrix.M21);
            ClassicAssert.AreEqual(1.0f, identityMatrix.M22);
            ClassicAssert.AreEqual(0.0f, identityMatrix.M31);
            ClassicAssert.AreEqual(0.0f, identityMatrix.M32);
        }

        [Test]
        public void Transform_Point_ReturnsTransformedPoint()
        {
            var matrix = new Matrix3Dx2f(1.0f, 0.0f, 0.0f, 1.0f, 10.0f, 20.0f);
            var point = new Vector2Df(5.0f, 5.0f);

            var transformedPoint = Matrix3Dx2f.TransformPoint(in matrix, in point);

            ClassicAssert.AreEqual(15.0f, transformedPoint.X);
            ClassicAssert.AreEqual(25.0f, transformedPoint.Y);
        }

        [Test]
        public void Matrix3Dx2f_MultiplyByScalar_ReturnsScaledMatrix()
        {
            // Arrange
            var matrix = new Matrix3Dx2f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f);
            var scalar = 2.0f;

            // Act
            var result = Matrix3Dx2f.Multiply(in matrix, scalar);

            // Assert
            ClassicAssert.AreEqual(2.0f, result.M11);
            ClassicAssert.AreEqual(4.0f, result.M12);
            ClassicAssert.AreEqual(6.0f, result.M21);
            ClassicAssert.AreEqual(8.0f, result.M22);
            ClassicAssert.AreEqual(10.0f, result.M31);
            ClassicAssert.AreEqual(12.0f, result.M32);
        }

        [Test]
        public void Matrix3Dx2f_DivideByScalar_ReturnsScaledMatrix()
        {
            // Arrange
            var matrix = new Matrix3Dx2f(2.0f, 4.0f, 6.0f, 8.0f, 10.0f, 12.0f);
            var scalar = 2.0f;

            // Act
            Matrix3Dx2f.Divide(in matrix, scalar, out var result);

            // Assert
            ClassicAssert.AreEqual(1.0f, result.M11);
            ClassicAssert.AreEqual(2.0f, result.M12);
            ClassicAssert.AreEqual(3.0f, result.M21);
            ClassicAssert.AreEqual(4.0f, result.M22);
            ClassicAssert.AreEqual(5.0f, result.M31);
            ClassicAssert.AreEqual(6.0f, result.M32);
        }

        [Test]
        public void Matrix3Dx2f_Negate_ReturnsNegatedMatrix()
        {
            // Arrange
            var matrix = new Matrix3Dx2f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f);

            // Act
            var result = Matrix3Dx2f.Negate(in matrix);

            // Assert
            ClassicAssert.AreEqual(-1.0f, result.M11);
            ClassicAssert.AreEqual(-2.0f, result.M12);
            ClassicAssert.AreEqual(-3.0f, result.M21);
            ClassicAssert.AreEqual(-4.0f, result.M22);
            ClassicAssert.AreEqual(-5.0f, result.M31);
            ClassicAssert.AreEqual(-6.0f, result.M32);
        }

        [Test]
        public void Matrix3Dx2f_Lerp_ReturnsInterpolatedMatrix()
        {
            // Arrange
            var start = new Matrix3Dx2f(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
            var end = new Matrix3Dx2f(10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 10.0f);
            var time = 0.5f;

            // Act
            var result = Matrix3Dx2f.Lerp(in start, in end, time);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.M11);
            ClassicAssert.AreEqual(5.0f, result.M12);
            ClassicAssert.AreEqual(5.0f, result.M21);
            ClassicAssert.AreEqual(5.0f, result.M22);
            ClassicAssert.AreEqual(5.0f, result.M31);
            ClassicAssert.AreEqual(5.0f, result.M32);
        }

        [Test]
        public void Matrix3Dx2f_Scaling_WithVector_ReturnsScalingMatrix()
        {
            // Arrange
            var scale = new Vector2Df(2.0f, 3.0f);

            // Act
            var result = Matrix3Dx2f.Scaling(in scale);

            // Assert
            ClassicAssert.AreEqual(2.0f, result.M11);
            ClassicAssert.AreEqual(0.0f, result.M12);
            ClassicAssert.AreEqual(0.0f, result.M21);
            ClassicAssert.AreEqual(3.0f, result.M22);
            ClassicAssert.AreEqual(0.0f, result.M31);
            ClassicAssert.AreEqual(0.0f, result.M32);
        }

        [Test]
        public void Matrix3Dx2f_Scaling_WithXY_ReturnsScalingMatrix()
        {
            // Arrange
            var x = 2.0f;
            var y = 3.0f;

            // Act
            var result = Matrix3Dx2f.Scaling(x, y);

            // Assert
            ClassicAssert.AreEqual(2.0f, result.M11);
            ClassicAssert.AreEqual(0.0f, result.M12);
            ClassicAssert.AreEqual(0.0f, result.M21);
            ClassicAssert.AreEqual(3.0f, result.M22);
        }

        [Test]
        public void Matrix3Dx2f_Scaling_WithUniformScale_ReturnsScalingMatrix()
        {
            // Arrange
            var scale = 2.0f;

            // Act
            var result = Matrix3Dx2f.Scaling(scale);

            // Assert
            ClassicAssert.AreEqual(2.0f, result.M11);
            ClassicAssert.AreEqual(2.0f, result.M22);
        }

        [Test]
        public void Matrix3Dx2f_Rotation_WithAngle_ReturnsRotationMatrix()
        {
            // Arrange
            var angle = 90.0f;

            // Act
            var result = Matrix3Dx2f.Rotation(XMath.DegreeToRadian_F * angle);

            // Assert
            // Для 90 градусов: cos(90) ≈ 0, sin(90) ≈ 1
            ClassicAssert.AreEqual(0.0f, result.M11, 0.1f);
            ClassicAssert.AreEqual(1.0f, result.M12, 0.1f);
            ClassicAssert.AreEqual(-1.0f, result.M21, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.M22, 0.1f);
        }

        [Test]
        public void Matrix3Dx2f_Translation_WithVector_ReturnsTranslationMatrix()
        {
            // Arrange
            var translation = new Vector2Df(10.0f, 20.0f);

            // Act
            var result = Matrix3Dx2f.Translation(in translation);

            // Assert
            ClassicAssert.AreEqual(1.0f, result.M11);
            ClassicAssert.AreEqual(0.0f, result.M12);
            ClassicAssert.AreEqual(0.0f, result.M21);
            ClassicAssert.AreEqual(1.0f, result.M22);
            ClassicAssert.AreEqual(10.0f, result.M31);
            ClassicAssert.AreEqual(20.0f, result.M32);
        }

        [Test]
        public void Matrix3Dx2f_Translation_WithXY_ReturnsTranslationMatrix()
        {
            // Arrange
            var x = 10.0f;
            var y = 20.0f;

            // Act
            var result = Matrix3Dx2f.Translation(x, y);

            // Assert
            ClassicAssert.AreEqual(1.0f, result.M11);
            ClassicAssert.AreEqual(0.0f, result.M12);
            ClassicAssert.AreEqual(0.0f, result.M21);
            ClassicAssert.AreEqual(1.0f, result.M22);
            ClassicAssert.AreEqual(10.0f, result.M31);
            ClassicAssert.AreEqual(20.0f, result.M32);
        }

        [Test]
        public void Matrix3Dx2f_TransformVector_ReturnsTransformedVector()
        {
            // Arrange
            var matrix = new Matrix3Dx2f(2.0f, 0.0f, 0.0f, 3.0f, 10.0f, 20.0f);
            var vector = new Vector2Df(5.0f, 5.0f);

            // Act
            var result = Matrix3Dx2f.TransformVector(in matrix, in vector);

            // Assert
            // TransformVector не применяет трансляцию
            ClassicAssert.AreEqual(10.0f, result.X);
            ClassicAssert.AreEqual(15.0f, result.Y);
        }

        [Test]
        public void Matrix3Dx2f_EqualityOperator_WithEqualMatrices_ReturnsTrue()
        {
            // Arrange
            var matrix1 = new Matrix3Dx2f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f);
            var matrix2 = new Matrix3Dx2f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f);

            // Act
            var result = matrix1 == matrix2;

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Matrix3Dx2f_EqualityOperator_WithDifferentMatrices_ReturnsFalse()
        {
            // Arrange
            var matrix1 = new Matrix3Dx2f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f);
            var matrix2 = new Matrix3Dx2f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 7.0f);

            // Act
            var result = matrix1 == matrix2;

            // Assert
            ClassicAssert.IsFalse(result);
        }
    }
}