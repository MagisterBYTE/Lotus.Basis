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
            var sparseSet = new SparseSet(1024);
            sparseSet.Add(15); // 0
            sparseSet.Add(20); // 1
            sparseSet.Add(25); // 2
            sparseSet.Add(30); // 3

            ClassicAssert.AreEqual(sparseSet.Contains(15), true);
            ClassicAssert.AreEqual(sparseSet.Contains(20), true);
            ClassicAssert.AreEqual(sparseSet.Contains(25), true);
            ClassicAssert.AreEqual(sparseSet.Contains(30), true);
            ClassicAssert.AreEqual(sparseSet.Count, 4);
            ClassicAssert.AreEqual(sparseSet.MaxCount, 1024);

            sparseSet.Remove(20);

            ClassicAssert.AreEqual(sparseSet.Contains(20), false);
            ClassicAssert.AreEqual(sparseSet[0], 15);
            ClassicAssert.AreEqual(sparseSet[1], 30);
            ClassicAssert.AreEqual(sparseSet[2], 25);

            ClassicAssert.AreEqual(sparseSet.Count, 3);
            ClassicAssert.AreEqual(sparseSet.MaxCount, 1024);

            sparseSet.Add(1024);
            ClassicAssert.AreEqual(sparseSet.Contains(1024), true);
            ClassicAssert.AreEqual(sparseSet.Count, 4);
            ClassicAssert.AreEqual(sparseSet.MaxCount, 2048);

            sparseSet.AddValues(233, 3666, 15, 5555, 66, 777);
            ClassicAssert.AreEqual(sparseSet.Contains(233), true);
            ClassicAssert.AreEqual(sparseSet.Contains(3666), true);
            ClassicAssert.AreEqual(sparseSet.Contains(5555), true);
            ClassicAssert.AreEqual(sparseSet.Contains(15), true);
            ClassicAssert.AreEqual(sparseSet.Contains(66), true);
            ClassicAssert.AreEqual(sparseSet.Contains(777), true);
            ClassicAssert.AreEqual(sparseSet.Count, 9);
            ClassicAssert.AreEqual(sparseSet.MaxCount, 8192);

            sparseSet.RemoveValues(233, 3666, 5555, 15, 66, 777);
            ClassicAssert.AreEqual(sparseSet.Count, 3);
            ClassicAssert.AreEqual(sparseSet.Contains(1024), true);
            ClassicAssert.AreEqual(sparseSet.Contains(25), true);
            ClassicAssert.AreEqual(sparseSet.Contains(30), true);
            ClassicAssert.AreEqual(sparseSet.MaxCount, 8192);
        }
    }
}