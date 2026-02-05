using System;

namespace Lotus.Maths
{
    /** \addtogroup MathRandom
    *@{*/
    /// <summary>
    /// Генератор псевдослучайных значений основанный на сдвигах (Xorshift).
    /// </summary>
    /// <remarks>
    /// Исправленная реализация Ranq1 из Numerical Recipes 3rd Edition. 
    /// ВНИМАНИЕ: Для корректной работы и периода 1.8 x 10^19 тип состояния изменен на ulong.
    /// </remarks>
    public class RandomShift : ILotusRandom
    {
        #region Const
        /// <summary>
        /// Recommended multiplier for D1 method (64-bit).
        /// </summary>
        private const ulong A = 2685821657736338717UL;

        /// <summary>
        /// First bit shift value.
        /// </summary>
        private const int A1 = 21;

        /// <summary>
        /// Second bit shift value.
        /// </summary>
        private const int A2 = 35;

        /// <summary>
        /// Third bit shift value.
        /// </summary>
        private const int A3 = 4;

        /// <summary>
        /// Initialization value (Константа для исключения нулевого состояния).
        /// </summary>
        private const ulong M = 4101842887655102017UL;

        /// <summary>
        /// Коэффициент перевода в вещественное число (1.0 / 2^32).
        /// Используем 1.0 / (2^64) для ulong или 1.0 / (2^32) для uint части
        /// Для float (Single) точности 1.0f / uint.MaxValue достаточно
        /// </summary>
        private const float TO_SINGLE_COEFF = 1.0f / 4294967296.0f;
        #endregion

        #region Fields
        /// <summary>
        /// Current state of the random number generation (64-bit state is mandatory for Ranq1).
        /// </summary>
        private ulong _value;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new random number generator with the current system time as seed.
        /// </summary>
        public RandomShift()
            : this((uint)DateTime.Now.Ticks)
        {
        }

        /// <summary>
        /// Constructs a new random number generator with the given 32-bit unsigned integer as seed.
        /// </summary>
        /// <param name="seed">32-bit unsigned integer to use as seed.</param>
        public RandomShift(uint seed)
        {
            // Смешиваем seed с константой M для инициализации 64-битного состояния
            this._value = M ^ (ulong)seed;
            // Прогреваем состояние первым проходом
            this._value = Step();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Внутренний шаг алгоритма Xorshift с умножением.
        /// </summary>
        private ulong Step()
        {
            this._value ^= this._value >> A1;
            this._value ^= this._value << A2;
            this._value ^= this._value >> A3;
            return this._value * A;
        }
        #endregion

        #region ILotusRandom methods
        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [0 - 1).
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle()
        {
            return NextInteger() * TO_SINGLE_COEFF;
        }

        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle(float max)
        {
            return NextSingle() * max;
        }

        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [min - max].
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle(float min, float max)
        {
            return min + (NextSingle() * (max - min));
        }

        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [0 - 4294967295].
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger()
        {
            // Выполняем 64-битный шаг и возвращаем старшие 32 бита (они наиболее хаотичны)
            return (uint)(Step() >> 32);
        }

        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger(uint max)
        {
            if (max == 0) return 0;
            return NextInteger() % max;
        }

        /// <summary>
        /// Получить следующее псевдослучайное число в диапазоне [min - max].
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger(uint min, uint max)
        {
            uint delta = max - min;
            if (delta == 0) return min;
            return min + (NextInteger() % delta);
        }
        #endregion
    }
    /**@}*/
}