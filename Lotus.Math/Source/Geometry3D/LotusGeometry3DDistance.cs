//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 3D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry3DDistance.cs
*		Вычисление дистанции.
*		Алгоритмы вычисление дистанции между основными геометрическими телами/примитивами.
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
		/// Статический класс реализующий методы вычисление дистанции между основными геометрическими телами/примитивами
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XDistance3D
		{
			#region ======================================= Point - Line ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояния между линией и ближайшей точки
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="line">Линия</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single PointLine(Vector3Df point, Line3Df line)
			{
				return Vector3Df.Distance(point, XClosest3D.PointLine(point, line));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояния между линией и ближайшей точки
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single PointLine(Vector3Df point, Vector3Df line_pos, Vector3Df line_dir)
			{
				return Vector3Df.Distance(point, XClosest3D.PointLine(point, line_pos, line_dir));
			}
			#endregion

			#region ======================================= Point - Ray ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние до самой близкой точки на луче
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="ray">Луч</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single PointRay(Vector3Df point, Ray3Df ray)
			{
				return Vector3Df.Distance(point, XClosest3D.PointRay(point, ray));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние до самой близкой точки на луче
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single PointRay(Vector3Df point, Vector3Df ray_pos, Vector3Df ray_dir)
			{
				return Vector3Df.Distance(point, XClosest3D.PointRay(point, ray_pos, ray_dir));
			}
			#endregion

			#region ======================================= Point - Segment ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние до самой близкой точки на отрезке
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="segment">Отрезок</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single PointSegment(Vector3Df point, Segment3Df segment)
			{
				return Vector3Df.Distance(point, XClosest3D.PointSegment(point, segment));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние до самой близкой точки на отрезке
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single PointSegment(Vector3Df point, Vector3Df start, Vector3Df end)
			{
				return Vector3Df.Distance(point, XClosest3D.PointSegment(point, start, end));
			}
			#endregion

			#region ======================================= Point - Sphere ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние до самой близкой точки на сферы
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="sphere">Сфера</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single PointSphere(Vector3Df point, Sphere3Df sphere)
			{
				return PointSphere(point, sphere.Center, sphere.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние до самой близкой точки на сферы
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single PointSphere(Vector3Df point, Vector3Df sphere_center, Single sphere_radius)
			{
				return (sphere_center - point).Length - sphere_radius;
			}
			#endregion

			#region ======================================= Line - Sphere =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на линии и сферы
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="sphere">Сфера</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single LineSphere(Line3Df line, Sphere3Df sphere)
			{
				return LineSphere(line.Position, line.Direction, sphere.Center, sphere.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на линии и сферы
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single LineSphere(Vector3Df line_pos, Vector3Df line_dir, Vector3Df sphere_center, Single sphere_radius)
			{
				Vector3Df pos_to_center = sphere_center - line_pos;
				Single center_projection = Vector3Df.Dot(ref line_dir, ref pos_to_center);
				Single sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
				{
					// No intersection
					return XMath.Sqrt(sqr_distance_to_line) - sphere_radius;
				}
				return 0;
			}
			#endregion

			#region ======================================= Ray - Sphere ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на луче и сферы
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="sphere">Сфера</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single RaySphere(Ray3Df ray, Sphere3Df sphere)
			{
				return RaySphere(ray.Position, ray.Direction, sphere.Center, sphere.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на луче и сферы
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single RaySphere(Vector3Df ray_pos, Vector3Df ray_dir, Vector3Df sphere_center, Single sphere_radius)
			{
				Vector3Df pos_to_center = sphere_center - ray_pos;
				Single center_projection = Vector3Df.Dot(ref ray_dir, ref pos_to_center);
				if (center_projection + sphere_radius < -XGeometry3D.Eplsilon_f)
				{
					// No intersection
					return XMath.Sqrt(pos_to_center.SqrLength) - sphere_radius;
				}

				Single sqr_distance_to_pos = pos_to_center.SqrLength;
				Single sqr_distance_to_line = sqr_distance_to_pos - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < -XGeometry3D.Eplsilon_f)
					{
						return XMath.Sqrt(sqr_distance_to_pos) - sphere_radius;
					}
					return XMath.Sqrt(sqr_distance_to_line) - sphere_radius;
				}
				if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
				{
					if (center_projection < -XGeometry3D.Eplsilon_f)
					{
						// No intersection
						return XMath.Sqrt(sqr_distance_to_pos) - sphere_radius;
					}
					// Point intersection
					return 0;
				}

				// Line intersection
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single distance_a = center_projection - distance_to_intersection;
				Single distance_b = center_projection + distance_to_intersection;

				if (distance_a < -XGeometry3D.Eplsilon_f)
				{
					if (distance_b < -XGeometry3D.Eplsilon_f)
					{
						// No intersection
						return XMath.Sqrt(sqr_distance_to_pos) - sphere_radius;
					}

					// Point intersection;
					return 0;
				}

				// Two points intersection;
				return 0;
			}
			#endregion

			#region ======================================= Segment - Sphere ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на отрезке и сферы
			/// </summary>
			/// <param name="segment">Отрезок</param>
			/// <param name="sphere">Сфера</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SegmentSphere(Segment3Df segment, Sphere3Df sphere)
			{
				return SegmentSphere(segment.Start, segment.End, sphere.Center, sphere.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на отрезке и сферы
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SegmentSphere(Vector3Df start, Vector3Df end, Vector3Df sphere_center, Single sphere_radius)
			{
				Vector3Df segment_start_to_center = sphere_center - start;
				Vector3Df from_start_to_end = end - start;
				Single segment_length = from_start_to_end.Length;
				if (segment_length < XGeometry3D.Eplsilon_f)
				{
					return segment_start_to_center.Length - sphere_radius;
				}

				Vector3Df segment_direction = from_start_to_end.Normalized;
				Single center_projection = Vector3Df.Dot(ref segment_direction, ref segment_start_to_center);
				if (center_projection + sphere_radius < -XGeometry3D.Eplsilon_f ||
					center_projection - sphere_radius > segment_length + XGeometry3D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < 0)
					{
						return XMath.Sqrt(segment_start_to_center.SqrLength) - sphere_radius;
					}
					return (sphere_center - end).Length - sphere_radius;
				}

				Single sqr_distance_to_a = segment_start_to_center.SqrLength;
				Single sqr_distance_to_line = sqr_distance_to_a - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < -XGeometry3D.Eplsilon_f)
					{
						return XMath.Sqrt(sqr_distance_to_a) - sphere_radius;
					}
					if (center_projection > segment_length + XGeometry3D.Eplsilon_f)
					{
						return (sphere_center - end).Length - sphere_radius;
					}
					return XMath.Sqrt(sqr_distance_to_line) - sphere_radius;
				}

				if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
				{
					if (center_projection < -XGeometry3D.Eplsilon_f)
					{
						// No intersection
						return XMath.Sqrt(sqr_distance_to_a) - sphere_radius;
					}
					if (center_projection > segment_length + XGeometry3D.Eplsilon_f)
					{
						// No intersection
						return (sphere_center - end).Length - sphere_radius;
					}
					// Point intersection
					return 0;
				}

				// Line intersection
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single distance_a = center_projection - distance_to_intersection;
				Single distance_b = center_projection + distance_to_intersection;

				Boolean point_a_is_after_segment_start = distance_a > -XGeometry3D.Eplsilon_f;
				Boolean point_b_is_before_segment_end = distance_b < segment_length + XGeometry3D.Eplsilon_f;

				if (point_a_is_after_segment_start && point_b_is_before_segment_end)
				{
					// Two points intersection
					return 0;
				}
				if (!point_a_is_after_segment_start && !point_b_is_before_segment_end)
				{
					// The segment is inside, but no intersection
					distance_b = -(distance_b - segment_length);
					return distance_a > distance_b ? distance_a : distance_b;
				}

				Boolean point_a_is_before_segment_end = distance_a < segment_length + XGeometry3D.Eplsilon_f;
				if (point_a_is_after_segment_start && point_a_is_before_segment_end)
				{
					// Point A intersection
					return 0;
				}
				Boolean point_b_is_after_segment_start = distance_b > -XGeometry3D.Eplsilon_f;
				if (point_b_is_after_segment_start && point_b_is_before_segment_end)
				{
					// Point B intersection
					return 0;
				}

				// No intersection
				if (center_projection < 0)
				{
					return XMath.Sqrt(sqr_distance_to_a) - sphere_radius;
				}
				return (sphere_center - end).Length - sphere_radius;
			}
			#endregion

			#region ======================================= Sphere - Sphere ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на сферах
			/// </summary>
			/// <param name="sphere_a">Первая сфера</param>
			/// <param name="sphere_b">Вторая сфера</param>
			/// <returns>
			/// Положительное значение, если сферы не пересекаются, отрицательный иначе
			/// Отрицательная величина может быть интерпретирована как глубина проникновения
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SphereSphere(Sphere3Df sphere_a, Sphere3Df sphere_b)
			{
				return SphereSphere(sphere_a.Center, sphere_a.Radius, sphere_b.Center, sphere_b.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на сферах
			/// </summary>
			/// <param name="center_a">Центр первой сферы</param>
			/// <param name="radius_a">Радиус первой сферы</param>
			/// <param name="center_b">Центр второй сферы</param>
			/// <param name="radius_b">Радиус второй сферы</param>
			/// <returns>
			/// Положительное значение, если сферы не пересекаются, отрицательный иначе
			/// Отрицательная величина может быть интерпретирована как глубина проникновения
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SphereSphere(Vector3Df center_a, Single radius_a, Vector3Df center_b, Single radius_b)
			{
				return Vector3Df.Distance(center_a, center_b) - radius_a - radius_b;
			}

			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================