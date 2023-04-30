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
			protected internal Int32[] mDenseItems;
			protected internal Int32[] mSparseItems;
			protected internal Int32 mCount;
			protected internal Int32 mMaxCount;
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
				get { return mCount; }
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
				get { return mMaxCount; }
			}

			/// <summary>
			/// Статус пустой коллекции
			/// </summary>
			public Boolean IsEmpty
			{
				get { return mCount == 0; }
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
				get { return mCount == mMaxCount; }
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
				get { return mDenseItems[index]; }
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
				mMaxCount = capacity > INIT_MAX_COUNT ? capacity : INIT_MAX_COUNT;
				mCount = 0;
				mDenseItems = new Int32[mMaxCount];
				mSparseItems = new Int32[mMaxCount];
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
				while (i < mCount)
				{
					yield return mDenseItems[i];
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
				if(value >= mMaxCount)
				{
					mMaxCount = Math.Max(value + 1, mMaxCount << 1);
					Array.Resize(ref mDenseItems, mMaxCount);
					Array.Resize(ref mSparseItems, mMaxCount);
				}

				if(mCount >= mMaxCount)
				{
					mMaxCount <<= 1;
					Array.Resize(ref mDenseItems, mMaxCount);
					Array.Resize(ref mSparseItems, mMaxCount);
				}

				mDenseItems[mCount] = value;
				mSparseItems[value] = mCount;
				mCount++;
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
				if (mCount + values.Length >= mMaxCount)
				{
					mMaxCount = Math.Max(mCount + values.Length + 1, mMaxCount << 1);
					Array.Resize(ref mDenseItems, mMaxCount);
					Array.Resize(ref mSparseItems, mMaxCount);
				}

				for (var i = 0; i < values.Length; i++)
				{
					var value = values[i];

					if (value >= mMaxCount)
					{
						mMaxCount = Math.Max(value + 1, mMaxCount << 1);
						Array.Resize(ref mDenseItems, mMaxCount);
						Array.Resize(ref mSparseItems, mMaxCount);
					}

					if (mDenseItems[mSparseItems[value]] != value || mSparseItems[value] >= mCount)
					{
						mDenseItems[mCount] = value;
						mSparseItems[value] = mCount;
						mCount++;
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
					mDenseItems[mSparseItems[value]] = mDenseItems[mCount - 1]; 

					// put the link to the removed value in the slot of the replaced value
					mSparseItems[mDenseItems[mCount - 1]] = mSparseItems[value];

					mCount--;
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
						mDenseItems[mSparseItems[value]] = mDenseItems[mCount - 1];

						// put the link to the removed value in the slot of the replaced value
						mSparseItems[mDenseItems[mCount - 1]] = mSparseItems[value];

						mCount--;
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
				if (value >= mMaxCount || value < 0)
				{
					return false;
				}
				else
				{
					// value must meet two conditions:
					// 1. link value from the sparse array must point to the current used range in the dense array
					// 2. there must be a valid two-way link
					return mSparseItems[value] < mCount && mDenseItems[mSparseItems[value]] == value;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка коллекции от элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Clear()
			{
				mCount = 0; // simply set n to 0 to clear the set; no re-initialization is required
				Array.Clear(mDenseItems, 0, mMaxCount);
				Array.Clear(mSparseItems, 0, mMaxCount);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Высокопроизводительная коллекция основанная на уплотнённом и разряженном массиве
		/// </summary>
		/// <typeparam name="TItem">Тип элемента списка</typeparam>
		//-------------------------------------------------------------------------------------------------------------
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
			protected internal Int32[] mDenseItems;
			protected internal Int32[] mSparseItems;
			protected internal TItem[] mItems;
			protected internal Int32 mCount;
			protected internal Int32 mMaxCount;
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
				get { return mCount; }
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
				get { return mMaxCount; }
			}

			/// <summary>
			/// Статус пустой коллекции
			/// </summary>
			public Boolean IsEmpty
			{
				get { return mCount == 0; }
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
				get { return mCount == mMaxCount; }
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
				get { return mItems[index]; }
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
				mMaxCount = capacity > INIT_MAX_COUNT ? capacity : INIT_MAX_COUNT;
				mCount = 0;
				mDenseItems = new Int32[mMaxCount];
				mSparseItems = new Int32[mMaxCount];
				mItems = new TItem[mMaxCount];
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
				while (i < mCount)
				{
					yield return mItems[i];
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
				if (index >= mMaxCount)
				{
					mMaxCount = Math.Max(index + 1, mMaxCount << 1);
					Array.Resize(ref mDenseItems, mMaxCount);
					Array.Resize(ref mSparseItems, mMaxCount);
					Array.Resize(ref mItems, mMaxCount);
				}

				if (mCount >= mMaxCount)
				{
					mMaxCount <<= 1;
					Array.Resize(ref mDenseItems, mMaxCount);
					Array.Resize(ref mSparseItems, mMaxCount);
					Array.Resize(ref mItems, mMaxCount);
				}

				mItems[mCount] = item;
				mDenseItems[mCount] = index;
				mSparseItems[index] = mCount;
				mCount++;
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
					mItems[mSparseItems[index]] = mItems[mCount - 1];
					mDenseItems[mSparseItems[index]] = mDenseItems[mCount - 1];
					
					// put the link to the removed value in the slot of the replaced value
					mSparseItems[mDenseItems[mCount - 1]] = mSparseItems[index];

					mCount--;
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
						mDenseItems[mSparseItems[index]] = mDenseItems[mCount - 1];
						mItems[mSparseItems[index]] = mItems[mCount - 1];

						// put the link to the removed value in the slot of the replaced value
						mSparseItems[mDenseItems[mCount - 1]] = mSparseItems[index];

						mCount--;
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
				if (index >= mMaxCount || index < 0)
				{
					return false;
				}
				else
				{
					// value must meet two conditions:
					// 1. link value from the sparse array must point to the current used range in the dense array
					// 2. there must be a valid two-way link
					return mSparseItems[index] < mCount && mDenseItems[mSparseItems[index]] == index;
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
				return (Array.IndexOf(mItems, item) > -1);
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
				return ref mItems[mSparseItems[index]];
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
				mItems[mSparseItems[index]] = item;
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
				return (mDenseItems);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка коллекции от элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Clear()
			{
				mCount = 0; // simply set n to 0 to clear the set; no re-initialization is required
				Array.Clear(mDenseItems, 0, mMaxCount);
				Array.Clear(mSparseItems, 0, mMaxCount);
				Array.Clear(mItems, 0, mMaxCount);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================