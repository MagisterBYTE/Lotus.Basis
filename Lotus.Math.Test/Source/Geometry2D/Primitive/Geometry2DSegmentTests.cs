using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DSegmentTests
    {
        [Test]
        public void Segment2Df_Constructor_WithStartAndEnd_InitializesCorrectly()
        {
            // Arrange
            var start = new Vector2Df(0.0f, 0.0f);
            var end = new Vector2Df(10.0f, 0.0f);

            // Act
            var segment = new Segment2Df(start, end);

            // Assert
            ClassicAssert.AreEqual(0.0f, segment.Start.X);
            ClassicAssert.AreEqual(0.0f, segment.Start.Y);
            ClassicAssert.AreEqual(10.0f, segment.End.X);
            ClassicAssert.AreEqual(0.0f, segment.End.Y);
        }

        [Test]
        public void Segment2Df_EqualityOperator_WithEqualSegments_ReturnsTrue()
        {
            // Arrange
            var segment1 = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));
            var segment2 = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));

            // Act
            var result = segment1 == segment2;

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Segment2Df_EqualityOperator_WithDifferentSegments_ReturnsFalse()
        {
            // Arrange
            var segment1 = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));
            var segment2 = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(20.0f, 0.0f));

            // Act
            var result = segment1 == segment2;

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Segment2Df_Location_ReturnsCenter()
        {
            // Arrange
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 20.0f));

            // Act
            var result = segment.Location;

            // Assert
            ClassicAssert.AreEqual(5.0f, result.X);
            ClassicAssert.AreEqual(10.0f, result.Y);
        }

        [Test]
        public void Segment2Df_Direction_ReturnsCorrectDirection()
        {
            // Arrange
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 20.0f));

            // Act
            var result = segment.Direction;

            // Assert
            ClassicAssert.AreEqual(10.0f, result.X);
            ClassicAssert.AreEqual(20.0f, result.Y);
        }

        [Test]
        public void Segment2Df_DirectionUnit_ReturnsNormalizedDirection()
        {
            // Arrange
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(3.0f, 4.0f));

            // Act
            var result = segment.DirectionUnit;

            // Assert
            ClassicAssert.AreEqual(1.0f, result.Length, 0.001f);
            ClassicAssert.AreEqual(0.6f, result.X, 0.001f);
            ClassicAssert.AreEqual(0.8f, result.Y, 0.001f);
        }

        [Test]
        public void Segment2Df_Contains_WithPointOnSegment_ReturnsTrue()
        {
            // Arrange
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));
            var point = new Vector2Df(5.0f, 0.0f);

            // Act
            var result = segment.Contains(in point);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Segment2Df_Contains_WithPointOutside_ReturnsFalse()
        {
            // Arrange
            var segment = new Segment2Df(new Vector2Df(0.0f, 0.0f), new Vector2Df(10.0f, 0.0f));
            var point = new Vector2Df(15.0f, 0.0f);

            // Act
            var result = segment.Contains(in point);

            // Assert
            ClassicAssert.IsFalse(result);
        }
    }
}
