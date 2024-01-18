//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема ViewModel
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusViewModelContextMenu.cs
*		Определение кроссплатформенного контекстного меню.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
//---------------------------------------------------------------------------------------------------------------------
#if USE_WINDOWS
using Lotus.Windows;
#endif
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
		/// Класс инкапсулирующий элемент контекстного меню
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUIContextMenuItem : ILotusDuplicate<CUIContextMenuItem>
		{
			#region ======================================= ДАННЫЕ ====================================================
			public ILotusViewModel? ViewModel;
			public Action<ILotusViewModel>? OnAction;
			public Action<ILotusViewModel>? OnAfterAction;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewModel? viewModel)
				: this(viewModel, String.Empty, null, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			/// <param name="name">Имя элемента меню</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewModel? viewModel, String name)
				: this(viewModel, name, null, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="onAction">Обработчик событие основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(String name, Action<ILotusViewModel> onAction)
				: this(null, name, onAction, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="onAction">Обработчик событие основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewModel? viewModel, String name, Action<ILotusViewModel> onAction)
				: this(viewModel, name, onAction, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="onAction">Обработчик событие основного действия</param>
			/// <param name="onAfterAction">Дополнительный обработчик события после основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewModel? viewModel, String name, Action<ILotusViewModel>? onAction,
				Action<ILotusViewModel>? onAfterAction)
			{
				ViewModel = viewModel;
				OnAction = onAction;
				OnAfterAction = onAfterAction;

#if USE_WINDOWS
				CreateMenuItem(name, null);
#endif
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusDuplicate ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата объекта
			/// </summary>
			/// <param name="parameters">Параметры дублирования объекта</param>
			/// <returns>Дубликат объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CUIContextMenuItem Duplicate(CParameters? parameters = null)
			{
				var item = new CUIContextMenuItem();
				item.ViewModel = ViewModel;
				item.OnAction = OnAction;
				item.OnAfterAction = OnAfterAction;
				return item;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс инкапсулирующий контекстное меню
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUIContextMenu
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			/// <summary>
			/// Элемент меню - загрузить объект из файла
			/// </summary>
			public readonly static CUIContextMenuItem Load = new CUIContextMenuItem("Загрузить...", OnLoadItemClick);

			/// <summary>
			/// Элемент меню - сохранить объект в файл
			/// </summary>
			public readonly static CUIContextMenuItem Save = new CUIContextMenuItem("Сохранить...", OnSaveItemClick);

			/// <summary>
			/// Элемент меню - удалить объект
			/// </summary>
			public readonly static CUIContextMenuItem Remove = new CUIContextMenuItem("Удалить", OnRemoveItemClick);

			/// <summary>
			/// Элемент меню - дублировать объект
			/// </summary>
			public readonly static CUIContextMenuItem Duplicate = new CUIContextMenuItem("Дублировать", OnDuplicateItemClick);

			/// <summary>
			/// Элемент меню - переместить объект вверх
			/// </summary>
			public readonly static CUIContextMenuItem MoveUp = new CUIContextMenuItem("Переместить вверх", OnMoveUpItemClick);

			/// <summary>
			/// Элемент меню - переместить объект вниз
			/// </summary>
			public readonly static CUIContextMenuItem MoveDown = new CUIContextMenuItem("Переместить вниз", OnMoveDownItemClick);

			/// <summary>
			/// Элемент меню - не учитывать объект в расчетах
			/// </summary>
			public readonly static CUIContextMenuItem NotCalculation = new CUIContextMenuItem("Не учитывать в расчетах", OnNotCalculationItemClick);
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события загрузка объекта из файла
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnLoadItemClick(ILotusViewModel viewModel)
			{
				if (viewModel != null && viewModel.Model != null)
				{
					var file_name = XFileDialog.Open();
					if (file_name != null && file_name.IsExists())
					{
						// Уведомляем о начале загрузки
						if (viewModel.Model is ILotusBeforeLoad before_load)
						{
							before_load.OnBeforeLoad(null);
						}

						if (viewModel.Model is ILotusOwnerObject owner_object)
						{
							owner_object.UpdateOwnedObjects();
						}

						// Уведомляем об окончании загрузки
						if (viewModel.Model is ILotusAfterLoad after_load)
						{
							after_load.OnAfterLoad(null);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события сохранения объекта в файл
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnSaveItemClick(ILotusViewModel viewModel)
			{
				if (viewModel != null && viewModel.Model != null)
				{
					var file_name = XFileDialog.Save();
					if (file_name != null && file_name.IsExists())
					{
						// Уведомляем о начале сохранения 
						if (viewModel.Model is ILotusBeforeSave before_save)
						{
							before_save.OnBeforeSave(null);
						}

						XSerializationDispatcher.SaveTo(file_name, viewModel.Model);

						// Уведомляем об окончании сохранения 
						if (viewModel.Model is ILotusAfterSave after_save)
						{
							after_save.OnAfterSave(null);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события удаления объекта
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnRemoveItemClick(ILotusViewModel viewModel)
			{
				// Удаляем с отображения
				if (viewModel.IOwner is ILotusOwnerObject owner)
				{
					owner.DetachOwnedObject(viewModel, true);
				}
				if (viewModel.IOwner is IList list)
				{
					if (list.IndexOf(viewModel) != -1)
					{
						list.Remove(viewModel);
					}
				}

				// Удаляем реальные данные
				if (viewModel.Model is ILotusOwnerObject owner_data_context)
				{
					owner_data_context.DetachOwnedObject((viewModel.Model as ILotusOwnedObject)!, true);
				}
				else
				{
					//if (view_item.DataContext is IList data_context)
					//{
					//	list.Remove(view_item.DataContext);
					//}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события дублирование объекта
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnDuplicateItemClick(ILotusViewModel viewModel)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события перемещение объекта вверх
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnMoveUpItemClick(ILotusViewModel viewModel)
			{
				if (viewModel != null && viewModel.IOwner is IList list)
				{
					var index = list.IndexOf(viewModel);
					if (index > 0)
					{
						list.MoveObjectUp(index);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события перемещение объекта вниз
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnMoveDownItemClick(ILotusViewModel viewModel)
			{
				if (viewModel != null && viewModel.IOwner is IList list)
				{
					var index = list.IndexOf(viewModel);
					if (index > -1 && index < list.Count - 1)
					{
						list.MoveObjectDown(index);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события смены статуса объекта для учитывания в расчетах
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnNotCalculationItemClick(ILotusViewModel viewModel)
			{
				if (viewModel != null && viewModel.Model is ILotusNotCalculation calculation)
				{
					calculation.NotCalculation = !calculation.NotCalculation;
				}
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			public ILotusViewModel? ViewModel;
			public Boolean? IsCreatedItems;
			public List<CUIContextMenuItem> Items;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenu()
				: this(null, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenu(ILotusViewModel? viewModel)
				: this(viewModel, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="viewModel">Элемент отображения</param>
			/// <param name="items">Набор элементов меню</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenu(ILotusViewModel? viewModel, params CUIContextMenuItem[]? items)
			{
				ViewModel = viewModel;
				if (items != null && items.Length > 0)
				{
					Items = new List<CUIContextMenuItem>(items);
				}
				else
				{
					Items = new List<CUIContextMenuItem>();
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента меню
			/// </summary>
			/// <param name="menuItem">Элемент меню</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItem(CUIContextMenuItem menuItem)
			{
				Items.Add(menuItem);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элементов меню
			/// </summary>
			/// <param name="items">Набор элементов меню</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItem(params CUIContextMenuItem[] items)
			{
				Items.AddRange(items);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента меню
			/// </summary>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="onAction">Обработчик событие основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddItem(String name, Action<ILotusViewModel> onAction)
			{
				Items.Add(new CUIContextMenuItem(name, onAction));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента меню
			/// </summary>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="onAction">Обработчик событие основного действия</param>
			/// <param name="onAfterAction">Дополнительный обработчик события после основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddItem(String name, Action<ILotusViewModel> onAction, Action<ILotusViewModel> onAfterAction)
			{
				Items.Add(new CUIContextMenuItem(null, name, onAction, onAfterAction));
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================