using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Memento
{
    /// <summary>
    /// Тесты для критических участков Memento (исправление имени переменной itemToRedo).
    /// </summary>
    [TestFixture]
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
        /// Тест Redo - корректная работа с исправленным именем переменной (itemToRedo вместо item_to_redo).
        /// </summary>
        [Test]
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
            ClassicAssert.AreEqual(1, state1.Value);
            ClassicAssert.AreEqual(0, state1.UndoCount);

            // Act - используем Redo с исправленным именем переменной
            manager.Redo();

            // Assert - проверяем что Redo выполнился корректно
            ClassicAssert.AreEqual(1, state2.RedoCount, "Redo should be called on correct state");
            ClassicAssert.IsTrue(manager.CanUndo, "Should be able to undo after redo");
            ClassicAssert.IsFalse(manager.CanRedo, "Should not be able to redo after last redo");
        }

        /// <summary>
        /// Тест Redo - корректный индекс при выполнении Redo.
        /// </summary>
        [Test]
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
            ClassicAssert.AreEqual(1, state2.RedoCount, "Should redo state2");
            ClassicAssert.AreEqual(0, state3.RedoCount, "Should not redo state3 yet");
            ClassicAssert.IsTrue(manager.CanRedo, "Should be able to redo state3");
        }

        /// <summary>
        /// Тест Redo - корректная обработка когда Redo невозможен.
        /// </summary>
        [Test]
        public void Redo_WhenCannotRedo_DoesNothing()
        {
            // Arrange
            var manager = new CMementoManager();
            var state1 = new TestMementoState { Value = 1 };

            manager.AddStateToHistory(state1);
            ClassicAssert.IsFalse(manager.CanRedo, "Should not be able to redo when at end");

            // Act
            manager.Redo();

            // Assert
            ClassicAssert.AreEqual(0, state1.RedoCount, "Redo should not be called when CanRedo is false");
        }

        /// <summary>
        /// Тест Redo - последовательные вызовы Redo.
        /// </summary>
        [Test]
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
            ClassicAssert.AreEqual(1, state1.RedoCount, "State1 should be redone");
            ClassicAssert.AreEqual(1, state2.RedoCount, "State2 should be redone");
            ClassicAssert.AreEqual(1, state3.RedoCount, "State3 should be redone");
            ClassicAssert.IsFalse(manager.CanRedo, "Should not be able to redo after all redone");
        }
    }
}
