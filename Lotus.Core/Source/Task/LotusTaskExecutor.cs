//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема задач
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusTaskExecutor.cs
*		Исполнитель задачи.
*		Реализация исполнителя для управления процессом выполнения задачи.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreTask
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		///  Определение интерфейса исполнителя задачи
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusTaskExecutor : ILotusNameable
		{

		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Исполнитель задачи
		/// </summary>
		/// <remarks>
		/// Реализация исполнителя для управления процессом выполнения задачи
		/// Метод исполнителя нужно вызывать вручную в соответствующих местах
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CTaskExecutor : ILotusTaskExecutor
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструирование класса-оболочки для хранения определенной задачи
			/// </summary>
			/// <returns>Оболочка для хранения определенной задачи</returns>
			//---------------------------------------------------------------------------------------------------------
			private static CTaskHolder ConstructorTaskHolder()
			{
				return new CTaskHolder(true);
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal String mName;
			protected internal PoolManager<CTaskHolder> mTaskHolderPools;
			protected internal List<CTaskHolder> mTasks;
			protected internal Dictionary<String, Action> mTaskHandlersCompleted;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя исполнителя задач
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Пул объектов типа оболочки для хранения задачи
			/// </summary>
			public PoolManager<CTaskHolder> TaskHolderPools
			{
				get { return mTaskHolderPools; }
			}

			/// <summary>
			/// Список всех одиночных задач
			/// </summary>
			public List<CTaskHolder> Tasks
			{
				get { return mTasks; }
			}

			/// <summary>
			/// Словарь всех обработчиков события окончания выполнения задачи
			/// </summary>
			public Dictionary<String, Action> TaskHandlersCompleted
			{
				get { return mTaskHandlersCompleted; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CTaskExecutor()
				: this("")
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя исполнителя задач</param>
			//---------------------------------------------------------------------------------------------------------
			public CTaskExecutor(String name)
			{
				mTaskHolderPools = new PoolManager<CTaskHolder>(10, ConstructorTaskHolder);
				mTasks = new List<CTaskHolder>(10);
				mTaskHandlersCompleted = new Dictionary<String, Action>(10);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление центрального диспетчера выполнения задач каждый кадр
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnUpdate()
			{
				// Выполняем отдельные задачи каждый кадр
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (!mTasks[i].IsTaskCompleted)
					{
						mTasks[i].ExecuteTask();
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Текущий статус
			/// </summary>
			/// <returns>Статус всех задач сформированных в текстовое представление</returns>
			//---------------------------------------------------------------------------------------------------------
			public String GetStatus()
			{
				var str = new StringBuilder(200);
				str.AppendLine("Всего задач: " + mTasks.Count.ToString());
				for (var it = 0; it < mTasks.Count; it++)
				{
					str.AppendLine("Задача: " + mTasks[it].Name);
				}

				return str.ToString();
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ЗАДАЧАМИ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение задачи по имени
			/// </summary>
			/// <param name="taskName">Имя задачи</param>
			/// <returns>Найденная задача или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CTaskHolder GetTask(String taskName)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == taskName)
					{
						return mTasks[i];
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление новой задачи
			/// </summary>
			/// <param name="task">Задача</param>
			/// <param name="method">Способ выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddTask(ILotusTask task, TTaskMethod method)
			{
				CTaskHolder task_holder = mTaskHolderPools.Take();
				task_holder.Task = task;
				task_holder.MethodMode = method;
				mTasks.Add(task_holder);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление новой задачи
			/// </summary>
			/// <param name="task">Задача</param>
			/// <param name="taskName">Имя задачи</param>
			/// <param name="method">Способ выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddTask(ILotusTask task, String taskName, TTaskMethod method)
			{
				CTaskHolder task_holder = mTaskHolderPools.Take();
				task_holder.Name = taskName;
				task_holder.Task = task;
				task_holder.MethodMode = method;
				mTasks.Add(task_holder);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление задачи
			/// </summary>
			/// <param name="task">Задача</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RemoveTask(ILotusTask task)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Task == task)
					{
						// 1) Возвращаем в пул
						CTaskHolder task_holder = mTasks[i];
						mTaskHolderPools.Release(task_holder);

						// 2) Удаляем
						mTasks.RemoveAt(i);
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление задачи
			/// </summary>
			/// <param name="taskName">Имя задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RemoveTask(String taskName)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == taskName)
					{
						// 1) Возвращаем в пул
						CTaskHolder task_holder = mTasks[i];
						mTaskHolderPools.Release(task_holder);

						// 2) Удаляем
						mTasks.RemoveAt(i);
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения задачи
			/// </summary>
			/// <param name="taskName">Имя задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RunTask(String taskName)
			{
				RunTask(taskName, 0.0f, null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения задачи
			/// </summary>
			/// <param name="taskName">Имя задачи</param>
			/// <param name="delayStart">Время задержки запуска выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RunTask(String taskName, Single delayStart)
			{
				RunTask(taskName, delayStart, null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения задачи
			/// </summary>
			/// <param name="taskName">Имя задачи</param>
			/// <param name="delayStart">Время задержки запуска выполнения задачи</param>
			/// <param name="onCompleted">Обработчик события окончания выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RunTask(String taskName, Single delayStart, Action onCompleted)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == taskName)
					{
						if (onCompleted != null)
						{
							if (mTaskHandlersCompleted.ContainsKey(taskName))
							{
								mTaskHandlersCompleted[taskName] = onCompleted;
							}
							else
							{
								mTaskHandlersCompleted.Add(taskName, onCompleted);
							}
						}

						mTasks[i].DelayStart = delayStart;
						mTasks[i].RunTask();
						return;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения задачи
			/// </summary>
			/// <param name="task">Задача</param>
			/// <param name="delayStart">Время задержки запуска выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RunTask(ILotusTask task, Single delayStart)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Task == task)
					{
						mTasks[i].DelayStart = delayStart;
						mTasks[i].RunTask();
						return;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Пауза выполнения задачи
			/// </summary>
			/// <param name="taskName">Имя задачи</param>
			/// <param name="pause">Статус паузы</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void PauseTask(String taskName, Boolean pause)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == taskName)
					{
						mTasks[i].IsPause = pause;
						return;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Принудительная остановка выполнения задачи
			/// </summary>
			/// <param name="taskName">Имя задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void StopTask(String taskName)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == taskName)
					{
						mTasks[i].StopTask();
						return;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка данных задачи
			/// </summary>
			/// <param name="taskName">Имя задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ResetTask(String taskName)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == taskName)
					{
						mTasks[i].ResetTask();
						return;
					}
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