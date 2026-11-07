#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

using System;


namespace Lotus.Core.Collections
{
    public class StackArrayTests
    {
        private StackArray<int> _stack;

        public StackArrayTests()
        {
            _stack = new StackArray<int>();
        }

        [Fact]
        public void Push_AddsItemToStack()
        {
            _stack.Push(10);

            Assert.Equal(1, _stack.Count);
            Assert.Equal(10, _stack.Peek());
        }

        [Fact]
        public void Pop_RemovesAndReturnsItemFromTop()
        {
            _stack.Push(10);
            _stack.Push(20);

            var result = _stack.Pop();

            Assert.Equal(20, result);
            Assert.Equal(1, _stack.Count);
            Assert.Equal(10, _stack.Peek());
        }

        [Fact]
        public void Peek_ReturnsItemFromTopWithoutRemovingIt()
        {
            _stack.Push(10);
            _stack.Push(20);

            var result = _stack.Peek();

            Assert.Equal(20, result);
            Assert.Equal(2, _stack.Count);
        }

        [Fact]
        public void Push_ResizesStackWhenCapacityExceeded()
        {
            // Инициализируем стек с начальной емкостью 2
            var stack = new StackArray<int>(2);

            stack.Push(10);
            stack.Push(20);
            stack.Push(30); // Должно вызвать увеличение емкости

            Assert.Equal(3, stack.Count);
            Assert.Equal(30, stack.Peek());
        }

        [Fact]
        public void Pop_ReturnsDefaultWhenStackIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => _stack.Pop());
        }

        [Fact]
        public void Peek_ReturnsDefaultWhenStackIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => _stack.Peek());
        }

        [Fact]
        public void Push_And_Pop_WorkCorrectly()
        {
            _stack.Push(10);
            _stack.Push(20);
            _stack.Push(30);

            Assert.Equal(30, _stack.Pop());
            Assert.Equal(20, _stack.Pop());
            Assert.Equal(10, _stack.Pop());

            Assert.Equal(0, _stack.Count);
        }

        [Fact]
        public void Clear_RemovesAllItemsFromStack()
        {
            _stack.Push(10);
            _stack.Push(20);

            _stack.Clear();

            Assert.Equal(0, _stack.Count);
        }
    }
}