using System;

namespace Lotus.Maths
{
    /** \addtogroup MathRandom
    *@{*/
    /// <summary>
    /// Современный генератор псевдослучайных значений на основе алгоритма Xorshift+.
    /// </summary>
    /// <remarks>
    /// Xorshift+ является улучшенной версией классического Xorshift. 
    /// Он обладает периодом 2^128 - 1 и обеспечивает отличную статистическую равномерность, 
    /// превосходя Ranq1 по качеству случайных чисел.
    /// </remarks>
    public class RandomXorShift : ILotusRandom
    {
        #region Const
        /// <summary>
        /// Коэффициент перевода в вещественное число (1.0 / 2^32).
        /// </summary>
        private const float TO_SINGLE_COEFF = 1.0f / 4294967296.0f;

        /// <summary>
        /// Константа для инициализации второго состояния.
        /// </summary>
        private const ulong SEED_MIXER = 0x9E3779B97F4A7C15UL;
        #endregion

        #region Fields
        /// <summary>
        /// Первое 64-битное состояние генератора.
        /// </summary>
        private ulong _s0;

        /// <summary>
        /// Второе 64-битное состояние генератора.
        /// </summary>
        private ulong _s1;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию, использующий системное время для инициализации.
        /// </summary>
        public RandomXorShift()
            : this((uint)DateTime.Now.Ticks)
        {
        }

        /// <summary>
        /// Конструктор с заданным 32-битным зерном (seed).
        /// </summary>
        /// <param name="seed">Базовое число для инициализации состояния.</param>
        public RandomXorShift(uint seed)
        {
            // Инициализируем два состояния, чтобы они не были нулевыми
            this._s0 = seed | ((ulong)seed << 32);
            this._s1 = this._s0 ^ SEED_MIXER;

            // "Прогрев" генератора для стабилизации последовательности
            for (int i = 0; i < 4; i++) NextInteger();
        }
        #endregion

        #region ILotusRandom methods
        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [0 - 1).
        /// </summary>
        /// <returns>Псевдослучайное число Single.</returns>
        public float NextSingle()
        {
            return NextInteger() * TO_SINGLE_COEFF;
        }

        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        public float NextSingle(float max)
        {
            return NextSingle() * max;
        }

        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [min - max].
        /// </summary>
        public float NextSingle(float min, float max)
        {
            return min + (NextSingle() * (max - min));
        }

        /// <summary>
        /// Основной метод генерации: возвращает случайное 32-битное целое.
        /// </summary>
        /// <returns>Псевдослучайное число uint.</returns>
        public uint NextInteger()
        {
            ulong x = _s0;
            ulong y = _s1;

            _s0 = y;
            x ^= x << 23; // a
            _s1 = x ^ y ^ (x >> 17) ^ (y >> 26); // b, c

            // В алгоритме Xorshift+ результат — это сумма состояний.
            // Берем старшие 32 бита для лучшей энтропии.
            return (uint)((_s1 + y) >> 32);
        }

        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [0 - max).
        /// </summary>
        public uint NextInteger(uint max)
        {
            if (max == 0) return 0;
            return NextInteger() % max;
        }

        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [min - max).
        /// </summary>
        public uint NextInteger(uint min, uint max)
        {
            uint delta = max - min;
            if (delta == 0) return min;
            return min + (NextInteger() % delta);
        }

        /// <summary>
        /// Заполняет массив байт случайными значениями.
        /// </summary>
        /// <param name="buffer">Массив для заполнения.</param>
        public void NextBytes(byte[] buffer)
        {
            if (buffer == null) return;

            for (int i = 0; i < buffer.Length; i++)
            {
                // Каждые 4 байта мы могли бы генерировать новое число, 
                // но для простоты заполняем побайтово.
                buffer[i] = (byte)(NextInteger() & 0xFF);
            }
        }
        #endregion
        /**@}*/
    }
}