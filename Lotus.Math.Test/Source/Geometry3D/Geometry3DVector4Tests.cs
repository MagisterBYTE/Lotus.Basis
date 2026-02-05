using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DVector4Tests
    {
        private const double EpsilonD = 0.00001;
        private const float EpsilonF = 0.00001f;

        #region Vector4D Tests
        [Test]
        public void Vector4D_Constructor_InitializesCorrectly()
        {
            var vector = new Vector4D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.AreEqual(1.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(2.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(3.0, vector.Z, EpsilonD);
            ClassicAssert.AreEqual(4.0, vector.W, EpsilonD);
        }

        [Test]
        public void Vector4D_Constructor_WithDefaultW_InitializesCorrectly()
        {
            var vector = new Vector4D(1.0, 2.0, 3.0);
            ClassicAssert.AreEqual(1.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(2.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(3.0, vector.Z, EpsilonD);
            ClassicAssert.AreEqual(1.0, vector.W, EpsilonD);
        }

        [Test]
        public void Vector4D_CopyConstructor_InitializesCorrectly()
        {
            var original = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var copy = new Vector4D(original);
            ClassicAssert.AreEqual(original.X, copy.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, copy.Y, EpsilonD);
            ClassicAssert.AreEqual(original.Z, copy.Z, EpsilonD);
            ClassicAssert.AreEqual(original.W, copy.W, EpsilonD);
        }

        [Test]
        public void Vector4D_StaticConstants_AreCorrect()
        {
            ClassicAssert.AreEqual(new Vector4D(1, 1, 1), Vector4D.One);
            ClassicAssert.AreEqual(new Vector4D(1, 0, 0), Vector4D.Right);
            ClassicAssert.AreEqual(new Vector4D(0, 1, 0), Vector4D.Up);
            ClassicAssert.AreEqual(new Vector4D(0, 0, 1), Vector4D.Forward);
            ClassicAssert.AreEqual(new Vector4D(0, 0, 0), Vector4D.Zero);
        }

        [Test]
        public void Vector4D_SqrLength_ReturnsCorrectSquaredLength()
        {
            var vector = new Vector4D(3.0, 4.0, 5.0, 6.0);
            ClassicAssert.AreEqual(50.0, vector.SqrLength, EpsilonD); // 3^2 + 4^2 + 5^2 = 9 + 16 + 25 = 50 (W не учитывается)
        }

        [Test]
        public void Vector4D_Length_ReturnsCorrectLength()
        {
            var vector = new Vector4D(3.0, 4.0, 0.0, 5.0);
            ClassicAssert.AreEqual(5.0, vector.Length, EpsilonD);
        }

        [Test]
        public void Vector4D_Normalized_ReturnsNormalizedVector()
        {
            var vector = new Vector4D(3.0, 4.0, 0.0, 5.0);
            var normalized = vector.Normalized;
            ClassicAssert.AreEqual(0.6, normalized.X, EpsilonD);
            ClassicAssert.AreEqual(0.8, normalized.Y, EpsilonD);
            ClassicAssert.AreEqual(0.0, normalized.Z, EpsilonD);
            ClassicAssert.AreEqual(1.0, normalized.Length, EpsilonD);
        }

        [Test]
        public void Vector4D_Normalize_NormalizesVectorInPlace()
        {
            var vector = new Vector4D(3.0, 4.0, 0.0, 5.0);
            vector.Normalize();
            ClassicAssert.AreEqual(0.6, vector.X, EpsilonD);
            ClassicAssert.AreEqual(0.8, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(0.0, vector.Z, EpsilonD);
            ClassicAssert.AreEqual(1.0, vector.Length, EpsilonD);
        }

        [Test]
        public void Vector4D_Equals_ReturnsTrueForEqualVectors()
        {
            var vector1 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var vector2 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.IsTrue(vector1.Equals(vector2));
            ClassicAssert.IsTrue(vector1.Equals((object)vector2));
        }

        [Test]
        public void Vector4D_Equals_ReturnsFalseForDifferentVectors()
        {
            var vector1 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var vector2 = new Vector4D(3.0, 4.0, 5.0, 6.0);
            ClassicAssert.IsFalse(vector1.Equals(vector2));
            ClassicAssert.IsFalse(vector1.Equals(null));
        }

        [Test]
        public void Vector4D_CompareTo_ReturnsCorrectComparisonResult()
        {
            var vector1 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var vector2 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var vector3 = new Vector4D(1.0, 2.0, 4.0, 3.0);
            var vector4 = new Vector4D(1.0, 3.0, 2.0, 4.0);
            var vector5 = new Vector4D(2.0, 1.0, 1.0, 4.0);

            ClassicAssert.AreEqual(0, vector1.CompareTo(vector2));
            ClassicAssert.AreEqual(-1, vector1.CompareTo(vector3));
            ClassicAssert.AreEqual(-1, vector1.CompareTo(vector4));
            ClassicAssert.AreEqual(-1, vector1.CompareTo(vector5));
        }

        [Test]
        public void Vector4D_GetHashCode_ReturnsConsistentHashCode()
        {
            var vector1 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var vector2 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.AreEqual(vector1.GetHashCode(), vector2.GetHashCode());
        }

        [Test]
        public void Vector4D_ToString_ReturnsFormattedString()
        {
            var vector = new Vector4D(1.234, 5.678, 9.012, 3.456);
            var result = vector.ToString().Replace(',', '.');
            ClassicAssert.IsTrue(result.Contains("1.23"));
            ClassicAssert.IsTrue(result.Contains("5.68"));
            ClassicAssert.IsTrue(result.Contains("9.01"));
        }

        [Test]
        public void Vector4D_AdditionOperator_AddsVectorsCorrectly()
        {
            var v1 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var v2 = new Vector4D(4.0, 5.0, 6.0, 7.0);
            var result = v1 + v2;
            ClassicAssert.AreEqual(5.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(7.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(9.0, result.Z, EpsilonD);
            // W не участвует в операциях
        }

        [Test]
        public void Vector4D_SubtractionOperator_SubtractsVectorsCorrectly()
        {
            var v1 = new Vector4D(5.0, 6.0, 7.0, 8.0);
            var v2 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var result = v1 - v2;
            ClassicAssert.AreEqual(4.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector4D_MultiplicationOperator_MultipliesByScalarCorrectly()
        {
            var v = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var scalar = 3.0;
            var result = v * scalar;
            ClassicAssert.AreEqual(3.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(6.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(9.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector4D_DivisionOperator_DividesByScalarCorrectly()
        {
            var v = new Vector4D(6.0, 9.0, 12.0, 15.0);
            var scalar = 3.0;
            var result = v / scalar;
            ClassicAssert.AreEqual(2.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(3.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector4D_DotProductOperator_ReturnsCorrectDotProduct()
        {
            var v1 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var v2 = new Vector4D(4.0, 5.0, 6.0, 7.0);
            var result = v1 * v2;
            ClassicAssert.AreEqual(32.0, result, EpsilonD); // 1*4 + 2*5 + 3*6 = 4 + 10 + 18 = 32
        }

        [Test]
        public void Vector4D_CrossProductOperator_ReturnsCorrectCrossProduct()
        {
            var v1 = new Vector4D(1.0, 0.0, 0.0, 1.0);
            var v2 = new Vector4D(0.0, 1.0, 0.0, 1.0);
            var result = v1 ^ v2;
            ClassicAssert.AreEqual(0.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(0.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(1.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector4D_EqualityOperator_ReturnsTrueForEqualVectors()
        {
            var v1 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var v2 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.IsTrue(v1 == v2);
        }

        [Test]
        public void Vector4D_InequalityOperator_ReturnsTrueForDifferentVectors()
        {
            var v1 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var v2 = new Vector4D(3.0, 4.0, 5.0, 6.0);
            ClassicAssert.IsTrue(v1 != v2);
        }

        [Test]
        public void Vector4D_LessThanOperator_ReturnsCorrectResult()
        {
            var v1 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var v2 = new Vector4D(1.0, 2.0, 4.0, 3.0);
            var v3 = new Vector4D(1.0, 3.0, 2.0, 4.0);
            var v4 = new Vector4D(2.0, 1.0, 1.0, 4.0);
            ClassicAssert.IsTrue(v1 < v2);
            ClassicAssert.IsTrue(v1 < v3);
            ClassicAssert.IsTrue(v1 < v4);
        }

        [Test]
        public void Vector4D_GreaterThanOperator_ReturnsCorrectResult()
        {
            var v1 = new Vector4D(1.0, 2.0, 4.0, 3.0);
            var v2 = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var v3 = new Vector4D(1.0, 3.0, 2.0, 4.0);
            var v4 = new Vector4D(2.0, 1.0, 1.0, 4.0);
            ClassicAssert.IsTrue(v1 > v2);
            ClassicAssert.IsTrue(v3 > v2);
            ClassicAssert.IsTrue(v4 > v2);
        }

        [Test]
        public void Vector4D_NegationOperator_NegatesVectorCorrectly()
        {
            var v = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var result = -v;
            ClassicAssert.AreEqual(-1.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(-2.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(-3.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector4D_Indexer_AccessesComponentsCorrectly()
        {
            var vector = new Vector4D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.AreEqual(1.0, vector[0], EpsilonD);
            ClassicAssert.AreEqual(2.0, vector[1], EpsilonD);
            ClassicAssert.AreEqual(3.0, vector[2], EpsilonD);
            ClassicAssert.AreEqual(4.0, vector[3], EpsilonD);

            vector[0] = 5.0;
            vector[1] = 6.0;
            vector[2] = 7.0;
            vector[3] = 8.0;
            ClassicAssert.AreEqual(5.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(6.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(7.0, vector.Z, EpsilonD);
            ClassicAssert.AreEqual(8.0, vector.W, EpsilonD);
        }

        [Test]
        public void Vector4D_StaticCos_ReturnsCorrectCosine()
        {
            var from = new Vector4D(1.0, 0.0, 0.0, 1.0);
            var to = new Vector4D(0.0, 1.0, 0.0, 1.0);
            ClassicAssert.AreEqual(0.0, Vector4D.Cos(in from, in to), EpsilonD);

            from = new Vector4D(1.0, 0.0, 0.0, 1.0);
            to = new Vector4D(1.0, 0.0, 0.0, 1.0);
            ClassicAssert.AreEqual(1.0, Vector4D.Cos(in from, in to), EpsilonD);
        }

        [Test]
        public void Vector4D_StaticAngle_ReturnsCorrectAngleInDegrees()
        {
            var from = new Vector4D(1.0, 0.0, 0.0, 1.0);
            var to = new Vector4D(0.0, 1.0, 0.0, 1.0);
            ClassicAssert.AreEqual(90.0, Vector4D.Angle(in from, in to), 0.1);

            from = new Vector4D(1.0, 0.0, 0.0, 1.0);
            to = new Vector4D(-1.0, 0.0, 0.0, 1.0);
            ClassicAssert.AreEqual(180.0, Vector4D.Angle(in from, in to), 0.1);
        }

        [Test]
        public void Vector4D_StaticDistance_ReturnsCorrectDistance()
        {
            var a = new Vector4D(0.0, 0.0, 0.0, 1.0);
            var b = new Vector4D(3.0, 4.0, 0.0, 1.0);
            ClassicAssert.AreEqual(5.0, Vector4D.Distance(in a, in b), EpsilonD);
        }

        [Test]
        public void Vector4D_StaticDot_ReturnsCorrectDotProduct()
        {
            var a = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var b = new Vector4D(4.0, 5.0, 6.0, 7.0);
            ClassicAssert.AreEqual(32.0, Vector4D.Dot(in a, in b), EpsilonD);
        }

        [Test]
        public void Vector4D_StaticLerp_InterpolatesVectorsCorrectly()
        {
            var from = new Vector4D(0.0, 0.0, 0.0, 0.0);
            var to = new Vector4D(10.0, 10.0, 10.0, 10.0);
            var result = Vector4D.Lerp(in from, in to, 0.5);
            ClassicAssert.AreEqual(5.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(5.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(5.0, result.Z, EpsilonD);
            ClassicAssert.AreEqual(5.0, result.W, EpsilonD);
        }

        [Test]
        public void Vector4D_Distance_ReturnsCorrectDistance()
        {
            var vector = new Vector4D(0.0, 0.0, 0.0, 1.0);
            var other = new Vector4D(3.0, 4.0, 0.0, 1.0);
            ClassicAssert.AreEqual(5.0, vector.Distance(in other), EpsilonD);
        }

        [Test]
        public void Vector4D_Dot_ReturnsCorrectDotProduct()
        {
            var vector = new Vector4D(1.0, 2.0, 3.0, 4.0);
            var other = new Vector4D(4.0, 5.0, 6.0, 7.0);
            ClassicAssert.AreEqual(32.0, vector.Dot(in other), EpsilonD);
        }

        [Test]
        public void Vector4D_SetMaximize_SetsComponentsToMaxValues()
        {
            var vector = new Vector4D(2.0, 3.0, 4.0, 5.0);
            var vectorToMaximizeTo = new Vector4D(4.0, 1.0, 5.0, 6.0);
            vector.SetMaximize(in vector, in vectorToMaximizeTo);
            ClassicAssert.AreEqual(4.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(3.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(5.0, vector.Z, EpsilonD);
        }

        [Test]
        public void Vector4D_SetMinimize_SetsComponentsToMinValues()
        {
            var vector = new Vector4D(2.0, 3.0, 4.0, 5.0);
            var vectorToMinimizeTo = new Vector4D(4.0, 1.0, 5.0, 6.0);
            vector.SetMinimize(in vector, in vectorToMinimizeTo);
            ClassicAssert.AreEqual(2.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(1.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(4.0, vector.Z, EpsilonD);
        }

        [Test]
        public void Vector4D_CrossNormalize_NormalizesCrossProduct()
        {
            var vector = new Vector4D();
            var left = new Vector4D(1.0, 0.0, 0.0, 1.0);
            var right = new Vector4D(0.0, 1.0, 0.0, 1.0);
            vector.CrossNormalize(in left, in right);
            ClassicAssert.AreEqual(0.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(0.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(1.0, vector.Z, EpsilonD);
            ClassicAssert.AreEqual(1.0, vector.Length, EpsilonD);
        }

        [Test]
        public void Vector4D_SerializeToString_ReturnsCorrectString()
        {
            var vector = new Vector4D(1.23, 4.56, 7.89, 10.12);
            ClassicAssert.AreEqual("1.23;4.56;7.89;10.12", vector.SerializeToString().Replace(',', '.'));
        }

        [Test]
        public void Vector4D_DeserializeFromString_ReturnsCorrectVector()
        {
            var data = "1.23;4.56;7.89;10.12";
            var vector = Vector4D.DeserializeFromString(data);
            ClassicAssert.AreEqual(1.23, vector.X, EpsilonD);
            ClassicAssert.AreEqual(4.56, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(7.89, vector.Z, EpsilonD);
            ClassicAssert.AreEqual(10.12, vector.W, EpsilonD);
        }
        #endregion

        #region Vector4Df Tests
        [Test]
        public void Vector4Df_Constructor_InitializesCorrectly()
        {
            var vector = new Vector4Df(1.0f, 2.0f, 3.0f, 4.0f);
            ClassicAssert.AreEqual(1.0f, vector.X, EpsilonF);
            ClassicAssert.AreEqual(2.0f, vector.Y, EpsilonF);
            ClassicAssert.AreEqual(3.0f, vector.Z, EpsilonF);
            ClassicAssert.AreEqual(4.0f, vector.W, EpsilonF);
        }

        [Test]
        public void Vector4Df_Constructor_WithDefaultW_InitializesCorrectly()
        {
            var vector = new Vector4Df(1.0f, 2.0f, 3.0f);
            ClassicAssert.AreEqual(1.0f, vector.X, EpsilonF);
            ClassicAssert.AreEqual(2.0f, vector.Y, EpsilonF);
            ClassicAssert.AreEqual(3.0f, vector.Z, EpsilonF);
            ClassicAssert.AreEqual(1.0f, vector.W, EpsilonF);
        }

        [Test]
        public void Vector4Df_StaticConstants_AreCorrect()
        {
            ClassicAssert.AreEqual(new Vector4Df(1, 1, 1), Vector4Df.One);
            ClassicAssert.AreEqual(new Vector4Df(1, 0, 0), Vector4Df.Right);
            ClassicAssert.AreEqual(new Vector4Df(0, 1, 0), Vector4Df.Up);
            ClassicAssert.AreEqual(new Vector4Df(0, 0, 1), Vector4Df.Forward);
            ClassicAssert.AreEqual(new Vector4Df(0, 0, 0), Vector4Df.Zero);
        }

        [Test]
        public void Vector4Df_SqrLength_ReturnsCorrectSquaredLength()
        {
            var vector = new Vector4Df(3.0f, 4.0f, 5.0f, 6.0f);
            ClassicAssert.AreEqual(50.0f, vector.SqrLength, EpsilonF);
        }

        [Test]
        public void Vector4Df_Length_ReturnsCorrectLength()
        {
            var vector = new Vector4Df(3.0f, 4.0f, 0.0f, 5.0f);
            ClassicAssert.AreEqual(5.0f, vector.Length, EpsilonF);
        }

        [Test]
        public void Vector4Df_Normalized_ReturnsNormalizedVector()
        {
            var vector = new Vector4Df(3.0f, 4.0f, 0.0f, 5.0f);
            var normalized = vector.Normalized;
            ClassicAssert.AreEqual(0.6f, normalized.X, EpsilonF);
            ClassicAssert.AreEqual(0.8f, normalized.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, normalized.Z, EpsilonF);
            ClassicAssert.AreEqual(1.0f, normalized.Length, EpsilonF);
        }

        [Test]
        public void Vector4Df_StaticCos_ReturnsCorrectCosine()
        {
            var from = new Vector4Df(1.0f, 0.0f, 0.0f, 1.0f);
            var to = new Vector4Df(0.0f, 1.0f, 0.0f, 1.0f);
            ClassicAssert.AreEqual(0.0f, Vector4Df.Cos(in from, in to), EpsilonF);
        }

        [Test]
        public void Vector4Df_StaticAngle_ReturnsCorrectAngleInDegrees()
        {
            var from = new Vector4Df(1.0f, 0.0f, 0.0f, 1.0f);
            var to = new Vector4Df(0.0f, 1.0f, 0.0f, 1.0f);
            ClassicAssert.AreEqual(90.0f, Vector4Df.Angle(in from, in to), 0.1f);
        }

        [Test]
        public void Vector4Df_StaticDistance_ReturnsCorrectDistance()
        {
            var a = new Vector4Df(0.0f, 0.0f, 0.0f, 1.0f);
            var b = new Vector4Df(3.0f, 4.0f, 0.0f, 1.0f);
            ClassicAssert.AreEqual(5.0f, Vector4Df.Distance(in a, in b), EpsilonF);
        }

        [Test]
        public void Vector4Df_StaticDot_ReturnsCorrectDotProduct()
        {
            var a = new Vector4Df(1.0f, 2.0f, 3.0f, 4.0f);
            var b = new Vector4Df(4.0f, 5.0f, 6.0f, 7.0f);
            ClassicAssert.AreEqual(32.0f, Vector4Df.Dot(a, b), EpsilonF);
        }

        [Test]
        public void Vector4Df_StaticLerp_InterpolatesVectorsCorrectly()
        {
            var from = new Vector4Df(0.0f, 0.0f, 0.0f, 0.0f);
            var to = new Vector4Df(10.0f, 10.0f, 10.0f, 10.0f);
            var result = Vector4Df.Lerp(in from, in to, 0.5f);
            ClassicAssert.AreEqual(5.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(5.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(5.0f, result.Z, EpsilonF);
            ClassicAssert.AreEqual(5.0f, result.W, EpsilonF);
        }

        [Test]
        public void Vector4Df_DeserializeFromString_ReturnsCorrectVector()
        {
            var data = "1.23;4.56;7.89;10.12";
            var vector = Vector4Df.DeserializeFromString(data);
            ClassicAssert.AreEqual(1.23f, vector.X, EpsilonF);
            ClassicAssert.AreEqual(4.56f, vector.Y, EpsilonF);
            ClassicAssert.AreEqual(7.89f, vector.Z, EpsilonF);
            ClassicAssert.AreEqual(10.12f, vector.W, EpsilonF);
        }
        #endregion
    }
}
