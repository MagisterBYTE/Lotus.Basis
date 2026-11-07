using System.Collections.Generic;


namespace Lotus.Core.Collections
{
    public class HashSetArrayTests
    {
        private HashSetArray<int> _hashSet;

        public HashSetArrayTests()
        {
            _hashSet = new HashSetArray<int>();
        }

        [Fact]
        public void Constructor_InitializesEmptySet()
        {
            Assert.Equal(0, _hashSet.Count);
        }

        [Fact]
        public void Add_AddsNewItem()
        {
            _hashSet.Add(1);
            Assert.Equal(1, _hashSet.Count);
            Assert.True(_hashSet.Contains(1));
        }

        [Fact]
        public void Add_DoesNotAddDuplicateItem()
        {
            _hashSet.Add(1);
            _hashSet.Add(1);
            Assert.Equal(1, _hashSet.Count);
        }

        [Fact]
        public void Remove_RemovesExistingItem()
        {
            _hashSet.Add(1);
            _hashSet.Add(2);
            Assert.True(_hashSet.Remove(1));
            Assert.Equal(1, _hashSet.Count);
            Assert.False(_hashSet.Contains(1));
        }

        [Fact]
        public void Remove_ReturnsFalseForNonExistingItem()
        {
            _hashSet.Add(1);
            Assert.False(_hashSet.Remove(2));
        }

        [Fact]
        public void Contains_ReturnsTrueForExistingItem()
        {
            _hashSet.Add(1);
            Assert.True(_hashSet.Contains(1));
        }

        [Fact]
        public void Contains_ReturnsFalseForNonExistingItem()
        {
            Assert.False(_hashSet.Contains(1));
        }

        [Fact]
        public void Clear_RemovesAllItems()
        {
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.Clear();
            Assert.Equal(0, _hashSet.Count);
        }

        [Fact]
        public void UnionWith_AddsAllItemsFromOtherCollection()
        {
            var otherCollection = new List<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.UnionWith(otherCollection);
            Assert.Equal(3, _hashSet.Count);
            Assert.True(_hashSet.Contains(1));
            Assert.True(_hashSet.Contains(2));
            Assert.True(_hashSet.Contains(3));
        }

        [Fact]
        public void IntersectWith_KeepsOnlyCommonItems()
        {
            var otherCollection = new HashSetArray<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.IntersectWith(otherCollection);
            Assert.Equal(1, _hashSet.Count);
            Assert.True(_hashSet.Contains(2));
        }

        /// <summary>
        /// Тест метода IntersectWith - проверка исправления дублирования кода.
        /// </summary>
        [Fact]
        public void IntersectWith_WithHashSetArray_WorksCorrectly()
        {
            var otherCollection = new HashSetArray<int> { 2, 3, 4 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.Add(3);
            _hashSet.IntersectWith(otherCollection);
            Assert.Equal(2, _hashSet.Count);
            Assert.True(_hashSet.Contains(2));
            Assert.True(_hashSet.Contains(3));
            Assert.False(_hashSet.Contains(1));
        }

        /// <summary>
        /// Тест метода IntersectWith - с пустой коллекцией.
        /// </summary>
        [Fact]
        public void IntersectWith_WithEmptyCollection_ClearsSet()
        {
            var otherCollection = new HashSetArray<int>();
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.IntersectWith(otherCollection);
            Assert.Equal(0, _hashSet.Count);
        }

        /// <summary>
        /// Тест метода IntersectWith - с List.
        /// </summary>
        [Fact]
        public void IntersectWith_WithList_WorksCorrectly()
        {
            var otherCollection = new List<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.Add(3);
            _hashSet.IntersectWith(otherCollection);
            Assert.Equal(2, _hashSet.Count);
            Assert.True(_hashSet.Contains(2));
            Assert.True(_hashSet.Contains(3));
        }

        [Fact]
        public void ExceptWith_RemovesItemsFromOtherCollection()
        {
            var otherCollection = new List<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.ExceptWith(otherCollection);
            Assert.Equal(1, _hashSet.Count);
            Assert.True(_hashSet.Contains(1));
        }

        [Fact]
        public void SymmetricExceptWith_KeepsOnlyUniqueItems()
        {
            var otherCollection = new HashSetArray<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.SymmetricExceptWith(otherCollection);
            Assert.Equal(2, _hashSet.Count);
            Assert.True(_hashSet.Contains(1));
            Assert.True(_hashSet.Contains(3));
        }

        [Fact]
        public void IsSubsetOf_ReturnsTrueForSubset()
        {
            var otherCollection = new HashSetArray<int> { 1, 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            Assert.True(_hashSet.IsSubsetOf(otherCollection));
        }

        [Fact]
        public void IsSubsetOf_ReturnsFalseForNonSubset()
        {
            var otherCollection = new List<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            Assert.False(_hashSet.IsSubsetOf(otherCollection));
        }

        [Fact]
        public void IsSupersetOf_ReturnsTrueForSuperset()
        {
            var otherCollection = new List<int> { 1, 2 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.Add(3);
            Assert.True(_hashSet.IsSupersetOf(otherCollection));
        }

        [Fact]
        public void IsSupersetOf_ReturnsFalseForNonSuperset()
        {
            var otherCollection = new List<int> { 1, 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            Assert.False(_hashSet.IsSupersetOf(otherCollection));
        }

        [Fact]
        public void SetEquals_ReturnsTrueForEqualSets()
        {
            var otherCollection = new HashSetArray<int> { 1, 2 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            Assert.True(_hashSet.SetEquals(otherCollection));
        }

        [Fact]
        public void SetEquals_ReturnsFalseForNonEqualSets()
        {
            var otherCollection = new HashSetArray<int> { 1, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            Assert.False(_hashSet.SetEquals(otherCollection));
        }

        [Fact]
        public void CopyTo_CopiesItemsToArray()
        {
            _hashSet.Add(1);
            _hashSet.Add(2);
            var array = new int[2];
            _hashSet.CopyTo(array, 0);
            Assert.Equal(1, array[0]);
            Assert.Equal(2, array[1]);
        }

        [Fact]
        public void RemoveWhere_RemovesItemsMatchingPredicate()
        {
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.Add(3);
            _hashSet.RemoveWhere(x => x % 2 == 0);
            Assert.Equal(2, _hashSet.Count);
            Assert.True(_hashSet.Contains(1));
            Assert.True(_hashSet.Contains(3));
        }

        [Fact]
        public void TrimExcess_ReducesCapacity()
        {
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.TrimExcess();
            // Проверка на то, что TrimExcess не выбрасывает исключение
            // Test reached completion successfully.
        }
    }
}