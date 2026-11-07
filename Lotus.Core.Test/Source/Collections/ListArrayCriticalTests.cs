using System;

namespace Lotus.Core.Collections
{
    /// <summary>
    /// Тесты для критических исправлений в <see cref="ListArray{TItem}"/>.
    /// </summary>
    public class ListArrayCriticalTests
    {
        /// <summary>
        /// Тест свойства ItemSecond - корректный доступ к индексу 1.
        /// </summary>
        [Fact]
        public void ItemSecond_WithAtLeastTwoElements_ReturnsElementAtIndex1()
        {
            var list = new ListArray<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            Assert.Equal(20, list.ItemSecond);
        }

        /// <summary>
        /// Тест свойства ItemSecond - установка значения по индексу 1.
        /// </summary>
        [Fact]
        public void ItemSecond_SetValue_UpdatesElementAtIndex1()
        {
            var list = new ListArray<int>();
            list.Add(10);
            list.Add(20);

            list.ItemSecond = 25;
            Assert.Equal(25, list[1]);
            Assert.Equal(10, list[0]);
        }

        /// <summary>
        /// Тест свойства ItemSecond - выбрасывает исключение при пустой коллекции.
        /// </summary>
        [Fact]
        public void ItemSecond_WithEmptyCollection_ThrowsIndexOutOfRangeException()
        {
            var list = new ListArray<int>();

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var _ = list.ItemSecond;
            });
        }

        /// <summary>
        /// Тест свойства ItemSecond - выбрасывает исключение при одном элементе.
        /// </summary>
        [Fact]
        public void ItemSecond_WithOneElement_ThrowsIndexOutOfRangeException()
        {
            var list = new ListArray<int>();
            list.Add(10);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var _ = list.ItemSecond;
            });
        }

        /// <summary>
        /// Тест метода RemoveRange - выбрасывает исключение при отрицательном индексе.
        /// </summary>
        [Fact]
        public void RemoveRange_WithNegativeIndex_ThrowsArgumentOutOfRangeException()
        {
            var list = new ListArray<int>();
            list.Add(10);
            list.Add(20);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                list.RemoveRange(-1, 1);
            });
        }

        /// <summary>
        /// Тест метода RemoveRange - выбрасывает исключение при отрицательном количестве.
        /// </summary>
        [Fact]
        public void RemoveRange_WithNegativeCount_ThrowsArgumentOutOfRangeException()
        {
            var list = new ListArray<int>();
            list.Add(10);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                list.RemoveRange(0, -1);
            });
        }

        /// <summary>
        /// Тест метода RemoveRange - выбрасывает исключение при выходе за границы.
        /// </summary>
        [Fact]
        public void RemoveRange_WithIndexAndCountOutOfBounds_ThrowsArgumentException()
        {
            var list = new ListArray<int>();
            list.Add(10);
            list.Add(20);

            Assert.Throws<ArgumentException>(() =>
            {
                list.RemoveRange(0, 5);
            });
        }

        /// <summary>
        /// Тест метода RemoveAt - выбрасывает исключение при отрицательном индексе.
        /// </summary>
        [Fact]
        public void RemoveAt_WithNegativeIndex_ThrowsArgumentOutOfRangeException()
        {
            var list = new ListArray<int>();
            list.Add(10);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                list.RemoveAt(-1);
            });
        }

        /// <summary>
        /// Тест метода RemoveAt - выбрасывает исключение при индексе больше размера.
        /// </summary>
        [Fact]
        public void RemoveAt_WithIndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            var list = new ListArray<int>();
            list.Add(10);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                list.RemoveAt(5);
            });
        }

        /// <summary>
        /// Тест метода UnionItems - добавляет только уникальные элементы (оптимизация с HashSet).
        /// </summary>
        [Fact]
        public void UnionItems_WithDuplicateItems_AddsOnlyUniqueItems()
        {
            var list = new ListArray<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            list.UnionItems(20, 30, 40, 50);

            Assert.Equal(5, list.Count);
            Assert.Equal(10, list[0]);
            Assert.Equal(20, list[1]);
            Assert.Equal(30, list[2]);
            Assert.Equal(40, list[3]);
            Assert.Equal(50, list[4]);
        }

        /// <summary>
        /// Тест метода UnionItems - работает корректно с пустой коллекцией.
        /// </summary>
        [Fact]
        public void UnionItems_WithEmptyCollection_AddsAllItems()
        {
            var list = new ListArray<int>();

            list.UnionItems(10, 20, 30);

            Assert.Equal(3, list.Count);
            Assert.Equal(10, list[0]);
            Assert.Equal(20, list[1]);
            Assert.Equal(30, list[2]);
        }

        /// <summary>
        /// Тест перечислителя ListArrayEnumerator - работает корректно с пустой коллекцией.
        /// </summary>
        [Fact]
        public void ListArrayEnumerator_WithEmptyCollection_DoesNotThrowException()
        {
            var list = new ListArray<int>();

            // Проверяем, что перечисление пустой коллекции не вызывает исключений
            var count = 0;
            foreach (var item in list)
            {
                count++;
            }

            Assert.Equal(0, count);
        }

        /// <summary>
        /// Тест перечислителя ListArrayEnumerator - Reset работает корректно с пустой коллекцией.
        /// </summary>
        [Fact]
        public void ListArrayEnumerator_ResetWithEmptyCollection_DoesNotThrowException()
        {
            var list = new ListArray<int>();
            var enumerator = list.GetEnumerator();

            // Reset должен работать без исключений даже для пустой коллекции
            enumerator.Reset();
            Assert.False(enumerator.MoveNext());
        }

        /// <summary>
        /// Тест перечислителя ListArrayEnumerator - корректно перечисляет элементы.
        /// </summary>
        [Fact]
        public void ListArrayEnumerator_EnumeratesAllItems()
        {
            var list = new ListArray<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            var items = new System.Collections.Generic.List<int>();
            foreach (var item in list)
            {
                items.Add(item);
            }

            Assert.Equal(3, items.Count);
            Assert.Equal(10, items[0]);
            Assert.Equal(20, items[1]);
            Assert.Equal(30, items[2]);
        }
    }
}
