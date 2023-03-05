//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема для работы со сплайнами
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMathSplineBezier3D.cs
*		Алгоритмы работы с кривыми и путями Безье в трехмерном пространстве.
*		Реализация алгоритмов работы с кривыми и путями Безье в трехмерном пространстве.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup MathSpline
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Квадратичная кривая Безье
		/// </summary>
		/// <remarks>
		/// Квадратичная кривая Безье второго порядка создается тремя опорным точками.
		/// При этом кривая проходит только через начальную и конечную точку.
		/// Другая точка (будет назвать её управляющей) определяет лишь форму кривой
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CBezierQuadratic3D : CSplineBase3D
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на кривой Безье представленной с помощью трех контрольных точек
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handle_point">Контрольная точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Позиция точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df CalculatePoint(Single time, Vector3Df start, Vector3Df handle_point, Vector3Df end)
			{
				Single u = 1 - time;
				Single tt = time * time;
				Single uu = u * u;

				return (uu * start) + (2 * time * u * handle_point) + (tt * end);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на кривой Безье представленной с помощью трех контрольных точек
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handle_point">Контрольная точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Позиция точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df CalculatePoint(Single time, ref Vector3Df start, ref Vector3Df handle_point, ref Vector3Df end)
			{
				Single u = 1 - time;
				Single tt = time * time;
				Single uu = u * u;

				return (uu * start) + (2 * time * u * handle_point) + (tt * end);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление первой производной точки на кривой Безье представленной с помощью трех контрольных точек
			/// </summary>
			/// <remarks>
			/// Первая производная показывает скорость изменения функции в данной точки.
			/// Физическим смысл производной - скорость в данной точке 
			/// </remarks>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handle_point">Контрольная точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Первая производная точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df CalculateFirstDerivative(Single time, Vector3Df start, Vector3Df handle_point, Vector3Df end)
			{
				return (2f * (1f - time) * (handle_point - start)) + (2f * time * (end - handle_point));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление первой производной точки на кривой Безье представленной с помощью трех контрольных точек
			/// </summary>
			/// <remarks>
			/// Первая производная показывает скорость изменения функции в данной точки.
			/// Физическим смысл производной - скорость в данной точке 
			/// </remarks>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handle_point">Контрольная точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Первая производная точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df CalculateFirstDerivative(Single time, ref Vector3Df start, ref Vector3Df handle_point, ref Vector3Df end)
			{
				return (2f * (1f - time) * (handle_point - start)) + (2f * time * (end - handle_point));
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Управляющая точка
			/// </summary>
			public Vector3Df HandlePoint
			{
				get { return mControlPoints[1]; }
				set { mControlPoints[1] = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CBezierQuadratic3D()
				: base(3)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="start_point">Начальная точка</param>
			/// <param name="end_point">Конечная точка</param>
			//---------------------------------------------------------------------------------------------------------
			public CBezierQuadratic3D(Vector3Df start_point, Vector3Df end_point)
				: base(start_point, end_point)
			{
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
				Single u = 1 - time;
				Single tt = time * time;
				Single uu = u * u;

				return (uu * mControlPoints[0]) + (2 * time * u * mControlPoints[1]) + (tt * mControlPoints[2]);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление первой производной точки на кривой Безье
			/// </summary>
			/// <remarks>
			/// Первая производная показывает скорость изменения функции в данной точки.
			/// Физическим смысл производной - скорость на данной точке 
			/// </remarks>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <returns>Первая производная точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df CalculateFirstDerivative(Single time)
			{
				return (2f * (1f - time) * (HandlePoint - StartPoint)) + (2f * time * (EndPoint - HandlePoint));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на управляющую точку
			/// </summary>
			/// <param name="index">Позиция(индекс) контрольной точки</param>
			/// <returns>Статус управляющей точки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean IsHandlePoint(Int32 index)
			{
				return index == 1;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Кубическая кривая Безье
		/// </summary>
		/// <remarks>
		/// Кубическая кривая Безье третьего порядка создается четырьмя опорным точками.
		/// При этом кривая проходит только через начальную и конечную точку.
		/// Другие две точки (будет назвать их управляющими) определяет лишь форму кривой
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CBezierCubic3D : CSplineBase3D
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на кривой Безье представленной с помощью четырех контрольных точек
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handle_point1">Первая управляющая точка</param>
			/// <param name="handle_point2">Вторая управляющая точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Позиция точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df CalculatePoint(Single time, Vector3Df start, Vector3Df handle_point1, Vector3Df handle_point2, Vector3Df end)
			{
				Single u = 1 - time;
				Single tt = time * time;
				Single uu = u * u;
				Single uuu = uu * u;
				Single ttt = tt * time;

				Vector3Df point = uuu * start;

				point += 3 * uu * time * handle_point1;
				point += 3 * u * tt * handle_point2;
				point += ttt * end;

				return point;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на кривой Безье представленной с помощью четырех контрольных точек
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handle_point1">Первая управляющая точка</param>
			/// <param name="handle_point2">Вторая управляющая точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Позиция точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df CalculatePoint(Single time, ref Vector3Df start, ref Vector3Df handle_point1,
				ref Vector3Df handle_point2, ref Vector3Df end)
			{
				Single u = 1 - time;
				Single tt = time * time;
				Single uu = u * u;
				Single uuu = uu * u;
				Single ttt = tt * time;

				Vector3Df point = uuu * start;

				point += 3 * uu * time * handle_point1;
				point += 3 * u * tt * handle_point2;
				point += ttt * end;

				return point;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление первой производной точки на кривой Безье представленной с помощью четырех контрольных точек
			/// </summary>
			/// <remarks>
			/// Первая производная показывает скорость изменения функции в данной точки.
			/// Физическим смысл производной - скорость в данной точке 
			/// </remarks>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handle_point1">Первая управляющая точка</param>
			/// <param name="handle_point2">Вторая управляющая точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Первая производная точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df CalculateFirstDerivative(Single time, Vector3Df start, Vector3Df handle_point1, Vector3Df handle_point2, Vector3Df end)
			{
				Single u = 1 - time;
				return (3f * u * u * (handle_point1 - start)) +
				       (6f * u * time * (handle_point2 - handle_point1)) +
				       (3f * time * time * (end - handle_point2));

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление первой производной точки на кривой Безье представленной с помощью четырех контрольных точек
			/// </summary>
			/// <remarks>
			/// Первая производная показывает скорость изменения функции в данной точки.
			/// Физическим смысл производной - скорость в данной точке 
			/// </remarks>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handle_point1">Первая управляющая точка</param>
			/// <param name="handle_point2">Вторая управляющая точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Первая производная точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df CalculateFirstDerivative(Single time, ref Vector3Df start, ref Vector3Df handle_point1,
				ref Vector3Df handle_point2, ref Vector3Df end)
			{
				Single u = 1 - time;
				return (3f * u * u * (handle_point1 - start)) +
				       (6f * u * time * (handle_point2 - handle_point1)) +
				       (3f * time * time * (end - handle_point2));
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Первая управляющая точка
			/// </summary>
			public Vector3Df HandlePoint1
			{
				get { return mControlPoints[1]; }
				set { mControlPoints[1] = value; }
			}

			/// <summary>
			/// Вторая управляющая точка
			/// </summary>
			public Vector3Df HandlePoint2
			{
				get { return mControlPoints[2]; }
				set { mControlPoints[2] = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CBezierCubic3D()
				: base(4)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="start_point">Начальная точка</param>
			/// <param name="end_point">Конечная точка</param>
			//---------------------------------------------------------------------------------------------------------
			public CBezierCubic3D(Vector3Df start_point, Vector3Df end_point)
								: base(4)
			{
				mControlPoints[0] = start_point;
				mControlPoints[1] = (start_point + end_point) / 3;
				mControlPoints[2] = (start_point + end_point) / 3 * 2;
				mControlPoints[3] = end_point;
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
				Single u = 1 - time;
				Single tt = time * time;
				Single uu = u * u;
				Single uuu = uu * u;
				Single ttt = tt * time;

				Vector3Df point = uuu * mControlPoints[0];

				point += 3 * uu * time * mControlPoints[1];
				point += 3 * u * tt * mControlPoints[2];
				point += ttt * mControlPoints[3];

				return point;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание кубической кривой проходящий через заданные(опорные) точки на равномерно заданном времени
			/// </summary>
			/// <param name="start">Начальная точка</param>
			/// <param name="point1">Первая точка</param>
			/// <param name="point2">Вторая точка</param>
			/// <param name="end">Конечная точка</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateFromPivotPoint(Vector3Df start, Vector3Df point1, Vector3Df point2, Vector3Df end)
			{
				mControlPoints[0] = start;
				mControlPoints[1].X = ((-5 * start.X) + (18 * point1.X) - (9 * point2.X) + (2 * end.X)) / 6;
				mControlPoints[1].Y = ((-5 * start.Y) + (18 * point1.Y) - (9 * point2.Y) + (2 * end.Y)) / 6;
				mControlPoints[2].X = ((2 * start.X) - (9 * point1.X) + (18 * point2.X) - (5 * end.X)) / 6;
				mControlPoints[2].Y = ((2 * start.Y) - (9 * point1.Y) + (18 * point2.Y) - (5 * end.Y)) / 6;
				mControlPoints[3] = end;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление первой производной точки на кривой Безье
			/// </summary>
			/// <remarks>
			/// Первая производная показывает скорость изменения функции в данной точки.
			/// Физическим смысл производной - скорость на данной точке 
			/// </remarks>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <returns>Первая производная точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df CalculateFirstDerivative(Single time)
			{
				Single u = 1 - time;
				return (3f * u * u * (HandlePoint1 - StartPoint)) +
				       (6f * u * time * (HandlePoint2 - HandlePoint1)) +
				       (3f * time * time * (EndPoint - HandlePoint2));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на управляющую точку
			/// </summary>
			/// <param name="index">Позиция(индекс) контрольной точки</param>
			/// <returns>Статус управляющей точки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean IsHandlePoint(Int32 index)
			{
				return index == 1 || index == 2;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Путь состоящий из кривых Безье
		/// </summary>
		/// <remarks>
		/// Реализация пути последовательно состоящего из кубических кривых Безье.
		/// Путь проходит через заданные опорные точки, управляющие точки определяют форму пути
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CBezierPath3D : CSplineBase3D
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			internal Boolean mIsClosed;
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			internal TBezierHandleMode[] mHandleModes;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
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

						if (mIsClosed == true)
						{
							mHandleModes[mHandleModes.Length - 1] = mHandleModes[0];
							SetControlPoint(0, mControlPoints[0]);
						}

						OnUpdateSpline();
					}
				}
			}

			/// <summary>
			/// Количество кривых в пути
			/// </summary>
			public Int32 CurveCount
			{
				get { return (mControlPoints.Length - 1) / 3; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CBezierPath3D()
				: base(4)
			{
				mHandleModes = new TBezierHandleMode[4];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор осуществляет построение пути(связанных кривых) Безье на основе опорных точек пути
			/// </summary>
			/// <remarks>
			/// Промежуточные управляющие точки генерируется автоматически
			/// </remarks>
			/// <param name="pivot_points">Опорные точки пути</param>
			//---------------------------------------------------------------------------------------------------------
			public CBezierPath3D(params Vector3Df[] pivot_points)
				: base(pivot_points)
			{
				mHandleModes = new TBezierHandleMode[4];
				CreateFromPivotPoints(pivot_points);
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
					index_curve = mControlPoints.Length - 4;
				}
				else
				{
					time = XMath.Clamp01(time) * CurveCount;
					index_curve = (Int32)time;
					time -= index_curve;
					index_curve *= 3;
				}

				Vector3Df point = CBezierCubic3D.CalculatePoint(time,
					ref mControlPoints[index_curve],
					ref mControlPoints[index_curve + 1],
					ref mControlPoints[index_curve + 2],
					ref mControlPoints[index_curve + 3]);

				return point;
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

				for (Int32 i = 0; i < CurveCount; i++)
				{
					Vector3Df prev = CalculateCurvePoint(i, 0);
					mDrawingPoints.Add(prev);
					for (Int32 ip = 1; ip < SegmentsSpline; ip++)
					{
						Single time = (Single)ip / SegmentsSpline;
						Vector3Df point = CalculateCurvePoint(i, time);

						// Добавляем если длина больше 1,4
						if ((point - prev).SqrLength > MinimalSqrLine)
						{
							mDrawingPoints.Add(point);
							prev = point;
						}
					}
				}

				if (mIsClosed)
				{
					CheckCorrectStartPoint();
				}
				else
				{
					CheckCorrectEndPoint();
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка режима редактирования управляющей точки
			/// </summary>
			/// <param name="index">Позиция(индекс) контрольной точки</param>
			//---------------------------------------------------------------------------------------------------------
			private void SetHandleMode(Int32 index)
			{
				Int32 mode_index = (index + 1) / 3;
				TBezierHandleMode mode = mHandleModes[mode_index];
				if (mode == TBezierHandleMode.Free || !mIsClosed && (mode_index == 0 || mode_index == mHandleModes.Length - 1))
				{
					return;
				}

				Int32 middle_index = mode_index * 3;
				Int32 fixed_index, enforced_index;
				if (index <= middle_index)
				{
					fixed_index = middle_index - 1;
					if (fixed_index < 0)
					{
						fixed_index = mControlPoints.Length - 2;
					}
					enforced_index = middle_index + 1;
					if (enforced_index >= mControlPoints.Length)
					{
						enforced_index = 1;
					}
				}
				else
				{
					fixed_index = middle_index + 1;
					if (fixed_index >= mControlPoints.Length)
					{
						fixed_index = 1;
					}
					enforced_index = middle_index - 1;
					if (enforced_index < 0)
					{
						enforced_index = mControlPoints.Length - 2;
					}
				}

				Vector3Df middle = mControlPoints[middle_index];
				Vector3Df enforced_tangent = middle - mControlPoints[fixed_index];
				if (mode == TBezierHandleMode.Aligned)
				{
					enforced_tangent = enforced_tangent.Normalized * Vector3Df.Distance(middle, mControlPoints[enforced_index]);
				}

				mControlPoints[enforced_index] = middle + enforced_tangent;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание пути Безье проходящего через заданные(опорные) точки
			/// </summary>
			/// <remarks>
			/// Промежуточные управляющие точки генерируется автоматически
			/// </remarks>
			/// <param name="pivot_points">Опорные точки пути</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateFromPivotPoints(params Vector3Df[] pivot_points)
			{
				// Если точек меньше двух выходим
				if (pivot_points.Length < 2)
				{
					return;
				}

				List<Vector3Df> points = new List<Vector3Df>();
				for (Int32 i = 0; i < pivot_points.Length; i++)
				{
					if (i == 0) // is first
					{
						Vector3Df p1 = pivot_points[i];
						Vector3Df p2 = pivot_points[i + 1];

						Vector3Df tangent = p2 - p1;
						Vector3Df q1 = p1 + tangent;

						points.Add(p1);
						points.Add(q1);
					}
					else if (i == pivot_points.Length - 1) //last
					{
						Vector3Df p0 = pivot_points[i - 1];
						Vector3Df p1 = pivot_points[i];
						Vector3Df tangent = p1 - p0;
						Vector3Df q0 = p1 - tangent;

						points.Add(q0);
						points.Add(p1);
					}
					else
					{
						Vector3Df p0 = pivot_points[i - 1];
						Vector3Df p1 = pivot_points[i];
						Vector3Df p2 = pivot_points[i + 1];
						Vector3Df tangent = (p2 - p0).Normalized;
						Vector3Df q0 = p1 - (tangent * (p1 - p0).Length);
						Vector3Df q1 = p1 + (tangent * (p2 - p1).Length);

						points.Add(q0);
						points.Add(p1);
						points.Add(q1);
					}
				}

				// При необходимости изменяем размер массива
				if (mControlPoints.Length != points.Count)
				{
					Array.Resize(ref mControlPoints, points.Count);
				}

				// Копируем данные
				for (Int32 i = 0; i < points.Count; i++)
				{
					mControlPoints[i] = points[i];
				}

				OnUpdateSpline();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка контрольной точки сплайна по индексу в локальных координатах
			/// </summary>
			/// <param name="index">Позиция(индекс) точки</param>
			/// <param name="point">Контрольная точка сплайна в локальных координатах</param>
			/// <param name="update_spline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetControlPoint(Int32 index, Vector3Df point, Boolean update_spline = false)
			{
				if (index % 3 == 0)
				{
					Vector3Df delta = point - mControlPoints[index];
					if (mIsClosed)
					{
						if (index == 0)
						{
							mControlPoints[1] += delta;
							mControlPoints[mControlPoints.Length - 2] += delta;
							mControlPoints[mControlPoints.Length - 1] = point;
						}
						else if (index == mControlPoints.Length - 1)
						{
							mControlPoints[0] = point;
							mControlPoints[1] += delta;
							mControlPoints[index - 1] += delta;
						}
						else
						{
							mControlPoints[index - 1] += delta;
							mControlPoints[index + 1] += delta;
						}
					}
					else
					{
						if (index > 0)
						{
							mControlPoints[index - 1] += delta;
						}
						if (index + 1 < mControlPoints.Length)
						{
							mControlPoints[index + 1] += delta;
						}
					}
				}
				mControlPoints[index] = point;
				SetHandleMode(index);

				if (update_spline)
				{
					OnUpdateSpline();
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ОТДЕЛЬНЫМИ КРИВЫМИ ========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить кривую
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void AddCurve()
			{
				Vector3Df point = mControlPoints[mControlPoints.Length - 1];
				Array.Resize(ref mControlPoints, mControlPoints.Length + 3);
				point.X += 100f;
				mControlPoints[mControlPoints.Length - 3] = point;
				point.X += 100f;
				mControlPoints[mControlPoints.Length - 2] = point;
				point.X += 100f;
				mControlPoints[mControlPoints.Length - 1] = point;

				Array.Resize(ref mHandleModes, mHandleModes.Length + 1);
				mHandleModes[mHandleModes.Length - 1] = mHandleModes[mHandleModes.Length - 2];
				SetHandleMode(mControlPoints.Length - 4);

				if (mIsClosed)
				{
					mControlPoints[mControlPoints.Length - 1] = mControlPoints[0];
					mHandleModes[mHandleModes.Length - 1] = mHandleModes[0];
					SetHandleMode(0);
				}

				OnUpdateSpline();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление последней кривой
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveCurve()
			{
				if (CurveCount > 1)
				{
					Array.Resize(ref mControlPoints, mControlPoints.Length - 3);
					Array.Resize(ref mHandleModes, mHandleModes.Length - 1);
					SetHandleMode(mControlPoints.Length - 2, TBezierHandleMode.Free);
					if (mIsClosed)
					{
						mControlPoints[mControlPoints.Length - 1] = mControlPoints[0];
						mHandleModes[mHandleModes.Length - 1] = mHandleModes[0];
						SetHandleMode(0);
					}

					OnUpdateSpline();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на отдельной кривой Безье
			/// </summary>
			/// <param name="curve_index">Индекс кривой</param>
			/// <param name="time">Время</param>
			/// <returns>Точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df CalculateCurvePoint(Int32 curve_index, Single time)
			{
				Int32 node_index = curve_index * 3;

				return CBezierCubic3D.CalculatePoint(time,
					ref mControlPoints[node_index],
					ref mControlPoints[node_index + 1],
					ref mControlPoints[node_index + 2],
					ref mControlPoints[node_index + 3]);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение контрольной точки на отдельной кривой Безье
			/// </summary>
			/// <param name="curve_index">Индекс кривой</param>
			/// <param name="point_index">Индекс контрольной точки</param>
			/// <returns>Контрольная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df GetCurveControlPoint(Int32 curve_index, Int32 point_index)
			{
				curve_index = curve_index * 3;
				return mControlPoints[curve_index + point_index];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка позиции контрольной точки на отдельной кривой Безье
			/// </summary>
			/// <param name="curve_index">Индекс кривой</param>
			/// <param name="point_index">Индекс точки</param>
			/// <param name="position">Позиция контрольной точки</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetCurveControlPoint(Int32 curve_index, Int32 point_index, Vector3Df position)
			{
				curve_index = curve_index * 3;
				mControlPoints[curve_index + point_index] = position;
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С УПРАВЛЯЮЩИМИ ТОЧКАМИ ======================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на управляющую точку
			/// </summary>
			/// <param name="index">Позиция(индекс) контрольной точки</param>
			/// <returns>Статус управляющей точки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean IsHandlePoint(Int32 index)
			{
				return index == 1 ||
				       index == 2 ||
				       index % 3 == 1 ||
				       index % 3 == 2;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получения режима редактирования управляющей точки
			/// </summary>
			/// <param name="index">Позиция(индекс) контрольной точки</param>
			/// <returns>Режим редактирования управляющей точки</returns>
			//---------------------------------------------------------------------------------------------------------
			public TBezierHandleMode GetHandleMode(Int32 index)
			{
				return mHandleModes[(index + 1) / 3];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка режима редактирования управляющей точки
			/// </summary>
			/// <param name="index">Позиция(индекс) контрольной точки</param>
			/// <param name="mode">Режим редактирования управляющей точки</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetHandleMode(Int32 index, TBezierHandleMode mode)
			{
				Int32 mode_index = (index + 1) / 3;
				mHandleModes[mode_index] = mode;

				if (mIsClosed)
				{
					if (mode_index == 0)
					{
						mHandleModes[mHandleModes.Length - 1] = mode;
					}
					else if (mode_index == mHandleModes.Length - 1)
					{
						mHandleModes[0] = mode;
					}
				}

				SetHandleMode(index);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================