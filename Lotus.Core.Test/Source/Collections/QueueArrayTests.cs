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
    public class QueueArrayTests
    {
        private QueueArray<int> _queue;

        [SetUp]
        public void SetUp()
        {
            _queue = new QueueArray<int>();
        }

        [Test]
        public void Enqueue_AddsItemToQueue()
        {
            _queue.Enqueue(10);

            ClassicAssert.AreEqual(1, _queue.Count);
            ClassicAssert.AreEqual(10, _queue.Peek());
        }

        [Test]
        public void Dequeue_RemovesAndReturnsItemFromHead()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            var result = _queue.Dequeue();

            ClassicAssert.AreEqual(10, result);
            ClassicAssert.AreEqual(1, _queue.Count);
            ClassicAssert.AreEqual(20, _queue.Peek());
        }

        [Test]
        public void Peek_ReturnsItemFromHeadWithoutRemovingIt()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            var result = _queue.Peek();

            ClassicAssert.AreEqual(10, result);
            ClassicAssert.AreEqual(2, _queue.Count);
        }

        [Test]
        public void Contains_ReturnsTrueIfItemExistsInQueue()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            ClassicAssert.IsTrue(_queue.Contains(10));
            ClassicAssert.IsTrue(_queue.Contains(20));
            ClassicAssert.IsFalse(_queue.Contains(30));
        }

        [Test]
        public void Clear_RemovesAllItemsFromQueue()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            _queue.Clear();

            ClassicAssert.AreEqual(0, _queue.Count);
            ClassicAssert.AreEqual(0, _queue.Head);
            ClassicAssert.AreEqual(-1, _queue.Tail);
        }

        [Test]
        public void Head_ReturnsCorrectIndex()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);
            _queue.Dequeue();

            ClassicAssert.AreEqual(1, _queue.Head);
        }

        [Test]
        public void Tail_ReturnsCorrectIndex()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            ClassicAssert.AreEqual(1, _queue.Tail);
        }

        [Test]
        public void Indexer_ReturnsCorrectItem()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);
            _queue.Enqueue(30);

            ClassicAssert.AreEqual(10, _queue[0]);
            ClassicAssert.AreEqual(20, _queue[1]);
            ClassicAssert.AreEqual(30, _queue[2]);
        }

        [Test]
        public void Indexer_SetsCorrectItem()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);
            _queue[1] = 25;

            ClassicAssert.AreEqual(25, _queue[1]);
        }

        [Test]
        public void GetElement_ReturnsCorrectItem()
        {
            _queue.Enqueue(10);
            _queue.Enqueue(20);

            ClassicAssert.AreEqual(10, _queue.GetElement(0));
            ClassicAssert.AreEqual(20, _queue.GetElement(1));
        }

        /// <summary>
        /// Тестирование методов <see cref="QueueArray{TItem}"/>.
        /// </summary>
        [Test]
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

            ClassicAssert.AreEqual(queue.Dequeue(), 100);
            ClassicAssert.AreEqual(queue.Dequeue(), 90);
            ClassicAssert.AreEqual(queue.Dequeue(), 80);
            ClassicAssert.AreEqual(queue.Dequeue(), 70);
            ClassicAssert.AreEqual(queue.Dequeue(), 60);
            ClassicAssert.AreEqual(queue.Dequeue(), 50);
            ClassicAssert.AreEqual(queue.Dequeue(), 30);
            ClassicAssert.AreEqual(queue.Count, 0);
        }
    }
}