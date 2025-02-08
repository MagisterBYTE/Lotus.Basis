using System;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Collections
{
    [TestFixture]
    public class DequeArrayTests
    {
        private DequeArray<int> _deque;

        [SetUp]
        public void SetUp()
        {
            _deque = new DequeArray<int>();
        }

        [Test]
        public void Constructor_InitializesWithDefaultCapacity()
        {
            ClassicAssert.AreEqual(0, _deque.Count);
            ClassicAssert.IsTrue(_deque.StartOffset > 0);
        }

        [Test]
        public void Constructor_InitializesWithSpecifiedCapacity()
        {
            var deque = new DequeArray<int>(100);
            ClassicAssert.AreEqual(0, deque.Count);
            ClassicAssert.AreEqual(50, deque.StartOffset);
        }

        [Test]
        public void AddFront_AddsElementToFront()
        {
            _deque.AddFront(10);
            ClassicAssert.AreEqual(1, _deque.Count);
            ClassicAssert.AreEqual(10, _deque.PeekFront());
        }

        [Test]
        public void AddBack_AddsElementToBack()
        {
            _deque.AddBack(20);
            ClassicAssert.AreEqual(1, _deque.Count);
            ClassicAssert.AreEqual(20, _deque.PeekBack());
        }

        [Test]
        public void RemoveFront_RemovesAndReturnsFrontElement()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            var frontElement = _deque.RemoveFront();
            ClassicAssert.AreEqual(10, frontElement);
            ClassicAssert.AreEqual(1, _deque.Count);
            ClassicAssert.AreEqual(20, _deque.PeekFront());
        }

        [Test]
        public void PeekFront_ReturnsFrontElementWithoutRemoving()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            var frontElement = _deque.PeekFront();
            ClassicAssert.AreEqual(10, frontElement);
            ClassicAssert.AreEqual(2, _deque.Count);
        }

        [Test]
        public void PeekBack_ReturnsBackElementWithoutRemoving()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            var backElement = _deque.PeekBack();
            ClassicAssert.AreEqual(20, backElement);
            ClassicAssert.AreEqual(2, _deque.Count);
        }

        [Test]
        public void Contains_ReturnsTrueIfElementExists()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            ClassicAssert.IsTrue(_deque.Contains(10));
            ClassicAssert.IsTrue(_deque.Contains(20));
            ClassicAssert.IsFalse(_deque.Contains(30));
        }

        [Test]
        public void Clear_RemovesAllElements()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            _deque.Clear();
            ClassicAssert.AreEqual(0, _deque.Count);
            ClassicAssert.IsFalse(_deque.Contains(10));
            ClassicAssert.IsFalse(_deque.Contains(20));
        }

        [Test]
        public void Indexer_ReturnsCorrectElement()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            ClassicAssert.AreEqual(10, _deque[0]);
            ClassicAssert.AreEqual(20, _deque[1]);
        }

        [Test]
        public void Indexer_SetsCorrectElement()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            _deque[0] = 30;
            _deque[1] = 40;

            ClassicAssert.AreEqual(30, _deque[0]);
            ClassicAssert.AreEqual(40, _deque[1]);
        }

        /// <summary>
        /// Тестирование методов <see cref="DequeArray{TItem}"/>.
        /// </summary>
        [Test]
        public static void AllMethods()
        {
            var deque = new DequeArray<int>();

            deque.AddFront(4);
            deque.AddFront(3);
            deque.AddFront(2);
            deque.AddFront(1);
            deque.AddBack(5);
            deque.AddBack(6);
            deque.AddBack(7);
            deque.AddBack(8);

            ClassicAssert.AreEqual(deque.GetElement(0), 1);
            ClassicAssert.AreEqual(deque.GetElement(1), 2);
            ClassicAssert.AreEqual(deque.GetElement(2), 3);
            ClassicAssert.AreEqual(deque.GetElement(3), 4);
            ClassicAssert.AreEqual(deque.GetElement(4), 5);
            ClassicAssert.AreEqual(deque.GetElement(5), 6);
            ClassicAssert.AreEqual(deque.GetElement(6), 7);
            ClassicAssert.AreEqual(deque.GetElement(7), 8);
            ClassicAssert.AreEqual(deque.Count, 8);


            deque.AddFront(100);
            deque.AddFront(200);
            deque.AddBack(10000);

            ClassicAssert.AreEqual(deque.GetElement(0), 200);
            ClassicAssert.AreEqual(deque.GetElement(1), 100);
            ClassicAssert.AreEqual(deque.GetElement(2), 1);
            ClassicAssert.AreEqual(deque.GetElement(3), 2);
            ClassicAssert.AreEqual(deque.GetElement(4), 3);
            ClassicAssert.AreEqual(deque.GetElement(5), 4);
            ClassicAssert.AreEqual(deque.GetElement(6), 5);
            ClassicAssert.AreEqual(deque.GetElement(7), 6);
            ClassicAssert.AreEqual(deque.GetElement(8), 7);
            ClassicAssert.AreEqual(deque.GetElement(9), 8);
            ClassicAssert.AreEqual(deque.GetElement(10), 10000);
            ClassicAssert.AreEqual(deque.Count, 11);


            deque.RemoveFront();
            deque.RemoveFront();
            deque.RemoveFront();
            deque.RemoveFront();

            ClassicAssert.AreEqual(deque.GetElement(0), 3);
            ClassicAssert.AreEqual(deque.GetElement(1), 4);
            ClassicAssert.AreEqual(deque.GetElement(2), 5);
            ClassicAssert.AreEqual(deque.GetElement(3), 6);
            ClassicAssert.AreEqual(deque.GetElement(4), 7);
            ClassicAssert.AreEqual(deque.GetElement(5), 8);
            ClassicAssert.AreEqual(deque.GetElement(6), 10000);
            ClassicAssert.AreEqual(deque.Count, 7);


            deque.RemoveBack();
            deque.RemoveBack();

            ClassicAssert.AreEqual(deque.GetElement(0), 3);
            ClassicAssert.AreEqual(deque.GetElement(1), 4);
            ClassicAssert.AreEqual(deque.GetElement(2), 5);
            ClassicAssert.AreEqual(deque.GetElement(3), 6);
            ClassicAssert.AreEqual(deque.GetElement(4), 7);
            ClassicAssert.AreEqual(deque.Count, 5);


            deque.AddFront(100);
            deque.AddFront(200);
            deque.AddBack(10000);

            ClassicAssert.AreEqual(deque.GetElement(0), 200);
            ClassicAssert.AreEqual(deque.GetElement(1), 100);
            ClassicAssert.AreEqual(deque.GetElement(2), 3);
            ClassicAssert.AreEqual(deque.GetElement(3), 4);
            ClassicAssert.AreEqual(deque.GetElement(4), 5);
            ClassicAssert.AreEqual(deque.GetElement(5), 6);
            ClassicAssert.AreEqual(deque.GetElement(6), 7);
            ClassicAssert.AreEqual(deque.GetElement(7), 10000);
            ClassicAssert.AreEqual(deque.Count, 8);
        }
    }
}
