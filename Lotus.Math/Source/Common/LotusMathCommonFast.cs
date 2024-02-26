using System.Runtime.InteropServices;

#if UNITY_2017_1_OR_NEWER
using UnityEngine;
#endif

namespace Lotus.Maths
{
    /** \addtogroup MathCommon
	*@{*/
    /// <summary>
    /// Структура реализующая объединение в памяти целого типа и вещественного одинарной точности.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    struct TFloatInt
    {
        /// <summary>
        /// Вещественное значение.
        /// </summary>
        [FieldOffset(0)]
        public float Float;

        /// <summary>
        /// Целое значение.
        /// </summary>
        [FieldOffset(0)]
        public int Int;
    }

    /// <summary>
    /// Статический класс реализующий математические методы основанные на кэшированных данных.
    /// </summary>
    public static class XMathFast
    {
        #region Const
        /// <summary>
        /// Маска для табличных значений синуса и косинуса.
        /// </summary>
        private const int SinCosIndexMask = ~(-1 << 12);

        /// <summary>
        /// Размер таблиц синуса и косинуса.
        /// </summary>
        private const int SinCosCacheSize = SinCosIndexMask + 1;

        /// <summary>
        /// Табличные значения синуса.
        /// </summary>
        private static float[] _sinTableCache = new float[] { 0 };

        /// <summary>
        /// Табличные значения косинуса.
        /// </summary>
        private static float[] _cosTableCache = new float[] { 0 };

        /// <summary>
        /// Точность заполнения таблиц синуса и косинуса.
        /// </summary>
        private static float _sinCosIndexFactor = SinCosCacheSize / XMath.PI_2_F;

        /// <summary>
        /// Размер таблицы для арктангенса.
        /// </summary>
        private const int Atan2Size = 1024;

        /// <summary>
        /// Размер таблицы для арктангенса.
        /// </summary>
        private const int Atan2NegSize = -Atan2Size;

        /// <summary>
        /// Служебная таблица для значений арктангенса.
        /// </summary>
        private static readonly float[] _atan2CachePPY = new float[Atan2Size + 1];

        /// <summary>
        /// Служебная таблица для значений арктангенса.
        /// </summary>
        private static readonly float[] _atan2CachePPX = new float[Atan2Size + 1];

        /// <summary>
        /// Служебная таблица для значений арктангенса.
        /// </summary>
        private static readonly float[] _atan2CachePNY = new float[Atan2Size + 1];

        /// <summary>
        /// Служебная таблица для значений арктангенса.
        /// </summary>
        private static readonly float[] _atan2CachePNX = new float[Atan2Size + 1];

        /// <summary>
        /// Служебная таблица для значений арктангенса.
        /// </summary>
        private static readonly float[] _atan2CacheNPY = new float[Atan2Size + 1];

        /// <summary>
        /// Служебная таблица для значений арктангенса.
        /// </summary>
        private static readonly float[] _atan2CacheNPX = new float[Atan2Size + 1];

        /// <summary>
        /// Служебная таблица для значений арктангенса.
        /// </summary>
        private static readonly float[] _atan2CacheNNY = new float[Atan2Size + 1];

        /// <summary>
        /// Служебная таблица для значений арктангенса.
        /// </summary>
        private static readonly float[] _atan2CacheNNX = new float[Atan2Size + 1];
        #endregion

        #region Main methods
        /// <summary>
        /// Перезапуск данных.
        /// </summary>
        public static void OnResetEditor()
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Первичная инициализация данных.
        /// </summary>
        public static void OnInit()
        {
            // Sin/Cos
            _sinTableCache = new float[SinCosCacheSize];
            _cosTableCache = new float[SinCosCacheSize];

            int i;
            for (i = 0; i < SinCosCacheSize; i++)
            {
                _sinTableCache[i] = (float)System.Math.Sin((i + 0.5f) / SinCosCacheSize * XMath.PI_2_F);
                _cosTableCache[i] = (float)System.Math.Cos((i + 0.5f) / SinCosCacheSize * XMath.PI_2_F);
            }

            var factor = SinCosCacheSize / 360f;
            for (i = 0; i < 360; i += 90)
            {
                _sinTableCache[(int)(i * factor) & SinCosIndexMask] = (float)System.Math.Sin(i * XMath.PI_F / 180f);
                _cosTableCache[(int)(i * factor) & SinCosIndexMask] = (float)System.Math.Cos(i * XMath.PI_F / 180f);
            }

            // Atan2
            var invAtan2Size = 1f / Atan2Size;
            for (i = 0; i <= Atan2Size; i++)
            {
                _atan2CachePPY[i] = (float)System.Math.Atan(i * invAtan2Size);
                _atan2CachePPX[i] = XMath.PI_2_F - _atan2CachePPY[i];
                _atan2CachePNY[i] = -_atan2CachePPY[i];
                _atan2CachePNX[i] = _atan2CachePPY[i] - XMath.PI_2_F;
                _atan2CacheNPY[i] = XMath.PI_F - _atan2CachePPY[i];
                _atan2CacheNPX[i] = _atan2CachePPY[i] + XMath.PI_2_F;
                _atan2CacheNNY[i] = _atan2CachePPY[i] - XMath.PI_F;
                _atan2CacheNNX[i] = -XMath.PI_2_F - _atan2CachePPY[i];
            }
        }

