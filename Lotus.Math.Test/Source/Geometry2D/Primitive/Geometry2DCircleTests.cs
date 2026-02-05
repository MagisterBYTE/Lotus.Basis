using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DCircleTests
    {
        [Test]
        public void Circle2Df_Constructor_WithCenterAndRadius_InitializesCorrectly()
        {
            // Arrange
            var center = new Vector2Df(10.0f, 20.0f);
            var radius = 5.0f;

            // Act
            var circle = new Circle2Df(center, radius);

            // Assert
            ClassicAssert.AreEqual(10.0f, circle.Center.X);
            ClassicAssert.AreEqual(20.0f, circle.Center.Y);
            ClassicAssert.AreEqual(5.0f, circle.Radius);
        }

        [Test]
        public void Circle2Df_Diameter_ReturnsCorrectDiameter()
        {
            // Arrange
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = circle.Diameter;

            // Assert
            ClassicAssert.AreEqual(10.0f, result);
        }

        [Test]
        public void Circle2Df_Diameter_Set_UpdatesRadius()
        {
            // Arrange
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            circle.Diameter = 20.0f;

            // Assert
            ClassicAssert.AreEqual(10.0f, circle.Radius);
        }

        [Test]
        public void Circle2Df_Circumference_ReturnsCorrectCircumference()
        {
            // Arrange
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = circle.Circumference;

            // Assert
            ClassicAssert.AreEqual(XMath.PI2_F * 5.0f, result, 0.001f);
        }

        [Test]
        public void Circle2Df_Circumference_Set_UpdatesRadius()
        {
            // Arrange
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var expectedRadius = 10.0f / XMath.PI2_F;

            // Act
            circle.Circumference = 10.0f;

            // Assert
            ClassicAssert.AreEqual(expectedRadius, circle.Radius, 0.001f);
        }

        [Test]
        public void Circle2Df_Area_ReturnsCorrectArea()
        {
            // Arrange
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = circle.Area;

            // Assert
            ClassicAssert.AreEqual(XMath.PI_F * 25.0f, result, 0.001f);
        }

        [Test]
        public void Circle2Df_Area_Set_UpdatesRadius()
        {
            // Arrange
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var expectedArea = XMath.PI_F * 100.0f; // radius = 10

            // Act
            circle.Area = expectedArea;

            // Assert
            ClassicAssert.AreEqual(10.0f, circle.Radius, 0.1f);
        }

        [Test]
        public void Circle2Df_Contains_WithPointOnCircle_ReturnsTrue()
        {
            // Arrange
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var point = new Vector2Df(5.0f, 0.0f);

            // Act
            var result = circle.Contains(in point);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Circle2Df_Contains_WithPointInside_ReturnsFalse()
        {
            // Arrange
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var point = new Vector2Df(2.0f, 0.0f);

            // Act
            var result = circle.Contains(in point);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Circle2Df_Contains_WithPointOutside_ReturnsFalse()
        {
            // Arrange
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var point = new Vector2Df(10.0f, 0.0f);

            // Act
            var result = circle.Contains(in point);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Circle2Df_EqualityOperator_WithEqualCircles_ReturnsTrue()
        {
            // Arrange
            var circle1 = new Circle2Df(new Vector2Df(10.0f, 20.0f), 5.0f);
            var circle2 = new Circle2Df(new Vector2Df(10.0f, 20.0f), 5.0f);

            // Act
            var result = circle1 == circle2;

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Circle2Df_EqualityOperator_WithDifferentCircles_ReturnsFalse()
        {
            // Arrange
            var circle1 = new Circle2Df(new Vector2Df(10.0f, 20.0f), 5.0f);
            var circle2 = new Circle2Df(new Vector2Df(10.0f, 20.0f), 10.0f);

            // Act
            var result = circle1 == circle2;

            // Assert
            ClassicAssert.IsFalse(result);
        }
    }
}
