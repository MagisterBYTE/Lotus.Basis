//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 2D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry2DDistance.cs
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
		//! \addtogroup MathGeometry2D
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы вычисление дистанции между основными геометрическими телами/примитивами
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XDistance2D
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
			public static Single PointLine(Vector2Df point, Line2Df line)
			{
				return Vector2Df.Distance(point, XClosest2D.PointLine(point, line));
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
			public static Single PointLine(Vector2Df point, Vector2Df line_pos, Vector2Df line_dir)
			{
				return Vector2Df.Distance(point, XClosest2D.PointLine(point, line_pos, line_dir));
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
			public static Single PointRay(Vector2Df point, Ray2Df ray)
			{
				return Vector2Df.Distance(point, XClosest2D.PointRay(point, ray));
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
			public static Single PointRay(Vector2Df point, Vector2Df ray_pos, Vector2Df ray_dir)
			{
				return Vector2Df.Distance(point, XClosest2D.PointRay(point, ray_pos, ray_dir));
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
			public static Single PointSegment(Vector2Df point, Segment2Df segment)
			{
				return Vector2Df.Distance(point, XClosest2D.PointSegment(point, segment));
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
			public static Single PointSegment(Vector2Df point, Vector2Df start, Vector2Df end)
			{
				return Vector2Df.Distance(point, XClosest2D.PointSegment(point, start, end));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние до самой близкой точки на отрезке
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="segment_direction">Направление отрезка</param>
			/// <param name="segment_length">Длина отрезка</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Single PointSegment(Vector2Df point, Vector2Df start, Vector2Df end, 
				Vector2Df segment_direction, Single segment_length)
			{
				Single point_projection = Vector2Df.Dot(segment_direction, point - start);
				if (point_projection < -XGeometry2D.Eplsilon_f)
				{
					return Vector2Df.Distance(point, start);
				}
				if (point_projection > segment_length + XGeometry2D.Eplsilon_f)
				{
					return Vector2Df.Distance(point, end);
				}
				return Vector2Df.Distance(point, start + (segment_direction * point_projection));
			}
			#endregion

			#region ======================================= Point - Circle ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние до самой близкой точки на окружности
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="circle">Окружность</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single PointCircle(Vector2Df point, Circle2Df circle)
			{
				return PointCircle(point, circle.Center, circle.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние до самой близкой точки на окружности
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single PointCircle(Vector2Df point, Vector2Df circle_center, Single circle_radius)
			{
				return (circle_center - point).Length - circle_radius;
			}
			#endregion

			#region ======================================= Line - Line ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на линиях
			/// </summary>
			/// <param name="line_a">Первая линия</param>
			/// <param name="line_b">Вторая линия</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single LineLine(Line2Df line_a, Line2Df line_b)
			{
				return LineLine(line_a.Position, line_a.Direction, line_b.Position, line_b.Direction);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на линиях
			/// </summary>
			/// <param name="pos_a">Позиция первой линии</param>
			/// <param name="dir_a">Направление первой линии</param>
			/// <param name="pos_b">Позиция второй линии</param>
			/// <param name="dir_b">Направление второй линии</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single LineLine(Vector2Df pos_a, Vector2Df dir_a, Vector2Df pos_b, 
				Vector2Df dir_b)
			{
				if (Math.Abs(Vector2Df.DotPerp(ref dir_a, ref dir_b)) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					Vector2Df pos_b_to_a = pos_a - pos_b;
					if (Math.Abs(Vector2Df.DotPerp(ref dir_a, ref pos_b_to_a)) > XGeometry2D.Eplsilon_f ||
						Math.Abs(Vector2Df.DotPerp(ref dir_b, ref pos_b_to_a)) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						Single origin_b_projection = Vector2Df.Dot(ref dir_a, ref pos_b_to_a);
						Single distance_sqr = pos_b_to_a.SqrLength - (origin_b_projection * origin_b_projection);

						// distanceSqr can be negative
						return distance_sqr <= 0 ? 0 : XMath.Sqrt(distance_sqr);
					}

					// Collinear
					return 0;
				}

				// Not parallel
				return 0;
			}
			#endregion

			#region ======================================= Line - Ray ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на линии и луче
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="ray">Луч</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single LineRay(Line2Df line, Ray2Df ray)
			{
				return LineRay(line.Position, line.Direction, ray.Position, ray.Direction);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на линии и луче
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single LineRay(Vector2Df line_pos, Vector2Df line_dir, Vector2Df ray_pos, Vector2Df ray_dir)
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
						Single ray_pos_projection = Vector2Df.Dot(ref line_dir, ref ray_pos_to_line_pos);
						Single distanceSqr = ray_pos_to_line_pos.SqrLength - (ray_pos_projection * ray_pos_projection);
						// distanceSqr can be negative
						return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
					}
					// Collinear
					return 0;
				}

				// Not parallel
				Single ray_distance = perp_dot_a / denominator;
				if (ray_distance < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					Single ray_pos_projection = Vector2Df.Dot(ref line_dir, ref ray_pos_to_line_pos);
					Vector2Df line_point = line_pos - (line_dir * ray_pos_projection);
					return Vector2Df.Distance(line_point, ray_pos);
				}
				// Point intersection
				return 0;
			}
			#endregion

			#region ======================================= Line - Segment ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на линии и отрезке
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="segment">Отрезок</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single LineSegment(Line2Df line, Segment2Df segment)
			{
				return LineSegment(line.Position, line.Direction, segment.Start, segment.End);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на линии и отрезке
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single LineSegment(Vector2Df line_pos, Vector2Df line_dir, Vector2Df start, Vector2Df end)
			{
				Vector2Df segment_start_to_pos = line_pos - start;
				Vector2Df segment_direction = end - start;
				Single denominator = Vector2Df.DotPerp(ref line_dir, ref segment_direction);
				Single perp_dot_a = Vector2Df.DotPerp(ref line_dir, ref segment_start_to_pos);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					// Normalized Direction gives more stable results 
					Single perp_dot_b = Vector2Df.DotPerp(segment_direction.Normalized, segment_start_to_pos);
					if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						Single segment_start_projection = Vector2Df.Dot(ref line_dir, ref segment_start_to_pos);
						Single distanceSqr = segment_start_to_pos.SqrLength - (segment_start_projection * segment_start_projection);
						// distanceSqr can be negative
						return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
					}
					// Collinear
					return 0;
				}

				// Not parallel
				Single segment_distance = perp_dot_a / denominator;
				if (segment_distance < -XGeometry2D.Eplsilon_f || segment_distance > 1 + XGeometry2D.Eplsilon_f)
				{
					// No intersection
					Vector2Df segment_point = start + (segment_direction * XMath.Clamp01(segment_distance));
					Single segmentPointProjection = Vector2Df.Dot(line_dir, segment_point - line_pos);
					Vector2Df line_point = line_pos + (line_dir * segmentPointProjection);
					return Vector2Df.Distance(line_point, segment_point);
				}
				// Point intersection
				return 0;
			}
			#endregion

			#region ======================================= Line - Circle =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на линии и окружности
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="circle">Окружность</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single LineCircle(Line2Df line, Circle2Df circle)
			{
				return LineCircle(line.Position, line.Direction, circle.Center, circle.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на линии и окружности
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single LineCircle(Vector2Df line_pos, Vector2Df line_dir, 
				Vector2Df circle_center, Single circle_radius)
			{
				Vector2Df pos_to_center = circle_center - line_pos;
				Single center_projection = Vector2Df.Dot(line_dir, pos_to_center);
				Single sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (circle_radius * circle_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					return XMath.Sqrt(sqr_distance_to_line) - circle_radius;
				}
				return 0;
			}
			#endregion

			#region ======================================= Ray - Ray =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на лучах
			/// </summary>
			/// <param name="ray_a">Первый луч</param>
			/// <param name="ray_b">Второй луч</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single RayRay(Ray2Df ray_a, Ray2Df ray_b)
			{
				return RayRay(ray_a.Position, ray_a.Direction, ray_b.Position, ray_b.Direction);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на лучах
			/// </summary>
			/// <param name="pos_a">Позиция первого луча</param>
			/// <param name="dir_a">Направление первого луча</param>
			/// <param name="pos_b">Позиция второго луча</param>
			/// <param name="dir_b">Направление второго луча</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single RayRay(Vector2Df pos_a, Vector2Df dir_a, Vector2Df pos_b, 
				Vector2Df dir_b)
			{
				Vector2Df pos_b_to_a = pos_a - pos_b;
				Single denominator = Vector2Df.DotPerp(ref dir_a, ref dir_b);
				Single perp_dot_a = Vector2Df.DotPerp(ref dir_a, ref pos_b_to_a);
				Single perp_dot_b = Vector2Df.DotPerp(ref dir_b, ref pos_b_to_a);

				Boolean codirected = Vector2Df.Dot(ref dir_a, ref dir_b) > 0;
				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					Single pos_b_projection = -Vector2Df.Dot(dir_a, pos_b_to_a);
					if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						if (!codirected && pos_b_projection < XGeometry2D.Eplsilon_f)
						{
							return Vector2Df.Distance(pos_a, pos_b);
						}
						Single distanceSqr = pos_b_to_a.SqrLength - (pos_b_projection * pos_b_projection);
						// distanceSqr can be negative
						return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
					}
					// Collinear

					if (codirected)
					{
						// Ray intersection
						return 0;
					}
					else
					{
						if (pos_b_projection < XGeometry2D.Eplsilon_f)
						{
							// No intersection
							return Vector2Df.Distance(pos_a, pos_b);
						}
						else
						{
							// Segment intersection
							return 0;
						}
					}
				}

				// Not parallel
				Single distance_a = perp_dot_b / denominator;
				Single distance_b = perp_dot_a / denominator;
				if (distance_a < -XGeometry2D.Eplsilon_f || distance_b < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					if (codirected)
					{
						Single pos_a_projection = Vector2Df.Dot(dir_b, pos_b_to_a);
						if (pos_a_projection > -XGeometry2D.Eplsilon_f)
						{
							Vector2Df ray_point_a = pos_a;
							Vector2Df ray_point_b = pos_b + (dir_b * pos_a_projection);
							return Vector2Df.Distance(ray_point_a, ray_point_b);
						}
						Single pos_b_projection = -Vector2Df.Dot(dir_a, pos_b_to_a);
						if (pos_b_projection > -XGeometry2D.Eplsilon_f)
						{
							Vector2Df ray_point_a = pos_a + (dir_a * pos_b_projection);
							Vector2Df ray_point_b = pos_b;
							return Vector2Df.Distance(ray_point_a, ray_point_b);
						}
						return Vector2Df.Distance(pos_a, pos_b);
					}
					else
					{
						if (distance_a > -XGeometry2D.Eplsilon_f)
						{
							Single pos_b_projection = -Vector2Df.Dot(dir_a, pos_b_to_a);
							if (pos_b_projection > -XGeometry2D.Eplsilon_f)
							{
								Vector2Df ray_point_a = pos_a + (dir_a * pos_b_projection);
								Vector2Df ray_point_b = pos_b;
								return Vector2Df.Distance(ray_point_a, ray_point_b);
							}
						}
						else if (distance_b > -XGeometry2D.Eplsilon_f)
						{
							Single pos_a_projection = Vector2Df.Dot(dir_b, pos_b_to_a);
							if (pos_a_projection > -XGeometry2D.Eplsilon_f)
							{
								Vector2Df ray_point_a = pos_a;
								Vector2Df ray_point_b = pos_b + (dir_b * pos_a_projection);
								return Vector2Df.Distance(ray_point_a, ray_point_b);
							}
						}
						return Vector2Df.Distance(pos_a, pos_b);
					}
				}
				// Point intersection
				return 0;
			}
			#endregion

			#region ======================================= Ray - Segment =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на луче и сегменте
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="segment">Отрезок</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single RaySegment(Ray2Df ray, Segment2Df segment)
			{
				return RaySegment(ray.Position, ray.Direction, segment.Start, segment.End);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на луче и сегменте
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single RaySegment(Vector2Df ray_pos, Vector2Df ray_dir, Vector2Df start, Vector2Df end)
			{
				Vector2Df segment_start_to_pos = ray_pos - start;
				Vector2Df segment_direction = end - start;
				Single denominator = Vector2Df.DotPerp(ref ray_dir, ref segment_direction);
				Single perp_dot_a = Vector2Df.DotPerp(ref ray_dir, ref segment_start_to_pos);
				
				// Normalized Direction gives more stable results 
				Single perp_dot_b = Vector2Df.DotPerp(segment_direction.Normalized, segment_start_to_pos);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					Single segment_start_projection = -Vector2Df.Dot(ray_dir, segment_start_to_pos);
					Vector2Df originToSegmentB = end - ray_pos;
					Single segment_end_projection = Vector2Df.Dot(ray_dir, originToSegmentB);
					if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						if (segment_start_projection > -XGeometry2D.Eplsilon_f)
						{
							Single distanceSqr = segment_start_to_pos.SqrLength - (segment_start_projection * segment_start_projection);
							// distanceSqr can be negative
							return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
						}
						if (segment_end_projection > -XGeometry2D.Eplsilon_f)
						{
							Single distanceSqr = originToSegmentB.SqrLength - (segment_end_projection * segment_end_projection);
							// distanceSqr can be negative
							return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
						}

						if (segment_start_projection > segment_end_projection)
						{
							return Vector2Df.Distance(ray_pos, start);
						}
						return Vector2Df.Distance(ray_pos, end);
					}
					// Collinear
					if (segment_start_projection > -XGeometry2D.Eplsilon_f || segment_end_projection > -XGeometry2D.Eplsilon_f)
					{
						// Point or segment intersection
						return 0;
					}
					// No intersection
					return segment_start_projection > segment_end_projection ? -segment_start_projection : -segment_end_projection;
				}

				// Not parallel
				Single ray_distance = perp_dot_b / denominator;
				Single segment_distance = perp_dot_a / denominator;
				if (ray_distance < -XGeometry2D.Eplsilon_f ||
					segment_distance < -XGeometry2D.Eplsilon_f || segment_distance > 1 + XGeometry2D.Eplsilon_f)
				{
					// No intersection
					Boolean codirected = Vector2Df.Dot(ref ray_dir, ref segment_direction) > 0;
					Vector2Df segment_end_to_pos;
					if (!codirected)
					{
						XMath.Swap(ref start, ref end);
						segment_direction = -segment_direction;
						segment_end_to_pos = segment_start_to_pos;
						segment_start_to_pos = ray_pos - start;
						segment_distance = 1 - segment_distance;
					}
					else
					{
						segment_end_to_pos = ray_pos - end;
					}

					Single segment_start_projection = -Vector2Df.Dot(ref ray_dir, ref segment_start_to_pos);
					Single segment_end_projection = -Vector2Df.Dot(ref ray_dir, ref segment_end_to_pos);
					Boolean segment_start_on_ray = segment_start_projection > -XGeometry2D.Eplsilon_f;
					Boolean segment_end_on_ray = segment_end_projection > -XGeometry2D.Eplsilon_f;
					if (segment_start_on_ray && segment_end_on_ray)
					{
						if (segment_distance < 0)
						{
							Vector2Df ray_point = ray_pos + (ray_dir * segment_start_projection);
							Vector2Df segment_point = start;
							return Vector2Df.Distance(ray_point, segment_point);
						}
						else
						{
							Vector2Df ray_point = ray_pos + (ray_dir * segment_end_projection);
							Vector2Df segment_point = end;
							return Vector2Df.Distance(ref ray_point, ref segment_point);
						}
					}
					else if (!segment_start_on_ray && segment_end_on_ray)
					{
						if (segment_distance < 0)
						{
							Vector2Df ray_point = ray_pos;
							Vector2Df segment_point = start;
							return Vector2Df.Distance(ray_point, segment_point);
						}
						else if (segment_distance > 1 + XGeometry2D.Eplsilon_f)
						{
							Vector2Df ray_point = ray_pos + (ray_dir * segment_end_projection);
							Vector2Df segment_point = end;
							return Vector2Df.Distance(ray_point, segment_point);
						}
						else
						{
							Vector2Df ray_point = ray_pos;
							Single pos_projection = Vector2Df.Dot(ref segment_direction, ref segment_start_to_pos);
							Vector2Df segment_point = start + (segment_direction * pos_projection / segment_direction.SqrLength);
							return Vector2Df.Distance(ref ray_point, ref segment_point);
						}
					}
					else
					{
						// Not on ray
						Vector2Df ray_point = ray_pos;
						Single pos_projection = Vector2Df.Dot(ref segment_direction, ref segment_start_to_pos);
						Single sqr_segment_length = segment_direction.SqrLength;
						if (pos_projection < 0)
						{
							return Vector2Df.Distance(ref ray_point, ref start);
						}
						else if (pos_projection > sqr_segment_length)
						{
							return Vector2Df.Distance(ref ray_point, ref end);
						}
						else
						{
							Vector2Df segment_point = start + (segment_direction * pos_projection / sqr_segment_length);
							return Vector2Df.Distance(ref ray_point, ref segment_point);
						}
					}
				}
				// Point intersection
				return 0;
			}
			#endregion

			#region ======================================= Ray - Circle ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на луче и окружности
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="circle">Окружность</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single RayCircle(Ray2Df ray, Circle2Df circle)
			{
				return RayCircle(ray.Position, ray.Direction, circle.Center, circle.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на луче и окружности
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single RayCircle(Vector2Df ray_pos, Vector2Df ray_dir, Vector2Df circle_center, 
				Single circle_radius)
			{
				Vector2Df pos_to_center = circle_center - ray_pos;
				Single center_projection = Vector2Df.Dot(ref ray_dir, ref pos_to_center);
				if (center_projection + circle_radius < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					return XMath.Sqrt(pos_to_center.SqrLength) - circle_radius;
				}

				Single sqr_distance_to_pos = pos_to_center.SqrLength;
				Single sqr_distance_to_line = sqr_distance_to_pos - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (circle_radius * circle_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < -XGeometry2D.Eplsilon_f)
					{
						return XMath.Sqrt(sqr_distance_to_pos) - circle_radius;
					}
					return XMath.Sqrt(sqr_distance_to_line) - circle_radius;
				}
				if (sqr_distance_to_intersection < XGeometry2D.Eplsilon_f)
				{
					if (center_projection < -XGeometry2D.Eplsilon_f)
					{
						// No intersection
						return XMath.Sqrt(sqr_distance_to_pos) - circle_radius;
					}
					// Point intersection
					return 0;
				}

				// Line intersection
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single distance_a = center_projection - distance_to_intersection;
				Single distance_b = center_projection + distance_to_intersection;

				if (distance_a < -XGeometry2D.Eplsilon_f)
				{
					if (distance_b < -XGeometry2D.Eplsilon_f)
					{
						// No intersection
						return XMath.Sqrt(sqr_distance_to_pos) - circle_radius;
					}

					// Point intersection;
					return 0;
				}

				// Two points intersection;
				return 0;
			}
			#endregion

			#region ======================================= Segment - Segment =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на отрезках
			/// </summary>
			/// <param name="segment1">Первый отрезок</param>
			/// <param name="segment2">Второй отрезок</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SegmentSegment(Segment2Df segment1, Segment2Df segment2)
			{
				return SegmentSegment(segment1.Start, segment1.End, segment2.Start, segment2.End);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на отрезках
			/// </summary>
			/// <param name="segment1_start">Начало первого отрезка</param>
			/// <param name="segment1_end">Окончание первого отрезка</param>
			/// <param name="segment2_start">Начало второго отрезка</param>
			/// <param name="segment2_end">Окончание второго отрезка</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SegmentSegment(Vector2Df segment1_start, Vector2Df segment1_end, 
				Vector2Df segment2_start, Vector2Df segment2_end)
			{
				Vector2Df from_2start_to_1start = segment1_start - segment2_start;
				Vector2Df direction1 = segment1_end - segment1_start;
				Vector2Df direction2 = segment2_end - segment2_start;
				Single segment_1length = direction1.Length;
				Single segment_2length = direction2.Length;

				Boolean segment1IsAPoint = segment_1length < XGeometry2D.Eplsilon_f;
				Boolean segment2IsAPoint = segment_2length < XGeometry2D.Eplsilon_f;
				if (segment1IsAPoint && segment2IsAPoint)
				{
					return Vector2Df.Distance(ref segment1_start, ref segment2_start);
				}
				if (segment1IsAPoint)
				{
					direction2.Normalize();
					return PointSegment(segment1_start, segment2_start, segment2_end, direction2, segment_2length);
				}
				if (segment2IsAPoint)
				{
					direction1.Normalize();
					return PointSegment(segment2_start, segment1_start, segment1_end, direction1, segment_1length);
				}

				direction1.Normalize();
				direction2.Normalize();
				Single denominator = Vector2Df.DotPerp(ref direction1, ref direction2);
				Single perpDot1 = Vector2Df.DotPerp(ref direction1, ref from_2start_to_1start);
				Single perpDot2 = Vector2Df.DotPerp(ref direction2, ref from_2start_to_1start);

				if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
				{
					// Parallel
					if (Math.Abs(perpDot1) > XGeometry2D.Eplsilon_f || Math.Abs(perpDot2) > XGeometry2D.Eplsilon_f)
					{
						// Not collinear
						Single segment2AProjection = -Vector2Df.Dot(ref direction1, ref from_2start_to_1start);
						if (segment2AProjection > -XGeometry2D.Eplsilon_f &&
							segment2AProjection < segment_1length + XGeometry2D.Eplsilon_f)
						{
							Single distanceSqr = from_2start_to_1start.SqrLength - (segment2AProjection * segment2AProjection);
							// distanceSqr can be negative
							return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
						}

						Vector2Df from1ATo2B = segment2_end - segment1_start;
						Single segment2BProjection = Vector2Df.Dot(ref direction1, ref from1ATo2B);
						if (segment2BProjection > -XGeometry2D.Eplsilon_f &&
							segment2BProjection < segment_1length + XGeometry2D.Eplsilon_f)
						{
							Single distanceSqr = from1ATo2B.SqrLength - (segment2BProjection * segment2BProjection);
							// distanceSqr can be negative
							return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
						}

						if (segment2AProjection < 0 && segment2BProjection < 0)
						{
							if (segment2AProjection > segment2BProjection)
							{
								return Vector2Df.Distance(ref segment1_start, ref segment2_start);
							}
							return Vector2Df.Distance(ref segment1_start, ref segment2_end);
						}
						if (segment2AProjection > 0 && segment2BProjection > 0)
						{
							if (segment2AProjection < segment2BProjection)
							{
								return Vector2Df.Distance(ref segment1_end, ref segment2_start);
							}
							return Vector2Df.Distance(ref segment1_end, ref segment2_end);
						}
						Single segment1AProjection = Vector2Df.Dot(ref direction2, ref from_2start_to_1start);
						Vector2Df segment2Point = segment2_start + (direction2 * segment1AProjection);
						return Vector2Df.Distance(ref segment1_start, ref segment2Point);
					}
					// Collinear

					Boolean codirected = Vector2Df.Dot(ref direction1, ref direction2) > 0;
					if (codirected)
					{
						// Codirected
						Single segment2AProjection = -Vector2Df.Dot(ref direction1, ref from_2start_to_1start);
						if (segment2AProjection > -XGeometry2D.Eplsilon_f)
						{
							// 1A------1B
							//     2A------2B
							return SegmentSegmentCollinear(segment1_start, segment1_end, segment2_start);
						}
						else
						{
							//     1A------1B
							// 2A------2B
							return SegmentSegmentCollinear(segment2_start, segment2_end, segment1_start);
						}
					}
					else
					{
						// Contradirected
						Single segment2BProjection = Vector2Df.Dot(direction1, segment2_end - segment1_start);
						if (segment2BProjection > -XGeometry2D.Eplsilon_f)
						{
							// 1A------1B
							//     2B------2A
							return SegmentSegmentCollinear(segment1_start, segment1_end, segment2_end);
						}
						else
						{
							//     1A------1B
							// 2B------2A
							return SegmentSegmentCollinear(segment2_end, segment2_start, segment1_start);
						}
					}
				}

				// Not parallel
				Single distance1 = perpDot2 / denominator;
				Single distance2 = perpDot1 / denominator;
				if (distance1 < -XGeometry2D.Eplsilon_f || distance1 > segment_1length + XGeometry2D.Eplsilon_f ||
					distance2 < -XGeometry2D.Eplsilon_f || distance2 > segment_2length + XGeometry2D.Eplsilon_f)
				{
					// No intersection
					Boolean codirected = Vector2Df.Dot(ref direction1, ref direction2) > 0;
					Vector2Df from1ATo2B;
					if (!codirected)
					{
						XMath.Swap(ref segment2_start, ref segment2_end);
						direction2 = -direction2;
						from1ATo2B = -from_2start_to_1start;
						from_2start_to_1start = segment1_start - segment2_start;
						distance2 = segment_2length - distance2;
					}
					else
					{
						from1ATo2B = segment2_end - segment1_start;
					}
					Vector2Df segment1Point;
					Vector2Df segment2Point;

					Single segment2AProjection = -Vector2Df.Dot(ref direction1, ref from_2start_to_1start);
					Single segment2BProjection = Vector2Df.Dot(ref direction1, ref from1ATo2B);

					Boolean segment2AIsAfter1A = segment2AProjection > -XGeometry2D.Eplsilon_f;
					Boolean segment2BIsBefore1B = segment2BProjection < segment_1length + XGeometry2D.Eplsilon_f;
					Boolean segment2AOnSegment1 = segment2AIsAfter1A && segment2AProjection < segment_1length + XGeometry2D.Eplsilon_f;
					Boolean segment2BOnSegment1 = segment2BProjection > -XGeometry2D.Eplsilon_f && segment2BIsBefore1B;
					if (segment2AOnSegment1 && segment2BOnSegment1)
					{
						if (distance2 < -XGeometry2D.Eplsilon_f)
						{
							segment1Point = segment1_start + (direction1 * segment2AProjection);
							segment2Point = segment2_start;
						}
						else
						{
							segment1Point = segment1_start + (direction1 * segment2BProjection);
							segment2Point = segment2_end;
						}
					}
					else if (!segment2AOnSegment1 && !segment2BOnSegment1)
					{
						if (!segment2AIsAfter1A && !segment2BIsBefore1B)
						{
							segment1Point = distance1 < -XGeometry2D.Eplsilon_f ? segment1_start : segment1_end;
						}
						else
						{
							// Not on segment
							segment1Point = segment2AIsAfter1A ? segment1_end : segment1_start;
						}
						Single segment1PointProjection = Vector2Df.Dot(direction2, segment1Point - segment2_start);
						segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment_2length);
						segment2Point = segment2_start + (direction2 * segment1PointProjection);
					}
					else if (segment2AOnSegment1)
					{
						if (distance2 < -XGeometry2D.Eplsilon_f)
						{
							segment1Point = segment1_start + (direction1 * segment2AProjection);
							segment2Point = segment2_start;
						}
						else
						{
							segment1Point = segment1_end;
							Single segment1PointProjection = Vector2Df.Dot(direction2, segment1Point - segment2_start);
							segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment_2length);
							segment2Point = segment2_start + (direction2 * segment1PointProjection);
						}
					}
					else
					{
						if (distance2 > segment_2length + XGeometry2D.Eplsilon_f)
						{
							segment1Point = segment1_start + (direction1 * segment2BProjection);
							segment2Point = segment2_end;
						}
						else
						{
							segment1Point = segment1_start;
							Single segment1PointProjection = Vector2Df.Dot(direction2, segment1Point - segment2_start);
							segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment_2length);
							segment2Point = segment2_start + (direction2 * segment1PointProjection);
						}
					}
					return Vector2Df.Distance(ref segment1Point, ref segment2Point);
				}

				// Point intersection
				return 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на отрезках
			/// </summary>
			/// <param name="left_a">Начало отрезка слева</param>
			/// <param name="left_b">Конец отрезка слева</param>
			/// <param name="right_a">Начало отрезка справа</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Single SegmentSegmentCollinear(Vector2Df left_a, Vector2Df left_b, Vector2Df right_a)
			{
				Vector2Df left_direction = left_b - left_a;
				Single right_a_projection = Vector2Df.Dot(left_direction.Normalized, right_a - left_b);
				if (Math.Abs(right_a_projection) < XGeometry2D.Eplsilon_f)
				{
					// LB == RA
					// LA------LB
					//         RA------RB

					// Point intersection
					return 0;
				}
				if (right_a_projection < 0)
				{
					// LB > RA
					// LA------LB
					//     RARB
					//     RA--RB
					//     RA------RB

					// Segment intersection
					return 0;
				}
				// LB < RA
				// LA------LB
				//             RA------RB

				// No intersection
				return right_a_projection;
			}
			#endregion

			#region ======================================= Segment - Circle ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на отрезке и окружности
			/// </summary>
			/// <param name="segment">Отрезок</param>
			/// <param name="circle">Окружность</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SegmentCircle(Segment2Df segment, Circle2Df circle)
			{
				return SegmentCircle(segment.Start, segment.End, circle.Center, circle.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на отрезке и окружности
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="circle_center">Центр окружности</param>
			/// <param name="circle_radius">Радиус окружности</param>
			/// <returns>Расстояние</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SegmentCircle(Vector2Df start, Vector2Df end, Vector2Df circle_center, Single circle_radius)
			{
				Vector2Df segment_start_to_center = circle_center - start;
				Vector2Df from_start_to_end = end - start;
				Single segment_length = from_start_to_end.Length;
				if (segment_length < XGeometry2D.Eplsilon_f)
				{
					return segment_start_to_center.Length - circle_radius;
				}

				Vector2Df segment_direction = from_start_to_end.Normalized;
				Single center_projection = Vector2Df.Dot(ref segment_direction, ref segment_start_to_center);
				if (center_projection + circle_radius < -XGeometry2D.Eplsilon_f ||
					center_projection - circle_radius > segment_length + XGeometry2D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < 0)
					{
						return XMath.Sqrt(segment_start_to_center.SqrLength) - circle_radius;
					}
					return (circle_center - end).Length - circle_radius;
				}

				Single sqr_distance_to_a = segment_start_to_center.SqrLength;
				Single sqr_distance_to_line = sqr_distance_to_a - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (circle_radius * circle_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
				{
					// No intersection
					if (center_projection < -XGeometry2D.Eplsilon_f)
					{
						return XMath.Sqrt(sqr_distance_to_a) - circle_radius;
					}
					if (center_projection > segment_length + XGeometry2D.Eplsilon_f)
					{
						return (circle_center - end).Length - circle_radius;
					}
					return XMath.Sqrt(sqr_distance_to_line) - circle_radius;
				}

				if (sqr_distance_to_intersection < XGeometry2D.Eplsilon_f)
				{
					if (center_projection < -XGeometry2D.Eplsilon_f)
					{
						// No intersection
						return XMath.Sqrt(sqr_distance_to_a) - circle_radius;
					}
					if (center_projection > segment_length + XGeometry2D.Eplsilon_f)
					{
						// No intersection
						return (circle_center - end).Length - circle_radius;
					}
					// Point intersection
					return 0;
				}

				// Line intersection
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single distance_a = center_projection - distance_to_intersection;
				Single distance_b = center_projection + distance_to_intersection;

				Boolean point_a_is_after_segment_start = distance_a > -XGeometry2D.Eplsilon_f;
				Boolean point_b_is_before_segment_end = distance_b < segment_length + XGeometry2D.Eplsilon_f;

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

				Boolean point_a_is_before_segment_end = distance_a < segment_length + XGeometry2D.Eplsilon_f;
				if (point_a_is_after_segment_start && point_a_is_before_segment_end)
				{
					// Point A intersection
					return 0;
				}
				Boolean point_b_is_after_segment_start = distance_b > -XGeometry2D.Eplsilon_f;
				if (point_b_is_after_segment_start && point_b_is_before_segment_end)
				{
					// Point B intersection
					return 0;
				}

				// No intersection
				if (center_projection < 0)
				{
					return XMath.Sqrt(sqr_distance_to_a) - circle_radius;
				}
				return (circle_center - end).Length - circle_radius;
			}
			#endregion

			#region ======================================= Circle - Circle ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на окружностях
			/// </summary>
			/// <param name="circle_a">Первая окружность</param>
			/// <param name="circle_b">Вторая окружность</param>
			/// <returns>
			/// Положительное значение, если окружности не пересекаются, отрицательный иначе
			/// Отрицательная величина может быть интерпретирована как глубина проникновения
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single CircleCircle(Circle2Df circle_a, Circle2Df circle_b)
			{
				return CircleCircle(circle_a.Center, circle_a.Radius, circle_b.Center, circle_b.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление расстояние между самыми близкими точками на окружностях
			/// </summary>
			/// <param name="center_a">Центр первой окружности</param>
			/// <param name="radius_a">Радиус первой окружности</param>
			/// <param name="center_b">Центр второй окружности</param>
			/// <param name="radius_b">Радиус второй окружности</param>
			/// <returns>
			/// Положительное значение, если окружности не пересекаются, отрицательный иначе
			/// Отрицательная величина может быть интерпретирована как глубина проникновения
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single CircleCircle(Vector2Df center_a, Single radius_a, Vector2Df center_b, 
				Single radius_b)
			{
				return Vector2Df.Distance(ref center_a, ref center_b) - radius_a - radius_b;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================