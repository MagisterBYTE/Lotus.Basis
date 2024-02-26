using System;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry2D
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы нахождения ближайших точек пересечения(проекции) основных геометрических тел/примитивов.
    /// </summary>
    public static class XClosest2D
    {
        #region Point - Line 
        /// <summary>
        /// Проекция точки на линию.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="line">Линия.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointLine(in Vector2Df point, in Line2Df line)
        {
            return PointLine(in point, in line.Position, in line.Direction, out _);
        }

        /// <summary>
        /// Проекция точки на линию.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="line">Линия.</param>
        /// <param name="distance">Расстояние от позиции линии до спроецированной точки.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointLine(in Vector2Df point, in Line2Df line, out float distance)
        {
            return PointLine(in point, in line.Position, in line.Direction, out distance);
        }

        /// <summary>
        /// Проекция точки на линию.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointLine(in Vector2Df point, in Vector2Df linePos, in Vector2Df lineDir)
        {
            return PointLine(in point, in linePos, in lineDir, out _);
        }

        /// <summary>
        /// Проекция точки на линию.
        /// </summary>
        /// <remarks>
        /// Проекция точки на прямую – это либо сама точка, если она лежит на данной прямой, либо основание перпендикуляра, 
        /// опущенного из этой точки на заданную прямую
        /// </remarks>
        /// <param name="point">Точка.</param>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <param name="distance">Расстояние от позиции линии до спроецированной точки.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointLine(in Vector2Df point, in Vector2Df linePos, in Vector2Df lineDir, out float distance)
        {
            // In theory, sqrMagnitude should be 1, but in practice this division helps with numerical stability
            distance = Vector2Df.Dot(lineDir, point - linePos) / lineDir.SqrLength;
            return linePos + (lineDir * distance);
        }
        #endregion

        #region Point - Ray 
        /// <summary>
        /// Проекция точки на луч.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="ray">Луч.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointRay(in Vector2Df point, in Ray2Df ray)
        {
            return PointRay(in point, in ray.Position, in ray.Direction, out _);
        }

        /// <summary>
        /// Проекция точки на луч.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="ray">Луч.</param>
        /// <param name="distance">Расстояние от позиции луча до спроецированной точки.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointRay(in Vector2Df point, in Ray2Df ray, out float distance)
        {
            return PointRay(in point, in ray.Position, in ray.Direction, out distance);
        }

        /// <summary>
        /// Проекция точки на луч.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointRay(in Vector2Df point, in Vector2Df rayPos, in Vector2Df rayDir)
        {
            return PointRay(point, rayPos, rayDir, out _);
        }

        /// <summary>
        /// Проекция точки на луч.
        /// </summary>
        /// <remarks>
        /// Проекция точки на прямую – это либо сама точка, если она лежит на данной прямой, либо основание перпендикуляра, 
        /// опущенного из этой точки на заданную прямую
        /// </remarks>
        /// <param name="point">Точка.</param>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <param name="distance">Расстояние от позиции луча до спроецированной точки.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointRay(in Vector2Df point, in Vector2Df rayPos, in Vector2Df rayDir, out float distance)
        {
            var point_projection = Vector2Df.Dot(in rayDir, point - rayPos);
            if (point_projection <= 0)
            {
                // Мы находимся по другую сторону луча
                distance = 0;
                return rayPos;
            }

            // In theory, sqrMagnitude should be 1, but in practice this division helps with numerical stability
            distance = point_projection / rayDir.SqrLength;
            return rayPos + (rayDir * distance);
        }
        #endregion Point-Ray

        #region Point - Segment 
        /// <summary>
        /// Проекция точки на отрезок.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="segment">Отрезок.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointSegment(in Vector2Df point, in Segment2Df segment)
        {
            return PointSegment(in point, in segment.Start, in segment.End, out _);
        }

        /// <summary>
        /// Проекция точки на отрезок.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="segment">Отрезок.</param>
        /// <param name="normalizeDistance">Нормализованная дистанция проецируемой точки от начала отрезка.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointSegment(in Vector2Df point, in Segment2Df segment, out float normalizeDistance)
        {
            return PointSegment(in point, in segment.Start, in segment.End, out normalizeDistance);
        }

        /// <summary>
        /// Проекция точки на отрезок.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end)
        {
            return PointSegment(in point, in start, in end, out _);
        }

        /// <summary>
        /// Проекция точки на отрезок.
        /// </summary>
        /// <remarks>
        /// Проекция точки на прямую – это либо сама точка, если она лежит на данной прямой, либо основание перпендикуляра, 
        /// опущенного из этой точки на заданную прямую
        /// </remarks>
        /// <param name="point">Точка.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="normalizeDistance">Нормализованная дистанция проецируемой точки от начала отрезка.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end, out float normalizeDistance)
        {
            Vector2Df segment_direction = end - start;
            var sqr_segment_length = segment_direction.SqrLength;
            if (sqr_segment_length < XGeometry2D.Eplsilon_f)
            {
                // The segment is a point
                normalizeDistance = 0;
                return start;
            }

            var point_projection = Vector2Df.Dot(in segment_direction, point - start);
            if (point_projection <= 0)
            {
                normalizeDistance = 0;
                return start;
            }
            if (point_projection >= sqr_segment_length)
            {
                normalizeDistance = 1;
                return end;
            }

            normalizeDistance = point_projection / sqr_segment_length;
            return start + (segment_direction * normalizeDistance);
        }

        /// <summary>
        /// Проекция точки на отрезок.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="segment_direction">Направление отрезка.</param>
        /// <param name="segment_length">Длина отрезка.</param>
        /// <returns>Спроецированная точка.</returns>
        private static Vector2Df PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end,
            in Vector2Df segment_direction, float segment_length)
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

        #region Point - Circle 
        /// <summary>
        /// Проекция точки на окружность.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="circle">Окружность.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointCircle(in Vector2Df point, in Circle2Df circle)
        {
            return PointCircle(in point, in circle.Center, circle.Radius);
        }

        /// <summary>
        /// Проекция точки на окружность.
        /// </summary>
        /// <remarks>
        /// Проекция точки на окружность – это либо сама точка, если она лежит на окружности, либо пересечение, 
        /// окружности отрезком от точки до центра окружности
        /// </remarks>
        /// <param name="point">Точка.</param>
        /// <param name="circleCenter">Центр окружности.</param>
        /// <param name="circleRadius">Радиус окружности.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector2Df PointCircle(in Vector2Df point, in Vector2Df circleCenter,
                float circleRadius)
        {
            return circleCenter + ((point - circleCenter).Normalized * circleRadius);
        }
        #endregion

        #region Line - Line 
        /// <summary>
        /// Поиск ближайших точек проекции линий.
        /// </summary>
        /// <param name="lineA">Первая линия.</param>
        /// <param name="lineB">Вторая линия.</param>
        /// <param name="pointA">Первая точка пересечения.</param>
        /// <param name="pointB">Вторая точка пересечения.</param>
        public static void LineLine(in Line2Df lineA, in Line2Df lineB, out Vector2Df pointA, out Vector2Df pointB)
        {
            LineLine(in lineA.Position, in lineA.Direction, in lineB.Position, in lineB.Direction, out pointA, out pointB);
        }

        /// <summary>
        /// Поиск ближайших точек проекции линий.
        /// </summary>
        /// <param name="posA">Позиция первой линии.</param>
        /// <param name="dirA">Направление первой линии.</param>
        /// <param name="posB">Позиция второй линии.</param>
        /// <param name="dirB">Направление второй линии.</param>
        /// <param name="pointA">Первая точка пересечения.</param>
        /// <param name="pointB">Вторая точка пересечения.</param>
        public static void LineLine(in Vector2Df posA, in Vector2Df dirA, in Vector2Df posB,
            in Vector2Df dirB, out Vector2Df pointA, out Vector2Df pointB)
        {
            Vector2Df pos_b_to_a = posA - posB;
            var denominator = Vector2Df.DotPerp(in dirA, in dirB);
            var perp_dot_b = Vector2Df.DotPerp(in dirB, in pos_b_to_a);

            if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
            {
                // Parallel
                if (Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f ||
                    Math.Abs(Vector2Df.DotPerp(in dirA, in pos_b_to_a)) > XGeometry2D.Eplsilon_f)
                {
                    // Not collinear
                    pointA = posA;
                    pointB = posB + (dirB * Vector2Df.Dot(in dirB, pos_b_to_a));
                    return;
                }

                // Collinear
                pointA = pointB = posA;
                return;
            }

            // Not parallel
            pointA = pointB = posA + (dirA * (perp_dot_b / denominator));
        }
        #endregion Line-Line

        #region Line - Ray 
        /// <summary>
        /// Поиск ближайших точек проекции линии и луча.
        /// </summary>
        /// <param name="line">Линия.</param>
        /// <param name="ray">Луч.</param>
        /// <param name="linePoint">Точка проекции на линии.</param>
        /// <param name="rayPoint">Точка проекции на луче.</param>
        public static void LineRay(in Line2Df line, in Ray2Df ray, out Vector2Df linePoint, out Vector2Df rayPoint)
        {
            LineRay(in line.Position, in line.Direction, in ray.Position, in ray.Direction, out linePoint, out rayPoint);
        }

        /// <summary>
        /// Поиск ближайших точек проекции линии и луча.
        /// </summary>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <param name="linePoint">Точка проекции на линии.</param>
        /// <param name="rayPoint">Точка проекции на луче.</param>
        public static void LineRay(in Vector2Df linePos, in Vector2Df lineDir, in Vector2Df rayPos, in Vector2Df rayDir,
            out Vector2Df linePoint, out Vector2Df rayPoint)
        {
            Vector2Df ray_pos_to_line_pos = linePos - rayPos;
            var denominator = Vector2Df.DotPerp(in lineDir, in rayDir);
            var perp_dot_a = Vector2Df.DotPerp(in lineDir, in ray_pos_to_line_pos);

            if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
            {
                // Parallel
                var perp_dot_b = Vector2Df.DotPerp(in rayDir, in ray_pos_to_line_pos);
                if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
                {
                    // Not collinear
                    var ray_pos_projection = Vector2Df.Dot(in lineDir, in ray_pos_to_line_pos);
                    linePoint = linePos - (lineDir * ray_pos_projection);
                    rayPoint = rayPos;
                    return;
                }
                // Collinear
                linePoint = rayPoint = rayPos;
                return;
            }

            // Not parallel
            var ray_distance = perp_dot_a / denominator;
            if (ray_distance < -XGeometry2D.Eplsilon_f)
            {
                // No intersection
                var ray_pos_projection = Vector2Df.Dot(in lineDir, in ray_pos_to_line_pos);
                linePoint = linePos - (lineDir * ray_pos_projection);
                rayPoint = rayPos;
                return;
            }

            // Point intersection
            linePoint = rayPoint = rayPos + (rayDir * ray_distance);
        }
        #endregion

        #region Line - Segment 
        /// <summary>
        /// Поиск ближайших точек проекции линии и отрезка.
        /// </summary>
        /// <param name="line">Линия.</param>
        /// <param name="segment">Отрезок.</param>
        /// <param name="linePoint">Точка проекции на линии.</param>
        /// <param name="segmentPoint">Точка проекции на сегменте.</param>
        public static void LineSegment(in Line2Df line, in Segment2Df segment, out Vector2Df linePoint,
            out Vector2Df segmentPoint)
        {
            LineSegment(in line.Position, in line.Direction, in segment.Start, in segment.End, out linePoint, out segmentPoint);
        }

        /// <summary>
        /// Поиск ближайших точек проекции линии и отрезка.
        /// </summary>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="linePoint">Точка проекции на линии.</param>
        /// <param name="segmentPoint">Точка проекции на сегменте.</param>
        public static void LineSegment(in Vector2Df linePos, in Vector2Df lineDir, in Vector2Df start, in Vector2Df end,
            out Vector2Df linePoint, out Vector2Df segmentPoint)
        {
            Vector2Df segment_direction = end - start;
            Vector2Df segment_start_to_pos = linePos - start;
            var denominator = Vector2Df.DotPerp(in lineDir, in segment_direction);
            var perp_dot_start = Vector2Df.DotPerp(in lineDir, in segment_start_to_pos);

            if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
            {
                // Parallel
                var codirected = Vector2Df.Dot(in lineDir, in segment_direction) > 0;

                // Normalized direction gives more stable results 
                var perp_dot_end = Vector2Df.DotPerp(segment_direction.Normalized, segment_start_to_pos);
                if (Math.Abs(perp_dot_start) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_end) > XGeometry2D.Eplsilon_f)
                {
                    // Not collinear
                    if (codirected)
                    {
                        var segment_start_projection = Vector2Df.Dot(in lineDir, in segment_start_to_pos);
                        linePoint = linePos - (lineDir * segment_start_projection);
                        segmentPoint = start;
                    }
                    else
                    {
                        var segment_end_projection = Vector2Df.Dot(in lineDir, linePos - end);
                        linePoint = linePos - (lineDir * segment_end_projection);
                        segmentPoint = end;
                    }
                    return;
                }

                // Collinear
                if (codirected)
                {
                    linePoint = segmentPoint = start;
                }
                else
                {
                    linePoint = segmentPoint = end;
                }
                return;
            }

            // Not parallel
            var segment_distance = perp_dot_start / denominator;
            if (segment_distance < -XGeometry2D.Eplsilon_f || segment_distance > 1 + XGeometry2D.Eplsilon_f)
            {
                // No intersection
                segmentPoint = start + (segment_direction * XMath.Clamp01(segment_distance));
                var segment_point_projection = Vector2Df.Dot(in lineDir, segmentPoint - linePos);
                linePoint = linePos + (lineDir * segment_point_projection);
                return;
            }
            // Point intersection
            linePoint = segmentPoint = start + (segment_direction * segment_distance);
        }
        #endregion

        #region Line - Circle 
        /// <summary>
        /// Поиск ближайших точек проекции линии и окружности.
        /// </summary>
        /// <param name="line">Линия.</param>
        /// <param name="circle">Окружность.</param>
        /// <param name="linePoint">Точка проекции на линии.</param>
        /// <param name="circlePoint">Точка проекции на окружности.</param>
        public static void LineCircle(in Line2Df line, in Circle2Df circle, out Vector2Df linePoint,
            out Vector2Df circlePoint)
        {
            LineCircle(in line.Position, in line.Direction, in circle.Center, circle.Radius, out linePoint, out circlePoint);
        }

        /// <summary>
        /// Поиск ближайших точек проекции линии и окружности.
        /// </summary>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <param name="circleCenter">Центр окружности.</param>
        /// <param name="circleRadius">Радиус окружности.</param>
        /// <param name="linePoint">Точка проекции на линии.</param>
        /// <param name="circlePoint">Точка проекции на окружности.</param>
        public static void LineCircle(in Vector2Df linePos, in Vector2Df lineDir, in Vector2Df circleCenter,
                float circleRadius, out Vector2Df linePoint, out Vector2Df circlePoint)
        {
            Vector2Df pos_to_center = circleCenter - linePos;
            var center_projection = Vector2Df.Dot(in lineDir, in pos_to_center);
            var sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
            var sqr_distance_to_intersection = (circleRadius * circleRadius) - sqr_distance_to_line;
            if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
            {
                // No intersection
                linePoint = linePos + (lineDir * center_projection);
                circlePoint = circleCenter + ((linePoint - circleCenter).Normalized * circleRadius);
                return;
            }
            if (sqr_distance_to_intersection < XGeometry2D.Eplsilon_f)
            {
                // Point intersection
                linePoint = circlePoint = linePos + (lineDir * center_projection);
                return;
            }

            // Two points intersection
            var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
            var distance_a = center_projection - distance_to_intersection;
            linePoint = circlePoint = linePos + (lineDir * distance_a);
        }
        #endregion

        #region Ray - Ray 
        /// <summary>
        /// Поиск ближайших точек проекции лучей.
        /// </summary>
        /// <param name="rayA">Первый луч.</param>
        /// <param name="rayB">Второй луч.</param>
        /// <param name="pointA">Точка пересечения на первом луче.</param>
        /// <param name="pointB">Точка пересечения на втором луче.</param>
        public static void RayRay(in Ray2Df rayA, in Ray2Df rayB, out Vector2Df pointA, out Vector2Df pointB)
        {
            RayRay(rayA.Position, rayA.Direction, rayB.Position, rayB.Direction, out pointA, out pointB);
        }

        /// <summary>
        /// Поиск ближайших точек проекции лучей.
        /// </summary>
        /// <param name="posA">Позиция первого луча.</param>
        /// <param name="dirA">Направление первого луча.</param>
        /// <param name="posB">Позиция второго луча.</param>
        /// <param name="dirB">Направление второго луча.</param>
        /// <param name="pointA">Точка пересечения на первом луче.</param>
        /// <param name="pointB">Точка пересечения на втором луче.</param>
        public static void RayRay(in Vector2Df posA, in Vector2Df dirA, in Vector2Df posB, in Vector2Df dirB,
            out Vector2Df pointA, out Vector2Df pointB)
        {
            Vector2Df pos_b_to_a = posA - posB;
            var denominator = Vector2Df.DotPerp(in dirA, in dirB);
            var perp_dot_a = Vector2Df.DotPerp(in dirA, in pos_b_to_a);
            var perp_dot_b = Vector2Df.DotPerp(in dirB, in pos_b_to_a);
            var codirected = Vector2Df.Dot(in dirA, in dirB) > 0;

            if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
            {
                // Parallel
                var origin_b_projection = Vector2Df.Dot(dirA, pos_b_to_a);
                if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
                {
                    // Not collinear
                    if (codirected)
                    {
                        if (origin_b_projection > -XGeometry2D.Eplsilon_f)
                        {
                            // Projection of pos_a is on ray_b
                            pointA = posA;
                            pointB = posB + (dirA * origin_b_projection);
                            return;
                        }
                        else
                        {
                            pointA = posA - (dirA * origin_b_projection);
                            pointB = posB;
                            return;
                        }
                    }
                    else
                    {
                        if (origin_b_projection > 0)
                        {
                            pointA = posA;
                            pointB = posB;
                            return;
                        }
                        else
                        {
                            // Projection of pos_a is on ray_b
                            pointA = posA;
                            pointB = posB + (dirA * origin_b_projection);
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
                        pointA = pointB = posA;
                        return;
                    }
                    else
                    {
                        pointA = pointB = posB;
                        return;
                    }
                }
                else
                {
                    if (origin_b_projection > 0)
                    {
                        // No intersection
                        pointA = posA;
                        pointB = posB;
                        return;
                    }
                    else
                    {
                        // Segment intersection
                        pointA = pointB = posA;
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
                    var originAProjection = Vector2Df.Dot(in dirB, in pos_b_to_a);
                    if (originAProjection > -XGeometry2D.Eplsilon_f)
                    {
                        pointA = posA;
                        pointB = posB + (dirB * originAProjection);
                        return;
                    }
                    var originBProjection = -Vector2Df.Dot(in dirA, in pos_b_to_a);
                    if (originBProjection > -XGeometry2D.Eplsilon_f)
                    {
                        pointA = posA + (dirA * originBProjection);
                        pointB = posB;
                        return;
                    }
                    pointA = posA;
                    pointB = posB;
                    return;
                }
                else
                {
                    if (distance_a > -XGeometry2D.Eplsilon_f)
                    {
                        var originBProjection = -Vector2Df.Dot(in dirA, in pos_b_to_a);
                        if (originBProjection > -XGeometry2D.Eplsilon_f)
                        {
                            pointA = posA + (dirA * originBProjection);
                            pointB = posB;
                            return;
                        }
                    }
                    else if (distance_b > -XGeometry2D.Eplsilon_f)
                    {
                        var originAProjection = Vector2Df.Dot(in dirB, in pos_b_to_a);
                        if (originAProjection > -XGeometry2D.Eplsilon_f)
                        {
                            pointA = posA;
                            pointB = posB + (dirB * originAProjection);
                            return;
                        }
                    }
                    pointA = posA;
                    pointB = posB;
                    return;
                }
            }
            // Point intersection
            pointA = pointB = posA + (dirA * distance_a);
        }
        #endregion Ray-Ray

        #region Ray - Segment 
        /// <summary>
        /// Поиск ближайших точек проекции луча и отрезка.
        /// </summary>
        /// <param name="ray">Луч.</param>
        /// <param name="segment">Отрезок.</param>
        /// <param name="rayPoint">Точка проекции на луче.</param>
        /// <param name="segmentPoint">Точка проекции на отрезке.</param>
        public static void RaySegment(in Ray2Df ray, in Segment2Df segment, out Vector2Df rayPoint, out Vector2Df segmentPoint)
        {
            RaySegment(in ray.Position, in ray.Direction, in segment.Start, in segment.End, out rayPoint, out segmentPoint);
        }

        /// <summary>
        /// Поиск ближайших точек проекции луча и отрезка.
        /// </summary>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="rayPoint">Точка проекции на луче.</param>
        /// <param name="segmentPoint">Точка проекции на отрезке.</param>
        public static void RaySegment(in Vector2Df rayPos, in Vector2Df rayDir, in Vector2Df start, in Vector2Df end,
            out Vector2Df rayPoint, out Vector2Df segmentPoint)
        {
            Vector2Df start_copy = start;
            Vector2Df end_copy = end;
            Vector2Df segment_direction = end_copy - start_copy;
            Vector2Df segment_start_to_pos = rayPos - start_copy;
            var denominator = Vector2Df.DotPerp(in rayDir, in segment_direction);
            var perp_dot_a = Vector2Df.DotPerp(in rayDir, in segment_start_to_pos);
            // Normalized direction gives more stable results 
            var perp_dot_b = Vector2Df.DotPerp(segment_direction.Normalized, segment_start_to_pos);

            if (Math.Abs(denominator) < XGeometry2D.Eplsilon_f)
            {
                // Parallel
                var segment_start_projection = -Vector2Df.Dot(rayDir, segment_start_to_pos);
                Vector2Df ray_posToSegmentB = end_copy - rayPos;
                var segment_end_projection = Vector2Df.Dot(rayDir, ray_posToSegmentB);
                if (Math.Abs(perp_dot_a) > XGeometry2D.Eplsilon_f || Math.Abs(perp_dot_b) > XGeometry2D.Eplsilon_f)
                {
                    // Not collinear
                    if (segment_start_projection > -XGeometry2D.Eplsilon_f && segment_end_projection > -XGeometry2D.Eplsilon_f)
                    {
                        if (segment_start_projection < segment_end_projection)
                        {
                            rayPoint = rayPos + (rayDir * segment_start_projection);
                            segmentPoint = start_copy;
                            return;
                        }
                        else
                        {
                            rayPoint = rayPos + (rayDir * segment_end_projection);
                            segmentPoint = end_copy;
                            return;
                        }
                    }
                    if (segment_start_projection > -XGeometry2D.Eplsilon_f || segment_end_projection > -XGeometry2D.Eplsilon_f)
                    {
                        rayPoint = rayPos;
                        var sqr_segment_length = segment_direction.SqrLength;
                        if (sqr_segment_length > XGeometry2D.Eplsilon_f)
                        {
                            var ray_pos_projection = Vector2Df.Dot(in segment_direction, in segment_start_to_pos) / sqr_segment_length;
                            segmentPoint = start_copy + (segment_direction * ray_pos_projection);
                        }
                        else
                        {
                            segmentPoint = start_copy;
                        }
                        return;
                    }
                    rayPoint = rayPos;
                    segmentPoint = segment_start_projection > segment_end_projection ? start_copy : end_copy;
                    return;
                }

                // Collinear
                if (segment_start_projection > -XGeometry2D.Eplsilon_f && segment_end_projection > -XGeometry2D.Eplsilon_f)
                {
                    // Segment intersection
                    rayPoint = segmentPoint = segment_start_projection < segment_end_projection ? start_copy : end_copy;
                    return;
                }
                if (segment_start_projection > -XGeometry2D.Eplsilon_f || segment_end_projection > -XGeometry2D.Eplsilon_f)
                {
                    // Point or segment intersection
                    rayPoint = segmentPoint = rayPos;
                    return;
                }
                // No intersection
                rayPoint = rayPos;
                segmentPoint = segment_start_projection > segment_end_projection ? start_copy : end_copy;
                return;
            }

            // Not parallel
            var ray_distance = perp_dot_b / denominator;
            var segment_distance = perp_dot_a / denominator;
            if (ray_distance < -XGeometry2D.Eplsilon_f ||
                segment_distance < -XGeometry2D.Eplsilon_f || segment_distance > 1 + XGeometry2D.Eplsilon_f)
            {
                // No intersection
                var codirected = Vector2Df.Dot(in rayDir, in segment_direction) > 0;
                Vector2Df segment_end_to_pos;
                if (!codirected)
                {
                    XMath.Swap(ref start_copy, ref end_copy);
                    segment_direction = -segment_direction;
                    segment_end_to_pos = segment_start_to_pos;
                    segment_start_to_pos = rayPos - start_copy;
                    segment_distance = 1 - segment_distance;
                }
                else
                {
                    segment_end_to_pos = rayPos - end_copy;
                }

                var segment_start_projection = -Vector2Df.Dot(in rayDir, in segment_start_to_pos);
                var segment_end_projection = -Vector2Df.Dot(in rayDir, in segment_end_to_pos);
                var segment_start_on_ray = segment_start_projection > -XGeometry2D.Eplsilon_f;
                var segment_end_on_ray = segment_end_projection > -XGeometry2D.Eplsilon_f;
                if (segment_start_on_ray && segment_end_on_ray)
                {
                    if (segment_distance < 0)
                    {
                        rayPoint = rayPos + (rayDir * segment_start_projection);
                        segmentPoint = start_copy;
                        return;
                    }
                    else
                    {
                        rayPoint = rayPos + (rayDir * segment_end_projection);
                        segmentPoint = end_copy;
                        return;
                    }
                }
                else if (!segment_start_on_ray && segment_end_on_ray)
                {
                    if (segment_distance < 0)
                    {
                        rayPoint = rayPos;
                        segmentPoint = start_copy;
                        return;
                    }
                    else if (segment_distance > 1 + XGeometry2D.Eplsilon_f)
                    {
                        rayPoint = rayPos + (rayDir * segment_end_projection);
                        segmentPoint = end_copy;
                        return;
                    }
                    else
                    {
                        rayPoint = rayPos;
                        var pos_projection = Vector2Df.Dot(in segment_direction, in segment_start_to_pos);
                        segmentPoint = start_copy + (segment_direction * pos_projection / segment_direction.SqrLength);
                        return;
                    }
                }
                else
                {
                    // Not on ray
                    rayPoint = rayPos;
                    var pos_projection = Vector2Df.Dot(in segment_direction, in segment_start_to_pos);
                    var sqr_segment_length = segment_direction.SqrLength;
                    if (pos_projection < 0)
                    {
                        segmentPoint = start_copy;
                        return;
                    }
                    else if (pos_projection > sqr_segment_length)
                    {
                        segmentPoint = end_copy;
                        return;
                    }
                    else
                    {
                        segmentPoint = start_copy + (segment_direction * pos_projection / sqr_segment_length);
                        return;
                    }
                }
            }
            // Point intersection
            rayPoint = segmentPoint = start_copy + (segment_direction * segment_distance);
        }
        #endregion

        #region Ray - Circle 
        /// <summary>
        /// Поиск ближайших точек проекции луча и окружности.
        /// </summary>
        /// <param name="ray">Луч.</param>
        /// <param name="circle">Окружность.</param>
        /// <param name="rayPoint">Точка проекции на луче.</param>
        /// <param name="circlePoint">Точка проекции на окружности.</param>
        public static void RayCircle(in Ray2Df ray, in Circle2Df circle, out Vector2Df rayPoint, out Vector2Df circlePoint)
        {
            RayCircle(in ray.Position, in ray.Direction, in circle.Center, circle.Radius, out rayPoint, out circlePoint);
        }

        /// <summary>
        /// Поиск ближайших точек проекции луча и окружности.
        /// </summary>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <param name="circleCenter">Центр окружности.</param>
        /// <param name="circleRadius">Радиус окружности.</param>
        /// <param name="rayPoint">Точка проекции на луче.</param>
        /// <param name="circlePoint">Точка проекции на окружности.</param>
        public static void RayCircle(in Vector2Df rayPos, in Vector2Df rayDir, in Vector2Df circleCenter, float circleRadius,
            out Vector2Df rayPoint, out Vector2Df circlePoint)
        {
            Vector2Df pos_to_center = circleCenter - rayPos;
            var center_projection = Vector2Df.Dot(in rayDir, in pos_to_center);
            if (center_projection + circleRadius < -XGeometry2D.Eplsilon_f)
            {
                // No intersection
                rayPoint = rayPos;
                circlePoint = circleCenter - (pos_to_center.Normalized * circleRadius);
                return;
            }

            var sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
            var sqr_distance_to_intersection = (circleRadius * circleRadius) - sqr_distance_to_line;
            if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
            {
                // No intersection
                if (center_projection < -XGeometry2D.Eplsilon_f)
                {
                    rayPoint = rayPos;
                    circlePoint = circleCenter - (pos_to_center.Normalized * circleRadius);
                    return;
                }
                rayPoint = rayPos + (rayDir * center_projection);
                circlePoint = circleCenter + ((rayPoint - circleCenter).Normalized * circleRadius);
                return;
            }
            if (sqr_distance_to_intersection < XGeometry2D.Eplsilon_f)
            {
                if (center_projection < -XGeometry2D.Eplsilon_f)
                {
                    // No intersection
                    rayPoint = rayPos;
                    circlePoint = circleCenter - (pos_to_center.Normalized * circleRadius);
                    return;
                }
                // Point intersection
                rayPoint = circlePoint = rayPos + (rayDir * center_projection);
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
                    rayPoint = rayPos;
                    circlePoint = circleCenter - (pos_to_center.Normalized * circleRadius);
                    return;
                }

                // Point intersection
                rayPoint = circlePoint = rayPos + (rayDir * distance_b);
                return;
            }

            // Two points intersection
            rayPoint = circlePoint = rayPos + (rayDir * distance_a);
        }
        #endregion Ray-Circle2Df

        #region Segment - Segment 
        /// <summary>
        /// Поиск ближайших точек проекции двух отрезков.
        /// </summary>
        /// <param name="segment1">Первый отрезок.</param>
        /// <param name="segment2">Второй отрезок.</param>
        /// <param name="segment1Point">Точка проекции на первом отрезке.</param>
        /// <param name="segment2Point">Точка проекции на втором отрезке.</param>
        public static void SegmentSegment(in Segment2Df segment1, in Segment2Df segment2, out Vector2Df segment1Point,
            out Vector2Df segment2Point)
        {
            SegmentSegment(in segment1.Start, in segment1.End, in segment2.Start, in segment2.End, out segment1Point, out segment2Point);
        }

        /// <summary>
        /// Поиск ближайших точек проекции двух отрезков.
        /// </summary>
        /// <param name="segment1Start">Начало первого отрезка.</param>
        /// <param name="segment1End">Окончание первого отрезка.</param>
        /// <param name="segment2Start">Начало второго отрезка.</param>
        /// <param name="segment2End">Окончание второго отрезка.</param>
        /// <param name="segment1Point">Точка проекции на первом отрезке.</param>
        /// <param name="segment2Point">Точка проекции на втором отрезке.</param>
        public static void SegmentSegment(in Vector2Df segment1Start, in Vector2Df segment1End,
            in Vector2Df segment2Start, in Vector2Df segment2End, out Vector2Df segment1Point,
            out Vector2Df segment2Point)
        {
            Vector2Df segment2_start_copy = segment2Start;
            Vector2Df segment2_end_copy = segment2End;
            Vector2Df from_2start_to_1start = segment1Start - segment2_start_copy;
            Vector2Df direction1 = segment1End - segment1Start;
            Vector2Df direction2 = segment2_end_copy - segment2_start_copy;
            var segment_1length = direction1.Length;
            var segment_2length = direction2.Length;

            var segment1IsAPoint = segment_1length < XGeometry2D.Eplsilon_f;
            var segment2IsAPoint = segment_2length < XGeometry2D.Eplsilon_f;
            if (segment1IsAPoint && segment2IsAPoint)
            {
                if (segment1Start == segment2_start_copy)
                {
                    segment1Point = segment2Point = segment1Start;
                    return;
                }
                segment1Point = segment1Start;
                segment2Point = segment2_start_copy;
                return;
            }
            if (segment1IsAPoint)
            {
                direction2.Normalize();
                segment1Point = segment1Start;
                segment2Point = PointSegment(in segment1Start, in segment2_start_copy, in segment2_end_copy, in direction2, segment_2length);
                return;
            }
            if (segment2IsAPoint)
            {
                direction1.Normalize();
                segment1Point = PointSegment(in segment2_start_copy, in segment1Start, in segment1End, in direction1, segment_1length);
                segment2Point = segment2_start_copy;
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
                        from_2start_to_1start = segment1Start - segment2_start_copy;
                    }
                    else
                    {
                        from1ATo2B = segment2_end_copy - segment1Start;
                    }
                    var segment2AProjection = -Vector2Df.Dot(direction1, from_2start_to_1start);
                    var segment2BProjection = Vector2Df.Dot(direction1, from1ATo2B);

                    var segment2AIsAfter1A = segment2AProjection > -XGeometry2D.Eplsilon_f;
                    var segment2BIsAfter1A = segment2BProjection > -XGeometry2D.Eplsilon_f;
                    if (!segment2AIsAfter1A && !segment2BIsAfter1A)
                    {
                        //           1A------1B
                        // 2A------2B
                        segment1Point = segment1Start;
                        segment2Point = segment2_end_copy;
                        return;
                    }
                    var segment2AIsBefore1B = segment2AProjection < segment_1length + XGeometry2D.Eplsilon_f;
                    var segment2BIsBefore1B = segment2BProjection < segment_1length + XGeometry2D.Eplsilon_f;
                    if (!segment2AIsBefore1B && !segment2BIsBefore1B)
                    {
                        // 1A------1B
                        //           2A------2B
                        segment1Point = segment1End;
                        segment2Point = segment2_start_copy;
                        return;
                    }

                    if (segment2AIsAfter1A && segment2BIsBefore1B)
                    {
                        // 1A------1B
                        //   2A--2B
                        segment1Point = segment1Start + (direction1 * segment2AProjection);
                        segment2Point = segment2_start_copy;
                        return;
                    }

                    if (segment2AIsAfter1A) // && segment2AIsBefore1B && !segment2BIsBefore1B)
                    {
                        // 1A------1B
                        //     2A------2B
                        segment1Point = segment1Start + (direction1 * segment2AProjection);
                        segment2Point = segment2_start_copy;
                        return;
                    }
                    else
                    {
                        //   1A------1B
                        // 2A----2B
                        // 2A----------2B
                        segment1Point = segment1Start;
                        var segment1AProjection = Vector2Df.Dot(in direction2, in from_2start_to_1start);
                        segment2Point = segment2_start_copy + (direction2 * segment1AProjection);
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
                        SegmentSegmentCollinear(in segment1Start, in segment1End, in segment2_start_copy, out segment1Point, out segment2Point);
                        return;
                    }
                    else
                    {
                        //     1A------1B
                        // 2A------2B
                        SegmentSegmentCollinear(in segment2_start_copy, in segment2_end_copy, in segment1Start, out segment2Point, out segment1Point);
                        return;
                    }
                }
                else
                {
                    // Contradirected
                    var segment2BProjection = Vector2Df.Dot(in direction1, segment2_end_copy - segment1Start);
                    if (segment2BProjection > -XGeometry2D.Eplsilon_f)
                    {
                        // 1A------1B
                        //     2B------2A
                        SegmentSegmentCollinear(in segment1Start, in segment1End, in segment2_end_copy, out segment1Point, out segment2Point);
                        return;
                    }
                    else
                    {
                        //     1A------1B
                        // 2B------2A
                        SegmentSegmentCollinear(in segment2_end_copy, in segment2_start_copy, in segment1Start, out segment2Point, out segment1Point);
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
                    from_2start_to_1start = segment1Start - segment2_start_copy;
                    distance2 = segment_2length - distance2;
                }
                else
                {
                    from1ATo2B = segment2_end_copy - segment1Start;
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
                        segment1Point = segment1Start + (direction1 * segment2AProjection);
                        segment2Point = segment2_start_copy;
                    }
                    else
                    {
                        segment1Point = segment1Start + (direction1 * segment2BProjection);
                        segment2Point = segment2_end_copy;
                    }
                }
                else if (!segment2AOnSegment1 && !segment2BOnSegment1)
                {
                    if (!segment2AIsAfter1A && !segment2BIsBefore1B)
                    {
                        segment1Point = distance1 < -XGeometry2D.Eplsilon_f ? segment1Start : segment1End;
                    }
                    else
                    {
                        // Not on segment
                        segment1Point = segment2AIsAfter1A ? segment1End : segment1Start;
                    }
                    var segment1PointProjection = Vector2Df.Dot(in direction2, segment1Point - segment2_start_copy);
                    segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment_2length);
                    segment2Point = segment2_start_copy + (direction2 * segment1PointProjection);
                }
                else if (segment2AOnSegment1)
                {
                    if (distance2 < -XGeometry2D.Eplsilon_f)
                    {
                        segment1Point = segment1Start + (direction1 * segment2AProjection);
                        segment2Point = segment2_start_copy;
                    }
                    else
                    {
                        segment1Point = segment1End;
                        var segment1PointProjection = Vector2Df.Dot(in direction2, segment1Point - segment2_start_copy);
                        segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment_2length);
                        segment2Point = segment2_start_copy + (direction2 * segment1PointProjection);
                    }
                }
                else
                {
                    if (distance2 > segment_2length + XGeometry2D.Eplsilon_f)
                    {
                        segment1Point = segment1Start + (direction1 * segment2BProjection);
                        segment2Point = segment2_end_copy;
                    }
                    else
                    {
                        segment1Point = segment1Start;
                        var segment1PointProjection = Vector2Df.Dot(in direction2, segment1Point - segment2_start_copy);
                        segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment_2length);
                        segment2Point = segment2_start_copy + (direction2 * segment1PointProjection);
                    }
                }
                return;
            }

            // Point intersection
            segment1Point = segment2Point = segment1Start + (direction1 * distance1);
        }

        /// <summary>
        /// Поиск ближайших точек проекции двух отрезков.
        /// </summary>
        /// <param name="left_a">Начало отрезка слева.</param>
        /// <param name="left_b">Конец отрезка слева.</param>
        /// <param name="right_a">Начало отрезка справа.</param>
        /// <param name="left_point">Точка проекции на отрезке слева.</param>
        /// <param name="right_point">Точка проекции на отрезке справа.</param>
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

        #region Segment - Circle 
        /// <summary>
        /// Поиск ближайших точек проекции отрезков и окружности.
        /// </summary>
        /// <param name="segment">Отрезок.</param>
        /// <param name="circle">Окружность.</param>
        /// <param name="segmentPoint">Точка проекции на отрезки.</param>
        /// <param name="circlePoint">Точка проекции на окружности.</param>
        public static void SegmentCircle(in Segment2Df segment, in Circle2Df circle, out Vector2Df segmentPoint, out Vector2Df circlePoint)
        {
            SegmentCircle(in segment.Start, in segment.End, in circle.Center, circle.Radius, out segmentPoint, out circlePoint);
        }

        /// <summary>
        /// Поиск ближайших точек проекции отрезков и окружности.
        /// </summary>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="circleCenter">Центр окружности.</param>
        /// <param name="circleRadius">Радиус окружности.</param>
        /// <param name="segmentPoint">Точка проекции на отрезки.</param>
        /// <param name="circlePoint">Точка проекции на окружности.</param>
        public static void SegmentCircle(in Vector2Df start, in Vector2Df end, in Vector2Df circleCenter, float circleRadius,
            out Vector2Df segmentPoint, out Vector2Df circlePoint)
        {
            Vector2Df segment_start_to_center = circleCenter - start;
            Vector2Df from_start_to_end = end - start;
            var segment_length = from_start_to_end.Length;
            if (segment_length < XGeometry2D.Eplsilon_f)
            {
                segmentPoint = start;
                var distance_to_point = segment_start_to_center.Length;
                if (distance_to_point < circleRadius + XGeometry2D.Eplsilon_f)
                {
                    if (distance_to_point > circleRadius - XGeometry2D.Eplsilon_f)
                    {
                        circlePoint = segmentPoint;
                        return;
                    }
                    if (distance_to_point < XGeometry2D.Eplsilon_f)
                    {
                        circlePoint = segmentPoint;
                        return;
                    }
                }
                Vector2Df to_point = -segment_start_to_center / distance_to_point;
                circlePoint = circleCenter + (to_point * circleRadius);
                return;
            }

            Vector2Df segment_direction = from_start_to_end.Normalized;
            var center_projection = Vector2Df.Dot(in segment_direction, in segment_start_to_center);
            if (center_projection + circleRadius < -XGeometry2D.Eplsilon_f ||
                center_projection - circleRadius > segment_length + XGeometry2D.Eplsilon_f)
            {
                // No intersection
                if (center_projection < 0)
                {
                    segmentPoint = start;
                    circlePoint = circleCenter - (segment_start_to_center.Normalized * circleRadius);
                    return;
                }
                segmentPoint = end;
                circlePoint = circleCenter - ((circleCenter - end).Normalized * circleRadius);
                return;
            }

            var sqr_distance_to_line = segment_start_to_center.SqrLength - (center_projection * center_projection);
            var sqr_distance_to_intersection = (circleRadius * circleRadius) - sqr_distance_to_line;
            if (sqr_distance_to_intersection < -XGeometry2D.Eplsilon_f)
            {
                // No intersection
                if (center_projection < -XGeometry2D.Eplsilon_f)
                {
                    segmentPoint = start;
                    circlePoint = circleCenter - (segment_start_to_center.Normalized * circleRadius);
                    return;
                }
                if (center_projection > segment_length + XGeometry2D.Eplsilon_f)
                {
                    segmentPoint = end;
                    circlePoint = circleCenter - ((circleCenter - end).Normalized * circleRadius);
                    return;
                }
                segmentPoint = start + (segment_direction * center_projection);
                circlePoint = circleCenter + ((segmentPoint - circleCenter).Normalized * circleRadius);
                return;
            }

            if (sqr_distance_to_intersection < XGeometry2D.Eplsilon_f)
            {
                if (center_projection < -XGeometry2D.Eplsilon_f)
                {
                    // No intersection
                    segmentPoint = start;
                    circlePoint = circleCenter - (segment_start_to_center.Normalized * circleRadius);
                    return;
                }
                if (center_projection > segment_length + XGeometry2D.Eplsilon_f)
                {
                    // No intersection
                    segmentPoint = end;
                    circlePoint = circleCenter - ((circleCenter - end).Normalized * circleRadius);
                    return;
                }
                // Point intersection
                segmentPoint = circlePoint = start + (segment_direction * center_projection);
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
                segmentPoint = circlePoint = start + (segment_direction * distance_a);
                return;
            }
            if (!point_a_is_after_segment_start && !point_b_is_before_segment_end)
            {
                // The segment is inside, but no intersection
                if (distance_a > -(distance_b - segment_length))
                {
                    segmentPoint = start;
                    circlePoint = start + (segment_direction * distance_a);
                    return;
                }
                segmentPoint = end;
                circlePoint = start + (segment_direction * distance_b);
                return;
            }

            var point_a_is_before_segment_end = distance_a < segment_length + XGeometry2D.Eplsilon_f;
            if (point_a_is_after_segment_start && point_a_is_before_segment_end)
            {
                // Point A intersection
                segmentPoint = circlePoint = start + (segment_direction * distance_a);
                return;
            }
            var point_b_is_after_segment_start = distance_b > -XGeometry2D.Eplsilon_f;
            if (point_b_is_after_segment_start && point_b_is_before_segment_end)
            {
                // Point B intersection
                segmentPoint = circlePoint = start + (segment_direction * distance_b);
                return;
            }

            // No intersection
            if (center_projection < 0)
            {
                segmentPoint = start;
                circlePoint = circleCenter - (segment_start_to_center.Normalized * circleRadius);
                return;
            }
            segmentPoint = end;
            circlePoint = circleCenter - ((circleCenter - end).Normalized * circleRadius);
        }
        #endregion

        #region Circle - Circle 
        /// <summary>
        /// Поиск ближайших точек проекции двух окружностей.
        /// </summary>
        /// <param name="circleA">Первая окружность.</param>
        /// <param name="circleB">Вторая окружность.</param>
        /// <param name="pointA">Точка проекции на первую окружность.</param>
        /// <param name="pointB">Точка проекции на вторую окружность.</param>
        public static void CircleCircle(in Circle2Df circleA, in Circle2Df circleB, out Vector2Df pointA, out Vector2Df pointB)
        {
            CircleCircle(circleA.Center, circleA.Radius, circleB.Center, circleB.Radius, out pointA, out pointB);
        }

        /// <summary>
        /// Поиск ближайших точек проекции двух окружностей.
        /// </summary>
        /// <param name="centerA">Центр первой окружности.</param>
        /// <param name="radiusA">Радиус первой окружности.</param>
        /// <param name="centerB">Центр второй окружности.</param>
        /// <param name="radiusB">Радиус второй окружности.</param>
        /// <param name="pointA">Точка проекции на первую окружность.</param>
        /// <param name="pointB">Точка проекции на вторую окружность.</param>
        public static void CircleCircle(in Vector2Df centerA, float radiusA, in Vector2Df centerB, float radiusB,
            out Vector2Df pointA, out Vector2Df pointB)
        {
            Vector2Df from_b_to_a = (centerA - centerB).Normalized;
            pointA = centerA - (from_b_to_a * radiusA);
            pointB = centerB + (from_b_to_a * radiusB);
        }
        #endregion
    }
    /**@}*/
}