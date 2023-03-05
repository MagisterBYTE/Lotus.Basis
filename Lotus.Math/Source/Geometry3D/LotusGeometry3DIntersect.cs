//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 3D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry3DIntersect.cs
*		Пересечение в 3D пространстве.
*		Пересечение основных геометрических тел/примитивов друг с другом и получение соответствующей информации
*	о пересечении. Это очень полезная информация. Фактические, на основе ее возможно обнаружения столкновений
*	пересечений не используя при этом родную физическую подсистему Unity3D. Это позволит более гибко управлять
*	многим аспектами как и 3D геометрий, визуализацией так и логической составляющей на ее основе.
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
		/// Тип пересечения в 3D пространстве
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TIntersectType3D
		{
			/// <summary>
			/// Пересечения нет
			/// </summary>
			None,

			/// <summary>
			/// Пересечения представляет собой точку.
			/// Обычно пересечения луча с геометрическими объектами
			/// </summary>
			Point,

			/// <summary>
			/// Пересечения представляет собой сегмент
			/// </summary>
			Segment,

			/// <summary>
			/// Пересечения представляет собой луч
			/// </summary>
			Ray,

			/// <summary>
			/// Пересечения представляет собой линию
			/// </summary>
			Line,

			/// <summary>
			/// Пересечения представляет собой полигон
			/// </summary>
			Polygon,

			/// <summary>
			/// Пересечения представляет собой плоскость
			/// </summary>
			Plane,

			/// <summary>
			/// Другой тип пересечения
			/// </summary>
			Other
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура для хранения информации о пересечении в 3D пространстве
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public struct TIntersectHit3D
		{
			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Дистанция
			/// </summary>
			public Double Distance;

			/// <summary>
			/// Барицентрические координаты
			/// </summary>
			public Vector3D BarycentricCoordinate;

			/// <summary>
			/// Первая точка попадания
			/// </summary>
			public Vector3D Point1;

			/// <summary>
			/// Вторая точка попадания
			/// </summary>
			public Vector3D Point2;

			/// <summary>
			/// Тип пересечения
			/// </summary>
			public TIntersectType3D IntersectType;
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура для хранения информации о пересечении в 3D пространстве
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public struct TIntersectHit3Df
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Нет пересечения
			/// </summary>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit3Df None()
			{
				TIntersectHit3Df hit = new TIntersectHit3Df();
				hit.IntersectType = TIntersectType3D.None;
				return (hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Пересечения представляет собой точку
			/// </summary>
			/// <param name="point">Точка пересечения</param>
			/// <returns>Информация о пересечении</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectHit3Df Point(in Vector3Df point)
			{
				TIntersectHit3Df hit = new TIntersectHit3Df();
				hit.IntersectType = TIntersectType3D.Point;
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
			public static TIntersectHit3Df Point(in Vector3Df point, Single distance)
			{
				TIntersectHit3Df hit = new TIntersectHit3Df();
				hit.IntersectType = TIntersectType3D.Point;
				hit.Point1 = point;
				hit.Distance = distance;
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
			public static TIntersectHit3Df Segment(in Vector3Df point_1, in Vector3Df point_2)
			{
				TIntersectHit3Df hit = new TIntersectHit3Df();
				hit.IntersectType = TIntersectType3D.Segment;
				hit.Point1 = point_1;
				hit.Point2 = point_2;
				return (hit);
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Тип пересечения
			/// </summary>
			public TIntersectType3D IntersectType;

			/// <summary>
			/// Дистанция
			/// </summary>
			public Single Distance;

			/// <summary>
			/// Барицентрические координаты
			/// </summary>
			public Vector3Df BarycentricCoordinate;

			/// <summary>
			/// Первая точка попадания
			/// </summary>
			public Vector3Df Point1;

			/// <summary>
			/// Вторая точка попадания
			/// </summary>
			public Vector3Df Point2;
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы для работы с пересечением в 3D
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XIntersect3D
		{
			#region ======================================= Point - Line ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на линии
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="line">Линия</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointLine(Vector3Df point, Line3Df line)
			{
				return PointLine(point, line.Position, line.Direction);
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
			public static Boolean PointLine(Vector3Df point, Vector3Df line_pos, Vector3Df line_dir)
			{
				return XDistance3D.PointLine(point, line_pos, line_dir) < XGeometry3D.Eplsilon_f;
			}
			#endregion

			#region ======================================= Point - Ray ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечении/нахождении некоторой точки на луче
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="ray">Луч</param>
			/// <param name="length">Длина до этой точке, если она находится на луче</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType3D PointRay(in Vector3D point, in Ray3D ray, out Double length)
			{
				Vector3D delta = point - ray.Position;
				Vector3D tim = new Vector3D(delta.X / ray.Direction.X, delta.Y / ray.Direction.Y,
					delta.Z / ray.Direction.Z);

				// Сравниваем по компонентно
				if (XMath.Approximately(tim.X, tim.Y) && XMath.Approximately(tim.Y, tim.Z))
				{
					length = tim.X;
					return TIntersectType3D.None;
				}
				else
				{
					length = 0;
					return TIntersectType3D.Point;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на нахождение точки на луче
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="ray">Луч</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointRay(Vector3Df point, Ray3Df ray)
			{
				return PointRay(point, ray.Position, ray.Direction);
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
			public static Boolean PointRay(Vector3Df point, Vector3Df ray_pos, Vector3Df ray_dir)
			{
				return XDistance3D.PointRay(point, ray_pos, ray_dir) < XGeometry3D.Eplsilon_f;
			}
			#endregion

			#region ======================================= Point - Segment ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка нахождения точки на отрезке
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="segment">Отрезок</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointSegment(in Vector3Df point, in Segment3Df segment)
			{
				return PointSegment(in point, in segment.Start, in segment.End);
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
			public static Boolean PointSegment(in Vector3Df point, in Vector3Df start, in Vector3Df end)
			{
				return XDistance3D.PointSegment(point, start, end) < XGeometry3D.Eplsilon_f;
			}
			#endregion

			#region ======================================= Point - Sphere ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на попадание точки в область сферы
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="sphere">Сфера</param>
			/// <returns>Статус попадания</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointSphere(in Vector3Df point, in Sphere3Df sphere)
			{
				return PointSphere(in point, in sphere.Center, sphere.Radius);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на попадание точки в область сферы
			/// </summary>
			/// <param name="point">Проверяемая точка</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <returns>Статус попадания</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean PointSphere(in Vector3Df point, in Vector3Df sphere_center, Single sphere_radius)
			{
				// For points on the sphere's surface magnitude is more stable than SqrLength
				return (point - sphere_center).Length < (sphere_radius * sphere_radius) + XGeometry3D.Eplsilon_f;
			}
			#endregion

			#region ======================================= Line - Line ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух линий
			/// </summary>
			/// <param name="line_a">Первая линия</param>
			/// <param name="line_b">Вторая линия</param>
			/// <returns>Статус пересечения линий</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineLine(in Line3Df line_a, in Line3Df line_b)
			{
				return LineLine(in line_a.Position, in line_a.Direction, in line_b.Position, in line_b.Direction, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка пересечения двух линий
			/// </summary>
			/// <param name="line_a">Первая линия</param>
			/// <param name="line_b">Вторая линия</param>
			/// <param name="hit">Точка пересечения линий если они пересекаются</param>
			/// <returns>Статус пересечения линий</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineLine(in Line3Df line_a, in Line3Df line_b, out Vector3Df hit)
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
			public static Boolean LineLine(in Vector3Df pos_a, in Vector3Df dir_a, in Vector3Df pos_b, in Vector3Df dir_b)
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
			/// <param name="hit">Точка пересечения линий если они пересекаются</param>
			/// <returns>Статус пересечения линий</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineLine(in Vector3Df pos_a, in Vector3Df dir_a, in Vector3Df pos_b, in Vector3Df dir_b,
				out Vector3Df hit)
			{
				Single sqr_length_a = dir_a.SqrLength;
				Single sqr_length_b = dir_b.SqrLength;
				Single dot_a_b = Vector3Df.Dot(in dir_a, in dir_b);

				Single denominator = (sqr_length_a * sqr_length_b) - (dot_a_b * dot_a_b);
				Vector3Df pos_b_to_a = pos_a - pos_b;
				Single a = Vector3Df.Dot(in dir_a, in pos_b_to_a);
				Single b = Vector3Df.Dot(in dir_b, in pos_b_to_a);

				Vector3Df closest_point_a;
				Vector3Df closest_point_b;
				if (Math.Abs(denominator) < XGeometry3D.Eplsilon_f)
				{
					// Parallel
					Single distance_b = dot_a_b > sqr_length_b ? a / dot_a_b : b / sqr_length_b;

					closest_point_a = pos_a;
					closest_point_b = pos_b + (dir_b * distance_b);
				}
				else
				{
					// Not parallel
					Single distance_a = ((sqr_length_a * b) - (dot_a_b * a)) / denominator;
					Single distance_b = ((dot_a_b * b) - (sqr_length_b * a)) / denominator;

					closest_point_a = pos_a + (dir_a * distance_a);
					closest_point_b = pos_b + (dir_b * distance_b);
				}

				if ((closest_point_b - closest_point_a).SqrLength < XGeometry3D.Eplsilon_f)
				{
					hit = closest_point_a;
					return true;
				}
				hit = Vector3Df.Zero;
				return false;
			}
			#endregion

			#region ======================================= Line - Sphere =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и сферы
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="sphere">Сфера</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineSphere(in Line3Df line, in Sphere3Df sphere)
			{
				return LineSphere(in line.Position, in line.Direction, in sphere.Center, sphere.Radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и сферы
			/// </summary>
			/// <param name="line">Линия</param>
			/// <param name="sphere">Сфера</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineSphere(in Line3Df line, in Sphere3Df sphere, out TIntersectHit3Df hit)
			{
				return LineSphere(in line.Position, in line.Direction, in sphere.Center, sphere.Radius, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и сферы
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineSphere(in Vector3Df line_pos, in Vector3Df line_dir, in Vector3Df sphere_center, Single sphere_radius)
			{
				return LineSphere(in line_pos, in line_dir, in sphere_center, sphere_radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения линии и сферы
			/// </summary>
			/// <param name="line_pos">Позиция линии</param>
			/// <param name="line_dir">Направление линии</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LineSphere(in Vector3Df line_pos, in Vector3Df line_dir, in Vector3Df sphere_center, Single sphere_radius,
				out TIntersectHit3Df hit)
			{
				Vector3Df pos_to_center = sphere_center - line_pos;
				Single center_projection = Vector3Df.Dot(in line_dir, in pos_to_center);
				Single sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);

				Single sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
				{
					hit = TIntersectHit3Df.None();
					return false;
				}
				if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
				{
					hit = TIntersectHit3Df.Point(line_pos + (line_dir * center_projection));
					return true;
				}

				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single distance_a = center_projection - distance_to_intersection;
				Single distance_b = center_projection + distance_to_intersection;

				Vector3Df point_a = line_pos + (line_dir * distance_a);
				Vector3Df point_b = line_pos + (line_dir * distance_b);
				hit = TIntersectHit3Df.Segment(point_a, point_b);
				return true;
			}
			#endregion Line-Sphere

			#region ======================================= Ray - Ray =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечении/совпадения лучей
			/// </summary>
			/// <param name="ray_1">Первый луч</param>
			/// <param name="ray_2">Второй луч</param>
			/// <param name="point">Точка пересечения лучей, если они пересекаются</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType3D RayToRay(in Ray3D ray_1, in Ray3D ray_2, out Vector3D point)
			{
				point = Vector3D.Zero;
				return TIntersectType3D.None;
			}
			#endregion

			#region ======================================= Ray - Plane ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечение луча и плоскости
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="plane">Плоскость</param>
			/// <param name="point">Точка пересечения, если они пересекаются</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType3D RayToPlane(in Ray3D ray, in Plane3D plane, out Vector3D point)
			{
				Double dot = Vector3D.Dot(in ray.Direction, in plane.Normal);
				Double distance = plane.GetDistanceToPoint(in ray.Position);

				// Не должно быть равным нулю
				if (XMath.Approximately(dot, 0) == false)
				{
					// Направление луча не параллельно плоскости. Пересечение есть
					// Точка пересечения
					point = ray.Position + (ray.Direction * (distance / dot));

					return TIntersectType3D.Point;
				}

				point = Vector3D.Zero;

				// А может на луч лежит плоскости
				if (XMath.Approximately(distance, XGeometry3D.Eplsilon_d))
				{
					return TIntersectType3D.Line;
				}

				return TIntersectType3D.None;
			}
			#endregion

			#region ======================================= Ray - Sphere ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечение луча и сферы. Используется аналитическое решение
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="position">Позиция центра сферы</param>
			/// <param name="radius">Радиус сферы</param>
			/// <param name="point">Точка пересечения, если они пересекаются</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType3D RayToSphere(in Ray3D ray, in Vector3D position, Single radius, out Vector3D point)
			{
				point = Vector3D.Zero;

				Double l = ray.Direction.X;
				Double m = ray.Direction.Y;
				Double n = ray.Direction.Z;

				Double dx = ray.Position.X - position.X;
				Double dy = ray.Position.Y - position.Y;
				Double dz = ray.Position.Z - position.Z;

				Double a = (l * l) + (m * m) + (n * n);
				Double b = (2 * dx * l) + (2 * dy * m) + (2 * dz * n);
				Double c = (dx * dx) + (dy * dy * dz * dz) - (radius * radius);

				Double t1, t2;
				Int32 result = XMathSolver.SolveQuadraticEquation(a, b, c, out t1, out t2);
				if (result == -1)
				{
					return TIntersectType3D.None;
				}
				else
				{
					if (result == 0)
					{
						point = ray.GetPoint(t1);
						return TIntersectType3D.Point;
					}
					else
					{
						Double value = 0;
						if (t1 > 0)
						{
							if (t2 < 0)
							{
								value = t1;
							}
							else
							{
								if (t1 < t2)
								{
									value = t1;
								}
								else
								{
									value = t2;
								}
							}
						}
						if (t2 > 0)
						{
							if (t1 < 0)
							{
								value = t2;
							}
							else
							{
								if (t2 < t1)
								{
									value = t2;
								}
								else
								{
									value = t1;
								}
							}
						}
						point = ray.GetPoint(value);
						return TIntersectType3D.Point;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и сферы
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="sphere">Сфера</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RaySphere(in Ray3Df ray, in Sphere3Df sphere)
			{
				return RaySphere(in ray.Position, in ray.Direction, in sphere.Center, sphere.Radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и сферы
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="sphere">Сфера</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RaySphere(in Ray3Df ray, in Sphere3Df sphere, out TIntersectHit3Df hit)
			{
				return RaySphere(in ray.Position, in ray.Direction, in sphere.Center, sphere.Radius, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и сферы
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RaySphere(in Vector3Df ray_pos, in Vector3Df ray_dir, in Vector3Df sphere_center, Single sphere_radius)
			{
				return RaySphere(in ray_pos, in ray_dir, in sphere_center, sphere_radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения луча и сферы
			/// </summary>
			/// <param name="ray_pos">Позиция луча</param>
			/// <param name="ray_dir">Направление луча</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RaySphere(in Vector3Df ray_pos, in Vector3Df ray_dir, in Vector3Df sphere_center, Single sphere_radius,
				out TIntersectHit3Df hit)
			{
				Vector3Df pos_to_center = sphere_center - ray_pos;
				Single center_projection = Vector3Df.Dot(in ray_dir, in pos_to_center);
				if (center_projection + sphere_radius < -XGeometry3D.Eplsilon_f)
				{
					hit = TIntersectHit3Df.None();
					return false;
				}

				Single sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
				{
					hit = TIntersectHit3Df.None();
					return false;
				}
				if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
				{
					if (center_projection < -XGeometry3D.Eplsilon_f)
					{
						hit = TIntersectHit3Df.None();
						return false;
					}
					hit = TIntersectHit3Df.Point(ray_pos + (ray_dir * center_projection));
					return true;
				}

				// Line hit
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single distance_a = center_projection - distance_to_intersection;
				Single distance_b = center_projection + distance_to_intersection;

				if (distance_a < -XGeometry3D.Eplsilon_f)
				{
					if (distance_b < -XGeometry3D.Eplsilon_f)
					{
						hit = TIntersectHit3Df.None();
						return false;
					}
					hit = TIntersectHit3Df.Point(ray_pos + (ray_dir * distance_b));
					return true;
				}

				Vector3Df point_a = ray_pos + (ray_dir * distance_a);
				Vector3Df point_b = ray_pos + (ray_dir * distance_b);
				hit = TIntersectHit3Df.Segment(point_a, point_b);
				return true;
			}
			#endregion

			#region ======================================= Ray - Triangle ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечение луча и треугольника
			/// </summary>
			/// <param name="ray">Луч</param>
			/// <param name="p1">Первая вершина треугольника</param>
			/// <param name="p2">Вторая вершина треугольника</param>
			/// <param name="p3">Третья вершина треугольника</param>
			/// <param name="ray_hit">Информация о пересечении</param>
			/// <returns>Тип пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TIntersectType3D RayToTriangle(in Ray3D ray, in Vector3D p1, in Vector3D p2, Vector3D p3,
				out TIntersectHit3D ray_hit)
			{
				ray_hit = new TIntersectHit3D();

				// Find vectors for two edges sharing vert0
				Vector3D edge1 = p2 - p1;
				Vector3D edge2 = p3 - p1;

				// Begin calculating determinant - also used to calculate U parameter
				Vector3D pvec = Vector3D.Cross(in ray.Direction, in edge2);

				// If determinant is near zero, ray lies in plane of triangle
				Double det = Vector3D.Dot(in edge1, in pvec);

				Vector3D tvec;
				if (det > 0)
				{
					tvec = ray.Position - p1;
				}
				else
				{
					tvec = p1 - ray.Position;
					det = -det;
				}

				if (det < XGeometry3D.Eplsilon_d)
				{
					return TIntersectType3D.None;
				}

				// Calculate U parameter and test bounds
				Double u = Vector3D.Dot(in tvec, in pvec);
				if (u < 0.0 || u > det)
				{
					return TIntersectType3D.None;
				}

				// Prepare to test V parameter
				Vector3D qvec = Vector3D.Cross(in tvec, in edge1);

				// Calculate V parameter and test bounds
				Double v = Vector3D.Dot(in ray.Direction, in qvec);
				if (v < 0.0 || u + v > det)
				{
					return TIntersectType3D.None;
				}

				// Calculate t, scale parameters, ray intersects triangle
				Double t = Vector3D.Dot(in edge2, in qvec);
				Double invert_t = 1.0 / det;
				t *= invert_t;
				u *= invert_t;
				v *= invert_t;

				// Сохраняем данные
				ray_hit.IntersectType = TIntersectType3D.Point;
				ray_hit.Distance = t;
				ray_hit.BarycentricCoordinate = new Vector3D(u, v, 0.0);
				ray_hit.Point1 = ray.GetPoint(t);

				return TIntersectType3D.Point;
			}
			#endregion

			#region ======================================= Segment - Sphere ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения отрезка и сферы
			/// </summary>
			/// <param name="segment">Отрезок</param>
			/// <param name="sphere">Сфера</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentSphere(in Segment3Df segment, in Sphere3Df sphere)
			{
				return SegmentSphere(in segment.Start, in segment.End, in sphere.Center, sphere.Radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения отрезка и сферы
			/// </summary>
			/// <param name="segment">Отрезок</param>
			/// <param name="sphere">Сфера</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentSphere(Segment3Df segment, Sphere3Df sphere, out TIntersectHit3Df hit)
			{
				return SegmentSphere(in segment.Start, in segment.End, in sphere.Center, sphere.Radius, out hit);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения отрезка и сферы
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentSphere(in Vector3Df start, in Vector3Df end, in Vector3Df sphere_center, Single sphere_radius)
			{
				return SegmentSphere(in start, in end, in sphere_center, sphere_radius, out _);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на пересечения отрезка и сферы
			/// </summary>
			/// <param name="start">Начало отрезка</param>
			/// <param name="end">Конец отрезка</param>
			/// <param name="sphere_center">Центр сферы</param>
			/// <param name="sphere_radius">Радиус сферы</param>
			/// <param name="hit">Информация о пересечении</param>
			/// <returns>Статус пересечения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SegmentSphere(in Vector3Df start, in Vector3Df end, in Vector3Df sphere_center, Single sphere_radius,
				out TIntersectHit3Df hit)
			{
				Vector3Df segment_start_to_center = sphere_center - start;
				Vector3Df from_start_to_end = end - start;
				Single segment_length = from_start_to_end.Length;
				if (segment_length < XGeometry3D.Eplsilon_f)
				{
					Single distanceToPoint = segment_start_to_center.Length;
					if (distanceToPoint < sphere_radius + XGeometry3D.Eplsilon_f)
					{
						if (distanceToPoint > sphere_radius - XGeometry3D.Eplsilon_f)
						{
							hit = TIntersectHit3Df.Point(start);
							return true;
						}
						hit = TIntersectHit3Df.None();
						return true;
					}
					hit = TIntersectHit3Df.None();
					return false;
				}

				Vector3Df segment_direction = from_start_to_end.Normalized;
				Single center_projection = Vector3Df.Dot(segment_direction, segment_start_to_center);
				if (center_projection + sphere_radius < -XGeometry3D.Eplsilon_f ||
					center_projection - sphere_radius > segment_length + XGeometry3D.Eplsilon_f)
				{
					hit = TIntersectHit3Df.None();
					return false;
				}

				Single sqr_distance_to_line = segment_start_to_center.SqrLength - (center_projection * center_projection);
				Single sqr_distance_to_intersection = (sphere_radius * sphere_radius) - sqr_distance_to_line;
				if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
				{
					hit = TIntersectHit3Df.None();
					return false;
				}

				if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
				{
					if (center_projection < -XGeometry3D.Eplsilon_f ||
						center_projection > segment_length + XGeometry3D.Eplsilon_f)
					{
						hit = TIntersectHit3Df.None();
						return false;
					}
					hit = TIntersectHit3Df.Point(start + (segment_direction * center_projection));
					return true;
				}

				// Line hit
				Single distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
				Single distance_a = center_projection - distance_to_intersection;
				Single distance_b = center_projection + distance_to_intersection;

				Boolean point_a_is_after_segment_start = distance_a > -XGeometry3D.Eplsilon_f;
				Boolean point_b_is_before_segment_end = distance_b < segment_length + XGeometry3D.Eplsilon_f;

				if (point_a_is_after_segment_start && point_b_is_before_segment_end)
				{
					Vector3Df point_a = start + (segment_direction * distance_a);
					Vector3Df point_b = start + (segment_direction * distance_b);
					hit = TIntersectHit3Df.Segment(in point_a, in point_b);
					return true;
				}
				if (!point_a_is_after_segment_start && !point_b_is_before_segment_end)
				{
					// The segment is inside, but no hit
					hit = TIntersectHit3Df.None();
					return true;
				}

				Boolean point_a_is_before_segment_end = distance_a < segment_length + XGeometry3D.Eplsilon_f;
				if (point_a_is_after_segment_start && point_a_is_before_segment_end)
				{
					// Point A hit
					hit = TIntersectHit3Df.Point(start + (segment_direction * distance_a));
					return true;
				}
				Boolean point_b_is_after_segment_start = distance_b > -XGeometry3D.Eplsilon_f;
				if (point_b_is_after_segment_start && point_b_is_before_segment_end)
				{
					// Point B hit
					hit = TIntersectHit3Df.Point(start + (segment_direction * distance_b));
					return true;
				}

				hit = TIntersectHit3Df.None();
				return false;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================