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

    /// <summary>
    /// Генератор псевдослучайных значений - Вихрь Мерсенна.
    /// </summary>
    /// <remarks>
    /// https://ru.wikipedia.org/wiki/Вихрь_Мерсенна.
    /// </remarks>
    public class RandomMersenneTwister : ILotusRandom
    {
        #region Const
        /// <summary>
        /// Size parameters.
        /// </summary>
        private const short SIZE = 624;

        /// <summary>
        /// Period parameters.
        /// </summary>
        private const short PERIOD = 397;

        /// <summary>
        /// Constant vector a.
        /// </summary>
        private const uint MATRIX_A = 0x9908b0df;

        /// <summary>
        /// Most significant w-r bits.
        /// </summary>
        private const uint UPPER_MASK = 0x80000000;

        /// <summary>
        /// Least significant r bits.
        /// </summary>
        private const uint LOWER_MASK = 0x7fffffff;

        /// <summary>
        /// Коэффициент перевода в вещественное число.
        /// </summary>
        private const float TO_SINGLE_COEFF = 1.0f / uint.MaxValue;

        /// <summary>
        /// Array for the state vector.
        /// </summary>
        private readonly uint[] MASSIVE = new uint[SIZE];

        /// <summary>
        /// Magic number.
        /// </summary>
        private readonly uint[] MAG_01 = [0, MATRIX_A];
        #endregion

        #region Fields
        private ushort _mti;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public RandomMersenneTwister()
        {
            _mti = SIZE + 1;

            // auto generate seed for .NET
            var seed_key = new uint[6];
            var rnseed = new byte[8];

            seed_key[0] = (uint)System.DateTime.UtcNow.Millisecond;
            seed_key[1] = (uint)System.DateTime.UtcNow.Second;
            seed_key[2] = (uint)System.DateTime.UtcNow.DayOfYear;
            seed_key[3] = (uint)System.DateTime.UtcNow.Year;

            var rn =
                System.Security.Cryptography.RandomNumberGenerator.Create();
            rn.GetNonZeroBytes(rnseed);

            seed_key[4] = ((uint)rnseed[0] << 24) | ((uint)rnseed[1] << 16)
                | ((uint)rnseed[2] << 8) | rnseed[3];
            seed_key[5] = ((uint)rnseed[4] << 24) | ((uint)rnseed[5] << 16)
                | ((uint)rnseed[6] << 8) | rnseed[7];

            InitFromArray(seed_key);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="seed">Начальное значение генератора.</param>
        public RandomMersenneTwister(uint seed)
        {
            _mti = SIZE + 1;
            InitGenrand(seed);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="initKey">Начальное значение генератора.</param>
        public RandomMersenneTwister(uint[] initKey)
        {
            _mti = SIZE + 1;
            InitFromArray(initKey);
        }
        #endregion

        #region ILotusRandom methods
        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - 1].
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle()
        {
            return GenerateInt32() * TO_SINGLE_COEFF;
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle(float max)
        {
            return GenerateInt32() * TO_SINGLE_COEFF * max;
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [min - max].
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle(float min, float max)
        {
            var delta = max - min;
            return min + (GenerateInt32() * TO_SINGLE_COEFF * delta);
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - 4294967295].
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger()
        {
            return GenerateInt32();
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger(uint max)
        {
            return GenerateInt32() % max;
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [min - max].
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger(uint min, uint max)
        {
            var delta = max - min;
            return min + (GenerateInt32() % delta);
        }
        #endregion

        #region Init methods
        /// <summary>
        /// Инициализация данных генератора.
        /// </summary>
        /// <param name="seed">Начальное значение.</param>
        private void InitGenrand(uint seed)
        {
            MASSIVE[0] = seed;

            for (_mti = 1; _mti < SIZE; _mti++)
            {
                MASSIVE[_mti] = (1812433253 * (MASSIVE[_mti - 1] ^ (MASSIVE[_mti - 1] >> 30))) + _mti;
                /* See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier. */
                /* In the previous versions, MSBs of the seed affect   */
                /* only MSBs of the array mt[].                        */
                /* 2002/01/09 modified by Makoto Matsumoto             */
            }
        }

        /// <summary>
        /// Инициализация данных генератора.
        /// </summary>
        /// <param name="init_key">Массив.</param>
        private void InitFromArray(uint[] init_key)
        {
            uint i, j;
            int k;
            var key_length = init_key.Length;

            InitGenrand(19650218);
            i = 1; j = 0;
            k = SIZE > key_length ? SIZE : key_length;

            for (; k > 0; k--)
            {
                MASSIVE[i] = (MASSIVE[i] ^ ((MASSIVE[i - 1] ^ (MASSIVE[i - 1] >> 30)) * 1664525))
                    + init_key[j] + j; /* non linear */
                i++; j++;
                if (i >= SIZE) { MASSIVE[0] = MASSIVE[SIZE - 1]; i = 1; }
                if (j >= key_length) j = 0;
            }
            for (k = SIZE - 1; k > 0; k--)
            {
                MASSIVE[i] = (MASSIVE[i] ^ ((MASSIVE[i - 1] ^ (MASSIVE[i - 1] >> 30)) * 1566083941))
                    - i; /* non linear */
                i++;
                if (i >= SIZE) { MASSIVE[0] = MASSIVE[SIZE - 1]; i = 1; }
            }

            MASSIVE[0] = 0x80000000; /* MSB is 1; assuring non-zero initial array */
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Генерация псевдослучайного целого числа в интервале [0 - 4294967295].
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        public uint GenerateInt32()
        {
            uint y;

            if (_mti >= SIZE)
            {
                /* generate N words at one time */
                short kk;

                if (_mti == SIZE + 1)   /* if init_genrand() has not been called, */
                    InitGenrand(5489); /* a default initial seed is used */

                for (kk = 0; kk < SIZE - PERIOD; kk++)
                {
                    y = ((MASSIVE[kk] & UPPER_MASK) | (MASSIVE[kk + 1] & LOWER_MASK)) >> 1;
                    MASSIVE[kk] = MASSIVE[kk + PERIOD] ^ MAG_01[MASSIVE[kk + 1] & 1] ^ y;
                }
                for (; kk < SIZE - 1; kk++)
                {
                    y = ((MASSIVE[kk] & UPPER_MASK) | (MASSIVE[kk + 1] & LOWER_MASK)) >> 1;
                    MASSIVE[kk] = MASSIVE[kk + (PERIOD - SIZE)] ^ MAG_01[MASSIVE[kk + 1] & 1] ^ y;
                }
                y = ((MASSIVE[SIZE - 1] & UPPER_MASK) | (MASSIVE[0] & LOWER_MASK)) >> 1;
                MASSIVE[SIZE - 1] = MASSIVE[PERIOD - 1] ^ MAG_01[MASSIVE[0] & 1] ^ y;

                _mti = 0;
            }

            y = MASSIVE[_mti++];

            /* Tempering */
            y ^= y >> 11;
            y ^= (y << 7) & 0x9d2c5680;
            y ^= (y << 15) & 0xefc60000;
            y ^= y >> 18;

            return y;
        }
        #endregion
    }

    /// <summary>
    /// Генератор псевдослучайных значений основанный на сдвигах.
    /// </summary>
    /// <remarks>
    /// Implementation of the Ranq1 struct found in Numerical Recipes in C: 3rd Edition. 
    /// Combined generator (Ranq1 = D1(A1(right-shift first))with a period of 1.8 x 10^19.
    /// </remarks>
    public class RandomShift : ILotusRandom
    {
        #region Const
        /// <summary>
        /// Recommended multiplier for D1 method.
        /// </summary>
        private const long A = 2685821657736338717L;

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
        /// Initialization value.
        /// </summary>
        private const long M = 4101842887655102017L;

        /// <summary>
        /// Коэффициент перевода в вещественное число.
        /// </summary>
        private const float TO_SINGLE_COEFF = (1.0f / uint.MaxValue);
        #endregion

        #region Fields
        /// <summary>
        /// Current state of the random number generation.
        /// </summary>
        private uint _value;
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
            this._value = (uint)(M ^ seed);
            this._value = NextInteger();
        }
        #endregion

        #region ILotusRandom methods
        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - 1].
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle()
        {
            return NextInteger() * TO_SINGLE_COEFF;
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle(float max)
        {
            return NextInteger() * TO_SINGLE_COEFF * max;
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [min - max].
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public float NextSingle(float min, float max)
        {
            var delta = max - min;
            return min + (NextInteger() * TO_SINGLE_COEFF * delta);
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - 4294967295].
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger()
        {
            this._value ^= this._value >> A1;
            this._value ^= this._value << A2;
            this._value ^= this._value >> A3;

            return (uint)(this._value * A);
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger(uint max)
        {
            return NextInteger() % max;
        }

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [min - max].
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        public uint NextInteger(uint min, uint max)
        {
            var delta = max - min;
            return min + (NextInteger() % delta);
        }
        #endregion
    }
    /**@}*/
}