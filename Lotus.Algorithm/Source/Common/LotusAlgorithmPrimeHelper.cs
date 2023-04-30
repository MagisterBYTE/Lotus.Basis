﻿//=====================================================================================================================
// Проект: Модуль алгоритмов
// Раздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAlgorithmPrimeHelper.cs
*		Вспомогательный класс для работы с простыми числами.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Algorithm
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup AlgorithmCommon
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Вспомогательный класс для работы с простыми числами
		/// </summary>
		/// <remarks>
		/// https://ru.wikipedia.org/wiki/простое_число
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XPrimeHelper
		{
			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Набор простых чисел
			/// </summary>
			public static readonly Int32[] Primes = new Int32[]
			{
				3,
				7,
				11,
				17,
				23,
				29,
				37,
				47,
				59,
				71,
				89,
				107,
				131,
				163,
				197,
				239,
				293,
				353,
				431,
				521,
				631,
				761,
				919,
				1103,
				1327,
				1597,
				1931,
				2333,
				2801,
				3371,
				4049,
				4861,
				5839,
				7013,
				8419,
				10103,
				12143,
				14591,
				17519,
				21023,
				25229,
				30293,
				36353,
				43627,
				52361,
				62851,
				75431,
				90523,
				108631,
				130363,
				156437,
				187751,
				225307,
				270371,
				324449,
				389357,
				467237,
				560689,
				672827,
				807403,
				968897,
				1162687,
				1395263,
				1674319,
				2009191,
				2411033,
				2893249,
				3471899,
				4166287,
				4999559,
				5999471,
				7199369
			};
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка числа на простое
			/// </summary>
			/// <param name="candidate">Проверяемое число</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsPrime(Int32 candidate)
			{
				if ((candidate & 1) != 0)
				{
					var num = (Int32)Math.Sqrt(candidate);
					for (var i = 3; i <= num; i += 2)
					{
						if (candidate % i == 0)
						{
							return false;
						}
					}
					return true;
				}
				return candidate == 2;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ближайшего простого числа
			/// </summary>
			/// <param name="min">Минимальное число</param>
			/// <returns>Ближайшее простое число</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 GetPrime(Int32 min)
			{
				for (var i = 0; i < Primes.Length; i++)
				{
					var prime = Primes[i];
					if (prime >= min)
					{
						return prime;
					}
				}
				for (var i = min | 1; i < 2147483647; i += 2)
				{
					if (IsPrime(i) && (i - 1) % 101 != 0)
					{
						return i;
					}
				}

				return min;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Увеличение простого числа
			/// </summary>
			/// <param name="old_size">Предыдущий размер</param>
			/// <returns>Большее простое число</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 ExpandPrime(Int32 old_size)
			{
				var num = 2 * old_size;
				if (num > 2146435069 && 2146435069 > old_size)
				{
					return 2146435069;
				}

				return GetPrime(num);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================