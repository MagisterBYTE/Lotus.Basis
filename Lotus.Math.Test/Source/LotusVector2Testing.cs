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
            bool equals1 = vector1 == vector2;
            bool equals2 = vector1 == vector3;

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
            bool notEquals1 = vector1 != vector3;
            bool notEquals2 = vector1 != vector2;

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
    }
}