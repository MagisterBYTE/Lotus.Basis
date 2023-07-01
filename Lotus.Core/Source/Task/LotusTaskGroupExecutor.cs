//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема задач
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusTaskGroupExecutor.cs
*		Исполнитель группы задачи.
*		Реализация исполнителя для управления процессом выполнения задач в группе.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Text;
using System.Collections.Generic;
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
		/// Исполнитель группы задачи
		/// </summary>
		/// <remarks>
		/// Реализация исполнителя для управления процессом выполнения задач в группе.
		/// Метод исполнителя нужно вызывать вручную в соответствующих местах
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CTaskGroupExecutor : ILotusTaskExecutor
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
			protected internal List<CGroupTask> mGroupTasks;
			protected internal Dictionary<String, Action> mGroupTaskHandlersCompleted;
			protected internal Dictionary<String, Action<ILotusTask>> mGroupTaskHandlersEachTaskCompleted;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя исполнителя групп задач
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
			/// Список всех групп задач
			/// </summary>
			public List<CGroupTask> GroupTasks
			{
				get { return mGroupTasks; }
			}

			/// <summary>
			/// Словарь всех обработчиков события окончания выполнения группы задачи
			/// </summary>
			public Dictionary<String, Action> GroupTaskHandlersCompleted
			{
				get { return mGroupTaskHandlersCompleted; }
			}

			/// <summary>
			/// Словарь всех обработчиков события окончания выполнения каждой задачи группы
			/// </summary>
			public Dictionary<String, Action<ILotusTask>> GroupTaskHandlersEachTaskCompleted
			{
				get { return mGroupTaskHandlersEachTaskCompleted; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CTaskGroupExecutor()
				: this("")
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя исполнителя задач</param>
			//---------------------------------------------------------------------------------------------------------
			public CTaskGroupExecutor(String name)
			{
				mTaskHolderPools = new PoolManager<CTaskHolder>(10, ConstructorTaskHolder);
				mGroupTasks = new List<CGroupTask>(10);
				mGroupTaskHandlersCompleted = new Dictionary<String, Action>(10);
				mGroupTaskHandlersEachTaskCompleted = new Dictionary<String, Action<ILotusTask>>(10);
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
				// Выполняем отдельные группы задачи каждый кадр
				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					CGroupTask group_task = mGroupTasks[i];

					if (!group_task.IsCompleted)
					{
						if (group_task.ExecuteMode == TTaskExecuteMode.Parallel)
						{
							group_task.ExecuteInParallel();
						}
						else
						{
							group_task.ExecuteSequentially();
						}
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
				str.AppendLine("Всего групп задач: " + mGroupTasks.Count.ToString());
				for (var ig = 0; ig < mGroupTasks.Count; ig++)
				{
					str.AppendLine("Группа: " + mGroupTasks[ig].Name + "(задач: " +
						mGroupTasks[ig].Tasks.Count.ToString() + ")");
				}

				return str.ToString();
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ГРУППАМИ ЗАДАЧАМ ==========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение группы задачи по имени
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <returns>Найденная группа задач или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask GetGroupTask(String groupName)
			{
				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						return mGroupTasks[i];
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление(существующей) группы задачи
			/// </summary>
			/// <param name="groupTask">Группа задач</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask AddGroupTask(CGroupTask groupTask)
			{
				mGroupTasks.Add(groupTask);
				return groupTask;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление(существующей) группы задачи
			/// </summary>
			/// <param name="groupTask">Группа задач</param>
			/// <param name="onCompletedEachTask">Обработчик завершения каждой задачи группы</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask AddGroupTask(CGroupTask groupTask, Action<ILotusTask> onCompletedEachTask)
			{
				mGroupTasks.Add(groupTask);

				if (onCompletedEachTask != null)
				{
					if (mGroupTaskHandlersEachTaskCompleted.ContainsKey(groupTask.Name))
					{
						mGroupTaskHandlersEachTaskCompleted[groupTask.Name] = onCompletedEachTask;
					}
					else
					{
						mGroupTaskHandlersEachTaskCompleted.Add(groupTask.Name, onCompletedEachTask);
					}
				}

				return groupTask;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление(создание) группы задачи выполняемых параллельно каждый кадр
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="list">Список задач группы</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask AddGroupTask(String groupName, params ILotusTask[] list)
			{
				return AddGroupTask(groupName, TTaskExecuteMode.Parallel, TTaskMethod.EachFrame, list);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление(создание) группы задачи выполняемых параллельно
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="method">Способ выполнения задач группы</param>
			/// <param name="list">Список задач группы</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask AddGroupTask(String groupName, TTaskMethod method, params ILotusTask[] list)
			{
				return AddGroupTask(groupName, TTaskExecuteMode.Parallel, method, list);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление(создание) группы задачи
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="method">Способ выполнения задач группы</param>
			/// <param name="list">Список задач группы</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask AddGroupTask(String groupName, TTaskExecuteMode executeMode, TTaskMethod method, params ILotusTask[] list)
			{
				var task = new CGroupTask(groupName, method, this, list);
				task.ExecuteMode = executeMode;
				mGroupTasks.Add(task);
				return task;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение существующей группы задач или создание новой группы задач с указанными задачами и параметрами
			/// </summary>
			/// <param name="groupName">Имя задачи</param>
			/// <param name="task">Задача</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask GetGroupTaskExistsTask(String groupName, ILotusTask task)
			{
				return GetGroupTaskExistsTask(groupName, TTaskExecuteMode.Parallel, TTaskMethod.EachFrame, task);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение существующей группы задач или создание новой группы задач с указанными задачами и параметрами
			/// </summary>
			/// <param name="groupName">Имя задачи</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="method">Способ выполнения группы задач</param>
			/// <param name="task">Задача</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask GetGroupTaskExistsTask(String groupName, TTaskExecuteMode executeMode, TTaskMethod method, ILotusTask task)
			{
				CGroupTask group_task = null;

				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						group_task = mGroupTasks[i];
						break;
					}
				}

				if (group_task == null)
				{
					group_task = new CGroupTask(groupName, method, this, task);
					group_task.ExecuteMode = executeMode;
					mGroupTasks.Add(group_task);
				}

				return group_task;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение существующей группы задач или создание новой группы задач с указанными задачами и параметрами
			/// </summary>
			/// <param name="groupName">Имя задачи</param>
			/// <param name="tasks">Задачи</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask GetGroupTaskExistsTasks(String groupName, params ILotusTask[] tasks)
			{
				return GetGroupTaskExistsTasks(groupName, TTaskExecuteMode.Parallel, TTaskMethod.EachFrame, tasks);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение существующей группы задач или создание новой группы задач с указанными задачами и параметрами
			/// </summary>
			/// <param name="groupName">Имя задачи</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="tasks">Задачи</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask GetGroupTaskExistsTasks(String groupName, TTaskExecuteMode executeMode, params ILotusTask[] tasks)
			{
				return GetGroupTaskExistsTasks(groupName, executeMode, TTaskMethod.EachFrame, tasks);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение существующей группы задач или создание новой группы задач с указанными задачами и параметрами
			/// </summary>
			/// <param name="groupName">Имя задачи</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="method">Способ выполнения группы задач</param>
			/// <param name="tasks">Задачи</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask GetGroupTaskExistsTasks(String groupName, TTaskExecuteMode executeMode, TTaskMethod method, params ILotusTask[] tasks)
			{
				CGroupTask group_task = null;

				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						group_task = mGroupTasks[i];
						break;
					}
				}

				if (group_task == null)
				{
					group_task = new CGroupTask(groupName, method, this, tasks);
					group_task.ExecuteMode = executeMode;
					mGroupTasks.Add(group_task);
				}

				return group_task;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление в существующую группу задач дополнительной задачи или создание новой группы задач
			/// </summary>
			/// <param name="groupName">Имя задачи</param>
			/// <param name="task">Задача</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask AddGroupTaskExistsTask(String groupName, ILotusTask task)
			{
				return AddGroupTaskExistsTask(groupName, TTaskExecuteMode.Parallel, TTaskMethod.EachFrame, task);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление в существующую группу задач дополнительной задачи или создание новой группы задач
			/// </summary>
			/// <param name="groupName">Имя задачи</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="method">Способ выполнения группы задач</param>
			/// <param name="task">Задача</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask AddGroupTaskExistsTask(String groupName, TTaskExecuteMode executeMode, TTaskMethod method, ILotusTask task)
			{
				CGroupTask group_task = null;

				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						group_task = mGroupTasks[i];
						break;
					}
				}

				if (group_task == null)
				{
					group_task = new CGroupTask(groupName, method, this, task);
					group_task.ExecuteMode = executeMode;
					mGroupTasks.Add(group_task);
				}
				else
				{
					group_task.Add(task);
				}

				return group_task;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление в существующую группу задач дополнительной задачи или создание новой группы задач
			/// </summary>
			/// <param name="groupName">Имя задачи</param>
			/// <param name="tasks">Задачи</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask AddGroupTaskExistsTasks(String groupName, params ILotusTask[] tasks)
			{
				return AddGroupTaskExistsTasks(groupName, TTaskExecuteMode.Parallel, TTaskMethod.EachFrame, tasks);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление в существующую группу задач дополнительной задачи или создание новой группы задач
			/// </summary>
			/// <param name="groupName">Имя задачи</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="tasks">Задачи</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask AddGroupTaskExistsTasks(String groupName, TTaskExecuteMode executeMode, params ILotusTask[] tasks)
			{
				return AddGroupTaskExistsTasks(groupName, executeMode, TTaskMethod.EachFrame, tasks);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление в существующую группу задач дополнительной задачи или создание новой группы задач
			/// </summary>
			/// <param name="groupName">Имя задачи</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="method">Способ выполнения группы задач</param>
			/// <param name="tasks">Задачи</param>
			/// <returns>Группа задач</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask AddGroupTaskExistsTasks(String groupName, TTaskExecuteMode executeMode, TTaskMethod method, params ILotusTask[] tasks)
			{
				CGroupTask group_task = null;

				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						group_task = mGroupTasks[i];
						break;
					}
				}

				if (group_task == null)
				{
					group_task = new CGroupTask(groupName, method, this, tasks);
					group_task.ExecuteMode = executeMode;
					mGroupTasks.Add(group_task);
				}
				else
				{
					group_task.AddList(tasks);
					group_task.ExecuteMode = executeMode;
					group_task.SetMethodMode(method);
				}

				return group_task;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление группы задачи
			/// </summary>
			/// <param name="groupTask">Группа задач</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RemoveGroupTask(CGroupTask groupTask)
			{
				if (mGroupTasks.Remove(groupTask))
				{
					// Удаляем все связанные задачи
					groupTask.Clear();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление группы задачи
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RemoveGroupTask(String groupName)
			{
				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						mGroupTasks[i].Clear();
						mGroupTasks.RemoveAt(i);
						return;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка группы от задач
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ClearGroupTask(String groupName)
			{
				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						mGroupTasks[i].Clear();
						return;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения группы задачи параллельно
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <returns>Запущенная группа задач или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask RunGroupTask(String groupName)
			{
				return RunGroupTask(groupName, 0.0f, null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения группы задачи параллельно
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <returns>Запущенная группа задач или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask RunGroupTask(String groupName, TTaskExecuteMode executeMode)
			{
				return RunGroupTask(groupName, executeMode, 0.0f, null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения группы задачи
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="delayStart">Задержка в секундах начало выполнения задач группы</param>
			/// <returns>Запущенная группа задач или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask RunGroupTask(String groupName, Single delayStart)
			{
				return RunGroupTask(groupName, delayStart, null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения группы задачи
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="delayStart">Задержка в секундах начало выполнения задач группы</param>
			/// <returns>Запущенная группа задач или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask RunGroupTask(String groupName, TTaskExecuteMode executeMode, Single delayStart)
			{
				return RunGroupTask(groupName, executeMode, delayStart, null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения группы задачи
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="onCompleted">Обработчик события окончания выполнения задач группы</param>
			/// <returns>Запущенная группа задач или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask RunGroupTask(String groupName, Action onCompleted)
			{
				return RunGroupTask(groupName, 0.0f, onCompleted);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения группы задачи
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="onCompleted">Обработчик события окончания выполнения задач группы</param>
			/// <returns>Запущенная группа задач или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask RunGroupTask(String groupName, TTaskExecuteMode executeMode, Action onCompleted)
			{
				return RunGroupTask(groupName, executeMode, 0.0f, onCompleted);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения группы задачи
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="delayStart">Задержка в секундах начало выполнения задач группы</param>
			/// <param name="onCompleted">Обработчик события окончания выполнения задач группы</param>
			/// <returns>Запущенная группа задач или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask RunGroupTask(String groupName, Single delayStart, Action onCompleted)
			{
				CGroupTask group_task = null;
				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						group_task = mGroupTasks[i];
						break;
					}
				}

				if(group_task != null)
				{
					if (onCompleted != null)
					{
						if (mGroupTaskHandlersCompleted.ContainsKey(group_task.Name))
						{
							mGroupTaskHandlersCompleted[group_task.Name] = onCompleted;
						}
						else
						{
							mGroupTaskHandlersCompleted.Add(group_task.Name, onCompleted);
						}
					}

					group_task.DelayStart = delayStart;
					group_task.Run();
				}

				return group_task;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения группы задачи
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="delayStart">Задержка в секундах начало выполнения задач группы</param>
			/// <param name="onCompleted">Обработчик события окончания выполнения задач группы</param>
			/// <returns>Запущенная группа задач или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask RunGroupTask(String groupName, TTaskExecuteMode executeMode, Single delayStart, Action onCompleted)
			{
				CGroupTask group_task = null;
				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						group_task = mGroupTasks[i];
						break;
					}
				}

				if (group_task != null)
				{
					if (onCompleted != null)
					{
						if (mGroupTaskHandlersCompleted.ContainsKey(group_task.Name))
						{
							mGroupTaskHandlersCompleted[group_task.Name] = onCompleted;
						}
						else
						{
							mGroupTaskHandlersCompleted.Add(group_task.Name, onCompleted);
						}
					}

					group_task.ExecuteMode = executeMode;
					group_task.DelayStart = delayStart;
					group_task.Run();
				}

				return group_task;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения группы задачи
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="executeMode">Режим выполнения группы задач</param>
			/// <param name="method">Способ выполнения группы задач</param>
			/// <param name="delayStart">Задержка в секундах начало выполнения задач группы</param>
			/// <param name="onCompleted">Обработчик события окончания выполнения задач группы</param>
			/// <returns>Запущенная группа задач или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CGroupTask RunGroupTask(String groupName, TTaskExecuteMode executeMode, TTaskMethod method, Single delayStart, Action onCompleted)
			{
				CGroupTask group_task = null;
				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						group_task = mGroupTasks[i];
						break;
					}
				}

				if (group_task != null)
				{
					if (onCompleted != null)
					{
						if (mGroupTaskHandlersCompleted.ContainsKey(group_task.Name))
						{
							mGroupTaskHandlersCompleted[group_task.Name] = onCompleted;
						}
						else
						{
							mGroupTaskHandlersCompleted.Add(group_task.Name, onCompleted);
						}
					}

					group_task.SetMethodMode(method);
					group_task.ExecuteMode = executeMode;
					group_task.DelayStart = delayStart;
					group_task.Run();
				}

				return group_task;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Пауза выполнения группы задачи
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			/// <param name="pause">Статус паузы</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void PauseGroupTask(String groupName, Boolean pause)
			{
				CGroupTask group_task = null;
				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						group_task = mGroupTasks[i];
						break;
					}
				}

				if (group_task != null)
				{
					group_task.IsPause = pause;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Принудительная остановка выполнения всех задач группы
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void StopGroupTask(String groupName)
			{
				CGroupTask group_task = null;
				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						group_task = mGroupTasks[i];
						break;
					}
				}

				if (group_task != null)
				{
					group_task.Stop();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка данных всех задач группы
			/// </summary>
			/// <param name="groupName">Имя группы задач</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ResetGroupTask(String groupName)
			{
				for (var i = 0; i < mGroupTasks.Count; i++)
				{
					if (mGroupTasks[i].Name == groupName)
					{
						mGroupTasks[i].Reset();
						break;
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