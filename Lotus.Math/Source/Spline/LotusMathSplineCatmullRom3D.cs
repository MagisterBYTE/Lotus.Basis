//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема для работы со сплайнами
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMathSplineCatmullRom3D.cs
*		Алгоритм Catmull-Rom сплайна для трехмерного пространства.
*		Реализация алгоритма кубического Catmull-Rom интерполяционного сплайна для трехмерного пространства.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup MathSpline
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Алгоритм Catmull-Rom сплайна для трехмерного пространства
		/// </summary>
		/// <remarks>
		/// Реализация алгоритма кубического Catmull-Rom интерполяционного сплайна для трехмерного пространства
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CCatmullRomSpline3D : CSplineBase3D
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на сплайне Catmull-Rom представленной с помощью четырех контрольных точек
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="p0">Первая контрольная точка</param>
			/// <param name="p1">Вторая контрольная точка</param>
			/// <param name="p2">Третья контрольная точка</param>
			/// <param name="p3">Четвертая контрольная точка</param>
			/// <returns>Позиция точки на сплайне Catmull-Rom</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df CalculatePoint(Single time, in Vector3Df p0, in Vector3Df p1, in Vector3Df p2, in Vector3Df p3)
			{
				//The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
				Vector3Df a = 2f * p1;
				Vector3Df b = p2 - p0;
				Vector3Df c = (2f * p0) - (5f * p1) + (4f * p2) - p3;
				Vector3Df d = -p0 + (3f * p1) - (3f * p2) + p3;

				//The cubic polynomial: a + b * t + c * t^2 + d * t^3
				Vector3Df point = 0.5f * (a + (b * time) + (c * time * time) + (d * time * time * time));

				return point;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление первой производной точки на сплайне Catmull-Rom представленной с помощью четырех контрольных точек
			/// </summary>
			/// <remarks>
			/// Первая производная показывает скорость изменения функции в данной точки.
			/// Физическим смысл производной - скорость в данной точке 
			/// </remarks>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="p0">Первая контрольная точка</param>
			/// <param name="p1">Вторая контрольная точка</param>
			/// <param name="p2">Третья контрольная точка</param>
			/// <param name="p3">Четвертая контрольная точка</param>
			/// <returns>Первая производная  на сплайне Catmull-Rom</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df CalculateFirstDerivative(Single time, in Vector3Df p0, in Vector3Df p1, in Vector3Df p2, in Vector3Df p3)
			{
				Vector3Df a = ((p1 - p2) * 1.5f) + ((p3 - p0) * 0.5f);
				Vector3Df b = (p2 * 2.0f) - (p1 * 2.5f) - (p3 * 0.5f) + p0;
				Vector3Df c = (p2 - p0) * 0.5f;
				return (3 * a * time) + (2 * b * time) + c;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			internal Boolean mIsClosed;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Статус замкнутости сплайна
			/// </summary>
			public Boolean IsClosed
			{
				get { return mIsClosed; }
				set
				{
					if (mIsClosed != value)
					{
						mIsClosed = value;
						OnUpdateSpline();
					}
				}
			}

			/// <summary>
			/// Количество кривых в пути
			/// </summary>
			public Int32 CurveCount
			{
				get { return mControlPoints.Length - 1; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CCatmullRomSpline3D()
				: base(4)
			{
				mControlPoints[0] = Vector3Df.Zero;
				mControlPoints[1] = Vector3Df.Zero;
				mControlPoints[2] = Vector3Df.Zero;
				mControlPoints[3] = Vector3Df.Zero;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="count">Количество контрольных точек</param>
			//---------------------------------------------------------------------------------------------------------
			public CCatmullRomSpline3D(Int32 count)
				: base(count)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="startPoint">Начальная точка</param>
			/// <param name="endPoint">Конечная точка</param>
			//---------------------------------------------------------------------------------------------------------
			public CCatmullRomSpline3D(Vector3Df startPoint, Vector3Df endPoint)
				: base(4)
			{
				mControlPoints[0] = startPoint;
				mControlPoints[1] = (startPoint + endPoint) / 3;
				mControlPoints[2] = (startPoint + endPoint) / 3 * 2;
				mControlPoints[3] = endPoint;
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusSpline3D =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на сплайне
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1</param>
			/// <returns>Позиция точки на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Vector3Df CalculatePoint(Single time)
			{
				Int32 index_curve;
				if (time >= 1f)
				{
					time = 1f;
					index_curve = mControlPoints.Length - 1;
				}
				else
				{
					time = XMath.Clamp01(time) * CurveCount;
					index_curve = (Int32)time;
					time -= index_curve;
					index_curve *= 1;
				}

				if (mIsClosed)
				{
					// Получаем индексы точек
					var ip_0 = ClampPosition(index_curve - 1);
					var ip_1 = ClampPosition(index_curve);
					var ip_2 = ClampPosition(index_curve + 1);
					var ip_3 = ClampPosition(index_curve + 2);

					Vector3Df point = CalculatePoint(time,
						in mControlPoints[ip_0],
						in mControlPoints[ip_1],
						in mControlPoints[ip_2],
						in mControlPoints[ip_3]);

					return point;
				}
				else
				{
					// Получаем индексы точек
					var ip_0 = index_curve - 1 < 0 ? 0 : index_curve - 1;
					var ip_1 = index_curve;
					var ip_2 = index_curve + 1 > mControlPoints.Length - 1 ? mControlPoints.Length - 1 : index_curve + 1;
					var ip_3 = index_curve + 2 > mControlPoints.Length - 1 ? mControlPoints.Length - 1 : index_curve + 2;

					Vector3Df point = CalculatePoint(time,
						in mControlPoints[ip_0],
						in mControlPoints[ip_1],
						in mControlPoints[ip_2],
						in mControlPoints[ip_3]);

					return point;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Растеризация сплайна - вычисление точек отрезков для рисования сплайна
			/// </summary>
			/// <remarks>
			/// Качество(степень) растеризации зависит от свойства <see cref="CSplineBase2D.SegmentsSpline"/>
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public override void ComputeDrawingPoints()
			{
				mDrawingPoints.Clear();

				if (mIsClosed)
				{
					for (var ip = 0; ip < mControlPoints.Length; ip++)
					{
						// Получаем индексы точек
						var ip_0 = ClampPosition(ip - 1);
						var ip_1 = ClampPosition(ip);
						var ip_2 = ClampPosition(ip + 1);
						var ip_3 = ClampPosition(ip + 2);

						Vector3Df prev = CalculatePoint(0, ip_0, ip_1, ip_2, ip_3);
						mDrawingPoints.Add(prev);
						for (var i = 1; i < mSegmentsSpline; i++)
						{
							var time = (Single)i / mSegmentsSpline;
							Vector3Df point = CalculatePoint(time, ip_0, ip_1, ip_2, ip_3);

							// Добавляем если длина больше 1,4
							if ((point - prev).SqrLength > 2)
							{
								mDrawingPoints.Add(point);
								prev = point;
							}
						}
					}

					CheckCorrectStartPoint();
				}
				else
				{
					for (var ip = 0; ip < mControlPoints.Length - 1; ip++)
					{
						// Получаем индексы точек
						var ip_0 = ip - 1 < 0 ? 0 : ip - 1;
						var ip_1 = ip;
						var ip_2 = ip + 1 > mControlPoints.Length - 1 ? mControlPoints.Length - 1 : ip + 1;
						var ip_3 = ip + 2 > mControlPoints.Length - 1 ? mControlPoints.Length - 1 : ip + 2;

						Vector3Df prev = CalculatePoint(0, ip_0, ip_1, ip_2, ip_3);
						mDrawingPoints.Add(prev);
						for (var i = 1; i < mSegmentsSpline; i++)
						{
							var time = (Single)i / mSegmentsSpline;
							Vector3Df point = CalculatePoint(time, ip_0, ip_1, ip_2, ip_3);

							// Добавляем если длина больше 1,4
							if ((point - prev).SqrLength > 2)
							{
								mDrawingPoints.Add(point);
								prev = point;
							}
						}
					}

					CheckCorrectStartPoint();
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Ограничение точек для организации замкнутой кривой
			/// </summary>
			/// <param name="pos">Позиция</param>
			/// <returns>Скорректированная позиция</returns>
			//---------------------------------------------------------------------------------------------------------
			protected Int32 ClampPosition(Int32 pos)
			{
				if (pos < 0)
				{
					pos = mControlPoints.Length - 1;
				}

				if (pos > mControlPoints.Length)
				{
					pos = 1;
				}
				else if (pos > mControlPoints.Length - 1)
				{
					pos = 0;
				}

				return pos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на сплайне CatmullRom представленной с помощью четырех контрольных точек
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="indexP0">Индекс первой точки</param>
			/// <param name="indexP1">Индекс второй точки</param>
			/// <param name="indexP2">Индекс третьей точки</param>
			/// <param name="indexP3">Индекс четвертой точки</param>
			/// <returns>Позиция точки на сплайне CatmullRom</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df CalculatePoint(Single time, Int32 indexP0, Int32 indexP1, Int32 indexP2, Int32 indexP3)
			{
				Vector3Df a = 2f * mControlPoints[indexP1];
				Vector3Df b = mControlPoints[indexP2] - mControlPoints[indexP0];
				Vector3Df c = (2f * mControlPoints[indexP0]) - (5f * mControlPoints[indexP1]) + (4f * mControlPoints[indexP2]) - mControlPoints[indexP3];
				Vector3Df d = -mControlPoints[indexP0] + (3f * mControlPoints[indexP1]) - (3f * mControlPoints[indexP2]) + mControlPoints[indexP3];

				//The cubic polynomial: a + b * t + c * t^2 + d * t^3
				Vector3Df pos = 0.5f * (a + (b * time) + (c * time * time) + (d * time * time * time));

				return pos;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================