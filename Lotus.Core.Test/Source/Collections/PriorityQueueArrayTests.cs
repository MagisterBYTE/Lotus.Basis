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
    public class PriorityQueueArrayTests
    {
        private PriorityQueue<int> _priorityQueue;

        [SetUp]
        public void SetUp()
        {
            _priorityQueue = new PriorityQueue<int>();
        }

        [Test]
        public void Push_AddsItemToQueue()
        {
            var index = _priorityQueue.Push(5);

            ClassicAssert.AreEqual(0, index);
            ClassicAssert.AreEqual(1, _priorityQueue.Count);
            ClassicAssert.AreEqual(5, _priorityQueue.Peek());
        }

        [Test]
        public void Pop_RemovesAndReturnsLowestPriorityItem()
        {
            _priorityQueue.Push(5);
            _priorityQueue.Push(3);
            _priorityQueue.Push(7);

            var result = _priorityQueue.Pop();

            ClassicAssert.AreEqual(3, result);
            ClassicAssert.AreEqual(2, _priorityQueue.Count);
            ClassicAssert.AreEqual(5, _priorityQueue.Peek());
        }

        [Test]
        public void Peek_ReturnsLowestPriorityItemWithoutRemovingIt()
        {
            _priorityQueue.Push(5);
            _priorityQueue.Push(3);
            _priorityQueue.Push(7);

            var result = _priorityQueue.Peek();

            ClassicAssert.AreEqual(3, result);
            ClassicAssert.AreEqual(3, _priorityQueue.Count);
        }

        [Test]
        public void Update_ChangesPriorityAndReordersQueue()
        {
            _priorityQueue.Push(5);
            _priorityQueue.Push(3);
            _priorityQueue.Push(7);

            _priorityQueue[1] = 2; // Изменяем приоритет элемента с 3 на 2
            _priorityQueue.Update(1);

            var result = _priorityQueue.Peek();

            ClassicAssert.AreEqual(2, result);
        }

        [Test]
        public void Peek_ReturnsDefaultWhenQueueIsEmpty()
        {
            var result = _priorityQueue.Peek();

            ClassicAssert.AreEqual(default(int), result);
        }

        [Test]
        public void Push_RespectsPriorityOrder()
        {
            _priorityQueue.Push(5);
            _priorityQueue.Push(3);
            _priorityQueue.Push(7);
            _priorityQueue.Push(1);

            ClassicAssert.AreEqual(1, _priorityQueue.Pop());
            ClassicAssert.AreEqual(3, _priorityQueue.Pop());
            ClassicAssert.AreEqual(5, _priorityQueue.Pop());
            ClassicAssert.AreEqual(7, _priorityQueue.Pop());
        }

        /// <summary>
        /// Тестирование методов <see cref="PriorityQueue{TItem}"/>.
        /// </summary>
        [Test]
        public void AllMethods()
        {
            var priorityQueue = new PriorityQueue<int>();

            priorityQueue.Push(3);
            priorityQueue.Push(2);
            priorityQueue.Push(1);
            priorityQueue.Push(5);
            priorityQueue.Push(6);
            priorityQueue.Push(7);
            priorityQueue.Push(8);
            priorityQueue.Push(4);

            ClassicAssert.AreEqual(priorityQueue.Pop(), 1);
            ClassicAssert.AreEqual(priorityQueue.Pop(), 2);
            ClassicAssert.AreEqual(priorityQueue.Pop(), 3);
            ClassicAssert.AreEqual(priorityQueue.Pop(), 4);
            ClassicAssert.AreEqual(priorityQueue.Pop(), 5);
            ClassicAssert.AreEqual(priorityQueue.Pop(), 6);

            priorityQueue.Push(60);
            priorityQueue.Push(70);
            priorityQueue.Push(80);
            priorityQueue.Push(40);

            ClassicAssert.AreEqual(priorityQueue.Pop(), 7);
            ClassicAssert.AreEqual(priorityQueue.Pop(), 8);
            ClassicAssert.AreEqual(priorityQueue.Pop(), 40);
            ClassicAssert.AreEqual(priorityQueue.Pop(), 60);
            ClassicAssert.AreEqual(priorityQueue.Pop(), 70);
            ClassicAssert.AreEqual(priorityQueue.Pop(), 80);
        }
    }
}