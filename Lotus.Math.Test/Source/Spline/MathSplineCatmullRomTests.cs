using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathSplineCatmullRomTests
    {
        private const float EpsilonF = 0.00001f;

        #region CCatmullRomSpline2D Tests
        [Test]
        public void CCatmullRomSpline2D_CalculatePoint_Static_AtTimeZero_ReturnsP1()
        {
            var p0 = new Vector2Df(0.0f, 0.0f);
            var p1 = new Vector2Df(1.0f, 0.0f);
            var p2 = new Vector2Df(2.0f, 0.0f);
            var p3 = new Vector2Df(3.0f, 0.0f);
            var result = CCatmullRomSpline2D.CalculatePoint(0.0f, p0, p1, p2, p3);
            ClassicAssert.AreEqual(p1, result);
        }

        [Test]
        public void CCatmullRomSpline2D_CalculatePoint_Static_AtTimeOne_ReturnsP2()
        {
            var p0 = new Vector2Df(0.0f, 0.0f);
            var p1 = new Vector2Df(1.0f, 0.0f);
            var p2 = new Vector2Df(2.0f, 0.0f);
            var p3 = new Vector2Df(3.0f, 0.0f);
            var result = CCatmullRomSpline2D.CalculatePoint(1.0f, p0, p1, p2, p3);
            ClassicAssert.AreEqual(p2, result);
        }

        [Test]
        public void CCatmullRomSpline2D_CalculatePoint_Instance_AtTimeZero_ReturnsFirstPoint()
        {
            var spline = new CCatmullRomSpline2D(
                new Vector2Df(0.0f, 0.0f),
                new Vector2Df(2.0f, 0.0f));
            var result = spline.CalculatePoint(0.0f);
            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public void CCatmullRomSpline2D_CalculateFirstDerivative_Static_ReturnsCorrectValue()
        {
            var p0 = new Vector2Df(0.0f, 0.0f);
            var p1 = new Vector2Df(1.0f, 0.0f);
            var p2 = new Vector2Df(2.0f, 0.0f);
            var p3 = new Vector2Df(3.0f, 0.0f);
            var result = CCatmullRomSpline2D.CalculateFirstDerivative(0.5f, p0, p1, p2, p3);
            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public void CCatmullRomSpline2D_IsClosed_GetSet_WorksCorrectly()
        {
            var spline = new CCatmullRomSpline2D();
            spline.IsClosed = true;
            ClassicAssert.IsTrue(spline.IsClosed);
            spline.IsClosed = false;
            ClassicAssert.IsFalse(spline.IsClosed);
        }

        [Test]
        public void CCatmullRomSpline2D_CurveCount_ReturnsCorrectCount()
        {
            var spline = new CCatmullRomSpline2D(5);
            ClassicAssert.AreEqual(4, spline.CurveCount);
        }

        [Test]
        public void CCatmullRomSpline2D_ComputeDrawingPoints_GeneratesPoints()
        {
            var spline = new CCatmullRomSpline2D(
                new Vector2Df(0.0f, 0.0f),
                new Vector2Df(2.0f, 0.0f));
            spline.SegmentsSpline = 10;
            spline.ComputeDrawingPoints();
            ClassicAssert.Greater(spline.DrawingPoints.Count, 0);
        }
        #endregion

        #region CCatmullRomSpline3D Tests
        [Test]
        public void CCatmullRomSpline3D_CalculatePoint_Static_AtTimeZero_ReturnsP1()
        {
            var p0 = new Vector3Df(0.0f, 0.0f, 0.0f);
            var p1 = new Vector3Df(1.0f, 0.0f, 0.0f);
            var p2 = new Vector3Df(2.0f, 0.0f, 0.0f);
            var p3 = new Vector3Df(3.0f, 0.0f, 0.0f);
            var result = CCatmullRomSpline3D.CalculatePoint(0.0f, in p0, in p1, in p2, in p3);
            ClassicAssert.AreEqual(p1, result);
        }

        [Test]
        public void CCatmullRomSpline3D_CalculatePoint_Static_AtTimeOne_ReturnsP2()
        {
            var p0 = new Vector3Df(0.0f, 0.0f, 0.0f);
            var p1 = new Vector3Df(1.0f, 0.0f, 0.0f);
            var p2 = new Vector3Df(2.0f, 0.0f, 0.0f);
            var p3 = new Vector3Df(3.0f, 0.0f, 0.0f);
            var result = CCatmullRomSpline3D.CalculatePoint(1.0f, in p0, in p1, in p2, in p3);
            ClassicAssert.AreEqual(p2, result);
        }

        [Test]
        public void CCatmullRomSpline3D_CalculatePoint_Instance_AtTimeZero_ReturnsFirstPoint()
        {
            var spline = new CCatmullRomSpline3D(
                new Vector3Df(0.0f, 0.0f, 0.0f),
                new Vector3Df(2.0f, 0.0f, 0.0f));
            var result = spline.CalculatePoint(0.0f);
            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public void CCatmullRomSpline3D_CalculateFirstDerivative_Static_ReturnsCorrectValue()
        {
            var p0 = new Vector3Df(0.0f, 0.0f, 0.0f);
            var p1 = new Vector3Df(1.0f, 0.0f, 0.0f);
            var p2 = new Vector3Df(2.0f, 0.0f, 0.0f);
            var p3 = new Vector3Df(3.0f, 0.0f, 0.0f);
            var result = CCatmullRomSpline3D.CalculateFirstDerivative(0.5f, in p0, in p1, in p2, in p3);
            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public void CCatmullRomSpline3D_IsClosed_GetSet_WorksCorrectly()
        {
            var spline = new CCatmullRomSpline3D();
            spline.IsClosed = true;
            ClassicAssert.IsTrue(spline.IsClosed);
            spline.IsClosed = false;
            ClassicAssert.IsFalse(spline.IsClosed);
        }

        [Test]
        public void CCatmullRomSpline3D_CurveCount_ReturnsCorrectCount()
        {
            var spline = new CCatmullRomSpline3D(5);
            ClassicAssert.AreEqual(4, spline.CurveCount);
        }

        [Test]
        public void CCatmullRomSpline3D_ComputeDrawingPoints_GeneratesPoints()
        {
            var spline = new CCatmullRomSpline3D(
                new Vector3Df(0.0f, 0.0f, 0.0f),
                new Vector3Df(2.0f, 0.0f, 0.0f));
            spline.SegmentsSpline = 10;
            spline.ComputeDrawingPoints();
            ClassicAssert.Greater(spline.DrawingPoints.Count, 0);
        }
        #endregion
    }
}
