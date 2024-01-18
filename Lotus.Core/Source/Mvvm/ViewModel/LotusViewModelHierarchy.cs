//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема ViewModel
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusViewModelHierarchy.cs
*		Определение элемента ViewModel который поддерживает иерархические отношения.
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
		/** \addtogroup CoreViewModel
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейса элемента ViewModel который поддерживает иерархические отношения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusViewModelHierarchy : ILotusViewModel, ILotusOwnerObject
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Уровень вложенности элемента ViewModel
			/// </summary>
			/// <remarks>
			/// Корневые элементы отображения имеют уровень 0
			/// </remarks>
			Int32 Level { get; set; }

			/// <summary>
			/// Статус корневого элемента ViewModel
			/// </summary>
			Boolean IsRoot { get; }

			/// <summary>
			/// Статус элемента ViewModel который не имеет дочерних элементов ViewModel
			/// </summary>
			Boolean IsLeaf { get; }

			/// <summary>
			/// Статус раскрытия элемента ViewModel
			/// </summary>
			Boolean IsExpanded { get; set; }

			/// <summary>
			/// Список дочерних элементов ViewModel
			/// </summary>
			IList IViewModels { get; }

			/// <summary>
			/// Количество дочерних элементов ViewModel
			/// </summary>
			Int32 CountViewModels { get; }

			/// <summary>
			/// Родительский элемент ViewModel
			/// </summary>
			ILotusViewModelHierarchy? IParent { get; set; }
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание конкретной ViewModel для указанной модели
			/// </summary>
			/// <param name="model">Модель</param>
			/// <param name="parent">Родительский элемент ViewModel</param>
			/// <returns>ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			ILotusViewModelHierarchy CreateViewModelHierarchy(System.Object model, ILotusViewModelHierarchy parent);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Раскрытие всего элемента ViewModel
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void Expanded();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сворачивание всего элемента ViewModel
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void Collapsed();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение количество выделенных элементов ViewModel включая дочерние
			/// </summary>
			/// <returns>Количество выделенных элементов ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			Int32 GetCountChecked();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на поддержку элемента ViewModel
			/// </summary>
			/// <param name="viewModel">Элемент ViewModel</param>
			/// <returns>Статус поддрежки</returns>
			//---------------------------------------------------------------------------------------------------------
			Boolean IsSupportViewModel(ILotusViewModelHierarchy viewModel);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение выбора всех элементов ViewModel кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент ViewModel</param>
			//---------------------------------------------------------------------------------------------------------
			void UnsetAllSelected(ILotusViewModelHierarchy? exclude);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение презентации всех элементов ViewModel кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент ViewModel</param>
			/// <param name="parameters">Параметры контекста исключения</param>
			//---------------------------------------------------------------------------------------------------------
			void UnsetAllPresent(ILotusViewModelHierarchy? exclude, CParameters parameters);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Посещение каждого элемента ViewModel с указанным предикатом
			/// </summary>
			/// <param name="match">Предикат</param>
			//---------------------------------------------------------------------------------------------------------
			void Visit(Predicate<ILotusViewModelHierarchy> match);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Построение дочерней иерархии согласно источнику данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void BuildFromModel();
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Шаблон реализующий минимальный элемент ViewModel который поддерживает иерархические отношения и реализует 
		/// основные параметры просмотра и управления
		/// </summary>
		/// <typeparam name="TModel">Тип модели</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class ViewModelHierarchy<TModel> : ListArray<ViewModelHierarchy<TModel>>, ILotusViewModelHierarchy
			where TModel : class
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsName = new PropertyChangedEventArgs(nameof(Name));
			protected static readonly PropertyChangedEventArgs PropertyArgsDataContext = new PropertyChangedEventArgs(nameof(Model));
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
			/// Рекурсивное создание элементов ViewModel
			/// </summary>
			/// <param name="rootModel">Корневая модель</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ViewModelHierarchy<TModel> Build(TModel rootModel, ILotusCollectionViewModelHierarchy owner)
			{
				ViewModelHierarchy<TModel> node_root_view = Build(rootModel, null, owner);
				return node_root_view;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов ViewModel
			/// </summary>
			/// <param name="model">Модель</param>
			/// <param name="parent">Родительский элемент ViewModel</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ViewModelHierarchy<TModel> Build(TModel model, ILotusViewModelHierarchy? parent, ILotusCollectionViewModelHierarchy owner)
			{
				var node_root_view = new ViewModelHierarchy<TModel>(model, parent);
				node_root_view.IOwner = owner;
				if (parent != null)
				{
					node_root_view.Level = parent.Level + 1;
					parent.IViewModels.Add(node_root_view);
					node_root_view.IOwner = owner;
					if (model is ILotusViewModelOwner view_item_owner)
					{
						view_item_owner.OwnerViewModel = node_root_view;
					}
				}

				// 1) Проверяем в порядке приоритета
				// Если есть поддержка интерфеса для построения используем его
				if (model is ILotusViewModelBuilder view_builder)
				{
					var count_child = view_builder.GetCountChildrenNode();
					for (var i = 0; i < count_child; i++)
					{
						var node_data = (TModel)view_builder.GetChildrenNode(i);
						if (node_data != null)
						{
							Build(node_data, node_root_view, owner);
						}
					}
				}
				else
				{
					// 2) Проверяем на обобщенный список
					if (model is IList list)
					{
						var count_child = list.Count;
						for (var i = 0; i < count_child; i++)
						{
							if (list[i] is TModel node_data)
							{
								Build(node_data, node_root_view, owner);
							}
						}
					}
					else
					{
						// 3) Проверяем на обобщенное перечисление
						if (model is IEnumerable enumerable)
						{
							foreach (var item in enumerable)
							{
								if (item is TModel node_data)
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
			/// Рекурсивное создание элементов ViewModel
			/// </summary>
			/// <param name="rootModel">Корневая модель</param>
			/// <param name="filter">Предикат фильтрации</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ViewModelHierarchy<TModel> BuildFilter(TModel rootModel, Predicate<TModel?> filter, ILotusCollectionViewModelHierarchy owner)
			{
				ViewModelHierarchy<TModel> node_root_view = BuildFilter(rootModel, null, filter, owner);
				return node_root_view;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов ViewModel
			/// </summary>
			/// <param name="model">Модель</param>
			/// <param name="parent">Родительский элемент ViewModel</param>
			/// <param name="filter">Предикат фильтрации</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ViewModelHierarchy<TModel> BuildFilter(TModel model, ILotusViewModelHierarchy? parent,
				Predicate<TModel?> filter, ILotusCollectionViewModelHierarchy owner)
			{
				var node_root_view = new ViewModelHierarchy<TModel>(model, parent);
				node_root_view.IOwner = owner;
				if (model is ILotusCheckOne<TModel> check)
				{
					if (check.CheckOne(filter))
					{
						if (parent != null)
						{
							node_root_view.Level = parent.Level + 1;
							parent.IViewModels.Add(node_root_view);
							node_root_view.IOwner = owner;
							if (model is ILotusViewModelOwner view_item_owner)
							{
								view_item_owner.OwnerViewModel = node_root_view;
							}
						}

						// 1) Проверяем в порядке приоритета
						// Если есть поддержка интерфеса для построения используем его
						if (model is ILotusViewModelBuilder view_builder)
						{
							var count_child = view_builder.GetCountChildrenNode();
							for (var i = 0; i < count_child; i++)
							{
								var node_data = (TModel)view_builder.GetChildrenNode(i);
								if (node_data != null)
								{
									BuildFilter(node_data, node_root_view, filter, owner);
								}
							}
						}
						else
						{
							// 2) Проверяем на обобщенный список
							if (model is IList list)
							{
								var count_child = list.Count;
								for (var i = 0; i < count_child; i++)
								{
									if (list[i] is TModel node_data)
									{
										BuildFilter(node_data, node_root_view, filter, owner);
									}
								}
							}
							else
							{
								// 3) Проверяем на обобщенное перечисление
								if (model is IEnumerable enumerable)
								{
									foreach (var item in enumerable)
									{
										if (item is TModel node_data)
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
			protected internal String _name;
			protected internal ILotusCollectionViewModelHierarchy? _owner;
			protected internal ILotusViewModelHierarchy? _parent;
			protected internal TModel _model;
			protected internal Int32 _level;
			protected internal Boolean _isExpanded;
			protected internal Boolean _isEnabled;
			protected internal Boolean _isSelected;
			protected internal Boolean? _isChecked = false;
			protected internal Boolean _isPresented;
			protected internal Boolean _isEditMode;

			// Элементы интерфейса
			protected internal CUIContextMenu _contextMenuUI;
			protected internal System.Object _elementUI;

			// Группирование
			protected internal Boolean _isGroupProperty;
			protected internal String _groupPropertyName;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Наименование элемента ViewModel
			/// </summary>
			public virtual String Name
			{
				get { return _name; }
				set
				{
					_name = value;
					NotifyPropertyChanged(PropertyArgsName);
					RaiseNameChanged();
				}
			}

			/// <summary>
			/// Владелец объекта
			/// </summary>
			public ILotusOwnerObject? IOwner
			{
				get { return _owner; }
				set { _owner = value as ILotusCollectionViewModelHierarchy; }
			}

			/// <summary>
			/// Модель
			/// </summary>
			/// <remarks>
			/// Ссылка на данные которые связаны с данным элементом ViewModel
			/// </remarks>
			Object ILotusViewModel.Model
			{
				get { return _model; }
			}

			/// <summary>
			/// Модель
			/// </summary>
			/// <remarks>
			/// Ссылка на данные которые связаны с данным элементом ViewModel
			/// </remarks>
			public TModel Model
			{
				get { return _model; }
			}

			/// <summary>
			/// Уровень вложенности элемента ViewModel
			/// </summary>
			/// <remarks>
			/// Корневые элементы отображения имеют уровень 0
			/// </remarks>
			public Int32 Level
			{
				get { return _level; }
				set
				{
					_level = value;
				}
			}

			/// <summary>
			/// Статус корневого элемента ViewModel
			/// </summary>
			public Boolean IsRoot
			{
				get { return _parent == null; }
			}

			/// <summary>
			/// Статус элемента ViewModel который не имеет дочерних элементов ViewModel
			/// </summary>
			public Boolean IsLeaf
			{
				get { return Count == 0; }
			}

			/// <summary>
			/// Статус раскрытия элемента ViewModel
			/// </summary>
			public virtual Boolean IsExpanded
			{
				get { return _isExpanded; }
				set
				{
					if (_isExpanded != value)
					{
						if (_model is ILotusModelExpanded view_expaneded)
						{
							view_expaneded.SetModelExpanded(this, value);
						}

						_isExpanded = value;
						NotifyPropertyChanged(PropertyArgsIsExpanded);
					}
				}
			}

			/// <summary>
			/// Выбор элемента ViewModel
			/// </summary>
			public Boolean IsSelected
			{
				get { return _isSelected; }
				set
				{
					if (_isSelected != value)
					{
						if (_model is ILotusModelSelected view_selected)
						{
							if (view_selected.CanModelSelected(this))
							{
								_isSelected = value;
								view_selected.SetModelSelected(this, value);
								NotifyPropertyChanged(PropertyArgsIsSelected);
								RaiseIsSelectedChanged();
							}
						}
						else
						{
							_isSelected = value;
							NotifyPropertyChanged(PropertyArgsIsSelected);
							RaiseIsSelectedChanged();
						}
					}
				}
			}

			/// <summary>
			/// Доступность элемента ViewModel
			/// </summary>
			public Boolean IsEnabled
			{
				get { return _isEnabled; }
				set
				{
					if (_isEnabled != value)
					{
						if (_model is ILotusModelEnabled view_enabled)
						{
							_isEnabled = value;
							view_enabled.SetModelEnabled(this, value);
							NotifyPropertyChanged(PropertyArgsIsEnabled);
							RaiseIsEnabledChanged();
						}
						else
						{
							_isEnabled = value;
							NotifyPropertyChanged(PropertyArgsIsEnabled);
							RaiseIsEnabledChanged();
						}
					}
				}
			}

			/// <summary>
			/// Выбор элемента элемента ViewModel
			/// </summary>
			public Boolean? IsChecked
			{
				get { return _isChecked; }
				set
				{
					if (_isChecked != value)
					{
						_isChecked = value;
						NotifyPropertyChanged(PropertyArgsIsChecked);
						RaiseIsCheckedChanged();

						if (_isChecked.HasValue)
						{
							for (var i = 0; i < _count; i++)
							{
								_arrayOfItems[i]!.IsChecked = value;
							}
						}
					}
				}
			}

			/// <summary>
			/// Отображение элемента ViewModel в отдельном контексте
			/// </summary>
			public Boolean IsPresented
			{
				get { return _isPresented; }
				set
				{
					if (_isPresented != value)
					{
						if (_model is ILotusModelPresented view_presented)
						{
							_isPresented = value;
							view_presented.SetModelPresented(this, value);
							NotifyPropertyChanged(PropertyArgsIsPresented);
							RaiseIsPresentedChanged();
						}
						else
						{
							_isPresented = value;
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
				get { return _isEditMode; }
				set
				{
					if (_isEditMode != value)
					{
						_isEditMode = value;
						NotifyPropertyChanged(PropertyArgsIsEditMode);
					}
				}
			}

			/// <summary>
			/// Список дочерних элементов ViewModel
			/// </summary>
			public IList IViewModels
			{
				get { return this; }
			}

			/// <summary>
			/// Количество дочерних элементов ViewModel
			/// </summary>
			public Int32 CountViewModels
			{
				get { return _count; }
			}

			/// <summary>
			/// Родительский элемент ViewModel
			/// </summary>
			public ILotusViewModelHierarchy? IParent
			{
				get { return _parent; }
				set { _parent = value; }
			}

			/// <summary>
			/// Элемент пользовательского интерфейса который непосредственно представляет данный элемент ViewModel
			/// </summary>
			public System.Object UIElement
			{
				get { return _elementUI; }
				set
				{
					_elementUI = value;
				}
			}

			/// <summary>
			/// Контекстное меню
			/// </summary>
			public CUIContextMenu UIContextMenu
			{
				get { return _contextMenuUI; }
			}

			/// <summary>
			/// Возможность перемещать элемент ViewModel в элементе пользовательского интерфейса
			/// </summary>
			public virtual Boolean UIDraggableStatus
			{
				get { return false; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="model">Модель</param>
			/// <param name="parentItem">Родительский узел</param>
			/// <param name="name">Наименование элемента ViewModel</param>
			//---------------------------------------------------------------------------------------------------------
			public ViewModelHierarchy(TModel model, ILotusViewModelHierarchy? parentItem, String name = "")
			{
				_parent = parentItem;
				_model = model;
				_name = name;
				SetModel();
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Имя элемента ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return _name;
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение имени элемента ViewModel.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseNameChanged()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение выбора элемента ViewModel.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsSelectedChanged()
			{
				if (_owner != null)
				{
					_owner.OnNotifyUpdated(this, IsSelected, nameof(IsSelected));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение доступности элемента ViewModel.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsEnabledChanged()
			{
				if (_owner != null)
				{
					_owner.OnNotifyUpdated(this, IsEnabled, nameof(IsEnabled));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение выбора элемента ViewModel.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsCheckedChanged()
			{
				if (_owner != null)
				{
					_owner.OnNotifyUpdated(this, IsChecked, nameof(IsChecked));
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
				if (_owner != null)
				{
					_owner.OnNotifyUpdated(this, IsPresented, nameof(IsPresented));
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
				if (ownedObject is ILotusViewModel view_model)
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
						view_model.IOwner = this;
						Add(view_model);
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
				if (ownedObject is ILotusViewModel view_model)
				{
					ownedObject.IOwner = null;

					if (remove)
					{
						// Ищем его
						var index = IndexOf(view_model);
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
				for (var i = 0; i < _count; i++)
				{
					_arrayOfItems[i]!.IParent = this;
					_arrayOfItems[i]!.UpdateOwnedObjects();
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
			public virtual Boolean OnNotifyUpdating(ILotusOwnedObject ownedObject, System.Object? data, String dataName)
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
			public virtual void OnNotifyUpdated(ILotusOwnedObject ownedObject, System.Object? data, String dataName)
			{
				if (_owner != null)
				{
					_owner.OnNotifyUpdated(this, ownedObject, dataName);
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewModel ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Открытие контекстного меню
			/// </summary>
			/// <param name="contextMenu">Контекстное меню</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void OpenContextMenu(System.Object contextMenu)
			{

			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewModelHierarchy ===========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание конкретной ViewModel для указанной модели
			/// </summary>
			/// <param name="model">Модель</param>
			/// <param name="parent">Родительский элемент ViewModel</param>
			/// <returns>ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual ILotusViewModelHierarchy CreateViewModelHierarchy(System.Object model, ILotusViewModelHierarchy? parent)
			{
				throw new NotImplementedException("CreateViewModelHierarchy must be implemented");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Раскрытие всего элемента ViewModel
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Expanded()
			{
				IsExpanded = true;
				for (var i = 0; i < _count; i++)
				{
					_arrayOfItems[i]!.Expanded();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сворачивание всего элемента ViewModel
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Collapsed()
			{
				IsExpanded = false;
				for (var i = 0; i < _count; i++)
				{
					_arrayOfItems[i]!.Collapsed();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение количество выделенных элементов ViewModel включая дочерние
			/// </summary>
			/// <returns>Количество выделенных элементов ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Int32 GetCountChecked()
			{
				return 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на поддержку элемента ViewModel
			/// </summary>
			/// <param name="viewModel">Элемент ViewModel</param>
			/// <returns>Статус поддрежки</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean IsSupportViewModel(ILotusViewModelHierarchy viewModel)
			{
				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение выбора всех элементов ViewModel кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент ViewModel</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnsetAllSelected(ILotusViewModelHierarchy? exclude)
			{
				if (exclude != null)
				{
					for (var i = 0; i < _count; i++)
					{
						if (Object.ReferenceEquals(_arrayOfItems[i], exclude) == false)
						{
							_arrayOfItems[i]!.IsSelected = false;
							_arrayOfItems[i]!.UnsetAllSelected(exclude);
						}
					}
				}
				else
				{
					for (var i = 0; i < _count; i++)
					{
						_arrayOfItems[i]!.IsSelected = false;
						_arrayOfItems[i]!.UnsetAllSelected(exclude);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение презентации сех элементов ViewModel кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент ViewModel</param>
			/// <param name="parameters">Параметры контекста исключения</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnsetAllPresent(ILotusViewModelHierarchy? exclude, CParameters? parameters = null)
			{
				if (exclude != null)
				{
					if (parameters == null)
					{
						for (var i = 0; i < _count; i++)
						{
							if (Object.ReferenceEquals(_arrayOfItems[i], exclude) == false)
							{
								_arrayOfItems[i]!.IsPresented = false;
								_arrayOfItems[i]!.UnsetAllPresent(exclude, parameters);
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
						// Выключаем все элемента ViewModel
						for (var i = 0; i < _count; i++)
						{
							_arrayOfItems[i]!.IsPresented = false;
							_arrayOfItems[i]!.UnsetAllPresent(exclude, parameters);
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
			/// Посещение каждого элемента ViewModel с указанным предикатом
			/// </summary>
			/// <param name="match">Предикат</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Visit(Predicate<ILotusViewModelHierarchy> match)
			{
				if (match(this))
				{
					for (var i = 0; i < _count; ++i)
					{
						_arrayOfItems[i]!.Visit(match);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Построение дочерней иерархии согласно модели
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void BuildFromModel()
			{

			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка модели
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetModel()
			{
				if (String.IsNullOrEmpty(_name))
				{
					if (_model is ILotusNameable nameable)
					{
						_name = nameable.Name;
					}
					else
					{
						_name = _model.ToString()!;
					}
				}

				if (_model is ILotusViewModelOwner view_item_owner)
				{
					view_item_owner.OwnerViewModel = this;
				}

				if (_model is INotifyCollectionChanged collection_changed)
				{
					collection_changed.CollectionChanged += OnCollectionChangedHandler;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка модели
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnsetModel()
			{
				if (_model is INotifyCollectionChanged collection_changed)
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
			private void OnCollectionChangedHandler(System.Object? sender, NotifyCollectionChangedEventArgs args)
			{
				switch (args.Action)
				{
					case NotifyCollectionChangedAction.Add:
						{
							IList? new_models = args.NewItems;
							if (new_models != null && new_models.Count > 0)
							{
								for (var i = 0; i < new_models.Count; i++)
								{
									// Проверяем на дубликаты
									var is_dublicate = false;
									for (var j = 0; j < Count; j++)
									{
										if (_arrayOfItems[j]!.Model == new_models[i])
										{
											is_dublicate = true;
											break;
										}
									}

									if (is_dublicate == false)
									{
										var model = new_models[i] as TModel;
										ILotusViewModelHierarchy view_model = this.CreateViewModelHierarchy(model!, null);
										view_model.IOwner = this.IOwner;
										view_model.IParent = this;

										if (model is ILotusViewModelOwner view_item_owner)
										{
											view_item_owner.OwnerViewModel = view_model;
										}

										this.Add(view_model);
									}
								}
							}
						}
						break;
					case NotifyCollectionChangedAction.Remove:
						{
							IList? old_models = args.OldItems;
							if (old_models != null && old_models.Count > 0)
							{
								for (var i = 0; i < old_models.Count; i++)
								{
									var model = old_models[i] as TModel;

									// Находим элемент с данным контекстом
									ILotusViewModelHierarchy? view_model = this.Search((item) =>
									{
										if (Object.ReferenceEquals(item!.Model, model))
										{
											return true;
										}
										else
										{
											return false;
										}
									});

									if (view_model != null)
									{
										this.Remove(view_model);
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
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================