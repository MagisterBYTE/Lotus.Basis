using System;

namespace Lotus.Maths
{
    /** \addtogroup MathRandom
	*@{*/
    /// <summary>
    /// Статический класс реализующий различных методы генерации шума в одномерном пространстве.
    /// </summary>
    public static class XGenerationNoise1D
    {
        #region Fields 
        /// <summary>
        /// Случайное число источника.
        /// </summary>
        public static int Seed = 16;

        /// <summary>
        /// Целочисленная шумовая функция в одномерном пространстве.
        /// </summary>
        public static Func<int, int> NoiseInteger1D = NoiseInteger1DV1;

        /// <summary>
        /// Вещественная шумовая функция в одномерном пространстве.
        /// </summary>
        public static Func<float, float>? NoiseSingle1D;

        #endregion

        #region Main methods
        /// <summary>
        /// Целочисленная шумовая функция в одномерном пространстве.
        /// Алгоритм Либермана/Hugo Elias
        /// </summary>
        /// <remarks>
        /// Получает равномерную случайную величину, постоянную для конкретных параметров
        /// <see href="http://www.gamedev.ru/code/forum/?id=215866"/>
        /// </remarks>
        /// <param name="value">Значение.</param>
        /// <returns>Случайная зависимая величина.</returns>
        public static int NoiseInteger1DV1(int value)
        {
            var m = value + XGenerationNoise1D.Seed;
            m = (m >> 13) ^ m;
            var nn = ((m * ((m * m * 60493) + 19990303)) + 1376312589) & 0x7fffffff;
            return nn;
        }

        /// <summary>
        /// Целочисленная шумовая функция в одномерном пространстве.
        /// </summary>
        /// <remarks>
        /// Получает равномерную случайную величину, постоянную для конкретных параметров
        /// <see href="http://www.gamedev.ru/code/forum/?id=215866"/>
        /// </remarks>
        /// <param name="value">Значение.</param>
        /// <returns>Случайная зависимая величина.</returns>
        public static int NoiseInteger1DV2(int value)
        {
            var state = (ulong)(value + XGenerationNoise1D.Seed);
            state *= state;
            state = (state * 6364136223846793005UL) + 1442695040888963407UL;
            var xorshifted = (long)(((state >> 18) ^ state) >> 27);
            var rot = (int)(state >> 59);
            var v1 = xorshifted >> rot;
            var v2 = xorshifted << (-rot & 31);
            return (int)(v1 | v2);
        }
        #endregion
    }
    /**@}*/
}