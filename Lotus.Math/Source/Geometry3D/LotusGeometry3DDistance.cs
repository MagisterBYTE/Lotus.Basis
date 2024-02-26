namespace Lotus.Maths
{
    /** \addtogroup MathGeometry3D
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы вычисление дистанции между основными геометрическими телами/примитивами.
    /// </summary>
    public static class XDistance3D
    {
        #region Point - Line 
        /// <summary>
        /// Вычисление расстояния между линией и ближайшей точки.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="line">Линия.</param>
        /// <returns>Расстояние.</returns>
        public static float PointLine(in Vector3Df point, in Line3Df line)
        {
            return Vector3Df.Distance(in point, XClosest3D.PointLine(in point, in line));
        }

        /// <summary>
        /// Вычисление расстояния между линией и ближайшей точки.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <returns>Расстояние.</returns>
        public static float PointLine(in Vector3Df point, in Vector3Df linePos, in Vector3Df lineDir)
        {
            return Vector3Df.Distance(in point, XClosest3D.PointLine(in point, in linePos, in lineDir));
        }
        #endregion

        #region Point - Ray 
        /// <summary>
        /// Вычисление расстояние до самой близкой точки на луче.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="ray">Луч.</param>
        /// <returns>Расстояние.</returns>
        public static float PointRay(in Vector3Df point, in Ray3Df ray)
        {
            return Vector3Df.Distance(in point, XClosest3D.PointRay(in point, in ray));
        }

        /// <summary>
        /// Вычисление расстояние до самой близкой точки на луче.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <returns>Расстояние.</returns>
        public static float PointRay(in Vector3Df point, in Vector3Df rayPos, in Vector3Df rayDir)
        {
            return Vector3Df.Distance(in point, XClosest3D.PointRay(in point, in rayPos, in rayDir));
        }
        #endregion

        #region Point - Segment 
        /// <summary>
        /// Вычисление расстояние до самой близкой точки на отрезке.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="segment">Отрезок.</param>
        /// <returns>Расстояние.</returns>
        public static float PointSegment(in Vector3Df point, in Segment3Df segment)
        {
            return Vector3Df.Distance(in point, XClosest3D.PointSegment(in point, in segment));
        }

        /// <summary>
        /// Вычисление расстояние до самой близкой точки на отрезке.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <returns>Расстояние.</returns>
        public static float PointSegment(in Vector3Df point, in Vector3Df start, in Vector3Df end)
        {
            return Vector3Df.Distance(in point, XClosest3D.PointSegment(in point, in start, in end));
        }
        #endregion

        #region Point - Sphere 
        /// <summary>
        /// Вычисление расстояние до самой близкой точки на сферы.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="sphere">Сфера.</param>
        /// <returns>Расстояние.</returns>
        public static float PointSphere(in Vector3Df point, in Sphere3Df sphere)
        {
            return PointSphere(in point, in sphere.Center, sphere.Radius);
        }

        /// <summary>
        /// Вычисление расстояние до самой близкой точки на сферы.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="sphereCenter">Центр сферы.</param>
        /// <param name="sphereRadius">Радиус сферы.</param>
        /// <returns>Расстояние.</returns>
        public static float PointSphere(in Vector3Df point, in Vector3Df sphereCenter, float sphereRadius)
        {
            return (sphereCenter - point).Length - sphereRadius;
        }
        #endregion

        #region Line - Sphere 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на линии и сферы.
        /// </summary>
        /// <param name="line">Линия.</param>
        /// <param name="sphere">Сфера.</param>
        /// <returns>Расстояние.</returns>
        public static float LineSphere(in Line3Df line, in Sphere3Df sphere)
        {
            return LineSphere(in line.Position, in line.Direction, in sphere.Center, sphere.Radius);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на линии и сферы.
        /// </summary>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <param name="sphereCenter">Центр сферы.</param>
        /// <param name="sphereRadius">Радиус сферы.</param>
        /// <returns>Расстояние.</returns>
        public static float LineSphere(in Vector3Df linePos, in Vector3Df lineDir, in Vector3Df sphereCenter, float sphereRadius)
        {
            Vector3Df pos_to_center = sphereCenter - linePos;
            var center_projection = Vector3Df.Dot(in lineDir, in pos_to_center);
            var sqr_distance_to_line = pos_to_center.SqrLength - (center_projection * center_projection);
            var sqr_distance_to_intersection = (sphereRadius * sphereRadius) - sqr_distance_to_line;
            if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
            {
                // No intersection
                return XMath.Sqrt(sqr_distance_to_line) - sphereRadius;
            }
            return 0;
        }
        #endregion

        #region Ray - Sphere 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на луче и сферы.
        /// </summary>
        /// <param name="ray">Луч.</param>
        /// <param name="sphere">Сфера.</param>
        /// <returns>Расстояние.</returns>
        public static float RaySphere(in Ray3Df ray, in Sphere3Df sphere)
        {
            return RaySphere(in ray.Position, in ray.Direction, in sphere.Center, sphere.Radius);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на луче и сферы.
        /// </summary>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <param name="sphereCenter">Центр сферы.</param>
        /// <param name="sphereRadius">Радиус сферы.</param>
        /// <returns>Расстояние.</returns>
        public static float RaySphere(in Vector3Df rayPos, in Vector3Df rayDir, in Vector3Df sphereCenter, float sphereRadius)
        {
            Vector3Df pos_to_center = sphereCenter - rayPos;
            var center_projection = Vector3Df.Dot(in rayDir, in pos_to_center);
            if (center_projection + sphereRadius < -XGeometry3D.Eplsilon_f)
            {
                // No intersection
                return XMath.Sqrt(pos_to_center.SqrLength) - sphereRadius;
            }

            var sqr_distance_to_pos = pos_to_center.SqrLength;
            var sqr_distance_to_line = sqr_distance_to_pos - (center_projection * center_projection);
            var sqr_distance_to_intersection = (sphereRadius * sphereRadius) - sqr_distance_to_line;
            if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
            {
                // No intersection
                if (center_projection < -XGeometry3D.Eplsilon_f)
                {
                    return XMath.Sqrt(sqr_distance_to_pos) - sphereRadius;
                }
                return XMath.Sqrt(sqr_distance_to_line) - sphereRadius;
            }
            if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
            {
                if (center_projection < -XGeometry3D.Eplsilon_f)
                {
                    // No intersection
                    return XMath.Sqrt(sqr_distance_to_pos) - sphereRadius;
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
                    return XMath.Sqrt(sqr_distance_to_pos) - sphereRadius;
                }

                // Point intersection;
                return 0;
            }

            // Two points intersection;
            return 0;
        }
        #endregion

        #region Segment - Sphere 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на отрезке и сферы.
        /// </summary>
        /// <param name="segment">Отрезок.</param>
        /// <param name="sphere">Сфера.</param>
        /// <returns>Расстояние.</returns>
        public static float SegmentSphere(in Segment3Df segment, in Sphere3Df sphere)
        {
            return SegmentSphere(in segment.Start, in segment.End, in sphere.Center, sphere.Radius);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на отрезке и сферы.
        /// </summary>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="sphereCenter">Центр сферы.</param>
        /// <param name="sphereRadius">Радиус сферы.</param>
        /// <returns>Расстояние.</returns>
        public static float SegmentSphere(in Vector3Df start, in Vector3Df end, in Vector3Df sphereCenter, float sphereRadius)
        {
            Vector3Df segment_start_to_center = sphereCenter - start;
            Vector3Df from_start_to_end = end - start;
            var segment_length = from_start_to_end.Length;
            if (segment_length < XGeometry3D.Eplsilon_f)
            {
                return segment_start_to_center.Length - sphereRadius;
            }

            Vector3Df segment_direction = from_start_to_end.Normalized;
            var center_projection = Vector3Df.Dot(in segment_direction, in segment_start_to_center);
            if (center_projection + sphereRadius < -XGeometry3D.Eplsilon_f ||
                center_projection - sphereRadius > segment_length + XGeometry3D.Eplsilon_f)
            {
                // No intersection
                if (center_projection < 0)
                {
                    return XMath.Sqrt(segment_start_to_center.SqrLength) - sphereRadius;
                }
                return (sphereCenter - end).Length - sphereRadius;
            }

            var sqr_distance_to_a = segment_start_to_center.SqrLength;
            var sqr_distance_to_line = sqr_distance_to_a - (center_projection * center_projection);
            var sqr_distance_to_intersection = (sphereRadius * sphereRadius) - sqr_distance_to_line;
            if (sqr_distance_to_intersection < -XGeometry3D.Eplsilon_f)
            {
                // No intersection
                if (center_projection < -XGeometry3D.Eplsilon_f)
                {
                    return XMath.Sqrt(sqr_distance_to_a) - sphereRadius;
                }
                if (center_projection > segment_length + XGeometry3D.Eplsilon_f)
                {
                    return (sphereCenter - end).Length - sphereRadius;
                }
                return XMath.Sqrt(sqr_distance_to_line) - sphereRadius;
            }

            if (sqr_distance_to_intersection < XGeometry3D.Eplsilon_f)
            {
                if (center_projection < -XGeometry3D.Eplsilon_f)
                {
                    // No intersection
                    return XMath.Sqrt(sqr_distance_to_a) - sphereRadius;
                }
                if (center_projection > segment_length + XGeometry3D.Eplsilon_f)
                {
                    // No intersection
                    return (sphereCenter - end).Length - sphereRadius;
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
                return XMath.Sqrt(sqr_distance_to_a) - sphereRadius;
            }
            return (sphereCenter - end).Length - sphereRadius;
        }
        #endregion

        #region Sphere - Sphere 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на сферах.
        /// </summary>
        /// <param name="sphereA">Первая сфера.</param>
        /// <param name="sphereB">Вторая сфера.</param>
        /// <returns>
        /// Положительное значение, если сферы не пересекаются, отрицательное иначе
        /// Отрицательная величина может быть интерпретирована как глубина проникновения
        /// </returns>
        public static float SphereSphere(in Sphere3Df sphereA, in Sphere3Df sphereB)
        {
            return SphereSphere(in sphereA.Center, sphereA.Radius, in sphereB.Center, sphereB.Radius);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на сферах.
        /// </summary>
        /// <param name="centerA">Центр первой сферы.</param>
        /// <param name="radiusA">Радиус первой сферы.</param>
        /// <param name="centerB">Центр второй сферы.</param>
        /// <param name="radiusB">Радиус второй сферы.</param>
        /// <returns>
        /// Положительное значение, если сферы не пересекаются, отрицательное иначе
        /// Отрицательная величина может быть интерпретирована как глубина проникновения
        /// </returns>
        public static float SphereSphere(in Vector3Df centerA, float radiusA, in Vector3Df centerB, float radiusB)
        {
            return Vector3Df.Distance(in centerA, in centerB) - radiusA - radiusB;
        }

        #endregion
    }
    /**@}*/
}