        /// <summary>
        /// Вычисление обратного квадратного корня.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Значение обратного квадратного корня.</returns>
        public static float InvSqrt(float value)
        {
            var wrapper = new TFloatInt();
            wrapper.Float = value;
            wrapper.Int = 0x5f3759df - (wrapper.Int >> 1);
            return wrapper.Float;
        }

        /// <summary>
        /// Вычисление квадратного корня.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Значение квадратного корня.</returns>
        public static float Sqrt(float value)
        {
            var wrapper = new TFloatInt();
            wrapper.Float = value;
            wrapper.Int = (1 << 29) + (wrapper.Int >> 1) - (1 << 22);
            return wrapper.Float;
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Быстрая нормализация вектора с точностью 0.001.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Нормализованный вектор.</returns>
		public static Vector2 NormalizedFast(ref Vector2 vector)
		{
			var wrapper = new TFloatInt();
			wrapper.Float = (vector.x * vector.x) + (vector.y * vector.y);
			wrapper.Int = 0x5f3759df - (wrapper.Int >> 1);
			vector.x *= wrapper.Float;
			vector.y *= wrapper.Float;
			return vector;
		}

		/// <summary>
		/// Быстрая нормализация вектора с точностью 0.001.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Нормализованный вектор.</returns>
		public static Vector3 NormalizedFast(ref Vector3 vector)
		{
			var wrapper = new TFloatInt();
			wrapper.Float = (vector.x * vector.x) + (vector.y * vector.y) + (vector.z * vector.z);
			wrapper.Int = 0x5f3759df - (wrapper.Int >> 1);
			vector.x *= wrapper.Float;
			vector.y *= wrapper.Float;
			vector.z *= wrapper.Float;
			return vector;
		}
#endif

        /// <summary>
        /// Быстрое вычисление синуса с точностью 0.0003.
        /// </summary>
        /// <param name="radians">Угол в радианах.</param>
        /// <returns>Значение синуса.</returns>
        public static float Sin(float radians)
        {
            return _sinTableCache[(int)(radians * _sinCosIndexFactor) & SinCosIndexMask];
        }

        /// <summary>
        /// Быстрое вычисление косинуса с точностью 0.0003.
        /// </summary>
        /// <param name="radians">Угол в радианах.</param>
        /// <returns>Значение косинуса.</returns>
        public static float Cos(float radians)
        {
            return _cosTableCache[(int)(radians * _sinCosIndexFactor) & SinCosIndexMask];
        }

        /// <summary>
        /// Быстрое вычисление арктангенса с точностью 0.02.
        /// </summary>
        /// <remarks>
        /// Возвращает значение угла(в радианах) в декартовой системе координат, сформированное осью X и вектором, начинающимся 
        /// в начале координат (0,0) и заканчивающимся в точке (x, y).
        /// </remarks>
        /// <param name="y">Катет.</param>
        /// <param name="x">Катет.</param>
        /// <returns>Значение арктангенса.</returns>
        public static float Atan2(float y, float x)
        {
            if (x >= 0)
            {
                if (y >= 0)
                {
                    if (x >= y)
                    {
                        return _atan2CachePPY[(int)((Atan2Size * y / x) + 0.5f)];
                    }
                    else
                    {
                        return _atan2CachePPX[(int)((Atan2Size * x / y) + 0.5f)];
                    }
                }
                else
                {
                    if (x >= -y)
                    {
                        return _atan2CachePNY[(int)((Atan2NegSize * y / x) + 0.5f)];
                    }
                    else
                    {
                        return _atan2CachePNX[(int)((Atan2NegSize * x / y) + 0.5f)];
                    }
                }
            }
            else
            {
                if (y >= 0)
                {
                    if (-x >= y)
                    {
                        return _atan2CacheNPY[(int)((Atan2NegSize * y / x) + 0.5f)];
                    }
                    else
                    {
                        return _atan2CacheNPX[(int)((Atan2NegSize * x / y) + 0.5f)];
                    }
                }
                else
                {
                    if (x <= y)
                    {
                        return _atan2CacheNNY[(int)((Atan2Size * y / x) + 0.5f)];
                    }
                    else
                    {
                        return _atan2CacheNNX[(int)((Atan2Size * x / y) + 0.5f)];
                    }
                }
            }
        }

        /// <summary>
        /// Быстрое вычисление арктангенса с точностью 0.02.
        /// </summary>
        /// <remarks>
        /// Возвращает значение угла в декартовой системе координат, сформированное осью X и вектором, начинающимся 
        /// в начале координат (0,0) и заканчивающимся в точке (x, y).
        /// </remarks>
        /// <param name="y">Катет.</param>
        /// <param name="x">Катет.</param>
        /// <returns>Значение арктангенса в градусах.</returns>
        public static float Atan2Angle(float y, float x)
        {
            var radian = Atan2(y, x);
            var angle = XMath.RadianToDegree_F * radian;
            return XMathAngle.NormalizationFull(angle);
        }
        #endregion
    }
    /**@}*/
}