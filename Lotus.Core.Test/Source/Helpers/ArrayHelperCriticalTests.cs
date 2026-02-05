using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Helpers
{
    /// <summary>
    /// Тесты для критических участков ArrayHelper (исправление AddRange).
    /// </summary>
    [TestFixture]
    public class ArrayHelperCriticalTests
    {
        /// <summary>
        /// Тест AddRange - критический баг был исправлен (array = items вместо array = new_array).
        /// </summary>
        [Test]
        public void AddRange_WithInsufficientCapacity_ResizesArrayCorrectly()
        {
            // Arrange - массив с недостаточной емкостью
            int[] originalArray = new int[2];
            int currentCount = 2;
            originalArray[0] = 10;
            originalArray[1] = 20;
            int[] itemsToAdd = { 30, 40, 50 }; // Нужно расширить массив

            // Act
            var result = XArrayHelper.AddRange(originalArray, ref currentCount, itemsToAdd);

            // Assert - проверяем что массив был корректно расширен (не заменен на items)
            ClassicAssert.AreEqual(5, result.Length, "Array should be resized to accommodate new items");
            ClassicAssert.AreEqual(5, currentCount, "Count should be updated");
            ClassicAssert.AreEqual(10, result[0], "Original item at index 0 should be preserved");
            ClassicAssert.AreEqual(20, result[1], "Original item at index 1 should be preserved");
            ClassicAssert.AreEqual(30, result[2], "New item at index 2 should be added");
            ClassicAssert.AreEqual(40, result[3], "New item at index 3 should be added");
            ClassicAssert.AreEqual(50, result[4], "New item at index 4 should be added");
        }

        /// <summary>
        /// Тест AddRange - с достаточной емкостью массива.
        /// </summary>
        [Test]
        public void AddRange_WithSufficientCapacity_DoesNotResize()
        {
            // Arrange - массив с достаточной емкостью
            int[] originalArray = new int[10];
            int currentCount = 2;
            originalArray[0] = 10;
            originalArray[1] = 20;
            int[] itemsToAdd = { 30, 40 };

            // Act
            var result = XArrayHelper.AddRange(originalArray, ref currentCount, itemsToAdd);

            // Assert
            ClassicAssert.AreEqual(10, result.Length, "Array should not be resized");
            ClassicAssert.AreEqual(4, currentCount, "Count should be updated");
            ClassicAssert.AreEqual(10, result[0], "Original item at index 0 should be preserved");
            ClassicAssert.AreEqual(20, result[1], "Original item at index 1 should be preserved");
            ClassicAssert.AreEqual(30, result[2], "New item at index 2 should be added");
            ClassicAssert.AreEqual(40, result[3], "New item at index 3 should be added");
        }

        /// <summary>
        /// Тест AddRange - с пустым массивом элементов для добавления.
        /// </summary>
        [Test]
        public void AddRange_WithEmptyItems_DoesNotChangeArray()
        {
            // Arrange
            int[] originalArray = new int[5];
            int currentCount = 2;
            originalArray[0] = 10;
            originalArray[1] = 20;
            int[] itemsToAdd = Array.Empty<int>();

            // Act
            var result = XArrayHelper.AddRange(originalArray, ref currentCount, itemsToAdd);

            // Assert
            ClassicAssert.AreEqual(5, result.Length, "Array length should remain unchanged");
            ClassicAssert.AreEqual(2, currentCount, "Count should remain unchanged");
            ClassicAssert.AreEqual(10, result[0], "Original item should be preserved");
            ClassicAssert.AreEqual(20, result[1], "Original item should be preserved");
        }

        /// <summary>
        /// Тест AddRange - с большим количеством элементов для добавления.
        /// </summary>
        [Test]
        public void AddRange_WithLargeNumberOfItems_ResizesCorrectly()
        {
            // Arrange
            int[] originalArray = new int[3];
            int currentCount = 1;
            originalArray[0] = 1;
            int[] itemsToAdd = new int[100];
            for (int i = 0; i < 100; i++)
            {
                itemsToAdd[i] = i + 2;
            }

            // Act
            var result = XArrayHelper.AddRange(originalArray, ref currentCount, itemsToAdd);

            // Assert
            ClassicAssert.AreEqual(101, result.Length, "Array should be resized to accommodate all items");
            ClassicAssert.AreEqual(101, currentCount, "Count should be updated");
            ClassicAssert.AreEqual(1, result[0], "Original item should be preserved");
            ClassicAssert.AreEqual(2, result[1], "First new item should be added");
            ClassicAssert.AreEqual(101, result[100], "Last new item should be added");
        }
    }
}
