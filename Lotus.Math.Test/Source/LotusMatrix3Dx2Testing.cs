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
    public class Matrix3Dx2fTests
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
    }
}