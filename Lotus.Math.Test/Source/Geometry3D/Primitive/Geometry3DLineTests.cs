using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Lotus.Maths
{
    [TestFixture]
    public class Geometry3DLineTests
    {
        private const float EpsilonF = 0.00001f;

        [Test]
        public void Line3Df_Constructor_InitializesCorrectly()
        {
            var position = new Vector3Df(1.0f, 2.0f, 3.0f);
            var direction = new Vector3Df(0.0f, 1.0f, 0.0f);
            var line = new Line3Df(position, direction);
            ClassicAssert.AreEqual(position, line.Position);
            ClassicAssert.AreEqual(direction, line.Direction);
        }

        [Test]
        public void Line3Df_CopyConstructor_InitializesCorrectly()
        {
            var original = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(0.0f, 1.0f, 0.0f));
            var copy = new Line3Df(original);
            ClassicAssert.AreEqual(original.Position, copy.Position);
            ClassicAssert.AreEqual(original.Direction, copy.Direction);
        }

        [Test]
        public void Line3Df_StaticConstants_AreCorrect()
        {
            ClassicAssert.AreEqual(Vector3Df.Zero, Line3Df.XAxis.Position);
            ClassicAssert.AreEqual(Vector3Df.Right, Line3Df.XAxis.Direction);
            ClassicAssert.AreEqual(Vector3Df.Zero, Line3Df.YAxis.Position);
            ClassicAssert.AreEqual(Vector3Df.Up, Line3Df.YAxis.Direction);
            ClassicAssert.AreEqual(Vector3Df.Zero, Line3Df.ZAxis.Position);
            ClassicAssert.AreEqual(Vector3Df.Forward, Line3Df.ZAxis.Direction);
        }

        [Test]
        public void Line3Df_Equals_ReturnsTrueForEqualLines()
        {
            var line1 = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(0.0f, 1.0f, 0.0f));
            var line2 = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(0.0f, 1.0f, 0.0f));
            ClassicAssert.IsTrue(line1.Equals(line2));
            ClassicAssert.IsTrue(line1.Equals((object)line2));
        }

        [Test]
        public void Line3Df_Equals_ReturnsFalseForDifferentLines()
        {
            var line1 = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(0.0f, 1.0f, 0.0f));
            var line2 = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            ClassicAssert.IsFalse(line1.Equals(line2));
            ClassicAssert.IsFalse(line1.Equals(null));
        }

        [Test]
        public void Line3Df_GetHashCode_ReturnsConsistentHashCode()
        {
            var line1 = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(0.0f, 1.0f, 0.0f));
            var line2 = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(0.0f, 1.0f, 0.0f));
            ClassicAssert.AreEqual(line1.GetHashCode(), line2.GetHashCode());
        }

        [Test]
        public void Line3Df_ToString_ReturnsFormattedString()
        {
            var line = new Line3Df(new Vector3Df(1.23f, 4.56f, 7.89f), new Vector3Df(0.78f, 0.91f, 0.12f));
            var result = line.ToString().Replace(',' , '.');
            ClassicAssert.IsTrue(result.Contains("1.23"));
            ClassicAssert.IsTrue(result.Contains("4.56"));
        }

        [Test]
        public void Line3Df_EqualityOperator_ReturnsTrueForEqualLines()
        {
            var l1 = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(0.0f, 1.0f, 0.0f));
            var l2 = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(0.0f, 1.0f, 0.0f));
            ClassicAssert.IsTrue(l1 == l2);
        }

        [Test]
        public void Line3Df_InequalityOperator_ReturnsTrueForDifferentLines()
        {
            var l1 = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(0.0f, 1.0f, 0.0f));
            var l2 = new Line3Df(new Vector3Df(1.0f, 2.0f, 3.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            ClassicAssert.IsTrue(l1 != l2);
        }

        [Test]
        public void Line3Df_GetPoint_ReturnsCorrectPointOnLine()
        {
            var line = new Line3Df(new Vector3Df(0.0f, 0.0f, 0.0f), new Vector3Df(1.0f, 0.0f, 0.0f));
            var point = line.GetPoint(5.0f);
            ClassicAssert.AreEqual(new Vector3Df(5.0f, 0.0f, 0.0f), point);
        }

        [Test]
        public void Line3Df_SetFromPoint_SetsLineFromTwoPoints()
        {
            var line = new Line3Df();
            var startPoint = new Vector3Df(0.0f, 0.0f, 0.0f);
            var endPoint = new Vector3Df(5.0f, 0.0f, 0.0f);
            line.SetFromPoint(in startPoint, in endPoint);
            ClassicAssert.AreEqual(startPoint, line.Position);
            ClassicAssert.AreEqual(new Vector3Df(1.0f, 0.0f, 0.0f), line.Direction);
        }
    }
}
