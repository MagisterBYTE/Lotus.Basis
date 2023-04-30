//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 2D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry2DClosest.cs
*		Поиск ближайших точек.
*		Алгоритмы поиска и нахождения ближайших точек пересечения(проекции) основных геометрических тел/примитивов.
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
		/// Статический класс реализующий методы нахождения ближайших точек пересечения(проекции) основных геометрических тел/примитивов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XClosest2D
		{
			#region ======================================= Point - Line ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на линию
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="line">Линия</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointLine(in Vector2Df point, in Line2Df line)
			{
				return PointLine(in point, in line.Position, in line.Direction, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на линию
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="line">Линия</param>
			/// <param name="distance">Расстояние от позиции линии до спроецированной точки</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointLine(in Vector2Df point, in Line2Df line, out Single distance)
			{
				return PointLine(in point, in line.Position, in line.Direction, out distance);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на линию
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointLine(in Vector2Df point, in Vector2Df line_pos, in Vector2Df line_dir)
			{
				return PointLine(in point, in line_pos, in line_dir, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на линию
			/// </summary>
			/// <remarks>
			/// Проекция точки на прямую – это либо сама точка, если она лежит на данной прямой, либо основание перпендикуляра, 
			/// опущенного из этой точки на заданную прямую
			/// </remarks>
			/// <param name="point">Точка</param>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="distance">Расстояние от позиции линии до спроецированной точки</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointLine(in Vector2Df point, in Vector2Df line_pos, in Vector2Df line_dir, out Single distance)
			{
				// In theory, sqrMagnitude should be 1, but in practice this division helps with numerical stability
				distance = Vector2Df.Dot(line_dir, point - line_pos) / line_dir.SqrLength;
				return line_pos + (line_dir * distance);
			}
			#endregion

			#region ======================================= Point - Ray ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на луч
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="ray">Луч</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointRay(in Vector2Df point, in Ray2Df ray)
			{
				return PointRay(in point, in ray.Position, in ray.Direction, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на луч
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="ray">Луч</param>
			/// <param name="distance">Расстояние от позиции луча до спроецированной точки</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointRay(in Vector2Df point, in Ray2Df ray, out Single distance)
			{
				return PointRay(in point, in ray.Position, in ray.Direction, out distance);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на луч
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointRay(in Vector2Df point, in Vector2Df ray_pos, in Vector2Df ray_dir)
			{
				return PointRay(point, ray_pos, ray_dir, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на луч
			/// </summary>
			/// <remarks>
			/// Проекция точки на прямую – это либо сама точка, если она лежит на данной прямой, либо основание перпендикуляра, 
			/// опущенного из этой точки на заданную прямую
			/// </remarks>
			/// <param name="point">Точка</param>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="distance">Расстояние от позиции луча до спроецированной точки</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointRay(in Vector2Df point, in Vector2Df ray_pos, in Vector2Df ray_dir, out Single distance)
			{
				var point_projection = Vector2Df.Dot(in ray_dir, point - ray_pos);
				if (point_projection <= 0)
				{
					// Мы находимся по другую сторону луча
					distance = 0;
					return ray_pos;
				}

				// In theory, sqrMagnitude should be 1, but in practice this division helps with numerical stability
				distance = point_projection / ray_dir.SqrLength;
				return ray_pos + (ray_dir * distance);
			}
			#endregion Point-Ray

			#region ======================================= Point - Segment ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на отрезок
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="segment">Отрезок</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointSegment(in Vector2Df point, in Segment2Df segment)
			{
				return PointSegment(in point, in segment.Start, in segment.End, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на отрезок
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="segment">Отрезок</param>
			/// <param name="normalize_distance">Нормализованная дистанция проецируемой точки от начала отрезка</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointSegment(in Vector2Df point, in Segment2Df segment, out Single normalize_distance)
			{
				return PointSegment(in point, in segment.Start, in segment.End, out normalize_distance);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на отрезок
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end)
			{
				return PointSegment(in point, in start, in end, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на отрезок
			/// </summary>
			/// <remarks>
			/// Проекция точки на прямую – это либо сама точка, если она лежит на данной прямой, либо основание перпендикуляра, 
			/// опущенного из этой точки на заданную прямую
			/// </remarks>
			/// <param name="point">Точка</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="normalize_distance">Нормализованная дистанция проецируемой точки от начала отрезка</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end, out Single normalize_distance)
			{
				Vector2Df segment_direction = end - start;
				var sqr_segment_length = segment_direction.SqrLength;
				if (sqr_segment_length < XGeometry2D.Eplsilon_f)
				{
					// The segment is a point
					normalize_distance = 0;
					return start;
				}

				var point_projection = Vector2Df.Dot(in segment_direction, point - start);
				if (point_projection <= 0)
				{
					normalize_distance = 0;
					return start;
				}
				if (point_projection >= sqr_segment_length)
				{
					normalize_distance = 1;
					return end;
				}

				normalize_distance = point_projection / sqr_segment_length;
				return start + (segment_direction * normalize_distance);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на отрезок
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="segment_direction">Направление отрезка</param>
			/// <param name="segment_length">Длина отрезка</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Vector2Df PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end, 
				in Vector2Df segment_direction, Single segment_length)
			{
				var point_projection = Vector2Df.Dot(in segment_direction, point - start);
				if (point_projection <= 0)
				{
					return start;
				}
				if (point_projection >= segment_length)
				{
					return end;
				}
				return start + (segment_direction * point_projection);
			}
			#endregion

			#region ======================================= Point - Circle ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на окружность
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="circle">Окружность</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointCircle(in Vector2Df point, in Circle2Df circle)
			{
				return PointCircle(in point, in circle.Center, circle.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на окружность
			/// </summary>
			/// <remarks>
			/// Проекция точки на окружность – это либо сама точка, если она лежит на окружности, либо пересечение, 
			/// окружности отрезком от точки до центра окружности
			/// </remarks>
			/// <param name="point">Точка</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df PointCircle(in Vector2Df point, in Vector2Df circle_center, 
				Single circle_radius)
			{
				return circle_center + ((point - circle_center).Normalized * circle_radius);
			}
			#endregion

			#region ======================================= Line - Line ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции линий
			/// </summary>
			/// <param name="line_a">Первая линия</param>
			/// <param name="line_b">Вторая линия</param>
			/// <param name="point_a">Первая точка пересечения</param>
			/// <param name="point_b">Вторая точка пересечения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LineLine(in Line2Df line_a, in Line2Df line_b, out Vector2Df point_a, out Vector2Df point_b)
			{
				LineLine(in line_a.Position, in line_a.Direction, in line_b.Position, in line_b.Direction, out point_a, out point_b);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции линий
			/// </summary>
			/// <param name="pos_a">Позиция первой линии</param>
			/// <param name="dir_a">Направление первой линии</param>
			/// <param name="pos_b">Позиция второй линии</param>
			/// <param name="dir_b">Направление второй линии</param>
			/// <param name="point_a">Первая точка пересечения</param>
			/// <param name="point_b">Вторая точка пересечения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LineLine(in Vector2Df pos_a, in Vector2Df dir_a, in Vector2Df pos_b, 
				in Vector2Df dir_b, out Vector2Df point_a, out Vector2Df point_b)
			{
				Vector2Df pos_b_to_a = pos_a - pos_b;
				var denominator = Vector2Df.DotPerp(in dir_a, in dir_b);
				var perp_dot_b = Vector2Df.DotPerp(in dir_b, in pos_b_to_a);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					if (Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f ||
						Math.Abs(Vector2Df.DotPerp(in dir_a, in pos_b_to_a)) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						point_a = pos_a;
						point_b = pos_b + (dir_b * Vector2Df.Dot(in dir_b, pos_b_to_a));
						return;
					}

					// Collinear
					point_a = point_b = pos_a;
					return;
				}

				// Not parallel
				point_a = point_b = pos_a + (dir_a * (perp_dot_b / denominator));
			}
			#endregion Line-Line

			#region ======================================= Line - Ray ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции линии и луча
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="ray">Луч</param>
			/// <param name="line_point">Точка проекции на линии</param>
			/// <param name="ray_point">Точка проекции на луче</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LineRay(in Line2Df line, in Ray2Df ray, out Vector2Df line_point, out Vector2Df ray_point)
			{
				LineRay(in line.Position, in line.Direction, in ray.Position, in ray.Direction, out line_point, out ray_point);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции линии и луча
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="line_point">Точка проекции на линии</param>
			/// <param name="ray_point">Точка проекции на луче</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LineRay(in Vector2Df line_pos, in Vector2Df line_dir, in Vector2Df ray_pos, in Vector2Df ray_dir,
				out Vector2Df line_point, out Vector2Df ray_point)
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
						var ray_pos_projection = Vector2Df.Dot(in line_dir, in ray_pos_to_line_pos);
						line_point = line_pos - (line_dir * ray_pos_projection);
						ray_point = ray_pos;
						return;
					}
					// Collinear
					line_point = ray_point = ray_pos;
					return;
				}

				// Not parallel
				var ray_distance = perp_dot_a / denominator;
				if (ray_distance < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					var ray_pos_projection = Vector2Df.Dot(in line_dir, in ray_pos_to_line_pos);
					line_point = line_pos - (line_dir * ray_pos_projection);
					ray_point = ray_pos;
					return;
				}

				// Point intersection
				line_point = ray_point = ray_pos + (ray_dir * ray_distance);
			}
			#endregion

			#region ======================================= Line - Segment ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции линии и отрезка
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="segment">Отрезок</param>
			/// <param name="line_point">Точка проекции на линии</param>
			/// <param name="segment_point">Точка проекции на сегменте</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LineSegment(in Line2Df line, in Segment2Df segment, out Vector2Df line_point, 
				out Vector2Df segment_point)
			{
				LineSegment(in line.Position, in line.Direction, in segment.Start, in segment.End, out line_point, out segment_point);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции линии и отрезка
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="line_point">Точка проекции на линии</param>
			/// <param name="segment_point">Точка проекции на сегменте</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LineSegment(in Vector2Df line_pos, in Vector2Df line_dir, in Vector2Df start, in Vector2Df end,
				out Vector2Df line_point, out Vector2Df segment_point)
			{
				Vector2Df segment_direction = end - start;
				Vector2Df segment_start_to_pos = line_pos - start;
				var denominator = Vector2Df.DotPerp(in line_dir, in segment_direction);
				var perp_dot_start = Vector2Df.DotPerp(in line_dir, in segment_start_to_pos);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					var codirected = Vector2Df.Dot(in line_dir, in segment_direction) > 0;

					// Normalized direction gives more stable results 
					var perp_dot_end = Vector2Df.DotPerp(segment_direction.Normalized, segment_start_to_pos);
					if (Math.Abs(perp_dot_start) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_end) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						if (codirected)
						{
							var segment_start_projection = Vector2Df.Dot(in line_dir, in segment_start_to_pos);
							line_point = line_pos - (line_dir * segment_start_projection);
							segment_point = start;
						}
						else
						{
							var segment_end_projection = Vector2Df.Dot(in line_dir, line_pos - end);
							line_point = line_pos - (line_dir * segment_end_projection);
							segment_point = end;
						}
						return;
					}

					// Collinear
					if (codirected)
					{
						line_point = segment_point = start;
					}
					else
					{
						line_point = segment_point = end;
					}
					return;
				}

				// Not parallel
				var segment_distance = perp_dot_start / denominator;
				if (segment_distance < -XGeometry2D.Eplsilon_f || segment_distance > 1 + XGeometry2D.Eplsilon_f)
				{
					// No intersection
					segment_point = start + (segment_direction * XMath.Clamp01(segment_distance));
					var segment_point_projection = Vector2Df.Dot(in line_dir, segment_point - line_pos);
					line_point = line_pos + (line_dir * segment_point_projection);
					return;
				}
				// Point intersection
				line_point = segment_point = start + (segment_direction * segment_distance);
			}
			#endregion

			#region ======================================= Line - Circle =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции линии и окружности
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="circle">Окружность</param>
			/// <param name="line_point">Точка проекции на линии</param>
			/// <param name="circle_point">Точка проекции на окружности</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LineCircle(in Line2Df line, in Circle2Df circle, out Vector2Df line_point, 
				out Vector2Df circle_point)
			{
				LineCircle(in line.Position, in line.Direction, in circle.Center, circle.Radius, out line_point, out circle_point);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции линии и окружности
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <param name="line_point">Точка проекции на линии</param>
			/// <param name="circle_point">Точка проекции на окружности</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LineCircle(in Vector2Df line_pos, in Vector2Df line_dir, in Vector2Df circle_center, 
				Single circle_radius, out Vector2Df line_point, out Vector2Df circle_point)
			{
				Vector2Df pos_to_center = circle_center - line_pos;
				var center_projection = Vector2Df.Dot(in line_dir, in pos_to_center);
				var sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
				var sqr_distance_to_intersection = (circle_radius * circle_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					line_point = line_pos + (line_dir * center_projection);
					circle_point = circle_center + ((line_point - circle_center).Normalized * circle_radius);
					return;
				}
				if (sqr_distance_to_intersection < XGeometry2D.Eplsilon_f)
				{
					// Point intersection
					line_point = circle_point = line_pos + (line_dir * center_projection);
					return;
				}

				// Two points intersection
				var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				var distance_a = center_projection - distance_to_intersection;
				line_point = circle_point = line_pos + (line_dir * distance_a);
			}
			#endregion

			#region ======================================= Ray - Ray =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции лучей
			/// </summary>
			/// <param name="ray_a">Первый луч</param>
			/// <param name="ray_b">Второй луч</param>
			/// <param name="point_a">Точка пересечения на первом луче</param>
			/// <param name="point_b">Точка пересечения на втором луче</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RayRay(in Ray2Df ray_a, in Ray2Df ray_b, out Vector2Df point_a, out Vector2Df point_b)
			{
				RayRay(ray_a.Position, ray_a.Direction, ray_b.Position, ray_b.Direction, out point_a, out point_b);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции лучей
			/// </summary>
			/// <param name="pos_a">Позиция первого луча</param>
			/// <param name="dir_a">Направление первого луча</param>
			/// <param name="pos_b">Позиция второго луча</param>
			/// <param name="dir_b">Направление второго луча</param>
			/// <param name="point_a">Точка пересечения на первом луче</param>
			/// <param name="point_b">Точка пересечения на втором луче</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RayRay(in Vector2Df pos_a, in Vector2Df dir_a, in Vector2Df pos_b, in Vector2Df dir_b,
				out Vector2Df point_a, out Vector2Df point_b)
			{
				Vector2Df pos_b_to_a = pos_a - pos_b;
				var denominator = Vector2Df.DotPerp(in dir_a, in dir_b);
				var perp_dot_a = Vector2Df.DotPerp(in dir_a, in pos_b_to_a);
				var perp_dot_b = Vector2Df.DotPerp(in dir_b, in pos_b_to_a);
				var codirected = Vector2Df.Dot(in dir_a, in dir_b) > 0;

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					var origin_b_projection = Vector2Df.Dot(dir_a, pos_b_to_a);
					if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						if (codirected)
						{
							if (origin_b_projection > -XGeometry2D.Eplsilon_f)
							{
								// Projection of pos_a is on ray_b
								point_a = pos_a;
								point_b = pos_b + (dir_a * origin_b_projection);
								return;
							}
							else
							{
								point_a = pos_a - (dir_a * origin_b_projection);
								point_b = pos_b;
								return;
							}
						}
						else
						{
							if (origin_b_projection > 0)
							{
								point_a = pos_a;
								point_b = pos_b;
								return;
							}
							else
							{
								// Projection of pos_a is on ray_b
								point_a = pos_a;
								point_b = pos_b + (dir_a * origin_b_projection);
								return;
							}
						}
					}
					// Collinear

					if (codirected)
					{
						// Ray intersection
						if (origin_b_projection > -XGeometry2D.Eplsilon_f)
						{
							// Projection of pos_a is on ray_b
							point_a = point_b = pos_a;
							return;
						}
						else
						{
							point_a = point_b = pos_b;
							return;
						}
					}
					else
					{
						if (origin_b_projection > 0)
						{
							// No intersection
							point_a = pos_a;
							point_b = pos_b;
							return;
						}
						else
						{
							// Segment intersection
							point_a = point_b = pos_a;
							return;
						}
					}
				}

				// Not parallel
				var distance_a = perp_dot_b / denominator;
				var distance_b = perp_dot_a / denominator;
				if (distance_a < -XGeometry2D.Eplsilon_f || distance_b < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					if (codirected)
					{
						var originAProjection = Vector2Df.Dot(in dir_b, in pos_b_to_a);
						if (originAProjection > -XGeometry2D.Eplsilon_f)
						{
							point_a = pos_a;
							point_b = pos_b + (dir_b * originAProjection);
							return;
						}
						var originBProjection = -Vector2Df.Dot(in dir_a, in pos_b_to_a);
						if (originBProjection > -XGeometry2D.Eplsilon_f)
						{
							point_a = pos_a + (dir_a * originBProjection);
							point_b = pos_b;
							return;
						}
						point_a = pos_a;
						point_b = pos_b;
						return;
					}
					else
					{
						if (distance_a > -XGeometry2D.Eplsilon_f)
						{
							var originBProjection = -Vector2Df.Dot(in dir_a, in pos_b_to_a);
							if (originBProjection > -XGeometry2D.Eplsilon_f)
							{
								point_a = pos_a + (dir_a * originBProjection);
								point_b = pos_b;
								return;
							}
						}
						else if (distance_b > -XGeometry2D.Eplsilon_f)
						{
							var originAProjection = Vector2Df.Dot(in dir_b, in pos_b_to_a);
							if (originAProjection > -XGeometry2D.Eplsilon_f)
							{
								point_a = pos_a;
								point_b = pos_b + (dir_b * originAProjection);
								return;
							}
						}
						point_a = pos_a;
						point_b = pos_b;
						return;
					}
				}
				// Point intersection
				point_a = point_b = pos_a + (dir_a * distance_a);
			}
			#endregion Ray-Ray

			#region ======================================= Ray - Segment =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции луча и отрезка
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="segment">Отрезок</param>
			/// <param name="ray_point">Точка проекции на луче</param>
			/// <param name="segment_point">Точка проекции на отрезке</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RaySegment(in Ray2Df ray, in Segment2Df segment, out Vector2Df ray_point, out Vector2Df segment_point)
			{
				RaySegment(in ray.Position, in ray.Direction, in segment.Start, in segment.End, out ray_point, out segment_point);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции луча и отрезка
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="ray_point">Точка проекции на луче</param>
			/// <param name="segment_point">Точка проекции на отрезке</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RaySegment(in Vector2Df ray_pos, in Vector2Df ray_dir, in Vector2Df start, in Vector2Df end,
				out Vector2Df ray_point, out Vector2Df segment_point)
			{
				Vector2Df start_copy = start;
				Vector2Df end_copy = end;
				Vector2Df segment_direction = end_copy - start_copy;
				Vector2Df segment_start_to_pos = ray_pos - start_copy;
				var denominator = Vector2Df.DotPerp(in ray_dir, in segment_direction);
				var perp_dot_a = Vector2Df.DotPerp(in ray_dir, in segment_start_to_pos);
				// Normalized direction gives more stable results 
				var perp_dot_b = Vector2Df.DotPerp(segment_direction.Normalized, segment_start_to_pos);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					var segment_start_projection = -Vector2Df.Dot(ray_dir, segment_start_to_pos);
					Vector2Df ray_posToSegmentB = end_copy - ray_pos;
					var segment_end_projection = Vector2Df.Dot(ray_dir, ray_posToSegmentB);
					if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						if (segment_start_projection > -XGeometry2D.Eplsilon_f && segment_end_projection > -XGeometry2D.Eplsilon_f)
						{
							if (segment_start_projection < segment_end_projection)
							{
								ray_point = ray_pos + (ray_dir * segment_start_projection);
								segment_point = start_copy;
								return;
							}
							else
							{
								ray_point = ray_pos + (ray_dir * segment_end_projection);
								segment_point = end_copy;
								return;
							}
						}
						if (segment_start_projection > -XGeometry2D.Eplsilon_f || segment_end_projection > -XGeometry2D.Eplsilon_f)
						{
							ray_point = ray_pos;
							var sqr_segment_length = segment_direction.SqrLength;
							if (sqr_segment_length > XGeometry2D.Eplsilon_f)
							{
								var ray_pos_projection = Vector2Df.Dot(in segment_direction, in segment_start_to_pos) / sqr_segment_length;
								segment_point = start_copy + (segment_direction * ray_pos_projection);
							}
							else
							{
								segment_point = start_copy;
							}
							return;
						}
						ray_point = ray_pos;
						segment_point = segment_start_projection > segment_end_projection ? start_copy : end_copy;
						return;
					}

					// Collinear
					if (segment_start_projection > -XGeometry2D.Eplsilon_f && segment_end_projection > -XGeometry2D.Eplsilon_f)
					{
						// Segment intersection
						ray_point = segment_point = segment_start_projection < segment_end_projection ? start_copy : end_copy;
						return;
					}
					if (segment_start_projection > -XGeometry2D.Eplsilon_f || segment_end_projection > -XGeometry2D.Eplsilon_f)
					{
						// Point or segment intersection
						ray_point = segment_point = ray_pos;
						return;
					}
					// No intersection
					ray_point = ray_pos;
					segment_point = segment_start_projection > segment_end_projection ? start_copy : end_copy;
					return;
				}

				// Not parallel
				var ray_distance = perp_dot_b / denominator;
				var segment_distance = perp_dot_a / denominator;
				if (ray_distance < -XGeometry2D.Eplsilon_f ||
					segment_distance < -XGeometry2D.Eplsilon_f || segment_distance > 1 + XGeometry2D.Eplsilon_f)
				{
					// No intersection
					var codirected = Vector2Df.Dot(in ray_dir, in segment_direction) > 0;
					Vector2Df segment_end_to_pos;
					if (!codirected)
					{
						XMath.Swap(ref start_copy, ref end_copy);
						segment_direction = -segment_direction;
						segment_end_to_pos = segment_start_to_pos;
						segment_start_to_pos = ray_pos - start_copy;
						segment_distance = 1 - segment_distance;
					}
					else
					{
						segment_end_to_pos = ray_pos - end_copy;
					}

					var segment_start_projection = -Vector2Df.Dot(in ray_dir, in segment_start_to_pos);
					var segment_end_projection = -Vector2Df.Dot(in ray_dir, in segment_end_to_pos);
					var segment_start_on_ray = segment_start_projection > -XGeometry2D.Eplsilon_f;
					var segment_end_on_ray = segment_end_projection > -XGeometry2D.Eplsilon_f;
					if (segment_start_on_ray && segment_end_on_ray)
					{
						if (segment_distance < 0)
						{
							ray_point = ray_pos + (ray_dir * segment_start_projection);
							segment_point = start_copy;
							return;
						}
						else
						{
							ray_point = ray_pos + (ray_dir * segment_end_projection);
							segment_point = end_copy;
							return;
						}
					}
					else if (!segment_start_on_ray && segment_end_on_ray)
					{
						if (segment_distance < 0)
						{
							ray_point = ray_pos;
							segment_point = start_copy;
							return;
						}
						else if (segment_distance > 1 + XGeometry2D.Eplsilon_f)
						{
							ray_point = ray_pos + (ray_dir * segment_end_projection);
							segment_point = end_copy;
							return;
						}
						else
						{
							ray_point = ray_pos;
							var pos_projection = Vector2Df.Dot(in segment_direction, in segment_start_to_pos);
							segment_point = start_copy + (segment_direction * pos_projection / segment_direction.SqrLength);
							return;
						}
					}
					else
					{
						// Not on ray
						ray_point = ray_pos;
						var pos_projection = Vector2Df.Dot(in segment_direction, in segment_start_to_pos);
						var sqr_segment_length = segment_direction.SqrLength;
						if (pos_projection < 0)
						{
							segment_point = start_copy;
							return;
						}
						else if (pos_projection > sqr_segment_length)
						{
							segment_point = end_copy;
							return;
						}
						else
						{
							segment_point = start_copy + (segment_direction * pos_projection / sqr_segment_length);
							return;
						}
					}
				}
				// Point intersection
				ray_point = segment_point = start_copy + (segment_direction * segment_distance);
			}
			#endregion

			#region ======================================= Ray - Circle ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции луча и окружности
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="circle">Окружность</param>
			/// <param name="ray_point">Точка проекции на луче</param>
			/// <param name="circle_point">Точка проекции на окружности</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RayCircle(in Ray2Df ray, in Circle2Df circle, out Vector2Df ray_point, out Vector2Df circle_point)
			{
				RayCircle(in ray.Position, in ray.Direction, in circle.Center, circle.Radius, out ray_point, out circle_point);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции луча и окружности
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <param name="ray_point">Точка проекции на луче</param>
			/// <param name="circle_point">Точка проекции на окружности</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RayCircle(in Vector2Df ray_pos, in Vector2Df ray_dir, in Vector2Df circle_center, Single circle_radius,
				out Vector2Df ray_point, out Vector2Df circle_point)
			{
				Vector2Df pos_to_center = circle_center - ray_pos;
				var center_projection = Vector2Df.Dot(in ray_dir, in pos_to_center);
				if (center_projection + circle_radius < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					ray_point = ray_pos;
					circle_point = circle_center - (pos_to_center.Normalized * circle_radius);
					return;
				}

				var sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
				var sqr_distance_to_intersection = (circle_radius * circle_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < -XGeometry2D.Eplsilon_f)
					{
						ray_point = ray_pos;
						circle_point = circle_center - (pos_to_center.Normalized * circle_radius);
						return;
					}
					ray_point = ray_pos + (ray_dir * center_projection);
					circle_point = circle_center + ((ray_point - circle_center).Normalized * circle_radius);
					return;
				}
				if (sqr_distance_to_intersection < XGeometry2D.Eplsilon_f)
				{
					if (center_projection < -XGeometry2D.Eplsilon_f)
					{
						// No intersection
						ray_point = ray_pos;
						circle_point = circle_center - (pos_to_center.Normalized * circle_radius);
						return;
					}
					// Point intersection
					ray_point = circle_point = ray_pos + (ray_dir * center_projection);
					return;
				}

				// Line intersection
				var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				var distance_a = center_projection - distance_to_intersection;

				if (distance_a < -XGeometry2D.Eplsilon_f)
				{
					var distance_b = center_projection + distance_to_intersection;
					if (distance_b < -XGeometry2D.Eplsilon_f)
					{
						// No intersection
						ray_point = ray_pos;
						circle_point = circle_center - (pos_to_center.Normalized * circle_radius);
						return;
					}

					// Point intersection
					ray_point = circle_point = ray_pos + (ray_dir * distance_b);
					return;
				}

				// Two points intersection
				ray_point = circle_point = ray_pos + (ray_dir * distance_a);
			}
			#endregion Ray-Circle2Df

			#region ======================================= Segment - Segment =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции двух отрезков
			/// </summary>
			/// <param name="segment1">Первый отрезок</param>
			/// <param name="segment2">Второй отрезок</param>
			/// <param name="segment1_point">Точка проекции на первом отрезке</param>
			/// <param name="segment2_point">Точка проекции на втором отрезке</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SegmentSegment(in Segment2Df segment1, in Segment2Df segment2, out Vector2Df segment1_point, 
				out Vector2Df segment2_point)
			{
				SegmentSegment(in segment1.Start, in segment1.End, in segment2.Start, in segment2.End, out segment1_point, out segment2_point);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции двух отрезков
			/// </summary>
			/// <param name="segment1_start">Начало первого отрезка</param>
			/// <param name="segment1_end">Окончание первого отрезка</param>
			/// <param name="segment2_start">Начало второго отрезка</param>
			/// <param name="segment2_end">Окончание второго отрезка</param>
			/// <param name="segment1_point">Точка проекции на первом отрезке</param>
			/// <param name="segment2_point">Точка проекции на втором отрезке</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SegmentSegment(in Vector2Df segment1_start, in Vector2Df segment1_end, 
				in Vector2Df segment2_start, in Vector2Df segment2_end, out Vector2Df segment1_point, 
				out Vector2Df segment2_point)
			{
				Vector2Df segment2_start_copy = segment2_start;
				Vector2Df segment2_end_copy = segment2_end;
				Vector2Df from_2start_to_1start = segment1_start - segment2_start_copy;
				Vector2Df direction1 = segment1_end - segment1_start;
				Vector2Df direction2 = segment2_end_copy - segment2_start_copy;
				var segment_1length = direction1.Length;
				var segment_2length = direction2.Length;
				
				var segment1IsAPoint = segment_1length < XGeometry2D.Eplsilon_f;
				var segment2IsAPoint = segment_2length < XGeometry2D.Eplsilon_f;
				if (segment1IsAPoint && segment2IsAPoint)
				{
					if (segment1_start == segment2_start_copy)
					{
						segment1_point = segment2_point = segment1_start;
						return;
					}
					segment1_point = segment1_start;
					segment2_point = segment2_start_copy;
					return;
				}
				if (segment1IsAPoint)
				{
					direction2.Normalize();
					segment1_point = segment1_start;
					segment2_point = PointSegment(in segment1_start, in segment2_start_copy, in segment2_end_copy, in direction2, segment_2length);
					return;
				}
				if (segment2IsAPoint)
				{
					direction1.Normalize();
					segment1_point = PointSegment(in segment2_start_copy, in segment1_start, in segment1_end, in direction1, segment_1length);
					segment2_point = segment2_start_copy;
					return;
				}

				direction1.Normalize();
				direction2.Normalize();
				var denominator = Vector2Df.DotPerp(in direction1, in direction2);
				var perpDot1 = Vector2Df.DotPerp(in direction1, in from_2start_to_1start);
				var perpDot2 = Vector2Df.DotPerp(in direction2, in from_2start_to_1start);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					var codirected = Vector2Df.Dot(direction1, direction2) > 0;
					if (Math.Abs(perpDot1) > XGeometry2D.Eplsilon_f || Math.Abs(perpDot2) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						Vector2Df from1ATo2B;
						if (!codirected)
						{
							XMath.Swap(ref segment2_start_copy, ref segment2_end_copy);
							direction2 = -direction2;
							from1ATo2B = -from_2start_to_1start;
							from_2start_to_1start = segment1_start - segment2_start_copy;
						}
						else
						{
							from1ATo2B = segment2_end_copy - segment1_start;
						}
						var segment2AProjection = -Vector2Df.Dot(direction1, from_2start_to_1start);
						var segment2BProjection = Vector2Df.Dot(direction1, from1ATo2B);

						var segment2AIsAfter1A = segment2AProjection > -XGeometry2D.Eplsilon_f;
						var segment2BIsAfter1A = segment2BProjection > -XGeometry2D.Eplsilon_f;
						if (!segment2AIsAfter1A && !segment2BIsAfter1A)
						{
							//           1A------1B
							// 2A------2B
							segment1_point = segment1_start;
							segment2_point = segment2_end_copy;
							return;
						}
						var segment2AIsBefore1B = segment2AProjection < segment_1length + XGeometry2D.Eplsilon_f;
						var segment2BIsBefore1B = segment2BProjection < segment_1length + XGeometry2D.Eplsilon_f;
						if (!segment2AIsBefore1B && !segment2BIsBefore1B)
						{
							// 1A------1B
							//           2A------2B
							segment1_point = segment1_end;
							segment2_point = segment2_start_copy;
							return;
						}

						if (segment2AIsAfter1A && segment2BIsBefore1B)
						{
							// 1A------1B
							//   2A--2B
							segment1_point = segment1_start + (direction1 * segment2AProjection);
							segment2_point = segment2_start_copy;
							return;
						}

						if (segment2AIsAfter1A) // && segment2AIsBefore1B && !segment2BIsBefore1B)
						{
							// 1A------1B
							//     2A------2B
							segment1_point = segment1_start + (direction1 * segment2AProjection);
							segment2_point = segment2_start_copy;
							return;
						}
						else
						{
							//   1A------1B
							// 2A----2B
							// 2A----------2B
							segment1_point = segment1_start;
							var segment1AProjection = Vector2Df.Dot(in direction2, in from_2start_to_1start);
							segment2_point = segment2_start_copy + (direction2 * segment1AProjection);
							return;
						}
					}
					// Collinear

					if (codirected)
					{
						// Codirected
						var segment2AProjection = -Vector2Df.Dot(in direction1, in from_2start_to_1start);
						if (segment2AProjection > -XGeometry2D.Eplsilon_f)
						{
							// 1A------1B
							//     2A------2B
							SegmentSegmentCollinear(in segment1_start, in segment1_end, in segment2_start_copy, out segment1_point, out segment2_point);
							return;
						}
						else
						{
							//     1A------1B
							// 2A------2B
							SegmentSegmentCollinear(in segment2_start_copy, in segment2_end_copy, in segment1_start, out segment2_point, out segment1_point);
							return;
						}
					}
					else
					{
						// Contradirected
						var segment2BProjection = Vector2Df.Dot(in direction1, segment2_end_copy - segment1_start);
						if (segment2BProjection > -XGeometry2D.Eplsilon_f)
						{
							// 1A------1B
							//     2B------2A
							SegmentSegmentCollinear(in segment1_start, in segment1_end, in segment2_end_copy, out segment1_point, out segment2_point);
							return;
						}
						else
						{
							//     1A------1B
							// 2B------2A
							SegmentSegmentCollinear(in segment2_end_copy, in segment2_start_copy, in segment1_start, out segment2_point, out segment1_point);
							return;
						}
					}
				}

				// Not parallel
				var distance1 = perpDot2 / denominator;
				var distance2 = perpDot1 / denominator;
				if (distance1 < -XGeometry2D.Eplsilon_f || distance1 > segment_1length + XGeometry2D.Eplsilon_f ||
					distance2 < -XGeometry2D.Eplsilon_f || distance2 > segment_2length + XGeometry2D.Eplsilon_f)
				{
					// No intersection
					var codirected = Vector2Df.Dot(in direction1, in direction2) > 0;
					Vector2Df from1ATo2B;
					if (!codirected)
					{
						XMath.Swap(ref segment2_start_copy, ref segment2_end_copy);
						direction2 = -direction2;
						from1ATo2B = -from_2start_to_1start;
						from_2start_to_1start = segment1_start - segment2_start_copy;
						distance2 = segment_2length - distance2;
					}
					else
					{
						from1ATo2B = segment2_end_copy - segment1_start;
					}

					var segment2AProjection = -Vector2Df.Dot(direction1, from_2start_to_1start);
					var segment2BProjection = Vector2Df.Dot(direction1, from1ATo2B);

					var segment2AIsAfter1A = segment2AProjection > -XGeometry2D.Eplsilon_f;
					var segment2BIsBefore1B = segment2BProjection < segment_1length + XGeometry2D.Eplsilon_f;
					var segment2AOnSegment1 = segment2AIsAfter1A && segment2AProjection < segment_1length + XGeometry2D.Eplsilon_f;
					var segment2BOnSegment1 = segment2BProjection > -XGeometry2D.Eplsilon_f && segment2BIsBefore1B;
					if (segment2AOnSegment1 && segment2BOnSegment1)
					{
						if (distance2 < -XGeometry2D.Eplsilon_f)
						{
							segment1_point = segment1_start + (direction1 * segment2AProjection);
							segment2_point = segment2_start_copy;
						}
						else
						{
							segment1_point = segment1_start + (direction1 * segment2BProjection);
							segment2_point = segment2_end_copy;
						}
					}
					else if (!segment2AOnSegment1 && !segment2BOnSegment1)
					{
						if (!segment2AIsAfter1A && !segment2BIsBefore1B)
						{
							segment1_point = distance1 < -XGeometry2D.Eplsilon_f ? segment1_start : segment1_end;
						}
						else
						{
							// Not on segment
							segment1_point = segment2AIsAfter1A ? segment1_end : segment1_start;
						}
						var segment1PointProjection = Vector2Df.Dot(in direction2, segment1_point - segment2_start_copy);
						segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment_2length);
						segment2_point = segment2_start_copy + (direction2 * segment1PointProjection);
					}
					else if (segment2AOnSegment1)
					{
						if (distance2 < -XGeometry2D.Eplsilon_f)
						{
							segment1_point = segment1_start + (direction1 * segment2AProjection);
							segment2_point = segment2_start_copy;
						}
						else
						{
							segment1_point = segment1_end;
							var segment1PointProjection = Vector2Df.Dot(in direction2, segment1_point - segment2_start_copy);
							segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment_2length);
							segment2_point = segment2_start_copy + (direction2 * segment1PointProjection);
						}
					}
					else
					{
						if (distance2 > segment_2length + XGeometry2D.Eplsilon_f)
						{
							segment1_point = segment1_start + (direction1 * segment2BProjection);
							segment2_point = segment2_end_copy;
						}
						else
						{
							segment1_point = segment1_start;
							var segment1PointProjection = Vector2Df.Dot(in direction2, segment1_point - segment2_start_copy);
							segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment_2length);
							segment2_point = segment2_start_copy + (direction2 * segment1PointProjection);
						}
					}
					return;
				}

				// Point intersection
				segment1_point = segment2_point = segment1_start + (direction1 * distance1);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции двух отрезков
			/// </summary>
			/// <param name="left_a">Начало отрезка слева</param>
			/// <param name="left_b">Конец отрезка слева</param>
			/// <param name="right_a">Начало отрезка справа</param>
			/// <param name="left_point">Точка проекции на отрезке слева</param>
			/// <param name="right_point">Точка проекции на отрезке справа</param>
			//---------------------------------------------------------------------------------------------------------
			private static void SegmentSegmentCollinear(in Vector2Df left_a, in Vector2Df left_b, in Vector2Df right_a,
				out Vector2Df left_point, out Vector2Df right_point)
			{
				Vector2Df left_direction = left_b - left_a;
				var rightAProjection = Vector2Df.Dot(left_direction.Normalized, right_a - left_b);
				if (Math.Abs(rightAProjection) < XGeometry2D.Eplsilon_f)
				{
					// LB == RA
					// LA------LB
					//         RA------RB

					// Point intersection
					left_point = right_point = left_b;
					return;
				}
				if (rightAProjection < 0)
				{
					// LB > RA
					// LA------LB
					//     RARB
					//     RA--RB
					//     RA------RB

					// Segment intersection
					left_point = right_point = right_a;
					return;
				}
				// LB < RA
				// LA------LB
				//             RA------RB

				// No intersection
				left_point = left_b;
				right_point = right_a;
			}
			#endregion Segment-Segment

			#region ======================================= Segment - Circle ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции отрезков и окружности
			/// </summary>
			/// <param name="segment">Отрезок</param>
			/// <param name="circle">Окружность</param>
			/// <param name="segment_point">Точка проекции на отрезки</param>
			/// <param name="circle_point">Точка проекции на окружности</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SegmentCircle(in Segment2Df segment, in Circle2Df circle, out Vector2Df segment_point, out Vector2Df circle_point)
			{
				SegmentCircle(in segment.Start, in segment.End, in circle.Center, circle.Radius, out segment_point, out circle_point);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции отрезков и окружности
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <param name="segment_point">Точка проекции на отрезки</param>
			/// <param name="circle_point">Точка проекции на окружности</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SegmentCircle(in Vector2Df start, in Vector2Df end, in Vector2Df circle_center, Single circle_radius,
				out Vector2Df segment_point, out Vector2Df circle_point)
			{
				Vector2Df segment_start_to_center = circle_center - start;
				Vector2Df from_start_to_end = end - start;
				var segment_length = from_start_to_end.Length;
				if (segment_length < XGeometry2D.Eplsilon_f)
				{
					segment_point = start;
					var distance_to_point = segment_start_to_center.Length;
					if (distance_to_point < circle_radius + XGeometry2D.Eplsilon_f)
					{
						if (distance_to_point > circle_radius - XGeometry2D.Eplsilon_f)
						{
							circle_point = segment_point;
							return;
						}
						if (distance_to_point < XGeometry2D.Eplsilon_f)
						{
							circle_point = segment_point;
							return;
						}
					}
					Vector2Df to_point = -segment_start_to_center / distance_to_point;
					circle_point = circle_center + (to_point * circle_radius);
					return;
				}

				Vector2Df segment_direction = from_start_to_end.Normalized;
				var center_projection = Vector2Df.Dot(in segment_direction, in segment_start_to_center);
				if (center_projection + circle_radius < -XGeometry2D.Eplsilon_f ||
					center_projection - circle_radius > segment_length + XGeometry2D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < 0)
					{
						segment_point = start;
						circle_point = circle_center - (segment_start_to_center.Normalized * circle_radius);
						return;
					}
					segment_point = end;
					circle_point = circle_center - ((circle_center - end).Normalized * circle_radius);
					return;
				}

				var sqr_distance_to_line = segment_start_to_center.SqrLength - (center_projection * center_projection);
				var sqr_distance_to_intersection = (circle_radius * circle_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < -XGeometry2D.Eplsilon_f)
					{
						segment_point = start;
						circle_point = circle_center - (segment_start_to_center.Normalized * circle_radius);
						return;
					}
					if (center_projection > segment_length + XGeometry2D.Eplsilon_f)
					{
						segment_point = end;
						circle_point = circle_center - ((circle_center - end).Normalized * circle_radius);
						return;
					}
					segment_point = start + (segment_direction * center_projection);
					circle_point = circle_center + ((segment_point - circle_center).Normalized * circle_radius);
					return;
				}

				if (sqr_distance_to_intersection < XGeometry2D.Eplsilon_f)
				{
					if (center_projection < -XGeometry2D.Eplsilon_f)
					{
						// No intersection
						segment_point = start;
						circle_point = circle_center - (segment_start_to_center.Normalized * circle_radius);
						return;
					}
					if (center_projection > segment_length + XGeometry2D.Eplsilon_f)
					{
						// No intersection
						segment_point = end;
						circle_point = circle_center - ((circle_center - end).Normalized * circle_radius);
						return;
					}
					// Point intersection
					segment_point = circle_point = start + (segment_direction * center_projection);
					return;
				}

				// Line intersection
				var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				var distance_a = center_projection - distance_to_intersection;
				var distance_b = center_projection + distance_to_intersection;

				var point_a_is_after_segment_start = distance_a > -XGeometry2D.Eplsilon_f;
				var point_b_is_before_segment_end = distance_b < segment_length + XGeometry2D.Eplsilon_f;

				if (point_a_is_after_segment_start && point_b_is_before_segment_end)
				{
					segment_point = circle_point = start + (segment_direction * distance_a);
					return;
				}
				if (!point_a_is_after_segment_start && !point_b_is_before_segment_end)
				{
					// The segment is inside, but no intersection
					if (distance_a > -(distance_b - segment_length))
					{
						segment_point = start;
						circle_point = start + (segment_direction * distance_a);
						return;
					}
					segment_point = end;
					circle_point = start + (segment_direction * distance_b);
					return;
				}

				var point_a_is_before_segment_end = distance_a < segment_length + XGeometry2D.Eplsilon_f;
				if (point_a_is_after_segment_start && point_a_is_before_segment_end)
				{
					// Point A intersection
					segment_point = circle_point = start + (segment_direction * distance_a);
					return;
				}
				var point_b_is_after_segment_start = distance_b > -XGeometry2D.Eplsilon_f;
				if (point_b_is_after_segment_start && point_b_is_before_segment_end)
				{
					// Point B intersection
					segment_point = circle_point = start + (segment_direction * distance_b);
					return;
				}

				// No intersection
				if (center_projection < 0)
				{
					segment_point = start;
					circle_point = circle_center - (segment_start_to_center.Normalized * circle_radius);
					return;
				}
				segment_point = end;
				circle_point = circle_center - ((circle_center - end).Normalized * circle_radius);
			}
			#endregion

			#region ======================================= Circle - Circle ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции двух окружностей
			/// </summary>
			/// <param name="circle_a">Первая окружность</param>
			/// <param name="circle_b">Вторая окружность</param>
			/// <param name="point_a">Точка проекции на первую окружность</param>
			/// <param name="point_b">Точка проекции на вторую окружность</param>
			//---------------------------------------------------------------------------------------------------------
			public static void CircleCircle(in Circle2Df circle_a, in Circle2Df circle_b, out Vector2Df point_a, out Vector2Df point_b)
			{
				CircleCircle(circle_a.Center, circle_a.Radius, circle_b.Center, circle_b.Radius, out point_a, out point_b);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции двух окружностей
			/// </summary>
			/// <param name="center_a">Центр первой окружности</param>
			/// <param name="radius_a">Радиус первой окружности</param>
			/// <param name="center_b">Центр второй окружности</param>
			/// <param name="radius_b">Радиус второй окружности</param>
			/// <param name="point_a">Точка проекции на первую окружность</param>
			/// <param name="point_b">Точка проекции на вторую окружность</param>
			//---------------------------------------------------------------------------------------------------------
			public static void CircleCircle(in Vector2Df center_a, Single radius_a, in Vector2Df center_b, Single radius_b,
				out Vector2Df point_a, out Vector2Df point_b)
			{
				Vector2Df from_b_to_a = (center_a - center_b).Normalized;
				point_a = center_a - (from_b_to_a * radius_a);
				point_b = center_b + (from_b_to_a * radius_b);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================