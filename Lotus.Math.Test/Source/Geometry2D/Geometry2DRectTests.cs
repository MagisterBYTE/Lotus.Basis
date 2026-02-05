using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry2DRectTests
    {
        #region Rect2D Tests
        [Test]
        public void Rect2D_Constructor_WithParameters_InitializesCorrectly()
        {
            // Arrange & Act
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);

            // Assert
            ClassicAssert.AreEqual(10.0, rect.X);
            ClassicAssert.AreEqual(20.0, rect.Y);
            ClassicAssert.AreEqual(30.0, rect.Width);
            ClassicAssert.AreEqual(40.0, rect.Height);
        }

        [Test]
        public void Rect2D_IsEmpty_WithZeroSize_ReturnsTrue()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 0.0, 0.0);

            // Act
            var result = rect.IsEmpty;

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Rect2D_IsEmpty_WithNonZeroSize_ReturnsFalse()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);

            // Act
            var result = rect.IsEmpty;

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Rect2D_Area_ReturnsCorrectArea()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);

            // Act
            var result = rect.Area;

            // Assert
            ClassicAssert.AreEqual(1200.0, result);
        }

        [Test]
        public void Rect2D_Diagonal_ReturnsCorrectDiagonal()
        {
            // Arrange
            var rect = new Rect2D(0.0, 0.0, 3.0, 4.0);

            // Act
            var result = rect.Diagonal;

            // Assert
            ClassicAssert.AreEqual(5.0, result, 0.001);
        }

        [Test]
        public void Rect2D_Right_ReturnsCorrectValue()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);

            // Act
            var result = rect.Right;

            // Assert
            ClassicAssert.AreEqual(40.0, result);
        }

        [Test]
        public void Rect2D_Bottom_ReturnsCorrectValue()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);

            // Act
            var result = rect.Bottom;

            // Assert
            ClassicAssert.AreEqual(60.0, result);
        }

        [Test]
        public void Rect2D_PointTopLeft_ReturnsCorrectPoint()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);

            // Act
            var result = rect.PointTopLeft;

            // Assert
            ClassicAssert.AreEqual(10.0, result.X);
            ClassicAssert.AreEqual(20.0, result.Y);
        }

        [Test]
        public void Rect2D_PointTopRight_ReturnsCorrectPoint()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);

            // Act
            var result = rect.PointTopRight;

            // Assert
            ClassicAssert.AreEqual(40.0, result.X);
            ClassicAssert.AreEqual(20.0, result.Y);
        }

        [Test]
        public void Rect2D_PointBottomLeft_ReturnsCorrectPoint()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);

            // Act
            var result = rect.PointBottomLeft;

            // Assert
            ClassicAssert.AreEqual(10.0, result.X);
            ClassicAssert.AreEqual(60.0, result.Y);
        }

        [Test]
        public void Rect2D_PointBottomRight_ReturnsCorrectPoint()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);

            // Act
            var result = rect.PointBottomRight;

            // Assert
            ClassicAssert.AreEqual(40.0, result.X);
            ClassicAssert.AreEqual(60.0, result.Y);
        }

        [Test]
        public void Rect2D_Contains_WithPointInside_ReturnsTrue()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);
            var point = new Vector2D(25.0, 35.0);

            // Act
            var result = rect.Contains(in point);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Rect2D_Contains_WithPointOutside_ReturnsFalse()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);
            var point = new Vector2D(50.0, 70.0);

            // Act
            var result = rect.Contains(in point);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Rect2D_Contains_WithPointOnBoundary_ReturnsTrue()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);
            var point = new Vector2D(10.0, 20.0);

            // Act
            var result = rect.Contains(in point);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Rect2D_StaticIntersectRect_WithOverlappingRects_ReturnsIntersection()
        {
            // Arrange
            var rect1 = new Rect2D(10.0, 20.0, 30.0, 40.0);
            var rect2 = new Rect2D(20.0, 30.0, 30.0, 40.0);

            // Act
            var result = Rect2D.IntersectRect(in rect1, in rect2);

            // Assert
            ClassicAssert.AreEqual(20.0, result.X);
            ClassicAssert.AreEqual(30.0, result.Y);
            ClassicAssert.AreEqual(20.0, result.Width);
            ClassicAssert.AreEqual(30.0, result.Height);
        }

        [Test]
        public void Rect2D_StaticIntersectRect_WithNonOverlappingRects_ReturnsEmpty()
        {
            // Arrange
            var rect1 = new Rect2D(10.0, 20.0, 30.0, 40.0);
            var rect2 = new Rect2D(100.0, 200.0, 30.0, 40.0);

            // Act
            var result = Rect2D.IntersectRect(in rect1, in rect2);

            // Assert
            ClassicAssert.IsTrue(result.IsEmpty);
        }

        [Test]
        public void Rect2D_StaticUnionRect_ReturnsUnion()
        {
            // Arrange
            var rect1 = new Rect2D(10.0, 20.0, 30.0, 40.0);
            var rect2 = new Rect2D(50.0, 60.0, 30.0, 40.0);

            // Act
            var result = Rect2D.UnionRect(in rect1, in rect2);

            // Assert
            ClassicAssert.AreEqual(10.0, result.X);
            ClassicAssert.AreEqual(20.0, result.Y);
            ClassicAssert.AreEqual(70.0, result.Width);
            ClassicAssert.AreEqual(80.0, result.Height);
        }

        [Test]
        public void Rect2D_DeserializeFromString_ReturnsCorrectRect()
        {
            // Arrange
            var data = "10.5; 20.7; 30.9; 40.1";

            // Act
            var result = Rect2D.DeserializeFromString(data);

            // Assert
            ClassicAssert.AreEqual(10.5, result.X, 0.001);
            ClassicAssert.AreEqual(20.7, result.Y, 0.001);
            ClassicAssert.AreEqual(30.9, result.Width, 0.001);
            ClassicAssert.AreEqual(40.1, result.Height, 0.001);
        }

        [Test]
        public void Rect2D_Offset_MovesRect()
        {
            // Arrange
            var rect = new Rect2D(10.0, 20.0, 30.0, 40.0);
            var offset = new Vector2D(5.0, 10.0);

            // Act
            rect.Offset(in offset);

            // Assert
            ClassicAssert.AreEqual(15.0, rect.X);
            ClassicAssert.AreEqual(30.0, rect.Y);
        }
        #endregion

        #region Rect2Df Tests
        [Test]
        public void Rect2Df_Constructor_WithParameters_InitializesCorrectly()
        {
            // Arrange & Act
            var rect = new Rect2Df(10.0f, 20.0f, 30.0f, 40.0f);

            // Assert
            ClassicAssert.AreEqual(10.0f, rect.X);
            ClassicAssert.AreEqual(20.0f, rect.Y);
            ClassicAssert.AreEqual(30.0f, rect.Width);
            ClassicAssert.AreEqual(40.0f, rect.Height);
        }

        [Test]
        public void Rect2Df_Contains_WithPointInside_ReturnsTrue()
        {
            // Arrange
            var rect = new Rect2Df(10.0f, 20.0f, 30.0f, 40.0f);
            var point = new Vector2Df(25.0f, 35.0f);

            // Act
            var result = rect.Contains(in point);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Rect2Df_StaticIntersectRect_WithOverlappingRects_ReturnsIntersection()
        {
            // Arrange
            var rect1 = new Rect2Df(10.0f, 20.0f, 30.0f, 40.0f);
            var rect2 = new Rect2Df(20.0f, 30.0f, 30.0f, 40.0f);

            // Act
            var result = Rect2Df.IntersectRect(in rect1, in rect2);

            // Assert
            ClassicAssert.AreEqual(20.0f, result.X);
            ClassicAssert.AreEqual(30.0f, result.Y);
            ClassicAssert.AreEqual(20.0f, result.Width);
            ClassicAssert.AreEqual(30.0f, result.Height);
        }

        [Test]
        public void Rect2Df_DeserializeFromString_ReturnsCorrectRect()
        {
            // Arrange
            var data = "10.5; 20.7; 30.9; 40.1";

            // Act
            var result = Rect2Df.DeserializeFromString(data);

            // Assert
            ClassicAssert.AreEqual(10.5f, result.X, 0.001f);
            ClassicAssert.AreEqual(20.7f, result.Y, 0.001f);
            ClassicAssert.AreEqual(30.9f, result.Width, 0.001f);
            ClassicAssert.AreEqual(40.1f, result.Height, 0.001f);
        }
        #endregion
    }
}
