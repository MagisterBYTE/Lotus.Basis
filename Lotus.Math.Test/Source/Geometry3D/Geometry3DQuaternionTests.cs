using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DQuaternionTests
    {
        private const double EpsilonD = 0.00001;
        private const float EpsilonF = 0.00001f;

        #region Quaternion3D Tests
        [Test]
        public void Quaternion3D_Constructor_InitializesCorrectly()
        {
            var quaternion = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.AreEqual(1.0, quaternion.X, EpsilonD);
            ClassicAssert.AreEqual(2.0, quaternion.Y, EpsilonD);
            ClassicAssert.AreEqual(3.0, quaternion.Z, EpsilonD);
            ClassicAssert.AreEqual(4.0, quaternion.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_Constructor_WithDefaultW_InitializesCorrectly()
        {
            var quaternion = new Quaternion3D(1.0, 2.0, 3.0);
            ClassicAssert.AreEqual(1.0, quaternion.X, EpsilonD);
            ClassicAssert.AreEqual(2.0, quaternion.Y, EpsilonD);
            ClassicAssert.AreEqual(3.0, quaternion.Z, EpsilonD);
            ClassicAssert.AreEqual(1.0, quaternion.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_Constructor_WithAxisAngle_InitializesCorrectly()
        {
            var axis = new Vector3D(1.0, 0.0, 0.0);
            var angle = 90.0;
            var quaternion = new Quaternion3D(axis, angle);
            // Для поворота на 90 градусов вокруг оси X
            ClassicAssert.AreEqual(0.7071067811865475, quaternion.X, 0.1);
            ClassicAssert.AreEqual(0.0, quaternion.Y, EpsilonD);
            ClassicAssert.AreEqual(0.0, quaternion.Z, EpsilonD);
            ClassicAssert.AreEqual(0.7071067811865475, quaternion.W, 0.1);
        }

        [Test]
        public void Quaternion3D_CopyConstructor_InitializesCorrectly()
        {
            var original = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var copy = new Quaternion3D(original);
            ClassicAssert.AreEqual(original.X, copy.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, copy.Y, EpsilonD);
            ClassicAssert.AreEqual(original.Z, copy.Z, EpsilonD);
            ClassicAssert.AreEqual(original.W, copy.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_Identity_IsCorrect()
        {
            ClassicAssert.AreEqual(new Quaternion3D(0, 0, 0, 1), Quaternion3D.Identity);
        }

        [Test]
        public void Quaternion3D_SqrLength_ReturnsCorrectSquaredLength()
        {
            var quaternion = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.AreEqual(30.0, quaternion.SqrLength, EpsilonD); // 1^2 + 2^2 + 3^2 + 4^2 = 1 + 4 + 9 + 16 = 30
        }

        [Test]
        public void Quaternion3D_Length_ReturnsCorrectLength()
        {
            var quaternion = new Quaternion3D(0.0, 0.0, 0.0, 1.0);
            ClassicAssert.AreEqual(1.0, quaternion.Length, EpsilonD);
        }

        [Test]
        public void Quaternion3D_Normalized_ReturnsNormalizedQuaternion()
        {
            var quaternion = new Quaternion3D(2.0, 0.0, 0.0, 0.0);
            var normalized = quaternion.Normalized;
            ClassicAssert.AreEqual(1.0, normalized.Length, EpsilonD);
        }

        [Test]
        public void Quaternion3D_Conjugated_ReturnsConjugatedQuaternion()
        {
            var quaternion = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var conjugated = quaternion.Conjugated;
            ClassicAssert.AreEqual(-1.0, conjugated.X, EpsilonD);
            ClassicAssert.AreEqual(-2.0, conjugated.Y, EpsilonD);
            ClassicAssert.AreEqual(-3.0, conjugated.Z, EpsilonD);
            ClassicAssert.AreEqual(4.0, conjugated.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_Equals_ReturnsTrueForEqualQuaternions()
        {
            var q1 = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var q2 = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.IsTrue(q1.Equals(q2));
            ClassicAssert.IsTrue(q1.Equals((object)q2));
        }

        [Test]
        public void Quaternion3D_Equals_ReturnsFalseForDifferentQuaternions()
        {
            var q1 = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var q2 = new Quaternion3D(5.0, 6.0, 7.0, 8.0);
            ClassicAssert.IsFalse(q1.Equals(q2));
            ClassicAssert.IsFalse(q1.Equals(null));
        }

        [Test]
        public void Quaternion3D_GetHashCode_ReturnsConsistentHashCode()
        {
            var q1 = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var q2 = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.AreEqual(q1.GetHashCode(), q2.GetHashCode());
        }

        [Test]
        public void Quaternion3D_AdditionOperator_AddsQuaternionsCorrectly()
        {
            var q1 = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var q2 = new Quaternion3D(5.0, 6.0, 7.0, 8.0);
            var result = q1 + q2;
            ClassicAssert.AreEqual(6.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(8.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(10.0, result.Z, EpsilonD);
            ClassicAssert.AreEqual(12.0, result.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_SubtractionOperator_SubtractsQuaternionsCorrectly()
        {
            var q1 = new Quaternion3D(5.0, 6.0, 7.0, 8.0);
            var q2 = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var result = q1 - q2;
            ClassicAssert.AreEqual(4.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Z, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_MultiplicationOperator_MultipliesByScalarCorrectly()
        {
            var q = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var scalar = 2.0;
            var result = q * scalar;
            ClassicAssert.AreEqual(2.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(6.0, result.Z, EpsilonD);
            ClassicAssert.AreEqual(8.0, result.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_DivisionOperator_DividesByScalarCorrectly()
        {
            var q = new Quaternion3D(2.0, 4.0, 6.0, 8.0);
            var scalar = 2.0;
            var result = q / scalar;
            ClassicAssert.AreEqual(1.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(2.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(3.0, result.Z, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_MultiplicationOperator_MultipliesQuaternionsCorrectly()
        {
            var q1 = new Quaternion3D(1.0, 0.0, 0.0, 0.0);
            var q2 = new Quaternion3D(0.0, 1.0, 0.0, 0.0);
            var result = q1 * q2;
            // Кватернионное умножение: i * j = k
            ClassicAssert.AreEqual(0.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(0.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(1.0, result.Z, EpsilonD);
            ClassicAssert.AreEqual(0.0, result.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_EqualityOperator_ReturnsTrueForEqualQuaternions()
        {
            var q1 = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var q2 = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.IsTrue(q1 == q2);
        }

        [Test]
        public void Quaternion3D_InequalityOperator_ReturnsTrueForDifferentQuaternions()
        {
            var q1 = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var q2 = new Quaternion3D(5.0, 6.0, 7.0, 8.0);
            ClassicAssert.IsTrue(q1 != q2);
        }

        [Test]
        public void Quaternion3D_NegationOperator_NegatesQuaternionCorrectly()
        {
            var q = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            var result = -q;
            ClassicAssert.AreEqual(-1.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(-2.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(-3.0, result.Z, EpsilonD);
            ClassicAssert.AreEqual(-4.0, result.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_Indexer_AccessesComponentsCorrectly()
        {
            var quaternion = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.AreEqual(1.0, quaternion[0], EpsilonD);
            ClassicAssert.AreEqual(2.0, quaternion[1], EpsilonD);
            ClassicAssert.AreEqual(3.0, quaternion[2], EpsilonD);
            ClassicAssert.AreEqual(4.0, quaternion[3], EpsilonD);

            quaternion[0] = 5.0;
            quaternion[1] = 6.0;
            quaternion[2] = 7.0;
            quaternion[3] = 8.0;
            ClassicAssert.AreEqual(5.0, quaternion.X, EpsilonD);
            ClassicAssert.AreEqual(6.0, quaternion.Y, EpsilonD);
            ClassicAssert.AreEqual(7.0, quaternion.Z, EpsilonD);
            ClassicAssert.AreEqual(8.0, quaternion.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_StaticAxisAngle_CreatesQuaternionFromAxisAngle()
        {
            var axis = new Vector3D(1.0, 0.0, 0.0);
            var angle = 90.0;
            Quaternion3D.AxisAngle(in axis, angle, out var result);
            ClassicAssert.AreEqual(0.7071067811865475, result.X, 0.1);
            ClassicAssert.AreEqual(0.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(0.0, result.Z, EpsilonD);
            ClassicAssert.AreEqual(0.7071067811865475, result.W, 0.1);
        }

        [Test]
        public void Quaternion3D_StaticLerp_InterpolatesQuaternionsCorrectly()
        {
            var from = new Quaternion3D(0.0, 0.0, 0.0, 0.0);
            var to = new Quaternion3D(1.0, 1.0, 1.0, 1.0);
            var result = Quaternion3D.Lerp(in from, in to, 0.5);
            ClassicAssert.AreEqual(0.5, result.X, EpsilonD);
            ClassicAssert.AreEqual(0.5, result.Y, EpsilonD);
            ClassicAssert.AreEqual(0.5, result.Z, EpsilonD);
            ClassicAssert.AreEqual(0.5, result.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_Normalize_NormalizesQuaternionInPlace()
        {
            var quaternion = new Quaternion3D(2.0, 0.0, 0.0, 0.0);
            quaternion.Normalize();
            ClassicAssert.AreEqual(1.0, quaternion.Length, EpsilonD);
        }

        [Test]
        public void Quaternion3D_Conjugate_ConjugatesQuaternionInPlace()
        {
            var quaternion = new Quaternion3D(1.0, 2.0, 3.0, 4.0);
            quaternion.Conjugate();
            ClassicAssert.AreEqual(-1.0, quaternion.X, EpsilonD);
            ClassicAssert.AreEqual(-2.0, quaternion.Y, EpsilonD);
            ClassicAssert.AreEqual(-3.0, quaternion.Z, EpsilonD);
            ClassicAssert.AreEqual(4.0, quaternion.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_Set_SetsComponentsCorrectly()
        {
            var quaternion = new Quaternion3D();
            quaternion.Set(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.AreEqual(1.0, quaternion.X, EpsilonD);
            ClassicAssert.AreEqual(2.0, quaternion.Y, EpsilonD);
            ClassicAssert.AreEqual(3.0, quaternion.Z, EpsilonD);
            ClassicAssert.AreEqual(4.0, quaternion.W, EpsilonD);
        }

        [Test]
        public void Quaternion3D_SetFromAxisAngle_SetsQuaternionFromAxisAngle()
        {
            var quaternion = new Quaternion3D();
            var axis = new Vector3D(1.0, 0.0, 0.0);
            var angle = 90.0;
            quaternion.SetFromAxisAngle(in axis, angle);
            ClassicAssert.AreEqual(0.7071067811865475, quaternion.X, 0.1);
            ClassicAssert.AreEqual(0.0, quaternion.Y, EpsilonD);
            ClassicAssert.AreEqual(0.0, quaternion.Z, EpsilonD);
            ClassicAssert.AreEqual(0.7071067811865475, quaternion.W, 0.1);
        }

        [Test]
        public void Quaternion3D_TransformVector_TransformsVectorCorrectly()
        {
            // Поворот на 90 градусов вокруг оси Z
            var quaternion = new Quaternion3D(new Vector3D(0.0, 0.0, 1.0), 90.0);
            var vector = new Vector3D(1.0, 0.0, 0.0);
            var result = quaternion.TransformVector(in vector);
            // Вектор (1,0,0) после поворота на 90° вокруг Z должен стать (0,1,0)
            ClassicAssert.AreEqual(0.0, result.X, 0.1);
            ClassicAssert.AreEqual(1.0, result.Y, 0.1);
            ClassicAssert.AreEqual(0.0, result.Z, EpsilonD);
        }

        [Test]
        public void Quaternion3D_SerializeToString_ReturnsCorrectString()
        {
            var quaternion = new Quaternion3D(1.23, 4.56, 7.89, 10.12);
            ClassicAssert.AreEqual("1.23;4.56;7.89;10.12", quaternion.SerializeToString().Replace(',', '.'));
        }

        [Test]
        public void Quaternion3D_DeserializeFromString_ReturnsCorrectQuaternion()
        {
            var data = "1.23;4.56;7.89;10.12";
            var quaternion = Quaternion3D.DeserializeFromString(data);
            ClassicAssert.AreEqual(1.23, quaternion.X, EpsilonD);
            ClassicAssert.AreEqual(4.56, quaternion.Y, EpsilonD);
            ClassicAssert.AreEqual(7.89, quaternion.Z, EpsilonD);
            ClassicAssert.AreEqual(10.12, quaternion.W, EpsilonD);
        }
        #endregion

        #region Quaternion3Df Tests
        [Test]
        public void Quaternion3Df_Constructor_InitializesCorrectly()
        {
            var quaternion = new Quaternion3Df(1.0f, 2.0f, 3.0f, 4.0f);
            ClassicAssert.AreEqual(1.0f, quaternion.X, EpsilonF);
            ClassicAssert.AreEqual(2.0f, quaternion.Y, EpsilonF);
            ClassicAssert.AreEqual(3.0f, quaternion.Z, EpsilonF);
            ClassicAssert.AreEqual(4.0f, quaternion.W, EpsilonF);
        }

        [Test]
        public void Quaternion3Df_Identity_IsCorrect()
        {
            ClassicAssert.AreEqual(new Quaternion3Df(0, 0, 0, 1), Quaternion3Df.Identity);
        }

        [Test]
        public void Quaternion3Df_SqrLength_ReturnsCorrectSquaredLength()
        {
            var quaternion = new Quaternion3Df(1.0f, 2.0f, 3.0f, 4.0f);
            ClassicAssert.AreEqual(30.0f, quaternion.SqrLength, EpsilonF);
        }

        [Test]
        public void Quaternion3Df_Length_ReturnsCorrectLength()
        {
            var quaternion = new Quaternion3Df(0.0f, 0.0f, 0.0f, 1.0f);
            ClassicAssert.AreEqual(1.0f, quaternion.Length, EpsilonF);
        }

        [Test]
        public void Quaternion3Df_Normalized_ReturnsNormalizedQuaternion()
        {
            var quaternion = new Quaternion3Df(2.0f, 0.0f, 0.0f, 0.0f);
            var normalized = quaternion.Normalized;
            ClassicAssert.AreEqual(1.0f, normalized.Length, EpsilonF);
        }

        [Test]
        public void Quaternion3Df_StaticAxisAngle_CreatesQuaternionFromAxisAngle()
        {
            var axis = new Vector3Df(1.0f, 0.0f, 0.0f);
            var angle = 90.0f;
            Quaternion3Df.AxisAngle(in axis, angle, out var result);
            ClassicAssert.AreEqual(0.7071067811865475f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);
            ClassicAssert.AreEqual(0.7071067811865475f, result.W, 0.1f);
        }

        [Test]
        public void Quaternion3Df_StaticLerp_InterpolatesQuaternionsCorrectly()
        {
            var from = new Quaternion3Df(0.0f, 0.0f, 0.0f, 0.0f);
            var to = new Quaternion3Df(1.0f, 1.0f, 1.0f, 1.0f);
            var result = Quaternion3Df.Lerp(in from, in to, 0.5f);
            ClassicAssert.AreEqual(0.5f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.5f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.5f, result.Z, EpsilonF);
            ClassicAssert.AreEqual(0.5f, result.W, EpsilonF);
        }

        [Test]
        public void Quaternion3Df_RotationYawPitchRoll_CreatesQuaternionCorrectly()
        {
            var result = Quaternion3Df.RotationYawPitchRoll(0.0f, 0.0f, 0.0f);
            // Для нулевых углов должен быть единичный кватернион
            ClassicAssert.AreEqual(0.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);
            ClassicAssert.AreEqual(1.0f, result.W, EpsilonF);
        }

        [Test]
        public void Quaternion3Df_Normalize_NormalizesQuaternionInPlace()
        {
            var quaternion = new Quaternion3Df(2.0f, 0.0f, 0.0f, 0.0f);
            quaternion.Normalize();
            ClassicAssert.AreEqual(1.0f, quaternion.Length, EpsilonF);
        }

        [Test]
        public void Quaternion3Df_Conjugate_ConjugatesQuaternionInPlace()
        {
            var quaternion = new Quaternion3Df(1.0f, 2.0f, 3.0f, 4.0f);
            quaternion.Conjugate();
            ClassicAssert.AreEqual(-1.0f, quaternion.X, EpsilonF);
            ClassicAssert.AreEqual(-2.0f, quaternion.Y, EpsilonF);
            ClassicAssert.AreEqual(-3.0f, quaternion.Z, EpsilonF);
            ClassicAssert.AreEqual(4.0f, quaternion.W, EpsilonF);
        }

        [Test]
        public void Quaternion3Df_Set_SetsComponentsCorrectly()
        {
            var quaternion = new Quaternion3Df();
            quaternion.Set(1.0f, 2.0f, 3.0f, 4.0f);
            ClassicAssert.AreEqual(1.0f, quaternion.X, EpsilonF);
            ClassicAssert.AreEqual(2.0f, quaternion.Y, EpsilonF);
            ClassicAssert.AreEqual(3.0f, quaternion.Z, EpsilonF);
            ClassicAssert.AreEqual(4.0f, quaternion.W, EpsilonF);
        }

        [Test]
        public void Quaternion3Df_DeserializeFromString_ReturnsCorrectQuaternion()
        {
            var data = "1.23;4.56;7.89;10.12";
            var quaternion = Quaternion3Df.DeserializeFromString(data);
            ClassicAssert.AreEqual(1.23f, quaternion.X, EpsilonF);
            ClassicAssert.AreEqual(4.56f, quaternion.Y, EpsilonF);
            ClassicAssert.AreEqual(7.89f, quaternion.Z, EpsilonF);
            ClassicAssert.AreEqual(10.12f, quaternion.W, EpsilonF);
        }
        #endregion
    }
}
