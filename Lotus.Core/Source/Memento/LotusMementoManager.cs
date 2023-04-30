//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема хранения состояний
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMementoManager.cs
*		Интерфейс менеджера для отмены/повторения действия.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreMemento
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс менеджера для отмены/повторения действия
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusMementoManager
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Возможность отмены действия
			/// </summary>
			Boolean CanUndo { get; }

			/// <summary>
			/// Возможность повтора действия
			/// </summary>
			Boolean CanRedo { get; }

			/// <summary>
			/// Доступность менеджера
			/// </summary>
			Boolean IsEnabled { get; set; }
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отмена последнего действия
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void Undo();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Повторение последнего действия
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void Redo();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление нового состояния элемента в историю действий
			/// </summary>
			/// <remarks>
			/// Вызывается клиентом после выполнения некоторых действий
			/// </remarks>
			/// <param name="state">Состояние объекта</param>
			//---------------------------------------------------------------------------------------------------------
			void AddStateToHistory(ILotusMementoState state);
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Менеджер для отмены/повторения действия
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CMementoManager : PropertyChangedBase, ILotusMementoManager, INotifyPropertyChanged
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static readonly PropertyChangedEventArgs PropertyArgsCanUndo = new PropertyChangedEventArgs(nameof(CanUndo));
			protected static readonly PropertyChangedEventArgs PropertyArgsCanRedo = new PropertyChangedEventArgs(nameof(CanRedo));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsEnabled = new PropertyChangedEventArgs(nameof(IsEnabled));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal ListArray<ILotusMementoState> mHistoryStates;
			protected internal Int32 mNextUndo;
			protected internal Boolean mIsEnabled;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// История состояния объектов
			/// </summary>
			public ListArray<ILotusMementoState> HistoryStates 
			{
				get { return (mHistoryStates); } 
			}

			/// <summary>
			/// Возможность отмены действия
			/// </summary>
			public Boolean CanUndo
			{
				get
				{
					// If the NextUndo pointer is -1, no commands to undo
					if (mNextUndo < 0 || mNextUndo > mHistoryStates.Count - 1) // precaution
					{
						return (false);
					}

					return (true);
				}
			}

			/// <summary>
			/// Возможность повтора действия
			/// </summary>
			public Boolean CanRedo
			{
				get
				{
					// If the NextUndo pointer points to the last item, no commands to redo
					if (mNextUndo == mHistoryStates.Count - 1)
					{
						return (false);
					}

					return (true);
				}
			}

			/// <summary>
			/// Статус работы менеджера
			/// </summary>
			public Boolean IsEnabled
			{
				get
				{
					return (mIsEnabled);
				}
				set
				{
					mIsEnabled = value;
					NotifyPropertyChanged(PropertyArgsIsEnabled);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CMementoManager()
			{
				mHistoryStates = new ListArray<ILotusMementoState>()
				{
					IsNotify = true
				};
				mNextUndo = -1;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка списка истории действий
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ClearHistory()
			{
				mHistoryStates.Clear();
				mNextUndo = -1;
				NotifyPropertyChanged(PropertyArgsCanUndo);
				NotifyPropertyChanged(PropertyArgsCanRedo);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление нового состояния элемента в историю действий
			/// </summary>
			/// <remarks>
			/// Вызывается клиентом после выполнения некоторых действий
			/// </remarks>
			/// <param name="state">Состояние объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddStateToHistory(ILotusMementoState state)
			{
				// Purge history list
				TrimHistoryList();

				// Add command and increment undo counter
				mHistoryStates.Add(state);

				mNextUndo++;

				NotifyPropertyChanged(PropertyArgsCanUndo);
				NotifyPropertyChanged(PropertyArgsCanRedo);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отмена последнего действия
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Undo()
			{
				if (!CanUndo)
				{
					return;
				}

				// Get the Command object to be undone
				ILotusMementoState state = mHistoryStates[mNextUndo];

				// Execute the Command object's undo method
				state.Undo();

				// Move the pointer up one item
				mNextUndo--;

				// Обновляем свойства
				NotifyPropertyChanged(PropertyArgsCanUndo);
				NotifyPropertyChanged(PropertyArgsCanRedo);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Повторение последнего действия
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Redo()
			{
				if (!CanRedo)
				{
					return;
				}

				// Get the Command object to redo
				var item_to_redo = mNextUndo + 1;
				ILotusMementoState state = mHistoryStates[item_to_redo];

				// Execute the Command object
				state.Redo();

				// Move the undo pointer down one item
				mNextUndo++;

				// Обновляем свойства
				NotifyPropertyChanged(PropertyArgsCanUndo);
				NotifyPropertyChanged(PropertyArgsCanRedo);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка служебного списка
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private void TrimHistoryList()
			{
				// We can redo any undone command until we execute a new 
				// command. The new command takes us off in a new direction,
				// which means we can no longer redo previously undone actions. 
				// So, we purge all undone commands from the history list.*/

				// Exit if no items in History list
				if (mHistoryStates.Count == 0)
				{
					return;
				}

				// Exit if NextUndo points to last item on the list
				if (mNextUndo == mHistoryStates.Count - 1)
				{
					return;
				}

				// Purge all items below the NextUndo pointer
				for (var i = mHistoryStates.Count - 1; i > mNextUndo; i--)
				{
					mHistoryStates.RemoveAt(i);
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