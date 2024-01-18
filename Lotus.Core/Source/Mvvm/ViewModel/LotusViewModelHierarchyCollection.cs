//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема ViewModel
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusViewModelHierarchyCollection.cs
*		Тип коллекции для хранения элементов ViewModel для иерархических данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
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
		/// Интерфейс для определения коллекции элементов ViewModel для иерархических данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusCollectionViewModelHierarchy : ILotusCollectionViewModel
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание конкретной ViewModel для указанной модели
			/// </summary>
			/// <param name="model">Модель</param>
			/// <param name="parent">Родительский элемент ViewModel</param>
			/// <returns>ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			ILotusViewModelHierarchy CreateViewModelHierarchy(System.Object model, ILotusViewModelHierarchy? parent);
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Шаблон коллекции для элементов ViewModel которая поддерживает концепцию просмотра и управления с полноценной 
		/// поддержкой всех операций
		/// </summary>
		/// <remarks>
		/// Данная коллекции позволяет управлять видимостью данных, обеспечивает их сортировку, группировку, фильтрацию, 
		/// позволяет выбирать данные и производить над ними операции
		/// </remarks>
		/// <typeparam name="TViewModelHierarchy">Тип элемента ViewModel</typeparam>
		/// <typeparam name="TModel">Тип модели</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class CollectionViewModelHierarchy<TViewModelHierarchy, TModel> : CollectionViewModel<TViewModelHierarchy, TModel>,
			ILotusCollectionViewModelHierarchy
			where TViewModelHierarchy : ILotusViewModelHierarchy
			where TModel : class
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов ViewModel
			/// </summary>
			/// <param name="rootModel">Корневая модель</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TViewModelHierarchy Build(TModel rootModel, ILotusCollectionViewModelHierarchy owner)
			{
				TViewModelHierarchy node_root_view = Build(rootModel, null, owner);
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
			public static TViewModelHierarchy Build(TModel model, ILotusViewModelHierarchy? parent, ILotusCollectionViewModelHierarchy owner)
			{
				var node_view = owner.CreateViewModelHierarchy(model, parent);
				node_view.IParent = parent;
				node_view.IOwner = owner;

				if (model is ILotusViewModelOwner view_item_owner)
				{
					view_item_owner.OwnerViewModel = node_view;
				}

				if (parent != null)
				{
					node_view.Level = parent.Level + 1;
					parent.IViewModels.Add(node_view);
					node_view.IOwner = owner;
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
							Build(node_data, node_view, owner);
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
								Build(node_data, node_view, owner);
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
									Build(node_data, node_view, owner);
								}
							}
						}
					}
				}

				return (TViewModelHierarchy)node_view;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов ViewModel
			/// </summary>
			/// <param name="parent">Родительский элемент ViewModel</param>
			/// <param name="owner">Коллекция владелец</param>
			//---------------------------------------------------------------------------------------------------------
			public static void BuildFromParent(ILotusViewModelHierarchy? parent, ILotusCollectionViewModelHierarchy owner)
			{
				if (parent != null)
				{
					// Получаем данные
					var data = parent.Model;

					// 1) Проверяем в порядке приоритета
					// Если есть поддержка интерфеса для построения используем его
					if (data is ILotusViewModelBuilder view_builder)
					{
						var count_child = view_builder.GetCountChildrenNode();
						for (var i = 0; i < count_child; i++)
						{
							var node_data = (TModel)view_builder.GetChildrenNode(i);
							if (node_data != null)
							{
								Build(node_data, parent, owner);
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
								if (list[i] is TModel node_data)
								{
									Build(node_data, parent, owner);
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
									if (item is TModel node_data)
									{
										Build(node_data, parent, owner);
									}
								}
							}
						}
					}
				}
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
			public static TViewModelHierarchy BuildFilter(TModel rootModel, Predicate<TModel?> filter, ILotusCollectionViewModelHierarchy owner)
			{
				TViewModelHierarchy node_root_view = BuildFilter(rootModel, null, filter, owner);
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
			public static TViewModelHierarchy BuildFilter(TModel model, ILotusViewModelHierarchy? parent,
				Predicate<TModel?> filter, ILotusCollectionViewModelHierarchy owner)
			{
				var node_root_view = owner.CreateViewModelHierarchy(model, parent);
				node_root_view.IParent = parent;
				node_root_view.IOwner = owner;
				if (model is ILotusCheckOne<TModel> check)
				{
					if (check.CheckOne(filter))
					{
						if (model is ILotusViewModelOwner view_item_owner)
						{
							view_item_owner.OwnerViewModel = node_root_view;
						}

						if (parent != null)
						{
							node_root_view.Level = parent.Level + 1;
							parent.IViewModels.Add(node_root_view);
							node_root_view.IOwner = owner;
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

				return (TViewModelHierarchy)node_root_view;
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CollectionViewModelHierarchy()
				: this(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public CollectionViewModelHierarchy(String name)
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
			public CollectionViewModelHierarchy(String name, System.Object source)
				: base(name, source)
			{
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override System.Object Clone()
			{
				var clone = new CollectionViewModelHierarchy<TViewModelHierarchy, TModel>();
				clone.Name = _name;

				for (var i = 0; i < _count; i++)
				{
					clone.Add(_arrayOfItems.Clone());
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
				return _name;
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusCollectionViewModel ==========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание конкретной ViewModel для указанной модели
			/// </summary>
			/// <typeparam name="TModelData">Тип модели</typeparam>
			/// <param name="model">Модель</param>
			/// <returns>ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual ILotusViewModel<TModelData> CreateViewModel<TModelData>(TModelData model)
			{
                throw new NotImplementedException("CreateViewModel must be implemented");
            }

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка источника данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void ResetSource()
			{
				Clear();

				if (Source is TModel data)
				{
					if (_isFiltered == false)
					{
						TViewModelHierarchy view_item_hierarchy = Build(data, this);
						Add(view_item_hierarchy);
					}
					else
					{
						if (_filter != null)
						{
							TViewModelHierarchy view_item_hierarchy = BuildFilter(data, _filter, this);
							Add(view_item_hierarchy);
						}
					}
				}
				else
				{
					if (Source is IList<TModel> list_data)
					{
						for (var i = 0; i < list_data.Count; i++)
						{
							TViewModelHierarchy view_item_hierarchy = Build(list_data[i], this);
							Add(view_item_hierarchy);
						}
					}
				}

				NotifyCollectionReset();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение выбора всех элементов ViewModel кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент ViewModel</param>
			//---------------------------------------------------------------------------------------------------------
			public override void UnsetAllSelected(ILotusViewModel exclude)
			{
				if (exclude != null)
				{
					for (var i = 0; i < _count; i++)
					{
						if (Object.ReferenceEquals(_arrayOfItems[i], exclude) == false)
						{
							_arrayOfItems[i]!.IsSelected = false;
							_arrayOfItems[i]!.UnsetAllSelected(exclude as ILotusViewModelHierarchy);
						}
					}

					SelectedViewModel = (TViewModelHierarchy)exclude;
				}
				else
				{
					for (var i = 0; i < _count; i++)
					{
						_arrayOfItems[i]!.IsSelected = false;
						_arrayOfItems[i]!.UnsetAllSelected(exclude as ILotusViewModelHierarchy);
					}

					SelectedViewModel = default;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выключение презентации сех элементов ViewModel кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент ViewModel</param>
			/// <param name="parameters">Параметры контекста исключения</param>
			//---------------------------------------------------------------------------------------------------------
			public override void UnsetAllPresent(ILotusViewModel exclude, CParameters parameters)
			{
				if (exclude != null)
				{
					for (var i = 0; i < _count; i++)
					{
						if (Object.ReferenceEquals(_arrayOfItems[i], exclude) == false)
						{
							_arrayOfItems[i]!.IsPresented = false;
							_arrayOfItems[i]!.UnsetAllPresent(exclude as ILotusViewModelHierarchy, parameters);
						}
					}

					PresentedViewModel = (TViewModelHierarchy)exclude;
				}
				else
				{
					// Выключаем все элемента ViewModel
					for (var i = 0; i < _count; i++)
					{
						_arrayOfItems[i]!.IsPresented = false;
						_arrayOfItems[i]!.UnsetAllPresent(exclude as ILotusViewModelHierarchy, parameters);
					}

					PresentedViewModel = default;
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusCollectionViewModelHierarchy =================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание конкретной ViewModel для указанной модели
			/// </summary>
			/// <param name="model">Модель</param>
			/// <param name="parent">Родительская модель</param>
			/// <returns>ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual ILotusViewModelHierarchy CreateViewModelHierarchy(System.Object model,
				ILotusViewModelHierarchy? parent)
			{
                throw new NotImplementedException("Model must be type <JObject>");
            }

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Раскрытие всего элемента ViewModel
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Expanded()
			{
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
			/// Посещение каждого элемента ViewModel с указанным предикатом
			/// </summary>
			/// <param name="match">Предикат</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Visit(Predicate<ILotusViewModelHierarchy> match)
			{
				for (var i = 0; i < _count; ++i)
				{
					_arrayOfItems[i]!.Visit(match);
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusOwnerObject ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление связей для зависимых объектов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void UpdateOwnedObjects()
			{
				for (var i = 0; i < _count; i++)
				{
					_arrayOfItems[i]!.IOwner = this;
					_arrayOfItems[i]!.UpdateOwnedObjects();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование данного объекта о начале изменения данных указанного объекта
			/// </summary>
			/// <param name="ownedObject">Зависимый объект</param>
			/// <param name="data">Объект данные которого будут меняться</param>
			/// <param name="dataName">Имя данных</param>
			/// <returns>Статус разрешения/согласования изменения данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean OnNotifyUpdating(ILotusOwnedObject ownedObject, System.Object? data, String dataName)
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
			public override void OnNotifyUpdated(ILotusOwnedObject ownedObject, System.Object? data, String dataName)
			{
				base.OnNotifyUpdated(ownedObject, data, dataName);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
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
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ТЕКУЩИМ ЭЛЕМЕНТОМ =========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Дублирование текущего элемента и добавление элемента в список элементов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void DublicateSelectedItem()
			{
				if (_selectedIndex != -1)
				{
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление текущего элемента из списка (удаляется объект)
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void DeleteSelectedItem()
			{
				if (SelectedViewModel != null)
				{
					RemoveAt(_selectedIndex);
					SelectedIndex = -1;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение текущего элемента назад
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void MoveSelectedBackward()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение текущего элемента вперед
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void MoveSelectedForward()
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