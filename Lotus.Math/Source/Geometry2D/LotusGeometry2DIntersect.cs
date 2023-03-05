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
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup MathGeometry2D
		/*@{*/
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
				TIntersectHit2D hit = new TIntersectHit2D();
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
				TIntersectHit2D hit = new TIntersectHit2D();
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
			public static TIntersectHit2D Point(Vector2D point)
			{
				TIntersectHit2D hit = new TIntersectHit2D();
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
			public static TIntersectHit2D Point(Vector2D point, Double distance)
			{
				TIntersectHit2D hit = new TIntersectHit2D();
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
			public static TIntersectHit2D Segment(Vector2D point_1, Vector2D point_2)
			{
				TIntersectHit2D hit = new TIntersectHit2D();
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
				TIntersectHit2Df hit = new TIntersectHit2Df();
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
				TIntersectHit2Df hit = new TIntersectHit2Df();
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
			public static TIntersectHit2Df Point(Vector2Df point)
			{
				TIntersectHit2Df hit = new TIntersectHit2Df();
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
			public static TIntersectHit2Df Point(Vector2Df point, Single distance)
			{
				TIntersectHit2Df hit = new TIntersectHit2Df();
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
			public static TIntersectHit2Df Segment(Vector2Df point_1, Vector2Df point_2)
			{
				TIntersectHit2Df hit = new TIntersectHit2Df();
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
			private static Single DotPerp(this UnityEngine.Vector2 @this, ref UnityEngine.Vector2 vector)
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
			public static Boolean PointLine(Vector2Df point, Line2Df line)
			{
				return PointLine(point, line.Position, line.Direction);
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
			public static Boolean PointLine(Vector2Df point, Line2Df line, out Int32 side)
			{
				return PointLine(point, line.Position, line.Direction, out side);
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
			public static Boolean PointLine(Vector2Df point, Vector2Df line_pos, Vector2Df line_dir)
			{
				Single perp_dot = Vector2Df.DotPerp(point - line_pos, line_dir);
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
			public static Boolean PointLine(Vector2Df point, Vector2Df line_pos, 
				Vector2Df line_dir, out Int32 side)
			{
				Single perp_dot = Vector2Df.DotPerp(point - line_pos, line_dir);
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
			public static Boolean PointRay(Vector2Df point, Ray2Df ray)
			{
				return PointRay(point, ray.Position, ray.Direction);
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
			public static Boolean PointRay(Vector2Df point, Ray2Df ray, out Int32 side)
			{
				return PointRay(point, ray.Position, ray.Direction, out side);
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
			public static Boolean PointRay(Vector2Df point, Vector2Df ray_pos, Vector2Df ray_dir)
			{
				// Считаем вектор на точку
				Vector2Df to_point = point - ray_pos;

				// Считаем скалярное произвдение между векторам
				// Чем ближе оно к нулю тем соответственно точки ближе прилегает к лучу
				Single perp_dot = Vector2Df.DotPerp(ref to_point, ref ray_dir);

				if(XMath.Approximately(perp_dot, XGeometry2D.Eplsilon_f))
				{
					// Если она прилегает
					if(Vector2Df.Dot(ref ray_dir, ref to_point) > -XGeometry2D.Eplsilon_f)
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
			public static Boolean PointRay(Vector2Df point, Vector2Df ray_pos, Vector2Df ray_dir, out Int32 side)
			{
				// Считаем вектор на точку
				Vector2Df to_point = point - ray_pos;

				// Считаем скалярное произвдение между векторам
				// Чем ближе оно к нулю тем соответственно точки ближе прилегает к лучу
				Single perp_dot = Vector2Df.DotPerp(ref to_point, ref ray_dir);

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

				return (Vector2Df.Dot(ref ray_dir, ref to_point) > -XGeometry2D.Eplsilon_f);
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
			public static Boolean PointOnSegment(Vector2D start, Vector2D end, Vector2D point, Double epsilon = 0.1)
			{
				return PointOnSegment(ref start, ref end, ref point, epsilon);
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
			public static Boolean PointOnSegment(Vector2Df start, Vector2Df end, Vector2Df point, Single epsilon = 0.1f)
			{
				return PointOnSegment(ref start, ref end, ref point, epsilon);
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
			public static Boolean PointOnSegment(ref Vector2D start, ref Vector2D end, ref Vector2D point, Double epsilon = 0.1)
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

				Double x = start.X + ((point.Y - start.Y) * (end.X - start.X) / (end.Y - start.Y));
				Double y = start.Y + ((point.X - start.X) * (end.Y - start.Y) / (end.X - start.X));

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
			public static Boolean PointOnSegment(ref Vector2Df start, ref Vector2Df end, ref Vector2Df point, Single epsilon = 0.1f)
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

				Single x = start.X + ((point.Y - start.Y) * (end.X - start.X) / (end.Y - start.Y));
				Single y = start.Y + ((point.X - start.X) * (end.Y - start.Y) / (end.X - start.X));

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
				return PointOnSegment(ref start, ref end, ref point, epsilon);
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
			public static Boolean PointOnSegment(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, ref UnityEngine.Vector2 point,
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
			public static Boolean PointOnRectBorder(Rect2Df rect, Vector2Df point, Single epsilon = 0.1f)
			{
				return PointOnRectBorder(ref rect, ref point, epsilon);
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
			public static Boolean PointOnRectBorder(ref Rect2Df rect, ref Vector2Df point, Single epsilon = 0.1f)
			{
				Boolean status1 = PointOnSegment(rect.PointTopLeft, rect.PointTopRight, point, epsilon);
				if (status1)
				{
					return true;
				}
				Boolean status2 = PointOnSegment(rect.PointTopLeft, rect.PointBottomLeft, point, epsilon);
				if (status2)
				{
					return true;
				}
				Boolean status3 = PointOnSegment(rect.PointTopRight, rect.PointBottomRight, point, epsilon);
				if (status3)
				{
					return true;
				}
				Boolean status4 = PointOnSegment(rect.PointBottomLeft, rect.PointBottomRight, point, epsilon);
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
				return PointOnRectBorder(ref rect, ref point, epsilon);
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
			public static Boolean PointOnRectBorder(ref UnityEngine.Rect rect, ref UnityEngine.Vector2 point, Single epsilon = 0.1f)
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
			public static Boolean PointSegment(Vector2Df point, Segment2Df segment)
			{
				return PointSegment(point, segment.Start, segment.End);
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
			public static Boolean PointSegment(Vector2Df point, Segment2Df segment, out Int32 side)
			{
				return PointSegment(point, segment.Start, segment.End, out side);
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
			public static Boolean PointSegment(Vector2Df point, Vector2Df start, Vector2Df end)
			{
				Vector2Df from_start_to_end = end - start;
				Single sqr_segment_length = from_start_to_end.SqrLength;
				if (sqr_segment_length < XGeometry2D.Eplsilon_f)
				{
					// The segment is a point
					return point == start;
				}
				// Normalized direction gives more stable results
				Vector2Df segment_direction = from_start_to_end.Normalized;
				Vector2Df to_point = point - start;
				Single perp_dot = Vector2Df.DotPerp(ref to_point, ref segment_direction);
				if (-XGeometry2D.Eplsilon_f < perp_dot && perp_dot < XGeometry2D.Eplsilon_f)
				{
					Single point_projection = Vector2Df.Dot(ref segment_direction, ref to_point);
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
			public static Boolean PointSegment(Vector2Df point, Vector2Df start, Vector2Df end, 
				out Int32 side)
			{
				Vector2Df from_start_to_end = end - start;
				Single sqr_segment_length = from_start_to_end.SqrLength;
				if (sqr_segment_length < XGeometry2D.Eplsilon_f)
				{
					// The segment is a point
					side = 0;
					return point == start;
				}
				// Normalized direction gives more stable results
				Vector2Df segment_direction = from_start_to_end.Normalized;
				Vector2Df to_point = point - start;
				Single perpDot = Vector2Df.DotPerp(ref to_point, ref segment_direction);
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
				Single point_projection = Vector2Df.Dot(ref segment_direction, ref to_point);
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
			private static Boolean PointSegment(Vector2Df point, Vector2Df start, 
				Vector2Df segment_direction, Single sqr_segment_length)
			{
				Single segment_length = XMath.Sqrt(sqr_segment_length);
				segment_direction /= segment_length;
				Vector2Df to_point = point - start;
				Single perpDot = Vector2Df.DotPerp(ref to_point, ref segment_direction);
				if (-XGeometry2D.Eplsilon_f < perpDot && perpDot < XGeometry2D.Eplsilon_f)
				{
					Single point_projection = Vector2Df.Dot(ref segment_direction, ref to_point);
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
			public static Boolean PointSegmentCollinear(Vector2Df start, Vector2Df end, Vector2Df point)
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
#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на попадание точки в область окружности
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="circle">Окружность</param>
			/// <returns>Статус попадания</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointCircle(Vector2Df point, Circle2Df circle)
			{
				return PointCircle(point, circle.Center, circle.Radius);
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
			public static Boolean PointCircle(Vector2Df point, Vector2Df circle_сenter, Single circle_radius)
			{
				// For points on the circle's edge Length is more stable than SqrLength
				return (point - circle_сenter).Length < circle_radius + XGeometry2D.Eplsilon_f;
			}
#endif
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
			public static Boolean LineLine(Line2Df line_a, Line2Df line_b)
			{
				TIntersectHit2Df hit;
				return LineLine(line_a.Position, line_a.Direction, line_b.Position, line_b.Direction, out hit);
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
			public static Boolean LineLine(Line2Df line_a, Line2Df line_b, out TIntersectHit2Df hit)
			{
				return LineLine(line_a.Position, line_a.Direction, line_b.Position, line_b.Direction, out hit);
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
			public static Boolean LineLine(Vector2Df pos_a, Vector2Df dir_a, Vector2Df pos_b, 
				Vector2Df dir_b)
			{
				TIntersectHit2Df hit;
				return LineLine(pos_a, dir_a, pos_b, dir_b, out hit);
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
			public static Boolean LineLine(Vector2Df pos_a, Vector2Df dir_a, Vector2Df pos_b, 
				Vector2Df dir_b, out TIntersectHit2Df hit)
			{
				Vector2Df pos_b_to_a = pos_a - pos_b;
				Single denominator = Vector2Df.DotPerp(ref dir_a, ref dir_b);
				Single perp_dot_b = Vector2Df.DotPerp(ref dir_b, ref pos_b_to_a);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					Single perp_dot_a = Vector2Df.DotPerp(ref dir_a, ref pos_b_to_a);
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
			public static TIntersectType2D LineLine(Line2Df line_a, Line2Df line_b, out Single dist_a, out Single dist_b)
			{
				return LineLine(line_a.Position, line_a.Direction, line_b.Position, line_b.Direction, out dist_a, out dist_b);
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
			public static TIntersectType2D LineLine(Vector2Df pos_a, Vector2Df dir_a, Vector2Df pos_b, 
				Vector2Df dir_b, out Single dist_a, out Single dist_b)
			{
				Vector2Df pos_b_to_a = pos_a - pos_b;
				Single denominator = Vector2Df.DotPerp(ref dir_a, ref dir_b);
				Single perp_dot_a = Vector2Df.DotPerp(ref dir_a, ref pos_b_to_a);
				Single perp_bot_b = Vector2Df.DotPerp(ref dir_b, ref pos_b_to_a);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_bot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						dist_a = 0;
						dist_b = Vector2Df.Dot(ref dir_b, ref pos_b_to_a);
						return TIntersectType2D.None;
					}
					// Collinear
					dist_a = 0;
					dist_b = Vector2Df.Dot(ref dir_b, ref pos_b_to_a);
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
			public static Boolean LineRay(Line2Df line, Ray2Df ray)
			{
				TIntersectHit2Df hit;
				return LineRay(line.Position, line.Direction, ray.Position, ray.Direction, out hit);
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
			public static Boolean LineRay(Line2Df line, Ray2Df ray, out TIntersectHit2Df hit)
			{
				return LineRay(line.Position, line.Direction, ray.Position, ray.Direction, out hit);
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
			public static Boolean LineRay(Vector2Df line_pos, Vector2Df line_dir, 
				Vector2Df ray_pos, Vector2Df ray_dir)
			{
				TIntersectHit2Df hit;
				return LineRay(line_pos, line_dir, ray_pos, ray_dir, out hit);
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
			public static Boolean LineRay(Vector2Df line_pos, Vector2Df line_dir, 
				Vector2Df ray_pos, Vector2Df ray_dir, out TIntersectHit2Df hit)
			{
				Vector2Df ray_pos_to_line_pos = line_pos - ray_pos;
				Single denominator = Vector2Df.DotPerp(ref line_dir, ref ray_dir);
				Single perp_dot_a = Vector2Df.DotPerp(ref line_dir, ref ray_pos_to_line_pos);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					Single perp_dot_b = Vector2Df.DotPerp(ref ray_dir, ref ray_pos_to_line_pos);
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
				Single ray_distance = perp_dot_a / denominator;
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
			public static Boolean LineSegment(Line2Df line, Segment2Df segment)
			{
				TIntersectHit2Df hit;
				return LineSegment(line.Position, line.Direction, segment.Start, segment.End, out hit);
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
			public static Boolean LineSegment(Line2Df line, Segment2Df segment, out TIntersectHit2Df hit)
			{
				return LineSegment(line.Position, line.Direction, segment.Start, segment.End, out hit);
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
			public static Boolean LineSegment(Vector2Df line_pos, Vector2Df line_dir, 
				Vector2Df start, Vector2Df end)
			{
				TIntersectHit2Df hit;
				return LineSegment(line_pos, line_dir, start, end, out hit);
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
			public static Boolean LineSegment(Vector2Df line_pos, Vector2Df line_dir, 
				Vector2Df start, Vector2Df end, out TIntersectHit2Df hit)
			{
				Vector2Df start_to_pos = line_pos - start;
				Vector2Df segment_direction = end - start;
				Single denominator = Vector2Df.DotPerp(ref line_dir, ref segment_direction);
				Single perp_dot_start = Vector2Df.DotPerp(ref line_dir, ref start_to_pos);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					// Normalized Direction gives more stable results 
					Single perp_dot_b = Vector2Df.DotPerp(segment_direction.Normalized, start_to_pos);
					if (Math.Abs(perp_dot_start) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						hit = TIntersectHit2Df.None();
						return false;
					}
					// Collinear
					Boolean segment_is_start_point = segment_direction.SqrLength < XGeometry2D.Eplsilon_f;
					if (segment_is_start_point)
					{
						hit = TIntersectHit2Df.Point(start);
						return true;
					}

					Boolean codirected = Vector2Df.Dot(ref line_dir, ref segment_direction) > 0;
					if (codirected)
					{
						hit = TIntersectHit2Df.Segment(start, end);
					}
					else
					{
						hit = TIntersectHit2Df.Segment(end, start);
					}
					return true;
				}

				// Not parallel
				Single segment_distance = perp_dot_start / denominator;
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
			public static Boolean LineCircle(Line2Df line, Circle2Df circle)
			{
				TIntersectHit2Df hit;
				return LineCircle(line.Position, line.Direction, circle.Center, circle.Radius, out hit);
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
			public static Boolean LineCircle(Line2Df line, Circle2Df circle, out TIntersectHit2Df hit)
			{
				return LineCircle(line.Position, line.Direction, circle.Center, circle.Radius, out hit);
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
			public static Boolean LineCircle(Vector2Df line_pos, Vector2Df line_dir, 
				Vector2Df circle_center, Single circle_radius)
			{
				TIntersectHit2Df hit;
				return LineCircle(line_pos, line_dir, circle_center, circle_radius, out hit);
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
			public static Boolean LineCircle(Vector2Df line_pos, Vector2Df line_dir, 
				Vector2Df circle_center, Single circle_radius, out TIntersectHit2Df hit)
			{
				Vector2Df pos_to_center = circle_center - line_pos;
				Single center_projection = Vector2Df.Dot(ref line_dir, ref pos_to_center);
				Single sqr_dist_to_line = pos_to_center.SqrLength - (center_projection * center_projection);

				Single sqr_dist_to_intersection = (circle_radius * circle_radius) - sqr_dist_to_line;
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

				Single distance_to_intersection = XMath.Sqrt(sqr_dist_to_intersection);
				Single dist_a = center_projection - distance_to_intersection;
				Single dist_b = center_projection + distance_to_intersection;

				Vector2Df point_a = line_pos + (line_dir * dist_a);
				Vector2Df point_b = line_pos + (line_dir * dist_b);
				hit = TIntersectHit2Df.Segment(point_a, point_b);
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
			public static TIntersectType2D RayToRay(Vector2D ray_pos_1, Vector2D ray_dir_1, Vector2D ray_pos_2,
				Vector2D ray_dir_2, ref TIntersectHit2Df hit)
			{
				return RayToRay(ref ray_pos_1, ref ray_dir_1, ref ray_pos_2, ref ray_dir_2, ref hit);
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
			public static TIntersectType2D RayToRay(ref Vector2D ray_pos_1, ref Vector2D ray_dir_1, ref Vector2D ray_pos_2,
				ref Vector2D ray_dir_2, ref TIntersectHit2Df hit)
			{
				Vector2D diff = ray_pos_2 - ray_pos_1;

				Double dot_perp_d1_d2 = ray_dir_1.DotPerp(ray_dir_2);

				if (Math.Abs(dot_perp_d1_d2) > XGeometry2D.Eplsilon_d)
				{
					// Segments intersect in a single point.
					Double inv_dot_perp_d1_d2 = 1 / dot_perp_d1_d2;

					Double diff_dot_perp_d1 = diff.DotPerp(ray_dir_1);
					Double diff_dot_perp_d2 = diff.DotPerp(ray_dir_2);

					hit.Distance1 = (Single)(diff_dot_perp_d2 * inv_dot_perp_d1_d2);
					hit.Distance2 = (Single)(diff_dot_perp_d1 * inv_dot_perp_d1_d2);
					hit.Point1 = ray_pos_1 + (ray_dir_1 * hit.Distance1);
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
				Double diff_dot_perp_dir2 = diff.DotPerp(ray_dir_2);
				if (Math.Abs(diff_dot_perp_dir2) <= XGeometry2D.Eplsilon_d)
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
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType2D RayToRay(Vector2Df ray_pos_1, Vector2Df ray_dir_1, Vector2Df ray_pos_2,
				Vector2Df ray_dir_2, ref TIntersectHit2Df hit)
			{
				return RayToRay(ref ray_pos_1, ref ray_dir_1, ref ray_pos_2, ref ray_dir_2, ref hit);
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
			public static TIntersectType2D RayToRay(ref Vector2Df ray_pos_1, ref Vector2Df ray_dir_1,
				ref Vector2Df ray_pos_2, ref Vector2Df ray_dir_2, ref TIntersectHit2Df hit)
			{
				Vector2Df diff = ray_pos_2 - ray_pos_1;

				Single dot_perp_d1_d2 = ray_dir_1.DotPerp(ref ray_dir_2);

				if (Math.Abs(dot_perp_d1_d2) > XGeometry2D.Eplsilon_f)
				{
					// Segments intersect in a single point.
					Single inv_dot_perp_d1_d2 = 1 / dot_perp_d1_d2;

					Single diff_dot_perp_d1 = diff.DotPerp(ref ray_dir_1);
					Single diff_dot_perp_d2 = diff.DotPerp(ref ray_dir_2);

					hit.Distance1 = diff_dot_perp_d2 * inv_dot_perp_d1_d2;
					hit.Distance2 = diff_dot_perp_d1 * inv_dot_perp_d1_d2;
					hit.Point1 = ray_pos_1 + (ray_dir_1 * hit.Distance1);
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
				Single diff_dot_perp_dir2 = diff.DotPerp(ref ray_dir_2);
				if (Math.Abs(diff_dot_perp_dir2) <= XGeometry2D.Eplsilon_f)
				{
					// Segments are colinear
					return TIntersectType2D.Parallel;
				}

				return TIntersectType2D.None;
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
				UnityEngine.Vector2 ray_pos_2, UnityEngine.Vector2 ray_dir_2, ref TIntersectHit2Df hit)
			{
				return RayToRay(ref ray_pos_1, ref ray_dir_1, ref ray_pos_2, ref ray_dir_2, ref hit);
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
			public static TIntersectType2D RayToRay(ref UnityEngine.Vector2 ray_pos_1, ref UnityEngine.Vector2 ray_dir_1,
				ref UnityEngine.Vector2 ray_pos_2, ref UnityEngine.Vector2 ray_dir_2, ref TIntersectHit2Df hit)
			{
				UnityEngine.Vector2 diff = ray_pos_2 - ray_pos_1;

				Single dot_perp_d1_d2 = ray_dir_1.DotPerp(ref ray_dir_2);

				if (Math.Abs(dot_perp_d1_d2) > XGeometry2D.Eplsilon_f)
				{
					// Segments intersect in a single point.
					Single inv_dot_perp_d1_d2 = 1 / dot_perp_d1_d2;

					Single diff_dot_perp_d1 = diff.DotPerp(ref ray_dir_1);
					Single diff_dot_perp_d2 = diff.DotPerp(ref ray_dir_2);

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
				Single diff_dot_perp_dir2 = diff.DotPerp(ref ray_dir_2);
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
				return RayToRay(ref ray_pos_1, ref ray_dir_1, ref ray_pos_2, ref ray_dir_2);
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
			public static Boolean RayToRay(ref UnityEngine.Vector2 ray_pos_1, ref UnityEngine.Vector2 ray_dir_1,
				ref UnityEngine.Vector2 ray_pos_2, ref UnityEngine.Vector2 ray_dir_2)
			{
				UnityEngine.Vector2 diff = ray_pos_2 - ray_pos_1;
				Single dot_perp_d1_d2 = ray_dir_1.DotPerp(ref ray_dir_2);

				if (Math.Abs(dot_perp_d1_d2) > XGeometry2D.Eplsilon_f)
				{
					// Segments intersect in a single point.
					Single inv_dot_perp_d1_d2 = 1.0f / dot_perp_d1_d2;

					Single diff_dot_perp_d1 = diff.DotPerp(ref ray_dir_1);
					Single diff_dot_perp_d2 = diff.DotPerp(ref ray_dir_2);

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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух лучей
			/// </summary>
			/// <param name="ray_a">Первый луч</param>
			/// <param name="ray_b">Второй луч</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RayRay(Ray2Df ray_a, Ray2Df ray_b)
			{
				TIntersectHit2Df hit;
				return RayRay(ray_a.Position, ray_a.Direction, ray_b.Position, ray_b.Direction, out hit);
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
			public static Boolean RayRay(Ray2Df ray_a, Ray2Df ray_b, out TIntersectHit2Df hit)
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
			public static Boolean RayRay(Vector2Df pos_a, Vector2Df dir_a, Vector2Df pos_b, 
				Vector2Df dir_b)
			{
				TIntersectHit2Df hit;
				return RayRay(pos_a, dir_a, pos_b, dir_b, out hit);
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
			public static Boolean RayRay(Vector2Df pos_a, Vector2Df dir_a, Vector2Df pos_b, 
				Vector2Df dir_b, out TIntersectHit2Df hit)
			{
				Vector2Df position_b_to_a = pos_a - pos_b;
				Single denominator = Vector2Df.DotPerp(ref dir_a, ref dir_b);
				Single perp_dot_a = Vector2Df.DotPerp(ref dir_a, ref position_b_to_a);
				Single perp_dot_b = Vector2Df.DotPerp(ref dir_b, ref position_b_to_a);

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
					Boolean codirected = Vector2Df.Dot(ref dir_a, ref dir_b) > 0;
					Single position_b_projection = -Vector2Df.Dot(ref dir_a, ref position_b_to_a);
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
				Single dist_a = perp_dot_b / denominator;
				if (dist_a < -XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				Single dist_b = perp_dot_a / denominator;
				if (dist_b < -XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				hit = TIntersectHit2Df.Point(pos_a + (dir_a * dist_a));
				return true;
			}
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
			public static Boolean RaySegment(Ray2Df ray, Segment2Df segment)
			{
				TIntersectHit2Df hit;
				return RaySegment(ray.Position, ray.Direction, segment.Start, segment.End, out hit);
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
			public static Boolean RaySegment(Ray2Df ray, Segment2Df segment, out TIntersectHit2Df hit)
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
			public static Boolean RaySegment(Vector2Df ray_pos, Vector2Df ray_dir, Vector2Df start, 
				Vector2Df end)
			{
				TIntersectHit2Df hit;
				return RaySegment(ray_pos, ray_dir, start, end, out hit);
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
			public static Boolean RaySegment(Vector2Df ray_pos, Vector2Df ray_dir, Vector2Df start, 
				Vector2Df end, out TIntersectHit2Df hit)
			{
				Vector2Df segment_start_to_pos = ray_pos - start;
				Vector2Df segment_dir = end - start;

				Single denominator = Vector2Df.DotPerp(ref ray_dir, ref segment_dir);
				Single perp_dot_start = Vector2Df.DotPerp(ref ray_dir, ref segment_start_to_pos);

				// Normalized direction gives more stable results 
				Single perp_dot_end = Vector2Df.DotPerp(segment_dir.Normalized, segment_start_to_pos);

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

					Boolean segment_is_start_point = segment_dir.SqrLength < XGeometry2D.Eplsilon_f;
					Single start_projection = Vector2Df.Dot(ray_dir, start - ray_pos);
					if (segment_is_start_point)
					{
						if (start_projection > -XGeometry2D.Eplsilon_f)
						{
							hit = TIntersectHit2Df.Point(start);
							return true;
						}
						hit = TIntersectHit2Df.None();
						return false;
					}

					Single endProjection = Vector2Df.Dot(ray_dir, end - ray_pos);
					if (start_projection > -XGeometry2D.Eplsilon_f)
					{
						if (endProjection > -XGeometry2D.Eplsilon_f)
						{
							if (endProjection > start_projection)
							{
								hit = TIntersectHit2Df.Segment(start, end);
							}
							else
							{
								hit = TIntersectHit2Df.Segment(end, start);
							}
						}
						else
						{
							if (start_projection > XGeometry2D.Eplsilon_f)
							{
								hit = TIntersectHit2Df.Segment(ray_pos, start);
							}
							else
							{
								hit = TIntersectHit2Df.Point(ray_pos);
							}
						}
						return true;
					}
					if (endProjection > -XGeometry2D.Eplsilon_f)
					{
						if (endProjection > XGeometry2D.Eplsilon_f)
						{
							hit = TIntersectHit2Df.Segment(ray_pos, end);
						}
						else
						{
							hit = TIntersectHit2Df.Point(ray_pos);
						}
						return true;
					}
					hit = TIntersectHit2Df.None();
					return false;
				}

				// Not parallel
				Single ray_distance = perp_dot_end / denominator;
				Single segment_distance = perp_dot_start / denominator;
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
			public static Boolean RayCircle(Ray2Df ray, Circle2Df circle)
			{
				TIntersectHit2Df hit;
				return RayCircle(ray.Position, ray.Direction, circle.Center, circle.Radius, out hit);
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
			public static Boolean RayCircle(Ray2Df ray, Circle2Df circle, out TIntersectHit2Df hit)
			{
				return RayCircle(ray.Position, ray.Direction, circle.Center, circle.Radius, out hit);
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
			public static Boolean RayCircle(Vector2Df ray_pos, Vector2Df ray_dir, Vector2Df circle_center, 
				Single circle_radius)
			{
				TIntersectHit2Df hit;
				return RayCircle(ray_pos, ray_dir, circle_center, circle_radius, out hit);
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
			public static Boolean RayCircle(Vector2Df ray_pos, Vector2Df ray_dir, Vector2Df circle_center, 
				Single circle_radius, out TIntersectHit2Df hit)
			{
				Vector2Df position_to_center = circle_center - ray_pos;
				Single center_projection = Vector2Df.Dot(ref ray_dir, ref position_to_center);
				if (center_projection + circle_radius < -XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				Single sqr_distance_to_line = position_to_center.SqrLength - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (circle_radius * circle_radius) - sqr_distance_to_line;
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
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single dist_a = center_projection - distance_to_intersection;
				Single dist_b = center_projection + distance_to_intersection;

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
				hit = TIntersectHit2Df.Segment(point_a, point_b);
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
			public static Boolean SegmentToRect(ref Vector2D start, ref Vector2D end, Rect2D rect)
			{
				return SegmentToSegment(start, end, new Vector2D(rect.X, rect.Y), new Vector2D(rect.X + rect.Width, rect.Y)) ||
					   SegmentToSegment(start, end, new Vector2D(rect.X + rect.Width, rect.Y), new Vector2D(rect.X + rect.Width, rect.Y + rect.Height)) ||
					   SegmentToSegment(start, end, new Vector2D(rect.X + rect.Width, rect.Y + rect.Height), new Vector2D(rect.X, rect.Y + rect.Height)) ||
					   SegmentToSegment(start, end, new Vector2D(rect.X, rect.Y + rect.Height), new Vector2D(rect.X, rect.Y)) ||
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
			public static Boolean SegmentToRect(ref Vector2Df start, ref Vector2Df end, Rect2Df rect)
			{
				return SegmentToSegment(start, end, new Vector2Df(rect.X, rect.Y), new Vector2Df(rect.X + rect.Width, rect.Y)) ||
					   SegmentToSegment(start, end, new Vector2Df(rect.X + rect.Width, rect.Y), new Vector2Df(rect.X + rect.Width, rect.Y + rect.Height)) ||
					   SegmentToSegment(start, end, new Vector2Df(rect.X + rect.Width, rect.Y + rect.Height), new Vector2Df(rect.X, rect.Y + rect.Height)) ||
					   SegmentToSegment(start, end, new Vector2Df(rect.X, rect.Y + rect.Height), new Vector2Df(rect.X, rect.Y)) ||
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
			public static Boolean SegmentToSegment(Vector2D start_1, Vector2D end_1, Vector2D start_2,
				Vector2D end_2)
			{
				return SegmentToSegment(ref start_1, ref end_1, ref start_2, ref end_2);
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
			public static Boolean SegmentToSegment(Vector2Df start_1, Vector2Df end_1, Vector2Df start_2,
				Vector2Df end_2)
			{
				return SegmentToSegment(ref start_1, ref end_1, ref start_2, ref end_2);
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
			public static Boolean SegmentToSegment(ref Vector2D start_1, ref Vector2D end_1, ref Vector2D start_2,
				ref Vector2D end_2)
			{
				Double x1 = start_1.X;
				Double y1 = start_1.Y;

				Double x2 = end_1.X;
				Double y2 = end_1.Y;

				Double x3 = start_2.X;
				Double y3 = start_2.Y;

				Double x4 = end_2.X;
				Double y4 = end_2.Y;

				// Проверяем параллельность
				Double d = ((x1 - x2) * (y3 - y4)) - ((y1 - y2) * (x3 - x4));
				if (XMath.Approximately(d, 0, XGeometry2D.Eplsilon_d))
				{
					return false;
				}

				Double qx = (((x1 * y2) - (y1 * x2)) * (x3 - x4)) - ((x1 - x2) * ((x3 * y4) - (y3 * x4)));
				Double qy = (((x1 * y2) - (y1 * x2)) * (y3 - y4)) - ((y1 - y2) * ((x3 * y4) - (y3 * x4)));

				Double point_x = qx / d;
				Double point_y = qy / d;

				// Проверяем что бы эта точка попала в области отрезков
				Double ddx = x2 - x1;
				Double tx = 0.5f;
				if (!XMath.Approximately(ddx, 0))
				{
					tx = (point_x - x1) / ddx;
				}

				Double ty = 0.5f;
				Double ddy = y2 - y1;
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
			public static Boolean SegmentToSegment(ref Vector2Df start_1, ref Vector2Df end_1,
				ref Vector2Df start_2, ref Vector2Df end_2)
			{
				Single x1 = start_1.X;
				Single y1 = start_1.Y;

				Single x2 = end_1.X;
				Single y2 = end_1.Y;

				Single x3 = start_2.X;
				Single y3 = start_2.Y;

				Single x4 = end_2.X;
				Single y4 = end_2.Y;

				// Проверяем параллельность
				Single d = ((x1 - x2) * (y3 - y4)) - ((y1 - y2) * (x3 - x4));
				if (XMath.Approximately(d, 0, 0.001f))
				{
					return false;
				}

				Single qx = (((x1 * y2) - (y1 * x2)) * (x3 - x4)) - ((x1 - x2) * ((x3 * y4) - (y3 * x4)));
				Single qy = (((x1 * y2) - (y1 * x2)) * (y3 - y4)) - ((y1 - y2) * ((x3 * y4) - (y3 * x4)));

				Single point_x = qx / d;
				Single point_y = qy / d;

				// Проверяем что бы эта точка попала в области отрезков
				// Проверяем что бы эта точка попала в области отрезков
				Single ddx = x2 - x1;
				Single tx = 0.5f;
				if (!XMath.Approximately(ddx, 0))
				{
					tx = (point_x - x1) / ddx;
				}

				Single ty = 0.5f;
				Single ddy = y2 - y1;
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
				return SegmentToSegment(ref start_1, ref end_1, ref start_2, ref end_2);
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
			public static Boolean SegmentToSegment(ref UnityEngine.Vector2 start_1, ref UnityEngine.Vector2 end_1,
				ref UnityEngine.Vector2 start_2, ref UnityEngine.Vector2 end_2)
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
			public static TIntersectType2D SegmentToSegment(ref UnityEngine.Vector2 start_1, ref UnityEngine.Vector2 end_1,
				ref UnityEngine.Vector2 start_2, ref UnityEngine.Vector2 end_2, ref TIntersectHit2Df hit)
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
			public static Boolean SegmentSegment(Segment2Df segment1, Segment2Df segment2)
			{
				TIntersectHit2Df hit;
				return SegmentSegment(segment1.Start, segment1.End, segment2.Start, segment2.End, out hit);
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
			public static Boolean SegmentSegment(Segment2Df segment1, Segment2Df segment2, out TIntersectHit2Df hit)
			{
				return SegmentSegment(segment1.Start, segment1.End, segment2.Start, segment2.End, out hit);
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
			public static Boolean SegmentSegment(Vector2Df start_1, Vector2Df end_1, Vector2Df start_2, 
				Vector2Df end_2)
			{
				TIntersectHit2Df hit;
				return SegmentSegment(start_1, end_1, start_2, end_2, out hit);
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
			public static Boolean SegmentSegment(Vector2Df start_1, Vector2Df end_1, Vector2Df start_2, 
				Vector2Df end_2, out TIntersectHit2Df hit)
			{
				Vector2Df from_2_start_to_1_start = start_1 - start_2;
				Vector2Df direction1 = end_1 - start_1;
				Vector2Df direction2 = end_2 - start_2;

				Single sqr_segment_1_length = direction1.SqrLength;
				Single sqr_segment_2_length = direction2.SqrLength;
				Boolean segment_1_is_point = sqr_segment_1_length < XGeometry2D.Eplsilon_f;
				Boolean segment_2_is_point = sqr_segment_2_length < XGeometry2D.Eplsilon_f;
				if (segment_1_is_point && segment_2_is_point)
				{
					if (start_1 == start_2)
					{
						hit = TIntersectHit2Df.Point(start_1);
						return true;
					}
					hit = TIntersectHit2Df.None();
					return false;
				}
				if (segment_1_is_point)
				{
					if (PointSegment(start_1, start_2, direction2, sqr_segment_2_length))
					{
						hit = TIntersectHit2Df.Point(start_1);
						return true;
					}
					hit = TIntersectHit2Df.None();
					return false;
				}
				if (segment_2_is_point)
				{
					if (PointSegment(start_2, start_1, direction1, sqr_segment_1_length))
					{
						hit = TIntersectHit2Df.Point(start_2);
						return true;
					}
					hit = TIntersectHit2Df.None();
					return false;
				}

				Single denominator = Vector2Df.DotPerp(ref direction1, ref direction2);
				Single perp_dot_1 = Vector2Df.DotPerp(ref direction1, ref from_2_start_to_1_start);
				Single perp_dot_2 = Vector2Df.DotPerp(ref direction2, ref from_2_start_to_1_start);

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

					Boolean codirected = Vector2Df.Dot(ref direction1, ref direction2) > 0;
					if (codirected)
					{
						// Codirected
						Single segment2AProjection = -Vector2Df.Dot(ref direction1, ref from_2_start_to_1_start);
						if (segment2AProjection > -XGeometry2D.Eplsilon_f)
						{
							// 1A------1B
							//     2A------2B
							return SegmentSegmentCollinear(start_1, end_1, sqr_segment_1_length, start_2, end_2, out hit);
						}
						else
						{
							//     1A------1B
							// 2A------2B
							return SegmentSegmentCollinear(start_2, end_2, sqr_segment_2_length, start_1, end_1, out hit);
						}
					}
					else
					{
						// Contradirected
						Single segment2BProjection = Vector2Df.Dot(direction1, end_2 - start_1);
						if (segment2BProjection > -XGeometry2D.Eplsilon_f)
						{
							// 1A------1B
							//     2B------2A
							return SegmentSegmentCollinear(start_1, end_1, sqr_segment_1_length, end_2, start_2, out hit);
						}
						else
						{
							//     1A------1B
							// 2B------2A
							return SegmentSegmentCollinear(end_2, start_2, sqr_segment_2_length, start_1, end_1, out hit);
						}
					}
				}

				// Not parallel
				Single distance1 = perp_dot_2 / denominator;
				if (distance1 < -XGeometry2D.Eplsilon_f || distance1 > 1 + XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				Single distance2 = perp_dot_1 / denominator;
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
			private static Boolean SegmentSegmentCollinear(Vector2Df left_a, Vector2Df left_b, Single sqr_left_length, 
				Vector2Df right_a, Vector2Df right_b, out TIntersectHit2Df hit)
			{
				Vector2Df left_direction = left_b - left_a;
				Single right_a_projection = Vector2Df.Dot(left_direction, right_a - left_b);
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
					Single right_b_projection = Vector2Df.Dot(left_direction, right_b - left_a);
					if (right_b_projection > sqr_left_length)
					{
						point_b = left_b;
					}
					else
					{
						point_b = right_b;
					}
					hit = TIntersectHit2Df.Segment(right_a, point_b);
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
			public static Boolean SegmentCircle(Segment2Df segment, Circle2Df circle)
			{
				TIntersectHit2Df hit;
				return SegmentCircle(segment.Start, segment.End, circle.Center, circle.Radius, out hit);
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
			public static Boolean SegmentCircle(Segment2Df segment, Circle2Df circle, out TIntersectHit2Df hit)
			{
				return SegmentCircle(segment.Start, segment.End, circle.Center, circle.Radius, out hit);
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
			public static Boolean SegmentCircle(Vector2Df start, Vector2Df end, Vector2Df circle_center, 
				Single circle_radius)
			{
				TIntersectHit2Df hit;
				return SegmentCircle(start, end, circle_center, circle_radius, out hit);
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
			public static Boolean SegmentCircle(Vector2Df start, Vector2Df end, Vector2Df circle_center, 
				Single circle_radius, out TIntersectHit2Df hit)
			{
				Vector2Df segment_start_to_center = circle_center - start;
				Vector2Df from_start_to_end = end - start;
				Single segment_length = from_start_to_end.Length;
				if (segment_length < XGeometry2D.Eplsilon_f)
				{
					Single distance_to_point = segment_start_to_center.Length;
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
				Single center_projection = Vector2Df.Dot(ref segment_direction, ref segment_start_to_center);
				if (center_projection + circle_radius < -XGeometry2D.Eplsilon_f ||
					center_projection - circle_radius > segment_length + XGeometry2D.Eplsilon_f)
				{
					hit = TIntersectHit2Df.None();
					return false;
				}

				Single sqr_distance_to_line = segment_start_to_center.SqrLength - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (circle_radius * circle_radius) - sqr_distance_to_line;
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
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single dist_a = center_projection - distance_to_intersection;
				Single dist_b = center_projection + distance_to_intersection;

				Boolean point_a_is_after_segment_start = dist_a > -XGeometry2D.Eplsilon_f;
				Boolean point_b_is_before_segment_end = dist_b < segment_length + XGeometry2D.Eplsilon_f;

				if (point_a_is_after_segment_start && point_b_is_before_segment_end)
				{
					Vector2Df point_a = start + (segment_direction * dist_a);
					Vector2Df point_b = start + (segment_direction * dist_b);
					hit = TIntersectHit2Df.Segment(point_a, point_b);
					return true;
				}
				if (!point_a_is_after_segment_start && !point_b_is_before_segment_end)
				{
					// The segment is inside, but no hit
					hit = TIntersectHit2Df.None();
					return true;
				}

				Boolean point_a_is_before_segment_end = dist_a < segment_length + XGeometry2D.Eplsilon_f;
				if (point_a_is_after_segment_start && point_a_is_before_segment_end)
				{
					// Point A hit
					hit = TIntersectHit2Df.Point(start + (segment_direction * dist_a));
					return true;
				}
				Boolean point_b_is_after_segment_start = dist_b > -XGeometry2D.Eplsilon_f;
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
			public static Boolean CircleCircle(Circle2Df circle_a, Circle2Df circle_b)
			{
				TIntersectHit2Df hit;
				return CircleCircle(circle_a.Center, circle_a.Radius, circle_b.Center, circle_b.Radius, out hit);
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
			public static Boolean CircleCircle(Circle2Df circle_a, Circle2Df circle_b, out TIntersectHit2Df hit)
			{
				return CircleCircle(circle_a.Center, circle_a.Radius, circle_b.Center, circle_b.Radius, out hit);
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
			public static Boolean CircleCircle(Vector2Df center_a, Single radius_a, Vector2Df center_b, 
				Single radius_b)
			{
				TIntersectHit2Df hit;
				return CircleCircle(center_a, radius_a, center_b, radius_b, out hit);
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
			public static Boolean CircleCircle(Vector2Df center_a, Single radius_a, Vector2Df center_b, 
				Single radius_b, out TIntersectHit2Df hit)
			{
				Vector2Df from_b_to_a = center_a - center_b;
				Single distance_from_b_to_a_sqr = from_b_to_a.SqrLength;
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
				Single distance_from_b_to_a = XMath.Sqrt(distance_from_b_to_a_sqr);

				Single sum_of_radii = radius_a + radius_b;
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

				Single difference_of_radii = radius_a - radius_b;
				Single difference_of_radii_abs = Math.Abs(difference_of_radii);
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
				Single radius_a_sqr = radius_a * radius_a;
				Single distanceToMiddle = (0.5f * (radius_a_sqr - (radius_b * radius_b)) / distance_from_b_to_a_sqr) + 0.5f;
				Vector2Df middle = center_a - (from_b_to_a * distanceToMiddle);

				Single discriminant = (radius_a_sqr / distance_from_b_to_a_sqr) - (distanceToMiddle * distanceToMiddle);
				Vector2Df offset = from_b_to_a.PerpToCCW() * XMath.Sqrt(discriminant);

				hit = TIntersectHit2Df.Segment(middle + offset, middle - offset);
				return true;
			}
			#endregion Circle-Circle
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================