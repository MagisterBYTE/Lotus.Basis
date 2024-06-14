using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotus.Core
{
    /** \addtogroup CoreExtension
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений для работы с коллекциями.
    /// </summary>
    public static class XCollectionsExtension
    {
        #region IEnumerable 
        /// <summary>
        /// Получение случайного элемента в коллекции.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента коллекции.</typeparam>
        /// <param name="this">Коллекция.</param>
        /// <returns>Элемент.</returns>
        public static TItem? RandomElement<TItem>(this IEnumerable<TItem> @this)
        {
            if (@this == null) return default(TItem);

            var count = @this.Count();

            if (count == 0)
            {
                return default(TItem);
            }

#if UNITY_2017_1_OR_NEWER
			return @this.ElementAt(UnityEngine.Random.Range(0, count));
#else
            var rand = new Random(System.Environment.TickCount);
            return @this.ElementAt(rand.Next(0, count));
#endif
        }

        /// <summary>
        /// Выполнить действие над каждым элементом коллекции.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента коллекции.</typeparam>
        /// <param name="this">Коллекция.</param>
        /// <param name="onActionItem">Обработчик действия над каждым элементов коллекции.</param>
        /// <returns>Коллекция.</returns>
        public static IEnumerable<TItem> ForEach<TItem>(this IEnumerable<TItem> @this, Action<TItem> onActionItem)
        {
            foreach (var item in @this)
            {
                onActionItem.Invoke(item);
            }

            return @this;
        }

        /// <summary>
        /// Выполнить действие над каждым элементом коллекции.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента коллекции.</typeparam>
        /// <param name="this">Коллекция.</param>
        /// <param name="onActionItem">Обработчик действия над каждым элементов коллекции.</param>
        /// <returns>Коллекция.</returns>
        public static IEnumerable<TItem> ForEach<TItem>(this IEnumerable<TItem> @this, Action<TItem, int> onActionItem)
        {
            var index = 0;
            foreach (var item in @this)
            {
                onActionItem.Invoke(item, index);
                index++;
            }

            return @this;
        }

        /// <summary>
        /// Проверка на наличие хотя бы одного элемента коллекции указанного типа.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента коллекции.</typeparam>
        /// <param name="this">Коллекция.</param>
        /// <returns>Статус наличия элемента.</returns>
        public static bool AnyIs<TItem>(this IEnumerable<object> @this) where TItem : class
        {
            return @this.Any(x => x is TItem);
        }

        /// <summary>
        /// Печать элементов коллекции.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента коллекции.</typeparam>
        /// <param name="this">Коллекция.</param>
        /// <param name="onOutputItem">Обработчик вывода (печати) каждого элемента коллекции.</param>
        public static void Print<TItem>(this IEnumerable<TItem> @this, Action<string> onOutputItem)
        {
            foreach (var item in @this)
            {
                var print = item?.ToString() ?? "null";
                onOutputItem(print);
            }
        }

        /// <summary>
        /// Перечисление только не нулевых элеметов коллекции.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента коллекции.</typeparam>
        /// <param name="this">Коллекция.</param>
        /// <returns>Элемент коллекции.</returns>
        public static IEnumerable<TItem> WhereNotNullRef<TItem>(this IEnumerable<TItem> @this)
            where TItem : class
        {
            foreach (var item in @this)
            {
                if (item != null)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Перечисление только не нулевых элеметов коллекции.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента коллекции.</typeparam>
        /// <param name="this">Коллекция.</param>
        /// <returns>Элемент коллекции.</returns>
        public static IEnumerable<TItem> WhereNotNullValue<TItem>(this IEnumerable<TItem?> @this)
            where TItem : struct
        {
            foreach (var item in @this)
            {
                if (item.HasValue)
                {
                    yield return item.Value;
                }
            }
        }
        #endregion

        #region ICollection 
        /// <summary>
        /// Добавление элемента к коллекцию только в случае его отсутствия.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента коллекции.</typeparam>
        /// <param name="this">Коллекция.</param>
        /// <param name="element">Элемент.</param>
        /// <returns>Статус успешности добавления.</returns>
        public static bool AddIfNotContains<TItem>(this ICollection<TItem> @this, TItem element)
        {
            if (!@this.Contains(element))
            {
                @this.Add(element);
                return true;
            }

            return false;
        }
        #endregion

        #region ICollection<String> 
        /// <summary>
        /// Преобразование списка строк в одну строку текста с указанным разделителем.
        /// </summary>
        /// <param name="this">Список строк.</param>
        /// <param name="separator">Разделитель.</param>
        /// <param name="useSpace">Использовать ли дополнительный пробел между элементами.</param>
        /// <returns>Строка.</returns>
        public static string ToTextString(this ICollection<string> @this, char separator, bool useSpace)
        {
            return ToTextString(@this, string.Empty, separator, useSpace);
        }

        /// <summary>
        /// Преобразование списка строк в одну строку текста с указанным разделителем.
        /// </summary>
        /// <param name="this">Список строк.</param>
        /// <param name="defaultText">Текст если список пустой.</param>
        /// <param name="separator">Разделитель.</param>
        /// <param name="useSpace">Использовать ли дополнительный пробел между элементами.</param>
        /// <returns>Строка.</returns>
        public static string ToTextString(this ICollection<string> @this, string defaultText, char separator, bool useSpace)
        {
            if (@this == null || @this.Count == 0)
            {
                return defaultText;
            }

            if (@this.Count == 1)
            {
                return @this.First();
            }
            else
            {
                var builder = new StringBuilder(@this.Count * 10);

                var i = 0;
                foreach (var item in @this)
                {
                    builder.Append(item);

                    if (i < @this.Count - 1)
                    {
                        builder.Append(separator);
                        if (useSpace)
                        {
                            builder.Append(XCharHelper.Space);
                        }
                    }

                    i++;
                }

                return builder.ToString();
            }
        }
        #endregion

        #region IList 
        /// <summary>
        /// Установка элемента списка по индексу с автоматическим увеличением размера при необходимости.
        /// </summary>
        /// <param name="this">Список.</param>
        /// <param name="index">Индекс элемента списка.</param>
        /// <param name="element">Элемент списка.</param>
        public static void SetAt(this IList @this, int index, object element)
        {
            if (index >= @this.Count)
            {
                var delta = index - @this.Count + 1;
                for (var i = 0; i < delta; i++)
                {
                    @this.Add(element);
                }

                @this[index] = element;
            }
            else
            {
                @this[index] = element;
            }
        }

        /// <summary>
        /// Получение элемента списка по индексу.
        /// </summary>
        /// <remarks>
        /// В случае если индекс выходит за границы списка, то возвращается последний элемент.
        /// </remarks>
        /// <param name="this">Список.</param>
        /// <param name="index">Индекс элемента списка.</param>
        /// <returns>Элемент.</returns>
        public static object? GetAt(this IList @this, int index)
        {
            if (index >= @this.Count)
            {
                if (@this.Count == 0)
                {
                    // Создаем объект по умолчанию
                    @this.Add(new object());
                    return @this[0];
                }
                else
                {
                    return @this[@this.Count - 1];
                }
            }
            else
            {
                return @this[index];
            }
        }

        /// <summary>
        /// Цикличный доступ к индексатору на получение элемента позволяющий выходить за пределы индекса.
        /// </summary>
        /// <param name="this">Список.</param>
        /// <param name="index">Индекс элемента списка.</param>
        /// <returns>Элемент.</returns>
        public static object? GetLoopedObject(this IList @this, int index)
        {
            while (index < 0)
            {
                index += @this.Count;
            }
            if (index >= @this.Count)
            {
                index %= @this.Count;
            }
            return @this[index];
        }

        /// <summary>
        /// Цикличный доступ к индексатору на установку элемента позволяющий выходить за пределы индекса.
        /// </summary>
        /// <param name="this">Список.</param>
        /// <param name="index">Индекс элемента списка.</param>
        /// <param name="value">Элемент.</param>
        public static void SetLoopedObject(this IList @this, int index, object value)
        {
            while (index < 0)
            {
                index += @this.Count;
            }
            if (index >= @this.Count)
            {
                index %= @this.Count;
            }
            @this[index] = value;
        }

        /// <summary>
        /// Перемещение элемента списка вниз.
        /// </summary>
        /// <param name="this">Список.</param>
        /// <param name="elementIndex">Индекс перемещаемого элемента.</param>
        public static void MoveObjectDown(this IList @this, int elementIndex)
        {
            var next = (elementIndex + 1) % @this.Count;
            SwapObject(@this, elementIndex, next);
        }

        /// <summary>
        /// Перемещение элемента списка вверх.
        /// </summary>
        /// <param name="this">Список.</param>
        /// <param name="elementIndex">Индекс перемещаемого элемента.</param>
        public static void MoveObjectUp(this IList @this, int elementIndex)
        {
            var previous = elementIndex - 1;
            if (previous < 0) previous = @this.Count - 1;
            SwapObject(@this, elementIndex, previous);
        }

        /// <summary>
        /// Обмен местами элементов списка.
        /// </summary>
        /// <param name="this">Список.</param>
        /// <param name="oldIndex">Старая позиция.</param>
        /// <param name="newIndex">Новая позиция.</param>
        /// <returns>Список.</returns>
        public static IList SwapObject(this IList @this, int oldIndex, int newIndex)
        {
            var temp = @this[oldIndex];
            @this[oldIndex] = @this[newIndex];
            @this[newIndex] = temp;
            return @this;
        }

        /// <summary>
        /// Сортировка элементов списка.
        /// </summary>
        /// <param name="this">Список</param>
        /// <param name="comparer">Компаратор для сравнивания элементов списка.</param>
        public static void Sort(this IList @this, IComparer comparer)
        {
            ArrayList.Adapter(@this).Sort(comparer);
        }
        #endregion

        #region IList<Type> 
        /// <summary>
        /// Цикличный доступ к индексатору на получение элемента позволяющий выходить за пределы индекса.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="index">Индекс элемента списка.</param>
        /// <returns>Элемент.</returns>
        public static TItem GetLooped<TItem>(this IList<TItem> @this, int index)
        {
            while (index < 0)
            {
                index += @this.Count;
            }
            if (index >= @this.Count)
            {
                index %= @this.Count;
            }
            return @this[index];
        }

        /// <summary>
        /// Цикличный доступ к индексатору на установку элемента позволяющий выходить за пределы индекса.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="index">Индекс элемента списка.</param>
        /// <param name="value">Элемент.</param>
        public static void SetLooped<TItem>(this IList<TItem> @this, int index, in TItem value)
        {
            while (index < 0)
            {
                index += @this.Count;
            }
            if (index >= @this.Count)
            {
                index %= @this.Count;
            }
            @this[index] = value;
        }

        /// <summary>
        /// Перемещение элемента списка вниз.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="elementIndex">Индекс перемещаемого элемента.</param>
        public static void MoveElementDown<TItem>(this IList<TItem> @this, int elementIndex)
        {
            var next = (elementIndex + 1) % @this.Count;
            Swap(@this, elementIndex, next);
        }

        /// <summary>
        /// Перемещение элемента списка вверх.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="elementIndex">Индекс перемещаемого элемента.</param>
        public static void MoveElementUp<TItem>(this IList<TItem> @this, int elementIndex)
        {
            var previous = elementIndex - 1;
            if (previous < 0) previous = @this.Count - 1;
            Swap(@this, elementIndex, previous);
        }

        /// <summary>
        /// Поиск элемента в списке.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="element">Искомый элемент.</param>
        /// <returns>Индекс найденного элемента или -1.</returns>
        public static int IndexOf<TItem>(this IList<TItem> @this, in TItem element)
        {
            if (typeof(TItem).IsValueType)
            {
                for (var i = 0; i < @this.Count; i++)
                {
                    if (@this[i]!.Equals(element))
                    {
                        return i;
                    }
                }
                return -1;
            }
            else
            {
                for (var i = 0; i < @this.Count; i++)
                {
                    if (XObjectHelper.ObjectEquals(@this[i], element))
                    {
                        return i;
                    }
                }

                return -1;
            }
        }

        /// <summary>
        /// Проверка на уникальность элементов списка.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <returns>Статус уникальности.</returns>
        public static bool IsUnique<TItem>(this IList<TItem> @this)
        {
            if (typeof(TItem).IsValueType)
            {
                for (var i = 0; i < @this.Count - 1; i++)
                {
                    for (var j = i + 1; j < @this.Count; j++)
                    {
                        if (@this[i]!.Equals(@this[j]))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (var i = 0; i < @this.Count - 1; i++)
                {
                    for (var j = i + 1; j < @this.Count; j++)
                    {
                        if (XObjectHelper.ObjectEquals(@this[i], @this[j]))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Поиск элемента в списке.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="element">Искомый элемент.</param>
        /// <returns>Индекс найденного элемента или 0.</returns>
        public static int IndexOfOrDefault<TItem>(this IList<TItem> @this, in TItem element)
        {
            if (typeof(TItem).IsValueType)
            {
                for (var i = 0; i < @this.Count; i++)
                {
                    if (@this[i]!.Equals(element))
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (var i = 0; i < @this.Count; i++)
                {
                    if (XObjectHelper.ObjectEquals(@this[i], element))
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Проверка на равенство элементов списка.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="other">Список.</param>
        /// <returns>Статус равенства элементов списка.</returns>
        public static bool EqualElements<TItem>(this IList<TItem> @this, IList<TItem> other)
        {
            if (@this.Count != other.Count)
            {
                return false;
            }

            if (typeof(TItem).IsValueType)
            {
                for (var i = 0; i < @this.Count; i++)
                {
                    if (!@this[i]!.Equals(other[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (var i = 0; i < @this.Count; i++)
                {
                    if (XObjectHelper.ObjectEquals(@this[i], other[i]) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Получение индекса последнего элемента списка.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <returns>Индекс последнего элемента списка.</returns>
        public static int LastIndex<TItem>(this IList<TItem> @this)
        {
            return @this.Count - 1;
        }

        /// <summary>
        /// Перетасовка элементов списка.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        public static void Shuffle<TItem>(this IList<TItem> @this)
        {
            var rand = new Random();
            var n = @this.Count;
            while (n > 1)
            {
                n--;
                var k = rand.Next(n + 1);
                @this.Swap(n, k);
            }
        }

        /// <summary>
        /// Обмен местами элементов списка.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="oldIndex">Старая позиция.</param>
        /// <param name="newIndex">Новая позиция.</param>
        /// <returns>Список.</returns>
        public static IList<TItem> Swap<TItem>(this IList<TItem> @this, int oldIndex, int newIndex)
        {
            var temp = @this[oldIndex];
            @this[oldIndex] = @this[newIndex];
            @this[newIndex] = temp;
            return @this;
        }

        /// <summary>
        /// Циклическое смещение элементов списка.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="forward">Статус смещение вперед.</param>
        /// <returns>Список.</returns>
        public static IList<TItem> Shift<TItem>(this IList<TItem> @this, bool forward)
        {
            var length = @this.Count;
            int start;
            int sign;
            var i = 0;
            Func<bool> condition;
            if (forward)
            {
                start = 0;
                sign = +1;
                condition = () => i < length;
            }
            else
            {
                start = length - 1;
                sign = -1;
                condition = () => i >= 0;
            }

            var element_to_move = @this[start];
            for (i = start; condition(); i += sign)
            {
                // - get the next element's atIndex
                int next_index;
                if (forward)
                {
                    next_index = (i + 1) % length;
                }
                else
                {
                    next_index = i - 1;
                    if (next_index < 0) next_index = length - 1;
                }
                // - save next element in a temp variable
                var next_element = @this[next_index];
                // - copy the current element over the next
                @this[next_index] = element_to_move;
                // - update element to move, to the next element
                element_to_move = next_element;
            }
            return @this;
        }
        #endregion

        #region IList<Single> 
        /// <summary>
        /// Получить индекс элемента список значение которого наболее близко указанному аргументу.
        /// </summary>
        /// <param name="this">Список.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Индекс.</returns>
        public static int GetNearestIndex(this IList<float> @this, float value)
        {
            if (value <= @this[0])
            {
                return 0;
            }
            if (value >= @this[@this.Count - 1])
            {
                return @this.Count - 1;
            }

            for (var i = 0; i < @this.Count; i++)
            {
                if (value < @this[i])
                {
                    var prev_delta = Math.Abs(value - @this[i - 1]);
                    var curr_delta = Math.Abs(@this[i] - value);
                    if (prev_delta < curr_delta)
                    {
                        return i - 1;
                    }
                    else
                    {
                        return i;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Получить значение элемента списка значение которого наболее близко указанному аргументу.
        /// </summary>
        /// <param name="this">Список.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Значение.</returns>
        public static float GetNearestValue(this IList<float> @this, float value)
        {
            return @this[@this.GetNearestIndex(value)];
        }

        /// <summary>
        /// Конвертация списка значений вещественных типов в массив вещественных значений двойной точности.
        /// </summary>
        /// <param name="this">Список вещественных значений одинарной точности.</param>
        /// <returns>Массив вещественных значений двойной точности.</returns>
        public static double[] ToDoubleArray(this IList<float> @this)
        {
            var massive = new double[@this.Count];
            for (var i = 0; i < @this.Count; i++)
            {
                massive[i] = @this[i];
            }

            return massive;
        }
        #endregion

        #region IList<Double> 
        /// <summary>
        /// Получить индекс элемента список значение которого наболее близко указанному аргументу.
        /// </summary>
        /// <param name="this">Список.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Индекс.</returns>
        public static int GetNearestIndex(this IList<double> @this, double value)
        {
            if (value <= @this[0])
            {
                return 0;
            }
            if (value >= @this[@this.Count - 1])
            {
                return @this.Count - 1;
            }

            for (var i = 0; i < @this.Count; i++)
            {
                if (value < @this[i])
                {
                    var prev_delta = Math.Abs(value - @this[i - 1]);
                    var curr_delta = Math.Abs(@this[i] - value);
                    if (prev_delta < curr_delta)
                    {
                        return i - 1;
                    }
                    else
                    {
                        return i;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Получить значение элемента списка значение которого наболее близко указанному аргументу.
        /// </summary>
        /// <param name="this">Список.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Значение.</returns>
        public static double GetNearestValue(this IList<double> @this, double value)
        {
            return @this[@this.GetNearestIndex(value)];
        }
        #endregion

        #region IList<String> 
        /// <summary>
        /// Конвертация списка значений строкового типа в массив целочисленных значений.
        /// </summary>
        /// <param name="this">Список значений строкового типа.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Массив целочисленных значений.</returns>
        public static int[] ToIntArray(this IList<string> @this, int defaultValue = 0)
        {
            var massive = new int[@this.Count];
            for (var i = 0; i < @this.Count; i++)
            {
                massive[i] = XNumberHelper.ParseInt(@this[i], defaultValue);
            }

            return massive;
        }

        /// <summary>
        /// Конвертация списка значений строкового типа в массив вещественных значений одинарной точности.
        /// </summary>
        /// <param name="this">Список значений строкового типа.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Массив вещественных значений одинарной точности.</returns>
        public static float[] ToFloatArray(this IList<string> @this, float defaultValue = 0)
        {
            var massive = new float[@this.Count];
            for (var i = 0; i < @this.Count; i++)
            {
                massive[i] = XNumberHelper.ParseSingle(@this[i], defaultValue);
            }

            return massive;
        }

        /// <summary>
        /// Конвертация списка значений строкового типа в массив вещественных значений двойной точности.
        /// </summary>
        /// <param name="this">Список значений строкового типа.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Массив вещественных значений двойной точности.</returns>
        public static double[] ToDoubleArray(this IList<string> @this, double defaultValue = 0)
        {
            var massive = new double[@this.Count];
            for (var i = 0; i < @this.Count; i++)
            {
                massive[i] = XNumberHelper.ParseDouble(@this[i], defaultValue);
            }

            return massive;
        }

        /// <summary>
        /// Конвертация списка значений строкового типа в массив значений Guid.
        /// </summary>
        /// <param name="this">Список значений строкового типа.</param>
        /// <returns>Массив значений Guid.</returns>
        public static Guid[] ToGuidArray(this IList<string> @this)
        {
            var massive = new Guid[@this.Count];
            for (var i = 0; i < @this.Count; i++)
            {
                massive[i] = Guid.Parse(@this[i]);
            }

            return massive;
        }
        #endregion

        #region Array 
        /// <summary>
        /// Установка элемента массива по индексу с автоматическим увеличением размера при необходимости.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента массива.</typeparam>
        /// <param name="this">Массив.</param>
        /// <param name="index">Индекс элемента массива.</param>
        /// <param name="element">Элемент массива.</param>
        public static void SetAt<TItem>(this TItem[] @this, int index, TItem element)
        {
            if (index >= @this.Length)
            {
                Array.Resize(ref @this, index + 1);
                @this[index] = element;
            }
            else
            {
                @this[index] = element;
            }
        }

        /// <summary>
        /// Получение элемента массива по индексу.
        /// </summary>
        /// <remarks>
        /// В случае если индекс выходит за границы массива, то возвращается последний элемент.
        /// </remarks>
        /// <typeparam name="TItem">Тип элемента массива.</typeparam>
        /// <param name="this">Массив.</param>
        /// <param name="index">Индекс элемента массива.</param>
        /// <returns>Элемент.</returns>
        public static TItem GetAt<TItem>(this TItem[] @this, int index)
        {
            if (index >= @this.Length)
            {
                if (@this.Length == 0)
                {
                    // Создаем объект по умолчанию
                    Array.Resize(ref @this, 1);
                    return @this[0];
                }
                else
                {
                    return @this[@this.Length - 1];
                }
            }
            else
            {
                return @this[index];
            }
        }

        /// <summary>
        /// Проверка на нахождение элемента в массиве.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента массива.</typeparam>
        /// <param name="this">Массив.</param>
        /// <param name="element">Элемент.</param>
        /// <returns>Статус нахождения.</returns>
        public static bool ContainsElement<TItem>(this TItem[] @this, TItem element)
        {
            return Array.IndexOf(@this, element) >= 0;
        }

        /// <summary>
        /// Получение следующего элемента в массиве.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента массива.</typeparam>
        /// <param name="this">Массив.</param>
        /// <param name="currentElement">Текущий элемент.</param>
        /// <returns>Элемент.</returns>
        public static TItem NextElement<TItem>(this TItem[] @this, TItem currentElement)
        {
            if (@this == null || @this.Length == 0)
            {
                return currentElement;
            }

            if (currentElement == null)
            {
                return @this[0];
            }

            var index = Array.IndexOf(@this, currentElement);

            index++;

            if (index >= @this.Length)
            {
                index = 0;
            }

            return @this[index];
        }

        /// <summary>
        /// Получение предыдущего элемента в массиве.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента массива.</typeparam>
        /// <param name="this">Массив.</param>
        /// <param name="currentElement">Текущий элемент.</param>
        /// <returns>Элемент.</returns>
        public static TItem BackElement<TItem>(this TItem[] @this, TItem currentElement)
        {
            if (@this == null || @this.Length == 0)
            {
                return currentElement;
            }

            if (currentElement == null)
            {
                return @this[0];
            }

            var index = Array.IndexOf(@this, currentElement);

            index--;

            if (index < 0)
            {
                index = @this.Length - 1;
            }

            return @this[index];
        }
        #endregion

        #region List 
        /// <summary>
        /// Установка элемента списка по индексу с автоматическим увеличением размера при необходимости.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="index">Индекс элемента списка.</param>
        /// <param name="element">Элемент списка.</param>
        public static void SetAt<TItem>(this List<TItem> @this, int index, in TItem? element)
        {
            if (index >= @this.Count)
            {
                var delta = index - @this.Count + 1;
                for (var i = 0; i < delta; i++)
                {
                    @this.Add(default(TItem)!);
                }

                @this[index] = element!;
            }
            else
            {
                @this[index] = element!;
            }
        }

        /// <summary>
        /// Получение элемента списка по индексу.
        /// </summary>
        /// <remarks>
        /// В случае если индекс выходит за границы списка, то возвращается последний элемент.
        /// </remarks>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="index">Индекс элемента списка.</param>
        /// <returns>Элемент.</returns>
        public static TItem? GetAt<TItem>(this List<TItem> @this, int index)
        {
            if (index >= @this.Count)
            {
                if (@this.Count == 0)
                {
                    // Создаем объект по умолчанию
                    @this.Add(default(TItem)!);
                    return @this[0];
                }
                else
                {
                    return @this[@this.Count - 1];
                }
            }
            else
            {
                return @this[index];
            }
        }

        /// <summary>
        /// Сортировка списка по убыванию.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <returns>Список.</returns>
        public static List<TItem>? SortDescending<TItem>(this List<TItem> @this)
        {
            @this.Sort();
            @this.Reverse();
            return @this;
        }

        /// <summary>
        /// Обрезать список сначала до указанного элемента.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="this">Список.</param>
        /// <param name="item">Элемент.</param>
        /// <param name="included">Включать указанный элемент в удаление.</param>
        /// <returns>Количество удаленных элементов.</returns>
        public static int TrimStart<TItem>(this List<TItem> @this, in TItem item, bool included = true) where TItem : IComparable<TItem>
        {
            var comprare_first = item.CompareTo(@this[0]);
            var comprare_last = item.CompareTo(@this[@this.Count - 1]);

            // Элемент находиться за пределами списка
            if (comprare_first <= 0)
            {
                return 0;
            }
            else
            {
                // Удаляем все элементы
                if (comprare_last > 0)
                {
                    var count = @this.Count;
                    @this.Clear();
                    return count;
                }
                else
                {
                    // Удаляем либо до последнего элемента, либо все элементы
                    if (comprare_last == 0)
                    {
                        if (included)
                        {
                            var count = @this.Count;
                            @this.Clear();
                            return count;
                        }
                        else
                        {
                            var count = @this.Count - 1;
                            @this.RemoveRange(0, count);
                            return count;
                        }
                    }
                    else
                    {
                        var max_count = @this.Count - 1;
                        for (var i = 1; i < max_count; i++)
                        {
                            if (@this[i].CompareTo(item) == 0)
                            {
                                if (included)
                                {
                                    @this.RemoveRange(0, i + 1);
                                    return i + 1;
                                }
                                else
                                {
                                    @this.RemoveRange(0, i);
                                    return i;
                                }
                            }
                            else
                            {
                                if (@this[i].CompareTo(item) > 0)
                                {
                                    @this.RemoveRange(0, i - 1);
                                    return i - 1;
                                }
                            }
                        }
                    }
                }
            }

            return 0;
        }
        #endregion

        #region Dictionary 
        /// <summary>
        /// Получить значение по ключу или указанное значение по умолчанию.
        /// </summary>
        /// <typeparam name="TKey">Тип ключа.</typeparam>
        /// <typeparam name="TValue">Тип значения.</typeparam>
        /// <param name="this">Словарь.</param>
        /// <param name="key">Ключ.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение.</returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> @this, in TKey key, TValue defaultValue)
            where TValue : struct
            where TKey : notnull
        {
            TValue result;
            if (!@this.TryGetValue(key, out result))
            {
                @this[key] = result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Получить значение по ключу или созданное значение по умолчанию.
        /// </summary>
        /// <typeparam name="TKey">Тип ключа.</typeparam>
        /// <typeparam name="TValue">Тип значения.</typeparam>
        /// <param name="this">Словарь.</param>
        /// <param name="key">Ключ.</param>
        /// <returns>Значение.</returns>
        public static TValue? Get<TKey, TValue>(this Dictionary<TKey, TValue> @this, in TKey key)
            where TValue : new()
            where TKey : notnull
        {
            TValue? result;
            if (!@this.TryGetValue(key, out result))
            {
                @this[key] = result = new TValue();
            }
            return result;
        }
        #endregion
    }
    /**@}*/
}