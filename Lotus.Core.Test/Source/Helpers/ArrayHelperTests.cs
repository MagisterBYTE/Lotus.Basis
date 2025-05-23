using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Helpers
{
    [TestFixture]
    public class ArrayHelperTests
    {
        #region Add Methods Tests

        [Test]
        public void Add_ShouldAddItemToEndOfArray()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3 };
            int itemToAdd = 4;

            // Act
            var result = XArrayHelper.Add(originalArray, itemToAdd);

            // Assert
            ClassicAssert.AreEqual(4, result.Length);
            ClassicAssert.AreEqual(4, result[3]);
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, result);
        }

        [Test]
        public void Add_WithRefCount_ShouldAddItemToEndOfArray()
        {
            // Arrange
            int[] originalArray = new int[4]; // Capacity 4
            int currentCount = 2;
            originalArray[0] = 1;
            originalArray[1] = 2;
            int itemToAdd = 3;

            // Act
            var result = XArrayHelper.Add(originalArray, ref currentCount, itemToAdd);

            // Assert
            ClassicAssert.AreEqual(4, result.Length); // Capacity remains same
            ClassicAssert.AreEqual(3, currentCount); // Count increased
            ClassicAssert.AreEqual(3, result[2]); // Item added at correct position
        }

        [Test]
        public void Add_WithRefCount_ShouldResizeWhenFull()
        {
            // Arrange
            int[] originalArray = new int[2]; // Capacity 2
            int currentCount = 2;
            originalArray[0] = 1;
            originalArray[1] = 2;
            int itemToAdd = 3;

            // Act
            var result = XArrayHelper.Add(originalArray, ref currentCount, itemToAdd);

            // Assert
            ClassicAssert.AreEqual(4, result.Length); // Capacity doubled
            ClassicAssert.AreEqual(3, currentCount);
            ClassicAssert.AreEqual(3, result[2]);
        }

        [Test]
        public void AddRange_ShouldAddMultipleItems()
        {
            // Arrange
            int[] originalArray = new int[5]; // Capacity 5
            int currentCount = 2;
            originalArray[0] = 1;
            originalArray[1] = 2;
            int[] itemsToAdd = { 3, 4 };

            // Act
            var result = XArrayHelper.AddRange(originalArray, ref currentCount, itemsToAdd);

            // Assert
            ClassicAssert.AreEqual(5, result.Length); // Capacity remains same
            ClassicAssert.AreEqual(4, currentCount);
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, result[0..4]);
        }

        #endregion

        #region Insert Methods Tests

        [Test]
        public void InsertAt_ShouldInsertItemAtSpecifiedIndex()
        {
            // Arrange
            int[] originalArray = { 1, 2, 4 };
            int itemToInsert = 3;

            // Act
            var result = XArrayHelper.InsertAt(originalArray, itemToInsert, 2);

            // Assert
            ClassicAssert.AreEqual(4, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, result);
        }

        [Test]
        public void InsertAt_WithMultipleItems_ShouldInsertItemsAtSpecifiedIndex()
        {
            // Arrange
            int[] originalArray = { 1, 4, 5 };
            int[] itemsToInsert = { 2, 3 };

            // Act
            var result = XArrayHelper.InsertAt(originalArray, 1, itemsToInsert);

            // Assert
            ClassicAssert.AreEqual(5, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5 }, result);
        }

        [Test]
        public void Push_ShouldInsertItemAtBeginning()
        {
            // Arrange
            int[] originalArray = { 2, 3, 4 };
            int itemToInsert = 1;

            // Act
            var result = XArrayHelper.Push(originalArray, itemToInsert);

            // Assert
            ClassicAssert.AreEqual(4, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, result);
        }

        #endregion

        #region Remove Methods Tests

        [Test]
        public void RemoveAt_ShouldRemoveItemAtIndex()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4 };

            // Act
            var result = XArrayHelper.RemoveAt(originalArray, 1);

            // Assert
            ClassicAssert.AreEqual(3, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 3, 4 }, result);
        }

        [Test]
        public void RemoveAt_WithCount_ShouldRemoveMultipleItems()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4, 5 };

            // Act
            var result = XArrayHelper.RemoveAt(originalArray, 1, 2);

            // Assert
            ClassicAssert.AreEqual(3, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 4, 5 }, result);
        }

        [Test]
        public void RemoveRange_ShouldRemoveItemsBetweenIndices()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4, 5 };

            // Act
            var result = XArrayHelper.RemoveRange(originalArray, 1, 3);

            // Assert
            ClassicAssert.AreEqual(2, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 5 }, result);
        }

        [Test]
        public void RemoveFirst_ShouldRemoveFirstItem()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3 };

            // Act
            var result = XArrayHelper.RemoveFirst(originalArray);

            // Assert
            ClassicAssert.AreEqual(2, result.Length);
            CollectionAssert.AreEqual(new[] { 2, 3 }, result);
        }

        [Test]
        public void RemoveFirst_WithCount_ShouldRemoveFirstItems()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4 };

            // Act
            var result = XArrayHelper.RemoveFirst(originalArray, 2);

            // Assert
            ClassicAssert.AreEqual(2, result.Length);
            CollectionAssert.AreEqual(new[] { 3, 4 }, result);
        }

        [Test]
        public void RemoveLast_ShouldRemoveLastItem()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3 };

            // Act
            var result = XArrayHelper.RemoveLast(originalArray);

            // Assert
            ClassicAssert.AreEqual(2, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 2 }, result);
        }

        [Test]
        public void RemoveLast_WithCount_ShouldRemoveLastItems()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4 };

            // Act
            var result = XArrayHelper.RemoveLast(originalArray, 2);

            // Assert
            ClassicAssert.AreEqual(2, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 2 }, result);
        }

        [Test]
        public void Remove_ShouldRemoveFirstMatchingItem()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 2, 4 };

            // Act
            var result = XArrayHelper.Remove(originalArray, 2);

            // Assert
            ClassicAssert.AreEqual(4, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 3, 2, 4 }, result);
        }

        [Test]
        public void RemoveAll_ShouldRemoveAllMatchingItems()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 2, 4 };

            // Act
            var result = XArrayHelper.RemoveAll(originalArray, 2);

            // Assert
            ClassicAssert.AreEqual(3, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 3, 4 }, result);
        }

        #endregion

        #region Shift Methods Tests

        [Test]
        public void Shift_ShouldMoveItemsRight()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4, 5 };

            // Act
            var result = XArrayHelper.Shift(originalArray, 1, 1, 2);

            // Assert
            CollectionAssert.AreEqual(new[] { 1, 4, 2, 3, 5 }, result);
        }

        [Test]
        public void Shift_ShouldMoveItemsLeft()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4, 5 };

            // Act
            var result = XArrayHelper.Shift(originalArray, 2, -1, 2);

            // Assert
            CollectionAssert.AreEqual(new[] { 1, 3, 4, 2, 5 }, result);
        }

        [Test]
        public void ShiftRight_ShouldMoveItemRight()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4 };

            // Act
            var result = XArrayHelper.ShiftRight(originalArray, 1);

            // Assert
            CollectionAssert.AreEqual(new[] { 1, 3, 2, 4 }, result);
        }

        [Test]
        public void ShiftLeft_ShouldMoveItemLeft()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4 };

            // Act
            var result = XArrayHelper.ShiftLeft(originalArray, 2);

            // Assert
            CollectionAssert.AreEqual(new[] { 1, 3, 2, 4 }, result);
        }

        #endregion

        #region Edge Cases Tests

        [Test]
        public void RemoveAt_WithInvalidIndex_ShouldReturnSameArray()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3 };

            // Act
            var result1 = XArrayHelper.RemoveAt(originalArray, -1);
            var result2 = XArrayHelper.RemoveAt(originalArray, 10);

            // Assert
            CollectionAssert.AreEqual(originalArray, result1);
            CollectionAssert.AreEqual(originalArray, result2);
        }

        [Test]
        public void RemoveAt_WithZeroCount_ShouldReturnSameArray()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3 };

            // Act
            var result = XArrayHelper.RemoveAt(originalArray, 1, 0);

            // Assert
            CollectionAssert.AreEqual(originalArray, result);
        }

        [Test]
        public void Shift_WithInvalidParameters_ShouldHandleGracefully()
        {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4, 5 };

            // Act
            var result1 = XArrayHelper.Shift(originalArray, -1, 1, 2); // Invalid start
            var result2 = XArrayHelper.Shift(originalArray, 1, 10, 2); // Large offset
            var result3 = XArrayHelper.Shift(originalArray, 1, -10, 2); // Large negative offset

            // Assert
            ClassicAssert.AreEqual(5, result1.Length);
            ClassicAssert.AreEqual(5, result2.Length);
            ClassicAssert.AreEqual(5, result3.Length);
        }

        #endregion
    }
}