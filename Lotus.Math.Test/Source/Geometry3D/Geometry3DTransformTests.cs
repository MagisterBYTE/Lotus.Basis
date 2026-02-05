using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DTransformTests
    {
        private const float EpsilonF = 0.00001f;

        #region Transform3Df Tests
        [Test]
        public void Transform3Df_Constructor_InitializesCorrectly()
        {
            var pivot = new Vector3Df(1.0f, 2.0f, 3.0f);
            var transform = new Transform3Df(pivot);
            ClassicAssert.AreEqual(pivot, transform.Pivot);
            ClassicAssert.AreEqual(Vector3Df.Zero, transform.Offset);
            ClassicAssert.AreEqual(Quaternion3Df.Identity, transform.Rotation);
            ClassicAssert.AreEqual(Vector3Df.Forward, transform.Forward);
            ClassicAssert.AreEqual(Vector3Df.Right, transform.Right);
            ClassicAssert.AreEqual(Vector3Df.Up, transform.Up);
        }

        [Test]
        public void Transform3Df_Pivot_GetAndSetWorksCorrectly()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            var newPivot = new Vector3Df(5.0f, 6.0f, 7.0f);
            transform.Pivot = newPivot;
            ClassicAssert.AreEqual(newPivot, transform.Pivot);
        }

        [Test]
        public void Transform3Df_Offset_GetAndSetWorksCorrectly()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            var newOffset = new Vector3Df(1.0f, 2.0f, 3.0f);
            transform.Offset = newOffset;
            ClassicAssert.AreEqual(newOffset, transform.Offset);
        }

        [Test]
        public void Transform3Df_Rotation_GetAndSetWorksCorrectly()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            var newRotation = new Quaternion3Df(0.0f, 0.0f, 0.0f, 1.0f);
            transform.Rotation = newRotation;
            ClassicAssert.AreEqual(newRotation, transform.Rotation);
        }

        [Test]
        public void Transform3Df_Forward_GetAndSetWorksCorrectly()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            var newForward = new Vector3Df(1.0f, 0.0f, 0.0f);
            transform.Forward = newForward;
            ClassicAssert.AreEqual(newForward, transform.Forward);
        }

        [Test]
        public void Transform3Df_Right_GetAndSetWorksCorrectly()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            var newRight = new Vector3Df(0.0f, 1.0f, 0.0f);
            transform.Right = newRight;
            ClassicAssert.AreEqual(newRight, transform.Right);
        }

        [Test]
        public void Transform3Df_Up_GetAndSetWorksCorrectly()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            var newUp = new Vector3Df(0.0f, 0.0f, 1.0f);
            transform.Up = newUp;
            ClassicAssert.AreEqual(newUp, transform.Up);
        }

        [Test]
        public void Transform3Df_MatrixTransform_ReturnsCorrectMatrix()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            var matrix = transform.MatrixTransform;
            // Для единичной трансформации матрица должна быть единичной
            ClassicAssert.AreEqual(1.0, matrix.M11, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M12, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M13, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M14, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M21, EpsilonF);
            ClassicAssert.AreEqual(1.0, matrix.M22, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M23, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M24, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M31, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M32, EpsilonF);
            ClassicAssert.AreEqual(1.0, matrix.M33, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M34, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M41, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M42, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M43, EpsilonF);
            ClassicAssert.AreEqual(1.0, matrix.M44, EpsilonF);
        }

        [Test]
        public void Transform3Df_SetRotate_RotatesTransformCorrectly()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            var axis = new Vector3Df(0.0f, 0.0f, 1.0f); // Ось Z
            var angle = 90.0f; // 90 градусов
            transform.SetRotate(in axis, angle);
            // После поворота на 90° вокруг Z, Forward должен быть направлен вправо
            ClassicAssert.AreEqual(0.0f, transform.Forward.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, transform.Forward.Y, 0.1f);
            ClassicAssert.AreEqual(1.0f, transform.Forward.Z, 0.1f);
        }

        [Test]
        public void Transform3Df_LookAt_SetsDirectionCorrectly()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            var target = new Vector3Df(1.0f, 0.0f, 0.0f);
            var up = new Vector3Df(0.0f, 1.0f, 0.0f);
            transform.LookAt(target, up);
            // Forward должен быть направлен к target
            ClassicAssert.AreEqual(1.0f, transform.Forward.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, transform.Forward.Y, 0.1f);
            ClassicAssert.AreEqual(0.0f, transform.Forward.Z, 0.1f);
        }

        [Test]
        public void Transform3Df_Update_NormalizesOrtVectors()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            // Устанавливаем ненормализованные векторы
            transform.Forward = new Vector3Df(2.0f, 0.0f, 0.0f);
            transform.Right = new Vector3Df(0.0f, 2.0f, 0.0f);
            transform.Up = new Vector3Df(0.0f, 0.0f, 2.0f);
            // Устанавливаем флаг обновления
            transform.SetRotate(new Vector3Df(0.0f, 0.0f, 1.0f), 0.0f);
            transform.Update();
            // После Update векторы должны быть нормализованы
            ClassicAssert.AreEqual(1.0f, transform.Forward.Length, 0.1f);
            ClassicAssert.AreEqual(1.0f, transform.Right.Length, 0.1f);
            ClassicAssert.AreEqual(1.0f, transform.Up.Length, 0.1f);
        }

        [Test]
        public void Transform3Df_SetRotate_UpdatesOrtVectors()
        {
            var transform = new Transform3Df(Vector3Df.Zero);
            var axis = new Vector3Df(0.0f, 0.0f, 1.0f);
            var angle = 90.0f;
            transform.SetRotate(in axis, angle);
            // После поворота орты должны быть обновлены
            ClassicAssert.AreEqual(1.0f, transform.Forward.Length, 0.1f);
            ClassicAssert.AreEqual(1.0f, transform.Right.Length, 0.1f);
            ClassicAssert.AreEqual(1.0f, transform.Up.Length, 0.1f);
        }

        [Test]
        public void Transform3Df_MatrixTransform_WithPivot_ReturnsCorrectMatrix()
        {
            var pivot = new Vector3Df(1.0f, 2.0f, 3.0f);
            var transform = new Transform3Df(pivot);
            var matrix = transform.MatrixTransform;
            // Матрица должна учитывать pivot
            ClassicAssert.AreEqual(1.0, matrix.M11, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M12, EpsilonF);
            ClassicAssert.AreEqual(0.0, matrix.M13, EpsilonF);
        }
        #endregion
    }
}
