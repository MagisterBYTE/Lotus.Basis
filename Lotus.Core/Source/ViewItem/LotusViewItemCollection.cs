//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема отображения данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusViewItemCollection.cs
*		Тип коллекции для хранения элементов отображения.
*		Определение коллекции для элементов отображения которая позволяет управлять видимостью данных, обеспечивает их
*	сортировку, группировку, фильтрацию, позволяет выбирать данные и производить над ними операции.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreViewItem
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для определения коллекции элементов отображения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusCollectionView : ILotusOwnerObject
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Источник данных
			/// </summary>
			System.Object Source { get; set; }

			/// <summary>
			/// Список элементов отображения
			/// </summary>
			IList IViewItems { get; }

			/// <summary>
			/// Количество элементов отображения
			/// </summary>
			Int32 CountViewItems { get; }

			//
			// ВЫБРАННЫЙ ЭЛЕМЕНТ
			//
			/// <summary>
			/// Выбранный индекс элемента отображения, -1 выбора нет
			/// </summary>
			/// <remarks>
			/// При множественном выборе индекс последнего выбранного элемента отображения
			/// </remarks>
			Int32 SelectedIndex { get; set; }

			/// <summary>
			/// Предпоследний выбранный индекс элемента отображения, -1 выбора нет
			/// </summary>
			Int32 PrevSelectedIndex { get; }

			/// <summary>
			/// Текущий выбранный элемент отображения
			/// </summary>
			ILotusViewItem ISelectedViewItem { get; set; }

			/// <summary>
			/// Текущий элемент отображения для отображения в отдельном контексте
			/// </summary>
			ILotusViewItem IPresentedViewItem { get; set; }

			//
			// ПАРАМЕТРЫ ФИЛЬТРАЦИИ
			//
			/// <summary>
			/// Статус фильтрации коллекции
			/// </summary>
			Boolean IsFiltered { get; set; }

			//
			// ПАРАМЕТРЫ СОРТИРОВКИ
			//
			/// <summary>
			/// Статус сортировки коллекции
			/// </summary>
			Boolean IsSorted { get; set; }

			/// <summary>
			/// Статус сортировки коллекции по возрастанию
			/// </summary>
			Boolean IsAscendingSorted { get; set; }

			//
			// МНОЖЕСТВЕННЫЙ ВЫБОР
			//
			/// <summary>
			/// Возможность выбора нескольких элементов
			/// </summary>
			Boolean IsMultiSelected { get; set; }

			/// <summary>
			/// Режим выбора нескольких элементов (первый раз выделение, второй раз снятие выделения)
			/// </summary>
			Boolean ModeSelectAddRemove { get; set; }

			/// <summary>
			/// При множественном выборе всегда должен быть выбран хотя бы один элемент
			/// </summary>
			Boolean AlwaysSelectedItem { get; set; }

			/// <summary>
			/// Режим включения отмены выделения элемента
			/// </summary>
			/// <remarks>
			/// При его включение будет вызваться метод элемента <see cref="ILotusViewSelected.SetViewSelected(ILotusViewItem, Boolean)"/>.
			/// Это может не понадобиться если, например, режим визуального реагирования как у кнопки
			/// </remarks>
			Boolean IsEnabledUnselectingItem { get; set; }
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка источника данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ResetSource();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение выбора всех элементов отображения кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			void UnsetAllSelected(ILotusViewItem exclude);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение презентации всех элементов отображения кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент отображения</param>
			/// <param name="parameters">Параметры контекста исключения</param>
			//---------------------------------------------------------------------------------------------------------
			void UnsetAllPresent(ILotusViewItem exclude, CParameters parameters);
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Шаблон коллекции для элементов отображения которая поддерживает концепцию просмотра и управления с полноценной 
		/// поддержкой всех операций
		/// </summary>
		/// <remarks>
		/// Данная коллекции позволяет управлять видимостью данных, обеспечивает их сортировку, группировку, фильтрацию, 
		/// позволяет выбирать данные и производить над ними операции
		/// </remarks>
		/// <typeparam name="TViewItem">Тип элемента отображения</typeparam>
		/// <typeparam name="TData">Тип данных</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class CollectionView<TViewItem, TData> : ListArray<TViewItem>, ILotusNameable, ILotusCollectionView
			where TViewItem : ILotusViewItem, new()
			where TData : class
		{
			#region ======================================= ВНУТРЕННИЕ ТИПЫ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Компара́тор для сортировки элементов отображения по возрастанию
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected sealed class ComparerAscending : IComparer<TViewItem>
			{
				/// <summary>
				/// Глобальный экземпляр
				/// </summary>
				public static readonly ComparerAscending Instance = new ComparerAscending();

				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Сравнение элементов отображения по возрастанию
				/// </summary>
				/// <param name="x">Первый объект</param>
				/// <param name="y">Второй объект</param>
				/// <returns>Статус сравнения</returns>
				//-----------------------------------------------------------------------------------------------------
				public Int32 Compare(TViewItem x, TViewItem y)
				{
					return ComprareOfAscending(x, y);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Компара́тор для сортировки элементов отображения по убыванию
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected sealed class ComparerDescending : IComparer<TViewItem>
			{
				/// <summary>
				/// Глобальный экземпляр
				/// </summary>
				public static readonly ComparerDescending Instance = new ComparerDescending();

				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Сравнение элементов отображения по убыванию
				/// </summary>
				/// <param name="x">Первый объект</param>
				/// <param name="y">Второй объект</param>
				/// <returns>Статус сравнения</returns>
				//-----------------------------------------------------------------------------------------------------
				public Int32 Compare(TViewItem x, TViewItem y)
				{
					var result = ComprareOfAscending(x, y);
					if(result == 1)
					{
						return -1;
					}
					else
					{
						if (result == -1)
						{
							return 1;
						}
						else
						{
							return 0;
						}
					}
				}
			}
			#endregion

			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Статус реализации типом интерейса <see cref="ILotusNameable"/>
			/// </summary>
			public static readonly Boolean IsSupportNameable = typeof(TData).IsSupportInterface<ILotusNameable>();

			/// <summary>
			/// Статус реализации типом интерейса <see cref="ILotusIdentifierId"/>
			/// </summary>
			public static readonly Boolean IsSupportIdentifierId = typeof(TData).IsSupportInterface<ILotusIdentifierId>();

			/// <summary>
			/// Статус реализации типом интерейса <see cref="ILotusViewSelected"/>
			/// </summary>
			public static readonly Boolean IsSupportViewSelected = typeof(TData).IsSupportInterface<ILotusViewSelected>();

			/// <summary>
			/// Статус реализации типом интерейса <see cref="ILotusViewEnabled"/>
			/// </summary>
			public static readonly Boolean IsSupportViewEnabled = typeof(TData).IsSupportInterface<ILotusViewEnabled>();
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			// Идентификация
			protected static readonly PropertyChangedEventArgs PropertyArgsName = new PropertyChangedEventArgs(nameof(Name));
			protected static readonly PropertyChangedEventArgs PropertyArgsSelectedViewItem = new PropertyChangedEventArgs(nameof(SelectedViewItem));
			protected static readonly PropertyChangedEventArgs PropertyArgsPresentedViewItem = new PropertyChangedEventArgs(nameof(PresentedViewItem));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsFiltered = new PropertyChangedEventArgs(nameof(IsFiltered));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsSorted = new PropertyChangedEventArgs(nameof(IsSorted));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsAscendingSorted = new PropertyChangedEventArgs(nameof(IsAscendingSorted));
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение элементов отображения по возрастанию
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус сравнения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 ComprareOfAscending(TViewItem left, TViewItem right)
			{
				if (left == null)
				{
					if (right == null)
					{
						return 0;
					}
					else
					{
						return 1;
					}
				}
				else
				{
					if (right == null)
					{
						return -1;
					}
					else
					{
						if (left.DataContext != null && right.DataContext != null)
						{
							if (left.DataContext is IComparable<TData> left_comparable)
							{
								return left_comparable.CompareTo((TData)right.DataContext);
							}
							else
							{
								return 0;
							}
						}
						else
						{
							if (left is IComparable left_comparable)
							{
								return left_comparable.CompareTo(right);
							}
							else
							{
								return 0;
							}
						}
					}
				}
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mName = "";
			protected internal System.Object mSource;

			// Выбранный элемент
			protected internal Int32 mSelectedIndex = -1;
			protected internal Int32 mPrevSelectedIndex = -1;
			protected internal TViewItem mSelectedViewItem;
			protected internal TViewItem mPresentedViewItem;

			// Параметры фильтрации
			protected internal Boolean mIsFiltered;
			protected internal Predicate<TData> mFilter;

			// Параметры сортировки
			protected internal Boolean mIsSorted;
			protected internal Boolean mIsAscendingSorted;

			// Множественный выбор
			protected internal Boolean mIsMultiSelected;
			protected internal Boolean mModeSelectAddRemove;
			protected internal Boolean mAlwaysSelectedItem;
			protected internal Boolean mIsEnabledUnselectingItem;
			protected internal ListArray<TData> mSelectedItems;

			// События
			protected internal Action mOnCurrentItemChanged;
			protected internal Action<Int32> mOnSelectedIndexChanged;
			protected internal Action<Int32> mOnSelectionAddItem;
			protected internal Action<Int32> mOnSelectionRemovedItem;
			protected internal Action<TViewItem> mOnSelectedItem;
			protected internal Action<TViewItem> mOnActivatedItem;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Наименование коллекции
			/// </summary>
			public virtual String Name
			{
				get { return mName; }
				set
				{
					mName = value;
					NotifyPropertyChanged(PropertyArgsName);
					RaiseNameChanged();
				}
			}

			/// <summary>
			/// Источник данных
			/// </summary>
			public System.Object Source
			{
				get { return mSource; }
				set
				{
					mSource = value;
					ResetSource();
				}
			}

			/// <summary>
			/// Список элементов отображения
			/// </summary>
			public IList IViewItems 
			{
				get { return this; }
			}

			/// <summary>
			/// Количество элементов отображения
			/// </summary>
			public Int32 CountViewItems 
			{
				get { return mCount; }
			}

			//
			// ВЫБРАННЫЙ ЭЛЕМЕНТ
			//
			/// <summary>
			/// Выбранный индекс элемента отображения, -1 выбора нет
			/// </summary>
			/// <remarks>
			/// При множественном выборе индекс последнего выбранного элемента отображения
			/// </remarks>
			public Int32 SelectedIndex
			{
				get { return mSelectedIndex; }
				set
				{
					this.SetSelectedItem(value);
				}
			}

			/// <summary>
			/// Предпоследний выбранный индекс элемента отображения, -1 выбора нет
			/// </summary>
			public Int32 PrevSelectedIndex
			{
				get { return mPrevSelectedIndex; }
			}

			/// <summary>
			/// Текущий выбранный элемент отображения
			/// </summary>
			public ILotusViewItem ISelectedViewItem
			{
				get { return mSelectedViewItem; }
				set
				{
					SelectedViewItem = (TViewItem)value;
				}
			}

			/// <summary>
			/// Текущий выбранный элемент отображения
			/// </summary>
			public TViewItem SelectedViewItem
			{
				get
				{
					return mSelectedViewItem;
				}
				set
				{
					if (ReferenceEquals(mSelectedViewItem, value) == false)
					{
						mSelectedViewItem = value;
						NotifyPropertyChanged(PropertyArgsSelectedViewItem);
					}
				}
			}

			/// <summary>
			/// Текущий элемент отображения для отображения в отдельном контексте
			/// </summary>
			public ILotusViewItem IPresentedViewItem
			{
				get { return mPresentedViewItem; }
				set
				{
					PresentedViewItem = (TViewItem)value;
				}
			}

			/// <summary>
			/// Текущий элемент отображения для отображения в отдельном контексте
			/// </summary>
			public TViewItem PresentedViewItem
			{
				get
				{
					return mPresentedViewItem;
				}
				set
				{
					if (ReferenceEquals(mPresentedViewItem, value) == false)
					{
						mPresentedViewItem = value;
						NotifyPropertyChanged(PropertyArgsPresentedViewItem);
					}
				}
			}

			//
			// ПАРАМЕТРЫ ФИЛЬТРАЦИИ
			//
			/// <summary>
			/// Статус фильтрации коллекции
			/// </summary>
			public Boolean IsFiltered 
			{
				get { return mIsFiltered; }
				set
				{
					mIsFiltered = value;
					NotifyPropertyChanged(PropertyArgsIsFiltered);
					RaiseIsFilteredChanged();
				}
			}

			/// <summary>
			/// Функтор осуществляющий фильтрацию данных
			/// </summary>
			public Predicate<TData> Filter
			{
				get
				{
					return mFilter;
				}
				set
				{
					if(mFilter == null || mFilter != value)
					{
						mFilter = value;
						if (mFilter != null)
						{
							mIsFiltered = true;
							NotifyPropertyChanged(PropertyArgsIsFiltered);
							ResetSource();
						}
						else
						{
							mIsFiltered = false;
							NotifyPropertyChanged(PropertyArgsIsFiltered);
							ResetSource();
						}
					}
				}
			}

			//
			// ПАРАМЕТРЫ СОРТИРОВКИ
			//
			/// <summary>
			/// Статус сортировки коллекции
			/// </summary>
			public Boolean IsSorted 
			{
				get { return mIsSorted; }
				set
				{
					mIsSorted = value;
					NotifyPropertyChanged(PropertyArgsIsSorted);
				}
			}

			/// <summary>
			/// Статус сортировки коллекции по возрастанию
			/// </summary>
			public Boolean IsAscendingSorted
			{
				get { return mIsAscendingSorted; }
				set
				{
					mIsAscendingSorted = value;
					NotifyPropertyChanged(PropertyArgsIsAscendingSorted);
				}
			}

			//
			// МНОЖЕСТВЕННЫЙ ВЫБОР
			//
			/// <summary>
			/// Возможность выбора нескольких элементов отображения
			/// </summary>
			public Boolean IsMultiSelected
			{
				get { return mIsMultiSelected; }
				set
				{
					mIsMultiSelected = value;
				}
			}

			/// <summary>
			/// Режим выбора нескольких элементов (первый раз выделение, второй раз снятие выделения)
			/// </summary>
			public Boolean ModeSelectAddRemove
			{
				get { return mModeSelectAddRemove; }
				set
				{
					mModeSelectAddRemove = value;
				}
			}

			/// <summary>
			/// При множественном выборе всегда должен быть выбран хотя бы один элемент
			/// </summary>
			public Boolean AlwaysSelectedItem
			{
				get { return mAlwaysSelectedItem; }
				set
				{
					mAlwaysSelectedItem = value;
				}
			}

			/// <summary>
			/// Режим включения отмены выделения элемента
			/// </summary>
			/// <remarks>
			/// При его включение будет вызваться метод элемента <see cref="ILotusViewSelected.SetViewSelected(ILotusViewItem, Boolean)"/>.
			/// Это может не понадобиться если, например, режим визуального реагирования как у кнопки
			/// </remarks>
			public Boolean IsEnabledUnselectingItem
			{
				get { return mIsEnabledUnselectingItem; }
				set
				{
					mIsEnabledUnselectingItem = value;
				}
			}

			//
			// СОБЫТИЯ
			//
			/// <summary>
			/// Событие для нотификации об изменение текущего выбранного элемента
			/// </summary>
			public Action OnCurrentItemChanged
			{
				get { return mOnCurrentItemChanged; }
				set { mOnCurrentItemChanged = value; }
			}

			/// <summary>
			/// Событие для нотификации об изменение индекса выбранного элемента. Аргумент - индекс выбранного элемента
			/// </summary>
			public Action<Int32> OnSelectedIndexChanged
			{
				get { return mOnSelectedIndexChanged; }
				set { mOnSelectedIndexChanged = value; }
			}

			/// <summary>
			/// Событие для нотификации о добавлении элемента к списку выделенных(после добавления). Аргумент - индекс (позиция) добавляемого элемента
			/// </summary>
			public Action<Int32> OnSelectionAddItem
			{
				get { return mOnSelectionAddItem; }
				set { mOnSelectionAddItem = value; }
			}

			/// <summary>
			/// Событие для нотификации о удалении элемента из списка выделенных(после удаления). Аргумент - индекс (позиция) удаляемого элемента
			/// </summary>
			public Action<Int32> OnSelectionRemovedItem
			{
				get { return mOnSelectionRemovedItem; }
				set { mOnSelectionRemovedItem = value; }
			}

			/// <summary>
			/// Событие для нотификации о выборе элемента
			/// </summary>
			/// <remarks>
			/// В основном применяется(должно применяется) для служебных целей
			/// </remarks>
			public Action<TViewItem> OnSelectedItem
			{
				get { return mOnSelectedItem; }
				set { mOnSelectedItem = value; }
			}

			/// <summary>
			/// Событие для нотификации о активации элемента
			/// </summary>
			/// <remarks>
			/// В основном применяется(должно применяется) для служебных целей
			/// </remarks>
			public Action<TViewItem> OnActivatedItem
			{
				get { return mOnActivatedItem; }
				set { mOnActivatedItem = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CollectionView()
				: this(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public CollectionView(String name)
			{
				mName = name;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			/// <param name="source">Источник данных</param>
			//---------------------------------------------------------------------------------------------------------
			public CollectionView(String name, System.Object source)
			{
				mName = name;
				mSource = source;
				ResetSource();
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual System.Object Clone()
			{
				var clone = new CollectionView<TViewItem, TData>();
				clone.Name = mName;

				for (var i = 0; i < mCount; i++)
				{
					clone.Add(mArrayOfItems.Clone());
				}

				return clone;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Имя коллекции</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return mName;
			}
			#endregion

			#region ======================================= МЕТОДЫ IDisposable ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Освобождение управляемых ресурсов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Освобождение управляемых ресурсов
			/// </summary>
			/// <param name="disposing">Статус освобождения</param>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void Dispose(Boolean disposing)
			{
				// Освобождаем только управляемые ресурсы
				if (disposing)
				{
					if (mSource is INotifyCollectionChanged collection_changed)
					{
						collection_changed.CollectionChanged -= OnCollectionChangedHandler;
					}
				}

				// Освобождаем неуправляемые ресурсы
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение имени коллекции.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseNameChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса сортировки коллекции.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsFilteredChanged()
			{
				if(mIsFiltered && mFilter != null)
				{
					ResetSource();
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusCollectionView ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение выбора всех элементов отображения кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnsetAllSelected(ILotusViewItem exclude)
			{
				if (exclude != null)
				{
					for (var i = 0; i < mCount; i++)
					{
						if (Object.ReferenceEquals(mArrayOfItems[i], exclude) == false)
						{
							mArrayOfItems[i].IsSelected = false;
						}
					}

					SelectedViewItem = (TViewItem)exclude;
				}
				else
				{
					for (var i = 0; i < mCount; i++)
					{
						mArrayOfItems[i].IsSelected = false;
					}

					SelectedViewItem = default;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение презентации сех элементов отображения кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент отображения</param>
			/// <param name="parameters">Параметры контекста исключения</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnsetAllPresent(ILotusViewItem exclude, CParameters parameters)
			{
				if (exclude != null)
				{
					if (parameters == null)
					{
						for (var i = 0; i < mCount; i++)
						{
							if (Object.ReferenceEquals(mArrayOfItems[i], exclude) == false)
							{
								mArrayOfItems[i].IsPresented = false;
							}
						}

						PresentedViewItem = (TViewItem)exclude;
					}
					else
					{
						Type present_type = parameters.GetValueOfType<Type>();
						if (present_type != null)
						{

						}
					}
				}
				else
				{
					if (parameters == null)
					{
						// Выключаем все элемента отображения
						for (var i = 0; i < mCount; i++)
						{
							mArrayOfItems[i].IsPresented = false;
						}

						PresentedViewItem = default;
					}
					else
					{
						Type present_type = parameters.GetValueOfType<Type>();
						if (present_type != null)
						{

						}
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusOwnerObject ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Присоединение указанного зависимого объекта
			/// </summary>
			/// <param name="owned_object">Объект</param>
			/// <param name="add">Статус добавления в коллекцию</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AttachOwnedObject(ILotusOwnedObject owned_object, Boolean add)
			{
				// Присоединять можем только объекты
				if (owned_object is ILotusViewItem view_item)
				{
					// Если владелец есть
					if (owned_object.IOwner != null)
					{
						// И он не равен текущему
						if (owned_object.IOwner != this)
						{
							// Отсоединяем
							owned_object.IOwner.DetachOwnedObject(owned_object, add);
						}
					}

					if (add)
					{
						view_item.IOwner = this;
						Add(view_item);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отсоединение указанного зависимого объекта
			/// </summary>
			/// <param name="owned_object">Объект</param>
			/// <param name="remove">Статус удаления из коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void DetachOwnedObject(ILotusOwnedObject owned_object, Boolean remove)
			{
				// Отсоединять можем только объекты
				if (owned_object is ILotusViewItem view_item)
				{
					owned_object.IOwner = null;

					if (remove)
					{
						// Ищем его
						var index = IndexOf(view_item);
						if (index != -1)
						{
							// Удаляем
							RemoveAt(index);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление связей для зависимых объектов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UpdateOwnedObjects()
			{
				for (var i = 0; i < mCount; i++)
				{
					mArrayOfItems[i].IOwner = this;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование данного объекта о начале изменения данных указанного объекта
			/// </summary>
			/// <param name="owned_object">Зависимый объект</param>
			/// <param name="data">Объект данные которого будут меняться</param>
			/// <param name="data_name">Имя данных</param>
			/// <returns>Статус разрешения/согласования изменения данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean OnNotifyUpdating(ILotusOwnedObject owned_object, System.Object data, String data_name)
			{
				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование данного объекта об окончании изменении данных указанного объекта
			/// </summary>
			/// <param name="owned_object">Зависимый объект</param>
			/// <param name="data">Объект, данные которого изменились</param>
			/// <param name="data_name">Имя данных</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void OnNotifyUpdated(ILotusOwnedObject owned_object, System.Object data, String data_name)
			{
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инициализация коллекции посредством указанного обобщённого списка
			/// </summary>
			/// <param name="list">Обобщенный список</param>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void CreateFromList(IList list)
			{
				if (mCount > 0)
				{
					Clear();
				}

				// Устанавливаем параметры
				mIsReadOnly = list.IsReadOnly;
				mIsFixedSize = list.IsFixedSize;

				// Смотрим статус фильта
				if (mFilter != null && mIsFiltered == true)
				{
					for (var i = 0; i < list.Count; i++)
					{
						// Если тип поддерживается
						if (list[i] is TData item)
						{
							// И проходит условия фильтрации
							if (mFilter(item))
							{
								var view_item = new TViewItem();
								view_item.IOwner = this;
								view_item.DataContext = item;

								if (list[i] is ILotusViewItemOwner view_item_owner)
								{
									view_item_owner.OwnerViewItem = view_item;
								}

								// Добавляем
								Add(view_item);
							}
						}
					}
				}
				else
				{
					for (var i = 0; i < list.Count; i++)
					{
						// Если тип поддерживается
						if (list[i] is TData item)
						{
							var view_item = new TViewItem();
							view_item.IOwner = this;
							view_item.DataContext = item;

							if (list[i] is ILotusViewItemOwner view_item_owner)
							{
								view_item_owner.OwnerViewItem = view_item;
							}

							// Добавляем
							Add(view_item);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка источника данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ResetSource()
			{
				if(mSource == null)
				{
					if (mCount > 0)
					{
						Clear();
					}

					mIsReadOnly = false;
					mIsFixedSize = false;
				}

				if(mSource is IList list)
				{
					CreateFromList(list);
				}

				if (mSource is INotifyCollectionChanged collection_changed)
				{
					collection_changed.CollectionChanged += OnCollectionChangedHandler;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка списка по возрастанию
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void SortAscending()
			{
				this.Sort(ComparerAscending.Instance);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка списка по убыванию
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void SortDescending()
			{
				this.Sort(ComparerDescending.Instance);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сброс выделения списка
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Unselect()
			{
				if (mSelectedIndex != -1 && mSelectedIndex < Count)
				{
					if (mIsMultiSelected == false && mIsEnabledUnselectingItem)
					{
						if (IsSupportViewSelected)
						{
							if(mArrayOfItems[mSelectedIndex].DataContext is ILotusViewSelected selected_item)
							{
								selected_item.SetViewSelected(mArrayOfItems[mSelectedIndex], false);
							}
						}
					}

					mPrevSelectedIndex = mSelectedIndex;
					mSelectedIndex = -1;

					// Информируем о смене выбранного элемента
					if (mOnSelectedIndexChanged != null) mOnSelectedIndexChanged(mSelectedIndex);
					if (mOnCurrentItemChanged != null) mOnCurrentItemChanged();

					if (mIsMultiSelected && mIsEnabledUnselectingItem)
					{
						for (var i = 0; i < mSelectedItems.Count; i++)
						{
						}

						mSelectedItems.Clear();
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ТЕКУЩИМ ЭЛЕМЕНТОМ =========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Активация элемента списка
			/// </summary>
			/// <param name="item">Элемент списка</param>
			//---------------------------------------------------------------------------------------------------------
			internal void ActivatedItemDirect(ILotusViewSelected item)
			{
				for (var i = 0; i < Count; i++)
				{
					// 1) Смотрим на совпадение
					if (mArrayOfItems[i].DataContext.Equals(item))
					{
						SetSelectedItem(i);
						if (mOnActivatedItem != null)
						{
							mOnActivatedItem(mArrayOfItems[i]);
						}
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка выбранного элемента
			/// </summary>
			/// <param name="index">Индекс выбранного элемента</param>
			//---------------------------------------------------------------------------------------------------------
			internal void SetSelectedItem(Int32 index)
			{
				if (index > -1 && index < Count)
				{
					// Выключенный элемент выбрать нельзя
					if (IsSupportViewSelected)
					{
						var selected_item = mArrayOfItems[index].DataContext as ILotusViewSelected;
						if (selected_item != null && selected_item.CanViewSelected(mArrayOfItems[index]) == false)
						{
							return;
						}
					}

					var old_index = mSelectedIndex;

					// Если выбран другой элемент
					if (old_index != index)
					{
						// Сохраняем старый выбор
						mPrevSelectedIndex = mSelectedIndex;
						mSelectedIndex = index;

						// Если нет режима мульти выбора
						if (!mIsMultiSelected)
						{
							// Обновляем статус
							if (IsSupportViewSelected)
							{
								if (mArrayOfItems[mSelectedIndex].DataContext is ILotusViewSelected selected_item)
								{
									selected_item.SetViewSelected(mArrayOfItems[mSelectedIndex], true);
								}
							}

							// Если предыдущий элемент был выбран, то снимаем выбор
							if (mPrevSelectedIndex != -1 && mPrevSelectedIndex < Count)
							{
								// Если нет мульти выбора
								if (IsSupportViewSelected && mIsEnabledUnselectingItem)
								{
									if(mArrayOfItems[mPrevSelectedIndex].DataContext is ILotusViewSelected prev_selected_item)
									{
										prev_selected_item.SetViewSelected(mArrayOfItems[mPrevSelectedIndex], false);
									}
								}
							}
						}

						// Информируем о смене выбранного элемента
						if (mOnSelectedIndexChanged != null) mOnSelectedIndexChanged(mSelectedIndex);
						if (mOnCurrentItemChanged != null) mOnCurrentItemChanged();
					}

					// Пользователь выбрал тот же элемент  - Только если включен мультирежим 
					if (mIsMultiSelected)
					{
						// Смотрим, есть ли у нас элемент в выделенных
						if (mSelectedItems.Contains(mArrayOfItems[index].DataContext))
						{
							// Есть
							// Режим снятие/выделения
							if (mModeSelectAddRemove)
							{
								// Только если мы можем оставлять элементе на невыбранными
								if (mAlwaysSelectedItem == false ||
								   (mAlwaysSelectedItem && mSelectedItems.Count > 1))
								{
									// Убираем выделение
									if (IsSupportViewSelected && mIsEnabledUnselectingItem)
									{
										if (mArrayOfItems[index].DataContext is ILotusViewSelected selected_item)
										{
											selected_item.SetViewSelected(mArrayOfItems[index], false);
										}
									}

									// Удаляем
									mSelectedItems.Remove(mArrayOfItems[index].DataContext);

									// Информируем - вызываем событие
									if (mOnSelectionRemovedItem != null) mOnSelectionRemovedItem(index);
								}
								else
								{
#if UNITY_EDITOR
									UnityEngine.Debug.Log("At least one element must be selected");
#else
									XLogger.LogInfo("At least one element must be selected");
#endif
								}
							}
						}
						else
						{
							// Нету - добавляем
							mSelectedItems.Add(mArrayOfItems[index].DataContext);

							// Выделяем элемент
							if (IsSupportViewSelected)
							{
								if (mArrayOfItems[index].DataContext is ILotusViewSelected selected_item)
								{
									selected_item.SetViewSelected(mArrayOfItems[index], true);
								}
							}

							// Информируем - вызываем событие
							if (mOnSelectionAddItem != null) mOnSelectionAddItem(index);
						}
					}
				}
				else
				{
					if (index < 0)
					{
						this.Unselect();
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Дублирование текущего элемента и добавление элемента в список элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void DublicateSelectedItem()
			{
				if (mSelectedIndex != -1)
				{
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление текущего элемента из списка (удаляется объект)
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void DeleteSelectedItem()
			{
				if (mSelectedIndex != -1)
				{
					RemoveAt(mSelectedIndex);
					SelectedIndex = -1;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение текущего элемента назад
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void MoveSelectedBackward()
			{
				// Корректируем индекс
				if (SelectedViewItem != null && mSelectedIndex > 0)
				{
					MoveUp(mSelectedIndex);

					// Корректируем индекс
					SetSelectedItem(mSelectedIndex - 1);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение текущего элемента вперед
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void MoveSelectedForward()
			{
				// Корректируем индекс
				if (SelectedViewItem != null && mSelectedIndex < Count - 1)
				{
					MoveDown(mSelectedIndex);

					// Корректируем индекс
					SelectedIndex++;
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ С МНОЖЕСТВЕННЫМ ВЫБОРОМ ============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Служебный метод
			/// </summary>
			/// <returns>Список выделенных индексов</returns>
			//---------------------------------------------------------------------------------------------------------
			public String GetSelectedIndexes()
			{
				var result = "{" + mSelectedItems.Count.ToString() + "} ";
				for (var i = 0; i < mSelectedItems.Count; i++)
				{
					if (mSelectedItems[i] != null)
					{
						result += mSelectedItems[i].ToString() + "; ";
					}
				}

				return result;
			}
			#endregion

			#region ======================================= ОБРАБОТЧИКИ СОБЫТИЙ =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработчик события изменения данных источника привязки
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void OnCollectionChangedHandler(Object sender, NotifyCollectionChangedEventArgs args)
			{
				switch (args.Action)
				{
					case NotifyCollectionChangedAction.Add:
						{
							IList new_objects = args.NewItems;
							if (new_objects != null && new_objects.Count > 0)
							{
								for (var i = 0; i < new_objects.Count; i++)
								{
									// Проверяем на дубликаты
									var is_dublicate = false;
									for (var j = 0; j < Count; j++)
									{
										if (mArrayOfItems[j].DataContext == new_objects[i])
										{
											is_dublicate = true;
											break;
										}
									}

									if (is_dublicate == false)
									{
										var view_item = new TViewItem();
										view_item.IOwner = this;

										var data = (TData)new_objects[i];
										view_item.DataContext = data;

										if (data is ILotusViewItemOwner view_item_owner)
										{
											view_item_owner.OwnerViewItem = view_item;
										}

										// Добавляем
										this.Add(view_item);
									}
								}
							}
						}
						break;
					case NotifyCollectionChangedAction.Move:
						{
							var old_index = args.OldStartingIndex;
							var new_index = args.NewStartingIndex;
							Move(old_index, new_index);
						}
						break;
					case NotifyCollectionChangedAction.Remove:
						{
							IList old_items = args.OldItems;
							if (old_items != null && old_items.Count > 0)
							{
								for (var i = 0; i < old_items.Count; i++)
								{
									var data_context = (TData)old_items[i];

									// Находим элемент с данным контекстом
									ILotusViewItem view_item = this.Search((item) =>
									{
										if (Object.ReferenceEquals(item.DataContext, data_context))
										{
											return true;
										}
										else
										{
											return false;
										}
									});

									if (view_item != null)
									{
										view_item.DataContext = null;
										this.Remove(view_item);
									}
								}
							}
						}
						break;
					case NotifyCollectionChangedAction.Replace:
						{

						}
						break;
					case NotifyCollectionChangedAction.Reset:
						{

						}
						break;
					default:
						break;
				}
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для элементов отображения которые поддерживают концепцию просмотра и управления с полноценной 
		/// поддержкой всех операций
		/// </summary>
		/// <remarks>
		/// Данная коллекции позволяет управлять видимостью данных, обеспечивает их сортировку, группировку, фильтрацию, 
		/// позволяет выбирать данные и производить над ними операции
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CCollectionView : CollectionView<CViewItem, System.Object>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionView()
				: base(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionView(String name)
				: base(name)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			/// <param name="source">Источник данных</param>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionView(String name, System.Object source)
				: base(name, source)
			{
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================