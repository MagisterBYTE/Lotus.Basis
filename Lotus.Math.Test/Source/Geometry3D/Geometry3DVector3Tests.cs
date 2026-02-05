using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DVector3Tests
    {
        private const double EpsilonD = 0.00001;
        private const float EpsilonF = 0.00001f;

        #region Vector3D Tests
        [Test]
        public void Vector3D_Constructor_InitializesCorrectly()
        {
            var vector = new Vector3D(1.0, 2.0, 3.0);
            ClassicAssert.AreEqual(1.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(2.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(3.0, vector.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_CopyConstructor_InitializesCorrectly()
        {
            var original = new Vector3D(1.0, 2.0, 3.0);
            var copy = new Vector3D(original);
            ClassicAssert.AreEqual(original.X, copy.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, copy.Y, EpsilonD);
            ClassicAssert.AreEqual(original.Z, copy.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_StaticConstants_AreCorrect()
        {
            ClassicAssert.AreEqual(new Vector3D(1, 1, 1), Vector3D.One);
            ClassicAssert.AreEqual(new Vector3D(1, 0, 0), Vector3D.Right);
            ClassicAssert.AreEqual(new Vector3D(-1, 0, 0), Vector3D.Left);
            ClassicAssert.AreEqual(new Vector3D(0, 1, 0), Vector3D.Up);
            ClassicAssert.AreEqual(new Vector3D(0, -1, 0), Vector3D.Down);
            ClassicAssert.AreEqual(new Vector3D(0, 0, 1), Vector3D.Forward);
            ClassicAssert.AreEqual(new Vector3D(0, 0, -1), Vector3D.Back);
            ClassicAssert.AreEqual(new Vector3D(0, 0, 0), Vector3D.Zero);
        }

        [Test]
        public void Vector3D_SqrLength_ReturnsCorrectSquaredLength()
        {
            var vector = new Vector3D(3.0, 4.0, 5.0);
            ClassicAssert.AreEqual(50.0, vector.SqrLength, EpsilonD); // 3^2 + 4^2 + 5^2 = 9 + 16 + 25 = 50
        }

        [Test]
        public void Vector3D_Length_ReturnsCorrectLength()
        {
            var vector = new Vector3D(3.0, 4.0, 0.0);
            ClassicAssert.AreEqual(5.0, vector.Length, EpsilonD);
        }

        [Test]
        public void Vector3D_Normalized_ReturnsNormalizedVector()
        {
            var vector = new Vector3D(3.0, 4.0, 0.0);
            var normalized = vector.Normalized;
            ClassicAssert.AreEqual(0.6, normalized.X, EpsilonD);
            ClassicAssert.AreEqual(0.8, normalized.Y, EpsilonD);
            ClassicAssert.AreEqual(0.0, normalized.Z, EpsilonD);
            ClassicAssert.AreEqual(1.0, normalized.Length, EpsilonD);
        }

        [Test]
        public void Vector3D_Normalize_NormalizesVectorInPlace()
        {
            var vector = new Vector3D(3.0, 4.0, 0.0);
            vector.Normalize();
            ClassicAssert.AreEqual(0.6, vector.X, EpsilonD);
            ClassicAssert.AreEqual(0.8, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(0.0, vector.Z, EpsilonD);
            ClassicAssert.AreEqual(1.0, vector.Length, EpsilonD);
        }

        [Test]
        public void Vector3D_Equals_ReturnsTrueForEqualVectors()
        {
            var vector1 = new Vector3D(1.0, 2.0, 3.0);
            var vector2 = new Vector3D(1.0, 2.0, 3.0);
            ClassicAssert.IsTrue(vector1.Equals(vector2));
            ClassicAssert.IsTrue(vector1.Equals((object)vector2));
        }

        [Test]
        public void Vector3D_Equals_ReturnsFalseForDifferentVectors()
        {
            var vector1 = new Vector3D(1.0, 2.0, 3.0);
            var vector2 = new Vector3D(3.0, 4.0, 5.0);
            ClassicAssert.IsFalse(vector1.Equals(vector2));
            ClassicAssert.IsFalse(vector1.Equals(null));
        }

        [Test]
        public void Vector3D_CompareTo_ReturnsCorrectComparisonResult()
        {
            var vector1 = new Vector3D(1.0, 2.0, 3.0);
            var vector2 = new Vector3D(1.0, 2.0, 3.0);
            var vector3 = new Vector3D(1.0, 2.0, 4.0);
            var vector4 = new Vector3D(1.0, 3.0, 2.0);
            var vector5 = new Vector3D(2.0, 1.0, 1.0);

            ClassicAssert.AreEqual(0, vector1.CompareTo(vector2));
            ClassicAssert.AreEqual(-1, vector1.CompareTo(vector3));
            ClassicAssert.AreEqual(-1, vector1.CompareTo(vector4));
            ClassicAssert.AreEqual(-1, vector1.CompareTo(vector5));
        }

        [Test]
        public void Vector3D_GetHashCode_ReturnsConsistentHashCode()
        {
            var vector1 = new Vector3D(1.0, 2.0, 3.0);
            var vector2 = new Vector3D(1.0, 2.0, 3.0);
            ClassicAssert.AreEqual(vector1.GetHashCode(), vector2.GetHashCode());
        }

        [Test]
        public void Vector3D_ToString_ReturnsFormattedString()
        {
            var vector = new Vector3D(1.234, 5.678, 9.012);
            var result = vector.ToString().Replace(',', '.');
            ClassicAssert.IsTrue(result.Contains("1.23"));
            ClassicAssert.IsTrue(result.Contains("5.68"));
            ClassicAssert.IsTrue(result.Contains("9.01"));
        }

        [Test]
        public void Vector3D_AdditionOperator_AddsVectorsCorrectly()
        {
            var v1 = new Vector3D(1.0, 2.0, 3.0);
            var v2 = new Vector3D(4.0, 5.0, 6.0);
            var result = v1 + v2;
            ClassicAssert.AreEqual(5.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(7.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(9.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_SubtractionOperator_SubtractsVectorsCorrectly()
        {
            var v1 = new Vector3D(5.0, 6.0, 7.0);
            var v2 = new Vector3D(1.0, 2.0, 3.0);
            var result = v1 - v2;
            ClassicAssert.AreEqual(4.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_MultiplicationOperator_MultipliesByScalarCorrectly()
        {
            var v = new Vector3D(1.0, 2.0, 3.0);
            var scalar = 3.0;
            var result = v * scalar;
            ClassicAssert.AreEqual(3.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(6.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(9.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_DivisionOperator_DividesByScalarCorrectly()
        {
            var v = new Vector3D(6.0, 9.0, 12.0);
            var scalar = 3.0;
            var result = v / scalar;
            ClassicAssert.AreEqual(2.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(3.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_DotProductOperator_ReturnsCorrectDotProduct()
        {
            var v1 = new Vector3D(1.0, 2.0, 3.0);
            var v2 = new Vector3D(4.0, 5.0, 6.0);
            var result = v1 * v2;
            ClassicAssert.AreEqual(32.0, result, EpsilonD); // 1*4 + 2*5 + 3*6 = 4 + 10 + 18 = 32
        }

        [Test]
        public void Vector3D_CrossProductOperator_ReturnsCorrectCrossProduct()
        {
            var v1 = new Vector3D(1.0, 0.0, 0.0);
            var v2 = new Vector3D(0.0, 1.0, 0.0);
            var result = v1 ^ v2;
            ClassicAssert.AreEqual(0.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(0.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(1.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_EqualityOperator_ReturnsTrueForEqualVectors()
        {
            var v1 = new Vector3D(1.0, 2.0, 3.0);
            var v2 = new Vector3D(1.0, 2.0, 3.0);
            ClassicAssert.IsTrue(v1 == v2);
        }

        [Test]
        public void Vector3D_InequalityOperator_ReturnsTrueForDifferentVectors()
        {
            var v1 = new Vector3D(1.0, 2.0, 3.0);
            var v2 = new Vector3D(3.0, 4.0, 5.0);
            ClassicAssert.IsTrue(v1 != v2);
        }

        [Test]
        public void Vector3D_LessThanOperator_ReturnsCorrectResult()
        {
            var v1 = new Vector3D(1.0, 2.0, 3.0);
            var v2 = new Vector3D(1.0, 2.0, 4.0);
            var v3 = new Vector3D(1.0, 3.0, 2.0);
            var v4 = new Vector3D(2.0, 1.0, 1.0);
            ClassicAssert.IsTrue(v1 < v2);
            ClassicAssert.IsTrue(v1 < v3);
            ClassicAssert.IsTrue(v1 < v4);
        }

        [Test]
        public void Vector3D_GreaterThanOperator_ReturnsCorrectResult()
        {
            var v1 = new Vector3D(1.0, 2.0, 4.0);
            var v2 = new Vector3D(1.0, 2.0, 3.0);
            var v3 = new Vector3D(1.0, 3.0, 2.0);
            var v4 = new Vector3D(2.0, 1.0, 1.0);
            ClassicAssert.IsTrue(v1 > v2);
            ClassicAssert.IsTrue(v3 > v2);
            ClassicAssert.IsTrue(v4 > v2);
        }

        [Test]
        public void Vector3D_NegationOperator_NegatesVectorCorrectly()
        {
            var v = new Vector3D(1.0, 2.0, 3.0);
            var result = -v;
            ClassicAssert.AreEqual(-1.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(-2.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(-3.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_Indexer_AccessesComponentsCorrectly()
        {
            var vector = new Vector3D(1.0, 2.0, 3.0);
            ClassicAssert.AreEqual(1.0, vector[0], EpsilonD);
            ClassicAssert.AreEqual(2.0, vector[1], EpsilonD);
            ClassicAssert.AreEqual(3.0, vector[2], EpsilonD);

            vector[0] = 4.0;
            vector[1] = 5.0;
            vector[2] = 6.0;
            ClassicAssert.AreEqual(4.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(5.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(6.0, vector.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_StaticAdd_AddsVectorsCorrectly()
        {
            var a = new Vector3D(1.0, 2.0, 3.0);
            var b = new Vector3D(4.0, 5.0, 6.0);
            Vector3D.Add(in a, in b, out var result);
            ClassicAssert.AreEqual(5.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(7.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(9.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_StaticSubtract_SubtractsVectorsCorrectly()
        {
            var a = new Vector3D(5.0, 6.0, 7.0);
            var b = new Vector3D(1.0, 2.0, 3.0);
            Vector3D.Subtract(in a, in b, out var result);
            ClassicAssert.AreEqual(4.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(4.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_StaticCos_ReturnsCorrectCosine()
        {
            var from = new Vector3D(1.0, 0.0, 0.0);
            var to = new Vector3D(0.0, 1.0, 0.0);
            ClassicAssert.AreEqual(0.0, Vector3D.Cos(in from, in to), EpsilonD);

            from = new Vector3D(1.0, 0.0, 0.0);
            to = new Vector3D(1.0, 0.0, 0.0);
            ClassicAssert.AreEqual(1.0, Vector3D.Cos(in from, in to), EpsilonD);
        }

        [Test]
        public void Vector3D_StaticAngle_ReturnsCorrectAngleInDegrees()
        {
            var from = new Vector3D(1.0, 0.0, 0.0);
            var to = new Vector3D(0.0, 1.0, 0.0);
            ClassicAssert.AreEqual(90.0, Vector3D.Angle(in from, in to), 0.1);

            from = new Vector3D(1.0, 0.0, 0.0);
            to = new Vector3D(-1.0, 0.0, 0.0);
            ClassicAssert.AreEqual(180.0, Vector3D.Angle(in from, in to), 0.1);
        }

        [Test]
        public void Vector3D_StaticDistance_ReturnsCorrectDistance()
        {
            var a = new Vector3D(0.0, 0.0, 0.0);
            var b = new Vector3D(3.0, 4.0, 0.0);
            ClassicAssert.AreEqual(5.0, Vector3D.Distance(in a, in b), EpsilonD);
        }

        [Test]
        public void Vector3D_StaticDot_ReturnsCorrectDotProduct()
        {
            var a = new Vector3D(1.0, 2.0, 3.0);
            var b = new Vector3D(4.0, 5.0, 6.0);
            ClassicAssert.AreEqual(32.0, Vector3D.Dot(in a, in b), EpsilonD);
        }

        [Test]
        public void Vector3D_StaticCross_ReturnsCorrectCrossProduct()
        {
            var left = new Vector3D(1.0, 0.0, 0.0);
            var right = new Vector3D(0.0, 1.0, 0.0);
            var result = Vector3D.Cross(in left, in right);
            ClassicAssert.AreEqual(0.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(0.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(1.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_StaticLerp_InterpolatesVectorsCorrectly()
        {
            var from = new Vector3D(0.0, 0.0, 0.0);
            var to = new Vector3D(10.0, 10.0, 10.0);
            var result = Vector3D.Lerp(in from, in to, 0.5);
            ClassicAssert.AreEqual(5.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(5.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(5.0, result.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_Distance_ReturnsCorrectDistance()
        {
            var vector = new Vector3D(0.0, 0.0, 0.0);
            var other = new Vector3D(3.0, 4.0, 0.0);
            ClassicAssert.AreEqual(5.0, vector.Distance(in other), EpsilonD);
        }

        [Test]
        public void Vector3D_Dot_ReturnsCorrectDotProduct()
        {
            var vector = new Vector3D(1.0, 2.0, 3.0);
            var other = new Vector3D(4.0, 5.0, 6.0);
            ClassicAssert.AreEqual(32.0, vector.Dot(in other), EpsilonD);
        }

        [Test]
        public void Vector3D_SetMaximize_SetsComponentsToMaxValues()
        {
            var vector = new Vector3D(2.0, 3.0, 4.0);
            var vectorToMaximizeTo = new Vector3D(4.0, 1.0, 5.0);
            vector.SetMaximize(in vector, in vectorToMaximizeTo);
            ClassicAssert.AreEqual(4.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(3.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(5.0, vector.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_SetMinimize_SetsComponentsToMinValues()
        {
            var vector = new Vector3D(2.0, 3.0, 4.0);
            var vectorToMinimizeTo = new Vector3D(4.0, 1.0, 5.0);
            vector.SetMinimize(in vector, in vectorToMinimizeTo);
            ClassicAssert.AreEqual(2.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(1.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(4.0, vector.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_CrossNormalize_NormalizesCrossProduct()
        {
            var vector = new Vector3D();
            var left = new Vector3D(1.0, 0.0, 0.0);
            var right = new Vector3D(0.0, 1.0, 0.0);
            vector.CrossNormalize(in left, in right);
            ClassicAssert.AreEqual(0.0, vector.X, EpsilonD);
            ClassicAssert.AreEqual(0.0, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(1.0, vector.Z, EpsilonD);
            ClassicAssert.AreEqual(1.0, vector.Length, EpsilonD);
        }

        [Test]
        public void Vector3D_SerializeToString_ReturnsCorrectString()
        {
            var vector = new Vector3D(1.23, 4.56, 7.89);
            ClassicAssert.AreEqual("1.23;4.56;7.89", vector.SerializeToString().Replace(',', '.'));
        }

        [Test]
        public void Vector3D_DeserializeFromString_ReturnsCorrectVector()
        {
            var data = "1.23;4.56;7.89";
            var vector = Vector3D.DeserializeFromString(data);
            ClassicAssert.AreEqual(1.23, vector.X, EpsilonD);
            ClassicAssert.AreEqual(4.56, vector.Y, EpsilonD);
            ClassicAssert.AreEqual(7.89, vector.Z, EpsilonD);
        }

        [Test]
        public void Vector3D_ToVector2XY_ReturnsCorrectVector()
        {
            var vector = new Vector3D(1.0, 2.0, 3.0);
            var result = vector.ToVector2XY();
            ClassicAssert.AreEqual(1.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(2.0, result.Y, EpsilonD);
        }

        [Test]
        public void Vector3D_ToVector2XZ_ReturnsCorrectVector()
        {
            var vector = new Vector3D(1.0, 2.0, 3.0);
            var result = vector.ToVector2XZ();
            ClassicAssert.AreEqual(1.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(3.0, result.Y, EpsilonD);
        }

        [Test]
        public void Vector3D_ToVector2YZ_ReturnsCorrectVector()
        {
            var vector = new Vector3D(1.0, 2.0, 3.0);
            var result = vector.ToVector2YZ();
            ClassicAssert.AreEqual(2.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(3.0, result.Y, EpsilonD);
        }

        [Test]
        public void Vector3D_ToVector3XY_ReturnsCorrectVector()
        {
            var vector = new Vector3D(1.0, 2.0, 3.0);
            var result = vector.ToVector3XY();
            ClassicAssert.AreEqual(1.0, result.X, EpsilonD);
            ClassicAssert.AreEqual(2.0, result.Y, EpsilonD);
            ClassicAssert.AreEqual(0.0, result.Z, EpsilonD);
        }
        #endregion

        #region Vector3Df Tests
        [Test]
        public void Vector3Df_Constructor_InitializesCorrectly()
        {
            var vector = new Vector3Df(1.0f, 2.0f, 3.0f);
            ClassicAssert.AreEqual(1.0f, vector.X, EpsilonF);
            ClassicAssert.AreEqual(2.0f, vector.Y, EpsilonF);
            ClassicAssert.AreEqual(3.0f, vector.Z, EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticConstants_AreCorrect()
        {
            ClassicAssert.AreEqual(new Vector3Df(1, 1, 1), Vector3Df.One);
            ClassicAssert.AreEqual(new Vector3Df(1, 0, 0), Vector3Df.Right);
            ClassicAssert.AreEqual(new Vector3Df(-1, 0, 0), Vector3Df.Left);
            ClassicAssert.AreEqual(new Vector3Df(0, 1, 0), Vector3Df.Up);
            ClassicAssert.AreEqual(new Vector3Df(0, -1, 0), Vector3Df.Down);
            ClassicAssert.AreEqual(new Vector3Df(0, 0, 1), Vector3Df.Forward);
            ClassicAssert.AreEqual(new Vector3Df(0, 0, -1), Vector3Df.Back);
            ClassicAssert.AreEqual(new Vector3Df(0, 0, 0), Vector3Df.Zero);
        }

        [Test]
        public void Vector3Df_SqrLength_ReturnsCorrectSquaredLength()
        {
            var vector = new Vector3Df(3.0f, 4.0f, 5.0f);
            ClassicAssert.AreEqual(50.0f, vector.SqrLength, EpsilonF);
        }

        [Test]
        public void Vector3Df_Length_ReturnsCorrectLength()
        {
            var vector = new Vector3Df(3.0f, 4.0f, 0.0f);
            ClassicAssert.AreEqual(5.0f, vector.Length, EpsilonF);
        }

        [Test]
        public void Vector3Df_Normalized_ReturnsNormalizedVector()
        {
            var vector = new Vector3Df(3.0f, 4.0f, 0.0f);
            var normalized = vector.Normalized;
            ClassicAssert.AreEqual(0.6f, normalized.X, EpsilonF);
            ClassicAssert.AreEqual(0.8f, normalized.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, normalized.Z, EpsilonF);
            ClassicAssert.AreEqual(1.0f, normalized.Length, EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticAdd_AddsVectorsCorrectly()
        {
            var a = new Vector3Df(1.0f, 2.0f, 3.0f);
            var b = new Vector3Df(4.0f, 5.0f, 6.0f);
            Vector3Df.Add(in a, in b, out var result);
            ClassicAssert.AreEqual(5.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(7.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(9.0f, result.Z, EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticSubtract_SubtractsVectorsCorrectly()
        {
            var a = new Vector3Df(5.0f, 6.0f, 7.0f);
            var b = new Vector3Df(1.0f, 2.0f, 3.0f);
            Vector3Df.Subtract(in a, in b, out var result);
            ClassicAssert.AreEqual(4.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(4.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(4.0f, result.Z, EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticScale_ScalesVectorCorrectly()
        {
            var vector = new Vector3Df(1.0f, 2.0f, 3.0f);
            var scale = new Vector3Df(2.0f, 3.0f, 4.0f);
            var result = Vector3Df.Scale(vector, scale);
            ClassicAssert.AreEqual(2.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(6.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(12.0f, result.Z, EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticCos_ReturnsCorrectCosine()
        {
            var from = new Vector3Df(1.0f, 0.0f, 0.0f);
            var to = new Vector3Df(0.0f, 1.0f, 0.0f);
            ClassicAssert.AreEqual(0.0f, Vector3Df.Cos(in from, in to), EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticAngle_ReturnsCorrectAngleInDegrees()
        {
            var from = new Vector3Df(1.0f, 0.0f, 0.0f);
            var to = new Vector3Df(0.0f, 1.0f, 0.0f);
            ClassicAssert.AreEqual(90.0f, Vector3Df.Angle(in from, in to), 0.1f);
        }

        [Test]
        public void Vector3Df_StaticDistance_ReturnsCorrectDistance()
        {
            var a = new Vector3Df(0.0f, 0.0f, 0.0f);
            var b = new Vector3Df(3.0f, 4.0f, 0.0f);
            ClassicAssert.AreEqual(5.0f, Vector3Df.Distance(in a, in b), EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticNormalize_ReturnsNormalizedVector()
        {
            var vector = new Vector3Df(3.0f, 4.0f, 0.0f);
            var result = Vector3Df.Normalize(in vector);
            ClassicAssert.AreEqual(0.6f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.8f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);
            ClassicAssert.AreEqual(1.0f, result.Length, EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticDot_ReturnsCorrectDotProduct()
        {
            var a = new Vector3Df(1.0f, 2.0f, 3.0f);
            var b = new Vector3Df(4.0f, 5.0f, 6.0f);
            ClassicAssert.AreEqual(32.0f, Vector3Df.Dot(in a, in b), EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticCross_ReturnsCorrectCrossProduct()
        {
            var left = new Vector3Df(1.0f, 0.0f, 0.0f);
            var right = new Vector3Df(0.0f, 1.0f, 0.0f);
            var result = Vector3Df.Cross(in left, in right);
            ClassicAssert.AreEqual(0.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(1.0f, result.Z, EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticCross_WithOutParameter_ReturnsCorrectCrossProduct()
        {
            var left = new Vector3Df(1.0f, 0.0f, 0.0f);
            var right = new Vector3Df(0.0f, 1.0f, 0.0f);
            Vector3Df.Cross(in left, in right, out var result);
            ClassicAssert.AreEqual(0.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(1.0f, result.Z, EpsilonF);
        }

        [Test]
        public void Vector3Df_StaticApproximately_ReturnsTrueForApproximatelyEqualVectors()
        {
            var a = new Vector3Df(1.00001f, 2.00002f, 3.00003f);
            var b = new Vector3Df(1.00000f, 2.00000f, 3.00000f);
            ClassicAssert.IsTrue(Vector3Df.Approximately(in a, in b, 0.0001f));
            ClassicAssert.IsFalse(Vector3Df.Approximately(in a, in b, 0.000001f));
        }

        [Test]
        public void Vector3Df_StaticLerp_InterpolatesVectorsCorrectly()
        {
            var from = new Vector3Df(0.0f, 0.0f, 0.0f);
            var to = new Vector3Df(10.0f, 10.0f, 10.0f);
            var result = Vector3Df.Lerp(in from, in to, 0.5f);
            ClassicAssert.AreEqual(5.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(5.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(5.0f, result.Z, EpsilonF);
        }

        [Test]
        public void Vector3Df_FromSpherical_ReturnsCorrectVector()
        {
            // Для theta=90, phi=0, radius=1: x=0, y=0, z=1
            var result = Vector3Df.FromSpherical(1.0f, 90.0f, 0.0f);
            ClassicAssert.AreEqual(0.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
            ClassicAssert.AreEqual(1.0f, result.Z, 0.1f);
        }

        [Test]
        public void Vector3Df_FromGeographicCoordSystem_ReturnsCorrectVector()
        {
            // Для latitude=90, longitude=0, radius=1: x=0, y=0, z=1
            var result = Vector3Df.FromGeographicCoordSystem(1.0f, 90.0f, 0.0f);
            ClassicAssert.AreEqual(0.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
            ClassicAssert.AreEqual(1.0f, result.Z, 0.1f);
        }

        [Test]
        public void Vector3Df_GetPerpendicularPlane_ReturnsCorrectVector()
        {
            var resultXZ = Vector3Df.GetPerpendicularPlane(TDimensionPlane.XZ);
            ClassicAssert.AreEqual(Vector3Df.Up, resultXZ);

            var resultZY = Vector3Df.GetPerpendicularPlane(TDimensionPlane.ZY);
            ClassicAssert.AreEqual(Vector3Df.Right, resultZY);

            var resultXY = Vector3Df.GetPerpendicularPlane(TDimensionPlane.XY);
            ClassicAssert.AreEqual(Vector3Df.Forward, resultXY);
        }

        [Test]
        public void Vector3Df_DeserializeFromString_ReturnsCorrectVector()
        {
            var data = "1.23;4.56;7.89";
            var vector = Vector3Df.DeserializeFromString(data);
            ClassicAssert.AreEqual(1.23f, vector.X, EpsilonF);
            ClassicAssert.AreEqual(4.56f, vector.Y, EpsilonF);
            ClassicAssert.AreEqual(7.89f, vector.Z, EpsilonF);
        }
        #endregion
    }
}
