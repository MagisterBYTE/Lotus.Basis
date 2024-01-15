//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема коллекций
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCollectionSparseSet.cs
*		Высокопроизводительная коллекция основанная на уплотнённом и разряженном массиве.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
        //-------------------------------------------------------------------------------------------------------------
        /** \addtogroup CoreCollections
		*@{*/
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Высокопроизводительная коллекция основанная на уплотнённом и разряженном массиве
        /// </summary>
        //-------------------------------------------------------------------------------------------------------------
        [Serializable]
        public class SparseSet : IEnumerable<Int32>
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Максимальное количество элементов на начальном этапе
			/// </summary>
			public const Int32 INIT_MAX_COUNT = 16;
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal Int32[] _denseItems;
			protected internal Int32[] _sparseItems;
			protected internal Int32 _count;
			protected internal Int32 _maxCount;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Количество элементов
			/// </summary>
			public Int32 Count
			{
				get { return _count; }
			}

			/// <summary>
			/// Максимальное количество элементов
			/// </summary>
			/// <remarks>
			/// Максимальное количество элементов на данном этапе, если текущее количество элементов будет равно максимальному,
			/// то при следующем добавления элемента в коллекцию произойдет перераспределения памяти и максимальное количество
			/// элементов увеличится в двое.
			/// </remarks>
			public Int32 MaxCount
			{
				get { return _maxCount; }
			}

			/// <summary>
			/// Статус пустой коллекции
			/// </summary>
			public Boolean IsEmpty
			{
				get { return _count == 0; }
			}

			/// <summary>
			/// Статус заполненной коллекции
			/// </summary>
			/// <remarks>
			/// Статус заполненной коллекции означает то на текущий момент текущее количество элементов равно максимальному 
			/// и при следующем добавления элемента в коллекцию произойдет перераспределения памяти и максимальное количество 
			/// элементов увеличится в двое
			/// </remarks>
			public Boolean IsFill
			{
				get { return _count == _maxCount; }
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация коллекции
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 this[Int32 index]
			{
				get { return _denseItems[index]; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные коллекции предустановленными данными
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public SparseSet()
				: this(INIT_MAX_COUNT)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные коллекции указанными данными
			/// </summary>
			/// <param name="capacity">Начальная максимальная емкость коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public SparseSet(Int32 capacity)
			{
				_maxCount = capacity > INIT_MAX_COUNT ? capacity : INIT_MAX_COUNT;
				_count = 0;
				_denseItems = new Int32[_maxCount];
				_sparseItems = new Int32[_maxCount];
			}
			#endregion

			#region ======================================= МЕТОДЫ IEnumerator ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Returns an enumerator that iterates through all elements in the set.
			/// </summary>
			/// <returns>Перечислитель </returns>
			//---------------------------------------------------------------------------------------------------------
			public IEnumerator<Int32> GetEnumerator()
			{
				var i = 0;
				while (i < _count)
				{
					yield return _denseItems[i];
					i++;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Returns an enumerator that iterates through all elements in the set.
			/// </summary>
			/// <returns>Перечислитель </returns>
			//---------------------------------------------------------------------------------------------------------
			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление значения в коллекцию
			/// </summary>
			/// <param name="value">Значение</param>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Add(Int32 value)
			{
				if (value >= _maxCount)
				{
					_maxCount = Math.Max(value + 1, _maxCount << 1);
					Array.Resize(ref _denseItems, _maxCount);
					Array.Resize(ref _sparseItems, _maxCount);
				}

				if (_count >= _maxCount)
				{
					_maxCount <<= 1;
					Array.Resize(ref _denseItems, _maxCount);
					Array.Resize(ref _sparseItems, _maxCount);
				}

				_denseItems[_count] = value;
				_sparseItems[value] = _count;
				_count++;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление значений массива в отсутствующих в коллекции
			/// </summary>
			/// <param name="values">Массив значений</param>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void AddValues(params Int32[] values)
			{
				if (_count + values.Length >= _maxCount)
				{
					_maxCount = Math.Max(_count + values.Length + 1, _maxCount << 1);
					Array.Resize(ref _denseItems, _maxCount);
					Array.Resize(ref _sparseItems, _maxCount);
				}

				for (var i = 0; i < values.Length; i++)
				{
					var value = values[i];

					if (value >= _maxCount)
					{
						_maxCount = Math.Max(value + 1, _maxCount << 1);
						Array.Resize(ref _denseItems, _maxCount);
						Array.Resize(ref _sparseItems, _maxCount);
					}

					if (_denseItems[_sparseItems[value]] != value || _sparseItems[value] >= _count)
					{
						_denseItems[_count] = value;
						_sparseItems[value] = _count;
						_count++;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление значения из коллекции
			/// </summary>
			/// <param name="value">Значение</param>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Remove(Int32 value)
			{
				if (Contains(value))
				{
					// put the value at the end of the dense array into the slot of the removed value
					_denseItems[_sparseItems[value]] = _denseItems[_count - 1];

					// put the link to the removed value in the slot of the replaced value
					_sparseItems[_denseItems[_count - 1]] = _sparseItems[value];

					_count--;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление массива значений из коллекции
			/// </summary>
			/// <param name="values">Массив значений</param>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void RemoveValues(params Int32[] values)
			{
				for (var i = 0; i < values.Length; i++)
				{
					var value = values[i];
					if (Contains(value))
					{
						// put the value at the end of the dense array into the slot of the removed value
						_denseItems[_sparseItems[value]] = _denseItems[_count - 1];

						// put the link to the removed value in the slot of the replaced value
						_sparseItems[_denseItems[_count - 1]] = _sparseItems[value];

						_count--;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка значения на наличие в коллекции
			/// </summary>
			/// <param name="value">Значение</param>
			/// <returns>Статус наличия значения</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Boolean Contains(Int32 value)
			{
				if (value >= _maxCount || value < 0)
				{
					return false;
				}
				else
				{
					// value must meet two conditions:
					// 1. link value from the sparse array must point to the current used range in the dense array
					// 2. there must be a valid two-way link
					return _sparseItems[value] < _count && _denseItems[_sparseItems[value]] == value;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка коллекции от элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Clear()
			{
				_count = 0; // simply set n to 0 to clear the set; no re-initialization is required
				Array.Clear(_denseItems, 0, _maxCount);
				Array.Clear(_sparseItems, 0, _maxCount);
			}
			#endregion
		}

        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Высокопроизводительная коллекция основанная на уплотнённом и разряженном массиве
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка</typeparam>
        //-------------------------------------------------------------------------------------------------------------
        [Serializable]
        public class SparseSet<TItem> : IEnumerable<TItem>
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Максимальное количество элементов на начальном этапе
			/// </summary>
			public const Int32 INIT_MAX_COUNT = 16;
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal Int32[] _denseItems;
			protected internal Int32[] _sparseItems;
			protected internal TItem[] _items;
			protected internal Int32 _count;
			protected internal Int32 _maxCount;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Количество элементов
			/// </summary>
			public Int32 Count
			{
				get { return _count; }
			}

			/// <summary>
			/// Максимальное количество элементов
			/// </summary>
			/// <remarks>
			/// Максимальное количество элементов на данном этапе, если текущее количество элементов будет равно максимальному,
			/// то при следующем добавления элемента в коллекцию произойдет перераспределения памяти и максимальное количество
			/// элементов увеличится в двое.
			/// </remarks>
			public Int32 MaxCount
			{
				get { return _maxCount; }
			}

			/// <summary>
			/// Статус пустой коллекции
			/// </summary>
			public Boolean IsEmpty
			{
				get { return _count == 0; }
			}

			/// <summary>
			/// Статус заполненной коллекции
			/// </summary>
			/// <remarks>
			/// Статус заполненной коллекции означает то на текущий момент текущее количество элементов равно максимальному 
			/// и при следующем добавления элемента в коллекцию произойдет перераспределения памяти и максимальное количество 
			/// элементов увеличится в двое
			/// </remarks>
			public Boolean IsFill
			{
				get { return _count == _maxCount; }
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация коллекции
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem this[Int32 index]
			{
				get { return _items[index]; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные коллекции предустановленными данными
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public SparseSet()
				: this(INIT_MAX_COUNT)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные коллекции указанными данными
			/// </summary>
			/// <param name="capacity">Начальная максимальная емкость коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public SparseSet(Int32 capacity)
			{
				_maxCount = capacity > INIT_MAX_COUNT ? capacity : INIT_MAX_COUNT;
				_count = 0;
				_denseItems = new Int32[_maxCount];
				_sparseItems = new Int32[_maxCount];
				_items = new TItem[_maxCount];
			}
			#endregion

			#region ======================================= МЕТОДЫ IEnumerator ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Returns an enumerator that iterates through all elements in the set.
			/// </summary>
			/// <returns>Перечислитель </returns>
			//---------------------------------------------------------------------------------------------------------
			public IEnumerator<TItem> GetEnumerator()
			{
				var i = 0;
				while (i < _count)
				{
					yield return _items[i];
					i++;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Returns an enumerator that iterates through all elements in the set.
			/// </summary>
			/// <returns>Перечислитель </returns>
			//---------------------------------------------------------------------------------------------------------
			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента в коллекцию
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Add(Int32 index, in TItem item)
			{
				if (index >= _maxCount)
				{
					_maxCount = Math.Max(index + 1, _maxCount << 1);
					Array.Resize(ref _denseItems, _maxCount);
					Array.Resize(ref _sparseItems, _maxCount);
					Array.Resize(ref _items, _maxCount);
				}

				if (_count >= _maxCount)
				{
					_maxCount <<= 1;
					Array.Resize(ref _denseItems, _maxCount);
					Array.Resize(ref _sparseItems, _maxCount);
					Array.Resize(ref _items, _maxCount);
				}

				_items[_count] = item;
				_denseItems[_count] = index;
				_sparseItems[index] = _count;
				_count++;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из коллекции по индексу
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Remove(Int32 index)
			{
				if (Contains(index))
				{
					// put the value at the end of the dense array into the slot of the removed value
					_items[_sparseItems[index]] = _items[_count - 1];
					_denseItems[_sparseItems[index]] = _denseItems[_count - 1];

					// put the link to the removed value in the slot of the replaced value
					_sparseItems[_denseItems[_count - 1]] = _sparseItems[index];

					_count--;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление массива элементов из коллекции по индексам
			/// </summary>
			/// <param name="indexes">Массив индексов</param>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void RemoveValues(params Int32[] indexes)
			{
				for (var i = 0; i < indexes.Length; i++)
				{
					var index = indexes[i];
					if (Contains(index))
					{
						// put the value at the end of the dense array into the slot of the removed value
						_denseItems[_sparseItems[index]] = _denseItems[_count - 1];
						_items[_sparseItems[index]] = _items[_count - 1];

						// put the link to the removed value in the slot of the replaced value
						_sparseItems[_denseItems[_count - 1]] = _sparseItems[index];

						_count--;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка элемента на наличие в коллекции по индексу
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Статус наличия элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Boolean Contains(Int32 index)
			{
				if (index >= _maxCount || index < 0)
				{
					return false;
				}
				else
				{
					// value must meet two conditions:
					// 1. link value from the sparse array must point to the current used range in the dense array
					// 2. there must be a valid two-way link
					return _sparseItems[index] < _count && _denseItems[_sparseItems[index]] == index;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка элемента на наличие в коллекции
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус наличия элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Boolean Contains(in TItem item)
			{
				return Array.IndexOf(_items, item) > -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ссылки на элемент коллекции
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Ссылка на элемент коллекции</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ref TItem GetValue(Int32 index)
			{
				return ref _items[_sparseItems[index]];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить значение элемента коллекции по индексу
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void SetValue(Int32 index, in TItem item)
			{
				_items[_sparseItems[index]] = item;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить список индексов элементов
			/// </summary>
			/// <returns>Список индексов</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Int32[] GetIndexes()
			{
				return _denseItems;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка коллекции от элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Clear()
			{
				_count = 0; // simply set n to 0 to clear the set; no re-initialization is required
				Array.Clear(_denseItems, 0, _maxCount);
				Array.Clear(_sparseItems, 0, _maxCount);
				Array.Clear(_items, 0, _maxCount);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================