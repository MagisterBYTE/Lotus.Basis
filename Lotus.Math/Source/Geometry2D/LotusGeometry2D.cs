using System.Collections.Generic;

namespace Lotus.Maths
{
    /**
     * \defgroup MathGeometry2D Подсистема 2D геометрии
     * \ingroup Math
     * \brief Подсистема 2D геометрии реализует работу с геометрическими данными в 2D пространстве.
     * \details Сюда входит математические структуры данных для работы в 2D пространстве, алгоритмы поиска и нахождения 
        ближайших точек проекции, пересечения и вычисления дистанции для основных геометрических тел/примитивов.
     * @{
     */
    /// <summary>
    /// Статический класс для реализации вспомогательных методов для работы с 2D пространством.
    /// </summary>
    public static class XGeometry2D
    {
        #region Fields
        /// <summary>
        /// Точность вещественного числа используемого при операция поиска/пересечения геометрических примитивов.
        /// </summary>
        /// <remarks>
        /// Это не константа, её можно регулировать для обеспечения нужной точности вычислений.
        /// </remarks>
        public static float Eplsilon_f = 0.001f;

        /// <summary>
        /// Точность вещественного числа используемого при операция поиска/пересечения геометрических примитивов.
        /// </summary>
        /// <remarks>
        /// Это не константа, её можно регулировать для обеспечения нужной точности вычислений.
        /// </remarks>
        public static float Eplsilon_d = 0.00f;

#pragma warning disable 0414
        //
        // Здесь приняты текстурные координаты как в OpenGL - начало координат нижний-левый угол
        // В Unity используется такая же система координат
        //
        /// <summary>
        /// Текстурные координаты.
        /// </summary>
        public static readonly Vector2Df MapUVTopLeft = new Vector2Df(0, 1);

        /// <summary>
        /// Текстурные координаты.
        /// </summary>
        public static readonly Vector2Df MapUVTopCenter = new Vector2Df(0.5f, 1);

        /// <summary>
        /// Текстурные координаты.
        /// </summary>
        public static readonly Vector2Df MapUVTopRight = new Vector2Df(1, 1);

        /// <summary>
        /// Текстурные координаты.
        /// </summary>
        public static readonly Vector2Df MapUVMiddleLeft = new Vector2Df(0, 0.5f);

        /// <summary>
        /// Текстурные координаты.
        /// </summary>
        public static readonly Vector2Df MapUVMiddleCenter = new Vector2Df(0.5f, 0.5f);

        /// <summary>
        /// Текстурные координаты.
        /// </summary>
        public static readonly Vector2Df MapUVMiddleRight = new Vector2Df(1, 0.5f);

        /// <summary>
        /// Текстурные координаты.
        /// </summary>
        public static readonly Vector2Df MapUVBottomLeft = new Vector2Df(0, 0);

        /// <summary>
        /// Текстурные координаты.
        /// </summary>
        public static readonly Vector2Df MapUVBottomCenter = new Vector2Df(0.5f, 0);

        /// <summary>
        /// Текстурные координаты.
        /// </summary>
        public static readonly Vector2Df MapUVBottomRight = new Vector2Df(1, 0);
#pragma warning restore 0414
        #endregion

        #region Main methods
        /// <summary>
        /// Получение точки на окружности в плоскости XY.
        /// </summary>
        /// <param name="radius">Радиус окружности.</param>
        /// <param name="angle">Угол в градусах.</param>
        /// <returns>Точка.</returns>
        public static Vector2Df PointOnCircle(float radius, float angle)
        {
            var angle_in_radians = angle * XMath.DegreeToRadian_F;
            return new Vector2Df(radius * XMath.Sin(angle_in_radians), radius * XMath.Cos(angle_in_radians));
        }

        /// <summary>
        /// Получение списка точек на окружности в плоскости XY.
        /// </summary>
        /// <param name="radius">Радиус окружности.</param>
        /// <param name="segments">Количество сегментов окружности.</param>
        /// <returns>Список точек.</returns>
        public static List<Vector2Df> PointsOnCircle(float radius, int segments)
        {
            var segment_angle = 360f / segments;
            float current_angle = 0;
            var ring = new List<Vector2Df>(segments);
            for (var i = 0; i < segments; i++)
            {
                ring.Add(PointOnCircle(radius, current_angle));
                current_angle += segment_angle;
            }
            return ring;
        }
        #endregion
    }
    /**@}*/
}