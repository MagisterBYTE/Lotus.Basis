//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема отображения данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusViewItemHierarchy.cs
*		Определение элемента отображения который поддерживает иерархические отношения.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
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
		/// Интерфейса элемента отображения который поддерживает иерархические отношения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusViewItemHierarchy : ILotusViewItem, ILotusOwnerObject
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Уровень вложенности элемента отображения
			/// </summary>
			/// <remarks>
			/// Корневые элементы отображения имеют уровень 0
			/// </remarks>
			Int32 Level { get; set; }

			/// <summary>
			/// Статус корневого элемента отображения
			/// </summary>
			Boolean IsRoot { get; }

			/// <summary>
			/// Статус элемента отображения который не имеет дочерних элементов отображения
			/// </summary>
			Boolean IsLeaf { get; }

			/// <summary>
			/// Статус раскрытия элемента отображения
			/// </summary>
			Boolean IsExpanded { get; set; }

			/// <summary>
			/// Список дочерних элементов отображения
			/// </summary>
			IList IViewItems { get; }

			/// <summary>
			/// Количество дочерних элементов отображения
			/// </summary>
			Int32 CountViewItems { get; }

			/// <summary>
			/// Родительский элемент отображения
			/// </summary>
			ILotusViewItemHierarchy IParent { get; set; }
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Раскрытие всего элемента отображения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void Expanded();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сворачивание всего элемента отображения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void Collapsed();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение количество выделенных элементов отображения включая дочерние
			/// </summary>
			/// <returns>Количество выделенных элементов отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			Int32 GetCountChecked();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на поддержку элемента отображения
			/// </summary>
			/// <param name="viewItem">Элемент отображения</param>
			/// <returns>Статус поддрежки</returns>
			//---------------------------------------------------------------------------------------------------------
			Boolean IsSupportViewItem(ILotusViewItemHierarchy viewItem);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение выбора всех элементов отображения кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			void UnsetAllSelected(ILotusViewItemHierarchy exclude);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение презентации всех элементов отображения кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент отображения</param>
			/// <param name="parameters">Параметры контекста исключения</param>
			//---------------------------------------------------------------------------------------------------------
			void UnsetAllPresent(ILotusViewItemHierarchy exclude, CParameters parameters);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Посещение каждого элемента отображения с указанным предикатом
			/// </summary>
			/// <param name="match">Предикат</param>
			//---------------------------------------------------------------------------------------------------------
			void Visit(Predicate<ILotusViewItemHierarchy> match);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание экземпляра иерархического элемента отображения
			/// </summary>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			ILotusViewItemHierarchy CreateViewItemHierarchy();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Построение дочерней иерархии согласно источнику данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void BuildFromDataContext();
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Шаблон реализующий минимальный элемент отображения который поддерживает иерархические отношения и реализует 
		/// основные параметры просмотра и управления
		/// </summary>
		/// <typeparam name="TData">Тип данных</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class ViewItemHierarchy<TData> : ListArray<ViewItemHierarchy<TData>>, ILotusViewItemHierarchy
			where TData : class
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsName = new PropertyChangedEventArgs(nameof(Name));
			protected static readonly PropertyChangedEventArgs PropertyArgsDataContext = new PropertyChangedEventArgs(nameof(DataContext));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsExpanded = new PropertyChangedEventArgs(nameof(IsExpanded));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsSelected = new PropertyChangedEventArgs(nameof(IsSelected));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsEnabled = new PropertyChangedEventArgs(nameof(IsEnabled));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsChecked = new PropertyChangedEventArgs(nameof(IsChecked));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsPresented = new PropertyChangedEventArgs(nameof(IsPresented));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsEditMode = new PropertyChangedEventArgs(nameof(IsEditMode));
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов отображения
			/// </summary>
			/// <param name="root">Данные корневого узла</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ViewItemHierarchy<TData> Build(TData root, ILotusCollectionViewHierarchy owner)
			{
				ViewItemHierarchy<TData> node_root_view = Build(root, null, owner);
				return node_root_view;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов отображения
			/// </summary>
			/// <param name="data">Данные элемента отображени</param>
			/// <param name="parent">Родительский элемент отображения</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ViewItemHierarchy<TData> Build(TData data, ILotusViewItemHierarchy parent, ILotusCollectionViewHierarchy owner)
			{
				var node_root_view = new ViewItemHierarchy<TData>();
				node_root_view.DataContext = data;
				node_root_view.IParent = parent;
				node_root_view.IOwner = owner;
				if (parent != null)
				{
					node_root_view.Level = parent.Level + 1;
					parent.IViewItems.Add(node_root_view);
					node_root_view.IOwner = owner;
					if (data is ILotusViewItemOwner view_item_owner)
					{
						view_item_owner.OwnerViewItem = node_root_view;
					}
				}

				// 1) Проверяем в порядке приоритета
				// Если есть поддержка интерфеса для построения используем его
				if (data is ILotusViewItemBuilder view_builder)
				{
					var count_child = view_builder.GetCountChildrenNode();
					for (var i = 0; i < count_child; i++)
					{
						var node_data = (TData)view_builder.GetChildrenNode(i);
						if (node_data != null)
						{
							Build(node_data, node_root_view, owner);
						}
					}
				}
				else
				{
					// 2) Проверяем на обобщенный список
					if (data is IList list)
					{
						var count_child = list.Count;
						for (var i = 0; i < count_child; i++)
						{
							if (list[i] is TData node_data)
							{
								Build(node_data, node_root_view, owner);
							}
						}
					}
					else
					{
						// 3) Проверяем на обобщенное перечисление
						if (data is IEnumerable enumerable)
						{
							foreach (var item in enumerable)
							{
								if (item is TData node_data)
								{
									Build(node_data, node_root_view, owner);
								}
							}
						}
					}
				}

				return node_root_view;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов отображения
			/// </summary>
			/// <param name="root">Данные корневого узла</param>
			/// <param name="filter">Предикат фильтрации</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ViewItemHierarchy<TData> BuildFilter(TData root, Predicate<TData> filter, ILotusCollectionViewHierarchy owner)
			{
				ViewItemHierarchy<TData> node_root_view = BuildFilter(root, null, filter, owner);
				return node_root_view;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов отображения
			/// </summary>
			/// <param name="data">Данные элемента отображени</param>
			/// <param name="parent">Родительский элемент отображения</param>
			/// <param name="filter">Предикат фильтрации</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ViewItemHierarchy<TData> BuildFilter(TData data, ILotusViewItemHierarchy parent,
				Predicate<TData> filter, ILotusCollectionViewHierarchy owner)
			{
				var node_root_view = new ViewItemHierarchy<TData>();
				node_root_view.DataContext = data;
				node_root_view.IParent = parent;
				node_root_view.IOwner = owner;
				if (data is ILotusCheckOne<TData> check)
				{
					if (check.CheckOne(filter))
					{
						if (parent != null)
						{
							node_root_view.Level = parent.Level + 1;
							parent.IViewItems.Add(node_root_view);
							node_root_view.IOwner = owner;
							if (data is ILotusViewItemOwner view_item_owner)
							{
								view_item_owner.OwnerViewItem = node_root_view;
							}
						}

						// 1) Проверяем в порядке приоритета
						// Если есть поддержка интерфеса для построения используем его
						if (data is ILotusViewItemBuilder view_builder)
						{
							var count_child = view_builder.GetCountChildrenNode();
							for (var i = 0; i < count_child; i++)
							{
								var node_data = (TData)view_builder.GetChildrenNode(i);
								if (node_data != null)
								{
									BuildFilter(node_data, node_root_view, filter, owner);
								}
							}
						}
						else
						{
							// 2) Проверяем на обобщенный список
							if (data is IList list)
							{
								var count_child = list.Count;
								for (var i = 0; i < count_child; i++)
								{
									if (list[i] is TData node_data)
									{
										BuildFilter(node_data, node_root_view, filter, owner);
									}
								}
							}
							else
							{
								// 3) Проверяем на обобщенное перечисление
								if (data is IEnumerable enumerable)
								{
									foreach (var item in enumerable)
									{
										if (item is TData node_data)
										{
											BuildFilter(node_data, node_root_view, filter, owner);
										}
									}
								}
							}
						}
					}
				}

				return node_root_view;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mName;
			protected internal ILotusCollectionViewHierarchy mOwner;
			protected internal ILotusViewItemHierarchy mParent;
			protected internal TData mDataContext;
			protected internal Int32 mLevel;
			protected internal Boolean mIsExpanded;
			protected internal Boolean mIsEnabled;
			protected internal Boolean mIsSelected;
			protected internal Boolean? mIsChecked = false;
			protected internal Boolean mIsPresented;
			protected internal Boolean mIsEditMode;

			// Элементы интерфейса
			protected internal CUIContextMenu mUIContextMenu;
			protected internal System.Object mUIElement;

			// Группирование
			protected internal Boolean mIsGroupProperty;
			protected internal String mGroupPropertyName;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Наименование элемента отображения
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
			/// Владелец объекта
			/// </summary>
			public ILotusOwnerObject IOwner
			{
				get { return mOwner; }
				set { mOwner = value as ILotusCollectionViewHierarchy; }
			}

			/// <summary>
			/// Данные
			/// </summary>
			/// <remarks>
			/// Ссылка на данные которые связаны с данным элементом отображения
			/// </remarks>
			Object ILotusViewItem.DataContext
			{
				get { return mDataContext; }
				set
				{
					if (mDataContext != null && mDataContext != value)
					{
						UnsetDataContext();
					}

					mDataContext = (TData)value;
					NotifyPropertyChanged(PropertyArgsDataContext);
					SetDataContext();
					RaiseDataContextChanged();
				}
			}

			/// <summary>
			/// Данные
			/// </summary>
			/// <remarks>
			/// Ссылка на данные которые связаны с данным элементом отображения
			/// </remarks>
			public TData DataContext
			{
				get { return mDataContext; }
				set
				{
					if (mDataContext != null && mDataContext != value)
					{
						UnsetDataContext();
					}

					mDataContext = value;
					NotifyPropertyChanged(PropertyArgsDataContext);
					SetDataContext();
					RaiseDataContextChanged();
				}
			}

			/// <summary>
			/// Уровень вложенности элемента отображения
			/// </summary>
			/// <remarks>
			/// Корневые элементы отображения имеют уровень 0
			/// </remarks>
			public Int32 Level
			{
				get { return mLevel; }
				set
				{
					mLevel = value;
				}
			}

			/// <summary>
			/// Статус корневого элемента отображения
			/// </summary>
			public Boolean IsRoot
			{
				get { return mParent == null; }
			}

			/// <summary>
			/// Статус элемента отображения который не имеет дочерних элементов отображения
			/// </summary>
			public Boolean IsLeaf
			{
				get { return Count == 0; }
			}

			/// <summary>
			/// Статус раскрытия элемента отображения
			/// </summary>
			public virtual Boolean IsExpanded
			{
				get { return mIsExpanded; }
				set
				{
					if (mIsExpanded != value)
					{
						if (mDataContext is ILotusViewExpanded view_expaneded)
						{
							view_expaneded.SetViewExpanded(this, value);
						}

						mIsExpanded = value;
						NotifyPropertyChanged(PropertyArgsIsExpanded);
					}
				}
			}

			/// <summary>
			/// Выбор элемента отображения
			/// </summary>
			public Boolean IsSelected
			{
				get { return mIsSelected; }
				set
				{
					if (mIsSelected != value)
					{
						if (mDataContext is ILotusViewSelected view_selected)
						{
							if (view_selected.CanViewSelected(this))
							{
								mIsSelected = value;
								view_selected.SetViewSelected(this, value);
								NotifyPropertyChanged(PropertyArgsIsSelected);
								RaiseIsSelectedChanged();
							}
						}
						else
						{
							mIsSelected = value;
							NotifyPropertyChanged(PropertyArgsIsSelected);
							RaiseIsSelectedChanged();
						}
					}
				}
			}

			/// <summary>
			/// Доступность элемента отображения
			/// </summary>
			public Boolean IsEnabled
			{
				get { return mIsEnabled; }
				set
				{
					if (mIsEnabled != value)
					{
						if (mDataContext is ILotusViewEnabled view_enabled)
						{
							mIsEnabled = value;
							view_enabled.SetViewEnabled(this, value);
							NotifyPropertyChanged(PropertyArgsIsEnabled);
							RaiseIsEnabledChanged();
						}
						else
						{
							mIsEnabled = value;
							NotifyPropertyChanged(PropertyArgsIsEnabled);
							RaiseIsEnabledChanged();
						}
					}
				}
			}

			/// <summary>
			/// Выбор элемента элемента отображения
			/// </summary>
			public Boolean? IsChecked
			{
				get { return mIsChecked; }
				set
				{
					if (mIsChecked != value)
					{
						mIsChecked = value;
						NotifyPropertyChanged(PropertyArgsIsChecked);
						RaiseIsCheckedChanged();

						if(mIsChecked.HasValue)
						{
							for (var i = 0; i < mCount; i++)
							{
								mArrayOfItems[i].IsChecked = value;
							}
						}
					}
				}
			}

			/// <summary>
			/// Отображение элемента отображения в отдельном контексте
			/// </summary>
			public Boolean IsPresented
			{
				get { return mIsPresented; }
				set
				{
					if (mIsPresented != value)
					{
						if (mDataContext is ILotusViewPresented view_presented)
						{
							mIsPresented = value;
							view_presented.SetViewPresented(this, value);
							NotifyPropertyChanged(PropertyArgsIsPresented);
							RaiseIsPresentedChanged();
						}
						else
						{
							mIsPresented = value;
							NotifyPropertyChanged(PropertyArgsIsPresented);
							RaiseIsPresentedChanged();
						}
					}
				}
			}

			/// <summary>
			/// Статус элемента находящегося в режиме редактирования
			/// </summary>
			public Boolean IsEditMode
			{
				get { return mIsEditMode; }
				set
				{
					if (mIsEditMode != value)
					{
						mIsEditMode = value;
						NotifyPropertyChanged(PropertyArgsIsEditMode);
					}
				}
			}

			/// <summary>
			/// Список дочерних элементов отображения
			/// </summary>
			public IList IViewItems
			{
				get { return this; }
			}

			/// <summary>
			/// Количество дочерних элементов отображения
			/// </summary>
			public Int32 CountViewItems
			{
				get { return mCount; }
			}

			/// <summary>
			/// Родительский элемент отображения
			/// </summary>
			public ILotusViewItemHierarchy IParent
			{
				get { return mParent; }
				set { mParent = value; }
			}

			/// <summary>
			/// Элемент пользовательского интерфейса который непосредственно представляет данный элемент отображения
			/// </summary>
			public System.Object UIElement
			{
				get { return mUIElement; }
				set
				{
					mUIElement = value;
				}
			}

			/// <summary>
			/// Контекстное меню
			/// </summary>
			[Browsable(false)]
			public CUIContextMenu UIContextMenu
			{
				get { return mUIContextMenu; }
			}

			/// <summary>
			/// Возможность перемещать элемент отображения в элементе пользовательского интерфейса
			/// </summary>
			[Browsable(false)]
			public virtual Boolean UIDraggableStatus
			{
				get { return false; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public ViewItemHierarchy()
				: this(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя элемента отображения</param>
			//---------------------------------------------------------------------------------------------------------
			public ViewItemHierarchy(String name)
			{
				mName = name;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="dataContext">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public ViewItemHierarchy(TData dataContext)
			{
				mDataContext = dataContext;
				SetDataContext();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="dataContext">Данные</param>
			/// <param name="parentItem">Родительский узел</param>
			//---------------------------------------------------------------------------------------------------------
			public ViewItemHierarchy(TData dataContext, ILotusViewItemHierarchy parentItem)
			{
				mParent = parentItem;
				mDataContext = dataContext;
				SetDataContext();
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Имя элемента отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return mName;
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение имени элемента отображения.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseNameChanged()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение данных элемента отображения.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseDataContextChanged()
			{
				if (mOwner != null)
				{
					mOwner.OnNotifyUpdated(this, DataContext, nameof(DataContext));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение выбора элемента отображения.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsSelectedChanged()
			{
				if (mOwner != null)
				{
					mOwner.OnNotifyUpdated(this, IsSelected, nameof(IsSelected));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение доступности элемента отображения.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsEnabledChanged()
			{
				if (mOwner != null)
				{
					mOwner.OnNotifyUpdated(this, IsEnabled, nameof(IsEnabled));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение выбора элемента отображения.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsCheckedChanged()
			{
				if (mOwner != null)
				{
					mOwner.OnNotifyUpdated(this, IsChecked, nameof(IsChecked));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса отображения элемента.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsPresentedChanged()
			{
				if (mOwner != null)
				{
					mOwner.OnNotifyUpdated(this, IsPresented, nameof(IsPresented));
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusOwnerObject ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Присоединение указанного зависимого объекта
			/// </summary>
			/// <param name="ownedObject">Объект</param>
			/// <param name="add">Статус добавления в коллекцию</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AttachOwnedObject(ILotusOwnedObject ownedObject, Boolean add)
			{
				// Присоединять можем только объекты
				if (ownedObject is ILotusViewItem view_item)
				{
					// Если владелец есть
					if (ownedObject.IOwner != null)
					{
						// И он не равен текущему
						if (ownedObject.IOwner != this)
						{
							// Отсоединяем
							ownedObject.IOwner.DetachOwnedObject(ownedObject, add);
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
			/// <param name="ownedObject">Объект</param>
			/// <param name="remove">Статус удаления из коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void DetachOwnedObject(ILotusOwnedObject ownedObject, Boolean remove)
			{
				// Отсоединять можем только объекты
				if (ownedObject is ILotusViewItem view_item)
				{
					ownedObject.IOwner = null;

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
					mArrayOfItems[i].IParent = this;
					mArrayOfItems[i].UpdateOwnedObjects();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование данного объекта о начале изменения данных указанного зависимого объекта
			/// </summary>
			/// <param name="ownedObject">Зависимый объект</param>
			/// <param name="data">Объект, данные которого будут меняться</param>
			/// <param name="dataName">Имя данных</param>
			/// <returns>Статус разрешения/согласования изменения данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean OnNotifyUpdating(ILotusOwnedObject ownedObject, System.Object data, String dataName)
			{
				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование данного объекта об окончании изменении данных указанного объекта
			/// </summary>
			/// <param name="ownedObject">Зависимый объект</param>
			/// <param name="data">Объект, данные которого изменились</param>
			/// <param name="dataName">Имя данных</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void OnNotifyUpdated(ILotusOwnedObject ownedObject, System.Object data, String dataName)
			{
				if (mOwner != null)
				{
					mOwner.OnNotifyUpdated(this, ownedObject, dataName);
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewItem =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Открытие контекстного меню
			/// </summary>
			/// <param name="contextMenu">Контекстное меню</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void OpenContextMenu(System.Object contextMenu)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание экземпляра элемента отображения
			/// </summary>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual ILotusViewItem CreateViewItem()
			{
				return new ViewItemHierarchy<TData>();
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewItemHierarchy ============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Раскрытие всего элемента отображения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Expanded()
			{
				IsExpanded = true;
				for (var i = 0; i < mCount; i++)
				{
					mArrayOfItems[i].Expanded();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сворачивание всего элемента отображения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Collapsed()
			{
				IsExpanded = false;
				for (var i = 0; i < mCount; i++)
				{
					mArrayOfItems[i].Collapsed();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение количество выделенных элементов отображения включая дочерние
			/// </summary>
			/// <returns>Количество выделенных элементов отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Int32 GetCountChecked()
			{
				return 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на поддержку элемента отображения
			/// </summary>
			/// <param name="viewItem">Элемент отображения</param>
			/// <returns>Статус поддрежки</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean IsSupportViewItem(ILotusViewItemHierarchy viewItem)
			{
				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение выбора всех элементов отображения кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnsetAllSelected(ILotusViewItemHierarchy exclude)
			{
				if (exclude != null)
				{
					for (var i = 0; i < mCount; i++)
					{
						if (Object.ReferenceEquals(mArrayOfItems[i], exclude) == false)
						{
							mArrayOfItems[i].IsSelected = false;
							mArrayOfItems[i].UnsetAllSelected(exclude);
						}
					}
				}
				else
				{
					for (var i = 0; i < mCount; i++)
					{
						mArrayOfItems[i].IsSelected = false;
						mArrayOfItems[i].UnsetAllSelected(exclude);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение презентации сех элементов отображения кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент отображения</param>
			/// <param name="parameters">Параметры контекста исключения</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnsetAllPresent(ILotusViewItemHierarchy exclude, CParameters parameters)
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
								mArrayOfItems[i].UnsetAllPresent(exclude, parameters);
							}
						}
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
							mArrayOfItems[i].UnsetAllPresent(exclude, parameters);
						}
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Посещение каждого элемента отображения с указанным предикатом
			/// </summary>
			/// <param name="match">Предикат</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Visit(Predicate<ILotusViewItemHierarchy> match)
			{
				if (match(this))
				{
					for (var i = 0; i < mCount; ++i)
					{
						mArrayOfItems[i].Visit(match);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание экземпляра иерархического элемента отображения
			/// </summary>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual ILotusViewItemHierarchy CreateViewItemHierarchy()
			{
				return new ViewItemHierarchy<TData>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Построение дочерней иерархии согласно источнику данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void BuildFromDataContext()
			{
				
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetDataContext()
			{
				if (mDataContext is ILotusNameable nameable)
				{
					mName = nameable.Name;
				}
				else
				{
					if (mDataContext != null)
					{
						mName = mDataContext.ToString();
					}
				}

				if (mDataContext is ILotusViewItemOwner view_item_owner)
				{
					view_item_owner.OwnerViewItem = this;
				}

				if(mDataContext is INotifyCollectionChanged collection_changed)
				{
					collection_changed.CollectionChanged += OnCollectionChangedHandler;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnsetDataContext()
			{
				if (mDataContext is INotifyCollectionChanged collection_changed)
				{
					collection_changed.CollectionChanged -= OnCollectionChangedHandler;
				}
			}
			#endregion

			#region ======================================= ОБРАБОТЧИКИ СОБЫТИЙ =======================================
			//-------------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение коллекции
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//-------------------------------------------------------------------------------------------------------------
			private void OnCollectionChangedHandler(Object sender, NotifyCollectionChangedEventArgs args)
			{
				switch (args.Action)
				{
					case NotifyCollectionChangedAction.Add:
						{
							IList new_objects = args.NewItems;
							if(new_objects != null && new_objects.Count > 0)
							{
								for (var i = 0; i < new_objects.Count; i++)
								{
									// Проверяем на дубликаты
									var is_dublicate = false;
									for (var j = 0; j < Count; j++)
									{
										if(mArrayOfItems[j].DataContext == new_objects[i])
										{
											is_dublicate = true;
											break;
										}
									}

									if (is_dublicate == false)
									{
										ILotusViewItemHierarchy view_item = CreateViewItemHierarchy();
										view_item.IOwner = this.IOwner;
										view_item.IParent = this;

										var data = (TData)new_objects[i];
										view_item.DataContext = data;

										if (data is ILotusViewItemOwner view_item_owner)
										{
											view_item_owner.OwnerViewItem = view_item;
										}

										this.Add(view_item);
									}
								}
							}
						}
						break;
					case NotifyCollectionChangedAction.Remove:
						{
							IList old_items = args.OldItems;
							if(old_items != null && old_items.Count > 0)
							{
								for (var i = 0; i < old_items.Count; i++)
								{
									var data_context = (TData)old_items[i];

									// Находим элемент с данным контекстом
									ILotusViewItemHierarchy view_item = this.Search((item) =>
									{
										if(Object.ReferenceEquals(item.DataContext, data_context))
										{
											return true;
										}
										else
										{
											return false;
										}
									});

									if(view_item != null)
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
					case NotifyCollectionChangedAction.Move:
						{
							var old_index = args.OldStartingIndex;
							var new_index = args.NewStartingIndex;
							Move(old_index, new_index);
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
		/// Класс реализующий минимальный элемент отображения который поддерживает иерархические отношения и 
		/// реализует основные параметры просмотра и управления
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CViewItemHierarchyObject : ViewItemHierarchy<System.Object>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CViewItemHierarchyObject()
				: this(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя модели</param>
			//---------------------------------------------------------------------------------------------------------
			public CViewItemHierarchyObject(String name)
				: base(name)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="dataContext">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CViewItemHierarchyObject(System.Object dataContext)
				: base(dataContext)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="dataContext">Данные</param>
			/// <param name="parentItem">Родительский узел</param>
			//---------------------------------------------------------------------------------------------------------
			public CViewItemHierarchyObject(System.Object dataContext, ILotusViewItemHierarchy parentItem)
				: base(dataContext, parentItem)
			{
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewItem =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание экземпляра элемента отображения
			/// </summary>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public override ILotusViewItem CreateViewItem()
			{
				return new CViewItemHierarchyObject();
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewItemHierarchy ============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание экземпляра иерархического элемента отображения
			/// </summary>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public override ILotusViewItemHierarchy CreateViewItemHierarchy()
			{
				return new CViewItemHierarchyObject();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Построение дочерней иерархии согласно источнику данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void BuildFromDataContext()
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