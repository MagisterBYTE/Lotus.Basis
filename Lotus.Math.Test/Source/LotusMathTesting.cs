#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    /// <summary>
    /// Статический класс для тестирования методов математического модуля.
    /// </summary>
    public static class XMathTesting
    {
        /// <summary>
        /// Тестирование методов <see cref="XMath"/>.
        /// </summary>
        [Test]
        public static void TestMath()
        {
            ClassicAssert.AreEqual(true, XMath.IsZero(0.00000000002));
            ClassicAssert.AreEqual(true, XMath.IsZero(0.000000002f));

            ClassicAssert.AreEqual(true, XMath.IsOne(1.00000000002));
            ClassicAssert.AreEqual(true, XMath.IsOne(1.000000002f));

            ClassicAssert.AreEqual(0, XMath.Clamp01(-000002));
            ClassicAssert.AreEqual(0, XMath.Clamp01(-1.000002f));
            ClassicAssert.AreEqual(1, XMath.Clamp01(2.00002));
            ClassicAssert.AreEqual(1, XMath.Clamp01(1.000002f));

            ClassicAssert.AreEqual(50.0, XMath.Clamp(550.0, 20.0, 50.0));
            ClassicAssert.AreEqual(50.0f, XMath.Clamp(550.0f, 20.0f, 50.0f));
            ClassicAssert.AreEqual(50, XMath.Clamp(550, 20, 50));

            ClassicAssert.AreEqual(20.0, XMath.Clamp(-550.0, 20.0, 50.0));
            ClassicAssert.AreEqual(20.0f, XMath.Clamp(-550.0f, 20.0f, 50.0f));
            ClassicAssert.AreEqual(20, XMath.Clamp(-550, 20, 50));

            ClassicAssert.AreEqual(true, XMath.Approximately(200.003, 200.0033));
            ClassicAssert.AreEqual(true, XMath.Approximately(200.003f, 200.0033f));

            double x1 = 2;
            double x2 = 5;
            double y1 = 0;
            double y2 = 6;
            double px = 4;
            ClassicAssert.AreEqual(4, XMath.ConvertInterval(x1, x2, y1, y2, px));

            x1 = 0;
            x2 = 10;
            y1 = 8;
            y2 = 0;
            px = 6;
            ClassicAssert.AreEqual(3.2, XMath.ConvertInterval(x1, x2, y1, y2, px), 0.001);


            ClassicAssert.AreEqual(2335.0, XMath.RoundToNearest(2335.0233, 1));
            ClassicAssert.AreEqual(2336.0, XMath.RoundToNearest(2335.0233, 2));
            ClassicAssert.AreEqual(2340.0, XMath.RoundToNearest(2335.0233, 10));
            ClassicAssert.AreEqual(2300.0, XMath.RoundToNearest(2330.0233, 100));

            ClassicAssert.AreEqual(1782.6f, XMath.RoundToSingle(1782.56f, 0.2f));
            ClassicAssert.AreEqual(1782.6f, XMath.RoundToSingle(1782.5f, 0.2f));
            ClassicAssert.AreEqual(1782.4f, XMath.RoundToSingle(1782.41f, 0.2f));
        }

        /// <summary>
        /// Тестирование методов <see cref="XMathAngle"/>.
        /// </summary>
        [Test]
        public static void TestMathAngle()
        {
            ClassicAssert.AreEqual(20.0, XMathAngle.NormalizationFull(20.0));
            ClassicAssert.AreEqual(359.0, XMathAngle.NormalizationFull(359.0));
            ClassicAssert.AreEqual(0.0, XMathAngle.NormalizationFull(360.0));
            ClassicAssert.AreEqual(1.0, XMathAngle.NormalizationFull(361.0));
            ClassicAssert.AreEqual(358.0, XMathAngle.NormalizationFull(-2.0));
            ClassicAssert.AreEqual(180.0, XMathAngle.NormalizationFull(-180.0));

            ClassicAssert.AreEqual(20.0f, XMathAngle.NormalizationFull(20.0f));
            ClassicAssert.AreEqual(359.0f, XMathAngle.NormalizationFull(359.0f));
            ClassicAssert.AreEqual(0.0f, XMathAngle.NormalizationFull(360.0f));
            ClassicAssert.AreEqual(1.0f, XMathAngle.NormalizationFull(361.0f));
            ClassicAssert.AreEqual(358.0f, XMathAngle.NormalizationFull(-2.0f));
            ClassicAssert.AreEqual(180.0f, XMathAngle.NormalizationFull(-180.0f));

            ClassicAssert.AreEqual(20.0, XMathAngle.NormalizationHalf(20.0));
            ClassicAssert.AreEqual(-1.0, XMathAngle.NormalizationHalf(359.0));
            ClassicAssert.AreEqual(0.0, XMathAngle.NormalizationHalf(360.0));
            ClassicAssert.AreEqual(1.0, XMathAngle.NormalizationHalf(361.0));
            ClassicAssert.AreEqual(-2.0, XMathAngle.NormalizationHalf(-2.0));
            ClassicAssert.AreEqual(180.0, XMathAngle.NormalizationHalf(-180.0));
            ClassicAssert.AreEqual(-90.0, XMathAngle.NormalizationHalf(270.0));

            ClassicAssert.AreEqual(20.0f, XMathAngle.NormalizationHalf(20.0f));
            ClassicAssert.AreEqual(-1.0f, XMathAngle.NormalizationHalf(359.0f));
            ClassicAssert.AreEqual(0.0f, XMathAngle.NormalizationHalf(360.0f));
            ClassicAssert.AreEqual(1.0f, XMathAngle.NormalizationHalf(361.0f));
            ClassicAssert.AreEqual(-2.0f, XMathAngle.NormalizationHalf(-2.0f));
            ClassicAssert.AreEqual(180.0f, XMathAngle.NormalizationHalf(-180.0f));
            ClassicAssert.AreEqual(-90.0f, XMathAngle.NormalizationHalf(270.0f));
        }

        /// <summary>
        /// Тестирование методов <see cref="XClosest2D"/>.
        /// </summary>
        [Test]
        public static void TestMathClosest2D()
        {
            var line_pos = new Vector2Df(0, 8);
            var line_dir = new Vector2Df(1, -1).Normalized;
            var point = new Vector2Df(4, 2);
            float distance;
            var result = XClosest2D.PointLine(point, line_pos, line_dir, out distance);
            ClassicAssert.AreEqual(true, Vector2Df.Approximately(result, new Vector2Df(5, 3), 0.1f));
            ClassicAssert.AreEqual(true, XMath.Approximately(distance, 7f, 0.1f));

            point = new Vector2Df(2, 5);
            result = XClosest2D.PointLine(point, line_pos, line_dir, out distance);
            ClassicAssert.AreEqual(true, Vector2Df.Approximately(result, new Vector2Df(2.5f, 5.5f), 0.1f));
            ClassicAssert.AreEqual(true, XMath.Approximately(distance, 3.5f, 0.1f));

            result = XClosest2D.PointSegment(point, line_pos, new Vector2Df(8, 0), out distance);
            ClassicAssert.AreEqual(true, Vector2Df.Approximately(result, new Vector2Df(2.5f, 5.5f), 0.1f));
            ClassicAssert.AreEqual(true, XMath.Approximately(distance, 0.3f, 0.1f));

            point = new Vector2Df(2.5f, 5.5f);
            result = XClosest2D.PointSegment(point, line_pos, new Vector2Df(8, 0), out distance);
            ClassicAssert.AreEqual(true, Vector2Df.Approximately(result, new Vector2Df(2.5f, 5.5f), 0.1f));
            ClassicAssert.AreEqual(true, XMath.Approximately(distance, 0.315f, 0.01f));
        }
    }
}