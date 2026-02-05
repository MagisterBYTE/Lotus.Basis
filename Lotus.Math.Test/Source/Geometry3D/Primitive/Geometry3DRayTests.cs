using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DRayTests
    {
        private const double EpsilonD = 0.00001;
        private const float EpsilonF = 0.00001f;

        #region Ray3D Tests
        [Test]
        public void Ray3D_Constructor_InitializesCorrectly()
        {
            var position = new Vector3D(1.0, 2.0, 3.0);
            var direction = new Vector3D(0.0, 1.0, 0.0);
            var ray = new Ray3D(position, direction);
            ClassicAssert.AreEqual(position, ray.Position);
            ClassicAssert.AreEqual(direction, ray.Direction);
        }

        [Test]
        public void Ray3D_CopyConstructor_InitializesCorrectly()
        {
            var original = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(0.0, 1.0, 0.0));
            var copy = new Ray3D(original);
            ClassicAssert.AreEqual(original.Position, copy.Position);
            ClassicAssert.AreEqual(original.Direction, copy.Direction);
        }

        [Test]
        public void Ray3D_Equals_ReturnsTrueForEqualRays()
        {
            var ray1 = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(0.0, 1.0, 0.0));
            var ray2 = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(0.0, 1.0, 0.0));
            ClassicAssert.IsTrue(ray1.Equals(ray2));
            ClassicAssert.IsTrue(ray1.Equals((object)ray2));
        }

        [Test]
        public void Ray3D_Equals_ReturnsFalseForDifferentRays()
        {
            var ray1 = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(0.0, 1.0, 0.0));
            var ray2 = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(1.0, 0.0, 0.0));
            ClassicAssert.IsFalse(ray1.Equals(ray2));
            ClassicAssert.IsFalse(ray1.Equals(null));
        }

        [Test]
        public void Ray3D_GetHashCode_ReturnsConsistentHashCode()
        {
            var ray1 = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(0.0, 1.0, 0.0));
            var ray2 = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(0.0, 1.0, 0.0));
            ClassicAssert.AreEqual(ray1.GetHashCode(), ray2.GetHashCode());
        }

        [Test]
        public void Ray3D_ToString_ReturnsFormattedString()
        {
            var ray = new Ray3D(new Vector3D(1.23, 4.56, 7.89), new Vector3D(0.78, 0.91, 0.12));
            var result = ray.ToString().Replace(',', '.');
            ClassicAssert.IsTrue(result.Contains("1.23"));
            ClassicAssert.IsTrue(result.Contains("4.56"));
        }

        [Test]
        public void Ray3D_EqualityOperator_ReturnsTrueForEqualRays()
        {
            var r1 = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(0.0, 1.0, 0.0));
            var r2 = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(0.0, 1.0, 0.0));
            ClassicAssert.IsTrue(r1 == r2);
        }

        [Test]
        public void Ray3D_InequalityOperator_ReturnsTrueForDifferentRays()
        {
            var r1 = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(0.0, 1.0, 0.0));
            var r2 = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(1.0, 0.0, 0.0));
            ClassicAssert.IsTrue(r1 != r2);
        }

        [Test]
        public void Ray3D_GetPoint_ReturnsCorrectPointOnRay()
        {
            var ray = new Ray3D(new Vector3D(0.0, 0.0, 0.0), new Vector3D(1.0, 0.0, 0.0));
            var point = ray.GetPoint(5.0);
            ClassicAssert.AreEqual(new Vector3D(5.0, 0.0, 0.0), point);
        }

        [Test]
        public void Ray3D_NegationOperator_NegatesRayDirectionCorrectly()
        {
            var ray = new Ray3D(new Vector3D(1.0, 2.0, 3.0), new Vector3D(1.0, 0.0, 0.0));
            var result = -ray;
            ClassicAssert.AreEqual(ray.Position, result.Position);
            ClassicAssert.AreEqual(new Vector3D(-1.0, 0.0, 0.0), result.Direction);
        }
        #endregion
    }
}
