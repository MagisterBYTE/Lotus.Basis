//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема коллекций
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCollectionListArray.cs
*		Список на основе массива.
*		Реализация списка на основе массива. Данный список является базовыми типом коллекции для реализации других типов
*	коллекции.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
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
		/// Список на основе массива
		/// </summary>
		/// <remarks>
		/// Среди основных преимуществ использования собственного типа списка можно выделить:
		/// - Обеспечивается более высокая скорость работы за счет отказа проверок границ списка.
		/// - Можно получить доступ непосредственно к родному массиву.
		/// - Также поддерживается сериализации на уровни Unity, с учетом собственного редактора.
		/// - Поддержка уведомлений о смене состояний коллекции.
		/// - Поддержка обобщенного интерфейса IList.
		/// </remarks>
		/// <typeparam name="TItem">Тип элемента списка</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class ListArray<TItem> : IList<TItem>, IList, ILotusCheckOne<TItem>,
			ILotusCheckAll<TItem>, ILotusVisit<TItem>, INotifyPropertyChanged, INotifyCollectionChanged
		{
			#region ======================================= ВНУТРЕННИЕ ТИПЫ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тип реализующий перечислителя по списку
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public struct ListArrayEnumerator : IEnumerator<TItem>
			{
				#region ======================================= ДАННЫЕ ================================================
				private ListArray<TItem> mList;
				private Int32 mIndex;
				private TItem mCurrent;
				#endregion

				#region ======================================= СВОЙСТВА ==============================================
				/// <summary>
				/// Текущий элемент
				/// </summary>
				public readonly TItem Current
				{
					get
					{
						return mCurrent;
					}
				}

				/// <summary>
				/// Текущий элемент
				/// </summary>
				readonly Object IEnumerator.Current
				{
					get
					{
						return Current;
					}
				}
				#endregion

				#region ======================================= КОНСТРУКТОРЫ ==========================================
				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Конструктор инициализирует данные перечислителя указанным списком
				/// </summary>
				/// <param name="list">Список</param>
				//-----------------------------------------------------------------------------------------------------
				internal ListArrayEnumerator(ListArray<TItem> list)
				{
					mList = list;
					mIndex = 0;
					mCurrent = default;
				}
				#endregion

				#region ======================================= ОБЩИЕ МЕТОДЫ ==========================================
				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Освобождение управляемых ресурсов
				/// </summary>
				//-----------------------------------------------------------------------------------------------------
				public readonly void Dispose()
				{
				}

				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Переход к следующему элементу списка
				/// </summary>
				/// <returns>Возможность перехода к следующему элементу списка</returns>
				//-----------------------------------------------------------------------------------------------------
				public Boolean MoveNext()
				{
					if (mIndex < mList.Count)
					{
						mCurrent = mList.mArrayOfItems[mIndex];
						mIndex++;
						return true;
					}
					else
					{
						mIndex = mList.Count + 1;
						mCurrent = default;
						return false;
					}
				}

				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Перестановка позиции на первый элемент списка
				/// </summary>
				//-----------------------------------------------------------------------------------------------------
				void IEnumerator.Reset()
				{
					mIndex = 0;
					mCurrent = default;
				}
				#endregion
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный функтор для сравнения элемента
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			internal sealed class FunctorComparer : IComparer<TItem>
			{
				readonly Comparison<TItem> Comparison;

				public FunctorComparer(Comparison<TItem> comparison)
				{
					Comparison = comparison;
				}

				public Int32 Compare(TItem x, TItem y)
				{
					return Comparison(x, y);
				}
			}
			#endregion

			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Максимальное количество элементов на начальном этапе
			/// </summary>
			public const Int32 INIT_MAX_COUNT = 8;

			/// <summary>
			/// Статус ссылочного типа элемента коллекции
			/// </summary>
			public static readonly Boolean IsNullable = !typeof(TItem).IsValueType || Nullable.GetUnderlyingType(typeof(TItem)) != null;

			/// <summary>
			/// Статус поддержки типом элемента интерфейса <see cref="ILotusIndexable"/>
			/// </summary>
			public static readonly Boolean IsIndexable = typeof(TItem).IsSupportInterface<ILotusIndexable>();

			/// <summary>
			/// Статус поддержки типом элемента интерфейса ILotusDuplicate
			/// </summary>
			public static readonly Boolean IsDuplicatable = typeof(TItem).IsSupportInterface<ILotusDuplicate<TItem>>();

			/// <summary>
			/// Компаратор поддержки операций сравнения объектов в отношении равенства
			/// </summary>
			public static readonly EqualityComparer<TItem> EqualityComparerDefault = EqualityComparer<TItem>.Default;

			/// <summary>
			/// Компаратор поддержки операций сравнения объектов при упорядочении
			/// </summary>
			public static readonly Comparer<TItem> ComparerDefault = Comparer<TItem>.Default;
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static readonly PropertyChangedEventArgs PropertyArgsCount = new PropertyChangedEventArgs(nameof(Count));
			protected static readonly PropertyChangedEventArgs PropertyArgsIndexer = new PropertyChangedEventArgs("Item[]");
			protected static readonly PropertyChangedEventArgs PropertyArgsIsEmpty = new PropertyChangedEventArgs(nameof(IsEmpty));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsFill = new PropertyChangedEventArgs(nameof(IsFill));
			protected static readonly NotifyCollectionChangedEventArgs CollectionArgsReset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal TItem[] mArrayOfItems;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.HideInInspector]
			[UnityEngine.SerializeField]
#endif
			protected internal Int32 mCount;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.HideInInspector]
			[UnityEngine.SerializeField]
#endif
			protected internal Int32 mMaxCount;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.HideInInspector]
			[UnityEngine.SerializeField]
