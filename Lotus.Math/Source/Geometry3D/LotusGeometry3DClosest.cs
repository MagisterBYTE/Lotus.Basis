namespace Lotus.Maths
{
    /** \addtogroup MathGeometry3D
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы нахождения ближайших точек пересечения основных геометрических тел/примитивов.
    /// </summary>
    public static class XClosest3D
    {
        #region Point - Line 
        /// <summary>
        /// Проекция точки на линию.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="line">Линия.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector3Df PointLine(in Vector3Df point, in Line3Df line)
        {
            return PointLine(in point, in line.Position, in line.Direction, out _);
        }

        /// <summary>
        /// Проекция точки на линию.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="line">Линия.</param>
        /// <param name="distance">Расстояние от начала линии до спроецированной точки.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector3Df PointLine(in Vector3Df point, in Line3Df line, out float distance)
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
        public static Vector3Df PointLine(in Vector3Df point, in Vector3Df linePos, in Vector3Df lineDir)
        {
            return PointLine(in point, in linePos, in lineDir, out _);
        }

        /// <summary>
        /// Проекция точки на линию.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <param name="distance">Расстояние от начала линии до спроецированной точки.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector3Df PointLine(in Vector3Df point, in Vector3Df linePos, in Vector3Df lineDir, out float distance)
        {
            // In theory, SqrLength should be 1, but in practice this division helps with numerical stability
            distance = Vector3Df.Dot(lineDir, point - linePos) / lineDir.SqrLength;
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
        public static Vector3Df PointRay(in Vector3Df point, in Ray3Df ray)
        {
            return PointRay(in point, in ray.Position, in ray.Direction, out _);
        }

        /// <summary>
        /// Проекция точки на луч.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="ray">Луч.</param>
        /// <param name="distance">Расстояние от начала луча до спроецированной точки.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector3Df PointRay(in Vector3Df point, in Ray3Df ray, out float distance)
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
        public static Vector3Df PointRay(in Vector3Df point, in Vector3Df rayPos, in Vector3Df rayDir)
        {
            return PointRay(in point, in rayPos, in rayDir, out _);
        }

        /// <summary>
        /// Проекция точки на луч.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <param name="distance">Расстояние от начала луча до спроецированной точки.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector3Df PointRay(in Vector3Df point, in Vector3Df rayPos, in Vector3Df rayDir, out float distance)
        {
            var toPoint = point - rayPos;
            var pointProjection = Vector3Df.Dot(in rayDir, in toPoint);
            if (pointProjection <= 0)
            {
                distance = 0;
                return rayPos;
            }

            // In theory, SqrLength should be 1, but in practice this division helps with numerical stability
            distance = pointProjection / rayDir.SqrLength;
            return rayPos + (rayDir * distance);
        }
        #endregion

        #region Point - Segment 
        /// <summary>
        /// Проекция точки на отрезок.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="segment">Отрезок.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector3Df PointSegment(in Vector3Df point, in Segment3Df segment)
        {
            return PointSegment(in point, in segment.Start, in segment.End, out _);
        }

        /// <summary>
        /// Проекция точки на отрезок.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="segment">Отрезок.</param>
        /// <param name="normalizeDistance">Нормализованная позиция проецируемой точки от начала отрезка.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector3Df PointSegment(in Vector3Df point, in Segment3Df segment, out float normalizeDistance)
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
        public static Vector3Df PointSegment(in Vector3Df point, in Vector3Df start, in Vector3Df end)
        {
            return PointSegment(in point, in start, in end, out _);
        }

        /// <summary>
        /// Проекция точки на отрезок.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="normalizeDistance">Нормализованная позиция проецируемой точки от начала отрезка.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector3Df PointSegment(in Vector3Df point, in Vector3Df start, in Vector3Df end,
            out float normalizeDistance)
        {
            var segmentDirection = end - start;
            var sqrSegmentLength = segmentDirection.SqrLength;
            if (sqrSegmentLength < XGeometry3D.Epsilon_f)
            {
                // The segment is a point
                normalizeDistance = 0;
                return start;
            }

            var pointProjection = Vector3Df.Dot(in segmentDirection, point - start);
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
        #endregion

        #region Point - Sphere 
        /// <summary>
        /// Проекция точки на сферу.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="sphere">Сфера.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector3Df PointSphere(in Vector3Df point, in Sphere3Df sphere)
        {
            return PointSphere(in point, in sphere.Center, sphere.Radius);
        }

        /// <summary>
        /// Проекция точки на сферу.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="sphereCenter">Центр сферы.</param>
        /// <param name="sphereRadius">Радиус сферы.</param>
        /// <returns>Спроецированная точка.</returns>
        public static Vector3Df PointSphere(in Vector3Df point, in Vector3Df sphereCenter, float sphereRadius)
        {
            return sphereCenter + ((point - sphereCenter).Normalized * sphereRadius);
        }
        #endregion

        #region Line - Sphere 
        /// <summary>
        /// Поиск ближайших точек проекции линии и сферы.
        /// </summary>
        /// <param name="line">Линия.</param>
        /// <param name="sphere">Сфера.</param>
        /// <param name="linePoint">Точка проекции на линии.</param>
        /// <param name="spherePoint">Точка проекции на сфере.</param>
        public static void LineSphere(in Line3Df line, in Sphere3Df sphere, out Vector3Df linePoint, out Vector3Df spherePoint)
        {
            LineSphere(line.Position, line.Direction, sphere.Center, sphere.Radius, out linePoint, out spherePoint);
        }

        /// <summary>
        /// Поиск ближайших точек проекции линии и сферы.
        /// </summary>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <param name="sphereCenter">Центр сферы.</param>
        /// <param name="sphereRadius">Радиус сферы.</param>
        /// <param name="linePoint">Точка проекции на линии.</param>
        /// <param name="spherePoint">Точка проекции на сфере.</param>
        public static void LineSphere(in Vector3Df linePos, in Vector3Df lineDir, in Vector3Df sphereCenter, float sphereRadius,
            out Vector3Df linePoint, out Vector3Df spherePoint)
        {
            var posToCenter = sphereCenter - linePos;
            var centerProjection = Vector3Df.Dot(in lineDir, in posToCenter);
            var sqrDistanceToLine = posToCenter.SqrLength - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (sphereRadius * sphereRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry3D.Epsilon_f)
            {
                // No intersection
                linePoint = linePos + (lineDir * centerProjection);
                spherePoint = sphereCenter + ((linePoint - sphereCenter).Normalized * sphereRadius);
                return;
            }
            if (sqrDistanceToIntersection < XGeometry3D.Epsilon_f)
            {
                // Point intersection
                linePoint = spherePoint = linePos + (lineDir * centerProjection);
                return;
            }

            // Two points intersection
            var distanceToIntersection = XMath.Sqrt(sqrDistanceToIntersection);
            var distanceA = centerProjection - distanceToIntersection;
            linePoint = spherePoint = linePos + (lineDir * distanceA);
        }
        #endregion

        #region Ray - Sphere 
        /// <summary>
        /// Поиск ближайших точек проекции луча и сферы.
        /// </summary>
        /// <param name="ray">Луч.</param>
        /// <param name="sphere">Сфера.</param>
        /// <param name="rayPoint">Точка проекции на луче.</param>
        /// <param name="spherePoint">Точка проекции на сферы.</param>
        public static void RaySphere(in Ray3Df ray, in Sphere3Df sphere, out Vector3Df rayPoint, out Vector3Df spherePoint)
        {
            RaySphere(in ray.Position, in ray.Direction, in sphere.Center, sphere.Radius, out rayPoint, out spherePoint);
        }

        /// <summary>
        /// Поиск ближайших точек проекции луча и сферы.
        /// </summary>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <param name="sphereCenter">Центр сферы.</param>
        /// <param name="sphereRadius">Радиус сферы.</param>
        /// <param name="rayPoint">Точка проекции на луче.</param>
        /// <param name="spherePoint">Точка проекции на сферы.</param>
        public static void RaySphere(in Vector3Df rayPos, in Vector3Df rayDir, in Vector3Df sphereCenter, float sphereRadius,
            out Vector3Df rayPoint, out Vector3Df spherePoint)
        {
            var posToCenter = sphereCenter - rayPos;
            var centerProjection = Vector3Df.Dot(rayDir, posToCenter);
            if (centerProjection + sphereRadius < -XGeometry3D.Epsilon_f)
            {
                // No intersection
                rayPoint = rayPos;
                spherePoint = sphereCenter - (posToCenter.Normalized * sphereRadius);
                return;
            }

            var sqrDistanceToLine = posToCenter.SqrLength - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (sphereRadius * sphereRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry3D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < -XGeometry3D.Epsilon_f)
                {
                    rayPoint = rayPos;
                    spherePoint = sphereCenter - (posToCenter.Normalized * sphereRadius);
                    return;
                }
                rayPoint = rayPos + (rayDir * centerProjection);
                spherePoint = sphereCenter + ((rayPoint - sphereCenter).Normalized * sphereRadius);
                return;
            }
            if (sqrDistanceToIntersection < XGeometry3D.Epsilon_f)
            {
                if (centerProjection < -XGeometry3D.Epsilon_f)
                {
                    // No intersection
                    rayPoint = rayPos;
                    spherePoint = sphereCenter - (posToCenter.Normalized * sphereRadius);
                    return;
                }
                // Point intersection
                rayPoint = spherePoint = rayPos + (rayDir * centerProjection);
                return;
            }

            // Line intersection
            var distanceToIntersection = XMath.Sqrt(sqrDistanceToIntersection);
            var distanceA = centerProjection - distanceToIntersection;

            if (distanceA < -XGeometry3D.Epsilon_f)
            {
                var distanceB = centerProjection + distanceToIntersection;
                if (distanceB < -XGeometry3D.Epsilon_f)
                {
                    // No intersection
                    rayPoint = rayPos;
                    spherePoint = sphereCenter - (posToCenter.Normalized * sphereRadius);
                    return;
                }

                // Point intersection
                rayPoint = spherePoint = rayPos + (rayDir * distanceB);
                return;
            }

            // Two points intersection
            rayPoint = spherePoint = rayPos + (rayDir * distanceA);
        }
        #endregion

        #region Segment - Sphere 
        /// <summary>
        /// Поиск ближайших точек проекции отрезков и сферы.
        /// </summary>
        /// <param name="segment">Отрезок.</param>
        /// <param name="sphere">Сфера.</param>
        /// <param name="segmentPoint">Точка проекции на отрезки.</param>
        /// <param name="spherePoint">Точка проекции на сферы.</param>
        public static void SegmentSphere(in Segment3Df segment, in Sphere3Df sphere, out Vector3Df segmentPoint, out Vector3Df spherePoint)
        {
            SegmentSphere(in segment.Start, in segment.End, in sphere.Center, sphere.Radius, out segmentPoint, out spherePoint);
        }

        /// <summary>
        /// Поиск ближайших точек проекции отрезков и сферы.
        /// </summary>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="sphereCenter">Центр сферы.</param>
        /// <param name="sphereRadius">Радиус сферы.</param>
        /// <param name="segmentPoint">Точка проекции на отрезки.</param>
        /// <param name="spherePoint">Точка проекции на сферы.</param>
        public static void SegmentSphere(in Vector3Df start, in Vector3Df end, in Vector3Df sphereCenter, float sphereRadius,
            out Vector3Df segmentPoint, out Vector3Df spherePoint)
        {
            var segmentStartToCenter = sphereCenter - start;
            var fromStartToEnd = end - start;
            var segmentLength = fromStartToEnd.Length;
            if (segmentLength < XGeometry3D.Epsilon_f)
            {
                segmentPoint = start;
                var distanceToPoint = segmentStartToCenter.Length;
                if (distanceToPoint < sphereRadius + XGeometry3D.Epsilon_f)
                {
                    if (distanceToPoint > sphereRadius - XGeometry3D.Epsilon_f)
                    {
                        spherePoint = segmentPoint;
                        return;
                    }
                    if (distanceToPoint < XGeometry3D.Epsilon_f)
                    {
                        spherePoint = segmentPoint;
                        return;
                    }
                }
                var toPoint = -segmentStartToCenter / distanceToPoint;
                spherePoint = sphereCenter + (toPoint * sphereRadius);
                return;
            }

            var segmentDirection = fromStartToEnd.Normalized;
            var centerProjection = Vector3Df.Dot(in segmentDirection, in segmentStartToCenter);
            if (centerProjection + sphereRadius < -XGeometry3D.Epsilon_f ||
                centerProjection - sphereRadius > segmentLength + XGeometry3D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < 0)
                {
                    segmentPoint = start;
                    spherePoint = sphereCenter - (segmentStartToCenter.Normalized * sphereRadius);
                    return;
                }
                segmentPoint = end;
                spherePoint = sphereCenter - ((sphereCenter - end).Normalized * sphereRadius);
                return;
            }

            var sqrDistanceToLine = segmentStartToCenter.SqrLength - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (sphereRadius * sphereRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry3D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < -XGeometry3D.Epsilon_f)
                {
                    segmentPoint = start;
                    spherePoint = sphereCenter - (segmentStartToCenter.Normalized * sphereRadius);
                    return;
                }
                if (centerProjection > segmentLength + XGeometry3D.Epsilon_f)
                {
                    segmentPoint = end;
                    spherePoint = sphereCenter - ((sphereCenter - end).Normalized * sphereRadius);
                    return;
                }
                segmentPoint = start + (segmentDirection * centerProjection);
                spherePoint = sphereCenter + ((segmentPoint - sphereCenter).Normalized * sphereRadius);
                return;
            }

            if (sqrDistanceToIntersection < XGeometry3D.Epsilon_f)
            {
                if (centerProjection < -XGeometry3D.Epsilon_f)
                {
                    // No intersection
                    segmentPoint = start;
                    spherePoint = sphereCenter - (segmentStartToCenter.Normalized * sphereRadius);
                    return;
                }
                if (centerProjection > segmentLength + XGeometry3D.Epsilon_f)
                {
                    // No intersection
                    segmentPoint = end;
                    spherePoint = sphereCenter - ((sphereCenter - end).Normalized * sphereRadius);
                    return;
                }
                // Point intersection
                segmentPoint = spherePoint = start + (segmentDirection * centerProjection);
                return;
            }

            // Line intersection
            var distanceToIntersection = XMath.Sqrt(sqrDistanceToIntersection);
            var distanceA = centerProjection - distanceToIntersection;
            var distanceB = centerProjection + distanceToIntersection;

            var pointAIsAfterSegmentStart = distanceA > -XGeometry3D.Epsilon_f;
            var pointBIsBeforeSegmentEnd = distanceB < segmentLength + XGeometry3D.Epsilon_f;

            if (pointAIsAfterSegmentStart && pointBIsBeforeSegmentEnd)
            {
                segmentPoint = spherePoint = start + (segmentDirection * distanceA);
                return;
            }
            if (!pointAIsAfterSegmentStart && !pointBIsBeforeSegmentEnd)
            {
                // The segment is inside, but no intersection
                if (distanceA > -(distanceB - segmentLength))
                {
                    segmentPoint = start;
                    spherePoint = start + (segmentDirection * distanceA);
                    return;
                }
                segmentPoint = end;
                spherePoint = start + (segmentDirection * distanceB);
                return;
            }

            var pointAIsBeforeSegmentEnd = distanceA < segmentLength + XGeometry3D.Epsilon_f;
            if (pointAIsAfterSegmentStart && pointAIsBeforeSegmentEnd)
            {
                // Point A intersection
                segmentPoint = spherePoint = start + (segmentDirection * distanceA);
                return;
            }
            var pointBIsAfterSegmentStart = distanceB > -XGeometry3D.Epsilon_f;
            if (pointBIsAfterSegmentStart && pointBIsBeforeSegmentEnd)
            {
                // Point B intersection
                segmentPoint = spherePoint = start + (segmentDirection * distanceB);
                return;
            }

            // No intersection
            if (centerProjection < 0)
            {
                segmentPoint = start;
                spherePoint = sphereCenter - (segmentStartToCenter.Normalized * sphereRadius);
                return;
            }
            segmentPoint = end;
            spherePoint = sphereCenter - ((sphereCenter - end).Normalized * sphereRadius);
        }
        #endregion

        #region Sphere - Sphere 
        /// <summary>
        /// Поиск ближайших точек проекции двух окружностей.
        /// </summary>
        /// <param name="sphereA">Первая окружность.</param>
        /// <param name="sphereB">Вторая окружность.</param>
        /// <param name="pointA">Точка проекции на первую окружность.</param>
        /// <param name="pointB">Точка проекции на вторую окружность.</param>
        public static void SphereSphere(in Sphere3Df sphereA, in Sphere3Df sphereB, out Vector3Df pointA, out Vector3Df pointB)
        {
            SphereSphere(in sphereA.Center, sphereA.Radius, in sphereB.Center, sphereB.Radius, out pointA, out pointB);
        }

        /// <summary>
        /// Поиск ближайших точек проекции двух окружностей.
        /// </summary>
        /// <param name="centerA">Центр первой сферы.</param>
        /// <param name="radiusA">Радиус первой сферы.</param>
        /// <param name="centerB">Центр второй сферы.</param>
        /// <param name="radiusB">Радиус второй сферы.</param>
        /// <param name="pointA">Точка проекции на первую окружность.</param>
        /// <param name="pointB">Точка проекции на вторую окружность.</param>
        public static void SphereSphere(in Vector3Df centerA, float radiusA, in Vector3Df centerB, float radiusB,
            out Vector3Df pointA, out Vector3Df pointB)
        {
            var fromBToA = (centerA - centerB).Normalized;
            pointA = centerA - (fromBToA * radiusA);
            pointB = centerB + (fromBToA * radiusB);
        }
        #endregion
    }
    /**@}*/
}