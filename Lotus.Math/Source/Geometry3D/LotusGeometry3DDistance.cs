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
            var posToCenter = sphereCenter - linePos;
            var centerProjection = Vector3Df.Dot(in lineDir, in posToCenter);
            var sqrDistanceToLine = posToCenter.SqrLength - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (sphereRadius * sphereRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry3D.Epsilon_f)
            {
                // No intersection
                return XMath.Sqrt(sqrDistanceToLine) - sphereRadius;
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
            var posToCenter = sphereCenter - rayPos;
            var centerProjection = Vector3Df.Dot(in rayDir, in posToCenter);
            if (centerProjection + sphereRadius < -XGeometry3D.Epsilon_f)
            {
                // No intersection
                return XMath.Sqrt(posToCenter.SqrLength) - sphereRadius;
            }

            var sqrDistanceToPos = posToCenter.SqrLength;
            var sqrDistanceToLine = sqrDistanceToPos - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (sphereRadius * sphereRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry3D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < -XGeometry3D.Epsilon_f)
                {
                    return XMath.Sqrt(sqrDistanceToPos) - sphereRadius;
                }
                return XMath.Sqrt(sqrDistanceToLine) - sphereRadius;
            }
            if (sqrDistanceToIntersection < XGeometry3D.Epsilon_f)
            {
                if (centerProjection < -XGeometry3D.Epsilon_f)
                {
                    // No intersection
                    return XMath.Sqrt(sqrDistanceToPos) - sphereRadius;
                }
                // Point intersection
                return 0;
            }

            // Line intersection
            var distanceToIntersection = XMath.Sqrt(sqrDistanceToIntersection);
            var distanceA = centerProjection - distanceToIntersection;
            var distanceB = centerProjection + distanceToIntersection;

            if (distanceA < -XGeometry3D.Epsilon_f)
            {
                if (distanceB < -XGeometry3D.Epsilon_f)
                {
                    // No intersection
                    return XMath.Sqrt(sqrDistanceToPos) - sphereRadius;
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
            var segmentStartToCenter = sphereCenter - start;
            var fromStartToEnd = end - start;
            var segmentLength = fromStartToEnd.Length;
            if (segmentLength < XGeometry3D.Epsilon_f)
            {
                return segmentStartToCenter.Length - sphereRadius;
            }

            var segmentDirection = fromStartToEnd.Normalized;
            var centerProjection = Vector3Df.Dot(in segmentDirection, in segmentStartToCenter);
            if (centerProjection + sphereRadius < -XGeometry3D.Epsilon_f ||
                centerProjection - sphereRadius > segmentLength + XGeometry3D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < 0)
                {
                    return XMath.Sqrt(segmentStartToCenter.SqrLength) - sphereRadius;
                }
                return (sphereCenter - end).Length - sphereRadius;
            }

            var sqrDistanceToA = segmentStartToCenter.SqrLength;
            var sqrDistanceToLine = sqrDistanceToA - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (sphereRadius * sphereRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry3D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < -XGeometry3D.Epsilon_f)
                {
                    return XMath.Sqrt(sqrDistanceToA) - sphereRadius;
                }
                if (centerProjection > segmentLength + XGeometry3D.Epsilon_f)
                {
                    return (sphereCenter - end).Length - sphereRadius;
                }
                return XMath.Sqrt(sqrDistanceToLine) - sphereRadius;
            }

            if (sqrDistanceToIntersection < XGeometry3D.Epsilon_f)
            {
                if (centerProjection < -XGeometry3D.Epsilon_f)
                {
                    // No intersection
                    return XMath.Sqrt(sqrDistanceToA) - sphereRadius;
                }
                if (centerProjection > segmentLength + XGeometry3D.Epsilon_f)
                {
                    // No intersection
                    return (sphereCenter - end).Length - sphereRadius;
                }
                // Point intersection
                return 0;
            }

            // Line intersection
            var distanceToIntersection = XMath.Sqrt(sqrDistanceToIntersection);
            var distanceA = centerProjection - distanceToIntersection;
            var distanceB = centerProjection + distanceToIntersection;

            var pointAIsAfterSegmentStart = distanceA > -XGeometry3D.Epsilon_f;
            var pointBIsBeforeSegmentEnd = distanceB < segmentLength + XGeometry3D.Epsilon_f;

            if (pointAIsAfterSegmentStart && pointBIsBeforeSegmentEnd)
            {
                // Two points intersection
                return 0;
            }
            if (!pointAIsAfterSegmentStart && !pointBIsBeforeSegmentEnd)
            {
                // The segment is inside, but no intersection
                distanceB = -(distanceB - segmentLength);
                return distanceA > distanceB ? distanceA : distanceB;
            }

            var pointAIsBeforeSegmentEnd = distanceA < segmentLength + XGeometry3D.Epsilon_f;
            if (pointAIsAfterSegmentStart && pointAIsBeforeSegmentEnd)
            {
                // Point A intersection
                return 0;
            }
            var pointBIsAfterSegmentStart = distanceB > -XGeometry3D.Epsilon_f;
            if (pointBIsAfterSegmentStart && pointBIsBeforeSegmentEnd)
            {
                // Point B intersection
                return 0;
            }

            // No intersection
            if (centerProjection < 0)
            {
                return XMath.Sqrt(sqrDistanceToA) - sphereRadius;
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