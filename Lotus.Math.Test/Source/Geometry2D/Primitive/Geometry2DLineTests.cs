using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DLineTests
    {
        [Test]
        public void Line2Df_Constructor_WithPositionAndDirection_InitializesCorrectly()
        {
            // Arrange
            var position = new Vector2Df(10.0f, 20.0f);
            var direction = new Vector2Df(1.0f, 0.0f);

            // Act
            var line = new Line2Df(position, direction);

            // Assert
            ClassicAssert.AreEqual(10.0f, line.Position.X);
            ClassicAssert.AreEqual(20.0f, line.Position.Y);
            ClassicAssert.AreEqual(1.0f, line.Direction.X);
            ClassicAssert.AreEqual(0.0f, line.Direction.Y);
        }

        [Test]
        public void Line2Df_GetPoint_ReturnsCorrectPoint()
        {
            // Arrange
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var position = 5.0f;

            // Act
            var result = line.GetPoint(position);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X);
            ClassicAssert.AreEqual(0.0f, result.Y);
        }

        [Test]
        public void Line2Df_GetPoint_WithNegativePosition_ReturnsCorrectPoint()
        {
            // Arrange
            var line = new Line2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(1.0f, 0.0f));
            var position = -5.0f;

            // Act
            var result = line.GetPoint(position);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X);
            ClassicAssert.AreEqual(20.0f, result.Y);
        }

        [Test]
        public void Line2Df_SetFromPoint_SetsCorrectPositionAndDirection()
        {
            // Arrange
            var line = new Line2Df();
            var startPoint = new Vector2Df(0.0f, 0.0f);
            var endPoint = new Vector2Df(10.0f, 0.0f);

            // Act
            line.SetFromPoint(in startPoint, in endPoint);

            // Assert
            ClassicAssert.AreEqual(0.0f, line.Position.X);
            ClassicAssert.AreEqual(0.0f, line.Position.Y);
            ClassicAssert.AreEqual(1.0f, line.Direction.X, 0.001f);
            ClassicAssert.AreEqual(0.0f, line.Direction.Y, 0.001f);
        }

        [Test]
        public void Line2Df_EqualityOperator_WithEqualLines_ReturnsTrue()
        {
            // Arrange
            var line1 = new Line2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(1.0f, 0.0f));
            var line2 = new Line2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = line1 == line2;

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Line2Df_EqualityOperator_WithDifferentLines_ReturnsFalse()
        {
            // Arrange
            var line1 = new Line2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(1.0f, 0.0f));
            var line2 = new Line2Df(new Vector2Df(10.0f, 20.0f), new Vector2Df(0.0f, 1.0f));

            // Act
            var result = line1 == line2;

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Line2Df_XAxis_IsCorrect()
        {
            // Arrange & Act
            var xAxis = Line2Df.XAxis;

            // Assert
            ClassicAssert.AreEqual(0.0f, xAxis.Position.X);
            ClassicAssert.AreEqual(0.0f, xAxis.Position.Y);
            ClassicAssert.AreEqual(1.0f, xAxis.Direction.X);
            ClassicAssert.AreEqual(0.0f, xAxis.Direction.Y);
        }

        [Test]
        public void Line2Df_YAxis_IsCorrect()
        {
            // Arrange & Act
            var yAxis = Line2Df.YAxis;

            // Assert
            ClassicAssert.AreEqual(0.0f, yAxis.Position.X);
            ClassicAssert.AreEqual(0.0f, yAxis.Position.Y);
            ClassicAssert.AreEqual(0.0f, yAxis.Direction.X);
            ClassicAssert.AreEqual(1.0f, yAxis.Direction.Y);
        }
    }
}
