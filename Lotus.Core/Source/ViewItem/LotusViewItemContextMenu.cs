//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема отображения данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusViewItemContextMenu.cs
*		Определение кроссплатформенного контекстного меню.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
		/** \addtogroup CoreViewItem
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс инкапсулирующий элемент контекстного меню
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUIContextMenuItem : ILotusDuplicate<CUIContextMenuItem>
		{
			#region ======================================= ДАННЫЕ ====================================================
			public ILotusViewItem ViewItem;
			public Action<ILotusViewItem> OnAction;
			public Action<ILotusViewItem> OnAfterAction;
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
			/// <param name="viewItem">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewItem viewItem)
				: this(viewItem, String.Empty, null, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="viewItem">Элемент отображения</param>
			/// <param name="name">Имя элемента меню</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewItem viewItem, String name)
				: this(viewItem, name, null, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="onAction">Обработчик событие основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(String name, Action<ILotusViewItem> onAction)
				: this(null, name, onAction, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="viewItem">Элемент отображения</param>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="onAction">Обработчик событие основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewItem viewItem, String name, Action<ILotusViewItem> onAction)
				: this(viewItem, name, onAction, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="viewItem">Элемент отображения</param>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="onAction">Обработчик событие основного действия</param>
			/// <param name="onAfterAction">Дополнительный обработчик события после основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewItem viewItem, String name, Action<ILotusViewItem> onAction, 
				Action<ILotusViewItem> onAfterAction)
			{
				ViewItem = viewItem;
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
			public virtual CUIContextMenuItem Duplicate(CParameters parameters = null)
			{
				var item = new CUIContextMenuItem();
				item.ViewItem = ViewItem;
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
			/// <param name="viewItem">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnLoadItemClick(ILotusViewItem viewItem)
			{
				if (viewItem != null && viewItem.DataContext != null)
				{
					var file_name = XFileDialog.Open();
					if (file_name.IsExists())
					{
						// Уведомляем о начале загрузки
						if (viewItem.DataContext is ILotusBeforeLoad before_load)
						{
							before_load.OnBeforeLoad(null);
						}

						if (viewItem.DataContext is ILotusOwnerObject owner_object)
						{
							owner_object.UpdateOwnedObjects();
						}

						// Уведомляем об окончании загрузки
						if (viewItem.DataContext is ILotusAfterLoad after_load)
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
			/// <param name="viewItem">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnSaveItemClick(ILotusViewItem viewItem)
			{
				if (viewItem != null && viewItem.DataContext != null)
				{
					var file_name = XFileDialog.Save();
					if (file_name.IsExists())
					{
						// Уведомляем о начале сохранения 
						if (viewItem.DataContext is ILotusBeforeSave before_save)
						{
							before_save.OnBeforeSave(null);
						}

						//XSerializationDispatcher.SaveTo(file_name, view_item.DataContext);

						// Уведомляем об окончании сохранения 
						if (viewItem.DataContext is ILotusAfterSave after_save)
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
			/// <param name="viewItem">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnRemoveItemClick(ILotusViewItem viewItem)
			{
				// Удаляем с отображения
				if(viewItem.IOwner is ILotusOwnerObject owner)
				{
					owner.DetachOwnedObject(viewItem, true);
				}
				if (viewItem.IOwner is IList list)
				{
					if(list.IndexOf(viewItem) != -1)
					{
						list.Remove(viewItem);
					}
				}

				// Удаляем реальные данные
				if (viewItem.DataContext is ILotusOwnerObject owner_data_context)
				{
					owner_data_context.DetachOwnedObject(viewItem.DataContext as ILotusOwnedObject, true);
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
			/// <param name="viewItem">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnDuplicateItemClick(ILotusViewItem viewItem)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события перемещение объекта вверх
			/// </summary>
			/// <param name="viewItem">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnMoveUpItemClick(ILotusViewItem viewItem)
			{
				if (viewItem != null && viewItem.IOwner is IList list)
				{
					var index = list.IndexOf(viewItem);
					if(index > 0)
					{
						list.MoveObjectUp(index);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события перемещение объекта вниз
			/// </summary>
			/// <param name="viewItem">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnMoveDownItemClick(ILotusViewItem viewItem)
			{
				if (viewItem != null && viewItem.IOwner is IList list)
				{
					var index = list.IndexOf(viewItem);
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
			/// <param name="viewItem">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void OnNotCalculationItemClick(ILotusViewItem viewItem)
			{
				if (viewItem != null && viewItem.DataContext is ILotusNotCalculation calculation)
				{
					calculation.NotCalculation = !calculation.NotCalculation;
				}
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			public ILotusViewItem ViewItem;
			public Boolean IsCreatedItems;
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
			/// <param name="viewItem">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenu(ILotusViewItem viewItem)
				: this(viewItem, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="viewItem">Элемент отображения</param>
			/// <param name="items">Набор элементов меню</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenu(ILotusViewItem viewItem, params CUIContextMenuItem[] items)
			{
				ViewItem = viewItem;
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
			public virtual void AddItem(String name, Action<ILotusViewItem> onAction)
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
			public virtual void AddItem(String name, Action<ILotusViewItem> onAction, Action<ILotusViewItem> onAfterAction)
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