#endif
			protected internal Boolean mIsNotify;
			protected internal Boolean mIsReadOnly;
			protected internal Boolean mIsFixedSize;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Максимальное количество элементов
			/// </summary>
			/// <remarks>
			/// Максимальное количество элементов на данном этапе, если текущее количество элементов будет равно максимальному,
			/// то при следующем добавления элемента в коллекцию произойдет перераспределения памяти и максимальное количество
			/// элементов увеличится в двое.
			/// Можно заранее увеличить максимальное количество элементов вызвав метод <see cref="Resize(Int32)"/>
			/// </remarks>
			public Int32 MaxCount
			{
				get { return mMaxCount;}
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

			/// <summary>
			/// Статус включения уведомлений коллекции о своих изменениях
			/// </summary>
			public Boolean IsNotify
			{
				get { return mIsNotify; }
				set
				{
					mIsNotify = value;
				}
			}

			/// <summary>
			/// Индекс последнего элемента
			/// </summary>
			public Int32 LastIndex
			{
				get { return mCount - 1; }
			}

			/// <summary>
			/// Данные массива для сериализации
			/// </summary>
			[Browsable(false)]
			public TItem[] SerializeItems
			{
				get 
				{
					TrimExcess();
					return mArrayOfItems; 
				}
				set
				{
					SetData(value, value.Length);
				}
			}

			//
			// ДОСТУП К ЭЛЕМЕНТАМ
			//
			/// <summary>
			/// Первый элемент
			/// </summary>
			[Browsable(false)]
			public TItem ItemFirst
			{
				get
				{
					return mArrayOfItems[0];
				}
				set
				{
					mArrayOfItems[0] = value;
				}
			}

			/// <summary>
			/// Второй элемент
			/// </summary>
			[Browsable(false)]
			public TItem ItemSecond
			{
				get
				{
					return mArrayOfItems[0];
				}
				set
				{
					mArrayOfItems[0] = value;
				}
			}

			/// <summary>
			/// Предпоследний элемент
			/// </summary>
			[Browsable(false)]
			public TItem ItemPenultimate
			{
				get
				{
					return mArrayOfItems[mCount - 2];
				}
				set
				{
					mArrayOfItems[mCount - 2] = value;
				}
			}

			/// <summary>
			/// Последний элемент
			/// </summary>
			[Browsable(false)]
			public TItem ItemLast
			{
				get
				{
					return mArrayOfItems[mCount - 1];
				}
				set
				{
					mArrayOfItems[mCount - 1] = value;
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА IList ============================================
			/// <summary>
			/// Количество элементов
			/// </summary>
			public Int32 Count
			{
				get { return mCount; }
			}

			/// <summary>
			/// Статус коллекции только для чтения
			/// </summary>
			public Boolean IsReadOnly
			{
				get { return mIsReadOnly; }
				set { mIsReadOnly = value; }
			}

			/// <summary>
			/// Статус фиксированной коллекции
			/// </summary>
			public Boolean IsFixedSize
			{
				get 
				{ 
					return mIsFixedSize;
				}
				set
				{
					mIsFixedSize = value;
				}
			}

			/// <summary>
			/// Статус синхронизации коллекции
			/// </summary>
			public Boolean IsSynchronized
			{
				get { return mArrayOfItems.IsSynchronized; }
			}

			/// <summary>
			/// Объект синхронизации
			/// </summary>
			public System.Object SyncRoot
			{
				get { return mArrayOfItems.SyncRoot; }
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация списка
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			System.Object IList.this[Int32 index]
			{
				get
				{
					return mArrayOfItems[index];
				}
				set
				{
					try
					{
						mArrayOfItems[index] = (TItem)value;

						if (mIsNotify)
						{

						}
					}
					catch (Exception exc)
					{
#if UNITY_2017_1_OR_NEWER
						UnityEngine.Debug.LogException(exc);
#else
						XLogger.LogException(exc);
#endif
					}

				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные списка предустановленными данными
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public ListArray()
				: this(INIT_MAX_COUNT)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные списка указанными данными
			/// </summary>
			/// <param name="capacity">Начальная максимальная емкость списка</param>
			//---------------------------------------------------------------------------------------------------------
			public ListArray(Int32 capacity)
			{
				mMaxCount = capacity > INIT_MAX_COUNT ? capacity : INIT_MAX_COUNT;
				mCount = 0;
				mArrayOfItems = new TItem[mMaxCount];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные списка указанными данными
			/// </summary>
			/// <param name="items">Список элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public ListArray(IList<TItem> items)
			{
				if (items != null && items.Count > 0)
				{
					mMaxCount = items.Count;
					mCount = items.Count;
					mArrayOfItems = new TItem[mMaxCount];

					for (var i = 0; i < items.Count; i++)
					{
						mArrayOfItems[i] = items[i];
					}
				}
				else
				{
					mMaxCount = INIT_MAX_COUNT;
					mCount = 0;
					mArrayOfItems = new TItem[mMaxCount];
				}
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация списка
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem this[Int32 index]
			{
				get { return mArrayOfItems[index]; }
				set
				{
					if (mIsNotify)
					{
						TItem original_item = mArrayOfItems[index];
						mArrayOfItems[index] = value;

						NotifyPropertyChanged(PropertyArgsIndexer);
						NotifyCollectionChanged(NotifyCollectionChangedAction.Replace, original_item, value, index);
					}
					else
					{
						mArrayOfItems[index] = value;
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ IEnumerable ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение перечислителя
			/// </summary>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public IEnumerator<TItem> GetEnumerator()
			{
				return new ListArrayEnumerator(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение перечислителя
			/// </summary>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListArrayEnumerator(this);
			}
			#endregion

			#region ======================================= МЕТОДЫ IList ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Количество элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 Add(System.Object item)
			{
				try
				{
					Add((TItem)item);
					return mCount;
				}
				catch (Exception exc)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogException(exc);
#else
					XLogger.LogException(exc);
#endif
				}

				return mCount;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие элемента в списке
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус наличия элемента в списке</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Contains(System.Object item)
			{
				return Array.IndexOf(mArrayOfItems, item, 0, mCount) > -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение индекса элемента в списке
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Индекс элемента в списке</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 IndexOf(System.Object item)
			{
				return Array.IndexOf(mArrayOfItems, item, 0, mCount);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента в указанную позицию
			/// </summary>
			/// <param name="index">Позиция вставки</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void Insert(Int32 index, System.Object item)
			{
				if(item is TItem item_type)
				{
					Insert(index, item_type);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void Remove(System.Object item)
			{
				if (item is TItem item_type)
				{
					Remove(item_type);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование элементов в указанный массив
			/// </summary>
			/// <param name="array">Целевой массив</param>
			/// <param name="index">Индекс с которого начинается копирование</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyTo(Array array, Int32 index)
			{
				mArrayOfItems.CopyTo(array, index);
			}
			#endregion

			#region ======================================= МЕТОДЫ IList<TItem> =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void Add(TItem item)
			{
				Add(in item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие элемента в списке
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус наличия элемента в списке</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Contains(TItem item)
			{
				return Contains(in item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение индекса элемента в списке
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Индекс элемента в списке</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 IndexOf(TItem item)
			{
				return Array.IndexOf(mArrayOfItems, item, 0, mCount);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента в указанную позицию
			/// </summary>
			/// <param name="index">Позиция вставки</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void Insert(Int32 index, TItem item)
			{
				Insert(index, in item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Remove(TItem item)
			{
				return Remove(in item);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка элемента списка по индексу с автоматическим увеличением размера при необходимости
			/// </summary>
			/// <param name="index">Индекс элемента списка</param>
			/// <param name="element">Элемент списка</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetAt(Int32 index, in TItem element)
			{
				if (index >= mCount)
				{
					Add(element);
				}
				else
				{
					mArrayOfItems[index] = element;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение элемента списка по индексу
			/// </summary>
			/// <remarks>
			/// В случае если индекс выходит за границы списка, то возвращается последний элемент
			/// </remarks>
			/// <param name="index">Индекс элемента списка</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem GetAt(Int32 index)
			{
				if (index >= mCount)
				{
					if (mCount == 0)
					{
						// Создаем объект по умолчанию
						var item = Activator.CreateInstance<TItem>();
						Add(in item);
						return mArrayOfItems[0];
					}
					else
					{
						return mArrayOfItems[LastIndex];
					}
				}
				else
				{
					return mArrayOfItems[index];
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Резервирование места на определённое количество элементов с учетом существующих
			/// </summary>
			/// <param name="count">Количество элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public void Reserve(Int32 count)
			{
				if (count <= 0) return;
				var new_count = mCount + count;

				if (new_count > mMaxCount)
				{
					while (mMaxCount < new_count)
					{
						mMaxCount <<= 1;
					}

					var items = new TItem[mMaxCount];
					Array.Copy(mArrayOfItems, items, mCount);
					mArrayOfItems = items;

					// Проходим по всем объектам и если надо создаем объект
					if (IsNullable)
					{
						for (var i = mCount; i < new_count; i++)
						{
							if (mArrayOfItems[i] == null)
							{
								mArrayOfItems[i] = Activator.CreateInstance<TItem>();
							}
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение максимального количества элементов которая может вместить коллекция
			/// </summary>
			/// <param name="newMaxCount">Новое максимальное количество элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public void Resize(Int32 newMaxCount)
			{
				// Если мы увеличиваем емкость массива
				if (newMaxCount > mMaxCount)
				{
					mMaxCount = newMaxCount;
					var items = new TItem[mMaxCount];
					Array.Copy(mArrayOfItems, items, mCount);
					mArrayOfItems = items;
				}
				else
				{
					// Максимальное количество элементов меньше текущего
					// Все ссылочные элементы в данном случае нам надо удалить
					if (newMaxCount < mCount)
					{
						if (IsNullable)
						{
							for (var i = newMaxCount; i < mCount; i++)
							{
								mArrayOfItems[i] = default;
							}
						}

						mCount = newMaxCount;
						mMaxCount = newMaxCount;
						var items = new TItem[mMaxCount];
						Array.Copy(mArrayOfItems, items, mCount);
						mArrayOfItems = items;
					}
					else
					{
						// Простое уменьшение размера массива
						mMaxCount = newMaxCount;
						var items = new TItem[mMaxCount];
						Array.Copy(mArrayOfItems, items, mCount);
						mArrayOfItems = items;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Определение емкости, равную фактическому числу элементов в списке, если это число меньше порогового значения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void TrimExcess()
			{
				var items = new TItem[mCount];
				Array.Copy(mArrayOfItems, items, mCount);
				mArrayOfItems = items;
				mMaxCount = mCount;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование элементов в указанный массив
			/// </summary>
			/// <param name="array">Целевой массив</param>
			/// <param name="arrayIndex">Позиция начала копирования</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyTo(TItem[] array, Int32 arrayIndex)
			{
				Array.Copy(mArrayOfItems, 0, array, arrayIndex, mCount);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных на прямую
			/// </summary>
			/// <param name="data">Данные</param>
			/// <param name="count">Количество данных</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetData(TItem[] data, Int32 count)
			{
				mArrayOfItems = data;
				mCount = count >= 0 ? count : 0;
				mMaxCount = mArrayOfItems.Length;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение непосредственно данных
			/// </summary>
			/// <returns>Данные</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem[] GetData()
			{
				return mArrayOfItems;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка индекса для элементов
			/// </summary>
			/// <remarks>
			/// Индексы присваиваются согласно порядковому номеру элемента
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public void SetIndexElement()
			{
				if (IsIndexable)
				{
					for (var i = 0; i < mCount; i++)
					{
						((ILotusIndexable)mArrayOfItems[i]).Index = i;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации о переустановке коллекции
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyCollectionReset()
			{
				PropertyChanged?.Invoke(this, PropertyArgsCount);
				PropertyChanged?.Invoke(this, PropertyArgsIndexer);
				CollectionChanged?.Invoke(this, CollectionArgsReset);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации об очистки коллекции
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyCollectionClear()
			{
				PropertyChanged?.Invoke(this, PropertyArgsCount);
				PropertyChanged?.Invoke(this, PropertyArgsIndexer);
				CollectionChanged?.Invoke(this, CollectionArgsReset);
			}
			#endregion

			#region ======================================= МЕТОДЫ ДОБАВЛЕНИЯ =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void Add(in TItem item)
			{
				// Если текущие количество элементов равно максимально возможному
				if (mCount == mMaxCount)
				{
					mMaxCount *= 2;
					var items = new TItem[mMaxCount];
					Array.Copy(mArrayOfItems, items, mCount);
					mArrayOfItems = items;
				}

				mArrayOfItems[mCount] = item;
				mCount++;

				if (mIsNotify)
				{
					PropertyChanged?.Invoke(this, PropertyArgsCount);
					PropertyChanged?.Invoke(this, PropertyArgsIndexer);
					NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, mCount - 1);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Дублирование последнего элемента и его добавление
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void AddDuplicateLastItem()
			{
				if (mCount > 0)
				{
					// Если текущие количество элементов равно максимально возможному
					if (mCount == mMaxCount)
					{
						mMaxCount *= 2;
						var items = new TItem[mMaxCount];
						Array.Copy(mArrayOfItems, items, mCount);
						mArrayOfItems = items;
					}

					var item = default(TItem);
					if (IsDuplicatable)
					{
						item = (ItemLast as ILotusDuplicate<TItem>).Duplicate();
					}
					else
					{
						if (ItemLast is ICloneable)
						{
							item = (TItem)(ItemLast as ICloneable).Clone();
						}
						else
						{
							if (IsNullable)
							{

							}
							else
							{
								item = ItemLast;
							}
						}
					}

					mArrayOfItems[mCount] = item;
					mCount++;

					if (mIsNotify)
					{
						PropertyChanged?.Invoke(this, PropertyArgsCount);
						PropertyChanged?.Invoke(this, PropertyArgsIndexer);
						NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, mCount - 1);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элементов
			/// </summary>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItems(params TItem[] items)
			{
				Reserve(items.Length);
				Array.Copy(items, 0, mArrayOfItems, mCount, items.Length);
				mCount += items.Length;

				if (mIsNotify)
				{
					PropertyChanged?.Invoke(this, PropertyArgsCount);
					PropertyChanged?.Invoke(this, PropertyArgsIndexer);
					var original_count = mCount - items.Length;
					for (var i = 0; i < items.Length; i++)
					{
						NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], original_count + i);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элементов
			/// </summary>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItems(IList<TItem> items)
			{
				Reserve(items.Count);
				for (var i = 0; i < items.Count; i++)
				{
					mArrayOfItems[i + mCount] = items[i];
				}
				mCount += items.Count;

				if (mIsNotify)
				{
					PropertyChanged?.Invoke(this, PropertyArgsCount);
					PropertyChanged?.Invoke(this, PropertyArgsIndexer);
					var original_count = mCount - items.Count;
					for (var i = 0; i < items.Count; i++)
					{
						NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], original_count + i);
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ВСТАВКИ ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента в указанную позицию
			/// </summary>
			/// <param name="index">Позиция вставки</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void Insert(Int32 index, in TItem item)
			{
				if (index >= mCount)
				{
					Add(item);
					return;
				}

				// Если текущие количество элементов равно максимально возможному
				if (mCount == mMaxCount)
				{
					mMaxCount *= 2;
					var items = new TItem[mMaxCount];
					Array.Copy(mArrayOfItems, items, mCount);
					mArrayOfItems = items;
				}

				Array.Copy(mArrayOfItems, index, mArrayOfItems, index + 1, mCount - index);
				mArrayOfItems[index] = item;
				mCount++;

				if (mIsNotify)
				{
					PropertyChanged?.Invoke(this, PropertyArgsCount);
					PropertyChanged?.Invoke(this, PropertyArgsIndexer);
					NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента после указанного элемента
			/// </summary>
			/// <param name="original">Элемент после которого будет произведена вставка</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void InsertAfter(in TItem original, in TItem item)
			{
				var index = Array.IndexOf(mArrayOfItems, original);
				Insert(index + 1, item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента перед указанным элементом
			/// </summary>
			/// <param name="original">Элемент перед которым будет произведена вставка</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void InsertBefore(in TItem original, in TItem item)
			{
				var index = Array.IndexOf(mArrayOfItems, original);
				Insert(index, item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элементов в указанную позицию
			/// </summary>
			/// <param name="index">Позиция вставки</param>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void InsertItems(Int32 index, params TItem[] items)
			{
				if (index >= mCount)
				{
					AddItems(items);
					return;
				}

				Reserve(items.Length);
				Array.Copy(mArrayOfItems, index, mArrayOfItems, index + items.Length, mCount - index);
				Array.Copy(items, 0, mArrayOfItems, index, items.Length);
				mCount += items.Length;

				if (mIsNotify)
				{
					PropertyChanged?.Invoke(this, PropertyArgsCount);
					PropertyChanged?.Invoke(this, PropertyArgsIndexer);
					var original_count = mCount - items.Length;
					for (var i = 0; i < items.Length; i++)
					{
						NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], index + i);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элементов в указанную позицию
			/// </summary>
			/// <param name="index">Позиция вставки</param>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void InsertItems(Int32 index, IList<TItem> items)
			{
				if (index >= mCount)
				{
					AddItems(items);
					return;
				}

				Reserve(items.Count);
				Array.Copy(mArrayOfItems, index, mArrayOfItems, index + items.Count, mCount - index);
				for (var i = 0; i < items.Count; i++)
				{
					mArrayOfItems[i + index] = items[i];
				}
				mCount += items.Count;

				if (mIsNotify)
				{
					PropertyChanged?.Invoke(this, PropertyArgsCount);
					PropertyChanged?.Invoke(this, PropertyArgsIndexer);
					var original_count = mCount - items.Count;
					for (var i = 0; i < items.Count; i++)
					{
						NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], index + i);
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ УДАЛЕНИЯ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Remove(in TItem item)
			{
				var index = Array.IndexOf(mArrayOfItems, item, 0, mCount);
				if (index != -1)
				{
					mCount--;
					Array.Copy(mArrayOfItems, index + 1, mArrayOfItems, index, mCount - index);
					mArrayOfItems[mCount] = default;

					if (mIsNotify)
					{
						PropertyChanged?.Invoke(this, PropertyArgsCount);
						PropertyChanged?.Invoke(this, PropertyArgsIndexer);
						NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
					}


					return true;
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элементов
			/// </summary>
			/// <param name="items">Элементы</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 RemoveItems(params TItem[] items)
			{
				var count = 0;
				for (var i = 0; i < items.Length; i++)
				{
					var index = Array.IndexOf(mArrayOfItems, items[i], 0, mCount);
					if (index != -1)
					{
						mCount--;
						Array.Copy(mArrayOfItems, index + 1, mArrayOfItems, index, mCount - index);
						mArrayOfItems[mCount] = default;
						count++;

						if (mIsNotify)
						{
							PropertyChanged?.Invoke(this, PropertyArgsIndexer);
							NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, items[i], index);
						}
					}
				}

				return count;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элементов
			/// </summary>
			/// <param name="items">Элементы</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 RemoveItems(IList<TItem> items)
			{
				var count = 0;
				for (var i = 0; i < items.Count; i++)
				{
					var index = Array.IndexOf(mArrayOfItems, items[i], 0, mCount);
					if (index != -1)
					{
						mCount--;
						Array.Copy(mArrayOfItems, index + 1, mArrayOfItems, index, mCount - index);
						mArrayOfItems[mCount] = default;
						count++;

						if (mIsNotify)
						{
							PropertyChanged?.Invoke(this, PropertyArgsCount);
							PropertyChanged?.Invoke(this, PropertyArgsIndexer);
							NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, items[i], index);
						}
					}
				}

				return count;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элементов с указанного индекса и до конца коллекции
			/// </summary>
			/// <param name="index">Индекс начала удаления элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveItemsEnd(Int32 index)
			{
				var count = mCount - index;
				RemoveRange(index, count);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элементов с определенного диапазона
			/// </summary>
			/// <param name="index">Индекс начала удаления элементов</param>
			/// <param name="count">Количество удаляемых элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveRange(Int32 index, Int32 count)
			{
				if (index < 0)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("Index is less than zero: <{0}> (Return)", index);
					index = 0;
#else
					XLogger.LogErrorFormat("Index is less than zero: <{0}> (Return)", index);
					index = 0;
#endif
				}

				if (count < 1)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("Count is less than one: <{0}> (Return)", count);
					count = 1;
#else
					XLogger.LogErrorFormat("Count is less than one: <{0}> (Return)", count);
					count = 1;
#endif
				}

				if (mCount - index < count)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("The index <{0}> + count <{1}> is greater than the number of elements: <2> (Return)", index, count, mCount);
					return;
#else
					XLogger.LogErrorFormat("The index <{0}> + count <{1}> is greater than the number of elements: <2> (Return)", index, count, mCount);
					return;
#endif
				}

				if (count > 0)
				{
					var i = mCount;
					mCount -= count;

					if (index < mCount)
					{
						Array.Copy(mArrayOfItems, index + count, mArrayOfItems, index, mCount - index);
					}

					Array.Clear(mArrayOfItems, mCount, count);

					if (mIsNotify)
					{
						PropertyChanged?.Invoke(this, PropertyArgsCount);
						PropertyChanged?.Invoke(this, PropertyArgsIndexer);
						NotifyCollectionReset();
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента
			/// </summary>
			/// <param name="index">Индекс удаляемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveAt(Int32 index)
			{
				if (index < 0)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("Index is less than zero: <{0}> (Return)", index);
					return;
#else
					XLogger.LogErrorFormat("Index is less than zero: <{0}> (Return)", index);
					return;
#endif
				}

				if (index > LastIndex)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("The index is greater than the number of elements: <{0}> (Return)", index);
					return;
#else
					XLogger.LogErrorFormat("The index is greater than the number of elements: <{0}> (Return)", index);
					return;
#endif
				}

				if (mIsNotify)
				{
					TItem temp = mArrayOfItems[index];

					mCount--;
					Array.Copy(mArrayOfItems, index + 1, mArrayOfItems, index, mCount - index);
					mArrayOfItems[mCount] = default;

					PropertyChanged?.Invoke(this, PropertyArgsCount);
					PropertyChanged?.Invoke(this, PropertyArgsIndexer);
					NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, temp, index);
				}
				else
				{
					mCount--;
					Array.Copy(mArrayOfItems, index + 1, mArrayOfItems, index, mCount - index);
					mArrayOfItems[mCount] = default;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление первого элемента
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveFirst()
			{
				RemoveAt(0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление последнего элемента
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveLast()
			{
				if (mCount > 0)
				{
					RemoveAt(mCount - 1);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление дубликатов элементов
			/// </summary>
			/// <returns>Количество дубликатов элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 RemoveDuplicates()
			{
				var unique = new TItem[mCount];
				var count = 0;
				for (var i = 0; i < mCount; i++)
				{
					TItem item = mArrayOfItems[i];
					var index = Array.IndexOf(unique, item);
					if(index == -1)
					{
						unique[count] = item;
						count++;
					}
				}

				// У нас есть дубликаты
				if (count < mCount)
				{
					Array.Copy(unique, 0, mArrayOfItems, 0, count);

					for (var i = count; i < mCount; i++)
					{
						mArrayOfItems[i] = default;
					}

					var delta = mCount - count;
					mCount = count;

					if (mIsNotify)
					{
						NotifyCollectionReset();
					}

					return delta;
				}

				return 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление всех элементов, удовлетворяющих условиям указанного предиката
			/// </summary>
			/// <param name="match">Предикат</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 RemoveAll(Predicate<TItem> match)
			{
				var free_index = 0;   // the first free slot in items array

				// Find the first item which needs to be removed.
				while (free_index < mCount && !match(mArrayOfItems[free_index])) free_index++;
				if (free_index >= mCount) return 0;

				var current = free_index + 1;
				while (current < mCount)
				{
					// Find the first item which needs to be kept.
					while (current < mCount && match(mArrayOfItems[current])) current++;

					if (current < mCount)
					{
						// copy item to the free slot.
						mArrayOfItems[free_index++] = mArrayOfItems[current++];
					}
				}

				Array.Clear(mArrayOfItems, free_index, mCount - free_index);
				var result = mCount - free_index;
				mCount = free_index;

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список сначала до указанного элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <param name="included">Включать указанный элемент в удаление</param>
			/// <returns>Количество удаленных элементов, -1 если элемент не найден</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 TrimStart(in TItem item, Boolean included = true)
			{
				var index = Array.IndexOf(mArrayOfItems, item);
				if(index > -1)
				{
					if(index == 0)
					{
						if (included)
						{
							// Удаляем первый элемент
							RemoveFirst();
							return 1;
						}
						else
						{
							return 0;
						}
					}
					else
					{
						if (index == LastIndex)
						{
							// Удаляем все элементы
							if (included)
							{
								var count = mCount;
								Clear();
								return count;
							}
							else
							{
								// Удаляем элементы до последнего
								var count = mCount - 1;
								RemoveRange(0, count);
								return count;
							}
						}
						else
						{
							if (included)
							{
								RemoveRange(0, index + 1);
								return index + 1;
							}
							else
							{
								RemoveRange(0, index);
								return index;
							}
						}
					}
				}

				return -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список с конца до указанного элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <param name="included">Включать указанный элемент в удаление</param>
			/// <returns>Количество удаленных элементов, -1 если элемент не найден</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 TrimEnd(in TItem item, Boolean included = true)
			{
				var index = Array.LastIndexOf(mArrayOfItems, item);
				if (index > -1)
				{
					if (index == 0)
					{
						// Удаляем все элементы
						if (included)
						{
							var count = mCount;
							Clear();
							return count;
						}
						else
						{
							// Удаляем элементы до первого
							var count = mCount - 1;
							RemoveRange(1, count);
							return count;
						}
					}
					else
					{
						if (index == LastIndex)
						{
							// Удаляем последний элемент
							if (included)
							{
								RemoveLast();
								return 1;
							}
							else
							{
								return 0;
							}
						}
						else
						{
							if (included)
							{
								var count = mCount - index;
								RemoveRange(index, count);
								return count;
							}
							else
							{
								var count = mCount - index - 1;
								RemoveRange(index + 1, count);
								return count;
							}
						}
					}
				}

				return -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка списка
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Clear()
			{
				Array.Clear(mArrayOfItems, 0, mCount);
				mCount = 0;

				if (mIsNotify)
				{
					NotifyCollectionClear();
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ МНОЖЕСТВ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Присвоение новых элементов списку
			/// </summary>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void AssignItems(params TItem[] items)
			{
				Reserve(items.Length - mCount);
				Array.Copy(items, 0, mArrayOfItems, 0, items.Length);
				mCount = items.Length;

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Присвоение новых элементов списку
			/// </summary>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void AssignItems(IList<TItem> items)
			{
				Reserve(items.Count - mCount);
				for (var i = 0; i < items.Count; i++)
				{
					mArrayOfItems[i] = items[i];
				}
				mCount = items.Count;

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение элементов и существующих элементов списка
			/// </summary>
			/// <remarks>
			/// Под объедением понимается присвоение только тех переданных элементов которых нет в исходном списке
			/// </remarks>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void UnionItems(params TItem[] items)
			{
				Reserve(items.Length);
				for (var i = 0; i < items.Length; i++)
				{
					if (Array.IndexOf(mArrayOfItems, items[i]) == -1)
					{
						Add(items[i]);
					}
				}
				mCount = items.Length;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение элементов и существующих элементов списка
			/// </summary>
			/// <remarks>
			/// Под объедением понимается присвоение только тех переданных элементов которых нет в исходном списке
			/// </remarks>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void UnionItems(IList<TItem> items)
			{
				Reserve(items.Count);
				for (var i = 0; i < items.Count; i++)
				{
					if(Array.IndexOf(mArrayOfItems, items[i]) == -1)
					{
						Add(items[i]);
					}
				}
				mCount = items.Count;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Разница между элементами списка и элементами указанного списка
			/// </summary>
			/// <remarks>
			/// Под разницей понимается список элементов, которые есть в исходном списке и нет в переданном. 
			/// По-другому это те элементы из исходного списка которые необходимо удалить что бы он соответствовал 
			/// переданному списку
			/// </remarks>
			/// <param name="items">Элементы</param>
			/// <returns>Список элементов списка</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> DifferenceItems(IList<TItem> items)
			{
				var difference = new ListArray<TItem>();

				for (var i = 0; i < mCount; i++)
				{
					if (items.Contains(mArrayOfItems[i]) == false)
					{
						difference.Add(mArrayOfItems[i]);
					}
				}

				return difference;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Разница между элементами списка и элементами указанного списка
			/// </summary>
			/// <remarks>
			/// Под разницей понимается список элементов, которые есть в исходном списке и нет в переданном. 
			/// По-другому это те элементы из исходного списка которые необходимо удалить что бы он соответствовал 
			/// переданному списку
			/// </remarks>
			/// <example>
			/// Исходный список
			/// [0, 2, 4, 7, 5]
			/// Переданный список
			/// [0, 4, 7, 12, 15, 7]
			/// Результат
			/// [2, 5]
			/// </example>
			/// <param name="items">Элементы</param>
			/// <returns>Список элементов списка</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> DifferenceItems(params TItem[] items)
			{
				var difference = new ListArray<TItem>();

				for (var i = 0; i < mCount; i++)
				{
					if (items.ContainsElement(mArrayOfItems[i]) == false)
					{
						difference.Add(mArrayOfItems[i]);
					}
				}

				return difference;
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОИСКА =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие элемента в списке
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус наличия</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Contains(in TItem item)
			{
				return Array.IndexOf(mArrayOfItems, item, 0, mCount) != -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с начала списка индекса указанного элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 IndexOf(in TItem item)
			{
				return Array.IndexOf(mArrayOfItems, item, 0, mCount);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с конца списка индекса указанного элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 LastIndexOf(in TItem item)
			{
				return Array.LastIndexOf(mArrayOfItems, item, 0, mCount);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с начала списка индекса элемента удовлетворяющему указанному предикату
			/// </summary>
			/// <param name="match">Предикат</param>
			/// <returns>Индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 Find(Predicate<TItem> match)
			{
				for (var i = 0; i < mCount; i++)
				{
					if (match(mArrayOfItems[i])) { return i; }
				}

				return -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с конца списка индекса элемента удовлетворяющему указанному предикату
			/// </summary>
			/// <param name="match">Предикат</param>
			/// <returns>Индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 FindLast(Predicate<TItem> match)
			{
				for (var i = LastIndex; i >= 0; i--)
				{
					if (match(mArrayOfItems[i])) { return i; }
				}

				return -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с начала списка элемента удовлетворяющему указанному предикату
			/// </summary>
			/// <param name="match">Предикат</param>
			/// <returns>Элемент или значение по умолчанию</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem Search(Predicate<TItem> match)
			{
				for (var i = 0; i < mCount; i++)
				{
					if (match(mArrayOfItems[i])) { return mArrayOfItems[i]; }
				}

				return default;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с конца списка элемента удовлетворяющему указанному предикату
			/// </summary>
			/// <param name="match">Предикат</param>
			/// <returns>Элемент или значение по умолчанию</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem SearchLast(Predicate<TItem> match)
			{
				for (var i = LastIndex; i >= 0; i--)
				{
					if (match(mArrayOfItems[i])) { return mArrayOfItems[i]; }
				}

				return default;
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОЛЬЗОВАТЕЛЬСКИХ ОПЕРАЦИЙ ==========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка элементов списка на удовлетворение указанного предиката
			/// </summary>
			/// <remarks>
			/// Список удовлетворяет условию предиката если каждый его элемент удовлетворяет условию предиката
			/// </remarks>
			/// <param name="match">Предикат проверки</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean CheckAll(Predicate<TItem> match)
			{
				var result = true;
				for (var i = 0; i < mCount; i++)
				{
					if(match(mArrayOfItems[i]) == false)
					{
						result = false;
						break;
					}
				}
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка элементов списка на удовлетворение указанного предиката
			/// </summary>
			/// <remarks>
			/// Список удовлетворяет условию предиката если хотя бы один его элемент удовлетворяет условию предиката
			/// </remarks>
			/// <param name="match">Предикат проверки</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean CheckOne(Predicate<TItem> match)
			{
				var result = false;
				for (var i = 0; i < mCount; i++)
				{
					if (match(mArrayOfItems[i]))
					{
						result = true;
						break;
					}
				}
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Посещение элементов списка указанным посетителем
			/// </summary>
			/// <param name="onVisitor">Делегат посетителя</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Visit(Action<TItem> onVisitor)
			{
				for (var i = 0; i < mCount; i++)
				{
					onVisitor(mArrayOfItems[i]);
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ МОДИФИКАЦИИ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обмен местами элементов списка
			/// </summary>
			/// <param name="oldIndex">Старая позиция</param>
			/// <param name="newIndex">Новая позиция</param>
			//---------------------------------------------------------------------------------------------------------
			public void Swap(Int32 oldIndex, Int32 newIndex)
			{
				TItem temp = mArrayOfItems[oldIndex];
				mArrayOfItems[oldIndex] = mArrayOfItems[newIndex];
				mArrayOfItems[newIndex] = temp;

				if (mIsNotify)
				{
					PropertyChanged?.Invoke(this, PropertyArgsIndexer);
					NotifyCollectionChanged(NotifyCollectionChangedAction.Move, temp, newIndex, oldIndex);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента в новую позицию
			/// </summary>
			/// <param name="oldIndex">Старая позиция</param>
			/// <param name="newIndex">Новая позиция</param>
			//---------------------------------------------------------------------------------------------------------
			public void Move(Int32 oldIndex, Int32 newIndex)
			{
				TItem temp = mArrayOfItems[oldIndex];
				RemoveAt(oldIndex);
				Insert(newIndex, temp);

				if (mIsNotify)
				{
					PropertyChanged?.Invoke(this, PropertyArgsIndexer);
					NotifyCollectionChanged(NotifyCollectionChangedAction.Move, temp, newIndex, oldIndex);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента списка вниз
			/// </summary>
			/// <remarks>
			/// При перемещении вниз индекс элемент увеличивается
			/// </remarks>
			/// <param name="elementIndex">Индекс перемещаемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public void MoveDown(Int32 elementIndex)
			{
				var next = (elementIndex + 1) % mCount;
				Swap(elementIndex, next);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента списка вверх
			/// </summary>
			/// <remarks>
			/// При перемещении вниз индекс элемент уменьшается
			/// </remarks>
			/// <param name="elementIndex">Индекс перемещаемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public void MoveUp(Int32 elementIndex)
			{
				var previous = elementIndex - 1;
				if (previous < 0) previous = LastIndex;
				Swap(elementIndex, previous);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Циклическое смещение элементов списка вниз
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ShiftDown()
			{
				XCollectionsExtension.Shift(this, true);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Циклическое смещение элементов списка вверх
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ShiftUp()
			{
				XCollectionsExtension.Shift(this, false);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перетасовка элементов списка
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Shuffle()
			{
				XCollectionsExtension.Shuffle(this);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка списка по возрастанию
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SortAscending()
			{
				Array.Sort(mArrayOfItems, 0, mCount);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка списка по убыванию
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SortDescending()
			{
				Array.Sort(mArrayOfItems, 0, mCount);
				Array.Reverse(mArrayOfItems, 0, mCount);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка элементов списка посредством делегата
			/// </summary>
			/// <param name="comparer">Делегат сравнивающий два объекта одного типа</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Sort(IComparer<TItem> comparer)
			{
				Array.Sort(mArrayOfItems, 0, mCount, comparer);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка элементов списка посредством делегата
			/// </summary>
			/// <remarks>
			/// Внимание, методом надо пользоваться с осторожностью так он учитывает реальный размер массива
			/// </remarks>
			/// <param name="comparison">Делегат сравнивающий два объекта одного типа</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Sort(Comparison<TItem> comparison)
			{
				IComparer<TItem> comparer = new FunctorComparer(comparison);

				Array.Sort(mArrayOfItems, 0, mCount, comparer);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ БЛИЖАЙШЕГО ИНДЕКСА =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайшего индекса элемента по его значению, не больше указанного значения
			/// </summary>
			/// <remarks>
			/// Применяется только для списков, отсортированных по возрастанию
			/// </remarks>
			/// <example>
			/// [2, 4, 10, 15] -> ищем элемент со значением 1, вернет индекс 0
			/// [2, 4, 10, 15] -> ищем элемент со значением 2, вернет индекс 0
			/// [2, 4, 10, 15] -> ищем элемент со значением 3, вернет индекс 0
			/// [2, 4, 10, 15] -> ищем элемент со значением 4, вернет индекс 1
			/// [2, 4, 10, 15] -> ищем элемент со значением 9, вернет индекс 1
			/// [2, 4, 10, 15] -> ищем элемент со значением 20, вернет индекс 3
			/// </example>
			/// <param name="item">Элемент</param>
			/// <returns>Ближайший индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetClosestIndex(in TItem item)
			{
				// Если элемент равен или меньше первого возвращаем нулевой индекс
				if (ComparerDefault.Compare(item, ItemFirst) <= 0)
				{
					return 0;
				}

				// Если элемент равен или больше последнего возвращаем последний индекс
				if (ComparerDefault.Compare(item, ItemLast) >= 0)
				{
					return LastIndex;
				}

				for (var i = 1; i < mCount; i++)
				{
					// Получаем статус сравнения
					var status = ComparerDefault.Compare(item, mArrayOfItems[i]);

					// Если элемент равен возвращаем данный индекс
					if (status == 0)
					{
						return i;
					}
					else
					{
						// Если он меньше предыдущего то возвращаем предыдущий индекс
						if (status == -1)
						{
							return i - 1;
						}
					}
				}

				return LastIndex;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список сначала до ближайшего указанного элемента
			/// </summary>
			/// <remarks>
			/// Применяется только для списков, отсортированных по возрастанию.
			/// Включаемый элемент будет удален если он совпадает
			/// </remarks>
			/// <param name="item">Элемент</param>
			/// <param name="included">Включать указанный элемент в удаление</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 TrimClosestStart(in TItem item, Boolean included = true)
			{
				var comprare_first = ComparerDefault.Compare(item, ItemFirst);
				var comprare_last = ComparerDefault.Compare(item, ItemLast);

				// Элемент находиться за пределами списка - в начале
				if (comprare_first <= 0)
				{
					// Удаляем первый элемент если он включен и равен
					if (comprare_first == 0 && included)
					{
						RemoveFirst();
						return 1;
					}
					else
					{
						return 0;
					}
				}

				// Элемент находиться за пределами списка - в конце 
				if (comprare_last >= 0)
				{
					// Если он совпал последним элементом и при этом его не надо удалять
					if (comprare_last == 0 && included == false)
					{
						// Удаляем элементы до последнего
						var count = Count - 1;
						RemoveRange(0, count);
						return count;
					}
					else
					{
						// Удаляем все элементы
						var count = mCount;
						Clear();
						return count;
					}
				}

				// Элемент находиться в пределах списка
				var max_count = Count - 1;
				for (var i = 1; i < max_count; i++)
				{
					var status = ComparerDefault.Compare(item, mArrayOfItems[i]);

					// Элемент полностью совпал
					if (status == 0)
					{
						if (included)
						{
							RemoveRange(0, i + 1);
							return i + 1;
						}
						else
						{
							RemoveRange(0, i);
							return i;
						}
					}
					else
					{
						// Только если он меньше
						if (status == -1)
						{
							RemoveRange(0, i);
							return i;
						}
					}
				}

				return 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список с конца до ближайшего указанного элемента
			/// </summary>
			/// <remarks>
			/// Применяется только для списков, отсортированных по возрастанию.
			/// Включаемый элемент будет удален если он совпадает
			/// </remarks>
			/// <param name="item">Элемент</param>
			/// <param name="included">Включать указанный элемент в удаление</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 TrimClosestEnd(in TItem item, Boolean included = true)
			{
				var comprare_first = ComparerDefault.Compare(item, ItemFirst);
				var comprare_last = ComparerDefault.Compare(item, ItemLast);

				// Элемент находиться за пределами списка - в начале
				if (comprare_first <= 0)
				{
					// Удаляем до первого элементы если он выключен и совпадает
					if (comprare_first == 0 && included == false)
					{
						var count = Count - 1;
						RemoveRange(1, count);
						return count;
					}
					else
					{
						// Удаляем все элементы
						var count = Count;
						Clear();
						return count;
					}
				}

				// Элемент находиться за пределами списка - в конце 
				if (comprare_last >= 0)
				{
					// Если он совпал последним элементом и при этом его надо удалять
					if (comprare_last == 0 && included)
					{
						// Удаляем последний элемент
						RemoveLast();
						return 1;
					}
					else
					{
						return 0;
					}
				}

				// Элемент находиться в пределах списка
				for (var i = Count - 1; i >= 1; i--)
				{
					var status = ComparerDefault.Compare(item, mArrayOfItems[i]);

					// Элемент полностью совпал
					if (status == 0)
					{
						if (included)
						{
							var count = mCount;
							RemoveItemsEnd(i);
							return count - i;
						}
						else
						{
							var count = mCount;
							RemoveItemsEnd(i + 1);
							return count - i - 1;
						}
					}
					else
					{
						// Только если он больше
						if (status == 1)
						{
							var count = mCount;
							RemoveItemsEnd(i + 1);
							return count - i - 1;
						}
					}
				}

				return 0;
			}
			#endregion

			#region ======================================= МЕТОДЫ ГРУППИРОВАНИЯ ======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка групп элементов сгруппированных по указанному свойству
			/// </summary>
			/// <param name="propertyName">Имя свойства</param>
			/// <returns>Список групп элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual ListArray<ListArray<TItem>> GetGroupedByProperty(String propertyName)
			{
				// Список групп
				var groups = new ListArray<ListArray<TItem>>();

				// Cписок уникальных значений
				var unique_list = new ListArray<System.Object>();

				if (IsNullable)
				{
					// Будем получать свойство для каждого элемента так как возможно наследование
					for (var i = 0; i < mCount; i++)
					{
						// Получем свойство
						PropertyInfo property_info = mArrayOfItems[i].GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

						if (property_info != null)
						{
							// Ищем совпадение в существующем списке
							var find_index = unique_list.Find((System.Object value) =>
							{
								var property_value = property_info.GetValue(mArrayOfItems[i], null);
								return value.Equals(property_value);
							});

							// Совпадение не нашли значит это уникальный элемент
							if (find_index == -1)
							{
								// Добавляем в уникальные значений
								unique_list.Add(property_info.GetValue(mArrayOfItems[i], null));

								// Создаем группу
								var group = new ListArray<TItem>();

								// Добавляем туда данный элемент
								group.Add(mArrayOfItems[i]);

								// Добавляем саму группу
								groups.Add(group);
							}
							else
							{
								// Это не уникальный элемент, а значит группа такая уже есть
								// Получаем группу
								ListArray<TItem> group = groups[find_index];

								// Добавляем туда данный элемент
								group.Add(mArrayOfItems[i]);
							}
						}
					}
				}
				else
				{
					// Получаем свойство 1 раз
					PropertyInfo property_info = typeof(TItem).GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
					if (property_info != null)
					{
						for (var i = 0; i < mCount; i++)
						{
							// Ищем совпадение в существующем списке
							var find_index = unique_list.Find((System.Object value) =>
							{
								return value.Equals(property_info.GetValue(mArrayOfItems[i], null));
							});

							// Совпадение не нашли значит это уникальный элемент
							if (find_index == -1)
							{
								// Создаем группу
								var group = new ListArray<TItem>();

								// Добавляем туда данный элемент
								group.Add(mArrayOfItems[i]);

								// Добавляем саму группу
								groups.Add(group);
							}
							else
							{
								// Это не уникальный элемент, а значит группа такая уже есть
								// Получаем группу
								ListArray<TItem> group = groups[find_index];

								// Добавляем туда данный элемент
								group.Add(mArrayOfItems[i]);
							}

						}
					}
				}

				return groups;
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОЛУЧЕНИЯ КОЛЛЕКЦИЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата коллекции
			/// </summary>
			/// <returns>Дубликат коллекции</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> GetItemsDuplicate()
			{
				var items = new ListArray<TItem>(MaxCount);
				Array.Copy(mArrayOfItems, 0, items.mArrayOfItems, 0, mCount);
				items.mCount = Count;
				return items;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение различающихся (уникальных) элементов списка
			/// </summary>
			/// <returns>Список различающихся(уникальных) элементов списка</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> GetItemsDistinct()
			{
				var unique_list = new ListArray<TItem>();

				for (var i = 0; i < mCount; i++)
				{
					// Ищем совпадение в существующем списке
					var find_index = unique_list.Find((TItem value) =>
					{
						return value.Equals(mArrayOfItems[i]);
					});

					// Совпадение не нашли значит это уникальный элемент
					if (find_index == -1)
					{
						// Добавляем в уникальные значений
						unique_list.Add(mArrayOfItems[i]);
					}
				}

				return unique_list;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка уникальных значений указанного свойства
			/// </summary>
			/// <remarks>
			/// Используется рефлексия
			/// </remarks>
			/// <param name="propertyName">Имя свойства</param>
			/// <returns>Список уникальных свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<System.Object> GetDistinctValueFromPropertyName(String propertyName)
			{
				var unique_list = new ListArray<System.Object>();

				if (IsNullable)
				{
					// Будем получать свойство и каждого элемента так как возможно наследование
					for (var i = 0; i < mCount; i++)
					{
						// Получем свойство
						PropertyInfo property_info = mArrayOfItems[i].GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

						if (property_info != null)
						{
							// Ищем совпадение в существующем списке
							var find_index = unique_list.Find((System.Object value) =>
							{
								return value.Equals(property_info.GetValue(mArrayOfItems[i], null));
							});

							// Совпадение не нашли значит это уникальный элемент
							if (find_index == -1)
							{
								// Добавляем в уникальные значений
								unique_list.Add(property_info.GetValue(mArrayOfItems[i], null));
							}
						}
					}
				}
				else
				{
					// Получаем свойство 1 раз
					PropertyInfo property_info = typeof(TItem).GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
					if (property_info != null)
					{
						for (var i = 0; i < mCount; i++)
						{
							// Ищем совпадение в существующем списке
							var find_index = unique_list.Find((System.Object value) =>
							{
								return value.Equals(property_info.GetValue(mArrayOfItems[i], null));
							});

							// Совпадение не нашли значит это уникальный элемент
							if (find_index == -1)
							{
								// Добавляем в уникальные значений
								unique_list.Add(property_info.GetValue(mArrayOfItems[i], null));
							}

						}
					}
				}

				return unique_list;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка элементов по указанному предикату
			/// </summary>
			/// <param name="match">Предикатор</param>
			/// <returns>Список элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> GetItemsWhere(Predicate<TItem> match)
			{
				var result = new ListArray<TItem>(mMaxCount);

				for (var i = 0; i < mCount; i++)
				{
					if(match(mArrayOfItems[i]))
					{
						result.Add(mArrayOfItems[i]);
					}
				}

				return result;
			}
			#endregion

			#region ======================================= ДАННЫЕ INotifyPropertyChanged =============================
			/// <summary>
			/// Событие срабатывает ПОСЛЕ изменения свойства
			/// </summary>
			public event PropertyChangedEventHandler PropertyChanged;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="propertyName">Имя свойства</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanged(String propertyName = "")
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanged(PropertyChangedEventArgs args)
			{
				PropertyChanged?.Invoke(this, args);
			}
			#endregion

			#region ======================================= ДАННЫЕ INotifyCollectionChanged ===========================
			/// <summary>
			/// Событие для информирование событий коллекций
			/// </summary>
			public event NotifyCollectionChangedEventHandler CollectionChanged;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений коллекции
			/// </summary>
			/// <param name="action">Действие с коллекцией</param>
			/// <param name="item">Элемент коллекции</param>
			/// <param name="index">Индекс элемента</param>
			//---------------------------------------------------------------------------------------------------------
			protected void NotifyCollectionChanged(NotifyCollectionChangedAction action, System.Object item, Int32 index)
			{
				CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, item, index));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений коллекции
			/// </summary>
			/// <param name="action">Действие с коллекцией</param>
			/// <param name="item">Элемент коллекции</param>
			/// <param name="index">Индекс элемента</param>
			/// <param name="oldIndex">Предыдущий индекс элемента</param>
			//---------------------------------------------------------------------------------------------------------
			protected void NotifyCollectionChanged(NotifyCollectionChangedAction action, System.Object item, Int32 index, Int32 oldIndex)
			{
				CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений коллекции
			/// </summary>
			/// <param name="action">Действие с коллекцией</param>
			/// <param name="oldItem">Элемент коллекции</param>
			/// <param name="newItem">Элемент коллекции</param>
			/// <param name="index">Индекс элемента</param>
			//---------------------------------------------------------------------------------------------------------
			protected void NotifyCollectionChanged(NotifyCollectionChangedAction action, System.Object oldItem, System.Object newItem, Int32 index)
			{
				CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================