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
				get { return (mName); }
				set { mName = value; }
			}

			/// <summary>
			/// Пул объектов типа оболочки для хранения задачи
			/// </summary>
			public PoolManager<CTaskHolder> TaskHolderPools
			{
				get { return (mTaskHolderPools); }
			}

			/// <summary>
			/// Список всех одиночных задач
			/// </summary>
			public List<CTaskHolder> Tasks
			{
				get { return (mTasks); }
			}

			/// <summary>
			/// Словарь всех обработчиков события окончания выполнения задачи
			/// </summary>
			public Dictionary<String, Action> TaskHandlersCompleted
			{
				get { return (mTaskHandlersCompleted); }
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
			/// <param name="task_name">Имя задачи</param>
			/// <returns>Найденная задача или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CTaskHolder GetTask(String task_name)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == task_name)
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
			/// <param name="task_name">Имя задачи</param>
			/// <param name="method">Способ выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddTask(ILotusTask task, String task_name, TTaskMethod method)
			{
				CTaskHolder task_holder = mTaskHolderPools.Take();
				task_holder.Name = task_name;
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
			/// <param name="task_name">Имя задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RemoveTask(String task_name)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == task_name)
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
			/// <param name="task_name">Имя задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RunTask(String task_name)
			{
				RunTask(task_name, 0.0f, null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения задачи
			/// </summary>
			/// <param name="task_name">Имя задачи</param>
			/// <param name="delay_start">Время задержки запуска выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RunTask(String task_name, Single delay_start)
			{
				RunTask(task_name, delay_start, null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения задачи
			/// </summary>
			/// <param name="task_name">Имя задачи</param>
			/// <param name="delay_start">Время задержки запуска выполнения задачи</param>
			/// <param name="on_completed">Обработчик события окончания выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RunTask(String task_name, Single delay_start, Action on_completed)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == task_name)
					{
						if (on_completed != null)
						{
							if (mTaskHandlersCompleted.ContainsKey(task_name))
							{
								mTaskHandlersCompleted[task_name] = on_completed;
							}
							else
							{
								mTaskHandlersCompleted.Add(task_name, on_completed);
							}
						}

						mTasks[i].DelayStart = delay_start;
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
			/// <param name="delay_start">Время задержки запуска выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RunTask(ILotusTask task, Single delay_start)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Task == task)
					{
						mTasks[i].DelayStart = delay_start;
						mTasks[i].RunTask();
						return;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Пауза выполнения задачи
			/// </summary>
			/// <param name="task_name">Имя задачи</param>
			/// <param name="pause">Статус паузы</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void PauseTask(String task_name, Boolean pause)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == task_name)
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
			/// <param name="task_name">Имя задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void StopTask(String task_name)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == task_name)
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
			/// <param name="task_name">Имя задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ResetTask(String task_name)
			{
				for (var i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == task_name)
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