#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Collections
{
    [TestFixture]
    public class SparseSetTests
    {
        /// <summary>
        /// Тестирование методов <see cref="SparseSet"/>.
        /// </summary>
        [Test]
        public void AllMethods()
        {
            var sparse_set = new SparseSet(1024);
            sparse_set.Add(15); // 0
            sparse_set.Add(20); // 1
            sparse_set.Add(25); // 2
            sparse_set.Add(30); // 3

            ClassicAssert.AreEqual(sparse_set.Contains(15), true);
            ClassicAssert.AreEqual(sparse_set.Contains(20), true);
            ClassicAssert.AreEqual(sparse_set.Contains(25), true);
            ClassicAssert.AreEqual(sparse_set.Contains(30), true);
            ClassicAssert.AreEqual(sparse_set.Count, 4);
            ClassicAssert.AreEqual(sparse_set.MaxCount, 1024);

            sparse_set.Remove(20);

            ClassicAssert.AreEqual(sparse_set.Contains(20), false);
            ClassicAssert.AreEqual(sparse_set[0], 15);
            ClassicAssert.AreEqual(sparse_set[1], 30);
            ClassicAssert.AreEqual(sparse_set[2], 25);

            ClassicAssert.AreEqual(sparse_set.Count, 3);
            ClassicAssert.AreEqual(sparse_set.MaxCount, 1024);

            sparse_set.Add(1024);
            ClassicAssert.AreEqual(sparse_set.Contains(1024), true);
            ClassicAssert.AreEqual(sparse_set.Count, 4);
            ClassicAssert.AreEqual(sparse_set.MaxCount, 2048);

            sparse_set.AddValues(233, 3666, 15, 5555, 66, 777);
            ClassicAssert.AreEqual(sparse_set.Contains(233), true);
            ClassicAssert.AreEqual(sparse_set.Contains(3666), true);
            ClassicAssert.AreEqual(sparse_set.Contains(5555), true);
            ClassicAssert.AreEqual(sparse_set.Contains(15), true);
            ClassicAssert.AreEqual(sparse_set.Contains(66), true);
            ClassicAssert.AreEqual(sparse_set.Contains(777), true);
            ClassicAssert.AreEqual(sparse_set.Count, 9);
            ClassicAssert.AreEqual(sparse_set.MaxCount, 8192);

            sparse_set.RemoveValues(233, 3666, 5555, 15, 66, 777);
            ClassicAssert.AreEqual(sparse_set.Count, 3);
            ClassicAssert.AreEqual(sparse_set.Contains(1024), true);
            ClassicAssert.AreEqual(sparse_set.Contains(25), true);
            ClassicAssert.AreEqual(sparse_set.Contains(30), true);
            ClassicAssert.AreEqual(sparse_set.MaxCount, 8192);
        }
    }
}