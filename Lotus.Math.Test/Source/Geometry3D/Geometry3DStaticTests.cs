using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DStaticTests
    {
        private const float EpsilonF = 0.00001f;
        private const double EpsilonD = 0.00001;

        #region XGeometry3D Tests
        [Test]
        public void XGeometry3D_GetPointOnCircleXZ_ReturnsCorrectPoint()
        {
            var result = XGeometry3D.GetPointOnCircleXZ(1.0f, 0.0f);
            ClassicAssert.AreEqual(1.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);

            result = XGeometry3D.GetPointOnCircleXZ(1.0f, 90.0f);
            ClassicAssert.AreEqual(0.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(1.0f, result.Z, 0.1f);
        }

        [Test]
        public void XGeometry3D_GetPointOnCircleZY_ReturnsCorrectPoint()
        {
            var result = XGeometry3D.GetPointOnCircleZY(1.0f, 0.0f);
            ClassicAssert.AreEqual(0.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(1.0f, result.Z, EpsilonF);

            result = XGeometry3D.GetPointOnCircleZY(1.0f, 90.0f);
            ClassicAssert.AreEqual(0.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(1.0f, result.Y, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Z, 0.1f);
        }

        [Test]
        public void XGeometry3D_GetPointOnCircleXY_ReturnsCorrectPoint()
        {
            var result = XGeometry3D.GetPointOnCircleXY(1.0f, 0.0f);
            ClassicAssert.AreEqual(1.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);

            result = XGeometry3D.GetPointOnCircleXY(1.0f, 90.0f);
            ClassicAssert.AreEqual(0.0f, result.X, 0.1f);
            ClassicAssert.AreEqual(1.0f, result.Y, 0.1f);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);
        }
        #endregion

        #region XClosest3D Tests
        [Test]
        public void XClosest3D_PointLine_ProjectsPointOntoLine()
        {
            var point = new Vector3Df(0.0f, 5.0f, 0.0f);
            var line = new Line3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            var result = XClosest3D.PointLine(in point, in line);
            ClassicAssert.AreEqual(0.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);
        }

        [Test]
        public void XClosest3D_PointLine_WithDistance_ReturnsCorrectDistance()
        {
            var point = new Vector3Df(5.0f, 5.0f, 0.0f);
            var linePos = new Vector3Df(0.0f, 0.0f, 0.0f);
            var lineDir = new Vector3Df(1.0f, 0.0f, 0.0f);
            var result = XClosest3D.PointLine(in point, in linePos, in lineDir, out float distance);
            ClassicAssert.AreEqual(5.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);
            ClassicAssert.AreEqual(5.0f, distance, EpsilonF);
        }

        [Test]
        public void XClosest3D_PointRay_ProjectsPointOntoRay()
        {
            var point = new Vector3Df(5.0f, 5.0f, 0.0f);
            var ray = new Ray3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            var result = XClosest3D.PointRay(in point, in ray);
            ClassicAssert.AreEqual(5.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);
        }

        [Test]
        public void XClosest3D_PointRay_WithPointBehindRay_ReturnsRayPosition()
        {
            var point = new Vector3Df(-5.0f, 5.0f, 0.0f);
            var rayPos = new Vector3Df(0.0f, 0.0f, 0.0f);
            var rayDir = new Vector3Df(1.0f, 0.0f, 0.0f);
            var result = XClosest3D.PointRay(in point, in rayPos, in rayDir, out float distance);
            ClassicAssert.AreEqual(rayPos, result);
            ClassicAssert.AreEqual(0.0f, distance, EpsilonF);
        }

        [Test]
        public void XClosest3D_PointSegment_ProjectsPointOntoSegment()
        {
            var point = new Vector3Df(5.0f, 5.0f, 0.0f);
            var segment = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(10.0f, 0.0f, 0.0f));
            var result = XClosest3D.PointSegment(in point, in segment);
            ClassicAssert.AreEqual(5.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);
        }

        [Test]
        public void XClosest3D_PointSegment_WithPointBeforeStart_ReturnsStart()
        {
            var point = new Vector3Df(-5.0f, 5.0f, 0.0f);
            var start = new Vector3Df(0.0f, 0.0f, 0.0f);
            var end = new Vector3Df(10.0f, 0.0f, 0.0f);
            var result = XClosest3D.PointSegment(in point, in start, in end, out float normalizeDistance);
            ClassicAssert.AreEqual(start, result);
            ClassicAssert.AreEqual(0.0f, normalizeDistance, EpsilonF);
        }

        [Test]
        public void XClosest3D_PointSegment_WithPointAfterEnd_ReturnsEnd()
        {
            var point = new Vector3Df(15.0f, 5.0f, 0.0f);
            var start = new Vector3Df(0.0f, 0.0f, 0.0f);
            var end = new Vector3Df(10.0f, 0.0f, 0.0f);
            var result = XClosest3D.PointSegment(in point, in start, in end, out float normalizeDistance);
            ClassicAssert.AreEqual(end, result);
            ClassicAssert.AreEqual(1.0f, normalizeDistance, EpsilonF);
        }

        [Test]
        public void XClosest3D_PointSphere_ProjectsPointOntoSphere()
        {
            var point = new Vector3Df(10.0f, 0.0f, 0.0f);
            var sphere = new Sphere3Df(Vector3Df.Zero, 5.0f);
            var result = XClosest3D.PointSphere(in point, in sphere);
            ClassicAssert.AreEqual(5.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);
        }

        [Test]
        public void XClosest3D_PointSphere_WithPointInsideSphere_ReturnsPointOnSurface()
        {
            var point = new Vector3Df(2.0f, 0.0f, 0.0f);
            var sphereCenter = Vector3Df.Zero;
            var sphereRadius = 5.0f;
            var result = XClosest3D.PointSphere(in point, in sphereCenter, sphereRadius);
            ClassicAssert.AreEqual(5.0f, result.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, result.Z, EpsilonF);
        }
        #endregion

        #region XDistance3D Tests
        [Test]
        public void XDistance3D_PointLine_ReturnsCorrectDistance()
        {
            var point = new Vector3Df(0.0f, 5.0f, 0.0f);
            var line = new Line3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            var distance = XDistance3D.PointLine(in point, in line);
            ClassicAssert.AreEqual(5.0f, distance, EpsilonF);
        }

        [Test]
        public void XDistance3D_PointRay_ReturnsCorrectDistance()
        {
            var point = new Vector3Df(5.0f, 5.0f, 0.0f);
            var ray = new Ray3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            var distance = XDistance3D.PointRay(in point, in ray);
            ClassicAssert.AreEqual(5.0f, distance, EpsilonF);
        }

        [Test]
        public void XDistance3D_PointSegment_ReturnsCorrectDistance()
        {
            var point = new Vector3Df(5.0f, 5.0f, 0.0f);
            var segment = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(10.0f, 0.0f, 0.0f));
            var distance = XDistance3D.PointSegment(in point, in segment);
            ClassicAssert.AreEqual(5.0f, distance, EpsilonF);
        }

        [Test]
        public void XDistance3D_PointSphere_ReturnsCorrectDistance()
        {
            var point = new Vector3Df(10.0f, 0.0f, 0.0f);
            var sphere = new Sphere3Df(Vector3Df.Zero, 5.0f);
            var distance = XDistance3D.PointSphere(in point, in sphere);
            ClassicAssert.AreEqual(5.0f, distance, EpsilonF); // 10 - 5 = 5
        }

        [Test]
        public void XDistance3D_PointSphere_WithPointInsideSphere_ReturnsNegativeDistance()
        {
            var point = new Vector3Df(2.0f, 0.0f, 0.0f);
            var sphereCenter = Vector3Df.Zero;
            var sphereRadius = 5.0f;
            var distance = XDistance3D.PointSphere(in point, in sphereCenter, sphereRadius);
            ClassicAssert.AreEqual(-3.0f, distance, EpsilonF); // 2 - 5 = -3
        }

        [Test]
        public void XDistance3D_LineSphere_WithIntersection_ReturnsZero()
        {
            var line = new Line3Df(Vector3Df.Zero, new Vector3Df(1.0f, 0.0f, 0.0f));
            var sphere = new Sphere3Df(new Vector3Df(5.0f, 0.0f, 0.0f), 1.0f);
            var distance = XDistance3D.LineSphere(in line, in sphere);
            ClassicAssert.AreEqual(0.0f, distance, EpsilonF);
        }

        [Test]
        public void XDistance3D_RaySphere_WithIntersection_ReturnsZero()
        {
            var ray = new Ray3Df(Vector3Df.Zero, new Vector3Df(1.0f, 0.0f, 0.0f));
            var sphere = new Sphere3Df(new Vector3Df(5.0f, 0.0f, 0.0f), 1.0f);
            var distance = XDistance3D.RaySphere(in ray, in sphere);
            ClassicAssert.AreEqual(0.0f, distance, EpsilonF);
        }

        [Test]
        public void XDistance3D_SphereSphere_ReturnsCorrectDistance()
        {
            var sphereA = new Sphere3Df(Vector3Df.Zero, 2.0f);
            var sphereB = new Sphere3Df(new Vector3Df(10.0f, 0.0f, 0.0f), 3.0f);
            var distance = XDistance3D.SphereSphere(in sphereA, in sphereB);
            ClassicAssert.AreEqual(5.0f, distance, EpsilonF); // 10 - 2 - 3 = 5
        }
        #endregion

        #region XIntersect3D Tests
        [Test]
        public void TIntersectHit3Df_None_ReturnsNoneHit()
        {
            var hit = TIntersectHit3Df.None();
            ClassicAssert.AreEqual(TIntersectType3D.None, hit.IntersectType);
        }

        [Test]
        public void TIntersectHit3Df_Point_ReturnsPointHit()
        {
            var point = new Vector3Df(1.0f, 2.0f, 3.0f);
            var hit = TIntersectHit3Df.Point(in point);
            ClassicAssert.AreEqual(TIntersectType3D.Point, hit.IntersectType);
            ClassicAssert.AreEqual(point, hit.Point1);
        }

        [Test]
        public void TIntersectHit3Df_Point_WithDistance_ReturnsPointHitWithDistance()
        {
            var point = new Vector3Df(1.0f, 2.0f, 3.0f);
            var distance = 5.0f;
            var hit = TIntersectHit3Df.Point(in point, distance);
            ClassicAssert.AreEqual(TIntersectType3D.Point, hit.IntersectType);
            ClassicAssert.AreEqual(point, hit.Point1);
            ClassicAssert.AreEqual(distance, hit.Distance, EpsilonF);
        }

        [Test]
        public void TIntersectHit3Df_Segment_ReturnsSegmentHit()
        {
            var point1 = new Vector3Df(0.0f, 0.0f, 0.0f);
            var point2 = new Vector3Df(5.0f, 0.0f, 0.0f);
            var hit = TIntersectHit3Df.Segment(in point1, in point2);
            ClassicAssert.AreEqual(TIntersectType3D.Segment, hit.IntersectType);
            ClassicAssert.AreEqual(point1, hit.Point1);
            ClassicAssert.AreEqual(point2, hit.Point2);
        }

        [Test]
        public void XIntersect3D_PointLine_WithPointOnLine_ReturnsTrue()
        {
            var point = new Vector3Df(5.0f, 0.0f, 0.0f);
            var line = new Line3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            ClassicAssert.IsTrue(XIntersect3D.PointLine(point, line));
        }

        [Test]
        public void XIntersect3D_PointLine_WithPointOffLine_ReturnsFalse()
        {
            var point = new Vector3Df(5.0f, 5.0f, 0.0f);
            var line = new Line3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            ClassicAssert.IsFalse(XIntersect3D.PointLine(point, line));
        }

        [Test]
        public void XIntersect3D_PointSphere_WithPointOnSphere_ReturnsTrue()
        {
            var point = new Vector3Df(5.0f, 0.0f, 0.0f);
            var sphere = new Sphere3Df(Vector3Df.Zero, 5.0f);
            ClassicAssert.IsTrue(XIntersect3D.PointSphere(in point, in sphere));
        }

        [Test]
        public void XIntersect3D_PointSphere_WithPointInsideSphere_ReturnsTrue()
        {
            var point = new Vector3Df(2.0f, 0.0f, 0.0f);
            var sphere = new Sphere3Df(Vector3Df.Zero, 5.0f);
            ClassicAssert.IsTrue(XIntersect3D.PointSphere(in point, in sphere));
        }

        [Test]
        public void XIntersect3D_PointSphere_WithPointOutsideSphere_ReturnsFalse()
        {
            var point = new Vector3Df(10.0f, 0.0f, 0.0f);
            var sphere = new Sphere3Df(Vector3Df.Zero, 5.0f);
            ClassicAssert.IsFalse(XIntersect3D.PointSphere(in point, in sphere));
        }

        [Test]
        public void XIntersect3D_LineSphere_WithIntersection_ReturnsTrue()
        {
            var line = new Line3Df(Vector3Df.Zero, new Vector3Df(1.0f, 0.0f, 0.0f));
            var sphere = new Sphere3Df(new Vector3Df(5.0f, 0.0f, 0.0f), 1.0f);
            ClassicAssert.IsTrue(XIntersect3D.LineSphere(in line, in sphere));
        }

        [Test]
        public void XIntersect3D_LineSphere_WithNoIntersection_ReturnsFalse()
        {
            var line = new Line3Df(new Vector3Df(0.0f, 10.0f, 0.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            var sphere = new Sphere3Df(Vector3Df.Zero, 5.0f);
            ClassicAssert.IsFalse(XIntersect3D.LineSphere(in line, in sphere));
        }

        [Test]
        public void XIntersect3D_RaySphere_WithIntersection_ReturnsTrue()
        {
            var ray = new Ray3Df(Vector3Df.Zero, new Vector3Df(1.0f, 0.0f, 0.0f));
            var sphere = new Sphere3Df(new Vector3Df(5.0f, 0.0f, 0.0f), 1.0f);
            ClassicAssert.IsTrue(XIntersect3D.RaySphere(in ray, in sphere));
        }

        [Test]
        public void XIntersect3D_RaySphere_WithNoIntersection_ReturnsFalse()
        {
            var ray = new Ray3Df(new Vector3Df(0.0f, 10.0f, 0.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            var sphere = new Sphere3Df(Vector3Df.Zero, 5.0f);
            ClassicAssert.IsFalse(XIntersect3D.RaySphere(in ray, in sphere));
        }

        [Test]
        public void XIntersect3D_SegmentSphere_WithIntersection_ReturnsTrue()
        {
            var segment = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(10.0f, 0.0f, 0.0f));
            var sphere = new Sphere3Df(new Vector3Df(5.0f, 0.0f, 0.0f), 1.0f);
            ClassicAssert.IsTrue(XIntersect3D.SegmentSphere(in segment, in sphere));
        }
        #endregion
    }
}
