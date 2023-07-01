//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема для работы со сплайнами
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMathSplineBezier2D.cs
*		Алгоритмы работы с кривыми и путями Безье в двухмерном пространстве.
*		Реализация алгоритмов работы с кривыми и путями Безье в двухмерном пространстве.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
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
		/// Квадратичная кривая Безье
		/// </summary>
		/// <remarks>
		/// Квадратичная кривая Безье второго порядка создается тремя опорным точками.
		/// При этом кривая проходит только через начальную и конечную точку.
		/// Другая точка (будет назвать её управляющей) определяет лишь форму кривой
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CBezierQuadratic2D : CSplineBase2D
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на кривой Безье представленной с помощью трех контрольных точек
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handlePoint">Контрольная точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Позиция точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df CalculatePoint(Single time, Vector2Df start, Vector2Df handlePoint, Vector2Df end)
			{
				var u = 1 - time;
				var tt = time * time;
				var uu = u * u;

				return (uu * start) + (2 * time * u * handlePoint) + (tt * end);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на кривой Безье представленной с помощью трех контрольных точек
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handlePoint">Контрольная точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Позиция точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df CalculatePoint(Single time, ref Vector2Df start, ref Vector2Df handlePoint, ref Vector2Df end)
			{
				var u = 1 - time;
				var tt = time * time;
				var uu = u * u;

				return (uu * start) + (2 * time * u * handlePoint) + (tt * end);
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
			/// <param name="handlePoint">Контрольная точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Первая производная точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df CalculateFirstDerivative(Single time, Vector2Df start, Vector2Df handlePoint, Vector2Df end)
			{
				return(2f * (1f - time) * (handlePoint - start)) + (2f * time * (end - handlePoint));
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
			/// <param name="handlePoint">Контрольная точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Первая производная точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df CalculateFirstDerivative(Single time, ref Vector2Df start, ref Vector2Df handlePoint, ref Vector2Df end)
			{
				return (2f * (1f - time) * (handlePoint - start)) + (2f * time * (end - handlePoint));
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Управляющая точка
			/// </summary>
			public Vector2Df HandlePoint
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
			public CBezierQuadratic2D()
				: base(3)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="startPoint">Начальная точка</param>
			/// <param name="endPoint">Конечная точка</param>
			//---------------------------------------------------------------------------------------------------------
			public CBezierQuadratic2D(Vector2Df startPoint, Vector2Df endPoint)
				: base(startPoint, endPoint)
			{
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusSpline2D =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на сплайне
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1</param>
			/// <returns>Позиция точки на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Vector2Df CalculatePoint(Single time)
			{
				var u = 1 - time;
				var tt = time * time;
				var uu = u * u;

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
			public Vector2Df CalculateFirstDerivative(Single time)
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
		public class CBezierCubic2D : CSplineBase2D
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на кривой Безье представленной с помощью четырех контрольных точек
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
			/// "правой" конечной точки кривой</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="handlePoint1">Первая управляющая точка</param>
			/// <param name="handlePoint2">Вторая управляющая точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Позиция точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df CalculatePoint(Single time, Vector2Df start, Vector2Df handlePoint1, Vector2Df handlePoint2, Vector2Df end)
			{
				var u = 1 - time;
				var tt = time * time;
				var uu = u * u;
				var uuu = uu * u;
				var ttt = tt * time;

				Vector2Df point = uuu * start;

				point += 3 * uu * time * handlePoint1;
				point += 3 * u * tt * handlePoint2;
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
			/// <param name="handlePoint1">Первая управляющая точка</param>
			/// <param name="handlePoint2">Вторая управляющая точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Позиция точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df CalculatePoint(Single time, ref Vector2Df start, ref Vector2Df handlePoint1,
				ref Vector2Df handlePoint2, ref Vector2Df end)
			{
				var u = 1 - time;
				var tt = time * time;
				var uu = u * u;
				var uuu = uu * u;
				var ttt = tt * time;

				Vector2Df point = uuu * start;

				point += 3 * uu * time * handlePoint1;
				point += 3 * u * tt * handlePoint2;
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
			/// <param name="handlePoint1">Первая управляющая точка</param>
			/// <param name="handlePoint2">Вторая управляющая точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Первая производная точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df CalculateFirstDerivative(Single time, Vector2Df start, Vector2Df handlePoint1, Vector2Df handlePoint2, Vector2Df end)
			{
				var u = 1 - time;
				return (3f * u * u * (handlePoint1 - start)) +
				       (6f * u * time * (handlePoint2 - handlePoint1)) +
				       (3f * time * time * (end - handlePoint2));

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
			/// <param name="handlePoint1">Первая управляющая точка</param>
			/// <param name="handlePoint2">Вторая управляющая точка</param>
			/// <param name="end">Конечная точка</param>
			/// <returns>Первая производная точки на кривой Безье</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df CalculateFirstDerivative(Single time, ref Vector2Df start, ref Vector2Df handlePoint1,
				ref Vector2Df handlePoint2, ref Vector2Df end)
			{
				var u = 1 - time;
				return (3f * u * u * (handlePoint1 - start)) +
				       (6f * u * time * (handlePoint2 - handlePoint1)) +
				       (3f * time * time * (end - handlePoint2));
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Первая управляющая точка
			/// </summary>
			public Vector2Df HandlePoint1
			{
				get { return mControlPoints[1]; }
				set { mControlPoints[1] = value; }
			}

			/// <summary>
			/// Вторая управляющая точка
			/// </summary>
			public Vector2Df HandlePoint2
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
			public CBezierCubic2D()
				:base(4)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="startPoint">Начальная точка</param>
			/// <param name="endPoint">Конечная точка</param>
			//---------------------------------------------------------------------------------------------------------
			public CBezierCubic2D(Vector2Df startPoint, Vector2Df endPoint)
								: base(4)
			{
				mControlPoints[0] = startPoint;
				mControlPoints[1] = (startPoint + endPoint) / 3;
				mControlPoints[2] = (startPoint + endPoint) / 3 * 2;
				mControlPoints[3] = endPoint;
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusSpline2D =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на сплайне
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1</param>
			/// <returns>Позиция точки на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Vector2Df CalculatePoint(Single time)
			{
				var u = 1 - time;
				var tt = time * time;
				var uu = u * u;
				var uuu = uu * u;
				var ttt = tt * time;

				Vector2Df point = uuu * mControlPoints[0];

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
			public void CreateFromPivotPoint(Vector2Df start, Vector2Df point1, Vector2Df point2, Vector2Df end)
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
			public Vector2Df CalculateFirstDerivative(Single time)
			{
				var u = 1 - time;
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
		/// Режим редактирования управляющей точки
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TBezierHandleMode
		{
			/// <summary>
			/// Свободный режим
			/// </summary>
			Free,

			/// <summary>
			/// Режим - при котором вторая управляющая точка(смежная по отношению к опорной) располагается симметрично
			/// </summary>
			Aligned,

			/// <summary>
			/// Режим - при котором вторая управляющая точка(смежная по отношению к опорной) располагается симметрично и
			/// на таком же расстоянии как и редактируемая точка
			/// </summary>
			Mirrored
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
		public class CBezierPath2D : CSplineBase2D
		{
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
			public CBezierPath2D()
				:base(4)
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
			/// <param name="pivotPoints">Опорные точки пути</param>
			//---------------------------------------------------------------------------------------------------------
			public CBezierPath2D(params Vector2Df[] pivotPoints)
				:base(pivotPoints)
			{
				mHandleModes = new TBezierHandleMode[4];
				CreateFromPivotPoints(pivotPoints);
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusSpline2D =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на сплайне
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1</param>
			/// <returns>Позиция точки на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Vector2Df CalculatePoint(Single time)
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

				Vector2Df point = CBezierCubic2D.CalculatePoint(time,
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

				for (var i = 0; i < CurveCount; i++)
				{
					Vector2Df prev = CalculateCurvePoint(i, 0);
					mDrawingPoints.Add(prev);
					for (var ip = 1; ip < SegmentsSpline; ip++)
					{
						var time = (Single)ip / SegmentsSpline;
						Vector2Df point = CalculateCurvePoint(i, time);

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
				var mode_index = (index + 1) / 3;
				TBezierHandleMode mode = mHandleModes[mode_index];
				if (mode == TBezierHandleMode.Free || !mIsClosed && (mode_index == 0 || mode_index == mHandleModes.Length - 1))
				{
					return;
				}

				var middle_index = mode_index * 3;
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

				Vector2Df middle = mControlPoints[middle_index];
				Vector2Df enforced_tangent = middle - mControlPoints[fixed_index];
				if (mode == TBezierHandleMode.Aligned)
				{
					enforced_tangent = enforced_tangent.Normalized * Vector2Df.Distance(middle, mControlPoints[enforced_index]);
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
			/// <param name="pivotPoints">Опорные точки пути</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateFromPivotPoints(params Vector2Df[] pivotPoints)
			{
				// Если точек меньше двух выходим
				if (pivotPoints.Length < 2)
				{
					return;
				}

				var points = new List<Vector2Df>();
				for (var i = 0; i < pivotPoints.Length; i++)
				{
					// Первая точка
					if (i == 0)
					{
						Vector2Df p1 = pivotPoints[i];
						Vector2Df p2 = pivotPoints[i + 1];

						// Расстояние
						var distance = (p2 - p1).Length;
						Vector2Df q1 = p1 + (distance * 0.5f * Vector2Df.Right);

						points.Add(p1);
						points.Add(q1);
					}
					else if (i == pivotPoints.Length - 1) //last
					{
						Vector2Df p0 = pivotPoints[i - 1];
						Vector2Df p1 = pivotPoints[i];

						// Расстояние
						var distance = (p0 - p1).Length;
						Vector2Df q0 = p1 + (distance * 0.5f * Vector2Df.Right);

						points.Add(q0);
						points.Add(p1);
					}
					else
					{
						Vector2Df p0 = pivotPoints[i - 1];
						Vector2Df p1 = pivotPoints[i];
						Vector2Df p2 = pivotPoints[i + 1];

						// Расстояние
						var distance1 = (p1 - p0).Length;
						var distance2 = (p2 - p1).Length;

						Vector2Df q0 = p1 + (distance1 * 0.5f * Vector2Df.Left);
						Vector2Df q1 = p1 + (distance2 * 0.5f * Vector2Df.Right);

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
				for (var i = 0; i < points.Count; i++)
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
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetControlPoint(Int32 index, Vector2Df point, Boolean updateSpline = false)
			{
				if (index % 3 == 0)
				{
					Vector2Df delta = point - mControlPoints[index];
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

				if (updateSpline)
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
				Vector2Df point = mControlPoints[mControlPoints.Length - 1];
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
			/// <param name="curveIndex">Индекс кривой</param>
			/// <param name="time">Время</param>
			/// <returns>Точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector2Df CalculateCurvePoint(Int32 curveIndex, Single time)
			{
				var node_index = curveIndex * 3;

				return CBezierCubic2D.CalculatePoint(time,
					ref mControlPoints[node_index],
					ref mControlPoints[node_index + 1],
					ref mControlPoints[node_index + 2],
					ref mControlPoints[node_index + 3]);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение контрольной точки на отдельной кривой Безье
			/// </summary>
			/// <param name="curveIndex">Индекс кривой</param>
			/// <param name="pointIndex">Индекс контрольной точки</param>
			/// <returns>Контрольная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector2Df GetCurveControlPoint(Int32 curveIndex, Int32 pointIndex)
			{
				curveIndex = curveIndex * 3;
				return mControlPoints[curveIndex + pointIndex];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка позиции контрольной точки на отдельной кривой Безье
			/// </summary>
			/// <param name="curveIndex">Индекс кривой</param>
			/// <param name="pointIndex">Индекс точки</param>
			/// <param name="position">Позиция контрольной точки</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetCurveControlPoint(Int32 curveIndex, Int32 pointIndex, Vector2Df position)
			{
				curveIndex = curveIndex * 3;
				mControlPoints[curveIndex + pointIndex] = position;
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
				var mode_index = (index + 1) / 3;
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
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================