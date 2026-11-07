using System;


namespace Lotus.Core.Collections
{
    public class DequeArrayTests
    {
        private DequeArray<int> _deque;

        public DequeArrayTests()
        {
            _deque = new DequeArray<int>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultCapacity()
        {
            Assert.Equal(0, _deque.Count);
            Assert.True(_deque.StartOffset > 0);
        }

        [Fact]
        public void Constructor_InitializesWithSpecifiedCapacity()
        {
            var deque = new DequeArray<int>(100);
            Assert.Equal(0, deque.Count);
            Assert.Equal(50, deque.StartOffset);
        }

        [Fact]
        public void AddFront_AddsElementToFront()
        {
            _deque.AddFront(10);
            Assert.Equal(1, _deque.Count);
            Assert.Equal(10, _deque.PeekFront());
        }

        [Fact]
        public void AddBack_AddsElementToBack()
        {
            _deque.AddBack(20);
            Assert.Equal(1, _deque.Count);
            Assert.Equal(20, _deque.PeekBack());
        }

        [Fact]
        public void RemoveFront_RemovesAndReturnsFrontElement()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            var frontElement = _deque.RemoveFront();
            Assert.Equal(10, frontElement);
            Assert.Equal(1, _deque.Count);
            Assert.Equal(20, _deque.PeekFront());
        }

        [Fact]
        public void PeekFront_ReturnsFrontElementWithoutRemoving()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            var frontElement = _deque.PeekFront();
            Assert.Equal(10, frontElement);
            Assert.Equal(2, _deque.Count);
        }

        [Fact]
        public void PeekBack_ReturnsBackElementWithoutRemoving()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            var backElement = _deque.PeekBack();
            Assert.Equal(20, backElement);
            Assert.Equal(2, _deque.Count);
        }

        [Fact]
        public void Contains_ReturnsTrueIfElementExists()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            Assert.True(_deque.Contains(10));
            Assert.True(_deque.Contains(20));
            Assert.False(_deque.Contains(30));
        }

        [Fact]
        public void Clear_RemovesAllElements()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            _deque.Clear();
            Assert.Equal(0, _deque.Count);
            Assert.False(_deque.Contains(10));
            Assert.False(_deque.Contains(20));
        }

        [Fact]
        public void Indexer_ReturnsCorrectElement()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            Assert.Equal(10, _deque[0]);
            Assert.Equal(20, _deque[1]);
        }

        [Fact]
        public void Indexer_SetsCorrectElement()
        {
            _deque.AddFront(10);
            _deque.AddBack(20);

            _deque[0] = 30;
            _deque[1] = 40;

            Assert.Equal(30, _deque[0]);
            Assert.Equal(40, _deque[1]);
        }

        /// <summary>
        /// Тестирование методов <see cref="DequeArray{TItem}"/>.
        /// </summary>
        [Fact]
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

            Assert.Equal(deque.GetElement(0), 1);
            Assert.Equal(deque.GetElement(1), 2);
            Assert.Equal(deque.GetElement(2), 3);
            Assert.Equal(deque.GetElement(3), 4);
            Assert.Equal(deque.GetElement(4), 5);
            Assert.Equal(deque.GetElement(5), 6);
            Assert.Equal(deque.GetElement(6), 7);
            Assert.Equal(deque.GetElement(7), 8);
            Assert.Equal(deque.Count, 8);


            deque.AddFront(100);
            deque.AddFront(200);
            deque.AddBack(10000);

            Assert.Equal(deque.GetElement(0), 200);
            Assert.Equal(deque.GetElement(1), 100);
            Assert.Equal(deque.GetElement(2), 1);
            Assert.Equal(deque.GetElement(3), 2);
            Assert.Equal(deque.GetElement(4), 3);
            Assert.Equal(deque.GetElement(5), 4);
            Assert.Equal(deque.GetElement(6), 5);
            Assert.Equal(deque.GetElement(7), 6);
            Assert.Equal(deque.GetElement(8), 7);
            Assert.Equal(deque.GetElement(9), 8);
            Assert.Equal(deque.GetElement(10), 10000);
            Assert.Equal(deque.Count, 11);


            deque.RemoveFront();
            deque.RemoveFront();
            deque.RemoveFront();
            deque.RemoveFront();

            Assert.Equal(deque.GetElement(0), 3);
            Assert.Equal(deque.GetElement(1), 4);
            Assert.Equal(deque.GetElement(2), 5);
            Assert.Equal(deque.GetElement(3), 6);
            Assert.Equal(deque.GetElement(4), 7);
            Assert.Equal(deque.GetElement(5), 8);
            Assert.Equal(deque.GetElement(6), 10000);
            Assert.Equal(deque.Count, 7);


            deque.RemoveBack();
            deque.RemoveBack();

            Assert.Equal(deque.GetElement(0), 3);
            Assert.Equal(deque.GetElement(1), 4);
            Assert.Equal(deque.GetElement(2), 5);
            Assert.Equal(deque.GetElement(3), 6);
            Assert.Equal(deque.GetElement(4), 7);
            Assert.Equal(deque.Count, 5);


            deque.AddFront(100);
            deque.AddFront(200);
            deque.AddBack(10000);

            Assert.Equal(deque.GetElement(0), 200);
            Assert.Equal(deque.GetElement(1), 100);
            Assert.Equal(deque.GetElement(2), 3);
            Assert.Equal(deque.GetElement(3), 4);
            Assert.Equal(deque.GetElement(4), 5);
            Assert.Equal(deque.GetElement(5), 6);
            Assert.Equal(deque.GetElement(6), 7);
            Assert.Equal(deque.GetElement(7), 10000);
            Assert.Equal(deque.Count, 8);
        }
    }
}
