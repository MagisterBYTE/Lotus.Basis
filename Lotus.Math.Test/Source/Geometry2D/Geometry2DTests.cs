using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DTests
    {
        [Test]
        public void XGeometry2D_PointOnCircle_WithZeroAngle_ReturnsPointOnXAxis()
        {
            // Arrange
            var radius = 5.0f;
            var angle = 0.0f;

            // Act
            var result = XGeometry2D.PointOnCircle(radius, angle);

            // Assert
            ClassicAssert.AreEqual(0.0f, result.X, 0.001f);
            ClassicAssert.AreEqual(5.0f, result.Y, 0.001f);
        }

        [Test]
        public void XGeometry2D_PointOnCircle_With90Degrees_ReturnsPointOnYAxis()
        {
            // Arrange
            var radius = 5.0f;
            var angle = 90.0f;

            // Act
            var result = XGeometry2D.PointOnCircle(radius, angle);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XGeometry2D_PointOnCircle_With180Degrees_ReturnsPointOnNegativeXAxis()
        {
            // Arrange
            var radius = 5.0f;
            var angle = 180.0f;

            // Act
            var result = XGeometry2D.PointOnCircle(radius, angle);

            // Assert
            ClassicAssert.AreEqual(0.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(-5.0f, result.Y, 0.1f);
        }

        [Test]
        public void XGeometry2D_PointOnCircle_With270Degrees_ReturnsPointOnNegativeYAxis()
        {
            // Arrange
            var radius = 5.0f;
            var angle = 270.0f;

            // Act
            var result = XGeometry2D.PointOnCircle(radius, angle);

            // Assert
            ClassicAssert.AreEqual(-5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XGeometry2D_PointOnCircle_With45Degrees_ReturnsCorrectPoint()
        {
            // Arrange
            var radius = 5.0f;
            var angle = 45.0f;

            // Act
            var result = XGeometry2D.PointOnCircle(radius, angle);

            // Assert
            var expectedX = radius * XMath.Sin(45.0f * XMath.DegreeToRadian_F);
            var expectedY = radius * XMath.Cos(45.0f * XMath.DegreeToRadian_F);
            ClassicAssert.AreEqual(expectedX, result.X, 0.001f);
            ClassicAssert.AreEqual(expectedY, result.Y, 0.001f);
        }

        [Test]
        public void XGeometry2D_PointOnCircle_WithUnitRadius_ReturnsUnitLength()
        {
            // Arrange
            var radius = 1.0f;
            var angle = 30.0f;

            // Act
            var result = XGeometry2D.PointOnCircle(radius, angle);

            // Assert
            ClassicAssert.AreEqual(1.0f, result.Length, 0.001f);
        }

        [Test]
        public void XGeometry2D_PointsOnCircle_WithFourSegments_ReturnsFourPoints()
        {
            // Arrange
            var radius = 5.0f;
            var segments = 4;

            // Act
            var result = XGeometry2D.PointsOnCircle(radius, segments);

            // Assert
            ClassicAssert.AreEqual(4, result.Count);
        }

        [Test]
        public void XGeometry2D_PointsOnCircle_WithFourSegments_ReturnsPointsAtCorrectAngles()
        {
            // Arrange
            var radius = 5.0f;
            var segments = 4;

            // Act
            var result = XGeometry2D.PointsOnCircle(radius, segments);

            // Assert
            ClassicAssert.AreEqual(4, result.Count);
            // Первая точка должна быть на 0 градусов
            ClassicAssert.AreEqual(0.0f, result[0].X, 0.1f);
            ClassicAssert.AreEqual(5.0f, result[0].Y, 0.1f);
        }

        [Test]
        public void XGeometry2D_PointsOnCircle_WithEightSegments_ReturnsEightPoints()
        {
            // Arrange
            var radius = 5.0f;
            var segments = 8;

            // Act
            var result = XGeometry2D.PointsOnCircle(radius, segments);

            // Assert
            ClassicAssert.AreEqual(8, result.Count);
        }

        [Test]
        public void XGeometry2D_PointsOnCircle_AllPointsHaveCorrectRadius()
        {
            // Arrange
            var radius = 5.0f;
            var segments = 8;

            // Act
            var result = XGeometry2D.PointsOnCircle(radius, segments);

            // Assert
            foreach (var point in result)
            {
                ClassicAssert.AreEqual(radius, point.Length, 0.1f);
            }
        }

        [Test]
        public void XGeometry2D_PointsOnCircle_WithZeroRadius_ReturnsZeroPoints()
        {
            // Arrange
            var radius = 0.0f;
            var segments = 4;

            // Act
            var result = XGeometry2D.PointsOnCircle(radius, segments);

            // Assert
            ClassicAssert.AreEqual(4, result.Count);
            foreach (var point in result)
            {
                ClassicAssert.AreEqual(0.0f, point.X);
                ClassicAssert.AreEqual(0.0f, point.Y);
            }
        }

        [Test]
        public void XGeometry2D_MapUVTopLeft_IsCorrect()
        {
            // Arrange & Act
            var result = XGeometry2D.MapUVTopLeft;

            // Assert
            ClassicAssert.AreEqual(0.0f, result.X);
            ClassicAssert.AreEqual(1.0f, result.Y);
        }

        [Test]
        public void XGeometry2D_MapUVMiddleCenter_IsCorrect()
        {
            // Arrange & Act
            var result = XGeometry2D.MapUVMiddleCenter;

            // Assert
            ClassicAssert.AreEqual(0.5f, result.X);
            ClassicAssert.AreEqual(0.5f, result.Y);
        }

        [Test]
        public void XGeometry2D_MapUVBottomRight_IsCorrect()
        {
            // Arrange & Act
            var result = XGeometry2D.MapUVBottomRight;

            // Assert
            ClassicAssert.AreEqual(1.0f, result.X);
            ClassicAssert.AreEqual(0.0f, result.Y);
        }
    }
}
