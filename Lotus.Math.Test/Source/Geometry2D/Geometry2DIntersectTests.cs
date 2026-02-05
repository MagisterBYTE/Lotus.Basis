using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DIntersectTests
    {
        #region PointLine Tests
        [Test]
        public void XIntersect2D_PointLine_WithPointOnLine_ReturnsTrue()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XIntersect2D.PointLine(in point, in line);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void XIntersect2D_PointLine_WithPointOffLine_ReturnsFalse()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 1.0f);
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XIntersect2D.PointLine(in point, in line);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void XIntersect2D_PointLine_WithSide_ReturnsCorrectSide()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 1.0f);
            var line = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XIntersect2D.PointLine(in point, in line, out var side);

            // Assert
            ClassicAssert.IsFalse(result);
            ClassicAssert.AreNotEqual(0, side);
        }
        #endregion

        #region PointRay Tests
        [Test]
        public void XIntersect2D_PointRay_WithPointOnRay_ReturnsTrue()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var ray = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XIntersect2D.PointRay(in point, in ray);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void XIntersect2D_PointRay_WithPointBehindRay_ReturnsFalse()
        {
            // Arrange
            var point = new Vector2Df(-5.0f, 0.0f);
            var ray = new Ray2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XIntersect2D.PointRay(in point, in ray);

            // Assert
            ClassicAssert.IsFalse(result);
        }
        #endregion

        #region PointSegment Tests
        [Test]
        public void XIntersect2D_PointSegment_WithPointOnSegment_ReturnsTrue()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = XIntersect2D.PointSegment(in point, in segment);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void XIntersect2D_PointSegment_WithPointOutsideSegment_ReturnsFalse()
        {
            // Arrange
            var point = new Vector2Df(15.0f, 0.0f);
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = XIntersect2D.PointSegment(in point, in segment);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void XIntersect2D_PointOnSegment_WithPointOnSegment_ReturnsTrue()
        {
            // Arrange
            var start = new Vector2Df(0.0f, 0.0f);
            var end = new Vector2Df(10.0f, 0.0f);
            var point = new Vector2Df(5.0f, 0.0f);

            // Act
            var result = XIntersect2D.PointOnSegment(in start, in end, in point);

            // Assert
            ClassicAssert.IsTrue(result);
        }
        #endregion

        #region PointCircle Tests
        [Test]
        public void XIntersect2D_PointCircle_WithPointOnCircle_ReturnsTrue()
        {
            // Arrange
            var point = new Vector2Df(5.0f, 0.0f);
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = XIntersect2D.PointCircle(in point, in circle);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void XIntersect2D_PointCircle_WithPointInsideCircle_ReturnsFalse()
        {
            // Arrange
            var point = new Vector2Df(2.0f, 0.0f);
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = XIntersect2D.PointCircle(in point, in circle);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void XIntersect2D_PointCircle_WithPointOutsideCircle_ReturnsFalse()
        {
            // Arrange
            var point = new Vector2Df(10.0f, 0.0f);
            var circle = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);

            // Act
            var result = XIntersect2D.PointCircle(in point, in circle);

            // Assert
            ClassicAssert.IsFalse(result);
        }
        #endregion

        #region LineLine Tests
        [Test]
        public void XIntersect2D_LineLine_WithIntersectingLines_ReturnsTrue()
        {
            // Arrange
            var lineA = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var lineB = new Line2Df(new Vector2Df(5.0f, -5.0f), new Vector2Df(0.0f, 1.0f));

            // Act
            var result = XIntersect2D.LineLine(in lineA, in lineB);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void XIntersect2D_LineLine_WithParallelLines_ReturnsFalse()
        {
            // Arrange
            var lineA = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var lineB = new Line2Df(new Vector2Df(0.0f, 5.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XIntersect2D.LineLine(in lineA, in lineB);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void XIntersect2D_LineLine_WithHit_ReturnsCorrectIntersectionPoint()
        {
            // Arrange
            var lineA = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var lineB = new Line2Df(new Vector2Df(5.0f, -5.0f), new Vector2Df(0.0f, 1.0f));

            // Act
            var result = XIntersect2D.LineLine(in lineA, in lineB, out var hit);

            // Assert
            ClassicAssert.IsTrue(result);
            ClassicAssert.AreEqual(TIntersectType2D.Point, hit.IntersectType);
            ClassicAssert.AreEqual(5.0f, hit.Point1.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, hit.Point1.Y, 0.1f);
        }

        [Test]
        public void XIntersect2D_LineLine_WithCollinearLines_ReturnsParallel()
        {
            // Arrange
            var lineA = new Line2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(1.0f, 0.0f));
            var lineB = new Line2Df(new Vector2Df(5.0f, 0.0f), new Vector2Df(1.0f, 0.0f));

            // Act
            var result = XIntersect2D.LineLine(in lineA, in lineB, out var hit);

            // Assert
            ClassicAssert.IsTrue(result);
            ClassicAssert.AreEqual(TIntersectType2D.Parallel, hit.IntersectType);
        }
        #endregion

        #region RayToRay Tests
        [Test]
        public void XIntersect2D_RayToRay_WithIntersectingRays_ReturnsPoint()
        {
            // Arrange
            var rayPos1 = new Vector2Df(0.0f, 0.0f);
            var rayDir1 = new Vector2Df(1.0f, 0.0f);
            var rayPos2 = new Vector2Df(5.0f, -5.0f);
            var rayDir2 = new Vector2Df(0.0f, 1.0f);

            // Act
            var result = XIntersect2D.RayToRay(in rayPos1, in rayDir1, in rayPos2, in rayDir2, out var hit);

            // Assert
            ClassicAssert.AreEqual(TIntersectType2D.Point, result);
            ClassicAssert.AreEqual(5.0f, hit.Point1.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, hit.Point1.Y, 0.1f);
        }

        [Test]
        public void XIntersect2D_RayToRay_WithNonIntersectingRays_ReturnsNone()
        {
            // Arrange
            var rayPos1 = new Vector2Df(0.0f, 0.0f);
            var rayDir1 = new Vector2Df(1.0f, 0.0f);
            var rayPos2 = new Vector2Df(5.0f, 5.0f);
            var rayDir2 = new Vector2Df(0.0f, 1.0f);

            // Act
            var result = XIntersect2D.RayToRay(in rayPos1, in rayDir1, in rayPos2, in rayDir2, out var hit);

            // Assert
            ClassicAssert.AreEqual(TIntersectType2D.None, result);
        }

        [Test]
        public void XIntersect2D_RayToRay_WithParallelRays_ReturnsParallel()
        {
            // Arrange
            var rayPos1 = new Vector2Df(0.0f, 0.0f);
            var rayDir1 = new Vector2Df(1.0f, 0.0f);
            var rayPos2 = new Vector2Df(0.0f, 5.0f);
            var rayDir2 = new Vector2Df(1.0f, 0.0f);

            // Act
            var result = XIntersect2D.RayToRay(in rayPos1, in rayDir1, in rayPos2, in rayDir2, out var hit);

            // Assert
            ClassicAssert.AreEqual(TIntersectType2D.Parallel, result);
        }
        #endregion

        #region SegmentToSegment Tests
        [Test]
        public void XIntersect2D_SegmentToSegment_WithIntersectingSegments_ReturnsTrue()
        {
            // Arrange
            var start1 = new Vector2Df(0.0f, 0.0f);
            var end1 = new Vector2Df(10.0f, 0.0f);
            var start2 = new Vector2Df(5.0f, -5.0f);
            var end2 = new Vector2Df(5.0f, 5.0f);

            // Act
            var result = XIntersect2D.SegmentToSegment(in start1, in end1, in start2, in end2);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void XIntersect2D_SegmentToSegment_WithNonIntersectingSegments_ReturnsFalse()
        {
            // Arrange
            var start1 = new Vector2Df(0.0f, 0.0f);
            var end1 = new Vector2Df(10.0f, 0.0f);
            var start2 = new Vector2Df(15.0f, -5.0f);
            var end2 = new Vector2Df(15.0f, 5.0f);

            // Act
            var result = XIntersect2D.SegmentToSegment(in start1, in end1, in start2, in end2);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void XIntersect2D_SegmentToSegment_WithOverlappingSegments_ReturnsTrue()
        {
            // Arrange
            var start1 = new Vector2Df(0.0f, 0.0f);
            var end1 = new Vector2Df(10.0f, 0.0f);
            var start2 = new Vector2Df(5.0f, 0.0f);
            var end2 = new Vector2Df(15.0f, 0.0f);

            // Act
            var result = XIntersect2D.SegmentToSegment(in start1, in end1, in start2, in end2);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void XIntersect2D_SegmentToSegment_WithParallelSegments_ReturnsFalse()
        {
            // Arrange
            var start1 = new Vector2Df(0.0f, 0.0f);
            var end1 = new Vector2Df(10.0f, 0.0f);
            var start2 = new Vector2Df(0.0f, 5.0f);
            var end2 = new Vector2Df(10.0f, 5.0f);

            // Act
            var result = XIntersect2D.SegmentToSegment(in start1, in end1, in start2, in end2);

            // Assert
            ClassicAssert.IsFalse(result);
        }
        #endregion

        #region CircleCircle Tests
        [Test]
        public void XIntersect2D_CircleCircle_WithIntersectingCircles_ReturnsTrue()
        {
            // Arrange
            var circleA = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var circleB = new Circle2Df(new Vector2Df(8.0f, 0.0f), 5.0f);

            // Act
            var result = XIntersect2D.CircleCircle(in circleA, in circleB);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void XIntersect2D_CircleCircle_WithNonIntersectingCircles_ReturnsFalse()
        {
            // Arrange
            var circleA = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var circleB = new Circle2Df(new Vector2Df(20.0f, 0.0f), 5.0f);

            // Act
            var result = XIntersect2D.CircleCircle(in circleA, in circleB);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void XIntersect2D_CircleCircle_WithTouchingCircles_ReturnsTrue()
        {
            // Arrange
            var circleA = new Circle2Df(new Vector2Df(0.0f, 0.0f), 5.0f);
            var circleB = new Circle2Df(new Vector2Df(10.0f, 0.0f), 5.0f);

            // Act
            var result = XIntersect2D.CircleCircle(in circleA, in circleB);

            // Assert
            ClassicAssert.IsTrue(result);
        }
        #endregion
    }
}
