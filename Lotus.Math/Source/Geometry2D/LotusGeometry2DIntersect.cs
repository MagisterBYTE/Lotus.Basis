//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 2D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry2DIntersect.cs
*		Пересечение в 2D пространстве.
*		Пересечение основных геометрических тел/примитивов друг с другом и получение соответствующей информации
*	о пересечении.
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
		/** \addtogroup MathGeometry2D
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип пересечения в 2D пространстве
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TIntersectType2D
		{
			/// <summary>
			/// Пересечения нет
			/// </summary>
			None,

			/// <summary>
			/// Пересечения нет.
			/// Линии или лучи параллельны
			/// </summary>
			Parallel,

			/// <summary>
			/// Пересечения представляет собой точку.
			/// Обычно пересечения луча/линии с геометрическими объектами
			/// </summary>
			Point,

			/// <summary>
			/// Пересечения представляет собой сегмент образованный двумя точками пересечение
			/// </summary>
			Segment
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура для хранения информации о пересечении в 2D пространстве
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public struct TIntersectHit2D
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Нет пересечения
			/// </summary>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit2D None()
			{
				var hit = new TIntersectHit2D();
				hit.IntersectType = TIntersectType2D.None;
				return (hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Нет пересечения, линии или лучи параллельны
			/// </summary>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit2D Parallel()
			{
				var hit = new TIntersectHit2D();
				hit.IntersectType = TIntersectType2D.Parallel;
				return (hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Пересечения представляет собой точку
			/// </summary>
			/// <param name="point">Точка пересечения</param>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit2D Point(in Vector2D point)
			{
				var hit = new TIntersectHit2D();
				hit.IntersectType = TIntersectType2D.Point;
				hit.Point1 = point;
				return (hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Пересечения представляет собой точку
			/// </summary>
			/// <param name="point">Точка пересечения</param>
			/// <param name="distance">Дистанция пересечения</param>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit2D Point(in Vector2D point, in Double distance)
			{
				var hit = new TIntersectHit2D();
				hit.IntersectType = TIntersectType2D.Point;
				hit.Point1 = point;
				hit.Distance1 = distance;
				return (hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Пересечения представляет собой отрезок
			/// </summary>
			/// <param name="point_1">Первая точка пересечения</param>
			/// <param name="point_2">Вторая точка пересечения</param>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit2D Segment(in Vector2D point_1, in Vector2D point_2)
			{
				var hit = new TIntersectHit2D();
				hit.IntersectType = TIntersectType2D.Segment;
				hit.Point1 = point_1;
				hit.Point2 = point_2;
				return (hit);
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Тип пересечения
			/// </summary>
			public TIntersectType2D IntersectType;

			/// <summary>
			/// Первая дистанция
			/// </summary>
			public Double Distance1;

			/// <summary>
			/// Первая точка пересечения
			/// </summary>
			public Vector2D Point1;

			/// <summary>
			/// Вторая дистанция
			/// </summary>
			public Double Distance2;

			/// <summary>
			/// Вторая точка пересечения
			/// </summary>
			public Vector2D Point2;
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура для хранения информации о пересечении в 2D пространстве
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public struct TIntersectHit2Df
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Нет пересечения
			/// </summary>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit2Df None()
			{
				var hit = new TIntersectHit2Df();
				hit.IntersectType = TIntersectType2D.None;
				return (hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Нет пересечения, линии или лучи параллельны
			/// </summary>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit2Df Parallel()
			{
				var hit = new TIntersectHit2Df();
				hit.IntersectType = TIntersectType2D.Parallel;
				return (hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Пересечения представляет собой точку
			/// </summary>
			/// <param name="point">Точка пересечения</param>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit2Df Point(in Vector2Df point)
			{
				var hit = new TIntersectHit2Df();
				hit.IntersectType = TIntersectType2D.Point;
				hit.Point1 = point;
				return (hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Пересечения представляет собой точку
			/// </summary>
			/// <param name="point">Точка пересечения</param>
			/// <param name="distance">Дистанция пересечения</param>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit2Df Point(in Vector2Df point, Single distance)
			{
				var hit = new TIntersectHit2Df();
				hit.IntersectType = TIntersectType2D.Point;
				hit.Point1 = point;
				hit.Distance1 = distance;
				return (hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Пересечения представляет собой отрезок
			/// </summary>
			/// <param name="point_1">Первая точка пересечения</param>
			/// <param name="point_2">Вторая точка пересечения</param>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit2Df Segment(in Vector2Df point_1, in Vector2Df point_2)
			{
				var hit = new TIntersectHit2Df();
				hit.IntersectType = TIntersectType2D.Segment;
				hit.Point1 = point_1;
				hit.Point2 = point_2;
				return (hit);
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Тип пересечения
			/// </summary>
			public TIntersectType2D IntersectType;

			/// <summary>
			/// Первая дистанция
			/// </summary>
			public Single Distance1;

			/// <summary>
			/// Первая точка пересечения
			/// </summary>
			public Vector2Df Point1;

			/// <summary>
			/// Вторая дистанция
			/// </summary>
			public Single Distance2;

			/// <summary>
			/// Вторая точка пересечения
			/// </summary>
			public Vector2Df Point2;
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы для работы с пересечением в 2D
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XIntersect2D
		{
			#region ======================================= ВНУТРЕННИЕ МЕТОДЫ РАСШИРЕНИЙ ==============================
#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Возвращение скалярного произведения с перпендикулярным вектором
			/// </summary>
			/// <param name="this">Исходный вектор</param>
			/// <param name="vector">Вектор</param>
			/// <returns>Скалярное произведение с перпендикулярным вектором</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Single DotPerp(this UnityEngine.Vector2 @this, in UnityEngine.Vector2 vector)
			{
				return @this.x * vector.y - @this.y * vector.x;
			}
#endif
			#endregion

			#region ======================================= Point - Line ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на линии
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="line">Линия</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointLine(in Vector2Df point, in Line2Df line)
			{
				return PointLine(in point, in line.Position, in line.Direction);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на линии
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="line">Линия</param>
			/// <param name="side">С какой стороны луча располагается точки</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointLine(in Vector2Df point, in Line2Df line, out Int32 side)
			{
				return PointLine(in point, in line.Position, in line.Direction, out side);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на линии
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointLine(in Vector2Df point, in Vector2Df line_pos, in Vector2Df line_dir)
			{
				var perp_dot = Vector2Df.DotPerp(point - line_pos, in line_dir);
				return (-XGeometry2D.Eplsilon_f < perp_dot && perp_dot < XGeometry2D.Eplsilon_f);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на линии
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="side">С какой стороны луча располагается точки</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointLine(in Vector2Df point, in Vector2Df line_pos, 
				in Vector2Df line_dir, out Int32 side)
			{
				var perp_dot = Vector2Df.DotPerp(point - line_pos, in line_dir);
				if (perp_dot < -XGeometry2D.Eplsilon_f)
				{
					side = -1;
					return false;
				}
				if (perp_dot > XGeometry2D.Eplsilon_f)
				{
					side = 1;
					return false;
				}
				side = 0;
				return true;
			}
			#endregion

			#region ======================================= Point - Ray ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на нахождение точки на луче
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="ray">Луч</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointRay(in Vector2Df point, in Ray2Df ray)
			{
				return PointRay(in point, in  ray.Position, in ray.Direction);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на нахождение точки на луче
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="ray">Луч</param>
			/// <param name="side">С какой стороны луча располагается точки</param>
			/// <returns>татус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointRay(in Vector2Df point, in Ray2Df ray, out Int32 side)
			{
				return PointRay(in point, in ray.Position, in ray.Direction, out side);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на нахождение точки на луче
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointRay(in Vector2Df point, in Vector2Df ray_pos, in Vector2Df ray_dir)
			{
				// Считаем вектор на точку
				Vector2Df to_point = point - ray_pos;

				// Считаем скалярное произвдение между векторам
				// Чем ближе оно к нулю тем соответственно точки ближе прилегает к лучу
				var perp_dot = Vector2Df.DotPerp(in to_point, in ray_dir);

				if(XMath.Approximately(perp_dot, XGeometry2D.Eplsilon_f))
				{
					// Если она прилегает
					if(Vector2Df.Dot(in ray_dir, in to_point) > -XGeometry2D.Eplsilon_f)
					{
						return (true);
					}
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на нахождение точки на луче
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="side">С какой стороны луча располагается точки</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointRay(in Vector2Df point, in Vector2Df ray_pos, in Vector2Df ray_dir, out Int32 side)
			{
				// Считаем вектор на точку
				Vector2Df to_point = point - ray_pos;

				// Считаем скалярное произвдение между векторам
				// Чем ближе оно к нулю тем соответственно точки ближе прилегает к лучу
				var perp_dot = Vector2Df.DotPerp(in to_point, in ray_dir);

				if (perp_dot < -XGeometry2D.Eplsilon_f)
				{
					side = -1;
					return false;
				}
				if (perp_dot > XGeometry2D.Eplsilon_f)
				{
					side = 1;
					return false;
				}
				side = 0;

				return (Vector2Df.Dot(in ray_dir, in to_point) > -XGeometry2D.Eplsilon_f);
			}
			#endregion

			#region ======================================= Point - Segment ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на линии
			/// </summary>
			/// <param name="start">Начало линии</param>
			/// <param name="end">Конец линии</param>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="epsilon">Погрешность</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointOnSegment(in Vector2D start, in Vector2D end, in Vector2D point, Double epsilon = 0.1)
			{
				if (point.X - Math.Max(start.X, end.X) > epsilon ||
					Math.Min(start.X, end.X) - point.X > epsilon ||
					point.Y - Math.Max(start.Y, end.Y) > epsilon ||
					Math.Min(start.Y, end.Y) - point.Y > epsilon)
				{
					return false;
				}

				if (Math.Abs(end.X - start.X) < epsilon)
				{
					return Math.Abs(start.X - point.X) < epsilon || Math.Abs(end.X - point.X) < epsilon;
				}
				if (Math.Abs(end.Y - start.Y) < epsilon)
				{
					return Math.Abs(start.Y - point.Y) < epsilon || Math.Abs(end.Y - point.Y) < epsilon;
				}

				var x = start.X + ((point.Y - start.Y) * (end.X - start.X) / (end.Y - start.Y));
				var y = start.Y + ((point.X - start.X) * (end.Y - start.Y) / (end.X - start.X));

				return Math.Abs(point.X - x) < epsilon || Math.Abs(point.Y - y) < epsilon;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на линии
			/// </summary>
			/// <param name="start">Начало линии</param>
			/// <param name="end">Конец линии</param>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="epsilon">Погрешность</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointOnSegment(in Vector2Df start, in Vector2Df end, in Vector2Df point, Single epsilon = 0.1f)
			{
				if (point.X - Math.Max(start.X, end.X) > epsilon ||
					Math.Min(start.X, end.X) - point.X > epsilon ||
					point.Y - Math.Max(start.Y, end.Y) > epsilon ||
					Math.Min(start.Y, end.Y) - point.Y > epsilon)
				{
					return false;
				}

				if (Math.Abs(end.X - start.X) < epsilon)
				{
					return Math.Abs(start.X - point.X) < epsilon || Math.Abs(end.X - point.X) < epsilon;
				}
				if (Math.Abs(end.Y - start.Y) < epsilon)
				{
					return Math.Abs(start.Y - point.Y) < epsilon || Math.Abs(end.Y - point.Y) < epsilon;
				}

				var x = start.X + ((point.Y - start.Y) * (end.X - start.X) / (end.Y - start.Y));
				var y = start.Y + ((point.X - start.X) * (end.Y - start.Y) / (end.X - start.X));

				return Math.Abs(point.X - x) < epsilon || Math.Abs(point.Y - y) < epsilon;
			}

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на линии
			/// </summary>
			/// <param name="start">Начало линии</param>
			/// <param name="end">Конец линии</param>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="epsilon">Погрешность</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointOnSegment(UnityEngine.Vector2 start, UnityEngine.Vector2 end, UnityEngine.Vector2 point,
				Single epsilon = 0.1f)
			{
				return PointOnSegment(in start, in end, in point, epsilon);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на линии
			/// </summary>
			/// <param name="start">Начало линии</param>
			/// <param name="end">Конец линии</param>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="epsilon">Погрешность</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointOnSegment(in UnityEngine.Vector2 start, in UnityEngine.Vector2 end, in UnityEngine.Vector2 point,
				Single epsilon = 0.1f)
			{
				if (point.x - UnityEngine.Mathf.Max(start.x, end.x) > epsilon ||
					UnityEngine.Mathf.Min(start.x, end.x) - point.x > epsilon ||
					point.y - UnityEngine.Mathf.Max(start.y, end.y) > epsilon ||
					UnityEngine.Mathf.Min(start.y, end.y) - point.y > epsilon)
				{
					return false;
				}

				if (Math.Abs(end.x - start.x) < epsilon)
				{
					return Math.Abs(start.x - point.x) < epsilon || Math.Abs(end.x - point.x) < epsilon;
				}
				if (Math.Abs(end.y - start.y) < epsilon)
				{
					return Math.Abs(start.y - point.y) < epsilon || Math.Abs(end.y - point.y) < epsilon;
				}

				Single x = start.x + (point.y - start.y) * (end.x - start.x) / (end.y - start.y);
				Single y = start.y + (point.x - start.x) * (end.y - start.y) / (end.x - start.x);

				return Math.Abs(point.x - x) < epsilon || Math.Abs(point.y - y) < epsilon;
			}
#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на границах прямоугольника
			/// </summary>
			/// <param name="rect">Прямоугольник</param>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="epsilon">Погрешность</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointOnRectBorder(in Rect2Df rect, in Vector2Df point, Single epsilon = 0.1f)
			{
				var status1 = PointOnSegment(rect.PointTopLeft, rect.PointTopRight, in point, epsilon);
				if (status1)
				{
					return true;
				}
				var status2 = PointOnSegment(rect.PointTopLeft, rect.PointBottomLeft, in point, epsilon);
				if (status2)
				{
					return true;
				}
				var status3 = PointOnSegment(rect.PointTopRight, rect.PointBottomRight, in point, epsilon);
				if (status3)
				{
					return true;
				}
				var status4 = PointOnSegment(rect.PointBottomLeft, rect.PointBottomRight, in point, epsilon);
				if (status4)
				{
					return true;
				}

				return false;
			}

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на границах прямоугольника
			/// </summary>
			/// <param name="rect">Прямоугольник</param>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="epsilon">Погрешность</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointOnRectBorder(UnityEngine.Rect rect, UnityEngine.Vector2 point, Single epsilon = 0.1f)
			{
				return PointOnRectBorder(in rect, in point, epsilon);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на границах прямоугольника
			/// </summary>
			/// <param name="rect">Прямоугольник</param>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="epsilon">Погрешность</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointOnRectBorder(in UnityEngine.Rect rect, in UnityEngine.Vector2 point, Single epsilon = 0.1f)
			{
				Boolean status1 = PointOnSegment(new UnityEngine.Vector2(rect.x, rect.y), new UnityEngine.Vector2(rect.xMax, rect.y), point, epsilon);
				if (status1)
				{
					return true;
				}
				Boolean status2 = PointOnSegment(new UnityEngine.Vector2(rect.x, rect.y), new UnityEngine.Vector2(rect.x, rect.yMax), point, epsilon);
				if (status2)
				{
					return true;
				}
				Boolean status3 = PointOnSegment(new UnityEngine.Vector2(rect.xMax, rect.y), new UnityEngine.Vector2(rect.xMax, rect.yMax), point, epsilon);
				if (status3)
				{
					return true;
				}
				Boolean status4 = PointOnSegment(new UnityEngine.Vector2(rect.x, rect.yMax), new UnityEngine.Vector2(rect.xMax, rect.yMax), point, epsilon);
				if (status4)
				{
					return true;
				}

				return false;
			}
#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на отрезке
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="segment">Отрезок</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointSegment(in Vector2Df point, in Segment2Df segment)
			{
				return PointSegment(in point, in segment.Start, in segment.End);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на отрезке
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="segment">Отрезок</param>
			/// <param name="side">С какой стороны отрезка располагается точки</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointSegment(in Vector2Df point, in Segment2Df segment, out Int32 side)
			{
				return PointSegment(in point, in segment.Start, in segment.End, out side);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на отрезке
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end)
			{
				Vector2Df from_start_to_end = end - start;
				var sqr_segment_length = from_start_to_end.SqrLength;
				if (sqr_segment_length < XGeometry2D.Eplsilon_f)
				{
					// The segment is a point
					return point == start;
				}
				// Normalized direction gives more stable results
				Vector2Df segment_direction = from_start_to_end.Normalized;
				Vector2Df to_point = point - start;
				var perp_dot = Vector2Df.DotPerp(in to_point, in segment_direction);
				if (-XGeometry2D.Eplsilon_f < perp_dot && perp_dot < XGeometry2D.Eplsilon_f)
				{
					var point_projection = Vector2Df.Dot(in segment_direction, in to_point);
					return point_projection > -XGeometry2D.Eplsilon_f &&
						   point_projection < XMath.Sqrt(sqr_segment_length) + XGeometry2D.Eplsilon_f;
				}
				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на отрезке
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="side">С какой стороны отрезка располагается точки</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end, 
				out Int32 side)
			{
				Vector2Df from_start_to_end = end - start;
				var sqr_segment_length = from_start_to_end.SqrLength;
				if (sqr_segment_length < XGeometry2D.Eplsilon_f)
				{
					// The segment is a point
					side = 0;
					return point == start;
				}
				// Normalized direction gives more stable results
				Vector2Df segment_direction = from_start_to_end.Normalized;
				Vector2Df to_point = point - start;
				var perpDot = Vector2Df.DotPerp(in to_point, in segment_direction);
				if (perpDot < -XGeometry2D.Eplsilon_f)
				{
					side = -1;
					return false;
				}
				if (perpDot > XGeometry2D.Eplsilon_f)
				{
					side = 1;
					return false;
				}
				side = 0;
				var point_projection = Vector2Df.Dot(in segment_direction, in to_point);
				return point_projection > -XGeometry2D.Eplsilon_f &&
					   point_projection < XMath.Sqrt(sqr_segment_length) + XGeometry2D.Eplsilon_f;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// 
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="segment_direction">Направление отрезка</param>
			/// <param name="sqr_segment_length">Квадрат длины отрезка</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Boolean PointSegment(in Vector2Df point, in Vector2Df start,
				in Vector2Df segment_direction, Single sqr_segment_length)
			{
				var segment_direction_copy = segment_direction;
				var segment_length = XMath.Sqrt(sqr_segment_length);
				segment_direction_copy /= segment_length;
				Vector2Df to_point = point - start;
				var perpDot = Vector2Df.DotPerp(in to_point, in segment_direction_copy);
				if (-XGeometry2D.Eplsilon_f < perpDot && perpDot < XGeometry2D.Eplsilon_f)
				{
					var point_projection = Vector2Df.Dot(in segment_direction_copy, in to_point);
					return point_projection > -XGeometry2D.Eplsilon_f &&
						   point_projection < segment_length + XGeometry2D.Eplsilon_f;
				}
				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на отрезке
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="point">Проверяемая точка</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointSegmentCollinear(in Vector2Df start, in Vector2Df end, in Vector2Df point)
			{
				if (Math.Abs(start.X - end.X) < XGeometry2D.Eplsilon_f)
				{
					// Vertical
					if (start.Y <= point.Y && point.Y <= end.Y)
					{
						return true;
					}
					if (start.Y >= point.Y && point.Y >= end.Y)
					{
						return true;
					}
				}
				else
				{
					// Not vertical
					if (start.X <= point.X && point.X <= end.X)
					{
						return true;
					}
					if (start.X >= point.X && point.X >= end.X)
					{
						return true;
					}
				}
				return false;
			}
			#endregion

			#region ======================================= Point - Circle ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на попадание точки в область окружности
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="circle">Окружность</param>
			/// <returns>Статус попадания</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointCircle(in Vector2Df point, in Circle2Df circle)
			{
				return PointCircle(in point, in circle.Center, circle.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на попадание точки в область окружности
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="circle_сenter">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <returns>Статус попадания</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointCircle(in Vector2Df point, in Vector2Df circle_сenter, Single circle_radius)
			{
				// For points on the circle's edge Length is more stable than SqrLength
				return (point - circle_сenter).Length < circle_radius + XGeometry2D.Eplsilon_f;
			}
			#endregion Point-Circle

			#region ======================================= Line - Line ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух линий
			/// </summary>
			/// <param name="line_a">Первая линия</param>
			/// <param name="line_b">Вторая линия</param>
			/// <returns>Статус пересечения линий</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineLine(in Line2Df line_a, in Line2Df line_b)
			{
				return LineLine(in line_a.Position, in line_a.Direction, in line_b.Position, in line_b.Direction, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух линий
			/// </summary>
			/// <param name="line_a">Первая линия</param>
			/// <param name="line_b">Вторая линия</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения линий</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineLine(in Line2Df line_a, in Line2Df line_b, out TIntersectHit2Df hit)
			{
				return LineLine(in line_a.Position, in line_a.Direction, in line_b.Position, in line_b.Direction, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух линий
			/// </summary>
			/// <param name="pos_a">Позиция первой линии</param>
			/// <param name="dir_a">Направление первой линии</param>
			/// <param name="pos_b">Позиция второй линии</param>
			/// <param name="dir_b">Направление второй линии</param>
			/// <returns>Статус пересечения линий</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineLine(in Vector2Df pos_a, in Vector2Df dir_a, in Vector2Df pos_b, 
				Vector2Df dir_b)
			{
				return LineLine(in pos_a, in dir_a, in pos_b, in dir_b, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух линий
			/// </summary>
			/// <param name="pos_a">Позиция первой линии</param>
			/// <param name="dir_a">Направление первой линии</param>
			/// <param name="pos_b">Позиция второй линии</param>
			/// <param name="dir_b">Направление второй линии</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения линий</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineLine(in Vector2Df pos_a, in Vector2Df dir_a, in Vector2Df pos_b,
				in Vector2Df dir_b, out TIntersectHit2Df hit)
			{
				Vector2Df pos_b_to_a = pos_a - pos_b;
				var denominator = Vector2Df.DotPerp(in dir_a, in dir_b);
				var perp_dot_b = Vector2Df.DotPerp(in dir_b, in pos_b_to_a);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					var perp_dot_a = Vector2Df.DotPerp(in dir_a, in pos_b_to_a);
					if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						hit = TIntersectHit2Df.None();
						return false;
					}

					// Collinear
					hit = TIntersectHit2Df.Parallel();
					return true;
				}

				// Not parallel
				hit = TIntersectHit2Df.Point(pos_a + (dir_a * (perp_dot_b / denominator)));
				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух линий
			/// </summary>
			/// <param name="line_a">Первая линия</param>
			/// <param name="line_b">Вторая линия</param>
			/// <param name="dist_a">Расстояние от первой линии</param>
			/// <param name="dist_b">Расстояние от второй линии</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType2D LineLine(in Line2Df line_a, in Line2Df line_b, out Single dist_a, out Single dist_b)
			{
				return LineLine(in line_a.Position, in line_a.Direction, in  line_b.Position, in line_b.Direction, out dist_a, out dist_b);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух линий
			/// </summary>
			/// <param name="pos_a">Позиция первой линии</param>
			/// <param name="dir_a">Направление первой линии</param>
			/// <param name="pos_b">Позиция второй линии</param>
			/// <param name="dir_b">Направление второй линии</param>
			/// <param name="dist_a">Расстояние от первой линии</param>
			/// <param name="dist_b">Расстояние от второй линии</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType2D LineLine(in Vector2Df pos_a, in Vector2Df dir_a, in Vector2Df pos_b,
				in Vector2Df dir_b, out Single dist_a, out Single dist_b)
			{
				Vector2Df pos_b_to_a = pos_a - pos_b;
				var denominator = Vector2Df.DotPerp(in dir_a, in dir_b);
				var perp_dot_a = Vector2Df.DotPerp(in dir_a, in pos_b_to_a);
				var perp_bot_b = Vector2Df.DotPerp(in dir_b, in pos_b_to_a);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_bot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						dist_a = 0;
						dist_b = Vector2Df.Dot(in dir_b, in pos_b_to_a);
						return TIntersectType2D.None;
					}
					// Collinear
					dist_a = 0;
					dist_b = Vector2Df.Dot(in dir_b, in pos_b_to_a);
					return TIntersectType2D.Parallel;
				}

				// Not parallel
				dist_a = perp_bot_b / denominator;
				dist_b = perp_dot_a / denominator;
				return TIntersectType2D.Point;
			}
			#endregion Line-Line

			#region ======================================= Line - Ray ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и луча
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="ray">Луч</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineRay(in Line2Df line, in Ray2Df ray)
			{
				return LineRay(in line.Position, in line.Direction, in ray.Position, in ray.Direction, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и луча
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="ray">Луч</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineRay(in Line2Df line, in Ray2Df ray, out TIntersectHit2Df hit)
			{
				return LineRay(in line.Position, in line.Direction, in ray.Position, in ray.Direction, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и луча
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineRay(in Vector2Df line_pos, in Vector2Df line_dir,
				in Vector2Df ray_pos, in Vector2Df ray_dir)
			{
				return LineRay(in line_pos, in line_dir, in ray_pos, in ray_dir, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и луча
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineRay(in Vector2Df line_pos, in Vector2Df line_dir,
				in Vector2Df ray_pos, in Vector2Df ray_dir, out TIntersectHit2Df hit)
			{
				Vector2Df ray_pos_to_line_pos = line_pos - ray_pos;
				var denominator = Vector2Df.DotPerp(in line_dir, in ray_dir);
				var perp_dot_a = Vector2Df.DotPerp(in line_dir, in ray_pos_to_line_pos);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					var perp_dot_b = Vector2Df.DotPerp(in ray_dir, in ray_pos_to_line_pos);
					if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						hit = TIntersectHit2Df.None();
						return false;
					}

					// Collinear
					hit = TIntersectHit2Df.Parallel();
					return true;
				}

				// Not parallel
				var ray_distance = perp_dot_a / denominator;
				if (ray_distance > -XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.Point(ray_pos + (ray_dir * ray_distance), ray_distance);
					return true;
				}

				hit = TIntersectHit2Df.None();
				return false;
			}
			#endregion Line-Ray

			#region ======================================= Line - Segment ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и отрезка
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="segment">Отрезок</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineSegment(in Line2Df line, in Segment2Df segment)
			{
				return LineSegment(in line.Position, in line.Direction, in segment.Start, in segment.End, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и отрезка
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="segment">Отрезок</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineSegment(in Line2Df line, in Segment2Df segment, out TIntersectHit2Df hit)
			{
				return LineSegment(in line.Position, in line.Direction, in segment.Start, in segment.End, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и отрезка
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineSegment(in Vector2Df line_pos, in Vector2Df line_dir,
				in Vector2Df start, in Vector2Df end)
			{
				return LineSegment(in line_pos, in line_dir, in start, in end, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и отрезка
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineSegment(in Vector2Df line_pos, in Vector2Df line_dir,
				in Vector2Df start, in Vector2Df end, out TIntersectHit2Df hit)
			{
				Vector2Df start_to_pos = line_pos - start;
				Vector2Df segment_direction = end - start;
				var denominator = Vector2Df.DotPerp(in line_dir, in segment_direction);
				var perp_dot_start = Vector2Df.DotPerp(in line_dir, in start_to_pos);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					// Normalized Direction gives more stable results 
					var perp_dot_b = Vector2Df.DotPerp(segment_direction.Normalized, in start_to_pos);
					if (Math.Abs(perp_dot_start) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						hit = TIntersectHit2Df.None();
						return false;
					}
					// Collinear
					var segment_is_start_point = segment_direction.SqrLength < XGeometry2D.Eplsilon_f;
					if (segment_is_start_point)
					{
						hit = TIntersectHit2Df.Point(start);
						return true;
					}

					var codirected = Vector2Df.Dot(in line_dir, in segment_direction) > 0;
					if (codirected)
					{
						hit = TIntersectHit2Df.Segment(in start, in end);
					}
					else
					{
						hit = TIntersectHit2Df.Segment(in end, in start);
					}
					return true;
				}

				// Not parallel
				var segment_distance = perp_dot_start / denominator;
				if (segment_distance > -XGeometry2D.Eplsilon_f && segment_distance < 1 + XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.Point(start + (segment_direction * segment_distance));
					return true;
				}
				hit = TIntersectHit2Df.None();
				return false;
			}
			#endregion Line-Segment

			#region ======================================= Line - Circle =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и окружности
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="circle">Окружность</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineCircle(in Line2Df line, in Circle2Df circle)
			{
				return LineCircle(in line.Position, in line.Direction, in circle.Center, circle.Radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и окружности
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="circle">Окружность</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineCircle(in Line2Df line, in Circle2Df circle, out TIntersectHit2Df hit)
			{
				return LineCircle(in line.Position, in line.Direction, in circle.Center, circle.Radius, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и окружности
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineCircle(in Vector2Df line_pos, in Vector2Df line_dir,
				in Vector2Df circle_center, Single circle_radius)
			{
				return LineCircle(in line_pos, in line_dir, in circle_center, circle_radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и окружности
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineCircle(in Vector2Df line_pos, in Vector2Df line_dir,
				in Vector2Df circle_center, Single circle_radius, out TIntersectHit2Df hit)
			{
				Vector2Df pos_to_center = circle_center - line_pos;
				var center_projection = Vector2Df.Dot(in line_dir, in pos_to_center);
				var sqr_dist_to_line = pos_to_center.SqrLength - (center_projection * center_projection);

				var sqr_dist_to_intersection = (circle_radius * circle_radius) - sqr_dist_to_line;
				if (sqr_dist_to_intersection < -XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}
				if (sqr_dist_to_intersection < XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.Point(line_pos + (line_dir * center_projection));
					return true;
				}

				var distance_to_intersection = XMath.Sqrt(sqr_dist_to_intersection);
				var dist_a = center_projection - distance_to_intersection;
				var dist_b = center_projection + distance_to_intersection;

				Vector2Df point_a = line_pos + (line_dir * dist_a);
				Vector2Df point_b = line_pos + (line_dir * dist_b);
				hit = TIntersectHit2Df.Segment(in point_a, in point_b);
				return true;
			}

			#endregion Line-Circle

			#region ======================================= Ray - Ray =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="ray_pos_1">Позиция первого луча</param>
			/// <param name="ray_dir_1">Направление первого луча</param>
			/// <param name="ray_pos_2">Позиция второго луча</param>
			/// <param name="ray_dir_2">Направление второго луча</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType2D RayToRay(in Vector2D ray_pos_1, in Vector2D ray_dir_1, in Vector2D ray_pos_2,
				in Vector2D ray_dir_2, out TIntersectHit2D hit)
			{
				var result = new TIntersectHit2D();

				Vector2D diff = ray_pos_2 - ray_pos_1;

				var dot_perp_d1_d2 = ray_dir_1.DotPerp(in ray_dir_2);

				if (Math.Abs(dot_perp_d1_d2) > XGeometry2D.Eplsilon_d)
				{
					// Segments intersect in a single point.
					var inv_dot_perp_d1_d2 = 1 / dot_perp_d1_d2;

					var diff_dot_perp_d1 = diff.DotPerp(ray_dir_1);
					var diff_dot_perp_d2 = diff.DotPerp(ray_dir_2);

					result.Distance1 = (Single)(diff_dot_perp_d2 * inv_dot_perp_d1_d2);
					result.Distance2 = (Single)(diff_dot_perp_d1 * inv_dot_perp_d1_d2);
					result.Point1 = ray_pos_1 + (ray_dir_1 * result.Distance1);
					if (result.Distance1 > 0 && result.Distance2 > 0)
					{
						hit = result;
						return TIntersectType2D.Point;
					}
					else
					{
						hit = result;
						return TIntersectType2D.None;
					}

				}

				// Segments are parallel
				diff.Normalize();
				var diff_dot_perp_dir2 = diff.DotPerp(ray_dir_2);
				if (Math.Abs(diff_dot_perp_dir2) <= XGeometry2D.Eplsilon_d)
				{
					// Segments are colinear
					hit = result;
					return TIntersectType2D.Parallel;
				}

				hit = result;
				return TIntersectType2D.None;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="ray_pos_1">Позиция первого луча</param>
			/// <param name="ray_dir_1">Направление первого луча</param>
			/// <param name="ray_pos_2">Позиция второго луча</param>
			/// <param name="ray_dir_2">Направление второго луча</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType2D RayToRay(in Vector2Df ray_pos_1, in Vector2Df ray_dir_1, in Vector2Df ray_pos_2,
				in Vector2Df ray_dir_2, out TIntersectHit2Df hit)
			{
				var result = new TIntersectHit2Df();

				Vector2Df diff = ray_pos_2 - ray_pos_1;

				var dot_perp_d1_d2 = ray_dir_1.DotPerp(in ray_dir_2);

				if (Math.Abs(dot_perp_d1_d2) > XGeometry2D.Eplsilon_d)
				{
					// Segments intersect in a single point.
					Double inv_dot_perp_d1_d2 = 1 / dot_perp_d1_d2;

					Double diff_dot_perp_d1 = diff.DotPerp(ray_dir_1);
					Double diff_dot_perp_d2 = diff.DotPerp(ray_dir_2);

					result.Distance1 = (Single)(diff_dot_perp_d2 * inv_dot_perp_d1_d2);
					result.Distance2 = (Single)(diff_dot_perp_d1 * inv_dot_perp_d1_d2);
					result.Point1 = ray_pos_1 + (ray_dir_1 * result.Distance1);
					if (result.Distance1 > 0 && result.Distance2 > 0)
					{
						hit = result;
						return TIntersectType2D.Point;
					}
					else
					{
						hit = result;
						return TIntersectType2D.None;
					}

				}

				// Segments are parallel
				diff.Normalize();
				var diff_dot_perp_dir2 = diff.DotPerp(ray_dir_2);
				if (Math.Abs(diff_dot_perp_dir2) <= XGeometry2D.Eplsilon_d)
				{
					// Segments are colinear
					hit = result;
					return TIntersectType2D.Parallel;
				}

				hit = result;
				return TIntersectType2D.None;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="ray_a">Первый луч</param>
			/// <param name="ray_b">Второй луч</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayRay(in Ray2Df ray_a, in Ray2Df ray_b)
			{
				return RayRay(in ray_a.Position, in ray_a.Direction, in ray_b.Position, in ray_b.Direction, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="ray_a">Первый луч</param>
			/// <param name="ray_b">Второй луч</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayRay(in Ray2Df ray_a, in Ray2Df ray_b, out TIntersectHit2Df hit)
			{
				return RayRay(ray_a.Position, ray_a.Direction, ray_b.Position, ray_b.Direction, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="pos_a">Позиция первого луча</param>
			/// <param name="dir_a">Направление первого луча</param>
			/// <param name="pos_b">Позиция второго луча</param>
			/// <param name="dir_b">Направление второго луча</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayRay(in Vector2Df pos_a, in Vector2Df dir_a, in Vector2Df pos_b, in Vector2Df dir_b)
			{
				return RayRay(pos_a, dir_a, pos_b, dir_b, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="pos_a">Позиция первого луча</param>
			/// <param name="dir_a">Направление первого луча</param>
			/// <param name="pos_b">Позиция второго луча</param>
			/// <param name="dir_b">Направление второго луча</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayRay(in Vector2Df pos_a, in Vector2Df dir_a, in Vector2Df pos_b,
				in Vector2Df dir_b, out TIntersectHit2Df hit)
			{
				Vector2Df position_b_to_a = pos_a - pos_b;
				var denominator = Vector2Df.DotPerp(in dir_a, in dir_b);
				var perp_dot_a = Vector2Df.DotPerp(in dir_a, in position_b_to_a);
				var perp_dot_b = Vector2Df.DotPerp(in dir_b, in position_b_to_a);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						hit = TIntersectHit2Df.None();
						return false;
					}

					// Collinear
					var codirected = Vector2Df.Dot(in dir_a, in dir_b) > 0;
					var position_b_projection = -Vector2Df.Dot(in dir_a, in position_b_to_a);
					if (codirected)
					{
						hit = TIntersectHit2Df.Parallel();
						return true;
					}
					else
					{
						if (position_b_projection < -XGeometry2D.Eplsilon_f)
						{
							hit = TIntersectHit2Df.None();
							return false;
						}
						if (position_b_projection < XGeometry2D.Eplsilon_f)
						{
							hit = TIntersectHit2Df.Point(pos_a);
							return true;
						}
						hit = TIntersectHit2Df.Segment(pos_a, pos_b);
						return true;
					}
				}

				// Not parallel
				var dist_a = perp_dot_b / denominator;
				if (dist_a < -XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				var dist_b = perp_dot_a / denominator;
				if (dist_b < -XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				hit = TIntersectHit2Df.Point(pos_a + (dir_a * dist_a));
				return true;
			}

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="ray_pos_1">Позиция первого луча</param>
			/// <param name="ray_dir_1">Направление первого луча</param>
			/// <param name="ray_pos_2">Позиция второго луча</param>
			/// <param name="ray_dir_2">Направление второго луча</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType2D RayToRay(UnityEngine.Vector2 ray_pos_1, UnityEngine.Vector2 ray_dir_1,
				UnityEngine.Vector2 ray_pos_2, UnityEngine.Vector2 ray_dir_2, out TIntersectHit2Df hit)
			{
				return RayToRay(in ray_pos_1, in ray_dir_1, in ray_pos_2, in ray_dir_2, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="ray_pos_1">Позиция первого луча</param>
			/// <param name="ray_dir_1">Направление первого луча</param>
			/// <param name="ray_pos_2">Позиция второго луча</param>
			/// <param name="ray_dir_2">Направление второго луча</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType2D RayToRay(in UnityEngine.Vector2 ray_pos_1, in UnityEngine.Vector2 ray_dir_1,
				in UnityEngine.Vector2 ray_pos_2, in UnityEngine.Vector2 ray_dir_2, in TIntersectHit2Df hit)
			{
				UnityEngine.Vector2 diff = ray_pos_2 - ray_pos_1;

				Single dot_perp_d1_d2 = ray_dir_1.DotPerp(in ray_dir_2);

				if (Math.Abs(dot_perp_d1_d2) > XGeometry2D.Eplsilon_f)
				{
					// Segments intersect in a single point.
					Single inv_dot_perp_d1_d2 = 1 / dot_perp_d1_d2;

					Single diff_dot_perp_d1 = diff.DotPerp(in ray_dir_1);
					Single diff_dot_perp_d2 = diff.DotPerp(in ray_dir_2);

					hit.Distance1 = diff_dot_perp_d2 * inv_dot_perp_d1_d2;
					hit.Distance2 = diff_dot_perp_d1 * inv_dot_perp_d1_d2;
					UnityEngine.Vector2 p1 = ray_pos_1 + ray_dir_1 * hit.Distance1;
					hit.Point1 = new Vector2Df(p1.x, p1.y);
					if (hit.Distance1 > 0 && hit.Distance2 > 0)
					{
						return TIntersectType2D.Point;
					}
					else
					{
						return TIntersectType2D.None;
					}

				}

				// Segments are parallel
				diff.Normalize();
				Single diff_dot_perp_dir2 = diff.DotPerp(in ray_dir_2);
				if (Math.Abs(diff_dot_perp_dir2) <= XGeometry2D.Eplsilon_f)
				{
					// Segments are colinear
					return TIntersectType2D.Parallel;
				}

				return TIntersectType2D.None;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="ray_pos_1">Позиция первого луча</param>
			/// <param name="ray_dir_1">Направление первого луча</param>
			/// <param name="ray_pos_2">Позиция второго луча</param>
			/// <param name="ray_dir_2">Направление второго луча</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayToRay(UnityEngine.Vector2 ray_pos_1, UnityEngine.Vector2 ray_dir_1,
				UnityEngine.Vector2 ray_pos_2, UnityEngine.Vector2 ray_dir_2)
			{
				return RayToRay(in ray_pos_1, in ray_dir_1, in ray_pos_2, in ray_dir_2);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="ray_pos_1">Позиция первого луча</param>
			/// <param name="ray_dir_1">Направление первого луча</param>
			/// <param name="ray_pos_2">Позиция второго луча</param>
			/// <param name="ray_dir_2">Направление второго луча</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayToRay(in UnityEngine.Vector2 ray_pos_1, in UnityEngine.Vector2 ray_dir_1,
				in UnityEngine.Vector2 ray_pos_2, in UnityEngine.Vector2 ray_dir_2)
			{
				UnityEngine.Vector2 diff = ray_pos_2 - ray_pos_1;
				Single dot_perp_d1_d2 = ray_dir_1.DotPerp(in ray_dir_2);

				if (Math.Abs(dot_perp_d1_d2) > XGeometry2D.Eplsilon_f)
				{
					// Segments intersect in a single point.
					Single inv_dot_perp_d1_d2 = 1.0f / dot_perp_d1_d2;

					Single diff_dot_perp_d1 = diff.DotPerp(in ray_dir_1);
					Single diff_dot_perp_d2 = diff.DotPerp(in ray_dir_2);

					Single distance1 = diff_dot_perp_d2 * inv_dot_perp_d1_d2;
					Single distance2 = diff_dot_perp_d1 * inv_dot_perp_d1_d2;
					if (distance1 > 0 && distance2 > 0)
					{
						return true;
					}
				}

				return false;
			}
#endif
			#endregion Ray-Ray

			#region ======================================= Ray - Segment =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и отрезка
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="segment">Отрезок</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RaySegment(in Ray2Df ray, in Segment2Df segment)
			{
				return RaySegment(in ray.Position, in ray.Direction, in segment.Start, in segment.End, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и отрезка
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="segment">Отрезок</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RaySegment(in Ray2Df ray, in Segment2Df segment, out TIntersectHit2Df hit)
			{
				return RaySegment(ray.Position, ray.Direction, segment.Start, segment.End, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и отрезка
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RaySegment(in Vector2Df ray_pos, in Vector2Df ray_dir, in Vector2Df start,
				in Vector2Df end)
			{
				return RaySegment(ray_pos, ray_dir, start, end, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и отрезка
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RaySegment(in Vector2Df ray_pos, in Vector2Df ray_dir, in Vector2Df start,
				in Vector2Df end, out TIntersectHit2Df hit)
			{
				Vector2Df segment_start_to_pos = ray_pos - start;
				Vector2Df segment_dir = end - start;

				var denominator = Vector2Df.DotPerp(in ray_dir, in segment_dir);
				var perp_dot_start = Vector2Df.DotPerp(in ray_dir, in segment_start_to_pos);

				// Normalized direction gives more stable results 
				var perp_dot_end = Vector2Df.DotPerp(segment_dir.Normalized, segment_start_to_pos);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					if (Math.Abs(perp_dot_start) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_end) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						hit = TIntersectHit2Df.None();
						return false;
					}
					// Collinear

					var segment_is_start_point = segment_dir.SqrLength < XGeometry2D.Eplsilon_f;
					var start_projection = Vector2Df.Dot(ray_dir, start - ray_pos);
					if (segment_is_start_point)
					{
						if (start_projection > -XGeometry2D.Eplsilon_f)
						{
							hit = TIntersectHit2Df.Point(in start);
							return true;
						}
						hit = TIntersectHit2Df.None();
						return false;
					}

					var endProjection = Vector2Df.Dot(ray_dir, end - ray_pos);
					if (start_projection > -XGeometry2D.Eplsilon_f)
					{
						if (endProjection > -XGeometry2D.Eplsilon_f)
						{
							if (endProjection > start_projection)
							{
								hit = TIntersectHit2Df.Segment(in start, in end);
							}
							else
							{
								hit = TIntersectHit2Df.Segment(in end, in start);
							}
						}
						else
						{
							if (start_projection > XGeometry2D.Eplsilon_f)
							{
								hit = TIntersectHit2Df.Segment(in ray_pos, in start);
							}
							else
							{
								hit = TIntersectHit2Df.Point(in ray_pos);
							}
						}
						return true;
					}
					if (endProjection > -XGeometry2D.Eplsilon_f)
					{
						if (endProjection > XGeometry2D.Eplsilon_f)
						{
							hit = TIntersectHit2Df.Segment(in ray_pos, in end);
						}
						else
						{
							hit = TIntersectHit2Df.Point(in ray_pos);
						}
						return true;
					}
					hit = TIntersectHit2Df.None();
					return false;
				}

				// Not parallel
				var ray_distance = perp_dot_end / denominator;
				var segment_distance = perp_dot_start / denominator;
				if (ray_distance > -XGeometry2D.Eplsilon_f &&
					segment_distance > -XGeometry2D.Eplsilon_f && segment_distance < 1 + XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.Point(start + (segment_dir * segment_distance), segment_distance);
					return true;
				}
				hit = TIntersectHit2Df.None();
				return false;
			}
			#endregion Ray-Segment

			#region ======================================= Ray - Circle ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и окружности
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="circle">Окружность</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayCircle(in Ray2Df ray, in Circle2Df circle)
			{
				return RayCircle(in ray.Position, in ray.Direction, in circle.Center, circle.Radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и окружности
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="circle">Окружность</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayCircle(in Ray2Df ray, in Circle2Df circle, out TIntersectHit2Df hit)
			{
				return RayCircle(in ray.Position, in ray.Direction, in circle.Center, circle.Radius, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и окружности
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayCircle(in Vector2Df ray_pos, in Vector2Df ray_dir, in Vector2Df circle_center, 
				Single circle_radius)
			{
				return RayCircle(in ray_pos, in ray_dir, in circle_center, circle_radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и окружности
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayCircle(in Vector2Df ray_pos, in Vector2Df ray_dir, in Vector2Df circle_center, 
				Single circle_radius, out TIntersectHit2Df hit)
			{
				Vector2Df position_to_center = circle_center - ray_pos;
				var center_projection = Vector2Df.Dot(in ray_dir, in position_to_center);
				if (center_projection + circle_radius < -XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				var sqr_distance_to_line = position_to_center.SqrLength - (center_projection * center_projection);
				var sqr_distance_to_intersection = (circle_radius * circle_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}
				if (sqr_distance_to_intersection < XGeometry2D.Eplsilon_f)
				{
					if (center_projection < -XGeometry2D.Eplsilon_f)
					{
						hit = TIntersectHit2Df.None();
						return false;
					}
					hit = TIntersectHit2Df.Point(ray_pos + (ray_dir * center_projection));
					return true;
				}

				// Line hit
				var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				var dist_a = center_projection - distance_to_intersection;
				var dist_b = center_projection + distance_to_intersection;

				if (dist_a < -XGeometry2D.Eplsilon_f)
				{
					if (dist_b < -XGeometry2D.Eplsilon_f)
					{
						hit = TIntersectHit2Df.None();
						return false;
					}
					hit = TIntersectHit2Df.Point(ray_pos + (ray_dir * dist_b));
					return true;
				}

				Vector2Df point_a = ray_pos + (ray_dir * dist_a);
				Vector2Df point_b = ray_pos + (ray_dir * dist_b);
				hit = TIntersectHit2Df.Segment(in point_a, in point_b);
				return true;
			}

			#endregion Ray-Circle

			#region ======================================= Segment - Segment =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения прямоугольника отрезком
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="rect">Прямоугольник</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentToRect(in Vector2D start, in Vector2D end, Rect2D rect)
			{
				return SegmentToSegment(in start, in end, new Vector2D(rect.X, rect.Y), new Vector2D(rect.X + rect.Width, rect.Y)) ||
					   SegmentToSegment(in start, in end, new Vector2D(rect.X + rect.Width, rect.Y), new Vector2D(rect.X + rect.Width, rect.Y + rect.Height)) ||
					   SegmentToSegment(in start, in end, new Vector2D(rect.X + rect.Width, rect.Y + rect.Height), new Vector2D(rect.X, rect.Y + rect.Height)) ||
					   SegmentToSegment(in start, in end, new Vector2D(rect.X, rect.Y + rect.Height), new Vector2D(rect.X, rect.Y)) ||
					   rect.Contains(start) && rect.Contains(end);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения прямоугольника отрезком
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="rect">Прямоугольник</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentToRect(in Vector2Df start, in Vector2Df end, Rect2Df rect)
			{
				return SegmentToSegment(in start, in end, new Vector2Df(rect.X, rect.Y), new Vector2Df(rect.X + rect.Width, rect.Y)) ||
					   SegmentToSegment(in start, in end, new Vector2Df(rect.X + rect.Width, rect.Y), new Vector2Df(rect.X + rect.Width, rect.Y + rect.Height)) ||
					   SegmentToSegment(in start, in end, new Vector2Df(rect.X + rect.Width, rect.Y + rect.Height), new Vector2Df(rect.X, rect.Y + rect.Height)) ||
					   SegmentToSegment(in start, in end, new Vector2Df(rect.X, rect.Y + rect.Height), new Vector2Df(rect.X, rect.Y)) ||
					   rect.Contains(start) && rect.Contains(end);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух отрезков
			/// </summary>
			/// <param name="start_1">Начало первого отрезка</param>
			/// <param name="end_1">Конец первого отрезка</param>
			/// <param name="start_2">Начало второго отрезка</param>
			/// <param name="end_2">Конец второго отрезка</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentToSegment(in Vector2D start_1, in Vector2D end_1, in Vector2D start_2,
				in Vector2D end_2)
			{
				var x1 = start_1.X;
				var y1 = start_1.Y;

				var x2 = end_1.X;
				var y2 = end_1.Y;

				var x3 = start_2.X;
				var y3 = start_2.Y;

				var x4 = end_2.X;
				var y4 = end_2.Y;

				// Проверяем параллельность
				var d = ((x1 - x2) * (y3 - y4)) - ((y1 - y2) * (x3 - x4));
				if (XMath.Approximately(d, 0, XGeometry2D.Eplsilon_d))
				{
					return false;
				}

				var qx = (((x1 * y2) - (y1 * x2)) * (x3 - x4)) - ((x1 - x2) * ((x3 * y4) - (y3 * x4)));
				var qy = (((x1 * y2) - (y1 * x2)) * (y3 - y4)) - ((y1 - y2) * ((x3 * y4) - (y3 * x4)));

				var point_x = qx / d;
				var point_y = qy / d;

				// Проверяем что бы эта точка попала в области отрезков
				var ddx = x2 - x1;
				Double tx = 0.5f;
				if (!XMath.Approximately(ddx, 0))
				{
					tx = (point_x - x1) / ddx;
				}

				Double ty = 0.5f;
				var ddy = y2 - y1;
				if (!XMath.Approximately(ddy, 0))
				{
					ty = (point_y - y1) / ddy;
				}


				if (tx < 0 || tx > 1 || ty < 0 || ty > 1)
				{
					return false;
				}

				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух отрезков
			/// </summary>
			/// <param name="start_1">Начало первого отрезка</param>
			/// <param name="end_1">Конец первого отрезка</param>
			/// <param name="start_2">Начало второго отрезка</param>
			/// <param name="end_2">Конец второго отрезка</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentToSegment(in Vector2Df start_1, in Vector2Df end_1, in Vector2Df start_2,
				in Vector2Df end_2)
			{
				var x1 = start_1.X;
				var y1 = start_1.Y;

				var x2 = end_1.X;
				var y2 = end_1.Y;

				var x3 = start_2.X;
				var y3 = start_2.Y;

				var x4 = end_2.X;
				var y4 = end_2.Y;

				// Проверяем параллельность
				var d = ((x1 - x2) * (y3 - y4)) - ((y1 - y2) * (x3 - x4));
				if (XMath.Approximately(d, 0, XGeometry2D.Eplsilon_d))
				{
					return false;
				}

				var qx = (((x1 * y2) - (y1 * x2)) * (x3 - x4)) - ((x1 - x2) * ((x3 * y4) - (y3 * x4)));
				var qy = (((x1 * y2) - (y1 * x2)) * (y3 - y4)) - ((y1 - y2) * ((x3 * y4) - (y3 * x4)));

				var point_x = qx / d;
				var point_y = qy / d;

				// Проверяем что бы эта точка попала в области отрезков
				var ddx = x2 - x1;
				var tx = 0.5f;
				if (!XMath.Approximately(ddx, 0))
				{
					tx = (point_x - x1) / ddx;
				}

				var ty = 0.5f;
				var ddy = y2 - y1;
				if (!XMath.Approximately(ddy, 0))
				{
					ty = (point_y - y1) / ddy;
				}


				if (tx < 0 || tx > 1 || ty < 0 || ty > 1)
				{
					return false;
				}

				return true;
			}

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух отрезков
			/// </summary>
			/// <param name="start_1">Начало первого отрезка</param>
			/// <param name="end_1">Конец первого отрезка</param>
			/// <param name="start_2">Начало второго отрезка</param>
			/// <param name="end_2">Конец второго отрезка</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentToSegment(UnityEngine.Vector2 start_1, UnityEngine.Vector2 end_1,
				UnityEngine.Vector2 start_2, UnityEngine.Vector2 end_2)
			{
				return SegmentToSegment(in start_1, in end_1, in start_2, in end_2);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух отрезков
			/// </summary>
			/// <param name="start_1">Начало первого отрезка</param>
			/// <param name="end_1">Конец первого отрезка</param>
			/// <param name="start_2">Начало второго отрезка</param>
			/// <param name="end_2">Конец второго отрезка</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentToSegment(in UnityEngine.Vector2 start_1, in UnityEngine.Vector2 end_1,
				in UnityEngine.Vector2 start_2, in UnityEngine.Vector2 end_2)
			{
				Single x1 = start_1.x;
				Single y1 = start_1.y;

				Single x2 = end_1.x;
				Single y2 = end_1.y;

				Single x3 = start_2.x;
				Single y3 = start_2.y;

				Single x4 = end_2.x;
				Single y4 = end_2.y;

				// Проверяем параллельность
				Single d = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
				if (XMath.Approximately(d, 0, XGeometry2D.Eplsilon_f))
				{
					return false;
				}

				Single qx = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
				Single qy = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);

				Single point_x = qx / d;
				Single point_y = qy / d;

				// Проверяем что бы эта точка попала в области отрезков
				Single ddx = x2 - x1;
				Single tx = 0.5f;
				if (!UnityEngine.Mathf.Approximately(ddx, 0))
				{
					tx = (point_x - x1) / ddx;
				}

				Single ty = 0.5f;
				Single ddy = y2 - y1;
				if (!UnityEngine.Mathf.Approximately(ddy, 0))
				{
					ty = (point_y - y1) / ddy;
				}


				if (tx < 0 || tx > 1 || ty < 0 || ty > 1)
				{
					return false;
				}

				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух отрезков
			/// </summary>
			/// <param name="start_1">Начало первого отрезка</param>
			/// <param name="end_1">Конец первого отрезка</param>
			/// <param name="start_2">Начало второго отрезка</param>
			/// <param name="end_2">Конец второго отрезка</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType2D SegmentToSegment(in UnityEngine.Vector2 start_1, in UnityEngine.Vector2 end_1,
				in UnityEngine.Vector2 start_2, in UnityEngine.Vector2 end_2, in TIntersectHit2Df hit)
			{
				Single x1 = start_1.x;
				Single y1 = start_1.y;

				Single x2 = end_1.x;
				Single y2 = end_1.y;

				Single x3 = start_2.x;
				Single y3 = start_2.y;

				Single x4 = end_2.x;
				Single y4 = end_2.y;

				// Проверяем параллельность
				Single d = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
				if (XMath.Approximately(d, 0, XGeometry2D.Eplsilon_f))
				{
					return TIntersectType2D.Parallel;
				}

				Single qx = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
				Single qy = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);

				Single point_x = qx / d;
				Single point_y = qy / d;

				// Проверяем что бы эта точка попала в области отрезков
				Single ddx = x2 - x1;
				Single tx = 0.5f;
				if (!UnityEngine.Mathf.Approximately(ddx, 0))
				{
					tx = (point_x - x1) / ddx;
				}

				Single ty = 0.5f;
				Single ddy = y2 - y1;
				if (!UnityEngine.Mathf.Approximately(ddy, 0))
				{
					ty = (point_y - y1) / ddy;
				}


				if (tx < 0 || tx > 1 || ty < 0 || ty > 1)
				{
					return TIntersectType2D.None;
				}

				hit.Point1 = new Vector2Df(point_x, point_y);

				return TIntersectType2D.Point;
			}
#endif

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух отрезков
			/// </summary>
			/// <param name="segment1">Первый отрезок</param>
			/// <param name="segment2">Второй отрезок</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentSegment(in Segment2Df segment1, in Segment2Df segment2)
			{
				return SegmentSegment(in segment1.Start, in segment1.End, in segment2.Start, in segment2.End, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух отрезков
			/// </summary>
			/// <param name="segment1">Первый отрезок</param>
			/// <param name="segment2">Второй отрезок</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentSegment(in Segment2Df segment1, in Segment2Df segment2, out TIntersectHit2Df hit)
			{
				return SegmentSegment(in segment1.Start, in segment1.End, in segment2.Start, in segment2.End, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух отрезков
			/// </summary>
			/// <param name="start_1">Начало первого отрезка</param>
			/// <param name="end_1">Конец первого отрезка</param>
			/// <param name="start_2">Начало второго отрезка</param>
			/// <param name="end_2">Конец второго отрезка</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentSegment(in Vector2Df start_1, in Vector2Df end_1, in Vector2Df start_2,
				in Vector2Df end_2)
			{
				return SegmentSegment(start_1, end_1, start_2, end_2, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух отрезков
			/// </summary>
			/// <param name="start_1">Начало первого отрезка</param>
			/// <param name="end_1">Конец первого отрезка</param>
			/// <param name="start_2">Начало второго отрезка</param>
			/// <param name="end_2">Конец второго отрезка</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentSegment(in Vector2Df start_1, in Vector2Df end_1, in Vector2Df start_2,
				in Vector2Df end_2, out TIntersectHit2Df hit)
			{
				Vector2Df from_2_start_to_1_start = start_1 - start_2;
				Vector2Df direction1 = end_1 - start_1;
				Vector2Df direction2 = end_2 - start_2;

				var sqr_segment_1_length = direction1.SqrLength;
				var sqr_segment_2_length = direction2.SqrLength;
				var segment_1_is_point = sqr_segment_1_length < XGeometry2D.Eplsilon_f;
				var segment_2_is_point = sqr_segment_2_length < XGeometry2D.Eplsilon_f;
				if (segment_1_is_point && segment_2_is_point)
				{
					if (start_1 == start_2)
					{
						hit = TIntersectHit2Df.Point(in start_1);
						return true;
					}
					hit = TIntersectHit2Df.None();
					return false;
				}
				if (segment_1_is_point)
				{
					if (PointSegment(in start_1, in start_2, in direction2, sqr_segment_2_length))
					{
						hit = TIntersectHit2Df.Point(in start_1);
						return true;
					}
					hit = TIntersectHit2Df.None();
					return false;
				}
				if (segment_2_is_point)
				{
					if (PointSegment(in start_2, in start_1, in direction1, sqr_segment_1_length))
					{
						hit = TIntersectHit2Df.Point(start_2);
						return true;
					}
					hit = TIntersectHit2Df.None();
					return false;
				}

				var denominator = Vector2Df.DotPerp(in direction1, in direction2);
				var perp_dot_1 = Vector2Df.DotPerp(in direction1, in from_2_start_to_1_start);
				var perp_dot_2 = Vector2Df.DotPerp(in direction2, in from_2_start_to_1_start);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					if (Math.Abs(perp_dot_1) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_2) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						hit = TIntersectHit2Df.None();
						return false;
					}
					// Collinear

					var codirected = Vector2Df.Dot(in direction1, in direction2) > 0;
					if (codirected)
					{
						// Codirected
						var segment2AProjection = -Vector2Df.Dot(in direction1, in from_2_start_to_1_start);
						if (segment2AProjection > -XGeometry2D.Eplsilon_f)
						{
							// 1A------1B
							//     2A------2B
							return SegmentSegmentCollinear(in start_1, in end_1, sqr_segment_1_length, in start_2, in end_2, out hit);
						}
						else
						{
							//     1A------1B
							// 2A------2B
							return SegmentSegmentCollinear(in start_2, in end_2, sqr_segment_2_length, in start_1, in end_1, out hit);
						}
					}
					else
					{
						// Contradirected
						var segment2BProjection = Vector2Df.Dot(direction1, end_2 - start_1);
						if (segment2BProjection > -XGeometry2D.Eplsilon_f)
						{
							// 1A------1B
							//     2B------2A
							return SegmentSegmentCollinear(in start_1, in end_1, sqr_segment_1_length, in end_2, in start_2, out hit);
						}
						else
						{
							//     1A------1B
							// 2B------2A
							return SegmentSegmentCollinear(in end_2, in start_2, sqr_segment_2_length, in start_1, in end_1, out hit);
						}
					}
				}

				// Not parallel
				var distance1 = perp_dot_2 / denominator;
				if (distance1 < -XGeometry2D.Eplsilon_f || distance1 > 1 + XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				var distance2 = perp_dot_1 / denominator;
				if (distance2 < -XGeometry2D.Eplsilon_f || distance2 > 1 + XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				hit = TIntersectHit2Df.Point(start_1 + (direction1 * distance1));
				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух отрезков
			/// </summary>
			/// <param name="left_a">Начало первого отрезка</param>
			/// <param name="left_b">Конец первого отрезка</param>
			/// <param name="sqr_left_length">Квадрат длины первого отрезка</param>
			/// <param name="right_a">Начало второго отрезка</param>
			/// <param name="right_b">Конец второго отрезка</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Boolean SegmentSegmentCollinear(in Vector2Df left_a, in Vector2Df left_b, Single sqr_left_length,
				in Vector2Df right_a, in Vector2Df right_b, out TIntersectHit2Df hit)
			{
				Vector2Df left_direction = left_b - left_a;
				var right_a_projection = Vector2Df.Dot(in left_direction, right_a - left_b);
				if (Math.Abs(right_a_projection) < XGeometry2D.Eplsilon_f)
				{
					// LB == RA
					// LA------LB
					//         RA------RB
					hit = TIntersectHit2Df.Point(left_b);
					return true;
				}
				if (right_a_projection < 0)
				{
					// LB > RA
					// LA------LB
					//     RARB
					//     RA--RB
					//     RA------RB
					Vector2Df point_b;
					var right_b_projection = Vector2Df.Dot(in left_direction, right_b - left_a);
					if (right_b_projection > sqr_left_length)
					{
						point_b = left_b;
					}
					else
					{
						point_b = right_b;
					}
					hit = TIntersectHit2Df.Segment(in right_a, in point_b);
					return true;
				}
				// LB < RA
				// LA------LB
				//             RA------RB
				hit = TIntersectHit2Df.None();
				return false;
			}
			#endregion Segment-Segment

			#region ======================================= Segment - Circle ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения отрезка и окружности
			/// </summary>
			/// <param name="segment">Отрезок</param>
			/// <param name="circle">Окружность</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentCircle(in Segment2Df segment, in Circle2Df circle)
			{
				return SegmentCircle(in segment.Start, in segment.End, in circle.Center, circle.Radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения отрезка и окружности
			/// </summary>
			/// <param name="segment">Отрезок</param>
			/// <param name="circle">Окружность</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentCircle(in Segment2Df segment, in Circle2Df circle, out TIntersectHit2Df hit)
			{
				return SegmentCircle(in segment.Start, in segment.End, in circle.Center, circle.Radius, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения отрезка и окружности
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentCircle(in Vector2Df start, in Vector2Df end, in Vector2Df circle_center, 
				Single circle_radius)
			{
				return SegmentCircle(in start, in end, in circle_center, circle_radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения отрезка и окружности
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentCircle(in Vector2Df start, in Vector2Df end, in Vector2Df circle_center, 
				Single circle_radius, out TIntersectHit2Df hit)
			{
				Vector2Df segment_start_to_center = circle_center - start;
				Vector2Df from_start_to_end = end - start;
				var segment_length = from_start_to_end.Length;
				if (segment_length < XGeometry2D.Eplsilon_f)
				{
					var distance_to_point = segment_start_to_center.Length;
					if (distance_to_point < circle_radius + XGeometry2D.Eplsilon_f)
					{
						if (distance_to_point > circle_radius - XGeometry2D.Eplsilon_f)
						{
							hit = TIntersectHit2Df.Point(start);
							return true;
						}
						hit = TIntersectHit2Df.None();
						return true;
					}
					hit = TIntersectHit2Df.None();
					return false;
				}

				Vector2Df segment_direction = from_start_to_end.Normalized;
				var center_projection = Vector2Df.Dot(in segment_direction, in segment_start_to_center);
				if (center_projection + circle_radius < -XGeometry2D.Eplsilon_f ||
					center_projection - circle_radius > segment_length + XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				var sqr_distance_to_line = segment_start_to_center.SqrLength - (center_projection * center_projection);
				var sqr_distance_to_intersection = (circle_radius * circle_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				if (sqr_distance_to_intersection < XGeometry2D.Eplsilon_f)
				{
					if (center_projection < -XGeometry2D.Eplsilon_f ||
						center_projection > segment_length + XGeometry2D.Eplsilon_f)
					{
						hit = TIntersectHit2Df.None();
						return false;
					}
					hit = TIntersectHit2Df.Point(start + (segment_direction * center_projection));
					return true;
				}

				// Line hit
				var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				var dist_a = center_projection - distance_to_intersection;
				var dist_b = center_projection + distance_to_intersection;

				var point_a_is_after_segment_start = dist_a > -XGeometry2D.Eplsilon_f;
				var point_b_is_before_segment_end = dist_b < segment_length + XGeometry2D.Eplsilon_f;

				if (point_a_is_after_segment_start && point_b_is_before_segment_end)
				{
					Vector2Df point_a = start + (segment_direction * dist_a);
					Vector2Df point_b = start + (segment_direction * dist_b);
					hit = TIntersectHit2Df.Segment(in point_a, in point_b);
					return true;
				}
				if (!point_a_is_after_segment_start && !point_b_is_before_segment_end)
				{
					// The segment is inside, but no hit
					hit = TIntersectHit2Df.None();
					return true;
				}

				var point_a_is_before_segment_end = dist_a < segment_length + XGeometry2D.Eplsilon_f;
				if (point_a_is_after_segment_start && point_a_is_before_segment_end)
				{
					// Point A hit
					hit = TIntersectHit2Df.Point(start + (segment_direction * dist_a));
					return true;
				}
				var point_b_is_after_segment_start = dist_b > -XGeometry2D.Eplsilon_f;
				if (point_b_is_after_segment_start && point_b_is_before_segment_end)
				{
					// Point B hit
					hit = TIntersectHit2Df.Point(start + (segment_direction * dist_b));
					return true;
				}

				hit = TIntersectHit2Df.None();
				return false;
			}
			#endregion Segment-Circle

			#region ======================================= Circle - Circle ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения двух окружностей
			/// </summary>
			/// <param name="circle_a">Первая окружность</param>
			/// <param name="circle_b">Вторая окружность</param>
			/// <returns>Статус пересечения окружностей (в том числе когда одна окружность содержится в другой)</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean CircleCircle(in Circle2Df circle_a, in Circle2Df circle_b)
			{
				return CircleCircle(circle_a.Center, circle_a.Radius, circle_b.Center, circle_b.Radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения двух окружностей
			/// </summary>
			/// <param name="circle_a">Первая окружность</param>
			/// <param name="circle_b">Вторая окружность</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения окружностей (в том числе когда одна окружность содержится в другой)</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean CircleCircle(in Circle2Df circle_a, in Circle2Df circle_b, out TIntersectHit2Df hit)
			{
				return CircleCircle(in circle_a.Center, circle_a.Radius, in circle_b.Center, circle_b.Radius, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения двух окружностей
			/// </summary>
			/// <param name="center_a">Центр первой окружности</param>
			/// <param name="radius_a">Радиус первой окружности</param>
			/// <param name="center_b">Центр второй окружности</param>
			/// <param name="radius_b">Радиус второй окружности</param>
			/// <returns>Статус пересечения окружностей (в том числе когда одна окружность содержится в другой)</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean CircleCircle(in Vector2Df center_a, in Single radius_a, in Vector2Df center_b, 
				Single radius_b)
			{
				return CircleCircle(in center_a, radius_a, in center_b, radius_b, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения двух окружностей
			/// </summary>
			/// <param name="center_a">Центр первой окружности</param>
			/// <param name="radius_a">Радиус первой окружности</param>
			/// <param name="center_b">Центр второй окружности</param>
			/// <param name="radius_b">Радиус второй окружности</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения окружностей (в том числе когда одна окружность содержится в другой)</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean CircleCircle(in Vector2Df center_a, Single radius_a, in Vector2Df center_b, 
				Single radius_b, out TIntersectHit2Df hit)
			{
				Vector2Df from_b_to_a = center_a - center_b;
				var distance_from_b_to_a_sqr = from_b_to_a.SqrLength;
				if (distance_from_b_to_a_sqr < XGeometry2D.Eplsilon_f)
				{
					if (Math.Abs(radius_a - radius_b) < XGeometry2D.Eplsilon_f)
					{
						// Circles are coincident
						hit = TIntersectHit2Df.Parallel();
						return true;
					}
					// One circle is inside the other
					hit = TIntersectHit2Df.None();
					return true;
				}

				// For intersections on the circle's edge Length is more stable than SqrLength
				var distance_from_b_to_a = XMath.Sqrt(distance_from_b_to_a_sqr);

				var sum_of_radii = radius_a + radius_b;
				if (Math.Abs(distance_from_b_to_a - sum_of_radii) < XGeometry2D.Eplsilon_f)
				{
					// One hit outside
					hit = TIntersectHit2Df.Point(center_b + (from_b_to_a * (radius_b / sum_of_radii)));
					return true;
				}
				if (distance_from_b_to_a > sum_of_radii)
				{
					// No intersections, circles are separate
					hit = TIntersectHit2Df.None();
					return false;
				}

				var difference_of_radii = radius_a - radius_b;
				var difference_of_radii_abs = Math.Abs(difference_of_radii);
				if (Math.Abs(distance_from_b_to_a - difference_of_radii_abs) < XGeometry2D.Eplsilon_f)
				{
					// One hit inside
					hit = TIntersectHit2Df.Point(center_b - (from_b_to_a * (radius_b / difference_of_radii)));
					return true;
				}
				if (distance_from_b_to_a < difference_of_radii_abs)
				{
					// One circle is contained within the other
					hit = TIntersectHit2Df.None();
					return true;
				}

				// Two intersections
				var radius_a_sqr = radius_a * radius_a;
				var distanceToMiddle = (0.5f * (radius_a_sqr - (radius_b * radius_b)) / distance_from_b_to_a_sqr) + 0.5f;
				Vector2Df middle = center_a - (from_b_to_a * distanceToMiddle);

				var discriminant = (radius_a_sqr / distance_from_b_to_a_sqr) - (distanceToMiddle * distanceToMiddle);
				Vector2Df offset = from_b_to_a.PerpToCCW() * XMath.Sqrt(discriminant);

				hit = TIntersectHit2Df.Segment(middle + offset, middle - offset);
				return true;
			}
			#endregion Circle-Circle
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================