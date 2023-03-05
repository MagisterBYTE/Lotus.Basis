//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 3D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry3DClosest.cs
*		Поиск ближайших точек.
*		Алгоритмы поиска и нахождения ближайших точек пересечения основных геометрических тел/примитивов друг с другом.
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
		//! \addtogroup MathGeometry3D
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы нахождения ближайших точек пересечения основных геометрических тел/примитивов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XClosest3D
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
			public static Vector3Df PointLine(in Vector3Df point, in Line3Df line)
			{
				return PointLine(in point, in line.Position, in line.Direction, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на линию
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="line">Линия</param>
			/// <param name="distance">Расстояние от начала линии до спроецированной точки</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df PointLine(in Vector3Df point, in Line3Df line, out Single distance)
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
			public static Vector3Df PointLine(in Vector3Df point, in Vector3Df line_pos, in Vector3Df line_dir)
			{
				return PointLine(in point, in line_pos, in line_dir, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на линию
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="distance">Расстояние от начала линии до спроецированной точки</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df PointLine(in Vector3Df point, in Vector3Df line_pos, in Vector3Df line_dir, out Single distance)
			{
				// In theory, SqrLength should be 1, but in practice this division helps with numerical stability
				distance = Vector3Df.Dot(line_dir, point - line_pos) / line_dir.SqrLength;
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
			public static Vector3Df PointRay(in Vector3Df point, in Ray3Df ray)
			{
				return PointRay(in point, in ray.Position, in ray.Direction, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на луч
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="ray">Луч</param>
			/// <param name="distance">Расстояние от начала луча до спроецированной точки</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df PointRay(in Vector3Df point, in Ray3Df ray, out Single distance)
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
			public static Vector3Df PointRay(in Vector3Df point, in Vector3Df ray_pos, in Vector3Df ray_dir)
			{
				return PointRay(in point, in ray_pos, in ray_dir, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на луч
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="distance">Расстояние от начала луча до спроецированной точки</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df PointRay(in Vector3Df point, in Vector3Df ray_pos, in Vector3Df ray_dir, out Single distance)
			{
				Vector3Df to_point = point - ray_pos;
				Single point_projection = Vector3Df.Dot(in ray_dir, in to_point);
				if (point_projection <= 0)
				{
					distance = 0;
					return ray_pos;
				}

				// In theory, SqrLength should be 1, but in practice this division helps with numerical stability
				distance = point_projection / ray_dir.SqrLength;
				return ray_pos + (ray_dir * distance);
			}
			#endregion

			#region ======================================= Point - Segment ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на отрезок
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="segment">Отрезок</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df PointSegment(in Vector3Df point, in Segment3Df segment)
			{
				return PointSegment(in point, in segment.Start, in segment.End, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на отрезок
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="segment">Отрезок</param>
			/// <param name="normalize_distance">Нормализованная позиция проецируемой точки от начала отрезка</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df PointSegment(in Vector3Df point, in Segment3Df segment, out Single normalize_distance)
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
			public static Vector3Df PointSegment(in Vector3Df point, in Vector3Df start, in Vector3Df end)
			{
				return PointSegment(in point, in start, in end, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на отрезок
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="normalize_distance">Нормализованная позиция проецируемой точки от начала отрезка</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df PointSegment(in Vector3Df point, in Vector3Df start, in Vector3Df end, 
				out Single normalize_distance)
			{
				Vector3Df segment_direction = end - start;
				Single sqr_segment_length = segment_direction.SqrLength;
				if (sqr_segment_length < XGeometry3D.Eplsilon_f)
				{
					// The segment is a point
					normalize_distance = 0;
					return start;
				}

				Single point_projection = Vector3Df.Dot(in segment_direction, point - start);
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
			#endregion

			#region ======================================= Point - Sphere ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на сферу
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="sphere">Сфера</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df PointSphere(in Vector3Df point, in Sphere3Df sphere)
			{
				return PointSphere(in point, in sphere.Center, sphere.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проекция точки на сферу
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <returns>Спроецированная точка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df PointSphere(in Vector3Df point, in Vector3Df sphere_center, Single sphere_radius)
			{
				return sphere_center + ((point - sphere_center).Normalized * sphere_radius);
			}
			#endregion

			#region ======================================= Line - Sphere =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции линии и сферы
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="sphere">Сфера</param>
			/// <param name="line_point">Точка проекции на линии</param>
			/// <param name="sphere_point">Точка проекции на сфере</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LineSphere(in Line3Df line, in Sphere3Df sphere, out Vector3Df line_point, out Vector3Df sphere_point)
			{
				LineSphere(line.Position, line.Direction, sphere.Center, sphere.Radius, out line_point, out sphere_point);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции линии и сферы
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <param name="line_point">Точка проекции на линии</param>
			/// <param name="sphere_point">Точка проекции на сфере</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LineSphere(in Vector3Df line_pos, in Vector3Df line_dir, in Vector3Df sphere_center, Single sphere_radius,
				out Vector3Df line_point, out Vector3Df sphere_point)
			{
				Vector3Df pos_to_center = sphere_center - line_pos;
				Single center_projection = Vector3Df.Dot(in line_dir, in pos_to_center);
				Single sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
				{
					// No intersection
					line_point = line_pos + (line_dir * center_projection);
					sphere_point = sphere_center + ((line_point - sphere_center).Normalized * sphere_radius);
					return;
				}
				if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
				{
					// Point intersection
					line_point = sphere_point = line_pos + (line_dir * center_projection);
					return;
				}

				// Two points intersection
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single distance_a = center_projection - distance_to_intersection;
				line_point = sphere_point = line_pos + (line_dir * distance_a);
			}
			#endregion

			#region ======================================= Ray - Sphere ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции луча и сферы
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="sphere">Сфера</param>
			/// <param name="ray_point">Точка проекции на луче</param>
			/// <param name="sphere_point">Точка проекции на сферы</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RaySphere(in Ray3Df ray, in Sphere3Df sphere, out Vector3Df ray_point, out Vector3Df sphere_point)
			{
				RaySphere(in ray.Position, in ray.Direction, in sphere.Center, sphere.Radius, out ray_point, out sphere_point);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции луча и сферы
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <param name="ray_point">Точка проекции на луче</param>
			/// <param name="sphere_point">Точка проекции на сферы</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RaySphere(in Vector3Df ray_pos, in Vector3Df ray_dir, in Vector3Df sphere_center, Single sphere_radius,
				out Vector3Df ray_point, out Vector3Df sphere_point)
			{
				Vector3Df pos_to_center = sphere_center - ray_pos;
				Single center_projection = Vector3Df.Dot(ray_dir, pos_to_center);
				if (center_projection + sphere_radius < -XGeometry3D.Eplsilon_f)
				{
					// No intersection
					ray_point = ray_pos;
					sphere_point = sphere_center - (pos_to_center.Normalized * sphere_radius);
					return;
				}

				Single sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < -XGeometry3D.Eplsilon_f)
					{
						ray_point = ray_pos;
						sphere_point = sphere_center - (pos_to_center.Normalized * sphere_radius);
						return;
					}
					ray_point = ray_pos + (ray_dir * center_projection);
					sphere_point = sphere_center + ((ray_point - sphere_center).Normalized * sphere_radius);
					return;
				}
				if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
				{
					if (center_projection < -XGeometry3D.Eplsilon_f)
					{
						// No intersection
						ray_point = ray_pos;
						sphere_point = sphere_center - (pos_to_center.Normalized * sphere_radius);
						return;
					}
					// Point intersection
					ray_point = sphere_point = ray_pos + (ray_dir * center_projection);
					return;
				}

				// Line intersection
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single distance_a = center_projection - distance_to_intersection;

				if (distance_a < -XGeometry3D.Eplsilon_f)
				{
					Single distance_b = center_projection + distance_to_intersection;
					if (distance_b < -XGeometry3D.Eplsilon_f)
					{
						// No intersection
						ray_point = ray_pos;
						sphere_point = sphere_center - (pos_to_center.Normalized * sphere_radius);
						return;
					}

					// Point intersection
					ray_point = sphere_point = ray_pos + (ray_dir * distance_b);
					return;
				}

				// Two points intersection
				ray_point = sphere_point = ray_pos + (ray_dir * distance_a);
			}
			#endregion

			#region ======================================= Segment - Sphere ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции отрезков и сферы
			/// </summary>
			/// <param name="segment">Отрезок</param>
			/// <param name="sphere">Сфера</param>
			/// <param name="segment_point">Точка проекции на отрезки</param>
			/// <param name="sphere_point">Точка проекции на сферы</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SegmentSphere(in Segment3Df segment, in Sphere3Df sphere, out Vector3Df segment_point, out Vector3Df sphere_point)
			{
				SegmentSphere(in segment.Start, in segment.End, in sphere.Center, sphere.Radius, out segment_point, out sphere_point);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции отрезков и сферы
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <param name="segment_point">Точка проекции на отрезки</param>
			/// <param name="sphere_point">Точка проекции на сферы</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SegmentSphere(in Vector3Df start, in Vector3Df end, in Vector3Df sphere_center, Single sphere_radius,
				out Vector3Df segment_point, out Vector3Df sphere_point)
			{
				Vector3Df segment_start_to_center = sphere_center - start;
				Vector3Df from_start_to_end = end - start;
				Single segment_length = from_start_to_end.Length;
				if (segment_length < XGeometry3D.Eplsilon_f)
				{
					segment_point = start;
					Single distanceToPoint = segment_start_to_center.Length;
					if (distanceToPoint < sphere_radius + XGeometry3D.Eplsilon_f)
					{
						if (distanceToPoint > sphere_radius - XGeometry3D.Eplsilon_f)
						{
							sphere_point = segment_point;
							return;
						}
						if (distanceToPoint < XGeometry3D.Eplsilon_f)
						{
							sphere_point = segment_point;
							return;
						}
					}
					Vector3Df to_point = -segment_start_to_center / distanceToPoint;
					sphere_point = sphere_center + (to_point * sphere_radius);
					return;
				}

				Vector3Df segment_direction = from_start_to_end.Normalized;
				Single center_projection = Vector3Df.Dot(in segment_direction, in segment_start_to_center);
				if (center_projection + sphere_radius < -XGeometry3D.Eplsilon_f ||
					center_projection - sphere_radius > segment_length + XGeometry3D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < 0)
					{
						segment_point = start;
						sphere_point = sphere_center - (segment_start_to_center.Normalized * sphere_radius);
						return;
					}
					segment_point = end;
					sphere_point = sphere_center - ((sphere_center - end).Normalized * sphere_radius);
					return;
				}

				Single sqr_distance_to_line = segment_start_to_center.SqrLength - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < -XGeometry3D.Eplsilon_f)
					{
						segment_point = start;
						sphere_point = sphere_center - (segment_start_to_center.Normalized * sphere_radius);
						return;
					}
					if (center_projection > segment_length + XGeometry3D.Eplsilon_f)
					{
						segment_point = end;
						sphere_point = sphere_center - ((sphere_center - end).Normalized * sphere_radius);
						return;
					}
					segment_point = start + (segment_direction * center_projection);
					sphere_point = sphere_center + ((segment_point - sphere_center).Normalized * sphere_radius);
					return;
				}

				if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
				{
					if (center_projection < -XGeometry3D.Eplsilon_f)
					{
						// No intersection
						segment_point = start;
						sphere_point = sphere_center - (segment_start_to_center.Normalized * sphere_radius);
						return;
					}
					if (center_projection > segment_length + XGeometry3D.Eplsilon_f)
					{
						// No intersection
						segment_point = end;
						sphere_point = sphere_center - ((sphere_center - end).Normalized * sphere_radius);
						return;
					}
					// Point intersection
					segment_point = sphere_point = start + (segment_direction * center_projection);
					return;
				}

				// Line intersection
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single distance_a = center_projection - distance_to_intersection;
				Single distance_b = center_projection + distance_to_intersection;

				Boolean point_a_is_after_segment_start = distance_a > -XGeometry3D.Eplsilon_f;
				Boolean point_b_is_before_segment_end = distance_b < segment_length + XGeometry3D.Eplsilon_f;

				if (point_a_is_after_segment_start && point_b_is_before_segment_end)
				{
					segment_point = sphere_point = start + (segment_direction * distance_a);
					return;
				}
				if (!point_a_is_after_segment_start && !point_b_is_before_segment_end)
				{
					// The segment is inside, but no intersection
					if (distance_a > -(distance_b - segment_length))
					{
						segment_point = start;
						sphere_point = start + (segment_direction * distance_a);
						return;
					}
					segment_point = end;
					sphere_point = start + (segment_direction * distance_b);
					return;
				}

				Boolean point_a_is_before_segment_end = distance_a < segment_length + XGeometry3D.Eplsilon_f;
				if (point_a_is_after_segment_start && point_a_is_before_segment_end)
				{
					// Point A intersection
					segment_point = sphere_point = start + (segment_direction * distance_a);
					return;
				}
				Boolean point_b_is_after_segment_start = distance_b > -XGeometry3D.Eplsilon_f;
				if (point_b_is_after_segment_start && point_b_is_before_segment_end)
				{
					// Point B intersection
					segment_point = sphere_point = start + (segment_direction * distance_b);
					return;
				}

				// No intersection
				if (center_projection < 0)
				{
					segment_point = start;
					sphere_point = sphere_center - (segment_start_to_center.Normalized * sphere_radius);
					return;
				}
				segment_point = end;
				sphere_point = sphere_center - ((sphere_center - end).Normalized * sphere_radius);
			}
			#endregion

			#region ======================================= Sphere - Sphere ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции двух окружностей
			/// </summary>
			/// <param name="sphere_a">Первая окружность</param>
			/// <param name="sphere_b">Вторая окружность</param>
			/// <param name="point_a">Точка проекции на первую окружность</param>
			/// <param name="point_b">Точка проекции на вторую окружность</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SphereSphere(in Sphere3Df sphere_a, in Sphere3Df sphere_b, out Vector3Df point_a, out Vector3Df point_b)
			{
				SphereSphere(in sphere_a.Center, sphere_a.Radius, in sphere_b.Center, sphere_b.Radius, out point_a, out point_b);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайших точек проекции двух окружностей
			/// </summary>
			/// <param name="center_a">Центр первой сферы</param>
			/// <param name="radius_a">Радиус первой сферы</param>
			/// <param name="center_b">Центр второй сферы</param>
			/// <param name="radius_b">Радиус второй сферы</param>
			/// <param name="point_a">Точка проекции на первую окружность</param>
			/// <param name="point_b">Точка проекции на вторую окружность</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SphereSphere(in Vector3Df center_a, Single radius_a, in Vector3Df center_b, Single radius_b,
				out Vector3Df point_a, out Vector3Df point_b)
			{
				Vector3Df from_b_to_a = (center_a - center_b).Normalized;
				point_a = center_a - (from_b_to_a * radius_a);
				point_b = center_b + (from_b_to_a * radius_b);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================