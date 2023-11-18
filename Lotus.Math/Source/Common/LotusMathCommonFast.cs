//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMathCommonFast.cs
*		Математические вычисления основанные на предварительно вычисленных(кэшированных) данных.
*		Реализация математических методов и функций основанных на кэшированных данных и предварительно вычисленных таблицах.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Runtime.InteropServices;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
#endif
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup MathCommon
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура реализующая объединение в памяти целого типа и вещественного одинарной точности
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[StructLayout(LayoutKind.Explicit)]
		struct TFloatInt
		{
			/// <summary>
			/// Вещественное значение
			/// </summary>
			[FieldOffset(0)]
			public Single Float;

			/// <summary>
			/// Целое значение
			/// </summary>
			[FieldOffset(0)]
			public Int32 Int;
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий математические методы основанные на кэшированных данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XMathFast
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Маска для табличных значений синуса и косинуса
			/// </summary>
			private const Int32 SinCosIndexMask = ~(-1 << 12);

			/// <summary>
			/// Размер таблиц синуса и косинуса
			/// </summary>
			private const Int32 SinCosCacheSize = SinCosIndexMask + 1;

			/// <summary>
			/// Табличные значения синуса
			/// </summary>
			private static Single[] _sinTableCache;

			/// <summary>
			/// Табличные значения косинуса
			/// </summary>
			private static Single[] _cosTableCache;

			/// <summary>
			/// Точность заполнения таблиц синуса и косинуса
			/// </summary>
			private static Single mSinCosIndexFactor = SinCosCacheSize / XMath.PI_2_F;

			/// <summary>
			/// Размер таблицы для арктангенса
			/// </summary>
			private const Int32 Atan2Size = 1024;

			/// <summary>
			/// Размер таблицы для арктангенса
			/// </summary>
			private const Int32 Atan2NegSize = -Atan2Size;

			/// <summary>
			/// Служебная таблица для значений арктангенса
			/// </summary>
			private static readonly Single[] mAtan2CachePPY = new Single[Atan2Size + 1];

			/// <summary>
			/// Служебная таблица для значений арктангенса
			/// </summary>
			private static readonly Single[] mAtan2CachePPX = new Single[Atan2Size + 1];

			/// <summary>
			/// Служебная таблица для значений арктангенса
			/// </summary>
			private static readonly Single[] mAtan2CachePNY = new Single[Atan2Size + 1];

			/// <summary>
			/// Служебная таблица для значений арктангенса
			/// </summary>
			private static readonly Single[] mAtan2CachePNX = new Single[Atan2Size + 1];

			/// <summary>
			/// Служебная таблица для значений арктангенса
			/// </summary>
			private static readonly Single[] mAtan2CacheNPY = new Single[Atan2Size + 1];

			/// <summary>
			/// Служебная таблица для значений арктангенса
			/// </summary>
			private static readonly Single[] mAtan2CacheNPX = new Single[Atan2Size + 1];

			/// <summary>
			/// Служебная таблица для значений арктангенса
			/// </summary>
			private static readonly Single[] mAtan2CacheNNY = new Single[Atan2Size + 1];

			/// <summary>
			/// Служебная таблица для значений арктангенса
			/// </summary>
			private static readonly Single[] mAtan2CacheNNX = new Single[Atan2Size + 1];
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перезапуск данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public static void OnResetEditor()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичная инициализация данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public static void OnInit()
			{
				// Sin/Cos
				_sinTableCache = new Single[SinCosCacheSize];
				_cosTableCache = new Single[SinCosCacheSize];

				Int32 i;
				for (i = 0; i < SinCosCacheSize; i++)
				{
					_sinTableCache[i] = (Single)System.Math.Sin((i + 0.5f) / SinCosCacheSize * XMath.PI_2_F);
					_cosTableCache[i] = (Single)System.Math.Cos((i + 0.5f) / SinCosCacheSize * XMath.PI_2_F);
				}

				var factor = SinCosCacheSize / 360f;
				for (i = 0; i < 360; i += 90)
				{
					_sinTableCache[(Int32)(i * factor) & SinCosIndexMask] = (Single)System.Math.Sin(i * XMath.PI_F / 180f);
					_cosTableCache[(Int32)(i * factor) & SinCosIndexMask] = (Single)System.Math.Cos(i * XMath.PI_F / 180f);
				}

				// Atan2
				var invAtan2Size = 1f / Atan2Size;
				for (i = 0; i <= Atan2Size; i++)
				{
					mAtan2CachePPY[i] = (Single)System.Math.Atan(i * invAtan2Size);
					mAtan2CachePPX[i] = XMath.PI_2_F - mAtan2CachePPY[i];
					mAtan2CachePNY[i] = -mAtan2CachePPY[i];
					mAtan2CachePNX[i] = mAtan2CachePPY[i] - XMath.PI_2_F;
					mAtan2CacheNPY[i] = XMath.PI_F - mAtan2CachePPY[i];
					mAtan2CacheNPX[i] = mAtan2CachePPY[i] + XMath.PI_2_F;
					mAtan2CacheNNY[i] = mAtan2CachePPY[i] - XMath.PI_F;
					mAtan2CacheNNX[i] = -XMath.PI_2_F - mAtan2CachePPY[i];
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление обратного квадратного корня
			/// </summary>
			/// <param name="value">Значение</param>
			/// <returns>Значение обратного квадратного корня</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single InvSqrt(Single value)
			{
				var wrapper = new TFloatInt();
				wrapper.Float = value;
				wrapper.Int = 0x5f3759df - (wrapper.Int >> 1);
				return wrapper.Float;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление квадратного корня
			/// </summary>
			/// <param name="value">Значение</param>
			/// <returns>Значение квадратного корня</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Sqrt(Single value)
			{
				var wrapper = new TFloatInt();
				wrapper.Float = value;
				wrapper.Int = (1 << 29) + (wrapper.Int >> 1) - (1 << 22);
				return wrapper.Float;
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Быстрая нормализация вектора с точностью 0.001
			/// </summary>
			/// <param name="vector">Вектор</param>
			/// <returns>Нормализованный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2 NormalizedFast(ref Vector2 vector)
			{
				var wrapper = new TFloatInt();
				wrapper.Float = (vector.x * vector.x) + (vector.y * vector.y);
				wrapper.Int = 0x5f3759df - (wrapper.Int >> 1);
				vector.x *= wrapper.Float;
				vector.y *= wrapper.Float;
				return vector;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Быстрая нормализация вектора с точностью 0.001
			/// </summary>
			/// <param name="vector">Вектор</param>
			/// <returns>Нормализованный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Быстрое вычисление синуса с точностью 0.0003
			/// </summary>
			/// <param name="radians">Угол в радианах</param>
			/// <returns>Значение синуса</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Sin(Single radians)
			{
				return _sinTableCache[(Int32)(radians * mSinCosIndexFactor) & SinCosIndexMask];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Быстрое вычисление косинуса с точностью 0.0003
			/// </summary>
			/// <param name="radians">Угол в радианах</param>
			/// <returns>Значение косинуса</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Cos(Single radians)
			{
				return _cosTableCache[(Int32)(radians * mSinCosIndexFactor) & SinCosIndexMask];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Быстрое вычисление арктангенса с точностью 0.02
			/// </summary>
			/// <remarks>
			/// Возвращает значение угла(в радианах) в декартовой системе координат, сформированное осью X и вектором, начинающимся 
			/// в начале координат (0,0) и заканчивающимся в точке (x, y)
			/// </remarks>
			/// <param name="y">Катет</param>
			/// <param name="x">Катет</param>
			/// <returns>Значение арктангенса</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Atan2(Single y, Single x)
			{
				if (x >= 0)
				{
					if (y >= 0)
					{
						if (x >= y)
						{
							return mAtan2CachePPY[(Int32)((Atan2Size * y / x) + 0.5f)];
						}
						else
						{
							return mAtan2CachePPX[(Int32)((Atan2Size * x / y) + 0.5f)];
						}
					}
					else
					{
						if (x >= -y)
						{
							return mAtan2CachePNY[(Int32)((Atan2NegSize * y / x) + 0.5f)];
						}
						else
						{
							return mAtan2CachePNX[(Int32)((Atan2NegSize * x / y) + 0.5f)];
						}
					}
				}
				else
				{
					if (y >= 0)
					{
						if (-x >= y)
						{
							return mAtan2CacheNPY[(Int32)((Atan2NegSize * y / x) + 0.5f)];
						}
						else
						{
							return mAtan2CacheNPX[(Int32)((Atan2NegSize * x / y) + 0.5f)];
						}
					}
					else
					{
						if (x <= y)
						{
							return mAtan2CacheNNY[(Int32)((Atan2Size * y / x) + 0.5f)];
						}
						else
						{
							return mAtan2CacheNNX[(Int32)((Atan2Size * x / y) + 0.5f)];
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Быстрое вычисление арктангенса с точностью 0.02
			/// </summary>
			/// <remarks>
			/// Возвращает значение угла в декартовой системе координат, сформированное осью X и вектором, начинающимся 
			/// в начале координат (0,0) и заканчивающимся в точке (x, y)
			/// </remarks>
			/// <param name="y">Катет</param>
			/// <param name="x">Катет</param>
			/// <returns>Значение арктангенса в градусах</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Atan2Angle(Single y, Single x)
			{
				var radian = Atan2(y, x);
				var angle = XMath.RadianToDegree_F * radian;
				return XMathAngle.NormalizationFull(angle);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================