using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DSphereTests
    {
        private const float EpsilonF = 0.00001f;

        [Test]
        public void Sphere3Df_Constructor_InitializesCorrectly()
        {
            var center = new Vector3Df(1.0f, 2.0f, 3.0f);
            var radius = 5.0f;
            var sphere = new Sphere3Df(center, radius);
            ClassicAssert.AreEqual(center, sphere.Center);
            ClassicAssert.AreEqual(radius, sphere.Radius, EpsilonF);
        }

        [Test]
        public void Sphere3Df_CopyConstructor_InitializesCorrectly()
        {
            var original = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 5.0f);
            var copy = new Sphere3Df(original);
            ClassicAssert.AreEqual(original.Center, copy.Center);
            ClassicAssert.AreEqual(original.Radius, copy.Radius, EpsilonF);
        }

        [Test]
        public void Sphere3Df_Equals_ReturnsTrueForEqualSpheres()
        {
            var sphere1 = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 5.0f);
            var sphere2 = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 5.0f);
            ClassicAssert.IsTrue(sphere1.Equals(sphere2));
            ClassicAssert.IsTrue(sphere1.Equals((object)sphere2));
        }

        [Test]
        public void Sphere3Df_Equals_ReturnsFalseForDifferentSpheres()
        {
            var sphere1 = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 5.0f);
            var sphere2 = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 6.0f);
            ClassicAssert.IsFalse(sphere1.Equals(sphere2));
            ClassicAssert.IsFalse(sphere1.Equals(null));
        }

        [Test]
        public void Sphere3Df_GetHashCode_ReturnsConsistentHashCode()
        {
            var sphere1 = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 5.0f);
            var sphere2 = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 5.0f);
            ClassicAssert.AreEqual(sphere1.GetHashCode(), sphere2.GetHashCode());
        }

        [Test]
        public void Sphere3Df_ToString_ReturnsFormattedString()
        {
            var sphere = new Sphere3Df(new Vector3Df(1.23f, 4.56f, 7.89f), 10.12f);
            var result = sphere.ToString().Replace(',', '.');
            ClassicAssert.IsTrue(result.Contains("1.23"));
            ClassicAssert.IsTrue(result.Contains("4.56"));
            ClassicAssert.IsTrue(result.Contains("10.12"));
        }

        [Test]
        public void Sphere3Df_EqualityOperator_ReturnsTrueForEqualSpheres()
        {
            var s1 = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 5.0f);
            var s2 = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 5.0f);
            ClassicAssert.IsTrue(s1 == s2);
        }

        [Test]
        public void Sphere3Df_InequalityOperator_ReturnsTrueForDifferentSpheres()
        {
            var s1 = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 5.0f);
            var s2 = new Sphere3Df(new Vector3Df(1.0f, 2.0f, 3.0f), 6.0f);
            ClassicAssert.IsTrue(s1 != s2);
        }
    }
}
