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
            var pointProjection = Vector2Df.Dot(in rayDir, point - rayPos);
            if (pointProjection <= 0)
            {
                // Мы находимся по другую сторону луча
                distance = 0;
                return rayPos;
            }

            // In theory, sqrMagnitude should be 1, but in practice this division helps with numerical stability
            distance = pointProjection / rayDir.SqrLength;
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
            var segmentDirection = end - start;
            var sqrSegmentLength = segmentDirection.SqrLength;
            if (sqrSegmentLength < XGeometry2D.Epsilon_f)
            {
                // The segment is a point
                normalizeDistance = 0;
                return start;
            }

            var pointProjection = Vector2Df.Dot(in segmentDirection, point - start);
            if (pointProjection <= 0)
            {
                normalizeDistance = 0;
                return start;
            }
            if (pointProjection >= sqrSegmentLength)
            {
                normalizeDistance = 1;
                return end;
            }

            normalizeDistance = pointProjection / sqrSegmentLength;
            return start + (segmentDirection * normalizeDistance);
        }

        /// <summary>
        /// Проекция точки на отрезок.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="segmentDirection">Направление отрезка.</param>
        /// <param name="segmentLength">Длина отрезка.</param>
        /// <returns>Спроецированная точка.</returns>
        private static Vector2Df PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end,
            in Vector2Df segmentDirection, float segmentLength)
        {
            var pointProjection = Vector2Df.Dot(in segmentDirection, point - start);
            if (pointProjection <= 0)
            {
                return start;
            }
            if (pointProjection >= segmentLength)
            {
                return end;
            }
            return start + (segmentDirection * pointProjection);
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
            var posBToA = posA - posB;
            var denominator = Vector2Df.DotPerp(in dirA, in dirB);
            var perpDotB = Vector2Df.DotPerp(in dirB, in posBToA);

            if (Math.Abs(denominator) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                if (Math.Abs(perpDotB) > XGeometry2D.Epsilon_f ||
                    Math.Abs(Vector2Df.DotPerp(in dirA, in posBToA)) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    pointA = posA;
                    pointB = posB + (dirB * Vector2Df.Dot(in dirB, posBToA));
                    return;
                }

                // Collinear
                pointA = pointB = posA;
                return;
            }

            // Not parallel
            pointA = pointB = posA + (dirA * (perpDotB / denominator));
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
            var rayPosToLinePos = linePos - rayPos;
            var denominator = Vector2Df.DotPerp(in lineDir, in rayDir);
            var perpDotA = Vector2Df.DotPerp(in lineDir, in rayPosToLinePos);

            if (Math.Abs(denominator) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                var perpDotB = Vector2Df.DotPerp(in rayDir, in rayPosToLinePos);
                if (Math.Abs(perpDotA) > XGeometry2D.Epsilon_f || Math.Abs(perpDotB) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    var rayPosProjection = Vector2Df.Dot(in lineDir, in rayPosToLinePos);
                    linePoint = linePos - (lineDir * rayPosProjection);
                    rayPoint = rayPos;
                    return;
                }
                // Collinear
                linePoint = rayPoint = rayPos;
                return;
            }

            // Not parallel
            var rayDistance = perpDotA / denominator;
            if (rayDistance < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                var rayPosProjection = Vector2Df.Dot(in lineDir, in rayPosToLinePos);
                linePoint = linePos - (lineDir * rayPosProjection);
                rayPoint = rayPos;
                return;
            }

            // Point intersection
            linePoint = rayPoint = rayPos + (rayDir * rayDistance);
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
            var segmentDirection = end - start;
            var segmentStartToPos = linePos - start;
            var denominator = Vector2Df.DotPerp(in lineDir, in segmentDirection);
            var perpDotStart = Vector2Df.DotPerp(in lineDir, in segmentStartToPos);

            if (Math.Abs(denominator) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                var codirected = Vector2Df.Dot(in lineDir, in segmentDirection) > 0;

                // Normalized direction gives more stable results 
                var perpDotEnd = Vector2Df.DotPerp(segmentDirection.Normalized, segmentStartToPos);
                if (Math.Abs(perpDotStart) > XGeometry2D.Epsilon_f || Math.Abs(perpDotEnd) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    if (codirected)
                    {
                        var segmentStartProjection = Vector2Df.Dot(in lineDir, in segmentStartToPos);
                        linePoint = linePos - (lineDir * segmentStartProjection);
                        segmentPoint = start;
                    }
                    else
                    {
                        var segmentEndProjection = Vector2Df.Dot(in lineDir, linePos - end);
                        linePoint = linePos - (lineDir * segmentEndProjection);
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
            var segmentDistance = perpDotStart / denominator;
            if (segmentDistance < -XGeometry2D.Epsilon_f || segmentDistance > 1 + XGeometry2D.Epsilon_f)
            {
                // No intersection
                segmentPoint = start + (segmentDirection * XMath.Clamp01(segmentDistance));
                var segmentPointProjection = Vector2Df.Dot(in lineDir, segmentPoint - linePos);
                linePoint = linePos + (lineDir * segmentPointProjection);
                return;
            }
            // Point intersection
            linePoint = segmentPoint = start + (segmentDirection * segmentDistance);
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
            var posToCenter = circleCenter - linePos;
            var centerProjection = Vector2Df.Dot(in lineDir, in posToCenter);
            var sqrDistanceToLine = posToCenter.SqrLength - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (circleRadius * circleRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                linePoint = linePos + (lineDir * centerProjection);
                circlePoint = circleCenter + ((linePoint - circleCenter).Normalized * circleRadius);
                return;
            }
            if (sqrDistanceToIntersection < XGeometry2D.Epsilon_f)
            {
                // Point intersection
                linePoint = circlePoint = linePos + (lineDir * centerProjection);
                return;
            }

            // Two points intersection
            var distanceToIntersection = XMath.Sqrt(sqrDistanceToIntersection);
            var distanceA = centerProjection - distanceToIntersection;
            linePoint = circlePoint = linePos + (lineDir * distanceA);
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
            var posBToA = posA - posB;
            var denominator = Vector2Df.DotPerp(in dirA, in dirB);
            var perpDotA = Vector2Df.DotPerp(in dirA, in posBToA);
            var perpDotB = Vector2Df.DotPerp(in dirB, in posBToA);
            var codirected = Vector2Df.Dot(in dirA, in dirB) > 0;

            if (Math.Abs(denominator) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                var originBProjection = Vector2Df.Dot(dirA, posBToA);
                if (Math.Abs(perpDotA) > XGeometry2D.Epsilon_f || Math.Abs(perpDotB) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    if (codirected)
                    {
                        if (originBProjection > -XGeometry2D.Epsilon_f)
                        {
                            // Projection of pos_a is on ray_b
                            pointA = posA;
                            pointB = posB + (dirA * originBProjection);
                            return;
                        }
                        else
                        {
                            pointA = posA - (dirA * originBProjection);
                            pointB = posB;
                            return;
                        }
                    }
                    else
                    {
                        if (originBProjection > 0)
                        {
                            pointA = posA;
                            pointB = posB;
                            return;
                        }
                        else
                        {
                            // Projection of pos_a is on ray_b
                            pointA = posA;
                            pointB = posB + (dirA * originBProjection);
                            return;
                        }
                    }
                }
                // Collinear

                if (codirected)
                {
                    // Ray intersection
                    if (originBProjection > -XGeometry2D.Epsilon_f)
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
                    if (originBProjection > 0)
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
            var distanceA = perpDotB / denominator;
            var distanceB = perpDotA / denominator;
            if (distanceA < -XGeometry2D.Epsilon_f || distanceB < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                if (codirected)
                {
                    var originAProjection = Vector2Df.Dot(in dirB, in posBToA);
                    if (originAProjection > -XGeometry2D.Epsilon_f)
                    {
                        pointA = posA;
                        pointB = posB + (dirB * originAProjection);
                        return;
                    }
                    var originBProjection = -Vector2Df.Dot(in dirA, in posBToA);
                    if (originBProjection > -XGeometry2D.Epsilon_f)
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
                    if (distanceA > -XGeometry2D.Epsilon_f)
                    {
                        var originBProjection = -Vector2Df.Dot(in dirA, in posBToA);
                        if (originBProjection > -XGeometry2D.Epsilon_f)
                        {
                            pointA = posA + (dirA * originBProjection);
                            pointB = posB;
                            return;
                        }
                    }
                    else if (distanceB > -XGeometry2D.Epsilon_f)
                    {
                        var originAProjection = Vector2Df.Dot(in dirB, in posBToA);
                        if (originAProjection > -XGeometry2D.Epsilon_f)
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
            pointA = pointB = posA + (dirA * distanceA);
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
            var startCopy = start;
            var endCopy = end;
            var segmentDirection = endCopy - startCopy;
            var segmentStartToPos = rayPos - startCopy;
            var denominator = Vector2Df.DotPerp(in rayDir, in segmentDirection);
            var perpDotA = Vector2Df.DotPerp(in rayDir, in segmentStartToPos);
            // Normalized direction gives more stable results 
            var perpDotB = Vector2Df.DotPerp(segmentDirection.Normalized, segmentStartToPos);

            if (Math.Abs(denominator) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                var segmentStartProjection = -Vector2Df.Dot(rayDir, segmentStartToPos);
                var rayPosToSegmentB = endCopy - rayPos;
                var segmentEndProjection = Vector2Df.Dot(rayDir, rayPosToSegmentB);
                if (Math.Abs(perpDotA) > XGeometry2D.Epsilon_f || Math.Abs(perpDotB) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    if (segmentStartProjection > -XGeometry2D.Epsilon_f && segmentEndProjection > -XGeometry2D.Epsilon_f)
                    {
                        if (segmentStartProjection < segmentEndProjection)
                        {
                            rayPoint = rayPos + (rayDir * segmentStartProjection);
                            segmentPoint = startCopy;
                            return;
                        }
                        else
                        {
                            rayPoint = rayPos + (rayDir * segmentEndProjection);
                            segmentPoint = endCopy;
                            return;
                        }
                    }
                    if (segmentStartProjection > -XGeometry2D.Epsilon_f || segmentEndProjection > -XGeometry2D.Epsilon_f)
                    {
                        rayPoint = rayPos;
                        var sqrSegmentLength = segmentDirection.SqrLength;
                        if (sqrSegmentLength > XGeometry2D.Epsilon_f)
                        {
                            var rayPosProjection = Vector2Df.Dot(in segmentDirection, in segmentStartToPos) / sqrSegmentLength;
                            segmentPoint = startCopy + (segmentDirection * rayPosProjection);
                        }
                        else
                        {
                            segmentPoint = startCopy;
                        }
                        return;
                    }
                    rayPoint = rayPos;
                    segmentPoint = segmentStartProjection > segmentEndProjection ? startCopy : endCopy;
                    return;
                }

                // Collinear
                if (segmentStartProjection > -XGeometry2D.Epsilon_f && segmentEndProjection > -XGeometry2D.Epsilon_f)
                {
                    // Segment intersection
                    rayPoint = segmentPoint = segmentStartProjection < segmentEndProjection ? startCopy : endCopy;
                    return;
                }
                if (segmentStartProjection > -XGeometry2D.Epsilon_f || segmentEndProjection > -XGeometry2D.Epsilon_f)
                {
                    // Point or segment intersection
                    rayPoint = segmentPoint = rayPos;
                    return;
                }
                // No intersection
                rayPoint = rayPos;
                segmentPoint = segmentStartProjection > segmentEndProjection ? startCopy : endCopy;
                return;
            }

            // Not parallel
            var rayDistance = perpDotB / denominator;
            var segmentDistance = perpDotA / denominator;
            if (rayDistance < -XGeometry2D.Epsilon_f ||
                segmentDistance < -XGeometry2D.Epsilon_f || segmentDistance > 1 + XGeometry2D.Epsilon_f)
            {
                // No intersection
                var codirected = Vector2Df.Dot(in rayDir, in segmentDirection) > 0;
                Vector2Df segmentEndToPos;
                if (!codirected)
                {
                    XMath.Swap(ref startCopy, ref endCopy);
                    segmentDirection = -segmentDirection;
                    segmentEndToPos = segmentStartToPos;
                    segmentStartToPos = rayPos - startCopy;
                    segmentDistance = 1 - segmentDistance;
                }
                else
                {
                    segmentEndToPos = rayPos - endCopy;
                }

                var segmentStartProjection = -Vector2Df.Dot(in rayDir, in segmentStartToPos);
                var segmentEndProjection = -Vector2Df.Dot(in rayDir, in segmentEndToPos);
                var segmentStartOnRay = segmentStartProjection > -XGeometry2D.Epsilon_f;
                var segmentEndOnRay = segmentEndProjection > -XGeometry2D.Epsilon_f;
                if (segmentStartOnRay && segmentEndOnRay)
                {
                    if (segmentDistance < 0)
                    {
                        rayPoint = rayPos + (rayDir * segmentStartProjection);
                        segmentPoint = startCopy;
                        return;
                    }
                    else
                    {
                        rayPoint = rayPos + (rayDir * segmentEndProjection);
                        segmentPoint = endCopy;
                        return;
                    }
                }
                else if (!segmentStartOnRay && segmentEndOnRay)
                {
                    if (segmentDistance < 0)
                    {
                        rayPoint = rayPos;
                        segmentPoint = startCopy;
                        return;
                    }
                    else if (segmentDistance > 1 + XGeometry2D.Epsilon_f)
                    {
                        rayPoint = rayPos + (rayDir * segmentEndProjection);
                        segmentPoint = endCopy;
                        return;
                    }
                    else
                    {
                        rayPoint = rayPos;
                        var posProjection = Vector2Df.Dot(in segmentDirection, in segmentStartToPos);
                        segmentPoint = startCopy + (segmentDirection * posProjection / segmentDirection.SqrLength);
                        return;
                    }
                }
                else
                {
                    // Not on ray
                    rayPoint = rayPos;
                    var posProjection = Vector2Df.Dot(in segmentDirection, in segmentStartToPos);
                    var sqrSegmentLength = segmentDirection.SqrLength;
                    if (posProjection < 0)
                    {
                        segmentPoint = startCopy;
                        return;
                    }
                    else if (posProjection > sqrSegmentLength)
                    {
                        segmentPoint = endCopy;
                        return;
                    }
                    else
                    {
                        segmentPoint = startCopy + (segmentDirection * posProjection / sqrSegmentLength);
                        return;
                    }
                }
            }
            // Point intersection
            rayPoint = segmentPoint = startCopy + (segmentDirection * segmentDistance);
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
            var posToCenter = circleCenter - rayPos;
            var centerProjection = Vector2Df.Dot(in rayDir, in posToCenter);
            if (centerProjection + circleRadius < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                rayPoint = rayPos;
                circlePoint = circleCenter - (posToCenter.Normalized * circleRadius);
                return;
            }

            var sqrDistanceToLine = posToCenter.SqrLength - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (circleRadius * circleRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < -XGeometry2D.Epsilon_f)
                {
                    rayPoint = rayPos;
                    circlePoint = circleCenter - (posToCenter.Normalized * circleRadius);
                    return;
                }
                rayPoint = rayPos + (rayDir * centerProjection);
                circlePoint = circleCenter + ((rayPoint - circleCenter).Normalized * circleRadius);
                return;
            }
            if (sqrDistanceToIntersection < XGeometry2D.Epsilon_f)
            {
                if (centerProjection < -XGeometry2D.Epsilon_f)
                {
                    // No intersection
                    rayPoint = rayPos;
                    circlePoint = circleCenter - (posToCenter.Normalized * circleRadius);
                    return;
                }
                // Point intersection
                rayPoint = circlePoint = rayPos + (rayDir * centerProjection);
                return;
            }

            // Line intersection
            var distanceToIntersection = XMath.Sqrt(sqrDistanceToIntersection);
            var distanceA = centerProjection - distanceToIntersection;

            if (distanceA < -XGeometry2D.Epsilon_f)
            {
                var distanceB = centerProjection + distanceToIntersection;
                if (distanceB < -XGeometry2D.Epsilon_f)
                {
                    // No intersection
                    rayPoint = rayPos;
                    circlePoint = circleCenter - (posToCenter.Normalized * circleRadius);
                    return;
                }

                // Point intersection
                rayPoint = circlePoint = rayPos + (rayDir * distanceB);
                return;
            }

            // Two points intersection
            rayPoint = circlePoint = rayPos + (rayDir * distanceA);
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
            var segment2StartCopy = segment2Start;
            var segment2EndCopy = segment2End;
            var from2StartTo1Start = segment1Start - segment2StartCopy;
            var direction1 = segment1End - segment1Start;
            var direction2 = segment2EndCopy - segment2StartCopy;
            var segment1Length = direction1.Length;
            var segment2Length = direction2.Length;

            var segment1IsAPoint = segment1Length < XGeometry2D.Epsilon_f;
            var segment2IsAPoint = segment2Length < XGeometry2D.Epsilon_f;
            if (segment1IsAPoint && segment2IsAPoint)
            {
                if (segment1Start == segment2StartCopy)
                {
                    segment1Point = segment2Point = segment1Start;
                    return;
                }
                segment1Point = segment1Start;
                segment2Point = segment2StartCopy;
                return;
            }
            if (segment1IsAPoint)
            {
                direction2.Normalize();
                segment1Point = segment1Start;
                segment2Point = PointSegment(in segment1Start, in segment2StartCopy, in segment2EndCopy, in direction2, segment2Length);
                return;
            }
            if (segment2IsAPoint)
            {
                direction1.Normalize();
                segment1Point = PointSegment(in segment2StartCopy, in segment1Start, in segment1End, in direction1, segment1Length);
                segment2Point = segment2StartCopy;
                return;
            }

            direction1.Normalize();
            direction2.Normalize();
            var denominator = Vector2Df.DotPerp(in direction1, in direction2);
            var perpDot1 = Vector2Df.DotPerp(in direction1, in from2StartTo1Start);
            var perpDot2 = Vector2Df.DotPerp(in direction2, in from2StartTo1Start);

            if (Math.Abs(denominator) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                var codirected = Vector2Df.Dot(direction1, direction2) > 0;
                if (Math.Abs(perpDot1) > XGeometry2D.Epsilon_f || Math.Abs(perpDot2) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    Vector2Df from1ATo2B;
                    if (!codirected)
                    {
                        XMath.Swap(ref segment2StartCopy, ref segment2EndCopy);
                        direction2 = -direction2;
                        from1ATo2B = -from2StartTo1Start;
                        from2StartTo1Start = segment1Start - segment2StartCopy;
                    }
                    else
                    {
                        from1ATo2B = segment2EndCopy - segment1Start;
                    }
                    var segment2AProjection = -Vector2Df.Dot(direction1, from2StartTo1Start);
                    var segment2BProjection = Vector2Df.Dot(direction1, from1ATo2B);

                    var segment2AIsAfter1A = segment2AProjection > -XGeometry2D.Epsilon_f;
                    var segment2BIsAfter1A = segment2BProjection > -XGeometry2D.Epsilon_f;
                    if (!segment2AIsAfter1A && !segment2BIsAfter1A)
                    {
                        //           1A------1B
                        // 2A------2B
                        segment1Point = segment1Start;
                        segment2Point = segment2EndCopy;
                        return;
                    }
                    var segment2AIsBefore1B = segment2AProjection < segment1Length + XGeometry2D.Epsilon_f;
                    var segment2BIsBefore1B = segment2BProjection < segment1Length + XGeometry2D.Epsilon_f;
                    if (!segment2AIsBefore1B && !segment2BIsBefore1B)
                    {
                        // 1A------1B
                        //           2A------2B
                        segment1Point = segment1End;
                        segment2Point = segment2StartCopy;
                        return;
                    }

                    if (segment2AIsAfter1A && segment2BIsBefore1B)
                    {
                        // 1A------1B
                        //   2A--2B
                        segment1Point = segment1Start + (direction1 * segment2AProjection);
                        segment2Point = segment2StartCopy;
                        return;
                    }

                    if (segment2AIsAfter1A) // && segment2AIsBefore1B && !segment2BIsBefore1B)
                    {
                        // 1A------1B
                        //     2A------2B
                        segment1Point = segment1Start + (direction1 * segment2AProjection);
                        segment2Point = segment2StartCopy;
                        return;
                    }
                    else
                    {
                        //   1A------1B
                        // 2A----2B
                        // 2A----------2B
                        segment1Point = segment1Start;
                        var segment1AProjection = Vector2Df.Dot(in direction2, in from2StartTo1Start);
                        segment2Point = segment2StartCopy + (direction2 * segment1AProjection);
                        return;
                    }
                }
                // Collinear

                if (codirected)
                {
                    // Codirected
                    var segment2AProjection = -Vector2Df.Dot(in direction1, in from2StartTo1Start);
                    if (segment2AProjection > -XGeometry2D.Epsilon_f)
                    {
                        // 1A------1B
                        //     2A------2B
                        SegmentSegmentCollinear(in segment1Start, in segment1End, in segment2StartCopy, out segment1Point, out segment2Point);
                        return;
                    }
                    else
                    {
                        //     1A------1B
                        // 2A------2B
                        SegmentSegmentCollinear(in segment2StartCopy, in segment2EndCopy, in segment1Start, out segment2Point, out segment1Point);
                        return;
                    }
                }
                else
                {
                    // Contradirected
                    var segment2BProjection = Vector2Df.Dot(in direction1, segment2EndCopy - segment1Start);
                    if (segment2BProjection > -XGeometry2D.Epsilon_f)
                    {
                        // 1A------1B
                        //     2B------2A
                        SegmentSegmentCollinear(in segment1Start, in segment1End, in segment2EndCopy, out segment1Point, out segment2Point);
                        return;
                    }
                    else
                    {
                        //     1A------1B
                        // 2B------2A
                        SegmentSegmentCollinear(in segment2EndCopy, in segment2StartCopy, in segment1Start, out segment2Point, out segment1Point);
                        return;
                    }
                }
            }

            // Not parallel
            var distance1 = perpDot2 / denominator;
            var distance2 = perpDot1 / denominator;
            if (distance1 < -XGeometry2D.Epsilon_f || distance1 > segment1Length + XGeometry2D.Epsilon_f ||
                distance2 < -XGeometry2D.Epsilon_f || distance2 > segment2Length + XGeometry2D.Epsilon_f)
            {
                // No intersection
                var codirected = Vector2Df.Dot(in direction1, in direction2) > 0;
                Vector2Df from1ATo2B;
                if (!codirected)
                {
                    XMath.Swap(ref segment2StartCopy, ref segment2EndCopy);
                    direction2 = -direction2;
                    from1ATo2B = -from2StartTo1Start;
                    from2StartTo1Start = segment1Start - segment2StartCopy;
                    distance2 = segment2Length - distance2;
                }
                else
                {
                    from1ATo2B = segment2EndCopy - segment1Start;
                }

                var segment2AProjection = -Vector2Df.Dot(direction1, from2StartTo1Start);
                var segment2BProjection = Vector2Df.Dot(direction1, from1ATo2B);

                var segment2AIsAfter1A = segment2AProjection > -XGeometry2D.Epsilon_f;
                var segment2BIsBefore1B = segment2BProjection < segment1Length + XGeometry2D.Epsilon_f;
                var segment2AOnSegment1 = segment2AIsAfter1A && segment2AProjection < segment1Length + XGeometry2D.Epsilon_f;
                var segment2BOnSegment1 = segment2BProjection > -XGeometry2D.Epsilon_f && segment2BIsBefore1B;
                if (segment2AOnSegment1 && segment2BOnSegment1)
                {
                    if (distance2 < -XGeometry2D.Epsilon_f)
                    {
                        segment1Point = segment1Start + (direction1 * segment2AProjection);
                        segment2Point = segment2StartCopy;
                    }
                    else
                    {
                        segment1Point = segment1Start + (direction1 * segment2BProjection);
                        segment2Point = segment2EndCopy;
                    }
                }
                else if (!segment2AOnSegment1 && !segment2BOnSegment1)
                {
                    if (!segment2AIsAfter1A && !segment2BIsBefore1B)
                    {
                        segment1Point = distance1 < -XGeometry2D.Epsilon_f ? segment1Start : segment1End;
                    }
                    else
                    {
                        // Not on segment
                        segment1Point = segment2AIsAfter1A ? segment1End : segment1Start;
                    }
                    var segment1PointProjection = Vector2Df.Dot(in direction2, segment1Point - segment2StartCopy);
                    segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment2Length);
                    segment2Point = segment2StartCopy + (direction2 * segment1PointProjection);
                }
                else if (segment2AOnSegment1)
                {
                    if (distance2 < -XGeometry2D.Epsilon_f)
                    {
                        segment1Point = segment1Start + (direction1 * segment2AProjection);
                        segment2Point = segment2StartCopy;
                    }
                    else
                    {
                        segment1Point = segment1End;
                        var segment1PointProjection = Vector2Df.Dot(in direction2, segment1Point - segment2StartCopy);
                        segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment2Length);
                        segment2Point = segment2StartCopy + (direction2 * segment1PointProjection);
                    }
                }
                else
                {
                    if (distance2 > segment2Length + XGeometry2D.Epsilon_f)
                    {
                        segment1Point = segment1Start + (direction1 * segment2BProjection);
                        segment2Point = segment2EndCopy;
                    }
                    else
                    {
                        segment1Point = segment1Start;
                        var segment1PointProjection = Vector2Df.Dot(in direction2, segment1Point - segment2StartCopy);
                        segment1PointProjection = XMath.Clamp(segment1PointProjection, 0, segment2Length);
                        segment2Point = segment2StartCopy + (direction2 * segment1PointProjection);
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
            var leftDirection = left_b - left_a;
            var rightAProjection = Vector2Df.Dot(leftDirection.Normalized, right_a - left_b);
            if (Math.Abs(rightAProjection) < XGeometry2D.Epsilon_f)
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
            var segmentStartToCenter = circleCenter - start;
            var fromStartToEnd = end - start;
            var segmentLength = fromStartToEnd.Length;
            if (segmentLength < XGeometry2D.Epsilon_f)
            {
                segmentPoint = start;
                var distanceToPoint = segmentStartToCenter.Length;
                if (distanceToPoint < circleRadius + XGeometry2D.Epsilon_f)
                {
                    if (distanceToPoint > circleRadius - XGeometry2D.Epsilon_f)
                    {
                        circlePoint = segmentPoint;
                        return;
                    }
                    if (distanceToPoint < XGeometry2D.Epsilon_f)
                    {
                        circlePoint = segmentPoint;
                        return;
                    }
                }
                var toPoint = -segmentStartToCenter / distanceToPoint;
                circlePoint = circleCenter + (toPoint * circleRadius);
                return;
            }

            var segmentDirection = fromStartToEnd.Normalized;
            var centerProjection = Vector2Df.Dot(in segmentDirection, in segmentStartToCenter);
            if (centerProjection + circleRadius < -XGeometry2D.Epsilon_f ||
                centerProjection - circleRadius > segmentLength + XGeometry2D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < 0)
                {
                    segmentPoint = start;
                    circlePoint = circleCenter - (segmentStartToCenter.Normalized * circleRadius);
                    return;
                }
                segmentPoint = end;
                circlePoint = circleCenter - ((circleCenter - end).Normalized * circleRadius);
                return;
            }

            var sqrDistanceToLine = segmentStartToCenter.SqrLength - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (circleRadius * circleRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < -XGeometry2D.Epsilon_f)
                {
                    segmentPoint = start;
                    circlePoint = circleCenter - (segmentStartToCenter.Normalized * circleRadius);
                    return;
                }
                if (centerProjection > segmentLength + XGeometry2D.Epsilon_f)
                {
                    segmentPoint = end;
                    circlePoint = circleCenter - ((circleCenter - end).Normalized * circleRadius);
                    return;
                }
                segmentPoint = start + (segmentDirection * centerProjection);
                circlePoint = circleCenter + ((segmentPoint - circleCenter).Normalized * circleRadius);
                return;
            }

            if (sqrDistanceToIntersection < XGeometry2D.Epsilon_f)
            {
                if (centerProjection < -XGeometry2D.Epsilon_f)
                {
                    // No intersection
                    segmentPoint = start;
                    circlePoint = circleCenter - (segmentStartToCenter.Normalized * circleRadius);
                    return;
                }
                if (centerProjection > segmentLength + XGeometry2D.Epsilon_f)
                {
                    // No intersection
                    segmentPoint = end;
                    circlePoint = circleCenter - ((circleCenter - end).Normalized * circleRadius);
                    return;
                }
                // Point intersection
                segmentPoint = circlePoint = start + (segmentDirection * centerProjection);
                return;
            }

            // Line intersection
            var distanceToIntersection = XMath.Sqrt(sqrDistanceToIntersection);
            var distanceA = centerProjection - distanceToIntersection;
            var distanceB = centerProjection + distanceToIntersection;

            var pointAIsAfterSegmentStart = distanceA > -XGeometry2D.Epsilon_f;
            var pointBIsBeforeSegmentEnd = distanceB < segmentLength + XGeometry2D.Epsilon_f;

            if (pointAIsAfterSegmentStart && pointBIsBeforeSegmentEnd)
            {
                segmentPoint = circlePoint = start + (segmentDirection * distanceA);
                return;
            }
            if (!pointAIsAfterSegmentStart && !pointBIsBeforeSegmentEnd)
            {
                // The segment is inside, but no intersection
                if (distanceA > -(distanceB - segmentLength))
                {
                    segmentPoint = start;
                    circlePoint = start + (segmentDirection * distanceA);
                    return;
                }
                segmentPoint = end;
                circlePoint = start + (segmentDirection * distanceB);
                return;
            }

            var pointAIsBeforeSegmentEnd = distanceA < segmentLength + XGeometry2D.Epsilon_f;
            if (pointAIsAfterSegmentStart && pointAIsBeforeSegmentEnd)
            {
                // Point A intersection
                segmentPoint = circlePoint = start + (segmentDirection * distanceA);
                return;
            }
            var pointBIsAfterSegmentStart = distanceB > -XGeometry2D.Epsilon_f;
            if (pointBIsAfterSegmentStart && pointBIsBeforeSegmentEnd)
            {
                // Point B intersection
                segmentPoint = circlePoint = start + (segmentDirection * distanceB);
                return;
            }

            // No intersection
            if (centerProjection < 0)
            {
                segmentPoint = start;
                circlePoint = circleCenter - (segmentStartToCenter.Normalized * circleRadius);
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
            var fromBToA = (centerA - centerB).Normalized;
            pointA = centerA - (fromBToA * radiusA);
            pointB = centerB + (fromBToA * radiusB);
        }
        #endregion
    }
    /**@}*/
}