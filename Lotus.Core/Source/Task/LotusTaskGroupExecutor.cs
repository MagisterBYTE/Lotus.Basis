using System;
using System.Collections.Generic;
using System.Text;

namespace Lotus.Core
{
    /** \addtogroup CoreTask
	*@{*/
    /// <summary>
    /// Исполнитель группы задачи.
    /// </summary>
    /// <remarks>
    /// Реализация исполнителя для управления процессом выполнения задач в группе.
    /// Метод исполнителя нужно вызывать вручную в соответствующих местах.
    /// </remarks>
    public class TaskGroupExecutor : ILotusTaskExecutor
    {
        #region Static methods
        /// <summary>
        /// Конструирование класса-оболочки для хранения определенной задачи.
        /// </summary>
        /// <returns>Оболочка для хранения определенной задачи.</returns>
        private static CTaskHolder ConstructorTaskHolder()
        {
            return new CTaskHolder(true);
        }
        #endregion

        #region Fields
        protected internal string _name;
        protected internal PoolManager<CTaskHolder> _taskHolderPools;
        protected internal List<GroupTask> _groupTasks;
        protected internal Dictionary<string, Action> _groupTaskHandlersCompleted;
        protected internal Dictionary<string, Action<ILotusTask>> _groupTaskHandlersEachTaskCompleted;
        #endregion

        #region Properties
        /// <summary>
        /// Имя исполнителя групп задач.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Пул объектов типа оболочки для хранения задачи.
        /// </summary>
        public PoolManager<CTaskHolder> TaskHolderPools
        {
            get { return _taskHolderPools; }
        }

        /// <summary>
        /// Список всех групп задач.
        /// </summary>
        public List<GroupTask> GroupTasks
        {
            get { return _groupTasks; }
        }

        /// <summary>
        /// Словарь всех обработчиков события окончания выполнения группы задачи.
        /// </summary>
        public Dictionary<string, Action> GroupTaskHandlersCompleted
        {
            get { return _groupTaskHandlersCompleted; }
        }

