//=====================================================================================================================
// Проект: Модуль алгоритмов
// Раздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAlgorithmKnapsack.cs
*		Алгоритмы для решения задач о рюкзаке. См. https://ru.wikipedia.org/wiki/Задача_о_рюкзаке
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
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
		/// Статический класс для решения задач о рюкзаке
		/// </summary>
		/// <remarks>
		/// https://ru.wikipedia.org/wiki/Задача_о_рюкзаке
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XAlgorithmKnapsack
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Решение задачи
			/// </summary>
			/// <typeparam name="TElement">Тип элемента</typeparam>
			/// <param name="set">Набор</param>
			/// <param name="capacity">Максимальный вес</param>
			/// <param name="knapsack">Заполненный словарь</param>
			/// <returns>
			/// Заполненный рюкзак, где значениями являются количество элементов типа key.
			/// Имеет тенденцию к перегрузке ранца: заполняет остаток одним наименьшим элементом.
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public static Dictionary<TElement, Int32> Knapsack<TElement>(Dictionary<TElement, Single> set, Single capacity,
				Dictionary<TElement, Int32>? knapsack = null) where TElement : notnull
			{
				var keys = new List<TElement>(set.Keys);
				// Sort keys by their weights in descending order
				keys.Sort((a, b) => -set[a].CompareTo(set[b]));

				if (knapsack == null)
				{
					knapsack = new Dictionary<TElement, Int32>();
					foreach (var key in keys)
					{
						knapsack[key] = 0;
					}
				}
				return Knapsack(set, keys, capacity, knapsack, 0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Решение задачи
			/// </summary>
			/// <typeparam name="TElement">Тип элемента</typeparam>
			/// <param name="set">Набор</param>
			/// <param name="keys">Список ключей</param>
			/// <param name="remainder">Остаток</param>
			/// <param name="knapsack">Заполненный словарь</param>
			/// <param name="start_index">Начальный индекс</param>
			/// <returns>Заполненный словарь</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Dictionary<TElement, Int32> Knapsack<TElement>(Dictionary<TElement, Single> set, List<TElement> keys, Single remainder,
				Dictionary<TElement, Int32> knapsack, Int32 start_index) where TElement : notnull
			{
				TElement smallest_key = keys[keys.Count - 1];
				if (remainder < set[smallest_key])
				{
					knapsack[smallest_key] = 1;
					return knapsack;
				}
				// Cycle through items and try to put them in knapsack
				for (var i = start_index; i < keys.Count; i++)
				{
					TElement key = keys[i];
					var weight = set[key];
					// Larger items won't fit, smaller items will fill as much space as they can
					knapsack[key] += (Int32)(remainder / weight);
					remainder %= weight;
				}
				if (remainder > 0)
				{
					// Throw out largest item and try again
					for (var i = 0; i < keys.Count; i++)
					{
						TElement key = keys[i];
						if (knapsack[key] != 0)
						{
							// Already tried every combination, return as is
							if (key.Equals(smallest_key))
							{
								return knapsack;
							}
							knapsack[key]--;
							remainder += set[key];
							start_index = i + 1;
							break;
						}
					}
					knapsack = Knapsack(set, keys, remainder, knapsack, start_index);
				}
				return knapsack;
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================