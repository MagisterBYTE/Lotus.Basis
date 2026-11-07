using System;

namespace Lotus.Core.Memento
{
    /// <summary>
    /// Тесты для критических участков Memento (исправление имени переменной itemToRedo).
    /// </summary>
    public class MementoCriticalTests
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        private class TestMementoState : ILotusMementoState
        {
            public int Value { get; set; }
            public int UndoCount { get; private set; }
            public int RedoCount { get; private set; }

            public void Undo()
            {
                UndoCount++;
                Value--;
            }

            public void Redo()
            {
                RedoCount++;
                Value++;
            }
        }

        /// <summary>
        /// Тест Redo - корректная работа с исправленным именем переменной (itemToRedo вместо itemToRedo).
        /// </summary>
        [Fact]
        public void Redo_WithValidState_ExecutesCorrectly()
        {
            // Arrange
            var manager = new CMementoManager();
            var state1 = new TestMementoState { Value = 1 };
            var state2 = new TestMementoState { Value = 2 };

            manager.AddStateToHistory(state1);
            manager.AddStateToHistory(state2);

            // Undo последнее действие
            manager.Undo();
            Assert.Equal(1, state1.Value);
            Assert.Equal(0, state1.UndoCount);

            // Act - используем Redo с исправленным именем переменной
            manager.Redo();

            // Assert - проверяем что Redo выполнился корректно
            Assert.Equal(1, state2.RedoCount);
            Assert.True(manager.CanUndo, "Should be able to undo after redo");
            Assert.False(manager.CanRedo, "Should not be able to redo after last redo");
        }

        /// <summary>
        /// Тест Redo - корректный индекс при выполнении Redo.
        /// </summary>
        [Fact]
        public void Redo_WithMultipleStates_UsesCorrectIndex()
        {
            // Arrange
            var manager = new CMementoManager();
            var state1 = new TestMementoState { Value = 1 };
            var state2 = new TestMementoState { Value = 2 };
            var state3 = new TestMementoState { Value = 3 };

            manager.AddStateToHistory(state1);
            manager.AddStateToHistory(state2);
            manager.AddStateToHistory(state3);

            // Undo два раза
            manager.Undo();
            manager.Undo();

            // Act - Redo должен использовать правильный индекс (itemToRedo = _nextUndo + 1)
            manager.Redo();

            // Assert
            Assert.Equal(1, state2.RedoCount);
            Assert.Equal(0, state3.RedoCount);
            Assert.True(manager.CanRedo, "Should be able to redo state3");
        }

        /// <summary>
        /// Тест Redo - корректная обработка когда Redo невозможен.
        /// </summary>
        [Fact]
        public void Redo_WhenCannotRedo_DoesNothing()
        {
            // Arrange
            var manager = new CMementoManager();
            var state1 = new TestMementoState { Value = 1 };

            manager.AddStateToHistory(state1);
            Assert.False(manager.CanRedo, "Should not be able to redo when at end");

            // Act
            manager.Redo();

            // Assert
            Assert.Equal(0, state1.RedoCount);
        }

        /// <summary>
        /// Тест Redo - последовательные вызовы Redo.
        /// </summary>
        [Fact]
        public void Redo_SequentialCalls_WorkCorrectly()
        {
            // Arrange
            var manager = new CMementoManager();
            var state1 = new TestMementoState { Value = 1 };
            var state2 = new TestMementoState { Value = 2 };
            var state3 = new TestMementoState { Value = 3 };

            manager.AddStateToHistory(state1);
            manager.AddStateToHistory(state2);
            manager.AddStateToHistory(state3);

            // Undo все действия
            manager.Undo();
            manager.Undo();
            manager.Undo();

            // Act - последовательные Redo
            manager.Redo();
            manager.Redo();
            manager.Redo();

            // Assert
            Assert.Equal(1, state1.RedoCount);
            Assert.Equal(1, state2.RedoCount);
            Assert.Equal(1, state3.RedoCount);
            Assert.False(manager.CanRedo, "Should not be able to redo after all redone");
        }
    }
}