        /// <summary>
        /// Словарь всех обработчиков события окончания выполнения каждой задачи группы.
        /// </summary>
        public Dictionary<string, Action<ILotusTask>> GroupTaskHandlersEachTaskCompleted
        {
            get { return _groupTaskHandlersEachTaskCompleted; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public TaskGroupExecutor()
            : this("")
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя исполнителя задач.</param>
        public TaskGroupExecutor(string name)
        {
            _taskHolderPools = new PoolManager<CTaskHolder>(10, ConstructorTaskHolder);
            _groupTasks = new List<GroupTask>(10);
            _groupTaskHandlersCompleted = new Dictionary<string, Action>(10);
            _groupTaskHandlersEachTaskCompleted = new Dictionary<string, Action<ILotusTask>>(10);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Обновление центрального диспетчера выполнения задач каждый кадр.
        /// </summary>
        public void OnUpdate()
        {
            // Выполняем отдельные группы задачи каждый кадр
            for (var i = 0; i < _groupTasks.Count; i++)
            {
                var group_task = _groupTasks[i];

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

        /// <summary>
        /// Текущий статус.
        /// </summary>
        /// <returns>Статус всех задач сформированных в текстовое представление.</returns>
        public string GetStatus()
        {
            var str = new StringBuilder(200);
            str.AppendLine("Всего групп задач: " + _groupTasks.Count.ToString());
            for (var ig = 0; ig < _groupTasks.Count; ig++)
            {
                str.AppendLine("Группа: " + _groupTasks[ig].Name + "(задач: " +
                    _groupTasks[ig].Tasks.Count.ToString() + ")");
            }

            return str.ToString();
        }
        #endregion

        #region Task group methods
        /// <summary>
        /// Получение группы задачи по имени.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <returns>Найденная группа задач или null.</returns>
        public virtual GroupTask? GetGroupTask(string groupName)
        {
            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    return _groupTasks[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Добавление(существующей) группы задачи.
        /// </summary>
        /// <param name="groupTask">Группа задач.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask AddGroupTask(GroupTask groupTask)
        {
            _groupTasks.Add(groupTask);
            return groupTask;
        }

        /// <summary>
        /// Добавление(существующей) группы задачи.
        /// </summary>
        /// <param name="groupTask">Группа задач.</param>
        /// <param name="onCompletedEachTask">Обработчик завершения каждой задачи группы.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask AddGroupTask(GroupTask groupTask, Action<ILotusTask> onCompletedEachTask)
        {
            _groupTasks.Add(groupTask);

            if (onCompletedEachTask != null)
            {
                if (_groupTaskHandlersEachTaskCompleted.ContainsKey(groupTask.Name))
                {
                    _groupTaskHandlersEachTaskCompleted[groupTask.Name] = onCompletedEachTask;
                }
                else
                {
                    _groupTaskHandlersEachTaskCompleted.Add(groupTask.Name, onCompletedEachTask);
                }
            }

            return groupTask;
        }

        /// <summary>
        /// Добавление(создание) группы задачи выполняемых параллельно каждый кадр.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="list">Список задач группы.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask AddGroupTask(string groupName, params ILotusTask[] list)
        {
            return AddGroupTask(groupName, TTaskExecuteMode.Parallel, TTaskMethod.EachFrame, list);
        }

        /// <summary>
        /// Добавление(создание) группы задачи выполняемых параллельно.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="method">Способ выполнения задач группы.</param>
        /// <param name="list">Список задач группы.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask AddGroupTask(string groupName, TTaskMethod method, params ILotusTask[] list)
        {
            return AddGroupTask(groupName, TTaskExecuteMode.Parallel, method, list);
        }

        /// <summary>
        /// Добавление(создание) группы задачи.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="method">Способ выполнения задач группы.</param>
        /// <param name="list">Список задач группы.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask AddGroupTask(string groupName, TTaskExecuteMode executeMode, TTaskMethod method, params ILotusTask[] list)
        {
            var task = new GroupTask(groupName, method, this, list);
            task.ExecuteMode = executeMode;
            _groupTasks.Add(task);
            return task;
        }

        /// <summary>
        /// Получение существующей группы задач или создание новой группы задач с указанными задачами и параметрами.
        /// </summary>
        /// <param name="groupName">Имя задачи.</param>
        /// <param name="task">Задача.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask GetGroupTaskExistsTask(string groupName, ILotusTask task)
        {
            return GetGroupTaskExistsTask(groupName, TTaskExecuteMode.Parallel, TTaskMethod.EachFrame, task);
        }

        /// <summary>
        /// Получение существующей группы задач или создание новой группы задач с указанными задачами и параметрами.
        /// </summary>
        /// <param name="groupName">Имя задачи.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="method">Способ выполнения группы задач.</param>
        /// <param name="task">Задача.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask GetGroupTaskExistsTask(string groupName, TTaskExecuteMode executeMode, TTaskMethod method, ILotusTask task)
        {
            GroupTask? group_task = null;

            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    group_task = _groupTasks[i];
                    break;
                }
            }

            if (group_task == null)
            {
                group_task = new GroupTask(groupName, method, this, task);
                group_task.ExecuteMode = executeMode;
                _groupTasks.Add(group_task);
            }

            return group_task;
        }

        /// <summary>
        /// Получение существующей группы задач или создание новой группы задач с указанными задачами и параметрами.
        /// </summary>
        /// <param name="groupName">Имя задачи.</param>
        /// <param name="tasks">Задачи.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask GetGroupTaskExistsTasks(string groupName, params ILotusTask[] tasks)
        {
            return GetGroupTaskExistsTasks(groupName, TTaskExecuteMode.Parallel, TTaskMethod.EachFrame, tasks);
        }

        /// <summary>
        /// Получение существующей группы задач или создание новой группы задач с указанными задачами и параметрами.
        /// </summary>
        /// <param name="groupName">Имя задачи.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="tasks">Задачи.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask GetGroupTaskExistsTasks(string groupName, TTaskExecuteMode executeMode, params ILotusTask[] tasks)
        {
            return GetGroupTaskExistsTasks(groupName, executeMode, TTaskMethod.EachFrame, tasks);
        }

        /// <summary>
        /// Получение существующей группы задач или создание новой группы задач с указанными задачами и параметрами.
        /// </summary>
        /// <param name="groupName">Имя задачи.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="method">Способ выполнения группы задач.</param>
        /// <param name="tasks">Задачи.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask GetGroupTaskExistsTasks(string groupName, TTaskExecuteMode executeMode, TTaskMethod method, params ILotusTask[] tasks)
        {
            GroupTask? group_task = null;

            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    group_task = _groupTasks[i];
                    break;
                }
            }

            if (group_task == null)
            {
                group_task = new GroupTask(groupName, method, this, tasks);
                group_task.ExecuteMode = executeMode;
                _groupTasks.Add(group_task);
            }

            return group_task;
        }

        /// <summary>
        /// Добавление в существующую группу задач дополнительной задачи или создание новой группы задач.
        /// </summary>
        /// <param name="groupName">Имя задачи.</param>
        /// <param name="task">Задача.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask AddGroupTaskExistsTask(string groupName, ILotusTask task)
        {
            return AddGroupTaskExistsTask(groupName, TTaskExecuteMode.Parallel, TTaskMethod.EachFrame, task);
        }

        /// <summary>
        /// Добавление в существующую группу задач дополнительной задачи или создание новой группы задач.
        /// </summary>
        /// <param name="groupName">Имя задачи.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="method">Способ выполнения группы задач.</param>
        /// <param name="task">Задача.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask AddGroupTaskExistsTask(string groupName, TTaskExecuteMode executeMode, TTaskMethod method, ILotusTask task)
        {
            GroupTask? group_task = null;

            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    group_task = _groupTasks[i];
                    break;
                }
            }

            if (group_task == null)
            {
                group_task = new GroupTask(groupName, method, this, task);
                group_task.ExecuteMode = executeMode;
                _groupTasks.Add(group_task);
            }
            else
            {
                group_task.Add(task);
            }

            return group_task;
        }

        /// <summary>
        /// Добавление в существующую группу задач дополнительной задачи или создание новой группы задач.
        /// </summary>
        /// <param name="groupName">Имя задачи.</param>
        /// <param name="tasks">Задачи.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask AddGroupTaskExistsTasks(string groupName, params ILotusTask[] tasks)
        {
            return AddGroupTaskExistsTasks(groupName, TTaskExecuteMode.Parallel, TTaskMethod.EachFrame, tasks);
        }

        /// <summary>
        /// Добавление в существующую группу задач дополнительной задачи или создание новой группы задач.
        /// </summary>
        /// <param name="groupName">Имя задачи.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="tasks">Задачи.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask AddGroupTaskExistsTasks(string groupName, TTaskExecuteMode executeMode, params ILotusTask[] tasks)
        {
            return AddGroupTaskExistsTasks(groupName, executeMode, TTaskMethod.EachFrame, tasks);
        }

        /// <summary>
        /// Добавление в существующую группу задач дополнительной задачи или создание новой группы задач.
        /// </summary>
        /// <param name="groupName">Имя задачи.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="method">Способ выполнения группы задач.</param>
        /// <param name="tasks">Задачи.</param>
        /// <returns>Группа задач.</returns>
        public virtual GroupTask AddGroupTaskExistsTasks(string groupName, TTaskExecuteMode executeMode, TTaskMethod method, params ILotusTask[] tasks)
        {
            GroupTask? group_task = null;

            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    group_task = _groupTasks[i];
                    break;
                }
            }

