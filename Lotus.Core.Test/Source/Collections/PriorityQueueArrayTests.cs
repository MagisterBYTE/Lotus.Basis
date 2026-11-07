#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

using System;


namespace Lotus.Core.Collections
{
    public class PriorityQueueArrayTests
    {
        private PriorityQueue<int> _priorityQueue;

        public PriorityQueueArrayTests()
        {
            _priorityQueue = new PriorityQueue<int>();
        }

        [Fact]
        public void Push_AddsItemToQueue()
        {
            var index = _priorityQueue.Push(5);

            Assert.Equal(0, index);
            Assert.Equal(1, _priorityQueue.Count);
            Assert.Equal(5, _priorityQueue.Peek());
        }

        [Fact]
        public void Pop_RemovesAndReturnsLowestPriorityItem()
        {
            _priorityQueue.Push(5);
            _priorityQueue.Push(3);
            _priorityQueue.Push(7);

            var result = _priorityQueue.Pop();

            Assert.Equal(3, result);
            Assert.Equal(2, _priorityQueue.Count);
            Assert.Equal(5, _priorityQueue.Peek());
        }

        [Fact]
        public void Peek_ReturnsLowestPriorityItemWithoutRemovingIt()
        {
            _priorityQueue.Push(5);
            _priorityQueue.Push(3);
            _priorityQueue.Push(7);

            var result = _priorityQueue.Peek();

            Assert.Equal(3, result);
            Assert.Equal(3, _priorityQueue.Count);
        }

        [Fact]
        public void Update_ChangesPriorityAndReordersQueue()
        {
            _priorityQueue.Push(5);
            _priorityQueue.Push(3);
            _priorityQueue.Push(7);

            _priorityQueue[1] = 2; // Изменяем приоритет элемента с 3 на 2
            _priorityQueue.Update(1);

            var result = _priorityQueue.Peek();

            Assert.Equal(2, result);
        }

        [Fact]
        public void Peek_ReturnsDefaultWhenQueueIsEmpty()
        {
            var result = _priorityQueue.Peek();

            Assert.Equal(default(int), result);
        }

        [Fact]
        public void Push_RespectsPriorityOrder()
        {
            _priorityQueue.Push(5);
            _priorityQueue.Push(3);
            _priorityQueue.Push(7);
            _priorityQueue.Push(1);

            Assert.Equal(1, _priorityQueue.Pop());
            Assert.Equal(3, _priorityQueue.Pop());
            Assert.Equal(5, _priorityQueue.Pop());
            Assert.Equal(7, _priorityQueue.Pop());
        }

        /// <summary>
        /// Тестирование методов <see cref="PriorityQueue{TItem}"/>.
        /// </summary>
        [Fact]
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

            Assert.Equal(priorityQueue.Pop(), 1);
            Assert.Equal(priorityQueue.Pop(), 2);
            Assert.Equal(priorityQueue.Pop(), 3);
            Assert.Equal(priorityQueue.Pop(), 4);
            Assert.Equal(priorityQueue.Pop(), 5);
            Assert.Equal(priorityQueue.Pop(), 6);

            priorityQueue.Push(60);
            priorityQueue.Push(70);
            priorityQueue.Push(80);
            priorityQueue.Push(40);

            Assert.Equal(priorityQueue.Pop(), 7);
            Assert.Equal(priorityQueue.Pop(), 8);
            Assert.Equal(priorityQueue.Pop(), 40);
            Assert.Equal(priorityQueue.Pop(), 60);
            Assert.Equal(priorityQueue.Pop(), 70);
            Assert.Equal(priorityQueue.Pop(), 80);
        }
    }
}