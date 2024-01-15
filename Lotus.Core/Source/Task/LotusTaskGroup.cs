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
// Последнее изменение от 30.04.2023
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
		/** \addtogroup CoreTask
		*@{*/
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
			protected internal String _name;
			protected internal List<CTaskHolder> _tasks;
			protected internal TTaskExecuteMode _executeMode;
			protected internal Single _delayStart;

			// Переменные состояния
			protected internal Boolean _isCompleted;
			protected internal Boolean _isRunning;
			protected internal Boolean _isPause;
			protected internal Boolean _isDelayStart;
			protected internal CTaskHolder _currentTask;
			protected internal Int32 _currentTaskIndex;
			protected internal Single _startTaskTime;
			protected internal Int32 _countTaskExecute;
			protected internal Boolean _isEachTaskCompletedHandler;

			// Исполнитель группы задач
			protected internal CTaskGroupExecutor _executor;

			// События
			protected internal Action<String> _onGroupTaskStarted;
			protected internal Action<String> _onGroupTaskCompleted;
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
				get { return _name; }
				set { _name = value; }
			}

			/// <summary>
			/// Список задач группы
			/// </summary>
			public List<CTaskHolder> Tasks
			{
				get { return _tasks; }
			}

			/// <summary>
			/// Режим выполнения задач группы
			/// </summary>
			public TTaskExecuteMode ExecuteMode
			{
				get { return _executeMode; }
				set { _executeMode = value; }
			}

			/// <summary>
			/// Задержка в секундах при выполнение группы задач
			/// </summary>
			public Single DelayStart
			{
				get { return _delayStart; }
				set
				{
					_delayStart = value;
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
				get { return _isEachTaskCompletedHandler; }
				set
				{
					_isEachTaskCompletedHandler = value;
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
				get { return _isCompleted; }
			}

			/// <summary>
			/// Статус выполнения задач группы
			/// </summary>
			public Boolean IsRunning
			{
				get { return _isRunning; }
			}

			/// <summary>
			/// Пауза выполнения задач группы
			/// </summary>
			public Boolean IsPause
			{
				get { return _isPause; }
				set
				{
					_isPause = value;
				}
			}

			/// <summary>
			/// Текущая исполняемая задача
			/// </summary>
			public ILotusTask CurrentTask
			{
				get { return _currentTask; }
			}

			/// <summary>
			/// Индекс текущей исполняемой задачи
			/// </summary>
			public Int32 CurrentTaskIndex
			{
				get { return _currentTaskIndex; }
			}

			//
			// ИСПОЛНИТЕЛЬ ГРУППЫ
			//
			/// <summary>
			/// Исполнитель группы задач
			/// </summary>
			public CTaskGroupExecutor Executor
			{
				get { return _executor; }
			}

			//
			// СОБЫТИЯ
			//
			/// <summary>
			/// Событие для нотификации о начале выполнения всех задач группы. Аргумент - имя группы задач
			/// </summary>
			public Action<String> OnGroupTaskStarted
			{
				get { return _onGroupTaskStarted; }
				set { _onGroupTaskStarted = value; }
			}

			/// <summary>
			/// Событие для нотификации об окончании выполнения всех задач группы. Аргумент - имя группы задач
			/// </summary>
			public Action<String> OnGroupTaskCompleted
			{
				get { return _onGroupTaskCompleted; }
				set { _onGroupTaskCompleted = value; }
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
			public CGroupTask(String name, TTaskMethod method, CTaskGroupExecutor executor, params ILotusTask[]? list)
			{
				_name = name;
				_executor = executor;
				_tasks = new List<CTaskHolder>();
				AddList(method, list);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка способа выполнения всех задач в группе
			/// </summary>
			/// <param name="methodMode">Способ выполнения задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetMethodMode(TTaskMethod methodMode)
			{
				for (var i = 0; i < _tasks.Count; i++)
				{
					_tasks[i].MethodMode = methodMode;
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
				for (var i = 0; i < _tasks.Count; i++)
				{
					if (_tasks[i].Task == task)
					{
#if UNITY_EDITOR
						UnityEngine.Debug.LogWarningFormat("Task: <{0}> already is present at the list <{1}>", task.ToString(),
							Name);
#endif
						return;
					}
				}

				CTaskHolder task_holder = _executor.TaskHolderPools.Take();
				task_holder.Task = task;
				_tasks.Add(task_holder);
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
				for (var i = 0; i < _tasks.Count; i++)
				{
					if (_tasks[i].Task == task)
					{
#if UNITY_EDITOR
						UnityEngine.Debug.LogWarningFormat("Task: <{0}> already is present at the list <{1}>", task.ToString(),
							Name);
#endif
						_tasks[i].MethodMode = method;
						return;
					}
				}

				CTaskHolder task_holder = _executor.TaskHolderPools.Take();
				task_holder.Task = task;
				task_holder.MethodMode = method;
				_tasks.Add(task_holder);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление списка задач в группу
			/// </summary>
			/// <param name="list">Список задач</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddList(params ILotusTask[] list)
			{
				for (var i = 0; i < list.Length; i++)
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
			public void AddList(TTaskMethod method, params ILotusTask[]? list)
			{
				if (list == null) return;

				for (var i = 0; i < list.Length; i++)
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
				for (var i = 0; i < _tasks.Count; i++)
				{
					if (_tasks[i].Task == task)
					{
						// 1) Возвращаем в пул
						CTaskHolder task_holder = _tasks[i];
						_executor.TaskHolderPools.Release(task_holder);

						// 2) Удаляем
						_tasks.RemoveAt(i);
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
			public void Remove(String taskName)
			{
				for (var i = 0; i < _tasks.Count; i++)
				{
					if (_tasks[i].Name == taskName)
					{
						// 1) Возвращаем в пул
						CTaskHolder task_holder = _tasks[i];
						_executor.TaskHolderPools.Release(task_holder);

						// 2) Удаляем
						_tasks.RemoveAt(i);
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
				_currentTaskIndex = 0;
				_currentTask = _tasks[_currentTaskIndex];
				_isRunning = true;
				_isPause = false;
				_isCompleted = false;
				_isDelayStart = _delayStart > 0;
				_startTaskTime = 0;
				_countTaskExecute = 0;

				if (_isDelayStart == false)
				{
					if (_executeMode == TTaskExecuteMode.Parallel)
					{
						for (var i = 0; i < _tasks.Count; i++)
						{
							_tasks[i].RunTask();
						}
					}
					else
					{
						_currentTask.RunTask();
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
				_isRunning = false;
				_isCompleted = true;
				_isPause = false;
				for (var i = 0; i < _tasks.Count; i++)
				{
					_tasks[i].StopTask();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка данных всех задач группы
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Reset()
			{
				_isRunning = false;
				_isCompleted = true;
				_isPause = false;
				for (var i = 0; i < _tasks.Count; i++)
				{
					_tasks[i].ResetTask();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Выполнение задач параллельно
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ExecuteInParallel()
			{
				if (_isDelayStart)
				{
#if UNITY_2017_1_OR_NEWER
					_startTaskTime += UnityEngine.Time.deltaTime;
#endif
					if (_startTaskTime > _delayStart)
					{
						for (var i = 0; i < _tasks.Count; i++)
						{
							_tasks[i].RunTask();
						}

						_isDelayStart = false;
					}
				}
				else
				{
					// Если
					var is_completed = true;
					var is_all_completed = true;

					for (var i = 0; i < _tasks.Count; i++)
					{
						// Проверка на исполнение задачи
						is_completed = _tasks[i].IsTaskCompleted;

						// Проверяем на то что все задачи точно выполнены
						if(is_all_completed)
						{
							is_all_completed = is_completed;
						}

						if (is_completed)
						{
							_countTaskExecute++;

							if(_isEachTaskCompletedHandler)
							{
								// Если был обработчик завершения каждой задачи группы
								if (_executor.GroupTaskHandlersEachTaskCompleted.ContainsKey(_name))
								{
									_executor.GroupTaskHandlersEachTaskCompleted[_name](_tasks[i].Task);
								}
							}
						}
						if (!is_completed && _isRunning && _isPause == false)
						{
							_tasks[i].ExecuteTask();
						}
					}

					// Все задачи выполнены
					if (is_all_completed)
					{
						_isCompleted = true;
						_isRunning = false;

						// Информируем
						if (_onGroupTaskCompleted != null) _onGroupTaskCompleted(_name);

						// Если был задан обработчик завершения задач, то вызываем его
						if (_executor.GroupTaskHandlersCompleted.ContainsKey(_name))
						{
							_executor.GroupTaskHandlersCompleted[_name]();
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
				if (_isDelayStart)
				{
#if UNITY_2017_1_OR_NEWER
					_startTaskTime += UnityEngine.Time.deltaTime;
#endif
					if (_startTaskTime > _delayStart)
					{
						_currentTask.RunTask();
						_isDelayStart = false;
					}
				}
				else
				{

					// Если есть задача
					if (_isRunning && _isPause == false)
					{
						_currentTask.ExecuteTask();
					}

					// Если задача завершена
					if (_currentTask.IsTaskCompleted)
					{
						if (_isEachTaskCompletedHandler)
						{
							// Если был обработчик завершения каждой задачи группы
							if (_executor.GroupTaskHandlersEachTaskCompleted.ContainsKey(_name))
							{
								_executor.GroupTaskHandlersEachTaskCompleted[_name](_currentTask.Task);
							}
						}

						// Следующая задача
						_currentTaskIndex++;

						// Если это была последняя задача
						if (_currentTaskIndex == _tasks.Count)
						{
							_isCompleted = true;
							_isRunning = false;

							// Информируем
							if (_onGroupTaskCompleted != null) _onGroupTaskCompleted(_name);

							// Если был прямой обработчик по имени задачи вызываем
							if (_executor.GroupTaskHandlersCompleted.ContainsKey(_name))
							{
								_executor.GroupTaskHandlersCompleted[_name]();
							}

							return;
						}

						// Если не последняя исполняем следующую
						_currentTask = _tasks[_currentTaskIndex];
						_currentTask.RunTask();
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
				for (var i = 0; i < _tasks.Count; i++)
				{
					// 1) Возвращаем в пул
					CTaskHolder task_holder = _tasks[i];
					_executor.TaskHolderPools.Release(task_holder);
				}

				_tasks.Clear();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================