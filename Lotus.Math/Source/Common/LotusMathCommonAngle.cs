using System;

namespace Lotus.Maths
{

    /**
     * \defgroup MathCommon Общая подсистема
     * \ingroup Math
     * \brief Общая математическая подсистема реализует работу с основными, общими структурами данных.
     * \details Сюда входит работа с углами, методы расширений для потоков данных применительно к математическим 
        структурам, математические вычисления основанные на предварительно вычисленных(кэшированных) данных.
     * @{
     */
    /// <summary>
    /// Статический класс реализующий методов работы с углами и различным единицами их представления.
    /// </summary>
    public static class XMathAngle
    {
        #region Main methods
        /// <summary>
        /// Нормализация угла в пределах от 0 до 360.
        /// </summary>
        /// <param name="angle">Угол, задается в градусах.</param>
        /// <returns>Нормализованный угол в пределах от 0 до 360.</returns>
        public static double NormalizationFull(double angle)
        {
            var degree_norm = angle;
            if (angle >= 360.0 || angle < 0.0)
            {
                degree_norm -= Math.Floor(angle / 360.0) * 360.0;
            }
            return degree_norm;
        }

        /// <summary>
        /// Нормализация угла в пределах от 0 до 360.
        /// </summary>
        /// <param name="angle">Угол, задается в градусах.</param>
        /// <returns>Нормализованный угол в пределах от 0 до 360.</returns>
        public static float NormalizationFull(float angle)
        {
            var degree_norm = angle;
            if (angle >= 360.0f || angle < 0.0f)
            {
                degree_norm -= (float)Math.Floor(angle / 360.0f) * 360.0f;
            }
            return degree_norm;
        }

        /// <summary>
        /// Нормализация угла в пределах от -180 до 180.
        /// </summary>
        /// <param name="angle">Угол, задается в градусах.</param>
        /// <returns>Нормализованный угол в пределах от -180 до 180.</returns>
        public static double NormalizationHalf(double angle)
        {
            var degree_norm = angle;
            if (angle >= 360.0 || angle < 0.0)
            {
                degree_norm -= Math.Floor(angle / 360.0) * 360.0;
            }
            if (degree_norm > 180.0)
            {
                degree_norm -= 360.0;
            }
            return degree_norm;
        }

        /// <summary>
        /// Нормализация угла в пределах от -180 до 180.
        /// </summary>
        /// <param name="angle">Угол, задается в градусах.</param>
        /// <returns>Нормализованный угол в пределах от -180 до 180.</returns>
        public static float NormalizationHalf(float angle)
        {
            var degree_norm = angle;
            if (angle >= 360.0f || angle < 0.0f)
            {
                degree_norm -= (float)Math.Floor(angle / 360.0f) * 360.0f;
            }
            if (degree_norm > 180.0f)
            {
                degree_norm -= 360.0f;
            }
            return degree_norm;
        }

        /// <summary>
        /// Ограничение угла.
        /// </summary>
        /// <param name="angle">Угол, задается в градусах.</param>
        /// <param name="min">Минимальный угол.</param>
        /// <param name="max">Максимальный угол.</param>
        /// <returns>Нормализованный угол.</returns>
        public static double Clamp(double angle, double min, double max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;

            if (angle > max)
            {
                return max;
            }
            if (angle < min)
            {
                return min;
            }

            return angle;
        }

        /// <summary>
        /// Ограничение угла.
        /// </summary>
        /// <param name="angle">Угол, задается в градусах.</param>
        /// <param name="min">Минимальный угол.</param>
        /// <param name="max">Максимальный угол.</param>
        /// <returns>Нормализованный угол.</returns>
        public static float Clamp(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;

            if (angle > max)
            {
                return max;
            }
            if (angle < min)
            {
                return min;
            }

            return angle;
        }

        /// <summary>
        /// Преобразование количества оборотов в градусы.
        /// </summary>
        /// <param name="revolution">Количество оборотов.</param>
        /// <returns>Количество градусов.</returns>
        public static double RevolutionsToDegrees(double revolution)
        {
            return revolution * 360.0;
        }

        /// <summary>
        /// Преобразование количества оборотов в градусы.
        /// </summary>
        /// <param name="revolution">Количество оборотов.</param>
        /// <returns>Количество градусов.</returns>
        public static float RevolutionsToDegrees(float revolution)
        {
            return revolution * 360.0f;
        }

        /// <summary>
        /// Преобразование количества оборотов в радианы.
        /// </summary>
        /// <param name="revolution">Количество оборотов.</param>
        /// <returns>Количество радиан.</returns>
        public static double RevolutionsToRadians(double revolution)
        {
            return revolution * XMath.PI2_D;
        }

        /// <summary>
        /// Преобразование количества оборотов в радианы.
        /// </summary>
        /// <param name="revolution">Количество оборотов.</param>
        /// <returns>Количество радиан.</returns>
        public static float RevolutionsToRadians(float revolution)
        {
            return revolution * XMath.PI2_F;
        }

        /// <summary>
        /// Преобразование количества оборотов в грады.
        /// </summary>
        /// <remarks>
        /// Град — сотая часть прямого угла.
        /// </remarks>
        /// <param name="revolution">Количество оборотов.</param>
        /// <returns>Количество град.</returns>
        public static double RevolutionsToGradians(double revolution)
        {
            return revolution * 400.0;
        }

        /// <summary>
        /// Преобразование количества оборотов в грады.
        /// </summary>
        /// <remarks>
        /// Град — сотая часть прямого угла.
        /// </remarks>
        /// <param name="revolution">Количество оборотов.</param>
        /// <returns>Количество град.</returns>
        public static float RevolutionsToGradians(float revolution)
        {
            return revolution * 400.0f;
        }
        #endregion
    }
    /**@}*/
}