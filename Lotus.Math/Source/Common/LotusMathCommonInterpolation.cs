using System;

namespace Lotus.Maths
{
    /** \addtogroup MathCommon
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы интерполяции данных.
    /// </summary>
    public static class XMathInterpolation
    {
        #region Interpolation methods
        /// <summary>
        /// Линейная интерполяция значения.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static double Lerp(double start, double end, double time)
        {
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Линейная интерполяция значения.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static float Lerp(float start, float end, float time)
        {
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Линейная интерполяция значения.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static byte Lerp(byte start, byte end, float time)
        {
            return (byte)(start + ((end - start) * time));
        }

        /// <summary>
        /// Кривая интерполяции по Эрмиту.
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Hermite_interpolation.
        /// </remarks>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static double Hermite(double start, double end, double time)
        {
            return start + ((start - end) * (time * time * (3.0 - (2.0 * time))));
        }

        /// <summary>
        /// Кривая интерполяции по Эрмиту.
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Hermite_interpolation.
        /// </remarks>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static float Hermite(float start, float end, float time)
        {
            return start + ((start - end) * (time * time * (3.0f - (2.0f * time))));
        }

        /// <summary>
        /// Синусоидальная интерполяция, этот метод интерполирует, ослабляя итоговое значение.
        /// когда значение будет близко к границам.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static double Sinerp(double start, double end, double time)
        {
            return Lerp(start, end, Math.Sin(time * XMath.PI_2_D));
        }

        /// <summary>
        /// Синусоидальная интерполяция, этот метод интерполирует, ослабляя итоговое значение.
        /// когда значение будет близко к границам.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static float Sinerp(float start, float end, float time)
        {
            return Lerp(start, end, (float)Math.Sin(time * XMath.PI_2_D));
        }

        /// <summary>
        /// Синусоидальная интерполяция (cos), этот метод интерполирует, ослабляя итоговое значение.
        /// когда значение будет близко к границам.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static double Coserp(double start, double end, double time)
        {
            return Lerp(start, end, 1.0 - Math.Cos(time * XMath.PI_2_D));
        }

        /// <summary>
        /// Синусоидальная интерполяция (cos), этот метод интерполирует, ослабляя итоговое значение.
        /// когда значение будет близко к границам.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static float Coserp(float start, float end, float time)
        {
            return Lerp(start, end, 1.0f - (float)Math.Cos(time * XMath.PI_2_D));
        }

        /// <summary>
        /// Performs smooth (cubic Hermite) interpolation between 0 and 1.
        /// </summary>
        /// <remarks>
        /// See https://en.wikipedia.org/wiki/Smoothstep.
        /// </remarks>
        /// <param name="amount">Значение в границах от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static float SmoothStep(float amount)
        {
            if (amount >= 1)
            {
                return amount <= 0 ? 0 : 1;
            }
            else
            {
                return amount <= 0 ? 0 : (amount * amount * (3 - (2 * amount)));
            }
        }

        /// <summary>
        /// Performs a smooth(er) interpolation between 0 and 1 with 1st and 2nd order derivatives of zero at endpoints.
        /// </summary>
        /// <remarks>
        /// See https://en.wikipedia.org/wiki/Smoothstep.
        /// </remarks>
        /// <param name="amount">Значение в границах от 0 до 1.</param>
        /// <returns>Интерполированное значение.</returns>
        public static float SmootherStep(float amount)
        {
            if (amount >= 1)
            {
                return amount <= 0 ? 0 : 1;
            }
            else
            {
                return amount <= 0 ? 0 : (amount * amount * amount * ((amount * ((amount * 6) - 15)) + 10));
            }
        }

        /// <summary>
        /// Smoothstep - скалярная функция интерполяции, обычно используемая в компьютерной графике.
        /// Метод интерполирует итоговое значение между двумя входными значениями, основанными на третьем,
        /// который должен быть между первыми двумя. Возвращенное значение находится между 0 и 1.
        /// </summary>
        /// <param name="start">Минимальное значение.</param>
        /// <param name="end">Максимальное значение.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Интерполированное значение.</returns>
        public static double SmoothStep(double start, double end, double value)
        {
            value = XMath.Clamp(value, start, end);
            var v1 = (value - start) / (end - start);
            var v2 = (value - start) / (end - start);
            return (-2.0 * v1 * v1 * v1) + (3.0 * v2 * v2);
        }

        /// <summary>
        /// Smoothstep - скалярная функция интерполяции, обычно используемая в компьютерной графике.
        /// Метод интерполирует итоговое значение между двумя входными значениями, основанными на третьем,
        /// который должен быть между первыми двумя. Возвращенное значение находится между 0 и 1.
        /// </summary>
        /// <param name="start">Минимальное значение.</param>
        /// <param name="end">Максимальное значение.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Интерполированное значение.</returns>
        public static float SmoothStep(float start, float end, float value)
        {
            value = XMath.Clamp(value, start, end);
            var v1 = (value - start) / (end - start);
            var v2 = (value - start) / (end - start);
            return (-2.0f * v1 * v1 * v1) + (3.0f * v2 * v2);
        }

        /// <summary>
        /// Гауссова функция.
        /// </summary>
        ///<remarks>
        /// Смотри http://en.wikipedia.org/wiki/Gaussian_function#Two-dimensional_Gaussian_function.
        ///</remarks>
        /// <param name="amplitude">Curve amplitude.</param>
        /// <param name="x">Position X.</param>
        /// <param name="y">Position Y.</param>
        /// <param name="centerX">Center X.</param>
        /// <param name="centerY">Center Y.</param>
        /// <param name="sigmaX">Curve sigma X.</param>
        /// <param name="sigmaY">Curve sigma Y.</param>
        /// <returns>The result of Gaussian function.</returns>
        public static double Gauss(double amplitude, double x, double y, double centerX, double centerY,
                double sigmaX, double sigmaY)
        {
            var cx = x - centerX;
            var cy = y - centerY;

            var component_x = cx * cx / (2 * sigmaX * sigmaX);
            var component_y = cy * cy / (2 * sigmaY * sigmaY);

            return amplitude * Math.Exp(-(component_x + component_y));
        }

        /// <summary>
        /// Гауссова функция.
        /// </summary>
        ///<remarks>
        /// Смотри http://en.wikipedia.org/wiki/Gaussian_function#Two-dimensional_Gaussian_function.
        ///</remarks>
        /// <param name="amplitude">Curve amplitude.</param>
        /// <param name="x">Position X.</param>
        /// <param name="y">Position Y.</param>
        /// <param name="centerX">Center X.</param>
        /// <param name="centerY">Center Y.</param>
        /// <param name="sigmaX">Curve sigma X.</param>
        /// <param name="sigmaY">Curve sigma Y.</param>
        /// <returns>The result of Gaussian function.</returns>
        public static float Gauss(float amplitude, float x, float y, float centerX, float centerY,
                float sigmaX, float sigmaY)
        {
            var cx = x - centerX;
            var cy = y - centerY;

            var component_x = cx * cx / (2 * sigmaX * sigmaX);
            var component_y = cy * cy / (2 * sigmaY * sigmaY);

            return amplitude * (float)Math.Exp(-(component_x + component_y));
        }
        #endregion
    }
    /**@}*/
}