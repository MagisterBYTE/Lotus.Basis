using System.Collections.Generic;

namespace Lotus.Algorithm
{
    /** \addtogroup AlgorithmCommon
	*@{*/
    /// <summary>
    /// Статический класс для решения задач о рюкзаке.
    /// </summary>
    /// <remarks>
    /// https://ru.wikipedia.org/wiki/Задача_о_рюкзаке
    /// </remarks>
    public static class XAlgorithmKnapsack
    {
        /// <summary>
        /// Решение задачи.
        /// </summary>
        /// <typeparam name="TElement">Тип элемента.</typeparam>
        /// <param name="set">Набор.</param>
        /// <param name="capacity">Максимальный вес.</param>
        /// <param name="knapsack">Заполненный словарь.</param>
        /// <returns>
        /// Заполненный рюкзак, где значениями являются количество элементов типа key.
        /// Имеет тенденцию к перегрузке ранца: заполняет остаток одним наименьшим элементом.
        /// </returns>
        public static Dictionary<TElement, int> Knapsack<TElement>(Dictionary<TElement, float> set, float capacity,
            Dictionary<TElement, int>? knapsack = null) where TElement : notnull
        {
            if(set.Count == 0) return new Dictionary<TElement, int> { };

            var keys = new List<TElement>(set.Keys);
            // Sort keys by their weights in descending order
            keys.Sort((a, b) => -set[a].CompareTo(set[b]));

            if (knapsack == null)
            {
                knapsack = new Dictionary<TElement, int>();
                foreach (var key in keys)
                {
                    knapsack[key] = 0;
                }
            }
            return Knapsack(set, keys, capacity, knapsack, 0);
        }

        /// <summary>
        /// Решение задачи.
        /// </summary>
        /// <typeparam name="TElement">Тип элемента.</typeparam>
        /// <param name="set">Набор.</param>
        /// <param name="keys">Список ключей.</param>
        /// <param name="remainder">Остаток.</param>
        /// <param name="knapsack">Заполненный словарь.</param>
        /// <param name="start_index">Начальный индекс.</param>
        /// <returns>Заполненный словарь.</returns>
        private static Dictionary<TElement, int> Knapsack<TElement>(Dictionary<TElement, float> set, List<TElement> keys, float remainder,
            Dictionary<TElement, int> knapsack, int start_index) where TElement : notnull
        {
            var smallest_key = keys[keys.Count - 1];
            if (remainder < set[smallest_key])
            {
                knapsack[smallest_key] = 1;
                return knapsack;
            }
            // Cycle through items and try to put them in knapsack
            for (var i = start_index; i < keys.Count; i++)
            {
                var key = keys[i];
                var weight = set[key];
                // Larger items won't fit, smaller items will fill as much space as they can
                knapsack[key] += (int)(remainder / weight);
                remainder %= weight;
            }
            if (remainder > 0)
            {
                // Throw out largest item and try again
                for (var i = 0; i < keys.Count; i++)
                {
                    var key = keys[i];
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
    /**@}*/
}