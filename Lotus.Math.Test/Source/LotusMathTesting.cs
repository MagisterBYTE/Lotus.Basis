//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль математической системы
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMathTesting.cs
*		Тестирование методов математического модуля.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace Math
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для тестирования методов математического модуля
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XMathTesting
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="XMath"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestMath()
			{
				Assert.AreEqual(XMath.IsZero(0.00000000002), true);
				Assert.AreEqual(XMath.IsZero(0.000000002f), true);

				Assert.AreEqual(XMath.IsOne(1.00000000002), true);
				Assert.AreEqual(XMath.IsOne(1.000000002f), true);

				Assert.AreEqual(XMath.Clamp01(-000002), 0);
				Assert.AreEqual(XMath.Clamp01(-1.000002f), 0);
				Assert.AreEqual(XMath.Clamp01(2.00002), 1);
				Assert.AreEqual(XMath.Clamp01(1.000002f), 1);

				Assert.AreEqual(XMath.Clamp(550.0, 20.0, 50.0), 50.0);
				Assert.AreEqual(XMath.Clamp(550.0f, 20.0f, 50.0f), 50.0f);
				Assert.AreEqual(XMath.Clamp(550, 20, 50), 50);

				Assert.AreEqual(XMath.Clamp(-550.0, 20.0, 50.0), 20.0);
				Assert.AreEqual(XMath.Clamp(-550.0f, 20.0f, 50.0f), 20.0f);
				Assert.AreEqual(XMath.Clamp(-550, 20, 50), 20);

				Assert.AreEqual(XMath.Approximately(200.003, 200.0033), true);
				Assert.AreEqual(XMath.Approximately(200.003f, 200.0033f), true);

				Double x1 = 2;
				Double x2 = 5;
				Double y1 = 0;
				Double y2 = 6;
				Double px = 4;
				Assert.AreEqual(XMath.ConvertInterval(x1, x2, y1, y2, px), 4);

				x1 = 0;
				x2 = 10;
				y1 = 8;
				y2 = 0;
				px = 6;
				Assert.AreEqual(XMath.ConvertInterval(x1, x2, y1, y2, px), 3.2, 0.001);


				Assert.AreEqual(XMath.RoundToNearest(2335.0233, 1), 2335.0);
				Assert.AreEqual(XMath.RoundToNearest(2335.0233, 2), 2336.0);
				Assert.AreEqual(XMath.RoundToNearest(2335.0233, 10), 2340.0);
				Assert.AreEqual(XMath.RoundToNearest(2330.0233, 100), 2300.0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="XMathAngle"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestMathAngle()
			{
				Assert.AreEqual(XMathAngle.NormalizationFull(20.0), 20.0);
				Assert.AreEqual(XMathAngle.NormalizationFull(359.0), 359.0);
				Assert.AreEqual(XMathAngle.NormalizationFull(360.0), 0.0);
				Assert.AreEqual(XMathAngle.NormalizationFull(361.0), 1.0);
				Assert.AreEqual(XMathAngle.NormalizationFull(-2.0), 358.0);
				Assert.AreEqual(XMathAngle.NormalizationFull(-180.0), 180.0);

				Assert.AreEqual(XMathAngle.NormalizationFull(20.0f), 20.0f);
				Assert.AreEqual(XMathAngle.NormalizationFull(359.0f), 359.0f);
				Assert.AreEqual(XMathAngle.NormalizationFull(360.0f), 0.0f);
				Assert.AreEqual(XMathAngle.NormalizationFull(361.0f), 1.0f);
				Assert.AreEqual(XMathAngle.NormalizationFull(-2.0f), 358.0f);
				Assert.AreEqual(XMathAngle.NormalizationFull(-180.0f), 180.0f);

				Assert.AreEqual(XMathAngle.NormalizationHalf(20.0), 20.0);
				Assert.AreEqual(XMathAngle.NormalizationHalf(359.0), -1.0);
				Assert.AreEqual(XMathAngle.NormalizationHalf(360.0), 0.0);
				Assert.AreEqual(XMathAngle.NormalizationHalf(361.0), 1.0);
				Assert.AreEqual(XMathAngle.NormalizationHalf(-2.0), -2.0);
				Assert.AreEqual(XMathAngle.NormalizationHalf(-180.0), 180.0);
				Assert.AreEqual(XMathAngle.NormalizationHalf(270.0), -90.0);

				Assert.AreEqual(XMathAngle.NormalizationHalf(20.0f), 20.0f);
				Assert.AreEqual(XMathAngle.NormalizationHalf(359.0f), -1.0f);
				Assert.AreEqual(XMathAngle.NormalizationHalf(360.0f), 0.0f);
				Assert.AreEqual(XMathAngle.NormalizationHalf(361.0f), 1.0f);
				Assert.AreEqual(XMathAngle.NormalizationHalf(-2.0f), -2.0f);
				Assert.AreEqual(XMathAngle.NormalizationHalf(-180.0f), 180.0f);
				Assert.AreEqual(XMathAngle.NormalizationHalf(270.0f), -90.0f);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="XClosest2D"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestMathClosest2D()
			{
				Vector2Df line_pos = new Vector2Df(0, 8);
				Vector2Df line_dir = new Vector2Df(1, -1).Normalized;
				Vector2Df point = new Vector2Df(4, 2);
				Single distance = 0;
				Vector2Df result = XClosest2D.PointLine(point, line_pos, line_dir, out distance);
				Assert.AreEqual(Vector2Df.Approximately(result, new Vector2Df(5, 3), 0.1f), true);
				Assert.AreEqual(XMath.Approximately(distance, 7f, 0.1f), true);

				point = new Vector2Df(2, 5);
				result = XClosest2D.PointLine(point, line_pos, line_dir, out distance);
				Assert.AreEqual(Vector2Df.Approximately(result, new Vector2Df(2.5f, 5.5f), 0.1f), true);
				Assert.AreEqual(XMath.Approximately(distance, 3.5f, 0.1f), true);

				result = XClosest2D.PointSegment(point, line_pos, new Vector2Df(8, 0), out distance);
				Assert.AreEqual(Vector2Df.Approximately(result, new Vector2Df(2.5f, 5.5f), 0.1f), true);
				Assert.AreEqual(XMath.Approximately(distance, 0.3f, 0.1f), true);

				point = new Vector2Df(2.5f, 5.5f);
				result = XClosest2D.PointSegment(point, line_pos, new Vector2Df(8, 0), out distance);
				Assert.AreEqual(Vector2Df.Approximately(result, new Vector2Df(2.5f, 5.5f), 0.1f), true);
				Assert.AreEqual(XMath.Approximately(distance, 0.315f, 0.01f), true);
			}
		}
	}
}
//=====================================================================================================================