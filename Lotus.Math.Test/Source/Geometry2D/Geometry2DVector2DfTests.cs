#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Vector2DfTests
    {
        [Test]
        public void Vector2Df_Equals_OperatorOverload_ReturnsExpectedValue()
        {
            // Arrange
            var vector1 = new Vector2Df(2.0f, 3.0f);
            var vector2 = new Vector2Df(2.0f, 3.0f);
            var vector3 = new Vector2Df(1.0f, 1.0f);

            // Act
            var equals1 = vector1 == vector2;
            var equals2 = vector1 == vector3;

            // ClassicAssert
            ClassicAssert.IsTrue(equals1);
            ClassicAssert.IsFalse(equals2);
        }

        [Test]
        public void Vector2Df_NotEquals_OperatorOverload_ReturnsExpectedValue()
        {
            // Arrange
            var vector1 = new Vector2Df(2.0f, 3.0f);
            var vector2 = new Vector2Df(2.0f, 3.0f);
            var vector3 = new Vector2Df(1.0f, 1.0f);

            // Act
            var notEquals1 = vector1 != vector3;
            var notEquals2 = vector1 != vector2;

            // ClassicAssert
            ClassicAssert.IsTrue(notEquals1);
            ClassicAssert.IsFalse(notEquals2);
        }

        [Test]
        public void Vector2Df_Add_AddsVectorComponentsCorrectly()
        {
            // Arrange
            var vector1 = new Vector2Df(2.0f, 3.0f);
            var vector2 = new Vector2Df(4.0f, 1.0f);

            // Act
            vector1 += vector2;

            // ClassicAssert
            ClassicAssert.AreEqual(6.0f, vector1.X);
            ClassicAssert.AreEqual(4.0f, vector1.Y);
        }

        [Test]
        public void Vector2Df_Subtract_SubtractsVectorComponentsCorrectly()
        {
            // Arrange
            var vector1 = new Vector2Df(5.0f, 2.0f);
            var vector2 = new Vector2Df(3.0f, 1.0f);

            // Act
            vector1 -= vector2;

            // ClassicAssert
            ClassicAssert.AreEqual(2.0f, vector1.X);
            ClassicAssert.AreEqual(1.0f, vector1.Y);
        }

        [Test]
        public void Vector2Df_MultiplyByScalar_MultipliesVectorByScalar()
        {
            // Arrange
            var vector = new Vector2Df(2.0f, 3.0f);
            var scalar = 3.0f;

            // Act
            vector *= scalar;

            // ClassicAssert
            ClassicAssert.AreEqual(6.0f, vector.X);
            ClassicAssert.AreEqual(9.0f, vector.Y);
        }

        [Test]
        public void Vector2Df_DivideByScalar_DividesVectorByScalar()
        {
            // Arrange
            var vector = new Vector2Df(6.0f, 9.0f);
            var scalar = 3.0f;

            // Act
            vector /= scalar;

            // ClassicAssert
            ClassicAssert.AreEqual(2.0f, vector.X);
            ClassicAssert.AreEqual(3.0f, vector.Y);
        }

        [Test]
        public void Vector2Df_Distance_ReturnsCorrectDistance()
        {
            // Arrange
            var vector1 = new Vector2Df(0.0f, 0.0f);
            var vector2 = new Vector2Df(3.0f, 4.0f);

            // Act
            var distance = vector1.Distance(vector2);

            // ClassicAssert
            ClassicAssert.AreEqual(5.0f, distance);
        }

        [Test]
        public void Vector2Df_Dot_ReturnsCorrectDotProduct()
        {
            // Arrange
            var vector1 = new Vector2Df(2.0f, 3.0f);
            var vector2 = new Vector2Df(4.0f, 5.0f);

            // Act
            var dotProduct = vector1.Dot(vector2);

            // ClassicAssert
            ClassicAssert.AreEqual(23.0f, dotProduct);
        }

        [Test]
        public void Vector2Df_SetMaximize_SetsComponentsToMaxValues()
        {
            // Arrange
            var vector = new Vector2Df(2.0f, 3.0f);
            var vectorToMaximizeTo = new Vector2Df(4.0f, 1.0f);

            // Act
            vector.SetMaximize(vector, vectorToMaximizeTo);

            // ClassicAssert
            ClassicAssert.AreEqual(4.0f, vector.X);
            ClassicAssert.AreEqual(3.0f, vector.Y);
        }

        [Test]
        public void Vector2Df_SqrDistance_ReturnsCorrectSquaredDistance()
        {
            // Arrange
            //var vector1 = new Vector2Df(0.0f, 0.0f);
            //var vector2 = new Vector2Df(3.0f, 4.0f);

            //// Act
            //var sqrDistance = vector1.SqrLength(vector2);

            //// Assert
            //ClassicAssert.AreEqual(25.0f, sqrDistance);
        }

        [Test]
        public void Vector2Df_PerpDot_ReturnsCorrectPerpendicularDotProduct()
        {
            // Arrange
            //var vector1 = new Vector2Df(1.0f, 0.0f);
            //var vector2 = new Vector2Df(0.0f, 1.0f);

            //// Act
            //var perpDot = vector1.PerpDot(vector2);

            //// Assert
            //ClassicAssert.AreEqual(1.0f, perpDot);
        }

        [Test]
        public void Vector2Df_Normalized_ReturnsNormalizedVector()
        {
            // Arrange
            var vector = new Vector2Df(3.0f, 4.0f);

            // Act
            var normalized = vector.Normalized;

            // Assert
            ClassicAssert.AreEqual(1.0f, normalized.Length, 0.001f);
            ClassicAssert.AreEqual(0.6f, normalized.X, 0.001f);
            ClassicAssert.AreEqual(0.8f, normalized.Y, 0.001f);
        }

        [Test]
        public void Vector2Df_Normalize_NormalizesVector()
        {
            // Arrange
            var vector = new Vector2Df(3.0f, 4.0f);

            // Act
            vector.Normalize();

            // Assert
            ClassicAssert.AreEqual(1.0f, vector.Length, 0.001f);
        }

        [Test]
        public void Vector2Df_SerializeToString_ReturnsCorrectString()
        {
            // Arrange
            var vector = new Vector2Df(2.5f, 3.7f);

            // Act
            var result = vector.SerializeToString();

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.IsTrue(result.Contains("2.5") || result.Contains("2,5"));
            ClassicAssert.IsTrue(result.Contains("3.7") || result.Contains("3,7"));
        }

        [Test]
        public void Vector2Df_DeserializeFromString_ReturnsCorrectVector()
        {
            // Arrange
            var data = "2.5; 3.7";

            // Act
            var result = Vector2Df.DeserializeFromString(data);

            // Assert
            ClassicAssert.AreEqual(2.5f, result.X, 0.001f);
            ClassicAssert.AreEqual(3.7f, result.Y, 0.001f);
        }

        [Test]
        public void Vector2Df_StaticDot_ReturnsCorrectDotProduct()
        {
            // Arrange
            var vector1 = new Vector2Df(2.0f, 3.0f);
            var vector2 = new Vector2Df(4.0f, 5.0f);

            // Act
            var result = Vector2Df.Dot(in vector1, in vector2);

            // Assert
            ClassicAssert.AreEqual(23.0f, result);
        }

        [Test]
        public void Vector2Df_StaticDistance_ReturnsCorrectDistance()
        {
            // Arrange
            var vector1 = new Vector2Df(0.0f, 0.0f);
            var vector2 = new Vector2Df(3.0f, 4.0f);

            // Act
            var result = Vector2Df.Distance(in vector1, in vector2);

            // Assert
            ClassicAssert.AreEqual(5.0f, result);
        }

        [Test]
        public void Vector2Df_StaticLerp_ReturnsInterpolatedVector()
        {
            // Arrange
            var from = new Vector2Df(0.0f, 0.0f);
            var to = new Vector2Df(10.0f, 20.0f);
            var time = 0.5f;

            // Act
            var result = Vector2Df.Lerp(in from, in to, time);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X);
            ClassicAssert.AreEqual(10.0f, result.Y);
        }

        [Test]
        public void Vector2Df_StaticMax_ReturnsMaxComponents()
        {
            // Arrange
            var vector1 = new Vector2Df(2.0f, 5.0f);
            var vector2 = new Vector2Df(4.0f, 3.0f);

            // Act
            var result = Vector2Df.Max(in vector1, in vector2);

            // Assert
            ClassicAssert.AreEqual(4.0f, result.X);
            ClassicAssert.AreEqual(5.0f, result.Y);
        }

        [Test]
        public void Vector2Df_StaticMin_ReturnsMinComponents()
        {
            // Arrange
            var vector1 = new Vector2Df(2.0f, 5.0f);
            var vector2 = new Vector2Df(4.0f, 3.0f);

            // Act
            var result = Vector2Df.Min(in vector1, in vector2);

            // Assert
            ClassicAssert.AreEqual(2.0f, result.X);
            ClassicAssert.AreEqual(3.0f, result.Y);
        }

        [Test]
        public void Vector2Df_StaticReflect_ReturnsReflectedVector()
        {
            // Arrange
            var vector = new Vector2Df(1.0f, 0.0f);
            var normal = new Vector2Df(0.0f, 1.0f);

            // Act
            var result = Vector2Df.Reflect(in vector, in normal);

            // Assert
            ClassicAssert.AreEqual(1.0f, result.X, 0.001f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.001f);
        }

        [Test]
        public void Vector2Df_StaticApproximately_WithSimilarVectors_ReturnsTrue()
        {
            // Arrange
            var vector1 = new Vector2Df(2.0f, 3.0f);
            var vector2 = new Vector2Df(2.001f, 3.001f);

            // Act
            var result = Vector2Df.Approximately(in vector1, in vector2, 0.01f);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Vector2Df_StaticApproximately_WithDifferentVectors_ReturnsFalse()
        {
            // Arrange
            var vector1 = new Vector2Df(2.0f, 3.0f);
            var vector2 = new Vector2Df(5.0f, 6.0f);

            // Act
            var result = Vector2Df.Approximately(in vector1, in vector2, 0.01f);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Vector2D_StaticDot_ReturnsCorrectDotProduct()
        {
            // Arrange
            var vector1 = new Vector2D(2.0, 3.0);
            var vector2 = new Vector2D(4.0, 5.0);

            // Act
            var result = Vector2D.Dot(in vector1, in vector2);

            // Assert
            ClassicAssert.AreEqual(23.0, result);
        }

        [Test]
        public void Vector2D_StaticDistance_ReturnsCorrectDistance()
        {
            // Arrange
            var vector1 = new Vector2D(0.0, 0.0);
            var vector2 = new Vector2D(3.0, 4.0);

            // Act
            var result = Vector2D.Distance(in vector1, in vector2);

            // Assert
            ClassicAssert.AreEqual(5.0, result);
        }

        [Test]
        public void Vector2D_Length_ReturnsCorrectLength()
        {
            // Arrange
            var vector = new Vector2D(3.0, 4.0);

            // Act
            var result = vector.Length;

            // Assert
            ClassicAssert.AreEqual(5.0, result);
        }

        [Test]
        public void Vector2D_SqrLength_ReturnsCorrectSquaredLength()
        {
            // Arrange
            var vector = new Vector2D(3.0, 4.0);

            // Act
            var result = vector.SqrLength;

            // Assert
            ClassicAssert.AreEqual(25.0, result);
        }

        [Test]
        public void Vector2D_Normalized_ReturnsNormalizedVector()
        {
            // Arrange
            var vector = new Vector2D(3.0, 4.0);

            // Act
            var normalized = vector.Normalized;

            // Assert
            ClassicAssert.AreEqual(1.0, normalized.Length, 0.001);
            ClassicAssert.AreEqual(0.6, normalized.X, 0.001);
            ClassicAssert.AreEqual(0.8, normalized.Y, 0.001);
        }

        [Test]
        public void Vector2D_StaticLerp_ReturnsInterpolatedVector()
        {
            // Arrange
            var from = new Vector2D(0.0, 0.0);
            var to = new Vector2D(10.0, 20.0);
            var time = 0.5;

            // Act
            var result = Vector2D.Lerp(in from, in to, time);

            // Assert
            ClassicAssert.AreEqual(5.0, result.X);
            ClassicAssert.AreEqual(10.0, result.Y);
        }

        [Test]
        public void Vector2D_StaticAngle_ReturnsCorrectAngle()
        {
            // Arrange
            var from = new Vector2D(1.0, 0.0);
            var to = new Vector2D(0.0, 1.0);

            // Act
            var result = Vector2D.Angle(in from, in to);

            // Assert
            ClassicAssert.AreEqual(90.0, result, 0.1);
        }

        [Test]
        public void Vector2D_StaticCos_ReturnsCorrectCosine()
        {
            // Arrange
            var from = new Vector2D(1.0, 0.0);
            var to = new Vector2D(0.0, 1.0);

            // Act
            var result = Vector2D.Cos(in from, in to);

            // Assert
            ClassicAssert.AreEqual(0.0, result, 0.001);
        }

        [Test]
        public void Vector2D_DeserializeFromString_ReturnsCorrectVector()
        {
            // Arrange
            var data = "2.5; 3.7";

            // Act
            var result = Vector2D.DeserializeFromString(data);

            // Assert
            ClassicAssert.AreEqual(2.5, result.X, 0.001);
            ClassicAssert.AreEqual(3.7, result.Y, 0.001);
        }
    }
}