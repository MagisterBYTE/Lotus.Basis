#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

using System;


namespace Lotus.Core.Collections
{
    public class QueueArrayTests
    {
        private QueueArray<int> _queue;

        public QueueArrayTests()
        {
            _queue = new QueueArray<int>();
        }

        [Fact]
        public void Enqueue_AddsItemToQueue()
        {
            _queue.Enqueue(10);

            Assert.Equal(1, _queue.Count);
            Assert.Equal(10, _queue.Peek());
        }

        [Fact]
        public void Dequeue_RemovesAndReturnsItemFromHead()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            var result = _queue.Dequeue();

            Assert.Equal(10, result);
            Assert.Equal(1, _queue.Count);
            Assert.Equal(20, _queue.Peek());
        }

        [Fact]
        public void Peek_ReturnsItemFromHeadWithoutRemovingIt()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            var result = _queue.Peek();

            Assert.Equal(10, result);
            Assert.Equal(2, _queue.Count);
        }

        [Fact]
        public void Contains_ReturnsTrueIfItemExistsInQueue()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            Assert.True(_queue.Contains(10));
            Assert.True(_queue.Contains(20));
            Assert.False(_queue.Contains(30));
        }

        [Fact]
        public void Clear_RemovesAllItemsFromQueue()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            _queue.Clear();

            Assert.Equal(0, _queue.Count);
            Assert.Equal(0, _queue.Head);
            Assert.Equal(-1, _queue.Tail);
        }

        [Fact]
        public void Head_ReturnsCorrectIndex()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);
            _queue.Dequeue();

            Assert.Equal(1, _queue.Head);
        }

        [Fact]
        public void Tail_ReturnsCorrectIndex()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            Assert.Equal(1, _queue.Tail);
        }

        [Fact]
        public void Indexer_ReturnsCorrectItem()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);
            _queue.Enqueue(30);

            Assert.Equal(10, _queue[0]);
            Assert.Equal(20, _queue[1]);
            Assert.Equal(30, _queue[2]);
        }

        [Fact]
        public void Indexer_SetsCorrectItem()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);
            _queue[1] = 25;

            Assert.Equal(25, _queue[1]);
        }

        [Fact]
        public void GetElement_ReturnsCorrectItem()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            Assert.Equal(10, _queue.GetElement(0));
            Assert.Equal(20, _queue.GetElement(1));
        }

        /// <summary>
        /// Тестирование методов <see cref="QueueArray{TItem}"/>.
        /// </summary>
        [Fact]
        public void AllMethods()
        {
            var queue = new QueueArray<int>();
            queue.Enqueue(100);
            queue.Enqueue(90);
            queue.Enqueue(80);
            queue.Enqueue(70);
            queue.Enqueue(60);
            queue.Enqueue(50);
            queue.Enqueue(30);

            Assert.Equal(queue.Dequeue(), 100);
            Assert.Equal(queue.Dequeue(), 90);
            Assert.Equal(queue.Dequeue(), 80);
            Assert.Equal(queue.Dequeue(), 70);
            Assert.Equal(queue.Dequeue(), 60);
            Assert.Equal(queue.Dequeue(), 50);
            Assert.Equal(queue.Dequeue(), 30);
            Assert.Equal(queue.Count, 0);
        }
    }
}