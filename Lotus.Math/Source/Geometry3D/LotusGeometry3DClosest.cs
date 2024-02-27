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
            var to_point = point - rayPos;
            var point_projection = Vector3Df.Dot(in rayDir, in to_point);
            if (point_projection <= 0)
            {
                distance = 0;
                return rayPos;
            }

            // In theory, SqrLength should be 1, but in practice this division helps with numerical stability
            distance = point_projection / rayDir.SqrLength;
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
            var segment_direction = end - start;
            var sqr_segment_length = segment_direction.SqrLength;
            if (sqr_segment_length < XGeometry3D.Eplsilon_f)
            {
                // The segment is a point
                normalizeDistance = 0;
                return start;
            }

            var point_projection = Vector3Df.Dot(in segment_direction, point - start);
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
            var pos_to_center = sphereCenter - linePos;
            var center_projection = Vector3Df.Dot(in lineDir, in pos_to_center);
            var sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
            var sqr_distance_to_intersection = (sphereRadius * sphereRadius) - sqr_distance_to_line;
            if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
            {
                // No intersection
                linePoint = linePos + (lineDir * center_projection);
                spherePoint = sphereCenter + ((linePoint - sphereCenter).Normalized * sphereRadius);
                return;
            }
            if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
            {
                // Point intersection
                linePoint = spherePoint = linePos + (lineDir * center_projection);
                return;
            }

            // Two points intersection
            var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
            var distance_a = center_projection - distance_to_intersection;
            linePoint = spherePoint = linePos + (lineDir * distance_a);
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
            var pos_to_center = sphereCenter - rayPos;
            var center_projection = Vector3Df.Dot(rayDir, pos_to_center);
            if (center_projection + sphereRadius < -XGeometry3D.Eplsilon_f)
            {
                // No intersection
                rayPoint = rayPos;
                spherePoint = sphereCenter - (pos_to_center.Normalized * sphereRadius);
                return;
            }

            var sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
            var sqr_distance_to_intersection = (sphereRadius * sphereRadius) - sqr_distance_to_line;
            if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
            {
                // No intersection
                if (center_projection < -XGeometry3D.Eplsilon_f)
                {
                    rayPoint = rayPos;
                    spherePoint = sphereCenter - (pos_to_center.Normalized * sphereRadius);
                    return;
                }
                rayPoint = rayPos + (rayDir * center_projection);
                spherePoint = sphereCenter + ((rayPoint - sphereCenter).Normalized * sphereRadius);
                return;
            }
            if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
            {
                if (center_projection < -XGeometry3D.Eplsilon_f)
                {
                    // No intersection
                    rayPoint = rayPos;
                    spherePoint = sphereCenter - (pos_to_center.Normalized * sphereRadius);
                    return;
                }
                // Point intersection
                rayPoint = spherePoint = rayPos + (rayDir * center_projection);
                return;
            }

            // Line intersection
            var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
            var distance_a = center_projection - distance_to_intersection;

            if (distance_a < -XGeometry3D.Eplsilon_f)
            {
                var distance_b = center_projection + distance_to_intersection;
                if (distance_b < -XGeometry3D.Eplsilon_f)
                {
                    // No intersection
                    rayPoint = rayPos;
                    spherePoint = sphereCenter - (pos_to_center.Normalized * sphereRadius);
                    return;
                }

                // Point intersection
                rayPoint = spherePoint = rayPos + (rayDir * distance_b);
                return;
            }

            // Two points intersection
            rayPoint = spherePoint = rayPos + (rayDir * distance_a);
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
            var segment_start_to_center = sphereCenter - start;
            var from_start_to_end = end - start;
            var segment_length = from_start_to_end.Length;
            if (segment_length < XGeometry3D.Eplsilon_f)
            {
                segmentPoint = start;
                var distanceToPoint = segment_start_to_center.Length;
                if (distanceToPoint < sphereRadius + XGeometry3D.Eplsilon_f)
                {
                    if (distanceToPoint > sphereRadius - XGeometry3D.Eplsilon_f)
                    {
                        spherePoint = segmentPoint;
                        return;
                    }
                    if (distanceToPoint < XGeometry3D.Eplsilon_f)
                    {
                        spherePoint = segmentPoint;
                        return;
                    }
                }
                var to_point = -segment_start_to_center / distanceToPoint;
                spherePoint = sphereCenter + (to_point * sphereRadius);
                return;
            }

            var segment_direction = from_start_to_end.Normalized;
            var center_projection = Vector3Df.Dot(in segment_direction, in segment_start_to_center);
            if (center_projection + sphereRadius < -XGeometry3D.Eplsilon_f ||
                center_projection - sphereRadius > segment_length + XGeometry3D.Eplsilon_f)
            {
                // No intersection
                if (center_projection < 0)
                {
                    segmentPoint = start;
                    spherePoint = sphereCenter - (segment_start_to_center.Normalized * sphereRadius);
                    return;
                }
                segmentPoint = end;
                spherePoint = sphereCenter - ((sphereCenter - end).Normalized * sphereRadius);
                return;
            }

            var sqr_distance_to_line = segment_start_to_center.SqrLength - (center_projection * center_projection);
            var sqr_distance_to_intersection = (sphereRadius * sphereRadius) - sqr_distance_to_line;
            if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
            {
                // No intersection
                if (center_projection < -XGeometry3D.Eplsilon_f)
                {
                    segmentPoint = start;
                    spherePoint = sphereCenter - (segment_start_to_center.Normalized * sphereRadius);
                    return;
                }
                if (center_projection > segment_length + XGeometry3D.Eplsilon_f)
                {
                    segmentPoint = end;
                    spherePoint = sphereCenter - ((sphereCenter - end).Normalized * sphereRadius);
                    return;
                }
                segmentPoint = start + (segment_direction * center_projection);
                spherePoint = sphereCenter + ((segmentPoint - sphereCenter).Normalized * sphereRadius);
                return;
            }

            if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
            {
                if (center_projection < -XGeometry3D.Eplsilon_f)
                {
                    // No intersection
                    segmentPoint = start;
                    spherePoint = sphereCenter - (segment_start_to_center.Normalized * sphereRadius);
                    return;
                }
                if (center_projection > segment_length + XGeometry3D.Eplsilon_f)
                {
                    // No intersection
                    segmentPoint = end;
                    spherePoint = sphereCenter - ((sphereCenter - end).Normalized * sphereRadius);
                    return;
                }
                // Point intersection
                segmentPoint = spherePoint = start + (segment_direction * center_projection);
                return;
            }

            // Line intersection
            var distance_to_intersection = XMath.Sqrt(sqr_distance_to_intersection);
            var distance_a = center_projection - distance_to_intersection;
            var distance_b = center_projection + distance_to_intersection;

            var point_a_is_after_segment_start = distance_a > -XGeometry3D.Eplsilon_f;
            var point_b_is_before_segment_end = distance_b < segment_length + XGeometry3D.Eplsilon_f;

            if (point_a_is_after_segment_start && point_b_is_before_segment_end)
            {
                segmentPoint = spherePoint = start + (segment_direction * distance_a);
                return;
            }
            if (!point_a_is_after_segment_start && !point_b_is_before_segment_end)
            {
                // The segment is inside, but no intersection
                if (distance_a > -(distance_b - segment_length))
                {
                    segmentPoint = start;
                    spherePoint = start + (segment_direction * distance_a);
                    return;
                }
                segmentPoint = end;
                spherePoint = start + (segment_direction * distance_b);
                return;
            }

            var point_a_is_before_segment_end = distance_a < segment_length + XGeometry3D.Eplsilon_f;
            if (point_a_is_after_segment_start && point_a_is_before_segment_end)
            {
                // Point A intersection
                segmentPoint = spherePoint = start + (segment_direction * distance_a);
                return;
            }
            var point_b_is_after_segment_start = distance_b > -XGeometry3D.Eplsilon_f;
            if (point_b_is_after_segment_start && point_b_is_before_segment_end)
            {
                // Point B intersection
                segmentPoint = spherePoint = start + (segment_direction * distance_b);
                return;
            }

            // No intersection
            if (center_projection < 0)
            {
                segmentPoint = start;
                spherePoint = sphereCenter - (segment_start_to_center.Normalized * sphereRadius);
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
            var from_b_to_a = (centerA - centerB).Normalized;
            pointA = centerA - (from_b_to_a * radiusA);
            pointB = centerB + (from_b_to_a * radiusB);
        }
        #endregion
    }
    /**@}*/
}