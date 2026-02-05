using System;

namespace Lotus.Maths
{
    /** \addtogroup MathRandom
	*@{*/
    /// <summary>
    /// Статический класс реализующий различных методы генерации шума в двухмерном пространстве.
    /// </summary>
    public static class XGenerationNoise2D
    {
        #region Fields 
        /// <summary>
        /// Случайное число источника.
        /// </summary>
        public static int Seed = 16;

        /// <summary>
        /// Целочисленная шумовая функция в двухмерном пространстве.
        /// </summary>
        public static Func<int, int, int> NoiseInteger2D = NoiseInteger2DV1;

        /// <summary>
        /// Вещественная шумовая функция в двухмерном пространстве.
        /// </summary>
        public static Func<float, float, float> NoiseSingle2D = NoiseSingle2DV1;
        #endregion

        #region Main methods
        /// <summary>
        /// Вычисление значение сглаженного шума по указанным координатам.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>Сглаженное значение шума.</returns>
        public static float SmoothNoiseSingle2D(float x, float y)
        {
            var corners = (NoiseSingle2D(x - 1, y - 1) +
                NoiseSingle2D(x + 1, y - 1) +
                NoiseSingle2D(x - 1, y + 1) +
                NoiseSingle2D(x + 1, y + 1)) / 16;
            var sides = (NoiseSingle2D(x - 1, y) +
                NoiseSingle2D(x + 1, y) +
                NoiseSingle2D(x, y - 1) +
                NoiseSingle2D(x, y + 1)) / 8;
            var center = NoiseSingle2D(x, y) / 4;

            return corners + sides + center;
        }

        /// <summary>
        /// Вычисление значение интерполированного шума по указанным координатам.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>Интерполированное значение шума.</returns>
        public static float InterpolatedNoiseSingle2D(float x, float y)
        {
            // Вычисляем целую и дробную часть по X
            var integerX = (int)x;
            var fractionalX = x - integerX;

            // Вычисляем целую и дробную часть по Y
            var integerY = (int)y;
            var fractionalY = y - integerY;

            var integerX1 = integerX + 1;
            var integerY1 = integerY + 1;

            // Получаем 4 сглаженных значения
            var v1 = SmoothNoiseSingle2D(integerX, integerY);
            var v2 = SmoothNoiseSingle2D(integerX1, integerY);
            var v3 = SmoothNoiseSingle2D(integerX, integerY1);
            var v4 = SmoothNoiseSingle2D(integerX1, integerY1);

            // Интерполируем значения 1 и 2 пары и производим интерполяцию между ними
            var i1 = XMathInterpolation.Lerp(v1, v2, fractionalX);
            var i2 = XMathInterpolation.Lerp(v3, v4, fractionalX);

            return XMathInterpolation.Lerp(i1, i2, fractionalY);
        }
        #endregion

        #region 2D methods
        /// <summary>
        /// Целочисленная шумовая функция в двухмерном пространстве.
        /// </summary>
        /// <remarks>
        /// Получает равномерную случайную величину, постоянную для конкретных параметров.
        /// </remarks>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>Случайная зависимая величина.</returns>
        public static int NoiseInteger2DV1(int x, int y)
        {
            var mW = 43;//x * 7 + y * 17 + x * y + 1; //x * 43 + 1;    /* must not be zero, nor 0x464fffff */
            var mZ = ((x * y * 57) + y) ^ (2 + (x * 7) + 1);    /* must not be zero, nor 0x9068ffff */

            mZ = (36969 * (mZ & 65535)) + (mZ >> 16);
            mW = (18000 * (mW & 65535)) + (mW >> 16);
            return (mZ << 16) + mW + Seed;
        }

        /// <summary>
        /// Целочисленная шумовая функция в двухмерном пространстве.
        /// </summary>
        /// <remarks>
        /// Получает равномерную случайную величину, постоянную для конкретных параметров.
        /// </remarks>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>Случайная зависимая величина.</returns>
        public static int NoiseInteger2DV2(int x, int y)
        {
            const int generatorNoiseX = 1619;
            const int generatorNoiseY = 31337;
            var n = ((generatorNoiseX * x) + (generatorNoiseY * y) + Seed) & 0x7fffffff;
            n = (n >> 13) ^ n;
            return ((n * ((n * n * 60493) + 19990303)) + 1376312589) & 0x7fffffff;
        }

        /// <summary>
        /// Вещественная шумовая функция в двухмерном пространстве.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>Случайная зависимая величина.</returns>
        public static float NoiseSingle2DV1(float x, float y)
        {
            var u = (uint)NoiseInteger2DV1((int)x, (int)y);
            return (u + 1.0f) * 2.328306435454494e-10f;
        }

        /// <summary>
        /// Вещественная шумовая функция в двухмерном пространстве.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>Случайная зависимая величина.</returns>
        public static float NoiseSingle2DV2(int x, int y)
        {
            return 1.0f - (NoiseInteger2DV2(x, y) / 1073741824.0f);
        }
        #endregion
    }
    /**@}*/
}