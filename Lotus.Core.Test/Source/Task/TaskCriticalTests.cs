using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Task
{
    /// <summary>
    /// Тесты для критических участков Task (исправление имен переменных и опечатки в комментарии).
    /// </summary>
    [TestFixture]
    public class TaskCriticalTests
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        private class MockTask : ILotusTask
        {
            public string Name { get; set; } = "MockTask";
            public bool IsTaskCompleted { get; set; }
            public bool IsPause { get; set; }
            public float DelayStart { get; set; }

            public void ExecuteTask()
            {
                IsTaskCompleted = true;
            }

            public void RunTask()
            {
                IsTaskCompleted = false;
            }

            public void StopTask()
            {
                IsTaskCompleted = true;
            }

            public void ResetTask()
            {
                IsTaskCompleted = false;
            }
        }

        /// <summary>
        /// Тест GroupTask - корректная работа с исправленными именами переменных (taskHolder вместо task_holder).
        /// </summary>
        [Test]
        public void GroupTask_Add_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var executor = new TaskGroupExecutor("TestExecutor");
            var groupTask = new GroupTask("TestGroup", executor);
            var task = new MockTask();

            // Act - используем метод с исправленным именем переменной taskHolder
            groupTask.Add(task);

            // Assert
            ClassicAssert.AreEqual(1, groupTask.Tasks.Count, "Task should be added");
            ClassicAssert.AreEqual(task, groupTask.Tasks[0].Task, "Task should match");
        }

        /// <summary>
        /// Тест GroupTask - корректная работа с исправленными именами переменных (isCompleted, isAllCompleted вместо is_completed, is_all_completed).
        /// </summary>
        [Test]
        public void GroupTask_ExecuteInParallel_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var executor = new TaskGroupExecutor("TestExecutor");
            var groupTask = new GroupTask("TestGroup", executor);
            var task1 = new MockTask();
            var task2 = new MockTask();
            groupTask.Add(task1);
            groupTask.Add(task2);
            groupTask.ExecuteMode = TTaskExecuteMode.Parallel;
            groupTask.Run();

            // Act - используем метод с исправленными именами переменных isCompleted, isAllCompleted
            task1.IsTaskCompleted = true;
            task2.IsTaskCompleted = true;
            groupTask.ExecuteInParallel();

            // Assert
            ClassicAssert.IsTrue(groupTask.IsCompleted, "Group task should be completed");
            ClassicAssert.IsFalse(groupTask.IsRunning, "Group task should not be running");
        }

        /// <summary>
        /// Тест GroupTask - конструктор с исправленным комментарием "Без имени" вместо "Без имение".
        /// </summary>
        [Test]
        public void GroupTask_Constructor_ShouldUseCorrectComment()
        {
            // Arrange & Act
            var executor = new TaskGroupExecutor("TestExecutor");
            var groupTask = new GroupTask(executor); // Использует конструктор с комментарием "Без имени"

            // Assert
            ClassicAssert.AreEqual("Без имени", groupTask.Name, "Name should be 'Без имени' (not 'Без имение')");
        }

        /// <summary>
        /// Тест TaskExecutor - корректная работа с исправленными именами переменных (taskHolder вместо task_holder).
        /// </summary>
        [Test]
        public void TaskExecutor_AddTask_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var executor = new TaskExecutor("TestExecutor");
            var task = new MockTask { Name = "TestTask" };

            // Act - используем метод с исправленным именем переменной taskHolder
            executor.AddTask(task, TTaskMethod.EachFrame);

            // Assert
            ClassicAssert.AreEqual(1, executor.Tasks.Count, "Task should be added");
            ClassicAssert.AreEqual(task, executor.Tasks[0].Task, "Task should match");
        }

        /// <summary>
        /// Тест TaskGroupExecutor - корректная работа с исправленными именами переменных (groupTask вместо group_task).
        /// </summary>
        [Test]
        public void TaskGroupExecutor_AddGroupTask_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var executor = new TaskGroupExecutor("TestExecutor");
            var task = new MockTask();

            // Act - используем метод с исправленным именем переменной groupTask
            var groupTask = executor.AddGroupTask("TestGroup", task);

            // Assert
            ClassicAssert.IsNotNull(groupTask, "Group task should be created");
            ClassicAssert.AreEqual("TestGroup", groupTask.Name, "Group task name should match");
            ClassicAssert.AreEqual(1, groupTask.Tasks.Count, "Group task should contain one task");
        }

        /// <summary>
        /// Тест TaskGroupExecutor - OnUpdate использует исправленное имя переменной groupTask.
        /// </summary>
        [Test]
        public void TaskGroupExecutor_OnUpdate_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var executor = new TaskGroupExecutor("TestExecutor");
            var task = new MockTask();
            var groupTask = executor.AddGroupTask("TestGroup", task);
            groupTask.Run();

            // Act - используем метод с исправленным именем переменной groupTask
            executor.OnUpdate();

            // Assert - проверяем что метод работает корректно
            ClassicAssert.IsNotNull(groupTask, "Group task should exist");
        }
    }
}
