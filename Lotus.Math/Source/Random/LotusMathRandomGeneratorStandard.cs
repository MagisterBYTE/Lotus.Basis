using System;

namespace Lotus.Maths
{
    /** \addtogroup MathRandom
	*@{*/
    /// <summary>
    /// Стандартный генератор .NET псевдослучайных значений.
    /// </summary>
    public class RandomStandard : ILotusRandom
    {
        #region Fields
        internal System.Random mRandom;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public RandomStandard()
        {
            mRandom = new Random(System.Environment.TickCount);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="seed">Начальное значение генератора.</param>
        public RandomStandard(int seed)
        {
            mRandom = new Random(seed);
        }
        #endregion

        #region ILotusRandom methods
        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - 1].
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle()
        {
            return (float)mRandom.NextDouble();
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle(float max)
        {
            return (float)mRandom.NextDouble() * max;
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [min - max].
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle(float min, float max)
        {
            return min + ((float)mRandom.NextDouble() * (max - min));
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - 4294967295].
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger()
        {
            return (uint)mRandom.Next(0, int.MaxValue);
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger(uint max)
        {
            return (uint)mRandom.Next((int)max);
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [min - max].
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger(uint min, uint max)
        {
            return (uint)mRandom.Next((int)min, (int)max);
        }
        #endregion
    }
    /**@}*/
}