using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DPlaneTests
    {
        private const double EpsilonD = 0.00001;
        private const float EpsilonF = 0.00001f;

        #region Plane3D Tests
        [Test]
        public void Plane3D_Constructor_WithComponents_InitializesCorrectly()
        {
            var plane = new Plane3D(1.0, 2.0, 3.0, 4.0);
            ClassicAssert.AreEqual(1.0, plane.Normal.X, EpsilonD);
            ClassicAssert.AreEqual(2.0, plane.Normal.Y, EpsilonD);
            ClassicAssert.AreEqual(3.0, plane.Normal.Z, EpsilonD);
            ClassicAssert.AreEqual(4.0, plane.Distance, EpsilonD);
        }

        [Test]
        public void Plane3D_Constructor_WithPointAndNormal_InitializesCorrectly()
        {
            var point = new Vector3D(1.0, 2.0, 3.0);
            var normal = new Vector3D(0.0, 1.0, 0.0);
            var plane = new Plane3D(in point, in normal);
            ClassicAssert.AreEqual(normal, plane.Normal);
            ClassicAssert.AreEqual(-2.0, plane.Distance, EpsilonD); // -(normal * point) = -(0*1 + 1*2 + 0*3) = -2
        }

        [Test]
        public void Plane3D_Constructor_WithThreePoints_InitializesCorrectly()
        {
            var p1 = new Vector3D(0.0, 0.0, 0.0);
            var p2 = new Vector3D(1.0, 0.0, 0.0);
            var p3 = new Vector3D(0.0, 1.0, 0.0);
            var plane = new Plane3D(in p1, in p2, in p3);
            // Плоскость XY должна иметь нормаль (0, 0, 1)
            ClassicAssert.AreEqual(0.0, plane.Normal.X, 0.1);
            ClassicAssert.AreEqual(0.0, plane.Normal.Y, 0.1);
            ClassicAssert.AreEqual(1.0, plane.Normal.Z, 0.1);
        }

        [Test]
        public void Plane3D_GetDistanceToPoint_ReturnsCorrectDistance()
        {
            var plane = new Plane3D(0.0, 1.0, 0.0, 0.0); // Плоскость Y=0
            var point = new Vector3D(0.0, 5.0, 0.0);
            ClassicAssert.AreEqual(5.0, plane.GetDistanceToPoint(in point), EpsilonD);
        }

        [Test]
        public void Plane3D_ComputeVector_ReturnsCorrectDotProduct()
        {
            var plane = new Plane3D(0.0, 1.0, 0.0, 0.0);
            var vector = new Vector3D(0.0, 2.0, 0.0);
            ClassicAssert.AreEqual(2.0, plane.ComputeVector(in vector), EpsilonD);
        }

        [Test]
        public void Plane3D_ComputePoint_ReturnsCorrectValue()
        {
            var plane = new Plane3D(0.0, 1.0, 0.0, 0.0); // Плоскость Y=0
            var point = new Vector3D(0.0, 5.0, 0.0);
            ClassicAssert.AreEqual(5.0, plane.ComputePoint(in point), EpsilonD);
        }

        [Test]
        public void Plane3D_Normalize_NormalizesPlane()
        {
            var plane = new Plane3D(2.0, 0.0, 0.0, 4.0);
            plane.Normalize();
            ClassicAssert.AreEqual(1.0, plane.Normal.Length, EpsilonD);
        }
        #endregion

        #region Plane3Df Tests
        [Test]
        public void Plane3Df_Constructor_WithComponents_InitializesCorrectly()
        {
            var plane = new Plane3Df(1.0f, 2.0f, 3.0f, 4.0f);
            ClassicAssert.AreEqual(1.0f, plane.Normal.X, EpsilonF);
            ClassicAssert.AreEqual(2.0f, plane.Normal.Y, EpsilonF);
            ClassicAssert.AreEqual(3.0f, plane.Normal.Z, EpsilonF);
            ClassicAssert.AreEqual(4.0f, plane.Distance, EpsilonF);
        }

        [Test]
        public void Plane3Df_Constructor_WithPointAndNormal_InitializesCorrectly()
        {
            var point = new Vector3Df(1.0f, 2.0f, 3.0f);
            var normal = new Vector3Df(0.0f, 1.0f, 0.0f);
            var plane = new Plane3Df(in point, in normal);
            ClassicAssert.AreEqual(normal, plane.Normal);
            ClassicAssert.AreEqual(-2.0f, plane.Distance, EpsilonF);
        }

        [Test]
        public void Plane3Df_Constructor_WithThreePoints_InitializesCorrectly()
        {
            var p1 = new Vector3Df(0.0f, 0.0f, 0.0f);
            var p2 = new Vector3Df(1.0f, 0.0f, 0.0f);
            var p3 = new Vector3Df(0.0f, 1.0f, 0.0f);
            var plane = new Plane3Df(in p1, in p2, in p3);
            ClassicAssert.AreEqual(0.0f, plane.Normal.X, 0.1f);
            ClassicAssert.AreEqual(0.0f, plane.Normal.Y, 0.1f);
            ClassicAssert.AreEqual(1.0f, plane.Normal.Z, 0.1f);
        }

        [Test]
        public void Plane3Df_GetDistanceToPoint_ReturnsCorrectDistance()
        {
            var plane = new Plane3Df(0.0f, 1.0f, 0.0f, 0.0f);
            var point = new Vector3Df(0.0f, 5.0f, 0.0f);
            ClassicAssert.AreEqual(5.0f, plane.GetDistanceToPoint(in point), EpsilonF);
        }

        [Test]
        public void Plane3Df_ComputeVector_ReturnsCorrectDotProduct()
        {
            var plane = new Plane3Df(0.0f, 1.0f, 0.0f, 0.0f);
            var vector = new Vector3Df(0.0f, 2.0f, 0.0f);
            ClassicAssert.AreEqual(2.0f, plane.ComputeVector(in vector), EpsilonF);
        }

        [Test]
        public void Plane3Df_ComputePoint_ReturnsCorrectValue()
        {
            var plane = new Plane3Df(0.0f, 1.0f, 0.0f, 0.0f);
            var point = new Vector3Df(0.0f, 5.0f, 0.0f);
            ClassicAssert.AreEqual(5.0f, plane.ComputePoint(in point), EpsilonF);
        }

        [Test]
        public void Plane3Df_Normalize_NormalizesPlane()
        {
            var plane = new Plane3Df(2.0f, 0.0f, 0.0f, 4.0f);
            plane.Normalize();
            ClassicAssert.AreEqual(1.0f, plane.Normal.Length, EpsilonF);
        }

        [Test]
        public void Plane3Df_Equals_ReturnsTrueForEqualPlanes()
        {
            var plane1 = new Plane3Df(1.0f, 2.0f, 3.0f, 4.0f);
            var plane2 = new Plane3Df(1.0f, 2.0f, 3.0f, 4.0f);
            ClassicAssert.IsTrue(plane1.Equals(plane2));
        }

        [Test]
        public void Plane3Df_GetHashCode_ReturnsConsistentHashCode()
        {
            var plane1 = new Plane3Df(1.0f, 2.0f, 3.0f, 4.0f);
            var plane2 = new Plane3Df(1.0f, 2.0f, 3.0f, 4.0f);
            ClassicAssert.AreEqual(plane1.GetHashCode(), plane2.GetHashCode());
        }
        #endregion
    }
}
