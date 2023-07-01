//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема отображения данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusViewItemHierarchyCollection.cs
*		Тип коллекции для хранения элементов отображения для иерархических данных.
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
		/** \addtogroup CoreViewItem
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для определения коллекции элементов отображения для иерархических данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusCollectionViewHierarchy : ILotusCollectionView
		{

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
		/// <typeparam name="TViewItemHierarchy">Тип элемента отображения</typeparam>
		/// <typeparam name="TData">Тип данных</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class CollectionViewHierarchy<TViewItemHierarchy, TData> : CollectionView<TViewItemHierarchy, TData>, ILotusCollectionViewHierarchy
			where TViewItemHierarchy : ILotusViewItemHierarchy, new()
			where TData : class
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов отображения
			/// </summary>
			/// <param name="root">Данные корневого узла</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TViewItemHierarchy Build(TData root, ILotusCollectionViewHierarchy owner)
			{
				TViewItemHierarchy node_root_view = Build(root, null, owner);
				return node_root_view;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов отображения
			/// </summary>
			/// <param name="data">Данные элемента отображения</param>
			/// <param name="parent">Родительский элемент отображения</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TViewItemHierarchy Build(TData data, ILotusViewItemHierarchy parent, ILotusCollectionViewHierarchy owner)
			{
				var node_view = new TViewItemHierarchy();
				node_view.DataContext = data;
				node_view.IParent = parent;
				node_view.IOwner = owner;

				if (data is ILotusViewItemOwner view_item_owner)
				{
					view_item_owner.OwnerViewItem = node_view;
				}

				if (parent != null)
				{
					node_view.Level = parent.Level + 1;
					parent.IViewItems.Add(node_view);
					node_view.IOwner = owner;
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
							Build(node_data, node_view, owner);
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
								Build(node_data, node_view, owner);
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
									Build(node_data, node_view, owner);
								}
							}
						}
					}
				}

				return node_view;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание элементов отображения
			/// </summary>
			/// <param name="parent">Родительский элемент отображения</param>
			/// <param name="owner">Коллекция владелец</param>
			//---------------------------------------------------------------------------------------------------------
			public static void BuildFromParent(ILotusViewItemHierarchy parent, ILotusCollectionViewHierarchy owner)
			{
				if(parent != null)
				{
					// Получаем данные
					var data = parent.DataContext;

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
								if (list[i] is TData node_data)
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
									if (item is TData node_data)
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
			/// Рекурсивное создание элементов отображения
			/// </summary>
			/// <param name="root">Данные корневого узла</param>
			/// <param name="filter">Предикат фильтрации</param>
			/// <param name="owner">Коллекция владелец</param>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TViewItemHierarchy BuildFilter(TData root, Predicate<TData> filter, ILotusCollectionViewHierarchy owner)
			{
				TViewItemHierarchy node_root_view = BuildFilter(root, null, filter, owner);
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
			public static TViewItemHierarchy BuildFilter(TData data, ILotusViewItemHierarchy parent, 
				Predicate<TData> filter, ILotusCollectionViewHierarchy owner)
			{
				var node_root_view = new TViewItemHierarchy();
				node_root_view.DataContext = data;
				node_root_view.IParent = parent;
				node_root_view.IOwner = owner;
				if (data is ILotusCheckOne<TData> check)
				{
					if (check.CheckOne(filter))
					{
						if (data is ILotusViewItemOwner view_item_owner)
						{
							view_item_owner.OwnerViewItem = node_root_view;
						}

						if (parent != null)
						{
							node_root_view.Level = parent.Level + 1;
							parent.IViewItems.Add(node_root_view);
							node_root_view.IOwner = owner;
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

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CollectionViewHierarchy()
				: this(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public CollectionViewHierarchy(String name)
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
			public CollectionViewHierarchy(String name, System.Object source)
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
				var clone = new CollectionViewHierarchy<TViewItemHierarchy, TData>();
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

			#region ======================================= МЕТОДЫ ILotusCollectionView ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Раскрытие всего элемента отображения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Expanded()
			{
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
			/// Выключение выбора всех элементов отображения кроме исключаемого
			/// </summary>
			/// <param name="exclude">Исключаемый элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			public override void UnsetAllSelected(ILotusViewItem exclude)
			{
				if (exclude != null)
				{
					for (var i = 0; i < mCount; i++)
					{
						if (Object.ReferenceEquals(mArrayOfItems[i], exclude) == false)
						{
							mArrayOfItems[i].IsSelected = false;
							mArrayOfItems[i].UnsetAllSelected(exclude as ILotusViewItemHierarchy);
						}
					}

					SelectedViewItem = (TViewItemHierarchy)exclude;
				}
				else
				{
					for (var i = 0; i < mCount; i++)
					{
						mArrayOfItems[i].IsSelected = false;
						mArrayOfItems[i].UnsetAllSelected(exclude as ILotusViewItemHierarchy);
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
			public override void UnsetAllPresent(ILotusViewItem exclude, CParameters parameters)
			{
				if (exclude != null)
				{
					for (var i = 0; i < mCount; i++)
					{
						if (Object.ReferenceEquals(mArrayOfItems[i], exclude) == false)
						{
							mArrayOfItems[i].IsPresented = false;
							mArrayOfItems[i].UnsetAllPresent(exclude as ILotusViewItemHierarchy, parameters);
						}
					}

					PresentedViewItem = (TViewItemHierarchy)exclude;
				}
				else
				{
					// Выключаем все элемента отображения
					for (var i = 0; i < mCount; i++)
					{
						mArrayOfItems[i].IsPresented = false;
						mArrayOfItems[i].UnsetAllPresent(exclude as ILotusViewItemHierarchy, parameters);
					}

					PresentedViewItem = default;
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
				for (var i = 0; i < mCount; ++i)
				{
					mArrayOfItems[i].Visit(match);
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
				for (var i = 0; i < mCount; i++)
				{
					mArrayOfItems[i].IOwner = this;
					mArrayOfItems[i].UpdateOwnedObjects();
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
			public override Boolean OnNotifyUpdating(ILotusOwnedObject owned_object, System.Object data, String data_name)
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
			public override void OnNotifyUpdated(ILotusOwnedObject owned_object, System.Object data, String data_name)
			{
				base.OnNotifyUpdated(owned_object, data, data_name);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка источника данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void ResetSource()
			{
				Clear();

				if(Source is TData data)
				{
					if (mIsFiltered == false)
					{
						TViewItemHierarchy view_item_hierarchy = Build(data, this);
						Add(view_item_hierarchy);
					}
					else
					{
						if(mFilter != null)
						{
							TViewItemHierarchy view_item_hierarchy = BuildFilter(data, mFilter, this);
							Add(view_item_hierarchy);
						}
					}
				}
				else
				{
					if (Source is IList<TData> list_data)
					{
						for (var i = 0; i < list_data.Count; i++)
						{
							TViewItemHierarchy view_item_hierarchy = Build(list_data[i], this);
							Add(view_item_hierarchy);
						}
					}
				}

				NotifyCollectionReset();
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
		public class CCollectionViewHierarchyObject : CollectionViewHierarchy<CViewItemHierarchyObject, System.Object>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionViewHierarchyObject()
				: base(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionViewHierarchyObject(String name)
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
			public CCollectionViewHierarchyObject(String name, System.Object source)
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