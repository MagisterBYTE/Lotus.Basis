using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DDistanceTests
    {
        #region PointLine Tests
        [Test]
        public void XDistance2D_PointLine_WithPointOnLine_ReturnsZero()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XDistance2D.PointLine(in point, in line);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.001f);
        }

        [Test]
        public void XDistance2D_PointLine_WithPointOffLine_ReturnsCorrectDistance()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 3.0f);
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XDistance2D.PointLine(in point, in line);

            // Assert
            ClassicAssert.AreEqual(3.0f, result, 0.1f);
        }

        [Test]
        public void XDistance2D_PointLine_WithPointAndLineComponents_ReturnsCorrectDistance()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 3.0f);
            var linePos = new Vector2Df(0.0f, 0.0f);
            var lineDir = new Vector2Df(1.0f, 0.0f);

            // Act
            var result = XDistance2D.PointLine(in point, in linePos, in lineDir);

            // Assert
            ClassicAssert.AreEqual(3.0f, result, 0.1f);
        }
        #endregion

        #region PointRay Tests
        [Test]
        public void XDistance2D_PointRay_WithPointOnRay_ReturnsZero()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var ray = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XDistance2D.PointRay(in point, in ray);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.001f);
        }

        [Test]
        public void XDistance2D_PointRay_WithPointBehindRay_ReturnsDistanceToOrigin()
        {
            // Arrange
            var point = new Vector2Df(-5.0f, 3.0f);
            var ray = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XDistance2D.PointRay(in point, in ray);

            // Assert
            var expectedDistance = new Vector2Df(-5.0f, 3.0f).Length;
            ClassicAssert.AreEqual(expectedDistance, result, 0.1f);
        }
        #endregion

        #region PointSegment Tests
        [Test]
        public void XDistance2D_PointSegment_WithPointOnSegment_ReturnsZero()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = XDistance2D.PointSegment(in point, in segment);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.001f);
        }

        [Test]
        public void XDistance2D_PointSegment_WithPointOutsideSegment_ReturnsDistanceToEndpoint()
        {
            // Arrange
            var point = new Vector2Df(15.0f, 0.0f);
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = XDistance2D.PointSegment(in point, in segment);

            // Assert
            ClassicAssert.AreEqual(5.0f, result, 0.1f);
        }

        [Test]
        public void XDistance2D_PointSegment_WithPointPerpendicularToSegment_ReturnsCorrectDistance()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 3.0f);
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = XDistance2D.PointSegment(in point, in segment);

            // Assert
            ClassicAssert.AreEqual(3.0f, result, 0.1f);
        }
        #endregion

        #region PointCircle Tests
        [Test]
        public void XDistance2D_PointCircle_WithPointOnCircle_ReturnsZero()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = XDistance2D.PointCircle(in point, in circle);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.1f);
        }

        [Test]
        public void XDistance2D_PointCircle_WithPointInsideCircle_ReturnsNegativeDistance()
        {
            // Arrange
            var point = new Vector2Df(2.0f, 0.0f);
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = XDistance2D.PointCircle(in point, in circle);

            // Assert
            ClassicAssert.AreEqual(-3.0f, result, 0.1f);
        }

        [Test]
        public void XDistance2D_PointCircle_WithPointOutsideCircle_ReturnsPositiveDistance()
        {
            // Arrange
            var point = new Vector2Df(10.0f, 0.0f);
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = XDistance2D.PointCircle(in point, in circle);

            // Assert
            ClassicAssert.AreEqual(5.0f, result, 0.1f);
        }
        #endregion

        #region LineLine Tests
        [Test]
        public void XDistance2D_LineLine_WithIntersectingLines_ReturnsZero()
        {
            // Arrange
            var lineA = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var lineB = new Line2Df(new Vector2Df(5.0f, -5.0f), new Vector2Df(0.0f, 1.0f));

            // Act
            var result = XDistance2D.LineLine(in lineA, in lineB);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.1f);
        }

        [Test]
        public void XDistance2D_LineLine_WithParallelLines_ReturnsCorrectDistance()
        {
            // Arrange
            var lineA = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var lineB = new Line2Df(new Vector2Df(0.0f, 5.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XDistance2D.LineLine(in lineA, in lineB);

            // Assert
            ClassicAssert.AreEqual(5.0f, result, 0.1f);
        }
        #endregion

        #region LineRay Tests
        [Test]
        public void XDistance2D_LineRay_WithIntersecting_ReturnsZero()
        {
            // Arrange
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var ray = new Ray2Df(new Vector2Df(5.0f, -5.0f), new Vector2Df(0.0f, 1.0f));

            // Act
            var result = XDistance2D.LineRay(in line, in ray);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.1f);
        }

        [Test]
        public void XDistance2D_LineRay_WithNonIntersecting_ReturnsCorrectDistance()
        {
            // Arrange
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var ray = new Ray2Df(new Vector2Df(5.0f, 5.0f), new Vector2Df(0.0f, 1.0f));

            // Act
            var result = XDistance2D.LineRay(in line, in ray);

            // Assert
            ClassicAssert.AreEqual(5.0f, result, 0.1f);
        }
        #endregion

        #region LineSegment Tests
        [Test]
        public void XDistance2D_LineSegment_WithIntersecting_ReturnsZero()
        {
            // Arrange
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var segment = new Segment2Df(new Vector2Df(5.0f, -5.0f), new Vector2Df(5.0f, 5.0f));

            // Act
            var result = XDistance2D.LineSegment(in line, in segment);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.1f);
        }
        #endregion

        #region RayRay Tests
        [Test]
        public void XDistance2D_RayRay_WithIntersectingRays_ReturnsZero()
        {
            // Arrange
            var rayA = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var rayB = new Ray2Df(new Vector2Df(5.0f, -5.0f), new Vector2Df(0.0f, 1.0f));

            // Act
            var result = XDistance2D.RayRay(in rayA, in rayB);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.1f);
        }

        [Test]
        public void XDistance2D_RayRay_WithParallelRays_ReturnsCorrectDistance()
        {
            // Arrange
            var rayA = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var rayB = new Ray2Df(new Vector2Df(0.0f, 5.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XDistance2D.RayRay(in rayA, in rayB);

            // Assert
            ClassicAssert.AreEqual(5.0f, result, 0.1f);
        }
        #endregion

        #region SegmentSegment Tests
        [Test]
        public void XDistance2D_SegmentSegment_WithIntersectingSegments_ReturnsZero()
        {
            // Arrange
            var segment1 = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));
            var segment2 = new Segment2Df(new Vector2Df(5.0f, -5.0f), new Vector2Df(5.0f, 5.0f));

            // Act
            var result = XDistance2D.SegmentSegment(in segment1, in segment2);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.1f);
        }

        [Test]
        public void XDistance2D_SegmentSegment_WithNonIntersectingSegments_ReturnsCorrectDistance()
        {
            // Arrange
            var segment1 = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));
            var segment2 = new Segment2Df(new Vector2Df(15.0f, 5.0f), new Vector2Df(20.0f, 5.0f));

            // Act
            var result = XDistance2D.SegmentSegment(in segment1, in segment2);

            // Assert
            ClassicAssert.Greater(result, 0.0f);
        }
        #endregion

        #region CircleCircle Tests
        [Test]
        public void XDistance2D_CircleCircle_WithIntersectingCircles_ReturnsNegativeDistance()
        {
            // Arrange
            var circleA = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var circleB = new Circle2Df(new Vector2Df(8.0f, 0.0f), 5.0f);

            // Act
            var result = XDistance2D.CircleCircle(in circleA, in circleB);

            // Assert
            ClassicAssert.Less(result, 0.0f);
        }

        [Test]
        public void XDistance2D_CircleCircle_WithNonIntersectingCircles_ReturnsPositiveDistance()
        {
            // Arrange
            var circleA = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var circleB = new Circle2Df(new Vector2Df(20.0f, 0.0f), 5.0f);

            // Act
            var result = XDistance2D.CircleCircle(in circleA, in circleB);

            // Assert
            ClassicAssert.AreEqual(10.0f, result, 0.1f);
        }

        [Test]
        public void XDistance2D_CircleCircle_WithTouchingCircles_ReturnsZero()
        {
            // Arrange
            var circleA = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var circleB = new Circle2Df(new Vector2Df(10.0f, 0.0f), 5.0f);

            // Act
            var result = XDistance2D.CircleCircle(in circleA, in circleB);

            // Assert
            ClassicAssert.AreEqual(0.0f, result, 0.1f);
        }
        #endregion
    }
}
