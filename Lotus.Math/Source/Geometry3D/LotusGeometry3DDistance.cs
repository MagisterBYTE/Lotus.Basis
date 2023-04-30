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
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Security.Cryptography;
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup MathGeometry3D
		*@{*/
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
			public static Single PointLine(in Vector3Df point, in Line3Df line)
			{
				return Vector3Df.Distance(in point, XClosest3D.PointLine(in point, in line));
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
			public static Single PointLine(in Vector3Df point, in Vector3Df line_pos, in Vector3Df line_dir)
			{
				return Vector3Df.Distance(in point, XClosest3D.PointLine(in point, in line_pos, in line_dir));
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
			public static Single PointRay(in Vector3Df point, in Ray3Df ray)
			{
				return Vector3Df.Distance(in point, XClosest3D.PointRay(in point, in ray));
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
			public static Single PointRay(in Vector3Df point, in Vector3Df ray_pos, in Vector3Df ray_dir)
			{
				return Vector3Df.Distance(in point, XClosest3D.PointRay(in point, in ray_pos, in ray_dir));
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
			public static Single PointSegment(in Vector3Df point, in Segment3Df segment)
			{
				return Vector3Df.Distance(in point, XClosest3D.PointSegment(in point, in segment));
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
			public static Single PointSegment(in Vector3Df point, in Vector3Df start, in Vector3Df end)
			{
				return Vector3Df.Distance(in point, XClosest3D.PointSegment(in point, in start, in end));
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
			public static Single PointSphere(in Vector3Df point, in Sphere3Df sphere)
			{
				return PointSphere(in point, in sphere.Center, sphere.Radius);
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
			public static Single PointSphere(in Vector3Df point, in Vector3Df sphere_center, Single sphere_radius)
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
			public static Single LineSphere(in Line3Df line, in Sphere3Df sphere)
			{
				return LineSphere(in line.Position, in line.Direction, in sphere.Center, sphere.Radius);
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
			public static Single LineSphere(in Vector3Df line_pos, in Vector3Df line_dir, in Vector3Df sphere_center, Single sphere_radius)
			{
				Vector3Df pos_to_center = sphere_center - line_pos;
				var center_projection = Vector3Df.Dot(in line_dir, in pos_to_center);
				var sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
				var sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
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
			public static Single RaySphere(in Ray3Df ray, in Sphere3Df sphere)
			{
				return RaySphere(in ray.Position, in ray.Direction, in sphere.Center, sphere.Radius);
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
			public static Single RaySphere(in Vector3Df ray_pos, in Vector3Df ray_dir, in Vector3Df sphere_center, Single sphere_radius)
			{
				Vector3Df pos_to_center = sphere_center - ray_pos;
				var center_projection = Vector3Df.Dot(in ray_dir, in pos_to_center);
				if (center_projection + sphere_radius < -XGeometry3D.Eplsilon_f)
				{
					// No intersection
					return XMath.Sqrt(pos_to_center.SqrLength) - sphere_radius;
				}

				var sqr_distance_to_pos = pos_to_center.SqrLength;
				var sqr_distance_to_line = sqr_distance_to_pos - (center_projection * center_projection);
				var sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
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
				var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				var distance_a = center_projection - distance_to_intersection;
				var distance_b = center_projection + distance_to_intersection;

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
			public static Single SegmentSphere(in Segment3Df segment, in Sphere3Df sphere)
			{
				return SegmentSphere(in segment.Start, in segment.End, in sphere.Center, sphere.Radius);
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
			public static Single SegmentSphere(in Vector3Df start, in Vector3Df end, in Vector3Df sphere_center, Single sphere_radius)
			{
				Vector3Df segment_start_to_center = sphere_center - start;
				Vector3Df from_start_to_end = end - start;
				var segment_length = from_start_to_end.Length;
				if (segment_length < XGeometry3D.Eplsilon_f)
				{
					return segment_start_to_center.Length - sphere_radius;
				}

				Vector3Df segment_direction = from_start_to_end.Normalized;
				var center_projection = Vector3Df.Dot(in segment_direction, in segment_start_to_center);
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

				var sqr_distance_to_a = segment_start_to_center.SqrLength;
				var sqr_distance_to_line = sqr_distance_to_a - (center_projection * center_projection);
				var sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
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
				var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				var distance_a = center_projection - distance_to_intersection;
				var distance_b = center_projection + distance_to_intersection;

				var point_a_is_after_segment_start = distance_a > -XGeometry3D.Eplsilon_f;
				var point_b_is_before_segment_end = distance_b < segment_length + XGeometry3D.Eplsilon_f;

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

				var point_a_is_before_segment_end = distance_a < segment_length + XGeometry3D.Eplsilon_f;
				if (point_a_is_after_segment_start && point_a_is_before_segment_end)
				{
					// Point A intersection
					return 0;
				}
				var point_b_is_after_segment_start = distance_b > -XGeometry3D.Eplsilon_f;
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
			/// Положительное значение, если сферы не пересекаются, отрицательное иначе
			/// Отрицательная величина может быть интерпретирована как глубина проникновения
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SphereSphere(in Sphere3Df sphere_a, in Sphere3Df sphere_b)
			{
				return SphereSphere(in sphere_a.Center, sphere_a.Radius, in sphere_b.Center, sphere_b.Radius);
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
			/// Положительное значение, если сферы не пересекаются, отрицательное иначе
			/// Отрицательная величина может быть интерпретирована как глубина проникновения
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SphereSphere(in Vector3Df center_a, Single radius_a, in Vector3Df center_b, Single radius_b)
			{
				return Vector3Df.Distance(in center_a, in center_b) - radius_a - radius_b;
			}

			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================