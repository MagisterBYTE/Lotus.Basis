#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

using System;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Collections
{
    [TestFixture]
    public class StackArrayTests
    {
        private StackArray<int> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new StackArray<int>();
        }

        [Test]
        public void Push_AddsItemToStack()
        {
            _stack.Push(10);

            ClassicAssert.AreEqual(1, _stack.Count);
            ClassicAssert.AreEqual(10, _stack.Peek());
        }

        [Test]
        public void Pop_RemovesAndReturnsItemFromTop()
        {
            _stack.Push(10);
            _stack.Push(20);

            var result = _stack.Pop();

            ClassicAssert.AreEqual(20, result);
            ClassicAssert.AreEqual(1, _stack.Count);
            ClassicAssert.AreEqual(10, _stack.Peek());
        }

        [Test]
        public void Peek_ReturnsItemFromTopWithoutRemovingIt()
        {
            _stack.Push(10);
            _stack.Push(20);

            var result = _stack.Peek();

            ClassicAssert.AreEqual(20, result);
            ClassicAssert.AreEqual(2, _stack.Count);
        }

        [Test]
        public void Push_ResizesStackWhenCapacityExceeded()
        {
            // Инициализируем стек с начальной емкостью 2
            var stack = new StackArray<int>(2);

            stack.Push(10);
            stack.Push(20);
            stack.Push(30); // Должно вызвать увеличение емкости

            ClassicAssert.AreEqual(3, stack.Count);
            ClassicAssert.AreEqual(30, stack.Peek());
        }

        [Test]
        public void Pop_ReturnsDefaultWhenStackIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => _stack.Pop());
        }

        [Test]
        public void Peek_ReturnsDefaultWhenStackIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => _stack.Peek());
        }

        [Test]
        public void Push_And_Pop_WorkCorrectly()
        {
            _stack.Push(10);
            _stack.Push(20);
            _stack.Push(30);

            ClassicAssert.AreEqual(30, _stack.Pop());
            ClassicAssert.AreEqual(20, _stack.Pop());
            ClassicAssert.AreEqual(10, _stack.Pop());

            ClassicAssert.AreEqual(0, _stack.Count);
        }

        [Test]
        public void Clear_RemovesAllItemsFromStack()
        {
            _stack.Push(10);
            _stack.Push(20);

            _stack.Clear();

            ClassicAssert.AreEqual(0, _stack.Count);
        }
    }
}