            if (group_task == null)
            {
                group_task = new GroupTask(groupName, method, this, tasks);
                group_task.ExecuteMode = executeMode;
                _groupTasks.Add(group_task);
            }
            else
            {
                group_task.AddList(tasks);
                group_task.ExecuteMode = executeMode;
                group_task.SetMethodMode(method);
            }

            return group_task;
        }

        /// <summary>
        /// Удаление группы задачи.
        /// </summary>
        /// <param name="groupTask">Группа задач.</param>
        public virtual void RemoveGroupTask(GroupTask groupTask)
        {
            if (_groupTasks.Remove(groupTask))
            {
                // Удаляем все связанные задачи
                groupTask.Clear();
            }
        }

        /// <summary>
        /// Удаление группы задачи.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        public virtual void RemoveGroupTask(string groupName)
        {
            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    _groupTasks[i].Clear();
                    _groupTasks.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Очистка группы от задач.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        public virtual void ClearGroupTask(string groupName)
        {
            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    _groupTasks[i].Clear();
                    return;
                }
            }
        }

        /// <summary>
        /// Запуск выполнения группы задачи параллельно.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <returns>Запущенная группа задач или null.</returns>
        public virtual GroupTask RunGroupTask(string groupName)
        {
            return RunGroupTask(groupName, 0.0f, null);
        }

        /// <summary>
        /// Запуск выполнения группы задачи параллельно.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <returns>Запущенная группа задач или null.</returns>
        public virtual GroupTask RunGroupTask(string groupName, TTaskExecuteMode executeMode)
        {
            return RunGroupTask(groupName, executeMode, 0.0f, null);
        }

        /// <summary>
        /// Запуск выполнения группы задачи.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="delayStart">Задержка в секундах начало выполнения задач группы.</param>
        /// <returns>Запущенная группа задач или null.</returns>
        public virtual GroupTask RunGroupTask(string groupName, float delayStart)
        {
            return RunGroupTask(groupName, delayStart, null);
        }

        /// <summary>
        /// Запуск выполнения группы задачи.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="delayStart">Задержка в секундах начало выполнения задач группы.</param>
        /// <returns>Запущенная группа задач или null.</returns>
        public virtual GroupTask RunGroupTask(string groupName, TTaskExecuteMode executeMode, float delayStart)
        {
            return RunGroupTask(groupName, executeMode, delayStart, null);
        }

        /// <summary>
        /// Запуск выполнения группы задачи.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="onCompleted">Обработчик события окончания выполнения задач группы.</param>
        /// <returns>Запущенная группа задач или null.</returns>
        public virtual GroupTask RunGroupTask(string groupName, Action onCompleted)
        {
            return RunGroupTask(groupName, 0.0f, onCompleted);
        }

        /// <summary>
        /// Запуск выполнения группы задачи.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="onCompleted">Обработчик события окончания выполнения задач группы.</param>
        /// <returns>Запущенная группа задач или null.</returns>
        public virtual GroupTask RunGroupTask(string groupName, TTaskExecuteMode executeMode, Action onCompleted)
        {
            return RunGroupTask(groupName, executeMode, 0.0f, onCompleted);
        }

        /// <summary>
        /// Запуск выполнения группы задачи.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="delayStart">Задержка в секундах начало выполнения задач группы.</param>
        /// <param name="onCompleted">Обработчик события окончания выполнения задач группы.</param>
        /// <returns>Запущенная группа задач или null.</returns>
        public virtual GroupTask RunGroupTask(string groupName, float delayStart, Action? onCompleted)
        {
            GroupTask? group_task = null;
            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    group_task = _groupTasks[i];
                    break;
                }
            }

            if (group_task != null)
            {
                if (onCompleted != null)
                {
                    if (_groupTaskHandlersCompleted.ContainsKey(group_task.Name))
                    {
                        _groupTaskHandlersCompleted[group_task.Name] = onCompleted;
                    }
                    else
                    {
                        _groupTaskHandlersCompleted.Add(group_task.Name, onCompleted);
                    }
                }

                group_task.DelayStart = delayStart;
                group_task.Run();
            }

            return group_task!;
        }

        /// <summary>
        /// Запуск выполнения группы задачи.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="delayStart">Задержка в секундах начало выполнения задач группы.</param>
        /// <param name="onCompleted">Обработчик события окончания выполнения задач группы.</param>
        /// <returns>Запущенная группа задач или null.</returns>
        public virtual GroupTask RunGroupTask(string groupName, TTaskExecuteMode executeMode, float delayStart, Action? onCompleted)
        {
            GroupTask? group_task = null;
            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    group_task = _groupTasks[i];
                    break;
                }
            }

            if (group_task != null)
            {
                if (onCompleted != null)
                {
                    if (_groupTaskHandlersCompleted.ContainsKey(group_task.Name))
                    {
                        _groupTaskHandlersCompleted[group_task.Name] = onCompleted;
                    }
                    else
                    {
                        _groupTaskHandlersCompleted.Add(group_task.Name, onCompleted);
                    }
                }

                group_task.ExecuteMode = executeMode;
                group_task.DelayStart = delayStart;
                group_task.Run();
            }

            return group_task!;
        }

        /// <summary>
        /// Запуск выполнения группы задачи.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="executeMode">Режим выполнения группы задач.</param>
        /// <param name="method">Способ выполнения группы задач.</param>
        /// <param name="delayStart">Задержка в секундах начало выполнения задач группы.</param>
        /// <param name="onCompleted">Обработчик события окончания выполнения задач группы.</param>
        /// <returns>Запущенная группа задач или null.</returns>
        public virtual GroupTask RunGroupTask(string groupName, TTaskExecuteMode executeMode, TTaskMethod method, float delayStart, Action? onCompleted)
        {
            GroupTask? group_task = null;
            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    group_task = _groupTasks[i];
                    break;
                }
            }

            if (group_task != null)
            {
                if (onCompleted != null)
                {
                    if (_groupTaskHandlersCompleted.ContainsKey(group_task.Name))
                    {
                        _groupTaskHandlersCompleted[group_task.Name] = onCompleted;
                    }
                    else
                    {
                        _groupTaskHandlersCompleted.Add(group_task.Name, onCompleted);
                    }
                }

                group_task.SetMethodMode(method);
                group_task.ExecuteMode = executeMode;
                group_task.DelayStart = delayStart;
                group_task.Run();
            }

            return group_task!;
        }

        /// <summary>
        /// Пауза выполнения группы задачи.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        /// <param name="pause">Статус паузы.</param>
        public virtual void PauseGroupTask(string groupName, bool pause)
        {
            GroupTask? group_task = null;
            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    group_task = _groupTasks[i];
                    break;
                }
            }

            if (group_task != null)
            {
                group_task.IsPause = pause;
            }
        }

        /// <summary>
        /// Принудительная остановка выполнения всех задач группы.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        public virtual void StopGroupTask(string groupName)
        {
            GroupTask? group_task = null;
            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    group_task = _groupTasks[i];
                    break;
                }
            }

            if (group_task != null)
            {
                group_task.Stop();
            }
        }

        /// <summary>
        /// Переустановка данных всех задач группы.
        /// </summary>
        /// <param name="groupName">Имя группы задач.</param>
        public virtual void ResetGroupTask(string groupName)
        {
            for (var i = 0; i < _groupTasks.Count; i++)
            {
                if (_groupTasks[i].Name == groupName)
                {
                    _groupTasks[i].Reset();
                    break;
                }
            }
        }
        #endregion
    }
    /**@}*/
}