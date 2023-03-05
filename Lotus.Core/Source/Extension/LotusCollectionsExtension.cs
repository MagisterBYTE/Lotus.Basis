//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Методы расширений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCollectionsExtension.cs
*		Методы расширения работы с коллекциями.
*		Реализация максимально обобщенных расширений направленных на работу с коллекциями и их отдельной функциональности.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreExtension
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений для работы с коллекциями
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XCollectionsExtension
		{
			#region ======================================= IEnumerable ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение случайного элемента в коллекции
			/// </summary>
			/// <typeparam name="TItem">Тип элемента коллекции</typeparam>
			/// <param name="this">Коллекция</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TItem RandomElement<TItem>(this IEnumerable<TItem> @this)
			{
				var count = @this.Count();

				if (count == 0)
				{
					return default(TItem);
				}

#if (UNITY_2017_1_OR_NEWER)
				return @this.ElementAt(UnityEngine.Random.Range(0, count));
#else
				System.Random rand = new Random(System.Environment.TickCount);
				return (@this.ElementAt(rand.Next(0, count)));
#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выполнить действие над каждым элементом коллекции
			/// </summary>
			/// <typeparam name="TItem">Тип элемента коллекции</typeparam>
			/// <param name="this">Коллекция</param>
			/// <param name="on_action_item">Обработчик действия над каждым элементов коллекции</param>
			/// <returns>Коллекция</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerable<TItem> ForEach<TItem>(this IEnumerable<TItem> @this, Action<TItem> on_action_item)
			{
				foreach (var item in @this)
				{
					on_action_item.Invoke(item);
				}

				return @this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие хотя бы одного элемента коллекции указанного типа
			/// </summary>
			/// <typeparam name="TItem">Тип элемента коллекции</typeparam>
			/// <param name="this">Коллекция</param>
			/// <returns>Статус наличия элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean AnyIs<TItem>(this IEnumerable<System.Object> @this) where TItem : class
			{
				return @this.Any(x => x is TItem);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Печать элементов коллекции
			/// </summary>
			/// <typeparam name="TItem">Тип элемента коллекции</typeparam>
			/// <param name="this">Коллекция</param>
			/// <param name="on_output_item">Обработчик вывода (печати) каждого элемента коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Print<TItem>(this IEnumerable<TItem> @this, Action<String> on_output_item)
			{
				foreach (var item in @this)
				{
					on_output_item(item.ToString());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перечисление только не нулевых элеметов коллекции
			/// </summary>
			/// <typeparam name="TItem">Тип элемента коллекции</typeparam>
			/// <param name="this">Коллекция</param>
			/// <returns>Элемент коллекции</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerable<TItem> WhereNotNullRef<TItem>(this IEnumerable<TItem> @this)
				where TItem : class
			{
				foreach (TItem item in @this)
				{
					if (item != null)
					{
						yield return item;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перечисление только не нулевых элеметов коллекции
			/// </summary>
			/// <typeparam name="TItem">Тип элемента коллекции</typeparam>
			/// <param name="this">Коллекция</param>
			/// <returns>Элемент коллекции</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerable<TItem> WhereNotNullValue<TItem>(this IEnumerable<TItem?> @this)
				where TItem : struct
			{
				foreach (TItem? item in @this)
				{
					if (item.HasValue)
					{
						yield return item.Value;
					}
				}
			}
			#endregion

			#region ======================================= ICollection ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента к коллекцию только в случае его отсутствия
			/// </summary>
			/// <typeparam name="TItem">Тип элемента коллекции</typeparam>
			/// <param name="this">Коллекция</param>
			/// <param name="element">Элемент</param>
			/// <returns>Статус успешности добавления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean AddIfNotContains<TItem>(this ICollection<TItem> @this, TItem element)
			{
				if (!@this.Contains(element))
				{
					@this.Add(element);
					return true;
				}

				return false;
			}
			#endregion

			#region ======================================= ICollection<String> =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование списка строк в одну строку текста с указанным разделителем
			/// </summary>
			/// <param name="this">Список строк</param>
			/// <param name="separator">Разделитель</param>
			/// <param name="use_space">Использовать ли дополнительный пробел между элементами</param>
			/// <returns>Строка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String ToTextString(this ICollection<String> @this, Char separator, Boolean use_space)
			{
				return (ToTextString(@this, String.Empty, separator, use_space));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование списка строк в одну строку текста с указанным разделителем
			/// </summary>
			/// <param name="this">Список строк</param>
			/// <param name="default_text">Текст если список пустой</param>
			/// <param name="separator">Разделитель</param>
			/// <param name="use_space">Использовать ли дополнительный пробел между элементами</param>
			/// <returns>Строка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String ToTextString(this ICollection<String> @this, String default_text, Char separator, Boolean use_space)
			{
				if (@this == null || @this.Count == 0)
				{
					return (default_text);
				}

				if (@this.Count == 1)
				{
					return (@this.First());
				}
				else
				{
					StringBuilder builder = new StringBuilder(@this.Count * 10);

					Int32 i = 0;
					foreach (var item in @this)
					{
						builder.Append(item);

						if (i < @this.Count - 1)
						{
							builder.Append(separator);
							if (use_space)
							{
								builder.Append(XChar.Space);
							}
						}

						i++;
					}

					return (builder.ToString());
				}
			}
			#endregion

			#region ======================================= IList =====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка элемента списка по индексу с автоматическим увеличением размера при необходимости
			/// </summary>
			/// <param name="this">Список</param>
			/// <param name="index">Индекс элемента списка</param>
			/// <param name="element">Элемент списка</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetAt(this IList @this, Int32 index, System.Object element)
			{
				if (index >= @this.Count)
				{
					Int32 delta = index - @this.Count + 1;
					for (Int32 i = 0; i < delta; i++)
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение элемента списка по индексу
			/// </summary>
			/// <remarks>
			/// В случае если индекс выходит за границы списка, то возвращается последний элемент
			/// </remarks>
			/// <param name="this">Список</param>
			/// <param name="index">Индекс элемента списка</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetAt(this IList @this, Int32 index)
			{
				if (index >= @this.Count)
				{
					if (@this.Count == 0)
					{
						// Создаем объект по умолчанию
						@this.Add(new System.Object());
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Цикличный доступ к индексатору на получение элемента позволяющий выходить за пределы индекса
			/// </summary>
			/// <param name="this">Список</param>
			/// <param name="index">Индекс элемента списка</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetLoopedObject(this IList @this, Int32 index)
			{
				while (index < 0)
				{
					index += @this.Count;
				}
				if (index >= @this.Count)
				{
					index %= @this.Count;
				}
				return (@this[index]);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Цикличный доступ к индексатору на установку элемента позволяющий выходить за пределы индекса
			/// </summary>
			/// <param name="this">Список</param>
			/// <param name="index">Индекс элемента списка</param>
			/// <param name="value">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetLoopedObject(this IList @this, Int32 index, System.Object value)
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента списка вниз
			/// </summary>
			/// <param name="this">Список</param>
			/// <param name="element_index">Индекс перемещаемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public static void MoveObjectDown(this IList @this, Int32 element_index)
			{
				Int32 next = (element_index + 1) % @this.Count;
				SwapObject(@this, element_index, next);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента списка вверх
			/// </summary>
			/// <param name="this">Список</param>
			/// <param name="element_index">Индекс перемещаемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public static void MoveObjectUp(this IList @this, Int32 element_index)
			{
				Int32 previous = element_index - 1;
				if (previous < 0) previous = @this.Count - 1;
				SwapObject(@this, element_index, previous);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обмен местами элементов списка
			/// </summary>
			/// <param name="this">Список</param>
			/// <param name="old_index">Старая позиция</param>
			/// <param name="new_index">Новая позиция</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IList SwapObject(this IList @this, Int32 old_index, Int32 new_index)
			{
				System.Object temp = @this[old_index];
				@this[old_index] = @this[new_index];
				@this[new_index] = temp;
				return @this;
			}
			#endregion

			#region ======================================= IList<Type> ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Цикличный доступ к индексатору на получение элемента позволяющий выходить за пределы индекса
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="index">Индекс элемента списка</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TItem GetLooped<TItem>(this IList<TItem> @this, Int32 index)
			{
				while (index < 0)
				{
					index += @this.Count;
				}
				if (index >= @this.Count)
				{
					index %= @this.Count;
				}
				return (@this[index]);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Цикличный доступ к индексатору на установку элемента позволяющий выходить за пределы индекса
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="index">Индекс элемента списка</param>
			/// <param name="value">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetLooped<TItem>(this IList<TItem> @this, Int32 index, in TItem value)
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента списка вниз
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="element_index">Индекс перемещаемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public static void MoveElementDown<TItem>(this IList<TItem> @this, Int32 element_index)
			{
				Int32 next = (element_index + 1) % @this.Count;
				Swap(@this, element_index, next);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента списка вверх
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="element_index">Индекс перемещаемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public static void MoveElementUp<TItem>(this IList<TItem> @this, Int32 element_index)
			{
				Int32 previous = element_index - 1;
				if (previous < 0) previous = @this.Count - 1;
				Swap(@this, element_index, previous);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск элемента в списке
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="element">Искомый элемент</param>
			/// <returns>Индекс найденного элемента или -1</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 IndexOf<TItem>(this IList<TItem> @this, in TItem element)
			{
				if (typeof(TItem).IsValueType)
				{
					for (Int32 i = 0; i < @this.Count; i++)
					{
						if (@this[i].Equals(element))
						{
							return (i);
						}
					}
					return (-1);
				}
				else
				{
					for (Int32 i = 0; i < @this.Count; i++)
					{
						if (XObject.ObjectEquals(@this[i], element))
						{
							return (i);
						}
					}

					return (-1);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на уникальность элементов списка
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <returns>Статус уникальности</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsUnique<TItem>(this IList<TItem> @this)
			{
				if (typeof(TItem).IsValueType)
				{
					for (Int32 i = 0; i < @this.Count - 1; i++)
					{
						for (Int32 j = i + 1; j < @this.Count; j++)
						{
							if (@this[i].Equals(@this[j]))
							{
								return (false);
							}
						}
					}
				}
				else
				{
					for (Int32 i = 0; i < @this.Count - 1; i++)
					{
						for (Int32 j = i + 1; j < @this.Count; j++)
						{
							if (XObject.ObjectEquals(@this[i], @this[j]))
							{
								return (false);
							}
						}
					}
				}

				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск элемента в списке
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="element">Искомый элемент</param>
			/// <returns>Индекс найденного элемента или 0</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 IndexOfOrDefault<TItem>(this IList<TItem> @this, in TItem element)
			{
				if (typeof(TItem).IsValueType)
				{
					for (Int32 i = 0; i < @this.Count; i++)
					{
						if (@this[i].Equals(element))
						{
							return (i);
						}
					}
				}
				else
				{
					for (Int32 i = 0; i < @this.Count; i++)
					{
						if (XObject.ObjectEquals(@this[i], element))
						{
							return (i);
						}
					}
				}
				return (0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на равенство элементов списка
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="other">Список</param>
			/// <returns>Статус равенства элементов списка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean EqualElements<TItem>(this IList<TItem> @this, IList<TItem> other)
			{
				if (@this.Count != other.Count)
				{
					return false;
				}

				if (typeof(TItem).IsValueType)
				{
					for (Int32 i = 0; i < @this.Count; i++)
					{
						if (!@this[i].Equals(other[i]))
						{
							return false;
						}
					}
				}
				else
				{
					for (Int32 i = 0; i < @this.Count; i++)
					{
						if (XObject.ObjectEquals(@this[i], other[i]) == false)
						{
							return false;
						}
					}
				}

				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение индекса последнего элемента списка
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <returns>Индекс последнего элемента списка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 LastIndex<TItem>(this IList<TItem> @this)
			{
				return @this.Count - 1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перетасовка элементов списка
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Shuffle<TItem>(this IList<TItem> @this)
			{
				Random rand = new Random();
				Int32 n = @this.Count;
				while (n > 1)
				{
					n--;
					Int32 k = rand.Next(n + 1);
					@this.Swap(n, k);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обмен местами элементов списка
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="old_index">Старая позиция</param>
			/// <param name="new_index">Новая позиция</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IList<TItem> Swap<TItem>(this IList<TItem> @this, Int32 old_index, Int32 new_index)
			{
				TItem temp = @this[old_index];
				@this[old_index] = @this[new_index];
				@this[new_index] = temp;
				return @this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Циклическое смещение элементов списка
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="forward">Статус смещение вперед</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IList<TItem> Shift<TItem>(this IList<TItem> @this, Boolean forward)
			{
				Int32 length = @this.Count;
				Int32 start;
				Int32 sign;
				Int32 i = 0;
				Func<Boolean> condition;
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

				TItem element_to_move = @this[start];
				for (i = start; condition(); i += sign)
				{
					// - get the next element's atIndex
					Int32 next_index;
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

			#region ======================================= IList<Single> =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить индекс элемента список значение которого наболее близко указанному аргументу
			/// </summary>
			/// <param name="this">Список</param>
			/// <param name="value">Значение</param>
			/// <returns>Индекс</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 GetNearestIndex(this IList<Single> @this, Single value)
			{
				if(value <= @this[0])
				{
					return (0);
				}
				if (value >= @this[@this.Count - 1])
				{
					return (@this.Count - 1);
				}

				for (Int32 i = 0; i < @this.Count; i++)
				{
					if(value < @this[i])
					{
						Single prev_delta = Math.Abs(value - @this[i - 1]);
						Single curr_delta = Math.Abs(@this[i] - value);
						if(prev_delta < curr_delta)
						{
							return (i - 1);
						}
						else
						{
							return (i);
						}
					}
				}

				return (0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить значение элемента списка значение которого наболее близко указанному аргументу
			/// </summary>
			/// <param name="this">Список</param>
			/// <param name="value">Значение</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single GetNearestValue(this IList<Single> @this, Single value)
			{
				return (@this[@this.GetNearestIndex(value)]);
			}
			#endregion

			#region ======================================= IList<Double> =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить индекс элемента список значение которого наболее близко указанному аргументу
			/// </summary>
			/// <param name="this">Список</param>
			/// <param name="value">Значение</param>
			/// <returns>Индекс</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 GetNearestIndex(this IList<Double> @this, Double value)
			{
				if (value <= @this[0])
				{
					return (0);
				}
				if (value >= @this[@this.Count - 1])
				{
					return (@this.Count - 1);
				}

				for (Int32 i = 0; i < @this.Count; i++)
				{
					if (value < @this[i])
					{
						Double prev_delta = Math.Abs(value - @this[i - 1]);
						Double curr_delta = Math.Abs(@this[i] - value);
						if (prev_delta < curr_delta)
						{
							return (i - 1);
						}
						else
						{
							return (i);
						}
					}
				}

				return (0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить значение элемента списка значение которого наболее близко указанному аргументу
			/// </summary>
			/// <param name="this">Список</param>
			/// <param name="value">Значение</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double GetNearestValue(this IList<Double> @this, Double value)
			{
				return (@this[@this.GetNearestIndex(value)]);
			}
			#endregion

			#region ======================================= Array =====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка элемента массива по индексу с автоматическим увеличением размера при необходимости
			/// </summary>
			/// <typeparam name="TItem">Тип элемента массива</typeparam>
			/// <param name="this">Массив</param>
			/// <param name="index">Индекс элемента массива</param>
			/// <param name="element">Элемент массива</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetAt<TItem>(this TItem[] @this, Int32 index, TItem element)
			{
				if(index >= @this.Length)
				{
					Array.Resize(ref @this, index + 1);
					@this[index] = element;
				}
				else
				{
					@this[index] = element;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение элемента массива по индексу
			/// </summary>
			/// <remarks>
			/// В случае если индекс выходит за границы массива, то возвращается последний элемент
			/// </remarks>
			/// <typeparam name="TItem">Тип элемента массива</typeparam>
			/// <param name="this">Массив</param>
			/// <param name="index">Индекс элемента массива</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TItem GetAt<TItem>(this TItem[] @this, Int32 index)
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на нахождение элемента в массиве
			/// </summary>
			/// <typeparam name="TItem">Тип элемента массива</typeparam>
			/// <param name="this">Массив</param>
			/// <param name="element">Элемент</param>
			/// <returns>Статус нахождения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean Contains<TItem>(this TItem[] @this, TItem element)
			{
				return (Array.IndexOf(@this, element) >= 0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение следующего элемента в массиве
			/// </summary>
			/// <typeparam name="TItem">Тип элемента массива</typeparam>
			/// <param name="this">Массив</param>
			/// <param name="current_element">Текущий элемент</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TItem NextElement<TItem>(this TItem[] @this, TItem current_element)
			{
				if (@this == null || @this.Length == 0)
				{
					return (current_element);
				}

				if (current_element == null)
				{
					return (@this[0]);
				}

				var index = Array.IndexOf(@this, current_element);

				index++;

				if (index >= @this.Length)
				{
					index = 0;
				}

				return (@this[index]);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение предыдущего элемента в массиве
			/// </summary>
			/// <typeparam name="TItem">Тип элемента массива</typeparam>
			/// <param name="this">Массив</param>
			/// <param name="current_element">Текущий элемент</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TItem BackElement<TItem>(this TItem[] @this, TItem current_element)
			{
				if (@this == null || @this.Length == 0)
				{
					return (current_element);
				}

				if (current_element == null)
				{
					return @this[0];
				}

				var index = Array.IndexOf(@this, current_element);

				index--;

				if (index < 0)
				{
					index = @this.Length - 1;
				}

				return (@this[index]);
			}
			#endregion

			#region ======================================= List ======================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка элемента списка по индексу с автоматическим увеличением размера при необходимости
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="index">Индекс элемента списка</param>
			/// <param name="element">Элемент списка</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetAt<TItem>(this List<TItem> @this, Int32 index, in TItem element)
			{
				if (index >= @this.Count)
				{
					Int32 delta = index - @this.Count + 1;
					for (Int32 i = 0; i < delta; i++)
					{
						@this.Add(default(TItem));
					}

					@this[index] = element;
				}
				else
				{
					@this[index] = element;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение элемента списка по индексу
			/// </summary>
			/// <remarks>
			/// В случае если индекс выходит за границы списка, то возвращается последний элемент
			/// </remarks>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="index">Индекс элемента списка</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TItem GetAt<TItem>(this List<TItem> @this, Int32 index)
			{
				if (index >= @this.Count)
				{
					if (@this.Count == 0)
					{
						// Создаем объект по умолчанию
						@this.Add(default(TItem));
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка списка по убыванию
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public static List<TItem> SortDescending<TItem>(this List<TItem> @this)
			{
				@this.Sort();
				@this.Reverse();
				return @this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список сначала до указанного элемента
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="this">Список</param>
			/// <param name="item">Элемент</param>
			/// <param name="included">Включать указанный элемент в удаление</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 TrimStart<TItem>(this List<TItem> @this, in TItem item, Boolean included = true) where TItem : IComparable<TItem>
			{
				Int32 comprare_first = item.CompareTo(@this[0]);
				Int32 comprare_last = item.CompareTo(@this[@this.Count - 1]);

				// Элемент находиться за пределами списка
				if (comprare_first <= 0)
				{
					return (0);
				}
				else
				{
					// Удаляем все элементы
					if (comprare_last > 0)
					{
						Int32 count = @this.Count;
						@this.Clear();
						return (count);
					}
					else
					{
						// Удаляем либо до последнего элемента, либо все элементы
						if (comprare_last == 0)
						{
							if (included)
							{
								Int32 count = @this.Count;
								@this.Clear();
								return (count);
							}
							else
							{
								Int32 count = @this.Count - 1;
								@this.RemoveRange(0, count);
								return (count);
							}
						}
						else
						{
							Int32 max_count = @this.Count - 1;
							for (Int32 i = 1; i < max_count; i++)
							{
								if (@this[i].CompareTo(item) == 0)
								{
									if (included)
									{
										@this.RemoveRange(0, i + 1);
										return (i + 1);
									}
									else
									{
										@this.RemoveRange(0, i);
										return (i);
									}
								}
								else
								{
									if (@this[i].CompareTo(item) > 0)
									{
										@this.RemoveRange(0, i - 1);
										return (i - 1);
									}
								}
							}
						}
					}
				}

				return (0);
			}
			#endregion

			#region ======================================= Dictionary ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить значение по ключу или указанное значение по умолчанию
			/// </summary>
			/// <typeparam name="TKey">Тип ключа</typeparam>
			/// <typeparam name="TValue">Тип значения</typeparam>
			/// <param name="this">Словарь</param>
			/// <param name="key">Ключ</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> @this, in TKey key, TValue default_value) where TValue : struct
			{
				TValue result;
				if (!@this.TryGetValue(key, out result))
				{
					@this[key] = result = default_value;
				}
				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить значение по ключу или созданное значение по умолчанию
			/// </summary>
			/// <typeparam name="TKey">Тип ключа</typeparam>
			/// <typeparam name="TValue">Тип значения</typeparam>
			/// <param name="this">Словарь</param>
			/// <param name="key">Ключ</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> @this, in TKey key) where TValue : new()
			{
				TValue result;
				if (!@this.TryGetValue(key, out result))
				{
					@this[key] = result = new TValue();
				}
				return (result);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================