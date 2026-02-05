using System;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry2D
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы вычисление дистанции между основными геометрическими телами/примитивами.
    /// </summary>
    public static class XDistance2D
    {
        #region Point - Line 
        /// <summary>
        /// Вычисление расстояния между линией и ближайшей точки.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="line">Линия.</param>
        /// <returns>Расстояние.</returns>
        public static float PointLine(in Vector2Df point, in Line2Df line)
        {
            return Vector2Df.Distance(in point, XClosest2D.PointLine(in point, in line));
        }

        /// <summary>
        /// Вычисление расстояния между линией и ближайшей точки.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <returns>Расстояние.</returns>
        public static float PointLine(in Vector2Df point, in Vector2Df linePos, in Vector2Df lineDir)
        {
            return Vector2Df.Distance(point, XClosest2D.PointLine(point, linePos, lineDir));
        }
        #endregion

        #region Point - Ray 
        /// <summary>
        /// Вычисление расстояние до самой близкой точки на луче.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="ray">Луч.</param>
        /// <returns>Расстояние.</returns>
        public static float PointRay(in Vector2Df point, in Ray2Df ray)
        {
            return Vector2Df.Distance(in point, XClosest2D.PointRay(in point, in ray));
        }

        /// <summary>
        /// Вычисление расстояние до самой близкой точки на луче.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <returns>Расстояние.</returns>
        public static float PointRay(in Vector2Df point, in Vector2Df rayPos, in Vector2Df rayDir)
        {
            return Vector2Df.Distance(in point, XClosest2D.PointRay(in point, in rayPos, in rayDir));
        }
        #endregion

        #region Point - Segment 
        /// <summary>
        /// Вычисление расстояние до самой близкой точки на отрезке.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="segment">Отрезок.</param>
        /// <returns>Расстояние.</returns>
        public static float PointSegment(in Vector2Df point, in Segment2Df segment)
        {
            return Vector2Df.Distance(in point, XClosest2D.PointSegment(in point, in segment));
        }

        /// <summary>
        /// Вычисление расстояние до самой близкой точки на отрезке.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <returns>Расстояние.</returns>
        public static float PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end)
        {
            return Vector2Df.Distance(in point, XClosest2D.PointSegment(in point, in start, in end));
        }

        /// <summary>
        /// Вычисление расстояние до самой близкой точки на отрезке.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="segmentDirection">Направление отрезка.</param>
        /// <param name="segmentLength">Длина отрезка.</param>
        /// <returns>Расстояние.</returns>
        private static float PointSegment(in Vector2Df point, in Vector2Df start, in Vector2Df end,
            in Vector2Df segmentDirection, float segmentLength)
        {
            var pointProjection = Vector2Df.Dot(in segmentDirection, point - start);
            if (pointProjection < -XGeometry2D.Epsilon_f)
            {
                return Vector2Df.Distance(in point, in start);
            }
            if (pointProjection > segmentLength + XGeometry2D.Epsilon_f)
            {
                return Vector2Df.Distance(in point, in end);
            }
            return Vector2Df.Distance(in point, start + (segmentDirection * pointProjection));
        }
        #endregion

        #region Point - Circle 
        /// <summary>
        /// Вычисление расстояние до самой близкой точки на окружности.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="circle">Окружность.</param>
        /// <returns>Расстояние.</returns>
        public static float PointCircle(in Vector2Df point, in Circle2Df circle)
        {
            return PointCircle(in point, in circle.Center, circle.Radius);
        }

        /// <summary>
        /// Вычисление расстояние до самой близкой точки на окружности.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="circleCenter">Центр окружности.</param>
        /// <param name="circleRadius">Радиус окружности.</param>
        /// <returns>Расстояние.</returns>
        public static float PointCircle(in Vector2Df point, in Vector2Df circleCenter, float circleRadius)
        {
            return (circleCenter - point).Length - circleRadius;
        }
        #endregion

        #region Line - Line 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на линиях.
        /// </summary>
        /// <param name="lineA">Первая линия.</param>
        /// <param name="lineB">Вторая линия.</param>
        /// <returns>Расстояние.</returns>
        public static float LineLine(in Line2Df lineA, in Line2Df lineB)
        {
            return LineLine(lineA.Position, lineA.Direction, lineB.Position, lineB.Direction);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на линиях.
        /// </summary>
        /// <param name="posA">Позиция первой линии.</param>
        /// <param name="dirA">Направление первой линии.</param>
        /// <param name="posB">Позиция второй линии.</param>
        /// <param name="dirB">Направление второй линии.</param>
        /// <returns>Расстояние.</returns>
        public static float LineLine(in Vector2Df posA, in Vector2Df dirA, in Vector2Df posB,
            in Vector2Df dirB)
        {
            if (Math.Abs(Vector2Df.DotPerp(in dirA, in dirB)) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                var posBToA = posA - posB;
                if (Math.Abs(Vector2Df.DotPerp(in dirA, in posBToA)) > XGeometry2D.Epsilon_f ||
                    Math.Abs(Vector2Df.DotPerp(in dirB, in posBToA)) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    var originBProjection = Vector2Df.Dot(in dirA, in posBToA);
                    var distanceSqr = posBToA.SqrLength - (originBProjection * originBProjection);

                    // distanceSqr can be negative
                    return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
                }

                // Collinear
                return 0;
            }

            // Not parallel
            return 0;
        }
        #endregion

        #region Line - Ray 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на линии и луче.
        /// </summary>
        /// <param name="line">Линия.</param>
        /// <param name="ray">Луч.</param>
        /// <returns>Расстояние.</returns>
        public static float LineRay(in Line2Df line, in Ray2Df ray)
        {
            return LineRay(in line.Position, in line.Direction, in ray.Position, in ray.Direction);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на линии и луче.
        /// </summary>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <returns>Расстояние.</returns>
        public static float LineRay(in Vector2Df linePos, in Vector2Df lineDir, in Vector2Df rayPos, in Vector2Df rayDir)
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
                    var distanceSqr = rayPosToLinePos.SqrLength - (rayPosProjection * rayPosProjection);
                    // distanceSqr can be negative
                    return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
                }
                // Collinear
                return 0;
            }

            // Not parallel
            var rayDistance = perpDotA / denominator;
            if (rayDistance < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                var rayPosProjection = Vector2Df.Dot(in lineDir, in rayPosToLinePos);
                var linePoint = linePos - (lineDir * rayPosProjection);
                return Vector2Df.Distance(in linePoint, in rayPos);
            }
            // Point intersection
            return 0;
        }
        #endregion

        #region Line - Segment 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на линии и отрезке.
        /// </summary>
        /// <param name="line">Линия.</param>
        /// <param name="segment">Отрезок.</param>
        /// <returns>Расстояние.</returns>
        public static float LineSegment(in Line2Df line, in Segment2Df segment)
        {
            return LineSegment(in line.Position, in line.Direction, in segment.Start, in segment.End);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на линии и отрезке.
        /// </summary>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <returns>Расстояние.</returns>
        public static float LineSegment(in Vector2Df linePos, in Vector2Df lineDir, in Vector2Df start, in Vector2Df end)
        {
            var segmentStartToPos = linePos - start;
            var segmentDirection = end - start;
            var denominator = Vector2Df.DotPerp(in lineDir, in segmentDirection);
            var perpDotA = Vector2Df.DotPerp(in lineDir, in segmentStartToPos);

            if (Math.Abs(denominator) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                // Normalized Direction gives more stable results 
                var perpDotB = Vector2Df.DotPerp(segmentDirection.Normalized, segmentStartToPos);
                if (Math.Abs(perpDotA) > XGeometry2D.Epsilon_f || Math.Abs(perpDotB) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    var segmentStartProjection = Vector2Df.Dot(in lineDir, in segmentStartToPos);
                    var distanceSqr = segmentStartToPos.SqrLength - (segmentStartProjection * segmentStartProjection);
                    // distanceSqr can be negative
                    return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
                }
                // Collinear
                return 0;
            }

            // Not parallel
            var segmentDistance = perpDotA / denominator;
            if (segmentDistance < -XGeometry2D.Epsilon_f || segmentDistance > 1 + XGeometry2D.Epsilon_f)
            {
                // No intersection
                var segmentPoint = start + (segmentDirection * XMath.Clamp01(segmentDistance));
                var segmentPointProjection = Vector2Df.Dot(lineDir, segmentPoint - linePos);
                var linePoint = linePos + (lineDir * segmentPointProjection);
                return Vector2Df.Distance(in linePoint, in segmentPoint);
            }
            // Point intersection
            return 0;
        }
        #endregion

        #region Line - Circle 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на линии и окружности.
        /// </summary>
        /// <param name="line">Линия.</param>
        /// <param name="circle">Окружность.</param>
        /// <returns>Расстояние.</returns>
        public static float LineCircle(in Line2Df line, in Circle2Df circle)
        {
            return LineCircle(in line.Position, in line.Direction, in circle.Center, circle.Radius);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на линии и окружности.
        /// </summary>
        /// <param name="linePos">Позиция линии.</param>
        /// <param name="lineDir">Направление линии.</param>
        /// <param name="circleCenter">Центр окружности.</param>
        /// <param name="circleRadius">Радиус окружности.</param>
        /// <returns>Расстояние.</returns>
        public static float LineCircle(in Vector2Df linePos, in Vector2Df lineDir,
            in Vector2Df circleCenter, float circleRadius)
        {
            var posToCenter = circleCenter - linePos;
            var centerProjection = Vector2Df.Dot(in lineDir, in posToCenter);
            var sqrDistanceToLine = posToCenter.SqrLength - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (circleRadius * circleRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                return XMath.Sqrt(sqrDistanceToLine) - circleRadius;
            }
            return 0;
        }
        #endregion

        #region Ray - Ray 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на лучах.
        /// </summary>
        /// <param name="rayA">Первый луч.</param>
        /// <param name="rayB">Второй луч.</param>
        /// <returns>Расстояние.</returns>
        public static float RayRay(in Ray2Df rayA, in Ray2Df rayB)
        {
            return RayRay(in rayA.Position, in rayA.Direction, in rayB.Position, in rayB.Direction);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на лучах.
        /// </summary>
        /// <param name="posA">Позиция первого луча.</param>
        /// <param name="dirA">Направление первого луча.</param>
        /// <param name="posB">Позиция второго луча.</param>
        /// <param name="dirB">Направление второго луча.</param>
        /// <returns>Расстояние.</returns>
        public static float RayRay(in Vector2Df posA, in Vector2Df dirA, in Vector2Df posB,
            in Vector2Df dirB)
        {
            var posBToA = posA - posB;
            var denominator = Vector2Df.DotPerp(in dirA, in dirB);
            var perpDotA = Vector2Df.DotPerp(in dirA, in posBToA);
            var perpDotB = Vector2Df.DotPerp(in dirB, in posBToA);

            var codirected = Vector2Df.Dot(in dirA, in dirB) > 0;
            if (Math.Abs(denominator) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                var posBProjection = -Vector2Df.Dot(dirA, posBToA);
                if (Math.Abs(perpDotA) > XGeometry2D.Epsilon_f || Math.Abs(perpDotB) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    if (!codirected && posBProjection < XGeometry2D.Epsilon_f)
                    {
                        return Vector2Df.Distance(posA, posB);
                    }
                    var distanceSqr = posBToA.SqrLength - (posBProjection * posBProjection);
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
                    if (posBProjection < XGeometry2D.Epsilon_f)
                    {
                        // No intersection
                        return Vector2Df.Distance(in posA, in posB);
                    }
                    else
                    {
                        // Segment intersection
                        return 0;
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
                    var posAProjection = Vector2Df.Dot(dirB, posBToA);
                    if (posAProjection > -XGeometry2D.Epsilon_f)
                    {
                        var rayPointA = posA;
                        var rayPointB = posB + (dirB * posAProjection);
                        return Vector2Df.Distance(in rayPointA, in rayPointB);
                    }
                    var posBProjection = -Vector2Df.Dot(dirA, posBToA);
                    if (posBProjection > -XGeometry2D.Epsilon_f)
                    {
                        var rayPointA = posA + (dirA * posBProjection);
                        var rayPointB = posB;
                        return Vector2Df.Distance(in rayPointA, in rayPointB);
                    }
                    return Vector2Df.Distance(in posA, in posB);
                }
                else
                {
                    if (distanceA > -XGeometry2D.Epsilon_f)
                    {
                        var posBProjection = -Vector2Df.Dot(dirA, posBToA);
                        if (posBProjection > -XGeometry2D.Epsilon_f)
                        {
                            var rayPointA = posA + (dirA * posBProjection);
                            var rayPointB = posB;
                            return Vector2Df.Distance(in rayPointA, in rayPointB);
                        }
                    }
                    else if (distanceB > -XGeometry2D.Epsilon_f)
                    {
                        var posAProjection = Vector2Df.Dot(in dirB, in posBToA);
                        if (posAProjection > -XGeometry2D.Epsilon_f)
                        {
                            var rayPointA = posA;
                            var rayPointB = posB + (dirB * posAProjection);
                            return Vector2Df.Distance(in rayPointA, in rayPointB);
                        }
                    }
                    return Vector2Df.Distance(in posA, in posB);
                }
            }
            // Point intersection
            return 0;
        }
        #endregion

        #region Ray - Segment 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на луче и сегменте.
        /// </summary>
        /// <param name="ray">Луч.</param>
        /// <param name="segment">Отрезок.</param>
        /// <returns>Расстояние.</returns>
        public static float RaySegment(in Ray2Df ray, in Segment2Df segment)
        {
            return RaySegment(in ray.Position, in ray.Direction, in segment.Start, in segment.End);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на луче и сегменте.
        /// </summary>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <returns>Расстояние.</returns>
        public static float RaySegment(in Vector2Df rayPos, in Vector2Df rayDir, in Vector2Df start, in Vector2Df end)
        {
            var startCopy = start;
            var endCopy = end;
            var segmentStartToPos = rayPos - startCopy;
            var segmentDirection = endCopy - startCopy;
            var denominator = Vector2Df.DotPerp(in rayDir, in segmentDirection);
            var perpDotA = Vector2Df.DotPerp(in rayDir, in segmentStartToPos);

            // Normalized Direction gives more stable results 
            var perpDotB = Vector2Df.DotPerp(segmentDirection.Normalized, in segmentStartToPos);

            if (Math.Abs(denominator) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                var segmentStartProjection = -Vector2Df.Dot(in rayDir, in segmentStartToPos);
                var originToSegmentB = endCopy - rayPos;
                var segmentEndProjection = Vector2Df.Dot(in rayDir, in originToSegmentB);
                if (Math.Abs(perpDotA) > XGeometry2D.Epsilon_f || Math.Abs(perpDotB) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    if (segmentStartProjection > -XGeometry2D.Epsilon_f)
                    {
                        var distanceSqr = segmentStartToPos.SqrLength - (segmentStartProjection * segmentStartProjection);
                        // distanceSqr can be negative
                        return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
                    }
                    if (segmentEndProjection > -XGeometry2D.Epsilon_f)
                    {
                        var distanceSqr = originToSegmentB.SqrLength - (segmentEndProjection * segmentEndProjection);
                        // distanceSqr can be negative
                        return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
                    }

                    if (segmentStartProjection > segmentEndProjection)
                    {
                        return Vector2Df.Distance(in rayPos, in startCopy);
                    }
                    return Vector2Df.Distance(in rayPos, in endCopy);
                }
                // Collinear
                if (segmentStartProjection > -XGeometry2D.Epsilon_f || segmentEndProjection > -XGeometry2D.Epsilon_f)
                {
                    // Point or segment intersection
                    return 0;
                }
                // No intersection
                return segmentStartProjection > segmentEndProjection ? -segmentStartProjection : -segmentEndProjection;
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
                        var rayPoint = rayPos + (rayDir * segmentStartProjection);
                        var segmentPoint = startCopy;
                        return Vector2Df.Distance(in rayPoint, in segmentPoint);
                    }
                    else
                    {
                        var rayPoint = rayPos + (rayDir * segmentEndProjection);
                        var segmentPoint = endCopy;
                        return Vector2Df.Distance(in rayPoint, in segmentPoint);
                    }
                }
                else if (!segmentStartOnRay && segmentEndOnRay)
                {
                    if (segmentDistance < 0)
                    {
                        var rayPoint = rayPos;
                        var segmentPoint = startCopy;
                        return Vector2Df.Distance(in rayPoint, in segmentPoint);
                    }
                    else if (segmentDistance > 1 + XGeometry2D.Epsilon_f)
                    {
                        var rayPoint = rayPos + (rayDir * segmentEndProjection);
                        var segmentPoint = endCopy;
                        return Vector2Df.Distance(in rayPoint, in segmentPoint);
                    }
                    else
                    {
                        var rayPoint = rayPos;
                        var posProjection = Vector2Df.Dot(in segmentDirection, in segmentStartToPos);
                        var segmentPoint = startCopy + (segmentDirection * posProjection / segmentDirection.SqrLength);
                        return Vector2Df.Distance(in rayPoint, in segmentPoint);
                    }
                }
                else
                {
                    // Not on ray
                    var rayPoint = rayPos;
                    var posProjection = Vector2Df.Dot(in segmentDirection, in segmentStartToPos);
                    var sqrSegmentLength = segmentDirection.SqrLength;
                    if (posProjection < 0)
                    {
                        return Vector2Df.Distance(in rayPoint, in startCopy);
                    }
                    else if (posProjection > sqrSegmentLength)
                    {
                        return Vector2Df.Distance(in rayPoint, in endCopy);
                    }
                    else
                    {
                        var segmentPoint = startCopy + (segmentDirection * posProjection / sqrSegmentLength);
                        return Vector2Df.Distance(in rayPoint, in segmentPoint);
                    }
                }
            }
            // Point intersection
            return 0;
        }
        #endregion

        #region Ray - Circle 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на луче и окружности.
        /// </summary>
        /// <param name="ray">Луч.</param>
        /// <param name="circle">Окружность.</param>
        /// <returns>Расстояние.</returns>
        public static float RayCircle(in Ray2Df ray, in Circle2Df circle)
        {
            return RayCircle(in ray.Position, in ray.Direction, in circle.Center, circle.Radius);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на луче и окружности.
        /// </summary>
        /// <param name="rayPos">Позиция луча.</param>
        /// <param name="rayDir">Направление луча.</param>
        /// <param name="circleCenter">Центр окружности.</param>
        /// <param name="circleRadius">Радиус окружности.</param>
        /// <returns>Расстояние.</returns>
        public static float RayCircle(in Vector2Df rayPos, in Vector2Df rayDir, in Vector2Df circleCenter,
                float circleRadius)
        {
            var posToCenter = circleCenter - rayPos;
            var centerProjection = Vector2Df.Dot(in rayDir, in posToCenter);
            if (centerProjection + circleRadius < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                return XMath.Sqrt(posToCenter.SqrLength) - circleRadius;
            }

            var sqrDistanceToPos = posToCenter.SqrLength;
            var sqrDistanceToLine = sqrDistanceToPos - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (circleRadius * circleRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < -XGeometry2D.Epsilon_f)
                {
                    return XMath.Sqrt(sqrDistanceToPos) - circleRadius;
                }
                return XMath.Sqrt(sqrDistanceToLine) - circleRadius;
            }
            if (sqrDistanceToIntersection < XGeometry2D.Epsilon_f)
            {
                if (centerProjection < -XGeometry2D.Epsilon_f)
                {
                    // No intersection
                    return XMath.Sqrt(sqrDistanceToPos) - circleRadius;
                }
                // Point intersection
                return 0;
            }

            // Line intersection
            var distanceToIntersection = XMath.Sqrt(sqrDistanceToIntersection);
            var distanceA = centerProjection - distanceToIntersection;
            var distanceB = centerProjection + distanceToIntersection;

            if (distanceA < -XGeometry2D.Epsilon_f)
            {
                if (distanceB < -XGeometry2D.Epsilon_f)
                {
                    // No intersection
                    return XMath.Sqrt(sqrDistanceToPos) - circleRadius;
                }

                // Point intersection;
                return 0;
            }

            // Two points intersection;
            return 0;
        }
        #endregion

        #region Segment - Segment 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на отрезках.
        /// </summary>
        /// <param name="segment1">Первый отрезок.</param>
        /// <param name="segment2">Второй отрезок.</param>
        /// <returns>Расстояние.</returns>
        public static float SegmentSegment(in Segment2Df segment1, in Segment2Df segment2)
        {
            return SegmentSegment(in segment1.Start, in segment1.End, in segment2.Start, in segment2.End);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на отрезках.
        /// </summary>
        /// <param name="segment1Start">Начало первого отрезка.</param>
        /// <param name="segment1End">Окончание первого отрезка.</param>
        /// <param name="segment2Start">Начало второго отрезка.</param>
        /// <param name="segment2End">Окончание второго отрезка.</param>
        /// <returns>Расстояние.</returns>
        public static float SegmentSegment(in Vector2Df segment1Start, in Vector2Df segment1End,
            in Vector2Df segment2Start, in Vector2Df segment2End)
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
                return Vector2Df.Distance(in segment1Start, in segment2StartCopy);
            }
            if (segment1IsAPoint)
            {
                direction2.Normalize();
                return PointSegment(in segment1Start, in segment2StartCopy, in segment2EndCopy, in direction2, segment2Length);
            }
            if (segment2IsAPoint)
            {
                direction1.Normalize();
                return PointSegment(in segment2StartCopy, in segment1Start, in segment1End, in direction1, segment1Length);
            }

            direction1.Normalize();
            direction2.Normalize();
            var denominator = Vector2Df.DotPerp(in direction1, in direction2);
            var perpDot1 = Vector2Df.DotPerp(in direction1, in from2StartTo1Start);
            var perpDot2 = Vector2Df.DotPerp(in direction2, in from2StartTo1Start);

            if (Math.Abs(denominator) < XGeometry2D.Epsilon_f)
            {
                // Parallel
                if (Math.Abs(perpDot1) > XGeometry2D.Epsilon_f || Math.Abs(perpDot2) > XGeometry2D.Epsilon_f)
                {
                    // Not collinear
                    var segment2AProjection = -Vector2Df.Dot(in direction1, in from2StartTo1Start);
                    if (segment2AProjection > -XGeometry2D.Epsilon_f &&
                        segment2AProjection < segment1Length + XGeometry2D.Epsilon_f)
                    {
                        var distanceSqr = from2StartTo1Start.SqrLength - (segment2AProjection * segment2AProjection);
                        // distanceSqr can be negative
                        return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
                    }

                    var from1ATo2B = segment2EndCopy - segment1Start;
                    var segment2BProjection = Vector2Df.Dot(in direction1, in from1ATo2B);
                    if (segment2BProjection > -XGeometry2D.Epsilon_f &&
                        segment2BProjection < segment1Length + XGeometry2D.Epsilon_f)
                    {
                        var distanceSqr = from1ATo2B.SqrLength - (segment2BProjection * segment2BProjection);
                        // distanceSqr can be negative
                        return distanceSqr <= 0 ? 0 : XMath.Sqrt(distanceSqr);
                    }

                    if (segment2AProjection < 0 && segment2BProjection < 0)
                    {
                        if (segment2AProjection > segment2BProjection)
                        {
                            return Vector2Df.Distance(in segment1Start, in segment2StartCopy);
                        }
                        return Vector2Df.Distance(in segment1Start, in segment2EndCopy);
                    }
                    if (segment2AProjection > 0 && segment2BProjection > 0)
                    {
                        if (segment2AProjection < segment2BProjection)
                        {
                            return Vector2Df.Distance(in segment1End, in segment2StartCopy);
                        }
                        return Vector2Df.Distance(in segment1End, in segment2EndCopy);
                    }
                    var segment1AProjection = Vector2Df.Dot(in direction2, in from2StartTo1Start);
                    var segment2Point = segment2StartCopy + (direction2 * segment1AProjection);
                    return Vector2Df.Distance(in segment1Start, in segment2Point);
                }
                // Collinear

                var codirected = Vector2Df.Dot(in direction1, in direction2) > 0;
                if (codirected)
                {
                    // Codirected
                    var segment2AProjection = -Vector2Df.Dot(in direction1, in from2StartTo1Start);
                    if (segment2AProjection > -XGeometry2D.Epsilon_f)
                    {
                        // 1A------1B
                        //     2A------2B
                        return SegmentSegmentCollinear(in segment1Start, in segment1End, in segment2StartCopy);
                    }
                    else
                    {
                        //     1A------1B
                        // 2A------2B
                        return SegmentSegmentCollinear(in segment2StartCopy, in segment2EndCopy, in segment1Start);
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
                        return SegmentSegmentCollinear(in segment1Start, in segment1End, in segment2EndCopy);
                    }
                    else
                    {
                        //     1A------1B
                        // 2B------2A
                        return SegmentSegmentCollinear(in segment2EndCopy, in segment2StartCopy, in segment1Start);
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
                Vector2Df segment1Point;
                Vector2Df segment2Point;

                var segment2AProjection = -Vector2Df.Dot(in direction1, in from2StartTo1Start);
                var segment2BProjection = Vector2Df.Dot(in direction1, in from1ATo2B);

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
                return Vector2Df.Distance(in segment1Point, in segment2Point);
            }

            // Point intersection
            return 0;
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на отрезках.
        /// </summary>
        /// <param name="left_a">Начало отрезка слева.</param>
        /// <param name="left_b">Конец отрезка слева.</param>
        /// <param name="right_a">Начало отрезка справа.</param>
        /// <returns>Расстояние.</returns>
        private static float SegmentSegmentCollinear(in Vector2Df left_a, in Vector2Df left_b, in Vector2Df right_a)
        {
            var leftDirection = left_b - left_a;
            var rightAProjection = Vector2Df.Dot(leftDirection.Normalized, right_a - left_b);
            if (Math.Abs(rightAProjection) < XGeometry2D.Epsilon_f)
            {
                // LB == RA
                // LA------LB
                //         RA------RB

                // Point intersection
                return 0;
            }
            if (rightAProjection < 0)
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
            return rightAProjection;
        }
        #endregion

        #region Segment - Circle 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на отрезке и окружности.
        /// </summary>
        /// <param name="segment">Отрезок.</param>
        /// <param name="circle">Окружность.</param>
        /// <returns>Расстояние.</returns>
        public static float SegmentCircle(in Segment2Df segment, in Circle2Df circle)
        {
            return SegmentCircle(in segment.Start, in segment.End, in circle.Center, circle.Radius);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на отрезке и окружности.
        /// </summary>
        /// <param name="start">Начало отрезка.</param>
        /// <param name="end">Конец отрезка.</param>
        /// <param name="circleCenter">Центр окружности.</param>
        /// <param name="circleRadius">Радиус окружности.</param>
        /// <returns>Расстояние.</returns>
        public static float SegmentCircle(in Vector2Df start, in Vector2Df end, in Vector2Df circleCenter, float circleRadius)
        {
            var segmentStartToCenter = circleCenter - start;
            var fromStartToEnd = end - start;
            var segmentLength = fromStartToEnd.Length;
            if (segmentLength < XGeometry2D.Epsilon_f)
            {
                return segmentStartToCenter.Length - circleRadius;
            }

            var segmentDirection = fromStartToEnd.Normalized;
            var centerProjection = Vector2Df.Dot(in segmentDirection, in segmentStartToCenter);
            if (centerProjection + circleRadius < -XGeometry2D.Epsilon_f ||
                centerProjection - circleRadius > segmentLength + XGeometry2D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < 0)
                {
                    return XMath.Sqrt(segmentStartToCenter.SqrLength) - circleRadius;
                }
                return (circleCenter - end).Length - circleRadius;
            }

            var sqrDistanceToA = segmentStartToCenter.SqrLength;
            var sqrDistanceToLine = sqrDistanceToA - (centerProjection * centerProjection);
            var sqrDistanceToIntersection = (circleRadius * circleRadius) - sqrDistanceToLine;
            if (sqrDistanceToIntersection < -XGeometry2D.Epsilon_f)
            {
                // No intersection
                if (centerProjection < -XGeometry2D.Epsilon_f)
                {
                    return XMath.Sqrt(sqrDistanceToA) - circleRadius;
                }
                if (centerProjection > segmentLength + XGeometry2D.Epsilon_f)
                {
                    return (circleCenter - end).Length - circleRadius;
                }
                return XMath.Sqrt(sqrDistanceToLine) - circleRadius;
            }

            if (sqrDistanceToIntersection < XGeometry2D.Epsilon_f)
            {
                if (centerProjection < -XGeometry2D.Epsilon_f)
                {
                    // No intersection
                    return XMath.Sqrt(sqrDistanceToA) - circleRadius;
                }
                if (centerProjection > segmentLength + XGeometry2D.Epsilon_f)
                {
                    // No intersection
                    return (circleCenter - end).Length - circleRadius;
                }
                // Point intersection
                return 0;
            }

            // Line intersection
            var distanceToIntersection = XMath.Sqrt(sqrDistanceToIntersection);
            var distanceA = centerProjection - distanceToIntersection;
            var distanceB = centerProjection + distanceToIntersection;

            var pointAIsAfterSegmentStart = distanceA > -XGeometry2D.Epsilon_f;
            var pointBIsBeforeSegmentEnd = distanceB < segmentLength + XGeometry2D.Epsilon_f;

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

            var pointAIsBeforeSegmentEnd = distanceA < segmentLength + XGeometry2D.Epsilon_f;
            if (pointAIsAfterSegmentStart && pointAIsBeforeSegmentEnd)
            {
                // Point A intersection
                return 0;
            }
            var pointBIsAfterSegmentStart = distanceB > -XGeometry2D.Epsilon_f;
            if (pointBIsAfterSegmentStart && pointBIsBeforeSegmentEnd)
            {
                // Point B intersection
                return 0;
            }

            // No intersection
            if (centerProjection < 0)
            {
                return XMath.Sqrt(sqrDistanceToA) - circleRadius;
            }
            return (circleCenter - end).Length - circleRadius;
        }
        #endregion

        #region Circle - Circle 
        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на окружностях.
        /// </summary>
        /// <param name="circleA">Первая окружность.</param>
        /// <param name="circleB">Вторая окружность.</param>
        /// <returns>
        /// Положительное значение, если окружности не пересекаются, отрицательное иначе
        /// Отрицательная величина может быть интерпретирована как глубина проникновения
        /// </returns>
        public static float CircleCircle(in Circle2Df circleA, in Circle2Df circleB)
        {
            return CircleCircle(in circleA.Center, circleA.Radius, in circleB.Center, circleB.Radius);
        }

        /// <summary>
        /// Вычисление расстояние между самыми близкими точками на окружностях.
        /// </summary>
        /// <param name="centerA">Центр первой окружности.</param>
        /// <param name="radiusA">Радиус первой окружности.</param>
        /// <param name="centerB">Центр второй окружности.</param>
        /// <param name="radiusB">Радиус второй окружности.</param>
        /// <returns>
        /// Положительное значение, если окружности не пересекаются, отрицательное иначе
        /// Отрицательная величина может быть интерпретирована как глубина проникновения
        /// </returns>
        public static float CircleCircle(in Vector2Df centerA, float radiusA, in Vector2Df centerB,
                float radiusB)
        {
            return Vector2Df.Distance(in centerA, in centerB) - radiusA - radiusB;
        }
        #endregion
    }
    /**@}*/
}