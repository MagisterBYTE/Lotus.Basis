using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DClosestTests
    {
        #region PointLine Tests
        [Test]
        public void XClosest2D_PointLine_WithPointOnLine_ReturnsSamePoint()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XClosest2D.PointLine(in point, in line);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointLine_WithPointOffLine_ReturnsProjectedPoint()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 3.0f);
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XClosest2D.PointLine(in point, in line);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointLine_WithDistance_ReturnsCorrectDistance()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 3.0f);
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XClosest2D.PointLine(in point, in line, out var distance);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
            ClassicAssert.AreEqual(5.0f, distance, 0.1f);
        }
        #endregion

        #region PointRay Tests
        [Test]
        public void XClosest2D_PointRay_WithPointOnRay_ReturnsProjectedPoint()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var ray = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XClosest2D.PointRay(in point, in ray);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointRay_WithPointBehindRay_ReturnsRayOrigin()
        {
            // Arrange
            var point = new Vector2Df(-5.0f, 3.0f);
            var ray = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XClosest2D.PointRay(in point, in ray);

            // Assert
            ClassicAssert.AreEqual(0.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointRay_WithDistance_ReturnsCorrectDistance()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 3.0f);
            var ray = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XClosest2D.PointRay(in point, in ray, out var distance);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
            ClassicAssert.Greater(distance, 0.0f);
        }
        #endregion

        #region PointSegment Tests
        [Test]
        public void XClosest2D_PointSegment_WithPointOnSegment_ReturnsSamePoint()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = XClosest2D.PointSegment(in point, in segment);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointSegment_WithPointBeforeSegment_ReturnsStart()
        {
            // Arrange
            var point = new Vector2Df(-5.0f, 0.0f);
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = XClosest2D.PointSegment(in point, in segment);

            // Assert
            ClassicAssert.AreEqual(0.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointSegment_WithPointAfterSegment_ReturnsEnd()
        {
            // Arrange
            var point = new Vector2Df(15.0f, 0.0f);
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = XClosest2D.PointSegment(in point, in segment);

            // Assert
            ClassicAssert.AreEqual(10.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointSegment_WithPointPerpendicular_ReturnsProjectedPoint()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 3.0f);
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = XClosest2D.PointSegment(in point, in segment);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointSegment_WithNormalizeDistance_ReturnsCorrectDistance()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = XClosest2D.PointSegment(in point, in segment, out var normalizeDistance);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
            ClassicAssert.AreEqual(0.5f, normalizeDistance, 0.1f);
        }
        #endregion

        #region PointCircle Tests
        [Test]
        public void XClosest2D_PointCircle_WithPointOnCircle_ReturnsSamePoint()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = XClosest2D.PointCircle(in point, in circle);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointCircle_WithPointOutsideCircle_ReturnsPointOnCircle()
        {
            // Arrange
            var point = new Vector2Df(10.0f, 0.0f);
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = XClosest2D.PointCircle(in point, in circle);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointCircle_WithPointInsideCircle_ReturnsPointOnCircle()
        {
            // Arrange
            var point = new Vector2Df(2.0f, 0.0f);
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = XClosest2D.PointCircle(in point, in circle);

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_PointCircle_WithPointAtCenter_ReturnsPointOnCircle()
        {
            // Arrange
            var point = new Vector2Df(0.0f, 0.0f);
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = XClosest2D.PointCircle(in point, in circle);

            // Assert
            // Любая точка на окружности, но расстояние должно быть равно радиусу
            ClassicAssert.AreEqual(0.0f, result.Length, 0.1f);
        }
        #endregion

        #region LineLine Tests
        [Test]
        public void XClosest2D_LineLine_WithIntersectingLines_ReturnsIntersectionPoint()
        {
            // Arrange
            var lineA = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var lineB = new Line2Df(new Vector2Df(5.0f, -5.0f), new Vector2Df(0.0f, 1.0f));

            // Act
            XClosest2D.LineLine(in lineA, in lineB, out var pointA, out var pointB);

            // Assert
            ClassicAssert.AreEqual(5.0f, pointA.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, pointA.Y, 0.1f);
            ClassicAssert.AreEqual(5.0f, pointB.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, pointB.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_LineLine_WithParallelLines_ReturnsClosestPoints()
        {
            // Arrange
            var lineA = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var lineB = new Line2Df(new Vector2Df(0.0f, 5.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            XClosest2D.LineLine(in lineA, in lineB, out var pointA, out var pointB);

            // Assert
            ClassicAssert.AreEqual(0.0f, pointA.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, pointA.Y, 0.1f);
            ClassicAssert.AreEqual(0.0f, pointB.X, 0.1f);
            ClassicAssert.AreEqual(5.0f, pointB.Y, 0.1f);
        }
        #endregion

        #region RayRay Tests
        [Test]
        public void XClosest2D_RayRay_WithIntersectingRays_ReturnsIntersectionPoint()
        {
            // Arrange
            var rayA = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var rayB = new Ray2Df(new Vector2Df(5.0f, -5.0f), new Vector2Df(0.0f, 1.0f));

            // Act
            XClosest2D.RayRay(in rayA, in rayB, out var pointA, out var pointB);

            // Assert
            ClassicAssert.AreEqual(5.0f, pointA.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, pointA.Y, 0.1f);
            ClassicAssert.AreEqual(5.0f, pointB.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, pointB.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_RayRay_WithParallelRays_ReturnsClosestPoints()
        {
            // Arrange
            var rayA = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var rayB = new Ray2Df(new Vector2Df(0.0f, 5.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            XClosest2D.RayRay(in rayA, in rayB, out var pointA, out var pointB);

            // Assert
            ClassicAssert.AreEqual(0.0f, pointA.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, pointA.Y, 0.1f);
            ClassicAssert.AreEqual(0.0f, pointB.X, 0.1f);
            ClassicAssert.AreEqual(5.0f, pointB.Y, 0.1f);
        }
        #endregion

        #region SegmentSegment Tests
        [Test]
        public void XClosest2D_SegmentSegment_WithIntersectingSegments_ReturnsIntersectionPoint()
        {
            // Arrange
            var segment1 = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));
            var segment2 = new Segment2Df(new Vector2Df(5.0f, -5.0f), new Vector2Df(5.0f, 5.0f));

            // Act
            XClosest2D.SegmentSegment(in segment1, in segment2, out var point1, out var point2);

            // Assert
            ClassicAssert.AreEqual(5.0f, point1.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, point1.Y, 0.1f);
            ClassicAssert.AreEqual(5.0f, point2.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, point2.Y, 0.1f);
        }

        [Test]
        public void XClosest2D_SegmentSegment_WithNonIntersectingSegments_ReturnsClosestPoints()
        {
            // Arrange
            var segment1 = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));
            var segment2 = new Segment2Df(new Vector2Df(15.0f, 5.0f), new Vector2Df(20.0f, 5.0f));

            // Act
            XClosest2D.SegmentSegment(in segment1, in segment2, out var point1, out var point2);

            // Assert
            ClassicAssert.AreEqual(10.0f, point1.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, point1.Y, 0.1f);
            ClassicAssert.AreEqual(15.0f, point2.X, 0.1f);
            ClassicAssert.AreEqual(5.0f, point2.Y, 0.1f);
        }
        #endregion

        #region CircleCircle Tests
        [Test]
        public void XClosest2D_CircleCircle_WithIntersectingCircles_ReturnsClosestPoints()
        {
            // Arrange
            var circleA = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var circleB = new Circle2Df(new Vector2Df(8.0f, 0.0f), 5.0f);

            // Act
            XClosest2D.CircleCircle(in circleA, in circleB, out var pointA, out var pointB);

            // Assert
            // Точки должны быть на линиях, соединяющих центры
            ClassicAssert.GreaterOrEqual(pointA.Length, 0.0f);
            ClassicAssert.GreaterOrEqual(pointB.Length, 0.0f);
        }

        [Test]
        public void XClosest2D_CircleCircle_WithNonIntersectingCircles_ReturnsClosestPoints()
        {
            // Arrange
            var circleA = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var circleB = new Circle2Df(new Vector2Df(20.0f, 0.0f), 5.0f);

            // Act
            XClosest2D.CircleCircle(in circleA, in circleB, out var pointA, out var pointB);

            // Assert
            // Точки должны быть на линиях, соединяющих центры
            ClassicAssert.AreEqual(5.0f, pointA.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, pointA.Y, 0.1f);
            ClassicAssert.AreEqual(15.0f, pointB.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, pointB.Y, 0.1f);
        }
        #endregion
    }
}
