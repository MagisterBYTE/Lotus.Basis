using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DRayTests
    {
        [Test]
        public void Ray2Df_Constructor_WithPositionAndDirection_InitializesCorrectly()
        {
            // Arrange
            var position = new Vector2Df(10.0f, 20.0f);
            var direction = new Vector2Df(1.0f, 0.0f);

            // Act
            var ray = new Ray2Df(position, direction);

            // Assert
            ClassicAssert.AreEqual(10.0f, ray.Position.X);
            ClassicAssert.AreEqual(20.0f, ray.Position.Y);
            ClassicAssert.AreEqual(1.0f, ray.Direction.X);
            ClassicAssert.AreEqual(0.0f, ray.Direction.Y);
        }

        [Test]
        public void Ray2Df_GetPoint_ReturnsCorrectPoint()
        {
            // Arrange
            var ray = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var position = 5.0f;

            // Act
            var result = ray.GetPoint(position);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X);
            ClassicAssert.AreEqual(0.0f, result.Y);
        }

        [Test]
        public void Ray2Df_GetPoint_WithNegativePosition_ReturnsPointBehindOrigin()
        {
            // Arrange
            var ray = new Ray2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(1.0f, 0.0f));
            var position = -5.0f;

            // Act
            var result = ray.GetPoint(position);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X);
            ClassicAssert.AreEqual(20.0f, result.Y);
        }

        [Test]
        public void Ray2Df_SetFromPoint_SetsCorrectPositionAndDirection()
        {
            // Arrange
            var ray = new Ray2Df();
            var startPoint = new Vector2Df(0.0f, 0.0f);
            var endPoint = new Vector2Df(10.0f, 0.0f);

            // Act
            ray.SetFromPoint(in startPoint, in endPoint);

            // Assert
            ClassicAssert.AreEqual(0.0f, ray.Position.X);
            ClassicAssert.AreEqual(0.0f, ray.Position.Y);
            ClassicAssert.AreEqual(1.0f, ray.Direction.X, 0.001f);
            ClassicAssert.AreEqual(0.0f, ray.Direction.Y, 0.001f);
        }

        [Test]
        public void Ray2Df_NegateOperator_ReturnsReversedDirection()
        {
            // Arrange
            var ray = new Ray2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = -ray;

            // Assert
            ClassicAssert.AreEqual(10.0f, result.Position.X);
            ClassicAssert.AreEqual(20.0f, result.Position.Y);
            ClassicAssert.AreEqual(-1.0f, result.Direction.X);
            ClassicAssert.AreEqual(0.0f, result.Direction.Y);
        }

        [Test]
        public void Ray2Df_EqualityOperator_WithEqualRays_ReturnsTrue()
        {
            // Arrange
            var ray1 = new Ray2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(1.0f, 0.0f));
            var ray2 = new Ray2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = ray1 == ray2;

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Ray2Df_EqualityOperator_WithDifferentRays_ReturnsFalse()
        {
            // Arrange
            var ray1 = new Ray2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(1.0f, 0.0f));
            var ray2 = new Ray2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(0.0f, 1.0f));

            // Act
            var result = ray1 == ray2;

            // Assert
            ClassicAssert.IsFalse(result);
        }
    }
}
