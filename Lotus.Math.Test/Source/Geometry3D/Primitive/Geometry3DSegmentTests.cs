using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DSegmentTests
    {
        private const float EpsilonF = 0.00001f;

        [Test]
        public void Segment3Df_Constructor_InitializesCorrectly()
        {
            var start = new Vector3Df(0.0f, 0.0f, 0.0f);
            var end = new Vector3Df(5.0f, 0.0f, 0.0f);
            var segment = new Segment3Df(start, end);
            ClassicAssert.AreEqual(start, segment.Start);
            ClassicAssert.AreEqual(end, segment.End);
        }

        [Test]
        public void Segment3Df_CopyConstructor_InitializesCorrectly()
        {
            var original = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            var copy = new Segment3Df(original);
            ClassicAssert.AreEqual(original.Start, copy.Start);
            ClassicAssert.AreEqual(original.End, copy.End);
        }

        [Test]
        public void Segment3Df_Location_ReturnsCorrectCenter()
        {
            var segment = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(10.0f, 0.0f, 0.0f));
            var center = segment.Location;
            ClassicAssert.AreEqual(5.0f, center.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, center.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, center.Z, EpsilonF);
        }

        [Test]
        public void Segment3Df_Direction_ReturnsCorrectDirection()
        {
            var segment = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            var direction = segment.Direction;
            ClassicAssert.AreEqual(5.0f, direction.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, direction.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, direction.Z, EpsilonF);
        }

        [Test]
        public void Segment3Df_DirectionUnit_ReturnsNormalizedDirection()
        {
            var segment = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            var directionUnit = segment.DirectionUnit;
            ClassicAssert.AreEqual(1.0f, directionUnit.X, EpsilonF);
            ClassicAssert.AreEqual(0.0f, directionUnit.Y, EpsilonF);
            ClassicAssert.AreEqual(0.0f, directionUnit.Z, EpsilonF);
            ClassicAssert.AreEqual(1.0f, directionUnit.Length, EpsilonF);
        }

        [Test]
        public void Segment3Df_Equals_ReturnsTrueForEqualSegments()
        {
            var segment1 = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            var segment2 = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            ClassicAssert.IsTrue(segment1.Equals(segment2));
            ClassicAssert.IsTrue(segment1.Equals((object)segment2));
        }

        [Test]
        public void Segment3Df_Equals_ReturnsFalseForDifferentSegments()
        {
            var segment1 = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            var segment2 = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(10.0f, 0.0f, 0.0f));
            ClassicAssert.IsFalse(segment1.Equals(segment2));
            ClassicAssert.IsFalse(segment1.Equals(null));
        }

        [Test]
        public void Segment3Df_GetHashCode_ReturnsConsistentHashCode()
        {
            var segment1 = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            var segment2 = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            ClassicAssert.AreEqual(segment1.GetHashCode(), segment2.GetHashCode());
        }

        [Test]
        public void Segment3Df_ToString_ReturnsFormattedString()
        {
            var segment = new Segment3Df(new Vector3Df(1.23f, 4.56f, 7.89f), new Vector3Df(10.12f, 13.45f, 16.78f));
            var result = segment.ToString().Replace(',', '.');
            ClassicAssert.IsTrue(result.Contains("1.23"));
            ClassicAssert.IsTrue(result.Contains("4.56"));
        }

        [Test]
        public void Segment3Df_EqualityOperator_ReturnsTrueForEqualSegments()
        {
            var s1 = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            var s2 = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            ClassicAssert.IsTrue(s1 == s2);
        }

        [Test]
        public void Segment3Df_InequalityOperator_ReturnsTrueForDifferentSegments()
        {
            var s1 = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(5.0f, 0.0f, 0.0f));
            var s2 = new Segment3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(10.0f, 0.0f, 0.0f));
            ClassicAssert.IsTrue(s1 != s2);
        }
    }
}
