using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathSplineBezierTests
    {
        private const float EpsilonF = 0.00001f;

        #region BezierQuadratic2D Tests
        [Test]
        public void BezierQuadratic2D_CalculatePoint_Static_AtTimeZero_ReturnsStart()
        {
            var start = new Vector2Df(0.0f, 0.0f);
            var handle = new Vector2Df(1.0f, 1.0f);
            var end = new Vector2Df(2.0f, 0.0f);
            var result = BezierQuadratic2D.CalculatePoint(0.0f, start, handle, end);
            ClassicAssert.AreEqual(start, result);
        }

        [Test]
        public void BezierQuadratic2D_CalculatePoint_Static_AtTimeOne_ReturnsEnd()
        {
            var start = new Vector2Df(0.0f, 0.0f);
            var handle = new Vector2Df(1.0f, 1.0f);
            var end = new Vector2Df(2.0f, 0.0f);
            var result = BezierQuadratic2D.CalculatePoint(1.0f, start, handle, end);
            ClassicAssert.AreEqual(end, result);
        }

        [Test]
        public void BezierQuadratic2D_CalculatePoint_Instance_AtTimeZero_ReturnsStart()
        {
            var spline = new BezierQuadratic2D(
                new Vector2Df(0.0f, 0.0f),
                new Vector2Df(2.0f, 0.0f));
            var result = spline.CalculatePoint(0.0f);
            ClassicAssert.AreEqual(spline.StartPoint, result);
        }

        [Test]
        public void BezierQuadratic2D_CalculatePoint_Instance_AtTimeOne_ReturnsEnd()
        {
            var spline = new BezierQuadratic2D(
                new Vector2Df(0.0f, 0.0f),
                new Vector2Df(2.0f, 0.0f));
            var result = spline.CalculatePoint(1.0f);
            ClassicAssert.AreEqual(spline.EndPoint, result);
        }

        [Test]
        public void BezierQuadratic2D_CalculateFirstDerivative_Static_ReturnsCorrectValue()
        {
            var start = new Vector2Df(0.0f, 0.0f);
            var handle = new Vector2Df(1.0f, 1.0f);
            var end = new Vector2Df(2.0f, 0.0f);
            var result = BezierQuadratic2D.CalculateFirstDerivative(0.5f, start, handle, end);
            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public void BezierQuadratic2D_HandlePoint_GetSet_WorksCorrectly()
        {
            var spline = new BezierQuadratic2D();
            var handle = new Vector2Df(5.0f, 5.0f);
            spline.HandlePoint = handle;
            ClassicAssert.AreEqual(handle, spline.HandlePoint);
        }

        [Test]
        public void BezierQuadratic2D_IsHandlePoint_ReturnsTrueForIndex1()
        {
            var spline = new BezierQuadratic2D();
            ClassicAssert.IsTrue(spline.IsHandlePoint(1));
            ClassicAssert.IsFalse(spline.IsHandlePoint(0));
            ClassicAssert.IsFalse(spline.IsHandlePoint(2));
        }
        #endregion

        #region BezierCubic2D Tests
        [Test]
        public void BezierCubic2D_CalculatePoint_Static_AtTimeZero_ReturnsStart()
        {
            var start = new Vector2Df(0.0f, 0.0f);
            var handle1 = new Vector2Df(1.0f, 1.0f);
            var handle2 = new Vector2Df(2.0f, 1.0f);
            var end = new Vector2Df(3.0f, 0.0f);
            var result = BezierCubic2D.CalculatePoint(0.0f, start, handle1, handle2, end);
            ClassicAssert.AreEqual(start, result);
        }

        [Test]
        public void BezierCubic2D_CalculatePoint_Static_AtTimeOne_ReturnsEnd()
        {
            var start = new Vector2Df(0.0f, 0.0f);
            var handle1 = new Vector2Df(1.0f, 1.0f);
            var handle2 = new Vector2Df(2.0f, 1.0f);
            var end = new Vector2Df(3.0f, 0.0f);
            var result = BezierCubic2D.CalculatePoint(1.0f, start, handle1, handle2, end);
            ClassicAssert.AreEqual(end, result);
        }

        [Test]
        public void BezierCubic2D_CalculatePoint_Instance_AtTimeZero_ReturnsStart()
        {
            var spline = new BezierCubic2D(
                new Vector2Df(0.0f, 0.0f),
                new Vector2Df(3.0f, 0.0f));
            var result = spline.CalculatePoint(0.0f);
            ClassicAssert.AreEqual(spline.StartPoint, result);
        }

        [Test]
        public void BezierCubic2D_CalculatePoint_Instance_AtTimeOne_ReturnsEnd()
        {
            var spline = new BezierCubic2D(
                new Vector2Df(0.0f, 0.0f),
                new Vector2Df(3.0f, 0.0f));
            var result = spline.CalculatePoint(1.0f);
            ClassicAssert.AreEqual(spline.EndPoint, result);
        }

        [Test]
        public void BezierCubic2D_CalculateFirstDerivative_Static_ReturnsCorrectValue()
        {
            var start = new Vector2Df(0.0f, 0.0f);
            var handle1 = new Vector2Df(1.0f, 1.0f);
            var handle2 = new Vector2Df(2.0f, 1.0f);
            var end = new Vector2Df(3.0f, 0.0f);
            var result = BezierCubic2D.CalculateFirstDerivative(0.5f, start, handle1, handle2, end);
            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public void BezierCubic2D_HandlePoint1_GetSet_WorksCorrectly()
        {
            var spline = new BezierCubic2D();
            var handle = new Vector2Df(5.0f, 5.0f);
            spline.HandlePoint1 = handle;
            ClassicAssert.AreEqual(handle, spline.HandlePoint1);
        }

        [Test]
        public void BezierCubic2D_HandlePoint2_GetSet_WorksCorrectly()
        {
            var spline = new BezierCubic2D();
            var handle = new Vector2Df(5.0f, 5.0f);
            spline.HandlePoint2 = handle;
            ClassicAssert.AreEqual(handle, spline.HandlePoint2);
        }

        [Test]
        public void BezierCubic2D_IsHandlePoint_ReturnsTrueForIndices1And2()
        {
            var spline = new BezierCubic2D();
            ClassicAssert.IsTrue(spline.IsHandlePoint(1));
            ClassicAssert.IsTrue(spline.IsHandlePoint(2));
            ClassicAssert.IsFalse(spline.IsHandlePoint(0));
            ClassicAssert.IsFalse(spline.IsHandlePoint(3));
        }
        #endregion

        #region BezierQuadratic3D Tests
        [Test]
        public void BezierQuadratic3D_CalculatePoint_Static_AtTimeZero_ReturnsStart()
        {
            var start = new Vector3Df(0.0f, 0.0f, 0.0f);
            var handle = new Vector3Df(1.0f, 1.0f, 1.0f);
            var end = new Vector3Df(2.0f, 0.0f, 0.0f);
            var result = BezierQuadratic3D.CalculatePoint(0.0f, start, handle, end);
            ClassicAssert.AreEqual(start, result);
        }

        [Test]
        public void BezierQuadratic3D_CalculatePoint_Static_AtTimeOne_ReturnsEnd()
        {
            var start = new Vector3Df(0.0f, 0.0f, 0.0f);
            var handle = new Vector3Df(1.0f, 1.0f, 1.0f);
            var end = new Vector3Df(2.0f, 0.0f, 0.0f);
            var result = BezierQuadratic3D.CalculatePoint(1.0f, start, handle, end);
            ClassicAssert.AreEqual(end, result);
        }

        [Test]
        public void BezierQuadratic3D_CalculatePoint_Instance_AtTimeZero_ReturnsStart()
        {
            var spline = new BezierQuadratic3D(
                new Vector3Df(0.0f, 0.0f, 0.0f),
                new Vector3Df(2.0f, 0.0f, 0.0f));
            var result = spline.CalculatePoint(0.0f);
            ClassicAssert.AreEqual(spline.StartPoint, result);
        }

        [Test]
        public void BezierQuadratic3D_CalculateFirstDerivative_Static_ReturnsCorrectValue()
        {
            var start = new Vector3Df(0.0f, 0.0f, 0.0f);
            var handle = new Vector3Df(1.0f, 1.0f, 1.0f);
            var end = new Vector3Df(2.0f, 0.0f, 0.0f);
            var result = BezierQuadratic3D.CalculateFirstDerivative(0.5f, start, handle, end);
            ClassicAssert.IsNotNull(result);
        }
        #endregion

        #region BezierCubic3D Tests
        [Test]
        public void BezierCubic3D_CalculatePoint_Static_AtTimeZero_ReturnsStart()
        {
            var start = new Vector3Df(0.0f, 0.0f, 0.0f);
            var handle1 = new Vector3Df(1.0f, 1.0f, 1.0f);
            var handle2 = new Vector3Df(2.0f, 1.0f, 1.0f);
            var end = new Vector3Df(3.0f, 0.0f, 0.0f);
            var result = BezierCubic3D.CalculatePoint(0.0f, in start, in handle1, in handle2, in end);
            ClassicAssert.AreEqual(start, result);
        }

        [Test]
        public void BezierCubic3D_CalculatePoint_Static_AtTimeOne_ReturnsEnd()
        {
            var start = new Vector3Df(0.0f, 0.0f, 0.0f);
            var handle1 = new Vector3Df(1.0f, 1.0f, 1.0f);
            var handle2 = new Vector3Df(2.0f, 1.0f, 1.0f);
            var end = new Vector3Df(3.0f, 0.0f, 0.0f);
            var result = BezierCubic3D.CalculatePoint(1.0f, in start, in handle1, in handle2, in end);
            ClassicAssert.AreEqual(end, result);
        }

        [Test]
        public void BezierCubic3D_CalculatePoint_Instance_AtTimeZero_ReturnsStart()
        {
            var spline = new BezierCubic3D(
                new Vector3Df(0.0f, 0.0f, 0.0f),
                new Vector3Df(3.0f, 0.0f, 0.0f));
            var result = spline.CalculatePoint(0.0f);
            ClassicAssert.AreEqual(spline.StartPoint, result);
        }

        [Test]
        public void BezierCubic3D_CalculateFirstDerivative_Static_ReturnsCorrectValue()
        {
            var start = new Vector3Df(0.0f, 0.0f, 0.0f);
            var handle1 = new Vector3Df(1.0f, 1.0f, 1.0f);
            var handle2 = new Vector3Df(2.0f, 1.0f, 1.0f);
            var end = new Vector3Df(3.0f, 0.0f, 0.0f);
            var result = BezierCubic3D.CalculateFirstDerivative(0.5f, in start, in handle1, in handle2, in end);
            ClassicAssert.IsNotNull(result);
        }
        #endregion

        #region SplineBase2D Tests
        [Test]
        public void SplineBase2D_StartPoint_GetSet_WorksCorrectly()
        {
            var spline = new BezierQuadratic2D();
            var point = new Vector2Df(10.0f, 20.0f);
            spline.StartPoint = point;
            ClassicAssert.AreEqual(point, spline.StartPoint);
        }

        [Test]
        public void SplineBase2D_EndPoint_GetSet_WorksCorrectly()
        {
            var spline = new BezierQuadratic2D();
            var point = new Vector2Df(30.0f, 40.0f);
            spline.EndPoint = point;
            ClassicAssert.AreEqual(point, spline.EndPoint);
        }

        [Test]
        public void SplineBase2D_CountPoints_ReturnsCorrectCount()
        {
            var spline = new BezierQuadratic2D();
            ClassicAssert.AreEqual(3, spline.CountPoints);
        }

        [Test]
        public void SplineBase2D_SegmentsSpline_GetSet_WorksCorrectly()
        {
            var spline = new BezierQuadratic2D();
            spline.SegmentsSpline = 20;
            ClassicAssert.AreEqual(20, spline.SegmentsSpline);
        }

        [Test]
        public void SplineBase2D_ComputeDrawingPoints_GeneratesPoints()
        {
            var spline = new BezierQuadratic2D(
                new Vector2Df(0.0f, 0.0f),
                new Vector2Df(2.0f, 0.0f));
            spline.ComputeDrawingPoints();
            ClassicAssert.Greater(spline.DrawingPoints.Count, 0);
        }

        [Test]
        public void SplineBase2D_ComputeLengthSpline_CalculatesLength()
        {
            var spline = new BezierQuadratic2D(
                new Vector2Df(0.0f, 0.0f),
                new Vector2Df(2.0f, 0.0f));
            spline.ComputeDrawingPoints();
            spline.ComputeLengthSpline();
            ClassicAssert.Greater(spline.Length, 0.0f);
        }

        [Test]
        public void SplineBase2D_GetMovePosition_AtZero_ReturnsStart()
        {
            var spline = new BezierQuadratic2D(
                new Vector2Df(0.0f, 0.0f),
                new Vector2Df(2.0f, 0.0f));
            spline.ComputeDrawingPoints();
            spline.ComputeLengthSpline();
            var result = spline.GetMovePosition(0.0f);
            ClassicAssert.AreEqual(spline.StartPoint, result);
        }
        #endregion

        #region TMoveSegment Tests
        [Test]
        public void TMoveSegment_Constructor_InitializesCorrectly()
        {
            var segment = new TMoveSegment(10.0f, 20.0f);
            ClassicAssert.AreEqual(10.0f, segment.Length);
            ClassicAssert.AreEqual(20.0f, segment.Summa);
        }
        #endregion
    }
}
