using System;

namespace Lotus.Core.Helpers
{
    /// <summary>
    /// Тесты для критических участков ArrayHelper (исправление AddRange).
    /// </summary>
    public class ArrayHelperCriticalTests
    {
        /// <summary>
        /// Тест AddRange - критический баг был исправлен (array = items вместо array = newArray).
        /// </summary>
        [Fact]
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
            Assert.Equal(5, result.Length);
            Assert.Equal(5, currentCount);
            Assert.Equal(10, result[0]);
            Assert.Equal(20, result[1]);
            Assert.Equal(30, result[2]);
            Assert.Equal(40, result[3]);
            Assert.Equal(50, result[4]);
        }

        /// <summary>
        /// Тест AddRange - с достаточной емкостью массива.
        /// </summary>
        [Fact]
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
            Assert.Equal(10, result.Length);
            Assert.Equal(4, currentCount);
            Assert.Equal(10, result[0]);
            Assert.Equal(20, result[1]);
            Assert.Equal(30, result[2]);
            Assert.Equal(40, result[3]);
        }

        /// <summary>
        /// Тест AddRange - с пустым массивом элементов для добавления.
        /// </summary>
        [Fact]
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
            Assert.Equal(5, result.Length);
            Assert.Equal(2, currentCount);
            Assert.Equal(10, result[0]);
            Assert.Equal(20, result[1]);
        }

        /// <summary>
        /// Тест AddRange - с большим количеством элементов для добавления.
        /// </summary>
        [Fact]
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
            Assert.Equal(101, result.Length);
            Assert.Equal(101, currentCount);
            Assert.Equal(1, result[0]);
            Assert.Equal(2, result[1]);
            Assert.Equal(101, result[100]);
        }
    }
}
