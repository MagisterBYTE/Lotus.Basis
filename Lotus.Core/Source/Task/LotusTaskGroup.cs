//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема задач
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusTaskGroup.cs
*		Определение класса для поддержки группы взаимосвязанных задач.
*		Под группой задач понимается несколько задач, выполняемых параллельно или последовательно определённым способом 
*	с возможностью задержки начала выполнения задач, паузой, информирования об окончании выполнения всех задач группы, 
*	и принудительной остановкой выполнения группы задачи.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreTask
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для реализации группы задач
		/// </summary>
		/// <remarks>
		/// Под группой задач понимается несколько задач выполняемых параллельно или последовательно определённым способом
		/// с возможностью задержки начала выполнения задач, паузой, информирования об окончании выполнения всех задач группы,
		/// и принудительной остановкой выполнения группы задачи
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CGroupTask
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mName;
			protected internal List<CTaskHolder> mTasks;
			protected internal TTaskExecuteMode mExecuteMode;
			protected internal Single mDelayStart;

			// Переменные состояния
			protected internal Boolean mIsCompleted;
			protected internal Boolean mIsRunning;
			protected internal Boolean mIsPause;
			protected internal Boolean mIsDelayStart;
			protected internal CTaskHolder mCurrentTask;
			protected internal Int32 mCurrentTaskIndex;
			protected internal Single mStartTaskTime;
			protected internal Int32 mCountTaskExecute;
			protected internal Boolean mIsEachTaskCompletedHandler;

			// Исполнитель группы задач
			protected internal CTaskGroupExecutor mExecutor;

			// События
			protected internal Action<String> mOnGroupTaskStarted;
			protected internal Action<String> mOnGroupTaskCompleted;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Имя группы задачи
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Список задач группы
			/// </summary>
			public List<CTaskHolder> Tasks
			{
				get { return mTasks; }
			}

			/// <summary>
			/// Режим выполнения задач группы
			/// </summary>
			public TTaskExecuteMode ExecuteMode
			{
				get { return mExecuteMode; }
				set { mExecuteMode = value; }
			}

			/// <summary>
			/// Задержка в секундах при выполнение группы задач
			/// </summary>
			public Single DelayStart
			{
				get { return mDelayStart; }
				set
				{
					mDelayStart = value;
				}
			}

			/// <summary>
			/// Статус наличия обработчика завершения каждой задачи
			/// </summary>
			/// <remarks>
			/// Обработчик события окончания каждой задачи должен располагаться в словаре <see cref="CTaskGroupExecutor.GroupTaskHandlersEachTaskCompleted"/>
			/// </remarks>
			public Boolean IsEachTaskCompletedHandler
			{
				get { return mIsEachTaskCompletedHandler; }
				set
				{
					mIsEachTaskCompletedHandler = value;
				}
			}

			//
			// ПЕРЕМЕННЫЕ СОСТОЯНИЯ
			//
			/// <summary>
			/// Статус завершения всех задач группы
			/// </summary>
			public Boolean IsCompleted
			{
				get { return mIsCompleted; }
			}

			/// <summary>
			/// Статус выполнения задач группы
			/// </summary>
			public Boolean IsRunning
			{
				get { return mIsRunning; }
			}

			/// <summary>
			/// Пауза выполнения задач группы
			/// </summary>
			public Boolean IsPause
			{
				get { return mIsPause; }
				set
				{
					mIsPause = value;
				}
			}

			/// <summary>
			/// Текущая исполняемая задача
			/// </summary>
			public ILotusTask CurrentTask
			{
				get { return mCurrentTask; }
			}

			/// <summary>
			/// Индекс текущей исполняемой задачи
			/// </summary>
			public Int32 CurrentTaskIndex
			{
				get { return mCurrentTaskIndex; }
			}

			//
			// ИСПОЛНИТЕЛЬ ГРУППЫ
			//
			/// <summary>
			/// Исполнитель группы задач
			/// </summary>
			public CTaskGroupExecutor Executor
			{
				get { return mExecutor; }
			}

			//
			// СОБЫТИЯ
			//
			/// <summary>
			/// Событие для нотификации о начале выполнения всех задач группы. Аргумент - имя группы задач
			/// </summary>
			public Action<String> OnGroupTaskStarted
			{
				get { return mOnGroupTaskStarted; }
				set { mOnGroupTaskStarted = value; }
			}

			/// <summary>
			/// Событие для нотификации об окончании выполнения всех задач группы. Аргумент - имя группы задач
			/// </summary>
			public Action<String> OnGroupTaskCompleted
			{
				get { return mOnGroupTaskCompleted; }
				set { mOnGroupTaskCompleted = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="executor">Исполнитель группы задач</param>
			//---------------------------------------------------------------------------------------------------------
			public CGroupTask(CTaskGroupExecutor executor)
				: this("Без имение", TTaskMethod.EachFrame, executor, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя группы задачи</param>
			/// <param name="executor">Исполнитель группы задач</param>
			//---------------------------------------------------------------------------------------------------------
			public CGroupTask(String name, CTaskGroupExecutor executor)
				: this(name, TTaskMethod.EachFrame, executor, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя группы задачи</param>
			/// <param name="executor">Исполнитель группы задач</param>
			/// <param name="list">Список задач</param>
			//---------------------------------------------------------------------------------------------------------
			public CGroupTask(String name, CTaskGroupExecutor executor, params ILotusTask[] list)
				: this(name, TTaskMethod.EachFrame, executor, list)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя группы задачи</param>
			/// <param name="method">Способ выполнения задачи</param>
			/// <param name="executor">Исполнитель группы задач</param>
			/// <param name="list">Список задач</param>
			//---------------------------------------------------------------------------------------------------------
			public CGroupTask(String name, TTaskMethod method, CTaskGroupExecutor executor, params ILotusTask[] list)
			{
				mName = name;
				mExecutor = executor;
				mTasks = new List<CTaskHolder>();
				AddList(method, list);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка способа выполнения всех задач в группе
			/// </summary>
			/// <param name="method_mode">Способ выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetMethodMode(TTaskMethod method_mode)
			{
				for (Int32 i = 0; i < mTasks.Count; i++)
				{
					mTasks[i].MethodMode = method_mode;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление задачи в группу
			/// </summary>
			/// <param name="task">Задача</param>
			//---------------------------------------------------------------------------------------------------------
			public void Add(ILotusTask task)
			{
				// Проверка против дублирования
				for (Int32 i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Task == task)
					{
#if UNITY_EDITOR
						UnityEngine.Debug.LogWarningFormat("Task: <{0}> already is present at the list <{1}>", task.ToString(),
							Name);
#endif
						return;
					}
				}

				CTaskHolder task_holder = mExecutor.TaskHolderPools.Take();
				task_holder.Task = task;
				mTasks.Add(task_holder);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление задачи в группу
			/// </summary>
			/// <param name="task">Задача</param>
			/// <param name="method">Способ выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public void Add(ILotusTask task, TTaskMethod method)
			{
				// Проверка против дублирования
				for (Int32 i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Task == task)
					{
#if UNITY_EDITOR
						UnityEngine.Debug.LogWarningFormat("Task: <{0}> already is present at the list <{1}>", task.ToString(),
							Name);
#endif
						mTasks[i].MethodMode = method;
						return;
					}
				}

				CTaskHolder task_holder = mExecutor.TaskHolderPools.Take();
				task_holder.Task = task;
				task_holder.MethodMode = method;
				mTasks.Add(task_holder);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление списка задач в группу
			/// </summary>
			/// <param name="list">Список задач</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddList(params ILotusTask[] list)
			{
				for (Int32 i = 0; i < list.Length; i++)
				{
					Add(list[i]);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление списка задач в группу
			/// </summary>
			/// <param name="method">Способ выполнения задачи</param>
			/// <param name="list">Список задач</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddList(TTaskMethod method, params ILotusTask[] list)
			{
				for (Int32 i = 0; i < list.Length; i++)
				{
					Add(list[i], method);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление задачи
			/// </summary>
			/// <param name="task">Задача</param>
			//---------------------------------------------------------------------------------------------------------
			public void Remove(ILotusTask task)
			{
				for (Int32 i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Task == task)
					{
						// 1) Возвращаем в пул
						CTaskHolder task_holder = mTasks[i];
						mExecutor.TaskHolderPools.Release(task_holder);

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
			public void Remove(String task_name)
			{
				for (Int32 i = 0; i < mTasks.Count; i++)
				{
					if (mTasks[i].Name == task_name)
					{
						// 1) Возвращаем в пул
						CTaskHolder task_holder = mTasks[i];
						mExecutor.TaskHolderPools.Release(task_holder);

						// 2) Удаляем
						mTasks.RemoveAt(i);
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения группы задач
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Run()
			{
				mCurrentTaskIndex = 0;
				mCurrentTask = mTasks[mCurrentTaskIndex];
				mIsRunning = true;
				mIsPause = false;
				mIsCompleted = false;
				mIsDelayStart = mDelayStart > 0;
				mStartTaskTime = 0;
				mCountTaskExecute = 0;

				if (mIsDelayStart == false)
				{
					if (mExecuteMode == TTaskExecuteMode.Parallel)
					{
						for (Int32 i = 0; i < mTasks.Count; i++)
						{
							mTasks[i].RunTask();
						}
					}
					else
					{
						mCurrentTask.RunTask();
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Принудительная остановка выполнения всех задач
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Stop()
			{
				mIsRunning = false;
				mIsCompleted = true;
				mIsPause = false;
				for (Int32 i = 0; i < mTasks.Count; i++)
				{
					mTasks[i].StopTask();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка данных всех задач группы
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Reset()
			{
				mIsRunning = false;
				mIsCompleted = true;
				mIsPause = false;
				for (Int32 i = 0; i < mTasks.Count; i++)
				{
					mTasks[i].ResetTask();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выполнение задач параллельно
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ExecuteInParallel()
			{
				if (mIsDelayStart)
				{
#if (UNITY_2017_1_OR_NEWER)
					mStartTaskTime += UnityEngine.Time.deltaTime;
#endif
					if (mStartTaskTime > mDelayStart)
					{
						for (Int32 i = 0; i < mTasks.Count; i++)
						{
							mTasks[i].RunTask();
						}

						mIsDelayStart = false;
					}
				}
				else
				{
					// Если
					Boolean is_completed = true;
					Boolean is_all_completed = true;

					for (Int32 i = 0; i < mTasks.Count; i++)
					{
						// Проверка на исполнение задачи
						is_completed = mTasks[i].IsTaskCompleted;

						// Проверяем на то что все задачи точно выполнены
						if(is_all_completed)
						{
							is_all_completed = is_completed;
						}

						if (is_completed)
						{
							mCountTaskExecute++;

							if(mIsEachTaskCompletedHandler)
							{
								// Если был обработчик завершения каждой задачи группы
								if (mExecutor.GroupTaskHandlersEachTaskCompleted.ContainsKey(mName))
								{
									mExecutor.GroupTaskHandlersEachTaskCompleted[mName](mTasks[i].Task);
								}
							}
						}
						if (!is_completed && mIsRunning && mIsPause == false)
						{
							mTasks[i].ExecuteTask();
						}
					}

					// Все задачи выполнены
					if (is_all_completed)
					{
						mIsCompleted = true;
						mIsRunning = false;

						// Информируем
						if (mOnGroupTaskCompleted != null) mOnGroupTaskCompleted(mName);

						// Если был задан обработчик завершения задач, то вызываем его
						if (mExecutor.GroupTaskHandlersCompleted.ContainsKey(mName))
						{
							mExecutor.GroupTaskHandlersCompleted[mName]();
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выполнение задач последовательно
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ExecuteSequentially()
			{
				if (mIsDelayStart)
				{
#if (UNITY_2017_1_OR_NEWER)
					mStartTaskTime += UnityEngine.Time.deltaTime;
#endif
					if (mStartTaskTime > mDelayStart)
					{
						mCurrentTask.RunTask();
						mIsDelayStart = false;
					}
				}
				else
				{

					// Если есть задача
					if (mIsRunning && mIsPause == false)
					{
						mCurrentTask.ExecuteTask();
					}

					// Если задача завершена
					if (mCurrentTask.IsTaskCompleted)
					{
						if (mIsEachTaskCompletedHandler)
						{
							// Если был обработчик завершения каждой задачи группы
							if (mExecutor.GroupTaskHandlersEachTaskCompleted.ContainsKey(mName))
							{
								mExecutor.GroupTaskHandlersEachTaskCompleted[mName](mCurrentTask.Task);
							}
						}

						// Следующая задача
						mCurrentTaskIndex++;

						// Если это была последняя задача
						if (mCurrentTaskIndex == mTasks.Count)
						{
							mIsCompleted = true;
							mIsRunning = false;

							// Информируем
							if (mOnGroupTaskCompleted != null) mOnGroupTaskCompleted(mName);

							// Если был прямой обработчик по имени задачи вызываем
							if (mExecutor.GroupTaskHandlersCompleted.ContainsKey(mName))
							{
								mExecutor.GroupTaskHandlersCompleted[mName]();
							}

							return;
						}

						// Если не последняя исполняем следующую
						mCurrentTask = mTasks[mCurrentTaskIndex];
						mCurrentTask.RunTask();
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка списка задач от всех задач
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Clear()
			{
				for (Int32 i = 0; i < mTasks.Count; i++)
				{
					// 1) Возвращаем в пул
					CTaskHolder task_holder = mTasks[i];
					mExecutor.TaskHolderPools.Release(task_holder);
				}

				mTasks.Clear();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================