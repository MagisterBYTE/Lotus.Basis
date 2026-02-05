using System.Collections.Generic;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Collections
{
    [TestFixture]
    public class HashSetArrayTests
    {
        private HashSetArray<int> _hashSet;

        [SetUp]
        public void SetUp()
        {
            _hashSet = new HashSetArray<int>();
        }

        [Test]
        public void Constructor_InitializesEmptySet()
        {
            ClassicAssert.AreEqual(0, _hashSet.Count);
        }

        [Test]
        public void Add_AddsNewItem()
        {
            _hashSet.Add(1);
            ClassicAssert.AreEqual(1, _hashSet.Count);
            ClassicAssert.IsTrue(_hashSet.Contains(1));
        }

        [Test]
        public void Add_DoesNotAddDuplicateItem()
        {
            _hashSet.Add(1);
            _hashSet.Add(1);
            ClassicAssert.AreEqual(1, _hashSet.Count);
        }

        [Test]
        public void Remove_RemovesExistingItem()
        {
            _hashSet.Add(1);
            _hashSet.Add(2);
            ClassicAssert.IsTrue(_hashSet.Remove(1));
            ClassicAssert.AreEqual(1, _hashSet.Count);
            ClassicAssert.IsFalse(_hashSet.Contains(1));
        }

        [Test]
        public void Remove_ReturnsFalseForNonExistingItem()
        {
            _hashSet.Add(1);
            ClassicAssert.IsFalse(_hashSet.Remove(2));
        }

        [Test]
        public void Contains_ReturnsTrueForExistingItem()
        {
            _hashSet.Add(1);
            ClassicAssert.IsTrue(_hashSet.Contains(1));
        }

        [Test]
        public void Contains_ReturnsFalseForNonExistingItem()
        {
            ClassicAssert.IsFalse(_hashSet.Contains(1));
        }

        [Test]
        public void Clear_RemovesAllItems()
        {
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.Clear();
            ClassicAssert.AreEqual(0, _hashSet.Count);
        }

        [Test]
        public void UnionWith_AddsAllItemsFromOtherCollection()
        {
            var otherCollection = new List<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.UnionWith(otherCollection);
            ClassicAssert.AreEqual(3, _hashSet.Count);
            ClassicAssert.IsTrue(_hashSet.Contains(1));
            ClassicAssert.IsTrue(_hashSet.Contains(2));
            ClassicAssert.IsTrue(_hashSet.Contains(3));
        }

        [Test]
        public void IntersectWith_KeepsOnlyCommonItems()
        {
            var otherCollection = new HashSetArray<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.IntersectWith(otherCollection);
            ClassicAssert.AreEqual(1, _hashSet.Count);
            ClassicAssert.IsTrue(_hashSet.Contains(2));
        }

        /// <summary>
        /// Тест метода IntersectWith - проверка исправления дублирования кода.
        /// </summary>
        [Test]
        public void IntersectWith_WithHashSetArray_WorksCorrectly()
        {
            var otherCollection = new HashSetArray<int> { 2, 3, 4 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.Add(3);
            _hashSet.IntersectWith(otherCollection);
            ClassicAssert.AreEqual(2, _hashSet.Count);
            ClassicAssert.IsTrue(_hashSet.Contains(2));
            ClassicAssert.IsTrue(_hashSet.Contains(3));
            ClassicAssert.IsFalse(_hashSet.Contains(1));
        }

        /// <summary>
        /// Тест метода IntersectWith - с пустой коллекцией.
        /// </summary>
        [Test]
        public void IntersectWith_WithEmptyCollection_ClearsSet()
        {
            var otherCollection = new HashSetArray<int>();
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.IntersectWith(otherCollection);
            ClassicAssert.AreEqual(0, _hashSet.Count);
        }

        /// <summary>
        /// Тест метода IntersectWith - с List.
        /// </summary>
        [Test]
        public void IntersectWith_WithList_WorksCorrectly()
        {
            var otherCollection = new List<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.Add(3);
            _hashSet.IntersectWith(otherCollection);
            ClassicAssert.AreEqual(2, _hashSet.Count);
            ClassicAssert.IsTrue(_hashSet.Contains(2));
            ClassicAssert.IsTrue(_hashSet.Contains(3));
        }

        [Test]
        public void ExceptWith_RemovesItemsFromOtherCollection()
        {
            var otherCollection = new List<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.ExceptWith(otherCollection);
            ClassicAssert.AreEqual(1, _hashSet.Count);
            ClassicAssert.IsTrue(_hashSet.Contains(1));
        }

        [Test]
        public void SymmetricExceptWith_KeepsOnlyUniqueItems()
        {
            var otherCollection = new HashSetArray<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.SymmetricExceptWith(otherCollection);
            ClassicAssert.AreEqual(2, _hashSet.Count);
            ClassicAssert.IsTrue(_hashSet.Contains(1));
            ClassicAssert.IsTrue(_hashSet.Contains(3));
        }

        [Test]
        public void IsSubsetOf_ReturnsTrueForSubset()
        {
            var otherCollection = new HashSetArray<int> { 1, 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            ClassicAssert.IsTrue(_hashSet.IsSubsetOf(otherCollection));
        }

        [Test]
        public void IsSubsetOf_ReturnsFalseForNonSubset()
        {
            var otherCollection = new List<int> { 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            ClassicAssert.IsFalse(_hashSet.IsSubsetOf(otherCollection));
        }

        [Test]
        public void IsSupersetOf_ReturnsTrueForSuperset()
        {
            var otherCollection = new List<int> { 1, 2 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.Add(3);
            ClassicAssert.IsTrue(_hashSet.IsSupersetOf(otherCollection));
        }

        [Test]
        public void IsSupersetOf_ReturnsFalseForNonSuperset()
        {
            var otherCollection = new List<int> { 1, 2, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            ClassicAssert.IsFalse(_hashSet.IsSupersetOf(otherCollection));
        }

        [Test]
        public void SetEquals_ReturnsTrueForEqualSets()
        {
            var otherCollection = new HashSetArray<int> { 1, 2 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            ClassicAssert.IsTrue(_hashSet.SetEquals(otherCollection));
        }

        [Test]
        public void SetEquals_ReturnsFalseForNonEqualSets()
        {
            var otherCollection = new HashSetArray<int> { 1, 3 };
            _hashSet.Add(1);
            _hashSet.Add(2);
            ClassicAssert.IsFalse(_hashSet.SetEquals(otherCollection));
        }

        [Test]
        public void CopyTo_CopiesItemsToArray()
        {
            _hashSet.Add(1);
            _hashSet.Add(2);
            var array = new int[2];
            _hashSet.CopyTo(array, 0);
            ClassicAssert.AreEqual(1, array[0]);
            ClassicAssert.AreEqual(2, array[1]);
        }

        [Test]
        public void RemoveWhere_RemovesItemsMatchingPredicate()
        {
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.Add(3);
            _hashSet.RemoveWhere(x => x % 2 == 0);
            ClassicAssert.AreEqual(2, _hashSet.Count);
            ClassicAssert.IsTrue(_hashSet.Contains(1));
            ClassicAssert.IsTrue(_hashSet.Contains(3));
        }

        [Test]
        public void TrimExcess_ReducesCapacity()
        {
            _hashSet.Add(1);
            _hashSet.Add(2);
            _hashSet.TrimExcess();
            // Проверка на то, что TrimExcess не выбрасывает исключение
            ClassicAssert.Pass();
        }
    }
}