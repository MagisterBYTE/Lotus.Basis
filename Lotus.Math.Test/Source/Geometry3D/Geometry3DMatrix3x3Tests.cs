using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DMatrix3x3Tests
    {
        private const double EpsilonD = 0.00001;
        private const float EpsilonF = 0.00001f;

        #region Matrix3Dx3 Tests
        [Test]
        public void Matrix3Dx3_Constructor_InitializesCorrectly()
        {
            var matrix = new Matrix3Dx3(1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0);
            ClassicAssert.AreEqual(1.0, matrix.M11, EpsilonD);
            ClassicAssert.AreEqual(2.0, matrix.M12, EpsilonD);
            ClassicAssert.AreEqual(3.0, matrix.M13, EpsilonD);
            ClassicAssert.AreEqual(4.0, matrix.M21, EpsilonD);
            ClassicAssert.AreEqual(5.0, matrix.M22, EpsilonD);
            ClassicAssert.AreEqual(6.0, matrix.M23, EpsilonD);
            ClassicAssert.AreEqual(7.0, matrix.M31, EpsilonD);
            ClassicAssert.AreEqual(8.0, matrix.M32, EpsilonD);
            ClassicAssert.AreEqual(9.0, matrix.M33, EpsilonD);
        }

        [Test]
        public void Matrix3Dx3_CopyConstructor_InitializesCorrectly()
        {
            var original = new Matrix3Dx3(1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0);
            var copy = new Matrix3Dx3(original);
            ClassicAssert.AreEqual(original.M11, copy.M11, EpsilonD);
            ClassicAssert.AreEqual(original.M22, copy.M22, EpsilonD);
            ClassicAssert.AreEqual(original.M33, copy.M33, EpsilonD);
        }

        [Test]
        public void Matrix3Dx3_Identity_IsCorrect()
        {
            var identity = Matrix3Dx3.Identity;
            ClassicAssert.AreEqual(1.0, identity.M11, EpsilonD);
            ClassicAssert.AreEqual(0.0, identity.M12, EpsilonD);
            ClassicAssert.AreEqual(0.0, identity.M13, EpsilonD);
            ClassicAssert.AreEqual(0.0, identity.M21, EpsilonD);
            ClassicAssert.AreEqual(1.0, identity.M22, EpsilonD);
            ClassicAssert.AreEqual(0.0, identity.M23, EpsilonD);
            ClassicAssert.AreEqual(0.0, identity.M31, EpsilonD);
            ClassicAssert.AreEqual(0.0, identity.M32, EpsilonD);
            ClassicAssert.AreEqual(1.0, identity.M33, EpsilonD);
        }

        [Test]
        public void Matrix3Dx3_StaticDeterminat_WithIdentityMatrix_ReturnsOne()
        {
            var result = Matrix3Dx3.Determinat(1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0);
            ClassicAssert.AreEqual(1.0, result, EpsilonD);
        }

        [Test]
        public void Matrix3Dx3_StaticDeterminat_WithSimpleMatrix_ReturnsCorrectDeterminant()
        {
            // Матрица: | 2 0 0 |
            //          | 0 3 0 |
            //          | 0 0 4 |
            // Определитель = 2 * 3 * 4 = 24
            var result = Matrix3Dx3.Determinat(2.0, 0.0, 0.0, 0.0, 3.0, 0.0, 0.0, 0.0, 4.0);
            ClassicAssert.AreEqual(24.0, result, EpsilonD);
        }

        [Test]
        public void Matrix3Dx3_StaticSetRotation_CreatesRotationMatrix()
        {
            var axis = new Vector3D(0.0, 0.0, 1.0); // Ось Z
            var angle = 90.0; // 90 градусов
            Matrix3Dx3.SetRotation(angle, in axis, out var result);
            // Для поворота на 90° вокруг Z: cos(90) = 0, sin(90) = 1
            ClassicAssert.AreEqual(0.0, result.M11, 0.1);
            ClassicAssert.AreEqual(1.0, result.M12, 0.1);
            ClassicAssert.AreEqual(0.0, result.M13, 0.1);
            ClassicAssert.AreEqual(-1.0, result.M21, 0.1);
            ClassicAssert.AreEqual(0.0, result.M22, 0.1);
            ClassicAssert.AreEqual(0.0, result.M23, 0.1);
            ClassicAssert.AreEqual(0.0, result.M31, 0.1);
            ClassicAssert.AreEqual(0.0, result.M32, 0.1);
            ClassicAssert.AreEqual(1.0, result.M33, 0.1);
        }

        [Test]
        public void Matrix3Dx3_GetHashCode_ReturnsConsistentHashCode()
        {
            var matrix1 = new Matrix3Dx3(1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0);
            var matrix2 = new Matrix3Dx3(1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0);
            ClassicAssert.AreEqual(matrix1.GetHashCode(), matrix2.GetHashCode());
        }
        #endregion

        #region Matrix3Dx3f Tests
        [Test]
        public void Matrix3Dx3f_Constructor_InitializesCorrectly()
        {
            var matrix = new Matrix3Dx3f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f);
            ClassicAssert.AreEqual(1.0f, matrix.M11, EpsilonF);
            ClassicAssert.AreEqual(2.0f, matrix.M12, EpsilonF);
            ClassicAssert.AreEqual(3.0f, matrix.M13, EpsilonF);
            ClassicAssert.AreEqual(4.0f, matrix.M21, EpsilonF);
            ClassicAssert.AreEqual(5.0f, matrix.M22, EpsilonF);
            ClassicAssert.AreEqual(6.0f, matrix.M23, EpsilonF);
            ClassicAssert.AreEqual(7.0f, matrix.M31, EpsilonF);
            ClassicAssert.AreEqual(8.0f, matrix.M32, EpsilonF);
            ClassicAssert.AreEqual(9.0f, matrix.M33, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_Identity_IsCorrect()
        {
            var identity = Matrix3Dx3f.Identity;
            ClassicAssert.AreEqual(1.0f, identity.M11, EpsilonF);
            ClassicAssert.AreEqual(0.0f, identity.M12, EpsilonF);
            ClassicAssert.AreEqual(0.0f, identity.M13, EpsilonF);
            ClassicAssert.AreEqual(0.0f, identity.M21, EpsilonF);
            ClassicAssert.AreEqual(1.0f, identity.M22, EpsilonF);
            ClassicAssert.AreEqual(0.0f, identity.M23, EpsilonF);
            ClassicAssert.AreEqual(0.0f, identity.M31, EpsilonF);
            ClassicAssert.AreEqual(0.0f, identity.M32, EpsilonF);
            ClassicAssert.AreEqual(1.0f, identity.M33, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_StaticAdd_AddsMatricesCorrectly()
        {
            var left = new Matrix3Dx3f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f);
            var right = new Matrix3Dx3f(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
            Matrix3Dx3f.Add(in left, in right, out var result);
            ClassicAssert.AreEqual(2.0f, result.M11, EpsilonF);
            ClassicAssert.AreEqual(3.0f, result.M12, EpsilonF);
            ClassicAssert.AreEqual(4.0f, result.M13, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_StaticSubtract_SubtractsMatricesCorrectly()
        {
            var left = new Matrix3Dx3f(5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f);
            var right = new Matrix3Dx3f(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
            Matrix3Dx3f.Subtract(in left, in right, out var result);
            ClassicAssert.AreEqual(4.0f, result.M11, EpsilonF);
            ClassicAssert.AreEqual(4.0f, result.M12, EpsilonF);
            ClassicAssert.AreEqual(4.0f, result.M13, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_StaticMultiply_WithScalar_MultipliesCorrectly()
        {
            var matrix = new Matrix3Dx3f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f);
            var scalar = 2.0f;
            Matrix3Dx3f.Multiply(in matrix, scalar, out var result);
            ClassicAssert.AreEqual(2.0f, result.M11, EpsilonF);
            ClassicAssert.AreEqual(4.0f, result.M12, EpsilonF);
            ClassicAssert.AreEqual(6.0f, result.M13, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_StaticMultiply_WithMatrix_MultipliesCorrectly()
        {
            var left = new Matrix3Dx3f(1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f);
            var right = new Matrix3Dx3f(2.0f, 0.0f, 0.0f, 0.0f, 2.0f, 0.0f, 0.0f, 0.0f, 2.0f);
            Matrix3Dx3f.Multiply(in left, in right, out var result);
            ClassicAssert.AreEqual(2.0f, result.M11, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M12, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M13, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M21, EpsilonF);
            ClassicAssert.AreEqual(2.0f, result.M22, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M23, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_StaticTranspose_TransposesMatrixCorrectly()
        {
            var matrix = new Matrix3Dx3f(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f);
            Matrix3Dx3f.Transpose(in matrix, out var result);
            ClassicAssert.AreEqual(1.0f, result.M11, EpsilonF);
            ClassicAssert.AreEqual(4.0f, result.M12, EpsilonF);
            ClassicAssert.AreEqual(7.0f, result.M13, EpsilonF);
            ClassicAssert.AreEqual(2.0f, result.M21, EpsilonF);
            ClassicAssert.AreEqual(5.0f, result.M22, EpsilonF);
            ClassicAssert.AreEqual(8.0f, result.M23, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_StaticInvert_InvertsIdentityMatrix()
        {
            var identity = Matrix3Dx3f.Identity;
            Matrix3Dx3f.Invert(in identity, out var result);
            // Обратная единичной матрицы - это сама единичная матрица
            ClassicAssert.AreEqual(1.0f, result.M11, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M12, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M13, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M21, EpsilonF);
            ClassicAssert.AreEqual(1.0f, result.M22, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_StaticLerp_InterpolatesMatricesCorrectly()
        {
            var start = new Matrix3Dx3f(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
            var end = new Matrix3Dx3f(10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 10.0f);
            Matrix3Dx3f.Lerp(in start, in end, 0.5f, out var result);
            ClassicAssert.AreEqual(5.0f, result.M11, EpsilonF);
            ClassicAssert.AreEqual(5.0f, result.M12, EpsilonF);
            ClassicAssert.AreEqual(5.0f, result.M13, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_StaticScaling_CreatesScalingMatrix()
        {
            var scale = new Vector3Df(2.0f, 3.0f, 4.0f);
            Matrix3Dx3f.Scaling(in scale, out var result);
            ClassicAssert.AreEqual(2.0f, result.M11, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M12, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M13, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M21, EpsilonF);
            ClassicAssert.AreEqual(3.0f, result.M22, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M23, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M31, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.M32, EpsilonF);
            ClassicAssert.AreEqual(4.0f, result.M33, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_StaticDeterminat_WithIdentityMatrix_ReturnsOne()
        {
            var result = Matrix3Dx3f.Determinat(1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f);
            ClassicAssert.AreEqual(1.0f, result, EpsilonF);
        }

        [Test]
        public void Matrix3Dx3f_StaticDeterminat_WithSimpleMatrix_ReturnsCorrectDeterminant()
        {
            // Матрица: | 2 0 0 |
            //          | 0 3 0 |
            //          | 0 0 4 |
            // Определитель = 2 * 3 * 4 = 24
            var result = Matrix3Dx3f.Determinat(2.0f, 0.0f, 0.0f, 0.0f, 3.0f, 0.0f, 0.0f, 0.0f, 4.0f);
            ClassicAssert.AreEqual(24.0f, result, EpsilonF);
        }
        #endregion
    }
}
