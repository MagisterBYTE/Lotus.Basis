using System;
using System.Collections.Generic;
using System.Text;

namespace Lotus.Core
{
    /** \addtogroup CoreTask
	*@{*/
    /// <summary>
    ///  Определение интерфейса исполнителя задачи.
    /// </summary>
    public interface ILotusTaskExecutor : ILotusNameable
    {

    }

    /// <summary>
    /// Исполнитель задачи.
    /// </summary>
    /// <remarks>
    /// Реализация исполнителя для управления процессом выполнения задачи.
    /// Метод исполнителя нужно вызывать вручную в соответствующих местах.
    /// </remarks>
    public class TaskExecutor : ILotusTaskExecutor
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
        protected internal List<CTaskHolder> _tasks;
        protected internal Dictionary<string, Action> _taskHandlersCompleted;
        #endregion

        #region Properties
        /// <summary>
        /// Имя исполнителя задач.
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
        /// Список всех одиночных задач.
        /// </summary>
        public List<CTaskHolder> Tasks
        {
            get { return _tasks; }
        }

        /// <summary>
        /// Словарь всех обработчиков события окончания выполнения задачи.
        /// </summary>
        public Dictionary<string, Action> TaskHandlersCompleted
        {
            get { return _taskHandlersCompleted; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public TaskExecutor()
            : this("")
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя исполнителя задач.</param>
        public TaskExecutor(string name)
        {
            _taskHolderPools = new PoolManager<CTaskHolder>(10, ConstructorTaskHolder);
            _tasks = new List<CTaskHolder>(10);
            _taskHandlersCompleted = new Dictionary<string, Action>(10);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Обновление центрального диспетчера выполнения задач каждый кадр.
        /// </summary>
        public void OnUpdate()
        {
            // Выполняем отдельные задачи каждый кадр
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (!_tasks[i].IsTaskCompleted)
                {
                    _tasks[i].ExecuteTask();
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
            str.AppendLine("Всего задач: " + _tasks.Count.ToString());
            for (var it = 0; it < _tasks.Count; it++)
            {
                str.AppendLine("Задача: " + _tasks[it].Name);
            }

            return str.ToString();
        }
        #endregion

        #region Task methods
        /// <summary>
        /// Получение задачи по имени.
        /// </summary>
        /// <param name="taskName">Имя задачи.</param>
        /// <returns>Найденная задача или null.</returns>
        public virtual CTaskHolder? GetTask(string taskName)
        {
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Name == taskName)
                {
                    return _tasks[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Добавление новой задачи.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <param name="method">Способ выполнения задачи.</param>
        public virtual void AddTask(ILotusTask task, TTaskMethod method)
        {
            var task_holder = _taskHolderPools.Take();
            task_holder.Task = task;
            task_holder.MethodMode = method;
            _tasks.Add(task_holder);
        }

        /// <summary>
        /// Добавление новой задачи.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <param name="taskName">Имя задачи.</param>
        /// <param name="method">Способ выполнения задачи.</param>
        public virtual void AddTask(ILotusTask task, string taskName, TTaskMethod method)
        {
            var task_holder = _taskHolderPools.Take();
            task_holder.Name = taskName;
            task_holder.Task = task;
            task_holder.MethodMode = method;
            _tasks.Add(task_holder);
        }

        /// <summary>
        /// Удаление задачи.
        /// </summary>
        /// <param name="task">Задача.</param>
        public virtual void RemoveTask(ILotusTask task)
        {
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Task == task)
                {
                    // 1) Возвращаем в пул
                    var task_holder = _tasks[i];
                    _taskHolderPools.Release(task_holder);

                    // 2) Удаляем
                    _tasks.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Удаление задачи.
        /// </summary>
        /// <param name="taskName">Имя задачи.</param>
        public virtual void RemoveTask(string taskName)
        {
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Name == taskName)
                {
                    // 1) Возвращаем в пул
                    var task_holder = _tasks[i];
                    _taskHolderPools.Release(task_holder);

                    // 2) Удаляем
                    _tasks.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Запуск выполнения задачи.
        /// </summary>
        /// <param name="taskName">Имя задачи.</param>
        public virtual void RunTask(string taskName)
        {
            RunTask(taskName, 0.0f, null);
        }

        /// <summary>
        /// Запуск выполнения задачи.
        /// </summary>
        /// <param name="taskName">Имя задачи.</param>
        /// <param name="delayStart">Время задержки запуска выполнения задачи.</param>
        public virtual void RunTask(string taskName, float delayStart)
        {
            RunTask(taskName, delayStart, null);
        }

        /// <summary>
        /// Запуск выполнения задачи.
        /// </summary>
        /// <param name="taskName">Имя задачи.</param>
        /// <param name="delayStart">Время задержки запуска выполнения задачи.</param>
        /// <param name="onCompleted">Обработчик события окончания выполнения задачи.</param>
        public virtual void RunTask(string taskName, float delayStart, Action? onCompleted)
        {
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Name == taskName)
                {
                    if (onCompleted != null)
                    {
                        if (_taskHandlersCompleted.ContainsKey(taskName))
                        {
                            _taskHandlersCompleted[taskName] = onCompleted;
                        }
                        else
                        {
                            _taskHandlersCompleted.Add(taskName, onCompleted);
                        }
                    }

                    _tasks[i].DelayStart = delayStart;
                    _tasks[i].RunTask();
                    return;
                }
            }
        }

        /// <summary>
        /// Запуск выполнения задачи.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <param name="delayStart">Время задержки запуска выполнения задачи.</param>
        public virtual void RunTask(ILotusTask task, float delayStart)
        {
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Task == task)
                {
                    _tasks[i].DelayStart = delayStart;
                    _tasks[i].RunTask();
                    return;
                }
            }
        }

        /// <summary>
        /// Пауза выполнения задачи.
        /// </summary>
        /// <param name="taskName">Имя задачи.</param>
        /// <param name="pause">Статус паузы.</param>
        public virtual void PauseTask(string taskName, bool pause)
        {
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Name == taskName)
                {
                    _tasks[i].IsPause = pause;
                    return;
                }
            }
        }

        /// <summary>
        /// Принудительная остановка выполнения задачи.
        /// </summary>
        /// <param name="taskName">Имя задачи.</param>
        public virtual void StopTask(string taskName)
        {
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Name == taskName)
                {
                    _tasks[i].StopTask();
                    return;
                }
            }
        }

        /// <summary>
        /// Переустановка данных задачи.
        /// </summary>
        /// <param name="taskName">Имя задачи.</param>
        public virtual void ResetTask(string taskName)
        {
            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Name == taskName)
                {
                    _tasks[i].ResetTask();
                    return;
                }
            }
        }
        #endregion
    }
    /**@}*/
}